using OxyPlot;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using System.ComponentModel;
using static SiliFish.UI.Controls.DynamicsTestControl;

namespace SiliFish.UI.Controls
{
    public partial class CellControl : UserControl
    {
        private static string coreUnitFileDefaultFolder;
        private Cell cell;
        private RunningModel model;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private ControlContainer frmDynamicControl;
 

        public event EventHandler SaveCell;
        public event EventHandler LoadCell;

        public Cell Cell
        {
            get
            {
                ReadDataFromControl();
                return cell;
            }
            set
            {
                cell = value;
                WriteDataToControl();
            }
        }
        public string JSONString;


        private bool skipCellTypeChange = false;
        private bool skipCoreTypeChange = false;
        public CellControl(RunningModel model)
        {
            InitializeComponent();
            this.model = model;
            ddCellPool.Items.AddRange(model.CellPools.ToArray());
            ddCellPool.Enabled = false;//MODEL EDIT
            ddCoreType.Items.AddRange(CellCoreUnit.GetCoreTypes().ToArray());// fill before celltypes
            ddCellType.DataSource = Enum.GetNames(typeof(CellType));
            ddCellType.Enabled = false;//It has to be linked to the cell pool, always disabled
        }

        private void ddCellType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipCellTypeChange) return;
            CellType cellType = (CellType)Enum.Parse(typeof(CellType), ddCellType.Text);
            if (cellType == CellType.Neuron)
                ddCoreType.Text = model.Settings.DefaultNeuronCore.GetType().ToString();
            else
                ddCoreType.Text = model.Settings.DefaultMuscleCellCore.GetType().ToString();
        }

        private void ddCoreType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipCoreTypeChange) return;
            string coreType = ddCoreType.Text;
            CellCoreUnit core = CellCoreUnit.CreateCore(coreType, null, 0, 0);
            propCore.SelectedObject = core;
        }

        private void linkLoadCell_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LoadCell == null) return;
            LoadCell.Invoke(this, new EventArgs());
            if (string.IsNullOrEmpty(JSONString))
                return;
            try
            {
                cell = (Cell)JsonUtil.ToObject(typeof(Cell), JSONString);
            }
            catch
            {
                MessageBox.Show("Selected file is not a valid Cell file.");
                return;
            }
            WriteDataToControl();
        }

        private void linkSavePool_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (SaveCell == null) return;
            ReadDataFromControl();
            JSONString = JsonUtil.ToJson(cell);
            SaveCell.Invoke(this, new EventArgs());
        }

        private void ReadDataFromControl()
        {
            if (cell == null || cell.Core.CoreType != ddCoreType.Text)
                cell = ddCellType.Text == CellType.Neuron.ToString() ? new Neuron(model, ddCoreType.Text, ddCellPool.Text, -1, -1, null, 0) :
                            new MuscleCell(model, ddCoreType.Text, ddCellPool.Text, -1, -1, null, 0);

            if (double.TryParse(eConductionVelocity.Text, out double cv))
                cell.ConductionVelocity = cv;
            double.TryParse(eX.Text, out double x);
            double.TryParse(eY.Text, out double y);
            double.TryParse(eZ.Text, out double z);
            cell.Coordinate = new Coordinate(x, y, z);
            cell.CellGroup = ddCellPool.Text;
            cell.PositionLeftRight = y < 0 ? SagittalPlane.Left : SagittalPlane.Right;
            cell.Somite = (int)eSomite.Value;
            cell.Sequence = (int)eSequence.Value;
            cell.Active = cbActive.Checked;
            cell.TimeLine_ms = timeLineControl.GetTimeLine();
        }


        private void WriteDataToControl()
        {
            if (cell == null) return;

            ddCellPool.SelectedItem = cell.CellPool;
            skipCellTypeChange = true;
            ddCellType.SelectedItem = cell is Neuron? CellType.Neuron: CellType.MuscleCell;
            skipCellTypeChange = false;
            skipCoreTypeChange = true;
            ddCoreType.Text = cell.Core.CoreType.ToString();
            skipCoreTypeChange = false;
            eSequence.Value = cell.Sequence;
            eSomite.Value = cell.Somite;
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
                        //MODEL EDIT
                    }
                }
            }
        }

        private void linkTestDynamics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dictionary<string, double> dparams = cell.Core.GetParameters();
            DynamicsTestControl dynControl = new(cell.Core.CoreType, dparams, testMode: false);
            dynControl.UseUpdatedParametersRequested += Dyncontrol_UseUpdatedParams;
            dynControl.CoreChanged += DynControl_CoreChanged;
            frmDynamicControl = new();
            frmDynamicControl.AddControl(dynControl);
            frmDynamicControl.Text = cell.ID;
            frmDynamicControl.SaveVisible = false;
            frmDynamicControl.Show();
        }

        private void DynControl_CoreChanged(object sender, EventArgs e)
        {
            if (frmDynamicControl != null)
                frmDynamicControl.Text = (e as CoreChangedEventArgs).CoreName;
        }

        private void Dyncontrol_UseUpdatedParams(object sender, EventArgs e)
        {
            if (e is not UpdatedParamsEventArgs args || args.ParamsAsDistribution == null)
                return;
            cell.Core.Parameters = args.ParamsAsDouble;
            //MODEL EDIT cell.Core.CoreType = args.CoreType;
            ddCoreType.Text = cell.Core.CoreType.ToString();
            MessageBox.Show($"Parameters are carried to {cell.ID}", "SiliFish");
        }


    }
}
