using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class HodgkinHuxleyClassic: CellCoreUnit
    {
        protected static double g_K_suggestedMin = 30;
        protected static double g_K_suggestedMax = 40;
        protected static double g_Na_suggestedMin = 100;
        protected static double g_Na_suggestedMax = 150;
        protected static double g_L_suggestedMin = 0.1;
        protected static double g_L_suggestedMax = 0.5;
        protected static double E_K_suggestedMin = -15;
        protected static double E_K_suggestedMax = -10;
        protected static double E_Na_suggestedMin = 100;
        protected static double E_Na_suggestedMax = 150;
        protected static double E_L_suggestedMin = 10;
        protected static double E_L_suggestedMax = 11;
        protected static double Vt_suggestedMin = -60;
        protected static double Vt_suggestedMax = -55;
        protected static double Vmax_suggestedMin = 10;
        protected static double Vmax_suggestedMax = 50;
        protected static double Vr_suggestedMin = -80;
        protected static double Vr_suggestedMax = -50;
        protected static double Cm_suggestedMin = 1;
        protected static double Cm_suggestedMax = 20;

        [Description("K channel conductance.")]
        public double g_K { get; set; } = 36;

        [Description("Na channel conductance.")]
        public double g_Na { get; set; } = 120;

        [Description("Leak channel conductance.")]
        public double g_L { get; set; } = 0.3;

        [Description("K equilibrium potential.")]
        public double E_K { get; set; } = -12;

        [Description("Na equilibrium potential.")]
        public double E_Na { get; set; } = 120;

        [Description("Leak equilibrium potential.")]
        public double E_L { get; set; } = 10.6;

        [Description("Threshold membrane potential.")]
        public double Vt { get; set; } = -57;

        [Description("The membrane capacitance.")]
        public double Cm { get; set; } = 10; 

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

        [JsonIgnore]
        public override string VThresholdParamName { get { return GetType().Name+".Vt"; } }
        [JsonIgnore]
        public override string VReversalParamName { get { return GetType().Name+".Vr"; } }
        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
         {
            string type= GetType().Name;
             Dictionary<string, double> MinValues = new() {
                { type+".g_K",  g_K_suggestedMin},
                { type+".g_Na",  g_Na_suggestedMin},
                { type+".g_L",  g_L_suggestedMin},
                { type+".E_K",  E_K_suggestedMin},
                { type+".E_Na",  E_Na_suggestedMin},
                { type+".E_L",  E_L_suggestedMin},
                { type+".Vmax", Vmax_suggestedMin},
                { type+".Vr", Vr_suggestedMin},
                { type+".Vt", Vt_suggestedMin},
                { type+".Cm", Cm_suggestedMin}
             };
             Dictionary<string, double> MaxValues = new() {
                { type+".g_K",  g_K_suggestedMax},
                { type+".g_Na",  g_Na_suggestedMax},
                { type+".g_L",  g_L_suggestedMax},
                { type+".E_K",  E_K_suggestedMax},
                { type+".E_Na",  E_Na_suggestedMax},
                { type+".E_L",  E_L_suggestedMax},
                { type+".Vmax", Vmax_suggestedMax},
                { type+".Vr", Vr_suggestedMax},
                { type+".Vt", Vt_suggestedMax},
                { type+".Cm", Cm_suggestedMax}
             };

             return (MinValues, MaxValues);
         }

        public override double GetNextVal(double I, ref bool spike)
        {
            double vNew, nNew, mNew, hNew;
            spike = false;
            
            double dtTracker = 0;
            while (dtTracker < deltaT)
            {
                dtTracker += deltaTEuler;
                // ODE eqs
                double Cdv = I - IK - INa - IL;
                vNew = V + Cdv * deltaTEuler / Cm;

                double dn = alpha_n * (1 - n) - beta_n * n;
                nNew = n + deltaTEuler * dn;

                double dm = alpha_m * (1 - m) - beta_m * m;
                mNew = m + deltaTEuler * dm;

                double dh = alpha_h * (1 - h) - beta_h * h;
                hNew = h + deltaTEuler * dh;

                V = vNew;
                n = nNew;
                m = mNew;
                h = hNew;
                if (V > Vmax)
                    spike = true;
            }
            return V;
        }

        public override DynamicsStats SolveODE(double[] I)
        {
            Initialize();
            bool onRise = false, tauRiseSet = false, onDecay = false, tauDecaySet = false;
            double decayStart = 0, riseStart = 0;
            int iMax = I.Length;
            DynamicsStats dyn = new(null, I, deltaT);
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
                    dyn.TauDecay.Add(deltaT * tIndex, deltaT * (tIndex - decayStart));
                    tauDecaySet = true;
                }
                //if passed the 0.63 of the rise (the difference between between Vmax and Vr): 
                //V >= 0.63 * (Vmax - Vr) + Vr => V >= 0.63 Vmax + 0.37 Vr
                else if (onRise && !tauRiseSet && riseStart > 0 && V >= 0.63 * Vmax + 0.37 * Vr)
                {
                    dyn.TauRise.Add(deltaT * tIndex, deltaT * (tIndex - riseStart));
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
