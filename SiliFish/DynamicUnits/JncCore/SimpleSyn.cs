using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class SimpleSyn: Synapse
    {
        private double ISynA = 0; //the momentary current value
        private double ISynB = 0; //the momentary current value
        [JsonIgnore]
        public override double ISyn { get { return ISynA - ISynB; } }


        public SimpleSyn()
        { }
        public SimpleSyn(SynapseParameters param, double conductance, double rundt, double eulerdt)
            :base(param, conductance, rundt, eulerdt)
        {
        }

        public SimpleSyn(SimpleSyn copyFrom)
            :base(copyFrom)
        {
        }
        public override void InitForSimulation(double conductance)
        {
            Conductance = conductance;
            ISynA = ISynB = 0;
        }

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            errors ??= new();
            return errors.Count == 0;
        }
        public override double GetNextVal(double vPreSynapse, double vPost, double _)
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
            return ISyn;
        }
    }

}
