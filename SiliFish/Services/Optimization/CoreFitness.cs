﻿using GeneticSharp;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Optimization
{
    public class CoreFitness : IFitness
    {
        readonly CoreSolver IzhikevichSolver;
        string CoreType { get; set; }
        TargetRheobaseFunction TargetRheobaseFunction { get; set; }
        List<FitnessFunction> FitnessFunctions { get; set; }

        public CoreFitness(CoreSolver izhikevichSolver, CoreSolverSettings settings)
        {
            IzhikevichSolver = izhikevichSolver;
            CoreType = settings.CoreType;
            TargetRheobaseFunction = settings.TargetRheobaseFunction;
            FitnessFunctions = settings.FitnessFunctions;
        }

        public static double Evaluate(TargetRheobaseFunction targetRheobaseFunction, List<FitnessFunction> fitnessFunctions, CellCoreUnit core)
        {
            if (core == null)
                return 0;

            double fitness = 0;
            double rheobase = 0;
            if (targetRheobaseFunction != null)
            {
                fitness += targetRheobaseFunction.CalculateFitness(core, out rheobase);
            }
            else if (fitnessFunctions.Any(ff => ff.CurrentRequired && ff.RheobaseBased))
                rheobase = core.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: CurrentSettings.Settings.RheobaseInfinity, dt: 0.1);

            List<double> currentValues = fitnessFunctions
                .Select(ff => ff.CurrentValueOrRheobaseMultiplier * (ff.RheobaseBased ? rheobase : 1))
                .Distinct()
                .ToList();

            //generate a dictionary of DynamicStats - to prevent multiple runs
            Dictionary<double, DynamicsStats> stats = new();
            foreach (double current in currentValues)
            {
                DynamicsStats stat = core.DynamicsTest(current, infinity: CurrentSettings.Settings.RheobaseInfinity, dt: 0.1);
                stats.Add(current, stat);
            }


            foreach (FitnessFunction function in fitnessFunctions)
            {
                double current = function.CurrentValueOrRheobaseMultiplier * (function.RheobaseBased ? rheobase : 1);
                DynamicsStats stat = stats[current];
                fitness += function.CalculateFitness(stat);
            }
            return fitness;
        }
        public double Evaluate(IChromosome chromosome)//TODO infinity, sensitivity etc
        {
            var fc = chromosome as FloatingPointChromosome;
            var values = fc.ToFloatingPoints();
            int iter = 0;
            string valueStr = "";
            Dictionary<string, double> instanceValues = new();
            foreach (string key in IzhikevichSolver.Settings.SortedKeys)
            {
                valueStr += $"{key}: {values[iter]}; ";
                instanceValues.Add(key, values[iter++]);
            }

            CellCoreUnit core = CellCoreUnit.CreateCore(CoreType, instanceValues);
            return Evaluate(TargetRheobaseFunction, FitnessFunctions, core);
        }
    }


}