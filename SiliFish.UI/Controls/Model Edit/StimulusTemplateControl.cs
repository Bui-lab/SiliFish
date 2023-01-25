using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;

namespace SiliFish.UI.Controls
{
    public partial class StimulusTemplateControl : UserControl
    {
        private event EventHandler stimulusChanged;
        public event EventHandler StimulusChanged
        {
            add
            {
                stimulusChanged += value;
                stimControl.StimulusChanged += value;
            }
            remove
            {
                stimulusChanged -= value;
                stimControl.StimulusChanged -= value;
            }
        }
        private StimulusBase Stimulus;
        public StimulusTemplateControl()
        {
            InitializeComponent();
            timeLineControl.TimeLineChanged += TimeLineControl_TimeLineChanged;
        }

        private void TimeLineControl_TimeLineChanged(object sender, EventArgs e)
        {
            stimulusChanged?.Invoke(this, EventArgs.Empty);
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
            if (Stimulus is StimulusTemplate)
            {
                Stimulus = new StimulusTemplate()
                {
                    Settings = stimControl.GetStimulus(),
                    TargetPool = ddTargetPool.Text,
                    TargetSomite = cbAllSomites.Checked ? "All somites" : eTargetSomites.Text,
                    TargetCell = cbAllCells.Checked ? "All cells" : eTargetCells.Text,
                    LeftRight = ddSagittalPosition.Text,
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

        private void ddTargetPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddTargetPool.Focused)
            {
                stimulusChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private void ddSagittalPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSagittalPosition.Focused)
                stimulusChanged?.Invoke(this, EventArgs.Empty);

        }

        private void cbAllSomites_CheckedChanged(object sender, EventArgs e)
        {
            eTargetSomites.ReadOnly = cbAllSomites.Checked;
        }

        private void cbAllCells_CheckedChanged(object sender, EventArgs e)
        {
            eTargetCells.ReadOnly = cbAllCells.Checked;
        }

        internal void SetStimulus(object cellPoolTemplates, StimulusTemplate stim)
        {
            throw new NotImplementedException();
        }
    }
}
