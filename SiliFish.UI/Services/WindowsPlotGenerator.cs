using SiliFish;
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
                        leftImages.Add(UtilWindows.CreatePlotImage("Left" + c.CellGroup, c.V, timeArray, iStart, iEnd, col));
                    else
                        rightImages.Add(UtilWindows.CreatePlotImage("Right" + c.CellGroup, c.V, timeArray, iStart, iEnd, col));
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
                        leftImages.Add(UtilWindows.CreatePlotImage("Left" + pool.CellGroup, voltageArray, timeArray, iStart, iEnd, col));
                    else
                        rightImages.Add(UtilWindows.CreatePlotImage("Right" + pool.CellGroup, voltageArray, timeArray, iStart, iEnd, col));
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
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, colors, AffarentCurrents, iStart, iEnd, timeArray));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, colors, AffarentCurrents, iStart, iEnd, timeArray));
            }

            if (pools != null)
                foreach (CellPool pool in pools)
            {
                IEnumerable<Cell> sampleCells = pool.GetCells(nSample);
                foreach (Cell c in sampleCells)
                {
                    (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = GetAffarentCurrentsOfCell(c, gap, chem);
                    if (pool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, colors, AffarentCurrents, iStart, iEnd, timeArray));
                    else
                        rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, colors, AffarentCurrents, iStart, iEnd, timeArray));
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
                        leftImages.Add(UtilWindows.CreatePlotImage("Left" + c.CellGroup, c.Stimulus?.getValues(timeArray.Length), timeArray, iStart, iEnd, col));
                    else
                        rightImages.Add(UtilWindows.CreatePlotImage("Right" + c.CellGroup, c.Stimulus?.getValues(timeArray.Length), timeArray, iStart, iEnd, col));
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
                        leftImages.Add(UtilWindows.CreatePlotImage("Left" + pool.CellGroup, stimArray, timeArray, iStart, iEnd, col));
                    else
                        rightImages.Add(UtilWindows.CreatePlotImage("Right" + pool.CellGroup, stimArray, timeArray, iStart, iEnd, col));
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
        public static (List<Image>, List<Image>) Plot(PlotType PlotType, double[] TimeArray, List<Cell> Cells, List<CellPool> Pools, int tStart = 0, int tEnd = -1, int tSkip = 0, int nSample = 0)
        {
            if ((Cells == null || !Cells.Any()) && 
                (Pools == null || !Pools.Any()))
                return (null, null);
            double dt = RunParam.dt;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            int offset = tSkip;
            if (iEnd < iStart || iEnd >= TimeArray.Length)
                iEnd = TimeArray.Length - 1;

            double[] TimeOffset = offset > 0 ? (TimeArray.Select(t => t - offset).ToArray()) : TimeArray;

            switch (PlotType)
            {
                case PlotType.MembPotential:
                    return PlotMembranePotentials(TimeOffset, Cells, Pools, iStart, iEnd, nSample: nSample);
                case PlotType.Current:
                    return PlotCurrents(TimeOffset, Cells, Pools, iStart, iEnd, gap: true, chem: true, nSample: nSample);
                case PlotType.GapCurrent:
                    return PlotCurrents(TimeOffset, Cells, Pools, iStart, iEnd, gap: true, chem: false, nSample: nSample);
                case PlotType.ChemCurrent:
                    return PlotCurrents(TimeOffset, Cells, Pools, iStart, iEnd, gap: false, chem: true, nSample: nSample);
                case PlotType.Stimuli:
                    return PlotStimuli(TimeOffset, Cells, Pools, iStart, iEnd, nSample: nSample);
                case PlotType.FullDyn:
                    return PlotFullDynamics(TimeOffset, Cells, Pools, iStart, iEnd, nSample: nSample);
                default:
                    break;
            }
            return (null, null);
        }


    }
}
