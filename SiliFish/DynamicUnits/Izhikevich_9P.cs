using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;

namespace SiliFish.DynamicUnits
{
    public class Izhikevich_9P : DynamicUnit
    {
        //a, b, c, d, are the parameters for the membrane potential dynamics
        //Default values are taken from Izhikevich 2003 (IEEE)
        private double a = 0.02;
        private double b = 0.2;
        private double c = -65;
        private double d = 2;

        private static double a_suggestedMin = 0.01;
        private static double a_suggestedMax = 1;
        private static double b_suggestedMin = 0.01;
        private static double b_suggestedMax = 1;
        private static double d_suggestedMin = -10;
        private static double d_suggestedMax = 10;
        private static double k_suggestedMin = -100;
        private static double k_suggestedMax = 100;
        private static double Cm_suggestedMin = 1;
        private static double Cm_suggestedMax = 500;


        // vmax is the peak membrane potential of single action potentials
        [JsonIgnore]
        public double Vmax;
        // vr, vt are the resting and threshold membrane potential 
        [JsonIgnore]
        public double Vr = -60, Vt = -57;
        // k is a coefficient of the quadratic polynomial 
        double k = 1;
        double Cm = 10; //the membrane capacitance

        [JsonIgnore]
        double V = -65;//Keeps the current value of V 
        [JsonIgnore]
        double u = 0;//Keeps the current value of u

        private void Initialize()
        {
            V = Vr;
            u = 0;
        }
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
            Initialize();
        }

