﻿using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.UI.EventArguments;

namespace SiliFish.UI.Controls
{
    public partial class JunctionControl : UserControl
    {
        private JunctionBase junction;
        private readonly RunningModel Model;
        private CellPool sourcePool, targetPool;
        private Cell sourceCell;
        private Cell targetCell;

        public JunctionControl(RunningModel model)
        {
            InitializeComponent();
            Model = model;

            ddDistanceMode.DataSource = Enum.GetNames(typeof(DistanceMode));

            //ddConnectionType is manually loaded as not all of them are displayed
            ddJunctionType.Items.Add(JunctionType.Gap);
            ddJunctionType.Items.Add(JunctionType.Synapse);
            ddJunctionType.Items.Add(JunctionType.NMJ);

            ddSourcePool.Items.AddRange([.. Model.CellPools]);
            ddSourcePool.SelectedIndex = -1;
            ddSourceCell.SelectedIndex = -1;

            ddTargetPool.Items.AddRange([.. Model.CellPools]);
            ddTargetPool.SelectedIndex = -1;
            ddTargetCell.SelectedIndex = -1;
        }

        private void FillJunctionTypes()
        {
            if (ddSourcePool.SelectedItem is CellPoolTemplate source
                && ddTargetPool.SelectedItem is CellPoolTemplate target)
            {
                if (source.CellType == CellType.MuscleCell)
                {
                    ddJunctionType.Items.Clear();
                    ddJunctionType.Items.Add(JunctionType.Gap);
                    ddJunctionType.SelectedItem = JunctionType.Gap;
                    ddCoreType.Items.AddRange([.. ElecSynapseCore.GetSynapseTypes()]);

                }
                else //Neuron
                {
                    JunctionType prevType = JunctionType.NotSet;
                    if (ddJunctionType.SelectedItem != null)
                        prevType = (JunctionType)ddJunctionType.SelectedItem;
                    ddJunctionType.Items.Clear();
                    ddJunctionType.Items.Add(JunctionType.Gap);
                    if (target.CellType == CellType.MuscleCell)
                        ddJunctionType.Items.Add(JunctionType.NMJ);
                    else
                        ddJunctionType.Items.Add(JunctionType.Synapse);
                    if (ddJunctionType.Items.Contains(prevType))
                        ddJunctionType.SelectedItem = prevType;
                    string lastSelection = ddCoreType.Text;
                    ddCoreType.Items.Clear();
                    ddCoreType.Items.AddRange(prevType == JunctionType.Gap ?
                        ElecSynapseCore.GetSynapseTypes().ToArray() :
                        [.. ChemSynapseCore.GetSynapseTypes()]);
                    ddCoreType.Text = lastSelection;
                }
            }
        }

