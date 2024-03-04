using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorTension : PlotGeneratorOfCells
    {
        public PlotGeneratorTension(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
            List<Cell> cells, PlotSelectionInterface plotSelection) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, cells, plotSelection)
        {
        }
        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }
        protected override void CreateCharts()
        {
            if (cells == null || cells.Count == 0)
                return;
            double yMin = 0;
            double yMax = 1.2;
            Util.SetYRange(ref yMin, ref yMax);
            List<Cell> cellList = cells.Cast<Cell>().ToList();

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cellList, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                List<double[]> yMultiData = [];
                double[] yData = null;
                string columnTitles = "Time,";
                List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<Color> colorPerChart = [];
                foreach (Cell cell in cellGroup)
                {
                    if (cell is not MuscleCell muscleCell) continue;
                    double[] Tension = muscleCell.RelativeTension;
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color);
                    yMultiData.Add(Tension[iStart..iEnd]);
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += Tension[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                }
                if (yMultiData.Count == 1)
                {
                    yData = yMultiData.FirstOrDefault();
                    yMultiData = null;
                }
                string csvData = columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray());
                Chart chart = new()
                {
                    CsvData = csvData,
                    Colors = colorPerChart,
                    Title = $"{cellGroup.Key} Relative Tension",
                    yLabel = "Relative Tension",
                    yMin = yMin,
                    yMax = yMax,
                    xMin = timeArray[iStart],
                    xMax = timeArray[iEnd] + 1,
                    xData = timeArray[iStart..iEnd],
                    yData = yData,
                    yMultiData = yMultiData
                };
                if (!AddChart(chart))
                    return;
            }
        }
    }

}
