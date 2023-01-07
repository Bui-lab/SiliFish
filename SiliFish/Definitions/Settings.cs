using SiliFish.DynamicUnits;
using System.ComponentModel;

namespace SiliFish.Definitions
{
    public class CurrentSettings
    {
        public static Settings Settings = new();
    }
    public class Settings
    {
        [Browsable(false), 
            Description("Folder that temporary files are saved under. Will be cleared after the program exits."),
            DisplayName("Temporary Folder"),
            Category("Folder")]
        public string TempFolder { get; set; }

        [Browsable(false), 
            Description("The default folder that output files are saved under."),
            DisplayName("Output Folder"),
            Category("Folder")]
        public string OutputFolder { get; set; }

        [Description("Values smaller than epsilon will be ignored."), 
            Category("Constant values")]
        public double Epsilon { get; set; } = 0.00001;
        
        [Category("Constant values")]
        public UnitOfMeasure UoM { get; set; } = UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad;//Used from data structures that don't have direct access to the model
        
        [Description(""), DisplayName("Number Format"), Category("Plotting")]
        public string DecimalPointFormat { get; set; } = "0.0###";

        [Description("In ms. The duration used to calculate rheobase values."), 
            DisplayName("Rheobase Infinity"),
            Category("Dynamics")]
        public int RheobaseInfinity { get; set; } = 500;
        [Description("The rheobase multipliers displayed on 'Test Dynamics' user interface."), 
            DisplayName("Rheobase multipliers"),
            Category("Dynamics")]
        public double[] RheobaseTestMultipliers { get; set; } = new double[] { 1, 1.1, 1.5 };//, 2 };

        [Description("Valid for all parameters."), 
            DisplayName("Suggested Min Value"),
            Category("Genetic Algorithm")]
        public double GeneticAlgorithmMinValue { get; set; } = -100;
        [Description("Valid for all parameters."),
            DisplayName("Suggested Max Value"),
            Category("Genetic Algorithm")]
        public double GeneticAlgorithmMaxValue { get; set; } = 100;

        [Browsable(false), Category("Default values")]
        public string DefaultNeuronCore { get; set; } = typeof(Izhikevich_9P).Name;
        [Browsable(false), Category("Default values")]
        public string DefaultMuscleCellCore { get; set; } = typeof(Leaky_Integrator).Name;


        [Description("The 'SD/Avg Interval' ratio for a burst sequence to be considered chattering"), 
            DisplayName ("Chattering Irregularity"),
            Category("Dynamics")]
        public double ChatteringIrregularity { get; set; } = 0.1;
        
        [Description("In ms, the maximum interval two spikes can have to be considered as part of a burst. " +
            "Used if the interval within spikes are not increasing with time."),
            DisplayName("Max Burst Interval - no spread"),
            Category("Dynamics")]
        public double MaxBurstInterval_DefaultLowerRange { get; set; } = 5;

        [Description("In ms, the maximum interval two spikes can have to be considered as part of a burst. " +
            "Used if the interval within spikes are increasing with time."),
            DisplayName("Max Burst Interval - spread"),
            Category("Dynamics")]
        public double MaxBurstInterval_DefaultUpperRange { get; set; } = 30;


        [Description("Centroid2 (average duration between bursts) < Centroid1 (average duration between spikes) * OneClusterMultiplier " +
            "means there is only one cluster (all spikes are part of a burst)"), 
            DisplayName("One Cluster Multiplier"),
            Category("Dynamics")]
        public double OneClusterMultiplier { get; set; } = 2;

        [Description("In ms, the range between the last spike and the end of current to be considered as tonic firing"), 
            DisplayName("Tonic Padding"),
            Category("Dynamics")]
        public double TonicPadding { get; set; } = 1;
    }
}
