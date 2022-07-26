using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.ModelUnits;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
                charts.AddRange(LineChartGenerator.CreatePotentialsMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
            if (pools != null)
                charts.AddRange(LineChartGenerator.CreatePotentialsMultiPlot(pools, cellSelection, TimeOffset, iStart, iEnd, ref chartindex));

            return LineChartGenerator.PlotCharts(filename, "Potentials", charts, numColumns: 2);
        }

        private static string PlotCurrents(double[] TimeOffset, List<Cell> cells, List<CellPool> pools, CellSelectionStruct cellSelection,
            int iStart, int iEnd, bool includeGap = true, bool includeChem = true, string filename = "")
        {
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
                charts.AddRange(LineChartGenerator.CreateCurrentsMultiPlot(cells, TimeOffset, iStart, iEnd, includeGap: includeGap, includeChem: includeChem, ref chartindex));
            if (pools != null)
                charts.AddRange(LineChartGenerator.CreateCurrentsMultiPlot(pools, cellSelection,TimeOffset,  iStart, iEnd, includeGap: includeGap, includeChem: includeChem, ref chartindex));
            return LineChartGenerator.PlotCharts(filename, "Currents", charts, numColumns: 2);
        }

        private static string PlotStimuli(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd,
            CellSelectionStruct cellSelection, string filename = "")
        {            
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
                charts.AddRange(LineChartGenerator.CreateStimuliMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
            if (pools != null)
                charts.AddRange(LineChartGenerator.CreateStimuliMultiPlot(pools, TimeOffset, iStart, iEnd, cellSelection, ref chartindex));
            return LineChartGenerator.PlotCharts(filename, "Stimuli", charts, numColumns: 2);
        }

        private static string PlotFullDynamics(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd,
            CellSelectionStruct cellSelection, string filename = "")
        {
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
            {
                charts.AddRange(LineChartGenerator.CreatePotentialsMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
                charts.AddRange(LineChartGenerator.CreateCurrentsMultiPlot(cells, TimeOffset, iStart, iEnd, includeGap: true, includeChem: true, ref chartindex));
                charts.AddRange(LineChartGenerator.CreateStimuliMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
            }
            if (pools != null)
            {
                charts.AddRange(LineChartGenerator.CreatePotentialsMultiPlot(pools, cellSelection, TimeOffset, iStart, iEnd, ref chartindex));
                charts.AddRange(LineChartGenerator.CreateCurrentsMultiPlot(pools, cellSelection, TimeOffset, iStart, iEnd, includeGap: true, includeChem: true, ref chartindex));
                charts.AddRange(LineChartGenerator.CreateStimuliMultiPlot(pools, TimeOffset, iStart, iEnd, cellSelection, ref chartindex));
            }
            return LineChartGenerator.PlotCharts(filename, "Full Dynamics", charts, numColumns: 2);
        }

        private static string PlotEpisodes(SwimmingModel model, double tStart, double tEnd)
        {
            List<string> charts = new();
            int iStart = model.runParam.iIndex(tStart);
            int iEnd = model.runParam.iIndex(tEnd);
            int chartIndex = 0;

            (Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes) = model.GetSwimmingEpisodes(-0.5, 0.5, 1000);

            double[] Time = model.TimeArray[iStart..(iEnd - iStart + 1)];
            double[] xValues = Time;
            double[] yValues = tail_tip_coord[iStart..(iEnd - iStart + 1)].Select(c => c.X).ToArray();
            string chart = LineChartGenerator.CreateLinePlot(xValues, yValues, chartIndex++, "Y-Coordinate", "Tail Movement", Color.Red.ToRGB());
            charts.Add(chart);
            if (episodes.Any())
            {
                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.EpisodeDuration).ToArray();
                chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Duration", "Episode Duration", Color.Red.ToRGB());
                charts.Add(chart);

                if (episodes.Count > 1)
                {
                    xValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i].End).ToArray();
                    yValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray();
                    chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Episode Intervals", "Episode Intervals", Color.Red.ToRGB());
                    charts.Add(chart);
                }

                xValues = episodes.SelectMany(e => e.Beats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InstantFequency).ToArray();
                chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Instantenous Frequency", "Instantenous Frequency", Color.Red.ToRGB());
                charts.Add(chart);

                xValues = episodes.SelectMany(e => e.InlierBeats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InlierInstantFequency).ToArray();
                chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Instantenous Frequency", "Instantenous Frequency (Outliers Removed)", Color.Red.ToRGB());
                charts.Add(chart);

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.BeatFrequency).ToArray();
                chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Frequency", "Tail Beat Freq.", Color.Red.ToRGB());
                charts.Add(chart);
            }

            return LineChartGenerator.PlotCharts("", "Episodes", charts, 2);
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
                case PlotType.BodyAngleHeatMap://TODO
                    break;

            }
            return PlotHTML;
        }

    }
}
