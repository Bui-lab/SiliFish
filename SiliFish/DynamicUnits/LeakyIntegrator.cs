using System.Collections.Generic;
using System.Linq;
using SiliFish.Extensions;

namespace SiliFish.DynamicUnits
{
    class Leaky_Integrator
    {
        public double R, C;
        double v; //keeps the current v value 
        public Leaky_Integrator(double R, double C, double init_v)
        {
            //Set Neuron constants.
            this.R = R;
            this.C = C;
            v = init_v;
        }
        public virtual Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = new()
            {
                { "Leaky_Integrator.R", R },
                { "Leaky_Integrator.C", C },
                { "Leaky_Integrator.V", v }
            };
            return paramDict;
        }

        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            R = paramExternal.Read("Leaky_Integrator.R", R);
            C = paramExternal.Read("Leaky_Integrator.C", C);
            v = paramExternal.Read("Leaky_Integrator.V", v);
        }

        public virtual string GetInstanceParams()
        {
            return string.Join("\r\n", GetParameters().Select(kv => kv.Key + ": " + kv.Value.ToString()));
        }

        public double GetNextVal(double Stim)
        {
            double I = Stim;
            // ODE eqs
            double dv = -1 / (R * C) * v + I / C;
            double vNew = v + dv * RunParam.static_dt;
            v = vNew;

            return v;
        }
        public double[] SolveODE(double[] I)
        {
            int tmax = I.Length;
            double[] Vlist = new double[tmax];

            for (int t = 0; t < tmax; t++)
            {
                GetNextVal(I[t]);
                Vlist[t] = this.v;
            }
            return Vlist;
        }
    }

}
