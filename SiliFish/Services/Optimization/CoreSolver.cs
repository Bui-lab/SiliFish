using GeneticSharp;
using SiliFish.Definitions;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
//https://diegogiacomelli.com.br/function-optimization-with-geneticsharp/
namespace SiliFish.Services.Optimization
{
    public class CoreSolverOutput
    {
        public Dictionary<string, double> Values;
        public double Fitness;
        public string ErrorMessage;
        public override bool Equals(object obj)
        {
            if (obj is not CoreSolverOutput cso)
                return false;
            if (Values.Count != cso.Values.Count)
                return false;
            foreach (KeyValuePair<string, double> kvp in Values)
            {
                if (!cso.Values.ContainsKey(kvp.Key))
                    return false;
                if (Math.Abs(cso.Values[kvp.Key] - kvp.Value) > double.Epsilon)
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
    public class CoreSolver
    {
        static string AssemblySuffix = ", GeneticSharp.Domain";

        public CoreSolverSettings Settings { get; set; }
        public DynamicsParam DynamicsParam { get; set; }

        private SelectionBase Selection;
        private CrossoverBase Crossover;
        private MutationBase Mutation;
        private ReinsertionBase Reinsertion;
        private ITermination Termination;
        private GeneticAlgorithm Algorithm;

        private double latestFitness = 0.0;
        private double bestFitness = 0.0;
        private List<FloatingPointChromosome> Candidates;
        /// <summary>
        /// The algorithm does not always return the best solution, if the next generation's solution is not as well.
        /// bestestchromosome is kept as a bookmark, and returns the best available solution at the end. 
        /// </summary>
        IChromosome bestestChromosome = null;

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
                    return (int)(100 * Algorithm.TimeEvolving / tet.MaxTime);
                if (Algorithm.Termination is FitnessStagnationTermination fst)
                    return -1;// fst.GetNumberOfStagnantGenerations() / fst.ExpectedStagnantGenerationsNumber;
                if (Algorithm.Termination is FitnessThresholdTermination ftt)
                    return (int)(100 * (Algorithm.BestChromosome?.Fitness.Value ?? 0) / ftt.ExpectedFitness);
                return 0;
            }
        }

        private void CreateTerminator(CoreSolverSettings settings)
        {
            List<TerminationBase> terminationList = [];
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
            Selection = (SelectionBase)Activator.CreateInstance(Type.GetType(Settings.SelectionType + AssemblySuffix));
            Crossover = (CrossoverBase)Activator.CreateInstance(Type.GetType(Settings.CrossOverType + AssemblySuffix));
            Mutation = (MutationBase)Activator.CreateInstance(Type.GetType(Settings.MutationType + AssemblySuffix));
            Reinsertion = (ReinsertionBase)Activator.CreateInstance(Type.GetType(Settings.ReinsertionType + AssemblySuffix));
            CreateTerminator(Settings);

            IFitness Fitness = new CoreFitness(this, Settings, DynamicsParam);
            ChromosomeBase chromosome = new FloatingPointChromosome(Settings.MinValues, Settings.MaxValues, Settings.NumBits, Settings.DecimalDigits);
            Population initialPopulation = new(Settings.MinPopulationSize, Settings.MaxPopulationSize, chromosome);
            Algorithm = new(initialPopulation, Fitness, Selection, Crossover, Mutation)
            {
                Termination = Termination,
                Reinsertion = Reinsertion
            };
            latestFitness = 0;
            Candidates = [];
        }

        private void CheckResult(FloatingPointChromosome chromosome, bool single)
        {
            latestFitness = chromosome.Fitness.Value;
            if (Candidates.Any(c => c.EquivalentTo(chromosome)))
                return;
            if (bestFitness < latestFitness)
            {
                bestFitness = latestFitness;
                bestestChromosome = chromosome;
                if (single)
                {
                    Candidates.Clear();
                    Candidates.Add(chromosome);
                    return;
                }
            }
            if (Candidates.Count < GlobalSettings.GeneticAlgorithmSolutionCount)
                Candidates.Add(chromosome);
            else if (Candidates.Min(c => c.Fitness.Value) < latestFitness)
            {
                FloatingPointChromosome toRemove = Candidates.First(c => c.Fitness.Value <= Candidates.Min(c => c.Fitness.Value) + double.Epsilon);
                Candidates.Remove(toRemove);
                Candidates.Add(chromosome);
            }
        }

        public (List<CoreSolverOutput> Outputs, string ErrMessage) Optimize(bool single)
        {
            try
            {
                string errMessage = null;
                InitializeOptimization();

                Algorithm.GenerationRan += (sender, e) =>
                {
                    FloatingPointChromosome bestChromosome = Algorithm.BestChromosome as FloatingPointChromosome;
                    if (bestChromosome != null)
                    {
                        CheckResult(bestChromosome, single);
                    }
                };
                errorMessage = "";
                try
                {
                    Algorithm.Start();
                }
                catch (Exception exc)
                {
                    errMessage = exc.Message;
                }

                List<CoreSolverOutput> results = [];
                foreach (FloatingPointChromosome chromosome in Candidates)
                {
                    CoreSolverOutput output = new();
                    Dictionary<string, double> paramValues = [];
                    int iter = 0;
                    foreach (string key in Settings.SortedKeys)
                    {
                        var phenotype = chromosome.ToFloatingPoints();
                        paramValues.Add(key, phenotype[iter++]);
                    }
                    output.Values = paramValues;
                    output.Fitness = chromosome.Fitness ?? 0;
                    results.Add(output);
                }
                return (results, errMessage);
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }
        public void CancelOptimization()
        {
            Algorithm?.Stop();
        }
    }
}
