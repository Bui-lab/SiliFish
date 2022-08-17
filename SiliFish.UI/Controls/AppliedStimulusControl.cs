using SiliFish.DataTypes;
using SiliFish.ModelUnits;

namespace SiliFish.UI.Controls
{
    public partial class AppliedStimulusControl : UserControl
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

        public AppliedStimulusControl()
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
            return GetStimulusTemplate().ToString() + activeStatus;
        }

        public void SetStimulus(List<CellPoolTemplate> pools, StimulusTemplate stim)
        {
            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0) return;

            if (stim == null)
                stim = new();

            stimControl.SetStimulus(stim.StimulusSettings);
            ddTargetPool.Items.AddRange(pools.ToArray());
            ddTargetPool.Text = stim.TargetPool;
            if (!ddTargetSomites.Items.Contains(stim.TargetSomite) && stim.TargetSomite != null)
                ddTargetSomites.Items.Add(stim.TargetSomite);
            ddTargetSomites.Text = stim.TargetSomite;
            if (!ddTargetCells.Items.Contains(stim.TargetCell) && stim.TargetCell != null)
                ddTargetCells.Items.Add(stim.TargetCell);
            ddTargetCells.Text = stim.TargetCell;
            ddSagittalPosition.Text = stim.LeftRight;
            cbActive.Checked = stim.Active;

            if (stim.TimeLine_ms!= null)
                timeLineControl.SetTimeLine(stim.TimeLine_ms);
        }

        public StimulusTemplate GetStimulusTemplate()
        {
            StimulusTemplate stim = new()
            {
                StimulusSettings = stimControl.GetStimulus(),
                TargetPool = ddTargetPool.Text,
                TargetSomite = ddTargetSomites.Text,
                TargetCell = ddTargetCells.Text,
                LeftRight = ddSagittalPosition.Text,
                TimeLine_ms = timeLineControl.GetTimeLine(),
                Active = cbActive.Checked
            };
            return stim;
        }

        private void ddTargetPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddTargetPool.Focused)
            {
                stimulusChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private void ddTargetSomites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddTargetSomites.Text == "All Somites")
                ddTargetSomites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            else 
                ddTargetSomites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
        }

        private void ddTargetCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddTargetCells.Text == "All Cells")
                ddTargetCells.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            else
                ddTargetCells.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
        }
        private void ddSagittalPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSagittalPosition.Focused)
                stimulusChanged?.Invoke(this, EventArgs.Empty);

        }
    }
}
