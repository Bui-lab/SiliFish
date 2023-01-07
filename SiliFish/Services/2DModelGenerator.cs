using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SiliFish.Services
{
    public class TwoDModelGenerator : VisualsGenerator
    {
        double XMult, YMult;
        double XMin = double.MaxValue, YMin = double.MaxValue;
        double XMax = double.MinValue, YMax = double.MinValue;
        double XOffset, YOffset;
        double WeightMax;
        double WeightMult;
        Dictionary<string, (double, double)> PoolCoordinates;
        private string CreateLinkDataPoint(InterPool interPool)
        {
            string curvInfo = interPool.SourcePool.ID == interPool.TargetPool.ID ? ",curv: 0.7" : "";
            string link = $"{{\"source\":\"{interPool.SourcePool.ID}\"," +
                $"\"target\":\"{interPool.TargetPool.ID}\"," +
                $"\"value\":{GetNewWeight(interPool.Reach.Weight):0.######}," +
                $"\"conductance\":{interPool.Reach.Weight:0.######} " +
                $"{curvInfo} }}";
            ;
            return link;
        }

        private void GetNewCoordinates(CellPool pool)
        {
            (double x, double y, double _) = pool.XYZMiddle();
            if (x < XMin) XMin = x;
            if (y < YMin) YMin = y;
            if (x > XMax) XMax = x;
            if (y > YMax) YMax = y;
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

        public string Create2DModel(bool saveFile, SwimmingModel model, List<CellPool> pools, int width, int height)
        {
            string filename = saveFile ? model.ModelName + "Model.html" : "";
            string title = model.ModelName + " 2D Model";

            StringBuilder html = new(ReadEmbeddedResource("SiliFish.Resources.2DModel.html"));

            html.Replace("__STYLE_SHEET__", ReadEmbeddedResource("SiliFish.Resources.StyleSheet.css"));

            if (Util.CheckOnlineStatus())
            {
                html.Replace("__OFFLINE_2D_SCRIPT__", "");
                html.Replace("__ONLINE_2D_SCRIPT__", "<script src=\"https://unpkg.com/force-graph\"></script>" +
                    "<script src=\"https://unpkg.com/three\"></script>");
            }
            else
            {
                html.Replace("__OFFLINE_2D_SCRIPT__", ReadEmbeddedResource("SiliFish.Resources.force-graph.min.js") +
                    ReadEmbeddedResource("SiliFish.Resources.three.js"));
                html.Replace("__ONLINE_2D_SCRIPT__", "");
            }

            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            html.Replace("__LEFT_HEADER__", HttpUtility.HtmlEncode(title + " - Gap Jnc"));
            html.Replace("__RIGHT_HEADER__", HttpUtility.HtmlEncode(title + " - Chemical Jnc"));

            PoolCoordinates = new();
            XMult = 1;
            YMult = 1;
            XOffset = 0;
            YOffset = 0;

            (_, WeightMax) = model.GetConnectionRange();
            WeightMult = 5 / WeightMax;

            pools.ForEach(pool => GetNewCoordinates(pool));
            //XMin, YMax etc set during node creation
            if (XMax > XMin)
            {
                XMult = width / (XMax - XMin) / 5;
                XOffset = -XMin * XMult + 10;
            }
            if (YMax > YMin)
            {
                YMult = height / (YMax - YMin) / 2;
            }
            List<string> nodes = new();
            pools.ForEach(pool => nodes.Add(CreateNodeDataPoint(pool)));

            html.Replace("__POOLS__", string.Join(",", nodes.Where(s => !string.IsNullOrEmpty(s))));

            List<string> gapLinks = new();
            model.GapPoolConnections.ForEach(con => gapLinks.Add(CreateLinkDataPoint(con)));
            html.Replace("__GAP_LINKS__", string.Join(",", gapLinks.Where(s => !String.IsNullOrEmpty(s))));

            List<string> chemLinks = new();
            model.ChemPoolConnections.ForEach(con => chemLinks.Add(CreateLinkDataPoint(con)));
            html.Replace("__CHEM_LINKS__", string.Join(",", chemLinks.Where(s => !String.IsNullOrEmpty(s))));

            List<string> colors = new();
            pools.ForEach(pool => colors.Add($"\"{pool.CellGroup}\": {pool.Color.ToRGBQuoted()}"));
            html.Replace("__COLOR_SET__", string.Join(",", colors.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            //FUTURE_IMPROVEMENT custom shapes
            List<string> shapes = new();
            pools.ForEach(pool => shapes.Add($"\"{pool.CellGroup}\": new THREE.SphereGeometry(5)"));
            html.Replace("__SHAPE_SET__", string.Join(",", shapes.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            if (!string.IsNullOrEmpty(filename))
                File.WriteAllText(filename, html.ToString());
            return html.ToString();
        }

    }
}

