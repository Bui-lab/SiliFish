using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.DynamicUnits
{
    /// <summary>
    /// The class that is used to show how a cell behaves to a certain stimulus
    /// </summary>
    public class DynamicsStats
    {
        private Cluster BurstCluster, InterBurstCluster;
        private Dictionary<double, double> intervals = null;

        public static double MaxBurstInterval { get; set; } = 1; //in dt, the maximum interval two spikes can have to be considered as part of a burst
        public static double OneClusterMultiplier { get; set; } = 0.1; // (Centroid2 - Centroid1) < Centroid1 * OneClusterMultiplier means there is only one cluster (all pisked part of a burst)
        public static double TonicPadding { get; set; } = 1; //in dt, the range between the last spike and the end of current to be considered as tonic firing
        /// <summary>
        /// The list of tau values for each spike (by time)
        /// </summary>
        public Dictionary<double, double> TauDecay, TauRise;

        /// <summary>
        /// The time points of each spike
        /// </summary>
        public List<double> SpikeTimeList;

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
        private FiringPattern firingPattern = FiringPattern.Unknown;

        public FiringPattern FiringPattern { get { return firingPattern; } }
        
        public Dictionary<double, double> Intervals
        {
            get
            {
                if (intervals == null && SpikeTimeList.Count > 1)
                {
                    intervals = new();
                    foreach (int i in Enumerable.Range(0, SpikeTimeList.Count - 1))
                        intervals.Add(RunParam.static_dt * SpikeTimeList[i], RunParam.static_dt * (SpikeTimeList[i + 1] - SpikeTimeList[i]));
                }
                return intervals;
            }
        }

        public Dictionary<double, double> FiringFrequency
        {
            get 
            {
                return Intervals?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value > 0 ? 1000 / kvp.Value : 0);
            }
        }

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
            if (Intervals == null || Intervals.Count <= 1)
                return;
            List<double> intervals = Intervals.Select(kvp => kvp.Value).ToList();

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

        public void SpikingPattern()
        {
            if (SpikeTimeList.Count == 1)
                firingPattern = FiringPattern.Phasic;

            if (Intervals == null || Intervals.Count <= 1) return;

            CreateClusters();
            if (BurstCluster == null && InterBurstCluster == null)
                return;

            double lastITime = IList.ToList().FindLastIndex(i => i > 0) * RunParam.static_dt;
            double lastSpikeTime = SpikeTimeList[^1];

            if (BurstCluster != null && InterBurstCluster == null)//can be tonic or phasic bursting
            {
                if (lastSpikeTime >= lastITime - TonicPadding)
                {
                    firingPattern = FiringPattern.Tonic;
                    return;
                }
                firingPattern = FiringPattern.PhasicBursting;
                return;
            }

            List<double> intervals = Intervals.Select(kvp => kvp.Value).ToList();
            if (BurstCluster == null && InterBurstCluster != null)//intervals: same, increasing, decreasing-->tonic; otherwise: unknown
            {
                if (intervals.Count > 2)
                {
                    firingPattern = FiringPattern.Tonic;
                    bool decreasing = intervals[1] < intervals[0];
                    for (int i = 2; i < intervals.Count; i++)
                    {
                        if (decreasing && intervals[i] > intervals[i - 1])
                        {
                            firingPattern = FiringPattern.Unknown;
                            break;
                        }
                        else if (!decreasing && intervals[i] < intervals[i - 1])
                        {
                            firingPattern = FiringPattern.Unknown;
                            break;
                        }
                    }
                }
                return;
            }
            //TonicBursting, Chattering, 

            bool[] home = new bool[intervals.Count];
            int ind = 0;
            foreach (double d in intervals)
            {
                home[ind++] = d <= BurstCluster.clusterMax;
            }
            List<List<double>> Bursts = new();
            bool newBurst = true;
            ind = 0;
            foreach (bool b in home)
            {
                if (b)
                {
                    if (newBurst)
                    {
                        Bursts.Add(new List<double>());
                        newBurst = false;
                    }
                    Bursts.Last().Add(ind);
                }
                else
                    newBurst = true;
                ind++;
            }

            if (Bursts.Count == 1)
            {
                //TODO test whether it ever comes here

                return;
            }
            int skipend = lastSpikeTime >= (lastITime - TonicPadding) ? 1 : 0;
            int skip = Bursts.Count - skipend > 2 ? 1 : 0;
            List<List<double>> MidBursts = Bursts.Skip(skip).Take(Bursts.Count - skip - skipend).ToList();

            if (MidBursts.Min(burst => burst.Count) == MidBursts.Max(burst => burst.Count))
                firingPattern = FiringPattern.TonicBursting;
            else
                firingPattern = FiringPattern.Chattering;
        }
        public DynamicsStats(double[] stimulus)
        {
            int iMax = stimulus.Length;
            IList = stimulus;
            VList = new double[iMax];
            SecList = new double[iMax];
            TauDecay = new();
            TauRise = new();
            SpikeTimeList = new();
        }
    }
}
