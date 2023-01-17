using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using System.Data;
using SiliFish.Services;
using SiliFish.ModelUnits.Stim;
using SiliFish.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SiliFish.ModelUnits.Junction;

namespace SiliFish.UI.Controls
{
    public partial class ModelControl : UserControl
    {
        private event EventHandler modelChanged;
        public event EventHandler ModelChanged { add => modelChanged += value; remove => modelChanged -= value; }
        enum Mode { Template, RunningModel };
        Mode CurrentMode = Mode.Template;

        private ModelBase Model;

        private bool modelUpdated = false;
        public bool ModelUpdated {
            get => modelUpdated;
            set 
            {
                modelUpdated = true;
                modelChanged?.Invoke(this, EventArgs.Empty);
            }
        } 
        
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
                    TextBox textBox = new()
                    {
                        Tag = kvp.Value,
                        Text = kvp.Value?.ToString()
                    };
                    flowPanel.Controls.Add(textBox);
                    flowPanel.SetFlowBreak(textBox, true);
                }
            }
        }

        private void LoadParamsAndSettings()
        {
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

        private void LoadModel()
        {
            LoadParamsAndSettings();
            LoadPools();
            LoadProjections();
            LoadStimuli();
            this.Text = $"SiliFish {Model.ModelName}";
        }

        public void SetModel(ModelBase model)
        {
            Model = model;
            CurrentMode = Model is ModelTemplate ? Mode.Template : Mode.RunningModel;
            LoadModel();
        }
        public ModelBase GetModel()
        {
            try
            {
                Model.ModelName = eModelName.Text;
                Model.ModelDescription = eModelDescription.Text;
                Model.Parameters = ReadParams();
                return Model;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        #endregion
        #region Cell Pool
         private void LoadPools()
        {
            listCellPools.ClearItems();
            foreach (CellPoolTemplate cp in Model.GetCellPools())
            {
                listCellPools.AppendItem(cp);
            }
        }       
        private CellPoolTemplate OpenCellPoolDialog(CellPoolTemplate pool)
        {
            ControlContainer frmControl = new();
            CellPoolControl cpl = new(Model.ModelDimensions.NumberOfSomites > 0);
            cpl.LoadPool += Cpl_LoadPool;
            cpl.SavePool += Cpl_SavePool;

            cpl.PoolBase = pool;
            frmControl.AddControl(cpl);
            frmControl.Text = pool?.ToString() ?? "New Cell Pool";

            if (frmControl.ShowDialog() == DialogResult.OK)
                return cpl.PoolBase;
            return null;
        }
        private void Cpl_SavePool(object sender, EventArgs e)
        {
            if (saveFileJson.ShowDialog() == DialogResult.OK)
            {
                CellPoolControl cpl = (CellPoolControl)sender;
                FileUtil.SaveToFile(saveFileJson.FileName, cpl.JSONString);
                ModelUpdated = true;
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
            CellPoolTemplate newPool = OpenCellPoolDialog(CurrentMode == Mode.RunningModel ? new CellPool() : new CellPoolTemplate());
            if (newPool != null)
            {
                Model.AddCellPool(newPool);
                listCellPools.AppendItem(newPool);
                ModelUpdated = true;
            }
        }
        private void listCellPool_CopyItem(object sender, EventArgs e)
        {
            if (listCellPools.SelectedItem is not CellPoolTemplate pool) return;
            CellPoolTemplate poolDuplicate = pool.CreateCopy();
            poolDuplicate.CellGroup += " Copy";
            poolDuplicate = OpenCellPoolDialog(poolDuplicate);
            while (Model.GetCellPools().Any(p => p.CellGroup == poolDuplicate?.CellGroup))
            {
                MessageBox.Show("Cell pool group names have to be unique. Please enter a different name.");
                poolDuplicate = OpenCellPoolDialog(poolDuplicate);
            }
            if (poolDuplicate != null)
            {
                Model.AddCellPool(poolDuplicate);
                //TODO ModelTemplate.CopyConnectionsOfCellPool(pool, poolDuplicate);
                listCellPools.AppendItem(poolDuplicate);
                LoadProjections();
                ModelUpdated = true;
            }
        }
        private void listCellPool_DeleteItem(object sender, EventArgs e)
        {
            if (listCellPools.SelectedIndex >= 0)
            {
                string msg = "Deleting a cell pool will remove all of its conections and applied stimuli as well. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    CellPoolTemplate cpl = (CellPoolTemplate)listCellPools.SelectedItem;
                    Model.RemoveCellPool(cpl);
                    listCellPools.RemoveItemAt(listCellPools.SelectedIndex);
                    LoadProjections();
                    LoadStimuli();
                    ModelUpdated = true;
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
                ModelUpdated = true;
                int ind = listCellPools.SelectedIndex;
                listCellPools.RefreshItem(ind, pool);
                if (oldName != pool.CellGroup)
                {
                    if (Model is ModelTemplate mt)
                    {
                        mt.RenameCellPool(oldName, pool.CellGroup);
                        LoadProjections();
                    }
                    else { }//TODO
                }
            }
        }

        private void listCellPool_ActivateItem(object sender, EventArgs e)
        {
            if (listCellPools.SelectedItem is not CellPoolTemplate pool) return;
            Model.GetCellPools().FirstOrDefault(p => p.Distinguisher == pool.Distinguisher).Active = pool.Active;
            ModelUpdated = true;
        }

        private void listCellPool_SortItems(object sender, EventArgs e)
        {
            Model.SortCellPools();
            LoadPools();
        }

        #endregion

        #region Connection
        private void LoadProjections()
        {
            if (CurrentMode != Mode.Template) return;
            listConnections.ClearItems();
            foreach (object jnc in Model.GetProjections())
            {
                listConnections.AppendItem(jnc);
            }
        }

        private InterPoolTemplate OpenConnectionDialog(InterPoolTemplate interpool)
        {
            if (Model==null) return null;
            ControlContainer frmControl = new();
            InterPoolControl ipl = new(Model.ModelDimensions.NumberOfSomites > 0);

            /*TODO ipl.SetInterPoolTemplate(ModelTemplate.CellPoolTemplates, interpool, ModelTemplate);
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
            if (listConnections.SelectedItem == null)
                return;
            InterPoolTemplate interpool = listConnections.SelectedItem as InterPoolTemplate;
            interpool = OpenConnectionDialog(interpool); //check modeltemplate's list
            if (interpool != null)
            {
                int ind = listConnections.SelectedIndex;
                listConnections.RefreshItem(ind, interpool);
                ModelUpdated = true;
            }
        }

        private void listConnections_ActivateItem(object sender, EventArgs e)
        {
            InterPoolTemplate jnc = sender as InterPoolTemplate;
            //TODO Model.GetProjections().FirstOrDefault(p => p.Distinguisher == jnc.Distinguisher).Active = jnc.Active;
            ModelUpdated = true;
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

         private void LoadStimuli()
        {
            if (CurrentMode != Mode.Template) return;
            listStimuli.ClearItems();
            foreach (object stim in Model.GetStimuli())
            {
                listStimuli.AppendItem(stim);
            }
        }       
        
        private StimulusBase OpenStimulusDialog(StimulusBase stim)
        {
            ControlContainer frmControl = new();
            AppliedStimulusControl sc = new();

            if (CurrentMode==Mode.Template) 
                sc.SetStimulus((Model as ModelTemplate).CellPoolTemplates, stim as StimulusTemplate);
            else
                sc.SetStimulus((Model as RunningModel).CellPools, stim as Stimulus);
            frmControl.AddControl(sc);
            frmControl.Text = stim?.ToString() ?? "New Stimulus";

            if (frmControl.ShowDialog() == DialogResult.OK)
                return sc.GetStimulus();
            return null;
        }
        private void listStimuli_AddItem(object sender, EventArgs e)
        {
            StimulusBase stimulus = OpenStimulusDialog(null);
            if (stimulus != null)
            {
                Model.AddStimulus(stimulus);
                listStimuli.AppendItem(stimulus);
                ModelUpdated = true;
            }
        }
        private void listStimuli_CopyItem(object sender, EventArgs e)
        {
            if (listStimuli.SelectedItem == null)
                return;
            StimulusBase stimulus = listStimuli.SelectedItem as StimulusBase;//TODO create copy!!!
            stimulus = OpenStimulusDialog(stimulus);
            if (stimulus != null)
            {
                Model.AddStimulus(stimulus);
                listStimuli.AppendItem(stimulus);
                ModelUpdated = true;
            }
        }
        private void listStimuli_DeleteItem(object sender, EventArgs e)
        {
            if (listStimuli.SelectedIndex >= 0)
            {
                StimulusBase stim = (StimulusBase)listStimuli.SelectedItem;
                Model.RemoveStimulus(stim);
                listStimuli.RemoveItemAt(listStimuli.SelectedIndex);
                ModelUpdated = true;
            }
        }
        private void listStimuli_ViewItem(object sender, EventArgs e)
        {
            if (listStimuli.SelectedItem == null)
                return;
            StimulusBase stimulus = listStimuli.SelectedItem as StimulusTemplate;
            stimulus = OpenStimulusDialog(stimulus); //check modeltemplate's list
            if (stimulus != null)
            {
                int ind = listStimuli.SelectedIndex;
                listStimuli.RefreshItem(ind, stimulus);
                ModelUpdated = true;
            }
        }

        private void listStimuli_ActivateItem(object sender, EventArgs e)
        {
            StimulusBase stim = sender as StimulusBase;
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


    }
}
