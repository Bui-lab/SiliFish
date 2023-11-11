using SiliFish.DynamicUnits.Firing;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Parameters;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class LeakyIntegrateAndFire : Leaky_Integrator
    {
        //private static double g_leak_suggestedMin = ;
        //private static double g_leak_suggestedMax = ;
        //private static double E_leak_suggestedMin = ;
        //private static double E_leak_suggestedMax = ;
        //private static double Cm_suggestedMin = ;
        //private static double Cm_suggestedMax = ;

        [Description("The threshold membrane potential for a spike.")]
        public double Vt { get; set; } = -57;
        [Description("Reset membrane potential after a spike.")]
        public double Vreset { get; set; } = -50;

        [JsonIgnore, Browsable(false)]
        public override double Vthreshold { get => Vt; set => Vt = value; }
        protected override void Initialize()
        {
            V = Vr;
        }
        public LeakyIntegrateAndFire()
        { }
        public LeakyIntegrateAndFire(Dictionary<string, double> paramExternal)
        {
            SetParameters(paramExternal);
            Initialize();
        }

        public override (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Dictionary<string, double> MinValues = new()
            {
            };
            Dictionary<string, double> MaxValues = new()
            {
            };

            return (MinValues, MaxValues);
        }

        public override double GetNextVal(double I, ref bool spike)
        {
            double vNew;
            spike = false;

            if (V >= Vthreshold && V < Vmax)
            {
                spike = true;
                V = Vmax;
            }
            else if (V >= Vmax)
            {
                vNew = Vreset;
                V = vNew;
            }
            else
            {
                // Cdv refers to Capacitance * dV/dt as in Izhikevich model
                // (Dynamical Systems in Neuroscience: page 268, Eq 8.1)
                double Cdv = I - (V - Vr) / R;
                vNew = V + Cdv * deltaT / C;
                V = vNew;
            }

            return V;
        }

        public override DynamicsStats SolveODE(double[] I)
        {
            Initialize();
            bool onRise = false, tauRiseSet = false, onDecay = false, tauDecaySet = false;
            double decayStart = 0, riseStart = 0;
            int iMax = I.Length;
            DynamicsStats dyn = new(null, I, deltaT);
            dyn.SecLists.Add("Rel. Tension", new double[I.Length]);
            double[] tensionList = dyn.SecLists["Rel. Tension"];
            
            bool spike = false;
            for (int tIndex = 0; tIndex < iMax; tIndex++)
            {
                GetNextVal(I[tIndex], ref spike);
                dyn.VList[tIndex] = V;
                tensionList[tIndex] = CalculateRelativeTension(V);
                //if passed the 0.37 of the drop (the difference between Vmax and Vreset): 
                //V <= Vmax - 0.37 * (Vmax - Vreset) => V <= 0.63 Vmax - 0.37 Vreset
                if (onDecay && !tauDecaySet && V <= 0.63 * Vmax - 0.37 * Vreset)
                {
                    dyn.TauDecay.Add(deltaT * tIndex, deltaT * (tIndex - decayStart));
                    tauDecaySet = true;
                }
                //if passed the 0.63 of the rise (the difference between between Vmax and Vr): 
                //V >= 0.63 * (Vmax - Vr) + Vr => V >= 0.63 Vmax + 0.37 Vr
                else if (onRise && !tauRiseSet && riseStart > 0 && V >= 0.63 * Vmax + 0.37 * Vr)
                {
                    dyn.TauRise.Add(deltaT * tIndex, deltaT * (tIndex - riseStart));
                    tauRiseSet = true;
                    riseStart = 0;
                }
                else if (!onRise && (V - Vr > 0))//Vr is used instead of Vt
                {
                    onRise = true;
                    tauRiseSet = false;
                    riseStart = tIndex;
                }
                else if (onDecay && tIndex > 0 && V > dyn.VList[tIndex - 1])
                {
                    onDecay = false;
                    tauDecaySet = false;
                }
                if (spike)
                {
                    if (tIndex > 0)
                        dyn.SpikeList.Add(tIndex - 1);
                    onRise = false;
                    tauRiseSet = false;
                    onDecay = true;
                    tauDecaySet = false;
                    decayStart = tIndex;
                }
            }
            dyn.DefineSpikingPattern();

            return dyn;
        }

    }
}
