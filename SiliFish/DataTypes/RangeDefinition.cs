using SiliFish.Helpers;

namespace SiliFish.DataTypes
{
    public struct NumberRangeDefinition
    {
        public double MinMultiplier;
        public double MaxMultiplier;
        public int NumOfPoints;
        public bool LogScale;
        public double[] GenerateValues(double origValue)
        {
            return Util.GenerateValues(origValue, this);
        }
    }
}
