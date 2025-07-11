using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class SingleExpSyn : ChemSynapseCore
    {
        private double iSyn = 0; //the momentary current value

        private double tLastSignificantSpike = -1;
        [JsonIgnore, Browsable(false)]
        public override double ISyn => iSyn;
        public override void ZeroISyn()
        {
            iSyn = 0;
        }
        public double TauD { get; set; }
        public double TauR { get; set; }

        [JsonIgnore, Browsable(false)]
        public override string Identifier => $"Conductance: {Conductance:0.####} τ(r/d): {TauR}/{TauD}";

        public SingleExpSyn()
        { }

        public SingleExpSyn(Dictionary<string, double> paramExternal)
            : base()
        {
            SetParameters(paramExternal);
        }

        public SingleExpSyn(SimpleSyn copyFrom)
            : base(copyFrom)
        {
        }
        public override void InitForSimulation(double deltaT, ref int uniqueID)
        {
            base.InitForSimulation(deltaT, ref uniqueID);
            iSyn = 0;
            tLastSignificantSpike = -1;
            if (TauD == TauR)
                TauD += GlobalSettings.Epsilon;
        }

        public override bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            errors ??= [];
            warnings ??= [];
            int preCount = errors.Count + warnings.Count;
            base.CheckValues(ref errors, ref warnings);
            if (TauD < GlobalSettings.Epsilon || TauR < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Tau has 0 value.");
            if (TauD == TauR)
                warnings.Add($"Chemical synapse: Tau decay is equal to the tau rise. " +
                    $"Due to mathematical modelling of the SingleExpSynapse, " +
                    $"tau decay will be increased by {GlobalSettings.Epsilon} during simulation.");
            return errors.Count + warnings.Count == preCount;
        }
        public override double GetNextVal(double vPreSynapse, double vPost, List<double> spikeArrivalTimes, double tCurrent, DynamicsParam settings, bool excitatory)
        {
            double g_t = 0;

            double threshold = Math.Max(tLastSignificantSpike, tCurrent - settings.ThresholdMultiplier * (TauR + TauD));
            List<double> closeBySpikes = spikeArrivalTimes.Where(t => t >= threshold && t < tCurrent).ToList();
            if (settings.SpikeTrainSpikeCount > 0)
                closeBySpikes = closeBySpikes.TakeLast(settings.SpikeTrainSpikeCount).ToList();

            foreach (var ti in closeBySpikes)
            {
                double t_t0 = tCurrent - ti;
                double rise = Math.Exp(-t_t0 / TauR);
                double decay = Math.Exp(-t_t0 / TauD);
                double mult = TauR * TauR / (TauR - TauD);
                double g_partial = Conductance * mult * (rise - decay);
                if (Math.Abs(g_partial) < GlobalSettings.Epsilon)
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
