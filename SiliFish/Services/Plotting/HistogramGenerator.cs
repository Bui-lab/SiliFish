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
        public static string GenerateHistogram(double[] dataPoints, string title, double width, double height)
        {
            if (!Util.CheckOnlineStatus("https://www.amcharts.com"))
                return "Histogram generation requires internet connection.";

            StringBuilder html = new(ReadEmbeddedText("SiliFish.Resources.AmChartHist.html"));
            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            html.Replace("__DATA__", string.Join(',', dataPoints));
            html.Replace("__WIDTH__", width.ToString());
            html.Replace("__HEIGHT__", height.ToString());
            double min = dataPoints.Min();
            double max = dataPoints.Max();
            int maxCols = (int)Math.Round((max - min) / 0.1);
            html.Replace("__MAX_COLS__", maxCols.ToString());
            return html.ToString();
        }
    }
}