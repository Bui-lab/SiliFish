using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using System.Data;
using SiliFish.Services;
using SiliFish.ModelUnits.Stim;
using SiliFish.Repositories;
using SiliFish.ModelUnits.Junction;
using System.Text.Json;
using SiliFish.UI.Definitions;
using SiliFish.Definitions;
using SiliFish.UI.Extensions;
using System.Diagnostics;
using SiliFish.UI.EventArguments;
using SiliFish.UI.Controls.Model_Edit;
using SiliFish.UI.Controls.General;
using SiliFish.UI.Services;

namespace SiliFish.UI.Controls
{
    public partial class ModelControl : UserControl
    {
        public event EventHandler ModelChanged;
        public event EventHandler PlotRequested;
        public event EventHandler HighlightRequested;
        RunMode CurrentMode = RunMode.Template;

        private ModelBase Model;

        private bool warnedImport = false;
        private bool modelUpdated = false;
        public bool ModelUpdated
        {
            get => modelUpdated;
            set => modelUpdated = value;
        }

        private CellPoolTemplate SelectedPoolTemplate;
        private CellPool SelectedPool;
        private Cell SelectedCell; //valid if Mode == Mode.RunningModel
        private char modeIOG;//I: incoming, O; outgoing; G: gap
        private JunctionBase SelectedJunction;
        private StimulusBase SelectedStimulus;
        private ModelUnitBase SelectedUnit => (ModelUnitBase)SelectedCell ?? SelectedPool ?? SelectedPoolTemplate;


        public ModelControl()
        {
            InitializeComponent();

            listStimuli.EnableImportExport();
            listStimuli.AddContextMenu("Stimulus Setter/Multiplier", listStimuli_ValueSetter);

            listCellPools.EnableImportExport();
            listCellPools.EnableHightlight();
            listCellPools.AddContextMenu("Projection Conductance Multiplier", listCellPools_ConductanceMultiplier);

            listCells.EnableImportExport();
            listCells.EnableHightlight();

            listIncoming.EnableImportExport();
            listIncoming.AddContextMenu("Go to Source", listConnections_GotoSource);
            listIncoming.AddContextMenu("Projection Conductance Multiplier", listConnections_ConductanceMultiplier);
            listOutgoing.EnableImportExport();
            listOutgoing.AddContextMenu("Go to Target", listConnections_GotoTarget);
            listOutgoing.AddContextMenu("Projection Conductance Multiplier", listConnections_ConductanceMultiplier);
            listGap.EnableImportExport();
            listGap.AddContextMenu("Go to Neighbor", listConnections_GotoNeighbor);
            listGap.AddContextMenu("Projection Conductance Multiplier", listConnections_ConductanceMultiplier);

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
            List<TabPage> obsoloteTabpages = [];
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

            List<string> paramGroups = ParamDict?.Keys.Where(k => k.IndexOf('.') > 0).Select(k => k[..k.IndexOf('.')]).Distinct().ToList() ?? [];
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
                ddDefaultNeuronCore.Items.AddRange(CellCore.GetCoreTypes().ToArray());
            ddDefaultNeuronCore.Text = Model?.Settings.DefaultNeuronCore;
            if (ddDefaultMuscleCellCore.Items.Count == 0)
                ddDefaultMuscleCellCore.Items.AddRange(CellCore.GetCoreTypes().ToArray());
            ddDefaultMuscleCellCore.Text = Model?.Settings.DefaultMuscleCellCore;
            propSettings.SelectedObject = Model?.Settings;
            propKinematics.SelectedObject = Model?.KinemParam;
            propDynamics.SelectedObject = Model?.DynamicsParam;
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
            Dictionary<string, object> ParamDict = [];
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
            splitCellPoolsAndCells.Panel2Collapsed = CurrentMode == RunMode.Template;
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
        private void listGeneric_ItemPlot(object sender, EventArgs e)
        {
            List<ModelUnitBase> units = (List<ModelUnitBase>)sender;
            SelectedUnitArgs args = new();
            args.unitsSelected.AddRange(units);
            PlotRequested?.Invoke(this, args);
        }

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
            if (Model == null) return;
            listCells.ClearItems();
            listCellPools.LoadItems(Model.GetCellPools().Cast<object>().ToList());
            SelectedPoolTemplate = null;
            SelectedPool = null;
            SelectedCell = null;
        }
        private CellPoolTemplate OpenCellPoolDialog(CellPoolTemplate pool)
        {
            if (Model == null) return null;
            ControlContainer frmControl = new(ParentForm.Location);
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
            ListBoxControl lb = sender as ListBoxControl;
            if (lb.SelectedItem is CellPool pool)
            {
                SelectedPool = pool;
                if (listCellPools.HighlightSelected)
                    listCellPools_ItemHighlight(sender, e);
                SelectedCell = null;
                LoadCells();
                LoadProjections(pool);
                LoadStimuli(pool);
            }
            else if (lb.SelectedItem is CellPoolTemplate cpt)
            {
                SelectedPoolTemplate = cpt;
                LoadProjections(cpt.CellGroup);
                LoadStimuli(cpt);
            }
            else if (lb.SelectedItem == null)
            {
                SelectedPoolTemplate = null;
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
                MessageBox.Show("Cell pool group names have to be unique. Please enter a different name.", "Warning");
                poolDuplicate = OpenCellPoolDialog(poolDuplicate);
            }
            if (poolDuplicate != null)
            {
                AddCellPool(poolDuplicate);
                RefreshProjections();
                ModelIsUpdated();
            }
        }
        private void listCellPools_ItemDelete(object sender, EventArgs e)
        {
            if (listCellPools.SelectedIndices.Count != 0)
            {
                string msg = "Deleting a cell pool will remove all of its connections and applied stimuli as well. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (var item in listCellPools.SelectedItems)
                    {
                        CellPoolTemplate cpl = (CellPoolTemplate)item;
                        Model.RemoveCellPool(cpl);
                    }
                    LoadPools();
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
                    RefreshProjections();
                }
            }
        }
        private void listCellPools_ItemHighlight(object sender, EventArgs e)
        {
            SelectedUnitArgs args = new();
            args.unitsSelected.Add(SelectedPool);
            HighlightRequested?.Invoke(this, args);
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

        private void listCellPools_ItemsExport(object sender, EventArgs e)
        {
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (ModelFile.SaveCellPoolsToCSV(saveFileCSV.FileName, Model))
                {
                    FileUtil.ShowFile(saveFileCSV.FileName);
                }
                else
                    MessageBox.Show("There is a problem with saving the csv file. Please make sure the file is not open.", "Error");
            }
        }

