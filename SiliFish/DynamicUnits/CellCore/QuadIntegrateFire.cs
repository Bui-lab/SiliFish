using SiliFish.Extensions;
using SiliFish.ModelUnits.Parameters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class QuadraticIntegrateAndFire : CellCoreUnit
    {
        protected override void Initialize()
        {
            V = Vr;
        }
        public QuadraticIntegrateAndFire()
        { }
        public QuadraticIntegrateAndFire(double Vr, double Vmax)
        {
            this.Vr = Vr;
            this.Vmax = Vmax;
            V = Vr;
        }
        public QuadraticIntegrateAndFire(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }

        public override double GetNextVal(double Stim, ref bool spike)
        {
            double dtTracker = 0;
            while (dtTracker < deltaT)
            {
                dtTracker += deltaTEuler;
                // ODE eqs
                if (V >= Vmax)
                {
                    spike = true;
                    V = Vr;
                    break;
                }
                else
                {
                    double dv = V * V + Stim;
                    double vNew = V + dv * deltaTEuler;
                    V = vNew;
                }
            }
            return V;
        }
        public override DynamicsStats SolveODE(double[] I)
        {
            int iMax = I.Length;
            DynamicsStats dyn = new(null, I, deltaT);
            bool spike = false;
            for (int t = 0; t < iMax; t++)
            {
                GetNextVal(I[t], ref spike);
                dyn.VList[t] = V;
            }
            return dyn;
        }
    }

}
