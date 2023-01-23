﻿using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
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
        private double dt;

        public Cluster BurstCluster;
        public Cluster InterBurstCluster;

        private Dictionary<double, double> intervals = null;
        private bool analyzed = false;
        private bool followedByQuiescence;
        private bool decreasingIntervals;
        private bool increasingIntervals;
        public double Irregularity { get; set; }

        public double MaxBurstInterval_LowerRange { get; set; } = CurrentSettings.Settings.MaxBurstInterval_DefaultLowerRange; //in ms, the maximum interval two spikes can have to be considered as part of a burst
        public double MaxBurstInterval_UpperRange { get; set; } = CurrentSettings.Settings.MaxBurstInterval_DefaultUpperRange; //in ms, the maximum interval two spikes can have to be considered as part of a burst
        /// <summary>
        /// The list of tau values for each spike (by time)
        /// </summary>
        public Dictionary<double, double> TauDecay, TauRise;

        /// <summary>
        /// The time index of each spike during the stimulus
        /// </summary>
        public List<int> SpikeList;
        public List<int> PreStimulusSpikeList;
        public List<int> PostStimulusSpikeList;

        /// <summary>
        /// The input current (stimulus)
        /// </summary>
        public double[] StimulusArray;

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
                            intervals.Add(dt * SpikeList[i], dt* (SpikeList[i + 1] - SpikeList[i]));
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
                        int curStart = StimulusArray.ToList().FindIndex(i => i > 0);
                        int firstSpike = SpikeList.FirstOrDefault(s => s >= curStart, -1);
                        if (firstSpike < 0) spikeDelay = -1;
                        else spikeDelay = (firstSpike - curStart) * dt;
                    }
                }
                return spikeDelay;
            }
        }
        public double CurrentStartTime
        {
            get 
            {
                int curStart = StimulusArray.ToList().FindIndex(i => i > 0);
                return curStart * dt;
            }
        }
        public double CurrentEndTime
        {
            get
            {
                int curEnd = StimulusArray.ToList().FindLastIndex(i => i > 0);
                return curEnd * dt;
            }
        }
        public double SpikeCoverage(bool ignoreDelay)
        {
            if (!SpikeList.Any()) 
                return 0;
            int firstIIndex = StimulusArray.ToList().FindIndex(i => i > 0);
            if (firstIIndex == -1) 
                return 0;
            int lastIIndex = StimulusArray.ToList().FindLastIndex(i => i > 0);
            if (firstIIndex == lastIIndex) 
                return 0;
            int lastSpikeIndex = SpikeList.Last();
            int firstSpikeIndex = ignoreDelay ? firstIIndex: SpikeList.First();
            return (double)(lastSpikeIndex - firstSpikeIndex) / (lastIIndex - firstIIndex);
        }
        public DynamicsStats()
        { }
        public DynamicsStats(double[] stimulus, double dt)
        {
            this.dt= dt;
            int iMax = stimulus.Length;
            StimulusArray = stimulus;
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
            if (centroid2 < centroid1 * CurrentSettings.Settings.OneClusterMultiplier)//single cluster
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
            if (InterBurstCluster.centroid < BurstCluster.centroid * CurrentSettings.Settings.OneClusterMultiplier ||
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
                Irregularity = intervalArray.Irregularity(out decreasingIntervals, out increasingIntervals);
                firingPattern = Irregularity > CurrentSettings.Settings.ChatteringIrregularity ? FiringPattern.Chattering : FiringPattern.Spiking;
                return;
            }
            firingPattern = FiringPattern.Mixed;
            return;
        }
        public void DefineSpikingPattern()
        {
            if (analyzed) return;
            burstsOrSpikes = new();
            int stimulusStart = StimulusArray.ToList().FindIndex(i => i > 0);
            int stimulusEnd = StimulusArray.ToList().FindLastIndex(i => i > 0);

            PreStimulusSpikeList = SpikeList.Where(s => s < stimulusStart).ToList();
            PostStimulusSpikeList = SpikeList.Where(s => s > stimulusEnd).ToList();
            if (stimulusStart >= 0) //if there is no stimulus - check without removing the spikes before the stimulus
                SpikeList.RemoveAll(s => s < stimulusStart || s > stimulusEnd);
            if (SpikeList.Count == 0)
            {
                firingPattern = FiringPattern.NoSpike;
                analyzed = true;
                return;
            }
            double firstStimulusTime = stimulusStart * dt;
            double firstSpikeTime = SpikeList[0] * dt;
            firingDelay = firstSpikeTime - firstStimulusTime;
            double lastStimulusTime = stimulusEnd * dt;
            double lastSpikeTime = SpikeList[^1] * dt;
            double quiescence = CurrentSettings.Settings.TonicPadding;
            if (Intervals_ms.Values.Any())
                quiescence = Intervals_ms.Values.Average();
            followedByQuiescence = lastStimulusTime - lastSpikeTime >= quiescence;

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
            double lastTime = SpikeList[0] * dt;
            burstOrSpike.SpikeTimeList.Add(lastTime);
            double lastInterval = double.NaN;
            bool spreadingOut = true;
            for (int spikeTimeIndex = 1; spikeTimeIndex < SpikeList.Count; spikeTimeIndex++)
            {
                double curTime = SpikeList[spikeTimeIndex] * dt;
                double curInterval = curTime - lastTime;
                if (lastInterval is not double.NaN && curInterval < lastInterval - GlobalSettings.Epsilon)
                    spreadingOut = false;
                if ((lastInterval is double.NaN && curInterval > MaxBurstInterval_LowerRange) ||
                    (spreadingOut && curInterval >= MaxBurstInterval_UpperRange + GlobalSettings.Epsilon) ||
                    (!spreadingOut && curInterval >= MaxBurstInterval_LowerRange + GlobalSettings.Epsilon))
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
            followedByQuiescence = lastStimulusTime - lastSpikeTime >= lastInterval + CurrentSettings.Settings.TonicPadding;
            SetFiringPatternOfList();
            analyzed = true;
        }



    }
}
