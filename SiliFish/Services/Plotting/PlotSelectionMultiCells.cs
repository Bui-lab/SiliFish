using SiliFish.Definitions;

namespace SiliFish.Services.Plotting
{
    public struct PlotSelectionMultiCells : PlotSelectionInterface
    {
        public string Pools = "All";
        public SagittalPlane SagittalPlane = SagittalPlane.Both;
        public PlotSelection somiteSelection = PlotSelection.All;
        public int nSomite = -1;
        public PlotSelection cellSelection = PlotSelection.All;
        public int nCell = -1;
        public PlotSelectionMultiCells()
        {
        }
    }
}
