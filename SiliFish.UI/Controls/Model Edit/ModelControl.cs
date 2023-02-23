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
using System.Windows.Forms;
using SiliFish.UI.Extensions;
using System.Diagnostics;
using System.IO;
using SiliFish.UI.EventArguments;
using OxyPlot;

namespace SiliFish.UI.Controls
{
    public partial class ModelControl : UserControl
    {
        public event EventHandler ModelChanged;
        public event EventHandler PlotRequested;
        RunMode CurrentMode = RunMode.Template;

        private ModelBase Model;

        private bool modelUpdated = false;
        public bool ModelUpdated {
            get => modelUpdated;
            set => modelUpdated = value;
        }

        private CellPoolTemplate SelectedPoolTemplate;
        private CellPool SelectedPool;
        private Cell SelectedCell; //valid if Mode == Mode.RunningModel
        private JunctionBase SelectedJunction;
        private StimulusBase SelectedStimulus;
        public ModelControl()
        {
            InitializeComponent();
            listConnections.AddSortContextMenu("Sort by Type", listConnections_SortByType);
            listConnections.AddSortContextMenu("Sort by Source", listConnections_SortBySource);
            listConnections.AddSortContextMenu("Sort by Target", listConnections_SortByTarget);
            listStimuli.EnableImportExport();
            tabModel.BackColor = Color.White;
            eModelJSON.AddContextMenu();
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
            if (Model is RunningModel)
                LoadCells();
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
                if (CurrentMode == RunMode.RunningModel)
                {
                    List<CellPool> cellPools = (Model as RunningModel).GenerateCellPoolsFrom(newPool);
                    foreach (CellPool cp in cellPools)
                        listCellPools.AppendItem(cp);
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
            SelectedPoolTemplate = null;
            SelectedPool = null;
            SelectedCell = null;
        }
        private CellPoolTemplate OpenCellPoolDialog(CellPoolTemplate pool)
        {
            if (Model == null) return null;
            ControlContainer frmControl = new();
            CellPoolControl cplControl = new(Model.ModelDimensions.NumberOfSomites > 0, Model.Settings);
            cplControl.LoadPool += Cpl_LoadPool;
            cplControl.SavePool += Cpl_SavePool;

            cplControl.PoolBase = pool;
            frmControl.AddControl(cplControl, cplControl.CheckValues);

            frmControl.Text = pool?.ToString() ?? "New Cell Pool";

            if (frmControl.ShowDialog() == DialogResult.OK)
                return cplControl.PoolBase;
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
            //a new cell pool is always generated as a CellPoolTemplate
            CellPoolTemplate newPool = OpenCellPoolDialog(new CellPoolTemplate());
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
                LoadCells();
                LoadProjections(pool);
                LoadStimuli(pool);
            }
            else if (sender is CellPoolTemplate cpt)
            {
                SelectedPoolTemplate = cpt;
                LoadProjections(cpt.CellGroup);
                LoadStimuli(cpt.CellGroup);
            }
            else if (sender == null)
            {
                SelectedPool = null;
                SelectedCell = null;
                LoadCells();//Full list
                LoadProjections();//Full list
                LoadStimuli();//Full list
            }
        }
        private void listCellPools_ItemCopy(object sender, EventArgs e)
        {
            if (listCellPools.SelectedItem is not CellPoolTemplate poolTemplate) return;
            CellPool pool = listCellPools.SelectedItem as CellPool;

            CellPoolTemplate poolDuplicate = pool?.CreateTemplateCopy() ?? poolTemplate.CreateTemplateCopy();
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
                    //Automatically loaded with SelectedIndexChangedEvent LoadProjections();
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
        private void listCellPools_ItemPlot(object sender, EventArgs e)
        {
            PlotRequestArgs args = new() { unitToPlot = SelectedPool };
            PlotRequested?.Invoke(this, args);
        }
        private void listCellPools_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }
        private void listCellPools_ItemsSort(object sender, EventArgs e)
        {
            Model.SortCellPools();
            LoadPools();
        }

        private void LoadCells()
        {
            if (Model is ModelTemplate) return;
            listCells.ClearItems();
            lCellsTitle.Text = SelectedPool != null ? $"Cells of {SelectedPool.ID}" : "Cells";

            List<Cell> Cells = (List<Cell>)(SelectedPool?.GetCells() ?? (Model as RunningModel).GetCells());
            foreach (Cell cell in Cells)
                listCells.AppendItem(cell);
            SelectedCell = null;
        } 
        
