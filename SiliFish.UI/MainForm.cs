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

namespace SiliFish.UI
{
    public partial class MainForm : Form
    {
        RunningModel Model;

        ModelTemplate ModelTemplate = new();
        string htmlAnimation = "";
        string htmlPlot = "";
        string PlotSubset = "";
        decimal tAnimdt;
        int tAnimStart = 0;
        int tAnimEnd = 0;
        int tPlotStart = 0;
        int tPlotEnd = 0;
        int tRunEnd = 0;
        int tRunSkip = 0;
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
                ModelTemplate.Settings.TempFolder = Path.GetTempPath() + "SiliFish";
                ModelTemplate.Settings.OutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
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
                //TODO LoadModelTemplate();
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

        private void RefreshModel()
        {
            //TODO if (modelUpdated && Model != null)
                return;
            //TODO modelUpdated = false;
            //TODO ReadModelTemplate(includeHidden: false);
            Model = new RunningModel(ModelTemplate);
        }

        private void RefreshModelSafeMode()
        {
            //TODO if (modelUpdated) return;
            if (Model == null || !Model.ModelRun)
                RefreshModel();
            else //TODO if (modifiedJncs || modifiedPools)
            {
                string msg = "Do you want to recreate the model using the modifications you have done in the UI? You will need to rerun the simulation.";
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
                RunParam rp = new() { tMax = tRunEnd, tSkip_ms = tRunSkip };
                Model.RunModel(0, rp, (int)eRunNumber.Value);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (Model.ModelRun)
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
            //TODO modifiedJncs = modifiedPools = false;

            btnPlotWindows.Enabled = false;
            btnPlotHTML.Enabled = false;
            btnAnimate.Enabled = false;
            linkExportOutput.Visible = false;

            lRunTime.Text = $"Last run cancelled\r\n" +
                $"Model: {Model.ModelName}";
        }
        private void CompleteRun()
        {
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            progressBarRun.Visible = false;
            btnRun.Text = "Run";
            //TODO modifiedJncs = modifiedPools = false;

            PopulatePlotPools();
            int NumberOfSomites = Model.ModelDimensions.NumberOfSomites;
            ddPlotSomiteSelection.Enabled = ePlotSomiteSelection.Enabled = NumberOfSomites > 0;
            eKinematicsSomite.Maximum = NumberOfSomites > 0 ? NumberOfSomites : Model.CellPools.Max(p => p.Cells.Max(c => c.Sequence));

            btnPlotWindows.Enabled = true;
            btnPlotHTML.Enabled = true;
            btnAnimate.Enabled = true;
            linkExportOutput.Visible = true;
            btnGenerateEpisodes.Enabled = true;

            TimeSpan ts = DateTime.Now - runStart;

            lRunTime.Text = $"Last run: {runStart:t}\r\n" +
                $"Duration: {Util.TimeSpanToString(ts)}\r\n" +
                $"Model: {Model.ModelName}";

        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.Text == "Stop Run")
            {
                if (Model != null)
                    Model.CancelRun = true;
                return;
            }
            runStart = DateTime.Now;
            lRunTime.Text = "";
            UseWaitCursor = true;
            progressBarRun.Value = 0;
            progressBarRun.Visible = true;
            //btnRun.Enabled = false;
            timerRun.Enabled = true;

            tRunEnd = (int)eTimeEnd.Value;
            tRunSkip = (int)eSkip.Value;

            RefreshModel();
            Model.RunParam.tSkip_ms = tRunSkip;
            Model.RunParam.tMax = tRunEnd;
            Model.RunParam.dt = (double)edt.Value;
            Model.RunParam.dtEuler = (double)edtEuler.Value;
            if (Model == null) return;
            btnRun.Text = "Stop Run";
            Task.Run(RunModel);
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            progressBarRun.Value = (int)((Model?.GetProgress() ?? 0) * progressBarRun.Maximum);
            if (eRunNumber.Value > 1)
            {
                int? i = Model?.GetRunCounter();
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
                Model.SaveToFile(saveFileCSV.FileName);
            }
        }

        #endregion

        #region Outputs

