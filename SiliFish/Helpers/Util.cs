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

        public static bool CheckOnlineStatus(string src)
        {
            using var httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = httpClient.Send(new HttpRequestMessage(HttpMethod.Head, src));
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
            List<int> ints = [];
            int i1;
            int sep = s.IndexOfAny(['-', '_', '.']);
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
            List<string> uncontinousRanges = [.. s.Split(',')];
            if (uncontinousRanges.Count == 0)
                return ParseContinousRange(s);
            List<int> ints = [];
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
                else if (yMax - yMin < GlobalSettings.Epsilon)
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
            return measure switch
            {
                Measure.Voltage => "mV",
                Measure.Current => uom == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens ? "pA" : "nA",
                Measure.Resistance => uom == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens ? "GΩ" : "MΩ",
                Measure.Capacitance => uom == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens ? "pF" : "nF",
                Measure.Conductance => uom == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens ? "nS" : "µS",
                _ => "",
            };
        }

        static public double[] GenerateValues(double origValue, NumberRangeDefinition rangeDefinition)
        {
            double minMultiplier = rangeDefinition.MinMultiplier;
            double maxMultiplier = rangeDefinition.MaxMultiplier;
            int numOfPoints = rangeDefinition.NumOfPoints;
            bool logScale = rangeDefinition.LogScale;
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

        static public (double[], List<double[]> yMultiData) MergeXYArrays(double[] xArray1, double[] yArray1, double[] xArray2, double[] yArray2)
        {
            if (xArray1.Length != yArray1.Length || xArray2.Length != yArray2.Length)
                throw new Exception("Merging XY arrays with different lengths");
            List<double> xList = [];
            List<double[]> yMultiData = [];
            int ind1 = 0;
            int ind2 = 0;
            while (ind1 < xArray1.Length && ind2 < xArray2.Length)
            {
                if (xArray1[ind1] == xArray2[ind2])
                {
                    xList.Add(xArray1[ind1]);
                    yMultiData.Add([yArray1[ind1++], yArray2[ind2++]]);
                }
                else if (xArray1[ind1] < xArray2[ind2])
                {
                    xList.Add(xArray1[ind1]);
                    yMultiData.Add([yArray1[ind1++], double.NaN]);
                }
                else //if (xArray1[ind1] > xArray2[ind2])
                {
                    xList.Add(xArray2[ind2]);
                    yMultiData.Add([double.NaN, yArray2[ind2++]]);
                }
            }
            while (ind1 < xArray1.Length)
            {
                xList.Add(xArray1[ind1]);
                yMultiData.Add([yArray1[ind1++], double.NaN]);
            }
            while (ind2 < xArray2.Length)
            {
                xList.Add(xArray2[ind2]);
                yMultiData.Add([double.NaN, yArray2[ind2++]]);
            }
            return (xList.ToArray(), yMultiData);
        }

    }
}

