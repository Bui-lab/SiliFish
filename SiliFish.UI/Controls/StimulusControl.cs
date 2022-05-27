using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            return GetStimulus().ToString();
        }
        private void ddStimulusMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddStimulusMode.Text)
            {
                case "Gaussian":
                    lValue1.Text = "Mean";
                    lValue2.Text = "StdDev";
                    lValue2.Visible = eValue2.Visible = true;
                    break;
                case "Ramp":
                    lValue1.Text = "Start Value";
                    lValue2.Text = "End Value";
                    lValue2.Visible = eValue2.Visible = true;
                    break;
                case "Step":
                    lValue1.Text = "Value";
                    lValue2.Visible = eValue2.Visible = false;
                    break;
            }
            if (ddStimulusMode.Focused)
                stimulusChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetStimulus(List<CellPoolTemplate> pools, AppliedStimulus stim)
        {
            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0 || stim == null) return;

            ddTargetPool.Items.AddRange(pools.ToArray());

            ddStimulusMode.Text = stim.Stimulus_ms?.Mode.ToString();
            eValue1.Text = stim.Stimulus_ms?.Value1.ToString();
            eValue2.Text = stim.Stimulus_ms?.Value2.ToString();
            ddTargetPool.Text = stim.Target;
            ddSagittalPosition.Text = stim.LeftRight;
            if (stim.Stimulus_ms != null)
                timeLineControl.SetTimeLine(stim.Stimulus_ms.TimeSpan_ms);
        }

        public AppliedStimulus GetStimulus()
        {
            AppliedStimulus stim = new AppliedStimulus();
            
            stim.Target = ddTargetPool.Text;
            stim.LeftRight = ddSagittalPosition.Text;

            StimulusMode stimMode = ddStimulusMode.Text == "Gaussian" ? StimulusMode.Gaussian :
                ddStimulusMode.Text == "Ramp" ? StimulusMode.Ramp :
                StimulusMode.Step;

            if (!double.TryParse(eValue1.Text, out double value1))
                value1 = 0;
            if (!double.TryParse(eValue2.Text, out double value2))
                value2 = 0;
            
            TimeLine tl = timeLineControl.GetTimeLine();
            int start = 0;
            int end = -1;
            if (tl?.GetTimeLine()?.Count>0)
            {
                start = tl.GetTimeLine().First().start;
                end = tl.GetTimeLine().First().end;
            }

            stim.Stimulus_ms = new Stimulus(stimMode, start, end, value1, value2);
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
                stimulusChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddSagittalPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSagittalPosition.Focused)
                stimulusChanged?.Invoke(this, EventArgs.Empty);

        }
    }
}
