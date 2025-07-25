﻿using GeneticSharp;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Services.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services.Optimization
{
    public class CoreFitness : IFitness
    {
        readonly CoreSolver coreUnitSolver;
        string CoreType { get; set; }
        TargetRheobaseFunction TargetRheobaseFunction { get; set; }
        List<FitnessFunction> FitnessFunctions { get; set; }

        DynamicsParam DynamicsParam { get; set; }

        public CoreFitness(CoreSolver coreUnitSolver, CoreSolverSettings settings, DynamicsParam dynamicsParam)
        {
            this.coreUnitSolver = coreUnitSolver;
            CoreType = settings.CoreType;
            TargetRheobaseFunction = settings.TargetRheobaseFunction;
            FitnessFunctions = settings.FitnessFunctions;
            DynamicsParam = dynamicsParam;
        }

        public static double Evaluate(DynamicsParam dynamicsParam, TargetRheobaseFunction targetRheobaseFunction, List<FitnessFunction> fitnessFunctions, CellCore core)
        {
            if (core == null)
                return 0;

            bool includePreStimulus = fitnessFunctions.Any(ff => ff.PreStimulus);
            bool includePostStimulus = fitnessFunctions.Any(ff => ff.PostStimulus);
            double fitness = 0;
            double rheobase = 0;
            int? warmup = includePreStimulus ? GlobalSettings.RheobaseInfinity : null;
            if (targetRheobaseFunction != null)
            {
                fitness += targetRheobaseFunction.CalculateFitness(core, out rheobase);
            }
            else if (fitnessFunctions.Any(ff => ff.CurrentRequired && ff.RheobaseBased))
                rheobase = core.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: GlobalSettings.RheobaseInfinity, dt: 0.1);

            List<double> currentValues = fitnessFunctions
                .Select(ff => ff.CurrentValueOrRheobaseMultiplier * (ff.RheobaseBased ? rheobase : 1))
                .Distinct()
                .ToList();

            //generate a dictionary of DynamicStats - to prevent multiple runs
            Dictionary<double, DynamicsStats> stats = [];
            foreach (double current in currentValues)
            {
                DynamicsStats stat = core.DynamicsTest(dynamicsParam, current, infinity: GlobalSettings.RheobaseInfinity, dt: 0.1, warmup: warmup, includePostStimulus: includePostStimulus);
                stats.Add(current, stat);
            }

            foreach (FitnessFunction function in fitnessFunctions)
            {
                if (function.RheobaseBased && rheobase <= 0)
                    continue;
                double current = function.CurrentValueOrRheobaseMultiplier * (function.RheobaseBased ? rheobase : 1);
                DynamicsStats stat = stats[current];
                fitness += function.CalculateFitness(stat);
            }
            return fitness;
        }
        public double Evaluate(IChromosome chromosome)//FUTURE_IMPROVEMENT infinity, sensitivity etc
        {
            var fc = chromosome as FloatingPointChromosome;
            var values = fc.ToFloatingPoints();
            int iter = 0;
            string valueStr = "";
            Dictionary<string, double> instanceValues = [];
            foreach (string key in coreUnitSolver.Settings.SortedKeys)
            {
                valueStr += $"{key}: {values[iter]}; ";
                instanceValues.Add(key, values[iter++]);
            }

            CellCore core = CellCore.CreateCore(CoreType, instanceValues, coreUnitSolver.Settings.DeltaT);
            return Evaluate(DynamicsParam, TargetRheobaseFunction, FitnessFunctions, core);
        }
    }


}
