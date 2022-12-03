using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using System;

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
            get
            {
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
                $"Weight: {Weight: 0.###}\r\n" +
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

            double dist = Util.Distance(cell1.coordinate, cell2.coordinate, DistanceMode) * noise;
            return dist >= MinReach && dist <= MaxReach;
        }

    }
 
}
