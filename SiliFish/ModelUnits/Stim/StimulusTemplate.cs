using SiliFish.DataTypes;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusTemplate : StimulusBase, IDataExporterImporter
    {
        public string TargetPool { get; set; }
        public string TargetSomite { get; set; } = "All";
        public string TargetCell { get; set; } = "All";

        public string LeftRight { get; set; }
        public double DelayPerSomite { get; set; }
        public double DelaySagittal { get; set; }


        public StimulusTemplate() { }

        public override ModelUnitBase CreateCopy()
        {
            StimulusTemplate stim = new()
            {
                TargetPool = TargetPool,
                TargetSomite = TargetSomite,
                TargetCell = TargetCell,
                LeftRight = LeftRight,
                DelayPerSomite = DelayPerSomite,
                DelaySagittal = DelaySagittal,
                Settings = Settings.Clone(),
                TimeLine_ms = new(TimeLine_ms)
            };
            return stim;
        }
        //TODO DiffersFrom is not implemented
        public override int CompareTo(ModelUnitBase otherbase)
        {
            StimulusTemplate other = otherbase as StimulusTemplate;
            return TargetPool.CompareTo(other.TargetPool);
        }
        public override string ToString()
        {
            string timeline = TimeLine_ms != null && !TimeLine_ms.IsBlank() ? TimeLine_ms.ToString() : "";
            string active = Active ? "" : " (inactive)";
            return $"{ID}: {Settings} {timeline} {active}".Replace("  ", " ");
        }
        [JsonIgnore]
        public override string ID => $"Target: {LeftRight} {TargetPool}-{TargetSomite} {TargetCell}; {Settings?.ToString()}";
        [JsonIgnore]
        public override string Tooltip => $"{ToString()}\r\n{Settings?.ToString()}\r\n{TimeLine_ms}";

        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames { get; } =
            ListBuilder.Build<string>("TargetPool", "TargetSomite", "TargetCell", "LeftRight", "DelayPerSomite", "DelaySagittal", "Active",  StimulusSettings.ColumnNames, TimeLine.ColumnNames);

        public List<string> ExportValues()
        {
            return ListBuilder.Build<string>(TargetPool, TargetSomite, TargetCell, LeftRight, DelayPerSomite, DelaySagittal, Active, Settings.ExportValues(), TimeLine_ms.ExportValues());
        }
        public void ImportValues(List<string> values)
        {
            if (values.Count != ColumnNames.Count) return;
            TargetPool = values[0];
            TargetSomite = values[1];
            TargetCell = values[2];
            LeftRight = values[3];
            if (double.TryParse(values[4], out double d))
                DelayPerSomite = d;
            if (double.TryParse(values[5], out d))
                DelaySagittal = d;
            Active = bool.Parse(values[6]);
            int lastSettingsCol = StimulusSettings.ColumnNames.Count + 7;
            Settings.ImportValues(values.Take(new Range(7, lastSettingsCol)).ToList());
            TimeLine_ms.ImportValues(values.Take(new Range(lastSettingsCol, values.Count)).ToList());
        }
    }
}
