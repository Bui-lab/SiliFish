using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using SiliFish.DynamicUnits;

namespace SiliFish.Services.Dynamics
{
    /// <summary>
    /// Class that keeps the train of bursts per cell/pool, or from rostral to caudal
    /// </summary>
    public class TrainOfBursts
    {
        public string Description { get; private set; }
        public string BurstDescription { get; private set; }
        public int iTrainID = -1; //integer identifier - the train sequence or the somite number
        public string sTrainID = null; //string identifier - cell or pool id
        public List<(int iBurstID, string sBurstID, BurstOrSpike Bursts)> BurstList;
        public (int iBurstID, string sBurstID, BurstOrSpike burst) EarliestSpike
        {
            get
            {
                if (BurstList == null || BurstList.Count == 0) return (-1, null, null);
                List<(int iBurstID, string sBurstID, BurstOrSpike Bursts)> bsl = BurstList.Where(bs => bs.Bursts.SpikeTimeList.Count > 0).ToList();
                double minSpike = bsl.Min(bs => bs.Bursts.SpikeTimeList[0]);
                return bsl.FirstOrDefault(bs => bs.Bursts.SpikeTimeList[0] == minSpike);
            }
        }

        public TrainOfBursts(List<BurstOrSpike> bsl, string description, string burstDescription,
            int iTrainID = -1, string sTrainID = null)
        {
            Description = description;
            BurstDescription = burstDescription;
            this.sTrainID = sTrainID;
            this.iTrainID = iTrainID;
            BurstList = [];
            foreach (BurstOrSpike b in bsl)
            {
                BurstList.Add((iTrainID, sTrainID, b));
            }
        }

        public void AddBurstsOrSpikes(List<BurstOrSpike> bsl, int iBurstID, string sBurstID)
        {
            foreach (BurstOrSpike b in bsl)
            {
                BurstList.Add((iBurstID, sBurstID, b));
            }
        }
    }
}
