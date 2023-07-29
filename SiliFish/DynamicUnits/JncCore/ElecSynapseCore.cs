﻿using SiliFish.DataTypes;
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
    [JsonDerivedType(typeof(SimpleGap), typeDiscriminator: "simple")]
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

        public static ElecSynapseCore CreateCore(string coreType, Dictionary<string, double> parameters, double rundt, double eulerdt)
        {
            ElecSynapseCore core = (ElecSynapseCore)Activator.CreateInstance(typeMap[coreType], parameters ?? new Dictionary<string, double>(), rundt, eulerdt);
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
            ElecSynapseCore core = CreateCore(coreType, null, 0, 0);
            return core?.GetParameters().ToDictionary(kvp => kvp.Key, kvp => new Constant_NoDistribution(kvp.Value) as Distribution);
        }
        #endregion

        public ElecSynapseCore()
        { }
        public ElecSynapseCore(double rundt, double eulerdt)
        {
            DeltaT = rundt;
            DeltaTEuler = eulerdt;
        }

        public ElecSynapseCore(ElecSynapseCore copyFrom)
        {
            DeltaT = copyFrom.DeltaT;
            DeltaTEuler = copyFrom.DeltaTEuler;
            SetParameters(copyFrom.GetParameters());//TODO - test; if works put parameter collection to the above constructor
        }


        public override bool CheckValues(ref List<string> errors)
        {
            errors ??= new();
            if (Conductance < GlobalSettings.Epsilon)
                errors.Add($"Electrical synapse: Conductance has 0 value.");
            return errors.Count == 0;
        }
        public static bool CheckValues(ref List<string> errors, string coreType, Dictionary<string, double> param)
        {
            errors ??= new();
            ChemSynapseCore core = ChemSynapseCore.CreateCore(coreType, param, 0, 0);
            return core.CheckValues(ref errors);
        }
        public virtual double GetNextVal(double VoltageDiffFrom1To2, double VoltageDiffFrom2To1)
        {
            throw new NotImplementedException();
        }
    }
}
