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
using SiliFish.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Imaging;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Stim;
using SiliFish.ModelUnits.Junction;
using SiliFish.Services.Plotting;
using SiliFish.Repositories;

namespace SiliFish.UI.Controls
{
    public partial class ModelOutputControl : UserControl
    {
        string htmlAnimation = "";
        string htmlPlot = "";
        string PlotSubset = "";
        decimal tAnimdt;
        int tAnimStart = 0;
        int tAnimEnd = 0;
        int tPlotStart = 0;
        int tPlotEnd = 0;
        int numOfPlots = 0;
        PlotType PlotType = PlotType.MembPotential;
        PlotSelectionInterface plotCellSelection;
        private string tempFile;
        List<ChartDataStruct> LastPlottedCharts;
        RunningModel RunningModel = null;
        bool rendered2D = false;

        private bool PlotSelectionVisible
        {
            get
            {
                if (pPlotSelection.Tag is bool visible)
                    return visible;
                return true;
            }
            set
            {
                pPlotSelection.Visible = cbCombineCells.Visible = cbCombineSomites.Visible = value;
                pPlotSelection.Tag = value;
                //The visible property can return false if the form is not in the visible field, regardless of this value. So use the tag field instead
            }
        }
        public ModelOutputControl()
        {
            InitializeComponent();
            WebViewInitializations();
            splitPlotWindows.SplitterDistance = splitPlotWindows.Width / 2;

            foreach (PlotType pt in Enum.GetValues(typeof(PlotType)))
            {
                if (pt == PlotType.Junction) continue;
                ddPlot.Items.Add(pt.GetDisplayName());
            }

            PlotSelectionVisible = true;
            foreach (PlotSelection ps in Enum.GetValues(typeof(PlotSelection)))
            {
                ddPlotSomiteSelection.Items.Add(ps.GetDisplayName());
                ddPlotCellSelection.Items.Add(ps.GetDisplayName());
            }
            foreach (SagittalPlane sp in Enum.GetValues(typeof(SagittalPlane)))
            {
                ddPlotSagittal.Items.Add(sp.GetDisplayName());
            }
            ddPlotSagittal.SelectedIndex = ddPlotSagittal.Items.Count - 1;
            ddPlotSomiteSelection.SelectedIndex = 0;
            ddPlotCellSelection.SelectedIndex = 0;

            pictureBoxLeft.MouseWheel += PictureBox_MouseWheel;
            pictureBoxRight.MouseWheel += PictureBox_MouseWheel;

            dd3DViewpoint.SelectedIndex = 0;

            ePlotWidth.Value = GlobalSettings.DefaultPlotWidth;
            ePlotHeight.Value = GlobalSettings.DefaultPlotHeight;
            try { ePlotEnd.Value = decimal.Parse(GlobalSettings.LastRunSettings["lTimeEnd"]); }
            catch { }
            if (tabPlotSub.TabPages.Contains(tPlotWindows))
                tabPlotSub.TabPages.Remove(tPlotWindows);
        }

