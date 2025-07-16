using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SiliFish.Services.Dynamics
{

    /// <summary>
    /// The class that is used to show how a cell behaves to a certain stimulus
    /// </summary>
    public class DynamicsStats
    {
        public static DynamicsParam DynamicsParams;
        private double chatteringIrregularity = 0.1;
        private double oneClusterMultiplier = 2;
        private double tonicPadding = 1;
        private int analysisStart = 0;
        private int analysisEnd = int.MaxValue;

        private double dt;
        public double DeltaT { get => dt; }

        public Cluster BurstCluster;
        public Cluster InterBurstCluster;

        private Dictionary<double, double> intervals = null;
        private Dictionary<double, (double Freq, double End)> spikeFreqs = null;
        private Dictionary<double, (double Freq, double End)> burstFreqs = null;
        private bool tausCalculated = false;
        public bool SpikesCreated = false;
        private bool patternized = false;
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
                    intervals = [];
                    if (SpikeList.Count > 1)
                    {
                        foreach (int i in Enumerable.Range(0, SpikeList.Count - 1))
                            intervals.Add(dt * SpikeList[i], dt * (SpikeList[i + 1] - SpikeList[i]));
                    }
                }
                return intervals;
            }
        }

        public Dictionary<double, (double Freq, double End)> SpikeFrequency_Grouped
        {
            get
            {
                if (spikeFreqs == null)
                {
                    spikeFreqs = [];
                    if (SpikeList.Count > 1)
                    {
                        int iStart = 0;
                        int iEnd = 1;
                        while (iEnd <= SpikeList.Count - 1)
                        {
                            while (dt * (SpikeList[iEnd] - SpikeList[iEnd - 1]) < DynamicsParams.SpikeBreak)
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
                        if (spikeFreqs.Count == 0)
                        {
                            spikeFreqs.Add(SpikeList[0] * dt, ((double)SpikeList.Count * 1000 / ((SpikeList[^1] - SpikeList[0]) * dt), SpikeList[^1] * dt));
                        }
                    }

                }
                return spikeFreqs;
            }
        }
        public double SpikeFrequency_Overall
        {
            get
            {
                if (SpikeList.Count > 1)
                {
                    double start = SpikeList[0] * dt;
                    double end = SpikeList[^1] * dt;
                    return SpikeList.Count * 1000 / (end - start);
                }
                return 0;
            }
        }
        public Dictionary<double, (double Freq, double End)> BurstingFrequency_Grouped
        {
            get
            {
                if (burstFreqs == null)
                {
                    burstFreqs = [];
                    if (BurstsOrSpikes.Count > 1)
                    {
                        int iStart = 0;
                        int iEnd = 1;
                        while (iEnd <= BurstsOrSpikes.Count - 1)
                        {
                            while (BurstsOrSpikes[iEnd].Start - BurstsOrSpikes[iEnd - 1].End < DynamicsParams.BurstBreak)
                            {
                                iEnd++;
                                if (iEnd == BurstsOrSpikes.Count)
                                    break;
                            }
                            if (iEnd == BurstsOrSpikes.Count && iEnd - 1 == iStart)
                                break;
                            double start = BurstsOrSpikes[iStart].Start;
                            double end = iEnd < BurstsOrSpikes.Count? 
                                BurstsOrSpikes[iEnd].Start: 
                                BurstsOrSpikes[iEnd - 1].End;
                            if (end > start)
                                burstFreqs.Add(start, ((iEnd - iStart) * 1000 / (end - start), end));
                            iStart = iEnd;
                            iEnd++;
                        }
                        if (burstFreqs.Count == 0)
                        {
                            burstFreqs.Add(BurstsOrSpikes[0].Start, ((double)BurstsOrSpikes.Count * 1000 / (BurstsOrSpikes[^1].End - BurstsOrSpikes[0].Start), BurstsOrSpikes[^1].End));
                        }
                    }

                }
                return burstFreqs;
            }
        }

        public double BurstingFrequency_Overall
        {
            get
            {
                if (BurstsOrSpikes.Count > 1)
                {
                    double start = BurstsOrSpikes[0].SpikeTimeList[0];
                    double end = BurstsOrSpikes[^1].SpikeTimeList[^1];
                    return BurstsOrSpikes.Count * 1000 / (end - start);
                }
                return 0;
            }
        }

        private List<BurstOrSpike> burstsOrSpikes;
        public List<BurstOrSpike> BurstsOrSpikes
        {
            get
            {
                if (!patternized)
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
            if (SpikeList.Count == 0)
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
        public DynamicsStats(DynamicsParam settings, double[] stimulus, double dt)
        {
            if (settings != null)
                DynamicsParams = settings;
            else
                DynamicsParams ??= new DynamicsParam();//use default values
            chatteringIrregularity = DynamicsParams.ChatteringIrregularity;
            oneClusterMultiplier = DynamicsParams.OneClusterMultiplier;
            tonicPadding = DynamicsParams.TonicPadding;
            MaxBurstInterval_LowerRange = DynamicsParams.MaxBurstInterval_DefaultLowerRange;
            MaxBurstInterval_UpperRange = DynamicsParams.MaxBurstInterval_DefaultUpperRange;

            this.dt = dt;
            int iMax = stimulus.Length;
            StimulusArray = stimulus;
            VList = new double[iMax];
            SecLists = [];
            TauDecay = [];
            TauRise = [];
            SpikeList = [];
            spikeDelay = double.NaN;
            patternized = false;
        }

        public DynamicsStats(DynamicsParam settings, double[] V, double dt, double VSpikeThreshold, int analysisStart, int analysisEnd)
        {
            DynamicsParams = settings != null ? settings.Clone() : new DynamicsParam();//if null use default values; cloned to prevent changes to the settings
            chatteringIrregularity = DynamicsParams.ChatteringIrregularity;
            oneClusterMultiplier = DynamicsParams.OneClusterMultiplier;
            tonicPadding = DynamicsParams.TonicPadding;
            MaxBurstInterval_LowerRange = DynamicsParams.MaxBurstInterval_DefaultLowerRange;
            MaxBurstInterval_UpperRange = DynamicsParams.MaxBurstInterval_DefaultUpperRange;

            this.dt = dt;
            StimulusArray = null;
            VList = V;
            SecLists = [];
            TauDecay = [];
            TauRise = [];
            SpikeList = [];
            spikeDelay = double.NaN;
            CreateSpikeList(VSpikeThreshold);
            patternized = false;
            this.analysisStart = analysisStart;
            this.analysisEnd = analysisEnd;
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
            if (centroid2 < centroid1 * oneClusterMultiplier || centroid1 == centroid2)//single cluster
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
            if (InterBurstCluster.centroid < BurstCluster.centroid * oneClusterMultiplier
                ||
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
            if (SpikesCreated) return;
            SpikeList.Clear();
            for (int i = 1; i <= VList.Length - 2; i++)
            {
                if (VList[i] > Vthreshold && VList[i] > VList[i - 1] && VList[i] > VList[i + 1])
                {
                    SpikeList.Add(i);
                    while (VList[i] > Vthreshold && i <= VList.Length - 2)
                        i++;
                }
            }
            SpikesCreated = true;
        }

        public void CalculateTaus(CellCore cellCore, double deltaT)
        {
            if (tausCalculated) return;
            CreateSpikeList(cellCore.Vthreshold);
            bool onRise = false, tauRiseSet = false, onDecay = false, tauDecaySet = false;
            double decayStart = 0, riseStart = 0;
            TauDecay.Clear();
            TauRise.Clear();
            bool spike;
            double V;
            double Vstart = cellCore.Vr;
            int iMax = VList.Length;
            for (int tIndex = 0; tIndex < iMax; tIndex++)
            {
                V = VList[tIndex];
                spike = SpikeList.Contains(tIndex);
                //if passed the 0.37 of the drop (the difference between Vmax and Vreset (or c)): 
                //V <= Vmax - 0.37 * (Vmax - Vreset) => V <= 0.63 Vmax + 0.37 Vreset
                if (onDecay && !tauDecaySet && V <= 0.63 * cellCore.Vmax + 0.37 * cellCore.Vreset)
                {
                    TauDecay.Add(deltaT * tIndex, deltaT * (tIndex - decayStart));
                    tauDecaySet = true;
                }
                //if passed the 0.63 of the rise (the difference between between Vmax and Vr): 
                //V >= 0.63 * (Vmax - Vr) + Vr => V >= 0.63 Vmax + 0.37 Vr
                //Vstart is used instead of Vr
                else if (onRise && !tauRiseSet && riseStart > 0 && V >= 0.63 * cellCore.Vmax + 0.37 * Vstart)
                {
                    TauRise.Add(deltaT * tIndex, deltaT * (tIndex - riseStart));
                    tauRiseSet = true;
                    riseStart = 0;
                }
                else if (!onRise && tIndex > 0 && V > VList[tIndex - 1] + GlobalSettings.VNoise && V - cellCore.Vr > GlobalSettings.VNoise)//Vr is used instead of Vt
                {
                    onRise = true;
                    Vstart = V;
                    tauRiseSet = false;
                    riseStart = tIndex;
                }
                else if (onDecay && tIndex > 0 && V > VList[tIndex - 1])
                {
                    onDecay = false;
                    tauDecaySet = false;
                }
                if (spike)
                {
                    onRise = false;
                    tauRiseSet = false;
                    onDecay = true;
                    tauDecaySet = false;
                    decayStart = tIndex;
                }
            }
            tausCalculated = true;
        }
        public void DefineSpikingPattern()
        {
            if (patternized) return;
            burstsOrSpikes = [];
            int stimulusStart = StimulusArray?.ToList().FindIndex(i => i > 0) ?? analysisStart;
            int stimulusEnd = StimulusArray?.ToList().FindLastIndex(i => i > 0) ?? analysisEnd;

            PreStimulusSpikeList = SpikeList.Where(s => s < stimulusStart).ToList();
            PostStimulusSpikeList = SpikeList.Where(s => s > stimulusEnd).ToList();
            if (stimulusStart >= 0) //if there is no stimulus - check without removing the spikes before the stimulus
                SpikeList.RemoveAll(s => s < stimulusStart || s > stimulusEnd);
            if (SpikeList.Count == 0)
            {
                firingPattern = FiringPattern.NoSpike;
                patternized = true;
                return;
            }
            double firstStimulusTime = stimulusStart * dt;
            double firstSpikeTime = SpikeList[0] * dt;
            firingDelay = firstSpikeTime - firstStimulusTime;
            double lastStimulusTime = stimulusEnd * dt;
            double lastSpikeTime = SpikeList[^1] * dt;
            double quiescence = tonicPadding;
            if (Intervals_ms.Values.Count != 0)
            {
                quiescence = Intervals_ms.Values.Average();
                if (intervals.Count > 1)
                {
                    decreasingIntervals = increasingIntervals = true;
                    double[] intervalValues = [.. intervals.Values];
                    for (int i = 0; i < intervals.Count - 1; i++)
                    {
                        if (intervalValues[i] < intervalValues[i + 1] - GlobalSettings.Epsilon)
                            decreasingIntervals = false;
                        if (intervalValues[i] > intervalValues[i + 1] + GlobalSettings.Epsilon)
                            increasingIntervals = false;
                        if (!increasingIntervals && !decreasingIntervals)
                            break;
                    }
                }
            }
            followedByQuiescence = lastStimulusTime - lastSpikeTime >= quiescence;


            if (SpikeList.Count == 1)
            {
                firingRhythm = FiringRhythm.Phasic;
                firingPattern = FiringPattern.Spiking;
                patternized = true;
                return;
            }
            bool hasClusters = HasClusters();
            if (!hasClusters)// && !followedByQuiescence && BurstCluster != null)//consider all spikes individually
            {
                MaxBurstInterval_LowerRange = double.MaxValue / 2;//to prevent crashing when epsilon is added
                MaxBurstInterval_UpperRange = double.MaxValue / 2;
            }
            else
            {
                MaxBurstInterval_LowerRange = BurstCluster?.clusterMax ?? 0;
                MaxBurstInterval_UpperRange = InterBurstCluster?.clusterMin ?? 0;
            }

            DynamicsParams.MaxBurstInterval_InstantLowerRange = MaxBurstInterval_LowerRange;
            DynamicsParams.MaxBurstInterval_InstantUpperRange = MaxBurstInterval_UpperRange;

            burstsOrSpikes = BurstOrSpike.SpikesToBursts(DynamicsParams, dt, SpikeList, out double lastInterval);
            if (double.IsNaN(lastInterval))
                lastInterval = quiescence;
            followedByQuiescence = lastStimulusTime - lastSpikeTime >= lastInterval + tonicPadding;
            SetFiringPatternOfList();
            patternized = true;
        }



    }

    public class DynamicsStatsParams
    {
        #region Dynamics - Two Exp Syn
        [Description("The number of spikes that would be considered in calculating the current conductance of a synapse. " +
            "Smaller numbers will lower the sensitivity of the simulation, whicle larger numbers will lower the performance." +
            "Enter 0 or a negative number to consider all of the spikes."),
            DisplayName("Spike Train Spike Count"),
            Category("Dynamics - Two Exp Syn")]
        public int SpikeTrainSpikeCount
        {
            get { return DynamicsStats.DynamicsParams.SpikeTrainSpikeCount; }
            set { DynamicsStats.DynamicsParams.SpikeTrainSpikeCount = value; }
        }

        [Description("To increase performance of the simulation, the spikes earlier than " +
            "ThresholdMultiplier * (TauR + TauDFast + TauDSlow) will be ignored in calculating the current conductance of a synapse."),
            DisplayName("Threshold multiplier"),
            Category("Dynamics - Two Exp Syn")]

        public double ThresholdMultiplier
        {
            get { return DynamicsStats.DynamicsParams.ThresholdMultiplier; }
            set { DynamicsStats.DynamicsParams.ThresholdMultiplier = value; }
        }

        [Description("If set to 'false', the negative currents in excitatory synapses and positive currents in inhibitory synapses will be zeroed out."),
            DisplayName("Allow Reverse Current"),
            Category("Dynamics - Two Exp Syn")]
        public bool AllowReverseCurrent
        {
            get { return DynamicsStats.DynamicsParams.AllowReverseCurrent; }
            set { DynamicsStats.DynamicsParams.AllowReverseCurrent = value; }
        }

        #endregion

        #region Firing Patterns
        [Description("The 'SD/Avg Interval' ratio for a burst sequence to be considered chattering"),
            DisplayName("Chattering Irregularity"),
            Category("Firing Patterns")]
        public double ChatteringIrregularity
        {
            get { return DynamicsStats.DynamicsParams.ChatteringIrregularity; }
            set { DynamicsStats.DynamicsParams.ChatteringIrregularity = value; }
        }

        [Description("In ms, the maximum interval two spikes can have to be considered as part of a burst. " +
            "Used if the intervals between spikes are not increasing with time."),
            DisplayName("Max Burst Interval - no spread"),
            Category("Firing Patterns")]
        public double MaxBurstInterval_DefaultLowerRange
        {
            get { return DynamicsStats.DynamicsParams.MaxBurstInterval_DefaultLowerRange; }
            set { DynamicsStats.DynamicsParams.MaxBurstInterval_DefaultLowerRange = value; }
        }

        [Description("In ms, the maximum interval two spikes can have to be considered as part of a burst. " +
            "Used if the intervals between spikes are increasing with time."),
            DisplayName("Max Burst Interval - spread"),
            Category("Firing Patterns")]
        public double MaxBurstInterval_DefaultUpperRange
        {
            get { return DynamicsStats.DynamicsParams.MaxBurstInterval_DefaultUpperRange; }
            set { DynamicsStats.DynamicsParams.MaxBurstInterval_DefaultUpperRange = value; }
        }


        [Description("Centroid2 (average duration between bursts) < Centroid1 (average duration between spikes) * OneClusterMultiplier " +
            "means there is only one cluster (all spikes are part of a burst)."),
            DisplayName("One Cluster Multiplier"),
            Category("Firing Patterns")]
        public double OneClusterMultiplier
        {
            get { return DynamicsStats.DynamicsParams.OneClusterMultiplier; }
            set { DynamicsStats.DynamicsParams.OneClusterMultiplier = value; }
        }

        [Description("In ms, the range between the last spike and the end of current to be considered as tonic firing."),
            DisplayName("Tonic Padding"),
            Category("Firing Patterns")]
        public double TonicPadding
        {
            get { return DynamicsStats.DynamicsParams.TonicPadding; }
            set { DynamicsStats.DynamicsParams.TonicPadding = value; }
        }

        [Description("In ms. The duration to be used as a break while calculating spiking frequency."),
            DisplayName("Spike Break"),
            Category("Firing Patterns")]
        public double SpikeBreak
        {
            get { return DynamicsStats.DynamicsParams.SpikeBreak; }
            set { DynamicsStats.DynamicsParams.SpikeBreak = value; }
        }

        [Description("In ms. The duration to be used as a break while calculating bursting frequency."),
            DisplayName("Burst Break"),
            Category("Firing Patterns")]
        public int BurstBreak
        {
            get { return DynamicsStats.DynamicsParams.BurstBreak; }
            set { DynamicsStats.DynamicsParams.BurstBreak = value; }
        }

        #endregion
        #region RC Spike Trains
        [Description("In ms. The max duration in between two bursts of successive somites to be considered the same train of bursts."),
            DisplayName("RC Positive Delay"),
            Category("RC Spike Trains")]
        public double RCPositiveDelay
        {
            get { return DynamicsStats.DynamicsParams.RCPositiveDelay; }
            set { DynamicsStats.DynamicsParams.RCPositiveDelay = value; }
        }

        [Description("In ms. The max negative duration in between two bursts of successive somites to be considered the same train of bursts."),
            DisplayName("RC Negative Delay"),
            Category("RC Spike Trains")]
        public double RCNegativeDelay
        {
            get { return DynamicsStats.DynamicsParams.RCNegativeDelay; }
            set { DynamicsStats.DynamicsParams.RCNegativeDelay = value; }
        }
        #endregion

    }

}
