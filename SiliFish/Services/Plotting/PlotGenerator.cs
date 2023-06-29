using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using System.Collections.Generic;
using System.Linq;
using SiliFish.ModelUnits.Junction;
using System;
using System.Runtime.InteropServices;
using SiliFish.ModelUnits.Stim;
using System.Reflection;
using SiliFish.Services.Plotting.PlotGenerators;
using SiliFish.Services.Plotting.PlotSelection;

namespace SiliFish.Services.Plotting
{
    public class PlotGenerator
    {
        public long NumOfDataPoints { get; set; } = 0;
        public string errorMessage { get; set; }

        private List<Chart> CreateFullDynamicsCharts(double[] TimeArray, List<Cell> cells,
            bool combinePools, bool combineSomites, bool combineCells,
            int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<Chart> charts = new();

            PlotGeneratorMembranePotentials plotGeneratorMP = new(this, cells, TimeArray, combinePools, combineSomites: true, combineCells: true, iStart, iEnd);
            plotGeneratorMP.CreateCharts(charts);
            if (!string.IsNullOrEmpty(errorMessage))
                return charts;

            PlotGeneratorCurrentsOfCells plotGeneratorCurrent = new(this, cells, TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd, UoM);
            plotGeneratorCurrent.CreateCharts(charts);
            if (!string.IsNullOrEmpty(errorMessage))
                return charts;

            PlotGeneratorStimuli plotGeneratorStimuli = new(this, cells, TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd, UoM);
            plotGeneratorStimuli.CreateCharts(charts);
            if (!string.IsNullOrEmpty(errorMessage))
                return charts;

            List<Cell> muscleCells = cells.Where(c => c is MuscleCell).ToList();
            if (muscleCells.Any())
            {
                PlotGeneratorTension plotGeneratorTension = new PlotGeneratorTension(this, cells, TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd);
                plotGeneratorTension.CreateCharts(charts);
            }
            return charts;
        }

        private List<Chart> CreateJunctionCharts(double[] TimeArray, List<JunctionBase> jncList, int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<Chart> charts = new();
            foreach (JunctionBase jnc in jncList)
            {
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

                PlotGeneratorMembranePotentials plotGeneratorMP = new(this, cells, TimeArray, combinePools: false, combineSomites: false, combineCells: false, iStart, iEnd);
                plotGeneratorMP.CreateCharts(charts);
                if (!string.IsNullOrEmpty(errorMessage))
                    return charts;

                PlotGeneratorCurrentsOfJunctions plotGeneratorCurrentsOfJunctions = new(this, gapJunctions, synapses, TimeArray, iStart, iEnd, UoM);
                plotGeneratorCurrentsOfJunctions.CreateCharts(charts);
                if (!string.IsNullOrEmpty(errorMessage))
                    return charts;
            }
            return charts;
        }

        //TODO write a PlotGenerator class for Tail Episode charts
        private List<Chart> CreateTailEpisodeCharts(RunningModel model, int iStart, int iEnd)
        {
            List<Chart> charts = new();

            (Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes) = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(model);

            double[] Time = model.TimeArray[iStart..(iEnd + 1)];
            double[] xValues = Time;
            double[] yValues = tail_tip_coord[iStart..(iEnd + 1)].Select(c => c.X).ToArray();

            //Tail Movement
            string title = "Time,Y-Axis";
            string[] data = new string[iEnd - iStart + 2];
            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                data[i] = xValues[i] + "," + yValues[i];
            string csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
            charts.Add(new Chart
            {
                CsvData = csvData,
                Title = $"`Tail Movement`",
                yLabel = "`Y-Coordinate`",
                xMin = Time[0],
                xMax = Time[^1] + 1,
                yMin = yValues.Min() - 1,
                yMax = yValues.Max() + 1
            });

            if (episodes.Any())
            {
                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.EpisodeDuration).ToArray();
                title = "Time,Episode Duration";
                data = new string[episodes.Count];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new Chart
                {
                    CsvData = csvData,
                    Title = $"`Episode Duration`",
                    yLabel = "`Duration (ms)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });


                if (episodes.Count > 1)
                {
                    xValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i].End).ToArray();
                    yValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray();
                    title = "Time,Episode Intervals";
                    data = new string[episodes.Count];
                    foreach (int i in Enumerable.Range(0, episodes.Count - 1))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    charts.Add(new Chart
                    {
                        CsvData = csvData,
                        Title = $"`Episode Intervals`",
                        yLabel = "`Interval (ms)`",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1
                    });
                }

