using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiliFish.Definitions
{
    //To convert string to enum:
    //ex: CellType cellType = (CellType)Enum.Parse(typeof(CellType), ddCellType.Text);
    
    //To fill dropdown with enum:
    //ddBodyPosition.DataSource = Enum.GetNames(typeof(BodyLocation));
    public enum CellType { Neuron, MuscleCell }
    public enum FiringRhythm { NoSpike, Phasic, Tonic }
    public enum FiringPattern { NoSpike, Spiking, Bursting, Chattering, Mixed }

    public enum BodyLocation { SpinalCord, MusculoSkeletal, SupraSpinal }

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
        [Display(Name = "Terminal Current")] ChemOutCurrent,
        [Display(Name = "Stimuli")] Stimuli,
        [Display(Name = "Full Dynamics")] FullDyn,
        [Display(Name = "Muscle Tension")] Tension,
        [Display(Name = "Junction")] Junction,
        [Display(Name = "Episodes")] Episodes
    }

    public enum PlotSomiteSelection
    {
        [Display(Name = "All")] All,
        [Display(Name = "Single")] Single,
        [Display(Name = "First/Middle/Last")] FirstMiddleLast,
        [Display(Name = "Random")] Random,
        [Display(Name = "Rostral to")] RostralTo,
        [Display(Name = "Caudal to")] CaudalTo,
    }
    public enum PlotCellSelection
    {
        [Display(Name = "All")] All,
        [Display(Name = "Single")] Single,
        [Display(Name = "First/Middle/Last")] FirstMiddleLast,
        [Display(Name = "Random")] Random,
        [Display(Name = "Spiking")] Spiking,
        [Display(Name = "Non-Spiking")] NonSpiking
    }

    public enum AxonReachMode { NotSet, Ipsilateral, Contralateral, Bilateral }
    public enum DistanceMode { Euclidean, Manhattan } //FUTURE_IMPROVEMENT, Chebyshev, Haversine }
    public enum ConnectionType { NotSet, Synapse, Gap, NMJ }
    public enum StimulusMode { Step, Gaussian, Ramp, Sinusoidal, Pulse }
    public enum NeuronClass { NotSet, Glycinergic, GABAergic, Glutamatergic, Cholinergic, Modulatory, Mixed }
    public enum CellInputMode { NotSet, Excitatory, Inhibitory, Modulatory, Electrical}

    public enum UnitOfMeasure
    {
        [Display(Name = "mV/pA/GΩ/pF/nS"), 
            Description("Voltage: mV; Current: pA; Resistance: GΩ; Capacitance: pF; Conductance: nS")] 
        milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens,
        [Display(Name = "mV/nA/MΩ/nF/µS"), 
            Description("Voltage: mV; Current: nA; Resistance: MΩ; Capacitance: nF; Conductance: µS")] 
        milliVolt_nanoAmpere_MegaOhm_nanoFarad_microSiemens
    }

    public enum Measure
    {
        Voltage,
        Current,
        Resistance,
        Capacitance,
        Conductance
    }
}
