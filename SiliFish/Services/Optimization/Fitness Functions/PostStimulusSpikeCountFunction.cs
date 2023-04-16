using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.Services.Optimization
{
    public class PostStimulusSpikeCountFunction : FitnessFunction
    {
        public FiringPattern TargetPattern { get; set; }

        [JsonIgnore]
        public override string Details
        {
            get
            {
                return $"{base.Details} Target Pattern: {TargetPattern}";
            }
        }

        public PostStimulusSpikeCountFunction()
        {
            MinMaxExists = true;
            CurrentRequired = true;
            ModeExists = false;
        }

        public override string[] GetFiringOptions()
        {
            return Enum.GetNames(typeof(FiringPattern));
        }
        public override double CalculateFitness(DynamicsStats stat)
        {
            int spikeCount = stat.PostStimulusSpikeList.Count;
            if (ValueMin <= spikeCount && ValueMax >= spikeCount)
                return Weight;
            if (spikeCount < ValueMin)
                return Weight / (ValueMin - spikeCount);
            return Weight / (spikeCount - ValueMax);
        }
    }


}
