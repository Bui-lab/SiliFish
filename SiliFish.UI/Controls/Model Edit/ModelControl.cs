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
using System.Text.Json;
using SiliFish.UI.Definitions;
using SiliFish.Definitions;

namespace SiliFish.UI.Controls
{
    public partial class ModelControl : UserControl
    {
        public event EventHandler ModelChanged;
        RunMode CurrentMode = RunMode.Template;

        private ModelBase Model;

        private bool modelUpdated = false;
        public bool ModelUpdated {
            get => modelUpdated;
            set => modelUpdated = value;
        }

        private CellPool SelectedPool; 
        private Cell SelectedCell; //valid if Mode == Mode.RunningModel

        public ModelControl()
        {
            InitializeComponent();
            listConnections.AddContextMenu("Sort by Type", listConnections_SortByType);
            listConnections.AddContextMenu("Sort by Source", listConnections_SortBySource);
            listConnections.AddContextMenu("Sort by Target", listConnections_SortByTarget);
            tabModel.BackColor = Color.White;
        }

        private void ModelIsUpdated()
        {
            modelUpdated = true;
            ModelChanged?.Invoke(this, EventArgs.Empty);
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
            eModelName.Text = Model?.ModelName;
            eModelDescription.Text = Model?.ModelDescription;
            propModelDimensions.SelectedObject = Model?.ModelDimensions;

            //Settings
            if (ddDefaultNeuronCore.Items.Count == 0)
                ddDefaultNeuronCore.Items.AddRange(CellCoreUnit.GetCoreTypes().ToArray());
            ddDefaultNeuronCore.Text = Model?.Settings.DefaultNeuronCore;
            if (ddDefaultMuscleCellCore.Items.Count == 0)
                ddDefaultMuscleCellCore.Items.AddRange(CellCoreUnit.GetCoreTypes().ToArray());
            ddDefaultMuscleCellCore.Text = Model?.Settings.DefaultMuscleCellCore;
            eTemporaryFolder.Text = Model?.Settings.TempFolder;
            eOutputFolder.Text = Model?.Settings.OutputFolder;
            propSettings.SelectedObject = Model?.Settings;
            propKinematics.SelectedObject = Model?.KinemParam;
            LoadParams(Model?.Parameters);
        }

        private static void ReadParamsFromTabPage(Dictionary<string, object> ParamDict, TabPage page)
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
            this.Text = $"SiliFish {Model?.ModelName}";
        }

