﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;

namespace SiliFish.Services
{
    internal class LineChartGenerator: VisualsGenerator
    {
        private static string CreateDataPoint(double x, double y)
        {
            return $"{{x:{x:0.##},y:{y:0.###} }}";
        }
        public static string CreateDataPoints(double[] Time, double[] data, int iStart, int iEnd)
        {
            string dataPoints = string.Join(",", Enumerable.Range(iStart, iEnd - iStart + 1).Select(i => CreateDataPoint(Time[i], data[i])));
            return dataPoints;
        }
        private static void SetPlotDimensions(StringBuilder html, int numColumns, int numChart)
        {
            int numRows = (int)Math.Ceiling((double)numChart / numColumns);
            int height = numRows < 4 ? numRows * 500 : numRows * 300;
            html.Replace("__HEIGHT_PX__", height.ToString());
            html.Replace("__PERCENT_WIDTH__", (100 / numColumns).ToString());
            html.Replace("__PERCENT_HEIGHT__", (100 / numRows).ToString());
            html.Replace("__COLUMNS__", numColumns.ToString());
        }

        public static string CreateLineChartSeries(string dataPoints, int chartIndex, string seriesName, string seriesIndex, string color, byte opacity)
        {
            StringBuilder series = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.Series.js"));
            dataPoints = dataPoints.Replace("__CHART_INDEX__", chartIndex.ToString());
            series.Replace("__CHART_INDEX__", chartIndex.ToString());
            series.Replace("__SERIES_NAME__", seriesName);
            series.Replace("__SERIES_INDEX__", seriesIndex);
            series.Replace("__SERIES_COLOR__", color);
            series.Replace("__SERIES_OPACITY__", opacity.ToString());
            series.Replace("__SERIES_DATA__", "[" + dataPoints + "]");
            return series.ToString();
        }
        public static string CreateScatterPlotSeries(string dataPoints, int chartIndex, string seriesName, string seriesIndex, string color, byte opacity)
        {
            StringBuilder series = new(ReadEmbeddedResource("SiliFish.Resources.ScatterPlot.Series.js"));
            dataPoints = dataPoints.Replace("__CHART_INDEX__", chartIndex.ToString());
            series.Replace("__CHART_INDEX__", chartIndex.ToString());
            series.Replace("__SERIES_NAME__", seriesName);
            series.Replace("__SERIES_INDEX__", seriesIndex);
            series.Replace("__SERIES_COLOR__", color);
            series.Replace("__SERIES_OPACITY__", opacity.ToString());
            series.Replace("__SERIES_DATA__", "[" + dataPoints + "]");
            return series.ToString();
        }

        public static string CreateSubPlot(int chartindex, string chartTitle, string series, double[] Time, int tstart, int tend, 
            double yMin, double yMax, bool absoluteYRange)
        {
            if (!absoluteYRange)
                Util.SetYRange(ref yMin, ref yMax);

            StringBuilder chart = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.Chart.js"));
            chart.Replace("__CHART_INDEX__", chartindex.ToString());
            chart.Replace("__X_START__", Time[tstart].ToString("0.##"));
            chart.Replace("__X_END__", Time[tend].ToString("0.##"));
            chart.Replace("__Y_START__", yMin.ToString("0.##"));
            chart.Replace("__Y_END__", yMax.ToString("0.##"));
            series = series.Replace("__CHART_INDEX__", chartindex.ToString());
            chart.Replace("__SERIES__", series);
            chart.Replace("__CHART_TITLE__", chartTitle);
            return chart.ToString();
        }

        public static string CreateLinePlot(double[] xValues, double[] yValues, int chartIndex, string seriesName, string chartName, string color, 
            bool absoluteYRange = false)
        {
            double yMin = yValues.Min();
            double yMax = yValues.Max();

            string dataPoints = CreateDataPoints(xValues, yValues, 0, xValues.Length - 1);
            string series = CreateLineChartSeries(dataPoints, chartIndex, seriesName, chartIndex.ToString(), color, 0);
            string chart = CreateSubPlot(chartIndex, chartName, series, xValues, 0, xValues.Length - 1, yMin, yMax, absoluteYRange);
            return chart;
        }

