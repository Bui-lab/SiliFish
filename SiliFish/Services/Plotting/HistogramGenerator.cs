using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SiliFish.Services.Plotting
{
    public class HistogramGenerator : EmbeddedResourceReader
    {
        private static StringBuilder AddHeader(string title, int numOfGraphs, double width, double height, int maxCols)
        {
            StringBuilder html = new(ReadEmbeddedText("SiliFish.Resources.AmChartHist.html"));
            html.Replace("__STYLE_SHEET__", ReadEmbeddedText("SiliFish.Resources.StyleSheet.css"));
            string chartDivs = "";
            for (int i = 0; i < numOfGraphs; i++)
                chartDivs += $"<div id = \"chartDiv{i}\" class=\"chartDiv\"> </div>\n";

            html.Replace("__CHART_DIVS__", chartDivs);
            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            html.Replace("__WIDTH__", width.ToString());
            html.Replace("__HEIGHT__", height.ToString());
            html.Replace("__MAX_COLS__", maxCols.ToString());
            return html;
        }

        private static StringBuilder AddChart(double[] dataPoints, int index)
        {
            StringBuilder jshtml = new(ReadEmbeddedText("SiliFish.Resources.AmChartHistJSTemplate.html"));
            jshtml.Replace("__INDEX__", index.ToString());
            jshtml.Replace("__DATA__", string.Join(',', dataPoints));
            return jshtml;
        }

        public static string GenerateHistogramHTML(double[] dataPoints, string title, double width, double height)
        {
            double min = dataPoints.Min();
            double max = dataPoints.Max();
            int maxCols = (int)Math.Round((max - min) / 0.1);
            StringBuilder html = AddHeader(title, 1, width, height, maxCols);
            StringBuilder jshtml = AddChart(dataPoints, 0);
            html.Replace("__JAVASCRIPT_HTML__", jshtml.ToString());

            StringBuilder scripts = new(); //scripts is updated after creating the chart, as am5themes_Animated exists in the chart level
            if (Util.CheckOnlineStatus("https://cdn.amcharts.com/lib/5/index.js"))
            {
                scripts.AppendLine("<script src = \"https://cdn.amcharts.com/lib/5/index.js\" ></script>");
                scripts.AppendLine("<script src = \"https://cdn.amcharts.com/lib/5/xy.js\" ></script>");
                scripts.AppendLine("<script src = \"https://cdn.amcharts.com/lib/5/plugins/exporting.js\" ></script>");
                scripts.AppendLine("<script src = \"https://cdn.amcharts.com/lib/5/themes/Animated.js\" ></script>");
            }
            else
            {
                scripts.AppendLine(HTMLUtil.CreateScriptHTML("SiliFish.Resources.amcharts-bundle.js"));
                html.Replace("am5themes_Animated.new", "am5themes_Animated.default.new");
            }
            html.Replace("__AMCHART5_SCRIPTS__", scripts.ToString());
            return html.ToString();
        }
    }
}