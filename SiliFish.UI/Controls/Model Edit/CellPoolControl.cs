using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.UI.Controls.General;
using SiliFish.UI.EventArguments;
using System.ComponentModel;
using static SiliFish.UI.Controls.DynamicsTestControl;

namespace SiliFish.UI.Controls
{
    public partial class CellPoolControl : UserControl
    {
        private static string coreUnitFileDefaultFolder;
        private ModelSettings settings;
        CellPoolTemplate poolBase;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private ControlContainer frmDynamicControl;

        private bool SomiteBased = false;

        public event EventHandler SavePool;
        public event EventHandler LoadPool;

        public CellPoolTemplate PoolBase
        {
            get
            {
                ReadDataFromControl();
                return poolBase;
            }
            set
            {
                bool createdCellPool = value is CellPool cp && cp.NumOfCells > 0;

                linkLoadPool.Enabled = !createdCellPool;
                ((Control)tSpatialDist).Enabled = !createdCellPool;
                tProjections.Enabled = !createdCellPool;
                tTimeline.Enabled = !createdCellPool;
                //The alone below also disables Test Dynamics Test
                //((Control)tDynamics).Enabled = !createdCellPool;
                //Disable all other controls individually 
                dgDynamics.Enabled = !createdCellPool;
                grConductionVelocity.Enabled = !createdCellPool;
                linkLoadCoreUnit.Enabled = !createdCellPool;

                pMainInfo.Enabled = !createdCellPool;
                linkLoadPool.Enabled = !createdCellPool;

                poolBase = value ?? new();
                WriteDataToControl();
            }
        }
        public string JSONString;