                xValues = episodes.SelectMany(e => e.Beats.Select(b => b.BeatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InstantFequency).ToArray();
                title = "Time,Instant. Freq.";
                data = new string[xValues.Length];
                foreach (int i in Enumerable.Range(0, xValues.Length))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new Chart
                {
                    CsvData = csvData,
                    Title = $"`Instantenous Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });

                xValues = episodes.SelectMany(e => e.InlierBeats.Select(b => b.BeatStart)).ToArray();
                if (xValues.Any())
                {
                    yValues = episodes.SelectMany(e => e.InlierInstantFequency).ToArray();
                    title = "Time,Instant. Freq.";
                    data = new string[xValues.Length];
                    foreach (int i in Enumerable.Range(0, xValues.Length))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    charts.Add(new Chart
                    {
                        CsvData = csvData,
                        Title = $"`Instantenous Frequency (Outliers Removed)`",
                        yLabel = "`Freq (Hz)`",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1
                    });
                }

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.BeatFrequency).ToArray();
                title = "Time,Tail Beat Freq.";
                data = new string[episodes.Count];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new Chart
                {
                    CsvData = csvData,
                    Title = $"`Tail Beat Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => (double)e.Beats.Count).ToArray();
                title = "Time,Tail Beat/Episode";
                data = new string[episodes.Count];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new Chart
                {
                    CsvData = csvData,
                    Title = $"`Tail Beat/Episode`",
                    yLabel = "`Count`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });
            }

            return charts;
        }

        //TODO write a PlotGenerator class for MN Episode charts
        private List<Chart> CreateMNEpisodeCharts(RunningModel model, int somiteNumber, int iStart, int iEnd)
        {
            List<Chart> charts = new();

            (List<Cell> LeftMNs, List<Cell> RightMNs) = model.GetMotoNeurons(somiteNumber);

            (double[] mnMaxPotentials, List<SwimmingEpisode> episodes) = SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(model, LeftMNs, RightMNs);

            double[] Time = model.TimeArray[iStart..(iEnd + 1)];
            double[] xValues = Time;
            double[] yValues = mnMaxPotentials;

            //Tail Movement
            string title = "Time,Y-Axis";
            string[] data = new string[iEnd - iStart + 2];
            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                data[i] = xValues[i] + "," + yValues[i];
            string csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
            charts.Add(new Chart
            {
                CsvData = csvData,
                Title = $"`Tail Movement`",
                yLabel = "`Y-Coordinate`",
                xMin = Time[0],
                xMax = Time[^1] + 1,
                yMin = yValues.Min() - 1,
                yMax = yValues.Max() + 1
            });

            if (episodes.Any())
            {
                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.EpisodeDuration).ToArray();
                title = "Time,Episode Duration";
                data = new string[episodes.Count];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new Chart
                {
                    CsvData = csvData,
                    Title = $"`Episode Duration`",
                    yLabel = "`Duration (ms)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });


                if (episodes.Count > 1)
                {
                    xValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i].End).ToArray();
                    yValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray();
                    title = "Time,Episode Intervals";
                    data = new string[episodes.Count];
                    foreach (int i in Enumerable.Range(0, episodes.Count - 1))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    charts.Add(new Chart
                    {
                        CsvData = csvData,
                        Title = $"`Episode Intervals`",
                        yLabel = "`Interval (ms)`",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1
                    });
                }

                xValues = episodes.SelectMany(e => e.Beats.Select(b => b.BeatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InstantFequency).ToArray();
                title = "Time,Instant. Freq.";
                data = new string[xValues.Length];
                foreach (int i in Enumerable.Range(0, xValues.Length))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new Chart
                {
                    CsvData = csvData,
                    Title = $"`Instantenous Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });

                xValues = episodes.SelectMany(e => e.InlierBeats.Select(b => b.BeatStart)).ToArray();
                if (xValues.Any())
                {
                    yValues = episodes.SelectMany(e => e.InlierInstantFequency).ToArray();
                    title = "Time,Instant. Freq.";
                    data = new string[xValues.Length];
                    foreach (int i in Enumerable.Range(0, xValues.Length))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    charts.Add(new Chart
                    {
                        CsvData = csvData,
                        Title = $"`Instantenous Frequency (Outliers Removed)`",
                        yLabel = "`Freq (Hz)`",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1
                    });
                }

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.BeatFrequency).ToArray();
                title = "Time,Tail Beat Freq.";
                data = new string[episodes.Count];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new Chart
                {
                    CsvData = csvData,
                    Title = $"`Tail Beat Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => (double)e.Beats.Count).ToArray();
                title = "Time,Tail Beat/Episode";
                data = new string[episodes.Count];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new Chart
                {
                    CsvData = csvData,
                    Title = $"`Tail Beat/Episode`",
                    yLabel = "`Count`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });
            }

            return charts;
        }

        public (string, List<Chart>) GetPlotData(PlotDefinition Plot, RunningModel model, List<Cell> Cells, List<CellPool> Pools, 
            int tStart = 0, int tEnd = -1)
        {
            errorMessage = string.Empty;
            List<Chart> charts = new();
            double dt = model.RunParam.DeltaT;
            int tSkip = model.RunParam.SkipDuration;
            int iStart = (int)((tStart + tSkip) / dt);
            if (tEnd < 0) tEnd = model.RunParam.MaxTime;
            int iEnd = (int)((tEnd + tSkip) / dt);
            if (iEnd < iStart || iEnd >= model.TimeArray.Length)
                iEnd = model.TimeArray.Length - 1;

            UnitOfMeasure UoM = model.Settings.UoM;
            bool combineCells = false;
            bool combineSomites = false;
            bool combinePools = false;
            bool combineJunctions = false;

            if (Plot.Selection is PlotSelectionUnits unitSelection)
            {
                List<JunctionBase> junctions = unitSelection.Units.Where(x => x is JunctionBase).Cast<JunctionBase>().ToList();
                if (junctions.Any())
                {
                    charts = CreateJunctionCharts(model.TimeArray, junctions, iStart, iEnd, UoM);
                    return ("Junction", charts);
                }
                List<Stimulus> stims = unitSelection.Units.Where(x => x is Stimulus).Cast<Stimulus>().ToList();
                if (stims.Any())
                {
                    List<Cell> targetCells = stims.Select(s => s.TargetCell).ToList();
                    PlotGeneratorStimuli plotGeneratorStimuli = new(this, targetCells, model.TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd, UoM);
                    plotGeneratorStimuli.CreateCharts(charts);
                    return ("Stimuli", charts);
                }
                //All other cases: Cells and Pools are pre-populated, and Plot.PlotType is taken into consideration
            }

            if (Plot.PlotType != PlotType.EpisodesTail &&
                Plot.PlotType != PlotType.EpisodesMN &&
                (Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return (null, null);
            if (Plot.Selection is PlotSelectionMultiCells cellSelection)
            {
                combinePools = cellSelection.CombinePools;
                combineCells = cellSelection.CombineCells;
                combineSomites = cellSelection.CombineSomites;
                combineJunctions = cellSelection.CombineJunctions;
            }
            string Title = "";
            switch (Plot.PlotType)
            {
                case PlotType.MembPotential:
                    Title = "Membrane Potentials";
                    PlotGeneratorMembranePotentials pg0 = new(this, Cells, model.TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd);
                    pg0.CreateCharts(charts);
                    break;
                case PlotType.Current:
                    Title = "Incoming Currents";
                    PlotGeneratorCurrentsOfCells pg1 = new(this, Cells, model.TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd, UoM, 
                        includeGap: true, includeChemIn: true, includeChemOut: false, combineJunctions: combineJunctions);
                    pg1.CreateCharts(charts);
                    break;
                case PlotType.GapCurrent:
                    Title = "Incoming Gap Currents";
                    PlotGeneratorCurrentsOfCells pg2 = new(this, Cells, model.TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd, UoM, 
                        includeGap: true, includeChemIn: false, includeChemOut: false, combineJunctions: combineJunctions);
                    pg2.CreateCharts(charts);
                    break;
                case PlotType.ChemCurrent:
                    Title = "Incoming Synaptic Currents";
                    PlotGeneratorCurrentsOfCells pg3 = new(this, Cells, model.TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd, UoM,
                        includeGap: false, includeChemIn: true, includeChemOut: false, combineJunctions: combineJunctions);
                    pg3.CreateCharts(charts);
                    break;
                case PlotType.ChemOutCurrent:
                    Title = "Outgoing Synaptic Currents";
                    PlotGeneratorCurrentsOfCells pg4 = new(this, Cells, model.TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd, UoM,
                        includeGap: false, includeChemIn: false, includeChemOut: true, combineJunctions: combineJunctions);
                    pg4.CreateCharts(charts);
                    break;
                case PlotType.Stimuli:
                    Title = "Applied Stimulus";
                    PlotGeneratorStimuli pg5 = new PlotGeneratorStimuli(this, Cells, model.TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd, UoM);
                    pg5.CreateCharts(charts);
                    break;
                case PlotType.FullDyn:
                    Title = "Full Dynamics";
                    charts = CreateFullDynamicsCharts(model.TimeArray, Cells, combinePools, combineSomites, combineCells, iStart, iEnd, UoM);
                    break;
                case PlotType.Tension:
                    Title = "Muscle Tension";
                    List<Cell> muscleCells = Cells.Where(c => c is MuscleCell).ToList();
                    if (muscleCells.Any())
                    {
                        PlotGeneratorTension plotGeneratorTension = new PlotGeneratorTension(this, muscleCells, model.TimeArray, combinePools, combineSomites, combineCells, iStart, iEnd);
                        plotGeneratorTension.CreateCharts(charts);
                    }
                    break;
                case PlotType.EpisodesTail:
                    Title = "Tail Beat Episodes";
                    charts = CreateTailEpisodeCharts(model, iStart, iEnd);
                    break;
                case PlotType.EpisodesMN:
                    Title = "Motoneuron Episodes";
                    int somite = model.ModelDimensions.NumberOfSomites;
                    if (Plot.Selection is PlotSelectionSomite pss)
                        somite = pss.Somite;
                    charts = CreateMNEpisodeCharts(model, somite, iStart, iEnd);
                    break;
                default:
                    charts = null;
                    break;
            }
            return (Title, charts);
        }

    }
}