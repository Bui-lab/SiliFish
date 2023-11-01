using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SiliFish.ModelUnits.Parameters
{
    public class KinemParam
    {
        #region Swim Dynamics
        [Description("Damping Coefficient"), DisplayName("Damping Coef"), Category("Swim Dynamics")]
        public double Zeta { get; set; } = 3.0; //#damping constant , high zeta =0.5/ low = 0.1

        [Description("Natural oscillation frequency"), DisplayName("w0"), Category("Swim Dynamics")]
        public double w0 { get; set; } = 2.5; //20Hz = 125.6

        [Description("If set to false, membrane potential values are used for animation.") , 
            DisplayName("Use Muscle Tension"), Category("Swim Dynamics")]
        public bool UseMuscleTension { get; set; } = false;

        [Description("Used if 'Use Muscle Tension' is set to false." +
            "If non-zero, (α + β * R) is used as 'Conversion Coefficient')"), 
            DisplayName("Alpha"), Category("Swim Dynamics")]
        public double Alpha { get; set; } = 0;

        [Description("Used if 'Use Muscle Tension' is set to false." +
            "If non-zero, (α + β * R) is used as 'Conversion Coefficient')"), 
            DisplayName("Beta"), Category("Swim Dynamics")]
        public double Beta { get; set; } = 0;

        [Description("Used if 'Use Muscle Tension' is set to false." +
            "Coefficient to convert membrane potential to driving force for the oscillation"), DisplayName("Potential Conversion Coef"), Category("Swim Dynamics")]
        public double ConvCoefPotential { get; set; } = 0.1;

        [Description("\"Used if 'Use Muscle Tension' is set to true." +
            "Coefficient to convert muscle tension to driving force for the oscillation"), DisplayName("Tension Conversion Coef"), Category("Swim Dynamics")]
        public double ConvCoefTension { get; set; } = 0.1;

        [Description("The distance considered as a move from the center line. Used only for tail based episode calculation."), 
            DisplayName("Boundary"), Category("Swim Dynamics")]
        public double Boundary { get; set; } = 0.5;

        //same as the time range that will be looked ahead to detect motion
        
        [Description("In ms. The duration required considered to be an episode break."), DisplayName("Episode Break"), Category("Swim Dynamics")]
        public int EpisodeBreak { get; set; } = 100;

        #endregion

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


        #endregion

        public double ThresholdMultiplier { get; set; } = 3;

        [Description("If set to 'false', the negative currents in excitatory synapses and positive currents in inhibitory synapses will be zeroed out."),
            DisplayName("Allow Reverse Current"),
            Category("Dynamics - Two Exp Syn")]
        public bool AllowReverseCurrent { get; set; } = true;

        #region Firing Patterns
        [Description("The 'SD/Avg Interval' ratio for a burst sequence to be considered chattering"),
            DisplayName("Chattering Irregularity"),
            Category("Firing Patterns")]
        public double ChatteringIrregularity { get; set; } = 0.1;

        [Description("In ms, the maximum interval two spikes can have to be considered as part of a burst. " +
            "Used if the interval within spikes are not increasing with time."),
            DisplayName("Max Burst Interval - no spread"),
            Category("Firing Patterns")]
        public double MaxBurstInterval_DefaultLowerRange { get; set; } = 5;

        [Description("In ms, the maximum interval two spikes can have to be considered as part of a burst. " +
            "Used if the interval within spikes are increasing with time."),
            DisplayName("Max Burst Interval - spread"),
            Category("Firing Patterns")]
        public double MaxBurstInterval_DefaultUpperRange { get; set; } = 30;


        [Description("Centroid2 (average duration between bursts) < Centroid1 (average duration between spikes) * OneClusterMultiplier " +
            "means there is only one cluster (all spikes are part of a burst)"),
            DisplayName("One Cluster Multiplier"),
            Category("Firing Patterns")]
        public double OneClusterMultiplier { get; set; } = 2;

        [Description("In ms, the range between the last spike and the end of current to be considered as tonic firing"),
            DisplayName("Tonic Padding"),
            Category("Firing Patterns")]
        public double TonicPadding { get; set; } = 1;

        [Description("In ms. The duration to be used as a break while calculating spiking frquency."), DisplayName("Spike Break"), 
            Category("Firing Patterns")]
        public int SpikeBreak { get; set; } = 10;

        #endregion

        [Browsable(false)]
        public double ConvCoef => UseMuscleTension ? ConvCoefTension: ConvCoefPotential;

        public KinemParam() { }

        public KinemParam Clone()
        {
            return (KinemParam)MemberwiseClone();
        }
        public Dictionary<string, object> BackwardCompatibility(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || !paramExternal.Keys.Any(k => k.StartsWith("Kinematics.")))
                return paramExternal;
            Zeta = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Damping Coef", Zeta);
            w0 = paramExternal.ReadDoubleAndRemoveKey("Kinematics.w0", w0);
            ConvCoefPotential = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Potential Conversion Coef", ConvCoefPotential);
            ConvCoefTension = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Tension Conversion Coef", ConvCoefTension);
            Alpha = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Alpha", Alpha);
            Beta = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Beta", Beta);
            Boundary = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Boundary", Boundary);
            EpisodeBreak = paramExternal.ReadIntegerAndRemoveKey("Kinematics.Delay", EpisodeBreak);
            return paramExternal;
        }

        public string GetAnimationDetails()
        {
            double convCoef = UseMuscleTension ? ConvCoefTension : ConvCoefPotential;
            string details = $"Damping coef: {Zeta}; Natural Oscillation Freq: {w0:0.###}\r\n" +
                $"Conv. Coef: {convCoef:0.###}; Alpha {Alpha:0.###}; Beta {Beta:0.###}";
            return details;
        }
    }
}
