using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SiliFish.UI
{
    public class UtilWindows
    {
        public static void SaveImage(string path, Image img)
        {
            img.Save(path);
        }

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="colors"></param>
        /// <param name="AffarentCurrents">A dictionary of currents with a unique name as key, which is used for color determination</param>
        /// <returns></returns>
        public static Image CreateCurrentsPlot(string target, string title,
            Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents,
            double[] Time, int iStart, int iEnd,
            string yAxis, double yMin, double yMax,
            int width = 1024, int height = 480,
            bool absoluteYRange = false)
        {
            Util.SetYRange(ref yMin, ref yMax);
            List<string> sources = AffarentCurrents.Keys.ToList();
            PlotModel model = CreateModel(title, Time[iStart], Time[iEnd], yAxis, yMin, yMax, absoluteYRange);
            foreach (string source in sources)
            {
                string colSource = source;
                while (colSource.EndsWith("'"))
                    colSource = colSource[..^1];
                Color color = colors[colSource];
                OxyColor col = color.ToOxyColor();
                byte a = 255;
                int numofcells = sources.Count;
                byte dec = (byte)(200 / numofcells);
                double[] values = AffarentCurrents[source].ToArray();
                LineSeries ls = new()
                {
                    Color = OxyColor.FromAColor(a, OxyColor.FromRgb(col.R, col.G, col.B))
                };
                a -= dec;
                if (iEnd >= values.Length)
                    iEnd = values.Length - 1;
                ls.Points.AddRange(Enumerable
                    .Range(iStart, iEnd - iStart)
                    .Select(i => new DataPoint(Time[i], values[i])));
                model.Series.Add(ls);
            }
            var stream = new MemoryStream();
            var pngExporter = new PngExporter { Width = width, Height = height };
            pngExporter.Export(model, stream);
            Image image = Image.FromStream(stream);
            image.Tag = target;
            return image;
        }

        public static Image CreateScatterPlot(string title, double[] data,
            double[] Time, double tStart, double tEnd,
            string yAxis, double? yMin, double? yMax,
            Color color, int width = 1024, int height = 480,
            bool absoluteYRange = false)
        {
            if (data == null || data.Length != Time.Length)
                return null;

            yMin ??= data.Min();
            yMax ??= data.Max();

            PlotModel model = CreateModel(title, tStart, tEnd, yAxis, (double)yMin, (double)yMax, absoluteYRange);

            OxyColor col = color.ToOxyColor();
            ScatterSeries ls = new();
            ls.Points.AddRange(Enumerable
                .Range(0, Time.Length)
                .Select(i => new ScatterPoint(Time[i] < 0 ? 0 : Time[i], data[i])));

            model.Series.Add(ls);

            var stream = new MemoryStream();
            var pngExporter = new PngExporter { Width = width, Height = height };
            pngExporter.Export(model, stream);
            Image image = Image.FromStream(stream);
            image.Tag = title;
            return image;
        }

        public static Image CreateLinePlot(string title, double[,] values, double[] Time, int iStart, int iEnd,
            string yAxis, double yMin, double yMax,
            Color color, int width = 1024, int height = 480,
            bool absoluteYRange = false)
        {
            if (values == null)
                return null;

            Util.SetYRange(ref yMin, ref yMax);

            PlotModel model = CreateModel(title, Time[iStart], Time[iEnd], yAxis, yMin, yMax, absoluteYRange);

            OxyColor col = color.ToOxyColor();
            byte a = (byte)255;
            int numOfLines = values.GetLength(0);
            if (numOfLines == 0) return null;
            byte dec = (byte)(200 / numOfLines);
            for (int c = 0; c < numOfLines; c++)
            {
                LineSeries ls = new()
                {
                    Color = OxyColor.FromAColor(a, OxyColor.FromRgb(col.R, col.G, col.B))
                };
                a -= dec;
                ls.Points.AddRange(Enumerable
                    .Range(iStart, iEnd - iStart)
                    .Select(i => new DataPoint(Time[i], values[c, i])));
                model.Series.Add(ls);
            }
            var stream = new MemoryStream();
            var pngExporter = new PngExporter { Width = width, Height = height };
            pngExporter.Export(model, stream);
            Image image = Image.FromStream(stream);
            image.Tag = title;
            return image;
        }

        public static Image CreateLinePlot(string title, double[] data,
            double[] Time, int iStart, int iEnd,
            string yAxis, double? yMin, double? yMax,
            Color color, int width = 1024, int height = 480, bool absoluteYRange = false)
        {
            if (data == null)
                return null;

            double dMin = yMin ?? data.Min();
            double dMax = yMax ?? data.Max();
            Util.SetYRange(ref dMin, ref dMax);
            PlotModel model = CreateModel(title, Time[iStart], Time[iEnd], yAxis, dMin, dMax, absoluteYRange);

            OxyColor col = color.ToOxyColor();
            byte a = 255;
            LineSeries ls = new()
            {
                Color = OxyColor.FromAColor(a, OxyColor.FromRgb(col.R, col.G, col.B))
            };
            ls.Points.AddRange(Enumerable
                .Range(iStart, iEnd - iStart)
                .Select(i => new DataPoint(Time[i], data[i])));
            model.Series.Add(ls);

            var stream = new MemoryStream();
            var pngExporter = new PngExporter { Width = width, Height = height };
            pngExporter.Export(model, stream);
            Image image = Image.FromStream(stream);
            image.Tag = title;
            return image;
        }

        public static void FillCells(ComboBox dd, CellPool pool, bool allCells = true)
        {
            dd.Items.Clear();
            if (pool.GetCells().Any())
            {
                if (allCells)
                    dd.Items.Add("All Cells");
                dd.Items.AddRange(pool.GetCells().ToArray());
                dd.SelectedIndex = 0;

            }
        }
    }
}
