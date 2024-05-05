using SiliFish.ModelUnits.Parameters;

namespace SiliFish.UI.Dialogs
{
    public partial class MultipleRunDialog : Form
    {
        public int NumOfSimulations { get; private set; }
        public RunParam RunParam { get; private set; }
        public MultipleRunDialog(bool runTimeStats, RunParam runParam)
        {
            InitializeComponent();
            if (runTimeStats) 
                lMessage.Text = "The simulations will be run with the same settings sequentially multiple times and time related information will be listed. \r\n" +
                    "Only the last simulation results will be available.";
            else 
                lMessage.Text = "The simulations will be run with the same settings in parallel (hardware permitting) " +
                    "and full information will be saved to the output folder or database.";
            RunParam = runParam;
            simulationSettingsControl1.SetValues(runParam);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            NumOfSimulations = (int)eNumber.Value;
            RunParam = simulationSettingsControl1.GetValues();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NumOfSimulations = 0;
            Close();
        }
    }
}