        private void listCellPools_ItemsImport(object sender, EventArgs e)
        {
            if (!warnedImport &&
                (Model is ModelTemplate modelTemplate && modelTemplate.CellPoolTemplates.Count != 0 ||
                Model is RunningModel runningModel && runningModel.CellPools.Count != 0))
            {
                warnedImport = true;
                string msg = $"Importing will remove all cell pools and create from the CSV file. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            }
            openFileCSV.Title = "Cell Pool Import";
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileCSV.FileName;
                if (ModelFile.ReadCellPoolsFromCSV(filename, Model))
                {
                    LoadPools();
                    MessageBox.Show($"The cell pools are imported.", "Information");
                }
                else
                    MessageBox.Show($"The import was unsuccesful. Make sure {filename} is a valid export file and not currently used by any other software.", "Error");
            }
        }
        private void listCellPools_ConductanceMultiplier(object sender, EventArgs e)
        {
            if (listCellPools.SelectedItem is not CellPoolTemplate) return;
            ControlContainer controlContainer = new()
            {
                Text = "Conductance Multiplier"
            };
            List<string> changes = [];

            ConductanceMultiplier multiplier = new();
            controlContainer.AddControl(multiplier, null);
            if (controlContainer.ShowDialog() == DialogResult.OK)
            {
                double mult = multiplier.GapMultiplier;
                if (mult != 1)
                {
                    foreach (var cp in listCellPools.SelectedItems)
                    {
                        if (cp is not CellPoolTemplate poolTemplate) continue;
                        if (mult == 0)
                        {
                            Model.GetGapProjections()
                                .Where(c => (c.SourcePool == poolTemplate.CellGroup || c.TargetPool == poolTemplate.CellGroup) && c.Active)
                                .ToList()
                                .ForEach(c => changes.Add($"{c.ID} deactivated"));
                            Model.GetGapProjections()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup || c.TargetPool == poolTemplate.CellGroup)
                                .ToList()
                                .ForEach(c => c.Active = false);
                        }
                        else
                        {
                            int count = Model.GetGapProjections()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup || c.TargetPool == poolTemplate.CellGroup).Count();
                            Model.GetGapProjections()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup || c.TargetPool == poolTemplate.CellGroup)
                                .ToList()
                                .ForEach(c => (c as InterPoolTemplate).Parameters["Conductance"].Multiply(mult));
                            changes.Add($"Conductance value of {count} gap junctions of {poolTemplate.CellGroup} multiplied by {mult}");
                        }
                    }
                }
                mult = multiplier.ChemMultiplier;
                if (mult != 1)
                {
                    foreach (var cp in listCellPools.SelectedItems)
                    {
                        if (cp is not CellPoolTemplate poolTemplate) continue;
                        if (mult == 0)
                        {
                            Model.GetChemicalProjections()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup && c.Active)
                                .ToList()
                                .ForEach(c => changes.Add($"{c.ID} deactivated"));
                            Model.GetChemicalProjections()
                                 .Where(c => c.SourcePool == poolTemplate.CellGroup)
                                 .ToList()
                                 .ForEach(c => c.Active = false);
                        }
                        else
                        {
                            int count = Model.GetChemicalProjections()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup).Count();
                            Model.GetChemicalProjections()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup)
                                .ToList()
                                .ForEach(c => (c as InterPoolTemplate).Parameters["Conductance"].Multiply(mult));
                            changes.Add($"Conductance value of {count} chem junctions originating from {poolTemplate.CellGroup} multiplied by {mult}");
                        }
                    }
                }

                TextDisplayer.Display($"Modified projection conductance values", changes);
                RefreshProjections();
                ModelIsUpdated();
            }
        }

        private void LoadCells()
        {
            if (Model is ModelTemplate) return;
            lCellsTitle.Text = SelectedPool != null ? $"Cells of {SelectedPool.ID}" : "Cells";

            List<Cell> Cells = (List<Cell>)(SelectedPool?.GetCells() ?? (Model as RunningModel).GetCells());
            if (SelectedPool == null && Cells.Count > GlobalSettings.MaxNumberOfUnits)
            {
                listCells.ClearItems();
                listCells.AppendItem("Please select a cell pool to list cells under...");
            }
            else
            {
                listCells.LoadItems(Cells.Cast<object>().ToList());
            }
            SelectedCell = null;
        }

        private Cell OpenCellDialog(Cell cell)
        {
            if (Model == null) return null;
            if (CurrentMode == RunMode.Template) return null;
            ControlContainer frmControl = new(ParentForm.Location);
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
            ListBoxControl lb = sender as ListBoxControl;
            if (lb.SelectedItem is Cell cell)
            {
                SelectedCell = cell;
                if (listCells.HighlightSelected)
                    listCells_ItemHighlight(sender, e);
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
                MessageBox.Show("Cell sequence has to be unique for a cell pool and somite. Please enter a different sequence.", "Warning");
                cellDuplicate = OpenCellDialog(cellDuplicate);
            }
            if (cellDuplicate != null)
            {
                cell.CellPool.AddCell(cellDuplicate);
                listCells.AppendItem(cellDuplicate);
                SelectedPool = cell.CellPool;
                SelectedCell = cell;
                RefreshProjections();
                ModelIsUpdated();
            }
        }
        private void listCells_ItemDelete(object sender, EventArgs e)
        {
            if (listCells.SelectedIndices.Count != 0)
            {
                string msg = "Deleting a cell will remove all of its connections and applied stimuli as well. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (var item in listCells.SelectedItems)
                    {
                        Cell cell = (Cell)item;
                        cell.ClearLinks();
                        cell.CellPool.RemoveCell(cell);
                    }
                    SelectedCell = null;
                    LoadCells();
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
                MessageBox.Show("Cell sequence has to be unique for a cell pool and somite. Please enter a different sequence.", "Warning");
                cell = OpenCellDialog(cell);
            }
            if (cell != null)
            {
                ModelIsUpdated();
                int ind = listCells.SelectedIndex;
                listCells.RefreshItem(ind, cell);
            }
        }

        private void listCells_ItemHighlight(object sender, EventArgs e)
        {
            SelectedUnitArgs args = new();
            args.unitsSelected.Add(SelectedCell);
            HighlightRequested?.Invoke(this, args);
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
        private bool SelectedUnitHasCell()
        {
            if (SelectedPool != null && SelectedPool.HasCells()
                || SelectedPool == null && (Model as RunningModel).HasCells())
                return true;
            return false;
        }
        private void listCells_ItemsExport(object sender, EventArgs e)
        {
            if (Model is RunningModel runningModel)
            {
                if (saveFileCSV.ShowDialog() == DialogResult.OK)
                {
                    if (ModelFile.SaveCellsToCSV(saveFileCSV.FileName, runningModel, SelectedPool))
                    {
                        FileUtil.ShowFile(saveFileCSV.FileName);
                    }
                    else
                        MessageBox.Show("There is a problem with saving the csv file. Please make sure the file is not open.", "Error");
                }
            }
        }

        private void listCells_ItemsImport(object sender, EventArgs e)
        {
            string unit = SelectedPool != null ? " of " + SelectedPool.ID : "";
            if (!warnedImport && SelectedUnitHasCell())
            {
                warnedImport = true;
                string msg = $"Importing will remove all existing cells{unit}. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            }
            openFileCSV.Title = $"Cell import {unit}";
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileCSV.FileName;
                if (ModelFile.ReadCellsFromCSV(filename, Model as RunningModel, SelectedPool))
                {
                    LoadCells();
                    string units = SelectedPool != null ? " of " + SelectedPool.ID : "";
                    MessageBox.Show($"The cells{units} are imported.", "Information");
                }
                else
                    MessageBox.Show($"The import was unsuccesful. Make sure {filename} is a valid export file and not currently used by any other software.", "Error");
            }
        }



        #endregion

        #region Connection
        private void ClearProjections()
        {
            listIncoming.ClearItems();
            listOutgoing.ClearItems();
            listGap.ClearItems();
        }

        private void RefreshProjections()
        {
            if (SelectedCell != null)
                LoadProjections(SelectedCell);
            else if (SelectedPool != null)
                LoadProjections(SelectedPool);
            else if (SelectedPoolTemplate != null)
                LoadProjections(SelectedPoolTemplate.CellGroup);
            else
                LoadProjections();
            if (SelectedJunction != null)
            {
                switch (modeIOG)
                {
                    case 'I':
                        listIncoming.SelectedItem = SelectedJunction;
                        break;
                    case 'O':
                        listOutgoing.SelectedItem = SelectedJunction;
                        break;
                    case 'G':
                        listGap.SelectedItem = SelectedJunction;
                        break;
                    default:
                        break;
                }
            }
        }
        private void LoadProjections()
        {
            if (Model == null) return;
            ClearProjections();
            lOutgoingTitle.Text = "Synaptic Connections";
            lGapTitle.Text = "Gap Junctions";
            splitChemicalJunctions.Panel1Collapsed = true;
            if (Model is RunningModel rm)
            {
                List<InterPool> chemInterPools = [.. rm.ChemPoolConnections];
                if (chemInterPools.Count > GlobalSettings.MaxNumberOfUnits)
                    listOutgoing.AppendItem("Please select a cell pool to list junctions under...");
                else
                    listOutgoing.LoadItems(chemInterPools.Cast<object>().ToList());
                List<InterPool> gapInterPools = [.. rm.GapPoolConnections];
                if (gapInterPools.Count > GlobalSettings.MaxNumberOfUnits)
                    listGap.AppendItem("Please select a cell pool to list junctions under...");
                else
                    listGap.LoadItems(gapInterPools.Cast<object>().ToList());
            }
            else if (Model is ModelTemplate mt)
            {
                List<InterPoolTemplate> chemInterPools = mt.InterPoolTemplates.Where(jnc => jnc.ConnectionType != ConnectionType.Gap).ToList();
                if (chemInterPools.Count > GlobalSettings.MaxNumberOfUnits)
                    listOutgoing.AppendItem("Please select a cell pool to list junctions under...");
                else
                    listOutgoing.LoadItems(chemInterPools.Cast<object>().ToList());
                List<InterPoolTemplate> gapInterPools = mt.InterPoolTemplates.Where(jnc => jnc.ConnectionType == ConnectionType.Gap).ToList();
                if (gapInterPools.Count > GlobalSettings.MaxNumberOfUnits)
                    listGap.AppendItem("Please select a cell pool to list junctions under...");
                else
                    listGap.LoadItems(gapInterPools.Cast<object>().ToList());
            }
        }

        private void LoadProjections(string cellPoolID)
        {
            if (string.IsNullOrEmpty(cellPoolID))
            {
                LoadProjections();
                return;
            }
            ClearProjections();
            lIncomingTitle.Text = $"Incoming Connections of {cellPoolID}";
            lOutgoingTitle.Text = $"Outgoing Connections of {cellPoolID}";
            lGapTitle.Text = $"Gap Junctions of {cellPoolID}";
            splitChemicalJunctions.Panel1Collapsed = false;
            if (Model == null || Model is not ModelTemplate) return;
            listOutgoing.LoadItems(Model.GetChemicalProjections()
                .Where(proj => ((InterPoolTemplate)proj).SourcePool == cellPoolID)
                .Cast<object>()
                .ToList());
            listIncoming.LoadItems(Model.GetChemicalProjections()
                .Where(proj => ((InterPoolTemplate)proj).TargetPool == cellPoolID)
                .Cast<object>()
                .ToList());
            listGap.LoadItems(Model.GetGapProjections()
                .Where(proj => ((InterPoolTemplate)proj).SourcePool == cellPoolID || ((InterPoolTemplate)proj).TargetPool == cellPoolID)
                .Cast<object>()
                .ToList());
        }
        private void LoadProjections(CellPool cp)
        {
            if (cp == null)
            {
                LoadProjections();
                return;
            }
            lIncomingTitle.Text = $"Incoming Connections of {cp.ID}";
            lOutgoingTitle.Text = $"Outgoing Connections of {cp.ID}";
            lGapTitle.Text = $"Gap Junctions of {cp.ID}";
            splitChemicalJunctions.Panel1Collapsed = false;
            ClearProjections();
            List<JunctionBase> gapJunctions = cp.Projections.Where(j => j is GapJunction).ToList();
            List<InterPool> gapInterPools = (Model as RunningModel).GapPoolConnections
                .Where(ip => ip.SourcePool == cp.ID || ip.TargetPool == cp.ID).ToList();
            listGap.LoadItems(gapInterPools.Cast<object>().ToList());

            List<JunctionBase> chemJunctions = cp.Projections.Where(j => j is not GapJunction).ToList();
            List<InterPool> chemInterPools = (Model as RunningModel).ChemPoolConnections
                .Where(ip => ip.SourcePool == cp.ID || ip.TargetPool == cp.ID).ToList();
            listOutgoing.LoadItems(chemInterPools.Where(ip => ip.SourcePool == cp.ID).Cast<object>().ToList());
            listIncoming.LoadItems(chemInterPools.Where(ip => ip.TargetPool == cp.ID).Cast<object>().ToList());
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
            ClearProjections();
            lIncomingTitle.Text = $"Incoming Connections of {cell.ID}";
            lOutgoingTitle.Text = $"Outgoing Connections of {cell.ID}";
            lGapTitle.Text = $"Gap Junctions of {cell.ID}";
            listGap.LoadItems(cell.GapJunctions.Cast<object>().ToList());

            if (cell is Neuron neuron)
            {
                listIncoming.LoadItems(neuron.Synapses.Cast<object>().ToList());
                listOutgoing.LoadItems(neuron.Terminals.Cast<object>().ToList());
            }
            if (cell is MuscleCell muscleCell)
            {
                listIncoming.LoadItems(muscleCell.EndPlates.Cast<object>().ToList());
            }
        }
        private List<InterPoolBase> OpenConnectionDialog(InterPoolBase interpool, bool newJunc,
            bool setSource = false, bool setTarget = false, bool setGap = false)
        {
            if (Model == null) return null;
            if (CurrentMode == RunMode.Template)
            {
                ControlContainer frmControl = new(ParentForm.Location);
                InterPoolControl ipControl = new(Model.Settings);
                ModelTemplate modelTemplate = Model as ModelTemplate;
                InterPoolTemplate interPoolTemplate = interpool as InterPoolTemplate;
                ipControl.WriteDataToControl(modelTemplate.CellPoolTemplates, interPoolTemplate);
                if (setSource && SelectedPoolTemplate != null)
                    ipControl.SetSourcePool(SelectedPoolTemplate);
                if (setTarget && SelectedPoolTemplate != null)
                    ipControl.SetTargetPool(SelectedPoolTemplate);
                if (setGap)
                    ipControl.SetAsGapJunction();
                frmControl.AddControl(ipControl, ipControl.CheckValues);
                frmControl.Text = interpool?.ToString() ?? "New Connection";

                if (frmControl.ShowDialog() == DialogResult.OK)
                {
                    interPoolTemplate = ipControl.ReadDataFromControl();
                    modelTemplate.LinkObjects(interPoolTemplate);
                    return [interPoolTemplate];
                }
            }
            else
            {
                ControlContainer frmControl = new(ParentForm.Location);
                JunctionControl jncControl = new(Model as RunningModel);
                jncControl.SetJunction(interpool as JunctionBase, newJunc);
                if (setSource)
                {
                    if (SelectedCell != null)
                        jncControl.SetSourceCell(SelectedCell);
                    else if (SelectedPool != null)
                        jncControl.SetSourcePool(SelectedPool);
                }
                if (setTarget)
                {
                    if (SelectedCell != null)
                        jncControl.SetTargetCell(SelectedCell);
                    else if (SelectedPool != null)
                        jncControl.SetTargetPool(SelectedPool);
                }
                if (setGap)
                    jncControl.SetAsGapJunction();
                frmControl.AddControl(jncControl, jncControl.CheckValues);
                frmControl.Text = interpool?.ToString() ?? "New Connection";

                if (frmControl.ShowDialog() == DialogResult.OK)
                {
                    return jncControl.GetJunctions().Cast<InterPoolBase>().ToList();
                }
            }
            return null;
        }

        private void listConnections_ItemAdd(object sender, EventArgs e)
        {
            ListBoxControl lbc = sender as ListBoxControl;
            List<InterPoolBase> jncs =
                lbc == listIncoming ? OpenConnectionDialog(null, true, setTarget: true) :
                lbc == listOutgoing ? OpenConnectionDialog(null, true, setSource: true) :
                lbc == listGap ? OpenConnectionDialog(null, true, setGap: true) :
                OpenConnectionDialog(null, true);

            if (jncs != null && jncs.Count != 0)
            {
                foreach (InterPoolBase jb in jncs)
                {
                    if (jb is InterPoolTemplate template)
                        Model.AddJunction(template);
                    else
                        jb.LinkObjects();
                    RefreshProjections();
                }
                ModelIsUpdated();
            }
        }
        private void listConnections_ItemCopy(object sender, EventArgs e)
        {
            if (Model == null) return;
            InterPoolBase jnc = null;
            if (sender is GapJunction gapJunction)
                jnc = new GapJunction(gapJunction);
            else if (sender is ChemicalSynapse synapse)
                jnc = new ChemicalSynapse(synapse);
            else if (sender is InterPoolTemplate ipt)
                jnc = new InterPoolTemplate(ipt);
            List<InterPoolBase> jncs = OpenConnectionDialog(jnc, true);
            if (jncs != null && jncs.Count != 0)
            {
                foreach (InterPoolBase jb in jncs)
                {
                    if (jb is InterPoolTemplate template)
                        Model.AddJunction(template);
                    else
                        jb.LinkObjects();
                }
                RefreshProjections();
                ModelIsUpdated();
            }
        }
        private void listConnections_ItemDelete(object sender, EventArgs e)
        {
            ListBoxControl lbc = sender as ListBoxControl;
            if (lbc.SelectedIndices.Count != 0)
            {
                foreach (var item in lbc.SelectedItems)
                    if (item is InterPoolTemplate ipt)
                        Model.RemoveJunction(ipt);
                    else if (item is JunctionBase jb)
                        jb.UnlinkObjects();
                RefreshProjections();
                ModelIsUpdated();
            }
        }
        private void listConnections_ItemView(object sender, EventArgs e)
        {
            if (sender is not InterPoolBase jnc) return;
            List<InterPoolBase> jncs = OpenConnectionDialog(jnc, false);
            if (jncs != null && jncs.Count != 0)
            {
                RefreshProjections();
                ModelIsUpdated();
            }
        }

        private void listConnections_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }

        private void listConnections_ItemSelect(object sender, EventArgs e)
        {
            ListBoxControl lb = sender as ListBoxControl;
            if (lb.SelectedItem is JunctionBase jnc)
            {
                modeIOG = lb == listOutgoing ? 'O' : lb == listIncoming ? 'I' : 'G';
                SelectedJunction = jnc;
            }
        }
        private void listConnections_ItemsSort(object sender, EventArgs e)
        {
            if (SelectedCell != null)
            {
                SelectedCell.SortJunctions();
                LoadProjections(SelectedCell);
            }
            else if (CurrentMode == RunMode.Template)
            {
                Model.SortJunctions();
                RefreshProjections();
            }
        }

        private void listConnections_ItemsExport(object sender, EventArgs e)
        {
            ModelUnitBase unit = SelectedUnit;
            ConnectionSelectionControl selectionControl = new()
            {
                ChemInOutExists = unit != null,
                GapJunctions = sender == listGap,
                CheminalIncoming = sender == listIncoming,
                ChemicalOutgoing = sender == listOutgoing
            };
            ControlContainer frmControl = new();
            frmControl.AddControl(selectionControl, null);
            frmControl.Text = "Export";
            selectionControl.Label = "Please select the junctions to be exported.";
            if (frmControl.ShowDialog() != DialogResult.OK) return;

            bool gap = selectionControl.GapJunctions;
            bool chemin = selectionControl.CheminalIncoming;
            bool chemout = selectionControl.ChemicalOutgoing;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (ModelFile.SaveConnectionsToCSV(saveFileCSV.FileName, Model, unit, gap, chemin, chemout))
                {
                    FileUtil.ShowFile(saveFileCSV.FileName);
                }
                else
                    MessageBox.Show("There is a problem with saving the csv file. Please make sure the file is not open.", "Error");
            }
        }

        private void listConnections_ItemsImport(object sender, EventArgs e)
        {
            ModelUnitBase unit = SelectedUnit;
            string ofUnit = unit != null ? $"of {unit.ID} " : "full model ";
            if (!warnedImport &&
                (Model is ModelTemplate modelTemplate && modelTemplate.HasConnections() ||
                Model is RunningModel runningModel && runningModel.HasConnections()))
            {
                warnedImport = true;
                string msg = $"Importing will remove all cell pool connections {ofUnit}and create from the CSV file. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            }
            ConnectionSelectionControl selectionControl = new()
            {
                ChemInOutExists = unit != null,
                GapJunctions = sender == listGap,
                CheminalIncoming = sender == listIncoming,
                ChemicalOutgoing = sender == listOutgoing
            };
            ControlContainer frmControl = new();
            frmControl.AddControl(selectionControl, null);
            frmControl.Text = "Import";
            selectionControl.Label = "Please select the junctions to be imported.";
            if (frmControl.ShowDialog() != DialogResult.OK) return;

            bool gap = selectionControl.GapJunctions;
            bool chemin = selectionControl.CheminalIncoming;
            bool chemout = selectionControl.ChemicalOutgoing;

            openFileCSV.Title = $"Connection import ({unit})";
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileCSV.FileName;
                if (ModelFile.ReadConnectionsFromCSV(filename, Model, unit, gap, chemin, chemout))
                {
                    RefreshProjections();
                    string jncList = chemin && chemout ? "chemical" :
                        !chemin && chemout ? "outgoing chemical" :
                        chemin && !chemout ? "incoming chemical" :
                        "";
                    if (gap && !string.IsNullOrEmpty(jncList))
                        jncList = "gap and " + jncList;
                    else if (gap)
                        jncList = "gap";
                    string units = unit != null ? " of " + unit.ID : "";
                    MessageBox.Show($"The {jncList} junctions{units} are imported.", "Information");
                }
                else
                    MessageBox.Show($"The import was unsuccesful. Make sure {filename} is a valid export file and not currently used by any other software.", "Error");
            }
        }

        private void listConnections_GotoSource(object sender, EventArgs e)
        {
            if (Model == null) return;
            object obj = (sender as ListBoxControl).SelectedItem;
            if (obj is ChemicalSynapse synapse)
            {
                listCellPools.SelectedItem = synapse.PreNeuron.CellPool;
                listCells.SelectedItem = synapse.PreNeuron;
            }
            else if (obj is InterPoolTemplate ipt)
            {
                listCellPools.SelectItem(ipt.SourcePool);
            }
            else if (obj is InterPool ip)
            {
                listCellPools.SelectItem(ip.SourcePool);
            }
        }

        private void listConnections_GotoTarget(object sender, EventArgs e)
        {
            if (Model == null) return;
            object obj = (sender as ListBoxControl).SelectedItem;
            if (obj is ChemicalSynapse synapse)
            {
                listCellPools.SelectedItem = synapse.PostCell.CellPool;
                listCells.SelectedItem = synapse.PostCell;
            }
            else if (obj is InterPoolTemplate ipt)
            {
                listCellPools.SelectItem(ipt.TargetPool);
            }
            else if (obj is InterPool ip)
            {
                listCellPools.SelectItem(ip.TargetPool);
            }
        }

        private void listConnections_GotoNeighbor(object sender, EventArgs e)
        {
            if (Model == null) return;
            object obj = (sender as ListBoxControl).SelectedItem;
            if (obj is GapJunction jnc)
            {
                Cell otherCell = SelectedCell == jnc.Cell1 ? jnc.Cell2 : jnc.Cell1;
                listCellPools.SelectedItem = otherCell.CellPool;
                listCells.SelectedItem = otherCell;
            }
            else if (obj is InterPoolTemplate ipt)
            {
                string otherPool = SelectedPoolTemplate.CellGroup == ipt.TargetPool ? ipt.SourcePool : ipt.TargetPool;
                listCellPools.SelectItem(otherPool);
            }
        }

        private void listConnections_ConductanceMultiplier(object sender, EventArgs e)
        {
            ControlContainer controlContainer = new()
            {
                Text = "Conductance Multiplier"
            };
            ListBoxControl lb = sender as ListBoxControl;
            bool gap = lb == listGap;
            ConductanceMultiplier multiplier = new(gap);
            controlContainer.AddControl(multiplier, null);
            List<string> changes = [];
            if (controlContainer.ShowDialog() == DialogResult.OK)
            {
                double mult = gap ? multiplier.GapMultiplier : multiplier.ChemMultiplier;
                if (mult == 1) return;
                foreach (var jnc in lb.SelectedItems)
                {
                    if (jnc is InterPoolTemplate jncTemplate)
                    {
                        if (mult == 0)
                        {
                            jncTemplate.Active = false;
                            changes.Add($"{jncTemplate.ID} deactivated");
                        }
                        else
                        {
                            double prevConductance = jncTemplate.Parameters["Conductance"].UniqueValue;
                            jncTemplate.Parameters["Conductance"].Multiply(mult);
                            double newConductance = jncTemplate.Parameters["Conductance"].UniqueValue;
                            changes.Add($"{jncTemplate.ID} conductance changed from {prevConductance} to {newConductance}");
                        }
                    }
                    else if (jnc is JunctionBase junct)
                    {
                        if (mult == 0)
                        {
                            junct.Active = false;
                            changes.Add($"{junct.ID} deactivated");
                        }
                        else
                        {
                            double prevConductance = junct.Core.Conductance;
                            junct.Core.Conductance *= mult;
                            double newConductance = junct.Core.Conductance;
                            changes.Add($"{junct.ID} conductance changed from {prevConductance} to {newConductance}");
                        }
                    }
                }
                TextDisplayer.Display($"Modified projection conductance values", changes);
                RefreshProjections();
                ModelIsUpdated();
            }
        }
        #endregion

        #region Stimulus

        private void RefreshStimuli()
        {
            if (SelectedCell != null)
                LoadStimuli(SelectedCell);
            else if (SelectedPool != null)
                LoadStimuli(SelectedPool);
            else if (SelectedPoolTemplate != null)
                LoadStimuli(SelectedPoolTemplate);
            else
                LoadStimuli();
            if (SelectedStimulus != null)
            {
                listStimuli.SelectedItem = SelectedStimulus;
            }
        }
        private void LoadStimuli()
        {
            lStimuliTitle.Text = $"Stimuli";
            SelectedStimulus = null;
            if (Model == null) return;
            listStimuli.LoadItems(Model.GetStimuli().Cast<object>().ToList());
        }

        private void LoadStimuli(ModelUnitBase unit)
        {
            if (unit is null)
                LoadStimuli();
            else if (unit is Cell cell)
                LoadStimuli(cell);
            else if (unit is CellPool cellPool)
                LoadStimuli(cellPool);
            else if (unit is CellPoolTemplate poolTemplate)
                LoadStimuli(poolTemplate);
        }


        private void LoadStimuli(CellPoolTemplate cellPoolTemplate)
        {
            if (cellPoolTemplate is null)
            {
                LoadStimuli();
                return;
            }
            if (Model == null || Model is not ModelTemplate) return;
            lStimuliTitle.Text = $"Stimuli of {cellPoolTemplate.CellGroup}";
            listStimuli.LoadItems(Model.GetStimuli().Cast<StimulusTemplate>().Where(st => st.TargetPool == cellPoolTemplate.CellGroup).Cast<object>().ToList());
        }
        private void LoadStimuli(CellPool cp)
        {
            if (cp == null)
            {
                LoadStimuli();
                return;
            }
            lStimuliTitle.Text = $"Stimuli of {cp.ID}";
            listStimuli.LoadItems(cp.GetStimuli().Cast<object>().ToList());
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
            listStimuli.LoadItems(cell.Stimuli.ListOfStimulus.Cast<object>().ToList());
        }

        private StimulusBase OpenStimulusDialog(StimulusBase stim)
        {
            if (Model == null) return null;
            ControlContainer frmControl = new(ParentForm.Location);
            if (CurrentMode == RunMode.Template)
            {
                StimulusTemplateControl stimControl = new();
                stimControl.SetStimulus((Model as ModelTemplate).CellPoolTemplates, stim as StimulusTemplate);
                frmControl.AddControl(stimControl, stimControl.CheckValues);
                if (stim == null && SelectedPoolTemplate != null)
                    stimControl.SetSourcePool(SelectedPoolTemplate);

                frmControl.Text = stim?.ToString() ?? "New Stimulus";

                if (frmControl.ShowDialog() == DialogResult.OK)
                    return stimControl.GetStimulus();
            }
            else
            {
                if (stim == null && SelectedCell == null && SelectedPool == null)
                {
                    MessageBox.Show("Please select the cell pool or cell the stimulus will be applied to.", "Confirmation");
                    return null;
                }
                StimulusControl stimControl = new();
                stimControl.SetStimulus(stim as Stimulus);
                frmControl.AddControl(stimControl, stimControl.CheckValues);
                frmControl.Text = stim?.ToString() ?? "New Stimulus";
                if (stim == null)
                {
                    stimControl.SetTargetCellOrPool(SelectedCell, SelectedPool);
                }
                if (frmControl.ShowDialog() == DialogResult.OK)
                    return stimControl.GetStimulus();
            }
            return null;
        }
        private void listStimuli_ItemSelect(object sender, EventArgs e)
        {
            ListBoxControl lb = sender as ListBoxControl;
            if (lb.SelectedItem is StimulusBase stim)
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
            if (listStimuli.SelectedIndices.Count != 0)
            {
                if (MessageBox.Show("Do you want to delete selected stimuli?", "Confirmation", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
                foreach (object obj in listStimuli.SelectedItems)
                {
                    StimulusBase stim = (StimulusBase)obj;
                    Model.RemoveStimulus(stim);
                }
                LoadStimuli(SelectedUnit);
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
                mt.StimulusTemplates.Sort();
                LoadStimuli(SelectedPoolTemplate);
            }

        }
        private bool SelectedUnitHasStimulus(ModelUnitBase unit)
        {
            if (unit == null)
            {
                if (Model is ModelTemplate mt)
                    return mt.HasStimulus();
                return (Model as RunningModel).HasStimulus();
            }
            if (unit is Cell cell)
                return cell.Stimuli.HasStimulus;
            if (unit is CellPool cellPool)
                return cellPool.GetStimuli().Count != 0;
            if (unit is CellPoolTemplate cellPoolTemplate)
                return (Model as ModelTemplate).StimulusTemplates.FirstOrDefault(stim => stim.TargetPool == cellPoolTemplate.CellGroup) != null;
            return false;
        }
        private void listStimuli_ItemsExport(object sender, EventArgs e)
        {
            ModelUnitBase selectedUnit = SelectedUnit;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (ModelFile.SaveStimulusToCSV(saveFileCSV.FileName, Model, selectedUnit))
                {
                    FileUtil.ShowFile(saveFileCSV.FileName);
                }
                else
                    MessageBox.Show("There is a problem with saving the csv file. Please make sure the file is not open.", "Error");
            }
        }

        private void listStimuli_ItemsImport(object sender, EventArgs e)
        {
            ModelUnitBase selectedUnit = SelectedUnit;
            string unit = selectedUnit?.ID ?? "the model";
            if (selectedUnit is CellPoolTemplate cpt)
                unit = cpt.CellGroup;
            if (!warnedImport && SelectedUnitHasStimulus(SelectedUnit))
            {
                warnedImport = true;
                string msg = $"Importing will remove all existing stimuli of {unit}. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            }
            openFileCSV.Title = $"Stimulus import ({unit})";
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileCSV.FileName;
                if (ModelFile.ReadStimulusFromCSV(filename, Model, selectedUnit))
                {
                    LoadStimuli(selectedUnit);
                    string units = selectedUnit != null ? " of " + selectedUnit.ID : "";
                    MessageBox.Show($"The stimuli{units} are imported.", "Information");
                }
                else
                    MessageBox.Show($"The import was unsuccesful. Make sure {filename} is a valid export file and not currently used by any other software.", "Error");
            }
        }

        private void listStimuli_ValueSetter(object sender, EventArgs e)
        {
            ControlContainer controlContainer = new()
            {
                Text = "Stimulus Setter"
            };
            StimulusMultiplier multiplier = new();
            controlContainer.AddControl(multiplier, null);
            if (controlContainer.ShowDialog() == DialogResult.OK)
            {
                double mult = multiplier.StimMultiplier;
                double value = multiplier.StimValue;

                foreach (var stim in listStimuli.SelectedItems)
                {
                    if (stim is not StimulusTemplate st) continue;
                    if (mult == 0 && value == 0)
                        st.Active = false;
                    else if (mult != 0)
                        st.Settings.Value1 *= mult;
                    else
                        st.Settings.Value1 = value;
                }
                RefreshStimuli();
                ModelIsUpdated();
            }
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
                MessageBox.Show("Updated JSON is loaded.", "Information");
            }
            catch (JsonException exc)
            {
                MessageBox.Show($"There is a problem with the JSON file. Please check the format of the text in a JSON editor. {exc.Message}", "Error");
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
            FileUtil.ShowFile(fullPath);
        }

        private void listGeneric_ListBoxActivated(object sender, EventArgs e)
        {
            listCellPools.BorderStyle = BorderStyle.None;
            listCells.BorderStyle = BorderStyle.None;
            listIncoming.BorderStyle = BorderStyle.None;
            listOutgoing.BorderStyle = BorderStyle.None;
            listGap.BorderStyle = BorderStyle.None;
            listStimuli.BorderStyle = BorderStyle.None;
            (sender as ListBoxControl).BorderStyle = BorderStyle.Fixed3D;
        }

        private void listGeneric_ItemsHideShowInactive(object sender, EventArgs e)
        {
            if (sender is ListBoxControl listBoxControl && e is GenericArgs args)
            {
                Label label = listBoxControl == listCellPools ? lCellPoolsTitle :
                    listBoxControl == listCells ? lCellsTitle :
                    listBoxControl == listStimuli ? lStimuliTitle :
                    listBoxControl == listOutgoing ? lOutgoingTitle :
                    listBoxControl == listIncoming ? lIncomingTitle :
                    listBoxControl == listGap ? lGapTitle :
                    null;
                if (label == null) return;
                bool showInactive = args.ValueBoolean;
                if (!showInactive && !label.Text.EndsWith(" (active only)"))
                    label.Text += " (active only)";
                else if (showInactive && label.Text.EndsWith(" (active only)"))
                    label.Text = label.Text.Remove(label.Text.Length - 14);
            }
        }

    }
}
