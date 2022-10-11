using GeneticSharp;
using System;
using System.Collections.Generic;



namespace SiliFish.Extensions
{
    public class GeneticAlgorithmExtension
    {
        public static List<Type> GetSelectionBases()
        {
            List<Type> selectionBases = new()
            {
                typeof(EliteSelection),
                typeof(RouletteWheelSelection),
                typeof(StochasticUniversalSamplingSelection),
                typeof(TournamentSelection),
                typeof(TruncationSelection)
            };
            return selectionBases;
        }

        public static List<Type> GetCrossoverBases()
        {
            List<Type> crossoverBases = new()
            {
                //can be only used with ordered chromosomes. The specified chromosome has repeated genes - typeof(AlternatingPositionCrossover),
                //can be only used with ordered chromosomes. The specified chromosome has repeated genes - typeof(CutAndSpliceCrossover),
                //can be only used with ordered chromosomes. The specified chromosome has repeated genes - typeof(CycleCrossover),
                typeof(OnePointCrossover),
                //can be only used with ordered chromosomes. The specified chromosome has repeated genes - typeof(OrderBasedCrossover),
                //can be only used with ordered chromosomes. The specified chromosome has repeated genes - typeof(OrderedCrossover),
                //can be only used with ordered chromosomes. The specified chromosome has repeated genes - typeof(PartiallyMappedCrossover),
                //can be only used with ordered chromosomes. The specified chromosome has repeated genes - typeof(PositionBasedCrossover),
                typeof(ThreeParentCrossover),
                typeof(TwoPointCrossover),
                typeof(UniformCrossover),
                typeof(VotingRecombinationCrossover)
            };
            return crossoverBases;
        }

        public static List<Type> GetMutationBases()
        {
            List<Type> mutationBases = new()
            {
                typeof(DisplacementMutation),
                typeof(FlipBitMutation),
                typeof(InsertionMutation),
                typeof(PartialShuffleMutation),
                typeof(ReverseSequenceMutation),
                typeof(TworsMutation),
                typeof(UniformMutation)
            };
            return mutationBases;
        }

        public static List<Type> GetReinsertionBases()
        {
            List<Type> mutationBases = new()
            {
                typeof(ElitistReinsertion),
                //Cannot expand the number of chromosome in population typeof(FitnessBasedReinsertion),
                //Cannot expand the number of chromosome in population typeof(PureReinsertion),
                typeof(UniformReinsertion)
            };
            return mutationBases;
        }

        public static string GetTerminationParameter(string terminationBase)
        {
            if (terminationBase == typeof(GenerationNumberTermination).FullName)
                return "Number of generations";
            if (terminationBase == typeof(TimeEvolvingTermination).FullName)
                return "Time to run in minutes";
            if (terminationBase == typeof(FitnessStagnationTermination).FullName)
                return "Max iteration w/o progress";
            if (terminationBase == typeof(FitnessThresholdTermination).FullName)
                return "Target fitness";

            return "No parameter required";
        }
        public static List<Type> GetTerminationBases()
        {
            List<Type> terminationBases = new()
            {
                typeof(GenerationNumberTermination),
                typeof(TimeEvolvingTermination),
                typeof(FitnessStagnationTermination),
                typeof(FitnessThresholdTermination)
            };

            //terminationBases.Add(typeof(AndTermination));
            //terminationBases.Add(typeof(OrTermination));
            return terminationBases;
        }
    }
}
