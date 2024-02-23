using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text.Json.Serialization;

//Modified from the code written by Yann Roussel and Tuan Bui

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

        [JsonIgnore, Browsable(false)]
        public override string Identifier => $"Conductance: {Conductance:0.####} τ(r/d): {TauR}/{TauD}";

        public SimpleSyn()
        { }

        public SimpleSyn(Dictionary<string, double> paramExternal)
            :base()
        {
            SetParameters(paramExternal);
        }

        public SimpleSyn(SimpleSyn copyFrom)
            :base(copyFrom)
        {
        }
        public override void InitForSimulation(double deltaT, ref int uniqueID)
        {
            base.InitForSimulation(deltaT, ref uniqueID);
            ISynA = ISynB = 0;
        }

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            errors ??= [];
            if (TauD < GlobalSettings.Epsilon || TauR < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Tau has 0 value.");
            return errors.Count == 0;
        }
        public override double GetNextVal(double vPreSynapse, double vPost, List<double> _, double tCurrent, DynamicsParam settings, bool excitatory)
        {
            if (vPreSynapse > Vth)//pre-synaptic neuron spikes
            {
                // mEPSC
                ISynA += (ERev - vPost) * Conductance;
                ISynB += (ERev - vPost) * Conductance;
                double dIsynA = -1 / TauD * ISynA;
                double dIsynB = -1 / TauR * ISynB;
                ISynA += DeltaT * dIsynA;
                ISynB += DeltaT * dIsynB;
            }
            else
            {
                // no synaptic event
                double dIsynA = -1 / TauD * ISynA;
                double dIsynB = -1 / TauR * ISynB;
                ISynA += DeltaT * dIsynA;
                ISynB += DeltaT * dIsynB;
            }

            return ISynA - ISynB;
        }
        public override bool CausesReverseCurrent(double V, bool excitatory)
        {
            return excitatory && V > ERev || !excitatory && V < ERev;
        }

    }

}
