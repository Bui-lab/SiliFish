using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
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
    [JsonDerivedType(typeof(TwoExpSyn), typeDiscriminator: "twoexp")]
    public class ChemSynapseCore: JunctionCore
    {
        #region Static members and functions
        private static readonly Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => typeof(ChemSynapseCore).IsAssignableFrom(type))
        .ToDictionary(type => type.Name, type => type);

        public static List<string> GetSynapseTypes()
        {
            return typeMap.Keys.Where(k => k != nameof(ChemSynapseCore)).ToList();
        }

        public static ChemSynapseCore CreateCore(string coreType, Dictionary<string, double> parameters)
        {
            ChemSynapseCore core = (ChemSynapseCore)Activator.CreateInstance(typeMap[coreType], parameters ?? []);
            return core;
        }

        public static ChemSynapseCore CreateCore(ChemSynapseCore copyFrom)
        {
            ChemSynapseCore syn = (ChemSynapseCore)Activator.CreateInstance(typeMap[copyFrom.SynapseType]);

            return syn;
        }

        /// <summary>
        /// static function to return dictionary of objects to keep the distributions
        /// </summary>
        /// <param name="coreType"></param>
        /// <returns></returns>
        public static Dictionary<string, Distribution> GetParameters(string coreType)
        {
            ChemSynapseCore core = CreateCore(coreType, null);
            return core?.GetParameters().ToDictionary(kvp => kvp.Key, kvp => new Constant_NoDistribution(kvp.Value) as Distribution);
        }
        #endregion

        public double ERev { get; set; }
        public ChemSynapseCore()
        { }

        public ChemSynapseCore(ChemSynapseCore copyFrom)
        {
            DeltaT = copyFrom.DeltaT;
            Conductance = copyFrom.Conductance; //unitary conductance
            SetParameters(copyFrom.GetParameters());
        }


        public override bool CheckValues(ref List<string> errors)
        {
            errors ??= [];
            if (Conductance < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Conductance has 0 value.");
            return errors.Count == 0;
        }
        public static bool CheckValues(ref List<string> errors, string coreType, Dictionary<string, double> param)
        {
            errors ??= [];
            ChemSynapseCore core = CreateCore(coreType, param);
            return core.CheckValues(ref errors);
        }
        public virtual double GetNextVal(double vPreSynapse, double vPost, List<double> spikeArrivalTimes, double tCurrent, DynamicsParam settings, bool excitatory)
        {
            throw new NotImplementedException();
        }

        public virtual bool CausesReverseCurrent(double V, bool excitatory)
        {
            throw new NotImplementedException();
        }

        public virtual void SetNeuroTransmitter(ModelSettings settings, NeuronClass neuronClass)
        {
            switch (neuronClass)//fill default values, based on the NT
            {
                case NeuronClass.Glycinergic:
                    ERev = settings.E_gly;
                    break;
                case NeuronClass.GABAergic:
                    ERev = settings.E_gaba;
                    break;
                case NeuronClass.Glutamatergic:
                    ERev = settings.E_glu;
                    break;
                case NeuronClass.Cholinergic:
                    ERev = settings.E_ach;
                    break;
                default:
                    break;
            }
        }
    }
}
