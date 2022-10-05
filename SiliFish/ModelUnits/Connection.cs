﻿using System;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;

namespace SiliFish.ModelUnits
{
    public class CellReach
    {
        #region Member Properties
        // Ascending reach checks only at the x axis, or the somite
        public double AscendingReach { get; set; }
        // Descending reach checks only the x axis, or the somite
        public double DescendingReach { get; set; }

        // Medial reach checks the y axis, towards to center
        public double MedialReach { get; set; } = 1000;
        // Lateral reach checks the y axis, outwards
        public double LateralReach { get; set; } = 1000;

        // Dorsal reach checks the z axis
        public double DorsalReach { get; set; } = 1000;
        // Ventral reach checks the z axis
        public double VentralReach { get; set; } = 1000;

        // Min reach checks the calculated distance (Euclidean or else)
        public double MinReach { get; set; } = 0.2;
        // Max reach checks the calculated distance (Euclidean or else)
        public double MaxReach { get; set; } = 1000;

        // MaxOutgoing shows how many junctions the source cell can have
        public int MaxOutgoing { get; set; } = 0;
        // MaxIncoming shows how many junctions the target cell can have
        public int MaxIncoming { get; set; } = 0;

        public DistanceMode DistanceMode { get; set; } = DistanceMode.Euclidean;
        public double? FixedDuration_ms { get; set; } = null;// in ms
        public double Delay_ms { get; set; } = 0;//in ms
        public double Weight { get; set; }

        public bool WithinSomite { get; set; } = true;
        public bool OtherSomite { get; set; } = true;
        public bool Autapse { get; set; } = false;
        public bool SomiteBased { get; set; } = false;

        string SomiteReach
        {
            get {
                return WithinSomite && OtherSomite ? "Same and other" :
                    WithinSomite ? "Same somite" :
                    OtherSomite ? "Other somite" :
                    "";
            }
        }
        #endregion
        public CellReach() { }
        public CellReach(CellReach cr)
        {
            AscendingReach = cr.AscendingReach;
            DescendingReach = cr.DescendingReach;
            LateralReach = cr.LateralReach;
            MedialReach = cr.MedialReach;
            DorsalReach = cr.DorsalReach;
            VentralReach = cr.VentralReach;
            MinReach = cr.MinReach;
            MaxReach = cr.MaxReach;
            MaxIncoming = cr.MaxIncoming;
            MaxOutgoing = cr.MaxOutgoing;
            FixedDuration_ms = cr.FixedDuration_ms;
            Delay_ms = cr.Delay_ms;
            Weight = cr.Weight;
            WithinSomite = cr.WithinSomite;
            OtherSomite = cr.OtherSomite;
            Autapse = cr.Autapse;
            SomiteBased = cr.SomiteBased;
        }
        internal object GetTooltip()
        {
            string reach =
                (AscendingReach > 0 ? $"Ascending: {AscendingReach:0.###}\r\n" : "") +
                (DescendingReach > 0 ? $"Descending: {DescendingReach:0.###}\r\n" : "") +
                (LateralReach > 0 ? $"Lateral: {LateralReach:0.###}\r\n" : "") +
                (MedialReach > 0 ? $"Medial: {MedialReach:0.###}\r\n" : "") +
                (DorsalReach > 0 ? $"Dorsal: {DorsalReach:0.###}\r\n" : "") +
                (VentralReach > 0 ? $"Ventral: {VentralReach:0.###}\r\n" : "") +
                (MinReach > 0 ? $"Min reach: {MinReach:0.###}\r\n" : "") +
                (MaxReach > 0 ? $"Max reach: {MaxReach:0.###}\r\n" : "");
            return reach +
                (MaxIncoming > 0 ? $"Max in: {MaxIncoming:0}\r\n" : "") +
                (MaxOutgoing > 0 ? $"Max out: {MaxOutgoing:0}\r\n" : "") +
                $"Fixed Duration: {FixedDuration_ms: 0.###}\r\n" +
                $"Delay: {Delay_ms: 0.###}\r\n" +
                $"Weight: {Weight: 0.###}\r\n"+
                $"Somite Reach: {SomiteReach}\r\n";
        }

