using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using System.ComponentModel;
using static SiliFish.UI.Controls.DynamicsTestControl;

namespace SiliFish.UI.Controls
{
    public partial class CellPoolControl : UserControl
    {
        private static string coreUnitFileDefaultFolder;
        private event EventHandler savePool;
        public event EventHandler SavePool { add => savePool += value; remove => savePool -= value; }

        private event EventHandler loadPool;
        public event EventHandler LoadPool { add => loadPool += value; remove => loadPool -= value; }

        CellPoolTemplate poolTemplate;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private DynamicsTestControl dyncontrol = null;
        private bool SomiteBased = false;
        public CellPoolTemplate PoolTemplate
        {
            get
            {
                ReadDataFromControl();
                return poolTemplate;
            }
            set
            {
                poolTemplate = value ?? new();
                WriteDataToControl();
            }
        }
        public string JSONString;

        public string CellName { get { return eGroupName.Text; } set { eGroupName.Text = value; } }

        private bool skipCellTypeChange = false;
        private bool skipCoreTypeChange = false;
        public CellPoolControl(bool somiteBased)
        {
            InitializeComponent();
            SomiteBased = somiteBased;
            distConductionVelocity.AbsoluteEnforced = true;
            ddCoreType.Items.AddRange(CellCoreUnit.GetCoreTypes().ToArray());// fill before celltypes
            ddCellType.DataSource = Enum.GetNames(typeof(CellType));
            ddNeuronClass.DataSource = Enum.GetNames(typeof(NeuronClass));
            ddBodyPosition.DataSource = Enum.GetNames(typeof(BodyLocation));
            distConductionVelocity.SetDistribution(new Constant_NoDistribution(CurrentSettings.Settings.cv, absolute: true, angular: false, noiseStdDev: 0));
            if (SomiteBased)
            {
                cbAllSomites.Enabled = eSomiteRange.Enabled = true;
                ddSelection.DataSource = Enum.GetNames(typeof(CountingMode));
            }
            else
            {
                cbAllSomites.Enabled = eSomiteRange.Enabled = false;
                ddSelection.Items.Clear();
                ddSelection.Items.Add(CountingMode.Total.ToString());
            }
        }