        public static string CreateScatterPlot(double[] Time, double[] xValues, double[] yValues, int chartIndex, string seriesName, string chartName, string color,
            bool absoluteYRange = false)
        {
            double yMin = yValues.Min();
            double yMax = yValues.Max();

            string dataPoints = CreateDataPoints(xValues, yValues, 0, xValues.Length - 1);
            string series = CreateScatterPlotSeries(dataPoints, chartIndex, seriesName, chartIndex.ToString(), color, 0);
            string chart = CreateSubPlot(chartIndex, chartName, series, Time, 0, Time.Length - 1, yMin, yMax, absoluteYRange);
            return chart;
        }

        #region Plot Stimuli
        private static string CreateStimulusDataPoint(Cell cell, double t, int timeInd)
        {
            return $"{{x:{t:0.##},y:{cell.Stimulus?[timeInd] ?? 0:0.###} }}";
        }

        private static string CreateStimuliSeries(int chartindex, Cell cell, double[] Time, int tstart, int tend, string color, ref byte opacity, byte dec)
        {
            string dataPoints = string.Join(",", Enumerable.Range(tstart, tend - tstart + 1).Select(i => CreateStimulusDataPoint(cell, Time[i], i)));
            string seriesName = cell.ID.Replace("\"", "\\\"");
            string seriesIndex = chartindex.ToString() + "_" + cell.Sequence.ToString();
            string series = CreateLineChartSeries(dataPoints, chartindex, seriesName, seriesIndex, color, opacity);
            opacity -= dec;
            return series;
        }

        public static string CreateStimuliSubPlot(int chartindex, string chartTitle, List<Cell> cells, double[] Time, int tstart, int tend, 
            double yMin, double yMax, string color)
        {
            byte a = 255;
            byte dec = (byte)(200 / cells.Count);
            string series = string.Join("\r\n", cells.Select(cell => CreateStimuliSeries(chartindex, cell, Time, tstart, tend, color, ref a, dec)));
            return CreateSubPlot(chartindex, chartTitle, series, Time, tstart, tend, yMin, yMax, absoluteYRange: false);
        }

        //One chart per pool
        public static List<string> CreateStimuliMultiPlot(List<CellPool> cellPools, double[] Time, int tstart, int tend,
            CellSelectionStruct cellSelection,
            ref int chartindex)
        {
            List<string> charts = new();
            double yMin = cellPools.Min(cp => cp.GetCells().Min(c => c.MinStimulusValue));
            double yMax = cellPools.Max(cp => cp.GetCells().Max(c => c.MaxStimulusValue));
            foreach (CellPool pool in cellPools)
            {
                List<Cell> cells = pool.GetCells(cellSelection).ToList();
                string color = pool.Color.ToRGB();
                charts.Add(CreateStimuliSubPlot(chartindex++, pool.ID + " Stimuli", cells, Time, tstart, tend, yMin, yMax, color));
            }
            return charts;
        }

        //One chart per cell
        public static List<string> CreateStimuliMultiPlot(List<Cell> cells, double[] Time, int tstart, int tend,
            ref int chartindex)
        {
            List<string> charts = new();
            double yMin = cells.Min(c => c.MinStimulusValue);
            double yMax = cells.Max(c => c.MaxStimulusValue);
            foreach (Cell cell in cells)
            {
                string color = cell.CellPool.Color.ToRGB();
                charts.Add(CreateStimuliSubPlot(chartindex++, cell.ID + " Stimuli", new List<Cell>() { cell }, Time, tstart, tend, yMin, yMax, color));
            }
            return charts;
        }

        #endregion

        #region Plot Membrane Potentials
        private static string CreatePotentialDataPoint(Cell cell, double t, int timeInd)
        {
            return $"{{x:{t:0.##},y:{cell.V[timeInd]:0.######} }}";
        }

        private static string CreatePotentialSeries(int chartindex, Cell cell, double[] Time, int tstart, int tend, string color, ref byte opacity, byte dec)
        {
            string dataPoints = string.Join(",", Enumerable.Range(tstart, tend - tstart + 1).Select(i => CreatePotentialDataPoint(cell, Time[i], i)));
            string seriesName = cell.ID.Replace("\"", "\\\"");
            string seriesIndex = chartindex.ToString() + "_" + cell.Sequence.ToString();
            string series = CreateLineChartSeries(dataPoints, chartindex, seriesName, seriesIndex, color, opacity);
            opacity -= dec;
            return series;
        }

