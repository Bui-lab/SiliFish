using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Junction
{
    public class GapJunction: JunctionBase
    {
        //public bool Bidirectional = true;//TODO currently unidirectional or different conductance gap junctions are not handled
          private int duration; //The number of time units (dt) it will take for the current to travel from one neuron to the other
        private double VoltageDiffFrom1To2 = 0; //momentary voltage difference that causes outgoing current
        private double VoltageDiffFrom2To1 = 0; //momentary voltage difference that causes incoming current
                                                //
                                                //Voltage Diff * 1/2 * conductance will give the momentary current value
        private double VoltageDiff { get { return VoltageDiffFrom2To1 - VoltageDiffFrom1To2; } }
        private int t_current = 0; //the time point  where the momentary values are kept for        public override string Target { get => Cell2.ID; set => target = value; }
        public Cell Cell1;
        public Cell Cell2;

        internal bool IsActive(int timepoint)
        {
            double t_ms = Cell1.Model.RunParam.GetTimeOfIndex(timepoint);
            return TimeLine_ms?.IsActive(t_ms) ?? true;
        }

        public double[] InputCurrent; //Current vector

        [JsonIgnore]
        public override string ID { get { return $"Gap: {Cell1.ID} -> {Cell2.ID}; Conductance: {Weight:0.#####}"; } }

        public GapJunction()
        { }
        public GapJunction(double conductance, Cell c1, Cell c2, DistanceMode distmode)
        {
            Weight = conductance;
            Cell1 = c1;
            Cell2 = c2;
            DistanceMode = distmode;
        }

        public void LinkObjects(RunningModel model)
        {
            CellPool cp = model.CellPools.Where(cp => cp.Cells.Exists(c => c.ID == target)).FirstOrDefault();
            Cell2 = cp.GetCell(target);
            Cell2.GapJunctions.Add(this);
        }
        public override string ToString()
        {
            string activeStatus = Active && TimeLine_ms.IsBlank() ? "" : Active ? " (timeline)" : " (inactive)";
            return $"{ID}{activeStatus}";
        }
        public void InitForSimulation(int nmax)
        {
            InputCurrent = new double[nmax];
            InputCurrent[0] = 0;
            if (FixedDuration_ms != null)
                duration = (int)(FixedDuration_ms / Cell1.Model.RunParam.DeltaT);
            else
            {
                double distance = Util.Distance(Cell1.Coordinate, Cell2.Coordinate, DistanceMode);
                int delay = (int)(Delay_ms / Cell1.Model.RunParam.DeltaT);
                if (Cell1.ConductionVelocity < GlobalSettings.Epsilon)
                    duration = int.MaxValue;
                else
                {
                    duration = Math.Max((int)(distance / (Cell1.ConductionVelocity * Cell1.Model.RunParam.DeltaT)), 1);
                    duration += delay;
                }
            }
        }

        public void SetFixedDuration(double dur)
        {
            FixedDuration_ms = dur;
        }
        public void SetDelay(double delay)
        {
            Delay_ms = delay;
        }
        public void SetTimeSpan(TimeLine span)
        {
            TimeLine_ms = span;
        }

        public void NextStep(int tIndex)
        {
            if (tIndex <= 0) return;
            t_current = tIndex;
            int tt = duration;
            double v1 = tt <= tIndex ? Cell1.V[tIndex - tt] : Cell1.RestingMembranePotential;
            double v2 = Cell2.V[tIndex - 1];
            VoltageDiffFrom1To2 = v1 - v2;
            v2 = tt <= tIndex ? Cell2.V[tIndex - tt] : Cell2.RestingMembranePotential;
            v1 = Cell1.V[tIndex - 1];
            VoltageDiffFrom2To1 = v2 - v1;
        }

        public double GetGapCurrent(Cell n, int tIndex)
        {
            double current = IsActive(tIndex) ? Weight * VoltageDiff : 0;

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
