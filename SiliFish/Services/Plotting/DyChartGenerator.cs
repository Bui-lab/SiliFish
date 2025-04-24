using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SiliFish.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml.Drawing.Chart;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing.Printing;

namespace SiliFish.Services.Plotting
{
    public class DyChartGenerator : EmbeddedResourceReader
    {
        public static string PlotCharts(string title, List<Chart> charts,
            int width, int height,
            bool synchronized = true, bool showZeroValues = false, bool optimizedForPrinting = false)
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

                StringBuilder styleSheet = new(ReadEmbeddedText("SiliFish.Resources.DyChartStyleSheet.css"));
                html.Replace("__STYLE_SHEET__", styleSheet.ToString());

                string singleXLabel = "";
                string singleYLabel = "";

                if (optimizedForPrinting) //check whether all charts have the same labels
                {
                    html.Replace("__EXTRA_STYLES__",
                        ".chart {max - width: 100 %;margin - left: 20px;}\r\n" +
                    ".dygraph-xlabel {\r\n    " +
                        "font-size: 24px;\r\n" +
                        "}\r\n\r\n" +
                        ".dygraph-ylabel {\r\n    " +
                        "margin-top: -20px;\r\n" +
                        "font-size: 24px;\r\n" +
                        "}\r\n\r\n" +
                        ".dygraph-axis-label-x {\r\n    " +
                        "font-size: 24px;\r\n" +
                        "}\r\n\r\n" +
                        ".dygraph-axis-label-y {\r\n" +
                        "font-size: 24px;\r\n" +
                        "}");
                    if (charts.Select(c => c.xLabel).Distinct().Count() == 1)
                        singleXLabel = HttpUtility.HtmlEncode(charts.First().xLabel);
                    if (charts.Select(c => c.yLabel).Distinct().Count() == 1)
                    {
                        singleYLabel = HttpUtility.HtmlEncode(charts.First().yLabelLong);
                        if (string.IsNullOrEmpty(singleYLabel))
                            singleYLabel = HttpUtility.HtmlEncode(charts.First().yLabel);
                    }
                }
                else
                {
                    html.Replace("__EXTRA_STYLES__", "");
                }


                html.Replace("__MERGED_X_AXIS_LABEL__", singleXLabel);
                html.Replace("__MERGED_Y_AXIS_LABEL__", singleYLabel);
                html.Replace("__LABEL_FONT_SIZE__", GlobalSettings.PlotMergedFontSize.ToString());
                html.Replace("__CHART_WIDTH_PERC__", (90 - GlobalSettings.LegendWidthPercentage).ToString());
                html.Replace("__LEGEND_WIDTH_PERC__", GlobalSettings.LegendWidthPercentage.ToString());

                StringBuilder scripts = new();
                if (Util.CheckOnlineStatus("https://cdnjs.cloudflare.com/ajax/libs/dygraph/2.2.1/dygraph.js"))
                {
                    scripts.AppendLine("<script src=\"https://cdnjs.cloudflare.com/ajax/libs/dygraph/2.2.1/dygraph.js\"></script>");
                }
                else
                {
                    scripts.AppendLine(HTMLUtil.CreateScriptHTML("SiliFish.Resources.dygraph.js"));
                }
                html.Replace("__DYGRAPH_SCRIPTS__", scripts.ToString());

                if (width < 200) width = 200;
                if (height < 100) height = 100;

