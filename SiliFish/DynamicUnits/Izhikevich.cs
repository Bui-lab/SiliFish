using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SiliFish.Extensions;

namespace SiliFish.DynamicUnits
{
    class Izhikevich_9P
    {
        //a, b, c, d, are the parameters for the membrane potential dynamics
        private double a;
        private double b;
        private double c;
        private double d;

        // vmax is the peak membrane potential of single action potentials
        double Vmax;
        // vr, vt are the resting and threshold membrane potential 
        double Vr, Vt;
        // k is a coefficient of the quadratic polynomial 
        double k;
        double Cm; //the membrane capacitance

        [JsonIgnore]
        double V = -65;//Keeps the current value of V 
        [JsonIgnore]
        double u = 0;//Keeps the current value of u

        double init_v;
        double init_u;

        public Izhikevich_9P(MembraneDynamics dyn, double init_v, double init_u)
        {
            a = dyn?.a ?? 0;
            b = dyn?.b ?? 0;
            c = dyn?.c ?? 0;
            d = dyn?.d ?? 0;
            Vmax = dyn?.Vmax ?? 0;
            Vr = dyn?.Vr ?? 0;
            Vt = dyn?.Vt ?? 0;
            k = dyn?.k ?? 0;
            Cm = dyn?.Cm ?? 0;
            V = init_v;
            u = init_u;
            this.init_v = init_v;
            this.init_u = init_u;
        }

        public virtual Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = new()
            {
                { "Izhikevich_9P.a", a },
                { "Izhikevich_9P.b", b },
                { "Izhikevich_9P.c", c },
                { "Izhikevich_9P.d", d },
                { "Izhikevich_9P.V_max", Vmax },
                { "Izhikevich_9P.V_r", Vr },
                { "Izhikevich_9P.V_t", Vt },
                { "Izhikevich_9P.k", k },
                { "Izhikevich_9P.Cm", Cm },
                { "Izhikevich_9P.V", V },
                { "Izhikevich_9P.u", u }
            };
            return paramDict;
        }

        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            a = paramExternal.Read("Izhikevich_9P.a", a);
            b = paramExternal.Read("Izhikevich_9P.b", b);
            c = paramExternal.Read("Izhikevich_9P.c", c);
            d = paramExternal.Read("Izhikevich_9P.d", d);
            Vmax = paramExternal.Read("Izhikevich_9P.V_max", Vmax);
            Vr = paramExternal.Read("Izhikevich_9P.V_r", Vr);
            Vt = paramExternal.Read("Izhikevich_9P.V_t", Vt);
            k = paramExternal.Read("Izhikevich_9P.k", k);
            Cm = paramExternal.Read("Izhikevich_9P.Cm", Cm);
            init_v = V = paramExternal.Read("Izhikevich_9P.V", V);
            init_u = u = paramExternal.Read("Izhikevich_9P.u", u);
        }

        public virtual string GetInstanceParams()
        {
            return string.Join("\r\n", GetParameters().Select(kv => kv.Key + ": " + kv.Value.ToString()));
        }
        public double GetNextVal(double Stim, ref bool spike)
        {
            double I = Stim;
            double vNew, uNew;
            spike = false;
            if (V < Vmax)
            {
                // ODE eqs
                // Cdv refers to Capacitance * dV/dt as in Izhikevich model (Dynamical Systems in Neuroscience: page 273, Eq 8.5)
                double Cdv = k * (V - Vr) * (V - Vt) - u + I;
                vNew = V + Cdv * RunParam.static_dt / Cm;
                double du = a * (b * (V - Vr) - u);
                uNew = u + RunParam.static_dt * du;
                V = vNew;
                u = uNew;
            }
            else
            {
                // Spike
                spike = true;
                vNew = c;
                uNew = u + d;
                V = vNew;
                u = uNew;
            }
            return V;
        }

        public (double[], double[]) SolveODE(double[] I)
        {
            int tmax = I.Length;
            double[] Vlist = new double[tmax];
            double[] ulist = new double[tmax];
            bool spike = false;
            for (int t = 0; t < tmax; t++)
            {
                GetNextVal(I[t], ref spike);
                Vlist[t] = V;
                ulist[t] = u;
            }
            return (Vlist, ulist);
        }

        public bool DoesSpike(double[] I, double init_v, double init_u, int warmup)
        {
            bool spike = false;
            int tmax = I.Length;
            V = init_v;
            u = init_u;
            for (int t = 0; t < tmax; t++)
            {
                GetNextVal(I[t], ref spike);
                if (t > warmup && spike) //ignore first little bit
                    break;
            }
            return spike;
        }


        public double CalculateRheoBase(double maxI, double sensitivity, int infinity, double dt, int warmup = 100)
        {
            infinity = (int)(infinity / dt);
            warmup = (int)(warmup / dt);
            int tmax = infinity + warmup + 10;
            double[] I = new double[tmax];
            double curI = maxI;
            double minI = 0;
            double rheobase = -1;

            while (curI >= minI + sensitivity)
            {
                foreach (int i in Enumerable.Range(warmup, infinity))
                    I[i] = curI;
                if (DoesSpike(I, init_v, init_u, warmup))
                {
                    rheobase = curI;
                    curI = (curI + minI) / 2;
                }
                else //increment
                {
                    minI = curI;
                    curI = (curI + (rheobase > 0 ? rheobase : maxI)) / 2;
                }
            }
            return rheobase;
        }
    }


}
