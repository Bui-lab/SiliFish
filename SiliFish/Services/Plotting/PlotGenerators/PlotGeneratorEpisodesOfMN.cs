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
    internal class PlotGeneratorEpisodesOfMN : PlotGeneratorBase
    {
        readonly double[] mnMaxPotentials;
        readonly SwimmingEpisodes episodes;
        readonly int somite;
        public PlotGeneratorEpisodesOfMN(PlotGenerator plotGenerator, double[] mnMaxPotentials, SwimmingEpisodes episodes, int somite,
            double[] timeArray, int iStart, int iEnd) :
            base(plotGenerator, timeArray, iStart, iEnd)
        {
            this.mnMaxPotentials = mnMaxPotentials;
            this.episodes = episodes;
            this.somite = somite;
        }

        protected override void CreateCharts()
        {
            CreateCharts(PlotType.EpisodesMN);
        }
        protected override void CreateCharts(PlotType plotType)
        { 
            if (mnMaxPotentials.Min() == mnMaxPotentials.Max() && mnMaxPotentials[0] == 0) 
                return; 
            double[] Time = timeArray[iStart..(iEnd + 1)];
            double[] xValues = Time;
            double[] yValues = mnMaxPotentials[iStart..(iEnd + 1)];
            string title, csvData;
            string[] data;

            //Tail Movement
            if (plotType == PlotType.EpisodesMN || plotType == PlotType.TailMovement)
            {
                title = "Time,Y-Axis";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                Chart chart = new()
                {
                    CsvData = csvData,
                    Title = $"`VR Output - Somite {somite}`",
                    yLabel = "`Y-Coordinate`",
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = yValues.Min() - 1,
                    yMax = yValues.Max() + 1
                };
                if (!AddChart(chart)) return;
            }
            if (episodes.HasEpisodes)
            {
                //Episode Duration
                if (plotType == PlotType.EpisodesMN || plotType == PlotType.EpisodeDuration)
                {
                    (xValues, yValues) = episodes.GetXYValues(EpisodeStats.EpisodeDuration);
                    title = "Time,Episode Duration";
                    data = new string[episodes.EpisodeCount];
                    foreach (int i in Enumerable.Range(0, episodes.EpisodeCount))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    Chart chart = new Chart
                    {
                        CsvData = csvData,
                        Title = $"`Episode Duration - Somite {somite}`",
                        yLabel = "`Duration (ms)`",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1
                    };
                    if (!AddChart(chart)) return;
                    if (plotType == PlotType.EpisodesMN && episodes.EpisodeCount > 1)
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
                            Title = $"`Episode Intervals - Somite {somite}`",
                            yLabel = "`Interval (ms)`",
                            ScatterPlot = true,
                            xMin = Time[0],
                            xMax = Time[^1] + 1,
                            yMin = 0,
                            yMax = yValues.Max() + 1
                        };
                        if (!AddChart(chart)) return;
                    }
                }
                //Beat/Episode
                if (plotType == PlotType.EpisodesMN || plotType == PlotType.TailBeatPerEpisode)
                {

                    (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatsPerEpisode);
                    title = "Time,Tail Beat/Episode";
                    data = new string[episodes.EpisodeCount];
                    foreach (int i in Enumerable.Range(0, episodes.EpisodeCount))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    Chart chart = new Chart
                    {
                        CsvData = csvData,
                        Title = $"`Beat/Episode - Somite  {somite} `",
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
            //Instant Frequency
            if (plotType == PlotType.EpisodesMN || plotType == PlotType.InstFreq)
            {
                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.InstantFreq);
                title = "Time,Instant. Freq.";
                data = new string[xValues.Length];
                foreach (int i in Enumerable.Range(0, xValues.Length))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                Chart chart = new()
                {
                    CsvData = csvData,
                    Title = $"`Instant. Freq. - Somite {somite}`",
                    yLabel = "`Freq (Hz)`",
                    ScatterPlot = true,
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = 0,
                    yMax = yValues.Max() + 1
                };
                if (!AddChart(chart)) return;
            }

            //Instant Frequency without outliers only displayed in the summary plot
            if (plotType == PlotType.EpisodesMN)
            {

                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.InlierInstantFreq);
                if (xValues.Any())
                {
                    title = "Time,Instant. Freq.";
                    data = new string[xValues.Length];
                    foreach (int i in Enumerable.Range(0, xValues.Length))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                    Chart chart = new Chart
                    {
                        CsvData = csvData,
                        Title = $"`Instant. Freq. - Somite  {somite} (outliers removed)`",
                        yLabel = "`Freq (Hz)`",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1
                    };
                    if (!AddChart(chart)) return;
                }
            }
            //Tail Beat Frequency
            if (plotType == PlotType.EpisodesMN || plotType == PlotType.TailBeatFreq)
            {
                (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatFreq);
                title = "Time,Tail Beat Freq.";
                data = new string[episodes.EpisodeCount];
                foreach (int i in Enumerable.Range(0, episodes.EpisodeCount))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = "`" + title + "\n" + string.Join("\n", data) + "`";
                Chart chart = new Chart
                {
                    CsvData = csvData,
                    Title = $"`Tail Beat Freq. - Somite  {somite} `",
                    yLabel = "`Freq (Hz)`",
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
