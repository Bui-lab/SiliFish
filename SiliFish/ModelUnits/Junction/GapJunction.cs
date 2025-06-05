using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Junction
{
    public class GapJunction : JunctionBase
    {
        //public bool Bidirectional = true;//currently unidirectional or different conductance gap junctions are not handled
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
        public Cell Cell1;
        public Cell Cell2;
        protected override int nMax => Cell1?.V.ToArray().Length ?? 0;
        protected override double dt => Cell1?.Model.DeltaT ?? 0.1;
        public override int iDuration => Math.Max(duration1, duration2);

        public override double Duration_ms
        {
            get
            {
                if (FixedDuration_ms != null)
                    return (double)FixedDuration_ms;
                else
                {
                    double distance = Util.Distance(Cell1.Coordinate, Cell2.Coordinate, DistanceMode);
                    return (distance / Cell1.ConductionVelocity) + (Delay_ms ?? 0);
                }
            }
        }


        [JsonIgnore]
        public override string ID
        {
            get
            {
                return
                    (Core as ElecSynapseCore).bidirectional ?
                    $"{Cell1.ID}  ↔ {Cell2.ID}" :
                    $"{Cell1.ID}  → {Cell2.ID}";
            }
        }

        [JsonIgnore]
        public override string Tooltip
        {
            get
            {
                return $"{Name}\r\n" +
                    $"Conductance:{Core.Conductance: 0.#####}\r\n" +
                    $"TimeLine: {TimeLine_ms}\r\n" +
                    $"Active: {Active}";
            }
        }
        public override string SourcePool
        {
            get => Cell1.CellPool.ID;
            set { }
        }
        public override string TargetPool
        {
            get => Cell2.CellPool.ID;
            set { }
        }
        public override List<string> ExportValues()
        {
            return ListBuilder.Build<string>(ID,
                Cell1.ID, Cell2.ID,
                JunctionType.Gap, Core.SynapseType,
                csvExportCoreValues,
                DistanceMode,
                FixedDuration_ms, Delay_ms, Duration_ms,
                Active,
                TimeLine_ms?.ExportValues());
        }
        public override void ImportValues(List<string> values)
        {
            try
            {
                int iter = 1; //skip the first id column
                if (values.Count < ColumnNames.Count - TimeLine.ColumnNames.Count) return;
                Source = values[iter++].Trim();
                Target = values[iter++].Trim();
                iter++; //junction type is already read before junction creation
                string coreType = values[iter++].Trim();
                Dictionary<string, double> parameters = [];
                for (int i = 1; i <= JunctionCore.CoreParamMaxCount; i++)
                {
                    if (iter > values.Count - 2) break;
                    string paramkey = values[iter++].Trim();
                    if (double.TryParse(values[iter++].Trim(), out double paramvalue) && !string.IsNullOrEmpty(paramkey))
                        parameters.Add(paramkey, paramvalue);
                }
                Core = ElecSynapseCore.CreateCore(coreType, parameters);

                DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), values[iter++]);
                if (double.TryParse(values[iter++], out double d))
                    FixedDuration_ms = d;
                if (double.TryParse(values[iter++], out double dd))
                    Delay_ms = dd;
                iter++;//Duration_ms is readonly
                Active = bool.Parse(values[iter++]);
                if (iter < values.Count)
                    TimeLine_ms.ImportValues([values[iter++]]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public GapJunction()
        { }
        public GapJunction(Cell c1, Cell c2, string coreType, Dictionary<string, double> synParams, DistanceMode distmode)
        {
            Cell1 = c1;
            Cell2 = c2;
            Source = Cell1.ID;
            Target = Cell2.ID;
            DistanceMode = distmode;
            Core = ElecSynapseCore.CreateCore(coreType, synParams);
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
        public override void LinkObjects(RunningModel model)
        {
            Cell1 ??= model.GetCell(Source);
            Source ??= Cell1.ID;
            if (!Cell1.GapJunctions.Contains(this))
                Cell1.GapJunctions.Add(this);

            Cell2 = model.GetCell(Target);
            if (!Cell2.GapJunctions.Contains(this))
                Cell2.GapJunctions.Add(this);
        }
        public override string ToString()
        {
            string activeStatus = Active && TimeLine_ms.IsBlank() ? "" : Active ? " (timeline)" : " (inactive)";
            return $"(🗲) {ID}{activeStatus}";
        }
        public override void InitForSimulation(RunParam runParam, ref int uniqueID)
        {
            base.InitForSimulation(runParam, ref uniqueID);
            RunningModel model = Cell1.Model;
            if (FixedDuration_ms != null)
                duration1 = duration2 = (int)(FixedDuration_ms / dt);
            else
            {
                double distance = Util.Distance(Cell1.Coordinate, Cell2.Coordinate, DistanceMode);
                int delay = (int)((Delay_ms ?? model.Settings.gap_delay) / runParam.DeltaT);
                duration1 = Math.Max((int)Math.Ceiling(distance / (Cell1.ConductionVelocity * dt)), 1);
                duration1 += delay;
                duration2 = Math.Max((int)Math.Ceiling(distance / (Cell2.ConductionVelocity * dt)), 1);
                duration2 += delay;
            }
        }

        public override (int source, int target) GetCellIndices()
        {
            int source = Cell1.CellPool.BodyLocation == BodyLocation.SupraSpinal ? Cell1.Sequence : Cell1.Somite;
            int target = Cell2.CellPool.BodyLocation == BodyLocation.SupraSpinal ? Cell2.Sequence : Cell2.Somite;
            return (source, target);
        }
        public override void NextStep(int tIndex)
        {
            if (tIndex <= 0) return;
            if (!IsActive(tIndex))
            {
                VoltageDiffFrom1To2 = VoltageDiffFrom2To1 = 0;
                if (inputCurrent != null)
                    inputCurrent[tIndex] = 0;
                return;
            }
            double v1 = duration1 <= tIndex ? Cell1.V[tIndex - duration1] : Cell1.RestingMembranePotential;
            double v2 = Cell2.V[tIndex - 1];
            VoltageDiffFrom1To2 = v1 - v2;
            v2 = duration2 <= tIndex ? Cell2.V[tIndex - duration2] : Cell2.RestingMembranePotential;
            v1 = Cell1.V[tIndex - 1];
            VoltageDiffFrom2To1 = v2 - v1;
            double IGap = (Core as ElecSynapseCore).GetNextVal(VoltageDiffFrom1To2, VoltageDiffFrom2To1);
            if (inputCurrent != null)
                inputCurrent[tIndex] = IGap;
        }
    }
}
