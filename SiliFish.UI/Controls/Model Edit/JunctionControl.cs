using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.UI.Extensions;
using System.Data;

namespace SiliFish.UI.Controls
{
    public partial class JunctionControl : UserControl
    {
        private readonly ModelSettings settings = null;
        public event EventHandler JunctionChanged;

        private JunctionBase junction;
        private RunningModel Model;
        private CellPool sourcePool, targetPool;
        private Cell sourceCell;
        public Cell TargetCell;

        private void FillConnectionTypes()
        {
            if (ddSourcePool.SelectedItem is CellPoolTemplate source
                && ddTargetPool.SelectedItem is CellPoolTemplate target)
            {
                if (source.CellType == CellType.MuscleCell)
                {
                    ddConnectionType.Items.Clear();
                    ddConnectionType.Items.Add(ConnectionType.Gap);
                    ddConnectionType.SelectedItem = ConnectionType.Gap;
                }
                else //Neuron
                {
                    ConnectionType prevType = ConnectionType.NotSet;
                    if (ddConnectionType.SelectedItem != null)
                        prevType = (ConnectionType)ddConnectionType.SelectedItem;
                    ddConnectionType.Items.Clear();
                    ddConnectionType.Items.Add(ConnectionType.Gap);
                    if (target.CellType == CellType.MuscleCell)
                        ddConnectionType.Items.Add(ConnectionType.NMJ);
                    else
                        ddConnectionType.Items.Add(ConnectionType.Synapse);
                    if (ddConnectionType.Items.Contains(prevType))
                        ddConnectionType.SelectedItem = prevType;
                }
            }
        }

