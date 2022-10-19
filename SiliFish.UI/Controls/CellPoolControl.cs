using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using System.ComponentModel;
using System.Diagnostics;
using static SiliFish.UI.Controls.DynamicsTestControl;

namespace SiliFish.UI.Controls
{
    public partial class CellPoolControl : UserControl
    {
        private event EventHandler savePool;
        public event EventHandler SavePool { add => savePool += value; remove => savePool -= value; }

        private event EventHandler loadPool;
        public event EventHandler LoadPool { add => loadPool += value; remove => loadPool -= value; }

        CellPoolTemplate poolTemplate = new();

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private DynamicsTestControl dyncontrol = null;
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
        public CellPoolControl()
        {
            InitializeComponent();
            distConductionVelocity.AbsoluteEnforced = true;
            ddCoreType.Items.AddRange(DynamicUnit.GetCoreTypes().ToArray());// fill before celltypes
            ddCellType.DataSource = Enum.GetNames(typeof(CellType));
            ddNeuronClass.DataSource = Enum.GetNames(typeof(NeuronClass));
            ddSelection.DataSource = Enum.GetNames(typeof(CountingMode));
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
                ddCoreType.SelectedIndex = 1;//TODO hardcoded for izhikevich 9P AAAAAAHHHHHRRGGHH
            }
            else
            {
                lNeuronClass.Visible = ddNeuronClass.Visible = false;
                ddCoreType.SelectedIndex = 2;//TODO hardcoded for LeakyIntegrator AAAAAAHHHHHRRGGHH
            }
        }

        private void ddCoreType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string coreType = ddCoreType.Text;
            poolTemplate.Parameters = DynamicUnit.GetParameters(coreType);
            ParamDictToGrid();
        }

        private void ParamDictToGrid()
        {
            dgDynamics.WriteToGrid(poolTemplate.Parameters);
        }

        private Dictionary<string, object> GridToParamDict()
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
            poolTemplate.PositionLeftRight = sagPlane;
            poolTemplate.ColumnIndex2D = (int)e2DColumn.Value;
            poolTemplate.NumOfCells = (int)eNumOfCells.Value;
            poolTemplate.PerSomiteOrTotal = (CountingMode)Enum.Parse(typeof(CountingMode), ddSelection.Text);
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

            poolTemplate.Attachments.Clear();
            foreach (var att in listAttachments.Items)
            {
                poolTemplate.Attachments.Add(att.ToString());
            }
        }


        private void WriteDataToControl()
        {
            if (poolTemplate == null) return;

            eGroupName.Text = poolTemplate.CellGroup;
            eDescription.Text = poolTemplate.Description;
            skipCellTypeChange = true;
            ddCellType.Text = poolTemplate.CellType.ToString();
            skipCellTypeChange = false;
            ddCoreType.Text = poolTemplate.CoreType?.ToString();
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
            btnColor.BackColor = poolTemplate.Color;

            distributionX.SetDistribution((Distribution)poolTemplate.XDistribution);
            distributionY.SetDistribution((Distribution)poolTemplate.Y_AngleDistribution);
            distributionZ.SetDistribution((Distribution)poolTemplate.Z_RadiusDistribution);
            rbYZAngular.Checked = distributionY.GetDistribution().Angular;

            cbActive.Checked = poolTemplate.Active;
            timeLineControl.SetTimeLine(poolTemplate.TimeLine_ms);
            distConductionVelocity.SetDistribution(poolTemplate.ConductionVelocity as Distribution);
            ParamDictToGrid();

            listAttachments.Items.Clear();
            listAttachments.Items.AddRange(poolTemplate.Attachments?.ToArray());
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
        private void linkClearTimeline_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timeLineControl.ClearTimeLine();
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
            poolTemplate.Parameters = args.ParamsAsObject;
            poolTemplate.CoreType = args.CoreType;
            ddCoreType.Text = poolTemplate.CoreType.ToString();
            ParamDictToGrid();
        }

        private void cmAttachments_Opening(object sender, CancelEventArgs e)
        {
            cmiViewFile.Enabled = cmiRemoveAttachment.Enabled = listAttachments.SelectedItems.Count > 0;
        }
        private void cmiViewFile_Click(object sender, EventArgs e)
        {
            string filename = listAttachments.SelectedItem.ToString();
            Process p = new()
            {
                StartInfo = new ProcessStartInfo(filename)
                {
                    UseShellExecute = true
                }
            };
            if (!File.Exists(filename))
            {
                MessageBox.Show($"Path or file {filename} does not exist.");
                return;
            }
            p.Start();
        }

        private void cmiAddAttachment_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                listAttachments.Items.Add(dlgOpenFile.FileName);
        }

        private void cmiRemoveAttachment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to remove the selected attachment?", "SiliFish", MessageBoxButtons.OKCancel) == DialogResult.OK)
                listAttachments.Items.RemoveAt(listAttachments.SelectedIndex);
        }


    }
}
