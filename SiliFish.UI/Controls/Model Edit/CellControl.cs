using OxyPlot;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Repositories;
using SiliFish.UI.EventArguments;
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
            CellCoreUnit core = CellCoreUnit.CreateCore(coreType, null);
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
            if (cell == null)
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
            if (ddCellPool.Enabled)
                cell.CellPool = ddCellPool.SelectedItem as CellPool;
            if (double.TryParse(eConductionVelocity.Text, out double cv))
                cell.ConductionVelocity = cv;
            cell.Coordinate = new Coordinate(double.Parse(eX.Text), double.Parse(eY.Text), double.Parse(eZ.Text));
            cell.CellGroup = (ddCellPool.SelectedItem as CellPool).CellGroup;
            cell.PositionLeftRight = cell.CellPool.PositionLeftRight;
            cell.Somite = (int)eSomite.Value;
            cell.Sequence = (int)eSequence.Value;
            cell.Active = cbActive.Checked;
            cell.TimeLine_ms = timeLineControl.GetTimeLine();
        }

        private void WriteDataToControl()
        {
            if (cell == null)
            {
                ddCellPool.Enabled = true;
                return;
            }

            ddCellPool.SelectedItem = cell.CellPool;
            skipCellTypeChange = true;
            ddCellType.SelectedItem = cell is Neuron? CellType.Neuron: CellType.MuscleCell;
            skipCellTypeChange = false;
            skipCoreTypeChange = true;
            ddCoreType.Text = cell.Core.CoreType.ToString();
            skipCoreTypeChange = false;
            eSequence.Value = cell.Sequence;
            eSomite.Value = cell.Somite;
            eX.Text = cell.X.ToString();
            eY.Text = cell.Y.ToString();
            eZ.Text = cell.Z.ToString();
            propCore.SelectedObject = cell.Core;
            eConductionVelocity.Text = cell.ConductionVelocity.ToString();
        }

        private void linkLoadCoreUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileJson.InitialDirectory = coreUnitFileDefaultFolder;
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                coreUnitFileDefaultFolder = Path.GetDirectoryName(openFileJson.FileName);

                CellCoreUnit core = CellCoreUnitFile.Load(openFileJson.FileName);
                if (core != null)
                {
                    skipCoreTypeChange = true;
                    ddCoreType.Text = core.CoreType;
                    propCore.SelectedObject = core;
                    skipCoreTypeChange = false;
                }
                else
                {
                    MessageBox.Show("Selected file is not a valid Core file.");
                }
            }
        }

        private void linkTestDynamics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dictionary<string, double> dparams = cell.Core.GetParameters();
            DynamicsTestControl dynControl = new(cell.Core.CoreType, dparams, testMode: false);
            frmDynamicControl = new();
            frmDynamicControl.AddControl(dynControl, null);
            frmDynamicControl.Text = cell.ID;
            frmDynamicControl.SaveVisible = false;
            frmDynamicControl.Show();
            dynControl.UseUpdatedParametersRequested += Dyncontrol_UseUpdatedParams;
            dynControl.ContentChanged += frmDynamicControl.ChangeCaption;
        }

        private void Dyncontrol_UseUpdatedParams(object sender, EventArgs e)
        {
            if (e is not UpdatedParamsEventArgs args || args.ParamsAsDistribution == null)
                return;
            if (args.CoreType != cell.Core.CoreType)
                cell.Core = CellCoreUnit.CreateCore(args.CoreType, args.ParamsAsDouble);
            else
                cell.Core.Parameters = args.ParamsAsDouble;
            ddCoreType.Text = cell.Core.CoreType.ToString();
            propCore.SelectedObject = cell.Core;
            MessageBox.Show($"Parameters are carried to {cell.ID}", "SiliFish");
        }

        public CellControl(RunningModel model)
        {
            InitializeComponent();
            this.model = model;
            ddCellPool.Items.AddRange(model.CellPools.ToArray());
            ddCellPool.Enabled = false;
            ddCoreType.Items.AddRange(CellCoreUnit.GetCoreTypes().ToArray());// fill before celltypes
            ddCellType.DataSource = Enum.GetNames(typeof(CellType));
            ddCellType.Enabled = false;//It has to be linked to the cell pool, always disabled
        }
        internal void CheckValues(object sender, EventArgs args)
        {
            CheckValuesArgs checkValuesArgs = args as CheckValuesArgs;
            checkValuesArgs.Errors = new();
            if (ddCellType.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Cell type not defined.");
            if (ddCoreType.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Core type not defined.");
            if (!double.TryParse(eX.Text, out double _) ||
                !double.TryParse(eY.Text, out double _) ||
                !double.TryParse(eZ.Text, out double _))
                checkValuesArgs.Errors.Add("Invalid coordinate values.");
            if (!double.TryParse(eConductionVelocity.Text, out double cv) || cv < GlobalSettings.Epsilon)
                checkValuesArgs.Errors.Add("Conduction velocity is 0. To disable a cell, use the Active field instead.");
            int somite = (int)eSomite.Value;
            int seq = (int)eSequence.Value;
            CellPool cp = ddCellPool.SelectedItem as CellPool;
            if (cp.Cells.Any(c => c != cell && c.Somite == somite && c.Sequence == seq))
                checkValuesArgs.Errors.Add("Cell sequence has to be unique for a cell pool and somite. Please enter a different sequence/somite.");
        }
    }
}
