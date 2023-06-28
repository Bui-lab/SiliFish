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
    public class PlotGeneratorMembranePotentials : PlotGeneratorOfCells
    {
        public PlotGeneratorMembranePotentials(List<Cell> cells, double[] timeArray, bool combinePools, bool combineSomites, bool combineCells, int iStart, int iEnd) :
            base(timeArray, iStart, iEnd, cells, combinePools, combineSomites, combineCells)
        {
        }
        protected override void CreateCharts()
        {
            if (cells == null || !cells.Any())
                return;

            double yMin = cells.Min(c => c.MinPotentialValue(iStart, iEnd));
            double yMax = cells.Max(c => c.MaxPotentialValue(iStart, iEnd));
            Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string columnTitles = "Time,";
                List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> colorPerChart = new();
                foreach (Cell cell in cellGroup)
                {
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += cell.V?[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                }
                if (!GlobalSettings.SameYAxis)
                {
                    yMin = cellGroup.Min(c => c.MinPotentialValue(iStart, iEnd));
                    yMax = cellGroup.Max(c => c.MaxPotentialValue(iStart, iEnd));
                    Util.SetYRange(ref yMin, ref yMax);
                }
                string csvData = "`" + columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
                Chart chartDataStruct = new Chart
                {
                    CsvData = csvData,
                    Color = string.Join(',', colorPerChart),
                    Title = $"`{cellGroup.Key} Membrane Potential`",
                    yLabel = "`Memb. Potential (mV)`",
                    yMin = yMin,
                    yMax = yMax,
                    xMin = timeArray[iStart],
                    xMax = timeArray[iEnd] + 1
                };

                if (!AddChart(chartDataStruct))
                    return;
            }
        }
    }

}
