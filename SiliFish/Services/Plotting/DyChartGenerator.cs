using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SiliFish.Services.Plotting.PlotGenerators;

namespace SiliFish.Services.Plotting
{
    public class DyChartGenerator : EmbeddedResourceReader
    {
        public static string PlotCharts(string title, List<Chart> charts, bool synchronized, bool showZeroValues, int width, int height)
        {
            try
            {
                StringBuilder html = synchronized ?
                    new(ReadEmbeddedText("SiliFish.Resources.DyChartSync.html")) :
                    new(ReadEmbeddedText("SiliFish.Resources.DyChartUnsync.html"));
                StringBuilder plot = new(ReadEmbeddedText("SiliFish.Resources.DyChart.js"));
                StringBuilder synchronizer = new(ReadEmbeddedText("SiliFish.Resources.DySynchronizer.js"));
                string chartDiv = ReadEmbeddedText("SiliFish.Resources.DyChartDiv.html");
                string eventListener = ReadEmbeddedText("SiliFish.Resources.DyChartEvent.html");

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
                string eventListeners = "";
                List<string> chartHTML = new();
                if (charts != null)
                {
                    foreach (int chartIndex in Enumerable.Range(0, charts.Count))
                    {
                        chartDivs += chartDiv.Replace("__CHART_INDEX__", chartIndex.ToString());
                        eventListeners += eventListener.Replace("__CHART_INDEX__", chartIndex.ToString());
                        StringBuilder chart = new(ReadEmbeddedText("SiliFish.Resources.DyChart.js"));
                        chart.Replace("__CHART_INDEX__", chartIndex.ToString());
                        chart.Replace("__CHART_DATA__", charts[chartIndex].CsvData);
                        chart.Replace("__CHART_COLORS__", charts[chartIndex].Color);
                        chart.Replace("__CHART_TITLE__", Util.JavaScriptEncode(charts[chartIndex].Title));
                        chart.Replace("__X_MIN__", charts[chartIndex].xMin.ToString(GlobalSettings.PlotDataFormat));
                        chart.Replace("__X_MAX__", charts[chartIndex].xMax.ToString(GlobalSettings.PlotDataFormat));
                        chart.Replace("__Y_MIN__", charts[chartIndex].yMin.ToString(GlobalSettings.PlotDataFormat));
                        chart.Replace("__Y_MAX__", charts[chartIndex].yMax.ToString(GlobalSettings.PlotDataFormat));
                        chart.Replace("__Y_LABEL__", Util.JavaScriptEncode(charts[chartIndex].yLabel));
                        chart.Replace("__X_LABEL__", Util.JavaScriptEncode(charts[chartIndex].xLabel));
                        chart.Replace("__DRAW_POINTS__", charts[chartIndex].ScatterPlot || charts[chartIndex].drawPoints ? "true" : "false");
                        chart.Replace("__SHOW_ZERO__", showZeroValues.ToString().ToLower());
                        chart.Replace("__LOG_SCALE__", charts[chartIndex].logScale ? "true" : "false");
                        chart.Replace("__SCATTER_PLOT__", charts[chartIndex].ScatterPlot ? "drawPoints: true,strokeWidth: 0," : "");
                        chartHTML.Add(chart.ToString());
                    }
                }
                html.Replace("__CHART_DIVS__", chartDivs);
                html.Replace("__DY_LISTENERS__", eventListeners);
                html.Replace("__CHARTS__", string.Join('\n', chartHTML));
                html.Replace("__DY_SYNCHRONIZER__", synchronizer.ToString());

                return html.ToString();
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }


        public static string Plot(string Title, List<Chart> charts, bool showZeroValues, int width = 480, int height = 240)
        {
            string PlotHTML = PlotCharts(Title, charts, synchronized: true, showZeroValues, width, height);
            return PlotHTML;
        }


        public static string PlotLineCharts(List<Chart> chartsData,
            string mainTitle, bool synchronized, bool showZeroValues,
            int width = 480, int height = 240)
        {
            List<Chart> charts = new();
            foreach (Chart chartData in chartsData)
            {
                charts.Add(chartData);
            }
            string PlotHTML = PlotCharts(title: mainTitle, charts, synchronized, showZeroValues, width, height);
            return PlotHTML;
        }
    }
}
