using Extensions;
using OxyPlot;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services.Plotting.PlotSelection;
using SiliFish.UI.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.VoiceCommands;

namespace SiliFish.UI.Controls.Display
{
    public partial class CellSelectionControl : UserControl
    {
        public event EventHandler SelectionChanged;
        private RunningModel runningModel;
        private List<ModelUnitBase> selectedUnits;
        public bool CombineOptionsVisible
        {
            set => pCombine.Visible = value;
            get => pCombine.Visible;
        }
        public string PoolSubset;
        public bool ControlsEnabled
        {
            set
            {
                foreach (Control ctrl in Controls)
                    ctrl.SetEnabled(value);
                int NumberOfSomites = runningModel?.ModelDimensions.NumberOfSomites ?? 0;
                ddSomiteSelection.Enabled = eSomiteSelection.Enabled = cbCombineSomites.Enabled = NumberOfSomites > 0;
                if (NumberOfSomites == 0)
                    cbCombineSomites.Checked = false;
            }
        }

        public List<ModelUnitBase> SelectedUnits
        {
            get => selectedUnits;
            set
            {
                selectedUnits = value;
                if (value != null && value.Count != 0)
                {
                    List<CellPool> cellPools = value.Where(c => c is CellPool).Select(c => c as CellPool).
                        Union(value.Where(c => c is Cell).Select(c => (c as Cell).CellPool)).Distinct().ToList();
                    if (cellPools.Count == 1 || (cellPools.Count == 2 && cellPools[0].CellGroup == cellPools[1].CellGroup))
                    {
                        ddPools.SelectedItem = cellPools[0].CellGroup;
                        ddSagittal.SelectedItem = cellPools.Count == 1 ?
                            cellPools[0].PositionLeftRight.ToString() :
                            "Left/Right";
                        if (!ddCellSelection.Items.Contains("Selection"))
                            ddCellSelection.Items.Add("Selection");
                        ddCellSelection.SelectedItem = "Selection";
                    }
                    else
                    {
                        if (!ddPools.Items.Contains("Selection"))
                            ddPools.Items.Add("Selection");
                        ddPools.SelectedItem = "Selection";
                        ddSagittal.Enabled = false;
                    }
                }
                else
                {
                    if (ddPools.Items.Contains("Selection"))
                        ddPools.Items.Remove("Selection");
                    ddSagittal.Enabled = true;
                }
            }
        }
        public RunningModel RunningModel
        {
            set
            {
                runningModel = value;
                PopulatePools();
                int NumberOfSomites = runningModel.ModelDimensions.NumberOfSomites;
                ddSomiteSelection.Enabled = eSomiteSelection.Enabled = cbCombineSomites.Enabled = NumberOfSomites > 0;
                if (NumberOfSomites == 0)
                    cbCombineSomites.Checked = false;
            }
        }
        public CellSelectionControl()
        {
            InitializeComponent();
            PopulatePools();
            foreach (PlotSomiteSelection ps in Enum.GetValues(typeof(PlotSomiteSelection)))
            {
                ddSomiteSelection.Items.Add(ps.GetDisplayName());
            }
            foreach (PlotCellSelection ps in Enum.GetValues(typeof(PlotCellSelection)))
            {
                ddCellSelection.Items.Add(ps.GetDisplayName());
            }
            foreach (SagittalPlane sp in Enum.GetValues(typeof(SagittalPlane)))
            {
                ddSagittal.Items.Add(sp.GetDisplayName());
            }
            ddSagittal.SelectedIndex = ddSagittal.Items.Count - 1;
            ddSomiteSelection.SelectedItem = PlotSomiteSelection.All.ToString();
            ddCellSelection.SelectedItem = PlotCellSelection.Spiking.ToString();
        }
        private void PopulatePools()
        {
            string prevSelection = ddPools.Text;
            ddPools.Items.Clear();
            ddPools.Text = "";
            List<string> itemList =
            [
                Const.AllPools,
                Const.SupraSpinal,
                Const.Spinal,
                Const.MusculoSkeletal,
                Const.MuscleCells,
                Const.Motoneurons,
                Const.Interneurons,
                Const.Neurons,
                Const.ExcitatoryNeurons,
                Const.InhibitoryNeurons
            ];
            if (runningModel == null || runningModel.CellPools.Count == 0) return;
            itemList.AddRange([.. runningModel.CellPools.Where(cp => cp.Active).Select(p => p.CellGroup).OrderBy(p => p)]);

            ddPools.Items.AddRange(itemList.Distinct().ToArray());
            if (ddPools.Items?.Count > 0)
            {
                ddPools.Text = prevSelection;
                if (ddPools.SelectedIndex < 0)
                    ddPools.SelectedIndex = 0;
            }
        }


