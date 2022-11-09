using System;
using System.Collections.Generic;

namespace SiliFish.DynamicUnits
{
    /// <summary>
    /// Model based on Neuronal Dynamics
    /// From Single Neurons to Networks and Models of Cognition
    /// Chapter 2
    /// </summary>
    public class HodgkinHuxley : HodgkinHuxleyClassic
    {
        /*protected override double alpha_n { get { return 0.01 * (V + 55) / (1 - Math.Exp((V + 55) / 10)); } }
        protected override double beta_n { get { return 0.125 * Math.Exp(- (V + 65) / 80); } }
        protected override double alpha_m { get { return 0.1 * (V + 40) / (1 - Math.Exp((V + 40) / 10)); } }
        protected override double beta_m { get { return 4 * Math.Exp(- (V + 65)/ 18); } }
        protected override double alpha_h { get { return 0.07 * Math.Exp(- (V + 65) / 20); } }
        protected override double beta_h { get { return 1 / (Math.Exp((V + 35) / 10) + 1); } }
        */
        protected override double alpha_n { get { return 0.02 * (V - 25) / (1 - Math.Exp(-1 * (V - 25) / 9)); } }
        protected override double beta_n { get { return -0.002 * (V - 25) / (1 - Math.Exp((V - 25) / 9)); } }
        protected override double alpha_m { get { return 0.182 * (V + 35) / (1 - Math.Exp(-1 * (V + 35) / 9)); } }
        protected override double beta_m { get { return -0.124 * (V + 35) / (1 - Math.Exp((V + 35) / 9)); } }
        protected override double alpha_h { get { return 0.25 * Math.Exp(-(V + 90) / 12); } }
        protected override double beta_h { get { return 0.25 * Math.Exp((V + 62) / 6) / (Math.Exp((V + 90) / 12)); } }


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

            V = Vr = -65;
        }

    }

}
