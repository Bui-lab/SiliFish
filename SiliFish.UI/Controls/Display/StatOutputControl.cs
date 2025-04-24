using Microsoft.Web.WebView2.Core;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using SiliFish.UI.Extensions;
using System.Data;
using SiliFish.Repositories;
using SiliFish.Services.Dynamics;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services.Plotting;
using SiliFish.DataTypes;
using System.Windows.Forms;
using SiliFish.Services.Plotting.PlotSelection;
using System.Collections.Generic;

namespace SiliFish.UI.Controls
{
    public partial class StatOutputControl : UserControl
    {
        private string tempFile;
        Simulation simulation = null;
        RunningModel model = null;
        public StatOutputControl()
        {
            InitializeComponent();
            if (!DesignMode)
                WebViewInitializations();
        }

        public void SetRunningModel(Simulation simulation, RunningModel model)
        {
            if (model == null) return;
            this.simulation = simulation;
            this.model = model;
            timeRangeStat.EndTime = model.Settings.SimulationEndTime;
            cellSelectionStats.RunningModel = model;
        }
        public void CompleteRun(RunParam runParam)
        {
            int tMax = runParam.MaxTime;
            timeRangeStat.EndTime = tMax;

            btnListStats.Enabled = true;
        }

        public void CancelRun()
        {
            btnListStats.Enabled = true; //to make partial results available
        }
        #region webViewPlot
        private void WebViewInitializations()
        {
            InitAsync();
            Wait();
            //webview's context menu does not work. Using amcharts's export menu where necessary
            webViewRCTrains.CoreWebView2.ContextMenuRequested += AmChartsCoreWebView2_ContextMenuRequested;
        }
        private async void InitAsync()
        {
            await webViewRCTrains.EnsureCoreWebView2Async();
            await webViewRasterPlot.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (!webViewRCTrains.Initialized)
                Application.DoEvents();
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            (sender as Control).Tag = true;
        }

        private void AmChartsCoreWebView2_ContextMenuRequested(object sender, CoreWebView2ContextMenuRequestedEventArgs args)
        {
            IList<CoreWebView2ContextMenuItem> menuList = args.MenuItems;
            CoreWebView2ContextMenuTargetKind context = args.ContextMenuTarget.Kind;
            if (context == CoreWebView2ContextMenuTargetKind.Page)
            {
                for (int index = menuList.Count - 1; index >= 0; index--)
                {
                    if (menuList[index].Name == "saveImageAs" ||
                        menuList[index].Name == "copyImage" ||
                        menuList[index].Name == "inspectElement")
                    {
                        menuList.RemoveAt(index);
                    }
                }
            }
        }

        #endregion