        public static string CreatePotentialsSubPlot(int chartindex, string chartTitle, List<Cell> cells, double[] Time, int tstart, int tend, 
            double yMin, double yMax, string color)
        {
            byte a = 255;
            byte dec = (byte)(200 / cells.Count);
            string series = string.Join("\r\n", cells.Select(cell => CreatePotentialSeries(chartindex, cell, Time, tstart, tend, color, ref a, dec)));
            return CreateSubPlot(chartindex, chartTitle, series, Time, tstart, tend, yMin, yMax, absoluteYRange: false);
        }

        
        //One chart per pool
        public static List<string> CreatePotentialsMultiPlot(List<CellPool> cellPools,
            CellSelectionStruct cellSelection, 
            double[] Time, int tstart, int tend,
            ref int chartindex)
        {
            List<string> charts = new();
            double yMin = cellPools.Min(cp => cp.GetCells().Min(c => c.MinPotentialValue));
            double yMax = cellPools.Max(cp => cp.GetCells().Max(c => c.MaxPotentialValue));

            foreach (CellPool pool in cellPools)
            {
                List<Cell> cells = pool.GetCells(cellSelection).ToList();
                string color = pool.Color.ToRGB();
                charts.Add(CreatePotentialsSubPlot(chartindex++, pool.ID + " Potantials", cells, Time, tstart, tend, yMin, yMax, color));
            }
            return charts;
        }

        //One chart per cell
        public static List<string> CreatePotentialsMultiPlot(List<Cell> cells, 
            double[] Time, int tstart, int tend,
            ref int chartindex)
        {
            List<string> charts = new();
            double yMin = cells.Min(c => c.MinPotentialValue);
            double yMax = cells.Max(c => c.MaxPotentialValue);

            foreach (Cell cell in cells)
            {
                string color = cell.CellPool.Color.ToRGB();
                charts.Add(CreatePotentialsSubPlot(chartindex++, cell.ID + " Potantials", new List<Cell>() { cell }, Time, tstart, tend, yMin, yMax, color));
            }
            return charts;
        }

        #endregion

        #region Plot Currents
        private static string CreateCurrentDataPoint(GapJunction jnc, double t, int timeInd, bool incoming)
        {
            int multiplier = incoming ? 1 : -1;
            return $"{{x:{t:0.##},y:{multiplier * jnc.InputCurrent[timeInd]:0.######} }}";
        }

        private static string CreateCurrentDataPoint(ChemicalSynapse jnc, double t, int timeInd)
        {
            return $"{{x:{t:0.##},y:{jnc.InputCurrent[timeInd]:0.###} }}";
        }

        private static string CreateIOGapCurrentSeries(int chartIndex, int seriesindex, GapJunction jnc, double[] Time, int tstart, int tend, string color, byte opacity, bool incoming)
        {
            string dataPoints = string.Join(",", Enumerable.Range(tstart, tend - tstart + 1).Select(i => CreateCurrentDataPoint(jnc, Time[i], i, incoming)));
            string seriesName = jnc.Cell1.ID.Replace("\"", "\\\"");
            string seriesIndex = chartIndex.ToString() + "_" + seriesindex.ToString();
            return CreateLineChartSeries(dataPoints, chartIndex, seriesName, seriesIndex, color, opacity);
        }

        private static string CreateCurrentSeries(int chartIndex, int seriesIndex, ChemicalSynapse jnc, double[] Time, int tstart, int tend, string color, byte opacity)
        {
            string dataPoints = string.Join(",", Enumerable.Range(tstart, tend - tstart + 1).Select(i => CreateCurrentDataPoint(jnc, Time[i], i)));
            string seriesName = jnc.PreNeuron.ID.Replace("\"", "\\\"");
            string seriesIndexs = chartIndex.ToString() + "_" + seriesIndex.ToString();
            return CreateLineChartSeries(dataPoints,chartIndex, seriesName, seriesIndexs, color, opacity);
        }

