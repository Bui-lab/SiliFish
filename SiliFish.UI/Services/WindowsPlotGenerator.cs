using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using SiliFish.UI;
using SiliFish.ModelUnits.Junction;
using SiliFish.Repositories;
using SiliFish.Services.Plotting.PlotSelection;
using static OfficeOpenXml.ExcelErrorValue;
using SiliFish.Services.Dynamics;

namespace Services
{
    public class WindowsPlotGenerator
    {
        private static (List<Image>, List<Image>) PlotMembranePotentials(double[] timeArray, List<Cell> cells, List<CellPool> cellPools,
                    PlotSelectionMultiCells cellSelection, int iStart, int iEnd)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            string yAxis = "Memb. Pot. (mV)";
            if (cells != null)
            {
                double yMin = cells.Min(c => c.MinPotentialValue(iStart, iEnd));
                double yMax = cells.Max(c => c.MaxPotentialValue(iStart, iEnd));
                foreach (Cell c in cells)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = c.MinPotentialValue(iStart, iEnd);
                        yMax = c.MaxPotentialValue(iStart, iEnd);
                        Util.SetYRange(ref yMin, ref yMax);
                    }
                    Color col = c.CellPool.Color;
                    if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot(c.ID + " Potentials", c.V, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot(c.ID + " Potentials", c.V, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                }
            }
            if (cellPools != null)
            {
                double yMin = cellPools.Min(cp => cp.GetCells().Min(c => c.MinPotentialValue(iStart, iEnd)));
                double yMax = cellPools.Max(cp => cp.GetCells().Max(c => c.MaxPotentialValue(iStart, iEnd)));
                foreach (CellPool pool in cellPools)
                {
                    List<Cell> poolcells = pool.GetCells(cellSelection, iStart, iEnd).ToList();
                    double[,] voltageArray = new double[poolcells.Count, timeArray.Length];
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = poolcells.Min(c => c.MinPotentialValue(iStart, iEnd));
                        yMax = poolcells.Max(c => c.MaxPotentialValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }
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


        private static (List<Image>, List<Image>) PlotCurrents(double[] timeArray, Cell c,
            int iStart, int iEnd,
            double yMin, double yMax,
            bool gap, bool chemin, bool chemout, UnitOfMeasure UoM)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            string yAxis = $"Current ({Util.GetUoM(UoM, Measure.Current)})";

            if (gap)
            {
                (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = c.GetGapCurrents();
                if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Gap Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Gap Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
            }
            if (chemin)
            {
                (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = c.GetIncomingSynapticCurrents();
                if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Incoming Syn. Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Incoming Syn. Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
            }
            if (chemout)
            { 
                (Dictionary<string, Color> colors, Dictionary<string, List<double>> EfferentCurrents) = c.GetOutgoingSynapticCurrents();
                if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Outgoing Syn. Currents", colors, EfferentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Outgoing Syn. Currents", colors, EfferentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
            }
            return (leftImages, rightImages);
        }

        private static (List<Image>, List<Image>) PlotCurrents(double[] timeArray, List<Cell> cells, List<CellPool> pools, PlotSelectionMultiCells cellSelection,
            int iStart, int iEnd,
            bool gap, bool chemin, bool chemout, UnitOfMeasure UoM)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            if (cells != null)
            {
                double yMin = cells.Min(c => c.MinCurrentValue(gap, chemin, chemout, iStart, iEnd));
                double yMax = cells.Max(c => c.MaxCurrentValue(gap, chemin, chemout, iStart, iEnd));
                foreach (Cell c in cells)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = c.MinCurrentValue(gap, chemin, chemout, iStart, iEnd);
                        yMax = c.MaxCurrentValue(gap, chemin, chemout, iStart, iEnd);
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    (List<Image> subLeftImages, List < Image > subRightImages) = PlotCurrents(timeArray, c, iStart, iEnd, yMin, yMax, gap, chemin, chemout, UoM);
                    leftImages.AddRange(subLeftImages);
                    rightImages.AddRange(subRightImages);
                }
            }


            if (pools != null)
            {
                double yMin = pools.Min(cp => cp.GetCells().Min(c => c.MinCurrentValue(gap, chemin, chemout, iStart, iEnd)));
                double yMax = pools.Max(cp => cp.GetCells().Max(c => c.MaxCurrentValue(gap, chemin, chemout, iStart, iEnd)));
                foreach (CellPool pool in pools)
                {
                    IEnumerable<Cell> sampleCells = pool.GetCells(cellSelection, iStart, iEnd);
                    foreach (Cell c in sampleCells)
                    {
                        if (!GlobalSettings.SameYAxis)
                        {
                            yMin = c.MinCurrentValue(gap, chemin, chemout, iStart, iEnd);
                            yMax = c.MaxCurrentValue(gap, chemin, chemout, iStart, iEnd);
                            Util.SetYRange(ref yMin, ref yMax);
                        }
                        (List<Image> subLeftImages, List<Image> subRightImages) = PlotCurrents(timeArray, c, iStart, iEnd, yMin, yMax, gap, chemin, chemout, UoM);
                        leftImages.AddRange(subLeftImages);
                        rightImages.AddRange(subRightImages);
                    }
                }
            }
            return (leftImages, rightImages);
        }

        private static (List<Image>, List<Image>) PlotStimuli(double[] timeArray, List<Cell> cells, List<CellPool> pools,
            PlotSelectionMultiCells cellSelection, int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            string yAxis = $"Stimulus ({Util.GetUoM(UoM, Measure.Current)})";

            if (cells != null)
            {
                double yMin = cells.Min(c => c.MinStimulusValue());
                double yMax = cells.Max(c => c.MaxStimulusValue());
                foreach (Cell c in cells)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = c.MinStimulusValue();
                        yMax = c.MaxStimulusValue();
                        Util.SetYRange(ref yMin, ref yMax);
                    }
                    Color col = c.CellPool.Color;
                    if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot(c.ID + " Stimulus", c.Stimuli.GetStimulusArray(timeArray.Length), timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot(c.ID + " Stimulus", c.Stimuli.GetStimulusArray(timeArray.Length), timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                }
            }
            if (pools != null)
            {
                double yMin = pools.Min(cp => cp.GetCells().Min(c => c.MinStimulusValue()));
                double yMax = pools.Max(cp => cp.GetCells().Max(c => c.MaxStimulusValue()));
                foreach (CellPool pool in pools)
                {
                    List<Cell> poolcells = pool.GetCells(cellSelection, iStart, iEnd).ToList();
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = poolcells.Min(c => c.MinStimulusValue());
                        yMax = poolcells.Max(c => c.MaxStimulusValue());
                        Util.SetYRange(ref yMin, ref yMax);
                    }
                    double[,] stimArray = new double[poolcells.Count, timeArray.Length];

                    int i = 0;
                    foreach (Cell c in poolcells)
                        stimArray.UpdateRow(i++, c.Stimuli.GetStimulusArray(timeArray.Length));

                    Color col = pool.Color;
                    if (pool.PositionLeftRight == SagittalPlane.Left)
                        leftImages.Add(UtilWindows.CreateLinePlot("Left " + pool.CellGroup + " Stimulus", stimArray, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                    else
                        rightImages.Add(UtilWindows.CreateLinePlot("Right " + pool.CellGroup + " Stimulus", stimArray, timeArray, iStart, iEnd, yAxis, yMin, yMax, col));
                }
            }
            return (leftImages, rightImages);
        }

        private static (List<Image>, List<Image>) PlotTension(double[] timeArray, List<MuscleCell> cells, List<CellPool> pools,
            PlotSelectionMultiCells cellSelection, int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            string yAxis = $"Muscle Tension";

            //TODO
            return (leftImages, rightImages);
        }
        private static (List<Image>, List<Image>) PlotFullDynamics(double[] timeArray, List<Cell> cells, List<CellPool> pools,
            PlotSelectionMultiCells cellSelection, int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            (List<Image> leftImagesSub, List<Image> rightImagesSub) = PlotMembranePotentials(timeArray, cells, pools, cellSelection, iStart, iEnd);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            (leftImagesSub, rightImagesSub) = PlotCurrents(timeArray, cells, pools, cellSelection, iStart, iEnd, gap: true, chemin: true, chemout: true, UoM);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            (leftImagesSub, rightImagesSub) = PlotStimuli(timeArray, cells, pools, cellSelection, iStart, iEnd, UoM);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            List<MuscleCell> muscleCells = cells.Where(c => c is MuscleCell).Select(c => (MuscleCell)c).ToList();
            if (muscleCells.Any())
            {
                (leftImagesSub, rightImagesSub) = PlotTension(timeArray, muscleCells, pools, cellSelection, iStart, iEnd, UoM);
                leftImages = leftImages.Concat(leftImagesSub).ToList();
                rightImages = rightImages.Concat(rightImagesSub).ToList();
            }

            return (leftImages, rightImages);
        }

        private static (List<Image>, List<Image>) PlotEpisodes(RunningModel model, double tStart, double tEnd)
        {
            List<Image> leftImages = new();
            int iStart = model.RunParam.iIndex(tStart);
            int iEnd = model.RunParam.iIndex(tEnd);
            //FUTURE_IMPROVEMENT color

            (Coordinate[] tail_tip_coord, SwimmingEpisodes episodes) = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(model);
            leftImages.Add(UtilWindows.CreateLinePlot("Tail Movement",
                tail_tip_coord.Select(c => c.X).ToArray(),
                model.TimeArray, iStart, iEnd,
                "X", null, null,
                Color.Red));
            double[] xValues, yValues;
            if (episodes.HasEpisodes)
            {
                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.EpisodeDuration);
                leftImages.Add(UtilWindows.CreateScatterPlot("Episode Duration",
                    xValues, yValues,
                    tStart, tEnd,
                    "Duration (ms)", 0, null,
                    Color.Red));
                if (episodes.EpisodeCount > 1)
                    leftImages.Add(UtilWindows.CreateScatterPlot("Episode Intervals",
                        Enumerable.Range(0, episodes.EpisodeCount - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray(),
                        Enumerable.Range(0, episodes.EpisodeCount - 1).Select(i => episodes[i].End).ToArray(),
                        tStart, tEnd,
                        "Intervals (ms)", 0, null,
                        Color.Red));
                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.InstantFreq);
                leftImages.Add(UtilWindows.CreateScatterPlot("Instantenous Frequency",
                    xValues, yValues,
                    tStart, tEnd,
                    "Freq.", 0, null,
                    Color.Red));
                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.InlierInstantFreq);
                leftImages.Add(UtilWindows.CreateScatterPlot("Instantenous Frequency (Outliers removed)",
                    xValues, yValues,
                    tStart, tEnd,
                    "Freq.", 0, null,
                    Color.Red));
                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatFreq);
                leftImages.Add(UtilWindows.CreateScatterPlot("Tail Beat Frequency",
                    xValues, yValues,
                    tStart, tEnd,
                    "Freq.", 0, null,
                    Color.Red));
                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatsPerEpisode);
                leftImages.Add(UtilWindows.CreateScatterPlot("Tail Beat/Episode",
                    xValues, yValues,
                    tStart, tEnd,
                    "Count", 0, null,
                    Color.Red));
            }

