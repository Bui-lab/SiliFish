using SiliFish.ModelUnits.Parameters;

namespace SiliFish.UI.Controls.General
{
    public partial class SimulationSettingsControl : UserControl
    {
        public int iMax => (int)(eTimeEnd.Value / edt.Value);

        public SimulationSettingsControl()
        {
            InitializeComponent();
        }

        public void SetValues(RunParam runParam)
        {
            edt.Value = (decimal)runParam.DeltaT;
            eSkip.Value = runParam.SkipDuration;
            eTimeEnd.Value = runParam.MaxTime;
        }
        public RunParam GetValues()
        {
            RunParam runParam = new()
            {
                SkipDuration = (int)eSkip.Value,
                MaxTime = (int)eTimeEnd.Value,
                DeltaT = (double)edt.Value
            };
            return runParam;
        }

        public void SaveSettings(Dictionary<string, string> LastRunSettings)
        {
            LastRunSettings[lTimeEnd.Name] = eTimeEnd.Text;
            LastRunSettings[lSkip.Name] = eSkip.Text;
            LastRunSettings[ldt.Name] = edt.Text;
        }

        public void ReadSettings(Dictionary<string, string> LastRunSettings)
        {
            try
            {
                eTimeEnd.Text = LastRunSettings[lTimeEnd.Name];
                eSkip.Text = LastRunSettings[lSkip.Name];
                edt.Text = LastRunSettings[ldt.Name];
            }
            catch { }
        }
    }
}