        private static void FillCells(ComboBox dd, CellPool pool)
        {
            dd.Items.Clear();
            if (pool.GetCells().Any())
            {
                dd.Items.AddRange(pool.GetCells().ToArray());
                dd.SelectedIndex = 0;
            }
        }
        private void ddSourcePool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillConnectionTypes();
            if (!ddSourcePool.Focused)
                return;
            if (ddSourcePool.SelectedItem is CellPool pool)
            {
                sourcePool = pool;
                sourceCell = null;
                FillCells(ddSourceCell, sourcePool);

                if (pool.VThreshold != null)
                {
                    synapseControl.VThreshold = (double)pool.VThreshold;
                }
                switch (pool.NTMode)//fill default values, based on the NT
                {
                    case NeuronClass.Glycinergic:
                        synapseControl.EReversal = settings.E_gly;
                        break;
                    case NeuronClass.GABAergic:
                        synapseControl.EReversal = settings.E_gaba;
                        break;
                    case NeuronClass.Glutamatergic:
                        synapseControl.EReversal = settings.E_glu;
                        break;
                    case NeuronClass.Cholinergic:
                        synapseControl.EReversal = settings.E_ach;
                        break;
                    default:
                        break;
                }
            }
            JunctionChanged?.Invoke(this, EventArgs.Empty);
        }
        private void ddSourceCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSourceCell.SelectedItem is Cell cell)
                sourceCell = cell;
            else
                sourceCell = null;
        }

        private void ddTargetPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillConnectionTypes();
            if (!ddTargetPool.Focused)
                return;
            if (ddTargetPool.SelectedItem is CellPool pool)
            {
                targetPool = pool;
                TargetCell = null;
                FillCells(ddTargetCell, targetPool);
                if (pool.VReversal != null)//update from the Vrev of the target
                {
                    synapseControl.EReversal= (double)pool.VReversal;
                }
            }
            JunctionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddTargetCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddTargetCell.SelectedItem is Cell cell)
                TargetCell = cell;
            else
                TargetCell = null;
        }

        private void ddConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gSynapse.Visible = ddConnectionType.Text != "Gap";
            if (junction == null || !ddConnectionType.Focused) return;
            JunctionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetJunction(JunctionBase junction, bool newJunc)
        {
            this.junction = junction;
            if (junction is GapJunction gapJunction)
            {
                sourceCell = gapJunction.Cell1;
                TargetCell = gapJunction.Cell2;
                numConductance.SetValue(gapJunction.Weight);
                numDelay.SetValue(gapJunction.Delay_ms);
                eFixedDuration.Text = gapJunction.FixedDuration_ms.ToString();
                ddConnectionType.SelectedItem = ConnectionType.Gap;
            }
            else if (junction is ChemicalSynapse syn)
            {
                sourceCell = syn.PreNeuron;
                TargetCell = syn.PostCell;
                ddConnectionType.SelectedItem = TargetCell is MuscleCell ? ConnectionType.NMJ : ConnectionType.Synapse;
                synapseControl.SetSynapseParameters(syn.SynapseParameters);
                numConductance.SetValue(syn.Weight);
                numDelay.SetValue(syn.Delay_ms);
                eFixedDuration.Text = syn.FixedDuration_ms.ToString();
            }
            if (this.junction != null)
            {
                sourcePool = sourceCell.CellPool;
                targetPool = TargetCell.CellPool;

                Cell backup = sourceCell;//cleared when dropdowns are reset
                ddSourcePool.SelectedItem = sourcePool;
                FillCells(ddSourceCell, sourcePool);
                sourceCell = backup;
                ddSourceCell.SelectedItem = sourceCell;
                backup = TargetCell;
                ddTargetPool.SelectedItem = targetPool;
                FillCells(ddTargetCell, targetPool);
                TargetCell = backup;
                ddTargetCell.SelectedItem = TargetCell;
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
                ddConnectionType.Enabled = false;
            }
        }




        private void cbActive_CheckedChanged(object sender, EventArgs e)
        {
            JunctionChanged?.Invoke(this, EventArgs.Empty);
        }
                public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return $"{ddSourcePool.Text}-->{ddTargetPool.Text} [{ddConnectionType.Text}]{activeStatus}";
        }
        public JunctionControl(RunningModel model)
        {
            InitializeComponent();
            Model = model;
            settings = model.Settings;

            ddDistanceMode.DataSource = Enum.GetNames(typeof(DistanceMode));

            //ddConnectionType is manually loaded as not all of them are displayed
            ddConnectionType.Items.Add(ConnectionType.Gap);
            ddConnectionType.Items.Add(ConnectionType.Synapse);
            ddConnectionType.Items.Add(ConnectionType.NMJ);

            ddSourcePool.Items.AddRange(Model.CellPools.ToArray());
            ddSourcePool.SelectedIndex = -1;
            ddSourceCell.SelectedIndex = -1;

            ddTargetPool.Items.AddRange(Model.CellPools.ToArray());
            ddTargetPool.SelectedIndex = -1;
            ddTargetCell.SelectedIndex = -1;
        }
        internal void CheckValues(object sender, EventArgs args)
        {
            CheckValuesArgs checkValuesArgs = args as CheckValuesArgs;
            checkValuesArgs.Errors = new();
            if (ddSourcePool.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No source pool selected.");
            else if (ddSourceCell.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No source cell selected.");
            if (ddTargetPool.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No target pool selected.");
            else if (ddTargetCell.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No target cell selected.");
            if (ddConnectionType.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Connection type not defined.");
            if ((double)numConductance.Value < GlobalSettings.Epsilon)
                checkValuesArgs.Errors.Add("Junction weight is 0. To disable a junction, use the Active field instead.");
            if (ddDistanceMode.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Distance mode is not selected.");
            ConnectionType ct = (ConnectionType)Enum.Parse(typeof(ConnectionType), ddConnectionType.Text);
            if (ct == ConnectionType.Synapse || ct == ConnectionType.NMJ)
                checkValuesArgs.Errors.AddRange(synapseControl.CheckValues());
        }
        public JunctionBase GetJunction()
        {
            double? fixedDuration = null;
            if (!string.IsNullOrEmpty(eFixedDuration.Text) && double.TryParse(eFixedDuration.Text, out double fd))
                fixedDuration = fd;
            double delay = (double)numDelay.Value;
            double conductance = (double)numConductance.Value;
            DistanceMode distanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), ddDistanceMode.Text);
            if (junction == null)
            {
                if ((ConnectionType)ddConnectionType.SelectedItem == ConnectionType.Gap)
                {
                    junction = new GapJunction(conductance, sourceCell, TargetCell, distanceMode)
                    {
                        Delay_ms = delay,
                        FixedDuration_ms = fixedDuration
                    };
                }
                else
                {
                    junction = new ChemicalSynapse(sourceCell as Neuron, TargetCell, synapseControl.GetSynapseParameters(), conductance, distanceMode)
                    {
                        Delay_ms = delay,
                        FixedDuration_ms = fixedDuration
                    };
                }
            }
            else if (junction is GapJunction gapJunction)
            {
                gapJunction.Delay_ms = delay;
                gapJunction.FixedDuration_ms = fixedDuration;
            }
            else if (junction is ChemicalSynapse synapse)
            {
                synapse.Delay_ms = delay;
                synapse.FixedDuration_ms = fixedDuration;
            }
            junction.Active = cbActive.Checked;
            junction.TimeLine_ms = timeLineControl.GetTimeLine();
            return junction;
        }
    }
}
