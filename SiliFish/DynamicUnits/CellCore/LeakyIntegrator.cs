using SiliFish.Definitions;
using SiliFish.DynamicUnits.Firing;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;

//Modified from the code written by Yann Roussel and Tuan Bui

namespace SiliFish.DynamicUnits
{
    public class Leaky_Integrator : ContractibleCellCore
    {
        [Description("Resistance")]
        public double R { get; set; } 

        [Description("Capacitance")]
        public double C { get; set; }

        [JsonIgnore, Browsable(false)]
        public double TimeConstant { get { return R * C; } }
        protected override void Initialize()
        {
            V = Vr;
        }
        public Leaky_Integrator(double R, double C, double Vr)
        {
            this.R = R;
            this.C = C;
            this.Vr = Vr;
            V = Vr;
            Vmax = 99999;
        }
        public Leaky_Integrator()
        { }
        public Leaky_Integrator(double R, double C, double Vr, double Va, double Tmax, double ka, double Vmax)
        {
            this.R = R;
            this.C = C;
            this.Vr = Vr;
            this.Va = Va;
            this.Tmax = Tmax;
            this.ka = ka;
            this.Vmax = Vmax;
            V = Vr;
        }

        public Leaky_Integrator(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }


        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            if (R < GlobalSettings.Epsilon)
                errors.Add($"Leaky integrator: R has 0 value.");
            if (C < GlobalSettings.Epsilon)
                errors.Add($"Leaky integrator: C has 0 value.");
            return errors.Count == 0;
        }

        public override double GetNextVal(double Stim, ref bool spike)
        {
            double I = Stim;

            // ODE eqs
            double dv = (-1 / (R * C)) * (V - Vr) + I / C;
            double vNew = V + dv * deltaT;
            V = vNew;
            if (V >= Vmax) V = Vmax;
            if (V > Vcontraction) spike = true;
            return V;
        }
        public override DynamicsStats CreateDynamicsStats(double[] I)
        {
            DynamicsStats dyn = new(null, I, deltaT);
            dyn.SecLists.Add("Rel. Tension", new double[I.Length]);
            dyn.SecLists.Add("Tension", new double[I.Length]);
            return dyn;
        }
        public override void UpdateDynamicStats(DynamicsStats dyn, int tIndex)
        {
            double[] RelTensionList = dyn.SecLists["Rel. Tension"];
            double[] TensionList = dyn.SecLists["Tension"];
            RelTensionList[tIndex] = CalculateRelativeTension(V);
            TensionList[tIndex] = RelTensionList[tIndex] * Tmax;
        }
    }

}
