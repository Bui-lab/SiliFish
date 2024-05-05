using SiliFish.Definitions;
using SiliFish.Services.Plotting.PlotSelection;

namespace SiliFish.DataTypes
{
    public class PlotDefinition
    {
        public string PlotSubset { get; set; }
        public PlotType PlotType { get; set; }
        public PlotSelectionInterface Selection { get; set; }
        public PlotDefinition()
        {
        }

        public override string ToString()
        {
            return $"{PlotType}-{PlotSubset}:{Selection}";
        }

 
    }
}
