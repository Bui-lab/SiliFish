namespace SiliFish.UI.Controls.General
{
    public partial class StimulusMultiplier : UserControl
    {
        private static bool bValue = false;
        private static double lastValue = 1;
        public double StimMultiplier => rbMultiplier.Checked ? (double)numStimulus.Value : 0;
        public double StimValue => rbValue.Checked ? (double)numStimulus.Value : 0;
        public StimulusMultiplier()
        {
            InitializeComponent();
            rbValue.Checked = bValue;
            numStimulus.Value = (decimal)lastValue;
        }

        private void numStimulus_ValueChanged(object sender, EventArgs e)
        {
            lastValue=(double)numStimulus.Value;
        }

        private void rbValue_CheckedChanged(object sender, EventArgs e)
        {
            bValue = rbValue.Checked;
        }
    }
}