        public Dictionary<string, string> GetSelectionAsDictionary()
        {
            Dictionary<string, string> selection = new()
            {
                { ddSagittal.Name, ddSagittal.Text },
                { ddPools.Name, ddPools.Text },
                { ddSomiteSelection.Name, ddSomiteSelection.Text },
                { ddCellSelection.Name, ddCellSelection.Text },
                { eSomiteSelection.Name, eSomiteSelection.Value.ToString() },
                { eCellSelection.Name, eCellSelection.Value.ToString() },
                { cbCombinePools.Name, cbCombinePools.Checked.ToString()},
                { cbCombineSomites.Name, cbCombineSomites.Checked.ToString()},
                { cbCombineCells.Name, cbCombineCells.Checked.ToString()}
            };
            return selection;
        }

        public void ClearSelection()
        {
            try
            {
                if (ddPools.Items.Contains("Selection"))
                    ddPools.Items.Remove("Selection");
                SelectedUnits = null;
            }
            catch { }
        }
        public void SetSelectionAsDictionary(Dictionary<string, string> selection)
        {
            try
            {
                if (selection == null || selection.Count == 0)
                {
                    ClearSelection();
                    return;
                }
                ddSagittal.Text = selection.GetValueOrDefault(ddSagittal.Name, SagittalPlane.Both.ToString());
                if (selection.GetValueOrDefault(ddPools.Name, Const.AllPools) == "Selection" && !ddPools.Items.Contains("Selection"))
                    ddPools.Items.Add("Selection");
                ddPools.Text = selection.GetValueOrDefault(ddPools.Name, Const.AllPools);
                ddSomiteSelection.Text = selection.GetValueOrDefault(ddSomiteSelection.Name, PlotSomiteSelection.All.ToString());
                if (selection.GetValueOrDefault(ddCellSelection.Name, PlotCellSelection.All.ToString()) == "Selection" && !ddPools.Items.Contains("Selection"))
                    ddCellSelection.Items.Add("Selection");
                ddCellSelection.Text = selection.GetValueOrDefault(ddCellSelection.Name, PlotCellSelection.All.ToString());
                eSomiteSelection.SetValue(double.Parse(selection.GetValueOrDefault(eSomiteSelection.Name, "1")));
                eCellSelection.SetValue(double.Parse(selection.GetValueOrDefault(eCellSelection.Name, "1")));
                cbCombinePools.Checked = bool.Parse(selection.GetValueOrDefault(cbCombinePools.Name, "True"));
                cbCombineSomites.Checked = bool.Parse(selection.GetValueOrDefault(cbCombineSomites.Name, "True"));
                cbCombineCells.Checked = bool.Parse(selection.GetValueOrDefault(cbCombineCells.Name, "True"));
            }
            catch { }
        }

