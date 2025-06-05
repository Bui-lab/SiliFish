using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Extensions
{
    public static class ListExtensions
    {
        public static bool Equivalent<T>(this List<T> thisList, List<T> secondList)
        {
            if (thisList?.Count != secondList?.Count)
                return false;
            for (int i = 0; i < thisList?.Count; i++)
            {
                if (!thisList[i].Equals(secondList[i])) return false;

            }
            return true;
        }
        public static bool IsEmpty(this List<string> thisList)
        {
            for (int i = 0; i < thisList?.Count; i++)
            {
                if (!string.IsNullOrEmpty(thisList[i])) return false;

            }
            return true;
        }

        public static double Mode(this List<double> thisList, int precision)
        {
            Dictionary<double, int> counts = [];
            foreach (double item in thisList)
            {
                // Round the item to the specified precision
                double roundedItem = System.Math.Round(item, precision);
                if (counts.TryGetValue(roundedItem, out int value))
                    counts[roundedItem] = ++value;
                else
                    counts[roundedItem] = 1;
            }
            double mode = 0;
            int maxCount = 0;
            foreach (var kvp in counts)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    mode = kvp.Key;
                }
            }
            return mode;
        }

        public static (double, (double, double)) MedianAndIQR(this List<double> thisList)
        {
            if (thisList == null || thisList.Count == 0)
                return (0, (0, 0));
            double median;
            List<double> sortedList = thisList.OrderBy(x => x).ToList();
            int count = sortedList.Count;
            if (count % 2 == 0)
                median = (sortedList[count / 2 - 1] + sortedList[count / 2]) / 2.0;
            else
                median = sortedList[count / 2];
            double Q1 = sortedList[count / 4];
            double Q3 = sortedList[3 * count / 4];
            return (median, (Q1, Q3));
        }
    }
}
