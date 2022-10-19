using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace SiliFish.Services.Optimization
{
    public class FitnessFunction
    {
        private static Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(FitnessFunction).IsAssignableFrom(type))
                .ToDictionary(type => type.Name, type => type);

        private string fitnessFunctionType;
        public string FitnessFunctionType { get { return fitnessFunctionType; } set { fitnessFunctionType = value; } }
        public FitnessFunctionOptions FitnessFunctionOption { get; set; }
        public double Weight { get; set; }

        public virtual double CalculateFitness(DynamicsStats stat)
        {
            throw new NotImplementedException();
        }

        public FitnessFunction()
        {
            fitnessFunctionType = GetType().Name;
        }

        public static FitnessFunction FromJson(string json)
        {
            FitnessFunction ff = JsonSerializer.Deserialize<FitnessFunction>(json);
            return typeMap.TryGetValue(ff.fitnessFunctionType, out var type) ?
                    (FitnessFunction)JsonSerializer.Deserialize(json, type) :
                    null;
        }
    }


    public class TargetRheobaseFunction : FitnessFunction
    {
        public double TargetRheobaseMin { get; set; } = double.NaN;
        public double TargetRheobaseMax { get; set; } = double.NaN;
        public double CalculateFitness(DynamicUnit core, out double rheobase)
        {
            rheobase = core.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: Const.RheobaseInfinity, dt: 0.1);
            if (rheobase < 0) return 0;
            if (TargetRheobaseMin <= rheobase && TargetRheobaseMax >= rheobase)
                return 10 * Weight; //TODO arbitrary 100 multiplier
            if (rheobase < TargetRheobaseMin)
                return 10 * Weight / (TargetRheobaseMin - rheobase);
            return 10 * Weight / (rheobase - TargetRheobaseMax);
        }

        public TargetRheobaseFunction()
        {
            FitnessFunctionOption = FitnessFunctionOptions.TargetRheobase;
        }
    }

    /*TODO needs current value
    public class FiringDelayFunction : FitnessFunction
    {
        public double FiringDelayMin { get; set; } = double.NaN;
        public double FiringDelayMax { get; set; } = double.NaN;
        public override double CalculateFitness(DynamicsStats stat)
        {
            double spikeDelay = stat.SpikeDelay;
            if (spikeDelay < 0) return 0;
            if (FiringDelayMin <= spikeDelay && FiringDelayMax >= spikeDelay)
                return Weight;
            if (spikeDelay < FiringDelayMin)
                return Weight / (FiringDelayMin - spikeDelay);
            return Weight / (spikeDelay - FiringDelayMax);
        }
    }*/

    public class FiringFitnessFunction : FitnessFunction
    {
        public bool RheobaseBased { get; set; }
        public double CurrentValueOrRheobaseMultiplier { get; set; }

        public static FiringFitnessFunction CreateFiringFitnessFunction(object obj)
        {
            if (obj is FiringFitnessFunction fff)
                return fff;
            if (obj is JsonElement element)
                return GetOfDerivedType(element.GetRawText());
            return null;
        }
        public static FiringFitnessFunction GetOfDerivedType(string json)
        {
            FiringFitnessFunction fff = JsonSerializer.Deserialize<FiringFitnessFunction>(json);
            if (fff != null)
            {
                if (fff.FitnessFunctionType == typeof(FiringRhythmFunction).ToString())
                    return JsonSerializer.Deserialize<FiringRhythmFunction>(json);
                if (fff.FitnessFunctionType == typeof(FiringPatternFunction).ToString())
                    return JsonSerializer.Deserialize<FiringPatternFunction>(json);
            }
            return fff;
        }
    }

    public class FiringRhythmFunction : FiringFitnessFunction
    {
        public FiringRhythm TargetRhythm { get; set; }
        public FiringRhythmFunction()
        {
            FitnessFunctionOption = FitnessFunctionOptions.FiringRhythm;
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
                    return Weight /10;//TODO hardcoded
            }
            return 0;
        }
    }
    
    public class FiringPatternFunction : FiringFitnessFunction
    {
        public FiringPattern TargetPattern { get; set; }

        public FiringPatternFunction()
        {
            FitnessFunctionOption = FitnessFunctionOptions.FiringPattern;
        }

        public override double CalculateFitness(DynamicsStats stat)
        {
            if (stat.FiringPattern == TargetPattern)
                return Weight;
            if (stat.FiringPattern == FiringPattern.NoSpike)
                return 0;
            double avg = stat.Intervals_ms.Values.ToArray().AverageValue();
            double SD = stat.Intervals_ms.Values.ToArray().StandardDeviation();
            
            switch (TargetPattern)//TODO how to quantify irregularity?
            {
                case FiringPattern.NoSpike:
                    return 0;
                case FiringPattern.Spiking://the lower SD should give a higher fitness value
                    return Weight / Math.Min(1, SD);
                case FiringPattern.Bursting://the higher SD should give a higher fitness value
                    return Weight * Math.Min(1, SD);
                case FiringPattern.Chattering:
                    break;
                case FiringPattern.Mixed:
                    break;
            }

            return 0;
        }
    }
}
