using SiliFish.DataTypes;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusTemplate : StimulusBase, IDataExporterImporter
    {
        private string targetPool;
        public string TargetPool
        {
            get { return targetPool; }
            set
            {
                bool rename = GeneratedName() == Name;
                targetPool = value;
                if (rename) Name = GeneratedName();
            }
        }
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
                Name = Name,
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
        public override string GeneratedName()
        {
            return $"Stim to {TargetPool}";
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
            return $"{ID}: {timeline} {active}".Replace("  ", " ");
        }
        [JsonIgnore]
        public override string ID => $"{Name}\r\nTarget: {LeftRight} {TargetPool}-{TargetSomite} {TargetCell}; {Settings?.ToString()}";
        [JsonIgnore]
        public override string Tooltip => $"{ToString()}\r\nDelay/somite: {DelayPerSomite}\r\nSagittal Delay: {DelaySagittal}";

        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames { get; } =
            ListBuilder.Build<string>("Name", "TargetPool", "TargetSomite", "TargetCell", "LeftRight", "DelayPerSomite", "DelaySagittal", "Active",  StimulusSettings.ColumnNames, TimeLine.ColumnNames);

        public List<string> ExportValues()
        {
            return ListBuilder.Build<string>(Name, TargetPool, TargetSomite, TargetCell, LeftRight, DelayPerSomite, DelaySagittal, Active, Settings.ExportValues(), TimeLine_ms.ExportValues());
        }
        public void ImportValues(List<string> values)
        {
            if (values.Count != ColumnNames.Count) return;
            int i = 0;
            Name = values[i++];
            TargetPool = values[i++];
            TargetSomite = values[i++];
            TargetCell = values[i++];
            LeftRight = values[i++];
            if (double.TryParse(values[i++], out double d))
                DelayPerSomite = d;
            if (double.TryParse(values[i++], out d))
                DelaySagittal = d;
            Active = bool.Parse(values[i++]);
            int lastSettingsCol = StimulusSettings.ColumnNames.Count + i;
            Settings.ImportValues(values.Take(new Range(i, lastSettingsCol)).ToList());
            TimeLine_ms.ImportValues(values.Take(new Range(lastSettingsCol, values.Count)).ToList());
        }
    }
}
