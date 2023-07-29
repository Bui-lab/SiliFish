using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class TwoExpSyn: ChemSynapseCore
    {
        private double iSyn = 0; //the momentary current value
        private double tLastSignificantSpike = -1;
        public double SlowComponent { get; set; }
        public double TauDFast { get; set; }
        public double TauDSlow { get; set; }
        public double TauR { get; set; }
        public double Vth { get; set; }
        public double ERev { get; set; }
        
        [JsonIgnore, Browsable(false)]
        public override double ISyn => iSyn;

        [JsonIgnore, Browsable(false)]
        public override string Identifier => $"Conductance: {Conductance:0.####} τ(r/slow/fast): {TauR}/{TauDSlow}/{TauDFast}";

        public override void ZeroISyn()
        {
            iSyn = 0;
        }
        public TwoExpSyn()
        { }
        public TwoExpSyn(Dictionary<string, double> paramExternal, double rundt, double eulerdt)
            : base( rundt, eulerdt)
        {
            SetParameters(paramExternal);
        }

        public TwoExpSyn(TwoExpSyn copyFrom)
            : base(copyFrom)
        {
        }
        public override void InitForSimulation()
        {
            iSyn = 0;
            tLastSignificantSpike = -1;
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
        public override double GetNextVal(double _, double vPost, List<double> spikeArrivalTimes, double tCurrent)
        {
            double g_t = 0;

            double threshold = Math.Max(tLastSignificantSpike, tCurrent - 3 * Math.Max(10 * TauDFast, TauDSlow));
            List<double> closeBySpikes = spikeArrivalTimes.Where(t => t > threshold && t < tCurrent).ToList();
            foreach (var ti in closeBySpikes.TakeLast(10))
            {
                double t_t0 = tCurrent - ti;
                double rise = 1 - Math.Exp(-t_t0 / TauR);
                double fast_decay = (1 - SlowComponent) * Math.Exp(-t_t0 / TauDFast);
                double slow_decay = (SlowComponent) * Math.Exp(-t_t0 / TauDSlow);
                double g_partial = Conductance * rise * (slow_decay + fast_decay);
                if (g_partial < GlobalSettings.Epsilon)
                    tLastSignificantSpike = ti; //if the conductance becomes very small, no need to use in future calculations
                g_t += g_partial;
            }
            iSyn = g_t * (ERev - vPost);
            return iSyn;
        }

    }

}
