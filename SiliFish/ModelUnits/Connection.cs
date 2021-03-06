using System;
using SiliFish.DataTypes;

namespace SiliFish.ModelUnits
{
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
            double diff_x = (cell1.X - cell2.X) * noise;//positive values mean cell1 is more caudal
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
        //public bool Bidirectional = true;//TODO currently unidirectional or different conductance gap junctions are not handled
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
            return timeLine_ms?.IsActive((int)(timepoint * RunParam.dt)) ?? true;
        }

        public double[] InputCurrent; //Current vector

        public string ID { get { return string.Format("Gap: {0} -> {1}; Conductance: {2:0.#####}", Cell1.ID, Cell2.ID, Conductance); } }

        public GapJunction(double conductance, Cell c1, Cell c2, DistanceMode mode)
        {
            Conductance = conductance;
            Cell1 = c1;
            Cell2 = c2;
            double distance = Global.Distance(c1, c2, mode);
            Duration = Math.Max((int)(distance / (c1.ConductionVelocity * RunParam.dt)), 1);
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
            Delay = (int)(delay / RunParam.dt);
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
    public class ChemicalSynapse
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
            return timeLine?.IsActive((int)(timepoint * RunParam.dt)) ?? true;
        }

        public ChemicalSynapse(Neuron preN, Cell postN, SynapseParameters param, double conductance, DistanceMode distmode)
        {
            Core = new TwoExp_syn(param, conductance);
            PreNeuron = preN;
            PostCell = postN;
            double distance = Global.Distance(PreNeuron, PostCell, distmode);
            Duration = Math.Max((int)(distance / (preN.ConductionVelocity * RunParam.dt)), 1);
        }
        public void InitVectors(int nmax)
        {
            InputCurrent = new double[nmax];
            InputCurrent[0] = 0;
        }

        public void SetFixedDuration(double dur)
        {
            Duration = (int)(dur / RunParam.dt);
        }
        public void SetDelay(double delay)
        {
            Delay = (int)(delay / RunParam.dt);
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
