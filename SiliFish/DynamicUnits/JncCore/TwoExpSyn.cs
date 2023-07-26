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
    public class TwoExpSyn: Synapse
    {
        private double iSyn = 0; //the momentary current value
        private double TauDecayFast;
        private double FastWeight;
        public override double ISyn => iSyn;
        public TwoExpSyn()
        { }
        public TwoExpSyn(SynapseParameters param, double conductance, double rundt, double eulerdt)
            :base(param, conductance, rundt, eulerdt)
        {
        }

        public TwoExpSyn(TwoExpSyn copyFrom)
            :base(copyFrom)
        {
        }
        public override void InitForSimulation(double conductance)
        {
            Conductance = conductance;
            iSyn = 0;
        }

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            errors ??= new();
            return errors.Count == 0;
        }
        public override double GetNextVal(double vPreSynapse, double vPost, double t_t0)
        {
            double g_t = 0;
            
            if (vPreSynapse > Vth)//pre-synaptic neuron spikes
            {
                double rise = 1 - Math.Exp(-t_t0 / TauR);
                double fast_decay = FastWeight * Math.Exp(-t_t0 / TauDecayFast);
                double slow_decay = (1 - FastWeight) * Math.Exp(-t_t0 / TauD);
                g_t = Conductance * rise * (slow_decay + fast_decay);
            }
            iSyn = g_t * (ERev - vPost);
            return iSyn;
        }
    }

}