        public void SetPlot(PlotDefinition plot)
        {
            ddPools.Text = PoolSubset = plot.PlotSubset;
            PlotSelectionInterface selection = plot.Selection;
            if (selection is PlotSelectionMultiCells multiCells)
            {
                ddSagittal.Text = multiCells.SagittalPlane.GetDisplayName();
                ddSomiteSelection.Text = multiCells.SomiteSelection.GetDisplayName();
                ddCellSelection.Text = multiCells.CellSelection.GetDisplayName();
                eSomiteSelection.SetValue(multiCells.NSomite);
                eCellSelection.SetValue(multiCells.NCell);
                cbCombinePools.Checked = multiCells.CombinePools;
                cbCombineSomites.Checked = multiCells.CombineSomites;
                cbCombineCells.Checked = multiCells.CombineCells;
            }
            if (selection is PlotSelectionUnits selectedUnits)
            {
                //SetPlotSelectionToUnits(selectedUnits.Units);
                if (!ddPools.Items.Contains("Selection"))
                    ddPools.Items.Add("Selection");
                ddPools.SelectedItem = "Selection";
                ddPools.Tag = selectedUnits.Units;
            }

        }
        public void TurnOnSingleCellOrSomite()
        {
            foreach(Control control in pMain.Controls) 
                control.Enabled = false;
            if (runningModel.ModelDimensions.NumberOfSomites > 0)
            {
                ddSomiteSelection.Text = PlotSomiteSelection.Single.ToString();
                eSomiteSelection.Enabled = true;
            }
            else
            {
                ddCellSelection.Text = PlotCellSelection.Single.ToString();
                eCellSelection.Enabled = true;
            }
        }

        public void TurnOnMultipleCellOrSomite()
        {
            foreach (Control control in pMain.Controls)
                control.Enabled = false;
            if (runningModel.ModelDimensions.NumberOfSomites > 0)
            {
                ddSomiteSelection.Enabled = true;
                eSomiteSelection.Enabled = true;
            }
            else
            {
                ddCellSelection.Enabled = true;
                eCellSelection.Enabled = true;
            }
        }

        public int GetSingleSomiteOrCell()
        {
            if (runningModel?.ModelDimensions.NumberOfSomites > 0)
                return (int)eSomiteSelection.Value;
            else
                return (int)eCellSelection.Value;
        }


