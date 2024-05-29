using SiliFish.DataTypes;
using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SiliFish.DynamicUnits.JncCore
{
    public class ElecSynapseCore : JunctionCore
    {
        #region Static members and functions
        private static readonly Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => typeof(ElecSynapseCore).IsAssignableFrom(type))
        .ToDictionary(type => type.Name, type => type);

        public static List<string> GetSynapseTypes()
        {
            return typeMap.Keys.Where(k => k != nameof(ElecSynapseCore)).ToList();
        }

        public static ElecSynapseCore CreateCore(string coreType, Dictionary<string, double> parameters)
        {
            ElecSynapseCore core = (ElecSynapseCore)Activator.CreateInstance(typeMap[coreType], parameters ?? []);
            return core;
        }

        public static ElecSynapseCore CreateCore(ElecSynapseCore copyFrom)
        {
            ElecSynapseCore syn = (ElecSynapseCore)Activator.CreateInstance(typeMap[copyFrom.SynapseType]);

            return syn;
        }

        /// <summary>
        /// static function to return dictionary of objects to keep the distributions
        /// </summary>
        /// <param name="coreType"></param>
        /// <returns></returns>
        public static Dictionary<string, Distribution> GetParameters(string coreType)
        {
            ElecSynapseCore core = CreateCore(coreType, null);
            return core?.GetParameters().ToDictionary(kvp => kvp.Key, kvp => new Constant_NoDistribution(kvp.Value) as Distribution);
        }
        #endregion

        public ElecSynapseCore()
        { }

        public ElecSynapseCore(ElecSynapseCore copyFrom)
        {
            SetParameters(copyFrom.GetParameters());
        }

        public override bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            errors ??= [];
            warnings ??= [];
            int preCount = errors.Count + warnings.Count;
            if (Conductance < GlobalSettings.Epsilon)
                errors.Add($"Electrical synapse: Conductance has 0 value.");            
            return errors.Count + warnings.Count == preCount;
        }
        public static bool CheckValues(ref List<string> errors, ref List<string> warnings, string coreType, Dictionary<string, double> param)
        {
            errors ??= [];
            warnings ??= [];
            int preCount = errors.Count + warnings.Count;     
            ChemSynapseCore core = ChemSynapseCore.CreateCore(coreType, param);
            core.CheckValues(ref errors, ref warnings);
            return errors.Count + warnings.Count == preCount;
        }
        public virtual double GetNextVal(double VoltageDiffFrom1To2, double VoltageDiffFrom2To1)
        {
            throw new NotImplementedException();
        }
    }
}
