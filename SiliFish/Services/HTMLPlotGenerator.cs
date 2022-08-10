﻿using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.ModelUnits;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            //for dychart 
             if (cells != null && cells.Any())
            {
                Cell cell = cells[0];
                List<string> test = new(TimeOffset.Select(t => t.ToString("0.0#") + ","));
                string title = "Time,";
                if (cell is Neuron neuron)
                {
                    int len = TimeOffset.Count();
                    foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell2 == cell))
                    {
                    title+=jnc.Cell1.ID+",";
                        foreach (int i in Enumerable.Range(0, len))
                            test[i] += jnc.InputCurrent[i].ToString("0.#########") + ",";
                    }
                    foreach (GapJunction jnc in neuron.GapJunctions.Where(j => j.Cell1 == cell))
                    {
                    title+=jnc.Cell2.ID+",";
                        foreach (int i in Enumerable.Range(0, len))
                            test[i] += jnc.InputCurrent[i].ToString("0.#########") + ",";
                    }
                    foreach (ChemicalSynapse jnc in neuron.Synapses)
                    {
                        title += jnc.PreNeuron.ID+",";
                        foreach (int i in Enumerable.Range(0, len))
                            test[i] += jnc.InputCurrent[i].ToString("0.#########") + ",";
                    }
                    foreach (int i in Enumerable.Range(0, len))
                        test[i] = "\"" + test[i][..^1] + "\\n\" +";
                    title = "\"" + title[..^1] + "\\n\" +";
                    string testHtml = title +"\n"+ string.Join("\n", test.ToArray());
                }
            }
            
            

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
            return LineChartGenerator.PlotCharts(filename, "Full Dynamics", charts, numColumns: 1);
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
                chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Duration (ms)", "Episode Duration", Color.Red.ToRGB());
                charts.Add(chart);

                if (episodes.Count > 1)
                {
                    xValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i].End).ToArray();
                    yValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray();
                    chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Interval (ms)", "Episode Intervals", Color.Red.ToRGB());
                    charts.Add(chart);
                }

                xValues = episodes.SelectMany(e => e.Beats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InstantFequency).ToArray();
                chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Freq (Hz)", "Instantenous Frequency", Color.Red.ToRGB());
                charts.Add(chart);

                xValues = episodes.SelectMany(e => e.InlierBeats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InlierInstantFequency).ToArray();
                chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Freq (Hz)", "Instantenous Frequency (Outliers Removed)", Color.Red.ToRGB());
                charts.Add(chart);

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.BeatFrequency).ToArray();
                chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Freq (Hz)", "Tail Beat Freq.", Color.Red.ToRGB());
                charts.Add(chart);

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => (double)e.Beats.Count).ToArray();
                chart = LineChartGenerator.CreateScatterPlot(Time, xValues, yValues, chartIndex++, "Count", "Tail Beat/Episode", Color.Red.ToRGB());
                charts.Add(chart);
            }

            return LineChartGenerator.PlotCharts("", "Episodes", charts, numColumns: 1);
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
