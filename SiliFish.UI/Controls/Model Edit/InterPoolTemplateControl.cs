using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.Repositories;
using SiliFish.UI.EventArguments;
using SiliFish.UI.Extensions;
using System.Windows.Forms;
using static OfficeOpenXml.ExcelErrorValue;
using Extensions;

namespace SiliFish.UI.Controls
{
    public partial class InterPoolTemplateControl : UserControl
    {
        private ModelSettings settings;
        private ModelDimensions dimensions;
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
            if (ddJunctionType.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Connection type not defined.");
            else
            {
                JunctionType ct = (JunctionType)Enum.Parse(typeof(JunctionType), ddJunctionType.Text);
                if (ct == JunctionType.Synapse || ct == JunctionType.NMJ)
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
                    ddJunctionType.Items.Clear();
                    ddJunctionType.Items.Add(JunctionType.Gap);
                    ddJunctionType.SelectedItem = JunctionType.Gap;
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
            ReviewConductanceMatrix();
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
            ReviewConductanceMatrix();
        }

        private void ReviewConductanceMatrix()
        {
            if (ddSourcePool.SelectedItem is CellPoolTemplate source
                && ddTargetPool.SelectedItem is CellPoolTemplate target)
            {
                string preRow = source.BodyLocation == BodyLocation.SupraSpinal ? "Cell" : "Somite";
                string preColumn = target.BodyLocation == BodyLocation.SupraSpinal ? "Cell" : "Somite";
                dgConductanceMatrix.RowCount = source.BodyLocation == BodyLocation.SupraSpinal ?
                    source.NumOfCells : dimensions.NumberOfSomites;
                dgConductanceMatrix.ColumnCount = target.BodyLocation == BodyLocation.SupraSpinal ?
                    target.NumOfCells : dimensions.NumberOfSomites;
                foreach (DataGridViewRow row in dgConductanceMatrix.Rows)
                    row.HeaderCell.Value = $"{preRow} {row.Index + 1}";
                foreach (DataGridViewColumn column in dgConductanceMatrix.Columns)
                    column.HeaderCell.Value = $"{preColumn} {column.Index + 1}";
            }
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

        private void ddJunctionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (interPoolTemplate == null || !ddJunctionType.Focused) return;
            interPoolTemplate.JunctionType = (JunctionType)Enum.Parse(typeof(JunctionType), ddJunctionType.Text);
            string lastSelection = ddCoreType.Text;
            ddCoreType.Items.Clear();
            ddCoreType.Items.AddRange(interPoolTemplate.JunctionType == JunctionType.Gap ?
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

        public InterPoolTemplateControl(ModelSettings settings, ModelDimensions dimensions)
        {
            InitializeComponent();
            ddAxonReachMode.DataSource = Enum.GetNames(typeof(AxonReachMode));
            ddDistanceMode.DataSource = Enum.GetNames(typeof(DistanceMode));
            //ddConnectionType is manually loaded as not all of them are displayed
            ddCoreType.Items.AddRange([.. ChemSynapseCore.GetSynapseTypes()]);
            ddCoreType.Items.AddRange([.. ElecSynapseCore.GetSynapseTypes()]);

            ddJunctionType.Items.Clear();
            ddJunctionType.Items.Add(JunctionType.Gap);
            ddJunctionType.Items.Add(JunctionType.Synapse);
            ddJunctionType.Items.Add(JunctionType.NMJ);
            ddReachMode.SelectedIndex = 0;

            this.settings = settings;
            this.dimensions = dimensions;
        }

        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return string.Format("{0}-->{1} [{2}]{3}", ddSourcePool.Text, ddTargetPool.Text, ddJunctionType.Text, activeStatus);
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
            try { ddJunctionType.SelectedItem = JunctionType.Gap; }
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
                JunctionType jnc = interPoolTemplate.JunctionType;
                SetDropDownValue(ddSourcePool, interPoolTemplate.SourcePool);
                SetDropDownValue(ddTargetPool, interPoolTemplate.TargetPool);
                ddAxonReachMode.Text = interPoolTemplate.AxonReachMode.ToString();
                ddReachMode.SelectedIndex = interPoolTemplate.CellReach.SomiteBased ? 0 : 1;
                interPoolTemplate.JunctionType = jnc; //target pool change can initialize the junc type list and update incoming info
                ddJunctionType.Text = interPoolTemplate.JunctionType.ToString();
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
            ReviewConductanceMatrix();
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
            interPoolTemplate.JunctionType = ddJunctionType.SelectedItem != null ? (JunctionType)ddJunctionType.SelectedItem : JunctionType.NotSet;
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
            interPoolTemplate.Parameters = interPoolTemplate.JunctionType == JunctionType.Gap ?
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

        private void linkLoadMatrix_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileCSV.Title = "Conductance matrix import";
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileCSV.FileName;
                string[] lines = FileUtil.ReadLinesFromFile(filename);
                if (lines.Length <= 1) return;
                for (int i = 0; i < dgConductanceMatrix.RowCount; i++)
                {
                    for (int j = 0; j < dgConductanceMatrix.ColumnCount; j++)
                        dgConductanceMatrix[j, i].Value = "";
                }
                List<string> values = [.. lines[0].Split(',')];
                int startIndex = 0;
                if (values.Any(v => !double.TryParse(v, out _)))//column names
                    startIndex = 1;
                for (int i = 0; i < dgConductanceMatrix.RowCount; i++)
                {
                    if (startIndex + i >= lines.Length)
                        break;
                    values = [.. lines[startIndex + i].Split(',')];
                    for (int j = 0; j < dgConductanceMatrix.ColumnCount; j++)
                    {
                        if (j >= values.Count)
                            break;
                        if (double.TryParse(values[j], out double d))
                            dgConductanceMatrix[j, i].Value = d;
                    }
                }

            }
        }

        private void linkSaveMatrix_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                List<string> ColumnNames = dgConductanceMatrix.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText).ToList();
                List<List<string>> Values = [];
                foreach (DataGridViewRow row in dgConductanceMatrix.Rows)
                {
                    Values.Add(row.Cells.Cast<DataGridViewCell>().Select(c => c.Value.ToString()).ToList());
                }
                FileUtil.SaveToCSVFile(saveFileCSV.FileName, ColumnNames, Values);
            }
        }

        private void linkClearMatrix_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow row in dgConductanceMatrix.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = null;
                }
            }
        }
    }
}