            ModelFile.SaveEpisodesToCSV("Episodes.csv", 1, episodes);

            return (leftImages, null);
        }

        public static (List<Image>, List<Image>) Plot(PlotType PlotType, RunningModel model, List<Cell> Cells, List<CellPool> Pools,
            PlotSelectionInterface cellSelectionInterface, int tStart = 0, int tEnd = -1)
        {
            if (PlotType.GetGroup() != "episode" &&
                (Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return (null, null);
            UnitOfMeasure uom = model.Settings.UoM;
            int iStart = model.RunParam.iIndex(tStart);
            int iEnd = model.RunParam.iIndex(tEnd);

            double[] TimeArray = model.TimeArray;
            if (iEnd < iStart || iEnd >= TimeArray.Length)
                iEnd = TimeArray.Length - 1;

            if (cellSelectionInterface is PlotSelectionMultiCells cellSelection)
            {
                switch (PlotType)
                {
                    case PlotType.MembPotential:
                        return PlotMembranePotentials(TimeArray, Cells, Pools, cellSelection, iStart, iEnd);
                    case PlotType.Current:
                            return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: true, chemin: true, chemout: true, uom);
                    case PlotType.GapCurrent:
                            return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: true, chemin: false, chemout: false, uom);
                    case PlotType.ChemCurrent:
                            return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: false, chemin: true, chemout: false, uom);
                    case PlotType.ChemOutCurrent:
                            return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: false, chemin: false, chemout: true, uom);
                    case PlotType.Stimuli:
                        return PlotStimuli(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, uom);
                    case PlotType.FullDyn:
                        return PlotFullDynamics(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, uom);
                    case PlotType.Tension:
                        List<MuscleCell> muscleCells = Cells.Where(c => c is MuscleCell).Select(c => (MuscleCell)c).ToList();
                        return PlotTension(TimeArray, muscleCells, Pools, cellSelection, iStart, iEnd, uom);
                    case PlotType.EpisodesTail:
                        return PlotEpisodes(model, tStart, tEnd);
                        //TODO windows plot for episodes
                    default:
                        break;
                }
            }
            return (null, null);
        }

    }
}
