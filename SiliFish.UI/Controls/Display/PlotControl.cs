using Microsoft.Web.WebView2.Core;
using Services;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using SiliFish.UI.Extensions;
using System.Drawing.Imaging;
using SiliFish.ModelUnits;
using SiliFish.Services.Plotting;
using SiliFish.Services.Plotting.PlotSelection;
using SiliFish.UI.Services;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Junction;
using SiliFish.UI.EventArguments;

namespace SiliFish.UI.Controls
{
    public partial class PlotControl : UserControl
    {
        public event EventHandler PlottingSelection;

        string errorMessage;
        string htmlPlot = "";
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
        public PlotControl()
        {
            InitializeComponent();
            if (!DesignMode)
                WebViewInitializations();

            pictureBox.MouseWheel += PictureBox_MouseWheel;

            cellSelectionPlot.SelectionChanged += CellSelectionPlot_SelectionChanged;

            try { timeRangePlot.EndTime = int.Parse(GlobalSettings.LastRunSettings["lTimeEnd"]); }
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
                cellSelectionPlot.SetPlot(new PlotDefinition() { PlotSubset = Const.Motoneurons, PlotType = PlotType, Selection = null });
                cellSelectionPlot.ClearSelection();
                cellSelectionPlot.TurnOnSingleCellOrSomite();
                cellSelectionPlot.Refresh();//the drop down boxes do not refresh properly otherwise
            }
            else if (PlotType == PlotType.TailMovementFreq)
            {
                cellSelectionPlot.Visible = false;
            }
            else if (PlotType.GetGroup() == "episode")
            {
                cellSelectionPlot.CombineOptionsVisible = false;
                cellSelectionPlot.Visible = true;
                cellSelectionPlot.SetPlot(new PlotDefinition() { PlotSubset = Const.Motoneurons, PlotType = PlotType, Selection = null });
                cellSelectionPlot.ClearSelection();
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
            timeRangePlot.EndTime = model.SimulationSettings.SimulationEndTime;
            cellSelectionPlot.RunningModel = model;
            PopulatePlotTypes();
            GetLastPlotSettings();
        }
        private void SaveLastPlotSettings()
        {
            GlobalSettings.LastPlotSettings[ddPlot.Name] = ddPlot.Text;
            foreach (var kvp in cellSelectionPlot.GetSelectionAsDictionary())
            {
                GlobalSettings.LastPlotSettings[kvp.Key] = kvp.Value;
            }
            int selCounter = 0;
            while (GlobalSettings.LastPlotSettings.Any(kvp => kvp.Key.StartsWith("SelectedUnit")))
            {
                string key = GlobalSettings.LastPlotSettings.FirstOrDefault(kvp => kvp.Key.StartsWith("SelectedUnit")).Key;
                GlobalSettings.LastPlotSettings.Remove(key);
            }
            if (cellSelectionPlot.SelectedUnits != null)
            {
                foreach (var sel in cellSelectionPlot.SelectedUnits)
                {
                    GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {sel.PlotType}"] = sel.ID;
                    if (sel is Cell cell)
                        GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {cell.CellPool.PlotType}"] = cell.CellPool.ID;
                    else if (sel is ChemicalSynapse syn)
                    {
                        GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {syn.PreNeuron.CellPool.PlotType}"] = syn.PreNeuron.CellPool.ID;
                        GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {syn.PreNeuron.PlotType}"] = syn.PreNeuron.ID;
                        GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {syn.PostCell.CellPool.PlotType}"] = syn.PostCell.CellPool.ID;
                        GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {syn.PostCell.PlotType}"] = syn.PostCell.ID;
                    }
                    else if (sel is GapJunction jnc)
                    {
                        GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {jnc.Cell1.CellPool.PlotType}"] = jnc.Cell1.CellPool.ID;
                        GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {jnc.Cell1.PlotType}"] = jnc.Cell1.ID;
                        GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {jnc.Cell2.CellPool.PlotType}"] = jnc.Cell2.CellPool.ID;
                        GlobalSettings.LastPlotSettings[$"SelectedUnit{selCounter++} {jnc.Cell2.PlotType}"] = jnc.Cell2.ID;
                    }
                }
            }
        }
        private void GetLastPlotSettings()
        {
            if (GlobalSettings.LastPlotSettings.Count != 0)
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
            int tMax = runParam.MaxTime;
            timeRangePlot.EndTime = tMax;

            cmPlot.Enabled =
                btnPlotHTML.Enabled = true;
        }

