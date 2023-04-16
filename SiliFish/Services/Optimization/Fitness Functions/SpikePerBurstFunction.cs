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
            if (ValueMin <= avgSpikePerBurst && ValueMax >= avgSpikePerBurst)
                return Weight;
            if (avgSpikePerBurst < ValueMin)
                return Weight / (ValueMin - avgSpikePerBurst);
            return Weight / (avgSpikePerBurst - ValueMax);
        }

    }

}
