using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Model;
using System;

namespace SiliFish.ModelUnits
{
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
            Delay = (int)(delay / RunParam.static_dt);
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
            double current = IsActive(tIndex) ? Conductance * VoltageDiff : 0;

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
 
}
