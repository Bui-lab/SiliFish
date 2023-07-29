using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class SimpleSyn: ChemSynapseCore
    {
        private double ISynA = 0; //the momentary current value
        private double ISynB = 0; //the momentary current value
        [JsonIgnore, Browsable(false)]
        public override double ISyn { get { return ISynA - ISynB; } }
        public override void ZeroISyn()
        {
            ISynA = ISynB = 0;
        }
        public double TauD { get; set; }
        public double TauR { get; set; }
        public double Vth { get; set; }
        public double ERev { get; set; }

        [JsonIgnore, Browsable(false)]
        public override string Identifier => $"Conductance: {Conductance:0.####} τ(r/d): {TauR}/{TauD}";

        public SimpleSyn()
        { }

        public SimpleSyn(Dictionary<string, double> paramExternal, double rundt, double eulerdt)
            :base(rundt, eulerdt)
        {
            SetParameters(paramExternal);
        }

        public SimpleSyn(SimpleSyn copyFrom)
            :base(copyFrom)
        {
        }
        public override void InitForSimulation()
        {
            ISynA = ISynB = 0;
        }

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            errors ??= new();
            if (TauD < GlobalSettings.Epsilon || TauR < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Tau has 0 value.");
            return errors.Count == 0;
        }
        public override double GetNextVal(double vPreSynapse, double vPost, List<double> _, double tCurrent)
        {
            double IsynANew = ISynA, IsynBNew = ISynB;
            double dtTracker = 0;
            while (dtTracker < DeltaT)
            {
                dtTracker += DeltaTEuler;
                if (vPreSynapse > Vth)//pre-synaptic neuron spikes
                {
                    // mEPSC
                    ISynA += (ERev - vPost) * Conductance;
                    ISynB += (ERev - vPost) * Conductance;
                    double dIsynA = -1 / TauD * ISynA;
                    double dIsynB = -1 / TauR * ISynB;
                    IsynANew = ISynA + DeltaTEuler * dIsynA;
                    IsynBNew = ISynB + DeltaTEuler * dIsynB;
                    break;
                }
                else
                {
                    // no synaptic event
                    double dIsynA = -1 / TauD * ISynA;
                    double dIsynB = -1 / TauR * ISynB;
                    IsynANew = ISynA + DeltaTEuler * dIsynA;
                    IsynBNew = ISynB + DeltaTEuler * dIsynB;
                }
            }
            ISynA = IsynANew;
            ISynB = IsynBNew;
            return ISynA - ISynB;
        }
    }

}
