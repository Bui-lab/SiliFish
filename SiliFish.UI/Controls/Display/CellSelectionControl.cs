﻿using Extensions;
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
            }
        }

        public List<ModelUnitBase> SelectedUnits
        {
            get => selectedUnits;
            set
            {
                selectedUnits = value;
                if (value != null && value.Any())
                {
                    //TODO update the selection if there are let's say single pool, but multiple cells etc
                    if (!ddPools.Items.Contains("Selection"))
                        ddPools.Items.Add("Selection");
                    ddPools.SelectedItem = "Selection";
                    ddSagittal.Enabled = false;
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
            List<string> itemList = new()
            {
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
            };
            if (runningModel == null || !runningModel.CellPools.Any()) return;
            itemList.AddRange(runningModel.CellPools.Where(cp => cp.Active).Select(p => p.CellGroup).OrderBy(p => p).ToArray());

            ddPools.Items.AddRange(itemList.Distinct().ToArray());
            if (ddPools.Items?.Count > 0)
            {
                ddPools.Text = prevSelection;
                if (ddPools.SelectedIndex < 0)
                    ddPools.SelectedIndex = 0;
            }
        }


        public Dictionary<string, string> GetSeLectionAsDictionary()
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

        public void SetSelectionAsDictionary(Dictionary<string, string> selection)
        {
            try
            {
                ddSagittal.Text = selection.GetValueOrDefault(ddSagittal.Name, SagittalPlane.Both.ToString());
                ddPools.Text = selection.GetValueOrDefault(ddPools.Name, Const.AllPools);
                ddSomiteSelection.Text = selection.GetValueOrDefault(ddSomiteSelection.Name, PlotSomiteSelection.All.ToString());
                ddCellSelection.Text = selection.GetValueOrDefault(ddCellSelection.Name, PlotCellSelection.All.ToString());
                eSomiteSelection.SetValue(double.Parse(selection.GetValueOrDefault(eSomiteSelection.Name, "1")));
                eCellSelection.SetValue(double.Parse(selection.GetValueOrDefault(eCellSelection.Name, "1")));
                cbCombinePools.Checked = bool.Parse(selection.GetValueOrDefault(cbCombinePools.Name, "True"));
                cbCombineSomites.Checked = bool.Parse(selection.GetValueOrDefault(cbCombineSomites.Name, "True"));
                cbCombineCells.Checked = bool.Parse(selection.GetValueOrDefault(cbCombineCells.Name, "True"));
            }
            catch { }
        }

        public void SetSelection(PlotSelectionInterface selection)
        {
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
            pMain.SetEnabledNoChild(true);
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
                    PoolsFull != null && PoolsFull.Any() ? PoolsFull.Max(p => p.GetMaxCellSomite()) :
                    CellsFull != null && CellsFull.Any() ? CellsFull.Max(c => c.Somite) :
                    Pools != null && Pools.Any() ? Pools.Max(p => p.GetMaxCellSomite()) :
                    Cells != null && Cells.Any() ? Cells.Max(c => c.Somite) : 0;

                eCellSelection.Maximum =
                    PoolsFull != null && PoolsFull.Any() ? PoolsFull.Max(p => p.GetMaxCellSequence()) :
                    CellsFull != null && CellsFull.Any() ? CellsFull.Max(c => c.Sequence) :
                    Pools != null && Pools.Any() ? Pools.Max(p => p.GetMaxCellSequence()) :
                    Cells != null && Cells.Any() ? Cells.Max(c => c.Sequence) : 0;
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
                if (!cbCombineCells.Checked && (cbCombinePools.Checked || cbCombineSomites.Checked))
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
            if (SelectedUnits != null && SelectedUnits.Any())
            {
                selection = new PlotSelectionUnits()
                {
                    Units = SelectedUnits,
                    SomiteSelection = ddSomiteSelection.Text.GetValueFromName(PlotSomiteSelection.All),
                    NSomite = (int)eSomiteSelection.Value,
                    CellSelection = ddCellSelection.Text.GetValueFromName(PlotCellSelection.All),
                    NCell = (int)eCellSelection.Value,
                    CombineCells = cbCombineCells.Checked,
                    CombineSomites = cbCombineSomites.Checked,
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
                    CombineSomites = cbCombineSomites.Checked,
                    CombinePools = cbCombinePools.Checked
                };
            return selection;
        }

    }
}