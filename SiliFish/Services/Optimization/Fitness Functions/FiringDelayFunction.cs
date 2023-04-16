using SiliFish.DynamicUnits;

namespace SiliFish.Services.Optimization
{
    public class FiringDelayFunction : FitnessFunction
    {
        public FiringDelayFunction()
        {
            MinMaxExists = true;
            CurrentRequired = true;
            ModeExists = false;
        }
        public override double CalculateFitness(DynamicsStats stat)
        {
            double spikeDelay = stat.SpikeDelay;
            if (spikeDelay < 0) return 0;
            if (ValueMin <= spikeDelay && ValueMax >= spikeDelay)
                return Weight;
            if (spikeDelay < ValueMin)
                return Weight / (ValueMin - spikeDelay);
            return Weight / (spikeDelay - ValueMax);
        }
    }

}
