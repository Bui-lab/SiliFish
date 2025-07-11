using SiliFish.Definitions;
using SiliFish.Services.Dynamics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

//Modified from the code written by Yann Roussel and Tuan Bui

namespace SiliFish.DynamicUnits
{
    public class Izhikevich_5P : CellCore
    {
        private static double a_suggestedMin = 0.01;
        private static double a_suggestedMax = 1;
        private static double b_suggestedMin = -1.01;
        private static double b_suggestedMax = 1.01;
        private static double d_suggestedMin = 0;
        private static double d_suggestedMax = 10.01;

        //Default values are taken from Izhikevich 2003 (IEEE)
        [Description("The time scale of the recovery variable, u.")]
        public double a { get; set; } = 0.02;
        [Description("The sensitivity of the recovery variable, u.")]
        public double b { get; set; } = 0.2;
        [Description("The membrane potential V resets to value c (in mV) after a spike.")]
        public double c { get; set; } = -65;
        [Description("The recovery variable u is incremented by the value d after a spike.")]
        public double d { get; set; } = 2;

        [JsonIgnore]
        double u = 0;//Keeps the current value of u

        [JsonIgnore, Browsable(false)]
        public override double Vreset { get => c; set => c = value; }

        [JsonIgnore, Browsable(false)]
        public override double Vthreshold { get => (c + Vmax) / 2; }
        protected override void Initialize()
        {
            V = Vr;
            u = b * V;
        }

        public Izhikevich_5P()
        {
        }
        public Izhikevich_5P(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }

        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Dictionary<string, double> MinValues = new() {
                { "c", c },
                { "Vmax", Vmax },
                { "Vr", Vr },
                { "a", a_suggestedMin },
                { "b", b_suggestedMin },
                { "d", d_suggestedMin }
            };
            Dictionary<string, double> MaxValues = new() {
                { "c", c },
                { "Vmax", Vmax },
                { "Vr", Vr },
                { "a", a_suggestedMax },
                { "b", b_suggestedMax },
                { "d", d_suggestedMax }
            };

            return (MinValues, MaxValues);
        }

        public override double GetNextVal(double I, ref bool spike)
        {
            double vNew, uNew;
            spike = false;
            if (V < Vmax)
            {
                // ODE eqs
                // Cdv refers to Capacitance * dV/dt as in Izhikevich model (Dynamical Systems in Neuroscience: page 273, Eq 8.5)
                double Cdv = 0.04 * V * V + 5 * V + 140 - u + I;
                vNew = V + Cdv * deltaT;// / Cm;
                double du = a * (b * V - u);
                uNew = u + deltaT * du;
                V = vNew;
                u = uNew;
            }
            else
            {
                // Spike
                spike = true;
                vNew = c;
                uNew = u + d;
                V = vNew;
                u = uNew;
            }
            return V;
        }
        public override DynamicsStats CreateDynamicsStats(DynamicsParam dynamicsParam, double[] I)
        {
            DynamicsStats dyn = new(dynamicsParam, I, deltaT);
            dyn.SecLists.Add("u", new double[I.Length]);
            return dyn;
        }
        public override void UpdateAdditionalDynamicStats(DynamicsStats dyn, int tIndex)
        {
            double[] feedbackCurrent = dyn.SecLists["u"];
            feedbackCurrent[tIndex] = u;
        }

    }
}
