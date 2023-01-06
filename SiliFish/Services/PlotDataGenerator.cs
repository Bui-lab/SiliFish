using SiliFish;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services
{

    public class PlotDataGenerator
    {
        public static List<ChartDataStruct> CreateMembranePotentialCharts(double[] TimeArray, List<Cell> cells, bool groupByPool,
            int iStart, int iEnd)
        {
            List<ChartDataStruct> charts = new();
            if (cells == null || !cells.Any())
                return charts;

            double yMin = cells.Min(c => c.MinPotentialValue(iStart, iEnd));
            double yMax = cells.Max(c => c.MaxPotentialValue(iStart, iEnd));
            Helpers.Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = cells.GroupBy(c => groupByPool ? c.CellPool.ID : c.ID);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string columnTitles = "Time,";
                List<string> data = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString("0.0#") + ","));
                List<string> colorPerChart = new();
                foreach (Cell cell in cellGroup)
                {
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += cell.V?[iStart + i].ToString(Settings.DecimalPointFormat) + ",";
                }
                string csvData = "`" + columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
                charts.Add(new ChartDataStruct
                {
                    CsvData = csvData,
                    Color = string.Join(',', colorPerChart),
                    Title = $"`{cellGroup.Key} Membrane Potential`",
                    yLabel = "`Memb. Potential (mV)`",
                    yMin = yMin,
                    yMax = yMax,
                    xMin = TimeArray[iStart],
                    xMax = (TimeArray[iEnd] + 1)
                });
            }
            return charts;
        }


        private static List<ChartDataStruct> CreateCurrentCharts(double[] TimeArray, List<Cell> cells, bool groupByPool,
            int iStart, int iEnd, bool includeGap = true, bool includeChem = true)
        {
            List<ChartDataStruct> gapCharts = new();
            List<ChartDataStruct> synCharts = new();
            double yMin = cells.Min(c => c.MinCurrentValue(iStart, iEnd));
            double yMax = cells.Max(c => c.MaxCurrentValue(iStart, iEnd));
            Helpers.Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = cells.GroupBy(c => groupByPool ? c.CellPool.ID : c.ID);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string gapTitle = "Time,";
                string synInTitle = "Time,";
                string synOutTitle = "Time,";
                List<string> gapData = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString("0.0#") + ","));
                List<string> synInData = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString("0.0#") + ","));
                List<string> synOutData = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString("0.0#") + ","));
                List<string> colorPerGapChart = new();
                List<string> colorPerInSynChart = new();
                List<string> colorPerOutSynChart = new();
                bool gapExists = false;
                bool synInExists = false;
                bool synOutExists = false;
                foreach (Cell cell in cellGroup)
                {
                    if (includeGap)
                    {
                        foreach (GapJunction jnc in cell.GapJunctions.Where(j => j.Cell2 == cell || j.Cell1 == cell))
                        {
                            gapExists = true;
                            Cell otherCell = jnc.Cell1 == cell ? jnc.Cell2 : jnc.Cell1;
                            gapTitle += "Gap: " + otherCell.ID + ",";
                            colorPerGapChart.Add(otherCell.CellPool.Color.ToRGBQuoted());
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                gapData[i] += jnc.InputCurrent[iStart + i].ToString(Settings.DecimalPointFormat) + ",";
                        }
                    }
                    if (includeChem)
                    {
                        List<ChemicalSynapse> synapses = null;
                        if (cell is Neuron neuron) synapses = neuron.Synapses;
                        else if (cell is MuscleCell muscleCell) synapses = muscleCell.EndPlates;
                        foreach (ChemicalSynapse jnc in synapses)
                        {
                            synInExists = true;
                            synInTitle += jnc.PreNeuron.ID + ",";
                            colorPerInSynChart.Add(jnc.PreNeuron.CellPool.Color.ToRGBQuoted());
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                synInData[i] += jnc.InputCurrent[iStart + i].ToString(Settings.DecimalPointFormat) + ",";
                        }
                        if (cell is Neuron neuron2)
                        {
                            foreach (ChemicalSynapse jnc in neuron2.Terminals)
                            {
                                synOutExists = true;
                                synOutTitle += jnc.PostCell.ID + ",";
                                colorPerOutSynChart.Add(jnc.PostCell.CellPool.Color.ToRGBQuoted());
                                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                    synOutData[i] += jnc.InputCurrent[iStart + i].ToString(Settings.DecimalPointFormat) + ",";
                            }
                        }
                    }
                }
                if (gapExists)
                {
                    string csvData = "`" + gapTitle[..^1] + "\n" + string.Join("\n", gapData.Select(line => line[..^1]).ToArray()) + "`";
                    gapCharts.Add(new ChartDataStruct
                    {
                        CsvData = csvData,
                        Color = string.Join(',', colorPerGapChart),
                        Title = $"`{cellGroup.Key} Gap Currents`",
                        yLabel = $"`Current ({Util.GetUoM(Settings.UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = TimeArray[iStart],
                        xMax = (TimeArray[iEnd] + 1)
                    });
                }
                if (synInExists)
                {
                    string csvData = "`" + synInTitle[..^1] + "\n" + string.Join("\n", synInData.Select(line => line[..^1]).ToArray()) + "`";
                    synCharts.Add(new ChartDataStruct
                    {
                        CsvData = csvData,
                        Color = string.Join(',', colorPerInSynChart),
                        Title = $"`{cellGroup.Key} Incoming Synaptic Currents`",
                        yLabel = $"`Current ({Util.GetUoM(Settings.UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = TimeArray[iStart],
                        xMax = (TimeArray[iEnd] + 1)
                    });
                }
                if (synOutExists)
                {
                    string csvData = "`" + synOutTitle[..^1] + "\n" + string.Join("\n", synOutData.Select(line => line[..^1]).ToArray()) + "`";
                    synCharts.Add(new ChartDataStruct
                    {
                        CsvData = csvData,
                        Color = string.Join(',', colorPerOutSynChart),
                        Title = $"`{cellGroup.Key} Outgoing Synaptic Currents`",
                        yLabel = $"`Current ({Util.GetUoM(Settings.UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = TimeArray[iStart],
                        xMax = (TimeArray[iEnd] + 1)
                    });
                }
            }
            return gapCharts.Union(synCharts).ToList();
        }

        private static List<ChartDataStruct> CreateStimuliCharts(double[] TimeArray, List<Cell> cells, bool groupByPool,
            int iStart, int iEnd)
        {
            List<ChartDataStruct> charts = new();
            if (cells == null || !cells.Any())
                return charts;

            double yMin = cells.Min(c => c.MinStimulusValue);
            double yMax = cells.Max(c => c.MaxStimulusValue);
            Helpers.Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = cells.GroupBy(c => groupByPool ? c.CellPool.ID : c.ID);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string title = "Time,";
                List<string> data = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString("0.0#") + ","));
                List<string> colorPerChart = new();
                bool stimExists = false;
                foreach (Cell cell in cellGroup.Where(c => c.Stimuli.HasStimulus))
                {
                    stimExists = true;
                    title += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += cell.Stimuli.GetStimulus(iStart + i).ToString(Settings.DecimalPointFormat) + ",";
                }
                if (stimExists)
                {
                    string csvData = "`" + title[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
                    charts.Add(new ChartDataStruct
                    {
                        CsvData = csvData,
                        Color = string.Join(',', colorPerChart),
                        Title = $"`{cellGroup.Key} Applied Stimuli`",
                        yLabel = $"`Stimulus ({Util.GetUoM(Settings.UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = TimeArray[iStart],
                        xMax = (TimeArray[iEnd] + 1)
                    });
                }
            }
            return charts;
        }

        private static List<ChartDataStruct> CreateTensionCharts(double[] TimeArray, List<MuscleCell> cells, bool groupByPool,
            int iStart, int iEnd)
        {
            List<ChartDataStruct> charts = new();

            double yMin = 0;
            double yMax = 1.2;
            Helpers.Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, MuscleCell>> cellGroups = cells.GroupBy(c => groupByPool ? c.CellPool.ID : c.ID);
            foreach (IGrouping<string, MuscleCell> cellGroup in cellGroups)
            {
                string title = "Time,";
                List<string> data = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString("0.0#") + ","));
                List<string> colorPerChart = new();
                foreach (MuscleCell cell in cellGroup)
                {
                    double[] Tension = cell.RelativeTension;
                    title += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += Tension[iStart + i].ToString(Settings.DecimalPointFormat) + ",";
                }
                string csvData = "`" + title[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
                charts.Add(new ChartDataStruct
                {
                    CsvData = csvData,
                    Color = string.Join(',', colorPerChart),
                    Title = $"`{cellGroup.Key} Relative Tension`",
                    yLabel = "`Relative Tension`",
                    yMin = yMin,
                    yMax = yMax,
                    xMin = TimeArray[iStart],
                    xMax = (TimeArray[iEnd] + 1)
                });
            }
            return charts;
        }

        private static List<ChartDataStruct> CreateFullDynamicsCharts(double[] TimeOffset, List<Cell> cells, bool groupByPool,
            int iStart, int iEnd)
        {
            List<ChartDataStruct> charts = new();
            charts.AddRange(CreateMembranePotentialCharts(TimeOffset, cells, groupByPool, iStart, iEnd));
            charts.AddRange(CreateCurrentCharts(TimeOffset, cells, groupByPool, iStart, iEnd, includeGap: true, includeChem: true));
            charts.AddRange(CreateStimuliCharts(TimeOffset, cells, groupByPool, iStart, iEnd));
            List<MuscleCell> muscleCells = cells.Where(c => c is MuscleCell).Select(c => (MuscleCell)c).ToList();
            if (muscleCells.Any())
                charts.AddRange(CreateTensionCharts(TimeOffset, muscleCells, groupByPool, iStart, iEnd));
            return charts;
        }

        private static List<ChartDataStruct> CreateEpisodeCharts(SwimmingModel model, int iStart, int iEnd)
        {
            List<ChartDataStruct> charts = new();

            (Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes) = SwimmingModelKinematics.GetSwimmingEpisodesUsingMuscleCells(model);

            double[] Time = model.TimeArray[iStart..(iEnd + 1)];
            double[] xValues = Time;
            double[] yValues = tail_tip_coord[iStart..(iEnd + 1)].Select(c => c.X).ToArray();

            //Tail Movement
            string title = "Time,Y-Axis";
            string[] data = new string[iEnd - iStart + 2];
            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                data[i] = xValues[i] + "," + yValues[i];
            string csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
            charts.Add(new ChartDataStruct
            {
                CsvData = csvData,
                Title = $"`Tail Movement`",
                yLabel = "`Y-Coordinate`",
                xMin = Time[0],
                xMax = (Time[^1] + 1)
            });

            if (episodes.Any())
            {
                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.EpisodeDuration).ToArray();
                title = "Time,Episode Duration";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new ChartDataStruct
                {
                    CsvData = csvData,
                    Title = $"`Episode Duration`",
                    yLabel = "`Duration (ms)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = (Time[^1] + 1)
                });


                if (episodes.Count > 1)
                {
                    xValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i].End).ToArray();
                    yValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray();
                    title = "Time,Episode Intervals";
                    data = new string[iEnd - iStart + 2];
                    foreach (int i in Enumerable.Range(0, episodes.Count - 1))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    charts.Add(new ChartDataStruct
                    {
                        CsvData = csvData,
                        Title = $"`Episode Intervals`",
                        yLabel = "`Interval (ms)`",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = (Time[^1] + 1)
                    });
                }

                xValues = episodes.SelectMany(e => e.Beats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InstantFequency).ToArray();
                title = "Time,Instant. Freq.";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, xValues.Length))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new ChartDataStruct
                {
                    CsvData = csvData,
                    Title = $"`Instantenous Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = (Time[^1] + 1)
                });

                xValues = episodes.SelectMany(e => e.InlierBeats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InlierInstantFequency).ToArray();
                title = "Time,Instant. Freq.";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, xValues.Length))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new ChartDataStruct
                {
                    CsvData = csvData,
                    Title = $"`Instantenous Frequency (Outliers Removed)`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = (Time[^1] + 1)
                });

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.BeatFrequency).ToArray();
                title = "Time,Tail Beat Freq.";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new ChartDataStruct
                {
                    CsvData = csvData,
                    Title = $"`Tail Beat Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = (Time[^1] + 1)
                });

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => (double)e.Beats.Count).ToArray();
                title = "Time,Tail Beat/Episode";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new ChartDataStruct
                {
                    CsvData = csvData,
                    Title = $"`Tail Beat/Episode`",
                    yLabel = "`Count`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = (Time[^1] + 1)
                });
            }

            return charts;
        }

       public static (string, List<ChartDataStruct>) GetPlotData(PlotType PlotType, SwimmingModel model, List<Cell> Cells, List<CellPool> Pools,
            CellSelectionStruct cellSelection, int tStart = 0, int tEnd = -1, int tSkip = 0)
        {
            if (PlotType != PlotType.Episodes &&
                (Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return (null, null);
            double dt = model.runParam.dt;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            if (iEnd < iStart || iEnd >= model.TimeArray.Length)
                iEnd = model.TimeArray.Length - 1;

            bool groupByPool = false;
            if (Cells == null || !Cells.Any())
            {
                Cells = Pools.OrderBy(p => p.ID).SelectMany(p => p.GetCells(cellSelection)).ToList();
                groupByPool = true;
            }
            string Title = "";
            List<ChartDataStruct> charts = new();
            switch (PlotType)
            {
                case PlotType.MembPotential:
                    Title = "Membrane Potentials";
                    charts = CreateMembranePotentialCharts(model.TimeArray, Cells, groupByPool, iStart, iEnd);
                    break;
                case PlotType.Current:
                    Title = "Incoming Currents";
                    charts = CreateCurrentCharts(model.TimeArray, Cells, groupByPool, iStart, iEnd, includeGap: true, includeChem: true);
                    break;
                case PlotType.GapCurrent:
                    Title = "Incoming Gap Currents";
                    charts = CreateCurrentCharts(model.TimeArray, Cells, groupByPool, iStart, iEnd, includeGap: true, includeChem: false);
                    break;
                case PlotType.ChemCurrent:
                    Title = "Incoming Synaptic Currents";
                    charts = CreateCurrentCharts(model.TimeArray, Cells, groupByPool, iStart, iEnd, includeGap: false, includeChem: true);
                    break;
                case PlotType.Stimuli:
                    Title = "Applied Stimulus";
                    charts = CreateStimuliCharts(model.TimeArray, Cells, groupByPool, iStart, iEnd);
                    break;
                case PlotType.FullDyn:
                    Title = "Full Dynamics";
                    charts = CreateFullDynamicsCharts(model.TimeArray, Cells, groupByPool, iStart, iEnd);
                    break;
                case PlotType.Episodes:
                    Title = "Tail Beat Episodes";
                    charts = CreateEpisodeCharts(model, iStart, iEnd);
                    break;
            }
            return (Title, charts);
        }

    }
}
