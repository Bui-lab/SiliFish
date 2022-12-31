using GeneticSharp;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
//https://diegogiacomelli.com.br/function-optimization-with-geneticsharp/
namespace SiliFish.Services.Optimization
{
    public class CoreSolverOutput
    {
        public Dictionary<string, double> BestValues;
        public double BestFitness;
        public string ErrorMessage;
    }
    public class CoreSolver
    {
        static string AssemblySuffix = ", GeneticSharp.Domain";

        public CoreSolverSettings Settings { get; set; }

        private SelectionBase Selection;
        private CrossoverBase Crossover;
        private MutationBase Mutation;
        private ReinsertionBase Reinsertion;
        private ITermination Termination;
        private GeneticAlgorithm Algorithm;

        private double latestFitness = 0.0;
        private double bestFitness = 0.0;
        private string errorMessage;

        [JsonIgnore]
        public string ProgressText 
        {
            get
            {
                if (!string.IsNullOrEmpty(errorMessage))
                    return errorMessage;
                if (Algorithm == null) return "";
                string msg = $"Generation: {Algorithm.GenerationsNumber}; Fitness: {latestFitness};" +
                        $"\r\nBest fitness: {bestFitness}";
                if (Algorithm.Termination is TimeEvolvingTermination tet)
                    return $"{msg}\r\nTime elapsed: {Algorithm.TimeEvolving.TotalMinutes:0.##} mins;";
                if (Algorithm.Termination is FitnessStagnationTermination fst)
                    return $"{msg}\r\nStagnant for ??? out of {fst.ExpectedStagnantGenerationsNumber:0.##};";
                if (Algorithm.Termination is FitnessThresholdTermination ftt)
                    return $"{msg}\r\nTarget Fitness: {ftt.ExpectedFitness:0.##}";

                return msg;
            }
        }

        [JsonIgnore]
        public int Progress
        {
            get
             {
                if (Algorithm == null) return 0;
                if (Algorithm.Termination is OrTermination)
                {
                    if (Settings.MaxGeneration != null)
                        return (int)(100 * Algorithm.GenerationsNumber / (int)Settings.MaxGeneration);
                    if (Settings.TargetFitness != null)
                        return (int)(100 * (Algorithm.BestChromosome?.Fitness.Value ?? 0) / (double)Settings.TargetFitness);
                }
                if (Algorithm.Termination is GenerationNumberTermination gnt)
                    return (int)(100 * Algorithm.GenerationsNumber / gnt.ExpectedGenerationNumber);
                if (Algorithm.Termination is TimeEvolvingTermination tet)
                    return (int)(100*Algorithm.TimeEvolving/tet.MaxTime);
                if (Algorithm.Termination is FitnessStagnationTermination fst)
                    return -1;// fst.GetNumberOfStagnantGenerations() / fst.ExpectedStagnantGenerationsNumber;
                if (Algorithm.Termination is FitnessThresholdTermination ftt)
                    return (int)(100 * (Algorithm.BestChromosome?.Fitness.Value ?? 0) / ftt.ExpectedFitness);
                return 0;
            }
        }

        private void CreateTerminator(CoreSolverSettings settings)
        {
            List<TerminationBase> terminationList = new();
            if (settings.MaxGeneration != null)
                terminationList.Add(new GenerationNumberTermination((int)settings.MaxGeneration));
            if (settings.TargetFitness != null)
                terminationList.Add(new FitnessThresholdTermination((double)settings.TargetFitness));
            if (!string.IsNullOrEmpty(settings.TerminationType))
            {
                string terminationType = settings.TerminationType;
                string terminationParam = settings.TerminationParam;
                TerminationBase custTermination;
                if (string.IsNullOrEmpty(terminationParam))
                    custTermination = (TerminationBase)Activator.CreateInstance(Type.GetType(terminationType + AssemblySuffix));
                else
                {
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
                        custTermination = (TerminationBase)Activator.CreateInstance(termType, iParam);
                    else if ((terminationType == typeof(FitnessThresholdTermination).FullName
                        || terminationType == nameof(FitnessThresholdTermination))
                        && dParam > 0)
                        custTermination = (TerminationBase)Activator.CreateInstance(termType, dParam);
                    else if (terminationType == typeof(TimeEvolvingTermination).FullName
                        || terminationType == nameof(TimeEvolvingTermination))
                        custTermination = (TerminationBase)Activator.CreateInstance(termType, new TimeSpan(0, iParam, 0));
                    else
                        custTermination = (TerminationBase)Activator.CreateInstance(termType);
                }
                terminationList.Add(custTermination);
            }
            if (terminationList.Count == 1)
                Termination = terminationList[0];
            else if (terminationList.Count > 1)
            {
                Termination = new OrTermination(terminationList.ToArray());
            }
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
            CreateTerminator(Settings);

            IFitness Fitness = new CoreFitness(this, Settings);
            ChromosomeBase chromosome = new FloatingPointChromosome(Settings.MinValues, Settings.MaxValues, Settings.NumBits, Settings.DecimalDigits);
            Population initialPopulation = new(Settings.MinPopulationSize, Settings.MaxPopulationSize, chromosome);
            Algorithm = new(initialPopulation, Fitness, Selection, Crossover, Mutation)
            {
                Termination = Termination,
                Reinsertion = Reinsertion
            };
        }
        public CoreSolverOutput Optimize()
        {
            CoreSolverOutput output = new();
            InitializeOptimization();
            latestFitness = 0.0;
            Algorithm.GenerationRan += (sender, e) =>
            {
                var bestChromosome = Algorithm.BestChromosome as FloatingPointChromosome;
                latestFitness = bestChromosome.Fitness.Value;
                if (bestFitness < latestFitness)
                    bestFitness = latestFitness;
            };
            errorMessage = "";
            try
            {
                Algorithm.Start();
            }
            catch (Exception exc)
            { 
                output.ErrorMessage = exc.Message;
            }

            Dictionary<string, double> BestValues = new();
            int iter = 0;
            foreach (string key in Settings.SortedKeys)
            {
                var phenotype = (Algorithm.BestChromosome as FloatingPointChromosome).ToFloatingPoints();
                BestValues.Add(key, phenotype[iter++]);
            }
            output.BestValues = BestValues;
            output.BestFitness = Algorithm.BestChromosome.Fitness ?? 0;
            return output;
        }
        public void CancelOptimization()
        {
            Algorithm?.Stop();
        }
    }
}
