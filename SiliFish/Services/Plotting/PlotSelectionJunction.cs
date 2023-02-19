using SiliFish.Definitions;
using SiliFish.ModelUnits.Junction;

namespace SiliFish.Services.Plotting
{
    public struct PlotSelectionJunction : PlotSelectionInterface
    {
        public JunctionBase Junction;
        public PlotSelectionJunction()
        {
        }
    }
}
