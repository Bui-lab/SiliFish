using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using SiliFish.Services;
using System.Reflection;
using System.Xml.Linq;

namespace SiliFish.ModelUnits.Junction
{
    public class GapJunction: JunctionBase
    {
        //public bool Bidirectional = true;//TODO currently unidirectional or different conductance gap junctions are not handled
        /// <summary>
        /// The number of time units (dt) it will take for the current to travel from cell1 to cell2
        /// Calculated at the begining of simulation
        /// </summary>
        private int duration1;
        /// <summary>
        /// The number of time units (dt) it will take for the current to travel from cell2 to cell1
        /// Calculated at the begining of simulation
        /// </summary>
        private int duration2; 
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

        public override string CSVExportValues
        {
            get => $"{ConnectionType.Gap},{Cell1.ID},{Cell2.ID}, " +
                $"{DistanceMode}, " +
                $"{SynapseParameters.CSVExportBlankValues}," +
                $"{Weight},{FixedDuration_ms},{Delay_ms}," +
                $"{Active}," +
                $"{TimeLine_ms?.CSVExportValues}";
            set
            {
                try
                {
                    int iter = 1;//junction type is already read before junction creation
                    string[] values = value.Split(',');
                    if (values.Length < CSVExportColumCount - TimeLine.CSVExportColumnNames.Length) return;
                    /*PoolSource = values[iter++].Trim();
                    PoolTarget = values[iter++].Trim();

                    AxonReachMode = (AxonReachMode)Enum.Parse(typeof(AxonReachMode), values[iter++]);
                    ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), values[iter++]);
                    DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), values[iter++]);
                    string cellReachString = string.Join(',', values[iter..(iter + CellReach.CSVExportColumCount)]);
                    CellReach.CSVExportValues = cellReachString;
                    iter += CellReach.CSVExportColumCount;
                    string synapseString = string.Join(',', values[iter..(iter + SynapseParameters.CSVExportColumCount)]);
                    if (!string.IsNullOrEmpty(synapseString.Replace(',', ' ').Trim()))
                    {
                        SynapseParameters = new()
                        {
                            CSVExportValues = synapseString
                        };
                    }
                    iter += SynapseParameters.CSVExportColumCount;
                    Probability = double.Parse(values[iter++]);
                    Weight = double.Parse(values[iter++]);
                    if (double.TryParse(values[iter++], out double d))
                        FixedDuration_ms = d;
                    Delay_ms = double.Parse(values[iter++]);
                    Active = bool.Parse(values[iter++]);
                    if (iter < values.Length)
                        TimeLine_ms.CSVExportValues = values[iter++];*/
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw;
                }
            }
        }

        public GapJunction()
        { }
        public GapJunction(double conductance, Cell c1, Cell c2, DistanceMode distmode)
        {
            Weight = conductance;
            Cell1 = c1;
            Cell2 = c2;
            Target = Cell2.ID;
            DistanceMode = distmode;
        }

        public GapJunction(GapJunction gapJunction) : base(gapJunction)
        {
            Cell1 = gapJunction.Cell1;
            Cell2 = gapJunction.Cell2;
        }

        public override void LinkObjects()
        {
            Cell1.GapJunctions.Add(this);
            Cell2.GapJunctions.Add(this);
        }
        public override void UnlinkObjects()
        {
            Cell1.GapJunctions.Remove(this);
            Cell2.GapJunctions.Remove(this);
        }
        public void LinkObjects(RunningModel model)
        {
            CellPool cp = model.CellPools.Where(cp => cp.Cells.Exists(c => c.ID == Target)).FirstOrDefault();
            if (cp is null) return;
            Cell2 = cp.GetCell(Target);
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
            double deltaT = Cell1.Model.RunParam.DeltaT;
            if (FixedDuration_ms != null)
                duration1 = duration2 = (int)(FixedDuration_ms / Cell1.Model.RunParam.DeltaT);
            else
            {
                double distance = Util.Distance(Cell1.Coordinate, Cell2.Coordinate, DistanceMode);
                int delay = (int)(Delay_ms / deltaT);
                duration1 = Math.Max((int)(distance / (Cell1.ConductionVelocity * deltaT)), 1);
                duration1 += delay;
                duration2 = Math.Max((int)(distance / (Cell2.ConductionVelocity * deltaT)), 1);
                duration2 += delay;
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
            double v1 = duration1 <= tIndex ? Cell1.V[tIndex - duration1] : Cell1.RestingMembranePotential;
            double v2 = Cell2.V[tIndex - 1];
            VoltageDiffFrom1To2 = v1 - v2;
            v2 = duration2 <= tIndex ? Cell2.V[tIndex - duration2] : Cell2.RestingMembranePotential;
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
