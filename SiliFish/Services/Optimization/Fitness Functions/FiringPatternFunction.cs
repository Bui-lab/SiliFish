using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using System;
using System.Text.Json.Serialization;

namespace SiliFish.Services.Optimization
{
    public class FiringPatternFunction : FitnessFunction
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

        public FiringPatternFunction()
        {
            MinMaxExists = false;
            CurrentRequired = true;
            ModeExists = true;
        }

        public override string[] GetFiringOptions()
        {
            return Enum.GetNames(typeof(FiringPattern));
        }
        public override double CalculateFitness(DynamicsStats stat)
        {
            if (stat.FiringPattern == TargetPattern)
                return Weight;
            if (stat.FiringPattern == FiringPattern.NoSpike)
                return 0;
            double irregularity = stat.Irregularity;
            if (stat.FiringPattern == FiringPattern.Bursting && TargetPattern == FiringPattern.Spiking)
                return 0;

            return TargetPattern switch
            {
                FiringPattern.NoSpike => 0,
                //the lower irregularity should give a higher fitness value
                FiringPattern.Spiking => Weight * Math.Max(0, (1 - irregularity)),
                //the higher irregularity should give a higher fitness value
                FiringPattern.Bursting => Weight * Math.Min(1, irregularity),
                //the higher irregularity should give a higher fitness value
                FiringPattern.Chattering => Weight * Math.Min(1, irregularity),
                //the higher irregularity should give a higher fitness value
                FiringPattern.Mixed => Weight * Math.Min(1, irregularity),
                _ => 0,
            };
        }
    }


}
