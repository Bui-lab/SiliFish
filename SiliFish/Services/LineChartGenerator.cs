using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SiliFish.Extensions;
using SiliFish.ModelUnits;

namespace SiliFish.Services
{
    internal class LineChartGenerator: VisualsGenerator
    {
        private void SetPlotDimensions(StringBuilder html, int numColumns, int numChart)
        {
            int numRows = (int)Math.Ceiling((double)numChart / numColumns);
            int height = numRows < 4 ? numRows * 500 : numRows * 300;
            html.Replace("__HEIGHT_PX__", height.ToString());
            html.Replace("__PERCENT_WIDTH__", (100 / numColumns).ToString());
            html.Replace("__PERCENT_HEIGHT__", (100 / numRows).ToString());
            html.Replace("__COLUMNS__", numColumns.ToString());
        }

        #region Plot Stimuli
        private string CreateStimulusDataPoint(Cell cell, double t, int timeInd)
        {
            return $"{{t:{t:0.##},v:{cell.Stimulus?[timeInd] ?? 0:0.###} }}";
        }

        private string CreateStimuliSeries(int chartindex, Cell cell, double[] Time, int tstart, int tend, string color, ref byte opacity, byte dec)
        {
            StringBuilder series = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.Series.js"));
            series.Replace("__SERIES_NAME__", cell.Name.Replace("\"", "\\\""));
            series.Replace("__SERIES_INDEX__", chartindex.ToString() + "_" + cell.Sequence.ToString());
            series.Replace("__SERIES_COLOR__", color);
            series.Replace("__SERIES_OPACITY__", opacity.ToString());
            opacity -= dec;

            string dataPoints = string.Join(",", Enumerable.Range(tstart, tend - tstart + 1).Select(i => CreateStimulusDataPoint(cell, Time[i], i)));
            series.Replace("__SERIES_DATA__", "[" + dataPoints + "]");
            return series.ToString();
        }

        public string CreateStimuliSubPlot(int chartindex, string chartTitle, List<Cell> cells, double[] Time, int tstart, int tend, string color)
        {
            StringBuilder chart = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.Chart.js"));
            chart.Replace("__CHART_INDEX__", chartindex.ToString());
            chart.Replace("__X_START__", Time[tstart].ToString("0.##"));
            chart.Replace("__X_END__", Time[tend].ToString("0.##"));
            byte a = (byte)255;
            byte dec = (byte)(200 / cells.Count);
            string series = string.Join("\r\n", cells.Select(cell => CreateStimuliSeries(chartindex, cell, Time, tstart, tend, color, ref a, dec)));
            series = series.Replace("__CHART_INDEX__", chartindex.ToString());
            chart.Replace("__SERIES__", series);
            chart.Replace("__CHART_TITLE__", chartTitle);
            return chart.ToString();
        }

        //One chart per pool
        public string CreateStimuliMultiPlot(string filename, string title, List<CellPool> cellPools, double[] Time, int tstart, int tend, int numColumns = 1, int nSample = 0)
        {
            List<Cell> cells = new();

            foreach (CellPool pool in cellPools)
            {
                cells.AddRange(pool.GetCells(nSample).ToList());
            }
            return CreateStimuliMultiPlot(filename, title, cells, Time, tstart, tend, numColumns);
        }

        //One chart per cell
        public string CreateStimuliMultiPlot(string filename, string title, List<Cell> cells, double[] Time, int tstart, int tend, int numColumns = 1)
        {
            StringBuilder html = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.html"));

            if (numColumns <= 0) numColumns = 1;

            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            int chartindex = 0;
            List<string> charts = new();
            foreach (Cell cell in cells)
            {
                string color = cell.CellPool.Color.ToRGB();
                charts.Add(CreateStimuliSubPlot(chartindex++, cell.Name, new List<Cell>() { cell }, Time, tstart, tend, color));
            }
            html.Replace("__CHARTS__", string.Join("\r\n", charts));
            SetPlotDimensions(html, numColumns, cells.Count);

            if (!string.IsNullOrEmpty(filename))
                File.WriteAllText(filename, html.ToString());
            return html.ToString();
        }

