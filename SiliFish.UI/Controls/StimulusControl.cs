using SiliFish.DataTypes;
using SiliFish.ModelUnits;

namespace SiliFish.UI.Controls
{
    public partial class StimulusControl : UserControl
    {
        private event EventHandler stimulusChanged;
        public event EventHandler StimulusChanged { add => stimulusChanged += value; remove => stimulusChanged -= value; }

        public StimulusControl()
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
        private void ddStimulusMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddStimulusMode.Text)
            {
                case "Gaussian":
                    lValue1.Text = "Mean";
                    lValue2.Text = "StdDev";
                    break;
                case "Ramp":
                    lValue1.Text = "Start Value";
                    lValue2.Text = "End Value";
                    break;
                case "Step":
                    lValue1.Text = "Value";
                    lValue2.Text = "Noise";
                    break;
            }
            if (ddStimulusMode.Focused)
                stimulusChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetStimulus(List<CellPoolTemplate> pools, StimulusTemplate stim)
        {
            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0) return;

            if (stim == null)
                stim = new();
            ddTargetPool.Items.AddRange(pools.ToArray());

            ddStimulusMode.Text = stim.Stimulus_ms?.Mode.ToString();
            eValue1.Text = stim.Stimulus_ms?.Value1.ToString();
            eValue2.Text = stim.Stimulus_ms?.Value2.ToString();
            ddTargetPool.Text = stim.TargetPool;
            if (!ddTargetSomites.Items.Contains(stim.TargetSomite) && stim.TargetSomite != null)
                ddTargetSomites.Items.Add(stim.TargetSomite);
            ddTargetSomites.Text = stim.TargetSomite;
            if (!ddTargetCells.Items.Contains(stim.TargetCell) && stim.TargetCell != null)
                ddTargetCells.Items.Add(stim.TargetCell);
            ddTargetCells.Text = stim.TargetCell;
            ddSagittalPosition.Text = stim.LeftRight;
            cbActive.Checked = stim.Active;

            if (stim.Stimulus_ms != null)
                timeLineControl.SetTimeLine(stim.Stimulus_ms.TimeSpan_ms);
        }

        public StimulusTemplate GetStimulus()
        {
            StimulusTemplate stim = new()
            {
                TargetPool = ddTargetPool.Text,
                TargetSomite = ddTargetSomites.Text,
                TargetCell = ddTargetCells.Text,
                LeftRight = ddSagittalPosition.Text
            };

            StimulusMode stimMode = ddStimulusMode.Text == "Gaussian" ? StimulusMode.Gaussian :
                ddStimulusMode.Text == "Ramp" ? StimulusMode.Ramp :
                StimulusMode.Step;

            if (!double.TryParse(eValue1.Text, out double value1))
                value1 = 0;
            if (!double.TryParse(eValue2.Text, out double value2))
                value2 = 0;

            stim.Active = cbActive.Checked;
            TimeLine tl = timeLineControl.GetTimeLine();
            stim.Stimulus_ms = new Stimulus(stimMode, tl, value1, value2);
            return stim;
        }

        private void eValue1_TextChanged(object sender, EventArgs e)
        {
             if (eValue1.Focused)
                stimulusChanged?.Invoke(this, EventArgs.Empty);
        }

        private void eValue2_TextChanged(object sender, EventArgs e)
        {
            if (eValue2.Focused)
                stimulusChanged?.Invoke(this, EventArgs.Empty);
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