        public void CancelRun()
        {
            cmPlot.Enabled =
                btnPlotHTML.Enabled = true; //to make partial results available
        }
        #region webViewPlot
        private void WebViewInitializations()
        {
            InitAsync();
            Wait();
        }
        private async void InitAsync()
        {
            await webViewPlot.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (!webViewPlot.Initialized)
                Application.DoEvents();
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            (sender as Control).Tag = true;
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
            if (PlotType == PlotType.TailMovementAndVROutput || PlotType == PlotType.TailMovementFreq)
            {
                switch ((plotSelection as PlotSelectionMultiCells).SomiteSelection)
                {
                    case PlotSomiteSelection.All:
                        numOfPlots = model.ModelDimensions.NumberOfSomites + 1;
                        break;
                    case PlotSomiteSelection.Single:
                        numOfPlots = 2; // 1 + 1;
                        break;
                    case PlotSomiteSelection.FirstMiddleLast:
                        numOfPlots = 4; // 3 + 1;
                        break;
                    case PlotSomiteSelection.Random:
                        numOfPlots = (plotSelection as PlotSelectionMultiCells).NSomite + 1;
                        break;
                    case PlotSomiteSelection.RostralTo:
                        numOfPlots = (plotSelection as PlotSelectionMultiCells).NSomite + 1;
                        break;
                    case PlotSomiteSelection.CaudalTo:
                        numOfPlots = model.ModelDimensions.NumberOfSomites - (plotSelection as PlotSelectionMultiCells).NSomite + 1;
                        break;
                }

                if (PlotType == PlotType.TailMovementFreq)
                    numOfPlots *= 2;
            }
            else
            {
                numOfPlots = Cells?.Count ?? 0 + Pools?.Count ?? 0;
                if (Cells != null && Cells.Count != 0)
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
                else if (Pools != null && Pools.Count != 0)
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
                }
            }
            if (numOfPlots > 0)
                toolTip.SetToolTip(btnPlotHTML, $"# of plots: {numOfPlots}");
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
            {
                DisplayNumberOfPlots();
                ddPlot.Items.Remove(PlotType.Junction.GetDisplayName());
            }
        }

