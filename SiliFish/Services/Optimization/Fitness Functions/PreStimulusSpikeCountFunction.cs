using SiliFish.Definitions;
using SiliFish.DynamicUnits.Firing;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.Services.Optimization
{
    public class PreStimulusSpikeCountFunction : FitnessFunction
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

        public PreStimulusSpikeCountFunction()
        {
            MinMaxExists = true;
            CurrentRequired = true;
            ModeExists = false;
            PreStimulus = true;
        }

        public override string[] GetFiringOptions()
        {
            return Enum.GetNames(typeof(FiringPattern));
        }
        public override double CalculateFitness(DynamicsStats stat)
        {
            int spikeCount = stat.PreStimulusSpikeList.Count;
            return CalculateFitnessFor(spikeCount);

        }
    }


}
