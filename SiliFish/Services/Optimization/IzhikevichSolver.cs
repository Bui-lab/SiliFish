using GeneticSharp;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://diegogiacomelli.com.br/function-optimization-with-geneticsharp/
namespace SiliFish.Services.Optimization
{
    public class IzhikevichFitness : IFitness
    {
        IzhikevichSolver IzhikevichSolver;
        //private readonly Func<IChromosome, double> m_func;
        public IzhikevichFitness(IzhikevichSolver izhikevichSolver)
        {
            IzhikevichSolver = izhikevichSolver;
//            m_func = func;
        }
        public double Evaluate(IChromosome chromosome)
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
            Neuron neuron = new(instanceValues);
            double d = neuron.CalculateRheoBase(dt: 0.1, maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity: 400);
            //IzhikevichSolver.OutputText.Add($"{valueStr} - rheobase:{d}\r\n");
            if (d < 0)//no rheobase
                return 0;
            if (IzhikevichSolver.TargetRheobase == d)
                return double.MaxValue;
            return 1 / Math.Abs(IzhikevichSolver.TargetRheobase - d);
        }
    }
    public class IzhikevichSolver
    {
        Dictionary<string, double> ParamValues;
        double[] MinValues, MaxValues;
        int[] NumBits, DecimalDigits;
        internal double TargetRheobase;
        internal List<string> OutputText = new();
        internal List<string> SortedKeys { get { return ParamValues.Keys.OrderBy(k => k).ToList(); } }

        public IzhikevichSolver(Dictionary<string, double> paramValues,
            double targetRheobase,
            Dictionary<string, double> minValues = null,
            Dictionary<string, double> maxValues = null)
        {
            ParamValues = paramValues;
            TargetRheobase = targetRheobase;
            int nCount = paramValues.Count;
            MinValues = new double[nCount];
            MaxValues = new double[nCount];
            NumBits = new int[nCount];
            DecimalDigits = new int[nCount];
            int iter = 0;
            foreach (string key in SortedKeys)
            {
                NumBits[iter] = 64;
                int numOfDecimalDigit = Util.NumOfDecimalDigits(paramValues[key]);
                MinValues[iter] = minValues?.GetValueOrDefault(key, Const.GeneticAlgorithmMinValue) ?? Const.GeneticAlgorithmMinValue;
                MaxValues[iter] = maxValues?.GetValueOrDefault(key, Const.GeneticAlgorithmMaxValue) ?? Const.GeneticAlgorithmMaxValue;
                DecimalDigits[iter++] = numOfDecimalDigit;
            }
        }
        public (Dictionary<string, double> BestValues, string) Optimize()
        {
            ChromosomeBase chromosome = new FloatingPointChromosome(
                MinValues,
                MaxValues,
                NumBits,
                DecimalDigits);
            Population population = new Population(50, 100, chromosome); //min 50, max 100
            var fitness = new IzhikevichFitness(this);
            SelectionBase selection = new StochasticUniversalSamplingSelection(); //Elite, Roulete Wheel, Stochastic Universal Sampling and Tournament.
            CrossoverBase crossover = new UniformCrossover(0.5f);//Cut and Splice, Cycle (CX), One-Point (C1), Order-based (OX2), Ordered (OX1), Partially Mapped (PMX), Position-based (POS), Three parent, Two-Point (C2) and Uniform
            MutationBase mutation = new FlipBitMutation(); //Flip-bit, Reverse Sequence (RSM), Twors and Uniform.
            TerminationBase termination = new FitnessThresholdTermination(0.05);// Generation number, Time evolving, Fitness stagnation, Fitness threshold, And e Or (allows combine others terminations).
            GeneticAlgorithm ga = new(
                population,
                fitness,
                selection,
                crossover,
                mutation)
            {
                Termination = termination
            };

            var latestFitness = 0.0;
            ga.GenerationRan += (sender, e) =>
            {
                var bestChromosome = ga.BestChromosome as FloatingPointChromosome;
                var bestFitness = bestChromosome.Fitness.Value;
                if (bestFitness != latestFitness)
                {
                    latestFitness = bestFitness;
                    var phenotype = bestChromosome.ToFloatingPoints();
                    int iter = 0;
                    string valueStr = "";
                    foreach (string key in SortedKeys)
                        valueStr += $"{key}: {phenotype[iter++]}; ";
                    OutputText.Add($"Generation {ga.GenerationsNumber}: {valueStr} Fitness = {bestFitness}\r\n");
                }
            };
            ga.Start();
            Dictionary<string, double> BestValues = new();
            int iter = 0;
            foreach (string key in SortedKeys)
            {
                var phenotype = (ga.BestChromosome as FloatingPointChromosome).ToFloatingPoints();
                BestValues.Add(key, phenotype[iter++]);
            }

            return (BestValues, string.Join("\r\n", OutputText));
        }
    }
}
