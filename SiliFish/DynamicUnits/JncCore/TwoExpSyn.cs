using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class TwoExpSyn : ChemSynapseCore
    {
        private double iSyn = 0; //the momentary current value
        private double tLastSignificantSpike = -1;
        public double SlowComponent { get; set; }
        public double TauDFast { get; set; }
        public double TauDSlow { get; set; }
        public double TauR { get; set; }

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
        public TwoExpSyn(Dictionary<string, double> paramExternal)
            : base()
        {
            SetParameters(paramExternal);
        }

        public TwoExpSyn(TwoExpSyn copyFrom)
            : base(copyFrom)
        {
        }
        public override void InitForSimulation(double deltaT, ref int uniqueID)
        {
            base.InitForSimulation(deltaT, ref uniqueID);
            iSyn = 0;
            tLastSignificantSpike = -1;
        }

        public override bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            errors ??= [];
            warnings ??= [];
            int preCount = errors.Count + warnings.Count;
            base.CheckValues(ref errors, ref warnings);
            if (SlowComponent < 0 || SlowComponent > 1)
                errors.Add($"Chemical synapse: slow component valid range is [0-1].");
            if (TauDFast * (1 - SlowComponent) + TauDSlow * SlowComponent < GlobalSettings.Epsilon || TauR < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Tau has 0 value.");
            return errors.Count + warnings.Count == preCount;
        }
        public override double GetNextVal(double _, double vPost, List<double> spikeArrivalTimes, double tCurrent, DynamicsParam settings, bool excitatory)
        {
            if (!settings.AllowReverseCurrent && (excitatory && vPost > ERev || !excitatory && vPost < ERev))
            {
                //iSyn = 0;
                return iSyn;
            }

            double g_t = 0;

            double threshold = Math.Max(tLastSignificantSpike, tCurrent - settings.ThresholdMultiplier * (TauR + TauDFast + TauDSlow));
            List<double> closeBySpikes = spikeArrivalTimes.Where(t => t > threshold && t < tCurrent).ToList();
            if (settings.SpikeTrainSpikeCount > 0)
                closeBySpikes = closeBySpikes.TakeLast(settings.SpikeTrainSpikeCount).ToList();

            foreach (var ti in closeBySpikes)
            {
                double t_t0 = tCurrent - ti;
                double rise = 1 - Math.Exp(-t_t0 / TauR);
                double fast_decay = (1 - SlowComponent) * Math.Exp(-t_t0 / TauDFast);
                double slow_decay = SlowComponent * Math.Exp(-t_t0 / TauDSlow);
                double g_partial = Conductance * rise * (slow_decay + fast_decay);
                if (g_partial < GlobalSettings.Epsilon)
                    tLastSignificantSpike = ti; //if the conductance becomes very small, no need to use in future calculations
                g_t += g_partial;
            }
            iSyn = g_t * (ERev - vPost);
            return iSyn;
        }
        public override bool CausesReverseCurrent(double V, bool excitatory)
        {
            return excitatory && V > ERev || !excitatory && V < ERev;
        }

    }

}
