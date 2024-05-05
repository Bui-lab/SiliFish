using SiliFish.Definitions;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.DynamicUnits
{
    public class BurstOrSpike
    {
        public static double MinBurstSpikeCount { get; set; } = 2; //the number of minimum spikes required
        public List<double> SpikeTimeList = [];
        public bool IsSpike { get { return SpikeTimeList?.Count == 1; } }
        public bool IsDoublet { get { return SpikeTimeList?.Count == 2; } }
        public bool IsBurst { get { return SpikeTimeList?.Count >= MinBurstSpikeCount; } }
        public int SpikeCount { get { return SpikeTimeList.Count; } }
        public double Start => SpikeTimeList.Count > 0 ? SpikeTimeList[0] : -1;
        public double End => SpikeTimeList.Count > 0 ? SpikeTimeList[^1] : -1;
        public double Center => SpikeTimeList.Count > 0 ? (SpikeTimeList[0] + SpikeTimeList[^1]) / 2 : -1;
        public double WeightedCenter => SpikeTimeList.Count > 0 ? SpikeTimeList.Average(): -1;

        public static List<BurstOrSpike> SpikesToBursts(DynamicsParam settings, double dt, List<int> SpikeList, out double lastInterval)
        {
            if (SpikeList == null || SpikeList.Count == 0)
            {
                lastInterval = 0;
                return null;
            }
            List<BurstOrSpike> burstsOrSpikes = [];
            BurstOrSpike burstOrSpike = new();
            burstsOrSpikes.Add(burstOrSpike);
            double lastTime = SpikeList[0] * dt;
            burstOrSpike.SpikeTimeList.Add(lastTime);
            lastInterval = double.NaN;
            bool spreadingOut = true;
            int sensitivity = BitConverter.GetBytes(decimal.GetBits((decimal)dt)[3])[2];
            if (sensitivity > 15) sensitivity = 15; //max precision of double
            for (int spikeTimeIndex = 1; spikeTimeIndex < SpikeList.Count; spikeTimeIndex++)
            {
                double curTime = SpikeList[spikeTimeIndex] * dt;
                double curInterval = Math.Round(curTime - lastTime, sensitivity);
                if (lastInterval is not double.NaN && curInterval < lastInterval - dt)//dt is used instead of epsilon, as the sensitivity is set by dt
                    spreadingOut = false;
                if ((lastInterval is double.NaN && curInterval > settings?.MaxBurstInterval_DefaultLowerRange) ||
                    (spreadingOut && curInterval >= settings?.MaxBurstInterval_DefaultUpperRange + GlobalSettings.Epsilon) ||
                    (!spreadingOut && curInterval >= settings?.MaxBurstInterval_DefaultLowerRange + GlobalSettings.Epsilon))
                {
                    burstOrSpike = new();
                    spreadingOut = lastInterval > 0;
                    lastInterval = double.NaN;
                    burstsOrSpikes.Add(burstOrSpike);
                }
                else
                {
                    lastInterval = curInterval;
                }
                burstOrSpike.SpikeTimeList.Add(curTime);
                lastTime = curTime;
            }
            //review bursts to remove the wide intervals that doesn't fit
            List<BurstOrSpike> burstsOrSpikesFiltered = [];
            foreach(BurstOrSpike burst in  burstsOrSpikes)
            {
                List<double> intervals = [];
                if (burst.SpikeCount <= 2)
                    burstsOrSpikesFiltered.Add(burst);
                else
                {
                    for (int i = 1; i < burst.SpikeCount; i++)
                    {
                        intervals.Add(burst.SpikeTimeList[i] - burst.SpikeTimeList[i - 1]);
                    }
                    double maxInterval = intervals.Max();
                    double avgInterval = (intervals.Sum() - maxInterval) / intervals.Count;
                    if (maxInterval > avgInterval * 10) //split into 2 or 3 bursts
                    {
                        int ind = 0;
                        BurstOrSpike b1 = new();
                        b1.SpikeTimeList.Add(burst.SpikeTimeList[0]);
                        burstsOrSpikesFiltered.Add(b1);
                        while (ind < intervals.Count)
                        {
                            if (intervals[ind] > avgInterval * 10)
                            {
                                b1 = new();
                                b1.SpikeTimeList.Add(burst.SpikeTimeList[++ind]);
                                burstsOrSpikesFiltered.Add(b1);
                            }
                            else
                            {
                                b1.SpikeTimeList.Add(burst.SpikeTimeList[++ind]);
                            }
                        }
                    }
                    else
                        burstsOrSpikesFiltered.Add(burst);
                }
            }
            return burstsOrSpikesFiltered;
        }

    }

}
