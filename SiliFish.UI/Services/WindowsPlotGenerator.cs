using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Services;
using SiliFish.UI;
using SiliFish.ModelUnits.Junction;
using SiliFish.Services.Plotting;
using SiliFish.Repositories;

namespace Services
{
    public class WindowsPlotGenerator
    {
        private static (List<Image>, List<Image>) PlotMembranePotentials(double[] timeArray, List<Cell> cells, List<CellPool> cellPools,
                    PlotSelectionMultiCells cellSelection, int iStart, int iEnd)
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
                    List<Cell> poolcells = pool.GetCells(cellSelection).ToList();
                    double[,] voltageArray = new double[poolcells.Count, timeArray.Length];
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = poolcells.Min(c => c.MinCurrentValue(iStart, iEnd));
                        yMax = poolcells.Max(c => c.MaxCurrentValue(iStart, iEnd));
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
            bool gap, bool chem, UnitOfMeasure UoM)
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
            if (chem)
            {
                (Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents) = c.GetIncomingSynapticCurrents();
                if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Incoming Syn. Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Incoming Syn. Currents", colors, AffarentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));

                (colors, Dictionary<string, List<double>> EfferentCurrents) = c.GetOutgoingSynapticCurrents();
                if (c.CellPool.PositionLeftRight == SagittalPlane.Left)
                    leftImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Outgoing Syn. Currents", colors, EfferentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
                else
                    rightImages.Add(UtilWindows.CreateCurrentsPlot(c.ID, c.ID + " Outgoing Syn. Currents", colors, EfferentCurrents, timeArray, iStart, iEnd, yAxis, yMin, yMax));
            }
            return (leftImages, rightImages);
        }

        private static (List<Image>, List<Image>) PlotCurrents(double[] timeArray, List<Cell> cells, List<CellPool> pools, PlotSelectionMultiCells cellSelection,
            int iStart, int iEnd,
            bool gap, bool chem, UnitOfMeasure UoM)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            if (cells != null)
            {
                double yMin = cells.Min(c => c.MinCurrentValue(iStart, iEnd));
                double yMax = cells.Max(c => c.MaxCurrentValue(iStart, iEnd));
                foreach (Cell c in cells)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = gap && !chem ? c.MinGapCurrentValue(iStart, iEnd) :
                            !gap && chem ? c.MinSynInCurrentValue(iStart, iEnd) + c.MinSynOutCurrentValue(iStart, iEnd) :
                            c.MinCurrentValue(iStart, iEnd);
                        yMax = gap && !chem ? c.MaxGapCurrentValue(iStart, iEnd) :
                            !gap && chem ? c.MaxSynInCurrentValue(iStart, iEnd) + c.MaxSynOutCurrentValue(iStart, iEnd) :
                            c.MaxCurrentValue(iStart, iEnd);
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    (List<Image> subLeftImages, List < Image > subRightImages) = PlotCurrents(timeArray, c, iStart, iEnd, yMin, yMax, gap, chem, UoM);
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
                        if (!GlobalSettings.SameYAxis)
                        {
                            yMin = gap && !chem ? c.MinGapCurrentValue(iStart, iEnd) :
                                !gap && chem ? c.MinSynInCurrentValue(iStart, iEnd) + c.MinSynOutCurrentValue(iStart, iEnd) :
                                c.MinCurrentValue(iStart, iEnd);
                            yMax = gap && !chem ? c.MaxGapCurrentValue(iStart, iEnd) :
                                !gap && chem ? c.MaxSynInCurrentValue(iStart, iEnd) + c.MaxSynOutCurrentValue(iStart, iEnd) :
                                c.MaxCurrentValue(iStart, iEnd);
                            Util.SetYRange(ref yMin, ref yMax);
                        }
                        (List<Image> subLeftImages, List<Image> subRightImages) = PlotCurrents(timeArray, c, iStart, iEnd, yMin, yMax, gap, chem, UoM);
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
                    List<Cell> poolcells = pool.GetCells(cellSelection).ToList();
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
        private static (List<Image>, List<Image>) PlotFullDynamics(double[] timeArray, List<Cell> cells, List<CellPool> pools, 
            PlotSelectionMultiCells cellSelection, int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<Image> leftImages = new();
            List<Image> rightImages = new();
            (List<Image> leftImagesSub, List<Image> rightImagesSub) = PlotMembranePotentials(timeArray, cells, pools, cellSelection, iStart, iEnd);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            (leftImagesSub, rightImagesSub) = PlotCurrents(timeArray, cells, pools, cellSelection, iStart, iEnd, gap: true, chem: true, UoM);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            (leftImagesSub, rightImagesSub) = PlotStimuli(timeArray, cells, pools, cellSelection, iStart, iEnd, UoM);
            leftImages = leftImages.Concat(leftImagesSub).ToList();
            rightImages = rightImages.Concat(rightImagesSub).ToList();

            return (leftImages, rightImages);
        }

        private static (List<Image>, List<Image>) PlotEpisodes(RunningModel model, double tStart, double tEnd)
        {
            List<Image> leftImages = new();
            int iStart = model.RunParam.iIndex(tStart);
            int iEnd = model.RunParam.iIndex(tEnd);
            //FUTURE_IMPROVEMENT color

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

            ModelFile.SaveEpisodesToCSV("Episodes.csv", 1, episodes);

            return (leftImages, null);
        }

        private static List<Image> PlotJunctionCharts(double[] TimeOffset, JunctionBase jnc, int iStart, int iEnd, UnitOfMeasure UoM)
        {
            /*TODO List<ChartDataStruct> charts = new();
             List<Cell> cells = new();
             List<GapJunction> gapJunctions = new();
             List<ChemicalSynapse> synapses = new();
             if (jnc is GapJunction gj)
             {
                 cells.Add(gj.Cell1);
                 cells.Add(gj.Cell2);
                 gapJunctions.Add(gj);
             }
             else if (jnc is ChemicalSynapse syn)
             {
                 cells.Add(syn.PreNeuron);
                 cells.Add(syn.PostCell);
                 synapses.Add(syn);
             }
             charts.AddRange(PlotMembranePotentials(TimeOffset, cells, null, null, iStart, iEnd));
             charts.AddRange(PlotCurrents(TimeOffset, gapJunctions, synapses, iStart, iEnd, UoM));
             return charts;*/
            return null;
        }
        public static (List<Image>, List<Image>) Plot(PlotType PlotType, RunningModel model, List<Cell> Cells, List<CellPool> Pools,
            PlotSelectionInterface cellSelectionInterface, int tStart = 0, int tEnd = -1)
        {
            if (PlotType != PlotType.Episodes &&
                (Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return (null, null);
            UnitOfMeasure uom = model.Settings.UoM;
            double dt = model.RunParam.DeltaT;
            int tSkip = model.RunParam.SkipDuration;
            int iStart = (int)((tStart + tSkip) / dt);
            if (tEnd < 0)
                tEnd = (int)model.TimeArray.Last();
            int iEnd = (int)((tEnd + tSkip) / dt);
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
                        return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: true, chem: true, uom);
                    case PlotType.GapCurrent:
                        return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: true, chem: false, uom);
                    case PlotType.ChemCurrent:
                        return PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, gap: false, chem: true, uom);
                    case PlotType.Stimuli:
                        return PlotStimuli(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, uom);
                    case PlotType.FullDyn:
                        return PlotFullDynamics(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, uom);
                    case PlotType.Episodes:
                        return PlotEpisodes(model, tStart, tEnd);
                    default:
                        break;
                }
            }
            return (null, null);
        }

    }
}
