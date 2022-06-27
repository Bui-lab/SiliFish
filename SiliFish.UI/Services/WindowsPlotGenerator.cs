﻿using SiliFish;
using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.ModelUnits;
using SiliFish.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class WindowsPlotGenerator
    {
        private static (List<Image>, List<Image>) PlotMembranePotentials(double[] timeArray, List<Cell> cells, List<CellPool> pools, int iStart, int iEnd, int nSample)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            if (cells != null)
                foreach (Cell c in cells)
                {
                    Color col = c.CellPool.Color;
                    if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot("Left " + c.CellGroup + " Potentials", c.V, timeArray, iStart, iEnd, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot("Right " + c.CellGroup + " Potentials", c.V, timeArray, iStart, iEnd, col));
                }
            if (pools != null)
                foreach (CellPool pool in pools)
                {
                    List<Cell> poolcells = pool.GetCells(nSample).ToList();
                    double[,] voltageArray = new double[poolcells.Count, timeArray.Length];

                    int i = 0;
                    foreach (Cell c in poolcells)
                        voltageArray.UpdateRow(i++, c.V);

                    Color col = pool.Color;
                    if (pool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot("Left " + pool.CellGroup + " Potentials", voltageArray, timeArray, iStart, iEnd, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot("Right " + pool.CellGroup + " Potentials", voltageArray, timeArray, iStart, iEnd, col));
                }
            return (leftImages, rightImages);
        }

        private static (Dictionary<string, Color> , Dictionary<string, List<double>> )GetAffarentCurrentsOfCell(Cell cell, bool gap, bool chem)
        {
            Dictionary<string, Color> colors = new();
            Dictionary<string, List<double>> AffarentCurrents = new();
            if (gap && cell is Neuron neuron)
            {
                foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell2 == cell))
                {
                    colors.Add(jnc.Cell1.ID, jnc.Cell1.CellPool.Color);
                    AffarentCurrents.AddObject(jnc.Cell1.ID, jnc.InputCurrent.ToList());
                }
                foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell1 == cell))
                {
                    colors.Add(jnc.Cell2.ID, jnc.Cell2.CellPool.Color);
                    AffarentCurrents.AddObject(jnc.Cell2.ID, jnc.InputCurrent.Select(d => -d).ToList());
                }
            }
            if (chem)
            {
                if (cell is Neuron neuron2)
                {
                    foreach (ChemicalSynapse jnc in neuron2.Synapses)
                    {
                        colors.Add(jnc.PreNeuron.ID, jnc.PreNeuron.CellPool.Color);
                        AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
                    }
                }
                else if (cell is MuscleCell muscle)
                {
                    foreach (ChemicalSynapse jnc in muscle.EndPlates)
                    {
                        colors.Add(jnc.PreNeuron.ID, jnc.PostCell.CellPool.Color);
                        AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
                    }
                }

            }
            return (colors, AffarentCurrents);
        }
        private static (List<Image>, List<Image>) PlotCurrents(double[] timeArray, List<Cell> cells, List<CellPool> pools, int iStart, int iEnd, int nSample, bool gap, bool chem)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            if (cells != null)
                foreach (Cell c in cells)
            {
                (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = GetAffarentCurrentsOfCell(c, gap, chem);
                if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID+ " Currents", colors, AffarentCurrents, iStart, iEnd, timeArray));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Currents", colors, AffarentCurrents, iStart, iEnd, timeArray));
            }

            if (pools != null)
                foreach (CellPool pool in pools)
            {
                IEnumerable<Cell> sampleCells = pool.GetCells(nSample);
                foreach (Cell c in sampleCells)
                {
                    (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = GetAffarentCurrentsOfCell(c, gap, chem);
                    if (pool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Currents", colors, AffarentCurrents, iStart, iEnd, timeArray));
                    else
                        rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Currents", colors, AffarentCurrents, iStart, iEnd, timeArray));
                }
            }
            return (leftImages, rightImages);
        }

        private static (List<Image>, List<Image>) PlotStimuli(double[] timeArray, List<Cell> cells, List<CellPool> pools, int iStart, int iEnd, int nSample)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            if (cells != null)
                foreach (Cell c in cells)
                {
                    Color col = c.CellPool.Color;
                    if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot("Left " + c.CellGroup + " Stimulus", c.Stimulus?.getValues(timeArray.Length), timeArray, iStart, iEnd, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot("Right " + c.CellGroup + " Stimulus", c.Stimulus?.getValues(timeArray.Length), timeArray, iStart, iEnd, col));
                }
            if (pools != null)
                foreach (CellPool pool in pools)
                {
                    List<Cell> poolcells = pool.GetCells(nSample).ToList();
                    double[,] stimArray = new double[poolcells.Count, timeArray.Length];

                    int i = 0;
                    foreach (Cell c in poolcells)
                        stimArray.UpdateRow(i++, c.Stimulus?.getValues(timeArray.Length));

                    Color col = pool.Color;
                    if (pool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot("Left " + pool.CellGroup + " Stimulus", stimArray, timeArray, iStart, iEnd, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot("Right " + pool.CellGroup + " Stimulus", stimArray, timeArray, iStart, iEnd, col));
                }
            return (leftImages, rightImages);
        }
        private static (List<Image>, List<Image>) PlotFullDynamics(double[] timeArray, List<Cell> cells, List<CellPool> pools, int iStart, int iEnd, int nSample)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            (List<Image> leftImagesSub, List<Image> rightImagesSub) = PlotMembranePotentials(timeArray, cells, pools, iStart, iEnd, nSample);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            (leftImagesSub, rightImagesSub) = PlotCurrents(timeArray, cells, pools, iStart, iEnd, gap: true, chem: true, nSample: nSample);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            (leftImagesSub, rightImagesSub) = PlotStimuli(timeArray, cells, pools, iStart, iEnd, nSample: nSample);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            return (leftImages, rightImages);
        }
        public static (List<Image>, List<Image>) Plot(PlotType PlotType, SwimmingModel model, List<Cell> Cells, List<CellPool> Pools, 
            double dt , int tStart = 0, int tEnd = -1, int tSkip = 0, int nSample = 0)
        {
            if ((Cells == null || !Cells.Any()) && 
                (Pools == null || !Pools.Any()))
                return (null, null);
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            double[] TimeArray = model.TimeArray;
            if (iEnd < iStart || iEnd >= TimeArray.Length)
                iEnd = TimeArray.Length - 1;
           
            switch (PlotType)
            {
                case PlotType.MembPotential:
                    return PlotMembranePotentials(TimeArray, Cells, Pools, iStart, iEnd, nSample: nSample);
                case PlotType.Current:
                    return PlotCurrents(TimeArray, Cells, Pools, iStart, iEnd, gap: true, chem: true, nSample: nSample);
                case PlotType.GapCurrent:
                    return PlotCurrents(TimeArray, Cells, Pools, iStart, iEnd, gap: true, chem: false, nSample: nSample);
                case PlotType.ChemCurrent:
                    return PlotCurrents(TimeArray, Cells, Pools, iStart, iEnd, gap: false, chem: true, nSample: nSample);
                case PlotType.Stimuli:
                    return PlotStimuli(TimeArray, Cells, Pools, iStart, iEnd, nSample: nSample);
                case PlotType.FullDyn:
                    return PlotFullDynamics(TimeArray, Cells, Pools, iStart, iEnd, nSample: nSample);
                case PlotType.Episodes://TODO
                    return PlotEpisodes(model, tStart, tEnd);
                case PlotType.BodyAngleHeatMap://TODO
                    break;
                case PlotType.TailBeatFrequency://TODO
                    break;
                default:
                    break;
            }
            return (null, null);
        }

        private static Image PlotHeatMap(List<SwimmingEpisode> episodes)
        {
            return null; //TODO
        }

        private static (List<Image>, List<Image>) PlotEpisodes(SwimmingModel model, double tStart, double tEnd)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            double threshold = 1;
            (double[] V, List<SwimmingEpisode> episodes) = model.DetectEvents(threshold);
            double[,] V_threshold = new double[2, V.Length];
            foreach (int i in Enumerable.Range(0, V.Length))
            {
                V_threshold[0, i] = V[i];
                V_threshold[1, i] = threshold;
            }

            int iStart = model.runParam.iIndex(tStart); 
            int iEnd = model.runParam.iIndex(tEnd);
            //TODO color
            leftImages.Add(UtilWindows.CreateLinePlot("Episodes", V_threshold, model.TimeArray, iStart, iEnd, 
                Color.Red));
            if (episodes.Any())
            {
                leftImages.Add(UtilWindows.CreateScatterPlot("Episode Duration",
                    episodes.Select(e => e.EpisodeDuration).ToArray(),
                    episodes.Select(e => e.Start).ToArray(),
                    tStart, tEnd, Color.Red));
                if (episodes.Count > 1)
                    leftImages.Add(UtilWindows.CreateScatterPlot("Episode Intervals",
                        Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray(),
                        Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i].End).ToArray(),
                        tStart, tEnd, Color.Red));
            }

            List<SwimmingEpisode> episodes2 = model.GetSwimmingEpisodes(-0.5, 0.5, 1000);
            if (episodes2.Any())
            {
                rightImages.Add(UtilWindows.CreateScatterPlot("Episode Duration",
                    episodes2.Select(e => e.EpisodeDuration).ToArray(),
                    episodes2.Select(e => e.Start).ToArray(),
                    tStart, tEnd, Color.Red));
                if (episodes2.Count > 1)
                    rightImages.Add(UtilWindows.CreateScatterPlot("Episode Intervals",
                        Enumerable.Range(0, episodes2.Count - 1).Select(i => episodes2[i + 1].Start - episodes[i].End).ToArray(),
                        Enumerable.Range(0, episodes2.Count - 1).Select(i => episodes2[i].End).ToArray(),
                        tStart, tEnd, Color.Red));
            }

            return (leftImages, rightImages);
        }

        private static Image PlotInstantaneousFrequency(List<SwimmingEpisode> episodes)
        {
            return null; //TODO
        }


    }
}