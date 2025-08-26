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
using SiliFish.UI.EventArguments;
using SiliFish.UI.Controls.Model_Edit;
using SiliFish.UI.Controls.General;
using SiliFish.UI.Services;
using static OfficeOpenXml.ExcelErrorValue;
using System.Reflection;
using Windows.ApplicationModel.VoiceCommands;

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

        private List<CellPoolTemplate> SelectedPoolTemplates;
        private CellPoolTemplate FirstSelectedCellPoolTemplate => SelectedPoolTemplates != null && SelectedPoolTemplates.Count > 0 ?
            SelectedPoolTemplates[0] : null;

        private List<CellPool> SelectedPools;
        private CellPool FirstSelectedPool => SelectedPools != null && SelectedPools.Count > 0 ?
            SelectedPools[0] : null;
        private string SelectedPoolIDs => SelectedPools != null ?
            string.Join(',', SelectedPools.Select(cp => cp.ID)) : null;

        private List<Cell> SelectedCells; //valid if Mode == Mode.RunningModel
        private string SelectedCellIDs => SelectedPools != null ?
            string.Join(',', SelectedCells.Select(c => c.ID)) : null;
        private Cell FirstSelectedCell => SelectedCells != null && SelectedCells.Count > 0 ?
               SelectedCells[0] : null;

        private char modeIOG;//I: incoming, O; outgoing; G: gap
        private JunctionBase SelectedJunction;
        private StimulusBase SelectedStimulus;
        private List<ModelUnitBase> SelectedUnits
        {
            get
            {
                return SelectedCells?.Count > 0 ? SelectedCells.Cast<ModelUnitBase>().ToList() :
                        (SelectedPools?.Count > 0 ? SelectedPools.Cast<ModelUnitBase>().ToList() :
                        SelectedPoolTemplates?.Count > 0 ? SelectedPoolTemplates.Cast<ModelUnitBase>().ToList() :
                        null);
            }
        }

        public ModelControl()
        {
            InitializeComponent();

            listStimuli.EnableImportExport();
            listStimuli.AddContextMenu("Stimulus Setter/Multiplier", listStimuli_ValueSetter);

            listCellPools.EnableImportExport();
            listCellPools.EnableHightlight();
            listCellPools.AddContextMenu("Synapse/Junction Conductance Multiplier", listCellPools_ConductanceMultiplier);

            listCells.EnableImportExport();
            listCells.EnableHightlight();

            listIncoming.EnableImportExport();
            listIncoming.AddContextMenu("Go to Source", listJunctions_GotoSource);
            listIncoming.AddContextMenu("Synapse/Junction Conductance Multiplier", listJunctions_ConductanceMultiplier);
            listOutgoing.EnableImportExport();
            listOutgoing.AddContextMenu("Go to Target", listJunctions_GotoTarget);
            listOutgoing.AddContextMenu("Synapse/Junction Conductance Multiplier", listJunctions_ConductanceMultiplier);
            listGap.EnableImportExport();
            listGap.AddContextMenu("Go to Neighbor", listJunctions_GotoNeighbor);
            listGap.AddContextMenu("Synapse/Junction Conductance Multiplier", listJunctions_ConductanceMultiplier);

            tabModel.BackColor = Color.White;
            eModelJSON.AddContextMenu();
            reviewControlSizes();
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
                ddDefaultNeuronCore.Items.AddRange([.. CellCore.GetCoreTypes()]);
            ddDefaultNeuronCore.Text = Model?.Settings.DefaultNeuronCore;
            if (ddDefaultMuscleCellCore.Items.Count == 0)
                ddDefaultMuscleCellCore.Items.AddRange([.. CellCore.GetCoreTypes()]);
            ddDefaultMuscleCellCore.Text = Model?.Settings.DefaultMuscleCellCore;
            propModelSettings.SelectedObject = Model?.Settings;
            propSimulationSettings.SelectedObject = Model?.SimulationSettings;
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
            LoadJunctions();
            LoadStimuli();
            this.Text = $"SiliFish {Model?.ModelName}";
        }

        public void SetModel(ModelBase model, bool clearJson = true)
        {
            Model = model;
            CurrentMode = Model is ModelTemplate ? RunMode.Template : RunMode.RunningModel;
            splitCellPoolsAndCells.Panel2Collapsed = CurrentMode == RunMode.Template;
            propModelDimensions.Enabled = propModelSettings.Enabled = CurrentMode == RunMode.Template;
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
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
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
            SelectedPoolTemplates = null;
            SelectedPools = null;
            SelectedCells = null;
        }
        private CellPoolTemplate OpenCellPoolDialog(CellPoolTemplate pool)
        {
            if (Model == null) return null;
            ControlContainer frmControl = new(ParentForm.Location);
            CellPoolControl cplControl = new(Model.ModelDimensions.NumberOfSomites > 0, Model.Settings, Model.DynamicsParam);
            cplControl.LoadPool += Cpl_LoadPool;
            cplControl.SavePool += Cpl_SavePool;

            cplControl.PoolBase = pool;
            bool createdCellPool = pool is CellPool cp && cp.NumOfCells > 0;
            frmControl.SaveVisible = !createdCellPool;
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
                SelectedPools = [.. lb.SelectedItems.Where(i => i is CellPool).Select(i => i as CellPool)];
                if (listCellPools.HighlightSelected)
                    listCellPools_ItemHighlight(sender, e);
                SelectedCells = null;
                LoadCells();
                LoadJunctions(SelectedPools);
                LoadStimuli(SelectedPools);
            }
            else if (lb.SelectedItem is CellPoolTemplate cpt)
            {
                SelectedPoolTemplates = [.. lb.SelectedItems.Where(i => i is CellPoolTemplate).Select(i => i as CellPoolTemplate)];
                LoadJunctions(SelectedPoolTemplates);
                LoadStimuli(SelectedPoolTemplates);
            }
            else if (lb.SelectedItem == null)
            {
                SelectedPoolTemplates = null;
                SelectedPools = null;
                SelectedCells = null;
                LoadCells();//Full list
                LoadJunctions();//Full list
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
                RefreshJunctions();
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
                    //Automatically loaded with SelectedIndexChangedEvent LoadJunctions();
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
                    RefreshJunctions();
                }
                else
                    (Model as ModelTemplate).UpdateCellPool(pool);
            }
        }
        private void listCellPools_ItemHighlight(object sender, EventArgs e)
        {
            if (SelectedPools == null || SelectedPools.Count == 0)
                return;
            SelectedUnitArgs args = new();
            args.unitsSelected.Add(FirstSelectedPool);
            HighlightRequested?.Invoke(this, args);
        }


        private void listCellPools_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }
        private void listCellPools_ItemsExport(object sender, EventArgs e)
        {
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (ModelFile.SaveCellPoolsToCSV(saveFileCSV.FileName, Model, SelectedUnits))
                {
                    string filename = saveFileCSV.FileName;
                    UtilWindows.DisplaySavedFile(filename);
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
                string selOrAll = (SelectedPools?.Count > 0 || SelectedPoolTemplates?.Count > 0) ? "selected" : "all";
                string msg = $"Importing will remove {selOrAll} cell pools and create from the CSV file. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            }
            openFileCSV.Title = "Cell Pool Import";
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileCSV.FileName;
                if (ModelFile.ReadCellPoolsFromCSV(filename, Model, SelectedUnits))
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
                            Model.GetGapJunctions()
                                .Where(c => (c.SourcePool == poolTemplate.CellGroup || c.TargetPool == poolTemplate.CellGroup) && c.Active)
                                .ToList()
                                .ForEach(c => changes.Add($"{c.ID} deactivated"));
                            Model.GetGapJunctions()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup || c.TargetPool == poolTemplate.CellGroup)
                                .ToList()
                                .ForEach(c => c.Active = false);
                        }
                        else
                        {
                            int count = Model.GetGapJunctions()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup || c.TargetPool == poolTemplate.CellGroup).Count();
                            Model.GetGapJunctions()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup || c.TargetPool == poolTemplate.CellGroup)
                                .ToList()
                                .ForEach(c => (c as InterPoolTemplate).MultiplyConductance(mult));
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
                            Model.GetChemicalJunctions()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup && c.Active)
                                .ToList()
                                .ForEach(c => changes.Add($"{c.ID} deactivated"));
                            Model.GetChemicalJunctions()
                                 .Where(c => c.SourcePool == poolTemplate.CellGroup)
                                 .ToList()
                                 .ForEach(c => c.Active = false);
                        }
                        else
                        {
                            int count = Model.GetChemicalJunctions()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup).Count();
                            Model.GetChemicalJunctions()
                                .Where(c => c.SourcePool == poolTemplate.CellGroup)
                                .ToList()
                                .ForEach(c => (c as InterPoolTemplate).MultiplyConductance(mult));
                            changes.Add($"Conductance value of {count} chem junctions originating from {poolTemplate.CellGroup} multiplied by {mult}");
                        }
                    }
                }

                TextDisplayer.Display($"Modified Synapse/Junction Conductance Values", changes);
                RefreshJunctions();
                ModelIsUpdated();
            }
        }

        private void LoadCells()
        {
            if (Model is ModelTemplate) return;
            lCellsTitle.Text = SelectedPoolIDs ?? "Cells";

            List<Cell> Cells =
                SelectedPools != null && SelectedPools.Count > 0 ?
                SelectedPools.SelectMany(sp => sp.GetCells()).OfType<Cell>().ToList() :
                (Model as RunningModel).GetCells();
            if ((SelectedPools == null || SelectedPools.Count > 1) && Cells.Count > GlobalSettings.MaxNumberOfUnitsToList)
            {
                listCells.ClearItems();
                listCells.AppendItem($"Number of items to display is greater than {GlobalSettings.MaxNumberOfUnitsToList}.");
                listCells.AppendItem("Please select a cell pool to list cells under...");
            }
            else
            {
                listCells.LoadItems(Cells.Cast<object>().ToList());
            }
            SelectedCells = null;
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
                if (SelectedPools != null && SelectedPools.Contains(newCell.CellPool))
                    listCells.AppendItem(newCell);
                else
                    SelectedPools = [newCell.CellPool];
                SelectedCells = [newCell];
                ModelIsUpdated();
            }
        }

        private void listCells_ItemSelect(object sender, EventArgs e)
        {
            if (CurrentMode != RunMode.RunningModel) return;
            ListBoxControl lb = sender as ListBoxControl;
            if (lb.SelectedItem is Cell cell)
            {
                SelectedCells = [.. lb.SelectedItems.Where(i => i is Cell).Select(i => i as Cell)];
                if (listCells.HighlightSelected)
                    listCells_ItemHighlight(sender, e);
                LoadJunctions(SelectedCells);
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
                SelectedPools = [cell.CellPool];
                SelectedCells = [cell];
                RefreshJunctions();
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
                    SelectedCells = null;
                    LoadCells();
                    LoadJunctions();
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
            args.unitsSelected.Add(FirstSelectedCell);
            HighlightRequested?.Invoke(this, args);
        }

        private void listCells_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }
        private bool SelectedUnitHasCell()
        {
            if (SelectedPools != null && SelectedPools.Any(cp => cp.HasCells())
                ||
                (SelectedPools == null || SelectedPools.Count == 0) && (Model as RunningModel).HasCells())
                return true;
            return false;
        }
        private void listCells_ItemsExport(object sender, EventArgs e)
        {
            if (Model is RunningModel runningModel)
            {
                if (saveFileCSV.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileCSV.FileName;
                    bool saved;
                    if (SelectedCells?.Count > 0)
                        saved = ModelFile.SaveCellsToCSV(filename, runningModel, SelectedCells.Cast<ModelUnitBase>().ToList());
                    else if (SelectedPools?.Count > 0)
                        saved = ModelFile.SaveCellsToCSV(filename, runningModel, SelectedPools.Cast<ModelUnitBase>().ToList());
                    else
                        saved = ModelFile.SaveCellsToCSV(filename, runningModel, null);//full model
                    if (saved)
                        UtilWindows.DisplaySavedFile(filename);
                    else
                        MessageBox.Show("There is a problem with saving the csv file. Please make sure the file is not open.", "Error");
                }
            }
        }

        private void listCells_ItemsImport(object sender, EventArgs e)
        {
            string unit = SelectedCells != null && SelectedCells.Count > 0 ? " [" + SelectedCellIDs + "]" :
                SelectedPools != null && SelectedPools.Count > 0 ? " of " + SelectedPoolIDs : "";
            if (!warnedImport && SelectedUnitHasCell())
            {
                warnedImport = true;
                string msg = $"Importing will remove the cells{unit}. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            }
            openFileCSV.Title = $"Cell import {unit}";
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileCSV.FileName;
                bool success;
                if (SelectedCells?.Count > 0)
                    success = ModelFile.ReadCellsFromCSV(filename, Model as RunningModel, SelectedCells.Cast<ModelUnitBase>().ToList());
                else if (SelectedPools?.Count > 0)
                    success = ModelFile.ReadCellsFromCSV(filename, Model as RunningModel, SelectedPools.Cast<ModelUnitBase>().ToList());
                else
                    success = ModelFile.ReadCellsFromCSV(filename, Model as RunningModel, null);
                if (success)
                {
                    LoadCells();
                    MessageBox.Show($"The cells{unit} are imported.", "Information");
                }
                else
                    MessageBox.Show($"The import was unsuccesful. Make sure {filename} is a valid export file and not currently used by any other software.", "Error");
            }
        }



        #endregion

        #region Connection
        private void ClearJunctions()
        {
            listIncoming.ClearItems();
            listOutgoing.ClearItems();
            listGap.ClearItems();
        }

        private void RefreshJunctions()
        {
            if (SelectedCells != null)
                LoadJunctions(SelectedCells);
            else if (SelectedPools != null)
                LoadJunctions(SelectedPools);
            else if (SelectedPoolTemplates != null && SelectedPoolTemplates.Count > 0)
                LoadJunctions(SelectedPoolTemplates);
            else
                LoadJunctions();
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
        private void LoadJunctions()
        {
            if (Model == null) return;
            ClearJunctions();
            lOutgoingTitle.Text = "Synaptic Connections";
            lGapTitle.Text = "Gap Junctions";
            splitChemicalJunctions.Panel1Collapsed = true;
            if (Model is RunningModel rm)
            {
                List<InterPool> chemInterPools = [.. rm.ChemPoolJunctions];
                if (chemInterPools.Count > GlobalSettings.MaxNumberOfUnitsToList)
                    listOutgoing.AppendItem("Please select a cell pool to list junctions under...");
                else
                    listOutgoing.LoadItems(chemInterPools.Cast<object>().ToList());
                List<InterPool> gapInterPools = [.. rm.GapPoolJunctions];
                if (gapInterPools.Count > GlobalSettings.MaxNumberOfUnitsToList)
                    listGap.AppendItem("Please select a cell pool to list junctions under...");
                else
                    listGap.LoadItems(gapInterPools.Cast<object>().ToList());
            }
            else if (Model is ModelTemplate mt)
            {
                List<InterPoolTemplate> chemInterPools = mt.InterPoolTemplates.Where(jnc => jnc.JunctionType != JunctionType.Gap).ToList();
                if (chemInterPools.Count > GlobalSettings.MaxNumberOfUnitsToList)
                    listOutgoing.AppendItem("Please select a cell pool to list junctions under...");
                else
                    listOutgoing.LoadItems(chemInterPools.Cast<object>().ToList());
                List<InterPoolTemplate> gapInterPools = mt.InterPoolTemplates.Where(jnc => jnc.JunctionType == JunctionType.Gap).ToList();
                if (gapInterPools.Count > GlobalSettings.MaxNumberOfUnitsToList)
                    listGap.AppendItem("Please select a cell pool to list junctions under...");
                else
                    listGap.LoadItems(gapInterPools.Cast<object>().ToList());
            }
        }

        private void LoadJunctions(List<CellPoolTemplate> cellpooltemplates)
        {
            if (cellpooltemplates == null || cellpooltemplates.Count == 0)
            {
                LoadJunctions();
                return;
            }
            ClearJunctions();
            string pools = string.Join(',', cellpooltemplates.Select(cp => cp.CellGroup));
            List<string> cellGroupNames = cellpooltemplates.Select(cp => cp.CellGroup).ToList();
            lIncomingTitle.Text = $"Incoming Connections of {pools}";
            lOutgoingTitle.Text = $"Outgoing Connections of {pools}";
            lGapTitle.Text = $"Gap Junctions of {pools}";
            splitChemicalJunctions.Panel1Collapsed = false;
            if (Model == null || Model is not ModelTemplate) return;
            listOutgoing.LoadItems(Model.GetChemicalJunctions()
                .Where(proj => cellGroupNames.Contains(((InterPoolTemplate)proj).SourcePool))
                .Cast<object>()
                .ToList());
            listIncoming.LoadItems(Model.GetChemicalJunctions()
                .Where(proj => cellGroupNames.Contains(((InterPoolTemplate)proj).TargetPool))
                .Cast<object>()
                .ToList());
            listGap.LoadItems(Model.GetGapJunctions()
                .Where(proj => cellGroupNames.Contains(((InterPoolTemplate)proj).SourcePool) || cellGroupNames.Contains(((InterPoolTemplate)proj).TargetPool))
                .Cast<object>()
                .ToList());
        }
        private void LoadJunctions(List<CellPool> cellpools)
        {
            if (cellpools == null)
            {
                LoadJunctions();
                return;
            }

            string pools = string.Join(',', cellpools.Select(cp => cp.ID));
            lIncomingTitle.Text = $"Incoming Connections of {pools}";
            lOutgoingTitle.Text = $"Outgoing Connections of {pools}";
            lGapTitle.Text = $"Gap Junctions of {pools}";
            splitChemicalJunctions.Panel1Collapsed = false;
            ClearJunctions();
            foreach (CellPool cp in cellpools)
            {
                List<JunctionBase> gapJunctions = cp.Junctions.Where(j => j is GapJunction).ToList();
                List<InterPool> gapInterPools = (Model as RunningModel).GapPoolJunctions
                    .Where(ip => ip.SourcePool == cp.ID || ip.TargetPool == cp.ID).ToList();
                listGap.AppendItems(gapInterPools.Cast<object>().ToList());

                List<JunctionBase> chemJunctions = cp.Junctions.Where(j => j is not GapJunction).ToList();
                List<InterPool> chemInterPools = (Model as RunningModel).ChemPoolJunctions
                    .Where(ip => ip.SourcePool == cp.ID || ip.TargetPool == cp.ID).ToList();
                listOutgoing.AppendItems(chemInterPools.Where(ip => ip.SourcePool == cp.ID).Cast<object>().ToList());
                listIncoming.AppendItems(chemInterPools.Where(ip => ip.TargetPool == cp.ID).Cast<object>().ToList());
            }
        }

        private void LoadJunctions(List<Cell> cells)
        {
            if (cells == null || cells.Count == 0)
            {
                if (SelectedPools != null)
                    LoadJunctions(SelectedPools);
                else
                    LoadJunctions();
                return;
            }
            ClearJunctions();
            string cellIDs = string.Join(',', cells.Select(cp => cp.ID));

            lIncomingTitle.Text = $"Incoming Connections of {cellIDs}";
            lOutgoingTitle.Text = $"Outgoing Connections of {cellIDs}";
            lGapTitle.Text = $"Gap Junctions of {cellIDs}";
            listGap.LoadItems(cells.SelectMany(c => c.GapJunctions).OfType<GapJunction>().Cast<object>().ToList());

            if (cells[0] is Neuron neuron)
            {
                listIncoming.LoadItems(neuron.Synapses.Cast<object>().ToList());
                listOutgoing.LoadItems(neuron.Terminals.Cast<object>().ToList());
            }
            if (cells[0] is MuscleCell muscleCell)
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
                InterPoolTemplateControl ipControl = new(Model.Settings, Model.ModelDimensions);
                ModelTemplate modelTemplate = Model as ModelTemplate;
                InterPoolTemplate interPoolTemplate = interpool as InterPoolTemplate;
                ipControl.WriteDataToControl(modelTemplate.CellPoolTemplates, interPoolTemplate);
                if (SelectedPoolTemplates != null && SelectedPoolTemplates.Count > 0)
                {
                    if (setSource)
                        ipControl.SetSourcePool(SelectedPoolTemplates[0]);
                    if (setTarget)
                        ipControl.SetTargetPool(SelectedPoolTemplates[0]);
                }
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
                    if (FirstSelectedCell != null)
                        jncControl.SetSourceCell(FirstSelectedCell);
                    else if (FirstSelectedPool != null)
                        jncControl.SetSourcePool(FirstSelectedPool);
                }
                if (setTarget)
                {
                    if (FirstSelectedCell != null)
                        jncControl.SetTargetCell(FirstSelectedCell);
                    else if (FirstSelectedPool != null)
                        jncControl.SetTargetPool(FirstSelectedPool);
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

        private void OpenConnectionDialog(InterPool interPool)
        {
            if (Model is not RunningModel rm) return;
            ControlContainer frmControl = new(ParentForm.Location);
            InterPoolControl ipControl = new(rm);
            ipControl.WriteDataToControl(interPool);
            frmControl.AddControl(ipControl, null);
            frmControl.Text = interPool.ToString();
            frmControl.ShowDialog();
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
                    RefreshJunctions();
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
            else if (sender is InterPool)
                MessageBox.Show("Please select an individual junction to duplicate.", "Info");
            else
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name,
                    new Exception($"Unhandled item type {sender.GetType()}"));
                return;
            }
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
                RefreshJunctions();
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
                RefreshJunctions();
                ModelIsUpdated();
            }
        }
        private void listConnections_ItemView(object sender, EventArgs e)
        {
            if (sender is InterPool ip)
            {
                OpenConnectionDialog(ip);
                return;
            }
            if (sender is not InterPoolBase jnc)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name,
                    new Exception($"Unhandled item type {sender.GetType()}"));
                return;
            }
            List<InterPoolBase> jncs = OpenConnectionDialog(jnc, false);
            if (jncs != null && jncs.Count != 0)
            {
                RefreshJunctions();
                ModelIsUpdated();
            }
        }

        private void listJunctions_ItemToggleActive(object sender, EventArgs e)
        {
            ModelIsUpdated();
        }

        private void listJunctions_ItemSelect(object sender, EventArgs e)
        {
            ListBoxControl lb = sender as ListBoxControl;
            if (lb.SelectedItem is JunctionBase jnc)
            {
                modeIOG = lb == listOutgoing ? 'O' : lb == listIncoming ? 'I' : 'G';
                SelectedJunction = jnc;
            }
        }

        private void listJunctions_ItemsExport(object sender, EventArgs e)
        {
            List<ModelUnitBase> units = SelectedUnits;
            JunctionSelectionControl selectionControl = new()
            {
                ChemInOutExists = units != null,
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
                if (ModelFile.SaveJunctionsToCSV(saveFileCSV.FileName, Model, units, gap, chemin, chemout))
                {
                    string filename = saveFileCSV.FileName;
                    UtilWindows.DisplaySavedFile(filename);
                }
                else
                    MessageBox.Show("There is a problem with saving the csv file. Please make sure the file is not open.", "Error");
            }
        }

        private void listJunctions_ItemsImport(object sender, EventArgs e)
        {
            List<ModelUnitBase> units = SelectedUnits;
            string ofUnits = units != null ? $" of {string.Join(", ", units.Select(u => u.ID))} " : " of full model ";
            if (!warnedImport &&
                (Model is ModelTemplate modelTemplate && modelTemplate.HasJunctions() ||
                Model is RunningModel runningModel && runningModel.HasJunctions()))
            {
                warnedImport = true;
                string msg = $"Importing will remove all cell pool junctions/synapses {ofUnits}and create from the CSV file. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            }
            JunctionSelectionControl selectionControl = new()
            {
                ChemInOutExists = units != null,
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

            openFileCSV.Title = $"Junction import ({units})";
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileCSV.FileName;
                if (ModelFile.ReadJunctionsFromCSV(filename, Model, units, gap, chemin, chemout))
                {
                    RefreshJunctions();
                    string jncList = chemin && chemout ? "chemical" :
                        !chemin && chemout ? "outgoing chemical" :
                        chemin && !chemout ? "incoming chemical" :
                        "";
                    if (gap && !string.IsNullOrEmpty(jncList))
                        jncList = "gap and " + jncList;
                    else if (gap)
                        jncList = "gap";
                    MessageBox.Show($"The {jncList} junctions{ofUnits} are imported.", "Information");
                }
                else
                    MessageBox.Show($"The import was unsuccesful. Make sure {filename} is a valid export file and not currently used by any other software.", "Error");
            }
        }

        private void listJunctions_GotoSource(object sender, EventArgs e)
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

        private void listJunctions_GotoTarget(object sender, EventArgs e)
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

        private void listJunctions_GotoNeighbor(object sender, EventArgs e)
        {
            if (Model == null) return;
            object obj = (sender as ListBoxControl).SelectedItem;
            if (obj is GapJunction jnc)
            {
                Cell otherCell = FirstSelectedCell == jnc.Cell1 ? jnc.Cell2 : jnc.Cell1;
                listCellPools.SelectedItem = otherCell.CellPool;
                listCells.SelectedItem = otherCell;
            }
            else if (obj is InterPoolTemplate ipt)
            {
                string otherPool = FirstSelectedCellPoolTemplate.CellGroup == ipt.TargetPool ? ipt.SourcePool : ipt.TargetPool;
                listCellPools.SelectItem(otherPool);
            }
        }

        private void listJunctions_ConductanceMultiplier(object sender, EventArgs e)
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
                            jncTemplate.MultiplyConductance(mult);
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
                TextDisplayer.Display($"Modified Synapse/Junction Conductance Values", changes);
                RefreshJunctions();
                ModelIsUpdated();
            }
        }
        #endregion

        #region Stimulus

        private void RefreshStimuli()
        {
            if (SelectedCells != null)
                LoadStimuli(SelectedCells);
            else if (SelectedPools != null)
                LoadStimuli(SelectedPools);
            else if (SelectedPoolTemplates != null)
                LoadStimuli(SelectedPoolTemplates);
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
                LoadStimuli(new List<Cell>() { cell });
            else if (unit is CellPool cellPool)
                LoadStimuli(new List<CellPool>() { cellPool });
            else if (unit is CellPoolTemplate poolTemplate)
                LoadStimuli(new List<CellPoolTemplate> { poolTemplate });
        }
        private void LoadStimuli(List<ModelUnitBase> units)
        {
            if (units is null || units.Count == 0)
                LoadStimuli();
            else
            {
                foreach (ModelUnitBase unit in units)
                    LoadStimuli(unit);
            }
        }


        private void LoadStimuli(List<CellPoolTemplate> cellPoolTemplates)
        {
            if (cellPoolTemplates == null || cellPoolTemplates.Count == 0)
            {
                LoadStimuli();
                return;
            }
            if (Model == null || Model is not ModelTemplate) return;
            lStimuliTitle.Text = $"Stimuli of {string.Join(',', cellPoolTemplates.Select(cp => cp.CellGroup))}";
            List<string> ids = cellPoolTemplates.Select(cp => cp.CellGroup).ToList();
            listStimuli.LoadItems(Model.GetStimuli().Cast<StimulusTemplate>().Where(st => ids.Contains(st.TargetPool)).Cast<object>().ToList());
        }
        private void LoadStimuli(List<CellPool> cellpools)
        {
            if (cellpools == null || cellpools.Count == 0)
            {
                LoadStimuli();
                return;
            }
            lStimuliTitle.Text = $"Stimuli of {string.Join(',', cellpools.Select(cp => cp.ID))}";
            listStimuli.LoadItems(cellpools.SelectMany(cp => cp.GetStimuli()).Cast<object>().ToList());
        }
        private void LoadStimuli(List<Cell> cells)
        {
            if (cells == null || cells.Count == 0)
            {
                LoadStimuli(SelectedPools);
                return;
            }
            lStimuliTitle.Text = $"Stimuli of {string.Join(',', cells.Select(cp => cp.ID))}";
            listStimuli.LoadItems(cells.SelectMany(c => c.Stimuli.ListOfStimulus).Cast<object>().ToList());
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
                if (stim == null && FirstSelectedCellPoolTemplate != null)
                    stimControl.SetSourcePool(FirstSelectedCellPoolTemplate);

                frmControl.Text = stim?.ToString() ?? "New Stimulus";

                if (frmControl.ShowDialog() == DialogResult.OK)
                    return stimControl.GetStimulus();
            }
            else
            {
                if (stim == null && FirstSelectedCell == null && FirstSelectedPool == null)
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
                    stimControl.SetTargetCellOrPool(FirstSelectedCell, FirstSelectedPool);
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
                LoadStimuli(SelectedUnits);
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
        private bool SelectedUnitsHasStimulus(List<ModelUnitBase> units)
        {
            if (units == null || units.Count == 0)
            {
                if (Model is ModelTemplate mt)
                    return mt.HasStimulus();
                return (Model as RunningModel).HasStimulus();
            }
            foreach (ModelUnitBase unit in units)
            {
                if (SelectedUnitHasStimulus(unit))
                    return true;
            }
            return false;
        }
        private void listStimuli_ItemsExport(object sender, EventArgs e)
        {
            List<ModelUnitBase> selectedUnits = SelectedUnits;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (ModelFile.SaveStimulusToCSV(saveFileCSV.FileName, Model, selectedUnits))
                {
                    string filename = saveFileCSV.FileName;
                    UtilWindows.DisplaySavedFile(filename);
                }
                else
                    MessageBox.Show("There is a problem with saving the csv file. Please make sure the file is not open.", "Error");
            }
        }

        private void listStimuli_ItemsImport(object sender, EventArgs e)
        {
            List<ModelUnitBase> selectedUnits = SelectedUnits;
            string ofUnits = selectedUnits != null ?
                $"{string.Join(", ", selectedUnits.Select(u => (u is CellPoolTemplate cpt) ? cpt.CellGroup : u.ID))} " :
                "the model ";
            if (!warnedImport && SelectedUnitsHasStimulus(SelectedUnits))
            {
                warnedImport = true;
                string msg = $"Importing will remove all existing stimuli of {ofUnits}. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            }
            openFileCSV.Title = $"Stimulus import ({ofUnits})";
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileCSV.FileName;
                if (ModelFile.ReadStimulusFromCSV(filename, Model, selectedUnits))
                {
                    LoadStimuli(selectedUnits);
                    MessageBox.Show($"The stimuli of {ofUnits} are imported.", "Information");
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
                    if (stim is Stimulus stimulus)
                    {
                        if (mult == 0 && value == 0)
                            stimulus.Active = false;
                        else if (mult != 0)
                            stimulus.Settings.Value1 *= mult;
                        else
                            stimulus.Settings.Value1 = value;
                    }
                    else if (stim is StimulusTemplate st)
                    {
                        if (mult == 0 && value == 0)
                            st.Active = false;
                        else if (mult != 0)
                            st.Settings.Value1 *= mult;
                        else
                            st.Settings.Value1 = value;
                    }
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
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
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
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exc);
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
        public void SetSelection(List<ModelUnitBase> units)
        {
            if (units == null || units.Count == 0) return;
            ModelUnitBase unit = units[0];
            if (unit is CellPool || unit is CellPoolTemplate)
                listCellPools.SelectItems(units.Select(u => u.ID).ToList());
            else if (unit is Cell)
            {
                listCellPools.SelectItems(units.Select(u => (u as Cell).CellPool.ID).ToList());
                listCells.SelectItems(units.Select(u => u.ID).ToList());
            }
            else if (unit is Stimulus)
            {
                listCellPools.SelectItems(units.Select(u => (u as Stimulus).TargetCell.CellPool.ID).ToList());
                listCells.SelectItems(units.Select(u => (u as Stimulus).TargetCell.ID).ToList());
                listStimuli.SelectItems(units.Select(u => u.ID).ToList());
            }
            else if (unit is JunctionBase)
            {
                listCellPools.SelectItems(units.Select(u => (u as JunctionBase).SourcePool).ToList());
                listCells.SelectItems(units.Select(u => (u as JunctionBase).Source).ToList());
                listIncoming.SelectItems(units.Select(u => u.ID).ToList());
                listOutgoing.SelectItems(units.Select(u => u.ID).ToList());
                listGap.SelectItems(units.Select(u => u.ID).ToList());
            }

        }
        internal void SetSelectionToLast()
        {
            try
            {
                Dictionary<string, string> dict = GlobalSettings.LastPlotSettings.Where(kvp => kvp.Key.StartsWith("SelectedUnit")).ToDictionary();
                if (dict.Count == 0) return;
                if (dict.Any(kvp => kvp.Key.Contains("CellPool")))
                {
                    listCellPools.SelectItems(dict.Where(kvp => kvp.Key.Contains("CellPool")).Select(kvp => kvp.Value).ToList());
                    dict = dict.Where(kvp => !kvp.Key.Contains("CellPool")).ToDictionary(k => k.Key, v => v.Value);
                }
                if (dict.Any(kvp => kvp.Key.Contains("Cell")))
                {
                    listCells.SelectItems(dict.Where(kvp => kvp.Key.Contains("Cell")).Select(kvp => kvp.Value).ToList());
                    dict = dict.Where(kvp => !kvp.Key.Contains("Cell")).ToDictionary(k => k.Key, v => v.Value);
                }
                if (dict.Any(kvp => kvp.Key.Contains("Stimulus")))
                {
                    listStimuli.SelectItems(dict.Where(kvp => kvp.Key.Contains("Stimulus")).Select(kvp => kvp.Value).ToList());
                    dict = dict.Where(kvp => !kvp.Key.Contains("Stimulus")).ToDictionary(k => k.Key, v => v.Value);
                }
                if (dict.Any(kvp => kvp.Key.Contains("InterPool")))
                {
                    listIncoming.SelectItems(dict.Where(kvp => kvp.Key.Contains("InterPool")).Select(kvp => kvp.Value).ToList());
                    listOutgoing.SelectItems(dict.Where(kvp => kvp.Key.Contains("InterPool")).Select(kvp => kvp.Value).ToList());
                    listGap.SelectItems(dict.Where(kvp => kvp.Key.Contains("InterPool")).Select(kvp => kvp.Value).ToList());
                    dict = dict.Where(kvp => !kvp.Key.Contains("InterPool")).ToDictionary(k => k.Key, v => v.Value);
                }
            }

            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exc);
            }
        }

        private void propKinematics_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (Model is RunningModel model)
                model.KinemParamsChanged();
        }

        private void splitGeneral_SizeChanged(object sender, EventArgs e)
        {
            reviewControlSizes();
        }

        private void splitGeneral_SplitterMoved(object sender, SplitterEventArgs e)
        {
            reviewControlSizes();
        }

        private void reviewControlSizes()
        {
            eModelDescription.Top = lDescription.Top;
            eModelDescription.Height = splitGeneral.SplitterDistance - 72;
            eModelName.Width = splitGeneral.Width - 144;
            eModelDescription.Width = splitGeneral.Width - 144;
        }
    }
}
