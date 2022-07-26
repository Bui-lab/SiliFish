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
    public class ThreeDModelGenerator : VisualsGenerator
    {
        bool SingleDimension = false;
        double XYZMult;
        double XMin, YMin, ZMin;
        double XOffset, YOffset, ZOffset;
        double WeightMax;
        double WeightMult;
        private string CreateLinkDataPoint(GapJunction jnc)
        {
            string curvInfo = jnc.Cell1.ID == jnc.Cell2.ID ? ",curv: 0.3" : "";
            return $"{{\"source\":\"{jnc.Cell1.ID}\"," +
                $"\"target\":\"{jnc.Cell2.ID}\"," +
                $"\"value\":{GetNewWeight(jnc.Conductance):0.##}," +
                $"\"conductance\":{jnc.Conductance:0.######}" +
                $"{curvInfo} }}";
        }
        private string CreateLinkDataPoint(ChemicalSynapse jnc)
        {
            string curvInfo = jnc.PreNeuron.ID == jnc.PostCell.ID ? ",curv: 0.3" : "";
            return $"{{\"source\":\"{jnc.PreNeuron.ID}\"," +
                $"\"target\":\"{jnc.PostCell.ID}\"," +
                $"\"value\":{GetNewWeight(jnc.Conductance):0.##}," +
                $"\"conductance\":{jnc.Conductance:0.######}" +
                $"{curvInfo} }}";
        }
        private (double, double, double) GetNewCoordinates(double x, double y, double z, int columnIndex2D)
        {
            //to be consistent with how x-y-z coordinates are used: flip y and z, and negate y
            double newX = (x - XMin) * XYZMult - XOffset;
            double newY = SingleDimension ? (y * columnIndex2D) * XYZMult - YOffset :
                (y - YMin) * XYZMult - YOffset;
            double newZ = ((z - ZMin) * XYZMult - ZOffset);
            return (newX, newZ, -newY);
        }
        private double GetNewWeight(double d)
        {
            double w = d * WeightMult;
            return w > 1 ? w : 1;
        }
        private string CreateNodeDataPoint(Cell cell)
        {
            (double newX, double newY, double newZ) = GetNewCoordinates(cell.X, cell.Y, cell.Z, cell.CellPool.columnIndex2D);
            return $"{{\"id\":\"{cell.ID}\",\"g\":\"{cell.CellGroup}\",\"crd\":\"{cell.coordinate}\",fx:{newX:0.##},fy:{newY:0.##},fz:{newZ:0.##}  }}";
        }
        private string CreateNodeDataPoints(CellPool pool)
        {
            List<string> nodes = new List<string>();
            foreach (Cell cell in pool.GetCells())
                nodes.Add(CreateNodeDataPoint(cell));
            return string.Join(",", nodes);
        }

        private string CreateLinkDataPoints(Cell cell, bool gap, bool chem)
        {
            List<string> links = new();
            if (cell is Neuron neuron)
            {
                if (gap)
                    foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell2 == cell))
                        links.Add(CreateLinkDataPoint(jnc));
                if (chem)
                    foreach (ChemicalSynapse jnc in neuron.Synapses)
                        links.Add(CreateLinkDataPoint(jnc));
            }
            else if (chem && cell is MuscleCell muscle)
            {
                foreach (ChemicalSynapse jnc in muscle.EndPlates)
                    links.Add(CreateLinkDataPoint(jnc));
            }
            return string.Join(",", links);
        }
        private string CreateLinkDataPoints(CellPool pool, bool gap, bool chem)
        {
            List<string> links = new();
            foreach (Cell cell in pool.GetCells())
            {
                string cellLinks = CreateLinkDataPoints(cell, gap, chem);
                if (!string.IsNullOrEmpty(cellLinks))
                    links.Add(cellLinks);
            }
            return string.Join(",", links);
        }


        //gap and chem are obsolete if singlePanel = false
        public string Create3DModel(bool saveFile, SwimmingModel model, List<CellPool> pools, bool singlePanel, bool gap, bool chem)
        {
            StringBuilder html = singlePanel? new(global::SiliFish.Services.VisualsGenerator.ReadEmbeddedResource("SiliFish.Resources.3DModelSinglePanel.html")):
                    new(global::SiliFish.Services.VisualsGenerator.ReadEmbeddedResource("SiliFish.Resources.3DModel.html"));

            string filename = saveFile ? model.ModelName + "Model.html" : "";
            string title = model.ModelName + " 3D Model";

            html.Replace("__STYLE_SHEET__", ReadEmbeddedResource("SiliFish.Resources.StyleSheet.css"));
            if (Util.CheckOnlineStatus())
            {
                html.Replace("__OFFLINE_3D_SCRIPT__", "");
                html.Replace("__ONLINE_3D_SCRIPT__", "<script src=\"https://unpkg.com/3d-force-graph@1\"></script>" +
                    "<script src=\"https://unpkg.com/d3-dsv\"></script>" +
                    "<script src=\"https://unpkg.com/three\"></script>");
            }
            else
            {
                html.Replace("__OFFLINE_3D_SCRIPT__", ReadEmbeddedResource("SiliFish.Resources.3d-force-graph.min.js") +
                    ReadEmbeddedResource("SiliFish.Resources.d3-dsv.min.js") +
                    ReadEmbeddedResource("SiliFish.Resources.three.js"));
                html.Replace("__ONLINE_3D_SCRIPT__", "");
            }

            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            if (!singlePanel)
            {
                html.Replace("__LEFT_HEADER__", HttpUtility.HtmlEncode(title + " - Gap Jnc"));
                html.Replace("__RIGHT_HEADER__", HttpUtility.HtmlEncode(title + " - Chem Jnc"));
            }
            else 
            {
                string s = gap && chem ? "Gap and Chem" : gap ? "Gap" : chem ? "Chem" : "No";
                html.Replace("__LEFT_HEADER__", HttpUtility.HtmlEncode(title + String.Format(" - {0} Jnc", s)));
            }
            ((XMin, double maxX), (YMin, double maxY), (ZMin, double maxZ), int YRange1D) = model.GetSpatialRange();

            double xRange = maxX - XMin;
            double yRange = maxY - YMin;
            double zRange = maxZ - ZMin;
            if (YMin == -1 && maxY == 1)
            {
                SingleDimension = true;
                yRange = 2 * YRange1D;
            }
            double range = Math.Max(xRange, Math.Max(yRange, zRange));
            int width = 400; 
            XYZMult = width / range;
            XOffset = singlePanel ? width/2 : width;
            YOffset = 0;
            ZOffset = 0;

            (_, WeightMax) = model.GetConnectionRange();
            WeightMult = 3 / WeightMax;

            List<string> nodes = new();
            pools.ForEach(pool => nodes.Add(CreateNodeDataPoints(pool)));
            html.Replace("__NODES__", string.Join(",", nodes.Where(s => !string.IsNullOrEmpty(s))));

            if (!singlePanel)
            {
                List<string> gapLinks = new();
                pools.ForEach(pool => gapLinks.Add(CreateLinkDataPoints(pool, gap: true, chem: false)));
                html.Replace("__GAP_LINKS__", string.Join(",", gapLinks.Where(s => !String.IsNullOrEmpty(s))));

                List<string> chemLinks = new();
                pools.ForEach(pool => chemLinks.Add(CreateLinkDataPoints(pool, gap: false, chem: true)));
                html.Replace("__CHEM_LINKS__", string.Join(",", chemLinks.Where(s => !String.IsNullOrEmpty(s))));
            }
            else
            {
                List<string> gapChemLinks = new();
                pools.ForEach(pool => gapChemLinks.Add(CreateLinkDataPoints(pool, gap: gap, chem: chem)));
                html.Replace("__GAP_CHEM_LINKS__", string.Join(",", gapChemLinks.Where(s => !String.IsNullOrEmpty(s))));
            }

            double spinalposY = model.SpinalBodyPosition + model.SpinalDorsalVentralDistance / 2;
            double spinalposZ = 0;
            double spinallag = Math.Max(model.SpinalRostralCaudalDistance, xRange);
            (double newX, double newY, double newZ) = GetNewCoordinates(-3, spinalposZ, spinalposY, 0);
            (double newX2, newY, newZ) = GetNewCoordinates(spinallag + 5, spinalposZ, spinalposY, 0);

            html.Replace("__SPINE_X__", newX.ToString());
            html.Replace("__SPINE_Y__", newY.ToString());
            html.Replace("__SPINE_Z__", newZ.ToString());
            html.Replace("__SPINE_LENGTH__", newX2.ToString());

            List<string> colors = new();
            pools.ForEach(pool => colors.Add($"\"{pool.CellGroup}\": \"{pool.Color.ToRGB()}\""));
            html.Replace("__COLOR_SET__", string.Join(",", colors.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            if (!string.IsNullOrEmpty(filename))
                File.WriteAllText(filename, html.ToString());
            return html.ToString();
        }
    }
}

