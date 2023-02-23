using SiliFish.DataTypes;
using SiliFish.ModelUnits.Cells;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusTemplate : StimulusBase
    {
        public string TargetPool { get; set; }
        public string TargetSomite { get; set; } = "All";
        public string TargetCell { get; set; } = "All";

        public string LeftRight { get; set; }

        public StimulusTemplate() { }

        public override ModelUnitBase CreateCopy()
        {
            StimulusTemplate stim = new()
            {
                TargetPool = TargetPool,
                TargetSomite = TargetSomite,
                TargetCell = TargetCell,
                LeftRight = LeftRight,
                Settings = Settings.Clone(),
                TimeLine_ms = new(TimeLine_ms)
            };
            return stim;
        }

        public override int CompareTo(ModelUnitBase otherbase)
        {
            StimulusTemplate other = otherbase as StimulusTemplate;
            return TargetPool.CompareTo(other.TargetPool);
        }
        public override string ToString()
        {
            return ID + (Active ? "" : " (inactive)");
        }
        [JsonIgnore]
        public override string ID => $"Target: {LeftRight} {TargetPool}-{TargetSomite} {TargetCell}; {Settings?.ToString()}";
        [JsonIgnore]
        public override string Tooltip => $"{ToString()}\r\n{Settings?.ToString()}\r\n{TimeLine_ms}";

        [JsonIgnore]
        public static string CSVExportColumnNames => $"TargetPool,TargetSomite,TargetCell,LeftRight,{StimulusSettings.CSVExportColumnNames},{TimeLine.CSVExportColumnNames}";
        private static int CSVExportColumCount => CSVExportColumnNames.Split(',').Length;
        [JsonIgnore]
        public string CSVExportValues
        {
            get => $"{TargetPool},{TargetSomite},{TargetCell},{LeftRight},{Settings.CSVExportValues},{TimeLine_ms.CSVExportValues}";

            set
            {
                string[] values = value.Split(',');
                if (values.Length != CSVExportColumCount) return;
                TargetPool = values[0];
                TargetSomite = values[1];
                TargetCell = values[2];
                LeftRight = values[3];
                Settings.CSVExportValues = string.Join(",", values[4..(StimulusSettings.CSVExportColumCount + 3)]);
                TimeLine_ms.CSVExportValues = string.Join(",", values[(StimulusSettings.CSVExportColumCount + 4)..]);
            }
        }
    }

}
