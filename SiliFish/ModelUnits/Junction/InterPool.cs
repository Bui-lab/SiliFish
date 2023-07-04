using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Junction
{
    /// <summary>
    /// Used to list connections between cell pools
    /// </summary>
    public struct InterPool
    {
        public string SourcePool, TargetPool;
        public int CountJunctions;
        public double MinConductance;
        public double MaxConductance;
        public CellInputMode Mode;

        public override string ToString()
        {
            string arrow = Mode == CellInputMode.Electrical ? "↔" : "→";
            return $"{SourcePool}{arrow}{TargetPool}";
        }

    }
}
