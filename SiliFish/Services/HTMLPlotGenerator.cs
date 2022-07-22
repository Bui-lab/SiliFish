using SiliFish.DataTypes;
using SiliFish.ModelUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services
{
    public class HTMLPlotGenerator
    {
        private static string PlotMembranePotentials(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,CellSelectionStruct cellSelection,
            int iStart, int iEnd, string filename = "")
        {
            LineChartGenerator lc = new();
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
                charts.AddRange(lc.CreatePotentialsMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
            if (pools != null)
                charts.AddRange(lc.CreatePotentialsMultiPlot(pools, cellSelection, TimeOffset, iStart, iEnd, ref chartindex));

            return lc.PlotCharts(filename, "Potentials", charts, numColumns: 2);
        }

        private static string PlotCurrents(double[] TimeOffset, List<Cell> cells, List<CellPool> pools, CellSelectionStruct cellSelection,
            int iStart, int iEnd, bool includeGap = true, bool includeChem = true, string filename = "")
        {
            LineChartGenerator lc = new();
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
                charts.AddRange(lc.CreateCurrentsMultiPlot(cells, TimeOffset, iStart, iEnd, includeGap: includeGap, includeChem: includeChem, ref chartindex));
            if (pools != null)
                charts.AddRange(lc.CreateCurrentsMultiPlot(pools, cellSelection,TimeOffset,  iStart, iEnd, includeGap: includeGap, includeChem: includeChem, ref chartindex));
            return lc.PlotCharts(filename, "Currents", charts, numColumns: 2);
        }

        private static string PlotStimuli(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd,
            CellSelectionStruct cellSelection, string filename = "")
        {            
            LineChartGenerator lc = new();
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
                charts.AddRange(lc.CreateStimuliMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
            if (pools != null)
                charts.AddRange(lc.CreateStimuliMultiPlot(pools, TimeOffset, iStart, iEnd, cellSelection, ref chartindex));
            return lc.PlotCharts(filename, "Stimuli", charts, numColumns: 2);
        }

        private static string PlotFullDynamics(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd,
            CellSelectionStruct cellSelection, string filename = "")
        {
            LineChartGenerator lc = new();
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
            {
                charts.AddRange(lc.CreatePotentialsMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
                charts.AddRange(lc.CreateCurrentsMultiPlot(cells, TimeOffset, iStart, iEnd, includeGap: true, includeChem: true, ref chartindex));
                charts.AddRange(lc.CreateStimuliMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
            }
            if (pools != null)
            {
                charts.AddRange(lc.CreatePotentialsMultiPlot(pools, cellSelection, TimeOffset, iStart, iEnd, ref chartindex));
                charts.AddRange(lc.CreateCurrentsMultiPlot(pools, cellSelection, TimeOffset, iStart, iEnd, includeGap: true, includeChem: true, ref chartindex));
                charts.AddRange(lc.CreateStimuliMultiPlot(pools, TimeOffset, iStart, iEnd, cellSelection, ref chartindex));
            }
            return lc.PlotCharts(filename, "Full Dynamics", charts, numColumns: 2);
        }

        private static string PlotEpisodes(SwimmingModel model, double tStart, double tEnd)
        {/*
            LineChartGenerator lc = new();
            List<string> charts = new();
            int chartindex = 0;
            int iStart = model.runParam.iIndex(tStart);
            int iEnd = model.runParam.iIndex(tEnd);

            (Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes) = model.GetSwimmingEpisodes(-0.5, 0.5, 1000);
            lc.CreateStimuliSubPlot
            leftImages.Add(UtilWindows.CreateLinePlot("Tail Movement",
                tail_tip_coord.Select(c => c.X).ToArray(),
                model.TimeArray,
                iStart, iEnd, Color.Red));
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
                leftImages.Add(UtilWindows.CreateScatterPlot("Instantenous Frequency",
                    episodes.SelectMany(e => e.InstantFequency).ToArray(),
                    episodes.SelectMany(e => e.Beats.Select(b => b.beatStart)).ToArray(),
                    tStart, tEnd, Color.Red));
                leftImages.Add(UtilWindows.CreateScatterPlot("Instantenous Frequency (Outliers removed)",
                    episodes.SelectMany(e => e.InlierInstantFequency).ToArray(),
                    episodes.SelectMany(e => e.InlierBeats.Select(b => b.beatStart)).ToArray(),
                    tStart, tEnd, Color.Red));
                leftImages.Add(UtilWindows.CreateScatterPlot("Tail Beat Frequency",
                    episodes.Select(e => e.BeatFrequency).ToArray(),
                    episodes.Select(e => e.Start).ToArray(),
                    tStart, tEnd, Color.Red));

            }

            return (leftImages, null);*/
            return "";
        }



        public static string Plot( PlotType PlotType, SwimmingModel model, List<Cell> Cells, List<CellPool> Pools,
            CellSelectionStruct cellSelection, 
            int tStart = 0, int tEnd = -1, int tSkip = 0)
        {
            if (PlotType != PlotType.Episodes &&
                (Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return "";
            double dt = model.runParam.dt;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            if (iEnd < iStart || iEnd >= model.TimeArray.Length)
                iEnd = model.TimeArray.Length - 1;  
            
            string PlotHTML = "";
            //URGENT remaining PlotTypes
            switch (PlotType)
            {
                case PlotType.MembPotential:
                    PlotHTML = PlotMembranePotentials(model.TimeArray, Cells, Pools, cellSelection, iStart, iEnd);
                    break;
                case PlotType.Current:
                    PlotHTML = PlotCurrents(model.TimeArray, Cells, Pools, cellSelection, iStart, iEnd, includeGap: true, includeChem: true);
                    break;
                case PlotType.GapCurrent:
                    PlotHTML = PlotCurrents(model.TimeArray, Cells, Pools, cellSelection, iStart, iEnd, includeGap: true, includeChem: false);
                    break;
                case PlotType.ChemCurrent:
                    PlotHTML = PlotCurrents(model.TimeArray, Cells, Pools, cellSelection, iStart, iEnd, includeGap: false, includeChem: true);
                    break;
                case PlotType.Stimuli:
                    PlotHTML = PlotStimuli(model.TimeArray, Cells, Pools, iStart, iEnd, cellSelection);
                    break;
                case PlotType.FullDyn:
                    PlotHTML = PlotFullDynamics(model.TimeArray, Cells, Pools, iStart, iEnd, cellSelection);
                    break;
                case PlotType.Episodes:
                    PlotHTML = PlotEpisodes(model, tStart, tEnd);
                    break;
            }
            return PlotHTML;
        }

    }
}
