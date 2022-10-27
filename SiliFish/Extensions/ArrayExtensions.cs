using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Extensions
{
    public static class ArrayExtensions
    {
        public static double[] AddArray(this double[] thisArray, double[] array)
        {
            return thisArray.Zip(array, (x, y) => x + y).ToArray();
        }

        public static double MinValue(this double[] thisArray, int iStart = 0, int iEnd = -1)
        {
            if (iEnd == -1 && iStart == 0)
                return thisArray.Min();
            if (iStart < 0 || iStart >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iEnd <= -1 || iEnd >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iStart > iEnd) return 0;
            return thisArray.Skip(iStart).Take(iEnd - iStart).Min();
        }
        public static double MaxValue(this double[] thisArray, int iStart = 0, int iEnd = -1)
        {
            if (iEnd == -1 && iStart == 0)
                return thisArray.Max();
            if (iStart < 0 || iStart >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iEnd <= -1 || iEnd >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iStart > iEnd) return 0;
            return thisArray.Skip(iStart).Take(iEnd - iStart).Max();
        }

        public static double AverageValue(this double[] thisArray, int iStart = 0, int iEnd = -1)
        {
            if (thisArray == null || !thisArray.Any())
                return 0;
            if (iEnd == -1 && iStart == 0)
                return thisArray.Average();
            if (iStart < 0 || iStart >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iEnd <= -1 || iEnd >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iStart > iEnd) return 0;
            return thisArray.Skip(iStart).Take(iEnd - iStart).Average();
        }

        public static double StandardDeviation(this double[] thisArray, int iStart = 0, int iEnd = -1)
        {
            if (thisArray == null || !thisArray.Any())
                return 0;
            double avg = thisArray.AverageValue(iStart, iEnd);
            
            if (iEnd == -1 && iStart == 0)
                return Math.Sqrt(thisArray.Average(v => Math.Pow(v - avg, 2))); 
            if (iStart < 0 || iStart >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iEnd <= -1 || iEnd >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iStart > iEnd) return 0;
            return Math.Sqrt(thisArray.Skip(iStart).Take(iEnd - iStart).Average(v => Math.Pow(v - avg, 2)));
        }

        public static bool IsRandom(this double[] thisArray, double randomDegree, out bool decreasing, out bool increasing)
        {
            if (thisArray.Length <= 1)
            {
                decreasing = false;
                increasing = false;
                return false;
            }
            decreasing = true;//skip the first interval thisArray[1] < thisArray[0];
            increasing = true;//skip the first intervalthisArray[1] > thisArray[0];
            for (int i = 2; i < thisArray.Length; i++)
            {
                if (decreasing && thisArray[i] > thisArray[i - 1] + Const.Epsilon)
                {
                    decreasing = false;
                }
                if (increasing && thisArray[i] < thisArray[i - 1] - Const.Epsilon)
                {
                    increasing = false; 
                }
                if (!decreasing && !increasing)
                    break;
            }
            if (increasing || decreasing)
                return false;
            double std = thisArray.StandardDeviation();
            double avg = thisArray.AverageValue();
            return (std / avg) > randomDegree;
        }
        public static List<int> GetPeakIndices(this double[] thisArray, double threshold, int iStart = 0, int iEnd = -1)
        {
            List<int> indices = new();
            if (iEnd < 0 || iEnd >= thisArray.Length)
                iEnd = thisArray.Length - 1;

            while (true)
            {
                int ind = Array.FindIndex(thisArray, iStart, value => value >= threshold);
                if (ind > iEnd || ind < 0) break;
                while (ind < iEnd - 1 && thisArray[ind + 1] > thisArray[ind]) //find the peak
                    ind++;
                indices.Add(ind);
                while (ind < iEnd - 1 && thisArray[ind + 1] > threshold) //continue to ierate until it falls below threshold value
                    ind++;
                iStart = ind + 1;
            }
            return indices;
        }
    }
}
