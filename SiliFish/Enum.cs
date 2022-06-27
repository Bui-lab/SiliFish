using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiliFish
{
    public enum CellType { Neuron, MuscleCell }
    public enum BodyLocation { SpinalCord, Body}

    public enum CountingMode { PerSomite, Total }

    public enum FrontalPlane
    {
        [Display(Name = "Not Set")] NotSet,
        Dorsal,
        Ventral
    }
    public enum TransversePlane { NotSet, Anterior, Central, Posterior }
    public enum SagittalPlane { NotSet, Left, Right, Both }

    public enum PlotExtend { FullModel, SingleCell, CellsInAPool, SinglePool, OppositePools }
    
    public enum PlotType {
        [Display(Name = "Memb. Potential")] MembPotential, 
        [Description("Gap and chemical currents")] Current,
        [Display(Name = "Gap Current")] GapCurrent,
        [Display(Name = "Synaptic Current")] ChemCurrent,
        [Display(Name = "Stimuli")] Stimuli,
        [Display(Name = "Full Dynamics")] FullDyn,
        [Display(Name = "Body Angle Heat Map")] BodyAngleHeatMap,
        Episodes,
        [Display(Name = "Tail Beat Frequency")] TailBeatFrequency,

    }

    public enum AxonReachMode { NotSet, Ipsilateral, Contralateral, Bilateral }
    public enum DistanceMode { Euclidean, Manhattan, Chebyshev, Haversine }
    public enum ConnectionType { NotSet, Synapse, Gap, NMJ}
    public enum StimulusMode { None, Step, Gaussian, Ramp}
    public enum NeuronClass { NotSet, Glycinergic, GABAergic, Glutamatergic, Cholinergic, Modulatory, Mixed}


}
