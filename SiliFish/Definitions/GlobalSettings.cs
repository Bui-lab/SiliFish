using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json.Serialization;

namespace SiliFish.Definitions
{
    public class GlobalSettings
    {
        public static string DatabaseName = "";
        public static string TempFolder = Path.GetTempPath() + "SiliFish";
        public static string OutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
        public static double Epsilon = 0.00001;
        public static double VNoise = 0.001;
        public static double BiologicalMinPotential = -100;
        public static double BiologicalMaxPotential = 100;
        public static double FileSizeWarningLimit = 20;//in MB
        public static double MemoryWarningLimit = 10;//in GB
        public static bool UseDBForMemory = false;
        public static double ProgressLimitToDiscard = 10;//%

        public static long PlotDataPointLimit = (long)5E6;
        public static int PlotWarningNumber = 10;
        public static string PlotDataFormat = "0.0###";
        public static string CoordinateFormat = "0.0###";

        public static int SimulationEndTime = 1000;
        public static int SimulationSkipTime = 0;
        public static double SimulationDeltaT = 0.1;
        public static bool JunctionLevelTracking = false;
        public static bool Randomization = true;

        public static bool SameYAxis = true;
        public static bool ShowZeroValues = true;
        public static int PlotWidth = 800;
        public static int PlotHeight = 200;
        public static int PlotPointSize = 3;

        public static int MaxNumberOfUnits = 100;

        public static int RheobaseInfinity = 500;
        public static int RheobaseLimit = 1000;
        public static double[] RheobaseTestMultipliers = [1, 1.1, 1.5, 2];
        public static int RollingFreqBeatCount = 5;
        public static int ActivityThresholdSpikeCount = 10;

        public static double GeneticAlgorithmMinValue = -100;
        public static double GeneticAlgorithmMaxValue = 100;
        public static int GeneticAlgorithmSolutionCount = 3;
        public static int GeneticAlgorithmExhaustiveSolutionCount = 30;

        [JsonIgnore, Browsable(false)]
        public static List<string> TempFiles = [];

        [JsonIgnore, Browsable(false)]
        public static Dictionary<string, string> LastPlotSettings = [];
        [JsonIgnore, Browsable(false)]
        public static Dictionary<string, string> LastRunSettings = [];

    }
    public class GlobalSettingsProperties
    {
        #region File & Folder
        [Browsable(false),
            Description("Folder that temporary files are saved under. Will be cleared after the program exits."),
            DisplayName("Temporary Folder"),
            Category("File & Folder")]
        public string TempFolder { get { return GlobalSettings.TempFolder; } set { GlobalSettings.TempFolder = value; } }


        [Browsable(false),
            Description("The default folder that output files are saved under."),
            DisplayName("Output Folder"),
            Category("File & Folder")]
        public string OutputFolder { get { return GlobalSettings.OutputFolder; } set { GlobalSettings.OutputFolder = value; } }

        [Browsable(false),
            Description("The SQLITE database table."),
            DisplayName("Database Name"),
            Category("File & Folder")]
        public string DatabaseName { get { return GlobalSettings.DatabaseName; } set { GlobalSettings.DatabaseName = value; } }

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

        [Description("The minimum value consider it noise rather than a change in membrane potential."), 
            DisplayName("Memb. Pot. Noise"),
            Category("Const")]
        public double VNoise { get { return GlobalSettings.VNoise; } set { GlobalSettings.VNoise = value; } }

        [Description("The maximum memory usage (in GB) allowed before the user is given the option to turn off junction level current tracking."), 
            DisplayName("Memory warning limit"),
            Category("Const")]
        public double MemoryWarningLimit { get { return GlobalSettings.MemoryWarningLimit; } set { GlobalSettings.MemoryWarningLimit = value; } }

        [Description("If set to true, simulation data will be kept mainly in a temporary database to prevent memory bottleneck for large models."),
            DisplayName("Use DB for memory"),
            Category("Const")]
        public bool UseDBForMemory { get { return GlobalSettings.UseDBForMemory; } set { GlobalSettings.UseDBForMemory = value; } }


