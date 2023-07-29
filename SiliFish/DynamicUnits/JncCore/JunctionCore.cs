using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits.JncCore
{
    public class JunctionCore: BaseCore
    {
        public static int CoreParamMaxCount = 10;
        public double Conductance { get; set; }

        [JsonIgnore]
        public virtual double ISyn { get { throw new NotImplementedException(); } }

        public virtual void ZeroISyn() { throw new NotImplementedException(); }

        [JsonIgnore, Browsable(false)]
        public string SynapseType => GetType().Name;

        [JsonIgnore, Browsable(false)]
        public virtual string Identifier => throw new NotImplementedException();
        
        public double DeltaT, DeltaTEuler;

        public virtual void InitForSimulation()
        {
            throw new NotImplementedException();
        }
    }
}
