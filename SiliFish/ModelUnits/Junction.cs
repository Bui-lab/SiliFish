using System;

using SiliFish.DataTypes;

namespace SiliFish.ModelUnits
{
    public class InterPoolTemplate: IComparable<InterPoolTemplate>
    {
        private string poolSource, poolTarget;
        public string PoolSource
        {
            get { return poolSource; }
            set
            {
                bool rename = GeneratedName() == Name;
                poolSource = value;
                if (rename) Name = GeneratedName();
            }
        }
        public string PoolTarget
        {
            get { return poolTarget; }
            set
            {
                bool rename = GeneratedName() == Name;
                poolTarget = value;
                if (rename) Name = GeneratedName();
            }
        }
        public CellReach CellReach { get; set; }
        public AxonReachMode AxonReachMode { get; set; } = AxonReachMode.NotSet;
        public JunctionType JunctionType { get; set; } = JunctionType.NotSet;
        public DistanceMode DistanceMode
        {
            get { return CellReach.DistanceMode; }
            set { CellReach.DistanceMode = value; }
        }

        private string _Name;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_Name))
                    _Name = GeneratedName();
                return _Name;
            }
            set { _Name = value; }
        }
        public string Description { get; set; }
        public SynapseParameters SynapseParameters { get; set; }//valid if junctionType is Synapse or NMJ
        public bool Active { get; set; } = true;
        public TimeLine TimeLine { get; set; } = new TimeLine();

        public InterPoolTemplate()
        { }

        public InterPoolTemplate(InterPoolTemplate ipt)
        {
            Name = ipt.Name;
            Description = ipt.Description;
            PoolSource = ipt.PoolSource;
            PoolTarget = ipt.PoolTarget;
            CellReach = new CellReach(ipt.CellReach);
            AxonReachMode = ipt.AxonReachMode;
            JunctionType = ipt.JunctionType;
            SynapseParameters = new SynapseParameters(ipt.SynapseParameters);
            Active = ipt.Active;
            TimeLine = new TimeLine(ipt.TimeLine);
        }

        public string GeneratedName()
        {
            return String.Format("{0}-->{1}", (string.IsNullOrEmpty(PoolSource) ? "__" : PoolSource),
                (string.IsNullOrEmpty(PoolTarget) ? "__" : PoolTarget));
        }
        public override string ToString()
        {
            string activeStatus = Active && TimeLine.IsBlank() ? "" :
                Active ? " (timeline)" : " (inactive)";
            return String.Format("{0} [{1}]/{2}{3}", Name, JunctionType.ToString(), AxonReachMode.ToString(), activeStatus);
        }

        public string GetTooltip()
        {
            return $"{Name}\r\n" +
                $"{Description}\r\n" +
                $"From {PoolSource} to {PoolTarget}\r\n" +
                $"Reach: {CellReach?.GetTooltip()}\r\n" +
                $"Mode: {AxonReachMode}\r\n" +
                $"Type: {JunctionType}\r\n" +
                $"Parameters: {SynapseParameters?.GetTooltip()}\r\n" +
                $"TimeLine: {TimeLine}\r\n" +
                $"Active: {Active}";
        }

        public int CompareTo(InterPoolTemplate other)
        {
            int c = this.PoolSource.CompareTo(other.PoolSource);
            if (c != 0) return c;
            c = this.PoolTarget.CompareTo(other.PoolTarget);
            if (c != 0) return c;
            return this.JunctionType.CompareTo(other.JunctionType);
        }
    }
    public class InterPool
    {
        internal CellPool poolSource, poolTarget;
        internal CellReach reach;
        internal SynapseParameters synapseParameters;
        internal bool IsChemical { get { return synapseParameters != null; } }
        internal TimeLine timeLine;
    }

    public class CellReach
    {
        /// <summary>
        /// Ascending reach checks only at the x axis
        /// </summary>
        public double AscendingReach { get; set; }
        /// <summary>
        /// Descending reach checks only the x axis
        /// </summary>
        public double DescendingReach { get; set; }
        /// <summary>
        /// Min reach checks the calculated distance (Euclidean or else)
        /// </summary>
        public double MinReach { get; set; } = 0.2;
        /// <summary>
        /// Max reach checks the calculated distance (Euclidean or else)
        /// </summary>
        public double MaxReach { get; set; } = 1000;
        public DistanceMode DistanceMode { get; set; } = DistanceMode.Euclidean;
        public double? FixedDuration_ms { get; set; } = null;// in ms
        public double Delay_ms { get; set; } = 0;//in ms
        public double Weight { get; set; }

        public CellReach() { }
        public CellReach(CellReach cr)
        {
            AscendingReach = cr.AscendingReach;
            DescendingReach = cr.DescendingReach;
            MinReach = cr.MinReach;
            MaxReach = cr.MaxReach;
            FixedDuration_ms = cr.FixedDuration_ms;
            Delay_ms = cr.Delay_ms;
            Weight = cr.Weight;
        }
        internal object GetTooltip()
        {
            return $"Ascending: {AscendingReach:0.###}\r\n" +
                $"Descending: {DescendingReach:0.###}\r\n" +
                $"MinReach: {MinReach: 0.###}\r\n" +
                $"MaxReach: {MaxReach: 0.###}\r\n" +
                $"Fixed Duration: {FixedDuration_ms: 0.###}\r\n" +
                $"Delay: {Delay_ms: 0.###}\r\n" +
                $"Weight: {Weight: 0.###}\r\n";
        }

        /// <summary>
        /// Checks whether cell1 can reach to cell2 with the current reach settings
        /// </summary>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        public bool WithinReach(Cell cell1, Cell cell2, double noise)
        {
            double diff_x = (cell1.x - cell2.x) * noise;//positive values mean cell1 is more caudal
            if (diff_x > 0 && diff_x > AscendingReach) //Not enough ascending reach 
                return false;
            else if (diff_x < 0 && Math.Abs(diff_x) > DescendingReach) //Not enough descending reach 
                return false;
            double dist = Global.Distance(cell1, cell2, DistanceMode) *  noise;
            return dist >= MinReach && dist <= MaxReach;
        }

    }

    public class SynapseParameters
    {
        public double TauD { get; set; }
        public double TauR { get; set; }
        public double VTh { get; set; }
        public double E_rev { get; set; }

        public SynapseParameters() { }
        public SynapseParameters(SynapseParameters sp) 
        {
            if (sp == null) return;
            TauD = sp.TauD; 
            TauR = sp.TauR;
            VTh = sp.VTh; 
            E_rev = sp.E_rev;
        }

        internal object GetTooltip()
        {
            return $"Tau D: {TauD:0.###}\r\n" +
                $"Tau R: {TauR:0.###}\r\n" +
                $"V thresh: {VTh:0.###}\r\n" +
                $"E rev: {E_rev:0.###}";
        }
    }
    public class GapJunction
    {
        public double Conductance = 0;
        public Cell Cell1;
        public Cell Cell2;
        public int Duration; //The number of time units (dt) it will take for the current to travel from one neuron to the other
        public int Delay = 0; //Extra number of time units (dt) to add to Duration

        private double VoltageDiff1To2 = 0; //momentary outgoing current value
        private double VoltageDiffFrom2To1 = 0; //momentary incoming current value
        private double VoltageDiff { get { return VoltageDiffFrom2To1 - VoltageDiff1To2; } } //momentary current value
        int t_current = 0; //the time point  where the momentary values are kept for
        private TimeLine timeLine_ms;
        internal bool IsActive(int timepoint)
        {
            return timeLine_ms?.IsActive((int)(timepoint * SwimmingModel.dt)) ?? true;
        }

        public double[] InputCurrent; //Current vector

        public string ID { get { return string.Format("Gap: {0} -> {1}; Conductance: {2:0.#####}", Cell1.ID, Cell2.ID, Conductance); } }

        public GapJunction(double conductance, Cell c1, Cell c2, DistanceMode mode)
        {
            Conductance = conductance;
            Cell1 = c1;
            Cell2 = c2;
            double distance = Global.Distance(c1, c2, mode);
            Duration = Math.Max((int)(distance / (c1.ConductionVelocity * SwimmingModel.dt)), 1);
        }
        public void InitVectors(int nmax)
        {
            InputCurrent = new double[nmax];
            InputCurrent[0] = 0;
        }

        public void SetFixedDuration(int dur)
        {
            Duration = dur;
        }
        public void SetDelay(double delay)
        {
            Delay = (int)(delay / SwimmingModel.dt);
        }
        public void SetTimeSpan(TimeLine span)
        {
            timeLine_ms = span;
        }

        public void NextStep(int t)
        {
            if (t <= 0) return;
            t_current = t;
            int tt = Duration + Delay;
            double v1 = tt <= t ? Cell1.V[t - tt] : 0;
            double v2 = Cell2.V[t - 1];
            VoltageDiff1To2 = v1 - v2;
            v1 = tt <= t ? Cell2.V[t - tt] : 0;
            v2 = Cell1.V[t - 1];
            VoltageDiffFrom2To1 = v1 - v2;
        }

        public double GetGapCurrent(Neuron n, int t)
        {
            double current = IsActive(t) ? Conductance * VoltageDiff : 0;

            if (n == Cell1)
                return current;
            else
            {
                current = -1 * current;
                InputCurrent[t_current] = current;
                return current;
            }
        }
    }

    public class ChemicalJunction
    {
        public TwoExp_syn Core;
        public Neuron PreNeuron;
        public Cell PostCell; //can be a neuron or a muscle cell
        public int Duration; //The number of time units (dt) it will take for the current to travel from presynaptic to postsynaptic neuron
        public int Delay = 0; //Extra number of time units (dt) to add to Duration
        public double ISynA = 0; //the momentary current value
        public double ISynB = 0; //the momentary current value
        int t_current = 0; //the time point  where the momentary values are kept for
        public double ISyn { get { return ISynA - ISynB; } }
        public double[] InputCurrent; //Current vector 
        private TimeLine timeLine;
        public string ID { get { return string.Format("Syn: {0} -> {1}; Conductance: {2:0.#####}", PreNeuron.ID, PostCell.ID, Conductance); } }
        public double Conductance {get{return Core.Conductance;} }
        internal bool IsActive(int timepoint)
        {
            return timeLine?.IsActive((int)(timepoint * SwimmingModel.dt)) ?? true;
        }

        public ChemicalJunction(Neuron preN, Cell postN, SynapseParameters param, double conductance, DistanceMode distmode)
        {
            Core = new TwoExp_syn(param, conductance);
            PreNeuron = preN;
            PostCell = postN;
            double distance = Global.Distance(PreNeuron, PostCell, distmode);
            Duration = Math.Max((int)(distance / (preN.ConductionVelocity * SwimmingModel.dt)), 1);
        }
        public void InitVectors(int nmax)
        {
            InputCurrent = new double[nmax];
            InputCurrent[0] = 0;
        }

        public void SetFixedDuration(double dur)
        {
            Duration = (int)(dur / SwimmingModel.dt);
        }
        public void SetDelay(double delay)
        {
            Delay = (int) (delay/SwimmingModel.dt);
        }
        public void SetTimeLine(TimeLine span)
        {
            timeLine = span;
        }
        public void NextStep(int t)
        {
            t_current = t;
            int tt = Duration + Delay;
            double vPre = tt <= t ? PreNeuron.V[t - tt] : 0;
            double vPost = t > 0 ? PostCell.V[t - 1] : 0;
            (ISynA, ISynB) = Core.GetNextVal(vPre, vPost, ISynA, ISynB);
        }
        public double GetSynapticCurrent(int t)
        {
            double current = IsActive(t) ? ISyn : 0;
            InputCurrent[t] = current;
            return current;
        }
    }

}
