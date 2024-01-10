 using SiliFish.DynamicUnits.Firing;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

//Modified from the code written by Yann Roussel and Tuan Bui

namespace SiliFish.DynamicUnits
{
    public class Izhikevich_9P : CellCore
    {
        private static readonly double a_suggestedMin = 0.01;
        private static readonly double a_suggestedMax = 10;
        private static readonly double b_suggestedMin = 0.01;
        private static readonly double b_suggestedMax = 20;
        private static readonly double d_suggestedMin = -10.01;
        private static readonly double d_suggestedMax = 10;
        private static readonly double k_suggestedMin = 0.01;
        private static readonly double k_suggestedMax = 10;
        private static readonly double Cm_suggestedMin = 0.01;
        private static readonly double Cm_suggestedMax = 20;

        //a, b, c, d, are the parameters for the membrane potential dynamics
        //Default values are taken from Izhikevich 2003 (IEEE)

        [Description("The time scale of the recovery variable, u.")]
        public double a { get; set; } = 0.021;
        [Description("The sensitivity of the recovery variable, u.")]
        public double b { get; set; } = 0.201;
        [Description("The membrane potential V resets to value c (in mV) after a spike.")]
        public double c { get; set; } = -65.001;
        [Description("The recovery variable u is incremented by the value d after a spike.")]
        public double d { get; set; } = 2.001;

        [Description("The threshold membrane potential for a spike.")]
        public double Vt { get; set; } = -57.001;
        // k is a coefficient of the quadratic polynomial 
        [Description("An approximation of the subthreshold region of the fast component of the I-V relationship of the neuron.")]
        public double k { get; set; } = 1.001;
        [Description("The membrane capacitance.")]
        public double Cm { get; set; } = 10.001; //the membrane capacitance

        [JsonIgnore]
        double u = 0;//Keeps the current value of u

        public Izhikevich_9P()
        {
        }

        public Izhikevich_9P(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }
        protected override void Initialize()
        {
            V = Vr;
            u = 0; // b * Vr;
        }

        [JsonIgnore, Browsable(false)]
        public override double Vthreshold { get => Vt; set => Vt = value; }
        [JsonIgnore, Browsable(false)]
        public override double Vreset { get => c; set => c = value; }

        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Dictionary<string, double> MinValues = new() {
                { "c", c },
                { "Vmax", Vmax },
                { "Vr", Vr },
                { "Vt", Vt },
                { "a", a_suggestedMin },
                { "b", b_suggestedMin },
                { "d", d_suggestedMin },
                { "k", k_suggestedMin },
                { "Cm", Cm_suggestedMin }
            };
            Dictionary<string, double> MaxValues = new() {
                { "c", c },
                { "Vmax", Vmax },
                { "Vr", Vr },
                { "Vt", Vt },
                { "a", a_suggestedMax },
                { "b", b_suggestedMax },
                { "d", d_suggestedMax },
                { "k", k_suggestedMax },
                { "Cm", Cm_suggestedMax }
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
                double Cdv = k * (V - Vr) * (V - Vt) - u + I;
                vNew = V + Cdv * deltaT / Cm;
                double du = a * (b * (V - Vr) - u);
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

        public override DynamicsStats CreateDynamicsStats(double[] I)
        {
            DynamicsStats dyn = new(null, I, deltaT);
            dyn.SecLists.Add("u", new double[I.Length]);
            return dyn;
        }
        public override void UpdateDynamicStats(DynamicsStats dyn, int tIndex)
        {
            double[] feedbackCurrent = dyn.SecLists["u"];
            feedbackCurrent[tIndex] = u;
        }

    }
}
