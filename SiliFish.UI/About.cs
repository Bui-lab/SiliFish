using System.Reflection;

namespace SiliFish.UI
{
    public partial class About : Form
    {
        //TODO find last compile time
        static DateTime dateUI = new(2022, 6, 13);
        static DateTime dateEngine = new(2022, 6, 13);
        public About()
        {
            InitializeComponent();

            Version versionUI = Assembly.GetExecutingAssembly().GetName().Version;
            lVersionWindows.Text = $"Version: {versionUI}";
            lBuiltOn.Text = $"Built on {dateUI:d}";
            //Version versionEngine = typeof(SwimmingModel).Assembly.GetName().Version;
            //lVersionEngine.Text = string.Format("Engine version: {0}.{1}, built on {2}", versionEngine.Major, versionEngine.Minor, dateEngine.ToString("d"));
        }
            public void SetTimer(int ms)
        {
            timerAbout.Interval = ms;
            timerAbout.Enabled = true;
            timerAbout.Tick += TimerAbout_Tick;
        }

        private void TimerAbout_Tick(object sender, EventArgs e)
        {
            timerAbout.Tick-=TimerAbout_Tick;
            timerAbout.Enabled = false;
            this.Close();
        }
    }
}
