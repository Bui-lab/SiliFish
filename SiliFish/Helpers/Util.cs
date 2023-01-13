using SiliFish.DataTypes;
using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;


namespace SiliFish.Helpers
{
    public class Util
    {

        public static string JavaScriptEncode(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            if (s.StartsWith('`') && s.EndsWith('`'))
                s = s[1..^1];
            s = s.Replace("\\`", "`");//to prevent double escape
            s = s.Replace("`", "\\`");
            s = '`' + s + '`';
            return s;
        }
        public static int NumOfDigits(double val)
        {
            int digits = 0;
            val = Math.Abs(val);
            while (val >= 1)
            {
                digits++;
                val /= 10;
            }
            return digits;
        }
        public static int NumOfDecimalDigits(double val)
        {
            return NumOfDecimalDigits((decimal)val);
        }
        public static int NumOfDecimalDigits(decimal val)
        {
            int[] bits = Decimal.GetBits(val);
            int decPoints = (bits[3] >> 16) & 0x7F;//https://docs.microsoft.com/en-us/dotnet/api/system.decimal.getbits?view=net-6.0
            //decimal inc = decPoints == 0 ? 1 : (decimal)(1 / (Math.Pow(10, decPoints)));
            return decPoints;
        }

        /// <summary>
        /// Currently not used
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private (double, double) CalculateRange(double value)
        {
            if (value == 0)
                return (CurrentSettings.Settings.GeneticAlgorithmMinValue, CurrentSettings.Settings.GeneticAlgorithmMaxValue);
            int numDigit = Util.NumOfDigits(value);
            int numDecimal = Util.NumOfDecimalDigits((decimal)value);
            if (numDigit == 0)
                numDigit = 1;

            double minValue = value - 10 * numDigit;
            if (value > 0 && minValue <= 0)
                minValue = Math.Pow(10, -numDecimal);

            double maxValue = value + 10 * numDigit;
            if (value < 0 && maxValue >= 0)
                maxValue = -Math.Pow(10, -numDecimal);

            return (minValue, maxValue);
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

        public static List<int> ParseContinousRange(string s)
        {
            List<int> ints = new();
            int i1;
            int sep = s.IndexOfAny(new[] { '-', '_', '.' });
            if (sep < 0)
            {
                if (int.TryParse(s, out i1))
                    ints.Add(i1);
                return ints;
            }
            if (!int.TryParse(s[..sep], out i1))
                i1 = 0;
            if (!int.TryParse(s[(sep + 1)..], out int i2))
                i2 = i1;
            if (i2 == i1)
            {
                ints.Add(i1);
                return ints;
            }
            if (i1 > i2)
                (i1, i2) = (i2, i1);
            return Enumerable.Range(i1, i2 - i1 + 1).ToList();
        }

        public static List<int> ParseRange(string s, int defMin, int defMax)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (defMin > defMax)
                    (defMin, defMax) = (defMax, defMin);
                return Enumerable.Range(defMin, defMax - defMin + 1).ToList();
            }
            List<string> uncontinousRanges = s.Split(',').ToList();
            if (uncontinousRanges.Count == 0)
                return ParseContinousRange(s);
            List<int> ints = new();
            foreach (string subrange in uncontinousRanges)
                ints.AddRange(ParseContinousRange(subrange));
            return ints;
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
                if (yMin > 0 && (yMax - yMin) > yMin / 10) //do not start from zero if the values are close to each other
                    yMin = 0;
                else if (yMax - yMin < CurrentSettings.Settings.Epsilon)
                    yMin *= 0.9;
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

        static public string GetUoM(UnitOfMeasure uom, Measure measure)
        {
            switch (measure)
            {
                case Measure.Voltage:
                    return "mV";
                case Measure.Current:
                    return uom == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad ? "pA" : "nA";
                case Measure.Resistance:
                    return uom == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad ? "GΩ" : "MΩ";
                case Measure.Capacitance:
                    return uom == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad ? "pF" : "nF";
            }
            return "";
        }

        static public double[] GenerateValues(double origValue, double minMultiplier, double maxMultiplier, int numOfPoints, bool logScale)
        {
            if (maxMultiplier < minMultiplier)
                (minMultiplier, maxMultiplier) = (maxMultiplier, minMultiplier);

            double[] values = new double[numOfPoints];
            if (!logScale)
            {
                double incMultiplier = (maxMultiplier - minMultiplier) / (numOfPoints - 1);
                foreach (int i in Enumerable.Range(0, numOfPoints))
                    values[i] = (incMultiplier * i + minMultiplier) * origValue;
            }
            else
            {
                double logMinMultiplier = Math.Log10(minMultiplier);
                double logMaxMultiplier = Math.Log10(maxMultiplier);
                double incMultiplier = (logMaxMultiplier - logMinMultiplier) / (numOfPoints - 1);
                foreach (int i in Enumerable.Range(0, numOfPoints))
                    values[i] = Math.Pow(10, incMultiplier * i + logMinMultiplier) * origValue;
            }
            return values;
        }
    }
}

