using System;
using System.Collections.Generic;

namespace SiliFish.DynamicUnits
{
    /// <summary>
    /// Model based on Neuronal Dynamics
    /// From Single Neurons to Networks and Models of Cognition
    /// Chapter 2
    /// https://www.cambridge.org/core/books/abs/neuronal-dynamics/preface/5E2EF649D53AD9A59C388F14471559B4
    /// https://neuronaldynamics.epfl.ch/online/errata.html
    /// </summary>
    public class HodgkinHuxley : HodgkinHuxleyClassic
    {
        private static double g_Na_suggestedMinHH = 30;
        private static double g_Na_suggestedMaxHH = 50;
        private static double E_K_suggestedMinHH = -80;
        private static double E_K_suggestedMaxHH = -70;
        private static double E_Na_suggestedMinHH = 50;
        private static double E_Na_suggestedMaxHH = 60;
        private static double E_L_suggestedMinHH = -70;
        private static double E_L_suggestedMaxHH = -60;

        protected override double alpha_n { get { return 0.02 * (V - 25) / (1 - Math.Exp(-1 * (V - 25) / 9)); } }
        protected override double beta_n { get { return -0.002 * (V - 25) / (1 - Math.Exp((V - 25) / 9)); } }
        protected override double alpha_m { get { return 0.182 * (V + 35) / (1 - Math.Exp(-1 * (V + 35) / 9)); } }
        protected override double beta_m { get { return -0.124 * (V + 35) / (1 - Math.Exp((V + 35) / 9)); } }
        protected override double alpha_h { get { return 0.25 * Math.Exp(-(V + 90) / 12); } }
        protected override double beta_h { get { return 0.25 * Math.Exp((V + 62) / 6) / (Math.Exp((V + 90) / 12)); } }


        public HodgkinHuxley()
        {
        }
        public HodgkinHuxley(Dictionary<string, double> paramExternal)
            : base(paramExternal)
        {
        }
        protected override void Initialize()
        {
            g_K = 35;
            g_Na = 40;
            g_L = 0.3;
            E_K = -77;
            E_Na = 55;
            E_L = -65;
            // threshold membrane potential 
            Vt = -57;
            Cm = 1; //the membrane capacitance
            Vr = -65;
            base.Initialize();
        }
        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) = base.GetSuggestedMinMaxValues();
            MinValues["g_Na"] = g_Na_suggestedMinHH;
            MaxValues["g_Na"] = g_Na_suggestedMaxHH;
            MinValues["E_K"] = E_K_suggestedMinHH;
            MaxValues["E_K"] = E_K_suggestedMaxHH;
            MinValues["E_Na"] = E_Na_suggestedMinHH;
            MaxValues["E_Na"] = E_Na_suggestedMaxHH;
            MinValues["E_L"] = E_L_suggestedMinHH;
            MaxValues["E_L"] = E_L_suggestedMaxHH;
            return (MinValues, MaxValues);
        }

    }

}
