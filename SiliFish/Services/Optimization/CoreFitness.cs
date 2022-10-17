using GeneticSharp;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Optimization
{

    public class FitnessFunction
    {
        public double Weight { get; set; }
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
    }

    public class FiringFitnessFunction : FitnessFunction
    {
        public bool RheobaseBased { get; set; }
        public double CurrentValueOrRheobaseMultiplier { get; set; }
        public FitnessFunctionOptions FitnessFunctionOptions { get; set; }
        public FiringPattern TargetPattern { get; set; }
        public FiringDelay TargetDelay { get; set; }
        public FiringRhythm TargetRhythm { get; set; }

        public double CalculateFitness(DynamicsStats stat)
        {
            switch (FitnessFunctionOptions)
            {
                case FitnessFunctionOptions.TargetRheobase:
                    break;
                case FitnessFunctionOptions.FiringDelay:
                    break;
                case FitnessFunctionOptions.FiringRhythm:
                    if (stat.FiringRhythm == TargetRhythm)
                        return Weight;
                    else
                        return 0;
                case FitnessFunctionOptions.FiringPattern:
                    if (stat.FiringPattern == TargetPattern)
                        return Weight;
                    else
                        return 0;
            }
            return 0;
        }
    }
    public class CoreFitness : IFitness
    {
        readonly CoreSolver IzhikevichSolver;
        string CoreType { get; set; }
        TargetRheobaseFunction TargetRheobaseFunction { get; set; }
        List<FiringFitnessFunction> FitnessFunctions { get; set; }

        public CoreFitness(CoreSolver izhikevichSolver, CoreSolverSettings settings)
        {
            IzhikevichSolver = izhikevichSolver;
            CoreType = settings.CoreType;
            TargetRheobaseFunction = settings.TargetRheobaseFunction;
            FitnessFunctions = settings.FitnessFunctions;
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

            DynamicUnit core = DynamicUnit.CreateCore(CoreType, instanceValues);
            if (core == null)
                return 0;

            double fitness = 0;
            double rheobase = 0;
            if (TargetRheobaseFunction != null)
            {
                fitness += TargetRheobaseFunction.CalculateFitness(core, out rheobase);
            }
            else if (FitnessFunctions.Any(ff => ff.RheobaseBased))
                rheobase = core.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: Const.RheobaseInfinity, dt: 0.1);

            List<double> currentValues = FitnessFunctions
                .Select(ff => ff.CurrentValueOrRheobaseMultiplier * (ff.RheobaseBased ? rheobase : 1))
                .Distinct()
                .ToList();

            //generate a dictionary of DynamicStats - to prevent multiple runs
            Dictionary<double, DynamicsStats> stats = new();
            foreach (double current in currentValues)
            {
                DynamicsStats stat = core.DynamicsTest(current, infinity: Const.RheobaseInfinity, dt: 0.1);
                stats.Add(current, stat);
            }

            double weight = 1;
            foreach (FiringFitnessFunction function in FitnessFunctions)
            {
                double current = function.CurrentValueOrRheobaseMultiplier * (function.RheobaseBased ? rheobase : 1);
                DynamicsStats stat = stats[current];
                fitness += weight * function.CalculateFitness(stat);
            }
            return fitness;
        }
    }


}
