using SiliFish.DynamicUnits;
using System.Linq;

namespace SiliFish.Services.Optimization
{
    public class SpikeFrequencyFunction : FitnessFunction
    {
        public SpikeFrequencyFunction()
        {
            MinMaxExists = true;
            CurrentRequired = true;
            ModeExists = false;
        }
        public override double CalculateFitness(DynamicsStats stat)
        {
            if (stat.BurstsOrSpikes.Count == 0)
                return 0;
            int spikeCount = stat.BurstsOrSpikes.Sum(bs => bs.SpikeCount);
            double timeRange = stat.CurrentEndTime - stat.CurrentStartTime;
            double freq = 1000 * spikeCount / timeRange;
            return CalculateFitnessFor(freq);
        }

    }

}
