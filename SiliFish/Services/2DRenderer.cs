﻿using SiliFish.Extensions;
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
using SiliFish.Definitions;

namespace SiliFish.Services
{
    public class TwoDRenderer : EmbeddedResourceReader
    {
        private const string Resource = "SiliFish.Resources.2DRender.html";
        double XMult, YMult;
        double XMin = double.MaxValue, YMin = double.MaxValue;
        double XMax = double.MinValue, YMax = double.MinValue;
        double XOffset, YOffset;
        double WeightMult;
        Dictionary<string, (double, double)> PoolCoordinates;
        private string CreateLinkDataPoint(InterPool interPool, bool gap)
        {
            string curvInfo = interPool.SourcePool == interPool.TargetPool ? ",curv: 0.7" : "";
            string conductance = interPool.MinConductance == interPool.MaxConductance ?
                interPool.MinConductance.ToString("0.######") :
                $"{interPool.MinConductance:0.######} -  {interPool.MaxConductance:0.######}";
            string link = $"{{\"source\":\"{interPool.SourcePool}\"," +
                $"\"target\":\"{interPool.TargetPool}\"," +
                $"\"type\":" +( gap?"\"gap\",":"\"chem\",")+
                $"\"value\":{GetNewWeight(interPool.CountJunctions):0.######}," +
                $"\"conductance\":\"{conductance}\"" +
                $"{curvInfo} }}";
            return link;
        }

        /// <summary>
        ///the x coordinate corresponds to y in the model (medial/lateral axis)
        ///the y coordinate corresponds to z in the model (dorsal/ventral axis)
        /// </summary>
        private void GetNewCoordinates(CellPool pool)
        {
            if (!PoolCoordinates.ContainsKey(pool.CellGroup))
            {
                (double _, double y, double z) = pool.XYZMiddle();
                if (pool.PositionLeftRight == SagittalPlane.Left)
                    y *= -1;
                PoolCoordinates.Add(pool.CellGroup, (y, z));
            }
        }
        private double GetNewWeight(double d)
        {
            double w = d * WeightMult;
            return w > 2 ? w : 2;
        }
        private string CreateNodeDataPoint(CellPool pool)
        {
            (double origX, double origY) = PoolCoordinates[pool.CellGroup];
            if (pool.PositionLeftRight == SagittalPlane.Left)
                origX *= -1;
            (double newX, double newY) = (origX * XMult + XOffset, origY * -1 * YMult + YOffset);
            return $"{{\"id\":\"{pool.ID}\",\"g\":\"{pool.CellGroup}\",x:{newX.ToString(GlobalSettings.CoordinateFormat)},y:{newY.ToString(GlobalSettings.CoordinateFormat)} }}";
        }