        private void LoadSelectedSourceValues()
        {
            if (ddSourcePool.SelectedItem is CellPool pool)
            {
                sourcePool = pool;
                sourceCell = null;
                UtilWindows.FillCells(ddSourceCell, sourcePool);
                if (junction?.Core is ChemSynapseCore csc)
                    csc.SetNeuroTransmitter(pool.Model.Settings, pool.NTMode);
            }
        }
        private void ddSourcePool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillJunctionTypes();
            if (!ddSourcePool.Focused)
                return;
            LoadSelectedSourceValues();
        }
        private void ddSourceCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSourceCell.SelectedItem is Cell cell)
                sourceCell = cell;
            else
                sourceCell = null;
        }
        private void LoadSelectedTargetValues()
        {
            if (ddTargetPool.SelectedItem is CellPool pool)
            {
                targetPool = pool;
                targetCell = null;
                UtilWindows.FillCells(ddTargetCell, targetPool);
            }
        }

        private void ddTargetPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillJunctionTypes();
            if (!ddTargetPool.Focused)
                return;
            LoadSelectedTargetValues();
        }

        private void ddTargetCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddTargetCell.SelectedItem is Cell cell)
                targetCell = cell;
            else
                targetCell = null;
        }

        private void ddJunctionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (junction == null || !ddJunctionType.Focused) return;
            JunctionType JunctionType = (JunctionType)Enum.Parse(typeof(JunctionType), ddJunctionType.Text);
            string lastSelection = ddCoreType.Text;
            ddCoreType.Items.Clear();
            ddCoreType.Items.AddRange(JunctionType == JunctionType.Gap ?
                ElecSynapseCore.GetSynapseTypes().ToArray() :
                [.. ChemSynapseCore.GetSynapseTypes()]);
            ddCoreType.Text = lastSelection;
        }

        public void SetJunction(JunctionBase junction, bool newJunc)
        {
            this.junction = junction;
            if (junction is GapJunction gapJunction)
            {
                sourceCell = gapJunction.Cell1;
                targetCell = gapJunction.Cell2;
                eFixedDuration.Text = gapJunction.FixedDuration_ms.ToString();
                eSynDelay.Text = gapJunction.Delay_ms.ToString();
                ddJunctionType.SelectedItem = JunctionType.Gap;
                ddCoreType.Items.Clear();
                ddCoreType.Items.AddRange([.. ElecSynapseCore.GetSynapseTypes()]);
                ddCoreType.Text = gapJunction.Core.GetType().Name;
                propCore.SelectedObject = gapJunction.Core;
            }
            else if (junction is ChemicalSynapse syn)
            {
                sourceCell = syn.PreNeuron;
                targetCell = syn.PostCell;
                ddJunctionType.SelectedItem = targetCell is MuscleCell ? JunctionType.NMJ : JunctionType.Synapse;
                ddCoreType.Items.Clear();
                ddCoreType.Items.AddRange([.. ChemSynapseCore.GetSynapseTypes()]);
                ddCoreType.Text = syn.Core.GetType().Name;
                propCore.SelectedObject = syn.Core;
                eFixedDuration.Text = syn.FixedDuration_ms.ToString();
                eSynDelay.Text = syn.Delay_ms.ToString();
            }
            if (this.junction != null)
            {
                sourcePool = sourceCell.CellPool;
                targetPool = targetCell.CellPool;

                Cell backup = sourceCell;//cleared when dropdowns are reset
                ddSourcePool.SelectedItem = sourcePool;
                UtilWindows.FillCells(ddSourceCell, sourcePool);
                sourceCell = backup;
                ddSourceCell.SelectedItem = sourceCell;
                backup = targetCell;
                ddTargetPool.SelectedItem = targetPool;
                UtilWindows.FillCells(ddTargetCell, targetPool);
                targetCell = backup;
                ddTargetCell.SelectedItem = targetCell;
                ddSourcePool.Enabled = false;
            }
            cbActive.Checked = junction?.Active ?? true;
            timeLineControl.SetTimeLine(junction?.TimeLine_ms);
            if (newJunc)
            {
                this.junction = null;
            }
            else
            {
                ddSourceCell.Enabled = false;
                ddTargetPool.Enabled = false;
                ddTargetCell.Enabled = false;
                ddJunctionType.Enabled = false;
                ddCoreType.Enabled = false;
                ddDistanceMode.Enabled = false;
            }
        }
        internal void SetSourcePool(CellPool selectedPool)
        {
            ddSourcePool.SelectedItem = selectedPool;
            LoadSelectedSourceValues();
        }

        internal void SetSourceCell(Cell selectedCell)
        {
            SetSourcePool(selectedCell.CellPool);
            ddSourceCell.SelectedItem = selectedCell;
        }

        internal void SetTargetPool(CellPool selectedPool)
        {
            ddTargetPool.SelectedItem = selectedPool;
            LoadSelectedTargetValues();
        }

        internal void SetTargetCell(Cell selectedCell)
        {
            SetTargetPool(selectedCell.CellPool);
            ddTargetCell.SelectedItem = selectedCell;
        }

        public void SetAsGapJunction()
        {
            try { ddJunctionType.SelectedItem = JunctionType.Gap; }
            catch { }
        }
        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return $"{ddSourcePool.Text}-->{ddTargetPool.Text} [{ddJunctionType.Text}]{activeStatus}";
        }
        internal void CheckValues(object sender, EventArgs args)
        {
            CheckValuesArgs checkValuesArgs = args as CheckValuesArgs;
            checkValuesArgs.Errors = [];
            if (ddSourcePool.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No source pool selected.");
            else if (ddSourceCell.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No source cell selected.");
            if (ddTargetPool.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No target pool selected.");
            else if (ddTargetCell.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No target cell selected.");
            if (ddJunctionType.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Junction type not defined.");
            else
            {
                List<string> errors = checkValuesArgs.Errors;
                List<string> dummy = null;//warnings are not displayed
                if (!(propCore.SelectedObject as BaseCore).CheckValues(ref errors, ref dummy))
                    checkValuesArgs.Errors = errors;
            }

            if (ddDistanceMode.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Distance mode is not selected.");
        }

        /// <summary>
        /// if a new junction is created, it can be for all cells in the target or source pool
        /// </summary>
        /// <returns></returns>
        public List<JunctionBase> GetJunctions()
        {
            double? fixedDuration = null;
            if (!string.IsNullOrEmpty(eFixedDuration.Text) && double.TryParse(eFixedDuration.Text, out double fd))
                fixedDuration = fd;
            double? delay = null;
            if (!string.IsNullOrEmpty(eSynDelay.Text) && double.TryParse(eSynDelay.Text, out double sd))
                delay = sd;
            DistanceMode distanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), ddDistanceMode.Text);
            List<JunctionBase> junctions = [];
            if (junction != null)
            {
                junctions.Add(junction);
                junction.DistanceMode = distanceMode;
                junction.Core = propCore.SelectedObject as JunctionCore;
            }
            else
            {
                List<Cell> targetCells = [];
                if (targetCell != null)
                    targetCells.Add(targetCell);
                else
                    targetCells.AddRange(targetPool.GetCells());
                List<Cell> sourceCells = [];
                if (sourceCell != null)
                    sourceCells.Add(sourceCell);
                else
                    sourceCells.AddRange(sourcePool.GetCells());
                foreach (Cell sc in sourceCells)
                {
                    foreach (Cell tc in targetCells)
                    {
                        if ((JunctionType)ddJunctionType.SelectedItem == JunctionType.Gap)
                            junction = new GapJunction(sc, tc, ddCoreType.Text, (propCore.SelectedObject as BaseCore).Parameters, distanceMode);
                        else
                            junction = new ChemicalSynapse(sc as Neuron, tc, ddCoreType.Text, (propCore.SelectedObject as BaseCore).Parameters, distanceMode);
                        junctions.Add(junction);
                    }
                }
                junction = null;
            }
            foreach (JunctionBase jnc in junctions)
            {

                jnc.Delay_ms = delay;
                jnc.FixedDuration_ms = fixedDuration;
                jnc.TimeLine_ms = timeLineControl.GetTimeLine();
                jnc.Active = cbActive.Checked;
            }
            return junctions;
        }

        private void JunctionControl_SizeChanged(object sender, EventArgs e)
        {
            Refresh();//the drop down boxes do not refresh properly otherwise
        }

    }
}
