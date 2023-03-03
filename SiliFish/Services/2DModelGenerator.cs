using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SiliFish.ModelUnits.Junction;
using System.Xml.Serialization;

namespace SiliFish.Services
{
    public class TwoDModelGenerator : EmbeddedResourceReader
    {
        double XMult, YMult;
        double XMin = double.MaxValue, YMin = double.MaxValue;
        double XMax = double.MinValue, YMax = double.MinValue;
        double XOffset, YOffset;
        double WeightMult;
        Dictionary<string, (double, double)> PoolCoordinates;
        private string CreateLinkDataPoint(InterPool interPool)
        {
            string curvInfo = interPool.SourcePool == interPool.TargetPool ? ",curv: 0.7" : "";
            string conductance = interPool.MinConductance == interPool.MaxConductance ?
                interPool.MinConductance.ToString("0.######") :
                $"{interPool.MinConductance:0.######} -  {interPool.MaxConductance:0.######}";
            string link = $"{{\"source\":\"{interPool.SourcePool}\"," +
                $"\"target\":\"{interPool.TargetPool}\"," +
                $"\"value\":{GetNewWeight(interPool.CountJunctions):0.######}," +
                $"\"conductance\":\"{conductance}\"" +
                $"{curvInfo} }}";
            ;
            return link;
        }

        private void GetNewCoordinates(CellPool pool)
        {
            (double x, double y, double _) = pool.XYZMiddle();
            PoolCoordinates.Add(pool.ID, (x, y));
        }
        private double GetNewWeight(double d)
        {
            double w = d * WeightMult;
            return w > 2 ? w : 2;
        }
        private string CreateNodeDataPoint(CellPool pool)
        {
            (double origX, double origY) = PoolCoordinates[pool.ID];
            (double newX, double newY) = (origX * XMult + XOffset, origY * -1 * YMult + YOffset);
            return $"{{\"id\":\"{pool.ID}\",\"g\":\"{pool.CellGroup}\",x:{newX:0.##},y:{newY:0.##} }}";
        }

        private List<string> CreatePoolNodes(List<CellPool> pools, int width, int height)
        {
            PoolCoordinates = new();
            XMult = 1;
            YMult = 1;
            XOffset = 0;
            YOffset = 0;

            pools.ForEach(GetNewCoordinates);
            Dictionary<string, (double, double)> spreadedPools = new();

            Dictionary<string, (double, double)> posPools = PoolCoordinates
                .Where(v => v.Value.Item1 >= 0)
                .OrderBy(v => Math.Abs(v.Value.Item1)).ToDictionary(t => t.Key, t => t.Value);
            Dictionary<string, (double, double)> negPools = PoolCoordinates
                .Where(v => v.Value.Item1 < 0)
                .OrderBy(v => Math.Abs(v.Value.Item1)).ToDictionary(t => t.Key, t => t.Value);
            double inc, curX, curY;

            if (posPools.Any())
            {
                XMin = posPools.Min(v => Math.Abs(v.Value.Item1));
                XMax = posPools.Max(v => Math.Abs(v.Value.Item1));
                inc = (XMax - XMin) / (posPools.Count - 1);
                if (Math.Abs(inc) < 1)
                    inc /= Math.Abs(inc);
                curX = XMin;
                foreach (var v in posPools)
                {
                    if (Math.Abs(v.Value.Item1) >= Math.Abs(curX))
                        curX = v.Value.Item1;
                    spreadedPools.Add(v.Key, (curX, v.Value.Item2));
                    curX += inc;
                }
            }
            if (negPools.Any())
            {
                XMin = negPools.Min(v => Math.Abs(v.Value.Item1));
                XMax = negPools.Max(v => Math.Abs(v.Value.Item1));
                inc = (XMax - XMin) / (posPools.Count - 1);
                if (Math.Abs(inc) < 1)
                    inc /= Math.Abs(inc);
                curX = XMin;
                foreach (var v in negPools)
                {
                    if (Math.Abs(v.Value.Item1) >= Math.Abs(curX))
                        curX = v.Value.Item1;
                    spreadedPools.Add(v.Key, (curX, v.Value.Item2));
                    curX += inc;
                }
            }


            posPools = spreadedPools
                .Where(v => v.Value.Item2 >= 0)
                .OrderBy(v => Math.Abs(v.Value.Item2)).ToDictionary(t => t.Key, t => t.Value);
            negPools = spreadedPools
                .Where(v => v.Value.Item2 < 0)
                .OrderBy(v => Math.Abs(v.Value.Item2)).ToDictionary(t => t.Key, t => t.Value);
            spreadedPools.Clear();

            if (posPools.Any())
            {
                YMin = posPools.Min(v => Math.Abs(v.Value.Item2));
                YMax = posPools.Max(v => Math.Abs(v.Value.Item2));
                inc = (YMax - YMin) / (posPools.Count - 1);
                if (Math.Abs(inc) < 1)
                    inc /= Math.Abs(inc);
                curY = YMin;
                foreach (var v in posPools)
                {
                    if (Math.Abs(v.Value.Item2) >= Math.Abs(curY))
                        curY = v.Value.Item2;
                    spreadedPools.Add(v.Key, (v.Value.Item1, curY));
                    curY += inc;
                }
            }

            if (negPools.Any())
            {


                YMin = PoolCoordinates.Min(v => Math.Abs(v.Value.Item2));
                YMax = PoolCoordinates.Max(v => Math.Abs(v.Value.Item2));
                inc = (YMax - YMin) / (PoolCoordinates.Count - 1);
                if (Math.Abs(inc) < 1)
                    inc /= Math.Abs(inc);

                curY = YMin;
                foreach (var v in negPools)
                {
                    if (Math.Abs(v.Value.Item1) >= Math.Abs(curY))
                        curY = v.Value.Item1;
                    spreadedPools.Add(v.Key, (v.Value.Item1, curY));
                    curY += inc;
                }
            }

            PoolCoordinates = spreadedPools;

            XMin = PoolCoordinates.Min(v => v.Value.Item1);
            XMax = PoolCoordinates.Max(v => v.Value.Item1);
            YMin = PoolCoordinates.Min(v => v.Value.Item2);
            YMax = PoolCoordinates.Max(v => v.Value.Item2);

            if (XMax > XMin)
            {
                XMult = width / (XMax - XMin) / 5;
                XOffset = 10 * XMult;
            }
            if (YMax > YMin)
            {
                YMult = height / (YMax - YMin) / 2;
                YOffset = 25 * YMult;
            }
            List<string> nodes = new();
            pools.ForEach(pool => nodes.Add(CreateNodeDataPoint(pool)));
            return nodes;
        }

