using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusTemplate : ModelUnitBase
    {
        public string TargetPool { get; set; }
        public string TargetSomite { get; set; }
        public string TargetCell { get; set; }

        public StimulusSettings StimulusSettings { get; set; }
        public string LeftRight { get; set; }

        public override int CompareTo(ModelUnitBase otherbase)
        {
            StimulusTemplate other = otherbase as StimulusTemplate;
            return TargetPool.CompareTo(other.TargetPool);
        }
        public override string ToString()
        {
            return Distinguisher + (Active ? "" : " (inactive)");
        }
        [JsonIgnore]
        public override string Distinguisher
        {
            get
            {
                return string.Format("Target: {0} {1}-{2} {3}; {4}",
                    LeftRight, TargetPool, TargetSomite, TargetCell, StimulusSettings?.ToString());
            }
        }
        [JsonIgnore]
        public override string Tooltip { get { return $"{ToString()}\r\n{StimulusSettings?.ToString()}\r\n{TimeLine_ms}"; } }
    }

}
