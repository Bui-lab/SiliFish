using SiliFish.Extensions;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services.Dynamics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class LeakyIntegrateAndFire : ContractibleCellCore
    {
        [Description("Resistance")]
        public double R { get; set; } = 10;

        [Description("Capacitance")]
        public double C { get; set; } = 1;

        [JsonIgnore, Browsable(false)]
        public double TimeConstant { get { return R * C; } }

        [Description("The threshold membrane potential for a spike.")]
        public double Vt { get; set; } = -57;
        [Description("Reset membrane potential after a spike."), Browsable(true)]
        public override double Vreset { get; set; } = -50;

        [JsonIgnore, Browsable(false)]
        public override double Vthreshold { get => Vt; set => Vt = value; }
        protected override void Initialize()
        {
            V = Vr;
        }
        public LeakyIntegrateAndFire()
        { }
        public LeakyIntegrateAndFire(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }

        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Dictionary<string, double> MinValues = [];
            Dictionary<string, double> MaxValues = [];

            return (MinValues, MaxValues);
        }

        public override double GetNextVal(double I, ref bool spike)
        {
            spike = false;

            if (V >= Vthreshold && V < Vmax)
            {
                spike = true;
                V = Vmax;
            }
            else if (V >= Vmax)
            {
                V = Vreset;
            }
            else
            {
                double dv = -1 / (R * C) * (V - Vr) + I / C;
                V += dv * deltaT;
            }

            return V;
        }
        public override DynamicsStats CreateDynamicsStats(double[] I)
        {
            DynamicsStats dyn = new(null, I, deltaT);
            dyn.SecLists.Add("Rel. Tension", new double[I.Length]);
            return dyn;
        }
        public override void UpdateAdditionalDynamicStats(DynamicsStats dyn, int tIndex)
        {
            double[] tensionList = dyn.SecLists["Rel. Tension"];
            tensionList[tIndex] = CalculateRelativeTension(V);
        }

    }
}
