using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Model;
using SiliFish.Services;
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
                double yMin = cells.Min(c => c.MinPotentialValue(iStart, iEnd));
                double yMax = cells.Max(c => c.MaxPotentialValue(iStart, iEnd));
                foreach (Cell c in cells)
                {
                    Color col = c.CellPool.Color;
                    if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot(c.ID + " Potentials", c.V, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot(c.ID + " Potentials", c.V, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1, col));
                }
            }
            if (cellPools != null)
            {
                double yMin = cellPools.Min(cp => cp.GetCells().Min(c => c.MinPotentialValue(iStart, iEnd)));
                double yMax = cellPools.Max(cp => cp.GetCells().Max(c => c.MaxPotentialValue(iStart, iEnd)));
                foreach (CellPool pool in cellPools)
                {
                    List<Cell> poolcells = pool.GetCells(cellSelection).ToList();
                    double[,] voltageArray = new double[poolcells.Count, timeArray.Length];

                    int i = 0;
                    foreach (Cell c in poolcells)
                        voltageArray.UpdateRow(i++, c.V);

                    Color col = pool.Color;
                    if (pool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot("Left " + pool.CellGroup + " Potentials", voltageArray, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot("Right " + pool.CellGroup + " Potentials", voltageArray, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1, col));
                }
            }
            return (leftImages, rightImages);
        }


        private static (List<Image>, List<Image>) PlotCurrents(double[] timeArray, Cell c,
            int iStart, int iEnd,
            double yMin, double yMax,
            bool gap, bool chem)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            string yAxis = $"Current ({Util.GetUoM(CurrentSettings.Settings.UoM, Measure.Current)})";

            if (gap)
            {
                (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = c.GetGapCurrents();
                if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Gap Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Gap Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1));
            }
            if (chem)
            {
                (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = c.GetIncomingSynapticCurrents();
                if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Incoming Syn. Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Incoming Syn. Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1));

                (colors, Dictionary<string, List<double>> EfferentCurrents) = c.GetOutgoingSynapticCurrents();
                if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Outgoing Syn. Currents", colors, EfferentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Outgoing Syn. Currents", colors, EfferentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1));
            }
            return (leftImages, rightImages);
        }
        private static (Dictionary<string, Color>, Dictionary<string, List<double>>) GetIncomingSynapticsCurrentsOfCell(Cell cell)
        {
            Dictionary<string, Color> colors = new();
            Dictionary<string, List<double>> AffarentCurrents = new();
            if (cell is Neuron neuron)
            {
                foreach (ChemicalSynapse jnc in neuron.Synapses)
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
            return (colors, AffarentCurrents);
        }
        private static (List<Image>, List<Image>) PlotCurrents(double[] timeArray, List<Cell> cells, List<CellPool> pools, CellSelectionStruct cellSelection,
            int iStart, int iEnd,
            bool gap, bool chem)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            if (cells != null)
            {
                double yMin = cells.Min(c => c.MinCurrentValue(iStart, iEnd));
                double yMax = cells.Max(c => c.MaxCurrentValue(iStart, iEnd));
                foreach (Cell c in cells)
                {
                    (List<Image> subLeftImages, List < Image > subRightImages) = PlotCurrents(timeArray, c, iStart, iEnd, yMin, yMax, gap, chem);
                    leftImages.AddRange(subLeftImages);
                    rightImages.AddRange(subRightImages);
                }
            }


            if (pools != null)
            {
                double yMin = pools.Min(cp => cp.GetCells().Min(c => c.MinCurrentValue(iStart, iEnd)));
                double yMax = pools.Max(cp => cp.GetCells().Max(c => c.MaxCurrentValue(iStart, iEnd)));
                foreach (CellPool pool in pools)
                {
                    IEnumerable<Cell> sampleCells = pool.GetCells(cellSelection);
                    foreach (Cell c in sampleCells)
                    {
                        (List<Image> subLeftImages, List<Image> subRightImages) = PlotCurrents(timeArray, c, iStart, iEnd, yMin, yMax, gap, chem);
                        leftImages.AddRange(subLeftImages);
                        rightImages.AddRange(subRightImages);
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
            string yAxis = $"Stimulus ({Util.GetUoM(CurrentSettings.Settings.UoM, Measure.Current)})";

            if (cells != null)
            {
                double yMin = cells.Min(c => c.MinStimulusValue);
                double yMax = cells.Max(c => c.MaxStimulusValue);
                foreach (Cell c in cells)
                {
                    Color col = c.CellPool.Color;
                    if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot(c.ID + " Stimulus", c.Stimuli.GetStimulusArray(timeArray.Length), timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot(c.ID + " Stimulus", c.Stimuli.GetStimulusArray(timeArray.Length), timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1, col));
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
                        stimArray.UpdateRow(i++, c.Stimuli.GetStimulusArray(timeArray.Length));

                    Color col = pool.Color;
                    if (pool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot("Left " + pool.CellGroup + " Stimulus", stimArray, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot("Right " + pool.CellGroup + " Stimulus", stimArray, timeArray, iStart, iEnd, yAxis, yMin, yMax * 1.1, col));
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

            (leftImagesSub, rightImagesSub) = PlotCurrents(timeArray, cells, pools, cellSelection, iStart, iEnd, gap: true, chem: true);
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
            int iStart = model.RunParam.iIndex(tStart);
            int iEnd = model.RunParam.iIndex(tEnd);
            //TODO color

            (Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes) = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(model);
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

            FileUtil.SaveEpisodesToCSV("Episodes.csv", 1, episodes);

            return (leftImages, null);
        }

        private static Image PlotHeatMap(List<SwimmingEpisode> episodes)
        {
            return null; //TODO
        }


        public static (List<Image>, List<Image>) Plot(PlotType PlotType, SwimmingModel model, List<Cell> Cells, List<CellPool> Pools,
            CellSelectionStruct cellSelection,
            double dt, int tStart = 0, int tEnd = -1, int tSkip = 0)
        {
            if (PlotType != PlotType.Episodes &&
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
                default:
                    break;
            }
            return (null, null);
        }

    }
}
