using GeneticSharp;
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

    public class GeneticAlgorithmTest
    {
        public static string Dun(Dictionary<string, double> paramValues, Dictionary<string, double> minValues, Dictionary<string, double> maxValues, double targetRheobase)
        {
            int[] numBits = new int[paramValues.Count]; 
            int[] decimalDigits = new int[paramValues.Count];
            double[] minValArray = new double[paramValues.Count];
            double[] maxValArray = new double[paramValues.Count];
            List<string> keys = paramValues.Keys.OrderBy(k => k).ToList();
            int iter = 0;
            foreach(string key in keys)
            {
                numBits[iter] = 64;
                int numOfDecimalDigit = Util.NumOfDecimalDigits(paramValues[key]);
                if (minValues.ContainsKey(key))
                {
                    numOfDecimalDigit = Math.Max(numOfDecimalDigit, Util.NumOfDecimalDigits(minValues[key]));
                    minValArray[iter] = minValues[key];
                }
                else
                {
                    int numDigit = Util.NumOfDigits(paramValues[key]);
                    if (numDigit == 0)
                        numDigit = 1;
                    minValArray[iter] = paramValues[key] - 10 * numDigit;
                }
                if (maxValues.ContainsKey(key))
                {
                    numOfDecimalDigit = Math.Max(numOfDecimalDigit, Util.NumOfDecimalDigits(maxValues[key]));
                    maxValArray[iter] = maxValues[key];
                }
                else
                {
                    int numDigit = Util.NumOfDigits(paramValues[key]);
                    if (numDigit == 0)
                        numDigit = 1;
                    maxValArray[iter] = paramValues[key] + 10 * numDigit;
                }
                decimalDigits[iter++] = numOfDecimalDigit;
            }

            List<string> list = new List<string>();

            var chromosome = new FloatingPointChromosome(
                minValArray,
                maxValArray,
                numBits,
                decimalDigits);
            var population = new Population(50, 100, chromosome); //min 50, max 100
            var fitness = new FuncFitness((c) =>
            {
                var fc = c as FloatingPointChromosome;
                var values = fc.ToFloatingPoints();
                int iter = 0;
                string valueStr = "";
                foreach (string key in keys)
                {
                    valueStr += $"{key}: {values[iter]}; ";
                    paramValues[key] = values[iter++];
                }
                Neuron neuron = new(paramValues);
                double d = neuron.CalculateRheoBase(dt:0.1,maxRheobase:1000, sensitivity:Math.Pow(0.1, 3), infinity:400);
                list.Add($"{valueStr} - rheobase:{d}\r\n");
                if (d < 0)//no rheobase
                    return 0;
                if (targetRheobase == d)
                    return double.MaxValue;
                return 1/Math.Abs(targetRheobase - d);
            });

            var selection = new EliteSelection(); //Elite, Roulete Wheel, Stochastic Universal Sampling and Tournament.
            var crossover = new UniformCrossover(0.5f);//Cut and Splice, Cycle (CX), One-Point (C1), Order-based (OX2), Ordered (OX1), Partially Mapped (PMX), Position-based (POS), Three parent, Two-Point (C2) and Uniform
            var mutation = new FlipBitMutation(); //Flip-bit, Reverse Sequence (RSM), Twors and Uniform.
            var termination = new FitnessThresholdTermination(0.2);// Generation number, Time evolving, Fitness stagnation, Fitness threshold, And e Or (allows combine others terminations).
            var ga = new GeneticAlgorithm(
                population,
                fitness,
                selection,
                crossover,
                mutation);
            ga.Termination = termination;

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
                    foreach (string key in keys)
                        valueStr += $"{key}: {phenotype[iter++]}; ";
                    list.Add($"Generation {ga.GenerationsNumber}: {valueStr} Fitness = {bestFitness}\r\n");
                }
            };
            ga.Start();
            return string.Join("\r\n", list);
        }
    }
}
