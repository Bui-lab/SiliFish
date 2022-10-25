﻿using SiliFish.Definitions;
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
                .Where(type => typeof(FitnessFunction).IsAssignableFrom(type) && nameof(FitnessFunction) != type.Name)
                .ToDictionary(type => type.Name, type => type);

        private string fitnessFunctionType;
        public string FitnessFunctionType { get { return fitnessFunctionType; } set { fitnessFunctionType = value; } }
        public double Weight { get; set; }
        public bool MinMaxExists, CurrentRequired, ModeExists;

        public double ValueMin { get; set; }//valid only if MinMaxExists = true
        public double ValueMax { get; set; }//valid only if MinMaxExists = true

        public bool RheobaseBased { get; set; }//valid only if CurrentRequired = true
        public double CurrentValueOrRheobaseMultiplier { get; set; }//valid only if CurrentRequired = true

        public virtual double CalculateFitness(DynamicsStats stat)
        {
            throw new NotImplementedException();
        }

        public FitnessFunction()
        {
            fitnessFunctionType = GetType().Name;
        }

        public virtual string[] GetFiringOptions()
        {
            return null;
        }

        public static FitnessFunction FromJson(string json)
        {
            FitnessFunction ff = JsonSerializer.Deserialize<FitnessFunction>(json);
            return typeMap.TryGetValue(ff.fitnessFunctionType, out var type) ?
                    (FitnessFunction)JsonSerializer.Deserialize(json, type) :
                    null;
        }

        public static Dictionary<string, Type> TypeMap { get => typeMap; }
    }


    public class TargetRheobaseFunction : FitnessFunction
    {
        public double CalculateFitness(DynamicUnit core, out double rheobase)
        {
            rheobase = core.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: Const.RheobaseInfinity, dt: 0.1);
            if (rheobase < 0) return 0;
            if (ValueMin <= rheobase && ValueMax >= rheobase)
                return 10 * Weight; //TODO arbitrary 10 multiplier
            if (rheobase < ValueMin)
                return 10 * Weight / (ValueMin - rheobase);
            return 10 * Weight / (rheobase - ValueMax);
        }

        public TargetRheobaseFunction()
        {
            MinMaxExists = true;
            CurrentRequired = false;
            ModeExists = false;
        }
    }

    public class FiringRhythmFunction : FitnessFunction
    {
        public FiringRhythm TargetRhythm { get; set; }
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
                    return Weight /10;//TODO hardcoded
            }
            return 0;
        }
    }
    
    public class FiringPatternFunction : FitnessFunction
    {
        public FiringPattern TargetPattern { get; set; }

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
            double avg = stat.Intervals_ms?.Values.ToArray().AverageValue()??0;
            double SD = stat.Intervals_ms?.Values.ToArray().StandardDeviation()??0;
            
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

    public class FiringDelayFunction : FitnessFunction
    {
        public FiringDelayFunction()
        {
            MinMaxExists = true;
            CurrentRequired = true;
            ModeExists = false;
        }
        public override double CalculateFitness(DynamicsStats stat)
        {
            double spikeDelay = stat.SpikeDelay;
            if (spikeDelay < 0) return 0;
            if (ValueMin <= spikeDelay && ValueMax >= spikeDelay)
                return Weight;
            if (spikeDelay < ValueMin)
                return Weight / (ValueMin - spikeDelay);
            return Weight / (spikeDelay - ValueMax);
        }
    }
    public class SpikePerBurstFunction : FitnessFunction
    {
        public SpikePerBurstFunction()
        {
            MinMaxExists = true;
            CurrentRequired = true;
            ModeExists = false;
        }
        public override double CalculateFitness(DynamicsStats stat)
        {
            if (stat.BurstsOrSpikes.Count == 0)
                return 0;
            double avgSpikePerBurst = stat.BurstsOrSpikes.Average(bs => bs.SpikeCount);
            if (ValueMin <= avgSpikePerBurst && ValueMax >= avgSpikePerBurst)
                return Weight;
            if (avgSpikePerBurst < ValueMin)
                return Weight / (ValueMin - avgSpikePerBurst);
            return Weight / (avgSpikePerBurst - ValueMax);
        }
        
    }

}
