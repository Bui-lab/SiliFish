﻿using SiliFish.Extensions;
using System.Collections.Generic;

namespace SiliFish.DynamicUnits
{
    public class QuadraticIntegrateAndFire : DynamicUnit
    {
        protected override void Initialize()
        {
            V = Vr;
        }
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
        public override Dictionary<string, double> GetParameters()
        {
            Dictionary<string, double> paramDict = new()
            {
                { "QIF.Vr", Vr },
                { "QIF.Vmax", Vmax }
            };
            return paramDict;
        }

        public virtual void FillMissingParameters(Dictionary<string, double> paramExternal)
        {
            paramExternal.AddObject("QIF.Vr", Vr, skipIfExists: true);
            paramExternal.AddObject("QIF.Vmax", Vmax, skipIfExists: true);
        }
        public override void SetParameters(Dictionary<string, double> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            FillMissingParameters(paramExternal);

            paramExternal.TryGetValue("QIF.Vr", out Vr);
            paramExternal.TryGetValue("QIF.Vmax", out Vmax);
            Initialize();
        }


        public override double GetNextVal(double Stim, ref bool spike)
        {
            // ODE eqs
            double dv = V * V + Stim;
            double vNew = V + dv * RunParam.static_dt;
            V = vNew;
            if (V >= Vmax)
            {
                spike = true;
                V = Vr;
            }
            return V;
        }
        public override DynamicsStats SolveODE(double[] I)
        {
            int iMax = I.Length;
            DynamicsStats dyn = new(I);
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
