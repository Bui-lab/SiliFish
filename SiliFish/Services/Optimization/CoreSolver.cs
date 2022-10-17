using GeneticSharp;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
//https://diegogiacomelli.com.br/function-optimization-with-geneticsharp/
namespace SiliFish.Services.Optimization
{

    public class CoreSolver
    {
        static string AssemblySuffix = ", GeneticSharp.Domain";

        public CoreSolverSettings Settings { get; set; }

        private SelectionBase Selection;
        private CrossoverBase Crossover;
        private MutationBase Mutation;
        private ReinsertionBase Reinsertion;
        private TerminationBase Termination;
        private GeneticAlgorithm Algorithm;

        private double latestFitness = 0.0;

        [JsonIgnore]
        public string ProgressText
        {
            get
            {
                if (Algorithm == null) return "";
                if (Algorithm.Termination is GenerationNumberTermination gnt)
                    return $"Generation: {Algorithm.GenerationsNumber}; Fitness: {latestFitness}";
                if (Algorithm.Termination is TimeEvolvingTermination tet)
                    return $"Generation: {Algorithm.GenerationsNumber}; Fitness: {latestFitness}; Time elapsed: {Algorithm.TimeEvolving.TotalMinutes:0.##} mins;";
                if (Algorithm.Termination is FitnessStagnationTermination fst)
                    return $"Generation: {Algorithm.GenerationsNumber}; Fitness: {latestFitness}; Stagnant for ??? out of {fst.ExpectedStagnantGenerationsNumber:0.##};";
                if (Algorithm.Termination is FitnessThresholdTermination ftt)
                    return $"Generation: {Algorithm.GenerationsNumber}; Fitness: {latestFitness}; Target Fitness: {ftt.ExpectedFitness:0.##}";
                return "Progress unknown";
            }
        }

        [JsonIgnore]
        public int Progress
        {
            get
             {
                if (Algorithm == null) return 0;
                if (Algorithm.Termination is GenerationNumberTermination gnt)
                    return (int)(100 * Algorithm.GenerationsNumber / gnt.ExpectedGenerationNumber);
                if (Algorithm.Termination is TimeEvolvingTermination tet)
                    return (int)(100*Algorithm.TimeEvolving/tet.MaxTime);
                if (Algorithm.Termination is FitnessStagnationTermination fst)
                    return 50;// fst.GetNumberOfStagnantGenerations() / fst.ExpectedStagnantGenerationsNumber;
                if (Algorithm.Termination is FitnessThresholdTermination ftt)
                    return (int)(100 * (Algorithm.BestChromosome?.Fitness.Value ?? 0) / ftt.ExpectedFitness);
                return 0;
            }
        }

        private void CreateTerminator(string terminationType, string terminationParam)
        {
            if (string.IsNullOrEmpty(terminationParam))
            {
                Termination = (TerminationBase)Activator.CreateInstance(Type.GetType(terminationType + AssemblySuffix));
                return;
            }

            if (!int.TryParse(terminationParam, out int iParam))
                iParam = 0;
            if (!double.TryParse(terminationParam, out double dParam))
                dParam = 0;

            Type termType = Type.GetType(terminationType + AssemblySuffix);
            if ((terminationType == typeof(GenerationNumberTermination).FullName
                || terminationType == typeof(FitnessStagnationTermination).FullName
                || terminationType == nameof(GenerationNumberTermination)
                || terminationType == nameof(FitnessStagnationTermination))
                && iParam > 0)
                Termination = (TerminationBase)Activator.CreateInstance(termType, iParam);
            else if ((terminationType == typeof(FitnessThresholdTermination).FullName
                || terminationType == nameof(FitnessThresholdTermination))
                && dParam > 0)
                Termination = (TerminationBase)Activator.CreateInstance(termType, dParam);
            else if (terminationType == typeof(TimeEvolvingTermination).FullName
                || terminationType == nameof(TimeEvolvingTermination))
                Termination = (TerminationBase)Activator.CreateInstance(termType, new TimeSpan(0, iParam, 0));
            else
                Termination = (TerminationBase)Activator.CreateInstance(termType);
        }

        //https://github.com/giacomelli/GeneticSharp/wiki/terminations
        //And e Or (allows combine others terminations).

        public CoreSolver()
        { }
        

        private void InitializeOptimization()
        {
            Selection = (SelectionBase)Activator.CreateInstance(Type.GetType(Settings.SelectionType+AssemblySuffix));
            Crossover = (CrossoverBase)Activator.CreateInstance(Type.GetType(Settings.CrossOverType + AssemblySuffix));
            Mutation = (MutationBase)Activator.CreateInstance(Type.GetType(Settings.MutationType + AssemblySuffix));
            Reinsertion = (ReinsertionBase)Activator.CreateInstance(Type.GetType(Settings.ReinsertionType + AssemblySuffix));
            CreateTerminator(Settings.TerminationType, Settings.TerminationParam);

            IFitness Fitness = new CoreFitness(this, Settings);
            ChromosomeBase chromosome = new FloatingPointChromosome(Settings.MinValues, Settings.MaxValues, Settings.NumBits, Settings.DecimalDigits);
            Population initialPopulation = new(Settings.MinPopulationSize, Settings.MaxPopulationSize, chromosome);
            Algorithm = new(initialPopulation, Fitness, Selection, Crossover, Mutation)
            {
                Termination = Termination,
                Reinsertion = Reinsertion
            };
        }
        public (Dictionary<string, double>, double) Optimize()
        {
            InitializeOptimization();
            latestFitness = 0.0;
            Algorithm.GenerationRan += (sender, e) =>
            {
                var bestChromosome = Algorithm.BestChromosome as FloatingPointChromosome;
                var bestFitness = bestChromosome.Fitness.Value;
                if (bestFitness != latestFitness)
                {
                    latestFitness = bestFitness;
              /*      var phenotype = bestChromosome.ToFloatingPoints();
                    int iter = 0;
                    string valueStr = "";
                    foreach (string key in Settings.SortedKeys)
                        valueStr += $"{key}: {phenotype[iter++]}; ";*/
                }
            };
            Algorithm.Start();

            Dictionary<string, double> BestValues = new();
            int iter = 0;
            foreach (string key in Settings.SortedKeys)
            {
                var phenotype = (Algorithm.BestChromosome as FloatingPointChromosome).ToFloatingPoints();
                BestValues.Add(key, phenotype[iter++]);
            }
            return (BestValues, latestFitness);
        }
        public void CancelOptimization()
        {
            Algorithm?.Stop();
        }
    }
}
