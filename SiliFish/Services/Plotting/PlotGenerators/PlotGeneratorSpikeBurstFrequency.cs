using OfficeOpenXml.Drawing.Slicer.Style;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits.Firing;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorSpikeBurstFrequency : PlotGeneratorOfCells
    {
        private readonly DynamicsParam dynamicsParam;
        private readonly double dt;
        private readonly bool burst = false;
        private readonly string title = "Spike";

        public PlotGeneratorSpikeBurstFrequency(PlotGenerator plotGenerator, List<Cell> cells, double[] timeArray,
            DynamicsParam dynamicsParam, double dt,
            PlotSelectionInterface plotSelection,
            int iStart, int iEnd, int groupSeq, bool burst) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, cells, plotSelection)
        {
            this.dt = dt;
            this.dynamicsParam = dynamicsParam;
            this.burst = burst;
            title = burst ? "Burst" : "Spike";
        }

        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }
        protected override void CreateCharts()
        {
            if (cells == null || cells.Count == 0)
                return;
            int chartSeq = 0;

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string columnTitles = "Time,";
                List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<Color> colorPerChart = [];
                if (cellGroup.Count() > 1)
                {
                    List<double[]> yMultiData = [];
                    double[] yData = null;
                    double yMin = double.MaxValue;
                    double yMax = double.MinValue;
                    bool chartExists = false;
                    foreach (Cell cell in cellGroup)
                    {
                        DynamicsStats dynamics = new(dynamicsParam, cell.V, dt, cell.Core.Vthreshold, iStart, iEnd);
                        Dictionary<double, (double Freq, double End)> SpikeBurstFrequency =
                            (burst ? dynamics.BurstingFrequency_Grouped : dynamics.SpikeFrequency_Grouped)
                            .Where(fr => fr.Value.End >= iStart * dt && fr.Key <= iEnd * dt).ToDictionary(fr => fr.Key, fr => (fr.Value.Freq, fr.Value.End));
                        if (SpikeBurstFrequency.Count == 0)
                            continue;
                        chartExists = true;
                        columnTitles += cell.ID + ",";
                        colorPerChart.Add(cell.CellPool.Color);
                        yMin = Math.Min(yMin, SpikeBurstFrequency.Min(sf => sf.Value.Freq));
                        yMax = Math.Max(yMax, SpikeBurstFrequency.Max(sf => sf.Value.Freq));
                        yData = new double[iEnd];
                        foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        {
                            yData[i] = double.NaN;
                            data[i] += "NaN,";
                        }
                        foreach (var ff in SpikeBurstFrequency)
                        {
                            for (int i = Math.Max((int)(ff.Key / dt), iStart); i < Math.Min((int)(ff.Value.End / dt), iEnd); i++)
                            {
                                yData[i-iStart] = ff.Value.Freq;
                                data[i - iStart] = string.Concat(data[i - iStart].AsSpan(0, data[i - iStart].Length - 4), ff.Value.Freq.ToString(GlobalSettings.PlotDataFormat), ",");
                            }
                        }
                        yMultiData.Add(yData);
                    }
                    if (yMultiData.Count == 1)
                    {
                        yData = yMultiData.FirstOrDefault();
                        yMultiData = null;
                    }
                    if (chartExists)
                    {
                        string csvData = columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray());
                        Util.SetYRange(ref yMin, ref yMax);
                        Chart chartDataStruct = new()
                        {
                            CsvData = csvData,
                            Colors = colorPerChart,
                            Title = $"{cellGroup.Key} {title} Frequency",
                            yLabel = "Burst Freq. (Hz)",
                            yMin = yMin,
                            yMax = yMax,
                            xMin = timeArray[iStart],
                            xMax = timeArray[iEnd] + 1,
                            xData = timeArray[iStart..iEnd],
                            yData = yData,
                            yMultiData = yMultiData
                        };
                        if (!AddChart(chartDataStruct, chartSeq++))
                            return;
                    }
                    else
                        chartSeq++;
                }
                else //more efficient way of plotting without creating a dot for every single time point
                {
                    Cell cell = cellGroup.First();
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color);
                    DynamicsStats dynamics = new(dynamicsParam, cell.V, dt, cell.Core.Vthreshold, iStart, iEnd);
                    Dictionary<double, (double Freq, double End)> SpikeBurstFrequency =
                        (burst ? dynamics.BurstingFrequency_Grouped : dynamics.SpikeFrequency_Grouped)
                        .Where(fr => fr.Value.End >= iStart * dt && fr.Key <= iEnd * dt).ToDictionary(fr => fr.Key, fr => (fr.Value.Freq, fr.Value.End));
                    if (SpikeBurstFrequency.Count > 0)
                    {
                        double[] xData = new double[SpikeBurstFrequency.Count * 3];
                        double[] yData = new double[SpikeBurstFrequency.Count * 3];
                        int i = 0;
                        foreach (var ff in SpikeBurstFrequency)
                        {
                            xData[i] = ff.Key;
                            yData[i++] = ff.Value.Freq;
                            xData[i] = ff.Value.End;
                            yData[i++] = ff.Value.Freq;
                            xData[i] = ff.Value.End + dt;
                            yData[i++] = double.NaN;
                        }
                        Chart burstFreqChart = new()
                        {
                            Title = $"{cell.ID} {title} Freq.",
                            Colors = [cell.CellPool.Color],
                            xData = xData,
                            xMin = timeArray[iStart],
                            xMax = timeArray[iEnd] + 1,
                            yMin = 0,
                            yData = yData,
                            yMultiData = null,
                            yLabel = "Freq (Hz)",
                            drawPoints = true
                        };
                        if (!AddChart(burstFreqChart, chartSeq++))
                            return;
                    }
                    else
                        chartSeq++;
                }
            }
        }
    }
}