        public string Create2DModel(RunningModel model, List<CellPool> pools, int width, int height)
        {
            string title = model.ModelName + " 2D Model";

            StringBuilder html = new(ReadEmbeddedText("SiliFish.Resources.2DModel.html"));

            html.Replace("__STYLE_SHEET__", ReadEmbeddedText("SiliFish.Resources.StyleSheet.css"));

            if (Util.CheckOnlineStatus())
            {
                html.Replace("__OFFLINE_2D_SCRIPT__", "");
                html.Replace("__ONLINE_2D_SCRIPT__", "<script src=\"https://unpkg.com/force-graph\"></script>" +
                    "<script src=\"https://unpkg.com/three\"></script>");
            }
            else
            {
                html.Replace("__OFFLINE_2D_SCRIPT__", ReadEmbeddedText("SiliFish.Resources.force-graph.min.js") +
                    ReadEmbeddedText("SiliFish.Resources.three.js"));
                html.Replace("__ONLINE_2D_SCRIPT__", "");
            }

            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            html.Replace("__LEFT_HEADER__", HttpUtility.HtmlEncode(title + " - Gap Jnc"));
            html.Replace("__RIGHT_HEADER__", HttpUtility.HtmlEncode(title + " - Chemical Jnc"));
            List<string> nodes = CreatePoolNodes(pools, width, height);


            html.Replace("__POOLS__", string.Join(",", nodes.Where(s => !string.IsNullOrEmpty(s))));

            List<InterPool> gapInterPools = model.GapPoolConnections;
            List<InterPool> chemInterPools = model.ChemPoolConnections;

            int CountMax = 0;
            if (gapInterPools.Any())
                CountMax = gapInterPools.Max(ip => ip.CountJunctions);
            if (chemInterPools.Any())
                CountMax = Math.Max(CountMax, chemInterPools.Max(ip => ip.CountJunctions));
            WeightMult = 5 / CountMax;

            List<string> gapLinks = new();
            gapInterPools.ForEach(con => gapLinks.Add(CreateLinkDataPoint(con)));
            html.Replace("__GAP_LINKS__", string.Join(",", gapLinks.Where(s => !String.IsNullOrEmpty(s))));

            List<string> chemLinks = new();
            chemInterPools.ForEach(con => chemLinks.Add(CreateLinkDataPoint(con)));
            html.Replace("__CHEM_LINKS__", string.Join(",", chemLinks.Where(s => !String.IsNullOrEmpty(s))));
            
            List<string> colors = new();
            pools.ForEach(pool => colors.Add($"\"{pool.CellGroup}\": {pool.Color.ToRGBQuoted()}"));
            html.Replace("__COLOR_SET__", string.Join(",", colors.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            //FUTURE_IMPROVEMENT custom shapes
            List<string> shapes = new();
            pools.ForEach(pool => shapes.Add($"\"{pool.CellGroup}\": new THREE.SphereGeometry(5)"));
            html.Replace("__SHAPE_SET__", string.Join(",", shapes.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            return html.ToString();
        }

    }
}