        private void cmGridExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (((ToolStripMenuItem)sender).Owner is ContextMenuStrip { SourceControl: DataGridView dg })
                {
                    UtilWindows.SaveTextFile(saveFileCSV, dg.ExportToStringBuilder().ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        private void cmGridCopyAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (((ToolStripMenuItem)sender).Owner is ContextMenuStrip { SourceControl: DataGridView dg })
                {
                    dg.SelectAll();
                    dg.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                    Clipboard.SetDataObject(dg.GetClipboardContent());
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        private void GenerateSpikeList()
        {
            UseWaitCursor = true;
            int iSpikeStart = simulation.RunParam.iIndex((double)timeRangeStat.StartTime);
            int iSpikeEnd = simulation.RunParam.iIndex((double)timeRangeStat.EndTime);
            List<Cell> Cells = model.GetSubsetCells(cellSelectionStats.PoolSubset, cellSelectionStats.GetSelection(), iSpikeStart, iSpikeEnd);
            (List<string> colNames, List<List<string>> values) = SimulationStats.GenerateSpikesForCSV(simulation, Cells, iSpikeStart, iSpikeEnd);
            int spikeCount = values.Count;
            if (spikeCount > 10 * GlobalSettings.MaxNumberOfUnitsToList)
            {
                string msg = $"There are {spikeCount} spikes, which can take a while to list. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            dgSpikeStats.LoadData(colNames, values, cbStatsAppend.Checked, out int scrollRow);
            if (scrollRow >= 0 && dgSpikeStats.RowCount > 0)
                dgSpikeStats.FirstDisplayedScrollingRowIndex = scrollRow;
            UseWaitCursor = false;
        }
        private void GenerateHistogramOfRCColumn(int colIndex)
        {
            double[] dataPoints = dgRCTrains.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[colIndex].Value != null)
                .Select(r => double.Parse(r.Cells[colIndex].Value.ToString()))
                .ToArray();
            if (dataPoints.Length == 0)
            {
                webViewRCTrains.NavigateToString("");
                return;
            }
            (double median, (double IQR1, double IQR3)) = dataPoints.ToList().MedianAndIQR();
            double mode = dataPoints.ToList().Mode(2);
            dataPoints = dataPoints.Select(dp => Math.Round(dp, 2)).ToArray();
            string title = $"{dgRCTrains.Columns[colIndex].HeaderText} Mode:{mode:0.##} Median:{median:0.##}:IQR[{IQR1:0.##} - {IQR3:0.##}]";
            double width = webViewRCTrains.ClientSize.Width;
            double height = webViewRCTrains.ClientSize.Height - 50;
            string histHtml = HistogramGenerator.GenerateHistogramHTML(dataPoints, title, width, height);
            bool navigated = false;
            webViewRCTrains.NavigateTo(histHtml, title, GlobalSettings.TempFolder, ref tempFile, ref navigated);
        }
        private void GenerateRCTrains()
        {
            if (simulation == null || !simulation.SimulationRun) return;
            UseWaitCursor = true;
            List<Cell> Cells = model.GetSubsetCells(cellSelectionStats.PoolSubset, cellSelectionStats.GetSelection());
            int iSpikeStart = simulation.RunParam.iIndex((double)timeRangeStat.StartTime);
            int iSpikeEnd = simulation.RunParam.iIndex((double)timeRangeStat.EndTime);

            List<string> pools = Cells.Select(c => c.CellPool.ID).Distinct().ToList();

            int scrollRow = 0;
            if (!cbStatsAppend.Checked)
                dgRCTrains.Rows.Clear();
            else scrollRow = -1;
            foreach (string pool in pools)
            {
                List<Cell> pooledCells = Cells.Where(c => c.CellPool.ID == pool).ToList();
                List<TrainOfBurstCars> burstTrains = SpikeDynamics.GenerateColumnsOfBursts(model.DynamicsParam, simulation.RunParam.DeltaT,
                    pooledCells, iSpikeStart, iSpikeEnd);
                foreach (TrainOfBurstCars burstTrain in burstTrains)
                {
                    burstTrain.CalculateCarDelays();
                    foreach (BurstOrSpikeCar car in burstTrain.BurstCarList)
                    {
                        dgRCTrains.RowCount++;
                        int rowIndex = dgRCTrains.RowCount - 1;
                        if (scrollRow < 0) scrollRow = rowIndex;
                        dgRCTrains[colRCTrainNumber.Index, rowIndex].Value = burstTrain.iTrainID;
                        dgRCTrains[colRCTrainCellGroup.Index, rowIndex].Value = car.sCarID;
                        dgRCTrains[colRCTrainSomite.Index, rowIndex].Value = car.iCarID;
                        dgRCTrains[colRCTrainStart.Index, rowIndex].Value = car.BurstOrSpike.Start;
                        dgRCTrains[colRCTrainEnd.Index, rowIndex].Value = car.BurstOrSpike.End;
                        dgRCTrains[colRCTrainMedianPoint.Index, rowIndex].Value = car.BurstOrSpike.Center;
                        dgRCTrains[colRCTrainCenter.Index, rowIndex].Value = car.BurstOrSpike.WeightedCenter;
                        dgRCTrains[colRCTrainStartDelay.Index, rowIndex].Value = car.DelayStart;
                        dgRCTrains[colRCTrainMedianDelay.Index, rowIndex].Value = car.DelayMedian;
                        dgRCTrains[colRCTrainCenterDelay.Index, rowIndex].Value = car.DelayCenter;
                    }
                }
            }
            tabStats.SelectedTab = tRCTrains;
            if (dgRCTrains.Rows.Count > 0) 
                dgRCTrains.FirstDisplayedScrollingRowIndex = scrollRow;
            GenerateHistogramOfRCColumn(colRCTrainStartDelay.Index);
            UseWaitCursor = false;
        }
        private void dgRCTrains_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            if (colIndex == colRCTrainCenterDelay.Index ||
                colIndex == colRCTrainStartDelay.Index ||
                colIndex == colRCTrainMedianDelay.Index)
                GenerateHistogramOfRCColumn(colIndex);
        }
        private void GenerateRasterPlot(List<Cell> Cells)
        {
            int iStatsStart = simulation.RunParam.iIndex(timeRangeStat.StartTime);
            int iStatsEnd = simulation.RunParam.iIndex(timeRangeStat.EndTime);

            (List<List<XYDataSet>> dataPoints, List<string> IDs, List<Color> colors) =
                SimulationStats.GenerateSpikeRasterDataSet(simulation, Cells, iStatsStart, iStatsEnd);

            double width = webViewRasterPlot.ClientSize.Width - 50;
            double height = webViewRasterPlot.ClientSize.Height - 70;
            string htmlPlot = RasterPlotGenerator.GenerateRasterPlotHTML(dataPoints, IDs, colors, "Raster Plot", width, height);
            bool navigated = false;
            webViewRasterPlot.NavigateTo(htmlPlot, "Raster Plot", GlobalSettings.TempFolder, ref tempFile, ref navigated);
        }
        private int AddCellPoolSpikeSummaryGrid(CellPool pool)
        {
            dgSpikeSummaryDetails.RowCount++;
            int rowIndex = dgSpikeSummaryDetails.RowCount - 1;
            dgSpikeSummaryDetails[colSumDetailsSagittal.Index, rowIndex].Value = pool.PositionLeftRight;
            dgSpikeSummaryDetails[colSumDetailsCellPool.Index, rowIndex].Value = pool.ID;
            dgSpikeSummaryDetails.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Gray;
            return rowIndex;
        }
        private int AddCellToSpikeSummaryGrid(Cell cell)
        {
            dgSpikeSummaryDetails.RowCount++;
            int rowIndex = dgSpikeSummaryDetails.RowCount - 1;
            dgSpikeSummaryDetails[colSumDetailsCellID.Index, rowIndex].Value = cell.ID;
            dgSpikeSummaryDetails[colSumDetailsSagittal.Index, rowIndex].Value = cell.PositionLeftRight;
            dgSpikeSummaryDetails[colSumDetailsCellPool.Index, rowIndex].Value = cell.CellPool;
            dgSpikeSummaryDetails[colSumDetailsSomite.Index, rowIndex].Value = cell.Somite;
            dgSpikeSummaryDetails[colSumDetailsCellSeq.Index, rowIndex].Value = cell.Sequence;
            return rowIndex;
        }


        private void GenerateSpikeSummary()
        {
            UseWaitCursor = true;
            (Dictionary<string, int> cellSpikes, Dictionary<string, int> cellPoolSpikes) = 
                SimulationStats.GenerateSpikeSummary(simulation, timeRangeStat.StartTime, timeRangeStat.EndTime);
            string period = $"{timeRangeStat.StartTime}-{timeRangeStat.EndTime}";
            int scrollRowSummary = 0;
            int scrollRowDetails = 0;
            if (!cbStatsAppend.Checked)
            {
                dgSpikeSummary.Rows.Clear();
                dgSpikeSummaryDetails.Rows.Clear();
            }
            else
            {
                scrollRowSummary = scrollRowDetails = -1;
            }
            foreach (CellPool cellPool in model.GetCellPools().Cast<CellPool>())
            {
                int poolIndex = AddCellPoolSpikeSummaryGrid(cellPool);
                if (scrollRowDetails < 0)
                    scrollRowDetails = dgSpikeSummaryDetails.RowCount - 1;
                dgSpikeSummaryDetails[colSumDetailsPeriod.Index, poolIndex].Value = period;
                foreach (Cell cell in cellPool.Cells)
                {
                    int cellIndex = AddCellToSpikeSummaryGrid(cell);
                    dgSpikeSummaryDetails[colSumDetailsSpikeCount.Index, cellIndex].Value = cellSpikes[cell.ID];
                }
                dgSpikeSummaryDetails[colSumDetailsSpikeCount.Index, poolIndex].Value = cellPoolSpikes[cellPool.ID];

                dgSpikeSummary.RowCount++;
                int rowIndex = dgSpikeSummary.RowCount - 1;
                if (scrollRowSummary < 0) scrollRowSummary = rowIndex;
                dgSpikeSummary[colSumPeriod.Index, rowIndex].Value = period;
                dgSpikeSummary[colSumCellPool.Index, rowIndex].Value = cellPool.ID;
                dgSpikeSummary[colSumSpikeCount.Index, rowIndex].Value = cellPoolSpikes[cellPool.ID];
            }

            UseWaitCursor = false;
            dgSpikeSummary.FirstDisplayedScrollingRowIndex = scrollRowSummary;
            dgSpikeSummaryDetails.FirstDisplayedScrollingRowIndex = scrollRowDetails;
        }

        private void GenerateEpisodes()
        {
            UseWaitCursor = true;
            (List<string> colNames, List<List<string>> values) = SimulationStats.GenerateEpisodes(simulation);
            dgEpisodes.LoadData(colNames, values, cbStatsAppend.Checked, out int scrollRow);
            if (scrollRow >= 0)
                dgEpisodes.FirstDisplayedScrollingRowIndex = scrollRow;
            UseWaitCursor = false;

        }
        private void btnListStats_Click(object sender, EventArgs e)
        {
            if (simulation == null || !simulation.SimulationRun) return;
            if (cellSelectionStats.Enabled && cellSelectionStats.PoolSubset == Const.AllPools)
            {
                if (MessageBox.Show("Generating stats for 'All Pools' may take a while. Do you want to continue?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }
            if (tabStats.SelectedTab == tSpikeSummary)
                GenerateSpikeSummary();
            else if (tabStats.SelectedTab == tSpikes)
                GenerateSpikeList();
            else if (tabStats.SelectedTab == tRCTrains)
                GenerateRCTrains();
            else if (tabStats.SelectedTab == tEpisodes)
                GenerateEpisodes();
            else if (tabStats.SelectedTab == tRCRasterPlots)
            {
                if (simulation == null || !simulation.SimulationRun) return;
                PlotSelectionInterface psi = cellSelectionStats.GetSelection();
                List<Cell> Cells = model.GetSubsetCells(cellSelectionStats.PoolSubset, cellSelectionStats.GetSelection());
                GenerateRasterPlot(Cells);
            }
        }
        private void tabStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabStats.SelectedTab == tSpikeSummary)
            {
                btnListStats.Text = "List Spike Summary";
                cellSelectionStats.Enabled = false;
                cbStatsAppend.Enabled = true;
                timeRangeStat.Enabled = true;
            }
            else if (tabStats.SelectedTab == tSpikes)
            {
                btnListStats.Text = "List Spikes";
                cellSelectionStats.Enabled = true;
                cbStatsAppend.Enabled = true;
                timeRangeStat.Enabled = true;
            }
            else if (tabStats.SelectedTab == tRCTrains)
            {
                btnListStats.Text = "List RC Spike Trains";
                cellSelectionStats.Enabled = true;
                cbStatsAppend.Enabled = true;
                timeRangeStat.Enabled = true;
            }
            else if (tabStats.SelectedTab == tRCRasterPlots)
            {
                btnListStats.Text = "RasterPlot RC Activity";
                cellSelectionStats.Enabled = true;
                cbStatsAppend.Enabled = false;
                timeRangeStat.Enabled = true;
            }
            else if (tabStats.SelectedTab == tEpisodes)
            {
                btnListStats.Text = "List Episodes";
                cellSelectionStats.Enabled = false;
                cbStatsAppend.Checked = false;
                cbStatsAppend.Enabled = false;
                timeRangeStat.Enabled = false;
            }
        }

    }

}
