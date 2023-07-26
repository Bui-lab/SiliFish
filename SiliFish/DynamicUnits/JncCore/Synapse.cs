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
    public class Synapse
    {
        #region Static members and functions
        private static readonly Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => typeof(Synapse).IsAssignableFrom(type))
        .ToDictionary(type => type.Name, type => type);

        public static List<string> GetSynapseTypes()
        {
            return typeMap.Keys.Where(k => k != nameof(Synapse)).ToList();
        }

        public static Synapse CreateSynapse(string coreType, SynapseParameters param, double conductance, double rundt, double eulerdt)
        {
            Synapse core = (Synapse)Activator.CreateInstance(typeMap[coreType], param, conductance, rundt, eulerdt);
            return core;
        }

        public static Synapse CreateSynapse(Synapse copyFrom)
        {
            Synapse syn = (Synapse)Activator.CreateInstance(typeMap[copyFrom.SynapseType]);

            return syn;
        }


        #endregion
        
        [JsonIgnore, Browsable(false)]
        public string SynapseType => GetType().Name;
        public double DeltaT, DeltaTEuler;
        public double TauR { get; set; }
        public double TauD { get; set; }
        public double Vth { get; set; }
        public double ERev { get; set; }
        public double Conductance { get; set; }

        [JsonIgnore]
        public virtual double ISyn { get { throw new NotImplementedException(); } }


        public Synapse()
        { }
        public Synapse(SynapseParameters param, double conductance, double rundt, double eulerdt)
        {
            DeltaT = rundt;
            DeltaTEuler = eulerdt;
            //Set synapse constants.
            TauD = param.TauD;
            TauR = param.TauR;
            Vth = param.Vth;
            ERev = param.Erev;
            Conductance = conductance; //unitary conductance
        }

        public Synapse(Synapse copyFrom)
        {
            DeltaT = copyFrom.DeltaT;
            DeltaTEuler = copyFrom.DeltaTEuler;
            //Set synapse constants.
            TauD = copyFrom.TauD;
            TauR = copyFrom.TauR;
            Vth = copyFrom.Vth;
            ERev = copyFrom.ERev;
            Conductance = copyFrom.Conductance; //unitary conductance
        }
        public virtual void InitForSimulation(double conductance)
        {
            throw new NotImplementedException();
        }

        public virtual bool CheckValues(ref List<string> errors)
        {
            errors ??= new();
            if (TauD < GlobalSettings.Epsilon || TauR < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Tau has 0 value.");
            if (Conductance < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Conductance has 0 value.");
            return errors.Count == 0;
        }
        public virtual double GetNextVal(double vPreSynapse, double vPost, double t_t0)
        {
            throw new NotImplementedException();
        }
    }
}
