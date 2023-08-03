using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorEpisodesOfTail : PlotGeneratorBase
    {
        readonly Coordinate[] tail_tip_coord;
        readonly List<SwimmingEpisode> episodes;
        public PlotGeneratorEpisodesOfTail(PlotGenerator plotGenerator, Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes,
            double[] timeArray, int iStart, int iEnd) :
            base(plotGenerator, timeArray, iStart, iEnd)
        {
            this.tail_tip_coord = tail_tip_coord;
            this.episodes = episodes;
        }
        protected override void CreateCharts()
        {
            double[] Time = timeArray[iStart..(iEnd + 1)];
            double[] xValues = Time;
            double[] yValues = tail_tip_coord[iStart..(iEnd + 1)].Select(c => c.X).ToArray();

            //Tail Movement
            string title = "Time,Y-Axis";
            string[] data = new string[iEnd - iStart + 2];
            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                data[i] = xValues[i] + "," + yValues[i];
            string csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
            Chart chart = new()
            {
                CsvData = csvData,
                Title = $"`Tail Movement`",
                yLabel = "`Y-Coordinate`",
                xMin = Time[0],
                xMax = Time[^1] + 1,
                yMin = yValues.Min() - 1,
                yMax = yValues.Max() + 1
            };
            if (!AddChart(chart)) return;

            if (episodes.Any())
            {
                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.EpisodeDuration).ToArray();
                title = "Time,Episode Duration";
                data = new string[episodes.Count];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                chart = new Chart
                {
                    CsvData = csvData,
                    Title = $"`Episode Duration`",
                    yLabel = "`Duration (ms)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                };
                if (!AddChart(chart)) return;


                if (episodes.Count > 1)
                {
                    xValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i].End).ToArray();
                    yValues = Enumerable.Range(0, episodes.Count - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray();
                    title = "Time,Episode Intervals";
                    data = new string[episodes.Count];
                    foreach (int i in Enumerable.Range(0, episodes.Count - 1))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    chart = new Chart
                    {
                        CsvData = csvData,
                        Title = $"`Episode Intervals`",
                        yLabel = "`Interval (ms)`",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1
                    };
                    if (!AddChart(chart)) return;
                }

                xValues = episodes.SelectMany(e => e.Beats.Select(b => b.BeatStart)).ToArray();
                yValues = episodes.SelectMany(e => e.InstantFequency).ToArray();
                title = "Time,Instant. Freq.";
                data = new string[xValues.Length];
                foreach (int i in Enumerable.Range(0, xValues.Length))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                chart = new Chart
                {
                    CsvData = csvData,
                    Title = $"`Instantenous Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                };
                if (!AddChart(chart)) return;

                xValues = episodes.SelectMany(e => e.InlierBeats.Select(b => b.BeatStart)).ToArray();
                if (xValues.Any())
                {
                    yValues = episodes.SelectMany(e => e.InlierInstantFequency).ToArray();
                    title = "Time,Instant. Freq.";
                    data = new string[xValues.Length];
                    foreach (int i in Enumerable.Range(0, xValues.Length))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    chart = new Chart
                    {
                        CsvData = csvData,
                        Title = $"`Instantenous Frequency (Outliers Removed)`",
                        yLabel = "`Freq (Hz)`",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1
                    };
                    if (!AddChart(chart)) return;
                }

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => e.BeatFrequency).ToArray();
                title = "Time,Tail Beat Freq.";
                data = new string[episodes.Count];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                chart = new Chart
                {
                    CsvData = csvData,
                    Title = $"`Tail Beat Frequency`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                };
                if (!AddChart(chart)) return;

                xValues = episodes.Select(e => e.Start).ToArray();
                yValues = episodes.Select(e => (double)e.Beats.Count).ToArray();
                title = "Time,Tail Beat/Episode";
                data = new string[episodes.Count];
                foreach (int i in Enumerable.Range(0, episodes.Count))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                chart = new Chart
                {
                    CsvData = csvData,
                    Title = $"`Tail Beat/Episode`",
                    yLabel = "`Count`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                };
                if (!AddChart(chart)) return;
            }
        }
    }
}
