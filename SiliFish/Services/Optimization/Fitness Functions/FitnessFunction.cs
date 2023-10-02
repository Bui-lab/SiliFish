using SiliFish.Definitions;
using SiliFish.DynamicUnits.Firing;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.Services.Optimization
{
    public class FitnessFunction
    {
        private static readonly Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(FitnessFunction).IsAssignableFrom(type) && nameof(FitnessFunction) != type.Name)
                .ToDictionary(type => type.Name, type => type);

        private string fitnessFunctionType;
        public string FitnessFunctionType { get { return fitnessFunctionType; } set { fitnessFunctionType = value; } }
        public double Weight { get; set; }
        public bool MinMaxExists, CurrentRequired, ModeExists;//no need to save in JSON - they are class based parameters
        public bool PreStimulus = false, PostStimulus = false;

        public double ValueMin { get; set; }//valid only if MinMaxExists = true
        public double ValueMax { get; set; }//valid only if MinMaxExists = true

        public bool RheobaseBased { get; set; }//valid only if CurrentRequired = true
        public double CurrentValueOrRheobaseMultiplier { get; set; }//valid only if CurrentRequired = true
        [JsonIgnore]
        public virtual string Details
        {
            get
            {
                string minmaxInfo = MinMaxExists ? $"[{ValueMin:0.###}-{ValueMax:0.###}];" : "";
                string rheobaseMult = CurrentRequired && RheobaseBased ? " Rheobase x " : "";
                string currentInfo = CurrentRequired ? $"Current: {rheobaseMult}{CurrentValueOrRheobaseMultiplier:0.###};" : "";
                return $"{minmaxInfo} {currentInfo}";
            }
        }
        public virtual double CalculateFitness(DynamicsStats stat)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public double CalculateFitnessFor(double d)
        {
            //rather than checking [ValueMin, ValueMax], check (ValueMin - 1, ValueMax + 1)
            //to prevent division with small numbers in the next step
            if (ValueMin - 1 < d && ValueMax + 1 > d)
                return Weight;
            if (d < ValueMin)
                return Weight / (ValueMin - d);
            return Weight / (d - ValueMax);

        }

        public FitnessFunction()
        {
            fitnessFunctionType = GetType().Name;
        }

        public virtual string[] GetFiringOptions()
        {
            return null;
        }

        public static FitnessFunction FromJson(string json)
        {
            FitnessFunction ff = JsonSerializer.Deserialize<FitnessFunction>(json);
            return typeMap.TryGetValue(ff.fitnessFunctionType, out var type) ?
                    (FitnessFunction)JsonSerializer.Deserialize(json, type) :
                    null;
        }

        public static Dictionary<string, Type> TypeMap { get => typeMap; }
    }
}
