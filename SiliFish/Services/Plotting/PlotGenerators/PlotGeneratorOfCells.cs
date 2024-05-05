using SiliFish.ModelUnits.Cells;
using SiliFish.Services.Plotting.PlotSelection;
using System.Collections.Generic;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal abstract class PlotGeneratorOfCells : PlotGeneratorBase
    {
        protected readonly List<Cell> cells;
        protected readonly bool combinePools;
        protected readonly bool combineSomites;
        protected readonly bool combineCells;
        protected readonly bool combineJunctions;

        protected PlotGeneratorOfCells(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
            List<Cell> cells, PlotSelectionInterface cellSelection) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, cellSelection)
        {
            this.cells = cells;
            if (cellSelection is PlotSelectionMultiCells cs)
            {
                combinePools = cs.CombinePools;
                combineSomites = cs.CombineSomites;
                combineCells = cs.CombineCells;
                combineJunctions = cs.CombineJunctions;
            }
        }
    }
}
