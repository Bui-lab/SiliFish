using Microsoft.Web.WebView2.Core;
using System.Text.Json;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.PredefinedModels;
using SiliFish.UI.Controls;
using SiliFish.DataTypes;

namespace SiliFish.UI
{
    public partial class MainForm : Form
    {
        SwimmingModel Model;

        SingleCoilModel scModel;
        DoubleCoilModel dcModel;
        BeatAndGlideModel bgModel;
        CustomSwimmingModel customModel;

        SwimmingModelTemplate ModelTemplate = new();
        string AnimationHTML = "";
        string PlotHTML = "";
        string PlotType = "";
        string PlotSubset = "";
        int tPlotStart = 0;
        int tPlotEnd = 0;
        int tRunEnd = 0;
        int tRunSkip = 0;
        PlotExtend plotExtend = PlotExtend.FullModel;
        string tempFolder;
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
                splitWindows.SplitterDistance = splitWindows.Width / 2;
                tempFolder = Path.GetTempPath() + "\\SiliFish";
                if (!Directory.Exists(tempFolder))
                    Directory.CreateDirectory(tempFolder);
                else
                {
                    foreach (string f in Directory.GetFiles(tempFolder))
                        File.Delete(f);
                }
                webViewAnimation.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;//TODO this doesn't work

                listCellPool.AddContextMenu("Sort by Type", listCellPool_SortByNTType);
                listJunctions.AddContextMenu("Sort by Type", listJunctions_SortByType);
                listJunctions.AddContextMenu("Sort by Source", listJunctions_SortBySource);
                listJunctions.AddContextMenu("Sort by Target", listJunctions_SortByTarget);
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
                return;
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
            listJunctions.ClearItems();
            listStimuli.ClearItems();
        }

        private void WriteGeneralParams(Dictionary<string, object> paramDict)
        {
            eModelName.Text = paramDict.Read("General.Name", "");
            eModelDescription.Text = paramDict.Read("General.Description", "");

            decimal d = (decimal)paramDict.ReadDouble("General.SpinalRostralCaudalDistance");
            eSpinalRostraoCaudal.Value = d;

            d = (decimal)paramDict.ReadDouble("General.SpinalDorsalVentralDistance");
            eSpinalDorsalVentral.Value = d;

            d = (decimal)paramDict.ReadDouble("General.SpinalMedialLateralDistance");
            eSpinalMedialLateral.Value = d;

            eSpinalBodyPosition.Value = (decimal)paramDict.ReadDouble("General.SpinalBodyPosition");

            eBodyDorsalVentral.Value = (decimal)paramDict.ReadDouble("General.BodyDorsalVentralDistance");
            eBodyMedialLateral.Value = (decimal)paramDict.ReadDouble("General.BodyMedialLateralDistance");
        }
        private void WriteParams(Dictionary<string, object> ParamDict)
        {
            if (ParamDict == null) return;
            this.Text = $"Silifish {Model.ModelName}";
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
            SwimmingModel swimmingModel = new SwimmingModel();
            Dictionary<string, object> currentParams = swimmingModel.GetParameters();
            List<string> currentParamGroups = currentParams.Keys.Where(k => k.IndexOf('.') > 0).Select(k => k.Substring(0, k.IndexOf('.'))).Distinct().ToList();

            List<string> paramGroups = ParamDict?.Keys.Where(k => k.IndexOf('.') > 0).Select(k => k.Substring(0, k.IndexOf('.'))).Distinct().ToList() ?? new List<string>();
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
                TabPage tabPage = new();
                tabPage.Text = group;
                tabPage.Tag = "Param";
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
                    lbl.Text = kvp.Key.Substring(group.Length + 1);
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
            paramDict.Add("General.SpinalRostralCaudalDistance", eSpinalRostraoCaudal.Value);
            paramDict.Add("General.SpinalDorsalVentralDistance", eSpinalDorsalVentral.Value);
            paramDict.Add("General.SpinalMedialLateralDistance", eSpinalMedialLateral.Value);
            paramDict.Add("General.SpinalBodyPosition", eSpinalBodyPosition.Value);
            paramDict.Add("General.BodyDorsalVentralDistance", eBodyDorsalVentral.Value);
            paramDict.Add("General.BodyMedialLateralDistance", eBodyMedialLateral.Value);
        }

