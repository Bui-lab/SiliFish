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

namespace SiliFish.Services.Plotting
{

    public class PlotDataGenerator
    {

        public static List<ChartDataStruct> CreateMembranePotentialCharts(double[] TimeArray, List<Cell> cells, 
            bool combinePools, bool combineSomites, bool combineCells,
            int iStart, int iEnd)
        {
            List<ChartDataStruct> charts = new();
            if (cells == null || !cells.Any())
                return charts;

            double yMin = cells.Min(c => c.MinPotentialValue(iStart, iEnd));
            double yMax = cells.Max(c => c.MaxPotentialValue(iStart, iEnd));
            Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string columnTitles = "Time,";
                List<string> data = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> colorPerChart = new();
                foreach (Cell cell in cellGroup)
                {
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += cell.V?[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                }
                if (!GlobalSettings.SameYAxis)
                {
                    yMin = cellGroup.Min(c => c.MinPotentialValue(iStart, iEnd));
                    yMax = cellGroup.Max(c => c.MaxPotentialValue(iStart, iEnd));
                    Util.SetYRange(ref yMin, ref yMax);
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
                    xMax = TimeArray[iEnd] + 1
                });
            }
            return charts;
        }
        private static List<ChartDataStruct> CreateCurrentCharts(double[] TimeArray,
             List<Cell> cells,
            bool combinePools, bool combineSomites, bool combineCells,
             int iStart, int iEnd,
             UnitOfMeasure UoM,
             bool includeGap = true, bool includeChemIn = true, bool includeChemOut = true)
        {
            if (cells==null || !cells.Any()) return new List<ChartDataStruct>();
            List<ChartDataStruct> gapCharts = new();
            List<ChartDataStruct> synCharts = new();
            double yMin = cells.Min(c => c.MinCurrentValue(includeGap, includeChemIn, includeChemOut, iStart, iEnd));
            double yMax = cells.Max(c => c.MaxCurrentValue(includeGap, includeChemIn, includeChemOut, iStart, iEnd));
            Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string gapTitle = "Time,";
                string synInTitle = "Time,";
                string synOutTitle = "Time,";
                List<string> gapData = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> synInData = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> synOutData = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
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
                            string cellID = combineCells ? cell.ID + "↔" : "";
                            gapTitle += $"Gap: {cellID}{otherCell.ID},";
                            colorPerGapChart.Add(otherCell.CellPool.Color.ToRGBQuoted());
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                gapData[i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                        }
                    }
                    if (includeChemIn)
                    {
                        List<ChemicalSynapse> synapses = null;
                        if (cell is Neuron neuron) synapses = neuron.Synapses;
                        else if (cell is MuscleCell muscleCell) synapses = muscleCell.EndPlates;
                        foreach (ChemicalSynapse jnc in synapses)
                        {
                            synInExists = true;
                            if (combineCells)
                                synInTitle += cell.ID + "←";
                            synInTitle += jnc.PreNeuron.ID + ",";
                            colorPerInSynChart.Add(jnc.PreNeuron.CellPool.Color.ToRGBQuoted());
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                synInData[i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                        }
                    }
                    if (includeChemOut) 
                    { 
                        if (cell is Neuron neuron2)
                        {
                            foreach (ChemicalSynapse jnc in neuron2.Terminals)
                            {
                                synOutExists = true;
                                if (combineCells)
                                    synOutTitle += cell.ID + "→";
                                synOutTitle += jnc.PostCell.ID + ",";
                                colorPerOutSynChart.Add(jnc.PostCell.CellPool.Color.ToRGBQuoted());
                                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                    synOutData[i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                            }
                        }
                    }
                }
                if (gapExists)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinGapCurrentValue(iStart, iEnd));
                        yMax = cellGroup.Max(c => c.MaxGapCurrentValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    string csvData = "`" + gapTitle[..^1] + "\n" + string.Join("\n", gapData.Select(line => line[..^1]).ToArray()) + "`";
                    gapCharts.Add(new ChartDataStruct
                    {
                        CsvData = csvData,
                        Color = string.Join(',', colorPerGapChart),
                        Title = $"`{cellGroup.Key} Gap Currents`",
                        yLabel = $"`Current ({Util.GetUoM(UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = TimeArray[iStart],
                        xMax = TimeArray[iEnd] + 1
                    });
                }
                if (synInExists)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinSynInCurrentValue(iStart, iEnd));
                        yMax = cellGroup.Max(c => c.MaxSynInCurrentValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    string csvData = "`" + synInTitle[..^1] + "\n" + string.Join("\n", synInData.Select(line => line[..^1]).ToArray()) + "`";
                    synCharts.Add(new ChartDataStruct
                    {
                        CsvData = csvData,
                        Color = string.Join(',', colorPerInSynChart),
                        Title = $"`{cellGroup.Key} Incoming Synaptic Currents`",
                        yLabel = $"`Current ({Util.GetUoM(UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = TimeArray[iStart],
                        xMax = TimeArray[iEnd] + 1
                    });
                }
                if (synOutExists)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinSynOutCurrentValue(iStart, iEnd));
                        yMax = cellGroup.Max(c => c.MaxSynOutCurrentValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    string csvData = "`" + synOutTitle[..^1] + "\n" + string.Join("\n", synOutData.Select(line => line[..^1]).ToArray()) + "`";
                    synCharts.Add(new ChartDataStruct
                    {
                        CsvData = csvData,
                        Color = string.Join(',', colorPerOutSynChart),
                        Title = $"`{cellGroup.Key} Outgoing Synaptic Currents`",
                        yLabel = $"`Current ({Util.GetUoM(UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = TimeArray[iStart],
                        xMax = TimeArray[iEnd] + 1
                    });
                }
            }
            return gapCharts.Union(synCharts).ToList();
        }

        private static List<ChartDataStruct> CreateCurrentCharts(double[] TimeArray,
             List<GapJunction> gapJunctions, List<ChemicalSynapse> synapses,
             int iStart, int iEnd,
             UnitOfMeasure UoM)
        {
            List<ChartDataStruct> gapCharts = new();
            List<ChartDataStruct> synCharts = new();
            double yMin = double.MaxValue;
            double yMax = double.MinValue;
            if (gapJunctions?.Count > 0)
            {
                yMin = gapJunctions.Min(jnc => jnc.InputCurrent?.Min() ?? 0);
                yMax = gapJunctions.Max(jnc => jnc.InputCurrent?.Max() ?? 0);
            }
            if (synapses?.Count > 0)
            {
                yMin = Math.Min(yMin, synapses.Min(jnc => jnc.InputCurrent?.Min()??0));
                yMax = Math.Max(yMax, synapses.Max(jnc => jnc.InputCurrent?.Max()??0));
            }
            Util.SetYRange(ref yMin, ref yMax);

            string columnTitles = "Time,";
            List<string> data = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
            
            foreach (GapJunction jnc in gapJunctions)
            {
                columnTitles += jnc.ID + ",";
                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                    data[i] += jnc.InputCurrent?[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
            }
            foreach (ChemicalSynapse jnc in synapses)
            {
                columnTitles += jnc.ID + ",";
                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                    data[i] += jnc.InputCurrent?[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
            }

            string csvData = "`" + columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
            List<ChartDataStruct> charts = new();
            string ampere = UoM == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens ? "pA" :
                UoM == UnitOfMeasure.milliVolt_nanoAmpere_MegaOhm_nanoFarad_microSiemens ? "nA" : "";
            charts.Add(new ChartDataStruct
            {
                CsvData = csvData,
                Title = $"`Junction:{columnTitles[5..]}`",
                yLabel =  $"`Current ({ampere})`",
                yMin = yMin,
                yMax = yMax,
                xMin = TimeArray[iStart],
                xMax = TimeArray[iEnd] + 1
            });
            return charts;
        }

        private static List<ChartDataStruct> CreateStimuliCharts(double[] TimeArray, List<Cell> cells,
            bool combinePools, bool combineSomites, bool combineCells,
            int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<ChartDataStruct> charts = new();
            if (cells == null || !cells.Any())
                return charts;

            double yMin = cells.Min(c => c.MinStimulusValue());
            double yMax = cells.Max(c => c.MaxStimulusValue());
            Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string title = "Time,";
                List<string> data = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> colorPerChart = new();
                bool stimExists = false;
                if (!GlobalSettings.SameYAxis)
                {
                    yMin = cellGroup.Min(c => c.MinStimulusValue());
                    yMax = cellGroup.Max(c => c.MaxStimulusValue());
                    Util.SetYRange(ref yMin, ref yMax);
                }

                foreach (Cell cell in cellGroup.Where(c => c.Stimuli.HasStimulus))
                {
                    stimExists = true;
                    title += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += cell.Stimuli.GetStimulus(iStart + i).ToString(GlobalSettings.PlotDataFormat) + ",";
                }
                if (stimExists)
                {
                    string csvData = "`" + title[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
                    charts.Add(new ChartDataStruct
                    {
                        CsvData = csvData,
                        Color = string.Join(',', colorPerChart),
                        Title = $"`{cellGroup.Key} Applied Stimuli`",
                        yLabel = $"`Stimulus ({Util.GetUoM(UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = TimeArray[iStart],
                        xMax = TimeArray[iEnd] + 1
                    });
                }
            }
            return charts;
        }

        private static List<ChartDataStruct> CreateTensionCharts(double[] TimeArray, List<MuscleCell> cells,
            bool combinePools, bool combineSomites, bool combineCells,
            int iStart, int iEnd)
        {
            List<ChartDataStruct> charts = new();

            double yMin = 0;
            double yMax = 1.2;
            Util.SetYRange(ref yMin, ref yMax);
            List<Cell> cellList = cells.Cast<Cell>().ToList();

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cellList, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string title = "Time,";
                List<string> data = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> colorPerChart = new();
                foreach (Cell cell in cellGroup)
                {
                    MuscleCell muscleCell = cell as MuscleCell;
                    double[] Tension = muscleCell.RelativeTension;
                    title += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += Tension[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
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
                    xMax = TimeArray[iEnd] + 1
                });
            }
            return charts;
        }

        private static List<ChartDataStruct> CreateFullDynamicsCharts(double[] TimeOffset, List<Cell> cells,
            bool combinePools, bool combineSomites, bool combineCells,
            int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<ChartDataStruct> charts = new();
            charts.AddRange(CreateMembranePotentialCharts(TimeOffset, cells, combinePools, combineSomites, combineCells, iStart, iEnd));
            charts.AddRange(CreateCurrentCharts(TimeOffset, cells, combinePools, combineSomites, combineCells, iStart, iEnd, UoM));
            charts.AddRange(CreateStimuliCharts(TimeOffset, cells, combinePools, combineSomites, combineCells, iStart, iEnd, UoM));
            List<MuscleCell> muscleCells = cells.Where(c => c is MuscleCell).Select(c => (MuscleCell)c).ToList();
            if (muscleCells.Any())
                charts.AddRange(CreateTensionCharts(TimeOffset, muscleCells, combinePools, combineSomites, combineCells, iStart, iEnd));
            return charts;
        }

        private static List<ChartDataStruct> CreateJunctionCharts(double[] TimeOffset, JunctionBase jnc, int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<ChartDataStruct> charts = new();
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
            charts.AddRange(CreateMembranePotentialCharts(TimeOffset, cells, combinePools: false, combineSomites: false, combineCells: false, iStart, iEnd));
            charts.AddRange(CreateCurrentCharts(TimeOffset, gapJunctions, synapses, iStart, iEnd, UoM));
            return charts;
        }

        private static List<ChartDataStruct> CreateEpisodeCharts(RunningModel model, int iStart, int iEnd)
        {
            List<ChartDataStruct> charts = new();

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
            charts.Add(new ChartDataStruct
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
                charts.Add(new ChartDataStruct
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
                    charts.Add(new ChartDataStruct
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

                xValues = episodes.SelectMany(e => e.Beats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InstantFequency).ToArray();
                title = "Time,Instant. Freq.";
                data = new string[xValues.Length];
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
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });

                xValues = episodes.SelectMany(e => e.InlierBeats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InlierInstantFequency).ToArray();
                title = "Time,Instant. Freq.";
                data = new string[xValues.Length];
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
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                });

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.BeatFrequency).ToArray();
                title = "Time,Tail Beat Freq.";
                data = new string[episodes.Count];
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
                charts.Add(new ChartDataStruct
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

        public static (string, List<ChartDataStruct>) GetPlotData(PlotType PlotType, RunningModel model, List<Cell> Cells, List<CellPool> Pools,
             PlotSelectionInterface selection, int tStart = 0, int tEnd = -1)
        {
            List<ChartDataStruct> charts;
            double dt = model.RunParam.DeltaT;
            int tSkip = model.RunParam.SkipDuration;
            int iStart = (int)((tStart + tSkip) / dt);
            if (tEnd < 0) tEnd = model.RunParam.MaxTime;
            int iEnd = (int)((tEnd + tSkip) / dt);
            if (iEnd < iStart || iEnd >= model.TimeArray.Length)
                iEnd = model.TimeArray.Length - 1;

            UnitOfMeasure UoM = model.Settings.UoM;
            if (selection is PlotSelectionJunction jncSelection)
            {
                charts = CreateJunctionCharts(model.TimeArray, jncSelection.Junction, iStart, iEnd, UoM);
                return ("Junction", charts);
            }

            if (PlotType != PlotType.Episodes &&
                (Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return (null, null);
            bool combineCells = false;
            bool combineSomites = false;
            bool combinePools = false;
            if (selection is PlotSelectionMultiCells cellSelection)
            {
                combinePools = cellSelection.combinePools;
                combineCells = cellSelection.combineCells;
                combineSomites = cellSelection.combineSomites;
            }
            string Title = "";
            switch (PlotType)
            {
                case PlotType.MembPotential:
                    Title = "Membrane Potentials";
                    charts = CreateMembranePotentialCharts(model.TimeArray, Cells, combinePools, combineSomites, combineCells, iStart, iEnd);
                    break;
                case PlotType.Current:
                    Title = "Incoming Currents";
                    charts = CreateCurrentCharts(model.TimeArray, Cells, combinePools, combineSomites, combineCells, iStart, iEnd, UoM, includeGap: true, includeChemIn: true, includeChemOut: false);
                    break;
                case PlotType.GapCurrent:
                    Title = "Incoming Gap Currents";
                    charts = CreateCurrentCharts(model.TimeArray, Cells, combinePools, combineSomites, combineCells, iStart, iEnd, UoM, includeGap: true, includeChemIn: false, includeChemOut: false);
                    break;
                case PlotType.ChemCurrent:
                    Title = "Incoming Synaptic Currents";
                    charts = CreateCurrentCharts(model.TimeArray, Cells, combinePools, combineSomites, combineCells, iStart, iEnd, UoM, includeGap: false, includeChemIn: true, includeChemOut: false);
                    break;
                case PlotType.ChemOutCurrent:
                    Title = "Outgoing Synaptic Currents";
                    charts = CreateCurrentCharts(model.TimeArray, Cells, combinePools, combineSomites, combineCells, iStart, iEnd, UoM, includeGap: false, includeChemIn: false, includeChemOut: true);
                    break;
                case PlotType.Stimuli:
                    Title = "Applied Stimulus";
                    charts = CreateStimuliCharts(model.TimeArray, Cells, combinePools, combineSomites, combineCells, iStart, iEnd, UoM);
                    break;
                case PlotType.FullDyn:
                    Title = "Full Dynamics";
                    charts = CreateFullDynamicsCharts(model.TimeArray, Cells, combinePools, combineSomites, combineCells, iStart, iEnd, UoM);
                    break;
                case PlotType.Tension:
                    Title = "Muscle Tension";
                    List<MuscleCell> muscleCells = Cells.Where(c => c is MuscleCell).Select(c => (MuscleCell)c).ToList();
                    charts = CreateTensionCharts(model.TimeArray, muscleCells, combinePools, combineSomites, combineCells, iStart, iEnd);
                    break;
                case PlotType.Episodes:
                    Title = "Tail Beat Episodes";
                    charts = CreateEpisodeCharts(model, iStart, iEnd);
                    break;
                default:
                    charts = null;
                    break;
            }
            return (Title, charts);
        }

        public static (string, List<ChartDataStruct>) GetPlotDataOfJunction(JunctionBase junction, int tStart = 0, int tEnd = -1)
        {
            if (junction == null)
                return (null, null);
            GapJunction gapJunc = junction as GapJunction;
            ChemicalSynapse synapse = junction as ChemicalSynapse;
            RunningModel model = gapJunc?.Cell1.Model ?? synapse?.PreNeuron.Model;
            if (model == null) return (null, null);
            Cell preCell = gapJunc?.Cell1 ?? synapse?.PreNeuron;
            Cell postCell = gapJunc?.Cell2 ?? synapse?.PostCell;

            double dt = model.RunParam.DeltaT;
            int tSkip = model.RunParam.SkipDuration;
            int iStart = (int)((tStart + tSkip) / dt);
            if (tEnd < 0) tEnd = model.RunParam.MaxTime;
            int iEnd = (int)((tEnd + tSkip) / dt);
            if (iEnd < iStart || iEnd >= model.TimeArray.Length)
                iEnd = model.TimeArray.Length - 1;

            UnitOfMeasure UoM = model.Settings.UoM;
            List<Cell> Cells = new() { preCell, postCell };
            List<ChartDataStruct> charts = CreateMembranePotentialCharts(model.TimeArray, Cells, combinePools: false, combineSomites: false, combineCells: false, iStart, iEnd);

            charts.AddRange(CreateCurrentCharts(model.TimeArray, Cells, combinePools: false, combineSomites: false, combineCells: false, iStart, iEnd, UoM, includeGap: gapJunc != null, includeChemIn: false, includeChemOut: synapse != null)) ;
            return ("Junction plot", charts);
        }

    }
}
