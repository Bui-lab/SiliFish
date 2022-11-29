using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SiliFish.Extensions;

namespace SiliFish.DynamicUnits
{
    class Leaky_Integrator
    {
        public double R, C;

        [JsonIgnore]
        double V; //keeps the current v value 
        public Leaky_Integrator(double R, double C, double init_v)
        {
            //Set Neuron constants.
            this.R = R;
            this.C = C;
            V = init_v;
        }
        public virtual Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = new()
            {
                { "Leaky_Integrator.R", R },
                { "Leaky_Integrator.C", C },
                { "Leaky_Integrator.V", V }
            };
            return paramDict;
        }

        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            R = paramExternal.Read("Leaky_Integrator.R", R);
            C = paramExternal.Read("Leaky_Integrator.C", C);
            V = paramExternal.Read("Leaky_Integrator.V", V);
        }

        public virtual string GetInstanceParams()
        {
            return string.Join("\r\n", GetParameters().Select(kv => kv.Key + ": " + kv.Value.ToString()));
        }

        public double GetNextVal(double Stim)
        {
            double I = Stim;
            // ODE eqs
            double dv = -1 / (R * C) * V + I / C;
            double vNew = V + dv * RunParam.static_dt;
            V = vNew;

            return V;
        }
        public double[] SolveODE(double[] I)
        {
            int tmax = I.Length;
            double[] Vlist = new double[tmax];

            for (int t = 0; t < tmax; t++)
            {
                GetNextVal(I[t]);
                Vlist[t] = this.V;
            }
            return Vlist;
        }
    }

}
