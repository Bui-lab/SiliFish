using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using SiliFish.DynamicUnits;

namespace SiliFish.Services.Dynamics
{
    /// <summary>
    /// Class that keeps the train of bursts per cell/pool, or from rostral to caudal
    /// </summary>
    public class TrainOfBurstCars
    {
        public string Description { get; private set; }
        public string CarDescription { get; private set; }
        public int iTrainID = -1; //integer identifier - the train sequence or the somite number
        public string sTrainID = null; //string identifier - cell or pool id
        public List<BurstOrSpikeCar> BurstCarList;
        public BurstOrSpikeCar EarliestSpike
        {
            get
            {
                if (BurstCarList == null || BurstCarList.Count == 0) return null;
                List<BurstOrSpikeCar> bsl = BurstCarList.Where(bs => bs.BurstOrSpike.SpikeTimeList.Count > 0).ToList();
                double minSpike = bsl.Min(bs => bs.BurstOrSpike.SpikeTimeList[0]);
                return bsl.FirstOrDefault(bs => bs.BurstOrSpike.SpikeTimeList[0] == minSpike);
            }
        }

        public TrainOfBurstCars(List<BurstOrSpike> bsl, string description, string burstDescription,
            int iTrainID = -1, string sTrainID = null)
        {
            Description = description;
            CarDescription = burstDescription;
            this.sTrainID = sTrainID;
            this.iTrainID = iTrainID;
            BurstCarList = [];
            foreach (BurstOrSpike b in bsl)
            {
                BurstCarList.Add(new BurstOrSpikeCar(b, iTrainID, sTrainID));
            }
        }

        public void CalculateCarDelays()
        {
            BurstCarList.Sort(new BurstIdComparer());
            for (int i = 1; i < BurstCarList.Count; i++)
            {
                BurstCarList[i].DelayStart = BurstCarList[i].BurstOrSpike.Start - BurstCarList[i-1].BurstOrSpike.Start;
                BurstCarList[i].DelayCenter = BurstCarList[i].BurstOrSpike.Center - BurstCarList[i - 1].BurstOrSpike.Center;
                BurstCarList[i].DelayMedian = BurstCarList[i].BurstOrSpike.WeightedCenter - BurstCarList[i - 1].BurstOrSpike.WeightedCenter;
            }
        }

    }
}
