using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.Definitions
{
    public class GlobalSettings
    {
        public static string TempFolder = Path.GetTempPath() + "SiliFish";
        public static string OutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
        public static double Epsilon = 0.00001;

        public static int PlotWarningNumber = 10;
        public static string DecimalPointFormat = "0.0###";

        public static bool SameYAxis = true;

        public static int DefaultPlotWidth = 800;

        public static int DefaultPlotHeight = 200;

        public static int RheobaseInfinity = 500;

        public static double[] RheobaseTestMultipliers = new double[] { 1, 1.1, 1.5 };//, 2 };
        public static double GeneticAlgorithmMinValue = -100;
        public static double GeneticAlgorithmMaxValue = 100;
        [JsonIgnore, Browsable(false)]
        public static List<string> TempFiles = new();

        [JsonIgnore, Browsable(false)]
        public static Dictionary<string, string> LastPlotSettings = new();

    }
    public class GlobalSettingsProperties
    {
        [Browsable(false),
            Description("Folder that temporary files are saved under. Will be cleared after the program exits."),
            DisplayName("Temporary Folder"),
            Category("Folder")]
        public string TempFolder { get { return GlobalSettings.TempFolder; } set { GlobalSettings.TempFolder = value; } }


        [Browsable(false),
            Description("The default folder that output files are saved under."),
            DisplayName("Output Folder"),
            Category("Folder")]
        public string OutputFolder { get { return GlobalSettings.OutputFolder; } set { GlobalSettings.OutputFolder = value; } }


        [Description("The maximum value between two numbers to consider them equal."), Category("Const")]
        public double Epsilon { get { return GlobalSettings.Epsilon; } set { GlobalSettings.Epsilon = value; } }

        [Description("The format used on charts. Useful to shorten numbers with high number of digitals."),
            DisplayName("Number Format"),
            Category("Plotting")]
        public string DecimalPointFormat { get { return GlobalSettings.DecimalPointFormat; } set { GlobalSettings.DecimalPointFormat = value; } }

        [Description("If there are more plots to be drawn than the 'Max # of Plots', the user will be warned to prevent unwanted resource usages."),
            DisplayName("Max # of Plots"),
            Category("Plotting")]
        public int PlotWarningNumber { get { return GlobalSettings.PlotWarningNumber; } set { GlobalSettings.PlotWarningNumber = value; } }

        [Description("Valid for HTML plots. Whether the graphs of the same types will be plotted using the same y-axis."),
            DisplayName("Use the same y-axis"),
            Category("Plotting")]
        public bool SameYAxis { get { return GlobalSettings.SameYAxis; } set { GlobalSettings.SameYAxis = value; } }

        [Description("In pixels."),
            DisplayName("Default plot width"),
            Category("Plotting")]
        public int DefaultPlotWidth { get { return GlobalSettings.DefaultPlotWidth; } set { GlobalSettings.DefaultPlotWidth = value; } }

        [Description("In pixels."),
            DisplayName("Default plot height"),
            Category("Plotting")]
        public int DefaultPlotHeight { get { return GlobalSettings.DefaultPlotHeight; } set { GlobalSettings.DefaultPlotHeight = value; } }

        [Description("In ms. The duration used to calculate rheobase values."),
            DisplayName("Rheobase Infinity"),
            Category("Dynamics")]
        public int RheobaseInfinity { get { return GlobalSettings.RheobaseInfinity; } set { GlobalSettings.RheobaseInfinity = value; } }

        [Description("The rheobase multipliers displayed on 'Cellular Dynamics' user interface."),
            DisplayName("Rheobase multipliers"),
            Category("Dynamics")]
        public double[] RheobaseTestMultipliers { get { return GlobalSettings.RheobaseTestMultipliers; } set { GlobalSettings.RheobaseTestMultipliers = value; } }

        [Description("Valid for all parameters."),
            DisplayName("Suggested Min Value"),
            Category("Genetic Algorithm")]
        public double GeneticAlgorithmMinValue { get { return GlobalSettings.GeneticAlgorithmMinValue; } set { GlobalSettings.GeneticAlgorithmMinValue = value; } }
        [Description("Valid for all parameters."),
            DisplayName("Suggested Max Value"),
            Category("Genetic Algorithm")]
        public double GeneticAlgorithmMaxValue { get { return GlobalSettings.GeneticAlgorithmMaxValue; } set { GlobalSettings.GeneticAlgorithmMaxValue = value; } }


        public void Save()
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\global.settings";
            string JSONString = JsonUtil.ToJson(this);
            FileUtil.SaveToFile(fileName, JSONString);
        }

        public static GlobalSettingsProperties Load()
        {
            string userSiliFishFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish";
            if (!Directory.Exists(userSiliFishFolder))
            {
                Directory.CreateDirectory(userSiliFishFolder);
            }
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\global.settings";
            if (!File.Exists(fileName)) { return new GlobalSettingsProperties(); }
            GlobalSettingsProperties gsp = (GlobalSettingsProperties)JsonUtil.ToObject(typeof(GlobalSettingsProperties), FileUtil.ReadFromFile(fileName));
            if (string.IsNullOrEmpty(gsp.TempFolder))
                gsp.TempFolder = Path.GetTempPath() + "SiliFish";
            if (!Directory.Exists(gsp.TempFolder))
            {
                try
                {
                    Directory.CreateDirectory(gsp.TempFolder);
                }
                catch { gsp.TempFolder = ""; }
            }
            if (string.IsNullOrEmpty(gsp.OutputFolder))
                gsp.OutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
            if (!Directory.Exists(gsp.OutputFolder))
            {
                try
                {
                    Directory.CreateDirectory(gsp.OutputFolder);
                }
                catch { gsp.OutputFolder = ""; }
            }
            return gsp;
        }

    }

}
