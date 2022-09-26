using System;
using System.Collections.Generic;
using GeneticSharp;



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
                typeof(StochasticUniversalSamplingSelection)
            };
            return selectionBases;
        }

        public static List<Type> GetCrossoverBases()
        {
            List<Type> crossoverBases = new()
            {
                typeof(UniformCrossover),
                //TODO giving an error message - test whether there needs to be a parameter typeof(CutAndSpliceCrossover),
                typeof(CycleCrossover),
                typeof(OnePointCrossover),
                typeof(OrderBasedCrossover),
                typeof(OrderedCrossover),
                typeof(PartiallyMappedCrossover),
                typeof(PositionBasedCrossover),
                typeof(ThreeParentCrossover),
                typeof(TwoPointCrossover),
                typeof(UniformCrossover)
            };
            return crossoverBases;
        }

        public static List<Type> GetMutationBases()
        {
            List<Type> mutationBases = new()
            {
                typeof(FlipBitMutation),
                typeof(ReverseSequenceMutation),
                typeof(TworsMutation),
                typeof(UniformMutation)
            };
            return mutationBases;
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
