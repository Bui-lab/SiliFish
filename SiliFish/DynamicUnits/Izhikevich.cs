using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SiliFish.Extensions;
using SiliFish.Helpers;

namespace SiliFish.DynamicUnits
{
       public class Izhikevich_9P
    {
        //a, b, c, d, are the parameters for the membrane potential dynamics
        private double a;
        private double b;
        private double c;
        private double d;

        // vmax is the peak membrane potential of single action potentials
        [JsonIgnore]
        public double Vmax;
        // vr, vt are the resting and threshold membrane potential 
        [JsonIgnore]
        public double Vr, Vt;
        // k is a coefficient of the quadratic polynomial 
        double k;
        double Cm; //the membrane capacitance

        [JsonIgnore]
        double V = -65;//Keeps the current value of V 
        [JsonIgnore]
        double u = 0;//Keeps the current value of u


        public Izhikevich_9P(MembraneDynamics dyn)
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
            V = Vr;
            u = 0;
        }

        public virtual Dictionary<string, double> GetParametersDouble()
        {
            Dictionary<string, double> paramDict = new()
            {
                { "Izhikevich_9P.a", a },
                { "Izhikevich_9P.b", b },
                { "Izhikevich_9P.c", c },
                { "Izhikevich_9P.d", d },
                { "Izhikevich_9P.V_max", Vmax },
                { "Izhikevich_9P.V_r", Vr },
                { "Izhikevich_9P.V_t", Vt },
                { "Izhikevich_9P.k", k },
                { "Izhikevich_9P.Cm", Cm }
            };
            return paramDict;
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
                { "Izhikevich_9P.Cm", Cm }
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
            V = Vr = paramExternal.Read("Izhikevich_9P.V_r", Vr);
            Vt = paramExternal.Read("Izhikevich_9P.V_t", Vt);
            k = paramExternal.Read("Izhikevich_9P.k", k);
            Cm = paramExternal.Read("Izhikevich_9P.Cm", Cm);
        }

        public virtual string GetInstanceParams()
        {
            return string.Join("\r\n", GetParameters().Select(kv => kv.Key + ": " + kv.Value.ToString()));
        }
        public double GetNextVal(double I, ref bool spike)
        {
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

        public DynamicsStats SolveODE(double[] I)
        {
            bool onRise = false, tauRiseSet = false, onDecay = false, tauDecaySet = false;
            double decayStart = 0, riseStart = 0;
            int iMax = I.Length;
            DynamicsStats dyn = new(I);
            bool spike = false;
            double dt = RunParam.static_dt;
            for (int t = 0; t < iMax; t++)
            {
                GetNextVal(I[t], ref spike);
                dyn.VList[t] = V;
                dyn.SecList[t] = u;
                //if passed the 0.37 of the drop (the difference between Vmax and Vreset (or c)): 
                //V <= Vmax - 0.37 * (Vmax - c) => V <= 0.63 Vmax - 0.37 c
                if (onDecay && !tauDecaySet && V <= 0.63 * Vmax - 0.37 * c)
                {
                    dyn.TauDecay.Add(dt * t, dt * (t - decayStart));
                    tauDecaySet = true;
                }
                //if passed the 0.63 of the rise (the difference between between Vmax and Vr): 
                //V >= 0.63 * (Vmax - Vr) + Vr => V >= 0.63 Vmax + 0.37 Vr
                else if (onRise && !tauRiseSet && riseStart > 0 && V >= 0.63 * Vmax + 0.37 * Vr)
                {
                    dyn.TauRise.Add(dt * t, dt * (t - riseStart));
                    tauRiseSet = true;
                    riseStart = 0;
                }
                else if (!onRise && (V - Vt > 0))
                {
                    onRise = true;
                    tauRiseSet = false;
                    riseStart = t;
                }
                else if (onDecay && t > 0 && V > dyn.VList[t - 1])
                {
                    onDecay = false;
                    tauDecaySet = false;
                }
                if (spike)
                {
                    if (t > 0)
                        dyn.SpikeList.Add(t - 1);
                    onRise = false;
                    tauRiseSet = false;
                    onDecay = true;
                    tauDecaySet = false;
                    decayStart = t;
                }
            }
            return dyn;
        }

        public bool DoesSpike(double[] I, int warmup)
        {
            bool spike = false;
            int tmax = I.Length;
            V = Vr;
            u = 0;
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
                if (DoesSpike(I, warmup))
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
