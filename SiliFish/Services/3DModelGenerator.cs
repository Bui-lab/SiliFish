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

namespace SiliFish.Services
{
    public class ThreeDModelGenerator : VisualsGenerator
    {
        bool SingleDimension = false;
        double XYZMult;
        double XMin, YMin, ZMin;
        double XOffset;
        double WeightMax;
        double WeightMult;
        private string CreateLinkDataPoint(GapJunction jnc)
        {
            string curvInfo = jnc.Cell1.ID == jnc.Cell2.ID ? ",curv: 0.3" : "";
            return $"{{\"source\":\"{jnc.Cell1.ID}\"," +
                $"\"target\":\"{jnc.Cell2.ID}\"," +
                $"\"type\":\"gap\"," +
                $"\"value\":{GetNewWeight(jnc.Conductance):0.###}," +
                $"\"conductance\":{jnc.Conductance:0.######}" +
                $"{curvInfo} }}";
        }
        private string CreateLinkDataPoint(ChemicalSynapse jnc)
        {
            string curvInfo = jnc.PreNeuron.ID == jnc.PostCell.ID ? ",curv: 0.3" : "";
            return $"{{\"source\":\"{jnc.PreNeuron.ID}\"," +
                $"\"target\":\"{jnc.PostCell.ID}\"," +
                $"\"type\":\"chem\"," +
                $"\"value\":{GetNewWeight(jnc.Conductance):0.###}," +
                $"\"conductance\":{jnc.Conductance:0.######}" +
                $"{curvInfo} }}";
        }
        private (double, double, double) GetNewCoordinates(double x, double y, double z, int columnIndex2D)
        {
            //to be consistent with how x-y-z coordinates are used: flip y and z, and negate both y and z 
            //y is negated because closer to us is left, minus
            //z is negated because more dorsal is considered as 0
            double newX = (x - XMin) * XYZMult - XOffset;
            double newY = SingleDimension ? (y * columnIndex2D) * XYZMult:
                (y - YMin) * XYZMult;
            double newZ = (z - ZMin) * XYZMult;
            return (newX, -newZ, -newY);
        }
        private double GetNewWeight(double d)
        {
            return d * WeightMult;
        }
        private string CreateNodeDataPoint(Cell cell)
        {
            (double newX, double newY, double newZ) = GetNewCoordinates(cell.X, cell.Y, cell.Z, cell.CellPool.ColumnIndex2D);
            return $"{{\"id\":\"{cell.ID}\",\"g\":\"{cell.CellGroup}\",\"s\":{cell.Somite},\"crd\":\"{cell.coordinate}\",fx:{newX:0.##},fy:{newY:0.##},fz:{newZ:0.##}  }}";
        }
        private string CreateNodeDataPoints(CellPool pool, int minSomite, int maxSomite)
        {
            List<string> nodes = new();
            List<Cell> cells = pool.GetCells().Where(c => c.Somite >= minSomite && c.Somite <= maxSomite).ToList();
            foreach (Cell cell in cells)
                nodes.Add(CreateNodeDataPoint(cell));
            return string.Join(",", nodes);
        }

        private string CreateLinkDataPoints(Cell cell, bool gap, bool chem, int minSomite, int maxSomite)
        {
            List<string> links = new();
            if (cell is Neuron neuron)
            {
                if (gap)
                    foreach (GapJunction jnc in neuron.GapJunctions
                            .Where(j => j.Cell2 == cell && j.Cell1.Somite >= minSomite && j.Cell1.Somite <= maxSomite))
                        links.Add(CreateLinkDataPoint(jnc));
                if (chem)
                    foreach (ChemicalSynapse jnc in neuron.Terminals
                            .Where(syn => syn.PostCell.Somite >= minSomite && syn.PostCell.Somite <= maxSomite))
                        links.Add(CreateLinkDataPoint(jnc));
            }
            else if (chem && cell is MuscleCell muscle)
            {
                foreach (ChemicalSynapse jnc in muscle.EndPlates)
                    links.Add(CreateLinkDataPoint(jnc));
            }
            return string.Join(",", links);
        }
        private string CreateLinkDataPoints(CellPool pool, bool gap, bool chem, int minSomite, int maxSomite)
        {
            List<string> links = new();
            List<Cell> cells = pool.GetCells().Where(c => c.Somite >= minSomite && c.Somite <= maxSomite).ToList();
            foreach (Cell cell in cells)
            {
                string cellLinks = CreateLinkDataPoints(cell, gap, chem, minSomite, maxSomite);
                if (!string.IsNullOrEmpty(cellLinks))
                    links.Add(cellLinks);
            }
            return string.Join(",", links);
        }

