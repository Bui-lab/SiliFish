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
        public static bool ShowFileFolderAfterSave = true;
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

        public static bool SameYAxis = true;
        public static bool ShowZeroValues = true;
        public static int PlotWidth = 800;
        public static int PlotHeight = 200;
        public static int PlotPointSize = 3;
        public static bool OptimizedForPrinting = false;
        public static int PlotMergedFontSize = 36;
        public static int LegendWidthPercentage = 20;
        public static bool FullDynamics_ShowGapCurrent = true;
        public static bool FullDynamics_ShowChemInCurrent = true;
        public static bool FullDynamics_ShowChemOutCurrent = true;
        public static bool FullDynamics_ShowStimulus = true;
        public static bool MembranePotential_ShowSpike = true;

        public static int MaxNumberOfUnitsToList = 100;
        public static int MaxNumberOfUnitsToRender = 50_000;

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
        public static void AddTempFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || TempFiles.Contains(fileName)) 
                return;
            TempFiles.Add(fileName);
        }
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

        [Browsable(true),
            Description("Whether the files or folders will be opened after exporting data."),
            DisplayName("Open File/Folder"),
            Category("File && Folder")]
        public bool ShowFileFolderAfterSave { get { return GlobalSettings.ShowFileFolderAfterSave; } set { GlobalSettings.ShowFileFolderAfterSave = value; } }
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
            DisplayName("Max number of units to list"),
            Category("Display")]
        public int MaxNumberOfUnitsToList { get { return GlobalSettings.MaxNumberOfUnitsToList; } set { GlobalSettings.MaxNumberOfUnitsToList = value; } }

        [Description("The maximum number of items that will be plotted on the 3D rendering without a warning."),
            DisplayName("Max number of units to render"),
            Category("Display")]
        public int MaxNumberOfUnitsToRender { get { return GlobalSettings.MaxNumberOfUnitsToRender; } set { GlobalSettings.MaxNumberOfUnitsToRender = value; } }
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

        [Description("Displays single and larger x and y axis labels."),
            DisplayName("Optimized for printing"),
            Category("Plotting")]
        public bool OptimizedForPrinting { get { return GlobalSettings.OptimizedForPrinting; } set { GlobalSettings.OptimizedForPrinting = value; } }

        [Description("The font size of the x and y axis labels on the plots."),
            DisplayName("Label font size"),
            Category("Plotting")]
        public int PlotMergedFontSize { get { return GlobalSettings.PlotMergedFontSize; } set { GlobalSettings.PlotMergedFontSize = value; } }

        [Description("The percentage of the chart width the legend can cover. (If you have long IDs for the cells, you may need to increase this value)."),
            DisplayName("Legend percentage"),
            Category("Plotting")]
        public int LegendWidthPercentage { get { return GlobalSettings.LegendWidthPercentage; } set { GlobalSettings.LegendWidthPercentage = value; } }


        [Description("Whether gap currents will be plotted in the Full Dynamics plots."),
            DisplayName("Show gap currents"),
            Category("Plotting")]
        public bool FullDynamics_ShowGapCurrent { get { return GlobalSettings.FullDynamics_ShowGapCurrent; } set { GlobalSettings.FullDynamics_ShowGapCurrent = value; } }

        [Description("Whether incoming chemical currents will be plotted in the Full Dynamics plots."),
            DisplayName("Show synaptic currents"),
            Category("Plotting")]
        public bool FullDynamics_ShowChemInCurrent { get { return GlobalSettings.FullDynamics_ShowChemInCurrent; } set { GlobalSettings.FullDynamics_ShowChemInCurrent = value; } }
        [Description("Whether outgoing chemical currents will be plotted in the Full Dynamics plots."),
            DisplayName("Show terminal currents"),
            Category("Plotting")]
        public bool FullDynamics_ShowChemOutCurrent { get { return GlobalSettings.FullDynamics_ShowChemOutCurrent; } set { GlobalSettings.FullDynamics_ShowChemOutCurrent = value; } }

        [Description("Whether stimulus will be plotted in the Full Dynamics plots."),
            DisplayName("Show stimulus"),
            Category("Plotting")]
        public bool FullDynamics_ShowStimulus { get { return GlobalSettings.FullDynamics_ShowStimulus; } set { GlobalSettings.FullDynamics_ShowStimulus = value; } }

        [Description("Whether spikes will be plotted in the Membrane Potential plots."),
            DisplayName("Show spikes"),
            Category("Plotting")]
        public bool MembranePotential_ShowSpike { get { return GlobalSettings.MembranePotential_ShowSpike; } set { GlobalSettings.MembranePotential_ShowSpike = value; } }



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
