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
    public class TwoExpSyn: SynapseCore
    {
        private double iSyn = 0; //the momentary current value
        public double SlowComponent { get; set; }
        public double TauDFast { get; set; }
        public double TauDSlow { get; set; }
        public double TauR { get; set; }
        public double Vth { get; set; }
        public double ERev { get; set; }
        public override double ISyn => iSyn;
        public TwoExpSyn()
        { }
        public TwoExpSyn(Dictionary<string, double> paramExternal, double conductance, double rundt, double eulerdt)
            : base(conductance, rundt, eulerdt)
        {
            SetParameters(paramExternal);
        }

        public TwoExpSyn(TwoExpSyn copyFrom)
            : base(copyFrom)
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
            if (SlowComponent < 0 || SlowComponent > 1)
                errors.Add($"Chemical synapse: slow component valid range is [0-1].");
            if (TauDFast * (1 - SlowComponent) + TauDSlow * SlowComponent < GlobalSettings.Epsilon || TauR < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Tau has 0 value.");
            return errors.Count == 0;
        }
        public override double GetNextVal(double vPreSynapse, double vPost, double t_t0)
        {
            double g_t = 0;
            
            if (vPreSynapse > Vth)//pre-synaptic neuron spikes
            {
                double rise = 1 - Math.Exp(-t_t0 / TauR);
                double fast_decay = (1-SlowComponent)* Math.Exp(-t_t0 / TauDFast);
                double slow_decay = (SlowComponent) * Math.Exp(-t_t0 / TauDSlow);
                g_t = Conductance * rise * (slow_decay + fast_decay);
            }
            iSyn = g_t * (ERev - vPost);
            return iSyn;
        }

    }

}
