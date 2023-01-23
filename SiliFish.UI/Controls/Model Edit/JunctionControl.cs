using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.UI.Extensions;

namespace SiliFish.UI.Controls
{
    public partial class JunctionControl : UserControl
    {
        private Settings settings;
        public event EventHandler JunctionChanged;

        private JunctionBase Junction;
        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return String.Format("{0}-->{1} [{2}]{3}", ddSourcePool.Text, ddTargetPool.Text, ddConnectionType.Text, activeStatus);
        }
        public JunctionControl(Settings settings)
        {
            InitializeComponent();
            ddDistanceMode.DataSource = Enum.GetNames(typeof(DistanceMode));
            //ddConnectionType is manually loaded as not all of them are displayed
            ddConnectionType.Items.Clear();
            ddConnectionType.Items.Add(ConnectionType.Gap);
            ddConnectionType.Items.Add(ConnectionType.Synapse);
            ddConnectionType.Items.Add(ConnectionType.NMJ);
            this.settings = settings;
        }

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

        private void ddSourcePool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillConnectionTypes();
            if (!ddSourcePool.Focused)
                return;
            if (ddSourcePool.SelectedItem is CellPoolTemplate pool)
            {
                if (pool.VThreshold != null)
                {
                    synapseControl.VThreshold = (double)pool.VThreshold;
                }
                    switch (pool.NTMode)
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
            //TODO junction.PoolSource = ddSourcePool.Text;
            JunctionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddTargetPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillConnectionTypes();
            if (!ddTargetPool.Focused)
                return;
            if (ddTargetPool.SelectedItem is CellPoolTemplate pool)
            {
                if (pool.VReversal != null)
                {
                    synapseControl.EReversal= (double)pool.VReversal;
                }
            }
            //TODO junction.PoolTarget = ddTargetPool.Text;
            JunctionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gSynapse.Visible = ddConnectionType.Text != "Gap";
            //TODO if (junction == null || !ddConnectionType.Focused) return;
            //TODO junction.ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), ddConnectionType.Text);
            JunctionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetJunction(JunctionBase junction, RunningModel model, bool newJunc)
        {
            Junction = junction;
            ddSourcePool.Items.Clear();
            ddTargetPool.Items.Clear();
            ddSourceCell.Items.Clear();
            ddTargetCell.Items.Clear();

            Cell precell =null;
            Cell postcell=null;
            if (junction is GapJunction gapJunction)
            {
                precell = gapJunction.Cell1;
                postcell = gapJunction.Cell2;
            }
            else if (junction is ChemicalSynapse syn)
            {
                precell = syn.PreNeuron;
                postcell = syn.PostCell;
                //TODO synapseControl.SetSynapseParameters(syn.Core.GetSynapseParameters);
            }
            if (newJunc)
            {
                CellPool prepool = precell?.CellPool;
                ddSourcePool.Items.AddRange(model.CellPools.Select(cp => cp.ID).ToArray());
                if (precell != null)
                    ddSourcePool.Text = precell.CellPool.ID;
                else
                {
                    ddSourcePool.SelectedIndex = 0;
                    prepool = model.CellPools.FirstOrDefault(cp => cp.ID == ddSourcePool.Text);
                }
                if (prepool != null)
                {
                    ddSourceCell.Items.AddRange(prepool.GetCells().Select(c => c.ID).ToArray());
                }


                CellPool postpool = postcell?.CellPool;
                ddTargetPool.Items.AddRange(model.CellPools.Select(cp => cp.ID).ToArray());
                if (postcell != null)
                    ddTargetPool.Text = postcell.CellPool.ID;
                else
                {
                    ddTargetPool.SelectedIndex = 0;
                    postpool = model.CellPools.FirstOrDefault(cp => cp.ID == ddTargetPool.Text);
                }
                if (postpool != null)
                {
                    ddTargetCell.Items.AddRange(postpool.GetCells().Select(c => c.ID).ToArray());
                }
            }
            else if (Junction != null)
            {
                CellPool prepool = precell.CellPool;
                ddSourcePool.Items.Add(prepool.ID);
                ddSourcePool.Enabled = false;
                ddSourcePool.Text = precell.CellPool.ID;
                ddSourceCell.Enabled = false;


                CellPool postpool = postcell?.CellPool;
                ddTargetPool.Items.AddRange(model.CellPools.Select(cp => cp.ID).ToArray());
                if (postcell != null)
                    ddTargetPool.Text = postcell.CellPool.ID;
                else
                {
                    ddTargetPool.SelectedIndex = 0;
                    postpool = model.CellPools.FirstOrDefault(cp => cp.ID == ddTargetPool.Text);
                }
                if (postpool != null)
                {
                    ddTargetCell.Items.AddRange(postpool.GetCells().Select(c => c.ID).ToArray());
                }
                double conductance = junction is GapJunction gj ? gj.Conductance : (junction as ChemicalSynapse).Conductance;
                numConductance.SetValue(conductance);
                double delay = junction is GapJunction gj2 ? gj2.Delay: (junction as ChemicalSynapse).Delay;
                numDelay.SetValue(delay);
                int duration = junction is GapJunction gj3 ? gj3.Duration: (junction as ChemicalSynapse).Duration;
                eFixedDuration.Text = duration.ToString();
                
                cbActive.Checked = junction.Active;
                timeLineControl.SetTimeLine(junction.TimeLine_ms);
            }
        }

        public JunctionBase GetJunction()
        {
            /*junction.PoolSource = GetDropDownValue(ddSourcePool);
            junction.PoolTarget = GetDropDownValue(ddTargetPool);

            double? fixedDuration = null;
            if (!string.IsNullOrEmpty(eFixedDuration.Text) && double.TryParse(eFixedDuration.Text, out double fd))
                fixedDuration = fd;
            junction.CellReach = new CellReach
            {
                Weight = (double)numConductance.Value,
                Delay_ms = (double)numDelay.Value,
                FixedDuration_ms = fixedDuration
            };

            junction.DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), ddDistanceMode.Text);
            junction.ConnectionType = ddConnectionType.SelectedItem != null ? (ConnectionType)ddConnectionType.SelectedItem : ConnectionType.NotSet;
            
            if (junction.ConnectionType != ConnectionType.Gap)
            {
                junction.SynapseParameters = synapseControl.GetSynapseParameters();
            }
            junction.JncActive = cbActive.Checked;
            junction.TimeLine_ms = timeLineControl.GetTimeLine();
            junction.Attachments = attachmentList.GetAttachments();
            */
            return Junction;
        }

        private void cbActive_CheckedChanged(object sender, EventArgs e)
        {
            JunctionChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
