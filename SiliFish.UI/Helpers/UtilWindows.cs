using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SiliFish.UI
{
    public class UtilWindows
    {
        public static string OutputFolder = @"C:\Master\Bui\SiliFish.Studio\Output\";


        public static void SaveImage(string path, Image img)
        {
            img.Save(path);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="colors"></param>
        /// <param name="AffarentCurrents">A dictionary of currents with a unique name as key, which is used for color determination</param>
        /// <returns></returns>
        public static Image CreateCurrentsPlot(string target, Dictionary<string, Color> colors, Dictionary<string, List<double>> AffarentCurrents, 
            int tstart, int tend, double[] Time, int width = 1024, int height = 480)
        {
            List<string> sources = AffarentCurrents.Keys.ToList();
            OxyPlot.PlotModel model = new PlotModel() { Title = target };
            foreach (string source in sources)
            {
                Color color = colors[source];
                OxyColor col = color.ToOxyColor();
                byte a = (byte)255;
                int numofcells = sources.Count;
                byte dec = (byte)(200 / numofcells);
                double[] values = AffarentCurrents[source].ToArray();
                LineSeries ls = new LineSeries();
                ls.Color = OxyColor.FromAColor(a, OxyColor.FromRgb(col.R, col.G, col.B));
                a -= dec;
                if (tend >= values.Length)
                    tend = values.Length - 1;
                ls.Points.AddRange(Enumerable
                    .Range(tstart, tend - tstart)
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

        public static Image? CreatePlotImage(string title, double[,] V, double[] Time, int tstart, int tend, Color color, int width = 1024, int height = 480)
        {
            if (V == null)
                return null;

            OxyPlot.PlotModel model = new PlotModel() { Title = title };

            OxyColor col = color.ToOxyColor();
            byte a = (byte)255;
            int numofcells = V.GetLength(0);
            if (numofcells == 0) return null;
            byte dec = (byte)(200 / numofcells);
            for (int c = 0; c < numofcells; c++)
            {
                LineSeries ls = new LineSeries();
                ls.Color = OxyColor.FromAColor(a, OxyColor.FromRgb(col.R, col.G, col.B));
                a -= dec;
                ls.Points.AddRange(Enumerable
                    .Range(tstart, tend - tstart)
                    .Select(i => new DataPoint(Time[i], V[c, i])));
                model.Series.Add(ls);
            }
            var stream = new MemoryStream();
            var pngExporter = new PngExporter { Width = width, Height = height};
            pngExporter.Export(model, stream);
            Image image = Image.FromStream(stream);
            image.Tag = title;
            return image;
        }

        public static Image? CreatePlotImage(string title, double[] values, double[] Time, int tstart, int tend, Color color, int width = 1024, int height = 480)
        {
            if (values == null)
                return null;

            OxyPlot.PlotModel model = new PlotModel() { Title = title };

            OxyColor col = color.ToOxyColor();
            byte a = (byte)255;
            LineSeries ls = new LineSeries();
            ls.Color = OxyColor.FromAColor(a, OxyColor.FromRgb(col.R, col.G, col.B));
            ls.Points.AddRange(Enumerable
                .Range(tstart, tend - tstart)
                .Select(i => new DataPoint(Time[i], values[i])));
            model.Series.Add(ls);

            var stream = new MemoryStream();
            var pngExporter = new PngExporter { Width = width, Height = height };
            pngExporter.Export(model, stream);
            Image image = Image.FromStream(stream);
            image.Tag = title;
            return image;
        }

    }
}
