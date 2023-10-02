using SiliFish.DynamicUnits.Firing;
using System.Linq;

namespace SiliFish.Services.Optimization
{
    public class BurstFrequencyFunction : FitnessFunction
    {
        public BurstFrequencyFunction()
        {
            MinMaxExists = true;
            CurrentRequired = true;
            ModeExists = false;
        }
        public override double CalculateFitness(DynamicsStats stat)
        {
            if (stat.BurstsOrSpikes.Count == 0)
                return 0;
            int burstCount = stat.BurstsOrSpikes.Count(bs => bs.IsBurst);
            double timeRange = stat.CurrentEndTime - stat.CurrentStartTime;
            double freq = 1000 * burstCount / timeRange;
            return CalculateFitnessFor(freq);
        }

    }
}
