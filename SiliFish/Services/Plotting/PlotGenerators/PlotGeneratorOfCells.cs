using SiliFish.ModelUnits.Cells;
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

        protected PlotGeneratorOfCells(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, List<Cell> cells, bool combinePools, bool combineSomites, bool combineCells) :
            base(plotGenerator, timeArray, iStart, iEnd)
        {
            this.cells = cells;
            this.combinePools = combinePools;
            this.combineSomites = combineSomites;
            this.combineCells = combineCells;
        }
    }
}
