using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits.Firing
{
    public class TrainOfBurstsOrSpikes
    {
        public int ID;
        public string Name;
        public List<BurstOrSpike> BurstOrSpikes;
        public BurstOrSpike EarliestSpike
        {
            get
            {
                if (BurstOrSpikes == null) return null;
                List<BurstOrSpike> bsl = BurstOrSpikes.Where(bs => bs.SpikeTimeList.Count > 0).ToList();
                double minSpike = bsl.Min(bs => bs.SpikeTimeList[0]);
                return bsl.FirstOrDefault(bs => bs.SpikeTimeList[0] == minSpike);
            }
        }
    }
}
