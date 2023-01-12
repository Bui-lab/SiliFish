using SiliFish.Extensions;
using SiliFish.ModelUnits.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class Leaky_Integrator : CellCoreUnit
    {
        public double R; //resustance
        public double C;//capacitance
        public double Va; //Vm when tension is half of Tmax/2
        public double Tmax; //maximum tension
        public double ka; //slope factor [Dulhunty 1992 (Prog. Biophys)]
        public double Vmax;

        [JsonIgnore]
        public double TimeConstant { get { return R * C; } }
        protected override void Initialize()
        {
            V = Vr;
        }
        //private double Vmax; //the possible maximum membrane potential 
        public Leaky_Integrator(double R, double C, double Vr)
        {
            this.R = R;
            this.C = C;
            this.Vr = Vr;
            V = Vr;
            Vmax = 99999;
        }
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

        public override string VThresholdParamName { get { return ""; } }
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

        public virtual void FillMissingParameters(Dictionary<string, double> paramExternal)
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
            FillMissingParameters(paramExternal);

            paramExternal.TryGetValue("Leaky_Integrator.R", out R);
            paramExternal.TryGetValue("Leaky_Integrator.C", out C);
            paramExternal.TryGetValue("Leaky_Integrator.Vr", out Vr);
            paramExternal.TryGetValue("Leaky_Integrator.Va", out Va);
            paramExternal.TryGetValue("Leaky_Integrator.Tmax", out Tmax);
            paramExternal.TryGetValue("Leaky_Integrator.ka", out ka);
            paramExternal.TryGetValue("Leaky_Integrator.Vmax", out Vmax);
            V = Vr;
        }


        //formula from [Dulhunty 1992 (Prog. Biophys)]
        public double CalculateRelativeTension(double? Vm = null) //if Vm is null, current V value is used
        {
            //T_a = T_max / (1 + exp(V_a - V_m) / k_a
            return 1 / (1 + Math.Exp((Va - (Vm ?? V)) / ka));
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
            double dt = RunParam.static_dt_Euler;
            double dtTracker = 0;
            while (dtTracker < RunParam.static_dt)
            {
                dtTracker += dt; // ODE eqs
                double dv = (-1 / (R * C)) * (V - Vr) + I / C;
                double vNew = V + dv * dt;
                V = vNew;
                if (V >= Vmax) V = Vmax;
            }
            return V;
        }
        public override DynamicsStats SolveODE(double[] I)
        {
            int iMax = I.Length;
            DynamicsStats dyn = new(I);
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
