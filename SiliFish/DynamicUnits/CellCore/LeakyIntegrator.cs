using SiliFish.Definitions;
using SiliFish.Services.Dynamics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

//Modified from the code written by Yann Roussel and Tuan Bui

namespace SiliFish.DynamicUnits
{
    public class Leaky_Integrator : ContractibleCellCore
    {
        [Description("Resistance")]
        public double R { get; set; } = 10;

        [Description("Capacitance")]
        public double C { get; set; } = 1;

        [JsonIgnore, Browsable(false)]
        public double TimeConstant { get { return R * C; } }

        [Description("Contraction threshold potential to determine rheobase for non-spiking cores")]
        public double Vcontraction { get; set; } = GlobalSettings.BiologicalMaxPotential;

        [JsonIgnore, Browsable(false)]
        public override double VSpikeThreshold { get => Vcontraction; }//to determine whether there is a spike or not


        public override double Vthreshold { get => Vcontraction; set => Vcontraction = value; }
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

        public override bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            errors ??= [];
            warnings ??= [];
            int preCount = errors.Count + warnings.Count;
            base.CheckValues(ref errors, ref warnings);
            if (R < GlobalSettings.Epsilon)
                errors.Add($"Leaky integrator: R has 0 value.");
            if (C < GlobalSettings.Epsilon)
                errors.Add($"Leaky integrator: C has 0 value.");           
            return errors.Count + warnings.Count == preCount;
        }

        public override double GetNextVal(double Stim, ref bool spike)
        {
            double I = Stim;
            spike = false;
            // ODE eqs
            double dv = (-1 / (R * C)) * (V - Vr) + I / C;
            double vNew = V + dv * deltaT;
            if (V < Vcontraction && vNew >= Vcontraction) 
                spike = true;
            V = vNew;
            if (V >= Vmax) V = Vmax;
            return V;
        }
        public override DynamicsStats CreateDynamicsStats(double[] I)
        {
            DynamicsStats dyn = new(null, I, deltaT);
            dyn.SecLists.Add("Rel. Tension", new double[I.Length]);
            dyn.SecLists.Add("Tension", new double[I.Length]);
            return dyn;
        }
        public override void UpdateAdditionalDynamicStats(DynamicsStats dyn, int tIndex)
        {
            double[] RelTensionList = dyn.SecLists["Rel. Tension"];
            double[] TensionList = dyn.SecLists["Tension"];
            RelTensionList[tIndex] = CalculateRelativeTension(V);
            TensionList[tIndex] = RelTensionList[tIndex] * Tmax;
        }
    }

}
