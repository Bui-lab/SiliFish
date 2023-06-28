﻿using SiliFish.Helpers;
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
        public static double BiologicalMinPotential = -100;
        public static double BiologicalMaxPotential = 100;
        public static double MemoryWarningLimit = 10;//in GB
        
        public static long PlotDataPointLimit = (long)5E6;
        public static int PlotWarningNumber = 10;
        public static string PlotDataFormat = "0.0###";
        public static string CoordinateFormat = "0.0###";

        public static int SimulationEndTime = 1000;
        public static int SimulationSkipTime = 0;
        public static double SimulationDeltaT = 0.1;
        public static double SimulationEulerDeltaT = 0.1;
        public static bool JunctionLevelTracking = false;

        public static bool SameYAxis = true;
        public static bool ShowZeroValues = true;

        public static int DefaultPlotWidth = 800;

        public static int DefaultPlotHeight = 200;

        public static int MaxNumberOfUnits = 100;

        public static int RheobaseInfinity = 500;

        public static double[] RheobaseTestMultipliers = new double[] { 1, 1.1, 1.5, 2 };
        public static double GeneticAlgorithmMinValue = -100;
        public static double GeneticAlgorithmMaxValue = 100;

        [JsonIgnore, Browsable(false)]
        public static List<string> TempFiles = new();

        [JsonIgnore, Browsable(false)]
        public static Dictionary<string, string> LastPlotSettings = new();
        [JsonIgnore, Browsable(false)]
        public static Dictionary<string, string> LastRunSettings = new();

    }
    public class GlobalSettingsProperties
    {
        #region Folder
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

        #endregion

        #region Const
        [Description("The minimum value a cell membrane potential can take."),
            DisplayName("Biological min V"),
            Category("Const")]
        public double BiologicalMinPotential { get { return GlobalSettings.BiologicalMinPotential; } set { GlobalSettings.BiologicalMinPotential = value; } }

        [Description("The maximum value a cell membrane potential can take."),
            DisplayName("Biological max V"),
            Category("Const")]
        public double BiologicalMaxPotential { get { return GlobalSettings.BiologicalMaxPotential; } set { GlobalSettings.BiologicalMaxPotential = value; } }


        [Description("The maximum value between two numbers to consider them equal."), Category("Const")]
        public double Epsilon { get { return GlobalSettings.Epsilon; } set { GlobalSettings.Epsilon = value; } }

        [Description("The maximum memory usage (in GB) allowed before the user is given the option to turn off junction level current tracking."), 
            DisplayName("Memory warning limit"),
            Category("Const")]
        public double MemoryWarningLimit { get { return GlobalSettings.MemoryWarningLimit; } set { GlobalSettings.MemoryWarningLimit = value; } }
        #endregion

        #region Display
        [Description("The maximum number of items that will be listed on the architecture lists. This number is obsolote when a single cell is selected."), 
            DisplayName("Max number of units"),
            Category("Display")]
        public int MaxNumberOfUnits { get { return GlobalSettings.MaxNumberOfUnits; } set { GlobalSettings.MaxNumberOfUnits = value; } }

        #endregion

        #region Plotting
        [Description("The format used on charts. Useful to shorten numbers with high number of digitals."),
            DisplayName("Number format"),
            Category("Plotting")]
        public string DecimalPointFormat { get { return GlobalSettings.PlotDataFormat; } set { GlobalSettings.PlotDataFormat = value; } }

        [Description("If there are more data points to be drawn than the 'Max # of data points', the charts will not be completed to prevent memory crashes."),
            DisplayName("Max # of data points"),
            Category("Plotting")]
        public long PlotDataPointLimit { get { return GlobalSettings.PlotDataPointLimit; } set { GlobalSettings.PlotDataPointLimit = value; } }

        [Description("If there are more plots to be drawn than the 'Max # of Plots', the user will be warned to prevent unwanted resource usages."),
            DisplayName("Max # of plots"),
            Category("Plotting")]
        public int PlotWarningNumber { get { return GlobalSettings.PlotWarningNumber; } set { GlobalSettings.PlotWarningNumber = value; } }

        [Description("Valid for HTML plots. Whether the graphs of the same types will be plotted using the same y-axis."),
            DisplayName("Use the same y-axis"),
            Category("Plotting")]
        public bool SameYAxis { get { return GlobalSettings.SameYAxis; } set { GlobalSettings.SameYAxis = value; } }

        [Description("Valid for HTML plots. Whether the zero values will be displayed on the legend."),
            DisplayName("Show zero values"),
            Category("Plotting")]
        public bool ShowZeroValues { get { return GlobalSettings.ShowZeroValues; } set { GlobalSettings.ShowZeroValues = value; } }

        [Description("In pixels."),
            DisplayName("Default plot width"),
            Category("Plotting")]
        public int DefaultPlotWidth { get { return GlobalSettings.DefaultPlotWidth; } set { GlobalSettings.DefaultPlotWidth = value; } }

        [Description("In pixels."),
            DisplayName("Default plot height"),
            Category("Plotting")]
        public int DefaultPlotHeight { get { return GlobalSettings.DefaultPlotHeight; } set { GlobalSettings.DefaultPlotHeight = value; } }
        #endregion

        #region Dynamics
        [Description("In ms. The duration used to calculate rheobase values."),
            DisplayName("Rheobase Infinity"),
            Category("Dynamics")]
        public int RheobaseInfinity { get { return GlobalSettings.RheobaseInfinity; } set { GlobalSettings.RheobaseInfinity = value; } }

        [Description("The rheobase multipliers displayed on 'Cellular Dynamics' user interface."),
            DisplayName("Rheobase multipliers"),
            Category("Dynamics")]
        public double[] RheobaseTestMultipliers { get { return GlobalSettings.RheobaseTestMultipliers; } set { GlobalSettings.RheobaseTestMultipliers = value; } }
        #endregion

        #region Genetic Algorithm
        [Description("Valid for all parameters."),
            DisplayName("Suggested Min Value"),
            Category("Genetic Algorithm")]
        public double GeneticAlgorithmMinValue { get { return GlobalSettings.GeneticAlgorithmMinValue; } set { GlobalSettings.GeneticAlgorithmMinValue = value; } }
        [Description("Valid for all parameters."),
            DisplayName("Suggested Max Value"),
            Category("Genetic Algorithm")]
        public double GeneticAlgorithmMaxValue { get { return GlobalSettings.GeneticAlgorithmMaxValue; } set { GlobalSettings.GeneticAlgorithmMaxValue = value; } }
        #endregion

        #region Simulation
        [Description("In ms - the default simulation duration."),
            DisplayName("Simulation Duration"),
            Category("Simulation")]
        public int SimulationEndTime { get { return GlobalSettings.SimulationEndTime; } set { GlobalSettings.SimulationEndTime= value; } }

        [Description("In ms - the default duration to skip."),
            DisplayName("Simulation Skip Duration"),
            Category("Simulation")]
        public int SimulationSkipTime { get { return GlobalSettings.SimulationSkipTime; } set { GlobalSettings.SimulationSkipTime = value; } }

        [Description("In ms - the default time unit."),
            DisplayName("Default δt"),
            Category("Simulation")]
        public double SimulationDeltaT { get { return GlobalSettings.SimulationDeltaT; } set { GlobalSettings.SimulationDeltaT= value; } }

        [Description("In ms - the default time unit used in differential equations."),
            DisplayName("Default Euler δt"),
            Category("Simulation")]
        public double SimulationEulerDeltaT { get { return GlobalSettings.SimulationEulerDeltaT; } set { GlobalSettings.SimulationEulerDeltaT = value; } }

        [Description("Whether junction level current tracking is on or off. For larger models turning it off is recommended."),
            DisplayName("Junction level current tracking"),
            Category("Simulation")]
        public bool JunctionLevelTracking { get { return GlobalSettings.JunctionLevelTracking; } set { GlobalSettings.JunctionLevelTracking = value; } }


        #endregion

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
            gsp ??= new GlobalSettingsProperties();
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
