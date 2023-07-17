using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.UI.EventArguments;
using SiliFish.UI.Extensions;

namespace SiliFish.UI.Controls
{
    public partial class InterPoolControl : UserControl
    {
        private ModelSettings settings;
        private bool autoGenerateName = true;
        private InterPoolTemplate interPoolTemplate;
        private Dictionary<string, object> Parameters;
        private bool SomiteBased = false;

        public void CheckValues(object sender, EventArgs args)
        {
            CheckValuesArgs checkValuesArgs = args as CheckValuesArgs;
            checkValuesArgs.Errors = new();
            if (ddSourcePool.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No source pool selected.");
            if (ddTargetPool.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No target pool selected.");
            if (ddAxonReachMode.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Axon reach mode not defined.");
            if (ddConnectionType.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Connection type not defined.");
            else
            {
                ConnectionType ct = (ConnectionType)Enum.Parse(typeof(ConnectionType), ddConnectionType.Text);
                if (ct == ConnectionType.Synapse || ct == ConnectionType.NMJ)
                    checkValuesArgs.Errors.AddRange(synapseControl.CheckValues());
            }
            if ((double)numConductance.Value < GlobalSettings.Epsilon)
                checkValuesArgs.Errors.Add("Junction weight is 0. To disable a junction, use the Active field instead.");
            if (ddDistanceMode.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Distance mode is not selected.");
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

        private void LoadSelectedSourceValues()
        {
            if (ddSourcePool.SelectedItem is CellPoolTemplate pool)
            {
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
            interPoolTemplate.PoolSource = ddSourcePool.SelectedItem is CellPoolTemplate cpt ? cpt.CellGroup : "";
            UpdateName();
        }
        private void ddSourcePool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillConnectionTypes();
            if (!ddSourcePool.Focused)
                return;
            LoadSelectedSourceValues();
        }

        private void LoadSelectedTargetValues()
        {
            interPoolTemplate.PoolTarget = ddTargetPool.SelectedItem is CellPoolTemplate cpt ? cpt.CellGroup : "";
            UpdateName();
        }

        private void ddTargetPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillConnectionTypes();
            if (!ddTargetPool.Focused)
                return;
            LoadSelectedTargetValues();
        }

        private void ddConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gSynapse.Visible = ddConnectionType.Text != "Gap";
            if (interPoolTemplate == null || !ddConnectionType.Focused) return;
            interPoolTemplate.ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), ddConnectionType.Text);
            UpdateName();
        }

        private void ddAxonReachMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateName();
        }

        private static void SetDropDownValue(ComboBox dd, string value)
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

        private static string GetDropDownValue(ComboBox dd)
        {
            return dd.Text.Replace(" (inactive)", "");
        }

        private void eName_Leave(object sender, EventArgs e)
        {
            autoGenerateName = string.IsNullOrEmpty(eName.Text) || eName.Text == interPoolTemplate.GeneratedName();
        }

        private void cbAscending_CheckedChanged(object sender, EventArgs e)
        {
            numMinAscReach.Enabled = numMaxAscReach.Enabled = cbAscending.Checked;
        }

        private void cbDescending_CheckedChanged(object sender, EventArgs e)
        {
            numMinDescReach.Enabled = numMaxDescReach.Enabled = cbDescending.Checked;
        }

        public InterPoolControl(bool somiteBased, ModelSettings settings)
        {
            InitializeComponent();
            SomiteBased = somiteBased;
            ddAxonReachMode.DataSource = Enum.GetNames(typeof(AxonReachMode));
            ddDistanceMode.DataSource = Enum.GetNames(typeof(DistanceMode));
            //ddConnectionType is manually loaded as not all of them are displayed
            ddConnectionType.Items.Clear();
            ddConnectionType.Items.Add(ConnectionType.Gap);
            ddConnectionType.Items.Add(ConnectionType.Synapse);
            ddConnectionType.Items.Add(ConnectionType.NMJ);
            if (SomiteBased)
            {
                lUoD1.Text = lUoD2.Text = "somites";
                toolTip1.SetToolTip(numMinAscReach, "Set 0 for within somite projections");
                toolTip1.SetToolTip(numMinDescReach, "Set 0 for within somite projections");
                toolTip1.SetToolTip(lMinReach, "Set 0 for within somite projections");
                numMinAscReach.DecimalPlaces = numMinDescReach.DecimalPlaces =
                    numMaxAscReach.DecimalPlaces = numMaxDescReach.DecimalPlaces = 0;
                numMinAscReach.Increment = numMinDescReach.Increment =
                    numMaxAscReach.Increment = numMaxDescReach.Increment = 1;
            }
            else
            {
                lUoD1.Text = lUoD2.Text = "";
                toolTip1.SetToolTip(numMinAscReach, "");
                toolTip1.SetToolTip(numMinDescReach, "");
                toolTip1.SetToolTip(lMinReach, "");
                numMinAscReach.DecimalPlaces = numMinDescReach.DecimalPlaces =
                    numMaxAscReach.DecimalPlaces = numMaxDescReach.DecimalPlaces = 3;
                numMinAscReach.Increment = numMinDescReach.Increment =
                    numMaxAscReach.Increment = numMaxDescReach.Increment = (decimal)0.1;
            }

            this.settings = settings;
        }

        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return String.Format("{0}-->{1} [{2}]{3}", ddSourcePool.Text, ddTargetPool.Text, ddConnectionType.Text, activeStatus);
        }