        public Izhikevich_9P(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal?.ToDictionary(kvp=>kvp.Key, kvp=>kvp.Value as object));
            Initialize();
        }
        public virtual Dictionary<string, double> GetParametersDouble()
        {
            Dictionary<string, double> paramDict = new()
            {
                { "Izhikevich_5P.a", a },
                { "Izhikevich_5P.b", b },
                { "Izhikevich_5P.c", c },
                { "Izhikevich_5P.d", d },
                { "Izhikevich_5P.V_max", Vmax },
                { "Izhikevich_5P.V_r", Vr },
                { "Izhikevich_5P.V_t", Vt },
                { "Izhikevich_5P.k", k },
                { "Izhikevich_5P.Cm", Cm }
            };
            return paramDict;
        }

        public virtual Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = new()
            {
                { "Izhikevich_5P.a", a },
                { "Izhikevich_5P.b", b },
                { "Izhikevich_5P.c", c },
                { "Izhikevich_5P.d", d },
                { "Izhikevich_5P.V_max", Vmax },
                { "Izhikevich_5P.V_r", Vr },
                { "Izhikevich_5P.V_t", Vt },
                { "Izhikevich_5P.k", k },
                { "Izhikevich_5P.Cm", Cm }
            };
            return paramDict;
        }


        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Dictionary<string, double> MinValues = new() {
                { "Izhikevich_5P.c", c },
                { "Izhikevich_5P.V_max", Vmax },
                { "Izhikevich_5P.V_r", Vr },
                { "Izhikevich_5P.V_t", Vt },
                { "Izhikevich_5P.a", a_suggestedMin },
                { "Izhikevich_5P.b", b_suggestedMin },
                { "Izhikevich_5P.d", d_suggestedMin },
                { "Izhikevich_5P.k", k_suggestedMin },
                { "Izhikevich_5P.Cm", Cm_suggestedMin }
            };
            Dictionary<string, double> MaxValues = new() {
                { "Izhikevich_5P.c", c },
                { "Izhikevich_5P.V_max", Vmax },
                { "Izhikevich_5P.V_r", Vr },
                { "Izhikevich_5P.V_t", Vt },
                { "Izhikevich_5P.a", a_suggestedMax },
                { "Izhikevich_5P.b", b_suggestedMax },
                { "Izhikevich_5P.d", d_suggestedMax },
                { "Izhikevich_5P.k", k_suggestedMax },
                { "Izhikevich_5P.Cm", Cm_suggestedMax }
            };

            return (MinValues, MaxValues);
        }
        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            a = paramExternal.Read("Izhikevich_5P.a", a);
            b = paramExternal.Read("Izhikevich_5P.b", b);
            c = paramExternal.Read("Izhikevich_5P.c", c);
            d = paramExternal.Read("Izhikevich_5P.d", d);
            Vmax = paramExternal.Read("Izhikevich_5P.V_max", Vmax);
            V = Vr = paramExternal.Read("Izhikevich_5P.V_r", Vr);
            Vt = paramExternal.Read("Izhikevich_5P.V_t", Vt);
            k = paramExternal.Read("Izhikevich_5P.k", k);
            Cm = paramExternal.Read("Izhikevich_5P.Cm", Cm);
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

        public override DynamicsStats SolveODE(double[] I)
        {
            Initialize();
            bool onRise = false, tauRiseSet = false, onDecay = false, tauDecaySet = false;
            double decayStart = 0, riseStart = 0;
            int iMax = I.Length;
            DynamicsStats dyn = new(I);
            bool spike = false;
            double dt = RunParam.static_dt;
            for (int tIndex = 0; tIndex < iMax; tIndex++)
            {
                GetNextVal(I[tIndex], ref spike);
                dyn.VList[tIndex] = V;
                dyn.SecList[tIndex] = u;
                //if passed the 0.37 of the drop (the difference between Vmax and Vreset (or c)): 
                //V <= Vmax - 0.37 * (Vmax - c) => V <= 0.63 Vmax - 0.37 c
                if (onDecay && !tauDecaySet && V <= 0.63 * Vmax - 0.37 * c)
                {
                    dyn.TauDecay.Add(dt * tIndex, dt * (tIndex - decayStart));
                    tauDecaySet = true;
                }
                //if passed the 0.63 of the rise (the difference between between Vmax and Vr): 
                //V >= 0.63 * (Vmax - Vr) + Vr => V >= 0.63 Vmax + 0.37 Vr
                else if (onRise && !tauRiseSet && riseStart > 0 && V >= 0.63 * Vmax + 0.37 * Vr)
                {
                    dyn.TauRise.Add(dt * tIndex, dt * (tIndex - riseStart));
                    tauRiseSet = true;
                    riseStart = 0;
                }
                else if (!onRise && (V - Vt > 0))
                {
                    onRise = true;
                    tauRiseSet = false;
                    riseStart = tIndex;
                }
                else if (onDecay && tIndex > 0 && V > dyn.VList[tIndex - 1])
                {
                    onDecay = false;
                    tauDecaySet = false;
                }
                if (spike)
                {
                    if (tIndex > 0)
                        dyn.SpikeList.Add(tIndex - 1);
                    onRise = false;
                    tauRiseSet = false;
                    onDecay = true;
                    tauDecaySet = false;
                    decayStart = tIndex;
                }
            }
            dyn.DefineSpikingPattern();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxRheobase">The maximum stimulus that will be applied (to prevent deadlocks)</param>
        /// <param name="sensitivity">of the stimulus current</param>
        /// <param name="infinity">The duration the stimulus will be applied for</param>
        /// <param name="dt">delta t</param>
        /// <param name="warmup">add a warmup region to the beginning with no stimulus and spiking is ignored</param>
        /// <returns></returns>
        public override double CalculateRheoBase(double maxRheobase, double sensitivity, int infinity, double dt, int warmup = 100, int cooldown = 100)
        {
            Initialize();
            infinity = (int)(infinity / dt);
            warmup = (int)(warmup / dt);
            cooldown = (int)(cooldown / dt);
            int tmax = infinity + warmup + cooldown;
            double[] I = new double[tmax];
            double curI = maxRheobase;
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
                    curI = (curI + (rheobase > 0 ? rheobase : maxRheobase)) / 2;
                }
            }
            return rheobase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">the parameter that is updated to see the effect on rheobase</param>
        /// <param name="logScale">if true, values are set as multiplies rather than increments</param>
        /// <param name="minMultiplier"></param>
        /// <param name="maxMultiplier"></param>
        /// <param name="numOfPoints"></param>
        /// <param name="dt">delta t</param>
        /// <param name="maxRheobase">The maximum stimulus that will be applied (to prevent deadlocks)</param>
        /// <param name="sensitivity">of the stimulus current</param>
        /// <param name="infinity">the duration the stimulus will be applied</param>
        /// <returns></returns>
        public override (double[], double[]) RheobaseSensitivityAnalysis(string param, bool logScale, double minMultiplier, double maxMultiplier, int numOfPoints,
                double dt, double maxRheobase = 100, double sensitivity = 0.001, int infinity = 300)
        {
            if (maxMultiplier < minMultiplier)
                (minMultiplier, maxMultiplier) = (maxMultiplier, minMultiplier);
            Dictionary<string, object> parameters = GetParameters();
            double origValue = (double)parameters[param];
            double[] values = new double[numOfPoints];
            if (!logScale)
            {
                double incMultiplier = (maxMultiplier - minMultiplier) / (numOfPoints - 1);
                foreach (int i in Enumerable.Range(0, numOfPoints))
                    values[i] = (incMultiplier * i + minMultiplier) * origValue;
            }
            else
            {
                double logMinMultiplier = Math.Log10(minMultiplier);
                double logMaxMultiplier = Math.Log10(maxMultiplier);
                double incMultiplier = (logMaxMultiplier - logMinMultiplier) / (numOfPoints - 1);
                foreach (int i in Enumerable.Range(0, numOfPoints))
                    values[i] = Math.Pow(10, incMultiplier * i + logMinMultiplier) * origValue;
            }
            double[] rheos = new double[numOfPoints];
            int counter = 0;
            foreach (double value in values)
            {
                parameters[param] = value;
                SetParameters(parameters);
                rheos[counter++] = CalculateRheoBase(maxRheobase, sensitivity, infinity, dt);
            }
            parameters[param] = origValue;
            SetParameters(parameters);
            return (values, rheos);
        }



    }
}