        #endregion

        #region Plot Membrane Potentials
        private string CreatePotentialDataPoint(Cell cell, double t, int timeInd)
        {
            return $"{{t:{t:0.##},v:{cell.V[timeInd]:0.######} }}";
        }

        private string CreatePotentialSeries(int chartindex, Cell cell, double[] Time, int tstart, int tend, string color, ref byte opacity, byte dec)
        {
            StringBuilder series = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.Series.js"));
            series.Replace("__SERIES_NAME__", cell.Name.Replace("\"", "\\\""));
            series.Replace("__SERIES_INDEX__", chartindex.ToString() + "_" + cell.Sequence.ToString());
            series.Replace("__SERIES_COLOR__", color);
            series.Replace("__SERIES_OPACITY__", opacity.ToString());
            opacity -= dec;

            string dataPoints = string.Join(",", Enumerable.Range(tstart, tend - tstart + 1).Select(i => CreatePotentialDataPoint(cell, Time[i], i)));
            series.Replace("__SERIES_DATA__", "[" + dataPoints + "]");
            return series.ToString();
        }

        public string CreatePotentialsSubPlot(int chartindex, string chartTitle, List<Cell> cells, double[] Time, int tstart, int tend, string color)
        {
            StringBuilder chart = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.Chart.js"));
            chart.Replace("__CHART_INDEX__", chartindex.ToString());
            chart.Replace("__X_START__", Time[tstart].ToString("0.##"));
            chart.Replace("__X_END__", Time[tend].ToString("0.##"));
            byte a = (byte)255;
            byte dec = (byte)(200 / cells.Count);
            string series = string.Join("\r\n", cells.Select(cell => CreatePotentialSeries(chartindex, cell, Time, tstart, tend, color, ref a, dec)));
            series = series.Replace("__CHART_INDEX__", chartindex.ToString());
            chart.Replace("__SERIES__", series);
            chart.Replace("__CHART_TITLE__", chartTitle);
            return chart.ToString();
        }

        //One chart per pool
        public string CreatePotentialsMultiPlot(string filename, string title, List<CellPool> cellPools, double[] Time, int tstart, int tend, int numColumns = 1, int nSample = 0)
        {
            List<Cell> cells = new();

            foreach (CellPool pool in cellPools)
            {
                cells.AddRange(pool.GetCells(nSample).ToList());
            }
            return CreatePotentialsMultiPlot(filename, title, cells, Time, tstart, tend, numColumns);
        }

        //One chart per cell
        public string CreatePotentialsMultiPlot(string filename, string title, List<Cell> cells, double[] Time, int tstart, int tend, int numColumns = 1)
        {
            StringBuilder html = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.html"));

            if (numColumns <= 0) numColumns = 1;

            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            int chartindex = 0;
            List<string> charts = new();
            foreach (Cell cell in cells)
            {
                string color = cell.CellPool.Color.ToRGB();
                charts.Add(CreatePotentialsSubPlot(chartindex++, cell.Name, new List<Cell>() { cell }, Time, tstart, tend, color));
            }
            html.Replace("__CHARTS__", string.Join("\r\n", charts));
            SetPlotDimensions(html, numColumns, cells.Count);

            if (!string.IsNullOrEmpty(filename))
                File.WriteAllText(filename, html.ToString());
            return html.ToString();
        }

        #endregion

        #region Plot Currents
        private string CreateCurrentDataPoint(GapJunction jnc, double t, int timeInd, bool incoming)
        {
            int multiplier = incoming ? 1 : -1;
            return $"{{t:{t:0.##},v:{multiplier * jnc.InputCurrent[timeInd]:0.######} }}";
        }

        private string CreateCurrentDataPoint(ChemicalSynapse jnc, double t, int timeInd)
        {
            return $"{{t:{t:0.##},v:{jnc.InputCurrent[timeInd]:0.###} }}";
        }

