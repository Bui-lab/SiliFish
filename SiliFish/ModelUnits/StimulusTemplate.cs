namespace SiliFish.ModelUnits
{
    public class StimulusTemplate : ModelUnitBase
    {
        public string Target { get; set; }

        public Stimulus Stimulus_ms { get; set; }

        public string LeftRight { get; set; }

        public override int CompareTo(ModelUnitBase otherbase)
        {
            StimulusTemplate other = otherbase as StimulusTemplate;
            return Target.CompareTo(other.Target);
        }
        public override string ToString()
        {
            return Distinguisher + (Active ? "" : " (inactive)");
        }
        public override string Distinguisher { get { return string.Format("Target: {0} {1}; {2}", LeftRight, Target, Stimulus_ms?.ToString()); } }
        public override string Tooltip { get { return $"{ToString()}\r\n{Stimulus_ms.GetTooltip()}"; } }
    }

}
