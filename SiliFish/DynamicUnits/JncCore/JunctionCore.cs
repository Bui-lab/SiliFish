using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits.JncCore
{
    [JsonDerivedType(typeof(TwoExpSyn), typeDiscriminator: "twoexpsyn")]
    [JsonDerivedType(typeof(SimpleSyn), typeDiscriminator: "simplesyn")]
    [JsonDerivedType(typeof(SimpleGap), typeDiscriminator: "simplegap")]
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
        
        public double DeltaT;

        public virtual void InitForSimulation(double deltaT, ref int uniqueID)
        {
            UniqueId = uniqueID++;
            this.DeltaT = deltaT;
        }
    }
}
