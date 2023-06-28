using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services.Plotting.PlotSelection
{
    public class PlotSelectionMultiCells : PlotSelectionInterface
    {
        public SagittalPlane SagittalPlane { get; set; } = SagittalPlane.Both;
        public PlotSomiteSelection SomiteSelection { get; set; } = PlotSomiteSelection.All;
        public int NSomite { get; set; } = -1;
        public PlotCellSelection CellSelection { get; set; } = PlotCellSelection.All;
        public int NCell { get; set; } = -1;
        public bool CombinePools { get; set; } = false;
        public bool CombineSomites { get; set; } = false;
        public bool CombineCells { get; set; } = false;
        public PlotSelectionMultiCells()
        {
        }

        public PlotSelectionMultiCells(PlotSelectionInterface copyFrom)
        {
            if (copyFrom is PlotSelectionMultiCells plot)
            {
                SagittalPlane = plot.SagittalPlane;
                SomiteSelection = plot.SomiteSelection;
                NSomite = plot.NSomite;
                CellSelection = plot.CellSelection;
                NCell = plot.NCell;
                CombinePools = plot.CombinePools;
                CombineSomites = plot.CombineSomites;
                CombineCells = plot.CombineCells;
            }
        }
        public override string ToString()
        {
            string sagittal = SagittalPlane == SagittalPlane.Left ? "[L]" : SagittalPlane == SagittalPlane.Right ? "[R]" : "";
            string somite = SomiteSelection == PlotSomiteSelection.All ? "" :
                SomiteSelection == PlotSomiteSelection.Single ? $"Somite:{NSomite}" :
                $"Somite: {SomiteSelection}";
            if (CombineSomites && !string.IsNullOrEmpty(somite)) somite = "[" + somite + "]";
            string cells = CellSelection == PlotCellSelection.All ? "" :
                CellSelection == PlotCellSelection.Single ? $"Cell:{NCell}" :
                $"Cells: {CellSelection}";
            if (CombineCells && !string.IsNullOrEmpty(cells)) cells = "[" + cells + "]";
            return CombinePools ? $"[{sagittal}{somite}{cells}]".Replace("  ", " ") :
                $"{sagittal} {somite} {cells}".Replace("  ", " ");
        }
        public static IEnumerable<IGrouping<string, Cell>> GroupCells(List<Cell> cells,
            bool combinePools, bool combineSomites, bool combineCells)
        {
            if (combineSomites || combinePools)
                combineCells = true;
            return cells.GroupBy(c =>
                !combinePools && !combineSomites && !combineCells ? $"{c.ID}" ://Each cell seperate
                !combinePools && !combineSomites && combineCells ? $"{c.CellPool.ID}-Somite:{c.Somite}" : //One group for each somite in a cell pool
                combinePools && !combineSomites ? $"Somite:{c.Somite} - {c.CellPool.PositionLeftRight}" ://One group for each somite
                !combinePools && combineSomites ? $"{c.CellPool.ID}" ://One group for each pool
                combinePools && combineSomites ? $"{c.CellPool.PositionLeftRight}" :
                                    c.ID); //Each cell is separate
        }
    }
}
