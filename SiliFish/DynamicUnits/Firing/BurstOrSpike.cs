using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits
{
    public class BurstOrSpike
    {
        public static double MinBurstSpikeCount { get; set; } = 2; //the number of minimum spikes required
        public List<double> SpikeTimeList = new();
        public bool IsSpike { get { return SpikeTimeList?.Count == 1; } }
        public bool IsDoublet { get { return SpikeTimeList?.Count == 2; } }
        public bool IsBurst { get { return SpikeTimeList?.Count >= MinBurstSpikeCount; } }
        public int SpikeCount { get { return SpikeTimeList.Count; } }
        public static List<BurstOrSpike> SpikesToBursts(ModelSettings settings, double dt, List<int> SpikeList, out double lastInterval)
        {
            List<BurstOrSpike> burstsOrSpikes = new();
            BurstOrSpike burstOrSpike = new();
            burstsOrSpikes.Add(burstOrSpike);
            double lastTime = SpikeList[0] * dt;
            burstOrSpike.SpikeTimeList.Add(lastTime);
            lastInterval = double.NaN;
            bool spreadingOut = true;
            int sensitivity = BitConverter.GetBytes(decimal.GetBits((decimal)dt)[3])[2];
            for (int spikeTimeIndex = 1; spikeTimeIndex < SpikeList.Count; spikeTimeIndex++)
            {
                double curTime = SpikeList[spikeTimeIndex] * dt;
                double curInterval = Math.Round(curTime - lastTime, sensitivity);
                if (lastInterval is not double.NaN && curInterval < lastInterval - dt)//dt is used instead of epsilon, as the sensitivity is set by dt
                    spreadingOut = false;
                if ((lastInterval is double.NaN && curInterval > settings.MaxBurstInterval_DefaultLowerRange) ||
                    (spreadingOut && curInterval >= settings.MaxBurstInterval_DefaultUpperRange + GlobalSettings.Epsilon) ||
                    (!spreadingOut && curInterval >= settings.MaxBurstInterval_DefaultLowerRange + GlobalSettings.Epsilon))
                {
                    burstOrSpike = new();
                    burstsOrSpikes.Add(burstOrSpike);
                    spreadingOut = true;
                    lastInterval = double.NaN;
                }
                else
                {
                    lastInterval = curInterval;
                }
                burstOrSpike.SpikeTimeList.Add(curTime);
                lastTime = curTime;
            }
            return burstsOrSpikes;
        }

    }

}
