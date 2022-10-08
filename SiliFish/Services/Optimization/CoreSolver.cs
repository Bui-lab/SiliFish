using GeneticSharp;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
//https://diegogiacomelli.com.br/function-optimization-with-geneticsharp/
namespace SiliFish.Services.Optimization
{

    public class FitnessFunction
    {
        public double Weight;
    }
    public class TargetRheobaseFunction: FitnessFunction
    {
        public double TargetRheobaseMin = double.NaN, TargetRheobaseMax = double.NaN;
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
        public bool RheobaseBased;
        public double CurrentValueOrRheobaseMultiplier;
        public FitnessFunctionOptions FitnessFunctionOptions;
        public FiringPattern TargetPattern;
        public FiringDelay TargetDelay;
        public FiringRhythm TargetRhythm;

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
        string CoreType;
        TargetRheobaseFunction TargetRheobaseFunction;
        List<FiringFitnessFunction> FitnessFunctions;

        public CoreFitness(CoreSolver izhikevichSolver,
            string coreType,
            TargetRheobaseFunction targetRheobaseFunction,
            List<FiringFitnessFunction> fitnessFunctions)
        {
            IzhikevichSolver = izhikevichSolver;
            CoreType = coreType;
            TargetRheobaseFunction = targetRheobaseFunction;
            FitnessFunctions = fitnessFunctions;
        }
        public double Evaluate(IChromosome chromosome)//TODO infinity, sensitivity etc
        {
            var fc = chromosome as FloatingPointChromosome;
            var values = fc.ToFloatingPoints();
            int iter = 0;
            string valueStr = "";
            Dictionary<string, double> instanceValues = new();
            foreach (string key in IzhikevichSolver.SortedKeys)
            {
                valueStr += $"{key}: {values[iter]}; ";
                instanceValues.Add(key, values[iter++]);
            }

            DynamicUnit core = DynamicUnit.CreateCore(CoreType, instanceValues);
            if (core == null)
                return 0;

            double fitnessMultiplier = 1;
            double rheobase = 0;
            if (TargetRheobaseFunction != null)
            {
                fitnessMultiplier *= TargetRheobaseFunction.CalculateFitness(core, out rheobase);
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
                weight += function.CalculateFitness(stat);
            }
            return fitnessMultiplier * weight;
        }
    }
    public class CoreSolver
    {
        Dictionary<string, double> ParamValues;

        internal List<string> SortedKeys { get { return ParamValues.Keys.OrderBy(k => k).ToList(); } }
        private readonly SelectionBase Selection;
        private readonly CrossoverBase Crossover;
        private readonly MutationBase Mutation;
        private readonly TerminationBase Termination;
        private GeneticAlgorithm Algorithm;
        private double latestFitness = 0.0;

        public string ProgressText
        {
            get
            {
                return $"Generation: {Algorithm.GenerationsNumber}; Fitness: {latestFitness}";
            }
        }
        public int Progress
        {
            get
             {
                if (Algorithm.Termination is GenerationNumberTermination gnt)
                    return (int)(100 * Algorithm.GenerationsNumber / gnt.ExpectedGenerationNumber);

                return 0;
            }
        }

        public CoreSolver(Type selectionType,
            Type crossOverType,
            Type mutationType,
            Type terminationType)

        {
            Selection = (SelectionBase)Activator.CreateInstance(selectionType); //Elite, Roulete Wheel, Stochastic Universal Sampling and Tournament.
            Crossover = (CrossoverBase)Activator.CreateInstance(crossOverType); //new UniformCrossover(0.5f);//Cut and Splice, Cycle (CX), One-Point (C1), Order-based (OX2), Ordered (OX1), Partially Mapped (PMX), Position-based (POS), Three parent, Two-Point (C2) and Uniform
            Mutation = (MutationBase)Activator.CreateInstance(mutationType); //Flip-bit, Reverse Sequence (RSM), Twors and Uniform.
            Termination = (TerminationBase)Activator.CreateInstance(terminationType); //new FitnessThresholdTermination(0.05);// Generation number, Time evolving, Fitness stagnation, Fitness threshold, And e Or (allows combine others terminations).
        }

        public void SetOptimizationSettings(int minPopulationSize,
            int maxPopulationSize,
            string coreType,
            Dictionary<string, double> paramValues,
            TargetRheobaseFunction targetRheobaseFunction,
            List<FiringFitnessFunction> fitnessFunctions,
            Dictionary<string, double> minValues = null,
            Dictionary<string, double> maxValues = null)
        {
            IFitness fitness = new CoreFitness(this, coreType, targetRheobaseFunction, fitnessFunctions);

            ParamValues = paramValues;
            int nCount = paramValues.Count;
            double[] MinValues = new double[nCount];
            double[] MaxValues = new double[nCount];
            int[] NumBits = new int[nCount];
            int[] DecimalDigits = new int[nCount];
            int iter = 0;
            foreach (string key in SortedKeys)
            {
                NumBits[iter] = 64;
                int numOfDecimalDigit = Util.NumOfDecimalDigits(paramValues[key]);
                MinValues[iter] = minValues?.GetValueOrDefault(key, Const.GeneticAlgorithmMinValue) ?? Const.GeneticAlgorithmMinValue;
                MaxValues[iter] = maxValues?.GetValueOrDefault(key, Const.GeneticAlgorithmMaxValue) ?? Const.GeneticAlgorithmMaxValue;
                DecimalDigits[iter++] = numOfDecimalDigit;
            }

            ChromosomeBase chromosome = new FloatingPointChromosome(
                MinValues,
                MaxValues,
                NumBits,
                DecimalDigits);
            Population population = new(minPopulationSize, maxPopulationSize, chromosome); //min 50, max 100
            Algorithm = new(
                population,
                fitness,
                Selection,
                Crossover,
                Mutation)
            {
                Termination = Termination
            };
        }
        public Dictionary<string, double> Optimize()
        {
            latestFitness = 0.0;
            Algorithm.GenerationRan += (sender, e) =>
            {
                var bestChromosome = Algorithm.BestChromosome as FloatingPointChromosome;
                var bestFitness = bestChromosome.Fitness.Value;
                if (bestFitness != latestFitness)
                {
                    latestFitness = bestFitness;
                    var phenotype = bestChromosome.ToFloatingPoints();
                    int iter = 0;
                    string valueStr = "";
                    foreach (string key in SortedKeys)
                        valueStr += $"{key}: {phenotype[iter++]}; ";
                }
            };
            Algorithm.Start();

            Dictionary<string, double> BestValues = new();
            int iter = 0;
            foreach (string key in SortedKeys)
            {
                var phenotype = (Algorithm.BestChromosome as FloatingPointChromosome).ToFloatingPoints();
                BestValues.Add(key, phenotype[iter++]);
            }
            return BestValues;
        }
        public void CancelOptimization()
        {
            Algorithm?.Stop();
        }
    }
}
