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
                return thisArray.Min();
            if (iStart < 0 || iStart >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iEnd <= -1 || iEnd >= thisArray.Length)
                iEnd = thisArray.Length - 1;
            if (iStart > iEnd) return 0;
            return thisArray.Skip(iStart).Take(iEnd - iStart).Max();
        }
    }
}
