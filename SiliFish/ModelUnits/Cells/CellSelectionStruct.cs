using SiliFish.Definitions;

namespace SiliFish.ModelUnits.Cells
{
    public struct CellSelectionStruct
    {
        public string Pools = "All";
        public SagittalPlane SagittalPlane = SagittalPlane.Both;
        public PlotSelection somiteSelection = PlotSelection.All;
        public int nSomite = -1;
        public PlotSelection cellSelection = PlotSelection.All;
        public int nCell = -1;
        public CellSelectionStruct()
        {
        }
    }
}
