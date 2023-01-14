﻿using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using System.Data;
using SiliFish.Services;
using OxyPlot;
using SiliFish.ModelUnits.Stim;

namespace SiliFish.UI.Controls
{
    public partial class ModelControl : UserControl
    {
        enum Mode { Template, RunningModel };
        Mode CurrentMode = Mode.Template;
        static string modelFileDefaultFolder;
        ModelBase Model;
        bool modelUpdated = false;
        string lastSavedCustomModelJSON;
        
        public ModelControl()
        {
            InitializeComponent();
            listConnections.AddContextMenu("Sort by Type", listConnections_SortByType);
            listConnections.AddContextMenu("Sort by Source", listConnections_SortBySource);
            listConnections.AddContextMenu("Sort by Target", listConnections_SortByTarget);
            tabModel.BackColor = Color.White;
        }
        
        #region Read/Load Model and Parameters

        /// <summary>
        /// LoadParams will be valid only when hardcoded models are implemented. Everything in the Modeltemplate.Parameters are carried over to classes
        /// </summary>
        /// <param name="ParamDict"></param>
        private void LoadParams(Dictionary<string, object> ParamDict)
        {
            List<TabPage> obsoloteTabpages = new();
            foreach (TabPage tp in tabModel.TabPages)
            {
                if (tp.Tag?.ToString() == "Param")
                    obsoloteTabpages.Add(tp);
            }
            foreach (TabPage tp in obsoloteTabpages)
            {
                tabModel.TabPages.Remove(tp);
            }

            if (ParamDict == null) return;

            List<string> paramGroups = ParamDict?.Keys.Where(k => k.IndexOf('.') > 0).Select(k => k[..k.IndexOf('.')]).Distinct().ToList() ?? new List<string>();
            int tabIndex = 1;
            var _ = tabModel.Handle;//requires for the insert command to work
            foreach (string group in paramGroups)
            {
                Dictionary<string, object> SubDict = ParamDict.Where(x => x.Key.StartsWith(group)).ToDictionary(x => x.Key, x => x.Value);
                TabPage tabPage = new()
                {
                    Text = group,
                    Tag = "Param",
                    BackColor = tGeneral.BackColor
                };
                //insert after the general tab, to keep Animation at the end
                tabModel.TabPages.Insert(tabIndex++, tabPage);
                int maxLen = SubDict.Keys.Select(k => k.Length).Max();
                FlowLayoutPanel flowPanel = new();
                tabPage.Controls.Add(flowPanel);
                flowPanel.FlowDirection = FlowDirection.LeftToRight;
                flowPanel.Dock = DockStyle.Fill;
                flowPanel.AutoScroll = true;
                foreach (KeyValuePair<string, object> kvp in SubDict)
                {
                    Label lbl = new()
                    {
                        Text = kvp.Key[(group.Length + 1)..],
                        Height = 23,
                        Width = 5 * maxLen,
                        TextAlign = ContentAlignment.BottomRight
                    };
                    flowPanel.Controls.Add(lbl);
                    TextBox textBox = new();
                    textBox.Tag = kvp.Value;
                    textBox.Text = kvp.Value?.ToString();
                    flowPanel.Controls.Add(textBox);
                    flowPanel.SetFlowBreak(textBox, true);
                }
            }
        }

        private void LoadParamsAndSettings()
        {
            if (CurrentMode != Mode.Template) return;
            //General Params
            eModelName.Text = Model.ModelName;
            eModelDescription.Text = Model.ModelDescription;
            propModelDimensions.SelectedObject = Model.ModelDimensions;

            //Settings
            if (ddDefaultNeuronCore.Items.Count == 0)
                ddDefaultNeuronCore.Items.AddRange(CellCoreUnit.GetCoreTypes().ToArray());
            ddDefaultNeuronCore.Text = Model.Settings.DefaultNeuronCore;
            if (ddDefaultMuscleCellCore.Items.Count == 0)
                ddDefaultMuscleCellCore.Items.AddRange(CellCoreUnit.GetCoreTypes().ToArray());
            ddDefaultMuscleCellCore.Text = Model.Settings.DefaultMuscleCellCore;
            eTemporaryFolder.Text = Model.Settings.TempFolder;
            eOutputFolder.Text = Model.Settings.OutputFolder;
            propSettings.SelectedObject = Model.Settings;
            propKinematics.SelectedObject = Model.KinemParam;
            LoadParams(Model.Parameters);
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
            foreach (TabPage page in tabModel.TabPages)
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
            foreach (TabPage page in tabModel.TabPages)
            {
                if (page.Tag?.ToString() != "Param")
                    continue;
                ReadParamsFromTabPage(ParamDict, page);
            }
            return ParamDict;
        }

