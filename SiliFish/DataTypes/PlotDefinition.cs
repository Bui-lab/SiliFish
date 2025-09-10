using SiliFish.Definitions;
using SiliFish.Services.Plotting.PlotSelection;

namespace SiliFish.DataTypes
{
    public class PlotDefinition
    {
        public string PlotSubset { get; set; }
        public PlotType PlotType { get; set; }
        public PlotSelectionInterface Selection { get; set; }
        public int PlotTimeStart { get; set; } = 0;
        public int PlotTimeEnd { get; set; } = 0;

        public PlotDefinition()
        {
        }

        public override string ToString()
        {
            string timeRange = PlotTimeStart == 0 && PlotTimeEnd == 0 ? "" : $"[{PlotTimeStart}-{PlotTimeEnd}]";
            return $"{PlotType}-{PlotSubset}:{Selection} {timeRange}";
        }

 
    }
}
