using GeneticSharp;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using System;
using System.Collections.Generic;
using System.Linq;
//https://diegogiacomelli.com.br/function-optimization-with-geneticsharp/
namespace SiliFish.Services.Optimization
{
    public class CoreFitness : IFitness
    {
        readonly CoreSolver IzhikevichSolver;
        CoreType CoreType;
        internal double TargetRheobaseMin, TargetRheobaseMax;
        internal Dictionary<double, FiringPattern> FiringPatterns;
        public CoreFitness(CoreSolver izhikevichSolver, CoreType coreType, double targetRheobaseMin, double targetRheobaseMax, Dictionary<double, FiringPattern> firingPatterns)
        {
            IzhikevichSolver = izhikevichSolver;
            CoreType = coreType;
            TargetRheobaseMin = targetRheobaseMin;
            TargetRheobaseMax = targetRheobaseMax;
            if (TargetRheobaseMax<TargetRheobaseMin)
                (TargetRheobaseMin, TargetRheobaseMax) = (TargetRheobaseMax, TargetRheobaseMin);
            FiringPatterns = firingPatterns;
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
            double d = core.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity: 400, dt: 0.1);
            //IzhikevichSolver.OutputText.Add($"{valueStr} - rheobase:{d}\r\n");
            if (d < 0)//no rheobase
                return 0;
            if (FiringPatterns.Any())
            {
                foreach (double multiplier in FiringPatterns.Keys)
                {
                    DynamicsStats stat = core.DynamicsTest(d * multiplier, infinity: 400, dt: 0.1);
                    if (stat.FiringPattern != FiringPatterns[multiplier])
                        return 0;
                }
            }
            if (TargetRheobaseMin <= d && TargetRheobaseMax >= d)
                return double.MaxValue;
            double distance = d > TargetRheobaseMax ? d - TargetRheobaseMax : TargetRheobaseMin - d;
            /*DynamicsStats stat2 = core.DynamicsTest(d * 1, infinity: 400, dt: 0.1);
            if (stat2.FiringPattern != FiringPattern.Phasic)
                return 0;
            if (stat2.SpikeList.Count > 1)
                return 0;*/
            return 1 / distance;
        }
    }
    public class CoreSolver
    {
        Dictionary<string, double> ParamValues;
        
        internal List<string> OutputText = new();
        internal List<string> SortedKeys { get { return ParamValues.Keys.OrderBy(k => k).ToList(); } }
        private readonly SelectionBase Selection;
        private readonly CrossoverBase Crossover;
        private readonly MutationBase Mutation;
        private readonly TerminationBase Termination;
        private GeneticAlgorithm Algorithm;
        private double latestFitness = 0.0;

        public string GetOutput() => string.Join("\r\n", OutputText);
        public string GetProgress() => $"Generation: {Algorithm.GenerationsNumber}; Target rheobase ± {(latestFitness != 0 ? (1 / latestFitness) : "N/A")}";

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
            CoreType coreType,
            Dictionary<string, double> paramValues,
            double minTargetRheobase, double maxTargetRheobase,
            Dictionary<double, FiringPattern> firingPatterns = null,
            Dictionary<string, double> minValues = null,
            Dictionary<string, double> maxValues = null)
        {
            IFitness fitness = new CoreFitness(this, coreType, minTargetRheobase, maxTargetRheobase, firingPatterns);

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
                    OutputText.Add($"Generation {Algorithm.GenerationsNumber}: {valueStr} Fitness = {bestFitness}");
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