        private bool skipCellTypeChange = false;
        private bool skipCoreTypeChange = false;
        private void ddCellType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipCellTypeChange) return;
            CellType cellType = (CellType)Enum.Parse(typeof(CellType), ddCellType.Text);
            if (cellType == CellType.Neuron)
            {
                if (ddNeuronClass.Items.Count > 0)
                    ddNeuronClass.SelectedIndex = 0;
                lNeuronClass.Visible = ddNeuronClass.Visible = true;
                ddCoreType.Text = settings.DefaultNeuronCore.GetType().ToString();
            }
            else
            {
                lNeuronClass.Visible = ddNeuronClass.Visible = false;
                ddCoreType.Text = settings.DefaultMuscleCellCore.GetType().ToString();
            }
        }

        private void ddCoreType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipCoreTypeChange) return;
            string coreType = ddCoreType.Text;
            poolBase.Parameters = CellCore.GetParameters(coreType);
            ParamDictToGrid();
        }

        private void ParamDictToGrid()
        {
            dgDynamics.WriteToGrid(poolBase.Parameters);
            eRheobase.Text = poolBase?.Rheobase.ToString();
        }

        private Dictionary<string, Distribution> GridToParamDict()
        {
            return dgDynamics.ReadFromGrid();
        }

        private void dgDynamics_Leave(object sender, EventArgs e)
        {
            if (poolBase == null) return;
            poolBase.Parameters = GridToParamDict();
            eRheobase.Text = poolBase.Rheobase.ToString();
        }

        private void eGroupName_TextChanged(object sender, EventArgs e)
        {
            if (poolBase != null)
                poolBase.CellGroup = eGroupName.Text;
        }

        private void linkLoadPool_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LoadPool == null) return;
            LoadPool.Invoke(this, new EventArgs());
            if (string.IsNullOrEmpty(JSONString))
                return;
            try
            {
                poolBase = (CellPoolTemplate)JsonUtil.ToObject(typeof(CellPoolTemplate), JSONString);
            }
            catch
            {
                MessageBox.Show("Selected file is not a valid Cell Pool Template file.", "Error");
                return;
            }
            if (poolBase == null)
            {
                MessageBox.Show("Selected file is not a valid Cell Pool Template file.", "Error");
                return;
            }
            WriteDataToControl();
        }

        private void linkSavePool_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (SavePool == null) return;
            ReadDataFromControl();
            JSONString = JsonUtil.ToJson(poolBase);
            SavePool.Invoke(this, new EventArgs());
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
            poolBase.BodyLocation = (BodyLocation)Enum.Parse(typeof(BodyLocation), ddBodyPosition.Text);
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
            poolBase.AscendingAxonLength = cbAscendingAxon.Checked ? distributionAscending.GetDistribution() : null;
            poolBase.DescendingAxonLength = cbDescendingAxon.Checked ? distributionDescending.GetDistribution() : null;
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
            ddSagittalPosition.Text = poolBase.PositionLeftRight.ToString();
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

            distributionX.SetDistribution(poolBase.XDistribution);
            distributionY.SetDistribution(poolBase.Y_AngleDistribution);
            distributionZ.SetDistribution(poolBase.Z_RadiusDistribution);
            rbYZAngular.Checked = distributionY.GetDistribution() != null && distributionY.GetDistribution().Angular;

            timeLineControl.SetTimeLine(poolBase.TimeLine_ms);
            distConductionVelocity.SetDistribution(poolBase.ConductionVelocity);
            if (poolBase.AscendingAxonLength != null)
            {
                cbAscendingAxon.Checked = true;
                distributionAscending.SetDistribution(poolBase.AscendingAxonLength);
            }
            else cbAscendingAxon.Checked = false;
            if (poolBase.DescendingAxonLength != null)
            {
                cbDescendingAxon.Checked = true;
                distributionDescending.SetDistribution(poolBase.DescendingAxonLength);
            }
            else cbDescendingAxon.Checked = false;
            ParamDictToGrid();

            cbActive.Checked = poolBase.Active;
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
                CellCore[] arr = (CellCore[])JsonUtil.ToObject(typeof(CellCore[]), JSONString);
                if (arr != null && arr.Any())
                {
                    CellCore core = arr[0];
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
            DynamicsTestControl dynControl = new(poolBase.CoreType, dparams, testMode: false);
            dynControl.UseUpdatedParametersRequested += Dyncontrol_UseUpdatedParams;
            frmDynamicControl = new();
            frmDynamicControl.AddControl(dynControl, null);
            dynControl.ContentChanged += frmDynamicControl.ChangeCaption;
            frmDynamicControl.Text = eGroupName.Text;
            frmDynamicControl.SaveVisible = false;
            frmDynamicControl.Show();
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
            eSomiteRange.Visible = !cbAllSomites.Checked;
        }

        public CellPoolControl(bool somiteBased, ModelSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            SomiteBased = somiteBased;
            distConductionVelocity.AbsoluteEnforced = true;
            ddCoreType.Items.AddRange(CellCore.GetCoreTypes().ToArray());// fill before celltypes
            ddCellType.DataSource = Enum.GetNames(typeof(CellType));
            ddNeuronClass.DataSource = Enum.GetNames(typeof(NeuronClass));
            ddBodyPosition.DataSource = Enum.GetNames(typeof(BodyLocation));
            distConductionVelocity.SetDistribution(new Constant_NoDistribution(settings.cv));
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
            ddSagittalPosition.DataSource = Enum.GetNames(typeof(SagittalPlane));
        }

        public void CheckValues(object sender, EventArgs args)
        {
            CheckValuesArgs checkValuesArgs = args as CheckValuesArgs;
            checkValuesArgs.Errors = new();
            if (ddCellType.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Cell type not defined.");
            if (ddCoreType.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Core type not defined.");
            if (ddBodyPosition.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Body position not defined.");
            if (ddSagittalPosition.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Sagittal position not defined.");
            if ((int)eNumOfCells.Value < GlobalSettings.Epsilon)
                checkValuesArgs.Errors.Add("Number of cells is 0. To disable a cell pool, use the Active field instead.");
        }

        private void cbAscendingAxon_CheckedChanged(object sender, EventArgs e)
        {
            distributionAscending.Enabled = cbAscendingAxon.Checked;
        }

        private void cbDescendingAxon_CheckedChanged(object sender, EventArgs e)
        {
            distributionDescending.Enabled = cbDescendingAxon.Checked;
        }

    }
}
