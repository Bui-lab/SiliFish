using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services.Plotting.PlotSelection;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorFullDynamics(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd,
     List<Cell> cells, PlotSelectionInterface plotSelection) : PlotGeneratorOfCells(plotGenerator, timeArray, iStart, iEnd, groupSeq: 0, cells, plotSelection)
    {
        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }
        protected override void CreateCharts()
        {
            if (cells == null || cells.Count == 0)
                return;
            UnitOfMeasure UoM = plotGenerator.UoM;

            PlotGeneratorMembranePotentials plotGeneratorMP = new(plotGenerator, timeArray, iStart, iEnd, 0,
                cells, plotSelection);
            plotGeneratorMP.CreateCharts(charts);

            PlotGeneratorCurrentsOfCells plotGeneratorCurrent = new(plotGenerator, timeArray, iStart, iEnd, 1,
                 cells, plotSelection,
                 includeGap: GlobalSettings.FullDynamics_ShowGapCurrent, 
                 includeChemIn: GlobalSettings.FullDynamics_ShowChemInCurrent,
                 includeChemOut: GlobalSettings.FullDynamics_ShowChemOutCurrent);
            plotGeneratorCurrent.CreateCharts(charts);

            if (GlobalSettings.FullDynamics_ShowStimulus)
            {
                PlotGeneratorStimuli plotGeneratorStimuli = new(plotGenerator, timeArray, iStart, iEnd, 4,
                    cells, plotSelection);
                plotGeneratorStimuli.CreateCharts(charts);
            }

            List<Cell> muscleCells = cells?.Where(c => c is MuscleCell).ToList();
            if (muscleCells != null && muscleCells.Count != 0)
            {
                PlotGeneratorTension plotGeneratorTension = new(plotGenerator, timeArray, iStart, iEnd, 5,
                    cells,
                    plotSelection);
                plotGeneratorTension.CreateCharts(charts);
            }
        }
    }
}