        private void ddCellType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipCellTypeChange) return;
            CellType cellType = (CellType)Enum.Parse(typeof(CellType), ddCellType.Text);
            if (cellType == CellType.Neuron)
            {
                if (ddNeuronClass.Items.Count > 0)
                    ddNeuronClass.SelectedIndex = 0;
                lNeuronClass.Visible = ddNeuronClass.Visible = true;
                ddCoreType.Text = CurrentSettings.Settings.DefaultNeuronCore.GetType().ToString();
            }
            else
            {
                lNeuronClass.Visible = ddNeuronClass.Visible = false;
                ddCoreType.Text = CurrentSettings.Settings.DefaultMuscleCellCore.GetType().ToString();
            }
        }

        private void ddCoreType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipCoreTypeChange) return;
            string coreType = ddCoreType.Text;
            poolTemplate.Parameters = CellCoreUnit.GetParameters(coreType);
            ParamDictToGrid();
        }

        private void ParamDictToGrid()
        {
            dgDynamics.WriteToGrid(poolTemplate.Parameters);
        }

        private Dictionary<string, Distribution> GridToParamDict()
        {
            return dgDynamics.ReadFromGrid();
        }

        private void eGroupName_TextChanged(object sender, EventArgs e)
        {
            if (poolTemplate != null)
                poolTemplate.CellGroup = eGroupName.Text;
        }

        private void linkLoadPool_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (loadPool == null) return;
            loadPool.Invoke(this, new EventArgs());
            if (string.IsNullOrEmpty(JSONString))
                return;
            try
            {
                poolTemplate = (CellPoolTemplate)JsonUtil.ToObject(typeof(CellPoolTemplate), JSONString);
            }
            catch
            {
                MessageBox.Show("Selected file is not a valid Cell Pool Template file.");
                return;
            }
            WriteDataToControl();
        }

        private void linkSavePool_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (savePool == null) return;
            ReadDataFromControl();
            JSONString = JsonUtil.ToJson(poolTemplate);
            savePool.Invoke(this, new EventArgs());
        }

        private void ReadDataFromControl()
        {
            SagittalPlane sagPlane = SagittalPlane.Both;
            if (ddSagittalPosition.Text == "Left")
                sagPlane = SagittalPlane.Left;
            else if (ddSagittalPosition.Text == "Right")
                sagPlane = SagittalPlane.Right;
            else if (ddSagittalPosition.Text == "Left/Right")
                sagPlane = SagittalPlane.Both;
            string groupName = eGroupName.Text;
            if (poolTemplate == null)
                poolTemplate = new CellPoolTemplate();
            poolTemplate.CellGroup = groupName;
            poolTemplate.Description = eDescription.Text;
            poolTemplate.BodyLocation= (BodyLocation)Enum.Parse(typeof(BodyLocation), ddBodyPosition.Text);
            poolTemplate.PositionLeftRight = sagPlane;
            poolTemplate.ColumnIndex2D = (int)e2DColumn.Value;
            poolTemplate.NumOfCells = (int)eNumOfCells.Value;
            poolTemplate.PerSomiteOrTotal = (CountingMode)Enum.Parse(typeof(CountingMode), ddSelection.Text);
            poolTemplate.SomiteRange = SomiteBased && !cbAllSomites.Checked ? eSomiteRange.Text : "";
            poolTemplate.XDistribution = distributionX.GetDistribution();
            poolTemplate.Y_AngleDistribution = distributionY.GetDistribution();
            poolTemplate.Z_RadiusDistribution = distributionZ.GetDistribution();
            poolTemplate.CellType = (CellType)Enum.Parse(typeof(CellType), ddCellType.Text);
            poolTemplate.CoreType = ddCoreType.Text;
            poolTemplate.NTMode = (NeuronClass)Enum.Parse(typeof(NeuronClass), ddNeuronClass.Text);
            poolTemplate.Parameters = GridToParamDict();
            poolTemplate.Color = btnColor.BackColor;
            poolTemplate.Active = cbActive.Checked;
            poolTemplate.TimeLine_ms = timeLineControl.GetTimeLine();
            poolTemplate.ConductionVelocity = distConductionVelocity.GetDistribution();

            poolTemplate.Attachments = attachmentList.GetAttachments();
        }


        private void WriteDataToControl()
        {
            if (poolTemplate == null) return;

            eGroupName.Text = poolTemplate.CellGroup;
            eDescription.Text = poolTemplate.Description;
            skipCellTypeChange = true;
            ddCellType.Text = poolTemplate.CellType.ToString();
            skipCellTypeChange = false;
            skipCoreTypeChange = true;
            ddCoreType.Text = poolTemplate.CoreType?.ToString();
            skipCoreTypeChange = false;
            ddBodyPosition.Text = poolTemplate.BodyLocation.ToString();
            lNeuronClass.Visible = ddNeuronClass.Visible = poolTemplate.CellType == CellType.Neuron;
            ddNeuronClass.Text = poolTemplate.NTMode.ToString();
            if (poolTemplate.PositionLeftRight == SagittalPlane.Both)
                ddSagittalPosition.Text = "Left/Right";
            else if (poolTemplate.PositionLeftRight == SagittalPlane.Left)
                ddSagittalPosition.Text = "Left";
            else if (poolTemplate.PositionLeftRight == SagittalPlane.Right)
                ddSagittalPosition.Text = "Right";
            e2DColumn.Value = Math.Max(poolTemplate.ColumnIndex2D, e2DColumn.Minimum);
            eNumOfCells.Value = poolTemplate.NumOfCells;
            ddSelection.Text = poolTemplate.PerSomiteOrTotal.ToString();
            if (string.IsNullOrEmpty(poolTemplate.SomiteRange))
            {
                cbAllSomites.Checked = true;
            }
            else
            {
                cbAllSomites.Checked = false;
                eSomiteRange.Text = poolTemplate.SomiteRange;
            }

            btnColor.BackColor = poolTemplate.Color;

            distributionX.SetDistribution((Distribution)poolTemplate.XDistribution);
            distributionY.SetDistribution((Distribution)poolTemplate.Y_AngleDistribution);
            distributionZ.SetDistribution((Distribution)poolTemplate.Z_RadiusDistribution);
            rbYZAngular.Checked = distributionY.GetDistribution() != null && distributionY.GetDistribution().Angular;

            cbActive.Checked = poolTemplate.Active;
            timeLineControl.SetTimeLine(poolTemplate.TimeLine_ms);
            distConductionVelocity.SetDistribution(poolTemplate.ConductionVelocity as Distribution);
            ParamDictToGrid();

            attachmentList.SetAttachments(poolTemplate.Attachments);
        }

        private void rbYZAngular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbYZAngular.Checked)
            {
                lYAxis.Text = "Angle (Dorsal[0]->Ventral[180])";
                distributionY.Angular = true;
                lZAxis.Text = "Radius";
            }
            else
            {
                lYAxis.Text = "Y - Axis (Medial->Lateral)";
                distributionY.Angular = false;
                lZAxis.Text = "Z - Axis (Dorsal->Ventral)";
            }
        }
        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = btnColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnColor.BackColor = colorDialog.Color;
            }
        }
        private void linkLoadCoreUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileJson.InitialDirectory = coreUnitFileDefaultFolder;
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                coreUnitFileDefaultFolder = Path.GetDirectoryName(openFileJson.FileName);

                string JSONString = FileUtil.ReadFromFile(openFileJson.FileName);
                if (string.IsNullOrEmpty(JSONString))
                    return;
                CellCoreUnit core = CellCoreUnit.GetOfDerivedType(JSONString);
                if (core != null)
                {
                    skipCoreTypeChange = true;
                    ddCoreType.Text = core.CoreType;
                    skipCoreTypeChange = false;
                    ddCoreType.Text = poolTemplate.CoreType = core.CoreType;
                    poolTemplate.Parameters = core.GetParameters().ToDictionary(kvp => kvp.Key, kvp => new Constant_NoDistribution(kvp.Value, true, false, 0) as  Distribution);
                    ParamDictToGrid();
                }
            }
        }

        private void linkTestDynamics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dictionary<string, double> dparams = poolTemplate.Parameters.ToDictionary(kvp => kvp.Key, kvp => kvp.Value is Distribution dist ? dist.UniqueValue : double.Parse(kvp.Value.ToString()));
            poolTemplate.CoreType = ddCoreType.Text;
            dyncontrol = new(poolTemplate.CoreType, dparams, testMode: false);
            dyncontrol.UseUpdatedParams += Dyncontrol_UseUpdatedParams;
            ControlContainer frmControl = new();
            frmControl.AddControl(dyncontrol);
            frmControl.Text = eGroupName.Text;
            frmControl.SaveVisible = false;
            frmControl.ShowDialog();
        }

        private void Dyncontrol_UseUpdatedParams(object sender, EventArgs e)
        {
            if (e is not UpdatedParamsEventArgs args || args.ParamsAsObject == null)
                return;
            if (poolTemplate.CoreType == args.CoreType && poolTemplate.Parameters.Values.Any(v => v is Distribution dist && !dist.IsConstant))
            {
                string msg = "This cell pool has defined distributions for dynamics parameters, which will be cleared. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
            }
            poolTemplate.Parameters = args.ParamsAsObject;
            poolTemplate.CoreType = args.CoreType;
            ddCoreType.Text = poolTemplate.CoreType.ToString();
            ParamDictToGrid();
            MessageBox.Show($"Parameters are carried to {poolTemplate.CellGroup}", "SiliFish");
        }

        private void cbAllSomites_CheckedChanged(object sender, EventArgs e)
        {
            eSomiteRange.ReadOnly = cbAllSomites.Checked;
            if (!cbAllSomites.Checked && eSomiteRange.Text == toolTip1.GetToolTip(eSomiteRange))
                eSomiteRange.Text = "";
        }


    }
}