        private Dictionary<string, object> ReadParams()
        {
            
            string key = "";
            Dictionary<string, object> ParamDict = new();
            ReadGeneralParams(ParamDict);
            foreach (TabPage page in tabParams.TabPages)
            {
                if (page.Tag?.ToString() != "Param")
                    continue;
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
            listJunctions.ClearItems();
            if (ModelTemplate?.InterPoolTemplates == null) return;
            foreach (InterPoolTemplate interPoolTemplate in ModelTemplate.InterPoolTemplates)
            {
                listJunctions.AppendItem(interPoolTemplate);
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
            this.Text = $"Silifish {ModelTemplate.ModelName}";
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
            foreach (var jnc in listJunctions.GetItems(includeHidden))
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

        private void ExceptionHandling(string name, Exception ex)
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
                        this.Text = $"Silifish {ModelTemplate.ModelName}";
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
            catch { }
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
                Model.MainLoop(0, tRunEnd, tRunSkip);
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                Invoke(CompleteRun);
            }
        }

        private void CompleteRun()
        {
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            progressBarRun.Visible = false;
            btnRun.Enabled = true;

            if (ddPlot.SelectedIndex < 0)
                ddPlot.SelectedIndex = 0;
            btnPlot.Enabled = true;
            btnAnimate.Enabled = true;
            linkSaveRun.Visible = true;

            ddCellPoolsWindows.Items.Clear();
            ddCellPoolsWindows.Items.AddRange(Model?.GetCellPools.Select(cp=>cp.CellGroup).Distinct().ToArray());

            if (tabOutputs.SelectedTab == tabTextOutput)
                eModelSummary.Text = Model.SummarizeModel();
            TimeSpan ts = DateTime.Now - runStart;
            
            lRunTime.Text = $"Last run: {Util.TimeSpanToString(ts)}";

        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            runStart = DateTime.Now;
            lRunTime.Text = "";
            UseWaitCursor = true;
            progressBarRun.Value = 0;
            progressBarRun.Visible = true;
            btnRun.Enabled = false;
            timerRun.Enabled = true;

            if (!int.TryParse(eTimeEnd.Text, out tRunEnd))
                tRunEnd = 10000;
            if (!int.TryParse(eSkip.Text, out tRunSkip))
                tRunSkip = 0;
            

            if (rbCustom.Checked)
                Model = customModel = new CustomSwimmingModel(ReadModelTemplate(includeHidden: false));
            else if (Model != null)
            {
                Dictionary<string, object> ParamDict = ReadParams();
                Model.SetParameters(ParamDict);
                StimulusMode stimMode = ddStimulusMode.Text == "Gaussian" ? StimulusMode.Gaussian :
                    ddStimulusMode.Text == "Ramp" ? StimulusMode.Ramp :
                    StimulusMode.Step; 
                (Model as PredefinedModel).SetStimulusMode(stimMode);
            }
            if (Model == null) return;
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
                System.Windows.Forms.Application.DoEvents();
        }
        private void webViewPlot_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webViewPlot.Tag= true;
        }
        private void webView2DModel_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webView2DModel.Tag = true;
        }
        private void webView3DModel_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webView3DModel.Tag = true;
        }
        private void webViewAnimation_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webViewAnimation.Tag = true;
        }
        #endregion

        private async void linkSaveHTML_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                else if (tabOutputs.SelectedTab == tabHTMLPlot)
                    File.WriteAllText(saveFileHTML.FileName, PlotHTML.ToString());
                else if (tabOutputs.SelectedTab == tabAnimation)
                {
                    File.WriteAllText(saveFileHTML.FileName, AnimationHTML);
                }
            }
        }

        private void btnAnimate_Click(object sender, EventArgs e)
        {
            if (Model == null || !Model.ModelRun) return;

            if (!int.TryParse(eAnimationStart.Text, out int tAnimStart))
                tAnimStart = 0;
            if (!int.TryParse(eAnimationEnd.Text, out int tAnimEnd))
                tAnimEnd = 1000;
            if (tAnimEnd > tRunEnd)
                tAnimEnd = tRunEnd;

            AnimationHTML = Model.GenerateAnimation(tAnimStart, tAnimEnd);

            if (AnimationHTML.Length > 1000000)
            {
                string filename = Path.GetFullPath(System.Guid.NewGuid().ToString() + ".html", tempFolder);
                File.WriteAllText(filename, AnimationHTML.ToString());
                webViewAnimation.CoreWebView2.Navigate(filename);
            }
            else
                webViewAnimation.CoreWebView2.NavigateToString(AnimationHTML);//TODO override by an extension - save if error
            linkSaveAnimation.Enabled = true;
        }
        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void linkSaveAnimation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(AnimationHTML))
                return;
            if (saveFileHTML.ShowDialog() != DialogResult.OK)
                return;
            File.WriteAllText(saveFileHTML.Filter, AnimationHTML.ToString());
        }

        private void btnGenerate2DModel_Click(object sender, EventArgs e)
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
            string html = Model.Plot2DModel(false);
            webView2DModel.CoreWebView2.NavigateToString(html);//TODO override by an extension - save if error

        }

        private void Generate3DModel()
        {
            if (Model == null) return;


            if (rbCustom.Checked)
            {
                Model = new CustomSwimmingModel(ReadModelTemplate(includeHidden: false));
            }
            else
            {
                Dictionary<string, object> ParamDict = ReadParams();
                Model.SetParameters(ParamDict);
            }

            string mode = dd3DModelType.Text;
            bool singlePanel = mode != "Gap/Chem";
            bool gap = mode.Contains("Gap");
            bool chem = mode.Contains("Chem");
            string html = Model.Plot3DModel(false, singlePanel, gap, chem);
            webView3DModel.CoreWebView2.NavigateToString(html);//TODO override by an extension - save if error
        }

        private void btnGenerate3DModel_Click(object sender, EventArgs e)
        {
            Generate3DModel();
        }

        private void dd3DModelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Generate3DModel();
        }

        #region HTML Plots

        string prevGroup = "";
        private void ddGrouping_Enter(object sender, EventArgs e)
        {
            prevGroup = ddGrouping.Text;
        }

        private void ddGrouping_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddGrouping.Text)
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
            if (plotExtend== PlotExtend.SingleCell && !ddPlot.Items.Contains("Full Dynamics"))
                ddPlot.Items.Add("Full Dynamics");
            else if (plotExtend != PlotExtend.SingleCell && ddPlot.Items.Contains("Full Dynamics"))
                ddPlot.Items.Remove("Full Dynamics");
            if (ddGrouping.Text != prevGroup)
                ClearPlot();
            PopulateCellTypes();
        }

        string prevPool = "";
        private void ddCellsPools_Enter(object sender, EventArgs e)
        {
            prevPool = ddCellsPools.Text;
        }

        private void ddCellsPools_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddCellsPools.Text != prevPool)
                ClearPlot();
        }

        string prevPlot = "";
        private void ddPlot_Enter(object sender, EventArgs e)
        {
            prevPlot = ddPlot.Text;
        }

        private void ddPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddPlot.Text != prevPlot)
                ClearPlot();
        }

        private void CreatePlot()
        {
            if (Model == null) return;
            PlotHTML = "";
            int nSample = cbSampleHTML.Checked ? (int)nbSample.Value: 0;
            if (PlotType == "Memb. Potentials")
            {
                PlotHTML = Model.PlotMembranePotentials(plotExtend, PlotSubset, tPlotStart, tPlotEnd, nSample: nSample);
            }
            else if (PlotType == "Gap Currents")
            {
                PlotHTML = Model.PlotCurrents(plotExtend, PlotSubset, tPlotStart, tPlotEnd, includeGap: true, includeChem: false, nSample: nSample);
            }
            else if (PlotType == "Syn Currents")
            {
                PlotHTML = Model.PlotCurrents(plotExtend, PlotSubset, tPlotStart, tPlotEnd, includeGap: false, includeChem: true, nSample: nSample);
            }
            else if (PlotType == "Currents")
            {
                PlotHTML = Model.PlotCurrents(plotExtend, PlotSubset, tPlotStart, tPlotEnd, includeGap: true, includeChem: true, nSample: nSample);
            }
            else if (PlotType == "Stimuli")
            {
                PlotHTML = Model.PlotStimuli(plotExtend, PlotSubset, tPlotStart, tPlotEnd, nSample: nSample);
            }
            else if (PlotType == "Full Dynamics")
            {
                
            }
            Invoke(CompleteCreatePlot);
        }
        private void btnPlot_Click(object sender, EventArgs e)
        {
            if (Model == null || !Model.ModelRun) return;
            if (!int.TryParse(ePlotStart.Text, out tPlotStart))
                tPlotStart = 0;
            if (!int.TryParse(ePlotEnd.Text, out tPlotEnd))
                tPlotEnd = 1000;
            if (tPlotEnd > tRunEnd)
                tPlotEnd = tRunEnd;
            PlotType = ddPlot.Text;
            PlotSubset = ddCellsPools.Text;

            tabOutputs.SelectedTab = tabHTMLPlot;
            UseWaitCursor = true;
            btnPlot.Enabled = false;
            Task.Run(CreatePlot);
        }

        private void ClearPlot()
        {
            webViewPlot.CoreWebView2.Navigate("about:blank");
        }
        private void CompleteCreatePlot()
        {
            if (PlotHTML.Length > 1000000)
            {
                string filename = Path.GetFullPath(System.Guid.NewGuid().ToString() + ".html", tempFolder);
                File.WriteAllText(filename, PlotHTML.ToString());
                webViewPlot.CoreWebView2.Navigate(filename);
            }
            else
                webViewPlot.CoreWebView2.NavigateToString(PlotHTML);//TODO override by an extension - save if error
            UseWaitCursor = false;
            btnPlot.Enabled = true;
        }

        private void PopulateCellTypes()
        {
            ddCellsPools.Items.Clear();
            ddCellsPools.Text = "";
            if (Model == null) return;
            if (plotExtend == PlotExtend.FullModel)
                return;
            List<string> itemList = new();
            foreach (CellPool pool in Model.GetCellPools)
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
            ddCellsPools.SelectedIndex = 0;
        }
        #endregion

        #region Windows Plot
        private void Plot()
        {
            try
            {
                if (Model == null)
                    return;
                int nSample = cbSample.Checked ? (int)enSample.Value : 0;
                if (!int.TryParse(ePlotWindowsStart.Text, out tPlotStart))
                    tPlotStart = 0;
                if (!int.TryParse(ePlotWindowsEnd.Text, out tPlotEnd))
                    tPlotEnd = 1000;
                if (tPlotEnd > tRunEnd)
                    tPlotEnd = tRunEnd;
                tPlotStart *= 10;
                tPlotEnd *= 10;
                string plotType = ddPlotWindows.Text;

                List<Image> leftImages = new List<Image>();
                List<Image> rightImages = new List<Image>();

                List<CellPool> pools = Model.GetCellPools.Where(cp => cp.CellGroup == ddCellPoolsWindows.Text).ToList();
                if (plotType == "Memb. Potentials")
                {
                    foreach (CellPool pool in pools)
                    {
                        List<Cell> cells = pool.GetCells(nSample).ToList();
                        double[,] voltageArray = new double[cells.Count, Model.MaxIndex];
                        int i = 0;
                        foreach (Cell c in cells)
                            voltageArray.UpdateRow(i++, c.V);

                        Color col = pool.Color;
                        if (pool.PositionLeftRight == SagittalPlane.Left)
                            leftImages.Add(UtilWindows.CreatePlotImage("Left" + pool.CellGroup, voltageArray, Model.TimeArray, tPlotStart, tPlotEnd, col));
                        else
                            rightImages.Add(UtilWindows.CreatePlotImage("Right" + pool.CellGroup, voltageArray, Model.TimeArray, tPlotStart, tPlotEnd, col));
                    }
                }
                else if (plotType == "Gap Currents")
                {
                    foreach (CellPool pool in pools)
                    {
                        IEnumerable<Cell> sampleCells = pool.GetCells(nSample);
                        foreach (Cell c in sampleCells)
                        {
                            if (c is not Neuron neuron) continue;
                            Dictionary<string, Color> colors = new();
                            Dictionary<string, List<double>> AffarentCurrents = new();
                            foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell2 == c))
                            {
                                colors.Add(jnc.Cell1.ID, jnc.Cell1.CellPool.Color);
                                AffarentCurrents.AddObject(jnc.Cell1.ID, jnc.InputCurrent.ToList());
                            }
                            foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell1 == c))
                            {
                                colors.Add(jnc.Cell2.ID, jnc.Cell2.CellPool.Color);
                                AffarentCurrents.AddObject(jnc.Cell2.ID, jnc.InputCurrent.Select(d => -d).ToList());
                            }
                            if (pool.PositionLeftRight == SagittalPlane.Left)
                                leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, colors, AffarentCurrents, tPlotStart, tPlotEnd, Model.TimeArray));
                            else
                                rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, colors, AffarentCurrents, tPlotStart, tPlotEnd, Model.TimeArray));
                        }
                    }
                }
                else if (plotType == "Syn Currents")
                {
                    foreach (CellPool pool in pools)
                    {
                        foreach (Cell c in pool.GetCells(nSample))
                        {
                            Dictionary<string, Color> colors = new();
                            Dictionary<string, List<double>> AffarentCurrents = new();
                            if (c is Neuron neuron)
                            {
                                foreach (ChemicalJunction jnc in neuron.Synapses)
                                {
                                    colors.Add(jnc.PreNeuron.ID, jnc.PreNeuron.CellPool.Color);
                                    AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
                                }
                            }
                            else if (c is MuscleCell muscle)
                            {
                                foreach (ChemicalJunction jnc in muscle.EndPlates)
                                {
                                    colors.Add(jnc.PreNeuron.ID, jnc.PostCell.CellPool.Color);
                                    AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
                                }
                            }
                            if (pool.PositionLeftRight == SagittalPlane.Left)
                                leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, colors, AffarentCurrents, tPlotStart, tPlotEnd, Model.TimeArray));
                            else
                                rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, colors, AffarentCurrents, tPlotStart, tPlotEnd, Model.TimeArray));
                        }
                    }
                }
                else if (plotType == "Stimulus")
                {
                    foreach (CellPool pool in pools)
                    {
                        Color col = pool.Color;
                        foreach (Cell c in pool.GetCells(nSample))
                        {
                            if (pool.PositionLeftRight == SagittalPlane.Left)
                                leftImages.Add(UtilWindows.CreatePlotImage(c.ID, c.Stimulus?.getValues(Model.MaxIndex), Model.TimeArray, tPlotStart, tPlotEnd, col));
                            else
                                rightImages.Add(UtilWindows.CreatePlotImage(c.ID, c.Stimulus?.getValues(Model.MaxIndex), Model.TimeArray, tPlotStart, tPlotEnd, col));
                        }
                    }
                }
                else if (plotType == "Full Dynamics")
                {
                    foreach (CellPool pool in pools)
                    {
                        Color col = pool.Color; 
                        nSample = (int)enSample.Value;
                        Cell c = pool.GetNthCell(nSample);
                        if (c != null)
                        {
                            if (pool.PositionLeftRight == SagittalPlane.Left)
                            {
                                leftImages.Add(UtilWindows.CreatePlotImage(c.ID + " Memb. Potential", c.V, Model.TimeArray, tPlotStart, tPlotEnd, col));
                                leftImages.Add(UtilWindows.CreatePlotImage(c.ID + " Stimulus", c.Stimulus?.getValues(Model.MaxIndex), Model.TimeArray, tPlotStart, tPlotEnd, col));
                            }
                            else
                            {
                                rightImages.Add(UtilWindows.CreatePlotImage(c.ID + " Memb. Potential", c.V, Model.TimeArray, tPlotStart, tPlotEnd, col));
                                rightImages.Add(UtilWindows.CreatePlotImage(c.ID + " Stimulus", c.Stimulus?.getValues(Model.MaxIndex), Model.TimeArray, tPlotStart, tPlotEnd, col));
                            }
                        }
                        Dictionary<string, Color> colors = new();
                        Dictionary<string, List<double>> AffarentCurrents = new();
                        if (c is Neuron neuron)
                        {
                            foreach (ChemicalJunction jnc in neuron.Synapses)
                            {
                                colors.AddObject(jnc.PreNeuron.ID, jnc.PreNeuron.CellPool.Color, skipIfExists: true);
                                AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
                            }
                        }
                        else if (c is MuscleCell muscle)
                        {
                            foreach (ChemicalJunction jnc in muscle.EndPlates)
                            {
                                colors.AddObject(jnc.PreNeuron.ID, jnc.PreNeuron.CellPool.Color, skipIfExists: true);
                                AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
                            }
                        }
                        if (pool.PositionLeftRight == SagittalPlane.Left)
                            leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID + " Syn. Currents", colors, AffarentCurrents, tPlotStart, tPlotEnd, Model.TimeArray));
                        else
                            rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID + " Syn. Currents", colors, AffarentCurrents, tPlotStart, tPlotEnd, Model.TimeArray));

                        if (c is not Neuron neuron2) continue;
                        AffarentCurrents.Clear();
                        colors.Clear();
                        foreach (GapJunction jnc in neuron2.GapJunctions.Where(j => j.Cell2 == c))
                        {
                            colors.AddObject(jnc.Cell1.ID, jnc.Cell1.CellPool.Color, skipIfExists: true);
                            AffarentCurrents.AddObject(jnc.Cell1.ID, jnc.InputCurrent.ToList());
                        }
                        foreach (GapJunction jnc in neuron2.GapJunctions.Where(j => j.Cell1 == c))
                        {
                            colors.AddObject(jnc.Cell2.ID, jnc.Cell2.CellPool.Color, skipIfExists: true);
                            AffarentCurrents.AddObject(jnc.Cell2.ID, jnc.InputCurrent.Select(d => -d).ToList());
                        }
                        if (pool.PositionLeftRight == SagittalPlane.Left)
                            leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID + " Gap Currents", colors, AffarentCurrents, tPlotStart, tPlotEnd, Model.TimeArray));
                        else
                            rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID + " Gap Currents", colors, AffarentCurrents, tPlotStart, tPlotEnd, Model.TimeArray));
                    }
                }
                leftImages.RemoveAll(img => img == null);
                rightImages.RemoveAll(img => img == null);

                int nrow = (int)Math.Ceiling((decimal)leftImages.Count / 2);
                int ncol = leftImages.Count > 1 ? 2 : 1;
                pictureBoxLeft.Image = ImageHelperWindows.MergeImages(leftImages, nrow, ncol);
                nrow = (int)Math.Ceiling((decimal)rightImages.Count / 2);
                ncol = rightImages.Count > 1 ? 2 : 1;
                pictureBoxRight.Image = ImageHelperWindows.MergeImages(rightImages, nrow, ncol);
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }
        private void btnPlotWindows_Click(object sender, EventArgs e)
        {
            Plot();
        }

        private void ddCellPoolsWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<CellPool> pools = Model.GetCellPools.Where(cp => cp.CellGroup == ddCellPoolsWindows.Text).ToList();
            enSample.Maximum = pools.Max(p => p.GetCells().Count());
            Plot();
        }

        private void ddPlotWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            string plotType = ddPlotWindows.Text;
            if (plotType == "Full Dynamics")
            {
                lCellNumber.Visible = true;
                cbSample.Visible = false;
                enSample.Visible = true;
            }
            else
            {
                lCellNumber.Visible = false;
                cbSample.Visible = true;
                enSample.Visible = cbSample.Checked;
            }
            Plot();
        }
        private void cbSample_CheckedChanged(object sender, EventArgs e)
        {
            enSample.Visible = cbSample.Checked;
        }
        #endregion

        #endregion

        #region Custom Model

        #region Cell Pool
        private CellPoolTemplate OpenCellPoolDialog(CellPoolTemplate pool)
        {
            ControlContainer frmControl = new();
            CellPoolControl cpl = new CellPoolControl();
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
            }
        }
        private void listCellPool_DeleteItem(object sender, EventArgs e)
        {
            if (listCellPool.SelectedIndex >= 0)
            {
                string msg = "Deleting a cell pool will remove all of its junctions and applied stimuli as well. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    CellPoolTemplate cpl = (CellPoolTemplate)listCellPool.SelectedItem;
                    ReadModelTemplateInterPools(includeHidden: true);
                    ReadModelTemplateStimuli(includeHidden: true);
                    ModelTemplate.RemoveCellPool(cpl);
                    listCellPool.RemoveItemAt(listCellPool.SelectedIndex);
                    LoadModelTemplateInterPools();
                    LoadModelTemplateStimuli();
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
            }
        }

        private void listCellPool_ActivateItem(object sender, EventArgs e)
        {
            CellPoolTemplate pool = listCellPool.SelectedItem as CellPoolTemplate;
            ModelTemplate.CellPoolTemplates.FirstOrDefault(p => p.Distinguisher == pool.Distinguisher).Active = pool.Active;
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

        #region Junction
        private InterPoolTemplate OpenJunctionDialog(InterPoolTemplate interpool)
        {
            ControlContainer frmControl = new();
            InterPoolControl ipl = new InterPoolControl();

            ipl.SetInterPoolTemplate(ModelTemplate.CellPoolTemplates, interpool, ModelTemplate);
            frmControl.AddControl(ipl);
            frmControl.Text = interpool?.ToString() ?? "New Junction";

            if (frmControl.ShowDialog() == DialogResult.OK)
            {
                InterPoolTemplate interPoolTemplate = ipl.GetInterPoolTemplate();
                ModelTemplate.LinkObjects(interPoolTemplate);
                return interPoolTemplate;
            }
            return null;
        }

        private void listJunctions_AddItem(object sender, EventArgs e)
        {
            InterPoolTemplate newJnc = OpenJunctionDialog(null);
            if (newJnc != null)
            {
                ModelTemplate.InterPoolTemplates.Add(newJnc);
                listJunctions.AppendItem(newJnc);
            }
        }
        private void listJunctions_CopyItem(object sender, EventArgs e)
        {
            if (listJunctions.SelectedItem == null)
                return;
            InterPoolTemplate jnc = new(listJunctions.SelectedItem as InterPoolTemplate);
            jnc = OpenJunctionDialog(jnc); 
            if (jnc != null)
            {
                ModelTemplate.InterPoolTemplates.Add(jnc);
                listJunctions.AppendItem(jnc);
            }
        }
        private void listJunctions_DeleteItem(object sender, EventArgs e)
        {
            if (listJunctions.SelectedIndex >= 0)
            {
                InterPoolTemplate ipl = (InterPoolTemplate)listJunctions.SelectedItem;
                ModelTemplate.InterPoolTemplates.Remove(ipl);
                listJunctions.RemoveItemAt(listJunctions.SelectedIndex);
            }
        }
        private void listJunctions_ViewItem(object sender, EventArgs e)
        {
            if (listJunctions.SelectedItem == null)
                return;
            InterPoolTemplate interpool = listJunctions.SelectedItem as InterPoolTemplate;
            interpool = OpenJunctionDialog(interpool); //check modeltemplate's list
            if (interpool != null)
            {
                int ind = listJunctions.SelectedIndex;
                listJunctions.RefreshItem(ind, interpool);
            }
        }

        private void listJunctions_ActivateItem(object sender, EventArgs e)
        {
            InterPoolTemplate jnc = sender as InterPoolTemplate;
            ModelTemplate.InterPoolTemplates.FirstOrDefault(p => p.Distinguisher == jnc.Distinguisher).Active = jnc.Active;
        }

        private void listJunctions_SortItems(object sender, EventArgs e)
        {
            ModelTemplate.InterPoolTemplates.Sort();
            LoadModelTemplateInterPools();
        }

        private void listJunctions_SortByType(object sender, EventArgs e)
        {
            ModelTemplate.InterPoolTemplates = ModelTemplate.InterPoolTemplates
                .OrderBy(jnc => jnc.JunctionType.ToString())
                .ToList();
            LoadModelTemplateInterPools();
        }
        private void listJunctions_SortBySource(object sender, EventArgs e)
        {
            ModelTemplate.InterPoolTemplates = ModelTemplate.InterPoolTemplates
                .OrderBy(jnc => jnc.PoolSource)
                .ToList();
            LoadModelTemplateInterPools();
        }

        private void listJunctions_SortByTarget(object sender, EventArgs e)
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
            splitWindows.SplitterDistance = splitWindows.Width / 2;
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
            About about = new About();
            about.ShowDialog();
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