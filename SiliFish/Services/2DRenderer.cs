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
using SiliFish.Definitions;
using GeneticSharp;

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
        Dictionary<(string, string), int> LinkCounter =new();

        private string CreateLinkDataPoint(InterPool interPool)
        {
            string mode = interPool.Mode == CellOutputMode.Excitatory ? "exc" :
                interPool.Mode == CellOutputMode.Inhibitory ? "inh" :
                interPool.Mode == CellOutputMode.Electrical ? "gap" :
                interPool.Mode == CellOutputMode.Cholinergic ? "ACh" :
                "";
            string p1 = interPool.SourcePool;
            string p2 = interPool.TargetPool;
            if (string.Compare(p1, p2)<0)
                (p1, p2) = (p2, p1);
            double curv = interPool.SourcePool == interPool.TargetPool ? 0.7 : 0;
            if (LinkCounter.ContainsKey((p1, p2)))
            {
                curv += LinkCounter[(p1, p2)] * 0.1;
                LinkCounter[(p1, p2)]++;
            }
            else
                LinkCounter[(p1, p2)] = 1;

            string curvInfo = curv>0 ? $",curv: {curv}" : "";
            string conductance = interPool.MinConductance == interPool.MaxConductance ?
                interPool.MinConductance.ToString("0.######") :
                $"{interPool.MinConductance:0.######} -  {interPool.MaxConductance:0.######}";
            string link = $"{{\"source\":\"{interPool.SourcePool}\"," +
                $"\"target\":\"{interPool.TargetPool}\"," +
                $"\"type\":\"{mode}\"," +
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
                (double x, double y, _) = pool.XYZMiddle();
                if (pool.PositionLeftRight == SagittalPlane.Left)
                    y *= -1;
                PoolCoordinates.Add(pool.CellGroup, (pool.ColumnIndex2D * y, x));
            }
        }
        private double GetNewWeight(double d)
        {
            double w = d * WeightMult;
            return w > 2 ? w : 2;
        }

        private (string, string, string) CreateSpine()
        {
            (double newX, double newY) = (XOffset, YOffset);
            string head = $"{{\"id\":\"_h_\",\"g\":\"Spine\",x:{newX.ToString(GlobalSettings.CoordinateFormat)},y:{(-newY).ToString(GlobalSettings.CoordinateFormat)}}}";
            string tail = $"{{\"id\":\"_t_\",\"g\":\"Spine\",x:{newX.ToString(GlobalSettings.CoordinateFormat)},y:{newY.ToString(GlobalSettings.CoordinateFormat)} }}";
            string link = $"{{\"source\":\"_h_\",\"target\":\"_t_\",\"type\":\"spine\"}}";
            return (head, tail, link);
        }
        private string CreateNodeDataPoint(CellPool pool)
        {
            (double origX, double origY) = PoolCoordinates[pool.CellGroup];
            if (pool.PositionLeftRight == SagittalPlane.Left)
                origX *= -1;
            if (Math.Abs(origX) < 1)
                origX /= Math.Abs(origX);
            (double newX, double newY) = (origX * XMult + XOffset, origY * -1 * YMult + YOffset);
            return $"{{\"id\":\"{pool.ID}\",\"g\":\"{pool.CellGroup}\",x:{newX.ToString(GlobalSettings.CoordinateFormat)},y:{newY.ToString(GlobalSettings.CoordinateFormat)} }}";
        }

        private Dictionary<string, (double, double)> SpreadPools(Dictionary<string, (double, double)> pools, bool item2 = false, bool neg = false)
        {
            Dictionary<string, (double, double)> spreadedPools = new();
            if (pools.Any())
            {
                double xyMin = item2 ? pools.Min(v => v.Value.Item2) : pools.Min(v => v.Value.Item1);
                double xyMax = item2 ? pools.Max(v => v.Value.Item2) : pools.Max(v => v.Value.Item1);
                double inc = (xyMax - xyMin) / (pools.Count - 1);
                if (inc < 1)
                    inc = 1;
                double curXY = xyMin;
                if (item2) //sort the y coordinate - the supraspinal pools are already put to the rostral region
                {
                    Random r = new();
                    //randomize the pools with the same x axis value (medial lateral axis)
                    foreach (var v in pools.OrderBy(p => p.Value.Item2).ThenBy(p => r.Next()))
                    {
                        if (Math.Abs(v.Value.Item2) >= Math.Abs(curXY))
                            curXY = v.Value.Item2;
                        spreadedPools.Add(v.Key, (v.Value.Item1, curXY));
                        curXY += inc * (neg ? -1 : 1);
                    }
                }
                else
                {
                    foreach (var v in pools)
                    {
                        if (Math.Abs(v.Value.Item1) >= Math.Abs(curXY))
                            curXY = v.Value.Item1;
                        spreadedPools.Add(v.Key, (curXY, v.Value.Item2));
                        curXY += inc * (neg ? -1 : 1);
                    }
                }
            }
            return spreadedPools;
        }

        private List<string> CreatePoolNodes(List<CellPool> pools, int width, int height)
        {
            if (pools == null || pools.Count == 0)
                return null;
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

            //put the supraspinal pools to the top, and musculoskeletal to the lateral region.
            List<string> supraPools = pools.Where(cp => cp.BodyLocation == BodyLocation.SupraSpinal).Select(cp => cp.CellGroup).ToList();
            List<string> musclePools = pools.Where(cp => cp.BodyLocation == BodyLocation.MusculoSkeletal).Select(cp => cp.CellGroup).ToList();
            List<string> spinalPools = pools.Where(cp => cp.BodyLocation == BodyLocation.SpinalCord).Select(cp => cp.CellGroup).ToList();
            //double supraMinY = supraPools.Any() ? PoolCoordinates.Where(kvp => supraPools.Contains(kvp.Key)).Min(kvp => kvp.Value.Item2) : 0;
            double supraMaxY = supraPools.Any() ? PoolCoordinates.Where(kvp => supraPools.Contains(kvp.Key)).Max(kvp => kvp.Value.Item2) : 0;
            double muscleMinX = musclePools.Any() ? PoolCoordinates.Where(kvp => musclePools.Contains(kvp.Key)).Min(kvp => kvp.Value.Item1) : 0;
            //double muscleMaxX = musclePools.Any() ? PoolCoordinates.Where(kvp => musclePools.Contains(kvp.Key)).Max(kvp => kvp.Value.Item1):0;
            double muscleMinY = musclePools.Any() ? PoolCoordinates.Where(kvp => musclePools.Contains(kvp.Key)).Min(kvp => kvp.Value.Item2) : 0;
            double muscleMaxY = musclePools.Any() ? PoolCoordinates.Where(kvp => musclePools.Contains(kvp.Key)).Max(kvp => kvp.Value.Item2) : 0;
            double spinalMinX = spinalPools.Any() ? PoolCoordinates.Where(kvp => spinalPools.Contains(kvp.Key)).Min(kvp => kvp.Value.Item1) : 0;
            double spinalMaxX = spinalPools.Any() ? PoolCoordinates.Where(kvp => spinalPools.Contains(kvp.Key)).Max(kvp => kvp.Value.Item1) : 0;
            double spinalMinY = spinalPools.Any() ? PoolCoordinates.Where(kvp => spinalPools.Contains(kvp.Key)).Min(kvp => kvp.Value.Item2) : 0;
            double spinalMaxY = spinalPools.Any() ? PoolCoordinates.Where(kvp => spinalPools.Contains(kvp.Key)).Max(kvp => kvp.Value.Item2) : 0;
            double msMinY = Math.Min(spinalMinY, muscleMinY);
            if (supraPools.Any() && supraMaxY > msMinY)
            {
                double msMaxY = Math.Max(spinalMaxY, muscleMaxY);
                double bufferY = supraMaxY - msMinY + (msMaxY - msMinY) / 10;
                foreach (var pc in PoolCoordinates.Where(kvp => supraPools.Contains(kvp.Key)))
                {
                    if (posPools.TryGetValue(pc.Key, out (double, double) valueP))
                        posPools[pc.Key] = (valueP.Item1, valueP.Item2 - bufferY);
                    if (negPools.TryGetValue(pc.Key, out (double, double) valueN))
                        negPools[pc.Key] = (valueN.Item1, valueN.Item2 - bufferY);
                }
            }
            if (musclePools.Any() && spinalPools.Any() && muscleMinX < spinalMaxX)
            {
                double bufferX = spinalMaxX - muscleMinX + (spinalMaxX - spinalMinX);
                foreach (var pc in PoolCoordinates.Where(kvp => musclePools.Contains(kvp.Key)))
                {
                    if (posPools.TryGetValue(pc.Key, out (double, double) valueP))
                        posPools[pc.Key] = (valueP.Item1 + bufferX, valueP.Item2);
                    if (negPools.TryGetValue(pc.Key, out (double, double) valueN))
                        negPools[pc.Key] = (valueN.Item1 + bufferX, valueN.Item2);
                }
            }

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
                XMult = width / (XMax - XMin) / 1.2;
                XOffset = -(XMax + XMin) * XMult / 2;
            }
            if (YMax > YMin)
            {
                YMult = -height / (YMax - YMin) / 1.2;
                YOffset = (YMax + YMin) * YMult / 2;
            }
            List<string> nodes = new();
            pools.ForEach(pool => nodes.Add(CreateNodeDataPoint(pool)));

            return nodes;
        }

        public string Create2DRendering(RunningModel model, List<CellPool> pools, int width, int height, bool showGap, bool showChem)
        {
            if (pools == null || pools.Count == 0)
                return null;
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
                //TODO how to keep up to date? https://unpkg.com/three@0.152.2/build/three.module.js
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
            (string head, string tail, string spine) = CreateSpine();
            nodes.Add(head);
            nodes.Add(tail);
            html.Replace("__POOLS__", string.Join(",", nodes.Where(s => !string.IsNullOrEmpty(s))));
            html.Replace("__NODE_SIZE__", "10");

            List<InterPool> gapInterPools = model.GapPoolConnections.Where(gpc => pools.Any(p => p.ID == gpc.SourcePool) && pools.Any(p => p.ID == gpc.TargetPool)).ToList();
            List<InterPool> chemInterPools = model.ChemPoolConnections.Where(cpc => pools.Any(p => p.ID == cpc.SourcePool) && pools.Any(p => p.ID == cpc.TargetPool)).ToList();

            int CountMax = 0;
            if (gapInterPools.Any())
                CountMax = gapInterPools.Max(ip => ip.CountJunctions);
            if (chemInterPools.Any())
                CountMax = Math.Max(CountMax, chemInterPools.Max(ip => ip.CountJunctions));
            WeightMult = 5 / CountMax;

            LinkCounter.Clear();
            List<string> gapChemLinks = new();
            gapInterPools.ForEach(con => gapChemLinks.Add(CreateLinkDataPoint(con)));
            chemInterPools.ForEach(con => gapChemLinks.Add(CreateLinkDataPoint(con)));
            gapChemLinks.Add(spine);
            html.Replace("__GAP_CHEM_LINKS__", string.Join(",", gapChemLinks.Where(s => !string.IsNullOrEmpty(s))));

            List<string> colors = new();
            pools.ForEach(pool => colors.Add($"\"{pool.CellGroup}\": {pool.Color.ToRGBQuoted()}"));
            html.Replace("__COLOR_SET__", string.Join(",", colors.Distinct().Where(s => !string.IsNullOrEmpty(s))));

            List<string> shapes = new();
            pools.ForEach(pool => shapes.Add($"\"{pool.CellGroup}\": new THREE.SphereGeometry(5)"));
            html.Replace("__SHAPE_SET__", string.Join(",", shapes.Distinct().Where(s => !string.IsNullOrEmpty(s))));

            //Add spine
            ModelDimensions MD = model.ModelDimensions;
            double spinalposX = MD.SupraSpinalRostralCaudalDistance;
            double spinalposY = MD.SpinalBodyPosition + MD.SpinalDorsalVentralDistance / 2;
            double spinallength = MD.SpinalRostralCaudalDistance;
            (double newX, double newY) = (/*origX * XMult + */XOffset, /*origY * -1 * YMult + */YOffset);
            html.Replace("__SPINE_X__", newX.ToString());
            html.Replace("__SPINE_Y__", newY.ToString());
            html.Replace("__SPINE_LENGTH__", newY.ToString());
            return html.ToString();
        }

    }
}

