using SiliFish.Services.Dynamics;

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
            return CalculateFitnessFor(spikeDelay);
        }
    }

}
