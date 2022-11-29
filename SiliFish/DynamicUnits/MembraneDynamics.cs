using SiliFish.Extensions;
using System;

namespace SiliFish.DynamicUnits
{
    public class MembraneDynamics
    {
        public double a, b, c, d;

        // vmax is the peak membrane potential of single action potentials
        public double Vmax;
        // vr, vt are the resting and threshold membrane potential 
        public double Vr, Vt;
        // k is a coefficient of the quadratic polynomial 
        public double k;
        public double Cm; //the membrane capacitance
        public MembraneDynamics RandomizedDynamics(double sigma)
        {
            if (sigma < 0.00000001) return this;
            Random rand = SwimmingModel.rand;
            MembraneDynamics dyn = new()
            {
                a = a * rand.Gauss(1, sigma),
                b = b * rand.Gauss(1, sigma),
                c = c * rand.Gauss(1, sigma),
                d = d * rand.Gauss(1, sigma),
                Vmax = Vmax * rand.Gauss(1, sigma),
                Vr = Vr * rand.Gauss(1, sigma),
                Vt = Vt * rand.Gauss(1, sigma),
                Cm = Cm * rand.Gauss(1, sigma)
            };
            return dyn;
        }
    }
}
