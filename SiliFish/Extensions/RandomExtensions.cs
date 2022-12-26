using System;
using System.Linq;

namespace SiliFish.Extensions
{
    public static class RandomExtensions
    {
        //https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
        public static void Shuffle(this Random rand, double[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rand.Next(n--);
                (array[k], array[n]) = (array[n], array[k]);
            }
        }

        //https://stackoverflow.com/questions/218060/random-gaussian-variables
        public static double Gauss(this Random rand, double mean, double stdDev, double? minValue = null, double? maxValue = null)
        {
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            minValue ??= 0;
            maxValue ??= mean * (1 + 10 * stdDev);
            if (minValue > maxValue)
                (minValue, maxValue) = (maxValue, minValue);
            if (randNormal < minValue)
                randNormal = (double)minValue;
            else if (randNormal > maxValue)
                randNormal = (double)maxValue;
            return randNormal;
        }
        /// <summary>
        /// Generate n numbers with normal distribution
        /// </summary>
        public static double[] Gauss(this Random rand, double mean, double stdDev, int n, double minValue, double maxValue)
        {
            if (n <= 0) return null;
            double[] result = new double[n];
            foreach (int i in Enumerable.Range(0, n))
            {
                result[i] = rand.Gauss(mean, stdDev, minValue, maxValue);
            }
            return result;
        }
        public static double Uniform(this Random rand, double start, double end)
        {
            double u1 = rand.NextDouble(); //uniform(0,1] random doubles
            return u1 * (end - start) + start;
        }
        /// <summary>
        /// Generate n numbers with uniform distribution
        /// </summary>
        public static double[] Uniform(this Random rand, double start, double end, int n)
        {
            if (n <= 0) return null;
            double[] result = new double[n];
            foreach (int i in Enumerable.Range(0, n))
            {
                result[i] = rand.Uniform(start, end);
                if (result[i] < start) result[i] = start;
                if (result[i] > end) result[i] = end;
            }
            return result;
        }

        /// <summary>
        /// Generate equally spaced numbers with noise
        /// </summary>
        public static double[] Spaced(this Random rand, double start, double end, double noiseStdDev, int n, bool ordered = false)
        {
            if (n <= 0) return null;
            double[] result = new double[n];
            if (n == 1)
            {
                double noise = noiseStdDev > 0 ? rand.Gauss(1, noiseStdDev) : 1;
                result[0] = noise * (end + start) / 2;
                return result;
            }
            double inc = (end - start) / (n - 1);
            foreach (int i in Enumerable.Range(0, n))
            {
                double noise = noiseStdDev > 0 ? rand.Gauss(1, noiseStdDev) : 1;
                result[i] = start + i * inc * noise;
                if (result[i] < start) result[i] = start;
                if (result[i] > end) result[i] = end;
            }
            if (!ordered)
            {
                Random rnd = new Random();
                rnd.Shuffle(result);
            }
            return result;
        }


        /// <summary>
        /// Returns a random value with a bimodal distribution
        /// </summary>
        /// <param name="mode1Weight">A value between 0 and 1, to select first peak</param>
        public static double Bimodal(this Random rand, double mean1, double stdDev1, double mean2, double stdDev2, double mode1Weight, double minValue, double maxValue)
        {
            double peakSel = rand.NextDouble(); //uniform(0,1] random doubles
            if (peakSel < mode1Weight)
                return rand.Gauss(mean1, stdDev1, minValue, maxValue);
            else
                return rand.Gauss(mean2, stdDev2, minValue, maxValue);
        }

        /// <summary>
        /// Generate n numbers with a bimodal distribution
        /// </summary>
        public static double[] Bimodal(this Random rand, double mean1, double stdDev1, double mean2, double stdDev2, double mode1Weight, int n, double minValue, double maxValue)
        {
            if (n <= 0) return null;
            double[] result = new double[n];
            foreach (int i in Enumerable.Range(0, n))
                result[i] = rand.Bimodal(mean1, stdDev1, mean2, stdDev2, mode1Weight, minValue, maxValue);
            return result;
        }

    }
}
