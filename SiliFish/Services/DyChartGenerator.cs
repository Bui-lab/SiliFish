using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SiliFish.Services
{
    public class DyChartGenerator : VisualsGenerator
    {
        public struct ChartStruct
        {
            public string csvData = "";
            public string Title = "", xLabel = "`Time (ms)`", yLabel = "", Color = "'red'";
            public string yMin = "null";
            public string yMax = "null";
            public string xMin = "";
            public string xMax = "";
            public bool ScatterPlot = false;
            public ChartStruct()
            {
            }
        }
        public static string PlotCharts(string title, List<ChartStruct> charts, int width, int height)
        {
            try
            {
                StringBuilder html = new(ReadEmbeddedResource("SiliFish.Resources.DyChart.html"));
                StringBuilder plot = new(ReadEmbeddedResource("SiliFish.Resources.DyChart.js"));
                StringBuilder synchronizer = new(ReadEmbeddedResource("SiliFish.Resources.DySynchronizer.js"));
                string chartDiv = ReadEmbeddedResource("SiliFish.Resources.DyChartDiv.html");

                html.Replace("__TITLE__", HttpUtility.HtmlEncode(title));

                if (Util.CheckOnlineStatus())
                {
                    html.Replace("__OFFLINE_DYGRAPH_SCRIPT__", "");
                    html.Replace("__ONLINE_DYGRAPH_SCRIPT__", "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/dygraph/2.1.0/dygraph.js\"></script>");
                }
                else
                {
                    html.Replace("__OFFLINE_DYGRAPH_SCRIPT__", ReadEmbeddedResource("SiliFish.Resources.dygraph.js"));
                    html.Replace("__ONLINE_DYGRAPH_SCRIPT__", "");
                }

                if (width < 200) width = 200;
                if (height < 100) height = 100;

                html.Replace("__WIDTH_PX__", width.ToString());
                html.Replace("__HEIGHT_PX__", height.ToString());
                string chartDivs = "";
                List<string> chartHTML = new();
                foreach (int chartIndex in Enumerable.Range(0, charts.Count))
                {
                    chartDivs += chartDiv.Replace("__CHART_INDEX__", chartIndex.ToString());
                    StringBuilder chart = new(ReadEmbeddedResource("SiliFish.Resources.DyChart.js"));
                    chart.Replace("__CHART_INDEX__", chartIndex.ToString());
                    chart.Replace("__CHART_DATA__", charts[chartIndex].csvData);
                    chart.Replace("__CHART_COLORS__", charts[chartIndex].Color);
                    chart.Replace("__CHART_TITLE__", charts[chartIndex].Title);
                    chart.Replace("__X_MIN__", charts[chartIndex].xMin);
                    chart.Replace("__X_MAX__", charts[chartIndex].xMax);
                    chart.Replace("__Y_MIN__", charts[chartIndex].yMin);
                    chart.Replace("__Y_MAX__", charts[chartIndex].yMax);
                    chart.Replace("__Y_LABEL__", charts[chartIndex].yLabel);
                    chart.Replace("__X_LABEL__", charts[chartIndex].xLabel);
                    chart.Replace("__SCATTER_PLOT__", charts[chartIndex].ScatterPlot ? "drawPoints: true,strokeWidth: 0," : "");
                    chartHTML.Add(chart.ToString());
                }
                html.Replace("__CHART_DIVS__", chartDivs);
                html.Replace("__CHARTS__", string.Join('\n', chartHTML));
                html.Replace("__DY_SYNCHRONIZER__", synchronizer.ToString());

                return html.ToString();
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }


        private static List<ChartStruct> CreateMembranePotentialCharts(double[] TimeArray, List<Cell> cells, bool groupByPool,
            int iStart, int iEnd)
        {
            List<ChartStruct> charts = new();

            double yMin = cells.Min(c => c.MinPotentialValue);
            double yMax = cells.Max(c => c.MaxPotentialValue);
            Helpers.Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = cells.GroupBy(c => groupByPool ? c.CellPool.CellGroup : c.ID);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string title = "Time,";
                List<string> data = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString("0.0#") + ","));
                List<string> colorPerChart = new();
                foreach (Cell cell in cellGroup)
                {
                    title += cell.ID + ",";
                    colorPerChart.Add("'" + cell.CellPool.Color.ToRGB() + "'");
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += cell.V?[iStart + i].ToString("0.0####") + ",";
                }
                string csvData = "`" + title[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
                charts.Add(new ChartStruct
                {
                    csvData = csvData,
                    Color = string.Join(',', colorPerChart),
                    Title = $"`{cellGroup.Key} Membrane Potential`",
                    yLabel = "`Memb. Potential (mV)`",
                    yMin = yMin.ToString("0.0"),
                    yMax = yMax.ToString("0.0"),
                    xMin = TimeArray[iStart].ToString(),
                    xMax = (TimeArray[iEnd] + 1).ToString()
                });
            }
            return charts;
        }


        private static List<ChartStruct> CreateCurrentCharts(double[] TimeArray, List<Cell> cells, bool groupByPool,
            int iStart, int iEnd, bool includeGap = true, bool includeChem = true)
        {
            List<ChartStruct> gapCharts = new();
            List<ChartStruct> synCharts = new();
            double yMin = cells.Min(c => c.MinCurrentValue);
            double yMax = cells.Max(c => c.MaxCurrentValue);
            Helpers.Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = cells.GroupBy(c => groupByPool ? c.CellPool.CellGroup : c.ID);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string gapTitle = "Time,";
                string synTitle = "Time,";
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
                            colorPerGapChart.Add("'" + otherCell.CellPool.Color.ToRGB() + "'");
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                gapData[i] += jnc.InputCurrent[iStart + i].ToString("0.0####") + ",";
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
                            synTitle += jnc.PreNeuron.ID + ",";
                            colorPerInSynChart.Add("'" + jnc.PreNeuron.CellPool.Color.ToRGB() + "'");
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                synInData[i] += jnc.InputCurrent[iStart + i].ToString("0.0####") + ",";
                        }
                        if (cell is Neuron neuron2)
                        {
                            foreach (ChemicalSynapse jnc in neuron2.Terminals)
                            {
                                synOutExists = true;
                                synTitle += jnc.PostCell.ID + ",";
                                colorPerOutSynChart.Add("'" + jnc.PostCell.CellPool.Color.ToRGB() + "'");
                                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                    synOutData[i] += jnc.InputCurrent[iStart + i].ToString("0.0####") + ",";
                            }
                        }
                    }
                }
                if (gapExists)
                {
                    string csvData = "`" + gapTitle[..^1] + "\n" + string.Join("\n", gapData.Select(line => line[..^1]).ToArray()) + "`";
                    gapCharts.Add(new ChartStruct
                    {
                        csvData = csvData,
                        Color = string.Join(',', colorPerGapChart),
                        Title = $"`{cellGroup.Key} Gap Currents`",
                        yLabel = "`Current (pA)`",
                        yMin = yMin.ToString("0.0"),
                        yMax = yMax.ToString("0.0"),
                        xMin = TimeArray[iStart].ToString(),
                        xMax = (TimeArray[iEnd] + 1).ToString()
                    });
                }
                if (synInExists)
                {
                    string csvData = "`" + synTitle[..^1] + "\n" + string.Join("\n", synInData.Select(line => line[..^1]).ToArray()) + "`";
                    synCharts.Add(new ChartStruct
                    {
                        csvData = csvData,
                        Color = string.Join(',', colorPerInSynChart),
                        Title = $"`{cellGroup.Key} Incoming Synaptic Currents`",
                        yLabel = "`Current (pA)`",
                        yMin = yMin.ToString("0.0"),
                        yMax = yMax.ToString("0.0"),
                        xMin = TimeArray[iStart].ToString(),
                        xMax = (TimeArray[iEnd] + 1).ToString()
                    });
                }
                if (synOutExists)
                { 
                    string csvData = "`" + synTitle[..^1] + "\n" + string.Join("\n", synOutData.Select(line => line[..^1]).ToArray()) + "`";
                    synCharts.Add(new ChartStruct
                    {
                        csvData = csvData,
                        Color = string.Join(',', colorPerOutSynChart),
                        Title = $"`{cellGroup.Key} Outgoing Synaptic Currents`",
                        yLabel = "`Current (pA)`",
                        yMin = yMin.ToString("0.0"),
                        yMax = yMax.ToString("0.0"),
                        xMin = TimeArray[iStart].ToString(),
                        xMax = (TimeArray[iEnd] + 1).ToString()
                    });
                }
            }
            return gapCharts.Union(synCharts).ToList();
        }

        private static List<ChartStruct> CreateStimuliCharts(double[] TimeArray, List<Cell> cells, bool groupByPool,
            int iStart, int iEnd)
        {
            List<ChartStruct> charts = new();

            double yMin = cells.Min(c => c.MinStimulusValue);
            double yMax = cells.Max(c => c.MaxStimulusValue);
            Helpers.Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = cells.GroupBy(c => groupByPool ? c.CellPool.CellGroup : c.ID);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string title = "Time,";
                List<string> data = new(TimeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString("0.0#") + ","));
                List<string> colorPerChart = new();
                foreach (Cell cell in cellGroup)
                {
                    title += cell.ID + ",";
                    colorPerChart.Add("'" + cell.CellPool.Color.ToRGB() + "'");
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += cell.Stimulus?[iStart + i].ToString("0.0####") + ",";
                }
                string csvData = "`" + title[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
                charts.Add(new ChartStruct
                {
                    csvData = csvData,
                    Color = string.Join(',', colorPerChart),
                    Title = $"`{cellGroup.Key} Applied Stimuli`",
                    yLabel = "`Stimulus (pA)`",
                    yMin = yMin.ToString("0.0"),
                    yMax = yMax.ToString("0.0"),
                    xMin = TimeArray[iStart].ToString(),
                    xMax = (TimeArray[iEnd] + 1).ToString()
                });
            }
            return charts;
        }

        private static List<ChartStruct> CreateFullDynamicsCharts(double[] TimeOffset, List<Cell> cells, bool groupByPool,
            int iStart, int iEnd)
        {
            List<ChartStruct> charts = new();
            charts.AddRange(CreateMembranePotentialCharts(TimeOffset, cells, groupByPool, iStart, iEnd));
            charts.AddRange(CreateCurrentCharts(TimeOffset, cells, groupByPool, iStart, iEnd, includeGap: true, includeChem: true));
            charts.AddRange(CreateStimuliCharts(TimeOffset, cells, groupByPool, iStart, iEnd));
            return charts;
        }

        private static List<ChartStruct> CreateEpisodeCharts(SwimmingModel model, int iStart, int iEnd)
        {
            List<ChartStruct> charts = new();

            (Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes) = model.GetSwimmingEpisodes();

            double[] Time = model.TimeArray[iStart..(iEnd + 1)];
            double[] xValues = Time;
            double[] yValues = tail_tip_coord[iStart..(iEnd + 1)].Select(c => c.X).ToArray();

            //Tail Movement
            string title = "Time,Y-Axis";
            string[] data = new string[iEnd - iStart + 2];
            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                data[i] = xValues[i] + "," + yValues[i];
            string csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
            charts.Add(new ChartStruct
            {
                csvData = csvData,
                Title = $"`Tail Movement`",
                yLabel = "`Y-Coordinate`",
                xMin = Time[0].ToString(),
                xMax = (Time[^1] + 1).ToString()
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
                charts.Add(new ChartStruct
                {
                    csvData = csvData,
                    Title = $"`Episode Duration`",
                    yLabel = "`Duration (ms)`",
                    ScatterPlot = true,
                    xMin = Time[0].ToString(),
                    xMax = (Time[^1] + 1).ToString()
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
                    charts.Add(new ChartStruct
                    {
                        csvData = csvData,
                        Title = $"`Episode Intervals`",
                        yLabel = "`Interval (ms)`",
                        ScatterPlot = true,
                        xMin = Time[0].ToString(),
                        xMax = (Time[^1] + 1).ToString()
                    });
                }

                xValues = episodes.SelectMany(e => e.Beats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InstantFequency).ToArray();
                title = "Time,Instant. Freq.";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, xValues.Length))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new ChartStruct
                {
                    csvData = csvData,
                    Title = $"`Instantenous Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0].ToString(),
                    xMax = (Time[^1] + 1).ToString()
                });

                xValues = episodes.SelectMany(e => e.InlierBeats.Select(b => b.beatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InlierInstantFequency).ToArray();
                title = "Time,Instant. Freq.";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, xValues.Length))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new ChartStruct
                {
                    csvData = csvData,
                    Title = $"`Instantenous Frequency (Outliers Removed)`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0].ToString(),
                    xMax = (Time[^1] + 1).ToString()
                });

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.BeatFrequency).ToArray();
                title = "Time,Tail Beat Freq.";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new ChartStruct
                {
                    csvData = csvData,
                    Title = $"`Tail Beat Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0].ToString(),
                    xMax = (Time[^1] + 1).ToString()
                });

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => (double)e.Beats.Count).ToArray();
                title = "Time,Tail Beat/Episode";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                charts.Add(new ChartStruct
                {
                    csvData = csvData,
                    Title = $"`Tail Beat/Episode`",
                    yLabel = "`Count`",
                    ScatterPlot = true,
                    xMin = Time[0].ToString(),
                    xMax = (Time[^1] + 1).ToString()
                });
            }

            return charts;
        }

        public static string Plot(PlotType PlotType, SwimmingModel model, List<Cell> Cells, List<CellPool> Pools,
            CellSelectionStruct cellSelection, int tStart = 0, int tEnd = -1, int tSkip = 0, int width = 480, int height = 240)
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

            bool groupByPool = false;
            if (Cells == null || !Cells.Any())
            {
                Cells = Pools.OrderBy(p => p.ID).SelectMany(p => p.GetCells(cellSelection)).ToList();
                groupByPool = true;
            }
            List<ChartStruct> charts = new();
            string Title = "";
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
            string PlotHTML = PlotCharts(Title, charts, width, height);
            return PlotHTML;
        }

    }
}
