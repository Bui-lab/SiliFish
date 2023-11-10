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
    internal class PlotGeneratorTension : PlotGeneratorOfCells
    {
        public PlotGeneratorTension(PlotGenerator plotGenerator, List<Cell> cells, double[] timeArray, bool combinePools, bool combineSomites, bool combineCells, int iStart, int iEnd, int groupSeq) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, cells, combinePools, combineSomites, combineCells)
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
            double yMin = 0;
            double yMax = 1.2;
            Util.SetYRange(ref yMin, ref yMax);
            List<Cell> cellList = cells.Cast<Cell>().ToList();

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cellList, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string columnTitles = "Time,";
                List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> colorPerChart = new();
                foreach (Cell cell in cellGroup)
                {
                    MuscleCell muscleCell = cell as MuscleCell;
                    double[] Tension = muscleCell.RelativeTension;
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += Tension[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                }
                string csvData = "`" + columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
                Chart chart = new()
                {
                    CsvData = csvData,
                    Color = string.Join(',', colorPerChart),
                    Title = $"`{cellGroup.Key} Relative Tension`",
                    yLabel = "`Relative Tension`",
                    yMin = yMin,
                    yMax = yMax,
                    xMin = timeArray[iStart],
                    xMax = timeArray[iEnd] + 1
                };
                if (!AddChart(chart))
                    return;
            }
        }
    }

}
