using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace SiliFish.Services
{
    public class HistogramGenerator : EmbeddedResourceReader
    {
        private static string CreateSomiteDataPoint(string somite, double x, double y)
        {
            return $"{{id:\"{somite}\",settings: {{ fill: am5.color(\"#800080\") }},x:{x:0.##},y:{y:0.##} }}";
        }

        private static string CreateTimeDataPoints(Dictionary<string, Coordinate[]> somiteCoordinates, int timeIndex)
        {
            if (somiteCoordinates == null) return "";
            List<string> somites = new();
            foreach (string somite in somiteCoordinates.Keys)
            {
                Coordinate coor = somiteCoordinates[somite][timeIndex];
                somites.Add(CreateSomiteDataPoint(somite, coor.X, coor.Y));
            }
            return $"[{string.Join(',', somites)}];";
        }

        public static string GenerateHistogram(double[] dataPoints, string title, double width, double height)
        {
            if (!Util.CheckOnlineStatus())
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