        public void SetModel(ModelBase model, bool clearJson = true)
        {
            Model = model;
            CurrentMode = Model is ModelTemplate ? RunMode.Template : RunMode.RunningModel;
            splitCellPools.Panel2Collapsed = CurrentMode == RunMode.Template;
            propModelDimensions.Enabled = propSettings.Enabled = CurrentMode == RunMode.Template;
            LoadModel();
            modelUpdated = false;
            if (clearJson)
                eModelJSON.Text = "";
        }
        public ModelBase GetModel()
        {
            try
            {
                if (Model == null) return null;
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
        private void AddCellPool(CellPoolTemplate newPool)
        {
            if (newPool != null)
            {
                if (newPool is CellPool cp)
                {
                    if (cp.PositionLeftRight == SagittalPlane.Both)
                    {
                        CellPool leftPool = cp;
                        leftPool.PositionLeftRight = SagittalPlane.Left;
                        CellPool rightPool = new(cp)
                        {
                            PositionLeftRight = SagittalPlane.Right
                        };

                        Model.AddCellPool(leftPool);
                        leftPool.GenerateCells();
                        listCellPools.AppendItem(leftPool);

                        Model.AddCellPool(rightPool);
                        rightPool.GenerateCells();
                        listCellPools.AppendItem(rightPool);
                    }
                    else
                    {
                        Model.AddCellPool(cp);
                        cp.GenerateCells();
                        listCellPools.AppendItem(cp);
                    }
                }
                else
                {
                    Model.AddCellPool(newPool);
                    listCellPools.AppendItem(newPool);
                }
                ModelIsUpdated();
            }
        }
        private void LoadPools()
        {
            listCellPools.ClearItems();
            listCells.ClearItems();
            if (Model == null) return;
            foreach (CellPoolTemplate cp in Model.GetCellPools())
            {
                listCellPools.AppendItem(cp);
            }
            SelectedPool = null;
            SelectedCell = null;
        }       
        private CellPoolTemplate OpenCellPoolDialog(CellPoolTemplate pool)
        {
            if (Model == null) return null;
            ControlContainer frmControl = new();
            CellPoolControl cpl = new(Model.ModelDimensions.NumberOfSomites > 0, Model.Settings);
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
                ModelIsUpdated();
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

        private void listCellPools_ItemAdd(object sender, EventArgs e)
        {
            CellPoolTemplate newPool = OpenCellPoolDialog(CurrentMode == RunMode.RunningModel ? new CellPool() : new CellPoolTemplate());
            if (newPool != null)
            {
                AddCellPool(newPool);
                ModelIsUpdated();
            }
        }
        private void listCellPools_ItemSelect(object sender, EventArgs e)
        {
            if (CurrentMode == RunMode.RunningModel) 
                listCells.ClearItems();
            if (sender is CellPool pool)
            {
                SelectedPool = pool;
                SelectedCell = null;
                LoadProjections(pool);
                LoadCells();
            }
            else if (sender is CellPoolTemplate cpt)
            {
                LoadProjections(cpt.CellGroup);
            }
            else if (sender == null)
            {
                SelectedPool = null;
                SelectedCell = null;
                LoadProjections();//Full list
            }
        }
        private void listCellPools_ItemCopy(object sender, EventArgs e)
        {
            if (listCellPools.SelectedItem is not CellPoolTemplate poolTemplate) return;
            if (listCellPools.SelectedItem is not CellPool pool) return;

            CellPoolTemplate poolDuplicate = pool?.CreateCopy() ?? poolTemplate.CreateCopy();
            poolDuplicate.CellGroup += " Copy";
            poolDuplicate = OpenCellPoolDialog(poolDuplicate);
            while (Model.GetCellPools().Any(p => p.CellGroup == poolDuplicate?.CellGroup))
            {
                MessageBox.Show("Cell pool group names have to be unique. Please enter a different name.");
                poolDuplicate = OpenCellPoolDialog(poolDuplicate);
            }
            if (poolDuplicate != null)
            {
                AddCellPool(poolDuplicate);
                LoadProjections();
                ModelIsUpdated();    
            }
        }
        private void listCellPools_ItemDelete(object sender, EventArgs e)
        {
            if (listCellPools.SelectedIndex >= 0)
            {
                string msg = "Deleting a cell pool will remove all of its connections and applied stimuli as well. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    CellPoolTemplate cpl = (CellPoolTemplate)listCellPools.SelectedItem;
                    Model.RemoveCellPool(cpl);
                    listCellPools.RemoveItemAt(listCellPools.SelectedIndex);
                    LoadProjections();
                    LoadStimuli();
                    ModelIsUpdated();
                }
            }
        }
        private void listCellPools_ItemView(object sender, EventArgs e)
        {
            if (listCellPools.SelectedItem == null)
                return;
            if (listCellPools.SelectedItem is not CellPoolTemplate pool) return;
            string oldName = pool.CellGroup;
            pool = OpenCellPoolDialog(pool); //check modeltemplate's list
            if (pool != null)
            {
                ModelIsUpdated();
                int ind = listCellPools.SelectedIndex;
                listCellPools.RefreshItem(ind, pool);
                if (oldName != pool.CellGroup)
                {
                    (Model as ModelTemplate).RenameCellPool(oldName, pool.CellGroup);
                    LoadProjections();
                }
            }
        }
        private void listCellPools_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }
        private void listCellPool_SortItems(object sender, EventArgs e)
        {
            Model.SortCellPools();
            LoadPools();
        }

        private void LoadCells()
        {
            listCells.ClearItems();
            if (SelectedPool == null) return;
            foreach (Cell c in SelectedPool.GetCells())
                listCells.AppendItem(c);
            SelectedCell = null;
        }
        private Cell OpenCellDialog(Cell cell)
        {
            if (Model == null) return null;
            if (CurrentMode == RunMode.Template) return null;
            MessageBox.Show("Implementation in progress.", "Model Edit");
            //MODEL EDIT 
            /*
            ControlContainer frmControl = new();
            CellControl cc = new(Model.ModelDimensions.NumberOfSomites > 0, Model.Settings);

            cell.CellBase = cell;
            frmControl.AddControl(cc);
            frmControl.Text = cell?.ToString() ?? "New Cell";

            if (frmControl.ShowDialog() == DialogResult.OK)
                return cc.CellBase;*/
            return null;
        }

        private void listCell_ItemAdd(object sender, EventArgs e)
        {
            if (SelectedPool == null) return;
            //MODEL EDIT

            Cell newCell = OpenCellDialog(null);// new Cell());
            /*
            if (newCell != null)
            {
                SelectedPool.AddCell(newCell);
                listCells.AppendItem(newCell);
                ModelIsUpdated();
            }*/
        }

        private void listCells_ItemSelect(object sender, EventArgs e)
        {
            if (CurrentMode != RunMode.RunningModel) return;
            if (sender is Cell cell)
            {
                SelectedCell = cell;
                LoadProjections(cell);
            }
        }

        private void listCells_ItemCopy(object sender, EventArgs e)
        {
            if (listCells.SelectedItem is not Cell cell) return;
            //MODEL EDIT
            /*Cell cellDuplicate = cell.CreateCopy();
            cellDuplicate.Sequence = cell.CellPool.GetCells().Max(c=>c.Sequence) + 1; //TODO getMaxSeq
            cellDuplicate = OpenCellDialog(cellDuplicate);
            while (Model.GetCellPools().Any(p => p.CellGroup == cellDuplicate?.CellGroup))
            {
                MessageBox.Show("Cell pool group names have to be unique. Please enter a different name.");
                cellDuplicate = OpenCellPoolDialog(cellDuplicate);
            }
            if (cellDuplicate != null)
            {
                cell.CellPool.AddCell(cellDuplicate);
                Model.CopyConnectionsOfCell(cell, cellDuplicate);
                listCells.AppendItem(cellDuplicate);
                LoadProjections();
                ModelIsUpdated();
            }*/
        }
        private void listCells_ItemDelete(object sender, EventArgs e)
        {
            //MODEL EDIT
            /*if (listCells.SelectedIndex >= 0)
            {
                string msg = "Deleting a cell will remove all of its connections and applied stimuli as well. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Cell cell = (Cell)listCells.SelectedItem;
                    cell.CellPool.RemoveCell(cell);
                    listCells.RemoveItemAt(listCells.SelectedIndex);
                    LoadProjections();
                    LoadStimuli();
                    ModelIsUpdated();
                }
            }*/
        }
        private void listCells_ItemView(object sender, EventArgs e)
        {
            if (listCells.SelectedItem == null)
                return;
            if (listCells.SelectedItem is not Cell cell) return;
            //MODEL EDIT
            /*
            int oldSeq = cell.Sequence;
            cell = OpenCellDialog(cell); //check modeltemplate's list
            if (cell != null)
            {
                ModelIsUpdated();
                int ind = listCells.SelectedIndex;
                listCells.RefreshItem(ind, cell);
                if (oldSeq != cell.Sequence)
                {
                   //check seq uniqueness
                    LoadProjections();
                }
            }*/
        } 
        private void listCell_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }

