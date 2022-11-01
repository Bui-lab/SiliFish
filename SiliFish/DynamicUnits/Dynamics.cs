using SiliFish.Definitions;
using SiliFish.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.DynamicUnits
{
    public class Cluster
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

        public void MergeCluster(Cluster c)
        {
            centroid = (centroid * numMember + c.centroid * c.numMember) / (numMember + c.numMember);
            numMember += c.numMember;
            if (clusterMax < c.clusterMin)
                clusterMax = c.clusterMax;
            else
                clusterMin = c.clusterMin;

        }

    }
    public class BurstOrSpike
    {
        public static double MinBurstSpikeCount { get; set; } = 2; //the number of minimum spikes required
        public List<double> SpikeTimeList = new();
        public bool IsSpike { get { return SpikeTimeList?.Count == 1; } }
        public bool IsDoublet { get { return SpikeTimeList?.Count == 2; } }
        public bool IsBurst { get { return SpikeTimeList?.Count >= MinBurstSpikeCount; } }
        public int SpikeCount { get { return SpikeTimeList.Count; } }
    }
    /// <summary>
    /// The class that is used to show how a cell behaves to a certain stimulus
    /// </summary>
    public class DynamicsStats
    {
        public Cluster BurstCluster;
        public Cluster InterBurstCluster;

        private Dictionary<double, double> intervals = null;
        private bool analyzed = false;
        private bool followedByQuiescence;
        private bool decreasingIntervals;
        private bool increasingIntervals;
        private bool randomIntervals;
            public double MaxBurstInterval_LowerRange { get; set; } = 5; //TODO in ms, the maximum interval two spikes can have to be considered as part of a burst
        public double MaxBurstInterval_UpperRange { get; set; } = 30; //TODO in ms, the maximum interval two spikes can have to be considered as part of a burst
        public static double OneClusterMultiplier { get; set; } = 2; //TODO Centroid2 < Centroid1 * OneClusterMultiplier means there is only one cluster (all spikes part of a burst)
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
        public Dictionary <string, double[]> SecLists;
        private FiringRhythm firingRhythm = FiringRhythm.NoSpike;
        private FiringPattern firingPattern = FiringPattern.NoSpike;
        private double firingDelay = -1;
        public FiringRhythm FiringRhythm { get { return firingRhythm; } }
        public FiringPattern FiringPattern { get { return firingPattern; } }
        public double FiringDelay { get { return firingDelay; } }

        public Dictionary<double, double> Intervals_ms
        {
            get
            {
                if (intervals == null)
                {
                    intervals = new();
                    if (SpikeList.Count > 1)
                    {
                        foreach (int i in Enumerable.Range(0, SpikeList.Count - 1))
                            intervals.Add(RunParam.static_dt * SpikeList[i], RunParam.static_dt * (SpikeList[i + 1] - SpikeList[i]));
                    }
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
        private List<BurstOrSpike> burstsOrSpikes;
        public List<BurstOrSpike> BurstsOrSpikes
        {
            get
            {
                if (!analyzed)
                    DefineSpikingPattern();
                return burstsOrSpikes;
            }
            set { burstsOrSpikes = value; }
        }

        private double spikeDelay = double.NaN;
        public double SpikeDelay
        {
            get
            {
                if (spikeDelay is double.NaN)
                {
                    if (SpikeList.Count == 0)
                    {
                        spikeDelay = -1;
                    }
                    else
                    {
                        int curStart = IList.ToList().FindIndex(i => i > 0);
                        int firstSpike = SpikeList.FirstOrDefault(s => s >= curStart, -1);
                        if (firstSpike < 0) spikeDelay = -1;
                        else spikeDelay = (firstSpike - curStart) * RunParam.static_dt;
                    }
                }
                return spikeDelay;
            }
        }

        public double Irregularity
        {
            get
            {
                if (decreasingIntervals || increasingIntervals)
                    return 0;
                if (SpikeList.Count > 1)
                {
                    double avg = Intervals_ms?.Values.ToArray().AverageValue() ?? 0;
                    double SD = Intervals_ms?.Values.ToArray().StandardDeviation() ?? 0;
                    return SD / avg;
                }
                return 0;
            }
        }

        public double SpikeCoverage(bool ignoreDelay)
        {
            if (!SpikeList.Any()) 
                return 0;
            int firstIIndex = IList.ToList().FindIndex(i => i > 0);
            if (firstIIndex == -1) 
                return 0;
            int lastIIndex = IList.ToList().FindLastIndex(i => i > 0);
            if (firstIIndex == lastIIndex) 
                return 0;
            int lastSpikeIndex = SpikeList.Last();
            int firstSpikeIndex = ignoreDelay ? firstIIndex: SpikeList.First();
            return (double)(lastSpikeIndex - firstSpikeIndex) / (lastIIndex - firstIIndex);
        }

        public DynamicsStats(double[] stimulus)
        {
            int iMax = stimulus.Length;
            IList = stimulus;
            VList = new double[iMax];
            SecLists = new Dictionary<string, double[]>();
            TauDecay = new();
            TauRise = new();
            SpikeList = new();
            spikeDelay = double.NaN;
            analyzed = false;
        }
        private bool HasClusters()//check whether there are two clusters of intervals: interspike & interburst
        {
            if (Intervals_ms == null || Intervals_ms.Count <= 1)
                return false;
            List<double> intervals = Intervals_ms.Select(kvp => kvp.Value).ToList();

            double centroid1 = intervals.Min();
            double centroid2 = intervals.Max();
            if (centroid1 > MaxBurstInterval_LowerRange)// no bursts, only single spikes
            {
                InterBurstCluster = new(centroid2);
                return false;
            }
            if (centroid2 < centroid1 * OneClusterMultiplier)//single cluster
            {
                BurstCluster = new(centroid1);
                return false;
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
            if (InterBurstCluster.centroid < BurstCluster.centroid * OneClusterMultiplier ||
                BurstCluster.clusterMax*BurstCluster.clusterMax/BurstCluster.clusterMin > InterBurstCluster.centroid)//single cluster
            {
                BurstCluster.MergeCluster(InterBurstCluster);
                InterBurstCluster = null;
                return false;
            }
            MaxBurstInterval_LowerRange = MaxBurstInterval_UpperRange = BurstCluster.clusterMax;
            return true;
        }
        private void SetFiringPatternOfList()
        {
            if (burstsOrSpikes.Count == 1)
            {
                if (burstsOrSpikes[0].IsSpike)
                {
                    firingRhythm = FiringRhythm.Phasic;
                    firingPattern = FiringPattern.Spiking;
                    return;
                }
                else if (followedByQuiescence)
                {
                    firingRhythm = FiringRhythm.Phasic;
                    firingPattern = FiringPattern.Bursting;
                    return;
                }
                else
                {
                    firingRhythm = FiringRhythm.Tonic;
                    firingPattern = FiringPattern.Spiking;
                    return;
                }
            }

            if (!burstsOrSpikes.Any(b => !b.IsBurst) ||
                !followedByQuiescence && !burstsOrSpikes.SkipLast(1).Any(b => !b.IsBurst))//All of them are bursts
            {
                firingRhythm = FiringRhythm.Tonic;
                firingPattern = FiringPattern.Bursting;
                return;
            }
            
            firingRhythm = followedByQuiescence ? FiringRhythm.Phasic : FiringRhythm.Tonic;
            if (!burstsOrSpikes.Any(b => !b.IsSpike))//all of them are spikes
            {
                if (burstsOrSpikes.Count == 2 || burstsOrSpikes.Count == 3 && followedByQuiescence)//if there are only 3 spikes, there cannot be any randomness
                {
                    firingPattern = FiringPattern.Chattering;
                    return;
                }

                //check whether the intervals are random
                double[] intervalArray = new double[burstsOrSpikes.Count - 1];
                int intIndex = 0;
                foreach (int i in Enumerable.Range(0, burstsOrSpikes.Count - 1))
                    intervalArray[intIndex++] = burstsOrSpikes[i + 1].SpikeTimeList[0] - burstsOrSpikes[i].SpikeTimeList[0];
                randomIntervals = intervalArray.IsRandom(0.1, out decreasingIntervals, out increasingIntervals);//TODO hardcoded 0.1
                firingPattern = randomIntervals ? FiringPattern.Chattering : FiringPattern.Spiking;
                return;
            }
            firingPattern = FiringPattern.Mixed;
            return;
        }
        public void DefineSpikingPattern()
        {
            if (analyzed) return;
            burstsOrSpikes = new();
            int curStart = IList.ToList().FindIndex(i => i > 0);

            SpikeList.RemoveAll(s => s < curStart);
            if (SpikeList.Count == 0)
            {
                firingPattern = FiringPattern.NoSpike;
                analyzed = true;
                return;
            }
            double firsttITime = curStart * RunParam.static_dt;
            double firstSpikeTime = SpikeList[0] * RunParam.static_dt;
            firingDelay = firstSpikeTime - firsttITime;
            double lastITime = IList.ToList().FindLastIndex(i => i > 0) * RunParam.static_dt;
            double lastSpikeTime = SpikeList[^1] * RunParam.static_dt;
            double quiescence = TonicPadding;
            if (Intervals_ms.Values.Any())
                quiescence = Intervals_ms.Values.Average();
            followedByQuiescence = lastITime - lastSpikeTime >= quiescence;

            if (SpikeList.Count == 1)
            {
                firingRhythm = FiringRhythm.Phasic;
                firingPattern = FiringPattern.Spiking;
                analyzed = true;
                return;
            }
            bool hasClusters = HasClusters();
            if (!hasClusters)
            {
                if (!followedByQuiescence && BurstCluster != null)//consider all spikes individually
                {
                    MaxBurstInterval_LowerRange = MaxBurstInterval_UpperRange = 0;
                }
            }

            BurstOrSpike burstOrSpike = new();
            burstsOrSpikes.Add(burstOrSpike);
            double lastTime = SpikeList[0] * RunParam.static_dt;
            burstOrSpike.SpikeTimeList.Add(lastTime);
            double lastInterval = double.NaN;
            bool spreadingOut = true;
            for (int spikeTimeIndex = 1; spikeTimeIndex < SpikeList.Count; spikeTimeIndex++)
            {
                double curTime = SpikeList[spikeTimeIndex] * RunParam.static_dt;
                double curInterval = curTime - lastTime;
                if (lastInterval is not double.NaN && curInterval < lastInterval - Const.Epsilon)
                    spreadingOut = false;
                if ((lastInterval is double.NaN && curInterval > MaxBurstInterval_LowerRange) ||
                    (spreadingOut && curInterval >= MaxBurstInterval_UpperRange + Const.Epsilon) ||
                    (!spreadingOut && curInterval >= MaxBurstInterval_LowerRange + Const.Epsilon))
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
            if (double.IsNaN(lastInterval))
                lastInterval = quiescence;
            followedByQuiescence = lastITime - lastSpikeTime >= lastInterval + TonicPadding;
            SetFiringPatternOfList();
            analyzed = true;
        }



    }
}
