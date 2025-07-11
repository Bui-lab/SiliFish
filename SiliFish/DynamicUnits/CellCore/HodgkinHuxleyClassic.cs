using SiliFish.Definitions;
using SiliFish.Services.Dynamics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class HodgkinHuxleyClassic : CellCore
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
        protected virtual double beta_h { get { return 1 / (Math.Exp((30 - V) / 10) + 1); } }

        protected double minE, maxE;
        public HodgkinHuxleyClassic()
        {
            Initialize();
        }
        public HodgkinHuxleyClassic(double Vr, double Vmax)
        {
            this.Vr = Vr;
            this.Vmax = Vmax;
            Initialize();
        }
        public HodgkinHuxleyClassic(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }
        protected override void Initialize()
        {
            V = Vr;
            minE = Math.Min(E_Na, Math.Min(E_K, E_L));
            maxE = Math.Max(E_Na, Math.Max(E_K, E_L));
        }

        [JsonIgnore, Browsable(false)]
        public override double Vthreshold { get => Vt; set => Vt = value; }

        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Dictionary<string, double> MinValues = new() {
                { "g_K",  g_K_suggestedMin},
                { "g_Na",  g_Na_suggestedMin},
                { "g_L",  g_L_suggestedMin},
                { "E_K",  E_K_suggestedMin},
                { "E_Na",  E_Na_suggestedMin},
                { "E_L",  E_L_suggestedMin},
                { "Vmax", Vmax_suggestedMin},
                { "Vr", Vr_suggestedMin},
                { "Vt", Vt_suggestedMin},
                { "Cm", Cm_suggestedMin}
             };
            Dictionary<string, double> MaxValues = new() {
                { "g_K",  g_K_suggestedMax},
                { "g_Na",  g_Na_suggestedMax},
                { "g_L",  g_L_suggestedMax},
                { "E_K",  E_K_suggestedMax},
                { "E_Na",  E_Na_suggestedMax},
                { "E_L",  E_L_suggestedMax},
                { "Vmax", Vmax_suggestedMax},
                { "Vr", Vr_suggestedMax},
                { "Vt", Vt_suggestedMax},
                { "Cm", Cm_suggestedMax}
             };

            return (MinValues, MaxValues);
        }

        public override double GetNextVal(double I, ref bool spike)
        {
            double vNew, nNew, mNew, hNew;
            spike = false;
            if (V > Vmax)
                V = Vr;

            // ODE eqs
            double Cdv = I - IK - INa - IL;
            vNew = V + Cdv * deltaT / Cm;

            double dn = alpha_n * (1 - n) - beta_n * n;
            nNew = n + deltaT * dn;

            double dm = alpha_m * (1 - m) - beta_m * m;
            mNew = m + deltaT * dm;

            double dh = alpha_h * (1 - h) - beta_h * h;
            hNew = h + deltaT * dh;

            V = vNew;
            n = nNew;
            m = mNew;
            h = hNew;
            if (V > Vmax)
            {
                if (V > maxE)
                    V = maxE;
                spike = true;
            }
            if (V < minE)
                V = minE;

            return V;
        }

        public override DynamicsStats CreateDynamicsStats(DynamicsParam dynamicsParam, double[] I)
        {
            DynamicsStats dyn = new(dynamicsParam, I, deltaT);
            dyn.SecLists.Add("n", new double[I.Length]);
            dyn.SecLists.Add("m", new double[I.Length]);
            dyn.SecLists.Add("h", new double[I.Length]);
            dyn.SecLists.Add("I_K", new double[I.Length]);
            dyn.SecLists.Add("I_Na", new double[I.Length]);
            dyn.SecLists.Add("I_L", new double[I.Length]);
            return dyn;
        }
        public override void UpdateAdditionalDynamicStats(DynamicsStats dyn, int tIndex)
        {
            double[] nList = dyn.SecLists["n"];
            double[] mList = dyn.SecLists["m"];
            double[] hList = dyn.SecLists["h"];
            double[] I_KList = dyn.SecLists["I_K"];
            double[] I_NaList = dyn.SecLists["I_Na"];
            double[] I_LList = dyn.SecLists["I_L"];
            nList[tIndex] = n;
            mList[tIndex] = m;
            hList[tIndex] = h;
            I_KList[tIndex] = IK;
            I_NaList[tIndex] = INa;
            I_LList[tIndex] = IL;
        }

    }

}