        #endregion

        #region Connection
        private void LoadProjections()
        {
            listConnections.ClearItems();
            lConnectionsTitle.Text = "Connections";
            if (Model == null) return;
            foreach (object jnc in Model.GetProjections())
            {
                listConnections.AppendItem(jnc);
            }
        }

        private void LoadProjections(string cellPoolID)
        {
            listConnections.ClearItems();
            lConnectionsTitle.Text = $"Connections of {cellPoolID}";
            if (Model == null || Model is not ModelTemplate) return;
            foreach (InterPoolTemplate jnc in Model.GetProjections()
                .Where(proj => ((InterPoolTemplate)proj).PoolSource == cellPoolID || ((InterPoolTemplate)proj).PoolTarget == cellPoolID)
                .Cast<InterPoolTemplate>())
            {
                listConnections.AppendItem(jnc);
            }
        }
        private void LoadProjections(CellPool cp)
        {
            if (cp == null)
            {
                LoadProjections();
                return;
            }
            lConnectionsTitle.Text = $"Connections of {cp.ID}";
            listConnections.ClearItems();
            foreach (JunctionBase jnc in cp.Projections)
            {
                listConnections.AppendItem(jnc);
            }
        }
        private void LoadProjections(Cell cell)
        {
            if (cell == null)
            {
                if (SelectedPool != null)
                    LoadProjections(SelectedPool);
                else
                    LoadProjections();
                return;
            }
            listConnections.ClearItems();
            lConnectionsTitle.Text = $"Connections of {cell.ID}";
            foreach (JunctionBase jnc in cell.GapJunctions)
            {
                listConnections.AppendItem(jnc);
            }
            if (cell is Neuron neuron)
            {
                foreach (JunctionBase jnc in neuron.Terminals.Union(neuron.Synapses))
                {
                    listConnections.AppendItem(jnc);
                }
            }
            if (cell is MuscleCell muscleCell)
            {
                foreach (JunctionBase jnc in muscleCell.EndPlates)
                {
                    listConnections.AppendItem(jnc);
                }
            }
        }
        private JunctionBase OpenConnectionDialog(JunctionBase interpool)
        {
            if (Model == null) return null;
            if (CurrentMode == RunMode.Template)
            {
                ControlContainer frmControl = new();
                InterPoolControl ipControl = new(Model.ModelDimensions.NumberOfSomites > 0, Model.Settings);
                ModelTemplate modelTemplate = Model as ModelTemplate;
                InterPoolTemplate interPoolTemplate = interpool as InterPoolTemplate;
                ipControl.SetInterPoolTemplate(modelTemplate.CellPoolTemplates, interPoolTemplate, modelTemplate);
                frmControl.AddControl(ipControl);
                frmControl.Text = interpool?.ToString() ?? "New Connection";

                if (frmControl.ShowDialog() == DialogResult.OK)
                {
                    interPoolTemplate = ipControl.GetInterPoolTemplate();
                    modelTemplate.LinkObjects(interPoolTemplate);
                    return interPoolTemplate;
                }
            }
            else
            {
                MessageBox.Show("Implementation in progress.", "Model Edit");
                //MODEL EDIT
                ControlContainer frmControl = new();
                JunctionControl jncControl = new(Model as RunningModel); 
                jncControl.SetJunction(interpool, false);
                frmControl.AddControl(jncControl);
                frmControl.Text = interpool?.ToString() ?? "New Connection";

                if (frmControl.ShowDialog() == DialogResult.OK)
                {
                    interpool = jncControl.GetJunction();
                    //TODO modelTemplate.LinkObjects(interPoolTemplate);
                    return interpool;
                }                
            }
            return null;
        }

