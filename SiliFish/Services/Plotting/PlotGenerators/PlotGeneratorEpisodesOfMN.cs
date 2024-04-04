using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorEpisodesOfMN(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
        double[] mnMaxPotentials, SwimmingEpisodes episodes, int somite) : PlotGeneratorBase(plotGenerator, timeArray, iStart, iEnd, groupSeq, plotSelection: null)
    {
        readonly double[] mnMaxPotentials = mnMaxPotentials;
        readonly SwimmingEpisodes episodes = episodes;
        readonly int somite = somite;

        protected override void CreateCharts()
        {
            CreateCharts(PlotType.EpisodesMN);
        }
        protected override void CreateCharts(PlotType plotType)
        {
            if (mnMaxPotentials.Min() == mnMaxPotentials.Max() && mnMaxPotentials[0] == 0)
                return;
            double[] Time = timeArray[iStart..(iEnd + 1)];
            double tStart = timeArray[iStart];
            double tEnd = timeArray[iEnd];
            double[] xValues = Time;
            double[] yValues = mnMaxPotentials[iStart..(iEnd + 1)];
            string title, csvData;
            string[] data;

            //Tail Movement
            if (plotType is PlotType.EpisodesMN or PlotType.TailMovement or PlotType.TailMovementFreq)
            {
                title = "Time,Y-Axis";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = title + "\n" + string.Join("\n", data);
                Chart chart = new()
                {
                    CsvData = csvData,
                    Title = $"VR Output - Somite {somite}",
                    yLabel = "Y-Coordinate",
                    Colors = [Color.Red],
                    xMin = Time[0],
                    xMax = Time[^1] + 1,
                    yMin = yValues.Min() - 1,
                    yMax = yValues.Max() + 1,
                    xData = xValues,
                    yData = yValues
                };
                if (!AddChart(chart)) return;
            }

            if (episodes.HasEpisodes)
            {
                //Tail Beat Frequency
                if (plotType is PlotType.EpisodesMN or PlotType.TailMovementFreq)
                {
                    (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatFreq, tStart, tEnd);
                    title = "Time,Tail Beat Freq.";
                    data = new string[xValues.Length];
                    foreach (int i in Enumerable.Range(0, xValues.Length))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = title + "\n" + string.Join("\n", data);
                    Chart chart = new()
                    {
                        CsvData = csvData,
                        Title = $"Tail Beat Freq. - Somite  {somite} ",
                        yLabel = "Freq (Hz)",
                        Colors = [Color.Red],
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1,
                        xData = xValues,
                        yData = yValues
                    };
                    if (!AddChart(chart)) return;
                }                
                //Episode Duration
                if (plotType == PlotType.EpisodesMN)
                {
                    (xValues, yValues) = episodes.GetXYValues(EpisodeStats.EpisodeDuration, tStart, tEnd);
                    title = "Time,Episode Duration";
                    data = new string[xValues.Length];
                    foreach (int i in Enumerable.Range(0, xValues.Length))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = title + "\n" + string.Join("\n", data);
                    Chart chart = new()
                    {
                        CsvData = csvData,
                        Title = $"Episode Duration - Somite {somite}",
                        yLabel = "Duration (ms)",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1,
                        xData = xValues,
                        yData = yValues
                    };
                    if (!AddChart(chart)) return;
                    if (plotType == PlotType.EpisodesMN && episodes.EpisodeCount > 1)
                    {
                        xValues = Enumerable.Range(0, episodes.EpisodeCount - 1).Select(i => episodes[i].End).ToArray();
                        yValues = Enumerable.Range(0, episodes.EpisodeCount - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray();
                        title = "Time,Episode Intervals";
                        data = new string[xValues.Length];
                        foreach (int i in Enumerable.Range(0, xValues.Length))
                            data[i] = xValues[i] + "," + yValues[i];
                        csvData = title + "\n" + string.Join("\n", data);
                        chart = new Chart
                        {
                            CsvData = csvData,
                            Title = $"Episode Intervals - Somite {somite}",
                            yLabel = "Interval (ms)",
                            ScatterPlot = true,
                            xMin = Time[0],
                            xMax = Time[^1] + 1,
                            yMin = 0,
                            yMax = yValues.Max() + 1,
                            xData = xValues,
                            yData = yValues
                        };
                        if (!AddChart(chart)) return;
                    }
                }
                //Beat/Episode
                if (plotType == PlotType.EpisodesMN)
                {

                    (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatsPerEpisode, tStart, tEnd);
                    title = "Time,Tail Beat/Episode";
                    data = new string[xValues.Length];
                    foreach (int i in Enumerable.Range(0, xValues.Length))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = title + "\n" + string.Join("\n", data);
                    Chart chart = new()
                    {
                        CsvData = csvData,
                        Title = $"Beat/Episode - Somite  {somite} ",
                        yLabel = "Count",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1,
                        xData = xValues,
                        yData = yValues
                    };
                    if (!AddChart(chart)) return;
                }
            }
        }

    }
}