        private void PopulatePlotTypes()
        {
            string lastSelection = ddPlot.Text;
            ddPlot.Items.Clear();
            foreach (PlotType pt in Enum.GetValues(typeof(PlotType)))
            {
                ddPlot.Items.Add(pt.GetDisplayName());
            }
            ddPlot.Items.Remove(PlotType.Junction.GetDisplayName());
            if (string.IsNullOrEmpty(lastSelection))
                ddPlot.Text = PlotType.MembPotential.GetDisplayName();
            else
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

            tPlotStart = timeRangePlot.StartTime;
            tPlotEnd = timeRangePlot.EndTime;
            if (tPlotEnd > model.MaxTime)
                tPlotEnd = model.MaxTime;
        }
        private void linkExportPlotData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Charts.Count == 0) return;

            if (Charts.Count > 1)
            {
                string msg = "Every plot data will be saved as a separate CSV file. Plot names will be appended to the file name selected.";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            List<string> fileNames = [];
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
            if (sender is not PlotDefinition plot)
                return;
            if (!simulation.SimulationRun)
                return;
            ddPlot.Text = plot.PlotType.GetDisplayName();
            plotSelection = plot.Selection;
            tPlotStart = timeRangePlot.StartTime;
            tPlotEnd = timeRangePlot.EndTime;
            if (tPlotEnd > simulation.RunParam.MaxTime)
                tPlotEnd = simulation.RunParam.MaxTime;

            cellSelectionPlot.SetPlot(plot);
            if (plot.Selection is PlotSelectionUnits selectedUnits)
            {
                SetEnablesBasedOnPlot();
                SelectedUnitArgs args = new() { unitsSelected = selectedUnits.Units };
                PlottingSelection?.Invoke(this, args);
            }
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
                List<PlotDefinition> plots = [.. listPlotHistory.GetItems<PlotDefinition>()];
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
                    string filename = saveFileJson.FileName;
                    File.WriteAllText(filename, json);
                    fileSaved = true;
                    UtilWindows.DisplaySavedFile(filename);
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
            string plotsubset = cellSelectionPlot.Visible && PlotType.GetGroup() != "episode" ? cellSelectionPlot.PoolSubset : "";
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
                if (PlotType.GetGroup() == "current" && lpsm.Equals(psm) && lastPlot.PlotSubset == plotsubset)
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
            (string Title, Charts) = PG.GetPlotData(lastPlot, simulation, Cells, Pools, tPlotStart, tPlotEnd);
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
                    $"(You can set the number of plots that triggers this warning through 'Settings')";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return false;
            }
            if (!MainForm.currentPlotWarning &&
                !model.SimulationSettings.JunctionLevelTracking && (PlotType.GetGroup() == "current" || PlotType == PlotType.FullDyn))
            {
                MainForm.currentPlotWarning = true;
                string msg = $"Plotting current information (including Full Dynamics) will require to regenerate " +
                    $"the current arrays for the whole simulation for the selected cells. " +
                    $"It can use extensive amount of memory. Do you want to continue?";
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
            htmlPlot = DyChartGenerator.Plot(Title, Charts,
                GlobalSettings.PlotWidth, GlobalSettings.PlotHeight,
                GlobalSettings.ShowZeroValues, GlobalSettings.OptimizedForPrinting);
            Invoke(CompletePlotHTML);
        }
        private void cmiOptimizedForPrint_Click(object sender, EventArgs e)
        {
            try { }
            catch { }
        }
        private void btnPlotHTML_Click(object sender, EventArgs e)
        {
            if (!PrePlotCheck())
                return;
            tabPlotSub.SelectedTab = tPlotHTML;
            UseWaitCursor = true;
            cmPlot.Enabled = false;
            btnPlotHTML.Enabled = false;

            Task.Run(PlotHTML);
        }
        private void CompletePlotHTML()
        {
            if (!string.IsNullOrEmpty(errorMessage))
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
            if (unitList.Count > 0 && unitList[0] is JunctionBase)
            {
                PlotType = PlotType.Junction;
                string jncPlot = PlotType.GetDisplayName();
                if (!ddPlot.Items.Contains(jncPlot))
                    ddPlot.Items.Add(jncPlot);
                ddPlot.SelectedItem = jncPlot;
            }
            else
            {
                PlotType = ddPlot.Text.GetValueFromName(PlotType.NotSet);
                if (PlotType.GetGroup() == "episode")
                {
                    PlotType = PlotType.MembPotential;
                    ddPlot.SelectedItem = PlotType.GetDisplayName();
                }
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

                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = true;
                cmPlot.Enabled = false;
                btnPlotHTML.Enabled = false;

                (string Title, Charts) = GenerateCharts();

                List<Image> Images = WindowsPlotGenerator.PlotCharts(Charts);

                Images?.RemoveAll(img => img == null);

                int ncol = 1; // Images?.Count > 5 ? 2 : 1;
                int nrow = (int)Math.Ceiling((decimal)(Images?.Count ?? 0) / ncol);

                if (Images != null && Images.Count != 0)
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



        private void cmiMaximizePlotWindow_Click(object sender, EventArgs e)
        {
            if (webViewPlot.CoreWebView2 == null || string.IsNullOrWhiteSpace(webViewPlot.CoreWebView2.DocumentTitle))
                return;
            WebViewMaximizedForm webView = new();
            bool navigated = false;
            webView.NavigateTo(htmlPlot, PlotType.GetDisplayName(), GlobalSettings.TempFolder, ref tempFile, ref navigated);
            webView.ShowDialog();
        }

    }

}