        public string Create3DModel(bool saveFile, RunningModel model, List<CellPool> pools, string somiteRange, bool showGap, bool showChem)
        {
            StringBuilder html = new(ReadEmbeddedText("SiliFish.Resources.3DModel.html"));

            string filename = saveFile ? model.ModelName + "Model.html" : "";
            string title = model.ModelName + " 3D Model";

            html.Replace("__STYLE_SHEET__", ReadEmbeddedText("SiliFish.Resources.StyleSheet.css"));
            html.Replace("__SHOW_GAP__", showGap.ToString().ToLower());
            html.Replace("__SHOW_CHEM__", showChem.ToString().ToLower());

            if (Util.CheckOnlineStatus())
            {
                html.Replace("__OFFLINE_3D_SCRIPT__", "");
                html.Replace("__ONLINE_3D_SCRIPT__", "<script src=\"https://unpkg.com/3d-force-graph@1\"></script>" +
                    "<script src=\"https://unpkg.com/d3-dsv\"></script>" +
                    "<script src=\"https://unpkg.com/three\"></script>");
            }
            else
            {
                html.Replace("__OFFLINE_3D_SCRIPT__", ReadEmbeddedText("SiliFish.Resources.3d-force-graph.min.js") +
                    ReadEmbeddedText("SiliFish.Resources.d3-dsv.min.js") +
                    ReadEmbeddedText("SiliFish.Resources.three.js"));
                html.Replace("__ONLINE_3D_SCRIPT__", "");
            }

            html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));
            
            html.Replace("__LEFT_HEADER__", HttpUtility.HtmlEncode(title));
            
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
            XOffset = width / 2; //The center of the window is 0 - so half width is removed from all X values

            int numOfConnections = model.GetNumberOfConnections();
            double maxjncsize = 0.03; // numOfConnections > 0 ? XYZMult * range / (100 * numOfConnections) : 1;
            (_, WeightMax) = model.GetConnectionRange();
            WeightMult = maxjncsize / WeightMax;

            ModelDimensions MD = model.ModelDimensions;
            if (somiteRange.StartsWith("All"))
                html.Replace("__SOMITES__", "");
            else
            {
                List<int> somites= Util.ParseRange(somiteRange, 1, MD.NumberOfSomites);
                html.Replace("__SOMITES__", String.Join(',', somites));
            }
            int numOfCells = model.GetNumberOfCells();
            double nodesize = numOfCells > 0 ?
                (zRange < 0.1 ?//No z axis
                Math.Sqrt(xRange * yRange / (30 * numOfCells)) :
                Math.Pow(xRange * yRange * zRange / (160 * numOfCells), 0.33))
                : 0; //~40 times the nodes to fit in the space
            nodesize *= XYZMult;

            List<string> nodes = new();
            pools.ForEach(pool => nodes.Add(CreateNodeDataPoints(pool, -1, MD.NumberOfSomites)));
            html.Replace("__NODES__", string.Join(",", nodes.Where(s => !string.IsNullOrEmpty(s))));
            html.Replace("__NODE_SIZE__", nodesize.ToString("0.##"));

            
            List<string> gapChemLinks = new();
            pools.ForEach(pool => gapChemLinks.Add(CreateLinkDataPoints(pool, gap: true, chem: true, -1, MD.NumberOfSomites)));
            html.Replace("__GAP_CHEM_LINKS__", string.Join(",", gapChemLinks.Where(s => !String.IsNullOrEmpty(s))));
            
            double spinalposX = MD.SupraSpinalRostralCaudalDistance;
            double spinalposY = MD.SpinalBodyPosition + MD.SpinalDorsalVentralDistance / 2;
            double spinalposZ = 0;
            double spinallength = MD.SpinalRostralCaudalDistance;
            (double newX, double newY, double newZ) = GetNewCoordinates(spinalposX, spinalposZ, spinalposY, 0);
            (double newX2, newY, newZ) = GetNewCoordinates(spinallength + spinalposX, spinalposZ, spinalposY, 0);
            html.Replace("__SPINE_X__", newX.ToString());
            html.Replace("__SPINE_Y__", newY.ToString());
            html.Replace("__SPINE_Z__", newZ.ToString());
            html.Replace("__SPINE_LENGTH__", newX2.ToString());

            double brainLength = spinalposX;
            double brainHeight = MD.SupraSpinalDorsalVentralDistance;
            double brainWidth = MD.SupraSpinalMedialLateralDistance;
            html.Replace("__BRAIN_WIDTH__", (brainLength * XYZMult).ToString());
            html.Replace("__BRAIN_HEIGHT__", (brainHeight * XYZMult).ToString());
            html.Replace("__BRAIN_DEPTH__", (brainWidth * XYZMult).ToString());

            string lateralHeadStr = Convert.ToBase64String(ReadEmbeddedBinary("SiliFish.Resources.LateralHead.png"));
            string lateralHead = $"data:image/png;base64,{lateralHeadStr}";
            html.Replace("__LATERAL_HEAD__", lateralHead);

            string dorsalHeadStr = Convert.ToBase64String(ReadEmbeddedBinary("SiliFish.Resources.DorsalHead.png"));
            string dorsalHead = $"data:image/png;base64,{dorsalHeadStr}";
            html.Replace("__DORSAL_HEAD__", dorsalHead);

            List<string> colors = new();
            pools.ForEach(pool => colors.Add($"\"{pool.CellGroup}\": {pool.Color.ToRGBQuoted()}"));
            html.Replace("__COLOR_SET__", string.Join(",", colors.Distinct().Where(s => !String.IsNullOrEmpty(s))));

            if (!string.IsNullOrEmpty(filename))
                File.WriteAllText(filename, html.ToString());
            return html.ToString();
        }
    }
}

