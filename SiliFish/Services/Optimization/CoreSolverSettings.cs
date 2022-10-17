using SiliFish.Definitions;
using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.Services.Optimization
{

    public class CoreSolverSettings
    {
        public string SelectionType { get; set; }
        public string CrossOverType { get; set; }
        public string MutationType { get; set; }
        public string ReinsertionType { get; set; }
        public string TerminationType { get; set; }
        public string TerminationParam { get; set; }
        public TargetRheobaseFunction TargetRheobaseFunction { get; set; }
        public List<FiringFitnessFunction> FitnessFunctions { get; set; }

        public Dictionary<string, double> ParamValues { get; set; }

        [JsonIgnore]
        public Dictionary<string, double> MinValueDictionary { get; set; }
        [JsonIgnore]
        public Dictionary<string, double> MaxValueDictionary { get; set; }

        [JsonIgnore]
        public List<string> SortedKeys { get { return ParamValues.Keys.OrderBy(k => k).ToList(); } }

        private double[] minValues = null;
        private double[] maxValues = null;
        private int[] numBits = null;
        private int[] decimalDigits = null;
        public double[] MinValues
        {
            get
            {
                if (minValues != null)
                    return minValues;
                int nCount = ParamValues.Count;
                minValues = new double[nCount];
                int iter = 0;
                foreach (string key in SortedKeys)
                {
                    minValues[iter++] = MinValueDictionary?.GetValueOrDefault(key, Const.GeneticAlgorithmMinValue) ?? Const.GeneticAlgorithmMinValue;
                }
                return minValues;
            }
            set { minValues = value; }
        }
        public double[] MaxValues
        {
            get
            {
                if (maxValues != null)
                    return maxValues;
                int nCount = ParamValues.Count;
                maxValues = new double[nCount];
                int iter = 0;
                foreach (string key in SortedKeys)
                {
                    maxValues[iter++] = MaxValueDictionary?.GetValueOrDefault(key, Const.GeneticAlgorithmMinValue) ?? Const.GeneticAlgorithmMinValue;
                }
                return maxValues;
            }
            set { maxValues = value; }
        }

        [JsonIgnore]
        public int[] NumBits
        {
            get
            {
                if (numBits != null)
                    return numBits;
                int nCount = ParamValues.Count;
                numBits = Enumerable.Repeat(64, nCount).ToArray();
                return numBits;
            }
            set { numBits = value; }
        }
        [JsonIgnore]
        public int[] DecimalDigits
        {
            get
            {
                if (decimalDigits != null)
                    return decimalDigits;
                int nCount = ParamValues.Count;
                decimalDigits = new int[nCount];
                int iter = 0;
                foreach (string key in SortedKeys)
                {
                    int numOfDecimalDigit = Util.NumOfDecimalDigits(ParamValues[key]);
                    decimalDigits[iter++] = numOfDecimalDigit;
                }

                decimalDigits = Enumerable.Repeat(64, nCount).ToArray();
                return decimalDigits;
            }
            set { decimalDigits = value; }
        }
        public int MinPopulationSize { get; set; }
        public int MaxPopulationSize { get; set; }
        public string CoreType { get; set; }
    }
}
