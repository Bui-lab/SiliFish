using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Services;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using SiliFish.UI.Extensions;
using System.Data;
using System.Text.Json;
using System.Drawing.Imaging;
using SiliFish.ModelUnits;
using SiliFish.Services.Plotting;
using SiliFish.Repositories;
using SiliFish.Services.Plotting.PlotSelection;
using SiliFish.UI.Services;
using SiliFish.Services.Dynamics;
using System.ComponentModel;
using SiliFish.ModelUnits.Parameters;

namespace SiliFish.UI.Controls
{
    public partial class ModelOutputControl : UserControl
    {
        class RCGridComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                DataGridViewRow row1 = (DataGridViewRow)x;
                DataGridViewRow row2 = (DataGridViewRow)y;
                //Sort by "Cell Group", "Train #", "Somite"
                string cellPool1 = row1.Cells["colRCTrainCellGroup"].Value.ToString();
                string cellPool2 = row2.Cells["colRCTrainCellGroup"].Value.ToString();
                int train1 = int.Parse(row1.Cells["colRCTrainNumber"].Value.ToString());
                int train2 = int.Parse(row2.Cells["colRCTrainNumber"].Value.ToString());
                int somite1 = int.Parse(row1.Cells["colRCTrainSomite"].Value.ToString());
                int somite2 = int.Parse(row2.Cells["colRCTrainSomite"].Value.ToString());
                int compareResult = string.Compare(cellPool1, cellPool2);
                if (compareResult == 0)
                    compareResult = train1.CompareTo(train2);
                if (compareResult == 0)
                    compareResult = somite1.CompareTo(somite2);

