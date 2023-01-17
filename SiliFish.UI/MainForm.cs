using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Services;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services;
using SiliFish.UI.Controls;
using SiliFish.UI.Extensions;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.Json;
using SiliFish.Repositories;

namespace SiliFish.UI
{
    public partial class MainForm : Form
    {
        static string modelFileDefaultFolder;
        bool templateUpdated = false;
        bool runningModelUpdated = false;

        ModelTemplate ModelTemplate = null;
        RunningModel RunningModel = null;
        string htmlAnimation = "";
        string htmlPlot = "";
        string PlotSubset = "";
        decimal tAnimdt;
        int tAnimStart = 0;
        int tAnimEnd = 0;
        int tPlotStart = 0;
        int tPlotEnd = 0;
        PlotType PlotType = PlotType.MembPotential;
        CellSelectionStruct plotCellSelection;
        private string tempFile;
        DateTime runStart;
        List<ChartDataStruct> LastPlottedCharts;

        public MainForm()
        {
            try
            {
                InitializeComponent();
                InitAsync();
                Wait();
                splitPlotWindows.SplitterDistance = splitPlotWindows.Width / 2;
                ModelTemplate = new();
                ModelTemplate.Settings.TempFolder = Path.GetTempPath() + "SiliFish";
                ModelTemplate.Settings.OutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
                mcTemplate.SetModel(ModelTemplate);
                CurrentSettings.Settings = ModelTemplate.Settings;
                if (!Directory.Exists(ModelTemplate.Settings.TempFolder))
                    Directory.CreateDirectory(ModelTemplate.Settings.TempFolder);
                if (!Directory.Exists(ModelTemplate.Settings.OutputFolder))
                    Directory.CreateDirectory(ModelTemplate.Settings.OutputFolder);
                webView2DModel.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
                webView3DModel.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
                webViewAnimation.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
                webViewPlot.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
                webViewSummaryV.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;

                foreach (PlotType pt in Enum.GetValues(typeof(PlotType)))
                {
                    ddPlot.Items.Add(pt.GetDisplayName());
                }

                foreach (PlotSelection ps in Enum.GetValues(typeof(PlotSelection)))
                {
                    if (ps != PlotSelection.Summary)
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
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        #region webViewPlot
        private async void InitAsync()
        {
            await webViewPlot.EnsureCoreWebView2Async();
            await webView2DModel.EnsureCoreWebView2Async();
            await webView3DModel.EnsureCoreWebView2Async();
            await webViewAnimation.EnsureCoreWebView2Async();
            await webViewSummaryV.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (webViewPlot.Tag == null ||
                webView2DModel.Tag == null || webView3DModel.Tag == null ||
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
                    target = Path.Combine(ModelTemplate.Settings.OutputFolder, prefix + ".html");
                    int suffix = 0;
                    while (File.Exists(target))
                        target = Path.Combine(ModelTemplate.Settings.OutputFolder, prefix + (suffix++).ToString() + ".html");
                    File.Copy(tempFile, target);
                    RegenerateWebview(sender as WebView2);
                    Invoke(() => WarningMessage("There was a problem with displaying the html file. It is saved as " + target + "."));
                }
            }
            catch { }
        }
        #endregion

        #region Model selection

        private void GetRunningModel()
        {
            ModelTemplate = mcTemplate.GetModel() as ModelTemplate;
            if (RunningModel==null)
            {
                RunningModel = new(ModelTemplate);
                mcRunningModel.SetModel(RunningModel);
            }
        }
        private void RefreshModel()
        {
            ModelTemplate = mcTemplate.GetModel() as ModelTemplate;
            RunningModel = mcRunningModel.GetModel() as RunningModel;
            if (ModelTemplate != null)
            {
                RunningModel = new(ModelTemplate);
                mcRunningModel.SetModel(RunningModel);
            }
            templateUpdated = runningModelUpdated = false;
        }

        private void RefreshModelSafeMode(bool ask)
        {
            if (RunningModel == null)
            {
                RefreshModel();
                return;
            }
            if (ask && (templateUpdated || runningModelUpdated))
            {
                string msg = "Do you want to regenerate the model using the modifications you have done in the UI?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    RefreshModel();
            }
        }


        #endregion



        #region Simulation
        private void eTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            if (lastEnteredTime != null)
            {
                int newValue = (int)eTimeEnd.Value;
                if ((int)ePlotEnd.Value == lastEnteredTime)
                    ePlotEnd.Value = newValue;
                if ((int)eAnimationEnd.Value == lastEnteredTime)
                    eAnimationEnd.Value = newValue;
                lastEnteredTime = newValue;
            }
        }
        int? lastEnteredTime;
        private void eTimeEnd_Enter(object sender, EventArgs e)
        {
            lastEnteredTime = (int)eTimeEnd.Value;
        }       
          private void edt_ValueChanged(object sender, EventArgs e)
        {
            eAnimationdt.Minimum = edt.Value;
            eAnimationdt.Increment = edt.Value;
            eAnimationdt.Value = 10 * edt.Value;
        }      
        private void RunModel()
        {
            try
            {
                RefreshModelSafeMode(false);
                ddPlotSomiteSelection.Enabled = ePlotSomiteSelection.Enabled = RunningModel.ModelDimensions.NumberOfSomites > 0;
                RunningModel.RunModel(0, (int)eRunNumber.Value);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (RunningModel.ModelRun)
                    Invoke(CompleteRun);
                else
                    Invoke(CancelRun);
            }
        }
        private void CancelRun()
        {
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            progressBarRun.Visible = false;
            btnRun.Text = "Run";
            
            btnPlotWindows.Enabled = false;
            btnPlotHTML.Enabled = false;
            btnAnimate.Enabled = false;
            linkExportOutput.Visible = false;

            lRunTime.Text = $"Last run cancelled\r\n" +
                $"Model: {RunningModel.ModelName}";
        }
        private void CompleteRun()
        {
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            progressBarRun.Visible = false;
            btnRun.Text = "Run";
            
            PopulatePlotPools();
            int NumberOfSomites = RunningModel.ModelDimensions.NumberOfSomites;
            ddPlotSomiteSelection.Enabled = ePlotSomiteSelection.Enabled = NumberOfSomites > 0;
            eKinematicsSomite.Maximum = NumberOfSomites > 0 ? NumberOfSomites : RunningModel.CellPools.Max(p => p.Cells.Max(c => c.Sequence));

            btnPlotWindows.Enabled = true;
            btnPlotHTML.Enabled = true;
            btnAnimate.Enabled = true;
            linkExportOutput.Visible = true;
            btnGenerateEpisodes.Enabled = true;

            TimeSpan ts = DateTime.Now - runStart;

            lRunTime.Text = $"Last run: {runStart:t}\r\n" +
                $"Duration: {Util.TimeSpanToString(ts)}\r\n" +
                $"Model: {RunningModel.ModelName}";

        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.Text == "Stop Run")
            {
                if (RunningModel != null)
                    RunningModel.CancelRun = true;
                return;
            }
            runStart = DateTime.Now;
            lRunTime.Text = "";
            UseWaitCursor = true;
            progressBarRun.Value = 0;
            progressBarRun.Visible = true;
            timerRun.Enabled = true;

            RefreshModelSafeMode(true);
            if (RunningModel == null) return;
            RunningModel.RunParam = new()
            {
                tSkip_ms = (int)eSkip.Value,
                tMax = (int)eTimeEnd.Value,
                dt = (double)edt.Value,
                dtEuler = (double)edtEuler.Value
            };
            btnRun.Text = "Stop Run";
            Task.Run(RunModel);
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            if (RunningModel == null) return;
            progressBarRun.Value = (int)((RunningModel?.GetProgress() ?? 0) * progressBarRun.Maximum);
            if (eRunNumber.Value > 1)
            {
                int? i = RunningModel?.GetRunCounter();
                if (i != null)
                    lRunTime.Text = $"Run number: {i}";
            }
        }
        private void linkExportOutput_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string msg = "Save Run may take a while depending on the duration and number of cells.";
            if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                RunningModel.SaveToFile(saveFileCSV.FileName);
            }
        }

        #endregion

        #region Outputs

        #region HTML and Windows Plots - Common Functions
        private void DisplayNumberOfPlots()
        {
            if (RunningModel == null) return;
            GetPlotSubset();
            (List<Cell> Cells, List<CellPool> Pools) = RunningModel.GetSubsetCellsAndPools(PlotSubset, plotCellSelection);
            int count = Cells?.Count ?? 0 + Pools?.Count ?? 0;
            if (count > 0)
            {
                if (PlotType == PlotType.FullDyn)
                    count *= 5;
                else if (PlotType == PlotType.Current)
                    count *= 3;
                else if (PlotType == PlotType.ChemCurrent)
                    count *= 2;
                if (PlotType == PlotType.Stimuli)
                    lNumberOfPlots.Text = lNumberOfPlots.Tag + " max " + count.ToString();
                else
                    lNumberOfPlots.Text = lNumberOfPlots.Tag + count.ToString();
                lNumberOfPlots.Visible = true;
            }
            else
                lNumberOfPlots.Visible = false;

        }
        private void ddPlotPools_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddPlotPools.SelectedIndex < 0 || ddPlotSagittal.SelectedIndex < 0)
                return;
            string pool = ddPlotPools.Text;
            SagittalPlane sagittal = ddPlotSagittal.Text.GetValueFromName<SagittalPlane>(SagittalPlane.Both);
            List<CellPool> pools = RunningModel.CellPools.Where(cp => (pool == "All" || cp.CellGroup == pool) && cp.OnSide(sagittal)).ToList();
            ePlotSomiteSelection.Maximum = (decimal)(pools?.Max(p => p.GetCells().Max(c => c.Somite)) ?? 0);
            ePlotCellSelection.Maximum = (decimal)(pools?.Max(p => p.GetCells().Max(c => c.Sequence)) ?? 0);
            if (ddPlotPools.Focused)
                DisplayNumberOfPlots();
        }

        private void ddPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotType plotType = ddPlot.Text.GetValueFromName<PlotType>(PlotType.NotSet);
            if (plotType == PlotType.Episodes)
            {
                ddPlotSomiteSelection.SelectedIndex =
                    ddPlotCellSelection.SelectedIndex =
                    ddPlotSagittal.SelectedIndex =
                    ddPlotPools.SelectedIndex = -1;
                ddPlotSomiteSelection.Enabled =
                    ePlotSomiteSelection.Enabled =
                    ddPlotCellSelection.Enabled =
                    ePlotCellSelection.Enabled =
                    ddPlotSagittal.Enabled =
                    ddPlotPools.Enabled = false;
            }
            else
            {
                ddPlotSomiteSelection.Enabled =
                    ePlotSomiteSelection.Enabled = RunningModel?.ModelDimensions.NumberOfSomites > 0;
                ddPlotCellSelection.Enabled =
                    ePlotCellSelection.Enabled =
                    ddPlotSagittal.Enabled =
                    ddPlotPools.Enabled = true;
            }
            toolTip.SetToolTip(ddPlot, plotType.GetDescription());
            if (ddPlot.Focused)
                DisplayNumberOfPlots();
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
        private void PopulatePlotPools()
        {
            string prevSelection = ddPlotPools.Text;
            ddPlotPools.Items.Clear();
            ddPlotPools.Text = "";
            if (RunningModel == null) return;
            List<string> itemList = new();
            itemList.AddRange(RunningModel.CellPools.Select(p => p.CellGroup).OrderBy(p => p).ToArray());
            itemList.Insert(0, "All");

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

            if (PlotType != PlotType.Episodes)
            {
                plotCellSelection = new CellSelectionStruct()
                {
                    SagittalPlane = ddPlotSagittal.Text.GetValueFromName(SagittalPlane.Both),
                    somiteSelection = ddPlotSomiteSelection.Text.GetValueFromName(PlotSelection.All),
                    nSomite = (int)ePlotSomiteSelection.Value,
                    cellSelection = ddPlotCellSelection.Text.GetValueFromName(PlotSelection.All),
                    nCell = (int)ePlotCellSelection.Value
                };
            }

            tPlotStart = (int)ePlotStart.Value;
            tPlotEnd = (int)ePlotEnd.Value;
            if (tPlotEnd > RunningModel.RunParam.tMax)
                tPlotEnd = RunningModel.RunParam.tMax;
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

            (List<Cell> Cells, List<CellPool> Pools) = RunningModel.GetSubsetCellsAndPools(PlotSubset, plotCellSelection);
            (string Title, LastPlottedCharts) = PlotDataGenerator.GetPlotData(PlotType, RunningModel, Cells, Pools, plotCellSelection, tPlotStart, tPlotEnd);

            htmlPlot = DyChartGenerator.Plot(Title, LastPlottedCharts,
                (int)ePlotWidth.Value, (int)ePlotHeight.Value);
            Invoke(CompletePlotHTML);
        }
        private void btnPlotHTML_Click(object sender, EventArgs e)
        {
            if (RunningModel == null || !RunningModel.ModelRun) return;

            GetPlotSubset();
            if (PlotType == PlotType.Episodes)
                RunningModel.SetAnimationParameters(ModelTemplate.KinemParam);
            tabOutputs.SelectedTab = tabPlot;
            tabPlotSub.SelectedTab = tPlotHTML;
            UseWaitCursor = true;
            btnPlotWindows.Enabled = false;
            btnPlotHTML.Enabled = false;

            Task.Run(PlotHTML);
        }
        private void CompletePlotHTML()
        {
            webViewPlot.NavigateTo(htmlPlot, ModelTemplate.Settings.TempFolder, ref tempFile);
            UseWaitCursor = false;
            btnPlotWindows.Enabled = true;
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

                GetPlotSubset();
                if (PlotType == PlotType.Episodes)
                    RunningModel.SetAnimationParameters(ModelTemplate.KinemParam);
                tabOutputs.SelectedTab = tabPlot;
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = true;
                btnPlotWindows.Enabled = false;
                btnPlotHTML.Enabled = false;

                (List<Cell> Cells, List<CellPool> Pools) = RunningModel.GetSubsetCellsAndPools(PlotSubset, plotCellSelection);

                (List<Image> leftImages, List<Image> rightImages) = WindowsPlotGenerator.Plot(PlotType, RunningModel, Cells, Pools, plotCellSelection,
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
                toolTip.SetToolTip(btnPlotWindows, $"Last plot: {DateTime.Now:t}");
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = false;
                btnPlotWindows.Enabled = true;
                btnPlotHTML.Enabled = true;
                linkSavePlots.Enabled = true;
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

        private void linkSavePlots_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
        
        #region 2D Model
        private void Generate2DModel()
        {
            if (RunningModel == null) return;
            RefreshModelSafeMode(true);
            TwoDModelGenerator modelGenerator = new();
            string html = modelGenerator.Create2DModel(false, RunningModel, RunningModel.CellPools, (int)webView2DModel.Width / 2, webView2DModel.Height);
            webView2DModel.NavigateTo(html, ModelTemplate.Settings.TempFolder, ref tempFile);

        }
        private void btnGenerate2DModel_Click(object sender, EventArgs e)
        {
            Generate2DModel();
        }

        private async void linkSaveHTML2D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (saveFileHTML.ShowDialog() == DialogResult.OK)
                {
                    string htmlHead = JsonSerializer.Deserialize<string>(await webView2DModel.ExecuteScriptAsync("document.head.outerHTML"));
                    string htmlBody = JsonSerializer.Deserialize<string>(await webView2DModel.ExecuteScriptAsync("document.body.outerHTML"));
                    string html = $@"<!DOCTYPE html> <html lang=""en"">{htmlHead}{htmlBody}</html>";
                    File.WriteAllText(saveFileHTML.FileName, html);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in saving the file:" + exc.Message);
            }
        }
        #endregion

        #region 3D Model

        private void Generate3DModel()
        {
            try
            {
                RefreshModelSafeMode(true);
                ThreeDModelGenerator threeDModelGenerator = new();
                string html = threeDModelGenerator.Create3DModel(saveFile:false, RunningModel, RunningModel.CellPools, 
                    somiteRange: cb3DAllSomites.Checked ? "All" : e3DSomiteRange.Text,
                    showGap: cb3DGapJunc.Checked, showChem: cb3DChemJunc.Checked);
                webView3DModel.NavigateTo(html, ModelTemplate.Settings.TempFolder, ref tempFile);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btnGenerate3DModel_Click(object sender, EventArgs e)
        {
            Generate3DModel();
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
            await webView3DModel.ExecuteScriptAsync(func);
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
                await webView3DModel.ExecuteScriptAsync(func);
            }
        }
        private async void linkSaveHTML3D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (saveFileHTML.ShowDialog() == DialogResult.OK)
                {
                    string htmlHead = JsonSerializer.Deserialize<string>(await webView3DModel.ExecuteScriptAsync("document.head.outerHTML"));
                    string htmlBody = JsonSerializer.Deserialize<string>(await webView3DModel.ExecuteScriptAsync("document.body.outerHTML"));
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
                await webView3DModel.ExecuteScriptAsync("ShowChemJunc();");
            else
                await webView3DModel.ExecuteScriptAsync("HideChemJunc();");
        }

        private async void cb3DGapJunc_CheckedChanged(object sender, EventArgs e)
        {
            if (cb3DGapJunc.Checked)
                await webView3DModel.ExecuteScriptAsync("ShowGapJunc();");
            else
                await webView3DModel.ExecuteScriptAsync("HideGapJunc();");
        }
        private void cb3DLegend_CheckedChanged(object sender, EventArgs e)
        {
            grLegend.Visible = cb3DLegend.Checked;
        }

        private async void dd3DViewpoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dd3DViewpoint.Text == "Dorsal view")
                await webView3DModel.ExecuteScriptAsync("DorsalView();");
            else if (dd3DViewpoint.Text == "Ventral view")
                await webView3DModel.ExecuteScriptAsync("VentralView();");
            else if (dd3DViewpoint.Text == "Rostral view")
                await webView3DModel.ExecuteScriptAsync("RostralView();");
            else if (dd3DViewpoint.Text == "Caudal view")
                await webView3DModel.ExecuteScriptAsync("CaudalView();");
            else if (dd3DViewpoint.Text == "Lateral view (left)")
                await webView3DModel.ExecuteScriptAsync("LateralLeftView();");
            else if (dd3DViewpoint.Text == "Lateral view (right)")
                await webView3DModel.ExecuteScriptAsync("LateralRightView();");
            else
                await webView3DModel.ExecuteScriptAsync("FreeView();");
        }

        private async void btnZoomOut_Click(object sender, EventArgs e)
        {
            await webView3DModel.ExecuteScriptAsync("ZoomOut();");
        }

        private async void btnZoomIn_Click(object sender, EventArgs e)
        {
            await webView3DModel.ExecuteScriptAsync("ZoomIn();");
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
                htmlAnimation = AnimationGenerator.GenerateAnimation(RunningModel, tAnimStart, tAnimEnd, (double)tAnimdt);
                Invoke(CompleteAnimation);
            }
            catch { Invoke(CancelAnimation); }
        }
        private void CompleteAnimation()
        {
            webViewAnimation.NavigateTo(htmlAnimation, ModelTemplate.Settings.TempFolder, ref tempFile);
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
            if (tAnimStart > RunningModel.RunParam.tMax || tAnimStart < 0)
                tAnimStart = 0;
            if (tAnimEnd > RunningModel.RunParam.tMax)
                tAnimEnd = RunningModel.RunParam.tMax;
            double dt = RunningModel.RunParam.dt;
            lastAnimationStartIndex = (int)(tAnimStart / dt);
            int lastAnimationEndIndex = (int)(tAnimEnd / dt);
            lastAnimationTimeArray = RunningModel.TimeArray;
            //TODO generatespinecoordinates is called twice (once in generateanimation) - fix it
            RunningModel.SetAnimationParameters(ModelTemplate.KinemParam);
            lastAnimationSpineCoordinates = SwimmingKinematics.GenerateSpineCoordinates(RunningModel, lastAnimationStartIndex, lastAnimationEndIndex);

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

            FileUtil.SaveAnimation(saveFileCSV.FileName, lastAnimationSpineCoordinates, lastAnimationTimeArray, lastAnimationStartIndex);
        }

        #endregion

        #region JSON
        private void eTemplateJSON_TextChanged(object sender, EventArgs e)
        {
            if (eTemplateJSON.Focused)
                btnLoadTemplateJSON.Enabled = true;
        }

        private void btnDisplayTemplateJSON_Click(object sender, EventArgs e)
        {
            //TODO ReadModelTemplate(includeHidden: true);
            try
            {
                eTemplateJSON.Text = JsonUtil.ToJson(ModelTemplate);
            }
            catch
            {
                MessageBox.Show("Selected file is not a valid Swimming Model Template file.");
                return;
            }
            btnLoadTemplateJSON.Enabled = false;
        }

        private void btnLoadTemplateJSON_Click(object sender, EventArgs e)
        {
            try
            {
                if (JsonUtil.ToObject(typeof(ModelTemplate), eTemplateJSON.Text) is ModelTemplate temp)
                {
                    ModelTemplate = temp;
                    ModelTemplate.LinkObjects();
                    //TODO LoadModelTemplate();
                    MessageBox.Show("Updated template is loaded.");
                }
            }
            catch (JsonException exc)
            {
                WarningMessage($"There is a problem with the JSON file. Please check the format of the text in a JSON editor. {exc.Message}");
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
            }
        }
        private void linkSaveTemplateJSON_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (saveFileJson.ShowDialog() == DialogResult.OK)
                    eTemplateJSON.SaveFile(saveFileJson.FileName, RichTextBoxStreamType.PlainText);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void eModelJSON_TextChanged(object sender, EventArgs e)
        {
            if (eModelJSON.Focused)
                btnLoadModelJSON.Enabled = true;
        }

        private void btnDisplayModelJSON_Click(object sender, EventArgs e)
        {
            //TODO modelUpdated = false;
            if (RunningModel == null) return;
            RefreshModelSafeMode(true);
            try
            {
                eModelJSON.Text = JsonUtil.ToJson(RunningModel);
            }
            catch
            {
                MessageBox.Show("Selected file is not a valid Swimming Model file.");
                return;
            }
            btnLoadModelJSON.Enabled = false;
        }

        private void btnLoadModelJSON_Click(object sender, EventArgs e)
        {
            try
            {
                if (JsonUtil.ToObject(typeof(RunningModel), eModelJSON.Text) is RunningModel model)
                {
                    RunningModel = model;
                    RunningModel.LinkObjects();
                    //TODO modelUpdated = true;
                }
            }
            catch (JsonException exc)
            {
                WarningMessage($"There is a problem with the JSON file. Please check the format of the text in a JSON editor. {exc.Message}");
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
            }
        }

        private void linkSaveModelJSON_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                if (saveFileJson.ShowDialog() == DialogResult.OK)
                    eModelJSON.SaveFile(saveFileJson.FileName, RichTextBoxStreamType.PlainText);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
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
            webViewSummaryV.NavigateTo(html, ModelTemplate.Settings.TempFolder, ref tempFile);
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


        
        #region Settings
        private void linkOpenOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", ModelTemplate.Settings.OutputFolder);
            }
            catch { }
        }

        private void linkOpenTempFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", ModelTemplate.Settings.TempFolder);
            }
            catch { }
        }




        #endregion

        #region General Form Functions
        private void btnAbout_Click(object sender, EventArgs e)
        {
            About about = new();
            about.ShowDialog();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            About about = new();
            about.SetTimer(2000);
            about.ShowDialog();
        }
        private void splitWindows_DoubleClick(object sender, EventArgs e)
        {
            splitPlotWindows.SplitterDistance = splitPlotWindows.Width / 2;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ModelTemplate != null)
                {
                    //check whether the custom model has changed
                    string jsonstring = JsonUtil.ToJson(ModelTemplate);
                    //TODO if (jsonstring != lastSavedCustomModelJSON)
                    {
                        DialogResult res = MessageBox.Show("The template has changed. Do you want to save the modifications?", "Model Template Save", MessageBoxButtons.YesNoCancel);
                        if (res == DialogResult.Yes)
                        {
                            //TODO if (!SaveModelTemplate())
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        else if (res == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                foreach (string f in CurrentSettings.Settings.TempFiles)
                {
                    File.Delete(f);
                }
                    
            }
            catch { }
        }
        #endregion

        private void btnCellularDynamics_Click(object sender, EventArgs e)
        {
            DynamicsTestControl dynControl = new("Izhikevich_9P", null, testMode: true);
            ControlContainer frmControl = new();
            frmControl.AddControl(dynControl);
            frmControl.Text = "Cellular Dynamics Test";
            frmControl.SaveVisible = false;
            frmControl.ShowDialog();
        }

        private void linkLoadModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                openFileJson.InitialDirectory = modelFileDefaultFolder;
                if (openFileJson.ShowDialog() == DialogResult.OK)
                {
                    ModelBase mb;
                    try
                    {
                        mb = ModelFile.Load(openFileJson.FileName, out List<string> issues);
                    }
                    catch
                    {
                        MessageBox.Show("Selected file is not a valid Model or Template file.");
                        return;
                    }
                    mb.BackwardCompatibility();
                    mb.LinkObjects();
                    templateUpdated = false;
                    runningModelUpdated = false;
                    if (mb is ModelTemplate)
                    {
                        ModelTemplate = mb as ModelTemplate;
                        mcTemplate.SetModel(ModelTemplate);
                        RunningModel = null;
                        if (!tabModel.TabPages.Contains(tTemplate))
                        {
                            tabModel.TabPages.Add(tTemplate);
                            mcTemplate.Enabled = true;
                        }
                        if (tabModel.TabPages.Contains(tModel))
                            tabModel.TabPages.Remove(tModel);
                    }
                    else
                    {
                        RunningModel = mb as RunningModel;
                        mcRunningModel.SetModel(RunningModel);
                        ModelTemplate = null;
                        if (!tabModel.TabPages.Contains(tModel))
                        {
                            tabModel.TabPages.Add(tModel);
                            mcRunningModel.Enabled = true;
                        }
                        if (tabModel.TabPages.Contains(tTemplate))
                            tabModel.TabPages.Remove(tTemplate);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"There is a problem in generating the model template from the JSON file.\r\n{exc.Message}");
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
            }
        }

        private bool SaveModel()
        {
            try
            {
                ModelBase mb = tabModel.SelectedTab == tTemplate ? 
                    mcTemplate.GetModel() : mcRunningModel.GetModel();

                if (!mb.CheckValues(out List<string> errors))
                {
                    MessageBox.Show(string.Join("\r\n", errors));
                    return false;
                }
                if (string.IsNullOrEmpty(saveFileJson.FileName))
                    saveFileJson.FileName = mb.ModelName;
                else
                    saveFileJson.InitialDirectory = modelFileDefaultFolder;
                if (saveFileJson.ShowDialog() == DialogResult.OK)
                {
                    modelFileDefaultFolder = Path.GetDirectoryName(saveFileJson.FileName);
                    ModelFile.Save(saveFileJson.FileName, mb);

                    this.Text = $"SiliFish {mb.ModelName}";
                    if (mb is ModelTemplate)
                        templateUpdated = false;
                    else
                        runningModelUpdated = false;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        private void linkSaveModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveModel();
        }

        private void linkClearModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Do you want to start from a brand new template? All changes you have made will be cleared.", "Warning",
                MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            ModelTemplate = new();
            mcTemplate.SetModel(ModelTemplate);
            RunningModel = null;
            mcRunningModel.SetModel(RunningModel);
            if (!tabModel.TabPages.Contains(tTemplate))
            {
                tabModel.TabPages.Add(tTemplate);
                mcTemplate.Enabled = true;
            }
            if (tabModel.TabPages.Contains(tModel))
                tabModel.TabPages.Remove(tModel);
        }

        private void linkGenerateRunningModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ModelTemplate == null)
            {
                MessageBox.Show("There is no model template loaded.", "Error");
                return;
            }
            if (RunningModel != null)
            {
                string msg = $"Do you want to write over current model? the Once you make changes in the model, the changes will not transfer to the model template." +
                    $"You will need to save the model independently.";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            RunningModel = new RunningModel(ModelTemplate);
            mcRunningModel.SetModel(RunningModel);
            if (!tabModel.TabPages.Contains(tModel))
            {
                tabModel.TabPages.Add(tModel);
                mcRunningModel.Enabled = true;
            }
        }

        private void mcTemplate_ModelChanged(object sender, EventArgs e)
        {
            if (!templateUpdated && tabModel.TabPages.Contains(tModel))
            {
                WarningMessage("The template is updated. Youwill need to regenerate the model.");
                mcRunningModel.Enabled = false;
            }
            templateUpdated = true;
        }

        private void mcRunningModel_ModelChanged(object sender, EventArgs e)
        {
            if (!runningModelUpdated && tabModel.TabPages.Contains(tTemplate))
            {
                WarningMessage("Model is updated, you cannot make any more changes to the template.");
                mcTemplate.Enabled = false;
            }
            runningModelUpdated = true;
        }
    }
}