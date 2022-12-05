using SiliFish.Definitions;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class Izhikevich_9P : CellCoreUnit
    {
        private static double a_suggestedMin = 0.01;
        private static double a_suggestedMax = 1;
        private static double b_suggestedMin = 0.01;
        private static double b_suggestedMax = 1;
        private static double d_suggestedMin = -10;
        private static double d_suggestedMax = 10;
        private static double k_suggestedMin = 0.01;
        private static double k_suggestedMax = 100;
        private static double Cm_suggestedMin = 1;
        private static double Cm_suggestedMax = 500;

        //a, b, c, d, are the parameters for the membrane potential dynamics
        //Default values are taken from Izhikevich 2003 (IEEE)
        public double a = 0.02;
        public double b = 0.2;
        public double c = -65;
        public double d = 2;

        // threshold membrane potential 
        public double Vt = -57;
        // k is a coefficient of the quadratic polynomial 
        public double k = 1;
        public double Cm = 10; //the membrane capacitance

        [JsonIgnore]
        double u = 0;//Keeps the current value of u

        public Izhikevich_9P(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }
        protected override void Initialize()
        {
            V = Vr;
            u = b * Vr;
        }

        public override Dictionary<string, double> GetParameters()
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

        public override string VThresholdParamName { get { return "Izhikevich_9P.V_t"; } } 
        public override string VReversalParamName { get { return "Izhikevich_9P.V_r"; } }
        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Dictionary<string, double> MinValues = new() {
                { "Izhikevich_9P.c", c },
                { "Izhikevich_9P.V_max", Vmax },
                { "Izhikevich_9P.V_r", Vr },
                { "Izhikevich_9P.V_t", Vt },
                { "Izhikevich_9P.a", a_suggestedMin },
                { "Izhikevich_9P.b", b_suggestedMin },
                { "Izhikevich_9P.d", d_suggestedMin },
                { "Izhikevich_9P.k", k_suggestedMin },
                { "Izhikevich_9P.Cm", Cm_suggestedMin }
            };
            Dictionary<string, double> MaxValues = new() {
                { "Izhikevich_9P.c", c },
                { "Izhikevich_9P.V_max", Vmax },
                { "Izhikevich_9P.V_r", Vr },
                { "Izhikevich_9P.V_t", Vt },
                { "Izhikevich_9P.a", a_suggestedMax },
                { "Izhikevich_9P.b", b_suggestedMax },
                { "Izhikevich_9P.d", d_suggestedMax },
                { "Izhikevich_9P.k", k_suggestedMax },
                { "Izhikevich_9P.Cm", Cm_suggestedMax }
            };

            return (MinValues, MaxValues);
        }
        public override void SetParameters(Dictionary<string, double> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            paramExternal.TryGetValue("Izhikevich_9P.a", out a);
            paramExternal.TryGetValue("Izhikevich_9P.b", out b);
            paramExternal.TryGetValue("Izhikevich_9P.c", out c);
            paramExternal.TryGetValue("Izhikevich_9P.d", out d);
            paramExternal.TryGetValue("Izhikevich_9P.V_max", out Vmax);
            paramExternal.TryGetValue("Izhikevich_9P.V_r", out Vr);
            paramExternal.TryGetValue("Izhikevich_9P.V_t", out Vt);
            paramExternal.TryGetValue("Izhikevich_9P.k", out k);
            paramExternal.TryGetValue("Izhikevich_9P.Cm", out Cm);
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
                    double Cdv = k * (V - Vr) * (V - Vt) - u + I;
                    vNew = V + Cdv * dt / Cm;
                    double du = a * (b * (V - Vr) - u);
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


    }
}
