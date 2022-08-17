using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SiliFish.Extensions;

namespace SiliFish.DynamicUnits
{
    class Leaky_Integrator
    {
        public double R; //resustance
        public double C;//capacitance
        public double Vr; //resting membrane potential
        public double Va; //Vm when tension is half of Tmax/2
        public double Tmax; //maximum tension
        public double ka; //slope factor [Dulhunty 1992 (Prog. Biophys)]

        [JsonIgnore]
        public double TimeConstant { get { return R * C; } }

        [JsonIgnore]
        double V; //keeps the current v value 
        public Leaky_Integrator(double R, double C, double Vr)
        {
            //Set Neuron constants.
            this.R = R;
            this.C = C;
            this.Vr = Vr;
            V = Vr;
        }
        public Leaky_Integrator(double R, double C, double Vr, double Va, double Tmax, double ka)
        {
            //Set Neuron constants.
            this.R = R;
            this.C = C;
            this.Vr = Vr;
            this.Va = Va;
            this.Tmax = Tmax;
            this.ka = ka;
            V = Vr;
        }

        public virtual Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = new()
            {
                { "Leaky_Integrator.R", R },
                { "Leaky_Integrator.C", C },
                { "Leaky_Integrator.Vr", Vr },
                { "Leaky_Integrator.Va", Va },
                { "Leaky_Integrator.Tmax", Tmax },
                { "Leaky_Integrator.ka", ka }
            };
            return paramDict;
        }

        public virtual void FillMissingParameters(Dictionary<string, object> paramExternal)
        {
            paramExternal.AddObject("Leaky_Integrator.R", R, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.C", C, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.Vr", Vr, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.Va",Va , skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.Tmax", Tmax, skipIfExists: true);
            paramExternal.AddObject("Leaky_Integrator.ka", ka, skipIfExists: true);
        }
        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            FillMissingParameters(paramExternal);
            R = paramExternal.Read("Leaky_Integrator.R", R);
            C = paramExternal.Read("Leaky_Integrator.C", C);
            Vr = paramExternal.Read("Leaky_Integrator.Vr", Vr);
            Va = paramExternal.Read("Leaky_Integrator.Va", Va);
            Tmax = paramExternal.Read("Leaky_Integrator.Tmax", Tmax);
            ka = paramExternal.Read("Leaky_Integrator.ka", ka);
        }

        public virtual string GetInstanceParams()
        {
            return string.Join("\r\n", GetParameters().Select(kv => kv.Key + ": " + kv.Value.ToString()));
        }

        //formula from [Dulhunty 1992 (Prog. Biophys)]
        public double GetRelativeTension(double? Vm = null) //if Vm is null, current V value is used
        {
            //T_a = T_max / (1 + exp(V_a - V_m) / k_a
            return 1 / (1 + Math.Exp((Va - (Vm??V)) / ka));
        }

        //formula from [Dulhunty 1992 (Prog. Biophys)]
        public double GetTension(double? Vm = null) //if Vm is null, current V value is used
        {
            //T_a = T_max / (1 + exp(V_a - V_m) / k_a
            return Tmax * GetRelativeTension(Vm);
        }



        public double GetNextVal(double Stim)
        {
            double I = Stim;
            // ODE eqs
            double dv = (-1 / (R * C)) * V + I / C;
            double vNew = V + dv * RunParam.static_dt;
            V = vNew;

            return V;
        }
        public Dynamics SolveODE(double[] I)
        {
            int iMax = I.Length;
            Dynamics dyn = new(iMax);

            for (int t = 0; t < iMax; t++)
            {
                GetNextVal(I[t]);
                dyn.Vlist[t] = this.V;
            }
            return dyn;
        }
    }

}
