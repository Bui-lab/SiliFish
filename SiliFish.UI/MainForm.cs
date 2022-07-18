using Microsoft.Web.WebView2.Core;
using System.Text.Json;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.PredefinedModels;
using SiliFish.UI.Controls;
using SiliFish.DataTypes;
using SiliFish.Services;
using SiliFish.UI.Extensions;
using Services;
using Microsoft.Web.WebView2.WinForms;
using System.Diagnostics;

namespace SiliFish.UI
{
    public partial class MainForm : Form
    {
        SwimmingModel Model;
        bool modelRefreshMsgShown = false;
        bool modifiedPools = false;
        bool modifiedJncs = false;

        SingleCoilModel scModel;
        DoubleCoilModel dcModel;
        BeatAndGlideModel bgModel;
        CustomSwimmingModel customModel;

        SwimmingModelTemplate ModelTemplate = new();
        string AnimationHTML = "";
        string PlotHTML = "";
        string PlotSubset = "";
        int tPlotStart = 0;
        int tPlotEnd = 0;
        int tRunEnd = 0;
        int tRunSkip = 0;
        PlotType PlotType = PlotType.MembPotential;
        PlotExtend plotExtend = PlotExtend.FullModel;
        CellSelectionStruct plotCellSelection;
        private readonly string tempFolder;
        private string tempFile;
        readonly string outputFolder;
        string lastSavedCustomModelJSON;
        Dictionary<string, object> lastSavedSCParams, lastSavedDCParams, lastSavedBGParams;
        DateTime runStart;

        public MainForm()
        {
            try
            {
                InitializeComponent();
                InitAsync();
                Wait();
                rbCustom.Checked = true;
                splitPlotWindows.SplitterDistance = splitPlotWindows.Width / 2;
                tempFolder = Path.GetTempPath() + "SiliFish";
                outputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
                if (!Directory.Exists(tempFolder))
                    Directory.CreateDirectory(tempFolder);
                else
                {
                    foreach (string f in Directory.GetFiles(tempFolder))
                        File.Delete(f);
                }
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);
                webView2DModel.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
                webView3DModel.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
                webViewAnimation.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;

                listCellPool.AddContextMenu("Sort by Type", listCellPool_SortByNTType);
                listConnections.AddContextMenu("Sort by Type", listConnections_SortByType);
                listConnections.AddContextMenu("Sort by Source", listConnections_SortBySource);
                listConnections.AddContextMenu("Sort by Target", listConnections_SortByTarget);

                foreach (PlotType pt in Enum.GetValues(typeof(PlotType)))
                {
                    ddPlot.Items.Add(pt.GetDisplayName());
                }

                foreach (PlotSelection ps in Enum.GetValues(typeof(PlotSelection)))
                {
                    ddPlotSomiteSelection.Items.Add(ps.GetDisplayName());
                    ddPlotCellSelection.Items.Add(ps.GetDisplayName());
                }
                ddPlotSomiteSelection.SelectedIndex = 0;
                ddPlotCellSelection.SelectedIndex = 0;

                pictureBoxLeft.MouseWheel += PictureBox_MouseWheel;
                pictureBoxRight.MouseWheel += PictureBox_MouseWheel;
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        #region Model selection
        private void linkClearModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (rbSingleCoil.Checked)
                Model = scModel = new SingleCoilModel();
            else if (rbDoubleCoil.Checked)
                Model = dcModel = new DoubleCoilModel();
            else if (rbBeatGlide.Checked)
                Model = bgModel = new BeatAndGlideModel();
            else if (rbCustom.Checked)
                Model = customModel = new CustomSwimmingModel(null);
            WriteParams(Model.GetParameters());
        }

