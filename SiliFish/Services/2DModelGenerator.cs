using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;

namespace SiliFish.Services
{
    public class TwoDModelGenerator : VisualsGenerator
    {
        double XMult, YMult;
        double XMin, YMin;
        double XOffset, YOffset;
        double WeightMax;
        double WeightMult;
        List<double> leftX = new(); //keeps a list of x values for the left and right halves
        List<double> rightX = new(); //keeps a list of x values for the left and right halves

        private string CreateLinkDataPoint(InterPool interPool)
        {
            string curvInfo = interPool.poolSource.ID == interPool.poolTarget.ID ? ",curv: 0.7" : "";
            string link = $"{{\"source\":\"{interPool.poolSource.ID}\"," +
                $"\"target\":\"{interPool.poolTarget.ID}\"," +
                $"\"value\":{GetNewWeight(interPool.reach.Weight):0.######}," +
                $"\"conductance\":{interPool.reach.Weight:0.######} " +
                $"{curvInfo} }}";
            ;
            return link;
        }

        private (double, double) GetNewCoordinates(CellPool pool)
        {
            double x = pool.XAvg();
            int y = (int)pool.YAvg();
            if (y < 0)
            {
                while (leftX.Any(xi => Math.Abs(xi - x) < XMult))
                {
                    x += XMult/10;
                }
                leftX.Add(x);
            }
            else
            {
                while (rightX.Any(xi => Math.Abs(xi - x) < XMult))
                {
                    x += XMult/10;
                }
                rightX.Add(x);
            }
            double newX = x * XMult + XOffset;
            double newY = (pool.YAvg() + pool.ZAvg())/2 * YMult + YOffset; 
            return (newX, newY);
        }
        private double GetNewWeight(double d)
        {
            double w = d * WeightMult;
            return w > 2 ? w : 2;
        }
        private string CreateNodeDataPoint(CellPool pool)
        {
            (double newX, double newY) = GetNewCoordinates(pool);
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
            
            ((XMin, double maxX), (YMin, double maxY), (_,_), int YRange1D) = model.GetSpatialRange();

            XMult = width / (maxX - XMin) / 5;
            YMult = height / Math.Max(2 * YRange1D, (maxY - YMin)) / 2;
            XOffset = 0;
            YOffset = 0;

            (_, WeightMax) = model.GetConnectionRange();
            WeightMult = 5 / WeightMax;

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
            pools.ForEach(pool => colors.Add($"\"{pool.CellGroup}\": \'{pool.Color.ToRGB()}\'"));
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

