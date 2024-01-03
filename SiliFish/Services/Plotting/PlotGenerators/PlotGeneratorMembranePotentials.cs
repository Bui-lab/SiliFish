using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits.Firing;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorMembranePotentials : PlotGeneratorOfCells
    {
        public PlotGeneratorMembranePotentials(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
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
            if (cells == null || !cells.Any())
                return;

            double yMin = cells.Min(c => c.MinPotentialValue(iStart, iEnd));
            double yMax = cells.Max(c => c.MaxPotentialValue(iStart, iEnd));
            Util.SetYRange(ref yMin, ref yMax);
            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                List<double[]> yMultiData = new();
                double[] yData = null;
                string columnTitles = "Time,";
                List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<Color> colorPerChart = new();
                foreach (Cell cell in cellGroup)
                {
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color);
                    yMultiData.Add(cell.V[iStart..iEnd]);
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                    {
                        data[i] += cell.V?[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";                        
                    }
                }
                if (!GlobalSettings.SameYAxis)
                {
                    yMin = cellGroup.Min(c => c.MinPotentialValue(iStart, iEnd));
                    yMax = cellGroup.Max(c => c.MaxPotentialValue(iStart, iEnd));
                    Util.SetYRange(ref yMin, ref yMax);
                }
                if (yMultiData.Count == 1)
                {
                    yData = yMultiData.FirstOrDefault();
                    yMultiData = null;
                }
                string csvData = columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray());
                Chart chartDataStruct = new()
                {
                    CsvData = csvData,
                    Colors = colorPerChart,
                    Title = $"{cellGroup.Key} Membrane Potential",
                    yLabel = "Memb. Pot. (mV)",
                    yMin = yMin,
                    yMax = yMax,
                    xMin = timeArray[iStart],
                    xMax = timeArray[iEnd] + 1,
                    xData = timeArray[iStart..iEnd],
                    yData = yData,
                    yMultiData = yMultiData
                };
                if (!AddChart(chartDataStruct))
                    return;
            }
        }
    }

}