        private static string CreateCurrentsSubPlot(int chartindex, string chartTitle, List<Cell> cells, double[] Time, int tstart, int tend, double yMin, double yMax, bool includeGap, bool includeChem)
        {
            byte a = 255;
            byte dec = (byte)(200 / cells.Count);
            string series = "";
            int seriesindex = 0;
            (double minWeight, double maxWeight) = cells.Max(c => c.GetConnectionRange());
            double mult = (maxWeight - minWeight) / 55;

            foreach (Cell cell in cells)
            {
                if (cell is Neuron neuron)
                {
                    if (includeGap)
                    {
                        foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell2 == cell))
                        {
                            a = (byte)((jnc.Conductance - minWeight) * mult + 200);
                            string color = jnc.Cell1.CellPool.Color.ToRGB();
                            series += CreateIOGapCurrentSeries(chartindex, seriesindex++, jnc, Time, tstart, tend, color, a, incoming: true);
                        }
                        foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell1 == cell))
                        {
                            a = (byte)((jnc.Conductance - minWeight) * mult + 200);
                            string color = jnc.Cell2.CellPool.Color.ToRGB();
                            series += CreateIOGapCurrentSeries(chartindex, seriesindex++, jnc, Time, tstart, tend, color, a, incoming: false);
                        }
                    }
                    if (includeChem)
                    {
                        foreach (ChemicalSynapse jnc in neuron.Synapses)
                        {
                            a = (byte)((jnc.Conductance - minWeight) * mult + 200);
                            string color = jnc.PreNeuron.CellPool.Color.ToRGB();
                            series += CreateCurrentSeries(chartindex, seriesindex++, jnc, Time, tstart, tend, color, a);
                        }
                    }
                }
                else if (cell is MuscleCell muscle)
                {
                    if (includeChem)
                    {
                        foreach (ChemicalSynapse jnc in muscle.EndPlates)
                        {
                            a = (byte)((jnc.Conductance - minWeight) * mult + 200);
                            string color = jnc.PreNeuron.CellPool.Color.ToRGB();
                            series += CreateCurrentSeries(chartindex, seriesindex++, jnc, Time, tstart, tend, color, a);
                        }
                    }
                }
            }
            return CreateSubPlot(chartindex, chartTitle, series, Time, tstart, tend, yMin, yMax, absoluteYRange: false);
        }


        //One chart per pool
        public static List<string> CreateCurrentsMultiPlot(List<CellPool> cellPools, CellSelectionStruct cellSelection,
            double[] Time, int tstart, int tend, 
            bool includeGap, bool includeChem,
            ref int chartindex)
        {
            List<string> charts = new();
            double yMin = cellPools.Min(cp => cp.GetCells().Min(c => c.MinCurrentValue));
            double yMax = cellPools.Max(cp => cp.GetCells().Max(c => c.MaxCurrentValue));
            foreach (CellPool pool in cellPools)
            {
                List<Cell> cells = pool.GetCells(cellSelection).ToList();
                charts.Add(CreateCurrentsSubPlot(chartindex++, pool.ID + " Currents", cells, Time, tstart, tend, yMin, yMax, includeGap, includeChem));
            }
            return charts;
        }

        //One chart per cell
        public static List<string> CreateCurrentsMultiPlot(List<Cell> cells, double[] Time, int tstart, int tend, 
            bool includeGap, bool includeChem,
            ref int chartindex)
        {
            List<string> charts = new();
            double yMin = cells.Min(c => c.MinCurrentValue);
            double yMax = cells.Max(c => c.MaxCurrentValue);

            foreach (Cell cell in cells)
            {
                charts.Add(CreateCurrentsSubPlot(chartindex++, cell.ID + " Currents", new List<Cell>() { cell }, Time, tstart, tend, yMin, yMax, includeGap, includeChem));
            }
            return charts;
        }



        #endregion

        public static string PlotCharts(string filename, string title, List<string> charts, int numColumns)
        {
            try
            {
                StringBuilder html = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.html"));

                if (numColumns <= 0) numColumns = 1;

                html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));

                SetPlotDimensions(html, numColumns, charts.Count);
                html.Replace("__CHARTS__", string.Join("\r\n", charts));

                if (!string.IsNullOrEmpty(filename))
                    File.WriteAllText(filename, html.ToString());
                return html.ToString();
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }

    }
}