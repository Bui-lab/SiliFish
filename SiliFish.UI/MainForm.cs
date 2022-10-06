using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Services;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.PredefinedModels;
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
        SwimmingModel Model;
        bool modelRefreshMsgShown = false;
        bool jsonRefreshMsgShown = false;
        bool modifiedPools = false;
        bool modifiedJncs = false;

        SingleCoilModel scModel;
        DoubleCoilModel dcModel;
        BeatAndGlideModel bgModel;
        CustomSwimmingModel customModel;
        bool modelUpdated = false;

        SwimmingModelTemplate ModelTemplate = new();
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
        private readonly string tempFolder;
        private string tempFile;
        readonly string outputFolder;
        string lastSavedCustomModelJSON;
        Dictionary<string, object> lastSavedSCParams, lastSavedDCParams, lastSavedBGParams, lastSavedCustomParams;
        Dictionary<string, object> lastRunSCParams, lastRunDCParams, lastRunBGParams, lastRunCustomParams;
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
                webViewPlot.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
                webViewSummaryV.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;

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
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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

        #region Model selection

        private void RefreshModel()
        {
            if (modelUpdated && Model != null)
                return;
            modelUpdated = false;
            if (rbCustom.Checked)
            {
                ReadModelTemplate(includeHidden: false);
                RefreshModelFromTemplate();
            }
            else
            {
                Model?.SetParameters(ReadParams());
            }
        }

        private void RefreshModelFromTemplate()
        {
            Model = customModel = new CustomSwimmingModel(ModelTemplate);
            lastSavedCustomParams = ModelTemplate.Parameters;
        }
        private void SwitchToModel()
        {
            if (Model == null) return;
            ddPlotSomiteSelection.Enabled = ePlotSomiteSelection.Enabled = Model.NumberOfSomites > 0;
            if (rbSingleCoil.Checked)
                LoadParams(Model.ModelRun ? lastRunSCParams : lastSavedSCParams);
            else if (rbDoubleCoil.Checked)
                LoadParams(Model.ModelRun ? lastRunDCParams : lastSavedDCParams);
            else if (rbBeatGlide.Checked)
                LoadParams(Model.ModelRun ? lastRunBGParams : lastSavedBGParams);
            else //custom
            {
                LoadParams(Model.ModelRun ? lastRunCustomParams : lastSavedCustomParams);
                LoadModelTemplate();
            }
        }
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
            LoadParams(Model.GetParameters());
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
            SwitchToModel();
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
            SwitchToModel();
        }

        private void rbBeatGlide_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbBeatGlide.Checked) return;
            if (bgModel != null)
            {
                Model = bgModel;
            }
            else
            {
                Model = bgModel = new BeatAndGlideModel();
                lastSavedBGParams = Model.GetParameters();
            }
            SwitchToModel();
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
                lastSavedCustomParams = Model.GetParameters();
                lastSavedCustomModelJSON = Util.CreateJSONFromObject(ModelTemplate);
            }
            SwitchToModel();
        }
        #endregion

        #region Read/Load Model and Parameters

        private void ClearCustomModelFields()
        {
            listCellPool.ClearItems();
            listConnections.ClearItems();
            listStimuli.ClearItems();
        }

        private void LoadGeneralParams(Dictionary<string, object> paramDict)
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
        private void LoadParams(Dictionary<string, object> ParamDict)
        {
            ddPlotSomiteSelection.Enabled = ePlotSomiteSelection.Enabled = Model.NumberOfSomites > 0;

            if (ParamDict == null) return;
            this.Text = $"SiliFish {Model.ModelName}";

            tabParams.TabPages.Clear();
            if (rbCustom.Checked)
            {
                ClearCustomModelFields();
                tabParams.TabPages.Add(tabGeneral);
                tabParams.TabPages.Add(tabCellPools);
                tabParams.TabPages.Add(tabStimuli);
            }

            //To fill in newly generated params that did not exist during the model creation
            SwimmingModel swimmingModel = new();
            Dictionary<string, object> currentParams = swimmingModel.GetParameters();
            Dictionary<string, object> paramDesc = swimmingModel.GetParameterDesc();
            List<string> currentParamGroups = currentParams.Keys.Where(k => k.IndexOf('.') > 0).Select(k => k[..k.IndexOf('.')]).Distinct().ToList();

            List<string> paramGroups = ParamDict?.Keys.Where(k => k.IndexOf('.') > 0).Select(k => k[..k.IndexOf('.')]).Distinct().ToList() ?? new List<string>();
            int tabIndex = rbCustom.Checked ? 1 : 0;
            var _ = tabParams.Handle;//requires for the insert command to work
            foreach (string group in paramGroups.Union(currentParamGroups).Distinct())
            {
                Dictionary<string, object> SubDict = ParamDict.Where(x => x.Key.StartsWith(group)).ToDictionary(x => x.Key, x => x.Value);
                if (group == "General")
                {
                    LoadGeneralParams(SubDict);
                    continue;
                }
                if (SubDict == null || SubDict.Count == 0)
                    SubDict = currentParams.Where(x => x.Key.StartsWith(group)).ToDictionary(x => x.Key, x => x.Value);
                TabPage tabPage = new()
                {
                    Text = group,
                    Tag = "Param"
                };
                //insert after the general tab, keep Animation at the end
                if (group == "Kinematics")
                    tabParams.TabPages.Add(tabPage);
                else
                    tabParams.TabPages.Insert(tabIndex++, tabPage);
                int maxLen = SubDict.Keys.Select(k => k.Length).Max();
                FlowLayoutPanel flowPanel = new();
                tabPage.Controls.Add(flowPanel);
                flowPanel.FlowDirection = FlowDirection.LeftToRight;
                flowPanel.Dock = DockStyle.Fill;
                flowPanel.AutoScroll = true;
                foreach (KeyValuePair<string, object> kvp in SubDict)
                {
                    paramDesc.TryGetValue(kvp.Key, out object desc);
                    Label lbl = new()
                    {
                        Text = kvp.Key[(group.Length + 1)..],
                        Height = 23,
                        Width = 5 * maxLen,
                        TextAlign = ContentAlignment.BottomRight
                    };
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
                    if (desc != null)
                    {
                        toolTip.SetToolTip(lbl, desc.ToString());
                        toolTip.SetToolTip(textBox, desc.ToString());
                    }
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

        private string CheckJSONVersion(string json)
        {
            //Compare to Version 0.1
            if (json.Contains("\"TimeLine\""))
            {
                //createa by old version 
                json = json.Replace("\"TimeLine\"", "\"TimeLine_ms\"");
                MessageBox.Show("This file was generated by an old version of SiliFish. " +
                    "Please double check the stimulus parameters for accuracy before running the model.");
            }
            return json;
        }
        private bool SaveParamsOrModelTemplate()
        {
            try
            {
                SwimmingModelTemplate mt = rbCustom.Checked ? ReadModelTemplate(includeHidden: true) : null;

                if (string.IsNullOrEmpty(saveFileJson.FileName))
                    saveFileJson.FileName = mt?.ModelName ?? Model?.ModelName;
                if (saveFileJson.ShowDialog() == DialogResult.OK)
                {
                    if (rbCustom.Checked)
                    {
                        //To make sure deactivated items are saved, even though not displayed
                        //Show all
                        Util.SaveToJSON(saveFileJson.FileName, ModelTemplate);
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
            SaveParamsOrModelTemplate();
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
                        json = CheckJSONVersion(json);
                        ModelTemplate = (SwimmingModelTemplate)Util.CreateObjectFromJSON(typeof(SwimmingModelTemplate), json);
                        ModelTemplate.LinkObjects();
                        Model = customModel = new CustomSwimmingModel(ModelTemplate);
                        lastSavedCustomParams = ModelTemplate.Parameters; //needs to be set before SwitchToModel
                        SwitchToModel();
                        lastSavedCustomModelJSON = Util.CreateJSONFromObject(ModelTemplate);
                    }
                    else
                    {
                        Dictionary<string, object> ParamDict = Util.ReadDictionaryFromJSON(openFileJson.FileName);
                        Model?.SetParameters(ParamDict);
                        if (rbSingleCoil.Checked)
                            lastSavedSCParams = ParamDict;
                        else if (rbDoubleCoil.Checked)
                            lastSavedDCParams = ParamDict;
                        else if (rbBeatGlide.Checked)
                            lastSavedBGParams = ParamDict;
                        SwitchToModel();
                    }
                }
            }
            catch
            {
                MessageBox.Show("There is a problem in reading the JSON file. Please make sure it is formatted properly.");
                throw (new Exception());
            }
            finally
            {
                tabParams.Visible = true;
            }
        }
        #endregion

        #region Run
        private void RunModel()
        {
            try
            {
                RunParam rp = new() { tMax = tRunEnd, tSkip_ms = tRunSkip };
                if (cbMultiple.Checked)
                    Model.RunModel(0, rp, (int)eRunNumber.Value);
                else
                    Model.RunModel(0, rp);
                if (Model is SingleCoilModel)
                    lastRunSCParams = Model.GetParameters();
                else if (Model is DoubleCoilModel)
                    lastRunDCParams = Model.GetParameters();
                else if (Model is BeatAndGlideModel)
                    lastRunBGParams = Model.GetParameters();
                else lastRunCustomParams = Model.GetParameters();

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

            PopulatePlotPools();
            ddPlotSomiteSelection.Enabled = ePlotSomiteSelection.Enabled = Model.NumberOfSomites > 0;
            eKinematicsSomite.Maximum = Model.NumberOfSomites > 0 ? Model.NumberOfSomites : Model.CellPools.Max(p => p.Cells.Max(c => c.Sequence));

            btnPlotWindows.Enabled = true;
            btnPlotHTML.Enabled = true;
            btnAnimate.Enabled = true;
            linkSaveRun.Visible = true;
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
            if (Model is PredefinedModel pdModel)
            {
                StimulusMode stimMode = ddStimulusMode.Text == "Gaussian" ? StimulusMode.Gaussian :
                  ddStimulusMode.Text == "Ramp" ? StimulusMode.Ramp :
                  StimulusMode.Step;
                pdModel.SetStimulusMode(stimMode);
            }

            Model.runParam.tSkip_ms = tRunSkip;
            Model.runParam.tMax = tRunEnd;
            Model.runParam.dt = (double)edt.Value;
            if (Model == null) return;
            btnRun.Text = "Stop Run";
            Task.Run(RunModel);
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            progressBarRun.Value = (int)((Model?.GetProgress() ?? 0) * progressBarRun.Maximum);
            if (cbMultiple.Checked)
            {
                int? i = Model?.GetRunCounter();
                if (i != null)
                    lRunTime.Text = $"Run number: {i}";
            }
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

        #region Outputs

        private void DisplayNumberOfPlots()
        {
            GetPlotSubset();
            (List<Cell> Cells, List<CellPool> Pools) = Model.GetSubsetCellsAndPools(PlotSubset, plotCellSelection);
            int count = Cells?.Count ?? 0 + Pools?.Count ?? 0;
            if (count > 0)
            {
                if (PlotType == PlotType.FullDyn)
                    count *= 3;
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
                    ePlotSomiteSelection.Enabled = Model.NumberOfSomites > 0;
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

        #region HTML Plots
        private void PlotHTML()
        {
            if (Model == null) return;
            htmlPlot = "";

            (List<Cell> Cells, List<CellPool> Pools) = Model.GetSubsetCellsAndPools(PlotSubset, plotCellSelection);
            htmlPlot = DyChartGenerator.Plot(PlotType,
                Model, Cells, Pools, plotCellSelection,
                tPlotStart, tPlotEnd, tRunSkip,
                (int)ePlotWidth.Value, (int)ePlotHeight.Value);
            Invoke(CompletePlotHTML);
        }
        private void btnPlotHTML_Click(object sender, EventArgs e)
        {
            if (Model == null || !Model.ModelRun) return;

            GetPlotSubset();
            if (PlotType == PlotType.Episodes)
                Model.SetAnimationParameters(ReadParams("Kinematics"));
            tabOutputs.SelectedTab = tabPlot;
            tabPlotSub.SelectedTab = tPlotHTML;
            UseWaitCursor = true;
            btnPlotWindows.Enabled = false;
            btnPlotHTML.Enabled = false;

            Task.Run(PlotHTML);
        }
        private void CompletePlotHTML()
        {
            webViewPlot.NavigateTo(htmlPlot, tempFolder, ref tempFile);
            UseWaitCursor = false;
            btnPlotWindows.Enabled = true;
            btnPlotHTML.Enabled = true;
            linkSaveHTMLPlots.Enabled = true;
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
                    Model.SetAnimationParameters(ReadParams("Kinematics"));
                tabOutputs.SelectedTab = tabPlot;
                tabPlotSub.SelectedTab = tPlotWindows;
                UseWaitCursor = true;
                btnPlotWindows.Enabled = false;
                btnPlotHTML.Enabled = false;

                (List<Cell> Cells, List<CellPool> Pools) = Model.GetSubsetCellsAndPools(PlotSubset, plotCellSelection);

                (List<Image> leftImages, List<Image> rightImages) = WindowsPlotGenerator.Plot(PlotType, Model, Cells, Pools, plotCellSelection,
                    Model.runParam.dt, tPlotStart, tPlotEnd, tRunSkip);

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
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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

            if (!Model.ModelRun)
            {
                RefreshModel();
            }
            else if (!modelRefreshMsgShown && (modifiedPools || modifiedJncs))
            {
                MessageBox.Show("The changes you made will not be visible until you rerun the model.");
                modelRefreshMsgShown = true;
            }
            TwoDModelGenerator modelGenerator = new();
            string html = modelGenerator.Create2DModel(false, Model, Model.CellPools, (int)webView2DModel.Width / 2, webView2DModel.Height);
            webView2DModel.NavigateTo(html, tempFolder, ref tempFile);

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
            if (Model == null) return;

            if (!Model.ModelRun)
            {
                RefreshModel();
            }
            else if (!modelRefreshMsgShown && (modifiedPools || modifiedJncs))
            {
                MessageBox.Show("The changes you made will not be visible until you rerun the model.");
                modelRefreshMsgShown = true;
            }
            string mode = dd3DModelType.Text;
            bool singlePanel = mode != "Gap/Chem";
            bool gap = mode.Contains("Gap");
            bool chem = mode.Contains("Chem");
            ThreeDModelGenerator threeDModelGenerator = new();
            string html = threeDModelGenerator.Create3DModel(false, Model, Model.CellPools, singlePanel, gap, chem, ddSomites.Text);

            webView3DModel.NavigateTo(html, tempFolder, ref tempFile);
        }

        private void btnGenerate3DModel_Click(object sender, EventArgs e)
        {
            Generate3DModel();
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
            webViewAnimation.NavigateTo(htmlAnimation, tempFolder, ref tempFile);
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

            lastAnimationStartIndex = (int)(tAnimStart / Model.runParam.dt);
            int lastAnimationEndIndex = (int)(tAnimEnd / Model.runParam.dt);
            lastAnimationTimeArray = Model.TimeArray;
            //TODO generatespinecoordinates is called twice (once in generateanimation) - fix it
            Model.SetAnimationParameters(ReadParams("Kinematics"));
            lastAnimationSpineCoordinates = SwimmingModelKinematics.GenerateSpineCoordinates(Model, lastAnimationStartIndex, lastAnimationEndIndex);

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

            Util.SaveAnimation(saveFileCSV.FileName, lastAnimationSpineCoordinates, lastAnimationTimeArray, lastAnimationStartIndex);
        }

        #endregion

        #region JSON
        private void eTemplateJSON_TextChanged(object sender, EventArgs e)
        {
            if (eTemplateJSON.Focused)
                btnLoadTemplateJSON.Enabled = rbCustom.Checked;
        }

        private void btnDisplayTemplateJSON_Click(object sender, EventArgs e)
        {
            if (Model == null) return;

            if (!Model.ModelRun)
            {
                RefreshModel();
            }
            else if (!jsonRefreshMsgShown && (modifiedPools || modifiedJncs))
            {
                MessageBox.Show("The changes you made will not be visible until you rerun the model.");
                jsonRefreshMsgShown = true;
            }
            eTemplateJSON.Text = Util.CreateJSONFromObject(ModelTemplate);
            btnLoadTemplateJSON.Enabled = false;
        }

        private void btnLoadTemplateJSON_Click(object sender, EventArgs e)
        {
            try
            {
                if (Util.CreateObjectFromJSON(typeof(SwimmingModelTemplate), eTemplateJSON.Text) is SwimmingModelTemplate temp)
                {

                    ModelTemplate = temp;
                    ModelTemplate.LinkObjects();
                    LoadModelTemplate();
                    RefreshModelFromTemplate();
                    MessageBox.Show("Updated template is loaded.");
                }
            }
            catch (JsonException exc)
            {
                WarningMessage($"There is a problem with the JSON file. Please check the format of the text in a JSON editor. {exc.Message}");
            }
            catch (Exception exc)
            {
                ExceptionHandling("Read template from JSON", exc);
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
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void eModelJSON_TextChanged(object sender, EventArgs e)
        {
            if (eModelJSON.Focused)
                btnLoadModelJSON.Enabled = true;
        }

        private void btnDisplayModelJSON_Click(object sender, EventArgs e)
        {
            if (Model == null) return;

            if (!Model.ModelRun)
            {
                RefreshModel();
            }
            else if (!jsonRefreshMsgShown && (modifiedPools || modifiedJncs))
            {
                MessageBox.Show("The changes you made will not be visible until you rerun the model.");
                jsonRefreshMsgShown = true;
            }
            eModelJSON.Text = Util.CreateJSONFromObject(Model);
            btnLoadModelJSON.Enabled = false;
        }

        private void btnLoadModelJSON_Click(object sender, EventArgs e)
        {
            try
            {
                if (Util.CreateObjectFromJSON(typeof(CustomSwimmingModel), eModelJSON.Text) is CustomSwimmingModel model)
                {
                    Model = customModel = model;
                    modelUpdated = true;
                }
            }
            catch (JsonException exc)
            {
                WarningMessage($"There is a problem with the JSON file. Please check the format of the text in a JSON editor. {exc.Message}");
            }
            catch (Exception exc)
            {
                ExceptionHandling("Read template from JSON", exc);
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
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
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
                cpl.JSONString = Util.ReadFromFile(openFileJson.FileName);
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
            if (listCellPool.SelectedItem is not CellPoolTemplate pool) return;
            CellPoolTemplate poolDuplicate = new(pool);
            poolDuplicate = OpenCellPoolDialog(poolDuplicate);
            while (ModelTemplate.CellPoolTemplates.Any(p => p.CellGroup == poolDuplicate?.CellGroup))
            {
                WarningMessage("Cell pool group names have to be unique. Please enter a different name.");
                poolDuplicate = OpenCellPoolDialog(poolDuplicate);
            }
            if (poolDuplicate != null)
            {
                ModelTemplate.CellPoolTemplates.Add(poolDuplicate);
                ModelTemplate.CopyConnectionsOfCellPool(pool, poolDuplicate);
                listCellPool.AppendItem(poolDuplicate);
                LoadModelTemplateInterPools();
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
            if (listCellPool.SelectedItem is not CellPoolTemplate pool) return;
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
            if (listCellPool.SelectedItem is not CellPoolTemplate pool) return;
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
            AppliedStimulusControl sc = new();

            sc.SetStimulus(ModelTemplate.CellPoolTemplates, stim);
            frmControl.AddControl(sc);
            frmControl.Text = stim?.ToString() ?? "New Stimulus";

            if (frmControl.ShowDialog() == DialogResult.OK)
                return sc.GetStimulusTemplate();
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
                            if (!SaveParamsOrModelTemplate())
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
                            if (!SaveParamsOrModelTemplate())
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
                            if (!SaveParamsOrModelTemplate())
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
                            if (!SaveParamsOrModelTemplate())
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

        private void linkBrowseToTempFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", tempFolder);
            }
            catch { }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            About about = new();
            about.SetTimer(2000);
            about.ShowDialog();
        }

        private void cbMultiple_CheckedChanged(object sender, EventArgs e)
        {
            eRunNumber.Visible = cbMultiple.Checked;
        }

        #region Body Size

        private decimal? prevBodyDorsalVentral = null;
        private void eBodyDorsalVentral_Enter(object sender, EventArgs e)
        {
            prevBodyDorsalVentral = eBodyDorsalVentral.Value;
        }

        private void eBodyDorsalVentral_ValueChanged(object sender, EventArgs e)
        {
            if (eBodyDorsalVentral.Focused)
            {
                decimal bodyPos = eSpinalBodyPosition.Value;
                decimal bodyDV = eBodyDorsalVentral.Value;
                decimal spinalDV = eSpinalDorsalVentral.Value;
                if (bodyDV <= bodyPos + spinalDV)
                {
                    WarningMessage("Body's dorsal-ventral height has to be greater than spinal dorsal-ventral height + spinal body position");
                    eBodyDorsalVentral.Value = prevBodyDorsalVentral ?? bodyPos + spinalDV + eBodyDorsalVentral.Increment;
                }
            }
        }

        private decimal? prevBodyMedialLateral = null;
        private void eBodyMedialLateral_Enter(object sender, EventArgs e)
        {
            prevBodyMedialLateral = eBodyMedialLateral.Value;
        }
        private void eBodyMedialLateral_ValueChanged(object sender, EventArgs e)
        {
            if (eBodyMedialLateral.Focused)
            {
                decimal bodyML = eBodyMedialLateral.Value;
                decimal spinalML = eSpinalMedialLateral.Value;
                if (bodyML <= spinalML)
                {
                    WarningMessage("Body's medial-lateral width has to be greater than spinal medial-lateral width");
                    eBodyMedialLateral.Value = prevBodyMedialLateral ?? spinalML + eBodyMedialLateral.Increment;
                }
            }
        }

        private decimal? prevSpinalDorsalVentral = null;
        private void eSpinalDorsalVentral_Enter(object sender, EventArgs e)
        {
            prevSpinalDorsalVentral = eSpinalDorsalVentral.Value;
        }

        private void eSpinalDorsalVentral_ValueChanged(object sender, EventArgs e)
        {
            if (eSpinalDorsalVentral.Focused)
            {
                decimal bodyPos = eSpinalBodyPosition.Value;
                decimal bodyDV = eBodyDorsalVentral.Value;
                decimal spinalDV = eSpinalDorsalVentral.Value;
                if (bodyDV <= bodyPos + spinalDV)
                {
                    WarningMessage("Body's dorsal-ventral height has to be greater than spinal dorsal-ventral height + spinal body position");
                    eSpinalDorsalVentral.Value = prevSpinalDorsalVentral ?? bodyDV - bodyPos - eSpinalDorsalVentral.Increment;
                }
            }
        }
        private decimal? prevSpinalMedialLateral = null;
        private void eSpinalMedialLateral_Enter(object sender, EventArgs e)
        {
            prevSpinalMedialLateral = eSpinalMedialLateral.Value;
        }

        private void edt_ValueChanged(object sender, EventArgs e)
        {
            eAnimationdt.Minimum = edt.Value;
            eAnimationdt.Increment = edt.Value;
            eAnimationdt.Value = 10 * edt.Value;
        }

        private void btnGenerateEpisodes_Click(object sender, EventArgs e)
        {
            (List<Cell> LeftMNs, List<Cell> RightMNs) = Model.GetMotoNeurons((int)eKinematicsSomite.Value);
            (List<SwimmingEpisode> episodesLeft, List<SwimmingEpisode> episodesRight) = SwimmingModelKinematics.GetSwimmingEpisodesUsingMotoNeurons(Model, LeftMNs, RightMNs,
                (int)eKinematicsBurstBreak.Value, (int)eKinematicsEpisodeBreak.Value);
            string html = DyChartGenerator.PlotSummaryMembranePotentials(Model, LeftMNs.Union(RightMNs).ToList(),
                width: (int)ePlotKinematicsWidth.Value, height: (int)ePlotKinematicsHeight.Value);
            webViewSummaryV.NavigateTo(html, tempFolder, ref tempFile);
            eEpisodesLeft.Text = "";
            eEpisodesRight.Text = "";
            foreach (SwimmingEpisode episode in episodesLeft)
                eEpisodesLeft.Text += episode.ToString() + "\r\n\r\n";
            foreach (SwimmingEpisode episode in episodesRight)
                eEpisodesRight.Text += episode.ToString() + "\r\n\r\n";
            lKinematicsTimes.Text = $"Last kinematics:{DateTime.Now:t}";
        }

        private void splitContainer1_Panel2_SizeChanged(object sender, EventArgs e)
        {
            eEpisodesLeft.Width = splitKinematics.Panel2.Width / 2;
        }

        private void ddSomites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSomites.Text == "All Somites")
                ddSomites.DropDownStyle = ComboBoxStyle.DropDownList;
            else
                ddSomites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
        }

        private void linkNeuronalCellDynamics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmNeuronalDynamics.Show(linkNeuronalCellDynamics, new Point(0, 0));
        }

        private void cmiIzhikevich_9P_Click(object sender, EventArgs e)
        {
            Izhikevich_9P izhi = new(paramExternal: null);
            DynamicsTestControl dynControl = new(CoreType.Izhikevich_9P, izhi.GetParameters(), testMode: true);
            ControlContainer frmControl = new();
            frmControl.AddControl(dynControl);
            frmControl.Text = "Neuronal Dynamics Test";
            frmControl.SaveVisible = false;
            frmControl.ShowDialog();
        }

        private void cmiIzhikevich5P_Click(object sender, EventArgs e)
        {
            Izhikevich_5P izhi = new(paramExternal: null);
            DynamicsTestControl dynControl = new(CoreType.Izhikevich_5P, izhi.GetParameters(), testMode: true);
            ControlContainer frmControl = new();
            frmControl.AddControl(dynControl);
            frmControl.Text = "Neuronal Dynamics Test";
            frmControl.SaveVisible = false;
            frmControl.ShowDialog();
        }

        private void eSpinalMedialLateral_ValueChanged(object sender, EventArgs e)
        {
            if (eSpinalMedialLateral.Focused)
            {
                decimal bodyML = eBodyMedialLateral.Value;
                decimal spinalML = eSpinalMedialLateral.Value;
                if (bodyML <= spinalML)
                {
                    WarningMessage("Body's medial-lateral width has to be greater than spinal medial-lateral width");
                    eSpinalMedialLateral.Value = prevSpinalMedialLateral ?? bodyML - eSpinalMedialLateral.Increment;
                }
            }
        }
        private decimal? prevSpinalBodyPos = null;
        private void eSpinalBodyPosition_Enter(object sender, EventArgs e)
        {
            prevSpinalBodyPos = eSpinalBodyPosition.Value;
        }

        private void eSpinalBodyPosition_ValueChanged(object sender, EventArgs e)
        {
            if (eSpinalBodyPosition.Focused)
            {
                decimal bodyPos = eSpinalBodyPosition.Value;
                decimal bodyDV = eBodyDorsalVentral.Value;
                decimal spinalDV = eSpinalDorsalVentral.Value;
                if (bodyDV <= bodyPos + spinalDV)
                {
                    WarningMessage("Body's dorsal-ventral height has to be greater than spinal dorsal-ventral height + spinal body position");
                    eSpinalBodyPosition.Value = prevSpinalBodyPos ?? bodyDV - spinalDV - eSpinalBodyPosition.Increment;
                }
            }
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
        #endregion

    }
}