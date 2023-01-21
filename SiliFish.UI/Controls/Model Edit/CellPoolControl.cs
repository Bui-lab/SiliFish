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

        CellPoolTemplate poolBase;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private DynamicsTestControl dyncontrol = null;
        private bool SomiteBased = false;
        public CellPoolTemplate PoolBase
        {
            get
            {
                ReadDataFromControl();
                return poolBase;
            }
            set
            {
                eNumOfCells.Enabled = value == null || value is not CellPool;
                poolBase = value ?? new();
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
            distConductionVelocity.SetDistribution(new Constant_NoDistribution(CurrentSettings.Settings.cv));
            if (SomiteBased)
            {
                cbAllSomites.Visible = eSomiteRange.Visible = true;
                ddSelection.DataSource = Enum.GetNames(typeof(CountingMode));
            }
            else
            {
                cbAllSomites.Visible = eSomiteRange.Visible = false;
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
            poolBase.Parameters = CellCoreUnit.GetParameters(coreType);
            ParamDictToGrid();
        }

        private void ParamDictToGrid()
        {
            dgDynamics.WriteToGrid(poolBase.Parameters);
        }

        private Dictionary<string, Distribution> GridToParamDict()
        {
            return dgDynamics.ReadFromGrid();
        }

        private void eGroupName_TextChanged(object sender, EventArgs e)
        {
            if (poolBase != null)
                poolBase.CellGroup = eGroupName.Text;
        }

        private void linkLoadPool_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (loadPool == null) return;
            loadPool.Invoke(this, new EventArgs());
            if (string.IsNullOrEmpty(JSONString))
                return;
            try
            {
                poolBase = (CellPoolTemplate)JsonUtil.ToObject(typeof(CellPoolTemplate), JSONString);
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
            JSONString = JsonUtil.ToJson(poolBase);
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
            poolBase ??= new CellPoolTemplate();
            poolBase.CellGroup = groupName;
            poolBase.Description = eDescription.Text;
            poolBase.BodyLocation= (BodyLocation)Enum.Parse(typeof(BodyLocation), ddBodyPosition.Text);
            poolBase.PositionLeftRight = sagPlane;
            poolBase.ColumnIndex2D = (int)e2DColumn.Value;
            poolBase.NumOfCells = (int)eNumOfCells.Value;
            poolBase.PerSomiteOrTotal = (CountingMode)Enum.Parse(typeof(CountingMode), ddSelection.Text);
            poolBase.SomiteRange = SomiteBased && !cbAllSomites.Checked ? eSomiteRange.Text : "";
            poolBase.XDistribution = distributionX.GetDistribution();
            poolBase.Y_AngleDistribution = distributionY.GetDistribution();
            poolBase.Z_RadiusDistribution = distributionZ.GetDistribution();
            poolBase.CellType = (CellType)Enum.Parse(typeof(CellType), ddCellType.Text);
            poolBase.CoreType = ddCoreType.Text;
            poolBase.NTMode = (NeuronClass)Enum.Parse(typeof(NeuronClass), ddNeuronClass.Text);
            poolBase.Parameters = GridToParamDict();
            poolBase.Color = btnColor.BackColor;
            poolBase.Active = cbActive.Checked;
            poolBase.TimeLine_ms = timeLineControl.GetTimeLine();
            poolBase.ConductionVelocity = distConductionVelocity.GetDistribution();

            poolBase.Attachments = attachmentList.GetAttachments();
        }


        private void WriteDataToControl()
        {
            if (poolBase == null) return;

            eGroupName.Text = poolBase.CellGroup;
            eDescription.Text = poolBase.Description;
            skipCellTypeChange = true;
            ddCellType.Text = poolBase.CellType.ToString();
            skipCellTypeChange = false;
            skipCoreTypeChange = true;
            ddCoreType.Text = poolBase.CoreType?.ToString();
            skipCoreTypeChange = false;
            ddBodyPosition.Text = poolBase.BodyLocation.ToString();
            lNeuronClass.Visible = ddNeuronClass.Visible = poolBase.CellType == CellType.Neuron;
            ddNeuronClass.Text = poolBase.NTMode.ToString();
            if (poolBase.PositionLeftRight == SagittalPlane.Both)
                ddSagittalPosition.Text = "Left/Right";
            else if (poolBase.PositionLeftRight == SagittalPlane.Left)
                ddSagittalPosition.Text = "Left";
            else if (poolBase.PositionLeftRight == SagittalPlane.Right)
                ddSagittalPosition.Text = "Right";
            e2DColumn.Value = Math.Max(poolBase.ColumnIndex2D, e2DColumn.Minimum);
            eNumOfCells.Value = poolBase.NumOfCells;
            ddSelection.Text = poolBase.PerSomiteOrTotal.ToString();
            if (string.IsNullOrEmpty(poolBase.SomiteRange))
            {
                cbAllSomites.Checked = true;
            }
            else
            {
                cbAllSomites.Checked = false;
                eSomiteRange.Text = poolBase.SomiteRange;
            }

            btnColor.BackColor = poolBase.Color;

            distributionX.SetDistribution((Distribution)poolBase.XDistribution);
            distributionY.SetDistribution((Distribution)poolBase.Y_AngleDistribution);
            distributionZ.SetDistribution((Distribution)poolBase.Z_RadiusDistribution);
            rbYZAngular.Checked = distributionY.GetDistribution() != null && distributionY.GetDistribution().Angular;

            cbActive.Checked = poolBase.Active;
            timeLineControl.SetTimeLine(poolBase.TimeLine_ms);
            distConductionVelocity.SetDistribution(poolBase.ConductionVelocity as Distribution);
            ParamDictToGrid();

            attachmentList.SetAttachments(poolBase.Attachments);
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
                //the core is saved as an array to benefit from $type tag added by the JsonSerializer
                CellCoreUnit[] arr = (CellCoreUnit[])JsonUtil.ToObject(typeof(CellCoreUnit[]), JSONString);
                if (arr != null && arr.Any())
                {
                    CellCoreUnit core = arr[0];
                    if (core != null)
                    {
                        skipCoreTypeChange = true;
                        ddCoreType.Text = core.CoreType;
                        skipCoreTypeChange = false;
                        ddCoreType.Text = poolBase.CoreType = core.CoreType;
                        poolBase.Parameters = core.GetParameters().ToDictionary(kvp => kvp.Key, kvp => new Constant_NoDistribution(kvp.Value) as Distribution);
                        ParamDictToGrid();
                    }
                }
            }
        }

        private void linkTestDynamics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dictionary<string, double> dparams = poolBase.Parameters.ToDictionary(kvp => kvp.Key, kvp => kvp.Value is Distribution dist ? dist.UniqueValue : double.Parse(kvp.Value.ToString()));
            poolBase.CoreType = ddCoreType.Text;
            dyncontrol = new(poolBase.CoreType, dparams, testMode: false);
            dyncontrol.UseUpdatedParams += Dyncontrol_UseUpdatedParams;
            ControlContainer frmControl = new();
            frmControl.AddControl(dyncontrol);
            frmControl.Text = eGroupName.Text;
            frmControl.SaveVisible = false;
            frmControl.Show();
        }

        private void Dyncontrol_UseUpdatedParams(object sender, EventArgs e)
        {
            if (e is not UpdatedParamsEventArgs args || args.ParamsAsDistribution == null)
                return;
            if (poolBase.CoreType == args.CoreType && poolBase.Parameters.Values.Any(v => v is Distribution dist && !dist.IsConstant))
            {
                string msg = "This cell pool has defined distributions for dynamics parameters, which will be cleared. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
            }
            poolBase.Parameters = args.ParamsAsDistribution;
            poolBase.CoreType = args.CoreType;
            ddCoreType.Text = poolBase.CoreType.ToString();
            ParamDictToGrid();
            MessageBox.Show($"Parameters are carried to {poolBase.CellGroup}", "SiliFish");
        }

        private void cbAllSomites_CheckedChanged(object sender, EventArgs e)
        {
            eSomiteRange.ReadOnly = cbAllSomites.Checked;
            if (!cbAllSomites.Checked && eSomiteRange.Text == toolTip1.GetToolTip(eSomiteRange))
                eSomiteRange.Text = "";
        }


    }
}