        private void rbSingleCoil_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbSingleCoil.Checked) return;
            if (scModel != null)
                Model = scModel;
            else
            {
                Model = scModel = new SingleCoilModel();
                lastSavedSCParams = Model.GetParameters();
            }
            WriteParams(Model.GetParameters());
        }

        private void rbDoubleCoil_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbDoubleCoil.Checked) return;
            if (dcModel != null)
                Model = dcModel;
            else
            {
                Model = dcModel = new DoubleCoilModel();
                lastSavedDCParams = Model.GetParameters();
            }
            WriteParams(Model.GetParameters());
        }

        private void rbBeatGlide_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbBeatGlide.Checked) return;
            if (bgModel != null)
                Model = bgModel;
            else
            {
                Model = bgModel = new BeatAndGlideModel();
                lastSavedBGParams = Model.GetParameters();
            }
            WriteParams(Model.GetParameters());
        }

        private void rbCustom_CheckedChanged(object sender, EventArgs e)
        {
            linkSaveParam.Text = rbCustom.Checked ? "Save Model" : "Save Parameters";
            linkLoadParam.Text = rbCustom.Checked ? "Load Model" : "Load Parameters";
            lStimulus.Visible = ddStimulusMode.Visible = !rbCustom.Checked;
            if (!rbCustom.Checked)
            {
                if (ddStimulusMode.SelectedIndex == -1)
                    ddStimulusMode.SelectedIndex = 0;
                return;
            }
            if (customModel != null)
                Model = customModel;
            else
            {
                Model = customModel = new CustomSwimmingModel(null);
                lastSavedCustomModelJSON = Util.CreateJSONFromObject(ModelTemplate);
            }
            WriteParams(Model.GetParameters());
            LoadModelTemplate();
            
        }
        #endregion

        #region Read/Load Model and Parameters

        private void ClearCustomModelFields()
        {
            listCellPool.ClearItems();
            listConnections.ClearItems();
            listStimuli.ClearItems();
        }

        private void WriteGeneralParams(Dictionary<string, object> paramDict)
        {
            eModelName.Text = paramDict.Read("General.Name", "");
            eModelDescription.Text = paramDict.Read("General.Description", "");

            eSpinalRostraoCaudal.Value = (decimal)paramDict.ReadDouble("General.SpinalRostralCaudalDistance");
            eSpinalDorsalVentral.Value = (decimal)paramDict.ReadDouble("General.SpinalDorsalVentralDistance");

            eSpinalMedialLateral.Value = (decimal)paramDict.ReadDouble("General.SpinalMedialLateralDistance");
            eNumSomites.Value = (decimal)paramDict.ReadInteger("General.NumberOfSomites");

            eSpinalBodyPosition.Value = (decimal)paramDict.ReadDouble("General.SpinalBodyPosition");

            eBodyDorsalVentral.Value = (decimal)paramDict.ReadDouble("General.BodyDorsalVentralDistance");
            eBodyMedialLateral.Value = (decimal)paramDict.ReadDouble("General.BodyMedialLateralDistance");
        }
        private void WriteParams(Dictionary<string, object> ParamDict)
        {
            if (ParamDict == null) return;
            this.Text = $"SiliFish {Model.ModelName}";
            Model?.FillMissingParameters(ParamDict);
            Model?.SetParameters(ParamDict);

            tabParams.TabPages.Clear();
            if (rbCustom.Checked)
            {
                ClearCustomModelFields();
                tabParams.TabPages.Add(tabCellPools);
                tabParams.TabPages.Add(tabStimuli);
                tabParams.TabPages.Add(tabGeneral);
            }

            //To fill in newly generated params that did not exist during the model creation
            SwimmingModel swimmingModel = new();
            Dictionary<string, object> currentParams = swimmingModel.GetParameters();
            List<string> currentParamGroups = currentParams.Keys.Where(k => k.IndexOf('.') > 0).Select(k => k[..k.IndexOf('.')]).Distinct().ToList();

            List<string> paramGroups = ParamDict?.Keys.Where(k => k.IndexOf('.') > 0).Select(k => k[..k.IndexOf('.')]).Distinct().ToList() ?? new List<string>();
            foreach (string group in paramGroups.Union(currentParamGroups).Distinct())
            {
                Dictionary<string, object> SubDict = ParamDict.Where(x=>x.Key.StartsWith(group)).ToDictionary(x => x.Key, x => x.Value);
                if (group == "General")
                {
                    WriteGeneralParams(SubDict);
                    continue;
                }
                if (SubDict == null || SubDict.Count == 0)
                    SubDict = currentParams.Where(x => x.Key.StartsWith(group)).ToDictionary(x => x.Key, x => x.Value);
                TabPage tabPage = new()
                {
                    Text = group,
                    Tag = "Param"
                };
                tabParams.TabPages.Add(tabPage);
                int maxLen = SubDict.Keys.Select(k => k.Length).Max();
                FlowLayoutPanel flowPanel = new();
                tabPage.Controls.Add(flowPanel);
                flowPanel.FlowDirection = FlowDirection.LeftToRight;
                flowPanel.Dock = DockStyle.Fill;
                flowPanel.AutoScroll = true;
                foreach (KeyValuePair<string, object> kvp in SubDict)
                {
                    Label lbl = new();
                    lbl.Text = kvp.Key[(group.Length + 1)..];
                    lbl.Height = 23;
                    lbl.Width = 8 * maxLen;
                    lbl.TextAlign = ContentAlignment.BottomRight;
                    flowPanel.Controls.Add(lbl);
                    TextBox textBox = new();
                    if (group == "General")
                    {
                        textBox.Width = 200;
                        textBox.Multiline = true;
                        textBox.Height = 60;
                        textBox.ScrollBars = ScrollBars.Both;
                    }
                    textBox.Tag = kvp.Value;
                    textBox.Text = kvp.Value?.ToString();
                    flowPanel.Controls.Add(textBox);
                    flowPanel.SetFlowBreak(textBox, true);
                }
            }
        }

        private void ReadGeneralParams(Dictionary<string, object> paramDict)
        {
            paramDict.Add("General.Name", eModelName.Text);
            paramDict.Add("General.Description", eModelDescription.Text);
            paramDict.Add("General.NumberOfSomites", eNumSomites.Value);
            paramDict.Add("General.SpinalRostralCaudalDistance", eSpinalRostraoCaudal.Value);
            paramDict.Add("General.SpinalDorsalVentralDistance", eSpinalDorsalVentral.Value);
            paramDict.Add("General.SpinalMedialLateralDistance", eSpinalMedialLateral.Value);
            paramDict.Add("General.SpinalBodyPosition", eSpinalBodyPosition.Value);
            paramDict.Add("General.BodyDorsalVentralDistance", eBodyDorsalVentral.Value);
            paramDict.Add("General.BodyMedialLateralDistance", eBodyMedialLateral.Value);
        }

        private void ReadParamsFromTabPage(Dictionary<string, object> ParamDict, TabPage page)
        {
            if (page.Tag?.ToString() != "Param" || ParamDict == null)
                return;

            string key = "";
            FlowLayoutPanel panel = page.Controls[0] as FlowLayoutPanel;
            foreach (Control control in panel.Controls)
            {
                if (control is Label)
                {
                    key = control.Text;
                    continue;
                }
                if (!string.IsNullOrEmpty(key) && control is TextBox)
                {
                    string value = control.Text;
                    object origValue = control.Tag;
                    if (origValue is double)
                        ParamDict.Add(page.Text + "." + key, double.Parse(value));
                    else if (origValue is int)
                        ParamDict.Add(page.Text + "." + key, int.Parse(value));
                    else //if (origValue is string)
                        ParamDict.Add(page.Text + "." + key, value);
                    key = "";
                }
            }
        }
        private Dictionary<string, object> ReadParams(string subGroup)
        {
            Dictionary<string, object> ParamDict = new();
            foreach (TabPage page in tabParams.TabPages)
            {
                if (page.Tag?.ToString() == "Param" && page.Text == subGroup)
                {
                    ReadParamsFromTabPage(ParamDict, page);
                    break;
                }
            }
            return ParamDict;
        }
        private Dictionary<string, object> ReadParams()
        {
            Dictionary<string, object> ParamDict = new();
            ReadGeneralParams(ParamDict);
            foreach (TabPage page in tabParams.TabPages)
            {
                if (page.Tag?.ToString() != "Param")
                    continue;
                ReadParamsFromTabPage(ParamDict, page);
            }
            return ParamDict;
        }

        private void LoadModelTemplatePools()
        {
            listCellPool.ClearItems();
            if (ModelTemplate?.CellPoolTemplates == null) return;
            foreach (CellPoolTemplate cpt in ModelTemplate.CellPoolTemplates)
            {
                listCellPool.AppendItem(cpt);
            }
        }
        private void LoadModelTemplateInterPools()
        {
            listConnections.ClearItems();
            if (ModelTemplate?.InterPoolTemplates == null) return;
            foreach (InterPoolTemplate interPoolTemplate in ModelTemplate.InterPoolTemplates)
            {
                listConnections.AppendItem(interPoolTemplate);
            }
        }

        private void LoadModelTemplateStimuli()
        {
            listStimuli.ClearItems();
            if (ModelTemplate?.AppliedStimuli == null) return;
            foreach (StimulusTemplate stim in ModelTemplate.AppliedStimuli)
            {
                listStimuli.AppendItem(stim);
            }
        }
        private void LoadModelTemplate()
        {
            if (ModelTemplate == null) return;
            Model = customModel = new CustomSwimmingModel(ModelTemplate);
            WriteParams(ModelTemplate.Parameters);

            LoadModelTemplatePools();
            LoadModelTemplateInterPools();
            LoadModelTemplateStimuli();
            this.Text = $"SiliFish {ModelTemplate.ModelName}";
        }
        private void ReadModelTemplatePools(bool includeHidden)
        {
            ModelTemplate.CellPoolTemplates.Clear();
            foreach (var v in listCellPool.GetItems(includeHidden))
            {
                if (v is CellPoolTemplate cpt)
                    ModelTemplate.CellPoolTemplates.Add(cpt);
            }
        }
        private void ReadModelTemplateInterPools(bool includeHidden)
        {
            ModelTemplate.InterPoolTemplates.Clear();
            foreach (var jnc in listConnections.GetItems(includeHidden))
            {
                if (jnc is InterPoolTemplate iptemp)
                {
                    ModelTemplate.InterPoolTemplates.Add(iptemp);
                }
            }
        }

        private void ReadModelTemplateStimuli(bool includeHidden)
        {
            ModelTemplate.AppliedStimuli.Clear();
            foreach (var stim in listStimuli.GetItems(includeHidden))
            {
                if (stim is StimulusTemplate appstim)
                {
                    ModelTemplate.AppliedStimuli.Add(appstim);
                }
            }
        }


        private SwimmingModelTemplate ReadModelTemplate(bool includeHidden)
        {
            try
            {
                ModelTemplate = new SwimmingModelTemplate();

                ReadModelTemplatePools(includeHidden);
                ReadModelTemplateInterPools(includeHidden);
                ReadModelTemplateStimuli(includeHidden);

                ModelTemplate.Parameters = ReadParams();
                string err = ModelTemplate.CheckTemplate();
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show(err);
                    return null;
                }
                return ModelTemplate;
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private static void ExceptionHandling(string name, Exception ex)
        {
            //throw new NotImplementedException();
        }

        private bool SaveParamsOrModel()
        {
            try
            {
                if (saveFileJson.ShowDialog() == DialogResult.OK)
                {
                    if (rbCustom.Checked)
                    {
                        //To make sure deactivated items are saved, even though not displayed
                        //Show all
                        Util.SaveToJSON(saveFileJson.FileName, ReadModelTemplate(includeHidden: true));
                        this.Text = $"SiliFish {ModelTemplate.ModelName}";
                        lastSavedCustomModelJSON = Util.CreateJSONFromObject(ModelTemplate);
                    }
                    else
                    {
                        Util.SaveToJSON(saveFileJson.FileName, ReadParams());
                        if (rbSingleCoil.Checked)
                            lastSavedSCParams = scModel.GetParameters();
                        else if (rbDoubleCoil.Checked)
                            lastSavedDCParams = dcModel.GetParameters();
                        else if (rbBeatGlide.Checked)
                            lastSavedBGParams = bgModel.GetParameters();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        private void linkSaveParam_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveParamsOrModel();
        }

        private void linkLoadParam_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (openFileJson.ShowDialog() == DialogResult.OK)
                {
                    tabParams.Visible = false;
                    Application.DoEvents();
                    if (rbCustom.Checked)
                    {
                        string json = Util.ReadFromFile(openFileJson.FileName);
                        ModelTemplate = (SwimmingModelTemplate)Util.CreateObjectFromJSON(typeof(SwimmingModelTemplate), json);
                        ModelTemplate.LinkObjects();
                        LoadModelTemplate();
                        lastSavedCustomModelJSON = Util.CreateJSONFromObject(ModelTemplate);
                    }
                    else
                    {
                        Dictionary<string, object> ParamDict = Util.ReadDictionaryFromJSON(openFileJson.FileName);
                        WriteParams(ParamDict);
                        if (rbSingleCoil.Checked)
                            lastSavedSCParams = ParamDict;
                        else if (rbDoubleCoil.Checked)
                            lastSavedDCParams = ParamDict;
                        else if (rbBeatGlide.Checked)
                            lastSavedBGParams = ParamDict;
                    }
                }
            }
            catch {
                throw (new Exception());
            }
            finally {
                tabParams.Visible = true;
            }
        }
        #endregion

        #region Run
        private void RunModel()
        {
            try
            {
                Model.MainLoop(0, new RunParam { tMax = tRunEnd, tSkip = tRunSkip });
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
            modifiedJncs = modifiedPools = false;

            btnPlotWindows.Enabled = false;
            btnPlotHTML.Enabled = false;
            btnAnimate.Enabled = false;
            linkSaveRun.Visible = false;

            if (tabOutputs.SelectedTab == tabTextOutput)
                eModelSummary.Text = Model.SummarizeModel();
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
            modifiedJncs = modifiedPools = false;

            btnPlotWindows.Enabled = true;
            btnPlotHTML.Enabled = true;
            btnAnimate.Enabled = true;
            linkSaveRun.Visible = true;

            if (tabOutputs.SelectedTab == tabTextOutput)
                eModelSummary.Text = Model.SummarizeModel();
            TimeSpan ts = DateTime.Now - runStart;
            
            lRunTime.Text = $"Last run: {runStart:t}\r\n" +
                $"Duration: {Util.TimeSpanToString(ts)}\r\n" +
                $"Model: {Model.ModelName}";

        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.Text == "Stop Run")
            {
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

            tRunEnd = (int) eTimeEnd.Value;
            tRunSkip = (int) eSkip.Value;

            if (rbCustom.Checked)
            {
                Model = customModel = new CustomSwimmingModel(ReadModelTemplate(includeHidden: false));
            }
            else if (Model != null)
            {
                Dictionary<string, object> ParamDict = ReadParams();
                Model.SetParameters(ParamDict);
                StimulusMode stimMode = ddStimulusMode.Text == "Gaussian" ? StimulusMode.Gaussian :
                    ddStimulusMode.Text == "Ramp" ? StimulusMode.Ramp :
                    StimulusMode.Step;
                (Model as PredefinedModel).SetStimulusMode(stimMode);
            }
            Model.runParam.tSkip = tRunSkip;
            Model.runParam.tMax = tRunEnd;
            Model.runParam.dt = (double) edt.Value;
            if (Model == null) return;
            btnRun.Text = "Stop Run";
            Task.Run(RunModel);
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            progressBarRun.Value = (int)((Model?.GetProgress() ?? 0) * progressBarRun.Maximum);
        }
        private void linkSaveRun_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        #region Plotting
        #region webViewPlot
        private async void InitAsync()
        {
            await webViewPlot.EnsureCoreWebView2Async();
            await webView2DModel.EnsureCoreWebView2Async();
            await webView3DModel.EnsureCoreWebView2Async();
            await webViewAnimation.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (webViewPlot.Tag == null || webView2DModel.Tag == null || webView3DModel.Tag == null || webViewAnimation.Tag == null)
                Application.DoEvents();
        }
        private void webViewPlot_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webViewPlot.Tag= true;
        }
        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            (sender as Control).Tag = true;
        }
        private void WarningMessage(string s)
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
            
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();

            ((System.ComponentModel.ISupportInitialize)(webView)).BeginInit();
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = System.Drawing.Color.White;
            webView.Dock = System.Windows.Forms.DockStyle.Fill;
            webView.Location = new System.Drawing.Point(0, 30);
            webView.Name = name;
            webView.Size = new System.Drawing.Size(622, 481);
            webView.TabIndex = 1;
            webView.ZoomFactor = 1D;
            webView.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);

            ((System.ComponentModel.ISupportInitialize)(webView)).EndInit();
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
                    target = Path.Combine(outputFolder, prefix + ".html");
                    int suffix = 0;
                    while (File.Exists(target))
                        target = Path.Combine(outputFolder, prefix + (suffix++).ToString() + ".html");
                    File.Copy(tempFile, target);
                    RegenerateWebview(sender as WebView2);
                    Invoke(() => WarningMessage("There was a problem with displaying the html file. It is saved as " + target + "."));
                }
            }
            catch { }
        }
        #endregion

        private async void linkSaveHTML_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (tabOutputs.SelectedTab == tabTextOutput)
                {
                    if (saveFileText.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileText.FileName, eModelSummary.Text);
                    }
                }
                else if (saveFileHTML.ShowDialog() == DialogResult.OK)
                {
                    if (tabOutputs.SelectedTab == tab2DModel)
                    {
                        string htmlHead = JsonSerializer.Deserialize<string>(await webView2DModel.ExecuteScriptAsync("document.head.outerHTML"));
                        string htmlBody = JsonSerializer.Deserialize<string>(await webView2DModel.ExecuteScriptAsync("document.body.outerHTML"));
                        string html = $@"<!DOCTYPE html> <html lang=""en"">{htmlHead}{htmlBody}</html>";
                        File.WriteAllText(saveFileHTML.FileName, html);
                    }
                    else if (tabOutputs.SelectedTab == tab3DModel)
                    {
                        string htmlHead = JsonSerializer.Deserialize<string>(await webView3DModel.ExecuteScriptAsync("document.head.outerHTML"));
                        string htmlBody = JsonSerializer.Deserialize<string>(await webView3DModel.ExecuteScriptAsync("document.body.outerHTML"));
                        string html = $@"<!DOCTYPE html> <html lang=""en"">{htmlHead}{htmlBody}</html>";
                        File.WriteAllText(saveFileHTML.FileName, html);
                    }
                    else if (tabOutputs.SelectedTab == tabPlot)
                        File.WriteAllText(saveFileHTML.FileName, PlotHTML.ToString());
                    else if (tabOutputs.SelectedTab == tabAnimation)
                    {
                        File.WriteAllText(saveFileHTML.FileName, AnimationHTML);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in saving the file:" + exc.Message);
            }
        }

        int lastAnimationStartIndex;
        double[] lastAnimationTimeArray;
        Dictionary<string, Coordinate[]> lastAnimationSpineCoordinates;
        private void btnAnimate_Click(object sender, EventArgs e)
        {
            if (Model == null || !Model.ModelRun) return;

            int tAnimStart = (int)eAnimationStart.Value;
            int tAnimEnd = (int)eAnimationEnd.Value;
            if (tAnimEnd > tRunEnd)
                tAnimEnd = tRunEnd;

            lastAnimationStartIndex = (int)(tAnimStart / Model.runParam.dt);
            int lastAnimationEndIndex = (int)(tAnimEnd / Model.runParam.dt);
            lastAnimationTimeArray = Model.TimeArray;
            //TODO generatespinecoordinates is called twice (once in generateanimation) - fix it
            lastAnimationSpineCoordinates = Model.GenerateSpineCoordinates(lastAnimationStartIndex, lastAnimationEndIndex);
            
            AnimationGenerator animationGenerator = new();
            Model.SetAnimationParameters(ReadParams("Animation"));
            AnimationHTML = animationGenerator.GenerateAnimation(Model, tAnimStart, tAnimEnd);

            webViewAnimation.NavigateTo(AnimationHTML, tempFolder, ref tempFile);
            linkSaveAnimationHTML.Enabled = linkSaveAnimationCSV.Enabled = true;
            lAnimationTime.Text = $"Last animation: {DateTime.Now:t}";
        }


        private void linkSaveAnimationHTML_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(AnimationHTML))
                return;
            if (saveFileHTML.ShowDialog() != DialogResult.OK)
                return;
            File.WriteAllText(saveFileHTML.FileName, AnimationHTML.ToString());
        }

        private void linkSaveAnimationCSV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(AnimationHTML))
                return;
            if (saveFileCSV.ShowDialog() != DialogResult.OK)
                return;

            Util.SaveAnimation(saveFileCSV.FileName, lastAnimationSpineCoordinates, lastAnimationTimeArray, lastAnimationStartIndex);
        }



        private void Generate2DModel()
        {
            if (Model == null) return;

            if (!Model.ModelRun)
            {
                if (rbCustom.Checked)
                {
                    Model = new CustomSwimmingModel(ReadModelTemplate(includeHidden: false));
                }
                else
                {
                    Dictionary<string, object> ParamDict = ReadParams();
                    Model.SetParameters(ParamDict);
                }
            }
            else if (!modelRefreshMsgShown && (modifiedPools || modifiedJncs))
            {
                MessageBox.Show("The changes you make will not be visible until you rerun the model.");
                modelRefreshMsgShown = true;
            }
            TwoDModelGenerator modelGenerator = new();
            string html = modelGenerator.Create2DModel(false, Model, Model.CellPools, (int)webView2DModel.Width/2, webView2DModel.Height);
            webView2DModel.NavigateTo(html, tempFolder, ref tempFile);

        }
        private void btnGenerate2DModel_Click(object sender, EventArgs e)
        {
            Generate2DModel();
        }

        private void Generate3DModel()
        {
            if (Model == null) return;

            if (!Model.ModelRun)
            {
                if (rbCustom.Checked)
                {
                    Model = new CustomSwimmingModel(ReadModelTemplate(includeHidden: false));
                }
                else
                {
                    Dictionary<string, object> ParamDict = ReadParams();
                    Model.SetParameters(ParamDict);
                }
            }
            else if (!modelRefreshMsgShown && (modifiedPools || modifiedJncs))
            {
                MessageBox.Show("The changes you make will not be visible until you rerun the model.");
                modelRefreshMsgShown = true;
            }
            string mode = dd3DModelType.Text;
            bool singlePanel = mode != "Gap/Chem";
            bool gap = mode.Contains("Gap");
            bool chem = mode.Contains("Chem");
            ThreeDModelGenerator threeDModelGenerator = new();
            string html = threeDModelGenerator.Create3DModel(false, Model, Model.CellPools, singlePanel, gap, chem);

            webView3DModel.NavigateTo(html, tempFolder, ref tempFile);
        }

        private void btnGenerate3DModel_Click(object sender, EventArgs e)
        {
            Generate3DModel();
        }

        private void dd3DModelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Generate3DModel();
        }


        private void PopulateCellTypes()
        {
            ddCellsPools.Items.Clear();
            ddCellsPools.Text = "";
            if (Model == null) return;
            if (plotExtend == PlotExtend.FullModel)
            {
                ddCellsPools.Enabled = false;
                return;
            }
            ddCellsPools.Enabled = true;
            List<string> itemList = new();
            foreach (CellPool pool in Model.CellPools)
            {
                if (plotExtend == PlotExtend.CellsInAPool || plotExtend == PlotExtend.SinglePool)
                    itemList.Add(pool.ID);
                else if (plotExtend == PlotExtend.OppositePools)
                    itemList.Add(pool.CellGroup);
                else if (plotExtend == PlotExtend.SingleCell)
                {
                    foreach (Cell cell in pool.GetCells())
                        itemList.Add(cell.ID);
                }
            }
            itemList.Sort();
            ddCellsPools.Items.AddRange(itemList.Distinct().ToArray());
            if (ddCellsPools.Items?.Count > 0)
                ddCellsPools.SelectedIndex = 0;
        }
        private void ddCellsPools_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pool = ddCellsPools.Text;
            List<CellPool> pools = Model.CellPools.Where(cp => cp.ID == pool || cp.CellGroup == pool).ToList();
            ePlotSomiteSelection.Maximum = (decimal)((bool)(pools?.Any()) ? (pools?.Max(p => p.GetCells().Count())) : 1);
        }

        private void ddPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotType plotType = ddPlot.Text.GetValueFromName<PlotType>();
            if (plotType==PlotType.Episodes)
            {
                ddGroupingWindows.Enabled = ddCellsPools.Enabled = false;
                ddGroupingWindows.SelectedIndex = -1;
                ddCellsPools.SelectedIndex = -1;
            }
            else
            {
                ddGroupingWindows.Enabled = ddCellsPools.Enabled = true;
            }
            toolTip.SetToolTip(ddPlot, plotType.GetDescription());
        }
        private void ddGroupingWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddGroupingWindows.Text)
            {
                case "Full":
                    plotExtend = PlotExtend.FullModel;
                    break;
                case "Individual Cell":
                    plotExtend = PlotExtend.SingleCell;
                    break;
                case "Cells in a Pool":
                    plotExtend = PlotExtend.CellsInAPool;
                    break;
                case "Individual Pool":
                    plotExtend = PlotExtend.SinglePool;
                    break;
                case "Pools on Opposite Sides":
                    plotExtend = PlotExtend.OppositePools;
                    break;
            }
            PopulateCellTypes();
        }
        
        #region HTML Plots

        private void CreatePlotHTML()
        {
            if (Model == null) return;
            PlotHTML = "";

            (List<Cell> Cells, List<CellPool> Pools) = Model.GetSubsetCellsAndPools(plotExtend, PlotSubset, plotCellSelection) ;
            PlotHTML = HTMLPlotGenerator.Plot(PlotType, Model.TimeArray, Cells, Pools, plotCellSelection, Model.runParam.dt, tPlotStart, tPlotEnd, tRunSkip);
            Invoke(CompleteCreatePlot);
        }
        private void btnPlotHTML_Click(object sender, EventArgs e)
        {
            if (Model == null || !Model.ModelRun) return;

            PlotType = ddPlot.Text.GetValueFromName<PlotType>();
            PlotSubset = ddCellsPools.Text;

            tPlotStart = (int)ePlotStart.Value;
            tPlotEnd = (int)ePlotEnd.Value;
            if (tPlotEnd > tRunEnd)
                tPlotEnd = tRunEnd;

            tabOutputs.SelectedTab = tabPlot;
            tabPlotSub.SelectedTab = tPlotHTML;
            UseWaitCursor = true;
            btnPlotWindows.Enabled = false;
            btnPlotHTML.Enabled = false;
            plotCellSelection = new CellSelectionStruct()
            {
                somiteSelection = ddPlotSomiteSelection.Text.GetValueFromName<PlotSelection>(),
                nSomite = (int)ePlotSomiteSelection.Value,
                cellSelection = ddPlotCellSelection.Text.GetValueFromName<PlotSelection>(),
                nCell = (int)ePlotCellSelection.Value
            };
            Task.Run(CreatePlotHTML);
        }
        private void CompleteCreatePlot()
        {
            webViewPlot.NavigateTo(PlotHTML, tempFolder, ref tempFile); 
            UseWaitCursor = false;
            btnPlotWindows.Enabled = true;
            btnPlotHTML.Enabled = true;
            lPlotTime.Text = $"Last HTML plot: {DateTime.Now:t}";
        }

        #endregion

        #region Windows Plot

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb.Tag == null)
            {
                pb.InitialImage = pb.Image;
                pb.Tag = 0;
            }
            if (e.Delta > 0)
                pb.Tag = (int)pb.Tag+1;
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

                PlotType = ddPlot.Text.GetValueFromName<PlotType>();
                PlotSubset = ddCellsPools.Text;

                tabOutputs.SelectedTab = tabPlot;
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = true;
                btnPlotWindows.Enabled = false;
                btnPlotHTML.Enabled = false;
                plotCellSelection = new CellSelectionStruct()
                {
                    somiteSelection = ddPlotSomiteSelection.Text.GetValueFromName<PlotSelection>(),
                    nSomite = (int)ePlotSomiteSelection.Value,
                    cellSelection = ddPlotCellSelection.Text.GetValueFromName<PlotSelection>(),
                    nCell = (int)ePlotCellSelection.Value
                };

                tPlotStart = (int)ePlotStart.Value;
                tPlotEnd = (int)ePlotEnd.Value;
                if (tPlotEnd > tRunEnd)
                    tPlotEnd = tRunEnd;

                (List<Cell> Cells, List<CellPool> Pools) = Model.GetSubsetCellsAndPools(plotExtend, PlotSubset, plotCellSelection);

                (List<Image> leftImages, List<Image> rightImages) = WindowsPlotGenerator.Plot(PlotType, Model, Cells, Pools, plotCellSelection,
                    Model.runParam.dt, tPlotStart, tPlotEnd, tRunSkip);

                leftImages?.RemoveAll(img => img == null);
                rightImages?.RemoveAll(img => img == null);

                int ncol = (!ddGroupingWindows.Enabled || plotExtend != PlotExtend.FullModel) && leftImages?.Count > 1 ? 2 : 1;
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
                    ncol = plotExtend != PlotExtend.FullModel && rightImages?.Count > 1 ? 2 : 1;
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
                lPlotTime.Text = $"Last plot: {DateTime.Now:t}";
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = false;
                btnPlotWindows.Enabled = true;
                btnPlotHTML.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        private void btnPlotWindows_Click(object sender, EventArgs e)
        {
            PlotWindows();
        }



        #endregion

        #endregion

        #region Custom Model

        #region Cell Pool
        private CellPoolTemplate OpenCellPoolDialog(CellPoolTemplate pool)
        {
            ControlContainer frmControl = new();
            CellPoolControl cpl = new();
            cpl.LoadPool += Cpl_LoadPool;
            cpl.SavePool += Cpl_SavePool;

            cpl.PoolTemplate = pool;
            frmControl.AddControl(cpl);
            frmControl.Text = pool?.ToString() ?? "New Cell Pool";

            if (frmControl.ShowDialog() == DialogResult.OK)
                return cpl.PoolTemplate;
            return null;
        }
        private void Cpl_SavePool(object sender, EventArgs e)
        {
            if (saveFileJson.ShowDialog() == DialogResult.OK)
            {
                CellPoolControl cpl = (CellPoolControl)sender;
                Util.SaveToFile(saveFileJson.FileName, cpl.JSONString);
                modifiedPools = true;
            }
        }
        private void Cpl_LoadPool(object sender, EventArgs e)
        {
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                CellPoolControl cpl = (CellPoolControl)sender;
                cpl.JSONString= Util.ReadFromFile(openFileJson.FileName);
            }
        }

        private void listCellPool_AddItem(object sender, EventArgs e)
        {
            CellPoolTemplate newPool = OpenCellPoolDialog(null);
            if (newPool != null)
            {
                ModelTemplate.CellPoolTemplates.Add(newPool);
                listCellPool.AppendItem(newPool);
                modifiedPools = true;
            }
        }
        private void listCellPool_CopyItem(object sender, EventArgs e)
        {
            if (listCellPool.SelectedItem == null)
                return;
            CellPoolTemplate pool = new(listCellPool.SelectedItem as CellPoolTemplate);
            pool = OpenCellPoolDialog(pool); 
            if (pool != null)
            {
                ModelTemplate.CellPoolTemplates.Add(pool);
                listCellPool.AppendItem(pool);
                modifiedPools = true;
            }
        }
        private void listCellPool_DeleteItem(object sender, EventArgs e)
        {
            if (listCellPool.SelectedIndex >= 0)
            {
                string msg = "Deleting a cell pool will remove all of its conections and applied stimuli as well. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    CellPoolTemplate cpl = (CellPoolTemplate)listCellPool.SelectedItem;
                    ReadModelTemplateInterPools(includeHidden: true);
                    ReadModelTemplateStimuli(includeHidden: true);
                    ModelTemplate.RemoveCellPool(cpl);
                    listCellPool.RemoveItemAt(listCellPool.SelectedIndex);
                    LoadModelTemplateInterPools();
                    LoadModelTemplateStimuli();
                    modifiedPools = true;
                    modifiedJncs = true;
                }
            }
        }
        private void listCellPool_ViewItem(object sender, EventArgs e)
        {
            if (listCellPool.SelectedItem == null)
                return;
            CellPoolTemplate pool = listCellPool.SelectedItem as CellPoolTemplate;
            string oldName = pool.CellGroup;
            pool = OpenCellPoolDialog(pool); //check modeltemplate's list
            if (pool != null)
            {
                if (oldName != pool.CellGroup)
                    ModelTemplate.RenameCellPool(oldName, pool.CellGroup);
                int ind = listCellPool.SelectedIndex;
                listCellPool.RefreshItem(ind, pool);
                LoadModelTemplateInterPools();
                modifiedPools = true;
            }
        }

        private void listCellPool_ActivateItem(object sender, EventArgs e)
        {
            CellPoolTemplate pool = listCellPool.SelectedItem as CellPoolTemplate;
            ModelTemplate.CellPoolTemplates.FirstOrDefault(p => p.Distinguisher == pool.Distinguisher).Active = pool.Active;
            modifiedPools = true;
        }

        private void listCellPool_SortItems(object sender, EventArgs e)
        {
            ModelTemplate.CellPoolTemplates.Sort();
            LoadModelTemplatePools();
        }

        private void listCellPool_SortByNTType(object sender, EventArgs e)
        {
            ModelTemplate.CellPoolTemplates = ModelTemplate.CellPoolTemplates
                .OrderBy(p => p.CellType.ToString())
                .ThenBy(p => p.NTMode.ToString())
                .ToList();
            LoadModelTemplatePools();
        }

        #endregion

        #region Connection
        private InterPoolTemplate OpenConnectionDialog(InterPoolTemplate interpool)
        {
            ControlContainer frmControl = new();
            InterPoolControl ipl = new();

            ipl.SetInterPoolTemplate(ModelTemplate.CellPoolTemplates, interpool, ModelTemplate);
            frmControl.AddControl(ipl);
            frmControl.Text = interpool?.ToString() ?? "New Connection";

            if (frmControl.ShowDialog() == DialogResult.OK)
            {
                InterPoolTemplate interPoolTemplate = ipl.GetInterPoolTemplate();
                ModelTemplate.LinkObjects(interPoolTemplate);
                return interPoolTemplate;
            }
            return null;
        }

        private void listConnections_AddItem(object sender, EventArgs e)
        {
            InterPoolTemplate newJnc = OpenConnectionDialog(null);
            if (newJnc != null)
            {
                ModelTemplate.InterPoolTemplates.Add(newJnc);
                listConnections.AppendItem(newJnc);
                modifiedJncs = true;
            }
        }
        private void listConnections_CopyItem(object sender, EventArgs e)
        {
            if (listConnections.SelectedItem == null)
                return;
            InterPoolTemplate jnc = new(listConnections.SelectedItem as InterPoolTemplate);
            jnc = OpenConnectionDialog(jnc); 
            if (jnc != null)
            {
                ModelTemplate.InterPoolTemplates.Add(jnc);
                listConnections.AppendItem(jnc);
                modifiedJncs = true;
            }
        }
        private void listConnections_DeleteItem(object sender, EventArgs e)
        {
            if (listConnections.SelectedIndex >= 0)
            {
                InterPoolTemplate ipl = (InterPoolTemplate)listConnections.SelectedItem;
                ModelTemplate.InterPoolTemplates.Remove(ipl);
                listConnections.RemoveItemAt(listConnections.SelectedIndex);
                modifiedJncs = true;
            }
        }
        private void listConnections_ViewItem(object sender, EventArgs e)
        {
            if (listConnections.SelectedItem == null)
                return;
            InterPoolTemplate interpool = listConnections.SelectedItem as InterPoolTemplate;
            interpool = OpenConnectionDialog(interpool); //check modeltemplate's list
            if (interpool != null)
            {
                int ind = listConnections.SelectedIndex;
                listConnections.RefreshItem(ind, interpool);
                modifiedJncs = true;
            }
        }

        private void listConnections_ActivateItem(object sender, EventArgs e)
        {
            InterPoolTemplate jnc = sender as InterPoolTemplate;
            ModelTemplate.InterPoolTemplates.FirstOrDefault(p => p.Distinguisher == jnc.Distinguisher).Active = jnc.Active;
            modifiedJncs = true;
        }

        private void listConnections_SortItems(object sender, EventArgs e)
        {
            ModelTemplate.InterPoolTemplates.Sort();
            LoadModelTemplateInterPools();
        }

        private void listConnections_SortByType(object sender, EventArgs e)
        {
            ModelTemplate.InterPoolTemplates = ModelTemplate.InterPoolTemplates
                .OrderBy(jnc => jnc.ConnectionType.ToString())
                .ToList();
            LoadModelTemplateInterPools();
        }
        private void listConnections_SortBySource(object sender, EventArgs e)
        {
            ModelTemplate.InterPoolTemplates = ModelTemplate.InterPoolTemplates
                .OrderBy(jnc => jnc.PoolSource)
                .ToList();
            LoadModelTemplateInterPools();
        }

        private void listConnections_SortByTarget(object sender, EventArgs e)
        {
            ModelTemplate.InterPoolTemplates = ModelTemplate.InterPoolTemplates
                .OrderBy(jnc => jnc.PoolTarget)
                .ToList();
            LoadModelTemplateInterPools();
        }

        #endregion

        #region Stimulus

        private StimulusTemplate OpenStimulusDialog(StimulusTemplate stim)
        {
            ControlContainer frmControl = new();
            StimulusControl sc = new StimulusControl();

            sc.SetStimulus(ModelTemplate.CellPoolTemplates, stim);
            frmControl.AddControl(sc);
            frmControl.Text = stim?.ToString() ?? "New Stimulus";

            if (frmControl.ShowDialog() == DialogResult.OK)
                return sc.GetStimulus();
            return null;
        }
        private void listStimuli_AddItem(object sender, EventArgs e)
        {
            StimulusTemplate stimulus = OpenStimulusDialog(null);
            if (stimulus != null)
            {
                ModelTemplate.AppliedStimuli.Add(stimulus);
                listStimuli.AppendItem(stimulus);
            }
        }
        private void listStimuli_CopyItem(object sender, EventArgs e)
        {
            if (listStimuli.SelectedItem == null)
                return;
            StimulusTemplate stimulus = listStimuli.SelectedItem as StimulusTemplate;
            stimulus = OpenStimulusDialog(stimulus);
            if (stimulus != null)
            {
                ModelTemplate.AppliedStimuli.Add(stimulus);
                listStimuli.AppendItem(stimulus);
            }
        }
        private void listStimuli_DeleteItem(object sender, EventArgs e)
        {
            if (listStimuli.SelectedIndex >= 0)
            {
                StimulusTemplate stim = (StimulusTemplate)listStimuli.SelectedItem;
                ModelTemplate.AppliedStimuli.Remove(stim);
                listStimuli.RemoveItemAt(listStimuli.SelectedIndex);
            }
        }
        private void listStimuli_ViewItem(object sender, EventArgs e)
        {
            if (listStimuli.SelectedItem == null)
                return;
            StimulusTemplate stimulus = listStimuli.SelectedItem as StimulusTemplate;
            stimulus = OpenStimulusDialog(stimulus); //check modeltemplate's list
            if (stimulus != null)
            {
                int ind = listStimuli.SelectedIndex;
                listStimuli.RefreshItem(ind, stimulus);
            }
        }

        private void listStimuli_ActivateItem(object sender, EventArgs e)
        {
            StimulusTemplate stim = sender as StimulusTemplate;
            ModelTemplate.AppliedStimuli.FirstOrDefault(p => p.Distinguisher == stim.Distinguisher).Active = stim.Active;
        }



        #endregion
        #endregion


        private void splitWindows_DoubleClick(object sender, EventArgs e)
        {
            splitPlotWindows.SplitterDistance = splitPlotWindows.Width / 2;
        }

        private void tabOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabOutputs.SelectedTab == tabTextOutput)
            {
                eModelSummary.Text = Model.SummarizeModel();
                linkSaveHTML.Text = "Save Text";
            }
            else
            {
                linkSaveHTML.Text = "Save HTML";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ModelTemplate != null)
                {
                    //check whether the custom model has changed
                    string jsonstring = Util.CreateJSONFromObject(ModelTemplate);
                    if (jsonstring != lastSavedCustomModelJSON)
                    {
                        rbCustom.Checked = true;
                        DialogResult res = MessageBox.Show("The custom model has changed. Do you want to save the modifications?", "Model Save", MessageBoxButtons.YesNoCancel);
                        if (res == DialogResult.Yes)
                        {
                            if (!SaveParamsOrModel())
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
                    //check whether the predefined model parameters have changed
                    if (lastSavedSCParams != null && lastSavedSCParams.Any() && !scModel.GetParameters().SameAs(lastSavedSCParams))
                    {
                        rbSingleCoil.Checked = true;
                        DialogResult res = MessageBox.Show("The single coil model parameters have changed. Do you want to save the modifications?", "Model Save", MessageBoxButtons.YesNoCancel);
                        if (res == DialogResult.Yes)
                        {
                            if (!SaveParamsOrModel())
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
                    if (lastSavedDCParams != null && lastSavedDCParams.Any() && !dcModel.GetParameters().SameAs(lastSavedDCParams))
                    {
                        rbDoubleCoil.Checked = true;
                        DialogResult res = MessageBox.Show("The double coil model parameters have changed. Do you want to save the modifications?", "Model Save", MessageBoxButtons.YesNoCancel);
                        if (res == DialogResult.Yes)
                        {
                            if (!SaveParamsOrModel())
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
                    if (lastSavedBGParams != null && lastSavedBGParams.Any() && !bgModel.GetParameters().SameAs(lastSavedBGParams))
                    {
                        rbBeatGlide.Checked = true;
                        DialogResult res = MessageBox.Show("The beat and glide coil model parameters have changed. Do you want to save the modifications?", "Model Save", MessageBoxButtons.YesNoCancel);
                        if (res == DialogResult.Yes)
                        {
                            if (!SaveParamsOrModel())
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
                foreach (string f in Directory.GetFiles(tempFolder))
                    File.Delete(f);
            }
            catch { }
        }

        private void pTop_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            About about = new();
            about.ShowDialog();
        }

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

        private void linkBrowseToOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", outputFolder);
            }
            catch { }
        }

        private void ddPlotSomiteSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotSelection sel = ddPlotSomiteSelection.Text.GetValueFromName<PlotSelection>();
            ePlotSomiteSelection.Enabled = sel == PlotSelection.Random;
        }

        private void ddPlotCellSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotSelection sel = ddPlotCellSelection.Text.GetValueFromName<PlotSelection>();
            ePlotCellSelection.Enabled = sel == PlotSelection.Random;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            About about = new About();
            about.SetTimer(2000);
            about.ShowDialog();
        }


        bool skipSizeChanged = false;
        private void pBodyDiagrams_SizeChanged(object sender, EventArgs e)
        {
            if (skipSizeChanged) return;
            skipSizeChanged = true;
            int prevWidth = picRostroCaudal.Width;
            picCrossSection.Width = picCrossSection.Height = picRostroCaudal.Width = pBodyDiagrams.Width - 6;
            picRostroCaudal.Height = picRostroCaudal.Width * picRostroCaudal.Height / prevWidth;
            picRostroCaudal.Top = picCrossSection.Bottom + 6;
            pBodyDiagrams.Height = picCrossSection.Height + picRostroCaudal.Height + 12;
            skipSizeChanged = false;
        }

    }
}