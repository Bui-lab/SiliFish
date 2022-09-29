using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiliFish.Definitions
{
    public enum CellType { Neuron, MuscleCell }
    public enum CoreType {Izhikevich_9P, Leaky_Integrator }

    public enum FiringPattern { Phasic, Tonic, PhasicBursting, TonicBursting, Chattering, Unknown}
    public enum BodyLocation { SpinalCord, Body }

    public enum CountingMode { PerSomite, Total }

    public enum FrontalPlane
    {
        [Display(Name = "Not Set")] NotSet,
        Dorsal,
        Ventral
    }
    public enum TransversePlane { NotSet, Anterior, Central, Posterior }
    public enum SagittalPlane
    {
        Left, Right,
        [Display(Name = "Left/Right")] Both
    }

    public enum PlotType
    {
        [Display(Name = "")] NotSet,
        [Display(Name = "Memb. Potential")] MembPotential,
        [Display(Name = "Current"), Description("Gap and chemical currents")] Current,
        [Display(Name = "Gap Current")] GapCurrent,
        [Display(Name = "Synaptic Current")] ChemCurrent,
        [Display(Name = "Stimuli")] Stimuli,
        [Display(Name = "Full Dynamics")] FullDyn,
        //[Display(Name = "Body Angle Heat Map")] BodyAngleHeatMap,
        [Display(Name = "Episodes")] Episodes
    }

    public enum PlotSelection
    {
        [Display(Name = "All")] All,
        [Display(Name = "Single")] Single,
        [Display(Name = "First/Middle/Last")] FirstMiddleLast,
        [Display(Name = "Random")] Random,
        [Display(Name = "Summary")] Summary
    }

    public enum AxonReachMode { NotSet, Ipsilateral, Contralateral, Bilateral }
    public enum DistanceMode { Euclidean, Manhattan } //FUTURE_IMPROVEMENT, Chebyshev, Haversine }
    public enum ConnectionType { NotSet, Synapse, Gap, NMJ }
    public enum StimulusMode { Step, Gaussian, Ramp, Sinusoidal, Pulse }
    public enum NeuronClass { NotSet, Glycinergic, GABAergic, Glutamatergic, Cholinergic, Modulatory, Mixed }

    public enum UnitOfMeasure
    {
        [Display(Name = "mV/pA/GΩ/pF"), Description("Voltage: mV; Current: pA; Resistance: GΩ; Capacitance: pF")] milliVolt_picoAmpere_GigaOhm_picoFarad,
        [Display(Name = "mV/nA/MΩ/nF"), Description("Voltage: mV; Current: nA; Resistance: MΩ; Capacitance: nF")] milliVolt_nanoAmpere_MegaOhm_nanoFarad
    }

    public enum Measure
    {
        Voltage,
        Current,
        Resistance,
        Capacitance
    }
}
