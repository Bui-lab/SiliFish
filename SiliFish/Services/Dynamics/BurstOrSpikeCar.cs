using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliFish.DynamicUnits;

namespace SiliFish.Services.Dynamics
{
    /// <summary>
    /// The class structure to be used in TrainOfBursts
    /// Keeps extra information other than the burst info
    /// </summary>
    public class BurstOrSpikeCar(BurstOrSpike burstOrSpike, int iCarID, string sCarID)
    {
        public int iCarID { get; } = iCarID;
        public string sCarID { get; } = sCarID;
        public BurstOrSpike BurstOrSpike { get; } = burstOrSpike;
        public double? DelayStart;//the delay between the start time of the current burst and the preceding one
        public double? DelayCenter;//the delay between the center of the current burst and the preceding one
        public double? DelayMedian;//the delay between the median of the current burst and the preceding one
    }

    class BurstIdComparer : IComparer<BurstOrSpikeCar>
    {
        public int Compare(BurstOrSpikeCar car1, BurstOrSpikeCar car2)
        {
            return car1.iCarID.CompareTo(car2.iCarID);
        }
    }


}
