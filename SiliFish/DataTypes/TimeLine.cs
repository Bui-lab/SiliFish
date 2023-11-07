using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Stim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using static OfficeOpenXml.ExcelErrorValue;

namespace SiliFish.DataTypes
{
    public class TimeLine : IDataExporterImporter
    {
        private List<(double start, double end)> Periods = new();
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
        public static List<string> ColumnNames { get; } = new() { "Periods" };

        public List<string> ExportValues() =>
            new() { string.Join(";", Periods.Select(p => $"{p.start} - {p.end}"))};

        public void ImportValues(List<string> values)
        {
            if (values.Count != ColumnNames.Count) return;
            string[] periods = values[0].Split(';');
            foreach (string period in periods)
            {
                if (string.IsNullOrEmpty(period))
                    continue;
                int sep = period.IndexOf(" - ");
                if (sep == -1)
                {
                    if (double.TryParse(period, out double i))
                    {
                        Periods.Add((i, -1));
                        continue;
                    }
                }
                if (double.TryParse(period.AsSpan(0, sep), out double j))
                {
                    if (double.TryParse(period.AsSpan(sep + 3), out double k))
                        Periods.Add((j, k));
                    else
                        Periods.Add((j, -1));
                }
            }
        }
  
        public TimeLine()
        { }

        public TimeLine(TimeLine tl)
        {
            if (tl != null && tl.Periods.Any())
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
            if (Periods.Count == 0 || delay_ms < double.Epsilon) 
                return;
            Periods = Periods.Select(p => (p.start + delay_ms, p.end == -1 ? p.end : p.end + delay_ms)).ToList();
        }
        public void MultiplyBy (double multiplier)
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