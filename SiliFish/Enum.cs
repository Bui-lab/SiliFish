namespace SiliFish
{
    public enum CellType { Neuron, MuscleCell }
    public enum BodyLocation { SpinalCord, Body}

    public enum CountingMode { PerSomite, Total }

    public enum FrontalPlane { NotSet, Dorsal, Ventral }
    public enum TransversePlane { NotSet, Anterior, Central, Posterior }
    public enum SagittalPlane { NotSet, Left, Right, Both }

    public enum PlotExtend { FullModel, SingleCell, CellsInAPool, SinglePool, OppositePools }

    public enum AxonReachMode { NotSet, Ipsilateral, Contralateral, Bilateral }
    public enum DistanceMode { Euclidean, Manhattan, Chebyshev, Haversine }
    public enum ConnectionType { NotSet, Synapse, Gap, NMJ}
    public enum StimulusMode { None, Step, Gaussian, Ramp}
    public enum NeuronClass { NotSet, Glycinergic, GABAergic, Glutamatergic, Cholinergic, Modulatory, Mixed}

}
