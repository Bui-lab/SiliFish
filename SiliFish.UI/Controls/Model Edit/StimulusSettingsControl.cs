using SiliFish.Definitions;
using SiliFish.ModelUnits.Stim;

namespace SiliFish.UI.Controls
{
    public partial class StimulusSettingsControl : UserControl
    {
        public event EventHandler StimulusChanged;

        public StimulusSettingsControl()
        {
            InitializeComponent();
            ddStimulusMode.DataSource = Enum.GetNames(typeof(StimulusMode));
        }
        public override string ToString()
        {
            if (DesignMode)
                return base.ToString();
            return GetStimulusSettings().ToString();
        }
        private void ddStimulusMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            StimulusMode mode = (StimulusMode)Enum.Parse(typeof(StimulusMode), ddStimulusMode.Text);
            switch (mode)
            {
                case StimulusMode.Gaussian:
                    lValue1.Text = "Mean";
                    lValue2.Text = "StdDev";
                    lValue2.Visible = eValue2.Visible = true;
                    lFrequency.Visible = eFrequency.Visible = false;
                    break;
                case StimulusMode.Ramp:
                    lValue1.Text = "Start Value";
                    lValue2.Text = "End Value";
                    lValue2.Visible = eValue2.Visible = true;
                    lFrequency.Visible = eFrequency.Visible = false;
                    break;
                case StimulusMode.Step:
                    lValue1.Text = "Value";
                    lValue2.Text = "Noise";
                    lValue2.Visible = eValue2.Visible = true;
                    lFrequency.Visible = eFrequency.Visible = false;
                    break;
                case StimulusMode.Sinusoidal:
                    lValue1.Text = "Amplitude";
                    lValue2.Visible = eValue2.Visible = false;
                    lFrequency.Visible = eFrequency.Visible = true;
                    break;
                case StimulusMode.Pulse:
                    lValue1.Text = "Amplitude";
                    lValue2.Text = "Duration";
                    lValue2.Visible = eValue2.Visible = true;
                    lFrequency.Visible = eFrequency.Visible = true;
                    break;
                default:
                    break;
            }
            if (ddStimulusMode.Focused)
                StimulusChanged?.Invoke(this, EventArgs.Empty);
        }

        private void eValue1_TextChanged(object sender, EventArgs e)
        {
            if (eValue1.Focused)
                StimulusChanged?.Invoke(this, EventArgs.Empty);
        }

        private void eValue2_TextChanged(object sender, EventArgs e)
        {
            if (eValue2.Focused)
                StimulusChanged?.Invoke(this, EventArgs.Empty);
        }
        private void eFrequency_TextChanged(object sender, EventArgs e)
        {
            if (eFrequency.Focused)
                StimulusChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetStimulusSettings(StimulusSettings stim)
        {
            if (stim == null)
                stim = new();

            ddStimulusMode.Text = stim.Mode.ToString();
            eValue1.Text = stim.Value1.ToString();
            eValue2.Text = stim.Value2.ToString();
            eFrequency.Text = stim.Frequency.ToString();
        }

        public StimulusSettings GetStimulusSettings()
        {
            StimulusMode stimMode = (StimulusMode)Enum.Parse(typeof(StimulusMode), ddStimulusMode.Text);

            if (!double.TryParse(eValue1.Text, out double value1))
                value1 = 0;
            if (!double.TryParse(eValue2.Text, out double value2))
                value2 = 0;
            if (!double.TryParse(eFrequency.Text, out double freq))
                freq = 0;

            StimulusSettings stim = new()
            {
                Mode = stimMode,
                Value1 = value1,
                Value2 = value2,
                Frequency = freq
            };
            return stim;
        }
        internal List<string> CheckValues()
        {
            List<string> errors = new();
            if (ddStimulusMode.SelectedIndex < 0)
                errors.Add("Stimulus mode not defined.");
            return errors;
        }


    }
}
