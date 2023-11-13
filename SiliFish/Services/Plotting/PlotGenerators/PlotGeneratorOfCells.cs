using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal abstract class PlotGeneratorOfCells : PlotGeneratorBase
    {
        protected readonly List<Cell> cells;
        protected readonly bool combinePools;
        protected readonly bool combineSomites;
        protected readonly bool combineCells;

        protected PlotGeneratorOfCells(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
            List<Cell> cells, PlotSelectionInterface cellSelection) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq)
        {
            this.cells = cells;
            if (cellSelection is PlotSelectionMultiCells cs)
            {
                combinePools = cs.CombinePools;
                combineSomites = cs.CombineSomites;
                combineCells = cs.CombineCells;
            }
        }
    }
}
