using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services.Plotting.PlotSelection;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            if (cells == null || cells.Count == 0)
                return;

            double yMin = cells.Min(c => c.MinPotentialValue(iStart, iEnd));
            double yMax = cells.Max(c => c.MaxPotentialValue(iStart, iEnd));
            Util.SetYRange(ref yMin, ref yMax);
            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                List<double[]> yMultiData = [];
                double[] yData = null;
                string columnTitles = "Time,";
                List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<Color> colorPerChart = [];
                foreach (Cell cell in cellGroup)
                {
                    if (GlobalSettings.MembranePotential_ShowSpike)
                        columnTitles += $"{cell.ID},{cell.ID}-Spikes,";
                    else
                        columnTitles += $"{cell.ID},";
                    colorPerChart.Add(cell.CellPool.Color);
                    yMultiData.Add(cell.V.ToArray()[iStart..iEnd]);
                    List<int> spikeTrains = cell.SpikeTrain?.Where(s => s >= iStart && s <= iEnd).ToList();
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                    {
                        string value = cell.V?[iStart + i].ToString(GlobalSettings.PlotDataFormat) ?? "";
                        data[i] += value + ",";
                        if (GlobalSettings.MembranePotential_ShowSpike)
                        {
                            if ((bool)(spikeTrains?.Contains(iStart + i)))
                                data[i] += value + ",";
                            else
                                data[i] += "null,";
                        }
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
                    yLabelLong = "Membrane Potential (mV)",
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
