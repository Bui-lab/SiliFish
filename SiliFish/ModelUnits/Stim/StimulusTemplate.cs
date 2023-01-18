using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusTemplate : StimulusBase
    {
        public string TargetPool { get; set; }
        public string TargetSomite { get; set; }
        public string TargetCell { get; set; }

        public string LeftRight { get; set; }

        public StimulusTemplate() { }

        public override StimulusBase CreateCopy()
        {
            StimulusTemplate stim = new()
            {
                TargetPool = TargetPool,
                TargetSomite = TargetSomite,
                TargetCell = TargetCell,
                LeftRight = LeftRight
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
    }

}
