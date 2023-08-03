using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Junction
{
    /// <summary>
    /// Used to list connections between cell pools
    /// </summary>
    public class InterPool: ModelUnitBase
    {
        public string SourcePool, TargetPool;
        public int CountJunctions;
        public double MinConductance;
        public double MaxConductance;
        public CellOutputMode Mode;

        public override string ID => ToString();
        public override string ToString()
        {
            string arrow = Mode == CellOutputMode.Electrical ? "↔" : "→";
            return $"{SourcePool}{arrow}{TargetPool}";
        }

    }
}
