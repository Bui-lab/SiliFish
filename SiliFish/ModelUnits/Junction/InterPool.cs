using SiliFish.Definitions;

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
            string ntmode = Mode == CellOutputMode.Cholinergic ? "C" :
                Mode == CellOutputMode.Excitatory ? "+" :
                Mode == CellOutputMode.Inhibitory ? "-" :
                Mode == CellOutputMode.Modulatory ? "M" :
                Mode == CellOutputMode.Electrical ? "🗲" :
                "?";
            string active = Active ? "" : " (inactive)";
            return $"({ntmode}) {SourcePool}{arrow}{TargetPool}{active}";
        }

        public InterPool()
        {
            PlotType = "InterPool";
        }

    }
}
