using OfficeOpenXml.ConditionalFormatting.Contracts;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SiliFish.DynamicUnits.Firing
{

    /// <summary>
    /// The class that is used to show how a cell behaves to a certain stimulus
    /// </summary>
    public class DynamicsStats
    {
        private KinemParam kinemParams;
        private double chatteringIrregularity = 0.1;
        private double oneClusterMultiplier = 2;
        private double tonicPadding = 1;

        private double dt;

        public Cluster BurstCluster;
        public Cluster InterBurstCluster;

        private Dictionary<double, double> intervals = null;
        private Dictionary<double, (double Freq, double End)> spikeFreqs = null;
        private Dictionary<double, (double Freq, double End)> burstFreqs = null;
        private bool analyzed = false;
        private bool followedByQuiescence;
        private bool decreasingIntervals;
        private bool increasingIntervals;
        public double Irregularity { get; set; }

        public double MaxBurstInterval_LowerRange { get; set; } //in ms, the maximum interval two spikes can have to be considered as part of a burst
        public double MaxBurstInterval_UpperRange { get; set; } //in ms, the maximum interval two spikes can have to be considered as part of a burst
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
        public Dictionary<string, double[]> SecLists;
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
                            intervals.Add(dt * SpikeList[i], dt * (SpikeList[i + 1] - SpikeList[i]));
                    }
                }
                return intervals;
            }
        }

        public Dictionary<double, (double Freq, double End)> SpikingFrequency 
        {
            get
            {
                if (spikeFreqs == null)
                {
                    spikeFreqs = new();
                    if (SpikeList.Count > 1)
                    {
                        int iStart = 0;
                        int iEnd = 1;
                        while (iEnd <= SpikeList.Count - 1)
                        {
                            while (dt * (SpikeList[iEnd] - SpikeList[iEnd - 1]) < kinemParams.SpikeBreak)
                            {
                                iEnd++;
                                if (iEnd == SpikeList.Count)
                                    break;
                            }
                            if (iEnd == SpikeList.Count && iEnd - 1 == iStart)
                                break;
                            double start = SpikeList[iStart] * dt;
                            double end = SpikeList[iEnd - 1] * dt;
                            if (end > start)
                                spikeFreqs.Add(start, ((iEnd - 1 - iStart) * 1000 / (end - start), end));
                            iStart = iEnd;
                            iEnd++;
                        }
                        if (!spikeFreqs.Any())
                        {
                            spikeFreqs.Add(SpikeList[0] * dt, ((double)SpikeList.Count * 1000 / ((SpikeList[^1] - SpikeList[0]) * dt), SpikeList[^1] * dt));
                        }
                    }

                }
                return spikeFreqs;
            }
        }
        public Dictionary<double, (double Freq, double End)> BurstingFrequency
        {
            get
            {
                if (burstFreqs == null)
                {
                    burstFreqs = new();
                    if (BurstsOrSpikes.Count > 1)
                    {
                        int iStart = 0;
                        int iEnd = 1;
                        while (iEnd <= BurstsOrSpikes.Count - 1)
                        {
                            while ((BurstsOrSpikes[iEnd].Start - BurstsOrSpikes[iEnd - 1].End) < kinemParams.EpisodeBreak)
                            {
                                iEnd++;
                                if (iEnd == BurstsOrSpikes.Count)
                                    break;
                            }
                            if (iEnd == BurstsOrSpikes.Count && iEnd - 1 == iStart)
                                break;
                            double start = BurstsOrSpikes[iStart].Start;
                            double end = BurstsOrSpikes[iEnd - 1].End;
                            if (end > start)
                                burstFreqs.Add(start, ((iEnd - 1 - iStart) * 1000 / (end - start), end));
                            iStart = iEnd;
                            iEnd++;
                        }
                        if (!burstFreqs.Any())
                        {
                            burstFreqs.Add(BurstsOrSpikes[0].Start, ((double)BurstsOrSpikes.Count * 1000 / ((BurstsOrSpikes[^1].End - BurstsOrSpikes[0].Start)), BurstsOrSpikes[^1].End));
                        }
                    }

                }
                return burstFreqs;
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
            int firstSpikeIndex = ignoreDelay ? firstIIndex : SpikeList.First();
            return (double)(lastSpikeIndex - firstSpikeIndex) / (lastIIndex - firstIIndex);
        }
        public DynamicsStats()
        { }
        public DynamicsStats(KinemParam settings, double[] stimulus, double dt)
        {
            kinemParams = settings ?? new KinemParam();//use default values
            chatteringIrregularity = kinemParams.ChatteringIrregularity;
            oneClusterMultiplier = kinemParams.OneClusterMultiplier;
            tonicPadding = kinemParams.TonicPadding;
            MaxBurstInterval_LowerRange = kinemParams.MaxBurstInterval_DefaultLowerRange;
            MaxBurstInterval_UpperRange = kinemParams.MaxBurstInterval_DefaultUpperRange;

            this.dt = dt;
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

        public DynamicsStats(KinemParam settings, double[] V, double dt, double Vthreshold)
        {
            kinemParams = settings ?? new KinemParam();//use default values
            chatteringIrregularity = kinemParams.ChatteringIrregularity;
            oneClusterMultiplier = kinemParams.OneClusterMultiplier;
            tonicPadding = kinemParams.TonicPadding;
            MaxBurstInterval_LowerRange = kinemParams.MaxBurstInterval_DefaultLowerRange;
            MaxBurstInterval_UpperRange = kinemParams.MaxBurstInterval_DefaultUpperRange;

            this.dt = dt;
            StimulusArray = null;
            VList = V;
            SecLists = new Dictionary<string, double[]>();
            TauDecay = new();
            TauRise = new();
            SpikeList = new();
            spikeDelay = double.NaN;
            CreateSpikeList(Vthreshold);
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
            if (centroid2 < centroid1 * oneClusterMultiplier)//single cluster
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
            if (InterBurstCluster.centroid < BurstCluster.centroid * oneClusterMultiplier ||
                BurstCluster.clusterMax * BurstCluster.clusterMax / BurstCluster.clusterMin > InterBurstCluster.centroid)//single cluster
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
                firingPattern = Irregularity > chatteringIrregularity ? FiringPattern.Chattering : FiringPattern.Spiking;
                return;
            }
            firingPattern = FiringPattern.Mixed;
            return;
        }

        public void CreateSpikeList(double Vthreshold)
        {
            if (analyzed) return;
            SpikeList.Clear();
            foreach (int i in Enumerable.Range(1, VList.Length - 2))
            {
                if (VList[i] > Vthreshold && VList[i] > VList[i - 1] && VList[i] > VList[i + 1])
                    SpikeList.Add(i);
            }
        }
        public void DefineSpikingPattern()
        {
            if (analyzed) return;
            burstsOrSpikes = new();
            int stimulusStart = StimulusArray?.ToList().FindIndex(i => i > 0) ?? 0;
            int stimulusEnd = StimulusArray?.ToList().FindLastIndex(i => i > 0) ?? int.MaxValue;

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
            double quiescence = tonicPadding;
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

            burstsOrSpikes = BurstOrSpike.SpikesToBursts(kinemParams, dt, SpikeList, out double lastInterval);
            if (double.IsNaN(lastInterval))
                lastInterval = quiescence;
            followedByQuiescence = lastStimulusTime - lastSpikeTime >= lastInterval + tonicPadding;
            SetFiringPatternOfList();
            analyzed = true;
        }



    }
}
