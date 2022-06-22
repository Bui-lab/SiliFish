using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SiliFish.DataTypes;
using SiliFish.ModelUnits;

namespace SiliFish.Services
{
    public class AnimationGenerator: VisualsGenerator
    {
        private static string CreateSomiteDataPoint(string somite, double x, double y)
        {
            return $"{{id:\"{somite}\",settings: {{ fill: am5.color(\"#800080\") }},x:{x:0.##},y:{y:0.##} }}";
        }

        private static string CreateTimeDataPoints(Dictionary<string, Coordinate[]> somiteCoordinates, int timeIndex)
        {
            if (somiteCoordinates == null) return "";
            List<string> timePoints = new();
            foreach (string somite in somiteCoordinates.Keys)
            {
                Coordinate coor = somiteCoordinates[somite][timeIndex];
                timePoints.Add(CreateSomiteDataPoint(somite, coor.X, coor.Y));
            }
            return $"fullData[{timeIndex}] = [{string.Join(',', timePoints)}];";
        }

        private static void SetChartDimensions(Dictionary<string, Coordinate[]> somiteCoordinates, StringBuilder html)
        {
            if (somiteCoordinates == null)
            {
                html.Replace("__X_MIN__", "0");
                html.Replace("__X_MAX__", "100");
                html.Replace("__Y_MIN__", "0");
                html.Replace("__Y_MAX__", "100");

                html.Replace("__WIDTH__", "100");
                html.Replace("__HEIGHT__", "100");
                return;
            }
            int minX = (int)Math.Floor(somiteCoordinates.Values.Min(v => v.Min(c => c.X))) - 1;
            int maxX = (int)Math.Ceiling(somiteCoordinates.Values.Max(v => v.Max(c => c.X))) + 1;
            if (minX <= 0 && maxX >= 0) //make symmetric
            {
                int abs = Math.Max(-minX, maxX);
                minX = -abs;
                maxX = abs;
            }
            int minY = (int)Math.Floor(somiteCoordinates.Values.Min(v => v.Min(c => c.Y))) - 1;
            int maxY = (int)Math.Ceiling(somiteCoordinates.Values.Max(v => v.Max(c => c.Y))) + 1;

            html.Replace("__X_MIN__", minX.ToString());
            html.Replace("__X_MAX__", maxX.ToString());
            html.Replace("__Y_MIN__", minY.ToString());
            html.Replace("__Y_MAX__", maxY.ToString());

            string swidth = "4800";
            string sheight = "800";
            //Calculate ratio
            double ratio = (double)(maxX-minX)/(maxY-minY);
            if (ratio > 1)
                sheight = (800 / ratio).ToString("0");
            else if (ratio < 1)
                swidth = (800 * ratio).ToString("0");


            html.Replace("__WIDTH__", swidth);
            html.Replace("__HEIGHT__", sheight);
        }
        public string CreateTimeSeries(string title, Dictionary<string, Coordinate[]> somiteCoordinates, double[] Time, 
            int timeStart, int timeEnd, int offset, double dt)
        {
            StringBuilder html = new(ReadEmbeddedResource("SiliFish.Resources.Animation.html"));
            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));

            List<string> timeSeries = new();
            foreach (int timeIndex in Enumerable.Range(0, timeEnd-timeStart+1))
                timeSeries.Add(CreateTimeDataPoints(somiteCoordinates, timeIndex));

            SetChartDimensions(somiteCoordinates, html);

            html.Replace("__FULL_DATA__", string.Join("\r\n", timeSeries));
            html.Replace("__TIME_START__", (Time[timeStart] - offset).ToString());
            html.Replace("__TIME_END__", (Time[timeEnd] - offset).ToString());
            html.Replace("__TIME_INCREMENT__", dt.ToString());
            
            return html.ToString();
        }
        
        

        public virtual string GenerateAnimation(SwimmingModel model, int tStart = 0, int tEnd = -1)
        {
            if (model == null || !model.ModelRun)
                return null;

            int tMax = model.runParam.tMax;
            int tSkip = model.runParam.tSkip;
            double dt = RunParam.dt;

            if (tEnd < tStart || tEnd >= tMax)
                tEnd = tMax - 1;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);

            AnimationGenerator animationGenerator = new();
            return animationGenerator.CreateTimeSeries(title: model.ModelName + "Animation.html", 
                model.GenerateSpineCoordinates(iStart, iEnd), model.TimeArray, iStart, iEnd, tSkip, dt);
        }
    }
}