        private void listConnections_ItemAdd(object sender, EventArgs e)
        {
            JunctionBase newJnc = OpenConnectionDialog(null);
            if (newJnc != null)
            {
                Model.AddJunction(newJnc);
                listConnections.AppendItem(newJnc);
                ModelIsUpdated();
            }
        }
        private void listConnections_ItemCopy(object sender, EventArgs e)
        {
            if (Model == null) return ;
            if (listConnections.SelectedItem == null)
                return;
            if (CurrentMode == RunMode.Template)
            {
                InterPoolTemplate jnc = new(listConnections.SelectedItem as InterPoolTemplate);
                jnc = OpenConnectionDialog(jnc) as InterPoolTemplate;
                if (jnc != null)
                {
                    Model.AddJunction(jnc);
                    listConnections.AppendItem(jnc);
                }
                ModelIsUpdated();
            }
            else
            {
                MessageBox.Show("Implementation in progress.", "Model Edit");
                //MODEL EDIT JunctionControl jncControl = new();
            }
        }
        private void listConnections_ItemDelete(object sender, EventArgs e)
        {
            if (listConnections.SelectedIndex >= 0)
            {
                Model.RemoveJunction(listConnections.SelectedItem as JunctionBase);
                listConnections.RemoveItemAt(listConnections.SelectedIndex);
                ModelIsUpdated();
            }
        }
        private void listConnections_ItemView(object sender, EventArgs e)
        {
            if (listConnections.SelectedItem == null)
                return;
            JunctionBase jnc = listConnections.SelectedItem as JunctionBase;
            jnc = OpenConnectionDialog(jnc); //check modeltemplate's list
            if (jnc != null)
            {
                int ind = listConnections.SelectedIndex;
                listConnections.RefreshItem(ind, jnc);
                ModelIsUpdated();
            }
        }

