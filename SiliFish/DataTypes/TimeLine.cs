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
  
        public TimeLine()
        { }

        public TimeLine(TimeLine tl)
        {
            if (tl != null && tl.Periods.Any())
                Periods = new List<(int start, int end)>(tl.Periods);
        }
        public TimeLine(TimeLine tl, double multiplier)
        {
            if (tl != null && tl.Periods.Any())
                Periods = new List<(int start, int end)>(tl.Periods.Select(p => ((int)(p.start * multiplier), (int)(p.end == -1 ? p.end : p.end * multiplier))).ToList());
        }
        public void AddTimeRange(int start_ms, int? end_ms = null)
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