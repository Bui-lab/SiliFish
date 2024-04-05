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
    internal class PlotGeneratorEpisodesOfTail(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
        SwimmingEpisodes episodes) : PlotGeneratorBase(plotGenerator, timeArray, iStart, iEnd, groupSeq, plotSelection: null)
    {
        readonly SwimmingEpisodes episodes = episodes;

        protected override void CreateCharts()
        {
            CreateCharts(PlotType.EpisodesTail);
        }
        protected override void CreateCharts(PlotType plotType)
        {
            double[] Time = timeArray[iStart..(iEnd + 1)];
            double[] xValues = Time;
            double[] yValues = episodes.TailTipCoordinates[iStart..(iEnd + 1)].Select(c => c.X).ToArray();
            string title, csvData;
            string[] data;
            double tStart = timeArray[iStart];
            double tEnd = timeArray[iEnd];


            //Tail Movement
            if (plotType is PlotType.EpisodesTail or PlotType.TailMovement or PlotType.TailMovementFreq)
            {
                title = "Time,Y-Axis";
                data = new string[iEnd - iStart + 2];
                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                    data[i] = xValues[i] + "," + yValues[i];
                csvData = title + "\n" + string.Join("\n", data);
                Chart chart = new()
                {
                    CsvData = csvData,
                    Title = $"Tail Movement",
                    yLabel = "Y-Coordinate",
                    Colors = [ Color.Red ],
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
                if (plotType is PlotType.EpisodesTail or PlotType.TailMovementFreq)
                {
                    (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatFreq, tStart, tEnd);
                    (double[] xValues2, double[] yValues2) = episodes.GetXYValues(EpisodeStats.RollingFreq, tStart, tEnd);
                    double yMax = Math.Max(yValues.Max(), yValues2.Max());
                    (double[] xData, List<double[]> yMultiData) = Util.MergeXYArrays(xValues, yValues, xValues2, yValues2);
                    List<Color> colorPerChart = [Color.Red, Color.Purple];
                    title = "Time,Summary TBF,Rolling TBF";
                    int len = xData.Length;
                    data = new string[len];
                    foreach (int i in Enumerable.Range(0, xData.Length))
                    {
                        data[i] = xData[i].ToString(GlobalSettings.PlotDataFormat);
                        foreach (int j in Enumerable.Range(0, yMultiData[0].Length))
                        {
                            data[i] += "," + yMultiData[i][j].ToString(GlobalSettings.PlotDataFormat);
                        }
                    }
                    csvData = title + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray());
                    Chart chart = new()
                    {
                        CsvData = csvData,
                        Colors = colorPerChart,
                        Title = $"Tail Beat Frequency",
                        yLabel = "Freq (Hz)",
                        ScatterPlot = true,
                        xData = xData,
                        yData = null,
                        yMultiData = yMultiData,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yMax
                    };
                    if (!AddChart(chart)) return;
                }

                if (plotType == PlotType.EpisodesTail)
                {
                    //Episode Duration
                    (xValues, yValues) = episodes.GetXYValues(EpisodeStats.EpisodeDuration, tStart, tEnd);
                    title = "Time,Episode Duration";
                    data = new string[episodes.EpisodeCount];
                    foreach (int i in Enumerable.Range(0, episodes.EpisodeCount))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = title + "\n" + string.Join("\n", data);
                    Chart chart = new()
                    {
                        CsvData = csvData,
                        Title = $"Episode Duration",
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

                    if (plotType == PlotType.EpisodesTail && episodes.EpisodeCount > 1)
                    {
                        xValues = Enumerable.Range(0, episodes.EpisodeCount - 1).Select(i => episodes[i].End).ToArray();
                        yValues = Enumerable.Range(0, episodes.EpisodeCount - 1).Select(i => episodes[i + 1].Start - episodes[i].End).ToArray();
                        title = "Time,Episode Intervals";
                        data = new string[episodes.EpisodeCount];
                        foreach (int i in Enumerable.Range(0, episodes.EpisodeCount - 1))
                            data[i] = xValues[i] + "," + yValues[i];
                        csvData = title + "\n" + string.Join("\n", data);
                        chart = new Chart
                        {
                            CsvData = csvData,
                            Title = $"Episode Intervals",
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
                    
                    //Beat/Episode
                    (xValues, yValues) = episodes.GetXYValues(EpisodeStats.BeatsPerEpisode, tStart, tEnd);
                    title = "Time,Tail Beat/Episode";
                    data = new string[episodes.EpisodeCount];
                    foreach (int i in Enumerable.Range(0, episodes.EpisodeCount))
                        data[i] = xValues[i] + "," + yValues[i];
                    csvData = title + "\n" + string.Join("\n", data);
                    Chart chart2 = new()
                    {
                        CsvData = csvData,
                        Title = $"Tail Beat/Episode",
                        yLabel = "Count",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = yValues.Max() + 1,
                        xData = xValues,
                        yData = yValues
                    };
                    if (!AddChart(chart2)) return;

                    //Tail beat amplitude
                    (xValues, double[] meanyValues) = episodes.GetXYValues(EpisodeStats.EpisodeMeanAmplitude, tStart, tEnd);
                    (xValues, double[] medianyValues) = episodes.GetXYValues(EpisodeStats.EpisodeMedianAmplitude, tStart, tEnd);
                    (xValues, double[] maxyValues) = episodes.GetXYValues(EpisodeStats.EpisodeMaxAmplitude, tStart, tEnd);
                    title = "Time,Mean Ampl.,Median Ampl.,Max Ampl.";
                    data = new string[episodes.EpisodeCount];
                    foreach (int i in Enumerable.Range(0, episodes.EpisodeCount))
                        data[i] = xValues[i] + "," + meanyValues[i] + "," + medianyValues[i] + "," + maxyValues[i];
                    csvData = title + "\n" + string.Join("\n", data);
                    List<Color> colorPerChart = [Color.Blue, Color.Purple, Color.Red];
                    Chart chart3 = new()
                    {
                        CsvData = csvData,
                        Colors = colorPerChart,
                        Title = $"Amplitude/Episode",
                        yLabel = "Count",
                        ScatterPlot = true,
                        xMin = Time[0],
                        xMax = Time[^1] + 1,
                        yMin = 0,
                        yMax = maxyValues.Max() + 1,
                        xData = xValues,
                        yData = yValues
                    };
                    if (!AddChart(chart3)) return;
                }
            }
        }
    }
}
