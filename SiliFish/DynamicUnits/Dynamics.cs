using SiliFish.Definitions;
using System.Collections.Generic;
using System.Linq;

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

        public static (FiringRhythm, FiringPattern) FiringPatternOfList(List<BurstOrSpike> Bursts, bool followedByQuiescence)
        {
            if (Bursts.Count == 1)
            {
                if (Bursts[0].IsSpike)
                    return (FiringRhythm.Phasic, FiringPattern.Spiking);  //leadByQuiescence?FiringPattern.DelayedPhasic : FiringPattern.Phasic;
                else if (followedByQuiescence)
                    return (FiringRhythm.Phasic, FiringPattern.Bursting);
                else return (FiringRhythm.Tonic, FiringPattern.Spiking);
            }

            //(Bursts.Count > 1)

            if (!Bursts.Any(b => !b.IsBurst))//All of them are bursts
                return (FiringRhythm.Tonic, FiringPattern.Bursting);

            if (!Bursts.Any(b => !b.IsSpike))//all of them are spikes
            {
                if (Bursts.Count == 2)
                    return (FiringRhythm.Tonic, FiringPattern.Spiking);

                FiringRhythm firingRhythm = FiringRhythm.Tonic;
                FiringPattern firingPattern = FiringPattern.Spiking;

                //check whether the intervals are random
                double[] intervalArray = new double[Bursts.Count - 1];
                int intIndex = 0;
                foreach (int i in Enumerable.Range(0, Bursts.Count - 1))
                    intervalArray[intIndex++] = Bursts[i + 1].SpikeTimeList[0] - Bursts[i + 1].SpikeTimeList[0];
                bool decreasing = intervalArray[1] < intervalArray[0];
                for (int i = 2; i < intervalArray.Length; i++)
                {
                    if (decreasing && intervalArray[i] > intervalArray[i - 1])
                    {
                        firingRhythm = FiringRhythm.Chattering;
                        firingPattern = FiringPattern.Chattering;
                        break;
                    }
                    else if (!decreasing && intervalArray[i] < intervalArray[i - 1])
                    {
                        firingRhythm = FiringRhythm.Chattering;
                        firingPattern = FiringPattern.Chattering;
                        break;
                    }
                }
                return (firingRhythm, firingPattern);
            }
            return (FiringRhythm.Chattering, FiringPattern.Mixed);
        }
    }
    /// <summary>
    /// The class that is used to show how a cell behaves to a certain stimulus
    /// </summary>
    public class DynamicsStats
    {
        private Dictionary<double, double> intervals = null;

        public static double MaxBurstInterval_LowerRange { get; set; } = 5; //in ms, the maximum interval two spikes can have to be considered as part of a burst
        public static double MaxBurstInterval_UpperRange { get; set; } = 30; //in ms, the maximum interval two spikes can have to be considered as part of a burst
        public static double TonicPadding { get; set; } = 1; //in ms, the range between the last spike and the end of current to be considered as tonic firing
        /// <summary>
        /// The list of tau values for each spike (by time)
        /// </summary>
        public Dictionary<double, double> TauDecay, TauRise;

        /// <summary>
        /// The time index of each spike
        /// </summary>
        public List<int> SpikeList;

        /// <summary>
        /// The input current (stimulus)
        /// </summary>
        public double[] IList;

        /// <summary>
        /// The membrane potential
        /// </summary>
        public double[] VList;

        /// <summary>
        /// u for neurons and rel. tension for muscle cells
        /// </summary>
        public double[] SecList;
        private FiringRhythm firingRhythm = FiringRhythm.Phasic;
        private FiringDelay firingDelay = FiringDelay.NoDelay;
        private FiringPattern firingPattern = FiringPattern.NoSpike;

        public FiringRhythm FiringRhythm { get { return firingRhythm; } }
        public FiringDelay FiringDelay { get { return firingDelay; } }
        public FiringPattern FiringPattern { get { return firingPattern; } }

        public Dictionary<double, double> Intervals_ms
        {
            get
            {
                if (intervals == null && SpikeList.Count > 1)
                {
                    intervals = new();
                    foreach (int i in Enumerable.Range(0, SpikeList.Count - 1))
                        intervals.Add(RunParam.static_dt * SpikeList[i], RunParam.static_dt * (SpikeList[i + 1] - SpikeList[i]));
                }
                return intervals;
            }
        }

        public Dictionary<double, double> FiringFrequency
        {
            get
            {
                return Intervals_ms?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value > 0 ? 1000 / kvp.Value : 0);
            }
        }
        /*
        internal class Cluster
        {
            public double clusterMin, clusterMax, centroid;
            public int numMember = 0;
            public Cluster(double center)
            {
                clusterMin = clusterMax = centroid = center;
                numMember++;
            }
            public Cluster(List<double> intervals)
            {
                clusterMin = intervals.Min();
                clusterMax = intervals.Max();
                centroid = intervals.Average();
                numMember = intervals.Count;
            }
            public void AddMember(double d)
            {
                centroid = (d + centroid * numMember) / ++numMember;
                if (clusterMin > d)
                    clusterMin = d;
                if (clusterMax < d)
                    clusterMax = d;
            }
        }
        private void CreateClusters()
        {
            BurstCluster = null;
            InterBurstCluster = null;
            if (Intervals_ms == null || Intervals_ms.Count <= 1)
                return;
            List<double> intervals = Intervals_ms.Select(kvp => kvp.Value).ToList();

            double centroid1 = intervals.Min();
            if (centroid1 > MaxBurstInterval)// no bursts, only single spikes
            {
                InterBurstCluster = new(intervals);
                return;
            }
            double centroid2 = intervals.Max();
            if (centroid2 - centroid1 < centroid1 * OneClusterMultiplier)//single cluster
            {
                BurstCluster = new(intervals);
                return;
            }
            intervals.Remove(centroid1);
            intervals.Remove(centroid2);
            BurstCluster = new(centroid1);
            InterBurstCluster = new(centroid2);
            double center = (centroid1 + centroid2) / 2;
            foreach (double d in intervals)
            {
                if (d < center) //add to cluster 1
                    BurstCluster.AddMember(d);
                else //add to cluster 2
                    InterBurstCluster.AddMember(d);
                center = (BurstCluster.centroid + InterBurstCluster.centroid) / 2;
            }
        }
        */
        public void DefineSpikingPattern()
        {
            int curStart = IList.ToList().FindIndex(i => i > 0);

            SpikeList.RemoveAll(s => s < curStart);
            if (SpikeList.Count == 0)
            {
                firingPattern = FiringPattern.NoSpike;
                return;
            }
            double firsttITime = curStart * RunParam.static_dt;
            double firstSpikeTime = SpikeList[0] * RunParam.static_dt;
            bool leadByQuiescence = firstSpikeTime - firsttITime >= 10 * TonicPadding;
            if (leadByQuiescence)
                firingDelay = FiringDelay.Delayed;

            if (SpikeList.Count == 1)
            {
                firingRhythm = FiringRhythm.Phasic;
                firingPattern = FiringPattern.Spiking;
                return;
            }

            List<BurstOrSpike> Bursts = new();
            BurstOrSpike burstOrSpike = new();
            Bursts.Add(burstOrSpike);
            double lastTime = SpikeList[0] * RunParam.static_dt;
            burstOrSpike.SpikeTimeList.Add(lastTime);
            double lastInterval = double.NaN;
            bool spreadingOut = true;
            for (int spikeTimeIndex = 1; spikeTimeIndex < SpikeList.Count; spikeTimeIndex++)
            {
                double curTime = SpikeList[spikeTimeIndex] * RunParam.static_dt;
                double curInterval = curTime - lastTime;
                if (!(lastInterval is double.NaN) && curInterval < lastInterval - Const.Epsilon)
                    spreadingOut = false;
                if ((lastInterval is double.NaN && curInterval > MaxBurstInterval_LowerRange) ||
                    (spreadingOut && curInterval > MaxBurstInterval_UpperRange) ||
                    (!spreadingOut && curInterval > MaxBurstInterval_LowerRange))
                {
                    burstOrSpike = new();
                    Bursts.Add(burstOrSpike);
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
            double lastITime = IList.ToList().FindLastIndex(i => i > 0) * RunParam.static_dt;
            double lastSpikeTime = SpikeList[^1] * RunParam.static_dt;
            if (lastInterval == double.NaN)
                lastInterval = TonicPadding;
            bool followedByQuiescence = lastITime - lastSpikeTime >= lastInterval + TonicPadding;
            (firingRhythm, firingPattern) = BurstOrSpike.FiringPatternOfList(Bursts, followedByQuiescence);
        }


        public DynamicsStats(double[] stimulus)
        {
            int iMax = stimulus.Length;
            IList = stimulus;
            VList = new double[iMax];
            SecList = new double[iMax];
            TauDecay = new();
            TauRise = new();
            SpikeList = new();
        }
    }
}
