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
        private static readonly Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(FitnessFunction).IsAssignableFrom(type) && nameof(FitnessFunction) != type.Name)
                .ToDictionary(type => type.Name, type => type);

        private string fitnessFunctionType;
        public string FitnessFunctionType { get { return fitnessFunctionType; } set { fitnessFunctionType = value; } }
        public double Weight { get; set; }
        public bool MinMaxExists, CurrentRequired, ModeExists;//no need to save in JSON - they are class based parameters

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
        /// <summary>
        /// Fitness is equal to weight if rheobase is within [ValueMin, ValueMax]
        /// Fitness is equal to 0 if rheobase <= ValueMin - range or rheobase >= ValueMax + range
        /// Where range is ValueMax-ValueMin or ValueMin/2 (if ValueMin==ValueMax)
        /// In other places, it is calculated by the tangent
        /// </summary>
        /// <param name="core"></param>
        /// <param name="rheobase"></param>
        /// <returns></returns>
        public double CalculateFitness(DynamicUnit core, out double rheobase)
        {
            rheobase = core.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: Const.RheobaseInfinity, dt: 0.1);
            if (rheobase < 0) return 0;
            if (ValueMin <= rheobase && ValueMax >= rheobase)
                return Weight;
            double range = ValueMax - ValueMin;
            if (range < Const.Epsilon)
                range = ValueMin / 2;
            if (rheobase <= ValueMin - range || rheobase >= ValueMax + range)
                return 0;
            double diff = rheobase < ValueMin ? (rheobase - (ValueMin - range)) : (ValueMax + range - rheobase);
            return Weight * diff / range;
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
                    return Weight * stat.SpikeCoverage(ignoreDelay: true);
                default:
                    break;
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
            double irregularity = stat.Irregularity;
            
            switch (TargetPattern)
            {
                case FiringPattern.NoSpike:
                    return 0;
                case FiringPattern.Spiking://the lower irregularity should give a higher fitness value
                    return Weight * Math.Max(0, (1 - irregularity));
                case FiringPattern.Bursting://the higher irregularity should give a higher fitness value
                    return Weight * Math.Min(1, irregularity);
                case FiringPattern.Chattering://the higher irregularity should give a higher fitness value
                    return Weight * Math.Min(1, irregularity);
                case FiringPattern.Mixed://the higher irregularity should give a higher fitness value
                    return Weight * Math.Min(1, irregularity);
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
