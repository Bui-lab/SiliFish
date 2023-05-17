using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services.Plotting
{
    public struct PlotSelectionMultiCells : PlotSelectionInterface
    {
        public string Pools = Const.AllPools;
        public SagittalPlane SagittalPlane = SagittalPlane.Both;
        public PlotSelection somiteSelection = PlotSelection.All;
        public int nSomite = -1;
        public PlotSelection cellSelection = PlotSelection.All;
        public int nCell = -1;
        public bool groupSomites = false;
        public bool groupCells = false;
        public PlotSelectionMultiCells()
        {
        }

        public static IEnumerable<IGrouping<string, Cell>> GroupCells(List<Cell> cells,
bool groupByPool, bool groupSomites)
        {
            return cells.GroupBy(c => (groupByPool && groupSomites) ? $"{c.CellPool.ID}" : //One group for all somites and cells in a cell pool
                                    groupByPool ? $"{c.CellPool.ID}-Somite:{c.Somite}" : //One group for each somite in a cell pool
                                    groupSomites ? $"Somite: {c.Somite}" : //One group for each somite - including all pools
                                    c.ID); //Each cell is separate
        }
    }
}
