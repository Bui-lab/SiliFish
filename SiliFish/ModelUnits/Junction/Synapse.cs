using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using System;

namespace SiliFish.ModelUnits
{
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
    public class ChemicalSynapse
    {
        public TwoExp_syn Core;
        public Neuron PreNeuron;
        public Cell PostCell; //can be a neuron or a muscle cell
        public int Duration; //The number of time units (dt) it will take for the current to travel from presynaptic to postsynaptic neuron
        public int Delay = 0; //Extra number of time units (dt) to add to Duration
        public double ISynA = 0; //the momentary current value
        public double ISynB = 0; //the momentary current value
        public double ISyn { get { return ISynA - ISynB; } }
        public double[] InputCurrent; //Current vector 
        private TimeLine timeLine_ms;
        public string ID { get { return string.Format("Syn: {0} -> {1}; Conductance: {2:0.#####}", PreNeuron.ID, PostCell.ID, Conductance); } }
        public double Conductance { get { return Core.Conductance; } }
        internal bool IsActive(int timepoint)
        {
            double t_ms = RunParam.GetTimeOfIndex(timepoint);
            return timeLine_ms?.IsActive(t_ms) ?? true;
        }

        public ChemicalSynapse(Neuron preN, Cell postN, SynapseParameters param, double conductance, DistanceMode distmode)
        {
            Core = new TwoExp_syn(param, conductance);
            PreNeuron = preN;
            PostCell = postN;
            double distance = Util.Distance(PreNeuron.coordinate, PostCell.coordinate, distmode);
            Duration = Math.Max((int)(distance / (preN.ConductionVelocity * RunParam.static_dt)), 1);
        }
        public void InitDataVectors(int nmax)
        {
            InputCurrent = new double[nmax];
            InputCurrent[0] = 0;
        }

        public void SetFixedDuration(double dur)
        {
            Duration = (int)(dur / RunParam.static_dt);
        }
        public void SetDelay(double delay)
        {
            Delay = (int)(delay / RunParam.static_dt);
        }
        public void SetTimeLine(TimeLine span)
        {
            timeLine_ms = span;
        }
        public void NextStep(int tIndex)
        {
            int tt = Duration + Delay;
            double vPre = tt <= tIndex ? PreNeuron.V[tIndex - tt] : PreNeuron.RestingMembranePotential;
            double vPost = tIndex > 0 ? PostCell.V[tIndex - 1] : 0;
            (ISynA, ISynB) = Core.GetNextVal(vPre, vPost, ISynA, ISynB);
        }
        public double GetSynapticCurrent(int tIndex)
        {
            double current = IsActive(tIndex) ? ISyn : 0;
            InputCurrent[tIndex] = current;
            return current;
        }
    }

}
