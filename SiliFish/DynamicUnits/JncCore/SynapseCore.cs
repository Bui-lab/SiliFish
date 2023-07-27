using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Junction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits.JncCore
{
    [JsonDerivedType(typeof(SimpleSyn), typeDiscriminator: "simple")]
    [JsonDerivedType(typeof(TwoExpSyn), typeDiscriminator: "hodgkinhuxley")]
    public class SynapseCore: BaseCore
    {
        #region Static members and functions
        private static readonly Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => typeof(SynapseCore).IsAssignableFrom(type))
        .ToDictionary(type => type.Name, type => type);

        public static int CoreParamMaxCount = 5;

        public static List<string> GetSynapseTypes()
        {
            return typeMap.Keys.Where(k => k != nameof(SynapseCore)).ToList();
        }

        public static SynapseCore CreateCore(string coreType, Dictionary<string, double> parameters, double conductance, double rundt, double eulerdt)
        {
            SynapseCore core = (SynapseCore)Activator.CreateInstance(typeMap[coreType], parameters ?? new Dictionary<string, double>(), conductance, rundt, eulerdt);
            return core;
        }

        public static SynapseCore CreateCore(SynapseCore copyFrom)
        {
            SynapseCore syn = (SynapseCore)Activator.CreateInstance(typeMap[copyFrom.SynapseType]);

            return syn;
        }

        /// <summary>
        /// static function to return dictionary of objects to keep the distributions
        /// </summary>
        /// <param name="coreType"></param>
        /// <returns></returns>
        public static Dictionary<string, Distribution> GetParameters(string coreType)
        {
            SynapseCore core = CreateCore(coreType, null, 0, 0, 0);
            return core?.GetParameters().ToDictionary(kvp => kvp.Key, kvp => new Constant_NoDistribution(kvp.Value) as Distribution);
        }
        #endregion

        [JsonIgnore, Browsable(false)]
        public string SynapseType => GetType().Name;
        public double DeltaT, DeltaTEuler;
        public double Conductance { get; set; }

        [JsonIgnore]
        public virtual double ISyn { get { throw new NotImplementedException(); } }


        public SynapseCore()
        { }
        public SynapseCore(double conductance, double rundt, double eulerdt)
        {
            DeltaT = rundt;
            DeltaTEuler = eulerdt;
            Conductance = conductance; //unitary conductance
        }

        public SynapseCore(SynapseCore copyFrom)
        {
            DeltaT = copyFrom.DeltaT;
            DeltaTEuler = copyFrom.DeltaTEuler;
            Conductance = copyFrom.Conductance; //unitary conductance
            SetParameters(copyFrom.GetParameters());//TODO - test; if works put parameter collection to the above constructor
        }
        public virtual void InitForSimulation(double conductance)
        {
            throw new NotImplementedException();
        }

        public override bool CheckValues(ref List<string> errors)
        {
            errors ??= new();
            if (Conductance < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Conductance has 0 value.");
            return errors.Count == 0;
        }
        public static bool CheckValues(ref List<string> errors, string coreType, Dictionary<string, double> param, double conductance)
        {
            errors ??= new();
            SynapseCore core = SynapseCore.CreateCore(coreType, param, conductance, 0, 0);
            return core.CheckValues(ref errors);
        }
        public virtual double GetNextVal(double vPreSynapse, double vPost, double t_t0)
        {
            throw new NotImplementedException();
        }
    }
}