        private void listConnections_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }

        private void listConnections_SortItems(object sender, EventArgs e)
        {
            if (SelectedCell != null)
            {
                SelectedCell.SortJunctions();
                LoadProjections(SelectedCell);
            }
            else if (CurrentMode == RunMode.Template)
            {
                Model.SortJunctions();
                LoadProjections();
            }
        }

        private void listConnections_SortByType(object sender, EventArgs e)
        {
            if (CurrentMode == RunMode.Template)
            {
                Model.SortJunctionsByType();
                LoadProjections();
            }
        }
        private void listConnections_SortBySource(object sender, EventArgs e)
        {
            if (SelectedCell != null)
            {
                SelectedCell.SortJunctionsBySource();
                LoadProjections(SelectedCell);
            }
            else if (CurrentMode == RunMode.Template)
            {
                Model.SortJunctionsBySource();
                LoadProjections();
            }
        }

        private void listConnections_SortByTarget(object sender, EventArgs e)
        {
            if (SelectedCell != null)
            {
                SelectedCell.SortJunctionsByTarget();
                LoadProjections(SelectedCell);
            }
            else if (CurrentMode == RunMode.Template)
            {
                Model.SortJunctionsByTarget();
                LoadProjections();
            }
        }

        #endregion

        #region Stimulus

        private void LoadStimuli()
        {
            listStimuli.ClearItems();
            if (Model == null) return;
            foreach (object stim in Model.GetStimuli())
            {
                listStimuli.AppendItem(stim);
            }
        }
        
        private StimulusBase OpenStimulusDialog(StimulusBase stim)
        {
            if (Model == null) return null;
            if (CurrentMode == RunMode.Template)
            {
                ControlContainer frmControl = new();
                StimulusTemplateControl sc = new();

                if (CurrentMode == RunMode.Template)
                    sc.SetStimulus((Model as ModelTemplate).CellPoolTemplates, stim as StimulusTemplate);
                else
                    sc.SetStimulus((Model as RunningModel).CellPools, stim as Stimulus);
                frmControl.AddControl(sc);
                frmControl.Text = stim?.ToString() ?? "New Stimulus";

                if (frmControl.ShowDialog() == DialogResult.OK)
                    return sc.GetStimulus();
            }
            else
            {
                MessageBox.Show("Implementation in progress.", "Model Edit");
                //MODEL EDIT 
            }
            return null;
        }
        private void listStimuli_ItemAdd(object sender, EventArgs e)
        {
            StimulusBase stimulus = OpenStimulusDialog(null);
            if (stimulus != null)
            {
                Model.AddStimulus(stimulus);
                listStimuli.AppendItem(stimulus);
                ModelIsUpdated();
            }
        }
        private void listStimuli_ItemCopy(object sender, EventArgs e)
        {
            if (listStimuli.SelectedItem == null)
                return;
            StimulusBase stimulus = (listStimuli.SelectedItem as StimulusBase).CreateCopy();
            stimulus = OpenStimulusDialog(stimulus);
            if (stimulus != null)
            {
                Model.AddStimulus(stimulus);
                listStimuli.AppendItem(stimulus);
                ModelIsUpdated();
            }
        }
        private void listStimuli_ItemDelete(object sender, EventArgs e)
        {
            if (listStimuli.SelectedIndex >= 0)
            {
                StimulusBase stim = (StimulusBase)listStimuli.SelectedItem;
                Model.RemoveStimulus(stim);
                listStimuli.RemoveItemAt(listStimuli.SelectedIndex);
                ModelIsUpdated();
            }
        }
        private void listStimuli_ItemView(object sender, EventArgs e)
        {
            if (listStimuli.SelectedItem == null)
                return;
            StimulusBase stimulus = listStimuli.SelectedItem as StimulusTemplate;
            stimulus = OpenStimulusDialog(stimulus); //check modeltemplate's list
            if (stimulus != null)
            {
                int ind = listStimuli.SelectedIndex;
                listStimuli.RefreshItem(ind, stimulus);
                ModelIsUpdated();
            }
        }
        private void listStimuli_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }

        #endregion

        #region Advanced Settings
        private void ddDefaultNeuronCore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Model == null) return;
            Model.Settings.DefaultNeuronCore = ddDefaultNeuronCore.Text;
        }

        private void ddDefaultMuscleCore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Model == null) return;
            Model.Settings.DefaultMuscleCellCore = ddDefaultMuscleCellCore.Text;
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


        #endregion

        private void eModelJSON_TextChanged(object sender, EventArgs e)
        {
            if (eModelJSON.Focused)
                btnLoadModelJSON.Enabled = true;
        }

        private void btnDisplayModelJSON_Click(object sender, EventArgs e)
        {
            GetModel();
            try
            {
                eModelJSON.Text = JsonUtil.ToJson(Model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            btnLoadModelJSON.Enabled = false;
        }

        private void btnLoadModelJSON_Click(object sender, EventArgs e)
        {
            try
            {
                string json = eModelJSON.Text;
                ModelBase mb = ModelFile.ReadFromJson(json, out List<string> issues);
                SetModel(mb, clearJson: false);
                ModelIsUpdated();
                MessageBox.Show("Updated JSON is loaded.");
            }
            catch (JsonException exc)
            {
                MessageBox.Show($"There is a problem with the JSON file. Please check the format of the text in a JSON editor. {exc.Message}");
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
            }

        }
    }
}