        #region HTML and Windows Plots - Common Functions
        private void DisplayNumberOfPlots()
        {
            if (Model == null) return;
            GetPlotSubset();
            (List<Cell> Cells, List<CellPool> Pools) = Model.GetSubsetCellsAndPools(PlotSubset, plotCellSelection);
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
            List<CellPool> pools = Model.CellPools.Where(cp => (pool == "All" || cp.CellGroup == pool) && cp.OnSide(sagittal)).ToList();
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
                    ePlotSomiteSelection.Enabled = Model?.ModelDimensions.NumberOfSomites > 0;
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
            if (Model == null) return;
            List<string> itemList = new();
            itemList.AddRange(Model.CellPools.Select(p => p.CellGroup).OrderBy(p => p).ToArray());
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
            if (tPlotEnd > tRunEnd)
                tPlotEnd = tRunEnd;
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
            if (Model == null) return;
            htmlPlot = "";

            (List<Cell> Cells, List<CellPool> Pools) = Model.GetSubsetCellsAndPools(PlotSubset, plotCellSelection);
            (string Title, LastPlottedCharts) = PlotDataGenerator.GetPlotData(PlotType, Model, Cells, Pools, plotCellSelection, tPlotStart, tPlotEnd, tRunSkip);

            htmlPlot = DyChartGenerator.Plot(Title, LastPlottedCharts,
                (int)ePlotWidth.Value, (int)ePlotHeight.Value);
            Invoke(CompletePlotHTML);
        }
        private void btnPlotHTML_Click(object sender, EventArgs e)
        {
            if (Model == null || !Model.ModelRun) return;

            GetPlotSubset();
            if (PlotType == PlotType.Episodes)
                Model.SetAnimationParameters(ModelTemplate.KinemParam);
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
                if (Model == null || !Model.ModelRun) return;

                GetPlotSubset();
                if (PlotType == PlotType.Episodes)
                    Model.SetAnimationParameters(ModelTemplate.KinemParam);
                tabOutputs.SelectedTab = tabPlot;
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = true;
                btnPlotWindows.Enabled = false;
                btnPlotHTML.Enabled = false;

                (List<Cell> Cells, List<CellPool> Pools) = Model.GetSubsetCellsAndPools(PlotSubset, plotCellSelection);

                (List<Image> leftImages, List<Image> rightImages) = WindowsPlotGenerator.Plot(PlotType, Model, Cells, Pools, plotCellSelection,
                    Model.RunParam.dt, tPlotStart, tPlotEnd, tRunSkip);

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
            if (Model == null) return;
            RefreshModelSafeMode();
            TwoDModelGenerator modelGenerator = new();
            string html = modelGenerator.Create2DModel(false, Model, Model.CellPools, (int)webView2DModel.Width / 2, webView2DModel.Height);
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
                RefreshModelSafeMode();
                ThreeDModelGenerator threeDModelGenerator = new();
                string html = threeDModelGenerator.Create3DModel(saveFile:false, Model, Model.CellPools, 
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
            e3DSomiteRange.Visible = !cb3DAllSomites.Checked;
            string func = $"SetSomites([]);";
            if (!cb3DAllSomites.Checked)
            {
                List<int> somites = Util.ParseRange(e3DSomiteRange.Text, 1, Model.ModelDimensions.NumberOfSomites);
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
            if (lastSomiteSelection != e3DSomiteRange.Text)
            {
                List<int> somites = Util.ParseRange(e3DSomiteRange.Text, 1, Model.ModelDimensions.NumberOfSomites);
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
                htmlAnimation = AnimationGenerator.GenerateAnimation(Model, tAnimStart, tAnimEnd, (double)tAnimdt);
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
            if (Model == null || !Model.ModelRun) return;

            btnAnimate.Enabled = false;

            tAnimStart = (int)eAnimationStart.Value;
            tAnimEnd = (int)eAnimationEnd.Value;
            if (tAnimStart > tRunEnd || tAnimStart < 0)
                tAnimStart = 0;
            if (tAnimEnd > tRunEnd)
                tAnimEnd = tRunEnd;

            lastAnimationStartIndex = (int)(tAnimStart / Model.RunParam.dt);
            int lastAnimationEndIndex = (int)(tAnimEnd / Model.RunParam.dt);
            lastAnimationTimeArray = Model.TimeArray;
            //TODO generatespinecoordinates is called twice (once in generateanimation) - fix it
            Model.SetAnimationParameters(ModelTemplate.KinemParam);
            lastAnimationSpineCoordinates = SwimmingKinematics.GenerateSpineCoordinates(Model, lastAnimationStartIndex, lastAnimationEndIndex);

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
                ExceptionHandler.ExceptionHandling("Read template from JSON", exc);
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
            RefreshModelSafeMode();
            try
            {
                eModelJSON.Text = JsonUtil.ToJson(Model);
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
                    Model = model;
                    Model.LinkObjects();
                    //TODO modelUpdated = true;
                }
            }
            catch (JsonException exc)
            {
                WarningMessage($"There is a problem with the JSON file. Please check the format of the text in a JSON editor. {exc.Message}");
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling("Read template from JSON", exc);
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
            (List<Cell> LeftMNs, List<Cell> RightMNs) = Model.GetMotoNeurons((int)eKinematicsSomite.Value);
            (List<SwimmingEpisode> episodesLeft, List<SwimmingEpisode> episodesRight) = SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(Model, LeftMNs, RightMNs,
                Model.KinemParam.BurstBreak, Model.KinemParam.EpisodeBreak);
            string html = DyChartGenerator.PlotSummaryMembranePotentials(Model, LeftMNs.Union(RightMNs).ToList(),
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

    }
}