        private Dictionary<string, (double, double)> SpreadPools(Dictionary<string, (double, double)> pools, bool item2 = false, bool neg = false)
        {
            Dictionary<string, (double, double)> spreadedPools = new();
            if (pools.Any())
            {
                double xyMin = item2? pools.Min(v => v.Value.Item2): pools.Min(v => v.Value.Item1);
                double xyMax = item2? pools.Max(v => v.Value.Item2): pools.Max(v => v.Value.Item1);
                double inc = (xyMax - xyMin) / (pools.Count - 1);
                if (inc < 1)
                    inc = 1;
                double curXY = xyMin;
                foreach (var v in pools)
                {
                    if (!item2)
                    {
                        if (Math.Abs(v.Value.Item1) >= Math.Abs(curXY))
                            curXY = v.Value.Item1;
                        spreadedPools.Add(v.Key, (curXY, v.Value.Item2));
                        curXY += inc * (neg?-1:1);
                    }
                    else
                    {
                        if (Math.Abs(v.Value.Item2) >= Math.Abs(curXY))
                            curXY = v.Value.Item2;
                        spreadedPools.Add(v.Key, (v.Value.Item1, curXY));
                        curXY += inc * (neg ? -1 : 1);
                    }
                }
            }
            return spreadedPools;
        }
        private List<string> CreatePoolNodes(List<CellPool> pools, int width, int height)
        {
            PoolCoordinates = new();
            XMult = 1;
            YMult = 1;
            XOffset = 0;
            YOffset = 0;

            pools.ForEach(GetNewCoordinates);

            Dictionary<string, (double, double)> posPools = PoolCoordinates
                .Where(v => v.Value.Item1 >= 0)
                .OrderBy(v => Math.Abs(v.Value.Item1)).ToDictionary(t => t.Key, t => t.Value);
            Dictionary<string, (double, double)> negPools = PoolCoordinates
                .Where(v => v.Value.Item1 < 0)
                .OrderBy(v => Math.Abs(v.Value.Item1)).ToDictionary(t => t.Key, t => t.Value);

            Dictionary<string, (double, double)> spreadedPools = SpreadPools(posPools);
            Dictionary<string, (double, double)> spreadedPools2 = SpreadPools(negPools, false, true);

            spreadedPools = spreadedPools.Concat(spreadedPools2).ToDictionary(x => x.Key, x => x.Value);
            posPools = spreadedPools
                .Where(v => v.Value.Item2 >= 0)
                .OrderBy(v => Math.Abs(v.Value.Item2)).ToDictionary(t => t.Key, t => t.Value);
            negPools = spreadedPools
                .Where(v => v.Value.Item2 < 0)
                .OrderBy(v => Math.Abs(v.Value.Item2)).ToDictionary(t => t.Key, t => t.Value);

            spreadedPools = SpreadPools(posPools, true);
            spreadedPools2 = SpreadPools(negPools, true, true);
            PoolCoordinates = spreadedPools.Concat(spreadedPools2).ToDictionary(x => x.Key, x => x.Value);

            XMax = PoolCoordinates.Max(v => v.Value.Item1);
            XMin = -1 * XMax;
            YMin = PoolCoordinates.Min(v => v.Value.Item2);
            YMax = PoolCoordinates.Max(v => v.Value.Item2);

            if (XMax > XMin)
            {
                XMult = width / (XMax - XMin) / 2;
                XOffset = -(XMax + XMin) * XMult / 2;
            }
            if (YMax > YMin)
            {
                YMult = height / (YMax - YMin) / 2;
                YOffset = (YMax + YMin) * YMult / 2;
            }
            List<string> nodes = new();
            pools.ForEach(pool => nodes.Add(CreateNodeDataPoint(pool)));
            return nodes;
        }

        public string Create2DRendering(RunningModel model, List<CellPool> pools, int width, int height, bool showGap, bool showChem)
        {
            string title = model.ModelName + " 2D Rendering";

            StringBuilder html = new(ReadEmbeddedText(Resource));

            html.Replace("__STYLE_SHEET__", ReadEmbeddedText("SiliFish.Resources.StyleSheet.css"));
            html.Replace("__SHOW_GAP__", showGap.ToString().ToLower());
            html.Replace("__SHOW_CHEM__", showChem.ToString().ToLower());

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
            html.Replace("__LEFT_HEADER__", HttpUtility.HtmlEncode(title));

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

            List<string> gapChemLinks = new();
            gapInterPools.ForEach(con => gapChemLinks.Add(CreateLinkDataPoint(con, true)));
            chemInterPools.ForEach(con => gapChemLinks.Add(CreateLinkDataPoint(con, false)));
            html.Replace("__GAP_CHEM_LINKS__", string.Join(",", gapChemLinks.Where(s => !String.IsNullOrEmpty(s))));

            List<string> colors = new();
            pools.ForEach(pool => colors.Add($"\"{pool.CellGroup}\": {pool.Color.ToRGBQuoted()}"));
            html.Replace("__COLOR_SET__", string.Join(",", colors.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            List<string> shapes = new();
            pools.ForEach(pool => shapes.Add($"\"{pool.CellGroup}\": new THREE.SphereGeometry(5)"));
            html.Replace("__SHAPE_SET__", string.Join(",", shapes.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            return html.ToString();
        }

    }
}

