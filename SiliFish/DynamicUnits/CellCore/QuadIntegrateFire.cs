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

        [JsonIgnore]
        public override string VThresholdParamName { get { return ""; } }
        [JsonIgnore]
        public override string VReversalParamName { get { return "QIF.Vr"; } }

        public override Dictionary<string, double> GetParameters()
        {
            Dictionary<string, double> paramDict = new()
            {
                { "QIF.Vr", Vr },
                { "QIF.Vmax", Vmax }
            };
            return paramDict;
        }

        public override void BackwardCompatibility (Dictionary<string, double> paramExternal)
        {
            paramExternal.AddObject("QIF.Vr", Vr, skipIfExists: true);
            paramExternal.AddObject("QIF.Vmax", Vmax, skipIfExists: true);
        }
        public override void SetParameters(Dictionary<string, double> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            BackwardCompatibility(paramExternal);

            paramExternal.TryGetValue("QIF.Vr", out Vr);
            paramExternal.TryGetValue("QIF.Vmax", out Vmax);
            Initialize();
        }


        public override double GetNextVal(double Stim, ref bool spike)
        {
            double dtTracker = 0;
            while (dtTracker < dt_run)
            {
                dtTracker += dt_euler;
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
                    double vNew = V + dv * dt_euler;
                    V = vNew;
                }
            }
            return V;
        }
        public override DynamicsStats SolveODE(double[] I)
        {
            int iMax = I.Length;
            DynamicsStats dyn = new(I, dt_run);
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
