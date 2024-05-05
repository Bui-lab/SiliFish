using System.Collections.Generic;
using System.ComponentModel;

namespace SiliFish.ModelUnits.Parameters
{
    public class DynamicsParam
    {
        #region Dynamics - Two Exp Syn
        [Description("The number of spikes that would be considered in calculating the current conductance of a synapse. " +
    "Smaller numbers will lower the sensitivity of the simulation, whicle larger numbers will lower the performance." +
    "Enter 0 or a negative number to consider all of the spikes."),
    DisplayName("Spike Train Spike Count"),
    Category("Dynamics - Two Exp Syn")]
        public int SpikeTrainSpikeCount { get; set; } = 10;

        [Description("To increase performance of the simulation, the spikes earlier than " +
            "ThresholdMultiplier * (TauR + TauDFast + TauDSlow) will be ignored in calculating the current conductance of a synapse."),
            DisplayName("Threshold multiplier"),
            Category("Dynamics - Two Exp Syn")]

        public double ThresholdMultiplier { get; set; } = 3;

        [Description("If set to 'false', the negative currents in excitatory synapses and positive currents in inhibitory synapses will be zeroed out."),
            DisplayName("Allow Reverse Current"),
            Category("Dynamics - Two Exp Syn")]
        public bool AllowReverseCurrent { get; set; } = true;

        #endregion

        #region Firing Patterns
        [Description("The 'SD/Avg Interval' ratio for a burst sequence to be considered chattering"),
            DisplayName("Chattering Irregularity"),
            Category("Firing Patterns")]
        public double ChatteringIrregularity { get; set; } = 0.1;

        [Description("In ms, the maximum interval two spikes can have to be considered as part of a burst. " +
            "Used if the intervals between spikes are not increasing with time."),
            DisplayName("Max Burst Interval - no spread"),
            Category("Firing Patterns")]
        public double MaxBurstInterval_DefaultLowerRange { get; set; } = 5;

        [Description("In ms, the maximum interval two spikes can have to be considered as part of a burst. " +
            "Used if the intervals between spikes are increasing with time."),
            DisplayName("Max Burst Interval - spread"),
            Category("Firing Patterns")]
        public double MaxBurstInterval_DefaultUpperRange { get; set; } = 30;


        [Description("Centroid2 (average duration between bursts) < Centroid1 (average duration between spikes) * OneClusterMultiplier " +
            "means there is only one cluster (all spikes are part of a burst)."),
            DisplayName("One Cluster Multiplier"),
            Category("Firing Patterns")]
        public double OneClusterMultiplier { get; set; } = 2;

        [Description("In ms, the range between the last spike and the end of current to be considered as tonic firing."),
            DisplayName("Tonic Padding"),
            Category("Firing Patterns")]
        public double TonicPadding { get; set; } = 1;

        [Description("In ms. The duration to be used as a break while calculating spiking frequency."), 
            DisplayName("Spike Break"), 
            Category("Firing Patterns")]
        public double SpikeBreak { get; set; } = 10;
        
        [Description("In ms. The duration to be used as a break while calculating bursting frequency."), 
            DisplayName("Burst Break"), 
            Category("Firing Patterns")]
        public int BurstBreak { get; set; } = 100;

        #endregion
        #region RC Spike Trains
        [Description("In ms. The max duration in between two bursts of successive somites to be considered the same train of bursts."), 
            DisplayName("RC Positive Delay"),
            Category("RC Spike Trains")]
        public double RCPositiveDelay { get; set; } = 10;

        [Description("In ms. The max negative duration in between two bursts of successive somites to be considered the same train of bursts."), 
            DisplayName("RC Negative Delay"),
            Category("RC Spike Trains")]
        public double RCNegativeDelay { get; set; } = -3;
        #endregion


        public DynamicsParam() { }

        public DynamicsParam Clone()
        {
            return (DynamicsParam)MemberwiseClone();
        }
        public Dictionary<string, object> BackwardCompatibility(Dictionary<string, object> paramExternal)
        {
            return paramExternal;
        }

    }
}
