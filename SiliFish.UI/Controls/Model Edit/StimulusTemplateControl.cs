using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;

namespace SiliFish.UI.Controls
{
    public partial class StimulusTemplateControl : UserControl
    {
        private StimulusBase Stimulus;
        public StimulusTemplateControl()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return GetStimulus().ToString() + activeStatus;
        }

        public void SetStimulus(List<CellPoolTemplate> pools, StimulusTemplate stim)
        {
            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0) return;

            if (stim == null)
                stim = new();
            Stimulus = stim;

            stimControl.SetStimulus(Stimulus.Settings);
            ddTargetPool.Items.AddRange(pools.ToArray());
            ddTargetPool.Text = stim.TargetPool;
            if (ddTargetPool.Text == "")
                ddTargetPool.Text = stim.TargetPool + " (inactive)";
            if (string.IsNullOrEmpty(stim.TargetSomite) || stim.TargetSomite.StartsWith("All"))
            {
                cbAllSomites.Checked = true;
            }
            else
            {
                cbAllSomites.Checked = false;
                eTargetSomites.Text = stim.TargetSomite;
            }
            if (string.IsNullOrEmpty(stim.TargetCell) || stim.TargetCell.StartsWith("All"))
            {
                cbAllCells.Checked = true;
            }
            else
            {
                cbAllCells.Checked = false;
                eTargetCells.Text = stim.TargetCell;
            }
            ddSagittalPosition.Text = stim.LeftRight;
            cbActive.Checked = stim.Active;

            if (stim.TimeLine_ms != null)
                timeLineControl.SetTimeLine(stim.TimeLine_ms);
        }

        public void SetStimulus(List<CellPool> pools, Stimulus stim)
        {

            if (stim == null)
                stim = new();
            Stimulus = stim;

            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0) return;
            Stimulus = stim;
            stimControl.SetStimulus(Stimulus.Settings);

            if (stim == null)
            {
                stim = new();
                ddTargetPool.Items.AddRange(pools.ToArray());
                cbAllSomites.Checked = true;
                cbAllCells.Checked = true;
            }
            else
            {
                ddTargetPool.Items.Add(stim.TargetCell.CellPool);
                ddTargetPool.SelectedIndex = 0;
                ddTargetPool.Enabled = false;
                cbAllSomites.Checked = false;
                eTargetSomites.Text = stim.TargetCell.Somite.ToString();
                cbAllCells.Checked = false;
                eTargetCells.Text = stim.TargetCell.Sequence.ToString(); ;
            }
            ddSagittalPosition.Text = stim.TargetCell.PositionLeftRight.ToString();

            cbActive.Checked = stim.Active;

            if (stim.TimeLine_ms != null)
                timeLineControl.SetTimeLine(stim.TimeLine_ms);
        }
        public StimulusBase GetStimulus()
        {
            SagittalPlane sagPlane = SagittalPlane.Both;
            if (ddSagittalPosition.Text == "Left")
                sagPlane = SagittalPlane.Left;
            else if (ddSagittalPosition.Text == "Right")
                sagPlane = SagittalPlane.Right;
            else if (ddSagittalPosition.Text == "Left/Right")
                sagPlane = SagittalPlane.Both;
            if (Stimulus is StimulusTemplate)
            {
                Stimulus = new StimulusTemplate()
                {
                    Settings = stimControl.GetStimulus(),
                    TargetPool = ddTargetPool.Text,
                    TargetSomite = cbAllSomites.Checked ? "All somites" : eTargetSomites.Text,
                    TargetCell = cbAllCells.Checked ? "All cells" : eTargetCells.Text,
                    LeftRight = sagPlane.ToString(),
                    TimeLine_ms = timeLineControl.GetTimeLine(),
                    Active = cbActive.Checked
                };
            }
            else
            {
                Stimulus.Settings = stimControl.GetStimulus();
                Stimulus.TimeLine_ms = timeLineControl.GetTimeLine();
                Stimulus.Active = cbActive.Checked;
            }
            return Stimulus;
        }

        private void cbAllSomites_CheckedChanged(object sender, EventArgs e)
        {
            eTargetSomites.ReadOnly = cbAllSomites.Checked;
            eTargetSomites.Visible = !cbAllSomites.Checked;
            lRange.Visible = !cbAllSomites.Checked || !cbAllCells.Checked;
        }

        private void cbAllCells_CheckedChanged(object sender, EventArgs e)
        {
            eTargetCells.ReadOnly = cbAllCells.Checked;
            eTargetCells.Visible = !cbAllCells.Checked;
            lRange.Visible = !cbAllSomites.Checked || !cbAllCells.Checked;
        }

        internal void CheckValues(object sender, EventArgs args)
        {
            CheckValuesArgs checkValuesArgs = args as CheckValuesArgs;
            checkValuesArgs.Errors = new();
            if (ddTargetPool.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No target pool selected.");
            if (ddSagittalPosition.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Sagittal position not defined.");
            checkValuesArgs.Errors.AddRange(stimControl.CheckValues());
        }
        internal void SetStimulus(object cellPoolTemplates, StimulusTemplate stim)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
    }
}
