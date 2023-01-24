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
        private string target;//used to temporarily hold the target cell's id while reading the JSON files
        public string Target { get => Cell2.ID; set => target = value; }
        public double Conductance { get; set; } = 0;
        public Cell Cell1;
        public Cell Cell2;
        public int Duration { get; set; } //The number of time units (dt) it will take for the current to travel from one neuron to the other
        public int Delay { get; set; } = 0; //Extra number of time units (dt) to add to Duration

        private double VoltageDiffFrom1To2 = 0; //momentary voltage difference that causes outgoing current
        private double VoltageDiffFrom2To1 = 0; //momentary voltage difference that causes incoming current
                                                //
                                                //Voltage Diff * 1/2 * conductance will give the momentary current value
        private double VoltageDiff { get { return VoltageDiffFrom2To1 - VoltageDiffFrom1To2; } }
        int t_current = 0; //the time point  where the momentary values are kept for
        internal bool IsActive(int timepoint)
        {
            double t_ms = Cell1.Model.RunParam.GetTimeOfIndex(timepoint);
            return TimeLine_ms?.IsActive(t_ms) ?? true;
        }

        public double[] InputCurrent; //Current vector

        [JsonIgnore]
        public override string ID { get { return $"Gap: {Cell1.ID} -> {Cell2.ID}; Conductance: {Conductance:0.#####}"; } }

        public GapJunction()
        { }
        public GapJunction(double conductance, Cell c1, Cell c2, DistanceMode mode)
        {
            Conductance = conductance;
            Cell1 = c1;
            Cell2 = c2;
            double distance = Util.Distance(c1.Coordinate, c2.Coordinate, mode);
            if (c1.ConductionVelocity < GlobalSettings.Epsilon)
                Duration = int.MaxValue;
            else
                Duration = Math.Max((int)(distance / (c1.ConductionVelocity * Cell1.Model.RunParam.DeltaT)), 1);
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
            Delay = (int)(delay / Cell1.Model.RunParam.DeltaT);
        }
        public void SetTimeSpan(TimeLine span)
        {
            TimeLine_ms = span;
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
