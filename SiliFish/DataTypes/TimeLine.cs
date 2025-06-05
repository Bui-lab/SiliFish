using SiliFish.Extensions;
using SiliFish.ModelUnits;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.DataTypes
{
    public class TimeLine : IDataExporterImporter
    {
        private List<(double start, double end)> Periods = [];
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
                        (double s, double e) se = (double.Parse(searr[0]), double.Parse(searr[1]));
                        Periods.Add(se);
                    }
                }
            }
        }
        [JsonIgnore]
        public double Start { get { return Periods.Count > 0 ? Periods.Min(tr => tr.start) : 0; } }
        [JsonIgnore]
        public double End { get { return Periods.Count > 0 ? Periods.Max(tr => tr.end) : -1; } set { } }


        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames { get; } = ["Periods"];

        public List<string> ExportValues() =>
            [string.Join(";", Periods.Select(p => $"{p.start} - {p.end}"))];

        public void ImportValues(List<string> values)
        {
            if (values.Count != ColumnNames.Count) return;
            string[] periods = values[0].Split(';');
            foreach (string period in periods)
            {
                if (string.IsNullOrEmpty(period))
                    continue;
                (double i, double j) = period.ParseRange(defStart: 0, defEnd: -1);
                Periods.Add((i, j));
            }
        }

        public TimeLine()
        { }

        public TimeLine(TimeLine tl)
        {
            if (tl != null && tl.Periods.Count != 0)
                Periods = new List<(double start, double end)>(tl.Periods);
        }
        public void AddTimeRange(double start_ms, double? end_ms = null)
        {
            Periods.Add((start_ms, end_ms ?? -1));
        }

        public bool IsBlank()
        {
            return Periods.Count == 0;
        }
        public bool IsActive(double time)
        {
            if (Periods.Count == 0) return true;
            return Periods.Exists(timeRange => timeRange.start <= time && (timeRange.end < 0 || timeRange.end >= time));
        }
        public double StartOf(double time_ms)
        {
            if (Periods.Count == 0) return 0;
            var (start, end) = Periods.FirstOrDefault(timeRange => timeRange.start <= time_ms && (timeRange.end < 0 || timeRange.end >= time_ms));
            return start;
        }
        public void AddOffset(double delay_ms)
        {
            if (delay_ms < double.Epsilon)
                return;
            if (Periods.Count == 0)
                Periods.Add((delay_ms, -1));
            else
                Periods = Periods.Select(p => (p.start + delay_ms, p.end == -1 ? p.end : p.end + delay_ms)).ToList();
        }
        public void MultiplyBy(double multiplier)
        {
            Periods = Periods.Select(p => (p.start * multiplier, p.end == -1 ? p.end : p.end * multiplier)).ToList();
        }
        public List<(double start, double end)> GetTimeLine()
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