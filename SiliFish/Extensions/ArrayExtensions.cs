using System.Linq;

namespace SiliFish.Extensions
{
    public static class ArrayExtensions
    {
        public static double[] AddArray(this double[] thisArray, double[] array)
        {
            return thisArray.Zip(array, (x, y) => x + y).ToArray();
        }
    }
}