                return compareResult;
            }
        }
        string errorMessage;
        string htmlAnimation = "";
        string htmlPlot = "";
        decimal tAnimdt;
        int tAnimStart = 0;
        int tAnimEnd = 0;
        int tPlotStart = 0;
        int tPlotEnd = 0;
        int numOfPlots = 0;
        PlotDefinition lastPlot = null;
        PlotType PlotType = PlotType.MembPotential;
        PlotSelectionInterface plotSelection;
        private string tempFile;
        List<Chart> Charts;
        Simulation simulation = null;
        RunningModel model = null;
        bool rendered2D = false;
        bool rendered2DFull = false; //whether the 2D rendering is done by hiding in inactive nodes - will need rerendering
        bool rendered3D = false;
        TwoDRenderer TwoDRenderer = new();

        public ModelOutputControl()
        {
            InitializeComponent();
            WebViewInitializations();

            PopulatePlotTypes();
            SetEnablesBasedOnPlot();


            pictureBox.MouseWheel += PictureBox_MouseWheel;

            cellSelectionPlot.SelectionChanged += CellSelectionPlot_SelectionChanged;

            dd3DViewpoint.SelectedIndex = 0;

            ePlotEnd.Value = eSpikeEnd.Value = GlobalSettings.SimulationEndTime;

            try { ePlotEnd.Value = decimal.Parse(GlobalSettings.LastRunSettings["lTimeEnd"]); }
            catch { }
            if (tabPlotSub.TabPages.Contains(tPlotWindows))
                tabPlotSub.TabPages.Remove(tPlotWindows);
        }

        private void CellSelectionPlot_SelectionChanged(object sender, EventArgs e)
        {
            plotSelection = cellSelectionPlot.GetSelection();
            DisplayNumberOfPlots();
        }

        public void SetEnablesBasedOnPlot()
        {
            if (PlotType == PlotType.EpisodesTail)
            {
                cellSelectionPlot.Visible = false;
            }
            else if (PlotType == PlotType.EpisodesMN)
            {
                cellSelectionPlot.CombineOptionsVisible = false;
                cellSelectionPlot.Visible = true;
                cellSelectionPlot.TurnOnSingleCellOrSomite();
                cellSelectionPlot.Refresh();//the drop down boxes do not refresh properly otherwise
            }
            else if (PlotType.GetGroup() == "episode")
            {
                cellSelectionPlot.CombineOptionsVisible = false;
                cellSelectionPlot.Visible = true;
                cellSelectionPlot.TurnOnMultipleCellOrSomite();
                cellSelectionPlot.Refresh();//the drop down boxes do not refresh properly otherwise
            }
            else
            {
                cellSelectionPlot.Visible = true;
                cellSelectionPlot.ControlsEnabled = true;
                cellSelectionPlot.CombineOptionsVisible = true;
            }
        }
        public void SetRunningModel(Simulation simulation, RunningModel model)
        {
            if (model == null) return;
            this.simulation = simulation;
            this.model = model;
            cellSelectionPlot.RunningModel = model;
            cellSelectionSpike.RunningModel = model;
            PopulatePlotTypes();
            int NumberOfSomites = model.ModelDimensions.NumberOfSomites;
            if (NumberOfSomites > 0)
            {
                cb3DAllSomites.Visible = true;
                e3DSomiteRange.Visible = !cb3DAllSomites.Checked;
            }
            else
            {
                cb3DAllSomites.Visible = e3DSomiteRange.Visible = false;
            }
            GetLastPlotSettings();
        }
        private void SaveLastPlotSettings()
        {
            GlobalSettings.LastPlotSettings[ddPlot.Name] = ddPlot.Text;
            foreach (var kvp in cellSelectionPlot.GetSeLectionAsDictionary())
            {
                GlobalSettings.LastPlotSettings[kvp.Key] = kvp.Value;
            }
        }
        private void GetLastPlotSettings()
        {
            if (GlobalSettings.LastPlotSettings.Any())
            {
                try
                {
                    ddPlot.Text = GlobalSettings.LastPlotSettings[ddPlot.Name];
                    cellSelectionPlot.SetSelectionAsDictionary(GlobalSettings.LastPlotSettings);
                }
                catch { }
            }
        }
        public void CompleteRun(RunParam runParam)
        {
            decimal dt = (decimal)runParam.DeltaT;
            eAnimationdt.Minimum = dt;
            eAnimationdt.Increment = dt;
            eAnimationdt.Value = dt; //10 * dt;

            decimal tMax = (decimal)runParam.MaxTime;
            ePlotEnd.Value = eSpikeEnd.Value = tMax;
            eAnimationEnd.Value = tMax;

            cmPlot.Enabled =
                btnListSpikes.Enabled = btnListRCTrains.Enabled =
                btnPlotHTML.Enabled =
                btnAnimate.Enabled = true;
        }

        public void CancelRun()
        {
            cmPlot.Enabled =
                btnPlotHTML.Enabled =
                btnAnimate.Enabled = false;
        }
        #region webViewPlot
        private void WebViewInitializations()
        {
            InitAsync();
            Wait();
            webView2DRender.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
            webView3DRender.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
            webViewAnimation.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
            webViewPlot.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
            webViewRCTrains.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;

        }
        private async void InitAsync()
        {
            await webViewPlot.EnsureCoreWebView2Async();
            await webView2DRender.EnsureCoreWebView2Async();
            await webView3DRender.EnsureCoreWebView2Async();
            await webViewAnimation.EnsureCoreWebView2Async();
            await webViewRCTrains.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (webViewPlot.Tag == null ||
                webView2DRender.Tag == null || webView3DRender.Tag == null ||
                webViewAnimation.Tag == null || webViewRCTrains.Tag == null)
                Application.DoEvents();
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            (sender as Control).Tag = true;
        }

        private static void WarningMessage(string s)
        {
            MessageBox.Show(s, "Warning");
        }

        private void RegenerateWebview(WebView2 webView)
        {
            if (webView == null) return;
            string name = webView.Name;
            Control parent = webView.Parent;
            parent.Controls.Remove(webView);
            try { webView.Dispose(); }
            catch { }

            webView = new WebView2();

            (webView as System.ComponentModel.ISupportInitialize).BeginInit();
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 30);
            webView.Name = name;
            webView.Size = new Size(622, 481);
            webView.TabIndex = 1;
            webView.ZoomFactor = 1D;
            webView.CoreWebView2InitializationCompleted += new EventHandler<CoreWebView2InitializationCompletedEventArgs>(webView_CoreWebView2InitializationCompleted);

            (webView as System.ComponentModel.ISupportInitialize).EndInit();
            parent.Controls.Add(webView);
            webView.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;

        }
        private void CoreWebView2_ProcessFailed(object sender, CoreWebView2ProcessFailedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tempFile))
                { }
                else
                {
                    string target = (sender as CoreWebView2).DocumentTitle;
                    target = FileUtil.CopyFileToOutputFolder(tempFile, target);
                    RegenerateWebview(sender as WebView2);
                    Invoke(() => WarningMessage("There was a problem with displaying the html file. It is saved as " + target + "."));
                }
            }
            catch { }
        }
        private static async void ClearBrowserCache(WebView2 webView)
        {
            await webView.CoreWebView2.Profile.ClearBrowsingDataAsync();
        }
        private void cmiAnimationClearCache_Click(object sender, EventArgs e)
        {
            ClearBrowserCache(webViewAnimation);
        }
        #endregion

        #region Outputs
        #region 2D Rendering
        private void RenderIn2D(bool refresh)
        {
            if (model == null) return;

            //If the full rendering has never been done, a brand new 2D rendering need to be created rather than refreshing the old one
            if (refresh && !cb2DHideNonspiking.Checked && !rendered2DFull)
                refresh = false;
            List<CellPool> cellPools = model.CellPools;
            if (cb2DHideNonspiking.Checked && simulation!=null && simulation.SimulationRun)
                cellPools = model.CellPools.Where(cp => cp.Cells.Any(c => c.IsSpiking())).ToList();
            string html = TwoDRenderer.Create2DRendering(model, cellPools, refresh, webView2DRender.Width, webView2DRender.Height,
                showGap: cb2DGapJunc.Checked, showChem: cb2DChemJunc.Checked, offline: cb2DOffline.Checked);
            if (string.IsNullOrEmpty(html))
                return;
            bool navigated = false;
            webView2DRender.NavigateTo(html, "2DRendering", GlobalSettings.TempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
            rendered2D = true;
            if (!refresh)
            {
                rendered2DFull = !cb2DHideNonspiking.Checked;
            }
        }
        private void btn2DRender_Click(object sender, EventArgs e)
        {
            RenderIn2D(false);
        }
        private async void linkSaveHTML2D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (saveFileHTML.ShowDialog() == DialogResult.OK)
                {
                    string htmlHead = JsonSerializer.Deserialize<string>(await webView2DRender.ExecuteScriptAsync("document.head.outerHTML"));
                    string htmlBody = JsonSerializer.Deserialize<string>(await webView2DRender.ExecuteScriptAsync("document.body.outerHTML"));
                    string html = $@"<!DOCTYPE html> <html lang=""en"">{htmlHead}{htmlBody}</html>";
                    File.WriteAllText(saveFileHTML.FileName, html);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
            }
        }

        private async void cb2DChemJunc_CheckedChanged(object sender, EventArgs e)
        {
            if (cb2DChemJunc.Checked)
                await webView2DRender.ExecuteScriptAsync("ShowChemJunc();");
            else
                await webView2DRender.ExecuteScriptAsync("HideChemJunc();");
        }

        private async void cb2DGapJunc_CheckedChanged(object sender, EventArgs e)
        {
            if (cb2DGapJunc.Checked)
                await webView2DRender.ExecuteScriptAsync("ShowGapJunc();");
            else
                await webView2DRender.ExecuteScriptAsync("HideGapJunc();");
        }
        private async void cb2DShowUnselectedNodes_CheckedChanged(object sender, EventArgs e)
        {
            if (cb2DShowUnselectedNodes.Checked)
                await webView2DRender.ExecuteScriptAsync("ShowInactiveNodes();");
            else
                await webView2DRender.ExecuteScriptAsync("HideInactiveNodes();");
        }
        private void cb2DLegend_CheckedChanged(object sender, EventArgs e)
        {
            gr2DLegend.Visible = cb2DLegend.Checked;
        }
        private async void ud2DNodeSize_SelectedItemChanged(object sender, EventArgs e)
        {
            if (ud2DNodeSize.UpClick)
                await webView2DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(1.1);");
            if (ud2DNodeSize.DownClick)
                await webView2DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(0.9);");
        }

        private async void ud2DLinkSize_SelectedItemChanged(object sender, EventArgs e)
        {
            if (ud2DLinkSize.UpClick)
                await webView2DRender.ExecuteScriptAsync("SetLinkSizeMultiplier(1.1);");
            if (ud2DLinkSize.DownClick)
                await webView2DRender.ExecuteScriptAsync("SetLinkSizeMultiplier(0.9);");
        }
        private void cb2DRenderShowOptions_CheckedChanged(object sender, EventArgs e)
        {
            p2DRenderOptions.Visible = cb2DRenderShowOptions.Checked;
        }


        private void cb2DHideNonspiking_CheckedChanged(object sender, EventArgs e)
        {
            RenderIn2D(true);
        }


        #endregion

        #region 3D Rendering

        private void RenderIn3D()
        {
            try
            {
                ThreeDRenderer threeDRenderer = new();
                string html = threeDRenderer.RenderIn3D(model, model.CellPools,
                    somiteRange: cb3DAllSomites.Checked ? "All" : e3DSomiteRange.Text,
                    webView3DRender.Width, webView3DRender.Height,
                    showGap: cb3DGapJunc.Checked, showChem: cb3DChemJunc.Checked, offline: cb3DOffline.Checked);
                bool navigated = false;
                webView3DRender.NavigateTo(html, "3DRendering", GlobalSettings.TempFolder, ref tempFile, ref navigated);
                if (!navigated)
                    Warner.LargeFileWarning(tempFile);
                rendered3D = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btn3DRender_Click(object sender, EventArgs e)
        {
            RenderIn3D();
        }
        private async void cb3DAllSomites_CheckedChanged(object sender, EventArgs e)
        {
            if (model == null) return;
            e3DSomiteRange.Visible = !cb3DAllSomites.Checked;
            string func = $"SetSomites([]);";
            if (!cb3DAllSomites.Checked)
            {
                List<int> somites = Util.ParseRange(e3DSomiteRange.Text, 1, model.ModelDimensions.NumberOfSomites);
                func = $"SetSomites([{string.Join(',', somites)}]);";
            }
            await webView3DRender.ExecuteScriptAsync(func);
        }

        private string lastSomiteSelection;
        private void e3DSomiteRange_Enter(object sender, EventArgs e)
        {
            lastSomiteSelection = e3DSomiteRange.Text;
        }

        private async void e3DSomiteRange_Leave(object sender, EventArgs e)
        {
            if (model == null) return;
            if (lastSomiteSelection != e3DSomiteRange.Text)
            {
                List<int> somites = Util.ParseRange(e3DSomiteRange.Text, 1, model.ModelDimensions.NumberOfSomites);
                string func = $"SetSomites([{string.Join(',', somites)}]);";
                await webView3DRender.ExecuteScriptAsync(func);
            }
        }
        private async void linkSaveHTML3D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (saveFileHTML.ShowDialog() == DialogResult.OK)
                {
                    string htmlHead = JsonSerializer.Deserialize<string>(await webView3DRender.ExecuteScriptAsync("document.head.outerHTML"));
                    string htmlBody = JsonSerializer.Deserialize<string>(await webView3DRender.ExecuteScriptAsync("document.body.outerHTML"));
                    string html = $@"<!DOCTYPE html> <html lang=""en"">{htmlHead}{htmlBody}</html>";
                    File.WriteAllText(saveFileHTML.FileName, html);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
            }
        }

        private async void cb3DChemJunc_CheckedChanged(object sender, EventArgs e)
        {
            if (cb3DChemJunc.Checked)
                await webView3DRender.ExecuteScriptAsync("ShowChemJunc();");
            else
                await webView3DRender.ExecuteScriptAsync("HideChemJunc();");
        }

        private async void cb3DGapJunc_CheckedChanged(object sender, EventArgs e)
        {
            if (cb3DGapJunc.Checked)
                await webView3DRender.ExecuteScriptAsync("ShowGapJunc();");
            else
                await webView3DRender.ExecuteScriptAsync("HideGapJunc();");
        }
        private async void cb3DShowUnselectedNodes_CheckedChanged(object sender, EventArgs e)
        {
            if (cb3DShowUnselectedNodes.Checked)
                await webView3DRender.ExecuteScriptAsync("ShowInactiveNodes();");
            else
                await webView3DRender.ExecuteScriptAsync("HideInactiveNodes();");
        }
        private void cb3DLegend_CheckedChanged(object sender, EventArgs e)
        {
            gr3DLegend.Visible = cb3DLegend.Checked;
        }

        private async void dd3DViewpoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dd3DViewpoint.Text == "Dorsal view")
                await webView3DRender.ExecuteScriptAsync("DorsalView();");
            else if (dd3DViewpoint.Text == "Ventral view")
                await webView3DRender.ExecuteScriptAsync("VentralView();");
            else if (dd3DViewpoint.Text == "Rostral view")
                await webView3DRender.ExecuteScriptAsync("RostralView();");
            else if (dd3DViewpoint.Text == "Caudal view")
                await webView3DRender.ExecuteScriptAsync("CaudalView();");
            else if (dd3DViewpoint.Text == "Lateral view (left)")
                await webView3DRender.ExecuteScriptAsync("LateralLeftView();");
            else if (dd3DViewpoint.Text == "Lateral view (right)")
                await webView3DRender.ExecuteScriptAsync("LateralRightView();");
            else
                await webView3DRender.ExecuteScriptAsync("FreeView();");
        }

        private async void btnZoomOut_Click(object sender, EventArgs e)
        {
            await webView3DRender.ExecuteScriptAsync("ZoomOut();");
        }

        private async void btnZoomIn_Click(object sender, EventArgs e)
        {
            await webView3DRender.ExecuteScriptAsync("ZoomIn();");
        }
        internal async void Highlight(ModelUnitBase unitToPlot, bool force)
        {
            if (model == null) return;
            if (unitToPlot == null) return;
            if (tabOutputs.SelectedTab != t2DRender && tabOutputs.SelectedTab != t3DRender)
            {
                if (!force) return;
                if (unitToPlot is Cell)
                    tabOutputs.SelectedTab = t2DRender;
                else
                    tabOutputs.SelectedTab = t3DRender;
            }
            if (unitToPlot is CellPool pool)
            {
                if (tabOutputs.SelectedTab == t2DRender)
                    await webView2DRender.ExecuteScriptAsync($"SelectCellPool('{pool.ID}');");
                else
                    await webView3DRender.ExecuteScriptAsync($"SelectCellPool('{pool.ID}');");
            }
            else if (unitToPlot is Cell cell)
            {
                if (tabOutputs.SelectedTab == t2DRender)
                    await webView2DRender.ExecuteScriptAsync($"SelectCellPool('{cell.CellPool.ID}');");
                else
                    await webView3DRender.ExecuteScriptAsync($"SelectCell('{cell.ID}');");
            }

        }
        private async void ud3DNodeSize_SelectedItemChanged(object sender, EventArgs e)
        {
            if (ud3DNodeSize.UpClick)
                await webView3DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(1.1);");
            if (ud3DNodeSize.DownClick)
                await webView3DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(0.9);");
        }
        private void cb3DRenderShowOptions_CheckedChanged(object sender, EventArgs e)
        {
            p3DRenderOptions.Visible = cb3DRenderShowOptions.Checked;
        }
        #endregion
        #region HTML and Windows Plots - Common Functions

        /// <summary>
        /// hidden feature
        /// to reset the plot selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lPlotPlot_DoubleClick(object sender, EventArgs e)
        {

            ddPlot.SelectedIndex = 1;
            cellSelectionPlot.ResetSelection();
        }

        private void DisplayNumberOfPlots(List<Cell> Cells, List<CellPool> Pools)
        {
            numOfPlots = Cells?.Count ?? 0 + Pools?.Count ?? 0;
            if (Cells != null && Cells.Any())
            {
                if (plotSelection is PlotSelectionMultiCells multiCells)
                {
                    IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(Cells, multiCells.CombinePools, multiCells.CombineSomites, multiCells.CombineCells);
                    if (PlotType == PlotType.Stimuli)
                        numOfPlots = cellGroups.Count(cg => cg.Any(c => c.HasStimulus()));
                    else
                        numOfPlots = cellGroups.Count();
                }
                else
                {
                    if (PlotType == PlotType.Stimuli)
                        numOfPlots = Cells.Count(c => c.HasStimulus());
                    else
                        numOfPlots = Cells.Count;
                }
            }
            else if (Pools != null && Pools.Any())
            {
                if (PlotType == PlotType.Stimuli)
                    numOfPlots = Pools.Count(p => p.HasStimulus());
                else
                    numOfPlots = Pools.Count;
            }

            if (numOfPlots > 0)
            {
                if (PlotType == PlotType.FullDyn)
                    numOfPlots *= 5;
                else if (PlotType == PlotType.Current)
                    numOfPlots *= 3;
                else if (PlotType == PlotType.MembPotentialWithSpikeFreq)
                    numOfPlots *= 3;
                else if (PlotType.GetGroup() == "episode")
                {
                    if (PlotType == PlotType.EpisodesMN || PlotType == PlotType.EpisodesTail)
                        numOfPlots = 7;
                    else
                        numOfPlots = model.ModelDimensions.NumberOfSomites + 1;
                }
                toolTip.SetToolTip(btnPlotHTML, $"# of plots: {numOfPlots}");
            }
        }
        private void DisplayNumberOfPlots()
        {
            if (model == null) return;
            GlobalSettings.LastPlotSettings.Clear();//to prevent overwriting the plot changes during a run

            if (PlotType == PlotType.EpisodesTail || PlotType == PlotType.EpisodesMN)
            {
                numOfPlots = 7;
                toolTip.SetToolTip(btnPlotHTML, $"# of plots: Max {numOfPlots}");
                return;
            }
            GetPlotSubset();
            (List<Cell> Cells, List<CellPool> Pools) = model.GetSubsetCellsAndPools(cellSelectionPlot.PoolSubset, plotSelection);
            DisplayNumberOfPlots(Cells, Pools);
        }

        private void ddPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotType = ddPlot.Text.GetValueFromName(PlotType.NotSet);
            SetEnablesBasedOnPlot();

            toolTip.SetToolTip(ddPlot, PlotType.GetDescription());
            if (ddPlot.Focused)
                DisplayNumberOfPlots();
        }

        private void PopulatePlotTypes()
        {
            string lastSelection = ddPlot.Text;
            ddPlot.Items.Clear();
            foreach (PlotType pt in Enum.GetValues(typeof(PlotType)))
            {
                ddPlot.Items.Add(pt.GetDisplayName());
            }
            ddPlot.Text = lastSelection;
        }
        private void GetPlotSubset()
        {
            PlotType = ddPlot.Text.GetValueFromName(PlotType.NotSet);
            plotSelection = null;

            if (PlotType == PlotType.EpisodesMN)
            {
                plotSelection = new PlotSelectionSomite()
                {
                    Somite = cellSelectionPlot.GetSingleSomiteOrCell()
                };
            }
            else if (PlotType != PlotType.EpisodesTail)
            {
                plotSelection = cellSelectionPlot.GetSelection();
            }

            tPlotStart = (int)ePlotStart.Value;
            tPlotEnd = (int)ePlotEnd.Value;
            if (tPlotEnd > model.RunParam.MaxTime)
                tPlotEnd = model.RunParam.MaxTime;
        }
        private void linkExportPlotData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Charts.Any()) return;

            if (Charts.Count > 1)
            {
                string msg = "Every plot data will be saved as a separate CSV file. Plot names will be appended to the file name selected.";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            List<string> fileNames = new();
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                foreach (Chart chart in Charts)
                {
                    string title = chart.Title.Replace("'", "_").Replace("\"", "_").Replace("`", "_").Replace("/", "_");
                    string ext = Path.GetExtension(saveFileCSV.FileName);
                    string path = Path.GetDirectoryName(saveFileCSV.FileName);
                    string filename = Path.GetFileNameWithoutExtension(saveFileCSV.FileName);
                    filename = filename + title + ext;
                    FileUtil.SaveToFile(path + "\\" + filename, chart.CsvData);
                    fileNames.Add(filename);
                }
                MessageBox.Show($"File(s) {string.Join(", ", fileNames)} are saved.", "Information");
            }
        }
        private void listPlotHistory_ItemSelect(object sender, EventArgs e)
        {
            if (sender is not PlotDefinition plot) return;
            if (!simulation.SimulationRun) return;
            ddPlot.Text = plot.PlotType.GetDisplayName();
            plotSelection = plot.Selection;
            tPlotStart = (int)ePlotStart.Value;
            tPlotEnd = (int)ePlotEnd.Value;
            if (tPlotEnd > simulation.RunParam.MaxTime)
                tPlotEnd = simulation.RunParam.MaxTime;

            cellSelectionPlot.SetPlot(plot);
            if (plot.Selection is PlotSelectionUnits selectedUnits)
                SetEnablesBasedOnPlot();
            Task.Run(PlotHTML);
        }
        private void listPlotHistory_ItemEdit(object sender, EventArgs e)
        {
            if (sender is not PlotDefinition plot) return;
            int ind = listPlotHistory.SelectedIndex - 1;
            ControlContainer frmControl = new();
            RichTextBox richTextBox = new()
            {
                Text = JsonUtil.ToJson(plot)
            };
            frmControl.AddControl(richTextBox, null);
            frmControl.Text = "Edit";
            if (frmControl.ShowDialog() == DialogResult.OK)
            {
                string JSONString = richTextBox.Text;
                plot = (PlotDefinition)JsonUtil.ToObject(typeof(PlotDefinition), JSONString);
                model.LinkPlotObjects(plot);
                List<PlotDefinition> plots = listPlotHistory.GetItems<PlotDefinition>().ToList();
                plots[ind] = plot;
                listPlotHistory.SetItems(plots);
            }
        }
        private void listPlotHistory_ItemsExport(object sender, EventArgs e)
        {
            bool fileSaved = false;
            try
            {
                string json = JsonUtil.ToJson(listPlotHistory.GetItems<PlotDefinition>());
                if (saveFileJson.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileJson.FileName, json);
                    fileSaved = true;
                    FileUtil.ShowFile(saveFileJson.FileName);
                }
            }
            catch (Exception exc)
            {
                if (!fileSaved)
                    MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
                else MessageBox.Show($"File {saveFileJson.FileName} is saved.", "Information");
            }
        }

        private void listPlotHistory_ItemsImport(object sender, EventArgs e)
        {
            try
            {
                if (openFileJson.ShowDialog() == DialogResult.OK)
                {
                    string JSONString = FileUtil.ReadFromFile(openFileJson.FileName);
                    List<PlotDefinition> plotList = (List<PlotDefinition>)JsonUtil.ToObject(typeof(List<PlotDefinition>), JSONString);
                    if (plotList.Any(pl => pl.Selection is PlotSelectionUnits))
                    {
                        foreach (PlotDefinition plot in plotList)
                        {
                            model.LinkPlotObjects(plot);
                        }
                    }
                    //plotList.RemoveAll(pl => pl.Selection == null); this removes Episodes plot as well - why was it added in the first place?
                    listPlotHistory.SetItems(plotList);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in loading the file:" + exc.Message, "Error");
            }
        }
        private (string Title, List<Chart>) GenerateCharts()
        {
            if (simulation == null || model == null) return ("", null);

            (List<Cell> Cells, List<CellPool> Pools) = (null, null);
            string plotsubset = cellSelectionPlot.Visible ? cellSelectionPlot.PoolSubset : "";
            if (plotSelection is PlotSelectionMultiCells || plotSelection is PlotSelectionUnits)
            {
                int tSkip = simulation.RunParam.SkipDuration;
                double dt = simulation.RunParam.DeltaT;
                int iStart = (int)((tPlotStart + tSkip) / dt);
                int iEnd = (int)((tPlotEnd + tSkip) / dt);
                (Cells, Pools) = model.GetSubsetCellsAndPools(plotsubset, plotSelection, iStart, iEnd);
            }

            if (lastPlot != null && lastPlot.PlotType.Equals(PlotType) &&
                lastPlot.Selection is PlotSelectionMultiCells lpsm && plotSelection is PlotSelectionMultiCells psm)
            {
                if (PlotType.GetGroup() == "current" && lpsm.Equals(psm))
                {
                    psm.CombineJunctions = !lpsm.CombineJunctions;
                }
            }

            lastPlot = new()
            {
                PlotSubset = plotsubset,
                PlotType = PlotType,
                Selection = plotSelection
            };
            PlotGenerator PG = new();
            (string Title, Charts) = PG.GetPlotData(lastPlot, simulation.Model, Cells, Pools, tPlotStart, tPlotEnd);
            errorMessage = PG.errorMessage;
            return (Title, Charts);
        }

        private bool PrePlotCheck()
        {
            if (simulation == null || !simulation.SimulationRun) return false;
            SaveLastPlotSettings();

            GetPlotSubset();
            if (numOfPlots > GlobalSettings.PlotWarningNumber)
            {
                string msg = $"Plotting {numOfPlots} charts will use a lot of resources. Do you want to continue?\r\n" +
                    $"(You can set the number of plots that triggers this warning through the 'Settings' button above)";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return false;
            }
            if (!MainForm.currentPlotWarning &&
                !model.JunctionCurrentTrackingOn && (PlotType.GetGroup() == "current" || PlotType == PlotType.FullDyn))
            {
                MainForm.currentPlotWarning = true;
                string msg = $"Plotting current information (including Full Dynamics) will require to regenerate the current arrays for the whole simulation for the selected cells. " +
                    $"It can use a lot of memory. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return false;
            }
            if (PlotType.GetGroup() == "episode")
                model.SetAnimationParameters(model.KinemParam);
            return true;
        }
        #endregion

        #region HTML Plots
        private void PlotHTML()
        {
            htmlPlot = "";
            (string Title, Charts) = GenerateCharts();
            htmlPlot = DyChartGenerator.Plot(Title, Charts, GlobalSettings.ShowZeroValues,
                GlobalSettings.DefaultPlotWidth, GlobalSettings.DefaultPlotHeight);
            Invoke(CompletePlotHTML);
        }
        private void btnPlotHTML_Click(object sender, EventArgs e)
        {
            if (!PrePlotCheck())
                return;
            tabOutputs.SelectedTab = tPlot;
            tabPlotSub.SelectedTab = tPlotHTML;
            UseWaitCursor = true;
            cmPlot.Enabled = false;
            btnPlotHTML.Enabled = false;

            Task.Run(PlotHTML);
        }
        private void CompletePlotHTML()
        {
            if (!string.IsNullOrEmpty(errorMessage)) //TODO convert error message to enum - and add "(You can set the number of plots that triggers this warning through the 'Settings' button above)"
                MessageBox.Show(errorMessage, "Error");
            bool navigated = false;
            webViewPlot.NavigateTo(htmlPlot, PlotType.GetDisplayName(), GlobalSettings.TempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
            if (cbPlotHistory.Checked)
                listPlotHistory.AppendItem(lastPlot);
            UseWaitCursor = false;
            cmPlot.Enabled = true;
            btnPlotHTML.Enabled = true;
            linkSaveHTMLPlots.Enabled = true;
            linkExportPlotData.Enabled = true;
            toolTip.SetToolTip(btnPlotHTML, $"Last HTML plot: {DateTime.Now:t}");
        }
        private void linkSaveHTMLPlots_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (saveFileHTML.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileHTML.FileName, htmlPlot.ToString());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
            }
        }

        private void SetPlotSelectionToUnits(List<ModelUnitBase> unitList)
        {
            cellSelectionPlot.SelectedUnits = unitList;
            PlotType = ddPlot.Text.GetValueFromName(PlotType.NotSet);
            if (PlotType.GetGroup() == "episode")
            {
                PlotType = PlotType.MembPotential;
                ddPlot.SelectedItem = PlotType.GetDisplayName();
            }
            plotSelection = cellSelectionPlot.GetSelection();
            SetEnablesBasedOnPlot();
        }

        /// <summary>
        /// Called from the architecture panel
        /// The plot type is determined by the units selected
        /// Assumption: all the units have the same type
        /// Eg: Currently plotting a cell pool and a junction is not supported
        /// </summary>
        /// <param name="unitsToPlot"></param>
        internal void Plot(List<ModelUnitBase> unitsToPlot)
        {
            SetPlotSelectionToUnits(unitsToPlot);
            if (!PrePlotCheck()) return;

            tabOutputs.SelectedTab = tPlot;
            tabPlotSub.SelectedTab = tPlotHTML;
            UseWaitCursor = true;
            cmPlot.Enabled = false;
            btnPlotHTML.Enabled = false;

            SetEnablesBasedOnPlot();
            Task.Run(PlotHTML);
        }


        #endregion

        #region Windows Plot

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb.Image == null)
                return;
            if (pb.Tag == null)
            {
                pb.InitialImage = pb.Image;
                pb.Tag = 0;
            }
            if (e.Delta > 0)
                pb.Tag = (int)pb.Tag + 1;
            else
                pb.Tag = (int)pb.Tag - 1;
            double mult = 1 + 0.1 * (int)pb.Tag;
            if (mult < 0.1) return;
            Size sz = new((int)(pb.InitialImage.Width * mult), (int)(pb.InitialImage.Height * mult));
            pb.Image = new Bitmap(pb.InitialImage, sz);
        }

        private void PlotWindows()
        {
            try
            {
                if (!PrePlotCheck())
                    return;

                tabOutputs.SelectedTab = tPlot;
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = true;
                cmPlot.Enabled = false;
                btnPlotHTML.Enabled = false;

                (string Title, Charts) = GenerateCharts();

                List<Image> Images = WindowsPlotGenerator.PlotCharts(Charts);

                Images?.RemoveAll(img => img == null);

                int ncol = 1; // Images?.Count > 5 ? 2 : 1;
                int nrow = (int)Math.Ceiling((decimal)(Images?.Count ?? 0) / ncol);

                if (Images != null && Images.Any())
                {
                    pictureBox.Image = ImageHelperWindows.MergeImages(Images, nrow, ncol);
                }
                pictureBox.Tag = null;//for proper zooming it has to be reset
                toolTip.SetToolTip(cmPlot, $"Last plot: {DateTime.Now:t}");
                toolTip.SetToolTip(pictureBox, $"Last plot: {DateTime.Now:t}");
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = false;
                cmPlot.Enabled = true;
                btnPlotHTML.Enabled = true;
                cmiPlotImageSave.Enabled = true;
                if (!tabPlotSub.TabPages.Contains(tPlotWindows))
                    tabPlotSub.TabPages.Add(tPlotWindows);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void cmiNonInteractivePlot_Click(object sender, EventArgs e)
        {
            PlotWindows();
        }


        private void cmiPlotImageSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileImage.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(saveFileImage.FileName);
                    ImageFormat imgFormat = ImageFormat.Png;
                    pictureBox.Image?.Save(saveFileImage.FileName, imgFormat);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
            }
        }

        #endregion

        #region Spike Stats
        private void linkExportSpikes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UtilWindows.SaveTextFile(saveFileCSV, dgSpikeStats.ExportToStringBuilder().ToString());
        }

        private int AddCellToSpikeGrid(Cell cell)
        {
            dgSpikeStats.RowCount++;
            int rowIndex = dgSpikeStats.RowCount - 1;
            dgSpikeStats[colSpikeCell.Index, rowIndex].Value = cell.ID;
            dgSpikeStats[colSpikeSagittal.Index, rowIndex].Value = cell.PositionLeftRight;
            dgSpikeStats[colSpikeCellPool.Index, rowIndex].Value = cell.CellPool.CellGroup;
            dgSpikeStats[colSpikeSomite.Index, rowIndex].Value = cell.Somite;
            dgSpikeStats[colSpikeCellSeq.Index, rowIndex].Value = cell.Sequence;
            return rowIndex;
        }
        private void btnListSpikes_Click(object sender, EventArgs e)
        {
            if (simulation == null || !simulation.SimulationRun) return;
            UseWaitCursor = true;
            int iSpikeStart = simulation.RunParam.iIndex((double)eSpikeStart.Value);
            int iSpikeEnd = simulation.RunParam.iIndex((double)eSpikeEnd.Value);
            (List<Cell> Cells, List<CellPool> Pools) = model.GetSubsetCellsAndPools(cellSelectionSpike.PoolSubset, cellSelectionSpike.GetSelection(), iSpikeStart, iSpikeEnd);
            Cells ??= new();
            if (Pools != null)
                foreach (CellPool pool in Pools)
                    Cells.AddRange(pool.Cells);
            Dictionary<Cell, List<int>> cellSpikes = new();
            Cells.ForEach(c => cellSpikes.Add(c, c.GetSpikeIndices(iSpikeStart, iSpikeEnd)));
            int spikeCount = cellSpikes.Values.Sum(l => l.Count);
            if (spikeCount > 10 * GlobalSettings.MaxNumberOfUnits)
            {
                string msg = $"There are {spikeCount} spikes, which can take a while to list. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            dgSpikeStats.Rows.Clear();
            foreach (Cell cell in Cells)
            {
                List<int> spikes = cellSpikes[cell];
                foreach (int spikeIndex in spikes)
                {
                    int rowIndex = AddCellToSpikeGrid(cell);
                    dgSpikeStats[colSpikeTime.Index, rowIndex].Value = model.TimeArray[spikeIndex];
                }
            }
            linkExportSpikes.Enabled = true;
            tabSpikesRCTrains.SelectedTab = tSpikes;
            UseWaitCursor = false;
        }
        private void GenerateHistogramOfRCColumn(int colIndex)
        {
            double[] dataPoints = dgRCTrains.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[colIndex].Value != null)
                .Select(r => double.Parse(r.Cells[colIndex].Value.ToString()))
                .ToArray();
            if (!dataPoints.Any())
            {
                webViewRCTrains.NavigateToString("");
                return;
            }
            double mean = dataPoints.Average();
            double std = dataPoints.StandardDeviation();
            dataPoints = dataPoints.Select(dp => Math.Round(dp, 2)).ToArray();

            string title = $"{dgRCTrains.Columns[colIndex].HeaderText} {mean:0.##}±{std:0.##}";
            double width = webViewRCTrains.ClientSize.Width;
            double height = webViewRCTrains.ClientSize.Height - 50;
            string histHtml = HistogramGenerator.GenerateHistogram(dataPoints, title, width, height);
            bool navigated = false;
            webViewRCTrains.NavigateTo(histHtml, title, GlobalSettings.TempFolder, ref tempFile, ref navigated);
        }
        private void btnListRCTrains_Click(object sender, EventArgs e)
        {
            if (simulation == null || !simulation.SimulationRun) return;
            UseWaitCursor = true;
            (List<Cell> Cells, List<CellPool> Pools) = model.GetSubsetCellsAndPools(cellSelectionSpike.PoolSubset, cellSelectionSpike.GetSelection());
            Cells ??= new();
            if (Pools != null)
                foreach (CellPool pool in Pools)
                    Cells.AddRange(pool.Cells);
            int iSpikeStart = simulation.RunParam.iIndex((double)eSpikeStart.Value);
            int iSpikeEnd = simulation.RunParam.iIndex((double)eSpikeEnd.Value);

            List<string> pools = Cells.Select(c => c.CellPool.ID).Distinct().ToList();

            dgRCTrains.Rows.Clear();
            foreach (string pool in pools)
            {
                List<Cell> pooledCells = Cells.Where(c => c.CellPool.ID == pool).ToList();
                List<TrainOfBursts> burstTrains = SpikeDynamics.GenerateColumnsOfBursts(model.DynamicsParam, simulation.RunParam.DeltaT,
                    pooledCells, iSpikeStart, iSpikeEnd);
                foreach (TrainOfBursts burstTrain in burstTrains)
                {
                    foreach (var (iID, sID, Bursts) in burstTrain.BurstList)
                    {
                        dgRCTrains.RowCount++;
                        int rowIndex = dgRCTrains.RowCount - 1;
                        dgRCTrains[colRCTrainNumber.Index, rowIndex].Value = burstTrain.iID;
                        dgRCTrains[colRCTrainCellGroup.Index, rowIndex].Value = sID;
                        dgRCTrains[colRCTrainSomite.Index, rowIndex].Value = iID;
                        dgRCTrains[colRCTrainStart.Index, rowIndex].Value = Bursts.Start;
                        dgRCTrains[colRCTrainEnd.Index, rowIndex].Value = Bursts.End;
                        dgRCTrains[colRCTrainMidPoint.Index, rowIndex].Value = Bursts.Center;
                        dgRCTrains[colRCTrainCenter.Index, rowIndex].Value = Bursts.WeightedCenter;
                    }
                }
            }
            dgRCTrains.Sort(new RCGridComparer());
            for (int i = 1; i < dgRCTrains.RowCount; i++)
            {
                DataGridViewRow rowPrev = dgRCTrains.Rows[i - 1];
                DataGridViewRow rowCurrent = dgRCTrains.Rows[i];
                //Sort by "Cell Group", "Train #", "Somite"
                string cellPoolPrev = rowPrev.Cells[colRCTrainCellGroup.Index].Value.ToString();
                string cellPoolCurrent = rowCurrent.Cells[colRCTrainCellGroup.Index].Value.ToString();
                int trainPrev = int.Parse(rowPrev.Cells[colRCTrainNumber.Index].Value.ToString());
                int trainCurrent = int.Parse(rowCurrent.Cells[colRCTrainNumber.Index].Value.ToString());
                if (cellPoolPrev == cellPoolCurrent && trainPrev == trainCurrent)
                {
                    rowCurrent.Cells[colRCTrainStartDelay.Index].Value =
                        double.Parse(rowCurrent.Cells[colRCTrainStart.Index].Value.ToString()) -
                        double.Parse(rowPrev.Cells[colRCTrainStart.Index].Value.ToString());
                    rowCurrent.Cells[colRCTrainMidPointDelay.Index].Value =
                        double.Parse(rowCurrent.Cells[colRCTrainMidPoint.Index].Value.ToString()) -
                        double.Parse(rowPrev.Cells[colRCTrainMidPoint.Index].Value.ToString());
                    rowCurrent.Cells[colRCTrainCenterDelay.Index].Value =
                        double.Parse(rowCurrent.Cells[colRCTrainCenter.Index].Value.ToString()) -
                        double.Parse(rowPrev.Cells[colRCTrainCenter.Index].Value.ToString());
                }
            }
            linkExportRCTrains.Enabled = true;

            tabSpikesRCTrains.SelectedTab = tRCTrains;
            GenerateHistogramOfRCColumn(colRCTrainStartDelay.Index);
            UseWaitCursor = false;
        }
        private void linkExportRCTrains_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UtilWindows.SaveTextFile(saveFileCSV, dgRCTrains.ExportToStringBuilder().ToString());
        }

        private void dgRCTrains_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            if (colIndex == colRCTrainCenterDelay.Index ||
                colIndex == colRCTrainStartDelay.Index ||
                colIndex == colRCTrainMidPointDelay.Index)
                GenerateHistogramOfRCColumn(colIndex);
        }
        #endregion


        #region Animation
        int lastAnimationStartIndex;
        double[] lastAnimationTimeArray;
        Dictionary<string, Coordinate[]> lastAnimationSpineCoordinates;

        public object ModelGenerator { get; private set; }

        private void Animate()
        {
            try
            {
                if (simulation == null || !simulation.SimulationRun) return;
                htmlAnimation = AnimationGenerator.GenerateAnimation(simulation.Model, tAnimStart, tAnimEnd, (double)tAnimdt, out lastAnimationSpineCoordinates);
                Invoke(CompleteAnimation);
            }
            catch { Invoke(CancelAnimation); }
        }
        private void CompleteAnimation()
        {
            bool navigated = false;
            webViewAnimation.NavigateTo(htmlAnimation, "Animation", GlobalSettings.TempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
            linkSaveAnimationHTML.Enabled = linkSaveAnimationCSV.Enabled = true;
            lAnimationTime.Text = $"Last animation: {DateTime.Now:t}";
            btnAnimate.Enabled = true;
        }
        private void CancelAnimation()
        {
            webViewAnimation.NavigateToString("about:blank");
            linkSaveAnimationHTML.Enabled = linkSaveAnimationCSV.Enabled = false;
            lAnimationTime.Text = $"Last animation aborted.";
            btnAnimate.Enabled = true;
        }
        private void btnAnimate_Click(object sender, EventArgs e)
        {
            if (simulation == null || !simulation.SimulationRun) return;

            btnAnimate.Enabled = false;

            tAnimStart = (int)eAnimationStart.Value;
            tAnimEnd = (int)eAnimationEnd.Value;
            if (tAnimStart > simulation.RunParam.MaxTime || tAnimStart < 0)
                tAnimStart = 0;
            if (tAnimEnd > simulation.RunParam.MaxTime)
                tAnimEnd = simulation.RunParam.MaxTime;
            double dt = simulation.RunParam.DeltaT;
            lastAnimationStartIndex = (int)(tAnimStart / dt);
            int lastAnimationEndIndex = (int)(tAnimEnd / dt);
            lastAnimationTimeArray = model.TimeArray;
            model.SetAnimationParameters(model.KinemParam);

            tAnimdt = eAnimationdt.Value;
            Invoke(Animate);
        }
        private void linkSaveAnimationHTML_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(htmlAnimation))
                return;
            if (saveFileHTML.ShowDialog() != DialogResult.OK)
                return;
            File.WriteAllText(saveFileHTML.FileName, htmlAnimation.ToString());
        }
        private void linkSaveAnimationCSV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(htmlAnimation))
                return;
            if (saveFileCSV.ShowDialog() != DialogResult.OK)
                return;

            ModelFile.SaveAnimation(saveFileCSV.FileName, lastAnimationSpineCoordinates, lastAnimationTimeArray, lastAnimationStartIndex);
        }

        #endregion

        #endregion


        private void tabOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabOutputs.SelectedTab == t2DRender && !rendered2D)
                RenderIn2D(false);
            else if (tabOutputs.SelectedTab == t3DRender && !rendered3D)
                RenderIn3D();
        }

        private void ModelOutputControl_Load(object sender, EventArgs e)
        {
            tabOutputs_SelectedIndexChanged(sender, e);
        }


    }

}
