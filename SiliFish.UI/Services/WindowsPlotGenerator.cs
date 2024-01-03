using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Helpers;

namespace Services
{
    public class WindowsPlotGenerator
    {
        public static PlotModel CreateModel(string title, double tStart, double tEnd, string yAxis, double yMin, double yMax, bool absoluteYRange)
        {
            PlotModel model = new() { Title = title };
            if (!absoluteYRange)
                Util.SetYRange(ref yMin, ref yMax);

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Time",
                Minimum = tStart,
                Maximum = tEnd
            });
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = yAxis,
                Minimum = yMin,
                Maximum = yMax
            });
            return model;
        }

        public static Image CreateScatterPlot(Chart chart, int width = 1024, int height = 480, bool absoluteYRange = false)
        {
            if (chart.xData == null || (chart.yData == null && chart.yMultiData == null))
                return null;

            int seriesCount = chart.yData != null ? 1 : chart.yMultiData.Count;
            double dMin = chart.yMin;
            double dMax = chart.yMax;
            Util.SetYRange(ref dMin, ref dMax);
            PlotModel model = CreateModel(chart.Title, chart.xMin, chart.xMax, chart.yLabel, dMin, dMax, absoluteYRange);
            byte a = 255;

            ScatterSeries[] scatterSeries = new ScatterSeries[seriesCount];
            for (int i = 0; i < seriesCount; i++)
            {
                Color color = i < chart.Colors.Count ? chart.Colors[i] : Color.Black;
                OxyColor oxyColor = OxyColor.FromAColor(a, OxyColor.FromRgb(color.R, color.G, color.B));
                ScatterSeries ls = new()
                {
                    MarkerStroke = oxyColor,
                    MarkerFill = oxyColor,
                    MarkerType = MarkerType.Circle,
                    MarkerSize = GlobalSettings.PlotPointSize
                };
                scatterSeries[i] = ls;
            }
            for (int i = 0; i < chart.xData.Length; i++)
            {
                ScatterSeries ls = scatterSeries[0];
                if (chart.yData?.Length > i)
                    ls.Points.Add(new ScatterPoint(chart.xData[i], chart.yData[i]));
                else if (chart.yMultiData != null)
                {
                    int seriesCounter = 0;
                    foreach (double[] yData in chart.yMultiData)
                    {
                        ls = scatterSeries[seriesCounter++];
                        if (yData.Length > i)
                            ls.Points.Add(new ScatterPoint(chart.xData[i], yData[i]));
                    }
                }
            }
            foreach (ScatterSeries ls in scatterSeries)
                model.Series.Add(ls);

            var stream = new MemoryStream();
            var pngExporter = new PngExporter { Width = width, Height = height };
            pngExporter.Export(model, stream);
            Image image = Image.FromStream(stream);
            image.Tag = chart.Title;
            return image;
        }
        public static Image CreateLinePlot(Chart chart, int width = 1024, int height = 480, bool absoluteYRange = false)
        {
            if (chart.xData == null || (chart.yData == null && chart.yMultiData == null))
                return null;

            int seriesCount = chart.yData != null ? 1 : chart.yMultiData.Count;
            double dMin = chart.yMin;
            double dMax = chart.yMax;
            Util.SetYRange(ref dMin, ref dMax);
            PlotModel model = CreateModel(chart.Title, chart.xMin, chart.xMax, chart.yLabel, dMin, dMax, absoluteYRange);
            byte a = 255;
            LineSeries[] lineSeries = new LineSeries[seriesCount];
            for (int i = 0; i < seriesCount; i++)
            {
                Color color = i < chart.Colors.Count ? chart.Colors[i] : Color.Black;
                LineSeries ls = new()
                {
                    Color = OxyColor.FromAColor(a, OxyColor.FromRgb(color.R, color.G, color.B))
                };
                lineSeries[i] = ls;
            }
            for (int i = 0; i < chart.xData.Length; i++)
            {
                LineSeries ls = lineSeries[0];
                if (chart.yData?.Length > i)
                    ls.Points.Add(new DataPoint(chart.xData[i], chart.yData[i]));
                else if (chart.yMultiData != null)
                {
                    int seriesCounter = 0;
                    foreach (double[] yData in chart.yMultiData)
                    {
                        ls = lineSeries[seriesCounter++];
                        if (yData.Length > i)
                            ls.Points.Add(new DataPoint(chart.xData[i], yData[i]));
                    }
                }
            }
            foreach (LineSeries ls in lineSeries)
                model.Series.Add(ls);

            var stream = new MemoryStream();
            var pngExporter = new PngExporter { Width = width, Height = height };
            pngExporter.Export(model, stream);
            Image image = Image.FromStream(stream);
            image.Tag = chart.Title;
            return image;
        }
        public static List<Image> PlotCharts(List<Chart> Charts)
        {
            List<Image> images = new();
            foreach (Chart chart in Charts)
            {
                if (chart.ScatterPlot)
                    images.Add(CreateScatterPlot(chart, GlobalSettings.DefaultPlotWidth, GlobalSettings.DefaultPlotHeight + 100, GlobalSettings.SameYAxis));
                else
                    images.Add(CreateLinePlot(chart, GlobalSettings.DefaultPlotWidth, GlobalSettings.DefaultPlotHeight + 100, GlobalSettings.SameYAxis));
            }

            return images;
        }

    }
}