        [Description("The maximum file size (in MB) to be displayed on the HTML windows. " +
            "This limit is put to prevent crashes due to memory when displaying large plot."),
            DisplayName("File size warning limit"),
            Category("Const")]
        public double FileSizeWarningLimit { get { return GlobalSettings.FileSizeWarningLimit; } set { GlobalSettings.FileSizeWarningLimit = value; } }

        [Description("The minimum progress percentage required for the simulation data not to be discarded when a simulation is cancelled."),
            DisplayName("Progress Limit To Discard"),
            Category("Const")]
        public double ProgressLimitToDiscard { get { return GlobalSettings.ProgressLimitToDiscard; } set { GlobalSettings.ProgressLimitToDiscard = value; } }
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
        public int PlotWidth { get { return GlobalSettings.PlotWidth; } set { GlobalSettings.PlotWidth = value; } }

        [Description("In pixels."),
            DisplayName("Default plot height"),
            Category("Plotting")]
        public int PlotHeight { get { return GlobalSettings.PlotHeight; } set { GlobalSettings.PlotHeight = value; } }

        [Description("In pixels."),
            DisplayName("Size of the points on the scatterplots"),
            Category("Plotting")]
        public int PlotPointSize { get { return GlobalSettings.PlotPointSize; } set { GlobalSettings.PlotPointSize = value; } }
        #endregion

        #region Dynamics
        [Description("In ms. The duration used to calculate rheobase values."),
            DisplayName("Rheobase Infinity"),
            Category("Dynamics")]
        public int RheobaseInfinity { get { return GlobalSettings.RheobaseInfinity; } set { GlobalSettings.RheobaseInfinity = value; } }

        [Description("The max amount of stimulus to be tested to calculate rheobase values."),
            DisplayName("Rheobase Limit"),
            Category("Dynamics")]
        public int RheobaseLimit { get { return GlobalSettings.RheobaseLimit; } set { GlobalSettings.RheobaseLimit = value; } }
        
        [Description("The rheobase multipliers displayed on 'Cellular Dynamics' user interface."),
            DisplayName("Rheobase multipliers"),
            Category("Dynamics")]
        public double[] RheobaseTestMultipliers { get { return GlobalSettings.RheobaseTestMultipliers; } set { GlobalSettings.RheobaseTestMultipliers = value; } }

        [Description("The number of beats to the left and right to calculate the rolling TBF."),
            DisplayName("Rolling Freq. Beat Count"),
            Category("Dynamics")]
        public int RollingFreqBeatCount { get { return GlobalSettings.RollingFreqBeatCount; } set { GlobalSettings.RollingFreqBeatCount = value; } }

        [Description("The number of beats to the left and right to calculate the rolling TBF."),
            DisplayName("Activity Threshold Spike Count"),
            Category("Dynamics")]
        public int ActivityThresholdSpikeCount { get { return GlobalSettings.ActivityThresholdSpikeCount; } set { GlobalSettings.ActivityThresholdSpikeCount = value; } }
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

        [Description("Valid for all exhaustive search."),
            DisplayName("# of solutions per each setting combination"),
            Category("Genetic Algorithm")]
        public int GeneticAlgorithmSolutionCount { get { return GlobalSettings.GeneticAlgorithmSolutionCount; } set { GlobalSettings.GeneticAlgorithmSolutionCount = value; } }

        [Description("Valid for all exhaustive search."),
            DisplayName("# of total solutions"),
            Category("Genetic Algorithm")]
        public int GeneticAlgorithmExhaustiveSolutionCount { get { return GlobalSettings.GeneticAlgorithmExhaustiveSolutionCount; } set { GlobalSettings.GeneticAlgorithmExhaustiveSolutionCount = value; } }
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

        [Description("Whether junction level current tracking is on or off. For larger models turning it off is recommended."),
            DisplayName("Junction level current tracking"),
            Category("Simulation")]
        public bool JunctionLevelTracking { get { return GlobalSettings.JunctionLevelTracking; } set { GlobalSettings.JunctionLevelTracking = value; } }

        [Description("If set to false, any single value will be set as the center of the distribution without any randomization."),
            DisplayName("Randomization"),
            Category("Simulation")]
        public bool Randomization { get { return GlobalSettings.Randomization; } set { GlobalSettings.Randomization = value; } }

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
