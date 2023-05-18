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
        public bool combinePools = false;
        public bool combineSomites = false;
        public bool combineCells = false;
        public PlotSelectionMultiCells()
        {
        }

        public static IEnumerable<IGrouping<string, Cell>> GroupCells(List<Cell> cells, 
            bool combinePools, bool combineSomites, bool combineCells)
        {
            if (combineSomites || combinePools)
                combineCells = true;
            return cells.GroupBy(c =>
                !combinePools && !combineSomites && !combineCells ? $"{c.ID}" ://Each cell seperate
                !combinePools && !combineSomites && combineCells ? $"{ c.CellPool.ID}-Somite:{ c.Somite}" : //One group for each somite in a cell pool
                combinePools && !combineSomites ? $"Somite:{c.Somite}" ://One group for each somite
                !combinePools && combineSomites ? $"{c.CellPool.ID}" ://One group for each pool
                (combinePools && combineSomites) ? $"All" :
                                    c.ID); //Each cell is separate
        }
    }
}
