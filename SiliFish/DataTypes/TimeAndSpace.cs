using System;
using System.Collections.Generic;
using System.Linq;

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
        private List<(int start, int end)> timeLine = new();
        public int Start { get { return timeLine.Count > 0 ? timeLine.Min(tr => tr.start) : 0; }  }
        public int End { get { return timeLine.Count > 0 ? timeLine.Max(tr => tr.end) : -1; } set { } }

        public TimeLine()
        { }

        public TimeLine(TimeLine tl)
        {
            timeLine = new List<(int start, int end)>(tl.timeLine);
        }
        public void AddTimeRange(int start_ms, int? end_ms = null)
        {
            timeLine.Add((start_ms, end_ms ?? -1));
        }

        public bool IsBlank()
        {
            return timeLine.Count == 0;
        }
        public bool IsActive(int time_ms)
        {
            if (timeLine.Count == 0) return true;
            return timeLine.Exists(timeRange => timeRange.start <= time_ms && (timeRange.end < 0 || timeRange.end >= time_ms));
        }

        public List<(int start, int end)> GetTimeLine()
        {
            return timeLine;
        }

        public void Clear()
        {
            timeLine.Clear();
        }

        public override string ToString()
        {
            if (timeLine.Count == 0)
                return "no timeline";
            return string.Join("; ", timeLine.Select(tr => $"{tr.start}-{((tr.end == -1) ? "end":tr.end)}"));
        }
    }


}