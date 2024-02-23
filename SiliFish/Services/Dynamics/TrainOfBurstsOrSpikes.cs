using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliFish.DynamicUnits;
using SiliFish.ModelUnits.Cells;

namespace SiliFish.Services.Dynamics
{
    /// <summary>
    /// Class that keeps the train of bursts per cell/pool, or from rostral to caudal
    /// </summary>
    public class TrainOfBursts
    {
        public int iID = -1; //integer identifier
        public string sID = null; //string identifier
        public List<(int iID, string sID, BurstOrSpike Bursts)> BurstList;
        public (int iID, string sID, BurstOrSpike burst) EarliestSpike
        {
            get
            {
                if (BurstList == null || BurstList.Count == 0) return (-1, null, null);
                List<(int iID, string sID, BurstOrSpike Bursts)> bsl = BurstList.Where(bs => bs.Bursts.SpikeTimeList.Count > 0).ToList();
                double minSpike = bsl.Min(bs => bs.Bursts.SpikeTimeList[0]);
                return bsl.FirstOrDefault(bs => bs.Bursts.SpikeTimeList[0] == minSpike);
            }
        }

        public TrainOfBursts(List<BurstOrSpike> bsl, string sID = null, int iID = -1)
        {
            this.sID = sID;
            this.iID = iID;
            BurstList = [];
            foreach (BurstOrSpike b in bsl)
            {
                BurstList.Add((iID, sID, b));
            }
        }
    }
}
