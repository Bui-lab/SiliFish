using System;
using System.Collections.Generic;
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
            return String.Format("[{0:0.##}, {1:0.##}, {2:0.##}]", X, Y, Z);
        }
        public static implicit operator Coordinate(ValueTuple<double, double, double> values) => new(values.Item1, values.Item2, values.Item3);
        public static implicit operator Coordinate(ValueTuple<double, double> values) => new(values.Item1, values.Item2);
    }

    public class TimeLine
    {
        private List<(int start, int end)> Periods = new();
        
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
        public int Start { get { return Periods.Count > 0 ? Periods.Min(tr => tr.start) : 0; }  }
        [JsonIgnore]
        public int End { get { return Periods.Count > 0 ? Periods.Max(tr => tr.end) : -1; } set { } }

        public TimeLine()
        { }

        public TimeLine(TimeLine tl)
        {
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
            return string.Join("; ", Periods.Select(tr => $"{tr.start}-{((tr.end == -1) ? "end":tr.end)}"));
        }
    }


}