        private Cell OpenCellDialog(Cell cell)
        {
            if (Model == null) return null;
            if (CurrentMode == RunMode.Template) return null;
            ControlContainer frmControl = new();
            CellControl cellControl = new(Model as RunningModel)
            {
                Cell = cell
            };
            frmControl.AddControl(cellControl, cellControl.CheckValues);

            frmControl.Text = cell?.ToString() ?? "New Cell";

            if (frmControl.ShowDialog() == DialogResult.OK)
                return cellControl.Cell;
            return null;
        }

        private void listCells_ItemAdd(object sender, EventArgs e)
        {
            Cell newCell = OpenCellDialog(null);
            if (newCell != null)
            {
                newCell.CellPool.AddCell(newCell);
                if (SelectedPool == newCell.CellPool)
                    listCells.AppendItem(newCell);
                else
                    SelectedPool = newCell.CellPool;
                SelectedCell = newCell;
                ModelIsUpdated();
            }
        }

        private void listCells_ItemSelect(object sender, EventArgs e)
        {
            if (CurrentMode != RunMode.RunningModel) return;
            if (sender is Cell cell)
            {
                SelectedCell = cell;
                LoadProjections(cell);
                LoadStimuli(cell);
            }
        }

        private void listCells_ItemCopy(object sender, EventArgs e)
        {
            if (listCells.SelectedItem is not Cell cell) return;
            Cell cellDuplicate = (Cell)cell.CreateCopy();
            cellDuplicate.Sequence = cell.CellPool.GetMaxCellSequence(cell.Somite) + 1; 
            cellDuplicate = OpenCellDialog(cellDuplicate);
            while (cellDuplicate != null && cell.CellPool.Cells.Any(c => c.ID == cellDuplicate.ID))
            {
                MessageBox.Show("Cell sequence has to be unique for a cell pool and somite. Please enter a different sequence.");
                cellDuplicate = OpenCellDialog(cellDuplicate);
            }
            if (cellDuplicate != null)
            {
                cell.CellPool.AddCell(cellDuplicate);
                listCells.AppendItem(cellDuplicate);
                SelectedPool = cell.CellPool;
                SelectedCell = cell;
                LoadProjections();
                ModelIsUpdated();
            }
        }
        private void listCells_ItemDelete(object sender, EventArgs e)
        {
            if (listCells.SelectedIndex >= 0)
            {
                string msg = "Deleting a cell will remove all of its connections and applied stimuli as well. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Cell cell = (Cell)listCells.SelectedItem;
                    cell.ClearLinks();
                    cell.CellPool.RemoveCell(cell);
                    listCells.RemoveItemAt(listCells.SelectedIndex);
                    SelectedCell = null;
                    LoadProjections();
                    LoadStimuli();
                    ModelIsUpdated();
                }
            }
        }
        private void listCells_ItemView(object sender, EventArgs e)
        {
            if (listCells.SelectedItem == null)
                return;
            if (listCells.SelectedItem is not Cell cell) return;
            int oldSeq = cell.Sequence;
            cell = OpenCellDialog(cell); //check modeltemplate's list
            while (cell != null && oldSeq != cell.Sequence && cell.CellPool.Cells.Any(c => c.ID == cell.ID && c != cell))
            {
                MessageBox.Show("Cell sequence has to be unique for a cell pool and somite. Please enter a different sequence.");
                cell = OpenCellDialog(cell);
            }            
            if (cell != null)
            {
                ModelIsUpdated();
                int ind = listCells.SelectedIndex;
                listCells.RefreshItem(ind, cell);
            }
        } 


        private void listCells_ItemPlot(object sender, EventArgs e)
        {
            PlotRequestArgs args = new() { unitToPlot = SelectedCell };
            PlotRequested?.Invoke(this, args);
        }

