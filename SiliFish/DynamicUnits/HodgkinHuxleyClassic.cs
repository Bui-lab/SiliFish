using SiliFish.Extensions;
using System;
using System.Collections.Generic;

namespace SiliFish.DynamicUnits
{
    public class HodgkinHuxleyClassic: DynamicUnit
    {
        public double g_K = 36;
        public double g_Na = 120;
        public double g_L = 0.3;
        public double E_K = -12;
        public double E_Na = 120;
        public double E_L = 10.6;

        // threshold membrane potential 
        public double Vt = -57;
        public double Cm = 10; //the membrane capacitance

        //keeps the current value of n, m, and h
        protected double n = 0;
        protected double m = 0;
        protected double h = 0;
        //protected double Vmax; //the possible maximum membrane potential 
        protected virtual double IK { get { return g_K * Math.Pow(n, 4) * (V - E_K); } }
        protected virtual double INa { get { return g_Na * Math.Pow(m, 3) * h * (V - E_Na); } }
        protected virtual double IL { get { return g_L * (V - E_L); } }

        protected virtual double alpha_n { get { return 0.01 * (10 - V) / (Math.Exp((10 - V) / 10) - 1); } }
        protected virtual double beta_n { get { return 0.125 * Math.Exp(-V / 80); } }
        protected virtual double alpha_m { get { return 0.1 * (25 - V) / (Math.Exp((25 - V) / 10) - 1); } }
        protected virtual double beta_m { get { return 4 * Math.Exp(-V / 18); } }
        protected virtual double alpha_h { get { return 0.07 * Math.Exp(-V / 20); } }
        protected virtual double beta_h { get { return 1 / (Math.Exp((10 - V) / 10) + 1); } }

        public HodgkinHuxleyClassic()
        {
            Initialize();
        }
        public HodgkinHuxleyClassic(double Vr, double Vmax)
        {
            this.Vr = Vr;
            this.Vmax = Vmax;
            V = Vr;
        }
        public HodgkinHuxleyClassic(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }
        protected override void Initialize()
        {
            V = Vr = 0;
        }
        public override Dictionary<string, double> GetParameters()
        {
            Dictionary<string, double> paramDict = new()
            {
                { "HodgkinHuxley.g_K",  g_K},
                { "HodgkinHuxley.g_Na",  g_Na},
                { "HodgkinHuxley.g_L",  g_L},
                { "HodgkinHuxley.E_K",  E_K},
                { "HodgkinHuxley.E_Na",  E_Na},
                { "HodgkinHuxley.E_L",  E_L},
                { "HodgkinHuxley.V_max", Vmax },
                { "HodgkinHuxley.V_r", Vr },
                { "HodgkinHuxley.V_t", Vt },
                { "HodgkinHuxley.Cm", Cm }
            };
            return paramDict;
        }

        public override string GetParamName_Threshold
        {
            get
            {
                return "HodgkinHuxley.V_t";
            }
        }
       /* public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Dictionary<string, double> MinValues = new() {
                { "HodgkinHuxley.c", c },
                { "HodgkinHuxley.V_max", Vmax },
                { "HodgkinHuxley.V_r", Vr },
                { "HodgkinHuxley.V_t", Vt },
                { "HodgkinHuxley.a", a_suggestedMin },
                { "HodgkinHuxley.b", b_suggestedMin },
                { "HodgkinHuxley.d", d_suggestedMin },
                { "HodgkinHuxley.k", k_suggestedMin },
                { "HodgkinHuxley.Cm", Cm_suggestedMin }
            };
            Dictionary<string, double> MaxValues = new() {
                { "HodgkinHuxley.c", c },
                { "HodgkinHuxley.V_max", Vmax },
                { "HodgkinHuxley.V_r", Vr },
                { "HodgkinHuxley.V_t", Vt },
                { "HodgkinHuxley.a", a_suggestedMax },
                { "HodgkinHuxley.b", b_suggestedMax },
                { "HodgkinHuxley.d", d_suggestedMax },
                { "HodgkinHuxley.k", k_suggestedMax },
                { "HodgkinHuxley.Cm", Cm_suggestedMax }
            };

            return (MinValues, MaxValues);
        }*/

        public override void SetParameters(Dictionary<string, double> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            paramExternal.TryGetValue("HodgkinHuxley.g_K", out g_K);
            paramExternal.TryGetValue("HodgkinHuxley.g_Na", out g_Na);
            paramExternal.TryGetValue("HodgkinHuxley.g_L", out g_L);
            paramExternal.TryGetValue("HodgkinHuxley.E_K", out E_K);
            paramExternal.TryGetValue("HodgkinHuxley.E_Na", out E_Na);
            paramExternal.TryGetValue("HodgkinHuxley.E_L", out E_L);
            paramExternal.TryGetValue("HodgkinHuxley.V_max", out Vmax);
            paramExternal.TryGetValue("HodgkinHuxley.V_r", out Vr);
            paramExternal.TryGetValue("HodgkinHuxley.V_t", out Vt);
            paramExternal.TryGetValue("HodgkinHuxley.Cm", out Cm);
        }

        public override double GetNextVal(double I, ref bool spike)
        {
            double vNew, nNew, mNew, hNew;
            spike = false;
            //if (V < Vmax)
            {
                // ODE eqs
                double Cdv = I - IK - INa - IL;
                vNew = V + Cdv * RunParam.static_dt / Cm;

                double dn = alpha_n * (1 - n) - beta_n * n;
                nNew = n + RunParam.static_dt * dn;

                double dm = alpha_m * (1 - m) - beta_m * m;
                mNew = m + RunParam.static_dt * dm;

                double dh = alpha_h * (1 - h) - beta_h * h;
                hNew = h + RunParam.static_dt * dh;

                V = vNew;
                n = nNew;
                m = mNew;
                h = hNew;
            }
            if (V > Vmax)
                spike = true;
            return V;
        }

        public override DynamicsStats SolveODE(double[] I)
        {
            Initialize();
            bool onRise = false, tauRiseSet = false, onDecay = false, tauDecaySet = false;
            double decayStart = 0, riseStart = 0;
            int iMax = I.Length;
            DynamicsStats dyn = new(I);
            dyn.SecLists.Add("n", new double[I.Length]);
            dyn.SecLists.Add("m", new double[I.Length]);
            dyn.SecLists.Add("h", new double[I.Length]);
            dyn.SecLists.Add("I_K", new double[I.Length]);
            dyn.SecLists.Add("I_Na", new double[I.Length]);
            dyn.SecLists.Add("I_L", new double[I.Length]);
            double[] nList = dyn.SecLists["n"];
            double[] mList = dyn.SecLists["m"];
            double[] hList = dyn.SecLists["h"];
            double[] I_KList = dyn.SecLists["I_K"];
            double[] I_NaList = dyn.SecLists["I_Na"];
            double[] I_LList = dyn.SecLists["I_L"];

            bool spike = false;
            double dt = RunParam.static_dt;
            for (int tIndex = 0; tIndex < iMax; tIndex++)
            {
                GetNextVal(I[tIndex], ref spike);
                dyn.VList[tIndex] = V;
                nList[tIndex] = n;
                mList[tIndex] = m;
                hList[tIndex] = h;
                I_KList[tIndex] = IK;
                I_NaList[tIndex] = INa;
                I_LList[tIndex] = IL;
                //if passed the 0.37 of the drop (the difference between Vmax and Vreset): 
                //V <= Vmax - 0.37 * (Vmax - c) => V <= 0.63 Vmax - 0.37 Vr
                if (onDecay && !tauDecaySet && V <= 0.63 * Vmax - 0.37 * Vr)
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
