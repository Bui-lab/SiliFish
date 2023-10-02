using SiliFish.Definitions;
using SiliFish.DynamicUnits.Firing;
using System;
using System.Text.Json.Serialization;

namespace SiliFish.Services.Optimization
{
    public class FiringRhythmFunction : FitnessFunction
    {
        public FiringRhythm TargetRhythm { get; set; }
        [JsonIgnore]
        public override string Details
        {
            get
            {
                return $"{base.Details} Target Rhythm: {TargetRhythm}";
            }
        }
        public FiringRhythmFunction()
        {
            MinMaxExists = false;
            CurrentRequired = true;
            ModeExists = true;
        }

        public override string[] GetFiringOptions()
        {
            return Enum.GetNames(typeof(FiringRhythm));
        }

        public override double CalculateFitness(DynamicsStats stat)
        {
            if (stat.BurstsOrSpikes.Count == 0)
                return TargetRhythm == FiringRhythm.NoSpike ? Weight : 0;
            if (stat.FiringRhythm == TargetRhythm)
                return Weight;
            switch (TargetRhythm)
            {
                case FiringRhythm.NoSpike:
                    return 0;
                case FiringRhythm.Phasic: //but stat is not phasic (tonic)
                    return Weight / stat.BurstsOrSpikes.Count;
                case FiringRhythm.Tonic: //but stat is not tonic (phasic)
                    return Weight * stat.SpikeCoverage(ignoreDelay: true);
                default:
                    break;
            }
            return 0;
        }
    }

}
