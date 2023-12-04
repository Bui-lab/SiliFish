using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Services.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SiliFish.Services
{
    public class AnimationGenerator : EmbeddedResourceReader
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
            double ratio = (double)(maxX - minX) / (maxY - minY);
            if (ratio > 1)
                sheight = (800 / ratio).ToString("0");
            else if (ratio < 1)
                swidth = (800 * ratio).ToString("0");


            html.Replace("__WIDTH__", swidth);
            html.Replace("__HEIGHT__", sheight);
        }
        public static string CreateTimeSeries(string title, string animParams,
            Dictionary<string, Coordinate[]> somiteCoordinates, double[] Time,
            int iStart, int iEnd, double dt, double animdt)
        {
            if (!Util.CheckOnlineStatus())
                return "Animation requires internet connection.";

            StringBuilder html = new(ReadEmbeddedText("SiliFish.Resources.Animation.html"));
            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            html.Replace("__PARAMS__", HttpUtility.HtmlEncode(animParams).Replace("\n", "<br/>"));

            int jump = (int)(animdt / dt);
            if (jump < 1) jump = 1;
            List<string> timeSeries = new();
            Dictionary<int, string> somitePoints = new();
            foreach (int timeIndex in Enumerable.Range(0, (iEnd - iStart + 1) / jump))
            {
                string curPos = CreateTimeDataPoints(somiteCoordinates, timeIndex * jump);
                somitePoints.Add(timeIndex * jump, curPos);
            }
            if (jump > 1)//add the final ppoint
            {
                string lastPos = CreateTimeDataPoints(somiteCoordinates, iEnd - iStart);
                somitePoints.AddObject(iEnd - iStart, lastPos);
            }

            foreach (int timeIndex in somitePoints.Keys)
                timeSeries.Add($"FD[{timeIndex}] ={somitePoints[timeIndex]}");

            SetChartDimensions(somiteCoordinates, html);

            html.Replace("__FULL_DATA__", string.Join("\r\n", timeSeries));
            html.Replace("__TIME_START__", (Time[iStart]).ToString());
            html.Replace("__TIME_END__", (Time[iEnd]).ToString());
            html.Replace("__TIME_INCREMENT__", dt.ToString());

            return html.ToString();
        }


        public static string GenerateAnimation(RunningModel model, int tStart, int tEnd, double animdt, out Dictionary<string, Coordinate[]> spineCoordinates)
        {
            spineCoordinates = null;
            if (model == null || !model.ModelRun)
                return null;

            int tMax = model.RunParam.MaxTime;
            int tSkip = model.RunParam.SkipDuration;
            double dt = model.RunParam.DeltaT;

            if (tEnd < tStart || tEnd > tMax)
                tEnd = tMax;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            if (iEnd >= model.TimeArray.Length)
                iEnd = model.TimeArray.Length - 1;

            spineCoordinates = SwimmingKinematics.GenerateSpineCoordinates(model, iStart, iEnd);
            return CreateTimeSeries(title: model.ModelName + "Animation.html",
                model.KinemParam.GetAnimationDetails(),
                spineCoordinates,
                model.TimeArray,
                iStart,
                iEnd,
                dt,
                animdt);
        }
    }
}