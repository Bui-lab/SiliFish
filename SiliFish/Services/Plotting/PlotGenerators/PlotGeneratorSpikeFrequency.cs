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
    internal class PlotGeneratorSpikeFrequency : PlotGeneratorOfCells
    {
        private KinemParam kinemParam;
        private double dt;

        public PlotGeneratorSpikeFrequency(PlotGenerator plotGenerator, List<Cell> cells, double[] timeArray,
            KinemParam kinemParam, double dt,
            bool combinePools, bool combineSomites, bool combineCells,
            int iStart, int iEnd) :
            base(plotGenerator, timeArray, iStart, iEnd, cells, combinePools, combineSomites, combineCells)
        {
            this.dt = dt;
            this.kinemParam = kinemParam;
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
                string columnTitles = "Time,";
                List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> colorPerChart = new();
                foreach (Cell cell in cellGroup)
                {
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                    {
                        data[i] += cell.V?[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                    }
                    DynamicsStats dynamics = new(kinemParam, cell.V, dt, cell.Core.Vthreshold);
                    Dictionary<double, (double Freq, double End)> SpikeFrequency = dynamics.SpikingFrequency
                        .Where(fr => fr.Value.End >= iStart * dt && fr.Key <= iEnd * dt).ToDictionary(fr => fr.Key, fr => (fr.Value.Freq, fr.Value.End));
                    Dictionary<double, (double Freq, double End)> BurstFrequency = dynamics.BurstingFrequency
                        .Where(fr => fr.Value.End >= iStart * dt && fr.Key <= iEnd * dt).ToDictionary(fr => fr.Key, fr => (fr.Value.Freq, fr.Value.End));
                    if (SpikeFrequency.Count > 0)
                    {
                        double[] xData = new double[SpikeFrequency.Count * 3];
                        double[] yData = new double[SpikeFrequency.Count * 3];
                        int i = 0;
                        foreach (var ff in SpikeFrequency)
                        {
                            xData[i] = ff.Key;
                            yData[i++] = ff.Value.Freq;
                            xData[i] = ff.Value.End;
                            yData[i++] = ff.Value.Freq;
                            xData[i] = ff.Value.End + dt;
                            yData[i++] = double.NaN;
                        }
                        Chart spikeFreqChart = new()
                        {
                            Title = $"{cell.ID} Spiking Freq.",
                            Color = Color.Blue.ToRGBQuoted(),
                            xData = xData,
                            xMin = timeArray[iStart],
                            xMax = timeArray[iEnd] + 1,
                            yMin = 0,
                            yData = yData,
                            yLabel = "Freq (Hz)",
                            drawPoints = true
                        };
                        if (!AddChart(spikeFreqChart))
                            return;
                    }
                    if (BurstFrequency.Count > 0)
                    {
                        double[] xData = new double[BurstFrequency.Count * 3];
                        double[] yData = new double[BurstFrequency.Count * 3];
                        int i = 0;
                        foreach (var ff in BurstFrequency)
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
                            Title = $"{cell.ID} Burst Freq.",
                            Color = Color.Blue.ToRGBQuoted(),
                            xData = xData,
                            xMin = timeArray[iStart],
                            xMax = timeArray[iEnd] + 1,
                            yMin = 0,
                            yData = yData,
                            yLabel = "Freq (Hz)",
                            drawPoints = true
                        };
                        if (!AddChart(burstFreqChart))
                            return;
                    }
                }
            }
        }
    }
}