        private string CreateIOGapCurrentSeries(int chartindex, int seriesindex, GapJunction jnc, double[] Time, int tstart, int tend, string color, byte opacity, bool incoming)
        {
            StringBuilder series = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.Series.js"));
            series.Replace("__SERIES_NAME__", jnc.Cell1.Name.Replace("\"", "\\\""));
            series.Replace("__SERIES_INDEX__", chartindex.ToString() + "_" + seriesindex.ToString());
            series.Replace("__SERIES_COLOR__", color);
            series.Replace("__SERIES_OPACITY__", opacity.ToString());

            string dataPoints = string.Join(",", Enumerable.Range(tstart, tend - tstart + 1).Select(i => CreateCurrentDataPoint(jnc, Time[i], i, incoming)));
            series.Replace("__SERIES_DATA__", "[" + dataPoints + "]");
            return series.ToString();
        }

        private string CreateCurrentSeries(int chartindex, int seriesindex, ChemicalSynapse jnc, double[] Time, int tstart, int tend, string color, byte opacity)
        {
            StringBuilder series = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.Series.js"));
            series.Replace("__SERIES_NAME__", jnc.PreNeuron.Name.Replace("\"", "\\\""));
            series.Replace("__SERIES_INDEX__", chartindex.ToString() + "_" + seriesindex.ToString());
            series.Replace("__SERIES_COLOR__", color);
            series.Replace("__SERIES_OPACITY__", opacity.ToString());

            string dataPoints = string.Join(",", Enumerable.Range(tstart, tend - tstart + 1).Select(i => CreateCurrentDataPoint(jnc, Time[i], i)));
            series.Replace("__SERIES_DATA__", "[" + dataPoints + "]");
            return series.ToString();
        }

        private string CreateCurrentsSubPlot(int chartindex, string chartTitle, Cell cell, double[] Time, int tstart, int tend, bool includeGap, bool includeChem)
        {
            StringBuilder chart = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.Chart.js"));
            chart.Replace("__CHART_INDEX__", chartindex.ToString());
            chart.Replace("__X_START__", Time[tstart].ToString("0.##"));
            chart.Replace("__X_END__", Time[tend].ToString("0.##"));
            byte a = (byte)255;

            (double minWeight, double maxWeight) = cell.GetConnectionRange();
            double mult = (maxWeight - minWeight) / 55;
            string series = "";
            int seriesindex = 0;
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
            series = series.Replace("__CHART_INDEX__", chartindex.ToString());
            chart.Replace("__SERIES__", series);
            chart.Replace("__CHART_TITLE__", chartTitle);
            return chart.ToString();
        }


        //One chart per cell within each pool
        public string CreateCurrentsMultiPlot(string filename, string title, List<CellPool> cellPools, double[] Time, int tstart, int tend, 
            int numColumns = 1, bool includeGap = true, bool includeChem = true, int nSample = 0)
        {
            List<Cell> cells = new();

            foreach (CellPool pool in cellPools)
            {
                cells.AddRange(pool.GetCells(nSample).ToList());
            }
            return CreateCurrentsMultiPlot(filename, title, cells, Time, tstart, tend, numColumns, includeGap, includeChem);
        }

        //One chart per cell
        public string CreateCurrentsMultiPlot(string filename, string title, List<Cell> cells, double[] Time, int tstart, int tend, int numColumns = 1, bool includeGap = true, bool includeChem = true)
        {
            StringBuilder html = new(ReadEmbeddedResource("SiliFish.Resources.LineChart.html"));

            if (numColumns <= 0) numColumns = 1;

            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            int chartindex = 0;
            List<string> charts = new();

            foreach (Cell cell in cells)
            {
                charts.Add(CreateCurrentsSubPlot(chartindex++, cell.Name, cell, Time, tstart, tend, includeGap, includeChem));
            }
            html.Replace("__CHARTS__", string.Join("\r\n", charts));
            SetPlotDimensions(html, numColumns, cells.Count);

            if (!string.IsNullOrEmpty(filename))
                File.WriteAllText(filename, html.ToString());
            return html.ToString();
        }



        #endregion
    }
}