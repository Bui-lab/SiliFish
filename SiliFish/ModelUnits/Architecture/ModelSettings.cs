using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SiliFish.ModelUnits.Architecture
{
    public class ModelSettings
    {
        #region Simulation
        [Description("In ms - the default simulation duration."),
            DisplayName("Simulation Duration"),
            Category("Simulation")]
        public int SimulationEndTime { get; set; }= 1000;

        [Description("In ms - the default duration to skip."),
            DisplayName("Simulation Skip Duration"),
            Category("Simulation")]
        public int SimulationSkipTime { get; set; }= 0;

        [Description("In ms - the default time unit."),
            DisplayName("Default δt"),
            Category("Simulation")]
        public double SimulationDeltaT { get; set; }= 0.1;

        [Description("Whether junction level current tracking is on or off. For larger models turning it off is recommended."),
            DisplayName("Junction level current tracking"),
            Category("Simulation")]
        public bool JunctionLevelTracking { get; set; }= false;

        #endregion

        [Description("If set to false, any single value will be set as the center of the distribution without any randomization."),
            DisplayName("Flicker Off"),
            Category("Randomization")]
        public bool FlickerOff { get; set; } = false;

        [Description("The seed to initialize the random number generator."), 
            DisplayName("Random Seed"), 
            Category("Randomization")]
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
