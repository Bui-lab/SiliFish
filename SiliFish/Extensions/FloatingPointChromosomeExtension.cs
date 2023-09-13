using GeneticSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Extensions
{
    public static class FloatingPointChromosomeExtension
    {

        public static bool EquivalentTo(this FloatingPointChromosome chromosome1, FloatingPointChromosome chromosome2)
        {
            double[] values1 = chromosome1.ToFloatingPoints();
            double[] values2 = chromosome2.ToFloatingPoints();
            for (int i = 0; i < values1.Length; i++)
            {
                if (Math.Abs(values1[i] - values2[i]) > double.Epsilon)
                    return false;
            }
            return true;
        }

    }
}
