using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits;

namespace SiliFish.UI.Controls
{
    public partial class InterPoolControl : UserControl
    {
        private event EventHandler interPoolChanged;
        public event EventHandler InterPoolChanged { add => interPoolChanged += value; remove => interPoolChanged -= value; }

        private bool autoGenerateName = true;
        private InterPoolTemplate interPoolTemplate;
        private Dictionary<string, object> Parameters;
        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return String.Format("{0}-->{1} [{2}]{3}", ddSourcePool.Text, ddTargetPool.Text, ddConnectionType.Text, activeStatus);
        }
        public InterPoolControl()
        {
            InitializeComponent();
            ddAxonReachMode.DataSource = Enum.GetNames(typeof(AxonReachMode));
            ddDistanceMode.DataSource = Enum.GetNames(typeof(DistanceMode));
            //ddConnectionType is manually loaded as not all of them are displayed
            ddConnectionType.Items.Clear();
            ddConnectionType.Items.Add(ConnectionType.Gap);
            ddConnectionType.Items.Add(ConnectionType.Synapse);
            ddConnectionType.Items.Add(ConnectionType.NMJ);
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
        private void UpdateName()
        {
            if (interPoolTemplate == null) return;
            if (autoGenerateName)
                eName.Text = interPoolTemplate.GeneratedName();
        }
        private void ddSourcePool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillConnectionTypes();
            if (!ddSourcePool.Focused)
                return;
            if (ddSourcePool.SelectedItem is CellPoolTemplate pool)
            {
                if (pool.Parameters != null && pool.Parameters.Any())
                {
                    numVthreshold.Value = pool.Parameters.Read("Izhikevich_9P.V_t", numVthreshold.Value);
                }
                if (Parameters != null && Parameters.Any())
                {
                    switch (pool.NTMode)
                    {
                        case NeuronClass.Glycinergic:
                            numEReversal.Value = Parameters.Read("Dynamic.E_gly", numEReversal.Value);
                            break;
                        case NeuronClass.GABAergic:
                            numEReversal.Value = Parameters.Read("Dynamic.E_gaba", numEReversal.Value);
                            break;
                        case NeuronClass.Glutamatergic:
                            numEReversal.Value = Parameters.Read("Dynamic.E_glu", numEReversal.Value);
                            break;
                        case NeuronClass.Cholinergic:
                            numEReversal.Value = Parameters.Read("Dynamic.E_ach", numEReversal.Value);
                            break;
                        default:
                            break;
                    }
                }
            }
            interPoolTemplate.PoolSource = ddSourcePool.Text;
            UpdateName();
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddTargetPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillConnectionTypes();
            if (!ddTargetPool.Focused)
                return;
            if (ddTargetPool.SelectedItem is CellPoolTemplate pool)
            {
                if (pool.Parameters != null && pool.Parameters.Any())
                {
                    numEReversal.Value = pool.Parameters.Read("Izhikevich_9P.V_r", numEReversal.Value);
                }
            }
            interPoolTemplate.PoolTarget = ddTargetPool.Text;
            UpdateName();
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gSynapse.Visible = ddConnectionType.Text != "Gap";
            if (interPoolTemplate == null || !ddConnectionType.Focused) return;
            interPoolTemplate.ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), ddConnectionType.Text);
            UpdateName();
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddAxonReachMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateName();
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetDropDownValue(ComboBox dd, string value)
        {
            string inactive = value + " (inactive)";
            foreach (var v in dd.Items)
            {
                if (v.ToString() == value || v.ToString() == inactive)
                {
                    dd.SelectedItem = v;
                    return;
                }
            }
        }

        private string GetDropDownValue(ComboBox dd)
        {
            return dd.Text.Replace(" (inactive)", "");
        }

        public void SetInterPoolTemplate(List<CellPoolTemplate> pools, InterPoolTemplate interPool, SwimmingModelTemplate modelTemplate)
        {
            Parameters = modelTemplate.Parameters;
            ddSourcePool.Items.Clear();
            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0)
                return;
            ddSourcePool.Items.AddRange(pools.ToArray());
            ddTargetPool.Items.AddRange(pools.ToArray());
            if (interPool != null)
            {
                interPoolTemplate = interPool;
                ConnectionType jnc = interPool.ConnectionType;
                SetDropDownValue(ddSourcePool, interPool.PoolSource);
                SetDropDownValue(ddTargetPool, interPool.PoolTarget);
                ddAxonReachMode.Text = interPool.AxonReachMode.ToString();
                interPool.ConnectionType = jnc; //target pool change can initialize the junc type list and update incoming info
                ddConnectionType.Text = interPool.ConnectionType.ToString();
                ddDistanceMode.Text = interPool.DistanceMode.ToString();

                string name = interPool.GeneratedName();
                autoGenerateName = name == interPool.Name;
                eName.Text = interPool.Name;
                eDescription.Text = interPool.Description;
                cbWithinSomite.Checked = interPool.CellReach.WithinSomite;
                cbOtherSomite.Checked = interPool.CellReach.OtherSomite;
                numProbability.Value = (decimal)interPool.Probability;
                numMinReach.Value = (decimal)interPool.CellReach.MinReach;
                numMaxReach.Value = (decimal)interPool.CellReach.MaxReach;
                numAscReach.Value = (decimal)interPool.CellReach.AscendingReach;
                numDescReach.Value = (decimal)interPool.CellReach.DescendingReach;
                numLateralReach.Value = (decimal)interPool.CellReach.LateralReach;
                numMedialReach.Value = (decimal)interPool.CellReach.MedialReach;
                numDorsalReach.Value = (decimal)interPool.CellReach.DorsalReach;
                numVentralReach.Value = (decimal)interPool.CellReach.VentralReach;
                numMaxIncoming.Value = (decimal)interPool.CellReach.MaxIncoming;
                numMaxOutgoing.Value = (decimal)interPool.CellReach.MaxOutgoing;

                numConductance.Value = (decimal)interPool.CellReach.Weight;
                numDelay.Value = (decimal)interPool.CellReach.Delay_ms;
                eFixedDuration.Text = interPool.CellReach.FixedDuration_ms?.ToString() ?? "";
                if (interPool.SynapseParameters != null)
                {
                    numTauD.Value = (decimal)interPool.SynapseParameters.TauD;
                    numTauR.Value = (decimal)interPool.SynapseParameters.TauR;
                    numVthreshold.Value = (decimal)interPool.SynapseParameters.VTh;
                    numEReversal.Value = (decimal)interPool.SynapseParameters.E_rev;
                }
                cbActive.Checked = interPool.JncActive;
                timeLineControl.SetTimeLine(interPool.TimeLine_ms);
            }
            else
                interPoolTemplate = new();
        }

        public InterPoolTemplate GetInterPoolTemplate()
        {
            interPoolTemplate.PoolSource =GetDropDownValue(ddSourcePool);
            interPoolTemplate.PoolTarget = GetDropDownValue(ddTargetPool);

            double? fixedDuration = null;
            if (!string.IsNullOrEmpty(eFixedDuration.Text) && double.TryParse(eFixedDuration.Text, out double fd))
                fixedDuration = fd;
            interPoolTemplate.CellReach = new CellReach
            {
                WithinSomite = cbWithinSomite.Checked,
                OtherSomite = cbOtherSomite.Checked,
                MinReach = (double)numMinReach.Value,
                MaxReach = (double)numMaxReach.Value,
                AscendingReach = (double)numAscReach.Value,
                DescendingReach = (double)numDescReach.Value,
                LateralReach = (double)numLateralReach.Value,
                MedialReach = (double)numMedialReach.Value,
                DorsalReach = (double)numDorsalReach.Value,
                VentralReach = (double)numVentralReach.Value,
                MaxIncoming = (int)numMaxIncoming.Value,
                MaxOutgoing = (int)numMaxOutgoing.Value,
                Weight = (double)numConductance.Value,
                Delay_ms = (double)numDelay.Value,
                FixedDuration_ms = fixedDuration
            };

            interPoolTemplate.AxonReachMode = (AxonReachMode)Enum.Parse(typeof(AxonReachMode), ddAxonReachMode.Text);
            interPoolTemplate.ConnectionType = ddConnectionType.SelectedItem != null ? (ConnectionType)ddConnectionType.SelectedItem : ConnectionType.NotSet;
            interPoolTemplate.DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), ddDistanceMode.Text);
            interPoolTemplate.Name = eName.Text;
            interPoolTemplate.Description = eDescription.Text;
            interPoolTemplate.Probability = (double)numProbability.Value;

            if (interPoolTemplate.ConnectionType != ConnectionType.Gap)
            {
                interPoolTemplate.SynapseParameters = new SynapseParameters
                {
                    TauD = (double)numTauD.Value,
                    TauR = (double)numTauR.Value,
                    VTh = (double)numVthreshold.Value,
                    E_rev = (double)numEReversal.Value,
                };
            }
            interPoolTemplate.JncActive = cbActive.Checked;
            interPoolTemplate.TimeLine_ms = timeLineControl.GetTimeLine();
            return interPoolTemplate;
        }

        private void eName_Leave(object sender, EventArgs e)
        {
            autoGenerateName = string.IsNullOrEmpty(eName.Text) || eName.Text == interPoolTemplate.GeneratedName();
        }

        private void cbActive_CheckedChanged(object sender, EventArgs e)
        {
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }

        private void cbWithinSomite_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWithinSomite.Focused && !cbWithinSomite.Checked)
                cbOtherSomite.Checked = true;
        }

        private void cbOtherSomite_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOtherSomite.Focused && !cbOtherSomite.Checked)
                cbWithinSomite.Checked = true;
        }
    }
}