                html.Replace("__WIDTH_PX__", width.ToString());
                html.Replace("__HEIGHT_PX__", height.ToString());
                string chartDivs = "";
                string eventListeners = "";
                List<string> chartHTML = [];
                Dictionary<string, (double MinY, double MaxY)> yRanges = [];
                if (GlobalSettings.SameYAxis)
                {
                    foreach (string yLabel in charts.Select(c => c.yLabel).Distinct())
                    {
                        yRanges.Add(yLabel, (charts.Where(c => c.yLabel == yLabel).Min(c => c.yMin), charts.Where(c => c.yLabel == yLabel).Max(c => c.yMax)));
                    }
                }
                if (charts != null)
                {
                    foreach (int chartIndex in Enumerable.Range(0, charts.Count))
                    {
                        chartDivs += chartDiv.Replace("__CHART_INDEX__", chartIndex.ToString());
                        eventListeners += eventListener.Replace("__CHART_INDEX__", chartIndex.ToString());
                        StringBuilder sbChart = new(ReadEmbeddedText("SiliFish.Resources.DyChart.js"));
                        sbChart.Replace("__CHART_INDEX__", chartIndex.ToString());
                        sbChart.Replace("__CHART_DATA__", Util.JavaScriptEncode(charts[chartIndex].CsvData));
                        sbChart.Replace("__CHART_COLORS__", charts[chartIndex].csvColors);
                        sbChart.Replace("__CHART_TITLE__", Util.JavaScriptEncode(charts[chartIndex].Title));
                        Chart chart = charts[chartIndex];
                        double yMin = yRanges.Count != 0 ? yRanges[chart.yLabel].MinY : chart.yMin;
                        if (yMin == double.NegativeInfinity) yMin = double.MinValue;
                        if (yMin == double.PositiveInfinity) yMin = double.MaxValue;
                        double yMax = yRanges.Count != 0 ? yRanges[chart.yLabel].MaxY : chart.yMax;
                        if (yMax == double.NegativeInfinity) yMax = double.MinValue;
                        if (yMax == double.PositiveInfinity) yMax = double.MaxValue;

                        sbChart.Replace("__X_MIN__", charts[chartIndex].xMin.ToString(GlobalSettings.PlotDataFormat));
                        sbChart.Replace("__X_MAX__", charts[chartIndex].xMax.ToString(GlobalSettings.PlotDataFormat));
                        sbChart.Replace("__Y_MIN__", yMin.ToString(GlobalSettings.PlotDataFormat));
                        sbChart.Replace("__Y_MAX__", yMax.ToString(GlobalSettings.PlotDataFormat));

                        if (string.IsNullOrEmpty(singleXLabel))
                            sbChart.Replace("__X_LABEL__", Util.JavaScriptEncode(charts[chartIndex].xLabel));
                        else
                            sbChart.Replace("__X_LABEL__", Util.JavaScriptEncode(" "));

                        if (string.IsNullOrEmpty(singleYLabel))
                            sbChart.Replace("__Y_LABEL__", Util.JavaScriptEncode(charts[chartIndex].yLabel));
                        else
                            sbChart.Replace("__Y_LABEL__", Util.JavaScriptEncode(" "));

                        sbChart.Replace("__DRAW_POINTS__", charts[chartIndex].ScatterPlot || charts[chartIndex].drawPoints ? "true" : "false");
                        sbChart.Replace("__POINT_SIZE__", GlobalSettings.PlotPointSize.ToString());
                        sbChart.Replace("__SHOW_ZERO__", showZeroValues.ToString().ToLower());
                        sbChart.Replace("__LOG_SCALE__", charts[chartIndex].logScale ? "true" : "false");
                        sbChart.Replace("__SCATTER_PLOT__", charts[chartIndex].ScatterPlot ? "drawPoints: true,strokeWidth: 0," : "");
                        chartHTML.Add(sbChart.ToString());
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


        public static string Plot(string Title, List<Chart> charts, int width = 480, int height = 240, 
            bool showZeroValues = false, bool optimizedForPrinting = false)
        {
            string PlotHTML = PlotCharts(Title, charts, width, height, 
                synchronized: true, showZeroValues, optimizedForPrinting: optimizedForPrinting);
            return PlotHTML;
        }


        public static string PlotLineCharts(List<Chart> chartsData,
            string mainTitle, int width = 480, int height = 240,
            bool synchronized=true, bool showZeroValues=false)
        {
            List<Chart> charts = [.. chartsData];
            string PlotHTML = PlotCharts(title: mainTitle, charts, width, height, synchronized, showZeroValues);
            return PlotHTML;
        }


    }
}