        public void SetRunningModel(RunningModel model)
        {
            if (model == null) return;
            RunningModel = model;
            PopulatePlotPools();
            int NumberOfSomites = RunningModel.ModelDimensions.NumberOfSomites;
            ddPlotSomiteSelection.Enabled = ePlotSomiteSelection.Enabled = NumberOfSomites > 0;
            if (NumberOfSomites > 0)
            {
                eKinematicsSomite.Maximum = NumberOfSomites;
                lSomitesNumMNDynamics.Text = "Somite #";
            }
            else
            {
                eKinematicsSomite.Maximum = RunningModel.CellPools.Any() ?
                    RunningModel.CellPools.Max(p => p.GetMaxCellSequence()) : 0;
                lSomitesNumMNDynamics.Text = "Cell Seq.";
            }
            GetLastPlotSettings();
        }
        private void SaveLastPlotSettings()
        {
            GlobalSettings.LastPlotSettings[ddPlot.Name] = ddPlot.Text;
            GlobalSettings.LastPlotSettings[ddPlotSagittal.Name] = ddPlotSagittal.Text;
            GlobalSettings.LastPlotSettings[ddPlotPools.Name] = ddPlotPools.Text;
            GlobalSettings.LastPlotSettings[ddPlotSomiteSelection.Name] = ddPlotSomiteSelection.Text;
            GlobalSettings.LastPlotSettings[ddPlotCellSelection.Name] = ddPlotCellSelection.Text;
            GlobalSettings.LastPlotSettings[ePlotSomiteSelection.Name] = ePlotSomiteSelection.Value.ToString();
            GlobalSettings.LastPlotSettings[ePlotCellSelection.Name] = ePlotCellSelection.Value.ToString();
            GlobalSettings.LastPlotSettings[cbCombinePools.Name] = cbCombinePools.Checked.ToString();
            GlobalSettings.LastPlotSettings[cbCombineSomites.Name] = cbCombineSomites.Checked.ToString();
            GlobalSettings.LastPlotSettings[cbCombineCells.Name] = cbCombineCells.Checked.ToString();
        }
        private void GetLastPlotSettings()
        {
            if (GlobalSettings.LastPlotSettings.Any())
            {
                try
                {
                    ddPlot.Text = GlobalSettings.LastPlotSettings[ddPlot.Name];
                    ddPlotSagittal.Text = GlobalSettings.LastPlotSettings[ddPlotSagittal.Name];
                    ddPlotPools.Text = GlobalSettings.LastPlotSettings[ddPlotPools.Name];
                    ddPlotSomiteSelection.Text = GlobalSettings.LastPlotSettings[ddPlotSomiteSelection.Name];
                    ddPlotCellSelection.Text = GlobalSettings.LastPlotSettings[ddPlotCellSelection.Name];
                    ePlotSomiteSelection.SetValue(double.Parse(GlobalSettings.LastPlotSettings[ePlotSomiteSelection.Name]));
                    ePlotCellSelection.SetValue(double.Parse(GlobalSettings.LastPlotSettings[ePlotCellSelection.Name]));
                    if (bool.TryParse(GlobalSettings.LastPlotSettings[cbCombinePools.Name], out bool b1))
                        cbCombinePools.Checked = b1;
                    if (bool.TryParse(GlobalSettings.LastPlotSettings[cbCombineSomites.Name], out bool b2))
                        cbCombineSomites.Checked = b2;
                    if (bool.TryParse(GlobalSettings.LastPlotSettings[cbCombineCells.Name], out bool b3))
                        cbCombineCells.Checked = b3;
                }
                catch { }
            }
        }
        public void CompleteRun()
        {
            decimal dt = (decimal)RunningModel.RunParam.DeltaT;
            eAnimationdt.Minimum = dt;
            eAnimationdt.Increment = dt;
            eAnimationdt.Value = dt; //10 * dt;

            decimal tMax = (decimal)RunningModel.RunParam.MaxTime;
            ePlotEnd.Value = tMax;
            eAnimationEnd.Value = tMax;

            cmPlot.Enabled =
                btnPlotHTML.Enabled =
                btnAnimate.Enabled =
                btnGenerateEpisodes.Enabled = true;
        }

        public void CancelRun()
        {
            cmPlot.Enabled =
                btnPlotHTML.Enabled =
                btnAnimate.Enabled =
                btnGenerateEpisodes.Enabled = false;
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
            webViewSummaryV.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
        }
        private async void InitAsync()
        {
            await webViewPlot.EnsureCoreWebView2Async();
            await webView2DRender.EnsureCoreWebView2Async();
            await webView3DRender.EnsureCoreWebView2Async();
            await webViewAnimation.EnsureCoreWebView2Async();
            await webViewSummaryV.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (webViewPlot.Tag == null ||
                webView2DRender.Tag == null || webView3DRender.Tag == null ||
                webViewAnimation.Tag == null || webViewSummaryV.Tag == null)
                Application.DoEvents();
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            (sender as Control).Tag = true;
        }

