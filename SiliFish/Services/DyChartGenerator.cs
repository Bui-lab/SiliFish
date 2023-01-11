using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SiliFish.Services
{
    public class DyChartGenerator : VisualsGenerator
    {

        private static ChartDataStruct CreateLineChart(ChartDataStruct chartData)
        {
            ChartDataStruct chart = new();
            if (chartData.xData?.Length != chartData.yData?.Length && chartData.xData?.Length != chartData.yMultiData?.FirstOrDefault().Length)
                return chart;

            double yMin = chartData.yMin;
            double yMax = chartData.yMax;
            double xMin = chartData.xMin;
            double xMax = chartData.xMax;
            Helpers.Util.SetYRange(ref yMin, ref yMax);

            string columnTitles = $"{chartData.xLabel},{chartData.yLabel}";
            List<string> data = new(chartData.xData.Select(t => t.ToString(CurrentSettings.Settings.DecimalPointFormat) + ","));
            if (chartData.yData != null)
            {
                foreach (int i in Enumerable.Range(0, chartData.yData.Length))
                    data[i] += chartData.yData[i].ToString(CurrentSettings.Settings.DecimalPointFormat) + ",";
            }
            else
            {
                for (int colIndex = 0; colIndex < chartData.yMultiData.Count; colIndex++)
                {
                    double[] singleyData = chartData.yMultiData[colIndex];
                    foreach (int i in Enumerable.Range(0, singleyData.Length))
                        data[i] += singleyData[i].ToString(CurrentSettings.Settings.DecimalPointFormat) + ",";
                }
            }
            string csvData = $"`{columnTitles}\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
            chart = new ChartDataStruct
            {
                CsvData = csvData,
                Color = $"`{chartData.Color}`",
                Title = $"`{chartData.Title}`",
                xLabel = $"`{chartData.xLabel}`",
                yLabel = $"`{chartData.yLabel}`",
                xMin = xMin,
                xMax = xMax,
                yMin = yMin,
                yMax = yMax,
                drawPoints = chartData.drawPoints,
                logScale = chartData.logScale
            };

            return chart;
        }

        public static string PlotCharts(string title, List<ChartDataStruct> charts, bool synchronized, int width, int height)
        {
            try
            {
                StringBuilder html = synchronized ?
                    new(ReadEmbeddedText("SiliFish.Resources.DyChartSync.html")) :
                    new(ReadEmbeddedText("SiliFish.Resources.DyChartUnsync.html"));
                StringBuilder plot = new(ReadEmbeddedText("SiliFish.Resources.DyChart.js"));
                StringBuilder synchronizer = new(ReadEmbeddedText("SiliFish.Resources.DySynchronizer.js"));
                string chartDiv = ReadEmbeddedText("SiliFish.Resources.DyChartDiv.html");

                html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));

                if (Util.CheckOnlineStatus())
                {
                    html.Replace("__OFFLINE_DYGRAPH_SCRIPT__", "");
                    html.Replace("__ONLINE_DYGRAPH_SCRIPT__", "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/dygraph/2.1.0/dygraph.js\"></script>");
                }
                else
                {
                    html.Replace("__OFFLINE_DYGRAPH_SCRIPT__", ReadEmbeddedText("SiliFish.Resources.dygraph.js"));
                    html.Replace("__ONLINE_DYGRAPH_SCRIPT__", "");
                }

                if (width < 200) width = 200;
                if (height < 100) height = 100;

                html.Replace("__WIDTH_PX__", width.ToString());
                html.Replace("__HEIGHT_PX__", height.ToString());
                string chartDivs = "";
                List<string> chartHTML = new();
                foreach (int chartIndex in Enumerable.Range(0, charts.Count))
                {
                    chartDivs += chartDiv.Replace("__CHART_INDEX__", chartIndex.ToString());
                    StringBuilder chart = new(ReadEmbeddedText("SiliFish.Resources.DyChart.js"));
                    chart.Replace("__CHART_INDEX__", chartIndex.ToString());
                    chart.Replace("__CHART_DATA__", charts[chartIndex].CsvData);
                    chart.Replace("__CHART_COLORS__", charts[chartIndex].Color);
                    chart.Replace("__CHART_TITLE__", Util.JavaScriptEncode(charts[chartIndex].Title));
                    chart.Replace("__X_MIN__", charts[chartIndex].xMin.ToString(CurrentSettings.Settings.DecimalPointFormat));
                    chart.Replace("__X_MAX__", charts[chartIndex].xMax.ToString(CurrentSettings.Settings.DecimalPointFormat));
                    chart.Replace("__Y_MIN__", charts[chartIndex].yMin.ToString(CurrentSettings.Settings.DecimalPointFormat));
                    chart.Replace("__Y_MAX__", charts[chartIndex].yMax.ToString(CurrentSettings.Settings.DecimalPointFormat));
                    chart.Replace("__Y_LABEL__", Util.JavaScriptEncode(charts[chartIndex].yLabel));
                    chart.Replace("__X_LABEL__", Util.JavaScriptEncode(charts[chartIndex].xLabel));
                    chart.Replace("__DRAW_POINTS__", charts[chartIndex].drawPoints ? "true" : "false");
                    chart.Replace("__LOG_SCALE__", charts[chartIndex].logScale ? "true" : "false");
                    chart.Replace("__SCATTER_PLOT__", charts[chartIndex].ScatterPlot ? "drawPoints: true,strokeWidth: 0," : "");
                    chartHTML.Add(chart.ToString());
                }
                html.Replace("__CHART_DIVS__", chartDivs);
                html.Replace("__CHARTS__", string.Join('\n', chartHTML));
                html.Replace("__DY_SYNCHRONIZER__", synchronizer.ToString());

                return html.ToString();
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }


        public static string Plot(string Title, List<ChartDataStruct> charts, int width = 480, int height = 240)
        {
            string PlotHTML = PlotCharts(Title, charts, synchronized: true, width, height);
            return PlotHTML;
        }

        public static string PlotSummaryMembranePotentials(SwimmingModel model, List<Cell> Cells,
            int tStart = 0, int tEnd = -1, int width = 480, int height = 240)
        {
            double dt = model.runParam.dt;
            int iStart = (int)((tStart + model.runParam.tSkip_ms) / dt);
            int iEnd = (int)((tEnd + model.runParam.tSkip_ms) / dt);
            if (iEnd < iStart || iEnd >= model.TimeArray.Length)
                iEnd = model.TimeArray.Length - 1;

            List<ChartDataStruct> charts = PlotDataGenerator.CreateMembranePotentialCharts(model.TimeArray, Cells, groupByPool: true, iStart, iEnd);
            string PlotHTML = PlotCharts(title: "Summary Membrane Potentials", charts, synchronized: true, width, height);
            return PlotHTML;
        }

        public static string PlotLineCharts(List<ChartDataStruct> chartsData,
            string mainTitle, bool synchronized,
            int width = 480, int height = 240)
        {
            List<ChartDataStruct> charts = new();
            foreach (ChartDataStruct chartData in chartsData)
            {
                charts.Add(chartData);// CreateLineChart(chartData));
            }
            string PlotHTML = PlotCharts(title: mainTitle, charts, synchronized, width, height);
            return PlotHTML;
        }
    }
}
