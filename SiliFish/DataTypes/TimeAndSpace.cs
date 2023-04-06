using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Stim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.DataTypes
{
    public struct Coordinate
    {
        public double X;
        public double Y;
        public double Z;
        public Coordinate() { X = Y = Z = 0; }
        public Coordinate(double x, double y, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override string ToString()
        {
            return $"[{X.ToString(GlobalSettings.CoordinateFormat)}, {Y.ToString(GlobalSettings.CoordinateFormat)}, {Z.ToString(GlobalSettings.CoordinateFormat)}]";
        }
        public static implicit operator Coordinate(ValueTuple<double, double, double> values) => new(values.Item1, values.Item2, values.Item3);
        public static implicit operator Coordinate(ValueTuple<double, double> values) => new(values.Item1, values.Item2);
        [JsonIgnore, Browsable(false)]
        public static string CSVExportColumnNames => $"X,Y,Z";

        [JsonIgnore, Browsable(false)]
        private static int CSVExportColumCount => CSVExportColumnNames.Split(',').Length;
        [JsonIgnore, Browsable(false)]
        public string CSVExportValues => $"{X},{Y},{Z}";
        public static Coordinate[] GenerateCoordinates(Random random,ModelDimensions modelDimensions, BodyLocation BodyLocation,
            Distribution XDistribution, Distribution Y_AngleDistribution,  Distribution Z_RadiusDistribution, int n, int somite = -1)
        {
            if (n <= 0) return null;
            Coordinate[] coordinates = new Coordinate[n];
            Distribution.Random = random;

            double x_length = 0, x_offset = 0;
            double y_length = 0, y_offset = 0;
            double z_length = 0, z_offset = 0;
            double radius = 0; //FUTURE_IMPROVEMENT use two radii for elliptic shapes
            switch (BodyLocation)
            {
                case BodyLocation.SpinalCord:
                    x_length = somite < 0 ? modelDimensions.SpinalRostralCaudalDistance : modelDimensions.SpinalRostralCaudalDistance / modelDimensions.NumberOfSomites;
                    x_offset = modelDimensions.SupraSpinalRostralCaudalDistance + (somite > 0 ? (somite - 1) * modelDimensions.SpinalRostralCaudalDistance / modelDimensions.NumberOfSomites : 0);
                    y_length = modelDimensions.SpinalMedialLateralDistance;
                    z_length = modelDimensions.SpinalDorsalVentralDistance;
                    z_offset = modelDimensions.SpinalBodyPosition;
                    radius = Math.Sqrt(Math.Pow(modelDimensions.SpinalMedialLateralDistance, 2) + Math.Pow(modelDimensions.SpinalDorsalVentralDistance / 2, 2));
                    break;
                case BodyLocation.MusculoSkeletal:
                    x_length = somite < 0 ? modelDimensions.SpinalRostralCaudalDistance : modelDimensions.SpinalRostralCaudalDistance / modelDimensions.NumberOfSomites;
                    x_offset = modelDimensions.SupraSpinalRostralCaudalDistance + (somite > 0 ? (somite - 1) * modelDimensions.SpinalRostralCaudalDistance / modelDimensions.NumberOfSomites : 0);
                    y_length = modelDimensions.BodyMedialLateralDistance;
                    z_length = modelDimensions.BodyDorsalVentralDistance;
                    radius = Math.Sqrt(Math.Pow(modelDimensions.BodyMedialLateralDistance, 2) + Math.Pow(modelDimensions.BodyDorsalVentralDistance / 2, 2));
                    break;
                case BodyLocation.SupraSpinal:
                    x_length = modelDimensions.SupraSpinalRostralCaudalDistance;
                    y_length = modelDimensions.SupraSpinalMedialLateralDistance;
                    z_length = modelDimensions.SupraSpinalDorsalVentralDistance;
                    radius = Math.Sqrt(Math.Pow(modelDimensions.SupraSpinalMedialLateralDistance, 2) + Math.Pow(modelDimensions.SupraSpinalDorsalVentralDistance / 2, 2));
                    break;
            }
            double[] x = XDistribution?.GenerateNNumbers(n, x_length, ordered: true) ?? Distribution.GenerateNRandomNumbers(n, x_length, ordered: true);
            double[] y = new double[n];
            double[] z = new double[n];

            if (Y_AngleDistribution != null && Y_AngleDistribution.Angular)
            {
                //FUTURE_IMPROVEMENT radii and angles should be generated together to handle elliptic shapes
                double[] angles = Y_AngleDistribution?.GenerateNNumbers(n, 180, ordered: false) ?? Distribution.GenerateNRandomNumbers(n, 180, ordered: false);
                double centerZ = z_length / 2;
                double[] radii = Z_RadiusDistribution?.GenerateNNumbers(n, radius, ordered: false) ?? Distribution.GenerateNRandomNumbers(n, radius, ordered: false);
                foreach (int i in Enumerable.Range(0, n))
                {
                    double radian = angles[i] * Math.PI / 180;
                    y[i] = Math.Sin(radian) * radii[i];
                    z[i] = centerZ - Math.Cos(radian) * radii[i];
                }
            }
            else
            {
                y = Y_AngleDistribution?.GenerateNNumbers(n, y_length, ordered: false) ?? Distribution.GenerateNRandomNumbers(n, y_length, ordered: false);
                z = Z_RadiusDistribution?.GenerateNNumbers(n, z_length, ordered: false) ?? Distribution.GenerateNRandomNumbers(n, z_length, ordered: false);
            }

            foreach (int i in Enumerable.Range(0, n))
            {
                coordinates[i].X = x[i] + x_offset;
                coordinates[i].Y = y[i] + y_offset;
                coordinates[i].Z = z[i] + z_offset;
            }
            return coordinates;
        }

    }

    public class TimeLine
    {
        private List<(int start, int end)> Periods = new();
        //private Dictionary<(int start, int end), (int active, int rest)> Cycles = new();
        public string PeriodsJSON//Required for JSON
        {
            get
            {
                return string.Join('/', Periods.Select(i => i.start.ToString() + ":" + i.end.ToString()));
            }
            set
            {
                Periods.Clear();
                foreach (string period in value.Split('/'))
                {
                    if (period.Contains(':'))
                    {
                        string[] searr = period.Split(':');
                        (int s, int e) se = (int.Parse(searr[0]), int.Parse(searr[1]));
                        Periods.Add(se);
                    }
                }
            }
        }
        [JsonIgnore]
        public int Start { get { return Periods.Count > 0 ? Periods.Min(tr => tr.start) : 0; } }
        [JsonIgnore]
        public int End { get { return Periods.Count > 0 ? Periods.Max(tr => tr.end) : -1; } set { } }

        [JsonIgnore, Browsable(false)   ]
        public static string CSVExportColumnNames => $"Periods";

        [JsonIgnore, Browsable(false)]
        public string CSVExportValues
        {
            get => $"{string.Join(";", Periods.Select(p => $"{p.start} - {p.end}"))}";
            set 
            {
                string[] periods = value.Split(';');
                foreach (string period in periods)
                {
                    if (string.IsNullOrEmpty(period))
                        continue;
                    int sep = period.IndexOf(" - ");
                    if (sep == -1)
                    {
                        if (int.TryParse(period, out int i))
                        {
                            Periods.Add((i, -1));
                            continue;
                        }
                    }
                    if (int.TryParse(period.AsSpan(0, sep), out int j))
                    {
                        if (int.TryParse(period.AsSpan(sep + 3), out int k))
                            Periods.Add((j, k));
                        else
                            Periods.Add((j, -1));
                    }
                }
            }
        }

        public TimeLine()
        { }

        public TimeLine(TimeLine tl)
        {
            if (tl != null && tl.Periods.Any())
                Periods = new List<(int start, int end)>(tl.Periods);
        }
        public void AddTimeRange(int start_ms, int? end_ms = null)
        {
            Periods.Add((start_ms, end_ms ?? -1));
        }

        public bool IsBlank()
        {
            return Periods.Count == 0;
        }
        public bool IsActive(double time_ms)
        {
            if (Periods.Count == 0) return true;
            return Periods.Exists(timeRange => timeRange.start <= time_ms && (timeRange.end < 0 || timeRange.end >= time_ms));
        }
        public int StartOf(double time_ms)
        {
            if (Periods.Count == 0) return 0;
            var (start, end) = Periods.FirstOrDefault(timeRange => timeRange.start <= time_ms && (timeRange.end < 0 || timeRange.end >= time_ms));
            return start;
        }
        public List<(int start, int end)> GetTimeLine()
        {
            return Periods;
        }

        public void Clear()
        {
            Periods.Clear();
        }

        public override string ToString()
        {
            if (Periods.Count == 0)
                return "no timeline";
            return string.Join("; ", Periods.Select(tr => $"{tr.start}-{((tr.end == -1) ? "end" : tr.end)}"));
        }
    }


}