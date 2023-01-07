using SiliFish.ModelUnits.Model;
using System.Reflection;

namespace SiliFish.UI
{
    public partial class About : Form
    {
        static DateTime dateUI = new(2022, 6, 13);
        static DateTime dateEngine = new(2022, 6, 13);
        public About()
        {
            InitializeComponent();
            //https://stackoverflow.com/questions/1600962/displaying-the-build-date
            //dateUI = DateTime.Parse(Properties.Resources.BuildDate); //There is a problem in getting the date info from UI.Resources
            dateEngine = DateTime.Parse(SiliFish.Properties.Resources.BuildDate);
            Version versionUI = Assembly.GetExecutingAssembly().GetName().Version;
            lVersionWindows.Text = string.Format("UI version: {0}.{1}", versionUI.Major, versionUI.Minor);
            //lVersionWindows.Text = string.Format("UI version: {0}.{1}, built on {2}", versionUI.Major, versionUI.Minor, dateUI.ToString("d"));
            Version versionEngine = typeof(SwimmingModel).Assembly.GetName().Version;
            lVersionEngine.Text = string.Format("Engine version: {0}.{1}, built on {2}", versionEngine.Major, versionEngine.Minor, dateEngine.ToString("d"));
        }
        public void SetTimer(int ms)
        {
            timerAbout.Interval = ms;
            timerAbout.Enabled = true;
            timerAbout.Tick += TimerAbout_Tick;
        }

        private void TimerAbout_Tick(object sender, EventArgs e)
        {
            timerAbout.Tick -= TimerAbout_Tick;
            timerAbout.Enabled = false;
            this.Close();
        }
    }
}
