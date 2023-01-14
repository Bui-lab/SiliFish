using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using System;

namespace SiliFish.ModelUnits
{
    public class CellReach
    {
        #region Member Properties

        public bool Ascending { get; set; } = true;
        public bool Descending { get; set; } = true;

        // Min asc reach checks the number of somites (if somite based) or the calculated distance (Euclidean or else)
        public double MinAscReach { get; set; } = 0;
        // Max asc reach checks the number of somites (if somite based) or the calculated distance (Euclidean or else)
        public double MaxAscReach { get; set; } = 100;

        // Min desc reach checks the number of somites (if somite based) or the calculated distance (Euclidean or else)
        public double MinDescReach { get; set; } = 0;
        // Max desc reach checks the number of somites (if somite based) or the calculated distance (Euclidean or else)
        public double MaxDescReach { get; set; } = 100;

        // MaxOutgoing shows how many junctions the source cell can have
        public int MaxOutgoing { get; set; } = 0;
        // MaxIncoming shows how many junctions the target cell can have
        public int MaxIncoming { get; set; } = 0;

        public DistanceMode DistanceMode { get; set; } = DistanceMode.Euclidean;
        public double? FixedDuration_ms { get; set; } = null;// in ms
        public double Delay_ms { get; set; } = 0;//in ms
        public double Weight { get; set; }

        public bool Autapse { get; set; } = false;
        public bool SomiteBased { get; set; } = false;

        #endregion
        public CellReach() { }
        public CellReach(CellReach cr)
        {
            MinAscReach = cr.MinAscReach;
            MaxAscReach = cr.MaxAscReach;
            MinDescReach = cr.MinDescReach;
            MaxDescReach = cr.MaxDescReach;
            MaxIncoming = cr.MaxIncoming;
            MaxOutgoing = cr.MaxOutgoing;
            FixedDuration_ms = cr.FixedDuration_ms;
            Delay_ms = cr.Delay_ms;
            Weight = cr.Weight;
            Autapse = cr.Autapse;
            SomiteBased = cr.SomiteBased;
        }
        internal object GetTooltip()
        {
            string reach =
                (Ascending ? $"Ascending: {MinAscReach:0.###} -  {MaxAscReach:0.###} \r\n" : "") +
                (Descending ? $"Descending: {MinDescReach:0.###} -  {MaxDescReach:0.###} \r\n" : "");
            return reach +
                (MaxIncoming > 0 ? $"Max in: {MaxIncoming:0}\r\n" : "") +
                (MaxOutgoing > 0 ? $"Max out: {MaxOutgoing:0}\r\n" : "") +
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
            if (!Autapse && cell1 == cell2)
                return false;

            double diff_x = SomiteBased ? cell2.Somite - cell1.Somite :
                (cell2.X - cell1.X) * noise;//positive values mean cell2 is more caudal
            if (Descending && diff_x >= 0 && diff_x >= MinDescReach && diff_x <= MaxDescReach)
                return true;
            if (Ascending && diff_x <= 0 && -diff_x >= MinAscReach && -diff_x <= MaxAscReach)
                return true;
            return false;

        }

    }
 
}