        private void listCells_ItemsSort(object sender, EventArgs e)
        {
            if (SelectedPool == null) return;
            SelectedPool.Cells.Sort();
            LoadCells();
        }
        private void listCells_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }

        #endregion

        #region Connection
        private void LoadProjections()
        {
            SelectedJunction = null;
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
            if (string.IsNullOrEmpty(cellPoolID))
            {
                LoadProjections();
                return;
            }
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
        private List<JunctionBase> OpenConnectionDialog(JunctionBase interpool, bool newJunc)
        {
            if (Model == null) return null;
            if (CurrentMode == RunMode.Template)
            {
                ControlContainer frmControl = new();
                InterPoolControl ipControl = new(Model.ModelDimensions.NumberOfSomites > 0, Model.Settings);
                ModelTemplate modelTemplate = Model as ModelTemplate;
                InterPoolTemplate interPoolTemplate = interpool as InterPoolTemplate;
                ipControl.SetInterPoolTemplate(modelTemplate.CellPoolTemplates, interPoolTemplate, modelTemplate);
                frmControl.AddControl(ipControl, ipControl.CheckValues);
                frmControl.Text = interpool?.ToString() ?? "New Connection";

                if (frmControl.ShowDialog() == DialogResult.OK)
                {
                    interPoolTemplate = ipControl.GetInterPoolTemplate();
                    modelTemplate.LinkObjects(interPoolTemplate);
                    return new List<JunctionBase> { interPoolTemplate };
                }
            }
            else
            {
                ControlContainer frmControl = new();
                JunctionControl jncControl = new(Model as RunningModel); 
                jncControl.SetJunction(interpool, newJunc);
                frmControl.AddControl(jncControl, jncControl.CheckValues);
                frmControl.Text = interpool?.ToString() ?? "New Connection";

                if (frmControl.ShowDialog() == DialogResult.OK)
                {
                    return jncControl.GetJunctions();
                }                
            }
            return null;
        }

        private void listConnections_ItemAdd(object sender, EventArgs e)
        {
            List<JunctionBase> jncs = OpenConnectionDialog(null, true);
            if (jncs != null && jncs.Any())
            {
                foreach (JunctionBase jb in jncs)
                {
                    if (jb is InterPoolTemplate template)
                        Model.AddJunction(template);
                    else
                        jb.LinkObjects();
                    listConnections.AppendItem(jb);
                }
                ModelIsUpdated();
            }
        }
        private void listConnections_ItemCopy(object sender, EventArgs e)
        {
            if (Model == null) return;
            if (listConnections.SelectedItem == null)
                return;
            JunctionBase jnc = null;
            if (listConnections.SelectedItem is GapJunction gapJunction)
                jnc = new GapJunction(gapJunction);
            else if (listConnections.SelectedItem is ChemicalSynapse synapse)
                jnc = new ChemicalSynapse(synapse);
            List<JunctionBase> jncs = OpenConnectionDialog(jnc, true);
            if (jncs != null && jncs.Any())
            {
                foreach (JunctionBase jb in jncs)
                {
                    if (jb is InterPoolTemplate template)
                        Model.AddJunction(template);
                    else
                        jb.LinkObjects();
                    listConnections.AppendItem(jb);
                }
                ModelIsUpdated();
            }
        }
        private void listConnections_ItemDelete(object sender, EventArgs e)
        {
            if (listConnections.SelectedIndex >= 0)
            {
                if (listConnections.SelectedItem is InterPoolTemplate ipt)
                    Model.RemoveJunction(ipt);
                else if (listConnections.SelectedItem is JunctionBase jb)
                    jb.UnlinkObjects();

                listConnections.RemoveItemAt(listConnections.SelectedIndex);
                ModelIsUpdated();
            }
        }
        private void listConnections_ItemView(object sender, EventArgs e)
        {
            if (listConnections.SelectedItem == null)
                return;
            JunctionBase jnc = listConnections.SelectedItem as JunctionBase;
            List<JunctionBase> jncs = OpenConnectionDialog(jnc, false);
            if (jncs != null && jncs.Any())
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

        private void listConnections_ItemPlot(object sender, EventArgs e)
        {
            PlotRequestArgs args = new() { unitToPlot = SelectedJunction };
            PlotRequested?.Invoke(this, args);
        }

        private void listConnections_ItemSelect(object sender, EventArgs e)
        {
            if (sender is JunctionBase jnc)
            {
                SelectedJunction = jnc;
            }
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
            SelectedStimulus = null;
            if (Model == null) return;
            foreach (StimulusBase stim in Model.GetStimuli())
            {
                listStimuli.AppendItem(stim);
            }
        }

        private void LoadStimuli(string cellPoolID)
        {
            if (string.IsNullOrEmpty(cellPoolID))
            {
                LoadStimuli();
                return;
            }
            lStimuliTitle.Text = $"Stimuli of {cellPoolID}";
            listStimuli.ClearItems();
            if (Model == null || Model is not ModelTemplate) return;
            IEnumerable<StimulusTemplate> list = Model.GetStimuli().Cast<StimulusTemplate>().Where(st => st.TargetPool == cellPoolID);
            foreach (StimulusTemplate stim in list)
                listStimuli.AppendItem(stim);
        }
        private void LoadStimuli(CellPool cp)
        {
            if (cp == null)
            {
                LoadStimuli();
                return;
            }
            lStimuliTitle.Text = $"Stimuli of {cp.ID}";
            listStimuli.ClearItems();
            foreach (Stimulus stim in cp.GetStimuli().Cast<Stimulus>())
                listStimuli.AppendItem(stim);
        }
        private void LoadStimuli(Cell cell)
        {
            if (cell == null)
            {
                if (SelectedPool != null)
                    LoadStimuli(SelectedPool);
                else
                    LoadStimuli();
                return;
            }
            lStimuliTitle.Text = $"Stimuli of {cell.ID}";
            listStimuli.ClearItems();
            foreach (Stimulus stim in cell.Stimuli.ListOfStimulus)
            {
                listStimuli.AppendItem(stim);
            }
        }

        private StimulusBase OpenStimulusDialog(StimulusBase stim)
        {
            if (Model == null) return null;
            ControlContainer frmControl = new();
            if (CurrentMode == RunMode.Template || stim==null)
            {
                StimulusTemplateControl stimControl = new();
                stimControl.SetStimulus((Model as ModelTemplate).CellPoolTemplates, stim as StimulusTemplate);
                frmControl.AddControl(stimControl, stimControl.CheckValues);

                frmControl.Text = stim?.ToString() ?? "New Stimulus";

                if (frmControl.ShowDialog() == DialogResult.OK)
                    return stimControl.GetStimulus();
            }
            else
            {
                StimulusControl stimControl = new();
                stimControl.SetStimulus(stim as Stimulus);
                frmControl.AddControl(stimControl, stimControl.CheckValues);
                frmControl.Text = stim?.ToString() ?? "New Stimulus";
                if (frmControl.ShowDialog() == DialogResult.OK)
                    return stimControl.GetStimulus();
            }
            return null;
        }
        private void listStimuli_ItemSelect(object sender, EventArgs e)
        {
            if (sender is StimulusBase stim)
            {
                SelectedStimulus = stim;
            }
        }

        private void listStimuli_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
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
            StimulusBase stimulus = (StimulusBase)(listStimuli.SelectedItem as StimulusBase).CreateCopy();
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
            StimulusBase stimulus = listStimuli.SelectedItem as StimulusBase;
            StimulusBase stimulus2 = OpenStimulusDialog(stimulus); //check modeltemplate's list
            if (stimulus2 != null)
            {
                int ind = listStimuli.SelectedIndex;
                Model.UpdateStimulus(stimulus, stimulus2);
                listStimuli.RefreshItem(ind, stimulus2);
                ModelIsUpdated();
            }
        }
        private void listStimuli_ItemsSort(object sender, EventArgs e)
        {
            if (SelectedCell != null)
            {
                SelectedCell.SortStimuli();
                LoadStimuli(SelectedCell);
            }
            else if (SelectedPool != null)
            {
                foreach (Cell c in SelectedPool.Cells)
                    c.SortStimuli();
                LoadStimuli(SelectedPool);
            }
            else if (Model is ModelTemplate mt)
            {
                mt.AppliedStimuli.Sort();
                LoadStimuli(SelectedPoolTemplate?.CellGroup);
            }

        }
        private void listStimuli_ItemsExport(object sender, EventArgs e)
        {
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (ModelFile.SaveStimulusToCSV(saveFileCSV.FileName, Model))
                    MessageBox.Show($"The stimulus information is exported to {saveFileCSV.FileName}.");
                else
                    MessageBox.Show("There is a problem with saving the csv file. Please check whether you have writing access to the folder selected");
            }
        }

        private void listStimuli_ItemsImport(object sender, EventArgs e)
        {
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string selection = SelectedCell != null ? SelectedCell.ID :
                    SelectedPool != null ? SelectedPool.ID :
                    SelectedPoolTemplate != null ? SelectedPoolTemplate.ID :
                    "the model";
                string msg = $"Do you want to remove all existing stimuli of {selection} and replace it with the data in {openFileCSV.FileName}";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    string filename = openFileCSV.FileName;
                    if (SelectedCell != null)
                    {
                        //TODO import stimuli
                        LoadStimuli(SelectedCell);
                    }
                    else if (SelectedPoolTemplate != null)
                    {
                        //TODO import stimuli
                    }
                    else if (SelectedPool != null)
                    {
                        //TODO import stimuli
                    }
                    else 
                    {
                        ModelFile.ReadStimulusFromCSV(filename, Model);
                        LoadStimuli();
                    }
                }
            }
        }

        private void listStimuli_ItemPlot(object sender, EventArgs e)
        {
            PlotRequestArgs args = new() { unitToPlot = SelectedStimulus };
            PlotRequested?.Invoke(this, args);
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

        private void linkOpenJsonViewer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string modelName = Model.ModelName ?? "JsonView";
            string filename = $"{modelName}_{DateTime.Now:yyMMdd-HHmm}.json";
            string json = eModelJSON.Text;
            if (string.IsNullOrEmpty(json))
                json = JsonUtil.ToJson(Model);
            string fullPath = FileUtil.SaveToTempFolder(filename, json);
            Process p = new()
            {
                StartInfo = new ProcessStartInfo(fullPath)
                {
                    UseShellExecute = true
                }
            };
            p.Start();
        }

    }
}
