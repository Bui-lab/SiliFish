using SiliFish.Definitions;
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
            ddStimulusMode.DataSource = Enum.GetNames(typeof(StimulusMode));
        }
        public override string ToString()
        {
            if (DesignMode)
                return base.ToString();
            return GetStimulus().ToString();
        }
        private void ddStimulusMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            StimulusMode mode = (StimulusMode)Enum.Parse(typeof(StimulusMode), ddStimulusMode.Text);
            switch (mode)
            {
                case StimulusMode.Gaussian:
                    lValue1.Text = "Mean";
                    lValue2.Text = "StdDev";
                    break;
                case StimulusMode.Ramp:
                    lValue1.Text = "Start Value";
                    lValue2.Text = "End Value";
                    break;
                case StimulusMode.Step:
                    lValue1.Text = "Value";
                    lValue2.Text = "Noise";
                    break;
                case StimulusMode.Sinusoidal:
                case StimulusMode.Pulse:
                    lValue1.Text = "Amplitude";
                    lValue2.Text = "Freq. (kHz)";
                    break;
                default:
                    break;
            }
            if (ddStimulusMode.Focused)
                stimulusChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetStimulus(StimulusSettings stim)
        {
            if (stim == null)
                stim = new();

            ddStimulusMode.Text = stim.Mode.ToString();
            eValue1.Text = stim.Value1.ToString();
            eValue2.Text = stim.Value2.ToString();
        }

        public StimulusSettings GetStimulus()
        {
            StimulusMode stimMode = (StimulusMode)Enum.Parse(typeof(StimulusMode), ddStimulusMode.Text);

            if (!double.TryParse(eValue1.Text, out double value1))
                value1 = 0;
            if (!double.TryParse(eValue2.Text, out double value2))
                value2 = 0;

            StimulusSettings stim = new()
            {
                Mode = stimMode,
                Value1 = value1,
                Value2 = value2
            };
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

    }
}
