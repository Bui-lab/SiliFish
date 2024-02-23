using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SiliFish.DynamicUnits
{
    /// <summary>
    /// Principles of Computational Modelling
    /// in Neuroscience
    /// Chapter 8 - Simplified Models of Neurons
    /// https://doi.org/10.1017/CBO9780511975899.009 
    /// </summary>
    public class QuadraticIntegrateAndFire : CellCore
    {
        [Description("Membrane resistance")]
        public double Rm { get; set; } = 10;

        [Description("Membrane capacitance")]
        public double Cm { get; set; } = 20;

        [Description("Threshold membrane potential.")]
        public double Vt { get; set; } = -57;
        private double R;
        protected override void Initialize()
        {
            V = Vr;
            R = Rm * (Vt - Vr);
            if (Math.Abs(R) < double.Epsilon)
                R = double.Epsilon;
        }
        public QuadraticIntegrateAndFire()
        {
            Initialize();
        }

        public QuadraticIntegrateAndFire(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }

        public override double GetNextVal(double Stim, ref bool spike)
        {
            // ODE eqs
            if (V >= Vt && V < Vmax)
            {
                spike = true;
                V = Vmax;
            }
            else if (V >= Vmax)
            {
                spike = true;
                V = Vr;
            }
            else
            {
                double Cdv = -(V - Vr) * (Vt - V) / R + Stim;
                double vNew = V + Cdv * deltaT / Cm;
                V = vNew;
            }

            return V;
        }
    }

}
