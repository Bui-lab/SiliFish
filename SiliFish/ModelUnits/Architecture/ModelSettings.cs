using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Architecture
{
    public class ModelSettings
    {
        [Description("The seed to initialize the random number generator."), DisplayName("Random Seed"), Category("Randomization")]
        public int Seed { get; set; } = 0;


        [Category("Constant values")]
        public UnitOfMeasure UoM { get; set; } = UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens;//Used from data structures that don't have direct access to the model

        [Browsable(false), Category("Default values")]
        public string DefaultNeuronCore { get; set; } = typeof(Izhikevich_9P).Name;
        [Browsable(false), Category("Default values")]
        public string DefaultMuscleCellCore { get; set; } = typeof(Leaky_Integrator).Name;

        [Description("Default conduction velocity of currents between cells."),
            DisplayName("Conduction Velocity"),
            Category("Default Values")]
        public double cv { get; set; } = 0.2;

        [Description("Default delay in chemical synapses."),
            DisplayName("Synaptic Delay"),
            Category("Default Values")]
        public double synaptic_delay { get; set; } = 0.5;
        [Description("Default delay in electrical junctions."),
            DisplayName("Gap Junction Delay"),
            Category("Default Values")]
        public double gap_delay { get; set; } = 0;

        [Description("Default reversal potential of glutamate."),
            Category("Default Values")]
        public double E_glu { get; set; } = 0;
        [Description("Default reversal potential of glycine."),
            Category("Default Values")]
        public double E_gly { get; set; } = -70; 
        [Description("Default reversal potential of GABA."),
            Category("Default Values")]
        public double E_gaba { get; set; } = -70; 
        [Description("Default reversal potential of ACh."),
            Category("Default Values")]
        public double E_ach { get; set; } = 120;

        [Description("If set to 'false', the negative currents in excitatory synapses and positive currents in inhibitory synapses will be zeroed out."),
            DisplayName("Allow Reverse Current"),
            Category("Dynamics - Chem Syn")]
        public bool AllowReverseCurrent{ get; set; } = true;

        [Description("The number of spikes that would be considered in calculating the current conductance of a synapse. " +
            "Smaller numbers will lower the sensitivity of the simulation, whicle larger numbers will lower the performance." +
            "Enter 0 or a negative number to consider all of the spikes."),
            DisplayName("Spike Train Spike Count"),
            Category("Dynamics - Two Exp Syn")]
        public int SpikeTrainSpikeCount{ get; set; } = 10;

        [Description("To increase performance of the simulation, the spikes earlier than " +
            "ThresholdMultiplier * (TauR + TauDFast + TauDSlow) will be ignored in calculating the current conductance of a synapse."),
            DisplayName("Threshold multiplier"),
            Category("Dynamics - Two Exp Syn")]
        public double ThresholdMultiplier{ get; set; } = 3;

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

        public ModelSettings Clone()
        {
            return (ModelSettings)MemberwiseClone();
        }
        public Dictionary<string, object> BackwardCompatibility(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || !paramExternal.Keys.Any(k => k.StartsWith("Dynamic.")))
                return paramExternal;

            cv = paramExternal.ReadDoubleAndRemoveKey("Dynamic.ConductionVelocity", cv);
            E_ach = paramExternal.ReadDoubleAndRemoveKey("Dynamic.E_ach", E_ach);
            E_gaba = paramExternal.ReadDoubleAndRemoveKey("Dynamic.E_gaba", E_gaba);
            E_glu = paramExternal.ReadDoubleAndRemoveKey("Dynamic.E_glu", E_glu);
            E_gly = paramExternal.ReadDoubleAndRemoveKey("Dynamic.E_gly", E_gly);
            return paramExternal;
        }
    }
}
