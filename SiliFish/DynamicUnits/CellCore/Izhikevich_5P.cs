using SiliFish.Extensions;
using SiliFish.ModelUnits.Parameters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class Izhikevich_5P : CellCoreUnit
    {
        private static double a_suggestedMin = 0.01;
        private static double a_suggestedMax = 1;
        private static double b_suggestedMin = -1.01;
        private static double b_suggestedMax = 1.01;
        private static double d_suggestedMin = 0;
        private static double d_suggestedMax = 10.01;

        //Default values are taken from Izhikevich 2003 (IEEE)
        public double a = 0.02;
        public double b = 0.2;
        public double c = -65;
        public double d = 2;
        [JsonIgnore]
        double u = 0;//Keeps the current value of u

        protected override void Initialize()
        {
            V = Vr;
            u = b * V;
        }

        public Izhikevich_5P()
        {
        }
        public Izhikevich_5P(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }
        public override Dictionary<string, double> GetParameters()
        {
            Dictionary<string, double> paramDict = new()
            {
                { "Izhikevich_5P.a", a },
                { "Izhikevich_5P.b", b },
                { "Izhikevich_5P.c", c },
                { "Izhikevich_5P.d", d },
                { "Izhikevich_5P.V_max", Vmax },
                { "Izhikevich_5P.V_r", Vr }
            };
            return paramDict;
        }

        public override string VThresholdParamName { get { return ""; } }
        public override string VReversalParamName { get { return "Izhikevich_5P.V_r"; } }
        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Dictionary<string, double> MinValues = new() {
                { "Izhikevich_5P.c", c },
                { "Izhikevich_5P.V_max", Vmax },
                { "Izhikevich_5P.V_r", Vr },
                { "Izhikevich_5P.a", a_suggestedMin },
                { "Izhikevich_5P.b", b_suggestedMin },
                { "Izhikevich_5P.d", d_suggestedMin }
            };
            Dictionary<string, double> MaxValues = new() {
                { "Izhikevich_5P.c", c },
                { "Izhikevich_5P.V_max", Vmax },
                { "Izhikevich_5P.V_r", Vr },
                { "Izhikevich_5P.a", a_suggestedMax },
                { "Izhikevich_5P.b", b_suggestedMax },
                { "Izhikevich_5P.d", d_suggestedMax }
            };

            return (MinValues, MaxValues);
        }

        public override void BackwardCompatibility(Dictionary<string, double> paramExternal)
        {
            paramExternal.AddObject("Izhikevich_5P.a", a, skipIfExists: true);
            paramExternal.AddObject("Izhikevich_5P.b", b, skipIfExists: true);
            paramExternal.AddObject("Izhikevich_5P.c", c, skipIfExists: true);
            paramExternal.AddObject("Izhikevich_5P.d", d, skipIfExists: true);
            paramExternal.AddObject("Izhikevich_5P.V_max", Vmax, skipIfExists: true);
            paramExternal.AddObject("Izhikevich_5P.V_r", Vr, skipIfExists: true);
        }
        public override void SetParameters(Dictionary<string, double> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            BackwardCompatibility(paramExternal);
            paramExternal.TryGetValue("Izhikevich_5P.a", out a);
            paramExternal.TryGetValue("Izhikevich_5P.b", out b);
            paramExternal.TryGetValue("Izhikevich_5P.c", out c);
            paramExternal.TryGetValue("Izhikevich_5P.d", out d);
            paramExternal.TryGetValue("Izhikevich_5P.V_max", out Vmax);
            paramExternal.TryGetValue("Izhikevich_5P.V_r", out Vr);
        }

        public override double GetNextVal(double I, ref bool spike)
        {
            double vNew, uNew;
            spike = false;
            double dt = RunParam.static_dt_Euler;
            double dtTracker = 0;
            while (dtTracker < RunParam.static_dt)
            {
                dtTracker += dt;
                if (V < Vmax)
                {
                    // ODE eqs
                    // Cdv refers to Capacitance * dV/dt as in Izhikevich model (Dynamical Systems in Neuroscience: page 273, Eq 8.5)
                    double Cdv = 0.04 * V * V + 5 * V + 140 - u + I;
                    vNew = V + Cdv * dt;// / Cm;
                    double du = a * (b * V - u);
                    uNew = u + dt * du;
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
                    break;
                }
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
            dyn.SecLists.Add("u", new double[I.Length]);
            double[] feedbackCurrent = dyn.SecLists["u"];
            
            bool spike = false;
            double dt = RunParam.static_dt;
            for (int tIndex = 0; tIndex < iMax; tIndex++)
            {
                GetNextVal(I[tIndex], ref spike);
                dyn.VList[tIndex] = V;
                feedbackCurrent[tIndex] = u;
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
                else if (!onRise && (V - Vr > 0))//Vr is used instead of Vt
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

    }
}
