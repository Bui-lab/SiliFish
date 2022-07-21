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

        public static void SaveToCSV(string filename, double[] Time, Dictionary<string, double[]> Values)
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
            int i1 = defMin;
            int i2 = defMax;
            int sep = s.IndexOfAny(new[] { '-', '_', '.' });
            if (sep < 0)
            {
                if (int.TryParse(s, out i1))
                    i2 = i1;
                return (i1, i2);
            }
            int.TryParse(s[..sep], out i1);
            int.TryParse(s[(sep + 1)..], out i2);
            return (i1, i2);
        }
    }
}