        private static void WarningMessage(string s)
        {
            MessageBox.Show(s);
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
                    string prefix = Path.GetFileNameWithoutExtension(target);
                    target = Path.Combine(GlobalSettings.OutputFolder, prefix + ".html");
                    int suffix = 0;
                    while (File.Exists(target))
                        target = Path.Combine(GlobalSettings.OutputFolder, prefix + (suffix++).ToString() + ".html");
                    File.Copy(tempFile, target);
                    RegenerateWebview(sender as WebView2);
                    Invoke(() => WarningMessage("There was a problem with displaying the html file. It is saved as " + target + "."));
                }
            }
            catch { }
        }
        private async void ClearBrowserCache(WebView2 webView)
        {
            await webView.CoreWebView2.Profile.ClearBrowsingDataAsync();
        }
        private void cmiAnimationClearCache_Click(object sender, EventArgs e)
        {
            ClearBrowserCache(webViewAnimation);
        }
        #endregion

        #region Outputs

        #region HTML and Windows Plots - Common Functions

        private void DisplayNumberOfPlots(List<Cell> Cells, List<CellPool> Pools)
        {
            numOfPlots = Cells?.Count ?? 0 + Pools?.Count ?? 0;
            if (Cells != null && Cells.Any())
            {
                if (plotCellSelection is PlotSelectionMultiCells multiCells)
                {
                    IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(Cells, multiCells.combinePools, multiCells.combineSomites, multiCells.combineCells);
                    if (PlotType == PlotType.Stimuli)
                        numOfPlots = cellGroups.Count(cg => cg.Count(c => c.HasStimulus()) > 0);
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
                toolTip.SetToolTip(btnPlotHTML, $"# of plots: {numOfPlots}");
            }
        }
        private void DisplayNumberOfPlots()
        {
            if (RunningModel == null) return;
            GlobalSettings.LastPlotSettings.Clear();//to prevent overwriting the plot changes during a run
            if (PlotType == PlotType.Junction)
            {
                numOfPlots = 3;
                toolTip.SetToolTip(btnPlotHTML, $"# of plots: {numOfPlots}");
                return;
            }
            if (PlotType == PlotType.Episodes)
            {
                numOfPlots = 7;
                toolTip.SetToolTip(btnPlotHTML, $"# of plots: Max {numOfPlots}");
                return;
            }
            GetPlotSubset();
            (List<Cell> Cells, List<CellPool> Pools) = RunningModel.GetSubsetCellsAndPools(PlotSubset, (PlotSelectionMultiCells)plotCellSelection);
            DisplayNumberOfPlots(Cells, Pools);
        }
        private void ddPlotPools_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddPlotPools.SelectedIndex < 0 || ddPlotSagittal.SelectedIndex < 0)
                return;
            if (plotCellSelection is PlotSelectionMultiCells multiCells)
            {
                GetPlotSubset();
                (List<Cell> Cells, List<CellPool> Pools) = RunningModel.GetSubsetCellsAndPools(PlotSubset, multiCells);
                PlotSelectionMultiCells selForMaxNumbers = new()
                {
                    Pools = multiCells.Pools,
                    SagittalPlane = multiCells.SagittalPlane,
                    cellSelection = PlotSelection.All,
                    somiteSelection = PlotSelection.All,
                    combineCells = false,
                    combineSomites = false,
                    combinePools = false
                };
                (List<Cell> CellsFull, List<CellPool> PoolsFull) = RunningModel.GetSubsetCellsAndPools(PlotSubset, selForMaxNumbers);

                ePlotSomiteSelection.Maximum =
                    PoolsFull != null && PoolsFull.Any() ? PoolsFull.Max(p => p.GetMaxCellSomite()) :
                    CellsFull != null && CellsFull.Any() ? CellsFull.Max(c => c.Somite) : 0;
                ePlotCellSelection.Maximum =
                    PoolsFull != null && PoolsFull.Any() ? PoolsFull.Max(p => p.GetMaxCellSequence()) :
                    CellsFull != null && CellsFull.Any() ? CellsFull.Max(c => c.Sequence) : 0;
                if (ddPlotPools.Focused)
                    DisplayNumberOfPlots(Cells, Pools);
            }
            else if (ddPlotPools.Focused)
                DisplayNumberOfPlots();

        }

        private void ddPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotType = ddPlot.Text.GetValueFromName(PlotType.NotSet);
            PlotSelectionVisible = PlotType != PlotType.Junction && PlotType != PlotType.Episodes;

            toolTip.SetToolTip(ddPlot, PlotType.GetDescription());
            if (ddPlot.Focused)
                DisplayNumberOfPlots();
        }

        private void ddPlot_DropDown(object sender, EventArgs e)
        {
            if (ddPlot.Items.Contains(PlotType.Junction.GetDisplayName()))
                ddPlot.Items.Remove(PlotType.Junction.GetDisplayName());

        }

        private void ddPlotSomiteSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddPlotSomiteSelection.SelectedIndex < 0) return;
            PlotSelection sel = ddPlotSomiteSelection.Text.GetValueFromName<PlotSelection>(PlotSelection.All);
            ePlotSomiteSelection.Enabled = sel == PlotSelection.Random || sel == PlotSelection.Single;
            if (ddPlotSomiteSelection.Focused)
                DisplayNumberOfPlots();
        }

        private void ddPlotCellSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddPlotCellSelection.SelectedIndex < 0) return;
            PlotSelection sel = ddPlotCellSelection.Text.GetValueFromName<PlotSelection>(PlotSelection.All);
            ePlotCellSelection.Enabled = sel == PlotSelection.Random || sel == PlotSelection.Single;
            if (ddPlotCellSelection.Focused)
                DisplayNumberOfPlots();
        }

        private void ddPlotSagittal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddPlotSagittal.Focused)
                DisplayNumberOfPlots();
        }

        private void ePlotCellSelection_ValueChanged(object sender, EventArgs e)
        {
            if (ePlotCellSelection.Focused)
                DisplayNumberOfPlots();
        }

        private void ePlotSomiteSelection_ValueChanged(object sender, EventArgs e)
        {
            if (ePlotSomiteSelection.Focused)
                DisplayNumberOfPlots();
        }

        private void cbCombineSomites_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCombineSomites.Focused)
            {
                if (cbCombineSomites.Checked)
                    cbCombineCells.Checked = true;
                DisplayNumberOfPlots();
            }
        }

        private void cbCombineCells_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCombineCells.Focused)
            {
                if (!cbCombineCells.Checked && (cbCombinePools.Checked || cbCombineSomites.Checked))
                    cbCombineCells.Checked = true; //has to be combined
                else
                    DisplayNumberOfPlots();
            }
        }
        private void cbCombinePools_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCombinePools.Focused)
                DisplayNumberOfPlots();
        }
        private void PopulatePlotPools()
        {
            string prevSelection = ddPlotPools.Text;
            ddPlotPools.Items.Clear();
            ddPlotPools.Text = "";
            if (RunningModel == null || !RunningModel.CellPools.Any()) return;
            List<string> itemList = new()
            {
                Const.AllPools,
                Const.SupraSpinal,
                Const.MuscleCells,
                Const.Motoneurons,
                Const.Interneurons,
                Const.Neurons,
                Const.ExcitatoryNeurons,
                Const.InhibitoryNeurons
            };
            itemList.AddRange(RunningModel.CellPools.Where(cp => cp.Active).Select(p => p.CellGroup).OrderBy(p => p).ToArray());

            ddPlotPools.Items.AddRange(itemList.Distinct().ToArray());
            if (ddPlotPools.Items?.Count > 0)
            {
                ddPlotPools.Text = prevSelection;
                if (ddPlotPools.SelectedIndex < 0)
                    ddPlotPools.SelectedIndex = 0;
            }
        }
        private void GetPlotSubset()
        {
            PlotType = ddPlot.Text.GetValueFromName<PlotType>(PlotType.NotSet);
            PlotSubset = ddPlotPools.Text;

            if (PlotSelectionVisible)
            {
                plotCellSelection = new PlotSelectionMultiCells()
                {
                    SagittalPlane = ddPlotSagittal.Text.GetValueFromName(SagittalPlane.Both),
                    somiteSelection = ddPlotSomiteSelection.Text.GetValueFromName(PlotSelection.All),
                    nSomite = (int)ePlotSomiteSelection.Value,
                    cellSelection = ddPlotCellSelection.Text.GetValueFromName(PlotSelection.All),
                    nCell = (int)ePlotCellSelection.Value,
                    combineCells = cbCombineCells.Checked,
                    combineSomites = cbCombineSomites.Checked,
                    combinePools = cbCombinePools.Checked
                };
            }

            tPlotStart = (int)ePlotStart.Value;
            tPlotEnd = (int)ePlotEnd.Value;
            if (tPlotEnd > RunningModel.RunParam.MaxTime)
                tPlotEnd = RunningModel.RunParam.MaxTime;
        }
        private void linkExportPlotData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!LastPlottedCharts.Any()) return;

            if (LastPlottedCharts.Count > 1)
            {
                string msg = "Every plot data will be saved as a separate CSV file. File names are appended to ";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            List<string> fileNames = new();
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                foreach (ChartDataStruct chart in LastPlottedCharts)
                {
                    string title = chart.Title.Replace("'", "").Replace("\"", "").Replace("`", "");
                    string ext = Path.GetExtension(saveFileCSV.FileName);
                    string path = Path.GetDirectoryName(saveFileCSV.FileName);
                    string filename = Path.GetFileNameWithoutExtension(saveFileCSV.FileName);
                    filename = filename + title + ext;
                    FileUtil.SaveToFile(path + "\\" + filename, chart.CsvData);
                    fileNames.Add(filename);
                }
            }
            MessageBox.Show($"File(s) {string.Join(",", fileNames)} are saved.");
        }
        #endregion

        #region HTML Plots
        private void PlotHTML()
        {
            if (RunningModel == null) return;
            htmlPlot = "";

            (List<Cell> Cells, List<CellPool> Pools) =
                !PlotSelectionVisible ? (null, null) :
                RunningModel.GetSubsetCellsAndPools(PlotSubset, (PlotSelectionMultiCells)plotCellSelection);
            (string Title, LastPlottedCharts) = PlotDataGenerator.GetPlotData(PlotType, RunningModel, Cells, Pools, plotCellSelection, tPlotStart, tPlotEnd);

            htmlPlot = DyChartGenerator.Plot(Title, LastPlottedCharts,
                (int)ePlotWidth.Value, (int)ePlotHeight.Value);
            Invoke(CompletePlotHTML);
        }
        private void btnPlotHTML_Click(object sender, EventArgs e)
        {
            if (RunningModel == null || !RunningModel.ModelRun) return;
            SaveLastPlotSettings();

            GetPlotSubset();
            if (numOfPlots > GlobalSettings.PlotWarningNumber)
            {
                string msg = $"Plotting {numOfPlots} charts will use a lot of resources. Do you want to continue?\r\n" +
                    $"(You can set the number of plots that triggers this warning through the 'Settings' button above)";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            if (PlotType == PlotType.Episodes)
                RunningModel.SetAnimationParameters(RunningModel.KinemParam);
            tabOutputs.SelectedTab = tPlot;
            tabPlotSub.SelectedTab = tPlotHTML;
            UseWaitCursor = true;
            cmPlot.Enabled = false;
            btnPlotHTML.Enabled = false;

            Task.Run(PlotHTML);
        }
        private void CompletePlotHTML()
        {
            webViewPlot.NavigateTo(htmlPlot, GlobalSettings.TempFolder, ref tempFile);
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
                MessageBox.Show("There is a problem in saving the file:" + exc.Message);
            }
        }

        private void SelectPlotSelectionOfCellPool(CellPool pool)
        {
            ddPlotPools.SelectedItem = pool.CellGroup;
            ddPlotSagittal.SelectedItem = pool.PositionLeftRight.ToString();
            ddPlotSomiteSelection.SelectedItem = "All";
            if (!Equals(ddPlotCellSelection.SelectedItem, "All"))
                cbCombineCells.Checked = true;
        }
        private void SelectPlotSelectionOfCell(Cell cell)
        {
            ddPlotPools.SelectedItem = cell.CellGroup;
            ddPlotSagittal.SelectedItem = cell.CellPool.PositionLeftRight.ToString();
            ddPlotSomiteSelection.SelectedItem = "Single";
            ePlotSomiteSelection.SetValue(cell.Somite);
            ddPlotCellSelection.SelectedItem = "Single";
            ePlotCellSelection.SetValue(cell.Sequence);
        }
        internal void Plot(ModelUnitBase unitToPlot)
        {
            if (RunningModel == null || !RunningModel.ModelRun) return;
            if (unitToPlot == null) return;
            if (unitToPlot is JunctionBase jnc)
            {
                if (!ddPlot.Items.Contains(PlotType.Junction.GetDisplayName()))
                    ddPlot.Items.Add(PlotType.Junction.GetDisplayName());
                ddPlot.SelectedItem = PlotType.Junction.GetDisplayName();
                PlotType = ddPlot.Text.GetValueFromName(PlotType.NotSet);
                PlotSubset = ddPlotPools.Text;
                plotCellSelection = new PlotSelectionJunction()
                {
                    Junction = jnc
                };

                tPlotStart = (int)ePlotStart.Value;
                tPlotEnd = (int)ePlotEnd.Value;
                if (tPlotEnd > RunningModel.RunParam.MaxTime)
                    tPlotEnd = RunningModel.RunParam.MaxTime;
            }
            else
            {
                if (unitToPlot is Stimulus stim)
                {
                    ddPlot.SelectedItem = PlotType.Stimuli.GetDisplayName();
                    SelectPlotSelectionOfCell(stim.TargetCell);
                }
                else if (unitToPlot is CellPool pool)
                {
                    if (ddPlot.Text.GetValueFromName(PlotType.NotSet) == PlotType.Episodes)
                        ddPlot.SelectedItem = PlotType.MembPotential.GetDisplayName();
                    SelectPlotSelectionOfCellPool(pool);
                }
                else if (unitToPlot is Cell cell)
                {
                    ddPlot.SelectedItem = PlotType.FullDyn.GetDisplayName();
                    SelectPlotSelectionOfCell(cell);
                }
                SaveLastPlotSettings();
                GetPlotSubset();
            }
            tabOutputs.SelectedTab = tPlot;
            tabPlotSub.SelectedTab = tPlotHTML;
            UseWaitCursor = true;
            cmPlot.Enabled = false;
            btnPlotHTML.Enabled = false;

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
                if (RunningModel == null || !RunningModel.ModelRun) return;
                SaveLastPlotSettings();

                GetPlotSubset();
                if (PlotType == PlotType.Episodes)
                    RunningModel.SetAnimationParameters(RunningModel.KinemParam);
                tabOutputs.SelectedTab = tPlot;
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = true;
                cmPlot.Enabled = false;
                btnPlotHTML.Enabled = false;

                if (plotCellSelection is not PlotSelectionMultiCells)
                    return;

                (List<Cell> Cells, List<CellPool> Pools) = RunningModel.GetSubsetCellsAndPools(PlotSubset, (PlotSelectionMultiCells)plotCellSelection);

                (List<Image> leftImages, List<Image> rightImages) = WindowsPlotGenerator.Plot(PlotType, RunningModel, Cells, Pools, (PlotSelectionMultiCells)plotCellSelection,
                    tPlotStart, tPlotEnd);

                leftImages?.RemoveAll(img => img == null);
                rightImages?.RemoveAll(img => img == null);

                int ncol = leftImages?.Count > 5 ? 2 : 1;
                int nrow = (int)Math.Ceiling((decimal)(leftImages?.Count ?? 0) / ncol);

                if (leftImages != null && leftImages.Any())
                {
                    pictureBoxLeft.Image = ImageHelperWindows.MergeImages(leftImages, nrow, ncol);
                    splitPlotWindows.Panel1Collapsed = false;
                }
                else
                {
                    pictureBoxLeft.Image = null;
                    splitPlotWindows.Panel1Collapsed = true;
                }
                if (rightImages != null && rightImages.Any())
                {
                    ncol = rightImages?.Count > 5 ? 2 : 1;
                    nrow = (int)Math.Ceiling((decimal)(rightImages?.Count ?? 0) / ncol);
                    pictureBoxRight.Image = ImageHelperWindows.MergeImages(rightImages, nrow, ncol);
                    splitPlotWindows.Panel2Collapsed = false;
                    splitPlotWindows.SplitterDistance = splitPlotWindows.Width / 2;
                }
                else
                {
                    pictureBoxRight.Image = null;
                    splitPlotWindows.Panel2Collapsed = true;
                }
                pictureBoxLeft.Tag = null;//for proper zooming it has to be reset
                pictureBoxRight.Tag = null;//for proper zooming it has to be reset
                toolTip.SetToolTip(cmPlot, $"Last plot: {DateTime.Now:t}");
                toolTip.SetToolTip(pictureBoxLeft, $"Last plot: {DateTime.Now:t}");
                toolTip.SetToolTip(pictureBoxRight, $"Last plot: {DateTime.Now:t}");
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
        private void btnPlotWindows_Click(object sender, EventArgs e)
        {
            PlotWindows();
        }

        private void cmiNonInteractivePlot_Click(object sender, EventArgs e)
        {
            PlotWindows();
        }


        private void linkSavePlots_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void cmiPlotImageSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileImage.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(saveFileImage.FileName);
                    ImageFormat imgFormat = ImageFormat.Png;
                    string leftFile = saveFileImage.FileName;
                    string rightFile = saveFileImage.FileName;

                    if (pictureBoxLeft.Image != null && pictureBoxRight.Image != null)
                    {
                        string s = Path.GetFileNameWithoutExtension(leftFile);
                        string path = Path.GetDirectoryName(leftFile);
                        leftFile = $"{path}\\{s}_Left{ext}";
                        rightFile = $"{path}\\{s}_Right{ext}";
                    }
                    pictureBoxLeft.Image?.Save(leftFile, imgFormat);
                    pictureBoxRight.Image?.Save(rightFile, imgFormat);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in saving the file:" + exc.Message);
            }
        }

        #endregion

        #region 2D Rendering
        private void RenderIn2D()
        {
            if (RunningModel == null) return;
            TwoDRenderer modelGenerator = new();
            string html = modelGenerator.Create2DRendering(RunningModel, RunningModel.CellPools, (int)webView2DRender.Width, webView2DRender.Height,
                showGap: cb2DGapJunc.Checked, showChem: cb2DChemJunc.Checked);
            webView2DRender.NavigateTo(html, GlobalSettings.TempFolder, ref tempFile);
            rendered2D = true;

        }
        private void btn2DRender_Click(object sender, EventArgs e)
        {
            RenderIn2D();
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
                MessageBox.Show("There is a problem in saving the file:" + exc.Message);
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
        private void cb2DLegend_CheckedChanged(object sender, EventArgs e)
        {
            gr2DLegend.Visible = cb2DLegend.Checked;
        }
        #endregion

        #region 3D Rendering

        private void RenderIn3D()
        {
            try
            {
                ThreeDRenderer threeDRenderer = new();
                string html = threeDRenderer.RenderIn3D(RunningModel, RunningModel.CellPools,
                    somiteRange: cb3DAllSomites.Checked ? "All" : e3DSomiteRange.Text,
                    webView3DRender.Width, webView3DRender.Height,
                    showGap: cb3DGapJunc.Checked, showChem: cb3DChemJunc.Checked);
                webView3DRender.NavigateTo(html, GlobalSettings.TempFolder, ref tempFile);
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
            if (RunningModel == null) return;
            e3DSomiteRange.Visible = !cb3DAllSomites.Checked;
            string func = $"SetSomites([]);";
            if (!cb3DAllSomites.Checked)
            {
                List<int> somites = Util.ParseRange(e3DSomiteRange.Text, 1, RunningModel.ModelDimensions.NumberOfSomites);
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
            if (RunningModel == null) return;
            if (lastSomiteSelection != e3DSomiteRange.Text)
            {
                List<int> somites = Util.ParseRange(e3DSomiteRange.Text, 1, RunningModel.ModelDimensions.NumberOfSomites);
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
                MessageBox.Show("There is a problem in saving the file:" + exc.Message);
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
        internal async void Highlight(ModelUnitBase unitToPlot)
        {
            if (RunningModel == null) return;
            if (unitToPlot == null) return;
            if (unitToPlot is CellPool pool)
            {
                await webView3DRender.ExecuteScriptAsync($"SelectCellPool('{pool.CellGroup}');");
            }
            else if (unitToPlot is Cell cell)
            {
                await webView3DRender.ExecuteScriptAsync($"SelectCell('{cell.ID}');");
            }
            tabOutputs.SelectedTab = t3DRender;
        }
        #endregion

        #region Animation
        int lastAnimationStartIndex;
        double[] lastAnimationTimeArray;
        Dictionary<string, Coordinate[]> lastAnimationSpineCoordinates;

        private void Animate()
        {
            try
            {
                if (RunningModel == null || !RunningModel.ModelRun) return;
                htmlAnimation = AnimationGenerator.GenerateAnimation(RunningModel, tAnimStart, tAnimEnd, (double)tAnimdt, out lastAnimationSpineCoordinates);
                Invoke(CompleteAnimation);
            }
            catch { Invoke(CancelAnimation); }
        }
        private void CompleteAnimation()
        {
            webViewAnimation.NavigateTo(htmlAnimation, GlobalSettings.TempFolder, ref tempFile);
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
            if (RunningModel == null || !RunningModel.ModelRun) return;

            btnAnimate.Enabled = false;

            tAnimStart = (int)eAnimationStart.Value;
            tAnimEnd = (int)eAnimationEnd.Value;
            if (tAnimStart > RunningModel.RunParam.MaxTime || tAnimStart < 0)
                tAnimStart = 0;
            if (tAnimEnd > RunningModel.RunParam.MaxTime)
                tAnimEnd = RunningModel.RunParam.MaxTime;
            double dt = RunningModel.RunParam.DeltaT;
            lastAnimationStartIndex = (int)(tAnimStart / dt);
            int lastAnimationEndIndex = (int)(tAnimEnd / dt);
            lastAnimationTimeArray = RunningModel.TimeArray;
            RunningModel.SetAnimationParameters(RunningModel.KinemParam);

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

        #region MN Based Kinematics

        private void btnGenerateEpisodes_Click(object sender, EventArgs e)
        {
            if (RunningModel == null || !RunningModel.ModelRun) return;

            (List<Cell> LeftMNs, List<Cell> RightMNs) = RunningModel.GetMotoNeurons((int)eKinematicsSomite.Value);
            (List<SwimmingEpisode> episodesLeft, List<SwimmingEpisode> episodesRight) =
                SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(RunningModel, LeftMNs, RightMNs);
            string html = DyChartGenerator.PlotSummaryMembranePotentials(RunningModel, LeftMNs.Union(RightMNs).ToList(),
                width: (int)ePlotKinematicsWidth.Value, height: (int)ePlotKinematicsHeight.Value);
            webViewSummaryV.NavigateTo(html, GlobalSettings.TempFolder, ref tempFile);
            eEpisodesLeft.Text = "";
            eEpisodesRight.Text = "";
            foreach (SwimmingEpisode episode in episodesLeft)
                eEpisodesLeft.Text += episode.ToString() + "\r\n\r\n";
            foreach (SwimmingEpisode episode in episodesRight)
                eEpisodesRight.Text += episode.ToString() + "\r\n\r\n";
            lKinematicsTimes.Text = $"Last kinematics:{DateTime.Now:t}";
        }
        private void splitKinematics_Panel2_SizeChanged(object sender, EventArgs e)
        {
            eEpisodesLeft.Width = splitKinematics.Panel2.Width / 2;
        }

        #endregion
        #endregion

        private async void udNodeSize_SelectedItemChanged(object sender, EventArgs e)
        {
            if (udNodeSize.UpClick)
                await webView3DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(1.1);");
            if (udNodeSize.DownClick)
                await webView3DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(0.9);");
        }

        private void tabOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabOutputs.SelectedTab == t2DRender && !rendered2D)
                RenderIn2D();
        }


    }

}
