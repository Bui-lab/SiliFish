using SiliFish.Definitions;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class Leaky_Integrator : CellCoreUnit
    {
        [Description("Resistance")]
        public double R { get; set; } 

        [Description("Capacitance")]
        public double C { get; set; }

        [Description("Vm when tension is half of Tmax/2")]
        public double Va { get; set; }

        [Description("Maximum tension")]
        public double Tmax { get; set; }

        [Description("Slope factor")]// [Dulhunty 1992 (Prog. Biophys)]
        public double ka { get; set; }

        [JsonIgnore]
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

        [JsonIgnore]
        public override string VThresholdParamName { get { return ""; } }
        [JsonIgnore]
        public override string VReversalParamName { get { return "Leaky_Integrator.Vr"; } }

        public override Dictionary<string, double> GetParameters()
        {
            Dictionary<string, double> paramDict = new()
            {
                { "Leaky_Integrator.R", R },
                { "Leaky_Integrator.C", C },
                { "Leaky_Integrator.Vr", Vr },
                { "Leaky_Integrator.Va", Va },
                { "Leaky_Integrator.Tmax", Tmax },
                { "Leaky_Integrator.ka", ka },
                { "Leaky_Integrator.Vmax", Vmax }
            };
            return paramDict;
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

        public override void BackwardCompatibility(Dictionary<string, double> paramExternal)
        {
            paramExternal.AddObject("Leaky_Integrator.R", R, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.C", C, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.Vr", Vr, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.Va", Va, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.Tmax", Tmax, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.ka", ka, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.Vmax", Vmax, skipIfExists: true);
        }
        public override void SetParameters(Dictionary<string, double> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            BackwardCompatibility(paramExternal);

            if (paramExternal.TryGetValue("Leaky_Integrator.R", out double R2))
                R= (double)R2;
            if (paramExternal.TryGetValue("Leaky_Integrator.C", out double C2))
                C= (double)C2;
            if (paramExternal.TryGetValue("Leaky_Integrator.Vr", out double Vr2))
                Vr= (double)Vr2;
            if (paramExternal.TryGetValue("Leaky_Integrator.Va", out double Va2))
                Va= (double)Va2;
            if (paramExternal.TryGetValue("Leaky_Integrator.Tmax", out double Tmax2))
                Tmax= (double)Tmax2;
            if (paramExternal.TryGetValue("Leaky_Integrator.ka", out double ka2))
                ka= (double)ka2;
            if (paramExternal.TryGetValue("Leaky_Integrator.Vmax", out double Vmax2))
                Vmax= (double)Vmax2;
            V = Vr;
        }

        public override void SetParameter(string name, double value)
        {
            switch (name)
            {
                case "Leaky_Integrator.R":
                    R = value;
                    break;
                case "Leaky_Integrator.C":
                    C = value;
                    break;
                case "Leaky_Integrator.Vr":
                    Vr = value;
                    break;
                case "Leaky_Integrator.Va":
                    Va = value;
                    break;
                case "Leaky_Integrator.Tmax":
                    Tmax = value;
                    break;
                case "Leaky_Integrator.ka":
                    ka = value;
                    break;
                case "Leaky_Integrator.Vmax":
                    Vmax = value;
                    break;
            }
        }


        //formula from [Dulhunty 1992 (Prog. Biophys)]
        public double CalculateRelativeTension(double? Vm = null) //if Vm is null, current V value is used
        {
            //T_a = T_max / (1 + exp(V_a - V_m) / k_a
            double dv = Va - (Vm ?? V);
            return 1 / (1 + Math.Exp(dv / ka));
        }

        //formula from [Dulhunty 1992 (Prog. Biophys)]
        public double CalculateTension(double? Vm = null) //if Vm is null, current V value is used
        {
            return Tmax * CalculateRelativeTension(Vm);
        }

        public double[] CalculateRelativeTension(double[] V)
        {
            return V.Select(v => CalculateRelativeTension(v)).ToArray();
        }
        public double[] CalculateTension(double[] V)
        {
            return V.Select(v => CalculateTension(v)).ToArray();
        }

        public override double GetNextVal(double Stim, ref bool spike)
        {
            double I = Stim;
            double dtTracker = 0;
             while (dtTracker < deltaT)
            {
                dtTracker += deltaTEuler;
                // ODE eqs
                double dv = (-1 / (R * C)) * (V - Vr) + I / C;
                double vNew = V + dv * deltaTEuler;
                V = vNew;
                if (V >= Vmax) V = Vmax;
            }
            return V;
        }
        public override DynamicsStats SolveODE(double[] I)
        {
            int iMax = I.Length;
            DynamicsStats dyn = new(null, I, deltaT);
            dyn.SecLists.Add("Rel. Tension", new double[I.Length]);
            double[] tensionList = dyn.SecLists["Rel. Tension"];
            bool spike = false;
            for (int t = 0; t < iMax; t++)
            {
                GetNextVal(I[t], ref spike);
                dyn.VList[t] = V;
                tensionList[t] = CalculateRelativeTension(V);
            }
            return dyn;
        }
    }

}