        /// <summary>
        /// Checks whether cell1 can reach to cell2 with the current reach settings
        /// </summary>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        public bool WithinReach(Cell cell1, Cell cell2, double noise)
        {
            if (!WithinSomite && cell1.Somite == cell2.Somite)
                return false;
            
            if (!OtherSomite && cell1.Somite != cell2.Somite)
                return false;
            
            if (!Autapse && cell1 == cell2)
                return false;
            
            double diff_x = SomiteBased ? cell2.Somite - cell1.Somite :
                (cell2.X - cell1.X) * noise;//positive values mean cell2 is more caudal
            if (diff_x > 0 && diff_x > DescendingReach) //Not enough descending reach 
                return false;
            else if (diff_x < 0 && Math.Abs(diff_x) > AscendingReach) //Not enough ascending reach 
                return false;
            
            double diff_y = cell2.Y - cell1.Y; //positive values mean cell2 is more lateral
            if (diff_y > 0 && diff_y > LateralReach)
                return false;
            else if (diff_y < 0 && Math.Abs(diff_y) > MedialReach)
                return false;


            double diff_z = cell2.Y - cell1.Y; //positive values mean cell2 is more ventral
            if (diff_z > 0 && diff_z > VentralReach)
                return false;
            else if (diff_z < 0 && Math.Abs(diff_z) > DorsalReach)
                return false;

            double dist = Util.Distance(cell1.coordinate, cell2.coordinate, DistanceMode) *  noise;
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

        private double VoltageDiffFrom1To2 = 0; //momentary voltage difference that causes outgoing current
        private double VoltageDiffFrom2To1 = 0; //momentary voltage difference that causes incoming current
                                                //
        //Voltage Diff * 1/2 * conductance will give the momentary current value
        private double VoltageDiff { get { return VoltageDiffFrom2To1 - VoltageDiffFrom1To2; } } 
        int t_current = 0; //the time point  where the momentary values are kept for
        private TimeLine timeLine_ms;
        internal bool IsActive(int timepoint)
        {
            double t_ms = RunParam.GetTimeOfIndex(timepoint);
            return timeLine_ms?.IsActive(t_ms) ?? true;
        }

        public double[] InputCurrent; //Current vector

        public string ID { get { return string.Format("Gap: {0} -> {1}; Conductance: {2:0.#####}", Cell1.ID, Cell2.ID, Conductance); } }

        public GapJunction(double conductance, Cell c1, Cell c2, DistanceMode mode)
        {
            Conductance = conductance;
            Cell1 = c1;
            Cell2 = c2;
            double distance = Util.Distance(c1.coordinate, c2.coordinate, mode);
            Duration = Math.Max((int)(distance / (c1.ConductionVelocity * RunParam.static_dt)), 1);
        }
        public void InitDataVectors(int nmax)
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
            Delay = (int)(delay / RunParam.static_dt );
        }
        public void SetTimeSpan(TimeLine span)
        {
            timeLine_ms = span;
        }

        public void NextStep(int tIndex)
        {
            if (tIndex <= 0) return;
            t_current = tIndex;
            int tt = Duration + Delay;
            double v1 = tt <= tIndex ? Cell1.V[tIndex - tt] : Cell1.RestingMembranePotential;
            double v2 = Cell2.V[tIndex - 1];
            VoltageDiffFrom1To2 = v1 - v2;
            v2 = tt <= tIndex ? Cell2.V[tIndex - tt] : Cell2.RestingMembranePotential;
            v1 = Cell1.V[tIndex - 1];
            VoltageDiffFrom2To1 = v2 - v1;
        }

        public double GetGapCurrent(Cell n, int tIndex)
        {
            double current = IsActive(tIndex) ? Conductance * VoltageDiff: 0; 

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
        public double ISyn { get { return ISynA - ISynB; } }
        public double[] InputCurrent; //Current vector 
        private TimeLine timeLine_ms;
        public string ID { get { return string.Format("Syn: {0} -> {1}; Conductance: {2:0.#####}", PreNeuron.ID, PostCell.ID, Conductance); } }
        public double Conductance {get{return Core.Conductance;} }
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
            Duration = Math.Max((int)(distance / (preN.ConductionVelocity * RunParam.static_dt )), 1);
        }
        public void InitDataVectors(int nmax)
        {
            InputCurrent = new double[nmax];
            InputCurrent[0] = 0;
        }

        public void SetFixedDuration(double dur)
        {
            Duration = (int)(dur / RunParam.static_dt );
        }
        public void SetDelay(double delay)
        {
            Delay = (int)(delay / RunParam.static_dt );
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