        private void ddSomiteSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSomiteSelection.SelectedIndex < 0) return;
            PlotSomiteSelection sel = ddSomiteSelection.Text.GetValueFromName(PlotSomiteSelection.All);
            eSomiteSelection.Enabled = sel == PlotSomiteSelection.Random || sel == PlotSomiteSelection.Single ||
                sel == PlotSomiteSelection.RostralTo || sel == PlotSomiteSelection.CaudalTo;
            if (ddSomiteSelection.Focused)
                SelectionChanged?.Invoke(this, EventArgs.Empty);

        }

        private void ddCellSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddCellSelection.SelectedIndex < 0) return;
            PlotCellSelection sel = ddCellSelection.Text.GetValueFromName<PlotCellSelection>(PlotCellSelection.All);
            eCellSelection.Enabled = sel == PlotCellSelection.Random || sel == PlotCellSelection.Single;
            if (ddCellSelection.Focused)
                SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddSagittal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSagittal.Focused)
                SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddPools_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddPools.SelectedIndex < 0 || ddSagittal.SelectedIndex < 0 || runningModel is null)
                return;
            PoolSubset = ddPools.Text;
            PlotSelectionInterface selectionInterface = GetSelection();
            if (selectionInterface is PlotSelectionMultiCells multiCells)
            {
                (List<Cell> Cells, List<CellPool> Pools) = runningModel.GetSubsetCellsAndPools(PoolSubset, multiCells);
                PlotSelectionMultiCells selForMaxNumbers = new()
                {
                    SagittalPlane = multiCells.SagittalPlane,
                    CellSelection = PlotCellSelection.All,
                    SomiteSelection = PlotSomiteSelection.All,
                    CombineCells = false,
                    CombineSomites = false,
                    CombinePools = false
                };
                (List<Cell> CellsFull, List<CellPool> PoolsFull) = runningModel.GetSubsetCellsAndPools(PoolSubset, selForMaxNumbers);

                eSomiteSelection.Maximum =
                    PoolsFull != null && PoolsFull.Count != 0 ? PoolsFull.Max(p => p.GetMaxCellSomite()) :
                    CellsFull != null && CellsFull.Count != 0 ? CellsFull.Max(c => c.Somite) :
                    Pools != null && Pools.Count != 0 ? Pools.Max(p => p.GetMaxCellSomite()) :
                    Cells != null && Cells.Count != 0 ? Cells.Max(c => c.Somite) : 0;

                eCellSelection.Maximum =
                    PoolsFull != null && PoolsFull.Count != 0 ? PoolsFull.Max(p => p.GetMaxCellSequence()) :
                    CellsFull != null && CellsFull.Count != 0 ? CellsFull.Max(c => c.Sequence) :
                    Pools != null && Pools.Count != 0 ? Pools.Max(p => p.GetMaxCellSequence()) :
                    Cells != null && Cells.Count != 0 ? Cells.Max(c => c.Sequence) : 0;
            }
            if (ddPools.Focused)
            {
                if (ddPools.Text != "Selection")
                    SelectedUnits = null;
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void eSomiteSelection_ValueChanged(object sender, EventArgs e)
        {
            if (eSomiteSelection.Focused)
                SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void eCellSelection_ValueChanged(object sender, EventArgs e)
        {
            if (eCellSelection.Focused)
                SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void cbCombineSomites_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCombineSomites.Focused)
            {
                if (cbCombineSomites.Checked)
                    cbCombineCells.Checked = true;
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void cbCombineCells_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCombineCells.Focused)
            {
                int NumberOfSomites = runningModel?.ModelDimensions.NumberOfSomites ?? 0;
                if (NumberOfSomites > 0 && !cbCombineCells.Checked && (cbCombinePools.Checked || cbCombineSomites.Checked))
                    cbCombineCells.Checked = true; //has to be combined
                else
                    SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private void cbCombinePools_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCombinePools.Focused)
                SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public PlotSelectionInterface GetSelection()
        {
            PlotSelectionInterface selection;
            int NumberOfSomites = runningModel?.ModelDimensions.NumberOfSomites ?? 0;
            if (SelectedUnits != null && SelectedUnits.Count != 0)
            {
                selection = new PlotSelectionUnits()
                {
                    Units = SelectedUnits,
                    SomiteSelection = ddSomiteSelection.Text.GetValueFromName(PlotSomiteSelection.All),
                    NSomite = (int)eSomiteSelection.Value,
                    CellSelection = ddCellSelection.Text.GetValueFromName(PlotCellSelection.All),
                    NCell = (int)eCellSelection.Value,
                    CombineCells = cbCombineCells.Checked,
                    CombineSomites = NumberOfSomites > 0 && cbCombineSomites.Checked,
                    CombinePools = cbCombinePools.Checked
                };
            }
            else
                selection = new PlotSelectionMultiCells()
                {
                    SagittalPlane = ddSagittal.Text.GetValueFromName(SagittalPlane.Both),
                    SomiteSelection = ddSomiteSelection.Text.GetValueFromName(PlotSomiteSelection.All),
                    NSomite = (int)eSomiteSelection.Value,
                    CellSelection = ddCellSelection.Text.GetValueFromName(PlotCellSelection.All),
                    NCell = (int)eCellSelection.Value,
                    CombineCells = cbCombineCells.Checked,
                    CombineSomites = NumberOfSomites > 0 && cbCombineSomites.Checked,
                    CombinePools = cbCombinePools.Checked
                };
            return selection;
        }

        internal void ResetSelection()
        {
            try
            {
                ddSagittal.SelectedItem = SagittalPlane.Both.GetDisplayName();
                ddSomiteSelection.SelectedItem = PlotSomiteSelection.All.ToString();
                ddPools.SelectedItem = Const.AllPools;
                ddCellSelection.SelectedItem = PlotCellSelection.Spiking.ToString();
                cbCombineCells.Checked = true;
                cbCombineSomites.Checked = true;
                cbCombinePools.Checked = false;
            }
            catch { }
        }
    }
}