        private void LoadPools()
        {
            listCellPools.ClearItems();
            foreach (CellPoolBase cp in Model.GetCellPools())
            {
                listCellPools.AppendItem(cp);
            }
        }

        private void LoadProjections()
        {
            if (CurrentMode != Mode.Template) return;
            listConnections.ClearItems();
            foreach (object jnc in Model.GetProjections())
            {
                listConnections.AppendItem(jnc);
            }
        }

        private void LoadStimuli()
        {
            if (CurrentMode != Mode.Template) return;
            listStimuli.ClearItems();
            foreach (object stim in Model.GetStimuli())
            {
                listStimuli.AppendItem(stim);
            }
        }

        private void LoadModel()
        {
            //TODO ddPlotSomiteSelection.Enabled = ePlotSomiteSelection.Enabled = Model.ModelDimensions.NumberOfSomites > 0;
            LoadParamsAndSettings();
            LoadPools();
            LoadProjections();
            LoadStimuli();
            this.Text = $"SiliFish {Model.ModelName}";
        }
        private void ReadModel()
        {
            try
            {
                Model.ModelName = eModelName.Text;
                Model.ModelDescription = eModelDescription.Text;
                Model.Parameters = ReadParams();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        private static string CheckJSONVersion(string json)
        {
            List<string> issues = JsonUtil.CheckJsonVersion(ref json);
            if (issues?.Count > 0)
            //Compare to Version 0.1
            {
                //created by old version 
                MessageBox.Show("This file was generated by an old version of SiliFish. " +
                    "Please double check the following items for accuracy before running the model:\r\n" +
                    string.Join("; ", issues));
            }
            return json;
        }
        private bool SaveModel()
        {
            try
            {
                ReadModel();
                
                if (!Model.CheckValues(out List<string> errors))
                {
                    //TODO WarningMessage(error);
                    return false;
                }
                if (string.IsNullOrEmpty(saveFileJson.FileName))
                    saveFileJson.FileName = Model.ModelName;
                else
                    saveFileJson.InitialDirectory = modelFileDefaultFolder;
                if (saveFileJson.ShowDialog() == DialogResult.OK)
                {
                    modelFileDefaultFolder = Path.GetDirectoryName(saveFileJson.FileName);
                    //To make sure deactivated items are saved, even though not displayed
                    //Show all
                    JsonUtil.SaveToJsonFile(saveFileJson.FileName, Model);
                    this.Text = $"SiliFish {Model.ModelName}";
                    lastSavedCustomModelJSON = JsonUtil.ToJson(Model);
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
        
        private void linkSaveTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveModel();
        }
        private void linkLoadModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                openFileJson.InitialDirectory = modelFileDefaultFolder;
                if (openFileJson.ShowDialog() == DialogResult.OK)
                {
                    tabModel.Visible = false;
                    Application.DoEvents();
                    string json = FileUtil.ReadFromFile(openFileJson.FileName);
                    json = CheckJSONVersion(json);
                    try
                    {
                        try
                        {
                            Model = (ModelTemplate)JsonUtil.ToObject(typeof(ModelTemplate), json);
                        //Model = (RunningModel)JsonUtil.ToObject(typeof(RunningModel), json);
                        }
                        catch 
                        {
                            
                        }
                        if (Model is RunningModel)
                        {
                            trackMode.Value = 1;
                            CurrentMode = Mode.RunningModel;
                        }
                        else if (Model is ModelTemplate)
                        {
                            trackMode.Value = 0;
                            CurrentMode = Mode.Template;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Selected file is not a valid Swimming Model Template file.");
                        return;
                    }
                    Model.BackwardCompatibility();
                    Model.LinkObjects();
                    LoadModel();
                    lastSavedCustomModelJSON = JsonUtil.ToJson(Model);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"There is a problem in generating the model template from the JSON file.\r\n{exc.Message}");
                throw new Exception();
            }
            finally
            {
                tabModel.Visible = true;
            }
        }
        #endregion
        #region Cell Pool
        private CellPoolTemplate OpenCellPoolDialog(CellPoolBase pool)
        {
            if (pool is CellPoolTemplate cpt)
            {
                ControlContainer frmControl = new();
                CellPoolControl cpl = new(Model.ModelDimensions.NumberOfSomites > 0);
                cpl.LoadPool += Cpl_LoadPool;
                cpl.SavePool += Cpl_SavePool;

                cpl.PoolTemplate = cpt;
                frmControl.AddControl(cpl);
                frmControl.Text = pool?.ToString() ?? "New Cell Pool";

                if (frmControl.ShowDialog() == DialogResult.OK)
                    return cpl.PoolTemplate;
            }
            return null;
        }
        private void Cpl_SavePool(object sender, EventArgs e)
        {
            if (saveFileJson.ShowDialog() == DialogResult.OK)
            {
                CellPoolControl cpl = (CellPoolControl)sender;
                FileUtil.SaveToFile(saveFileJson.FileName, cpl.JSONString);
                modelUpdated = true;
            }
        }
        private void Cpl_LoadPool(object sender, EventArgs e)
        {
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                CellPoolControl cpl = (CellPoolControl)sender;
                cpl.JSONString = FileUtil.ReadFromFile(openFileJson.FileName);
            }
        }

        private void listCellPool_AddItem(object sender, EventArgs e)
        {
            CellPoolBase newPool = OpenCellPoolDialog(null);
            if (newPool != null)
            {
                Model.AddCellPool(newPool);
                listCellPools.AppendItem(newPool);
                modelUpdated = true;
            }
        }
        private void listCellPool_CopyItem(object sender, EventArgs e)
        {
            if (listCellPools.SelectedItem is not CellPoolBase pool) return;
            CellPoolBase poolDuplicate = new(pool);
            poolDuplicate = OpenCellPoolDialog(poolDuplicate);
            while (Model.GetCellPools().Any(p => p.CellGroup == poolDuplicate?.CellGroup))
            {
                //TODO WarningMessage("Cell pool group names have to be unique. Please enter a different name.");
                poolDuplicate = OpenCellPoolDialog(poolDuplicate);
            }
            if (poolDuplicate != null)
            {
                Model.AddCellPool(poolDuplicate);
                //TODO ModelTemplate.CopyConnectionsOfCellPool(pool, poolDuplicate);
                listCellPools.AppendItem(poolDuplicate);
                LoadProjections();
                modelUpdated = true;
            }
        }
        private void listCellPool_DeleteItem(object sender, EventArgs e)
        {
            if (listCellPools.SelectedIndex >= 0)
            {
                string msg = "Deleting a cell pool will remove all of its conections and applied stimuli as well. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    CellPoolBase cpl = (CellPoolBase)listCellPools.SelectedItem;
                    Model.RemoveCellPool(cpl);
                    listCellPools.RemoveItemAt(listCellPools.SelectedIndex);
                    LoadProjections();
                    LoadStimuli();
                    modelUpdated = true;
                }
            }
        }
        private void listCellPool_ViewItem(object sender, EventArgs e)
        {
            if (listCellPools.SelectedItem is not CellPoolTemplate pool) return;
            string oldName = pool.CellGroup;
            pool = OpenCellPoolDialog(pool); //check modeltemplate's list
            if (pool != null)
            {
                modelUpdated = true;
                int ind = listCellPools.SelectedIndex;
                listCellPools.RefreshItem(ind, pool);
                if (oldName != pool.CellGroup && Model is ModelTemplate mt)
                {
                    mt.RenameCellPool(oldName, pool.CellGroup);
                    LoadProjections();
                }
            }
        }

        private void listCellPool_ActivateItem(object sender, EventArgs e)
        {
            if (listCellPools.SelectedItem is not CellPoolBase pool) return;
            Model.GetCellPools().FirstOrDefault(p => p.Distinguisher == pool.Distinguisher).Active = pool.Active;
            modelUpdated = true;
        }

        private void listCellPool_SortItems(object sender, EventArgs e)
        {
                Model.SortCellPools();
            LoadPools();
        }

        #endregion

        #region Connection
        private InterPoolTemplate OpenConnectionDialog(InterPoolTemplate interpool)
        {
           /*TODO ControlContainer frmControl = new();
            InterPoolControl ipl = new(ModelTemplate.ModelDimensions.NumberOfSomites > 0);

            ipl.SetInterPoolTemplate(ModelTemplate.CellPoolTemplates, interpool, ModelTemplate);
            frmControl.AddControl(ipl);
            frmControl.Text = interpool?.ToString() ?? "New Connection";

            if (frmControl.ShowDialog() == DialogResult.OK)
            {
                InterPoolTemplate interPoolTemplate = ipl.GetInterPoolTemplate();
                ModelTemplate.LinkObjects(interPoolTemplate);
                return interPoolTemplate;
            }*/
            return null;
        }

        private void listConnections_AddItem(object sender, EventArgs e)
        {
           /*TODO  InterPoolTemplate newJnc = OpenConnectionDialog(null);
            if (newJnc != null)
            {
                ModelTemplate.InterPoolTemplates.Add(newJnc);
                listConnections.AppendItem(newJnc);
                modifiedJncs = true;
            }*/
        }
        private void listConnections_CopyItem(object sender, EventArgs e)
        {
            /*TODOif (listConnections.SelectedItem == null)
                return;
            InterPoolTemplate jnc = new(listConnections.SelectedItem as InterPoolTemplate);
            jnc = OpenConnectionDialog(jnc);
            if (jnc != null)
            {
                ModelTemplate.InterPoolTemplates.Add(jnc);
                listConnections.AppendItem(jnc);
                modifiedJncs = true;
            }*/
        }
        private void listConnections_DeleteItem(object sender, EventArgs e)
        {
            /*TODO if (listConnections.SelectedIndex >= 0)
            {
                InterPoolTemplate ipl = (InterPoolTemplate)listConnections.SelectedItem;
                ModelTemplate.InterPoolTemplates.Remove(ipl);
                listConnections.RemoveItemAt(listConnections.SelectedIndex);
                modifiedJncs = true;
            }*/
        }
        private void listConnections_ViewItem(object sender, EventArgs e)
        {
            /*TODO if (listConnections.SelectedItem == null)
                return;
            InterPoolTemplate interpool = listConnections.SelectedItem as InterPoolTemplate;
            interpool = OpenConnectionDialog(interpool); //check modeltemplate's list
            if (interpool != null)
            {
                int ind = listConnections.SelectedIndex;
                listConnections.RefreshItem(ind, interpool);
                modifiedJncs = true;
            }*/
        }

        private void listConnections_ActivateItem(object sender, EventArgs e)
        {
            InterPoolTemplate jnc = sender as InterPoolTemplate;
            //TODO Model.GetProjections().FirstOrDefault(p => p.Distinguisher == jnc.Distinguisher).Active = jnc.Active;
            modelUpdated = true;
        }

        private void listConnections_SortItems(object sender, EventArgs e)
        {
            /*TODO ModelTemplate.InterPoolTemplates.Sort();
            LoadModelTemplateInterPools();*/
        }

        private void listConnections_SortByType(object sender, EventArgs e)
        {
           /*TODO  ModelTemplate.InterPoolTemplates = ModelTemplate.InterPoolTemplates
                .OrderBy(jnc => jnc.ConnectionType.ToString())
                .ToList();
            LoadModelTemplateInterPools();*/
        }
        private void listConnections_SortBySource(object sender, EventArgs e)
        {
           /*TODO   ModelTemplate.InterPoolTemplates = ModelTemplate.InterPoolTemplates
                .OrderBy(jnc => jnc.PoolSource)
                .ToList();
            LoadModelTemplateInterPools();*/
        }

        private void listConnections_SortByTarget(object sender, EventArgs e)
        {
           /*TODO  ModelTemplate.InterPoolTemplates = ModelTemplate.InterPoolTemplates
                .OrderBy(jnc => jnc.PoolTarget)
                .ToList();
            LoadModelTemplateInterPools();*/
        }

        #endregion

        #region Stimulus

        private StimulusTemplate OpenStimulusDialog(StimulusTemplate stim)
        {
            ControlContainer frmControl = new();
            AppliedStimulusControl sc = new();

            /*TODOsc.SetStimulus(ModelTemplate.CellPoolTemplates, stim);
            frmControl.AddControl(sc);
            frmControl.Text = stim?.ToString() ?? "New Stimulus";

            if (frmControl.ShowDialog() == DialogResult.OK)
                return sc.GetStimulusTemplate();*/
            return null;
        }
        private void listStimuli_AddItem(object sender, EventArgs e)
        {
            StimulusTemplate stimulus = OpenStimulusDialog(null);
            if (stimulus != null)
            {
                //TODO ModelTemplate.AppliedStimuli.Add(stimulus);
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
                //TODO ModelTemplate.AppliedStimuli.Add(stimulus);
                listStimuli.AppendItem(stimulus);
            }
        }
        private void listStimuli_DeleteItem(object sender, EventArgs e)
        {
            if (listStimuli.SelectedIndex >= 0)
            {
                StimulusTemplate stim = (StimulusTemplate)listStimuli.SelectedItem;
                //TODO ModelTemplate.AppliedStimuli.Remove(stim);
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
            //TODO ModelTemplate.AppliedStimuli.FirstOrDefault(p => p.Distinguisher == stim.Distinguisher).Active = stim.Active;
        }



        #endregion
        
        private void ddDefaultNeuronCore_SelectedIndexChanged(object sender, EventArgs e)
        {
            Model.Settings.DefaultNeuronCore = ddDefaultNeuronCore.Text;
        }

        private void ddDefaultMuscleCore_SelectedIndexChanged(object sender, EventArgs e)
        {
            Model.Settings.DefaultMuscleCellCore = ddDefaultMuscleCellCore.Text;
        }
        
        private void linkClearModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*TODO ModelTemplate = new();
            Model = null;*/
            LoadModel();
        }
        
        private void eOutputFolder_Click(object sender, EventArgs e)
        {
            browseFolder.InitialDirectory = eOutputFolder.Text;
            if (browseFolder.ShowDialog() == DialogResult.OK)
            {
               Model.Settings.OutputFolder =
                    eOutputFolder.Text = browseFolder.SelectedPath;
            }
        }

        private void eTemporaryFolder_Click(object sender, EventArgs e)
        {
            browseFolder.InitialDirectory = eTemporaryFolder.Text;
            if (browseFolder.ShowDialog() == DialogResult.OK)
            {
                Model.Settings.TempFolder =
                    eTemporaryFolder.Text = browseFolder.SelectedPath;
            }
        }

        private void trackMode_ValueChanged(object sender, EventArgs e)
        {
            if (!trackMode.Focused) { return; }
           if (trackMode.Value == 1)
            {
                Model = new(Model);
                CurrentMode = Mode.RunningModel;
                LoadModel();
                string msg = $"Once you make changes in the model, the changes will not transfer to the model template." +
                    $"You will need to save the model independently.";
                //TODO 
            }
            else
            {
                string msg = "Do you want to recreate the model? Otherwise it will switch to the previous model.";
                //TODO 
                CurrentMode= Mode.Template;
            }
        }
    }
}
