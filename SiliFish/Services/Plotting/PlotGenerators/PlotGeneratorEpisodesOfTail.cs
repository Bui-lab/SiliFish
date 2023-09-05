﻿using SiliFish.DataTypes;
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
        readonly SwimmingEpisodes episodes;
        public PlotGeneratorEpisodesOfTail(PlotGenerator plotGenerator, Coordinate[] tail_tip_coord, SwimmingEpisodes episodes,
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

            if (episodes.HasEpisodes)
            {
                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.EpisodeDuration);
                title = "Time,Episode Duration";
                data = new string[episodes.EpisodeCount];
                foreach (int i in Enumerable.Range(0, episodes.EpisodeCount))
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


                if (episodes.EpisodeCount > 1)
                {
                    xValues = Enumerable.Range(0, episodes.EpisodeCount - 1).Select(i => episodes[i].End).ToArray();
                    yValues = Enumerable.Range(0, episodes.EpisodeCount - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray();
                    title = "Time,Episode Intervals";
                    data = new string[episodes.EpisodeCount];
                    foreach (int i in Enumerable.Range(0, episodes.EpisodeCount - 1))
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

                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.InstantFreq);
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

                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.InlierInstantFreq);
                if (xValues.Any())
                {
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

                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatFreq);
                title = "Time,Tail Beat Freq.";
                data = new string[episodes.EpisodeCount];
                foreach (int i in Enumerable.Range(0, episodes.EpisodeCount))
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

                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatsPerEpisode);
                title = "Time,Tail Beat/Episode";
                data = new string[episodes.EpisodeCount];
                foreach (int i in Enumerable.Range(0, episodes.EpisodeCount))
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
