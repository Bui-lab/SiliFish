using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

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

        public bool Autapse { get; set; } = false;
        public bool SomiteBased { get; set; } = true;

        public string Projection => Ascending && Descending ? "Bifurcating" : Ascending ? "Ascending" : Descending ? "Descending" : "N/A";

        #endregion
        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames { get; } = ["Asc.", " Min Asc. Reach", " Max Asc. Reach",
            " Desc.", " Min Desc. Reach", " Max Desc. Reach",
            "Max Out", " Max In", " Autapse", " Somite Based"];

        public List<string> ExportValues()
        {
            return ListBuilder.Build<string>(Ascending, MinAscReach, MaxAscReach,
                Descending, MinDescReach, MaxDescReach,
                MaxOutgoing, MaxIncoming, Autapse, SomiteBased);
        }
        public void Importvalues(List<string> values)
            {
                if (values.Count != ColumnNames.Count) return;
                try
                {
                    int iter = 0;
                    Ascending = bool.Parse(values[iter++]);
                    MinAscReach = double.Parse(values[iter++]);
                    MaxAscReach = double.Parse(values[iter++]);
                    Descending = bool.Parse(values[iter++]);
                    MinDescReach = double.Parse(values[iter++]);
                    MaxDescReach = double.Parse(values[iter++]);
                    MaxOutgoing = int.Parse(values[iter++]);
                    MaxIncoming = int.Parse(values[iter++]);
                    Autapse = bool.Parse(values[iter++]);
                    SomiteBased = bool.Parse(values[iter++]);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw;
                }
        }
        public CellReach() { }
        public CellReach(CellReach cr)
        {
            MinAscReach = cr.MinAscReach;
            MaxAscReach = cr.MaxAscReach;
            MinDescReach = cr.MinDescReach;
            MaxDescReach = cr.MaxDescReach;
            MaxIncoming = cr.MaxIncoming;
            MaxOutgoing = cr.MaxOutgoing;
            Ascending = cr.Ascending;
            Descending = cr.Descending;
            Autapse = cr.Autapse;
            SomiteBased = cr.SomiteBased;
        }

        public override string ToString()
        {
            string reach =
                (Ascending ? $"Ascending: {MinAscReach:0.###} -  {MaxAscReach:0.###}; " : "") +
                (Descending ? $"Descending: {MinDescReach:0.###} -  {MaxDescReach:0.###}; " : "");
            return reach +
                (MaxIncoming > 0 ? $"Max in: {MaxIncoming:0}; " : "") +
                (MaxOutgoing > 0 ? $"Max out: {MaxOutgoing:0}; " : "");
        }
        internal object GetTooltip()
        {
            return ToString();
        }

        /// <summary>
        /// Checks whether cell1 can reach to cell2 with the current reach settings
        /// </summary>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        public bool WithinReach(Cell cell1, Cell cell2)
        {
            if (!Autapse && cell1 == cell2)
                return false;

            double diff_x = SomiteBased ? cell2.Somite - cell1.Somite :
                cell2.X - cell1.X;//positive values mean cell2 is more caudal
            if (Descending && diff_x >= 0 && 
                diff_x <= cell1.DescendingAxonLength && 
                diff_x >= MinDescReach && diff_x <= MaxDescReach)
                return true;
            if (Ascending && diff_x <= 0 && 
                -diff_x <= cell1.AscendingAxonLength &&
                -diff_x >= MinAscReach && -diff_x <= MaxAscReach)
                return true;
            return false;

        }

    }
 
}
