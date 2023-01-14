using SiliFish.ModelUnits.Architecture;
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
            Version versionEngine = typeof(RunningModel).Assembly.GetName().Version;
            lVersionEngine.Text = string.Format("Engine version: {0}.{1}, built on {2}", versionEngine.Major, versionEngine.Minor, dateEngine.ToString("d"));
            eCredits.Rtf = @"{\rtf1\pc \i Sili\i0 Fish uses some 3rd party tools. Corresponding licences are below:"+
                            @"\par" +
                            @"\par \b GeneticSharp \b0 (genetic algorithms in ‘Cellular Dynamics’ tool)" +
                            @"\par https://github.com/giacomelli/GeneticSharp/blob/master/LICENSE"+
                            @"\par"+
                            @"\par \b 3d-force-graph \b0(generating 3D models)" +
                            @"\par https://github.com/vasturiano/3d-force-graph/blob/master/LICENSE" +
                            @"\par" +
                            @"\par \b force-graph \b0(generating 2D models)" +
                            @"\par https://github.com/vasturiano/force-graph/blob/master/LICENSE" +
                            @"\par" +
                            @"\par \b Three.js \b0(the base of 3D and 2D models)" +
                            @"\par https://threejs.org/" +
                            @"\par https://github.com/mrdoob/three.js/blob/master/LICENSE" +
                            @"\par" +
                            @"\par \b d3 \b0(JavaScript library)" +
                            @"\par https://d3js.org/" +
                            @"\par https://github.com/d3/d3/blob/main/LICENSE" +
                            @"\par" +
                            @"\par \b dygraph \b0(used in generating HTML based plots)" +
                            @"\par https://dygraphs.com/" +
                            @"\par" +
                            @"\par \b amCharts5 \b0(used in animations)" +
                            @"\par https://www.amcharts.com/" +
                            @"\par" +
                            @"\par \b OxyPlot \b0(used by Windows based plots)" +
                            @"\par https://oxyplot.github.io/" +
                            @"\par https://licenses.nuget.org/MIT" +
                            @"\par" +
                            @"\par \b The \i Sili\i0 Fish icon\b0" +
                            @"\par https://www.flaticon.com/free-icons/fish-bone Fish bone icons created by Freepik - Flaticon" +
                            @"\par \b Lateral brain view in 3D models modified from:\b0" +
                            @"\par https://www.flickr.com/photos/zeissmicro/22984668685" +
                            @"\par \b Dorsal brain view in 3D models modified from:\b0" +
                            @"\par https://www.flickr.com/photos/nihgov/25413283437}"; 
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
