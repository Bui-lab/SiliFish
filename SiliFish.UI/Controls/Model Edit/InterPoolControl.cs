using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
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
        private bool skipCoreTypeChange = false;

        public void CheckValues(object sender, EventArgs args)
        {
            CheckValuesArgs checkValuesArgs = args as CheckValuesArgs;
            checkValuesArgs.Errors = [];
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
                {
                    if (ddCoreType.SelectedIndex < 0)
                        checkValuesArgs.Errors.Add("Synapse type not defined.");
                }
            }
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
                double EReversal = 0;
                switch (pool.NTMode)
                {
                    case NeuronClass.Glycinergic:
                        EReversal = settings.E_gly;
                        break;
                    case NeuronClass.GABAergic:
                        EReversal = settings.E_gaba;
                        break;
                    case NeuronClass.Glutamatergic:
                        EReversal = settings.E_glu;
                        break;
                    case NeuronClass.Cholinergic:
                        EReversal = settings.E_ach;
                        break;
                    default:
                        break;
                }
                interPoolTemplate.SetParameter("ERev", new Constant_NoDistribution(EReversal));
                ParamDictToGrid();
            }
            interPoolTemplate.SourcePool = ddSourcePool.SelectedItem is CellPoolTemplate cpt ? cpt.CellGroup : "";
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
            interPoolTemplate.TargetPool = ddTargetPool.SelectedItem is CellPoolTemplate cpt ? cpt.CellGroup : "";
            UpdateName();
        }
        private void ParamDictToGrid()
        {
            dgDynamics.WriteToGrid(interPoolTemplate.Parameters);
        }

        private Dictionary<string, Distribution> GridToParamDict()
        {
            return dgDynamics.ReadFromGrid();
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
            if (interPoolTemplate == null || !ddConnectionType.Focused) return;
            interPoolTemplate.ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), ddConnectionType.Text);
            string lastSelection = ddCoreType.Text;
            ddCoreType.Items.Clear();
            ddCoreType.Items.AddRange(interPoolTemplate.ConnectionType == ConnectionType.Gap ?
                ElecSynapseCore.GetSynapseTypes().ToArray() :
                [.. ChemSynapseCore.GetSynapseTypes()]);
            ddCoreType.Text = lastSelection;
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

        public InterPoolControl(ModelSettings settings)
        {
            InitializeComponent();
            ddAxonReachMode.DataSource = Enum.GetNames(typeof(AxonReachMode));
            ddDistanceMode.DataSource = Enum.GetNames(typeof(DistanceMode));
            //ddConnectionType is manually loaded as not all of them are displayed
            ddCoreType.Items.AddRange([.. ChemSynapseCore.GetSynapseTypes()]);
            ddCoreType.Items.AddRange([.. ElecSynapseCore.GetSynapseTypes()]);

            ddConnectionType.Items.Clear();
            ddConnectionType.Items.Add(ConnectionType.Gap);
            ddConnectionType.Items.Add(ConnectionType.Synapse);
            ddConnectionType.Items.Add(ConnectionType.NMJ);
            ddReachMode.SelectedIndex = 0;

            this.settings = settings;
        }

        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return string.Format("{0}-->{1} [{2}]{3}", ddSourcePool.Text, ddTargetPool.Text, ddConnectionType.Text, activeStatus);
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
        public void WriteDataToControl(List<CellPoolTemplate> pools, InterPoolTemplate interPoolTemplate)
        {
            ddSourcePool.Items.Clear();
            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0)
                return;
            ddSourcePool.Items.AddRange([.. pools]);
            ddTargetPool.Items.AddRange([.. pools]);
            if (interPoolTemplate != null)
            {
                this.interPoolTemplate = interPoolTemplate;
                ConnectionType jnc = interPoolTemplate.ConnectionType;
                SetDropDownValue(ddSourcePool, interPoolTemplate.SourcePool);
                SetDropDownValue(ddTargetPool, interPoolTemplate.TargetPool);
                ddAxonReachMode.Text = interPoolTemplate.AxonReachMode.ToString();
                ddReachMode.SelectedIndex = interPoolTemplate.CellReach.SomiteBased ? 0 : 1;
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

                eFixedDuration.Text = interPoolTemplate.FixedDuration_ms?.ToString() ?? "";
                eSynDelay.Text = interPoolTemplate.Delay_ms?.ToString() ?? "";
                if (interPoolTemplate.Parameters != null)
                {
                    skipCoreTypeChange = true;
                    ddCoreType.Text = interPoolTemplate.CoreType?.ToString();
                    skipCoreTypeChange = false;
                }
                cbActive.Checked = interPoolTemplate.Active;
                timeLineControl.SetTimeLine(interPoolTemplate.TimeLine_ms);
            }
            else
                this.interPoolTemplate = new();
            ParamDictToGrid();
        }

        public InterPoolTemplate ReadDataFromControl()
        {
            interPoolTemplate.SourcePool = GetDropDownValue(ddSourcePool);
            interPoolTemplate.TargetPool = GetDropDownValue(ddTargetPool);
            bool somiteBased = ddReachMode.SelectedIndex == 0;
            double? fixedDuration = null;
            if (!string.IsNullOrEmpty(eFixedDuration.Text) && double.TryParse(eFixedDuration.Text, out double fd))
                fixedDuration = fd;
            double? synDelay = null;
            if (!string.IsNullOrEmpty(eSynDelay.Text) && double.TryParse(eSynDelay.Text, out double sd))
                synDelay = sd;
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
                SomiteBased = somiteBased
            };

            interPoolTemplate.AxonReachMode = (AxonReachMode)Enum.Parse(typeof(AxonReachMode), ddAxonReachMode.Text);
            interPoolTemplate.ConnectionType = ddConnectionType.SelectedItem != null ? (ConnectionType)ddConnectionType.SelectedItem : ConnectionType.NotSet;
            interPoolTemplate.DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), ddDistanceMode.Text);
            interPoolTemplate.FixedDuration_ms = fixedDuration;
            interPoolTemplate.Delay_ms = synDelay;
            interPoolTemplate.Name = eName.Text;
            interPoolTemplate.Description = eDescription.Text;
            interPoolTemplate.Probability = (double)numProbability.Value;

            interPoolTemplate.CoreType = ddCoreType.Text;
            interPoolTemplate.Parameters = GridToParamDict();

            interPoolTemplate.Active = cbActive.Checked;
            interPoolTemplate.TimeLine_ms = timeLineControl.GetTimeLine();

            return interPoolTemplate;
        }

        private void splitContainerMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainerMain.Panel1.Refresh();//the drop down boxes do not refresh properly otherwise - still doesn;t work
        }

        private void ddCoreType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipCoreTypeChange) return;
            string coreType = ddCoreType.Text;
            interPoolTemplate.Parameters = interPoolTemplate.ConnectionType == ConnectionType.Gap ?
                ElecSynapseCore.GetParameters(coreType) :
                ChemSynapseCore.GetParameters(coreType);
            ParamDictToGrid();
        }

        private void dgDynamics_Leave(object sender, EventArgs e)
        {
            if (interPoolTemplate == null) return;
            interPoolTemplate.Parameters = GridToParamDict();
        }

        private void splitContainerMain_Panel1_SizeChanged(object sender, EventArgs e)
        {
            splitContainerMain.Panel1.Refresh();//the drop down boxes do not refresh properly otherwise - still doesn;t work
        }

        private void ddReachMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lUoD1.Text = ddReachMode.Text;
            lUoD2.Text = ddReachMode.Text;
            bool somiteBased = ddReachMode.SelectedIndex == 0;
            if (somiteBased)
            {
                toolTip1.SetToolTip(numMinAscReach, "Set 0 for within somite projections");
                toolTip1.SetToolTip(numMinDescReach, "Set 0 for within somite projections");
                toolTip1.SetToolTip(lMinReach, "Set 0 for within somite projections");
                //a single somite of the model can represent multiple somites of the fish
                //therefore, decimal places are allowed so that no data is lost
                numMinAscReach.DecimalPlaces = numMinDescReach.DecimalPlaces =
                    numMaxAscReach.DecimalPlaces = numMaxDescReach.DecimalPlaces = 1;
                numMinAscReach.Increment = numMinDescReach.Increment =
                    numMaxAscReach.Increment = numMaxDescReach.Increment = 1;
            }
            else
            {
                toolTip1.SetToolTip(numMinAscReach, "");
                toolTip1.SetToolTip(numMinDescReach, "");
                toolTip1.SetToolTip(lMinReach, "");
                numMinAscReach.DecimalPlaces = numMinDescReach.DecimalPlaces =
                    numMaxAscReach.DecimalPlaces = numMaxDescReach.DecimalPlaces = 3;
                numMinAscReach.Increment = numMinDescReach.Increment =
                    numMaxAscReach.Increment = numMaxDescReach.Increment = (decimal)0.1;
            }

        }
    }
}
