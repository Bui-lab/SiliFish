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

            string swidth = "800";
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

        private Dictionary<string, Coordinate[]> GenerateSomiteCoordinates(SwimmingModel model, int startIndex, int endIndex)
        {
            List<Cell> LeftMuscleCells = model.MusclePools.Where(mp => mp.PositionLeftRight == SagittalPlane.Left).SelectMany(mp => mp.GetCells()).ToList();
            List<Cell> RightMuscleCells = model.MusclePools.Where(mp => mp.PositionLeftRight == SagittalPlane.Right).SelectMany(mp => mp.GetCells()).ToList();

            int nmax = endIndex - startIndex + 1;
            //TODO: left and right muscle cell count can be different
            int nMuscle = LeftMuscleCells.Count;
            if (nMuscle == 0) return null;
            // Allocating arrays for velocity and position
            double[,] vel = new double[nMuscle, nmax];
            double[,] angle = new double[nMuscle, nmax];
            // Setting constants and initial values for vel. and pos.
            double khi = model.khi;
            double w0 = model.w0;
            double vel0 = 0.0;
            double angle0 = 0.0;
            double dt = RunParam.dt;
            double alpha = model.alpha;
            //Wd = w0
            int k = 0;

            foreach (MuscleCell leftMuscle in LeftMuscleCells.OrderBy(c => c.Sequence))
            {
                MuscleCell rightMuscle = (MuscleCell)RightMuscleCells.FirstOrDefault(c => c.Sequence == leftMuscle.Sequence);
                vel[k, 0] = vel0;
                angle[k, 0] = angle0;
                angle[nMuscle - 1, 0] = 0.0;
                foreach (var i in Enumerable.Range(1, nmax - 1))
                {
                    double R = (leftMuscle.R + rightMuscle.R) / 2;
                    double coef = alpha * (1 - 0.2 * R);//The formula in the paper
                    //TODO test the coef in the paper
                    coef = 0.1;
                    double voltDiff = rightMuscle.V[startIndex + i - 1] - leftMuscle.V[startIndex + i - 1];
                    double acc = -Math.Pow(w0, 2) * angle[k, i - 1] - 2 * vel[k, i - 1] * khi * w0 + coef * voltDiff;
                    vel[k, i] = vel[k, i - 1] + acc * dt;
                    angle[k, i] = angle[k, i - 1] + vel[k, i - 1] * dt;
                }
                k++;
            }

            //## DYNAMIC PLOTTING
            double[,] x = new double[nMuscle, nmax + 1];
            double[,] y = new double[nMuscle, nmax + 1];
            foreach (var i in Enumerable.Range(0, nmax))
            {
                x[0, i] = 0;
                y[0, i] = 0;
                angle[0, i] = 0;
                foreach (int l in Enumerable.Range(1, nMuscle - 1))
                {
                    angle[l, i] = angle[l - 1, i] + angle[l, i];
                    x[l, i] = x[l - 1, i] + Math.Sin(angle[l, i]);
                    y[l, i] = y[l - 1, i] - Math.Cos(angle[l, i]);
                }
            }

            Dictionary<string, Coordinate[]> somiteCoordinates = new();
            foreach (int somiteIndex in Enumerable.Range(0, nMuscle))
            {
                string somite = "Somite" + somiteIndex.ToString();
                somiteCoordinates.Add(somite, new Coordinate[nmax + 1]);
                somiteCoordinates[somite][0] = (0, 0);
                foreach (var i in Enumerable.Range(0, nmax))
                {
                    somiteCoordinates[somite][i] = (x[somiteIndex, i], y[somiteIndex, i]);
                }
            }
            return somiteCoordinates;
            /*TODO save animation CSV
                using FileStream fs = File.Open(Util.OutputFolder + "AnimationXY.csv", FileMode.Create, FileAccess.Write);
            using StreamWriter sw = new(fs);
            sw.WriteLine("Time,Somite,X,Y");

            foreach (var i in Enumerable.Range(1, nmax))
            {
                string rowStart = (Time[startIndex + i - 1] - offset).ToString("0.##");
                foreach (int l in Enumerable.Range(1, nMuscle - 1))
                {
                    string row = rowStart + ",Somite" + l.ToString() + "," + x[l, i].ToString() + "," + y[l, i].ToString();
                    sw.WriteLine(row);
                }
            }*/
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
                GenerateSomiteCoordinates(model, iStart, iEnd), model.TimeArray, iStart, iEnd, tSkip, dt);
        }
    }
}