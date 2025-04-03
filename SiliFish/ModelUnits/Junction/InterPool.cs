using SiliFish.Definitions;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.ModelUnits.Junction
{
    /// <summary>
    /// Used to list junctions between cell pools
    /// </summary>
    public class InterPool: ModelUnitBase
    {
        public IEnumerable<JunctionBase> Junctions { get; private set; }

        public string SourcePool, TargetPool;
        public int CountJunctions { get{ return Junctions.Count(); }}
        public double MinConductance { get {  return Junctions.Min(j=>j.Core?.Conductance??0); } }
        public double MaxConductance { get { return Junctions.Max(j => j.Core?.Conductance ?? 0); } }

        public override bool Active
        {
            get
            {
                return Junctions.All(j => j.Active);
            }
            set
            {
                foreach (JunctionBase junction in Junctions)
                    junction.Active = value;
            }
        }
        public CellOutputMode Mode;
        public override string ID => ToString();
        public override string ToString()
        {
            string arrow = Mode == CellOutputMode.Electrical ? "↔" : "→";
            string ntmode = Mode == CellOutputMode.Cholinergic ? "C" :
                Mode == CellOutputMode.Excitatory ? "+" :
                Mode == CellOutputMode.Inhibitory ? "-" :
                Mode == CellOutputMode.Modulatory ? "M" :
                Mode == CellOutputMode.Electrical ? "🗲" :
                "?";
            string active = Active ? "" : " (inactive/partial)";
            return $"({ntmode}) {SourcePool}{arrow}{TargetPool}{active}";
        }

        public InterPool(IEnumerable<JunctionBase> junctions)
        {
            Junctions = junctions;
            PlotType = "InterPool";
        }

    }
}
