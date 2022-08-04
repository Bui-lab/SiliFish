using SiliFish;
using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.UI;

namespace Services
{
    public class WindowsPlotGenerator
    {
        private static (List<Image>, List<Image>) PlotMembranePotentials(double[] timeArray, List<Cell> cells, List<CellPool> cellPools,
            CellSelectionStruct cellSelection, int iStart, int iEnd)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            string yAxis = "Memb. Potential (mV)";
            if (cells != null)
            {
                double yMin = cells.Min(c => c.MinPotentialValue);
                double yMax = cells.Max(c => c.MaxPotentialValue);
                foreach (Cell c in cells)
                {
                    Color col = c.CellPool.Color;
                    if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot(c.ID + " Potentials", c.V, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot(c.ID + " Potentials", c.V, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                }
            }
            if (cellPools != null)
            {
                double yMin = cellPools.Min(cp => cp.GetCells().Min(c => c.MinPotentialValue));
                double yMax = cellPools.Max(cp => cp.GetCells().Max(c => c.MaxPotentialValue));
                foreach (CellPool pool in cellPools)
                {
                    List<Cell> poolcells = pool.GetCells(cellSelection).ToList();
                    double[,] voltageArray = new double[poolcells.Count, timeArray.Length];

                    int i = 0;
                    foreach (Cell c in poolcells)
                        voltageArray.UpdateRow(i++, c.V);

                    Color col = pool.Color;
                    if (pool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot("Left " + pool.CellGroup + " Potentials", voltageArray, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot("Right " + pool.CellGroup + " Potentials", voltageArray, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                }
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
                    colors.TryAdd(jnc.Cell1.ID, jnc.Cell1.CellPool.Color);
                    AffarentCurrents.AddObject(jnc.Cell1.ID, jnc.InputCurrent.ToList());
                }
                foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell1 == cell))
                {
                    colors.TryAdd(jnc.Cell2.ID, jnc.Cell2.CellPool.Color);
                    AffarentCurrents.AddObject(jnc.Cell2.ID, jnc.InputCurrent.Select(d => -d).ToList());
                }
            }
            if (chem)
            {
                if (cell is Neuron neuron2)
                {
                    foreach (ChemicalSynapse jnc in neuron2.Synapses)
                    {
                        colors.TryAdd(jnc.PreNeuron.ID, jnc.PreNeuron.CellPool.Color);
                        AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
                    }
                }
                else if (cell is MuscleCell muscle)
                {
                    foreach (ChemicalSynapse jnc in muscle.EndPlates)
                    {
                        colors.TryAdd(jnc.PreNeuron.ID, jnc.PostCell.CellPool.Color);
                        AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
                    }
                }

            }
            return (colors, AffarentCurrents);
        }
        private static (List<Image>, List<Image>) PlotCurrents(double[] timeArray, List<Cell> cells, List<CellPool> pools, CellSelectionStruct cellSelection, 
            int iStart, int iEnd,
            bool gap, bool chem)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            string yAxis = "Current (pA)";
            if (cells != null)
            {
                double yMin = cells.Min(c => c.MinCurrentValue);
                double yMax = cells.Max(c => c.MaxCurrentValue);
                foreach (Cell c in cells)
                {
                    (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = GetAffarentCurrentsOfCell(c, gap, chem);
                    if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
                    else
                        rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
                }
            }

            if (pools != null)
            {
                double yMin = pools.Min(cp => cp.GetCells().Min(c => c.MinCurrentValue));
                double yMax = pools.Max(cp => cp.GetCells().Max(c => c.MaxCurrentValue));
                foreach (CellPool pool in pools)
                {
                    IEnumerable<Cell> sampleCells = pool.GetCells(cellSelection);
                    foreach (Cell c in sampleCells)
                    {
                        (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = GetAffarentCurrentsOfCell(c, gap, chem);
                        if (pool.PositionLeftRight == SagittalPlane.Left)
                            leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
                        else
                            rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
                    }
                }
            }
            return (leftImages, rightImages);
        }

        private static (List<Image>, List<Image>) PlotStimuli(double[] timeArray, List<Cell> cells, List<CellPool> pools,
            CellSelectionStruct cellSelection, int iStart, int iEnd)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            string yAxis = "Stimulus (pA)";

            if (cells != null)
            {
                double yMin = cells.Min(c => c.MinStimulusValue);
                double yMax = cells.Max(c => c.MaxStimulusValue);
                foreach (Cell c in cells)
                {
                    Color col = c.CellPool.Color;
                    if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot(c.ID + " Stimulus", c.Stimulus?.getValues(timeArray.Length), timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot(c.ID + " Stimulus", c.Stimulus?.getValues(timeArray.Length), timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                }
            }
            if (pools != null)
            {
                double yMin = pools.Min(cp => cp.GetCells().Min(c => c.MinStimulusValue));
                double yMax = pools.Max(cp => cp.GetCells().Max(c => c.MaxStimulusValue));
                foreach (CellPool pool in pools)
                {
                    List<Cell> poolcells = pool.GetCells(cellSelection).ToList();
                    double[,] stimArray = new double[poolcells.Count, timeArray.Length];

                    int i = 0;
                    foreach (Cell c in poolcells)
                        stimArray.UpdateRow(i++, c.Stimulus?.getValues(timeArray.Length));

                    Color col = pool.Color;
                    if (pool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot("Left " + pool.CellGroup + " Stimulus", stimArray, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot("Right " + pool.CellGroup + " Stimulus", stimArray, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                }
            }
            return (leftImages, rightImages);
        }
        private static (List<Image>, List<Image>) PlotFullDynamics(double[] timeArray, List<Cell> cells, List<CellPool> pools, CellSelectionStruct cellSelection, int iStart, int iEnd)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            (List<Image> leftImagesSub, List<Image> rightImagesSub) = PlotMembranePotentials(timeArray, cells, pools, cellSelection, iStart, iEnd);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            (leftImagesSub, rightImagesSub) = PlotCurrents(timeArray, cells, pools, cellSelection,iStart, iEnd,  gap: true, chem: true);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            (leftImagesSub, rightImagesSub) = PlotStimuli(timeArray, cells, pools, cellSelection, iStart, iEnd);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            return (leftImages, rightImages);
        }

        private static (List<Image>, List<Image>) PlotEpisodes(SwimmingModel model, double tStart, double tEnd)
        {
            List<Image> leftImages = new();
            int iStart = model.runParam.iIndex(tStart);
            int iEnd = model.runParam.iIndex(tEnd);
            //TODO color

            (Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes) = model.GetSwimmingEpisodes(-0.5, 0.5, 1000);
            leftImages.Add(UtilWindows.CreateLinePlot("Tail Movement",
                tail_tip_coord.Select(c => c.X).ToArray(),
                model.TimeArray, iStart, iEnd, 
                "X", null, null,
                Color.Red));
            if (episodes.Any())
            {
                leftImages.Add(UtilWindows.CreateScatterPlot("Episode Duration",
                    episodes.Select(e => e.EpisodeDuration).ToArray(),
                    episodes.Select(e => e.Start).ToArray(),
                    tStart, tEnd,
                    "Duration (ms)", 0, null,
                    Color.Red));
                if (episodes.Count > 1)
                    leftImages.Add(UtilWindows.CreateScatterPlot("Episode Intervals",
                        Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray(),
                        Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i].End).ToArray(),
                        tStart, tEnd,
                        "Intervals (ms)", 0, null,
                        Color.Red));
                leftImages.Add(UtilWindows.CreateScatterPlot("Instantenous Frequency",
                    episodes.SelectMany(e => e.InstantFequency).ToArray(),
                    episodes.SelectMany(e => e.Beats.Select(b => b.beatStart)).ToArray(),
                    tStart, tEnd,
                    "Freq.", 0, null,
                    Color.Red));
                leftImages.Add(UtilWindows.CreateScatterPlot("Instantenous Frequency (Outliers removed)",
                    episodes.SelectMany(e => e.InlierInstantFequency).ToArray(),
                    episodes.SelectMany(e => e.InlierBeats.Select(b => b.beatStart)).ToArray(),
                    tStart, tEnd,
                    "Freq.", 0, null,
                    Color.Red));
                leftImages.Add(UtilWindows.CreateScatterPlot("Tail Beat Frequency",
                    episodes.Select(e => e.BeatFrequency).ToArray(),
                    episodes.Select(e => e.Start).ToArray(),
                    tStart, tEnd,
                    "Freq.", 0, null,
                    Color.Red));
                leftImages.Add(UtilWindows.CreateScatterPlot("Tail Beat/Episode",
                    episodes.Select(e => (double)e.Beats.Count).ToArray(),
                    episodes.Select(e => e.Start).ToArray(),
                    tStart, tEnd,
                    "Count", 0, null,
                    Color.Red));
            }

            string summary = "";
            Util.SaveToFile("Episodes"+DateTime.Now.ToShortTimeString(), summary);

            return (leftImages, null);
        }

        private static Image PlotHeatMap(List<SwimmingEpisode> episodes)
        {
            return null; //TODO
        }


        public static (List<Image>, List<Image>) Plot(PlotType PlotType, SwimmingModel model, List<Cell> Cells, List<CellPool> Pools,
            CellSelectionStruct cellSelection, 
            double dt , int tStart = 0, int tEnd = -1, int tSkip = 0)
        {
            if (PlotType != PlotType .Episodes &&
                (Cells == null || !Cells.Any()) && 
                (Pools == null || !Pools.Any()))
                return (null, null);
            int iStart = (int)((tStart + tSkip) / dt);
            if (tEnd < 0)
                tEnd = (int)model.TimeArray.Last();
            int iEnd = (int)((tEnd + tSkip) / dt);
            double[] TimeArray = model.TimeArray;
            if (iEnd < iStart || iEnd >= TimeArray.Length)
                iEnd = TimeArray.Length - 1;
           
            switch (PlotType)
            {
                case PlotType.MembPotential:
                    return PlotMembranePotentials(TimeArray, Cells, Pools, cellSelection, iStart, iEnd);
                case PlotType.Current:
                    return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: true, chem: true);
                case PlotType.GapCurrent:
                    return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: true, chem: false);
                case PlotType.ChemCurrent:
                    return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: false, chem: true);
                case PlotType.Stimuli:
                    return PlotStimuli(TimeArray, Cells, Pools, cellSelection, iStart, iEnd);
                case PlotType.FullDyn:
                    return PlotFullDynamics(TimeArray, Cells, Pools, cellSelection, iStart, iEnd);
                case PlotType.Episodes:
                    return PlotEpisodes(model, tStart, tEnd);
                case PlotType.BodyAngleHeatMap://TODO
                    break;

                default:
                    break;
            }
            return (null, null);
        }

    }
}
