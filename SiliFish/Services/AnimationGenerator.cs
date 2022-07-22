﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SiliFish.DataTypes;
using SiliFish.Extensions;
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
            double ratio = (double)(maxX-minX)/(maxY-minY);
            if (ratio > 1)
                sheight = (800 / ratio).ToString("0");
            else if (ratio < 1)
                swidth = (800 * ratio).ToString("0");


            html.Replace("__WIDTH__", swidth);
            html.Replace("__HEIGHT__", sheight);
        }
        public string CreateTimeSeries(string title, string animParams,
            Dictionary<string, Coordinate[]> somiteCoordinates, double[] Time, 
            int iStart, int iEnd, double dt)
        {
            StringBuilder html = new(ReadEmbeddedResource("SiliFish.Resources.Animation.html"));
            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            html.Replace("__PARAMS__", HttpUtility.HtmlEncode(animParams).Replace("\n", "<br/>"));

            List<string> timeSeries = new();
            Dictionary<int, string> somitePoints = new Dictionary<int, string>();
            string lastPos = "";
            string initialPos = "";
            foreach (int timeIndex in Enumerable.Range(0, iEnd - iStart + 1))
            {
                string curPos = CreateTimeDataPoints(somiteCoordinates, timeIndex);
                if (initialPos == "")
                    initialPos = curPos;
                if (lastPos == curPos && lastPos == initialPos)
                    curPos = "[];";
                else
                {
                    if (lastPos != "")
                        somitePoints.AddObject(timeIndex - 1, lastPos);
                    lastPos = curPos;
                }
                somitePoints.Add(timeIndex, curPos);
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
        
        

        public static string GenerateAnimation(SwimmingModel model, int tStart = 0, int tEnd = -1)
        {
            if (model == null || !model.ModelRun)
                return null;

            int tMax = model.runParam.tMax;
            int tSkip = model.runParam.tSkip_ms;
            double dt = model.runParam.dt;

            if (tEnd < tStart || tEnd > tMax)
                tEnd = tMax;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            if (iEnd >= model.TimeArray.Length)
                iEnd = model.TimeArray.Length - 1;

            AnimationGenerator animationGenerator = new();
            Dictionary<string, object> animParamDict = model.GetParameters().Where(p => p.Key.StartsWith("Animation")).ToDictionary(x => x.Key, x => x.Value);
            string animParams = "";
            foreach (var param in animParamDict)
            {
                animParams += $"{param.Key}: {param.Value}\n";
            }

            return animationGenerator.CreateTimeSeries(title: model.ModelName + "Animation.html",
                animParams,
                model.GenerateSpineCoordinates(iStart, iEnd), model.TimeArray, iStart, iEnd, dt);
        }
    }
}