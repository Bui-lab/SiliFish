using GeneticSharp;
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
        public static void Dun()
        {
            float maxWidth = 998f;
            float maxHeight = 680f;
            var chromosome = new FloatingPointChromosome(
                new double[] { 0, 0, 0, 0 },//minimum values for 4 variables
                new double[] { maxWidth, maxHeight, maxWidth, maxHeight },//maximum values for 4 variables
                new int[] { 10, 10, 10, 10 },//total bits used to represent each number?
                new int[] { 0, 0, 0, 0 });//decimal digits
            var population = new Population(50, 100, chromosome); //min 50, max 100
            var fitness = new FuncFitness((c) =>
            {
                var fc = c as FloatingPointChromosome;
                var values = fc.ToFloatingPoints();
                var x1 = values[0];
                var y1 = values[1];
                var x2 = values[2];
                var y2 = values[3];
                return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            });

            var selection = new EliteSelection(); //Elite, Roulete Wheel, Stochastic Universal Sampling and Tournament.
            var crossover = new UniformCrossover(0.5f);//Cut and Splice, Cycle (CX), One-Point (C1), Order-based (OX2), Ordered (OX1), Partially Mapped (PMX), Position-based (POS), Three parent, Two-Point (C2) and Uniform
            var mutation = new FlipBitMutation(); //Flip-bit, Reverse Sequence (RSM), Twors and Uniform.
            var termination = new FitnessStagnationTermination(100);// Generation number, Time evolving, Fitness stagnation, Fitness threshold, And e Or (allows combine others terminations).
            var ga = new GeneticAlgorithm(
                population,
                fitness,
                selection,
                crossover,
                mutation);
            ga.Termination = termination;

            Console.WriteLine("Generation: (x1, y1), (x2, y2) = distance");
            var latestFitness = 0.0;
            ga.GenerationRan += (sender, e) =>
            {
                var bestChromosome = ga.BestChromosome as FloatingPointChromosome;
                var bestFitness = bestChromosome.Fitness.Value;
                if (bestFitness != latestFitness)
                {
                    latestFitness = bestFitness;
                    var phenotype = bestChromosome.ToFloatingPoints();
                    Console.WriteLine(
                        "Generation {0,2}: ({1},{2}),({3},{4}) = {5}",
                        ga.GenerationsNumber,
                        phenotype[0],
                        phenotype[1],
                        phenotype[2],
                        phenotype[3],
                        bestFitness
                    );
                }
            };
            ga.Start();
            Console.ReadKey();
        }
    }
}
