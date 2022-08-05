using SiliFish.DataTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace SiliFish.Helpers
{
    public class Util
    {
        //https://stackoverflow.com/questions/69664644/serialize-deserialize-system-drawing-color-with-system-text-json
        public class ColorJsonConverter : JsonConverter<Color>
        {
            public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ColorTranslator.FromHtml(reader.GetString());

            public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) => writer.WriteStringValue("#" + value.R.ToString("X2") + value.G.ToString("X2") + value.B.ToString("X2").ToLower());
        }

        public static string CreateJSONFromObject(object content)
        {
            var options = new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() },
                WriteIndented = true
            };
            string jsonstring = JsonSerializer.Serialize(content, options);
            return jsonstring;
        }
        public static object CreateObjectFromJSON(Type content, string jsonstring)
        {
            var options = new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() }
            };
            return JsonSerializer.Deserialize(jsonstring, content, options);
        }
        public static string ReadFromFile(string path)
        {
            return File.ReadAllText(path);
        }

        public static void SaveToFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public static void SaveToJSON(string path, object content)
        {
            var options = new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() },
                WriteIndented = true
            };
            string jsonstring = JsonSerializer.Serialize(content, options);
            File.WriteAllText(path, jsonstring);
        }

        public static Dictionary<string, object> ReadDictionaryFromJSON(string path)
        {
            string jsonstring = File.ReadAllText(path);
            var options = new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() }
            };
            return (Dictionary<string, object>)JsonSerializer.Deserialize(jsonstring, typeof(Dictionary<string, object>), options);
        }

        public static void SaveModelDynamicsToCSV(string filename, double[] Time, Dictionary<string, double[]> Values)
        { 
            if (filename == null || Time == null || Values == null)
                return;

            using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
            using StreamWriter sw = new(fs);
            sw.WriteLine("Time," + string.Join(',', Values.Keys));

            for (int t = 0; t < Time.Length; t++)
            {
                string row = Time[t].ToString() + ',' +
                    string.Join(',', Values.Select(item => item.Value[t]));
                sw.WriteLine(row);
            }
        }

        public static void SaveTailMovementToCSV(string filename, double[] Time, Coordinate[] tail_tip_coord)
        {
            if (filename == null || tail_tip_coord == null)
                return;

            string tailFilename = Path.ChangeExtension(filename, "TailTip.csv");
            using FileStream fsTail = File.Open(tailFilename, FileMode.Create, FileAccess.Write);
            using StreamWriter swTail = new(fsTail);
            swTail.WriteLine("Time,X,Y,Z");
            for (int t = 0; t < Time.Length; t++)
            {
                if (t >= tail_tip_coord.Length)
                    break;
                string row = $"{Time[t]:0.##},{tail_tip_coord[t].X:0.000},{tail_tip_coord[t].Y:0.000},{tail_tip_coord[t].Z:0.000}";
                swTail.WriteLine(row);
            }
        }

        public static void SaveEpisodesToCSV(string filename, List<SwimmingEpisode> episodes)
        {
            if (filename == null || episodes == null)
                return;

            string episodeFilename = Path.ChangeExtension(filename, "Episodes.csv");
            string beatsFilename = Path.ChangeExtension(filename, "Beats.csv");
            using FileStream fsEpisode = File.Open(episodeFilename, FileMode.Create, FileAccess.Write);
            using FileStream fsBeats = File.Open(beatsFilename, FileMode.Create, FileAccess.Write);
            using StreamWriter swEpisode = new(fsEpisode);
            using StreamWriter swBeats = new(fsBeats);
            swEpisode.WriteLine("Episode,Start,Finish,Duration,Time to Next,Tail Beat Freq.,# of Tail Beats");
            swBeats.WriteLine("Episode,Beat,Start,Finish,Instan.Freq.");
            int epiCounter = 0;
            foreach (SwimmingEpisode episode in episodes)
            {
                double? timeToNext = null;
                if (epiCounter < episodes.Count - 1)
                {
                    SwimmingEpisode nextEpisode = episodes[epiCounter + 1];
                    timeToNext = nextEpisode.Start - episode.End;
                }
                string epiRow = $"{epiCounter}," +
                    $"{episode.Start:0.####}," +
                    $"{episode.End:0.####}," +
                    $"{episode.EpisodeDuration:0.####}," +
                    $"{timeToNext?.ToString() ?? ""}," +
                    $"{episode.BeatFrequency}," +
                    $"{episode.Beats.Count}";
                swEpisode.WriteLine(epiRow);
                int beatCounter = 0;
                foreach ((double beatStart, double beatEnd) in episode.Beats)
                {
                    string beatRow = $"{epiCounter}," +
                        $"{beatCounter++}," +
                        $"{beatStart:0.####}," +
                        $"{beatEnd:0.####}," +
                        $"{1000 / (beatEnd - beatStart):0.####}";
                    swBeats.WriteLine(beatRow);
                }
                epiCounter++;
            }
        }
        public static Dictionary<string, double[]> ReadFromCSV(string filename)
        {
            if (filename == null)
                return null;
            Dictionary<string, double[]> data = new();
            Dictionary<string, List<double>> tempdata = new();
            using StreamReader sr = new(filename);
            string line = sr.ReadLine();
            if (line == null) return null;
            string[] columns = line.Split(",");
            foreach (string column in columns)
                tempdata.Add(column, new List<double>());
            int counter = 0;
            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split(",");
                for (int i = 0; i < values.Length; i++)
                    tempdata[columns[i]].Add(double.Parse(values[i]));
                counter++;
                //if (counter > 10000) break;
            }
            foreach (string column in columns)
                data.Add(column, tempdata[column].ToArray());

            return data;
        }
        public static void SaveAnimation(string filename, Dictionary<string, Coordinate[]> somiteCoordinates, double[] Time, int startIndex)
        {
            using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
            using StreamWriter sw = new(fs);
            string columnHeaders = string.Join(',', somiteCoordinates.Keys.Select(k => k + "-X," + k + "-Y'"));
            sw.WriteLine(columnHeaders);
            int nmax = Time.Length - 1;
            foreach (var i in Enumerable.Range(1, nmax))
            {
                string rowStart = (Time[startIndex + i - 1]).ToString("0.##");
                string row = rowStart + string.Join(',', somiteCoordinates.Select(item => item.Value[i].X + "," + item.Value[i].X));
                sw.WriteLine(row);
            }
        }
        public static bool CheckOnlineStatus()
        {
            using var httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = httpClient.Send(new HttpRequestMessage(HttpMethod.Head, "https://unpkg.com/three"));
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public static string TimeSpanToString(TimeSpan ts)
        {
            string s = "";
            if (ts.TotalDays >= 1)
            {
                int td = (int)Math.Floor(ts.TotalDays);
                s = td.ToString() + " days; ";
                ts = ts.Subtract(new TimeSpan(td, 0, 0, 0));
            }
            int th = (int)Math.Floor(ts.TotalHours);
            s += th.ToString("00") + ":";
            ts = ts.Subtract(new TimeSpan(th, 0, 0));
            int tm = (int)Math.Floor(ts.TotalMinutes);
            s += tm.ToString("00") + ":";
            ts = ts.Subtract(new TimeSpan(0, tm, 0));
            s += Math.Floor(ts.TotalSeconds).ToString("00");
            return s;
        }

        public static (int, int) ParseRange(string s, int defMin = -1, int defMax = int.MaxValue)
        {
            int i1;
            int i2 = defMax;
            int sep = s.IndexOfAny(new[] { '-', '_', '.' });
            if (sep < 0)
            {
                if (int.TryParse(s, out i1))
                    i2 = i1;
                else i1 = defMin;
                return (i1, i2);
            }
            if (!int.TryParse(s[..sep], out i1))
                i1 = defMin;
            if (!int.TryParse(s[(sep + 1)..], out i2))
                i2 = defMax;
            return (i1, i2);
        }

        public static void SetYRange(ref double yMin, ref double yMax)
        {
            if (yMax > 0 && yMin < 0)
            {
                yMax *= 1.1;
                yMin *= 1.1;
            }
            else
            {
                double padding = (yMax - yMin) / 10;
                yMin -= padding;
                if (yMin > 0)
                    yMin = 0;
                yMax += padding;
            }
        }

        static public double Distance(Coordinate point1, Coordinate point2, DistanceMode mode)
        {
            double x = Math.Abs(point1.X - point2.X);
            double y = Math.Abs(point1.Y - point2.Y);
            double z = Math.Abs(point1.Z - point2.Z);
            return mode switch
            {
                DistanceMode.Manhattan => x + y + z,
                DistanceMode.Euclidean => Math.Sqrt(x * x + y * y + z * z),
                //FUTURE_IMPROVEMENT
                //case DistanceMode.Chebyshev:
                //    return Math.Max(x, Math.Max(y, z));
                //case DistanceMode.Haversine:
                //Euclidean
                _ => Math.Sqrt(x * x + y * y + z * z),
            };
        }
    }
}
