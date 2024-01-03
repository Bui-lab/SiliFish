using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorFullDynamics : PlotGeneratorOfCells
    {
           public PlotGeneratorFullDynamics(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, 
            List<Cell> cells, PlotSelectionInterface plotSelection) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq: 0, cells, plotSelection)
        {
        }
        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }
        protected override void CreateCharts()
        {
            if (cells == null || !cells.Any())
                return;
            UnitOfMeasure UoM = plotGenerator.UoM;

            PlotGeneratorMembranePotentials plotGeneratorMP = new(plotGenerator, timeArray, iStart, iEnd, 0,
                cells, plotSelection);
            plotGeneratorMP.CreateCharts(charts);

            PlotGeneratorCurrentsOfCells plotGeneratorCurrent = new(plotGenerator, timeArray, iStart, iEnd, 1,
                 cells, plotSelection);
            plotGeneratorCurrent.CreateCharts(charts);

            PlotGeneratorStimuli plotGeneratorStimuli = new(plotGenerator, timeArray, iStart, iEnd, 4,
                cells, plotSelection);
            plotGeneratorStimuli.CreateCharts(charts);

            List<Cell> muscleCells = cells?.Where(c => c is MuscleCell).ToList();
            if (muscleCells != null && muscleCells.Any())
            {
                PlotGeneratorTension plotGeneratorTension = new(plotGenerator, timeArray, iStart, iEnd, 5,
                    cells,
                    plotSelection);
                plotGeneratorTension.CreateCharts(charts);
            }
        }
    }
}
