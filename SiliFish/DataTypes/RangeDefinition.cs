using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
