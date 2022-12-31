using SiliFish.Definitions;
using SiliFish.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.Services.Optimization
{

    public class CoreSolverSettings
    {
        private List<FitnessFunction> fitnessFunctions;

        public string SelectionType { get; set; }
        public string CrossOverType { get; set; }
        public string MutationType { get; set; }
        public string ReinsertionType { get; set; }
        public double? TargetFitness { get; set; }
        public int? MaxGeneration { get; set; }
        public string TerminationType { get; set; }
        public string TerminationParam { get; set; }
        public TargetRheobaseFunction TargetRheobaseFunction { get; set; }

        [JsonIgnore]
        public List<FitnessFunction> FitnessFunctions
        {
            get => fitnessFunctions ??= new();
            set => fitnessFunctions = value;
        }

        public object[] FitnessFunctionsObjects
        {
            get => FitnessFunctions.ToArray();
            set
            {
                fitnessFunctions = new();
                
                foreach (object item in value)
                {
                    if (item is JsonElement element)
                        fitnessFunctions.Add(FitnessFunction.FromJson(element.GetRawText()));
                    else if (item is FitnessFunction fff)
                        fitnessFunctions.Add(fff);
                }
            }
        }

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

                return decimalDigits;
            }
            set { decimalDigits = value; }
        }
        public int MinPopulationSize { get; set; }
        public int MaxPopulationSize { get; set; }
        public string CoreType { get; set; }
    }
}
