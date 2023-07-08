using SiliFish.DynamicUnits;
using System.Linq;

namespace SiliFish.Services.Optimization
{
    public class SpikePerBurstFunction : FitnessFunction
    {
        public SpikePerBurstFunction()
        {
            MinMaxExists = true;
            CurrentRequired = true;
            ModeExists = false;
        }
        public override double CalculateFitness(DynamicsStats stat)
        {
            if (stat.BurstsOrSpikes.Count == 0)
                return 0;
            double avgSpikePerBurst = stat.BurstsOrSpikes.Average(bs => bs.SpikeCount);
            return CalculateFitnessFor(avgSpikePerBurst);
        }

    }

}
