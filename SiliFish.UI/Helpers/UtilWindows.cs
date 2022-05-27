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

        public static void SaveToJSON(string path, object content)
        {
            string jsonstring = JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, jsonstring);
        }

        public static void SaveCurrentsToCSV(string filename, double[] Time, Dictionary<string, List<double>> synapticCurrents, Dictionary<string, List<double>> gapCurrents)
        {
            using FileStream fsSyn = File.Open(OutputFolder + "Syn" + filename, FileMode.Create, FileAccess.Write);
            using FileStream fsGap = File.Open(OutputFolder + "Gap" + filename, FileMode.Create, FileAccess.Write);
            using StreamWriter swSyn = new(fsSyn);
            using StreamWriter swGap = new(fsGap);

            string headers = "Time," + string.Join(',', synapticCurrents.Keys);
            swSyn.WriteLine(headers);

            headers = "Time," + string.Join(',', gapCurrents.Keys);
            swGap.WriteLine(headers);

            List<double>[] synCurArr = synapticCurrents.Values.ToArray();
            List<double>[] gapCurArr = gapCurrents.Values.ToArray();
            for (int t = 1; t < Time.Length; t++)
            {
                string rowSyn = Time[t - 1].ToString();
                string rowGap = Time[t - 1].ToString();
                foreach (var listvalues in synCurArr)
                    if (listvalues.Count == 0)
                        rowSyn += ",0";
                    else
                        rowSyn += "," + listvalues[t - 1].ToString();
                foreach (var listvalues in gapCurArr)
                    if (listvalues.Count == 0)
                        rowGap += ",0";
                    else
                        rowGap += "," + listvalues[t - 1].ToString();
                swSyn.WriteLine(rowSyn);
                swGap.WriteLine(rowGap);
            }
        }

        public static void SaveToCSV(string filename, double[] Time, List<string> cell_names, List<double[,]> data_lists)

        {
            //check for input accuracy - a file name and Time array need to be provided
            //the number of data_lists needs to be twice as the number of cell_names: for left and right
            if (filename == null || Time == null || cell_names == null || cell_names.Count == 0 || data_lists == null || data_lists.Count != 2 * cell_names.Count)
            {
                return;
            }

            using FileStream fs = File.Open(UtilWindows.OutputFolder + filename, FileMode.Create, FileAccess.Write);
            using StreamWriter sw = new(fs);
            int numofcelltypes = cell_names.Count;
            int[] num_cells = new int[numofcelltypes];

            //For every cell type, there are two data lists: Left and Right.
            foreach (var i in Enumerable.Range(0, numofcelltypes))
            {
                num_cells[i] = data_lists[2 * i].GetLength(0);
            }

            string headers = "Time";
            foreach (var i in Enumerable.Range(0, cell_names.Count))
            {
                foreach (var j in Enumerable.Range(0, Convert.ToInt32(num_cells[i]) - 0))
                {
                    headers += ",Left_" + cell_names[i] + j.ToString();
                }
                foreach (var k in Enumerable.Range(0, Convert.ToInt32(num_cells[i]) - 0))
                {
                    headers += ",Right_" + cell_names[i] + k.ToString();
                }
            }

            sw.WriteLine(headers);

            for (int t = 0; t < Time.Length; t++)
            {
                string row = Time[t].ToString();
                foreach (var i in Enumerable.Range(0, cell_names.Count))
                {
                    foreach (var j in Enumerable.Range(0, Convert.ToInt32(num_cells[i]) - 0))
                    {
                        var values = data_lists[i * 2];
                        row += "," + values[j, t].ToString();// col_df = header_name + "," + string.Join(',', values.ConvertRowToList(j));
                    }
                    foreach (var k in Enumerable.Range(0, Convert.ToInt32(num_cells[i]) - 0))
                    {
                        var values = data_lists[i * 2 + 1];
                        row += "," + values[k, t].ToString();
                    }
                }
                sw.WriteLine(row);
            }

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
