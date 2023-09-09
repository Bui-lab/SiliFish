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
using System.Diagnostics;
using SiliFish.UI.Services;

namespace SiliFish.UI.Controls
{
    public partial class ModelOutputControl : UserControl
    {
        bool currentPlotWarning = false;
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
        List<Chart> LastPlottedCharts;
        RunningModel RunningModel = null;
        bool rendered2D = false;
        bool rendered3D = false;


        public ModelOutputControl()
        {
            InitializeComponent();
            WebViewInitializations();
            splitPlotWindows.SplitterDistance = splitPlotWindows.Width / 2;

            PopulatePlotTypes();
            SetEnablesBasedOnPlot();


            pictureBoxLeft.MouseWheel += PictureBox_MouseWheel;
            pictureBoxRight.MouseWheel += PictureBox_MouseWheel;

            cellSelectionPlot.SelectionChanged += CellSelectionPlot_SelectionChanged;

            dd3DViewpoint.SelectedIndex = 0;

            ePlotEnd.Value = GlobalSettings.SimulationEndTime;

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
            else
            {
                cellSelectionPlot.Visible = true;
                cellSelectionPlot.ControlsEnabled = true;
                cellSelectionPlot.CombineOptionsVisible = true;
            }
        }
        public void SetRunningModel(RunningModel model)
        {
            if (model == null) return;
            RunningModel = model;
            cellSelectionPlot.RunningModel = model;
            cellSelectionSpike.RunningModel = model;
            PopulatePlotTypes();
            int NumberOfSomites = RunningModel.ModelDimensions.NumberOfSomites;
            if (NumberOfSomites > 0)
            {
                eKinematicsSomite.Maximum = NumberOfSomites;
                lSomitesNumMNDynamics.Text = "Somite #";
                cb3DAllSomites.Visible = true;
                e3DSomiteRange.Visible = !cb3DAllSomites.Checked;
            }
            else
            {
                eKinematicsSomite.Maximum = RunningModel.CellPools.Any() ?
                    RunningModel.CellPools.Max(p => p.GetMaxCellSequence()) : 0;
                lSomitesNumMNDynamics.Text = "Cell Seq.";
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
                btnListSpikes.Enabled =
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
                if (plotSelection is PlotSelectionMultiCells multiCells)
                {
                    IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(Cells, multiCells.CombinePools, multiCells.CombineSomites, multiCells.CombineCells);
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

            if (PlotType == PlotType.EpisodesTail || PlotType == PlotType.EpisodesMN)
            {
                numOfPlots = 7;
                toolTip.SetToolTip(btnPlotHTML, $"# of plots: Max {numOfPlots}");
                return;
            }
            GetPlotSubset();
            (List<Cell> Cells, List<CellPool> Pools) = RunningModel.GetSubsetCellsAndPools(cellSelectionPlot.PoolSubset, plotSelection);
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
            if (tPlotEnd > RunningModel.RunParam.MaxTime)
                tPlotEnd = RunningModel.RunParam.MaxTime;
        }
        private void linkExportPlotData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!LastPlottedCharts.Any()) return;

            if (LastPlottedCharts.Count > 1)
            {
                string msg = "Every plot data will be saved as a separate CSV file. Plot names will be appended to the file name selected.";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            List<string> fileNames = new();
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                foreach (Chart chart in LastPlottedCharts)
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
            ddPlot.Text = plot.PlotType.GetDisplayName();
            plotSelection = plot.Selection;
            tPlotStart = (int)ePlotStart.Value;
            tPlotEnd = (int)ePlotEnd.Value;
            if (tPlotEnd > RunningModel.RunParam.MaxTime)
                tPlotEnd = RunningModel.RunParam.MaxTime;

            cellSelectionPlot.SetPlot(plot);
            if (plot.Selection is PlotSelectionUnits selectedUnits)
                SetEnablesBasedOnPlot();
            Task.Run(PlotHTML);
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
                    Process p = new()
                    {
                        StartInfo = new ProcessStartInfo(saveFileJson.FileName)
                        {
                            UseShellExecute = true
                        }
                    };
                    p.Start();
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
                            if (plot.Selection is PlotSelectionUnits plotSelection)
                            {
                                PlotSelectionUnits linkedSelection = new(plotSelection);
                                foreach (Tuple<string, string> unitTag in plotSelection.UnitTags)
                                {
                                    ModelUnitBase unit = null;
                                    if (unitTag.Item1 == "CellPool")
                                        unit = RunningModel.GetCellPools().FirstOrDefault(cp => cp.ID == unitTag.Item2);
                                    else if (unitTag.Item1 == "Neuron" || unitTag.Item1 == "MuscleCell")
                                        unit = RunningModel.GetCells().FirstOrDefault(cp => cp.ID == unitTag.Item2);
                                    else if (unitTag.Item1 == "ChemicalSynapse")
                                        unit = RunningModel.GetChemicalProjections().FirstOrDefault(cp => cp.ID == unitTag.Item2);
                                    else if (unitTag.Item1 == "GapJunction")
                                        unit = RunningModel.GetGapProjections().FirstOrDefault(cp => cp.ID == unitTag.Item2);
                                    else
                                    { }
                                    if (unit != null)
                                        linkedSelection.AddUnit(unit);
                                }
                                if (linkedSelection.Units?.Count > 0)
                                    plot.Selection = linkedSelection;
                                else plot.Selection = null;
                            }
                        }
                    }
                    plotList.RemoveAll(pl => pl.Selection == null);
                    listPlotHistory.SetItems(plotList);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in loading the file:" + exc.Message, "Error");
            }
        }
        #endregion

        #region HTML Plots
        private void PlotHTML()
        {
            if (RunningModel == null) return;
            htmlPlot = "";

            (List<Cell> Cells, List<CellPool> Pools) = (null, null);
            string plotsubset = cellSelectionPlot.PoolSubset;
            if (plotSelection is PlotSelectionMultiCells || plotSelection is PlotSelectionUnits)
            {
                (Cells, Pools) = RunningModel.GetSubsetCellsAndPools(plotsubset, plotSelection);
            }
            if (plotsubset == "Selection" && PlotType.GetGroup() == "episode")
                PlotType = PlotType.MembPotential;
            if (lastPlot != null &&
                lastPlot.PlotSubset.Equals(plotsubset) &&
                lastPlot.PlotType.Equals(PlotType) &&
                lastPlot.Selection != null && lastPlot.Selection.Equals(plotSelection))//replot the same plot
            {
                if (lastPlot.Selection is PlotSelectionMultiCells lpsm && plotSelection is PlotSelectionMultiCells psm)
                {
                    //TODO lpsm.Equals(psm) returns false even if they are equal. Investigate
                    if (PlotType.GetGroup() == "current")
                    {
                        psm.CombineJunctions = !lpsm.CombineJunctions;
                    }
                }
            }
            lastPlot = new()
            {
                PlotSubset = plotsubset,
                PlotType = PlotType,
                Selection = plotSelection
            };
            PlotGenerator PG = new();
            (string Title, LastPlottedCharts) = PG.GetPlotData(lastPlot, RunningModel, Cells, Pools, tPlotStart, tPlotEnd);
            errorMessage = PG.errorMessage;

            htmlPlot = DyChartGenerator.Plot(Title, LastPlottedCharts, GlobalSettings.ShowZeroValues,
                GlobalSettings.DefaultPlotWidth, GlobalSettings.DefaultPlotHeight);
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
            if (!currentPlotWarning &&
                !RunningModel.JunctionCurrentTrackingOn && (PlotType.GetGroup() == "current" || PlotType == PlotType.FullDyn))
            {
                currentPlotWarning = true;
                string msg = $"Plotting current information (including Full Dynamics) will require to regenerate the current arrays for the whole simulation for the selected cells. " +
                    $"It can use a lot of memory. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            if (PlotType == PlotType.EpisodesTail)
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
            if (RunningModel == null || !RunningModel.ModelRun) return;
            if (unitsToPlot == null || !unitsToPlot.Any()) return;
            plotSelection = null;
            SetPlotSelectionToUnits(unitsToPlot);

            tPlotStart = (int)ePlotStart.Value;
            tPlotEnd = (int)ePlotEnd.Value;
            if (tPlotEnd > RunningModel.RunParam.MaxTime)
                tPlotEnd = RunningModel.RunParam.MaxTime;

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
                if (PlotType == PlotType.EpisodesTail)
                    RunningModel.SetAnimationParameters(RunningModel.KinemParam);
                tabOutputs.SelectedTab = tPlot;
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = true;
                cmPlot.Enabled = false;
                btnPlotHTML.Enabled = false;

                if (plotSelection is not PlotSelectionMultiCells)
                    return;

                (List<Cell> Cells, List<CellPool> Pools) = RunningModel.GetSubsetCellsAndPools(cellSelectionPlot.PoolSubset, (PlotSelectionMultiCells)plotSelection);

                (List<Image> leftImages, List<Image> rightImages) = WindowsPlotGenerator.Plot(PlotType, RunningModel, Cells, Pools, (PlotSelectionMultiCells)plotSelection,
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
                MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
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
            bool navigated = false;
            webView2DRender.NavigateTo(html, "2DRendering", GlobalSettings.TempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
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
            if (RunningModel == null) return;
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
                    await webView3DRender.ExecuteScriptAsync($"SelectCellPool('{pool.CellGroup}');");//TODO for 3 D - both left and right pools are displayed
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

            int cellNumber = (int)eKinematicsSomite.Value;
            (List<Cell> LeftMNs, List<Cell> RightMNs) = RunningModel.GetMotoNeurons(cellNumber);
            (_, SwimmingEpisodes episodes) =
                SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(RunningModel, LeftMNs, RightMNs);
            List<Cell> allMNs = cbSpikingMNs.Checked ? LeftMNs.Union(RightMNs).Where(c => c.IsSpiking()).OrderBy(c => c.CellGroup).ToList() :
                RightMNs.Union(LeftMNs).OrderBy(c => c.CellGroup).ToList();

            PlotSelectionInterface plotSelection = new PlotSelectionUnits()
            {
                Units = allMNs.Cast<ModelUnitBase>().ToList(),
                SomiteSelection = PlotSomiteSelection.All,
                CellSelection = PlotCellSelection.All,
                CombinePools = cbCombineMNPools.Checked
            };
            PlotDefinition plotDefinition = new PlotDefinition()
            {
                PlotType = PlotType.MembPotential,
                Selection = plotSelection
            };
            PlotGenerator PG = new();
            (string _, List<Chart> charts) = PG.GetPlotData(plotDefinition, RunningModel, allMNs, null);
            string html = DyChartGenerator.PlotCharts("Summary Membrane Potentials", charts, true, GlobalSettings.ShowZeroValues,
                GlobalSettings.DefaultPlotWidth, GlobalSettings.DefaultPlotHeight);
            bool navigated = false;
            webViewSummaryV.NavigateTo(html, "Episodes", GlobalSettings.TempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
            eEpisodes.Text = "";
            foreach (SwimmingEpisode episode in episodes.Episodes)
                eEpisodes.Text += episode.ToString() + "\r\n\r\n";
            lKinematicsTimes.Text = $"Last kinematics:{DateTime.Now:t}";
        }

        #endregion
        #endregion


        private void tabOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabOutputs.SelectedTab == t2DRender && !rendered2D)
                RenderIn2D();
            else if (tabOutputs.SelectedTab == t3DRender && !rendered3D)
                RenderIn3D();
        }

        private void linkExportSpikes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                bool saved = false;
                try
                {
                    FileUtil.SaveToFile(saveFileCSV.FileName, dgSpikeStats.ExportToStringBuilder().ToString());
                    saved = true;
                    Process p = new()
                    {
                        StartInfo = new ProcessStartInfo(saveFileCSV.FileName)
                        {
                            UseShellExecute = true
                        }
                    };
                    p.Start();
                }
                catch (Exception exc)
                {
                    if (!saved)
                    {
                        MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
                    }
                    else
                    {
                        MessageBox.Show($"File {saveFileCSV.FileName} is saved.", "Information");
                    }
                }
            }
        }

        private int AddCellToSpikeGrid(Cell cell)
        {
            dgSpikeStats.RowCount++;
            int rowIndex = dgSpikeStats.RowCount - 1;
            dgSpikeStats[colSpikeCell.Index, rowIndex].Value = cell.ID;
            dgSpikeStats[colSpikeSagittal.Index, rowIndex].Value = cell.PositionLeftRight;
            dgSpikeStats[colSpikeCellPool.Index, rowIndex].Value = cell.CellPool.CellGroup;
            dgSpikeStats[colSpikeSomite.Index, rowIndex].Value = cell.Somite;
            dgSpikeStats[colSpikeSeq.Index, rowIndex].Value = cell.Sequence;
            return rowIndex;
        }
        private void btnListSpikes_Click(object sender, EventArgs e)
        {
            if (RunningModel == null || !RunningModel.ModelRun) return;
            UseWaitCursor = true;
            (List<Cell> Cells, List<CellPool> Pools) = RunningModel.GetSubsetCellsAndPools(cellSelectionSpike.PoolSubset, cellSelectionSpike.GetSelection());
            if (Cells == null)
                Cells = new();
            if (Pools != null)
                foreach (CellPool pool in Pools)
                    Cells.AddRange(pool.Cells);
            Dictionary<Cell, List<int>> cellSpikes = new();
            Cells.ForEach(c => cellSpikes.Add(c, c.GetSpikeIndices()));
            int spikeCount = cellSpikes.Values.Sum(l => l.Count);
            if (spikeCount > 10 * GlobalSettings.MaxNumberOfUnits)
            {
                string msg = $"There are {spikeCount} spikes, which can take a while to list. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            double episodeBreak = RunningModel.KinemParam.EpisodeBreak;
            dgSpikeStats.Rows.Clear();
            colSpikeEpisode.Visible = cbSpikeEpisodes.Checked;
            foreach (Cell cell in Cells)
            {
                List<int> spikes = cellSpikes[cell];
                if (cbSpikeEpisodes.Checked)
                {
                    SwimmingEpisodes Episodes = SwimmingEpisode.GenerateEpisodes(RunningModel.TimeArray, spikes, episodeBreak);
                    foreach (SwimmingEpisode episode in Episodes.Episodes)
                    {
                        int rowIndex = AddCellToSpikeGrid(cell);
                        dgSpikeStats[colSpikeEpisode.Index, rowIndex].Value = "Start";
                        dgSpikeStats[colSpikeTime.Index, rowIndex].Value = episode.Start;

                        rowIndex = AddCellToSpikeGrid(cell);
                        dgSpikeStats[colSpikeEpisode.Index, rowIndex].Value = "End";
                        dgSpikeStats[colSpikeTime.Index, rowIndex].Value = episode.End;
                    }
                }
                else
                {
                    foreach (int spikeIndex in spikes)
                    {
                        int rowIndex = AddCellToSpikeGrid(cell);
                        dgSpikeStats[colSpikeTime.Index, rowIndex].Value = RunningModel.TimeArray[spikeIndex];
                    }
                }
            }
            linkExportSpikes.Enabled = true;
            UseWaitCursor = false;
        }

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

    }

}
