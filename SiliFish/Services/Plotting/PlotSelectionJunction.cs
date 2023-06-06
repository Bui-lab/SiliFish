using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;

namespace SiliFish.Services.Plotting
{
    public class PlotSelectionJunction : PlotSelectionInterface
    {
        public JunctionBase Junction { get; set; }
        public PlotSelectionJunction()
        {
        }
        public override string ToString()
        {
            return Junction.ToString();
        }
    }
}
