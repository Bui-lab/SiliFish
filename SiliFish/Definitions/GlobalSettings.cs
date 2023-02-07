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
        public static double Epsilon = 0.00001;

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


    }
    public class GlobalSettingsProperties
    {

        [Description("The maximum value between two numbers to consider them equal."), Category("Const")]
        public double Epsilon { get { return GlobalSettings.Epsilon; } set { GlobalSettings.Epsilon = value; } }

        [Description("The format used on charts. Useful to shorten numbers with high number of digitals."),
            DisplayName("Number Format"),
            Category("Plotting")]
        public string DecimalPointFormat { get { return GlobalSettings.DecimalPointFormat; } set { GlobalSettings.DecimalPointFormat = value; } }

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

        [Description("The rheobase multipliers displayed on 'Test Dynamics' user interface."),
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
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\global.settings";
            return (GlobalSettingsProperties)JsonUtil.ToObject(typeof(GlobalSettingsProperties), FileUtil.ReadFromFile(fileName));
        }

    }

}
