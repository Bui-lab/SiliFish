using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zebrafish.Helpers
{
    internal class OxyPlotHelper
    {
        //Plots incoming currents for a single cell
        //Color determined by the source cell
        //weight determined by the distance of cell sequence
        public static (Image, Image) CreateCurrentsPlot(string title, Cell cell, double[] Time, int tstart, int tend, int width = 1024, int height = 480, bool saveImg = false)
        {
            OxyPlot.PlotModel? modelSyn = null;
            OxyPlot.PlotModel modelGap = null;

            byte a = (byte)255;
            if (cell is Neuron neuron)
            {
                modelSyn = new() { Title = title + " Synaptic Current" };
                modelGap = new() { Title = title + " Gap Current" };
                (double minWeight, double maxWeight) = neuron.GetConnectionRange();
                double mult = (maxWeight - minWeight) / 55;
                foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell2 == cell))
                {
                    LineSeries ls = new LineSeries();
                    Color color = Const.CellColors[jnc.Cell1.CellGroup];
                    a = (byte)((jnc.Conductance - minWeight) * mult + 200);
                    ls.Color = OxyColor.FromAColor(a, OxyColor.FromRgb(color.R, color.G, color.B));
                    ls.Points.AddRange(Enumerable
                        .Range(tstart, tend - tstart + 1)
                        .Select(i => new DataPoint(Time[i], jnc.InputCurrent[i])));
                    modelGap.Series.Add(ls);
                }
                foreach (ChemicalJunction jnc in neuron.Synapses)
                {
                    LineSeries ls = new LineSeries();
                    Color color = Const.CellColors[jnc.PreNeuron.CellGroup];
                    a = (byte)((jnc.Weight - minWeight) * mult + 200);
                    ls.Color = OxyColor.FromAColor(a, OxyColor.FromRgb(color.R, color.G, color.B));
                    ls.Points.AddRange(Enumerable
                        .Range(tstart, tend - tstart + 1)
                        .Select(i => new DataPoint(Time[i], jnc.InputCurrent[i])));
                    modelSyn.Series.Add(ls);
                }
            }
            else if (cell is MuscleCell muscle)
            {
                modelSyn = new() { Title = title + " Synaptic Current" };
                (double minWeight, double maxWeight) = muscle.GetConnectionRange();
                double mult = (maxWeight - minWeight) / 55;
                foreach (ChemicalJunction jnc in muscle.EndPlates)
                {
                    LineSeries ls = new LineSeries();
                    Color color = Const.CellColors[jnc.PreNeuron.CellGroup];
                    a = (byte)((jnc.Weight - minWeight) * mult + 200);
                    ls.Color = OxyColor.FromAColor(a, OxyColor.FromRgb(color.R, color.G, color.B));
                    ls.Points.AddRange(Enumerable
                        .Range(tstart, tend - tstart + 1)
                        .Select(i => new DataPoint(Time[i], jnc.InputCurrent[i])));
                    modelSyn.Series.Add(ls);
                }
            }

            Image imgSyn = null, imgGap = null;
            if (modelSyn != null)
            {
                PlotView plotView = new();
                plotView.Model = modelSyn;
                if (saveImg)
                {
                    string filename = Const.OutputFolder + title + "Syn.png";
                    PngExporter.Export(modelSyn, filename, width, height);
                }            }
            if (modelGap != null)
            {
                if (saveImg)
                { 
                    string filename = saveImg ? Const.OutputFolder + title + "Gap.png";
                    PngExporter.Export(modelGap, filename, width, height);
                }
            }

            return (imgSyn, imgGap);
        }
        
    }
}