        public void SetSourcePool(CellPoolTemplate pool)
        {
            SetDropDownValue(ddSourcePool, pool.CellGroup);
            LoadSelectedSourceValues();
        }
        public void SetTargetPool(CellPoolTemplate pool)
        {
            SetDropDownValue(ddTargetPool, pool.CellGroup);
            LoadSelectedTargetValues();
        }

        public void SetAsGapJunction()
        {
            try { ddConnectionType.SelectedItem = ConnectionType.Gap; }
            catch { }
        }
        public void SetInterPoolTemplate(List<CellPoolTemplate> pools, InterPoolTemplate interPoolTemplate, ModelTemplate modelTemplate)
        {
            Parameters = modelTemplate.Parameters;
            ddSourcePool.Items.Clear();
            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0)
                return;
            ddSourcePool.Items.AddRange(pools.ToArray());
            ddTargetPool.Items.AddRange(pools.ToArray());
            if (interPoolTemplate != null)
            {
                this.interPoolTemplate = interPoolTemplate;
                ConnectionType jnc = interPoolTemplate.ConnectionType;
                SetDropDownValue(ddSourcePool, interPoolTemplate.PoolSource);
                SetDropDownValue(ddTargetPool, interPoolTemplate.PoolTarget);
                ddAxonReachMode.Text = interPoolTemplate.AxonReachMode.ToString();
                interPoolTemplate.ConnectionType = jnc; //target pool change can initialize the junc type list and update incoming info
                ddConnectionType.Text = interPoolTemplate.ConnectionType.ToString();
                ddDistanceMode.Text = interPoolTemplate.DistanceMode.ToString();

                string name = interPoolTemplate.GeneratedName();
                autoGenerateName = name == interPoolTemplate.Name;
                eName.Text = interPoolTemplate.Name;
                eDescription.Text = interPoolTemplate.Description;

                numProbability.SetValue(interPoolTemplate.Probability);
                cbAscending.Checked = interPoolTemplate.CellReach.Ascending;
                cbDescending.Checked = interPoolTemplate.CellReach.Descending;
                numMinAscReach.SetValue(interPoolTemplate.CellReach.MinAscReach);
                numMaxAscReach.SetValue(interPoolTemplate.CellReach.MaxAscReach);
                numMinDescReach.SetValue(interPoolTemplate.CellReach.MinDescReach);
                numMaxDescReach.SetValue(interPoolTemplate.CellReach.MaxDescReach);
                numMaxIncoming.SetValue(interPoolTemplate.CellReach.MaxIncoming);
                numMaxOutgoing.SetValue(interPoolTemplate.CellReach.MaxOutgoing);

                numConductance.SetValue(interPoolTemplate.Weight);
                numDelay.SetValue(interPoolTemplate.Delay_ms);
                eFixedDuration.Text = interPoolTemplate.FixedDuration_ms?.ToString() ?? "";
                if (interPoolTemplate.SynapseParameters != null)
                {
                    synapseControl.SetSynapseParameters(interPoolTemplate.SynapseParameters);
                }
                cbActive.Checked = interPoolTemplate.Active;
                timeLineControl.SetTimeLine(interPoolTemplate.TimeLine_ms);
            }
            else
                this.interPoolTemplate = new();
            attachmentList.SetAttachments(this.interPoolTemplate.Attachments);
        }

        public InterPoolTemplate GetInterPoolTemplate()
        {
            interPoolTemplate.PoolSource = GetDropDownValue(ddSourcePool);
            interPoolTemplate.PoolTarget = GetDropDownValue(ddTargetPool);

            double? fixedDuration = null;
            if (!string.IsNullOrEmpty(eFixedDuration.Text) && double.TryParse(eFixedDuration.Text, out double fd))
                fixedDuration = fd;
            interPoolTemplate.CellReach = new CellReach
            {
                Ascending = cbAscending.Checked,
                Descending = cbDescending.Checked,
                MinAscReach = (double)numMinAscReach.Value,
                MaxAscReach = (double)numMaxAscReach.Value,
                MinDescReach = (double)numMinDescReach.Value,
                MaxDescReach = (double)numMaxDescReach.Value,
                MaxIncoming = (int)numMaxIncoming.Value,
                MaxOutgoing = (int)numMaxOutgoing.Value,
                SomiteBased = SomiteBased
            };

            interPoolTemplate.AxonReachMode = (AxonReachMode)Enum.Parse(typeof(AxonReachMode), ddAxonReachMode.Text);
            interPoolTemplate.ConnectionType = ddConnectionType.SelectedItem != null ? (ConnectionType)ddConnectionType.SelectedItem : ConnectionType.NotSet;
            interPoolTemplate.DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), ddDistanceMode.Text);
            interPoolTemplate.Weight = (double)numConductance.Value;
            interPoolTemplate.Delay_ms = (double)numDelay.Value;
            interPoolTemplate.FixedDuration_ms = fixedDuration;
            interPoolTemplate.Name = eName.Text;
            interPoolTemplate.Description = eDescription.Text;
            interPoolTemplate.Probability = (double)numProbability.Value;

            if (interPoolTemplate.ConnectionType != ConnectionType.Gap)
            {
                interPoolTemplate.SynapseParameters = synapseControl.GetSynapseParameters();
            }
            interPoolTemplate.Active = cbActive.Checked;
            interPoolTemplate.TimeLine_ms = timeLineControl.GetTimeLine();
            interPoolTemplate.Attachments = attachmentList.GetAttachments();

            return interPoolTemplate;
        }

        private void splitContainerMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainerMain.Panel1.Refresh();//the drop down boxes do not refresh properly otherwise
        }
    }
}
