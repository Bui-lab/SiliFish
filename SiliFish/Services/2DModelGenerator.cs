using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SiliFish.Extensions;
using SiliFish.ModelUnits;

namespace SiliFish.Services
{
    internal class TwoDModelGenerator : VisualsGenerator
    {
        double XMult, YMult;
        double XMin, YMin;
        double XOffset, YOffset;
        double WeightMax;
        double WeightMult;
        private string CreateLinkDataPoint(InterPool interPool)
        {
            return $"{{\"source\":\"{interPool.poolSource.ID}\",\"target\":\"{interPool.poolTarget.ID}\",\"value\":{GetNewWeight(interPool.reach.Weight):0.######},\"conductance\":{interPool.reach.Weight:0.######} }}";
        }
        private (double, double) GetNewCoordinates(CellPool pool)
        {
            (double r1, _) = pool.XRange();
            double newX = (r1 - XMin) * XMult - XOffset;
            r1 = pool.PositionLeftRight == SagittalPlane.Left ? -1 * pool.columnIndex2D : pool.columnIndex2D;
            double newY = (r1 - YMin) * YMult - YOffset;
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


        public string Create2DModel(string filename, string title, SwimmingModel model, List<CellPool> pools)
        {
            StringBuilder html = new(ReadEmbeddedResource("SiliFish.Resources.2DModel.html"));

            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            html.Replace("__LEFT_HEADER__", HttpUtility.HtmlEncode(title + " - Gap Jnc"));
            html.Replace("__RIGHT_HEADER__", HttpUtility.HtmlEncode(title + " - Chemical Jnc"));
            
            ((XMin, double maxX), (YMin, double maxY), (_,_), int YRange1D) = model.GetSpatialRange();
            XMult = 200 / (maxX - XMin);
            YMult = 100 / (maxY - YMin);
            XOffset = 100;
            YOffset = 50;
            if (YMin == -1 && maxY == 1)
            {
                //TODO plots only cell pools, put back single cells in two D ? SingleDimension = true;
                YMult = 100 / YRange1D;
            }
            (_, WeightMax) = model.GetConnectionRange();
            WeightMult = 5 / WeightMax;

            List<string> nodes = new();
            pools.ForEach(pool => nodes.Add(CreateNodeDataPoint(pool)));
            html.Replace("__POOLS__", string.Join(",", nodes.Where(s => !string.IsNullOrEmpty(s))));

            List<string> gapLinks = new();
            model.GetGapPoolConnections.ForEach(con => gapLinks.Add(CreateLinkDataPoint(con)));
            html.Replace("__GAP_LINKS__", string.Join(",", gapLinks.Where(s => !String.IsNullOrEmpty(s))));

            List<string> chemLinks = new();
            model.GetChemPoolConnections.ForEach(con => chemLinks.Add(CreateLinkDataPoint(con)));
            html.Replace("__CHEM_LINKS__", string.Join(",", chemLinks.Where(s => !String.IsNullOrEmpty(s))));

            List<string> colors = new();
            pools.ForEach(pool => colors.Add($"\"{pool.CellGroup}\": new THREE.MeshBasicMaterial({{color:{pool.Color.ToHex()}}})"));
            html.Replace("__COLOR_SET__", string.Join(",", colors.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            //TODO custom shapes
            List<string> shapes = new();
            pools.ForEach(pool => shapes.Add($"\"{pool.CellGroup}\": new THREE.SphereGeometry(5)"));
            html.Replace("__SHAPE_SET__", string.Join(",", shapes.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            if (!string.IsNullOrEmpty(filename))
                File.WriteAllText(filename, html.ToString());
            return html.ToString();
        }
    }
}

