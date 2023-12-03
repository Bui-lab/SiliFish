using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using static OfficeOpenXml.ExcelErrorValue;

namespace SiliFish.DataTypes
{
    public struct Beat
    {
        public SagittalPlane Direction;
        public double BeatStart, BeatEnd;
    }

    /// <summary>
    /// The list of Beats (direction and beat start/end) of a single ventral root or tail tip
    /// </summary>
    public class SwimmingEpisode
    {
        private readonly double episodeStart;
        private double episodeEnd;
        private bool episodeEnded = false;
        double lastBeatStart = -1;
        SagittalPlane lastbeatDirection;
        public bool EpisodeEnded { get { return episodeEnded; } }
        List<Beat> beats;
        public List<Beat> Beats { get => beats; }
        public List<Beat> InlierBeats
        {
            get
            {
                double n_1 = beats.Count - 1;
                if (n_1 > 1)
                {
                    List<double> beatDurations = beats.Select(b => b.BeatEnd - b.BeatStart).ToList();
                    beatDurations.Sort();
                    double median = beatDurations[beats.Count / 2];
                    double avgDuration = beats.Average(b => b.BeatEnd - b.BeatStart);
                    double sumOfSquares = beats.Sum(b => (Math.Pow((b.BeatEnd - b.BeatStart - avgDuration), 2)));
                    double stdDev = Math.Sqrt(sumOfSquares / n_1);
                    double stdError = stdDev / Math.Sqrt(beats.Count);
                    double rangeMin = avgDuration - 1.96 * stdError;
                    double rangeMax = avgDuration + 1.96 * stdError;
                    if (median < rangeMin || median > rangeMax)//use the mode instead of the mean
                    {
                        rangeMin = median - 1.96 * stdError;
                        rangeMax = median + 1.96 * stdError;
                    }
                    return beats.Where(b => (b.BeatEnd - b.BeatStart) >= rangeMin && (b.BeatEnd - b.BeatStart) <= rangeMax).ToList();
                }
                else return beats;
            }
        }

        public SwimmingEpisode(double start)
        {
            episodeStart = start;
            beats = new();
            lastBeatStart = start;
        }
        public SwimmingEpisode(double start, double end)
        {
            episodeStart = start;
            episodeEnd = end;
            episodeEnded = true;
            beats = new();
            lastBeatStart = start;
        }

        public override string ToString()
        {
            string ret = $"Episode Start - End {episodeStart:0.##}-{episodeEnd:0.##}";
            foreach (Beat b in beats)
            {
                ret += $"\r\n{b.Direction} beat:{b.BeatStart:0.##}-{b.BeatEnd:0.##}";
            }
            return ret;
        }
        public void StartBeat(double start, SagittalPlane direction)
        {
            lastBeatStart = start;
            lastbeatDirection = direction;
        }
        public void EndBeat(double e)
        {
            if (lastBeatStart < 0) return;
            beats.Add(new Beat()
            {
                Direction = lastbeatDirection,
                BeatStart = lastBeatStart,
                BeatEnd = e
            });
            lastBeatStart = -1;
        }

        public void EndEpisode(double e)
        {
            EndBeat(e);
            episodeEnd = e;
            episodeEnded = true;
        }

        public int NumOfBeats
        {
            get
            {
                if (beats != null)
                {
                    int leftBeats = beats.Count(b => b.Direction == SagittalPlane.Left);
                    int rightBeats = beats.Count(b => b.Direction == SagittalPlane.Right);
                    return Math.Max(leftBeats, rightBeats);
                }
                return 0;
            }
        }

        public double[] InlierInstantFequency { get { return InlierBeats?.Select(b => b.BeatEnd > b.BeatStart ? 1000 / (b.BeatEnd - b.BeatStart) : 0).ToArray(); } }
        public double[] InstantFequency { get { return beats?.Select(b => b.BeatEnd > b.BeatStart ? 1000 / (b.BeatEnd - b.BeatStart) : 0).ToArray(); } }
        public double BeatFrequency { get { return EpisodeDuration > 0 ? 1000 * NumOfBeats / EpisodeDuration : 0; } }
        public double EpisodeDuration { get { return episodeEnd - episodeStart; } }

        public double Start { get { return episodeStart; } }
        public double End { get { return episodeEnd; } }

        /// <summary>
        /// The start of a beat:
        ///     If there is a beat before: the mid point between the two indices
        /// The end of a beat:
        ///     If there is a beat after: the mid point between the two indices
        /// </summary>        
        internal void Smooth()
        {
            if (beats.Count <= 1) return;
            for (int i = 0; i < beats.Count - 2; i++)
            {
                double midPoint = (beats[i].BeatEnd + beats[i + 1].BeatStart) / 2;
                beats[i] = new Beat()
                {
                    Direction = beats[i].Direction,
                    BeatStart = beats[i].BeatStart,
                    BeatEnd = midPoint
                };
                beats[i + 1] = new Beat()
                {
                    Direction = beats[i + 1].Direction,
                    BeatStart = midPoint,
                    BeatEnd = beats[i + 1].BeatEnd
                };
            }
        }
        /// <summary>
        /// returns episode start and end times excluding the skip period (after skip, the time is set to 0)
        /// The start of a beat:
        ///     If there is a beat before: the mid point between the two indices
        /// The end of a beat:
        ///     If there is a beat after: the mid point between the two indices
        /// </summary>
        /// <param name="TimeArray"></param>
        /// <param name="leftIndices">indices including the skiped period</param>
        /// <param name="rightIndices">indices including the skiped period</param>
        /// <param name="episodeBreak"></param>
        /// <returns></returns>
        public static SwimmingEpisodes GenerateEpisodes(double[] TimeArray, DynamicsParam settings, double dt, List<int> leftIndices, List<int> rightIndices, double episodeBreak)
        {
            SwimmingEpisodes episodes = new();
            if (!leftIndices.Any() || !rightIndices.Any())
                return episodes;
            bool inEpisode = false;
            SwimmingEpisode episode = null;
            double last_t = -1;
            List<BurstOrSpike> leftBurstOrSpikes = BurstOrSpike.SpikesToBursts(settings, dt, leftIndices, out double _);
            List<BurstOrSpike> rightBurstOrSpikes = BurstOrSpike.SpikesToBursts(settings, dt, rightIndices, out double _);
            leftIndices = leftBurstOrSpikes.Select(b => (int)(b.Center / dt)).ToList();
            rightIndices = rightBurstOrSpikes.Select(b => (int)(b.Center / dt)).ToList();

            //remove all the matching spikes on both sides
            List<int> common = leftIndices.Intersect(rightIndices).ToList();
            leftIndices.RemoveAll(common.Contains);
            rightIndices.RemoveAll(common.Contains);
            List<int> sortedIndices = leftIndices.Union(rightIndices).ToList();
            sortedIndices.Sort();
            int indexOfInd = 0;
            SagittalPlane direction;
            while (indexOfInd < sortedIndices.Count)
            {
                int ind = sortedIndices[indexOfInd++];
                double t = TimeArray[ind];
                double beat_end = t;
                if (leftIndices.Contains(ind))
                {
                    direction = SagittalPlane.Left;
                    while (indexOfInd < sortedIndices.Count)
                    {
                        if (!leftIndices.Contains(sortedIndices[indexOfInd]))
                            break;
                        else
                            beat_end = TimeArray[sortedIndices[indexOfInd++]];
                        if (beat_end - t > episodeBreak)
                        {
                            indexOfInd -= 2;
                            beat_end = TimeArray[sortedIndices[indexOfInd++]];
                            break;
                        }
                    }
                }
                else //rightIndices.Contains(ind)
                {
                    direction = SagittalPlane.Right;
                    while (indexOfInd < sortedIndices.Count)
                    {
                        if (!rightIndices.Contains(sortedIndices[indexOfInd]))
                            break;
                        else
                            beat_end = TimeArray[sortedIndices[indexOfInd++]];
                        if (beat_end - t > episodeBreak)
                        {
                            indexOfInd -= 2;
                            beat_end = TimeArray[sortedIndices[indexOfInd++]];
                            break;
                        }
                    }
                }

                if (!inEpisode)
                {
                    inEpisode = true;
                    episode = new(t);
                    episode.StartBeat(t, direction);
                    episode.EndBeat(beat_end);
                    episodes.AddEpisode(episode);

                }
                else if (last_t > 0)
                {
                    if ((t - last_t) > episodeBreak)
                    {
                        episode.EndEpisode(last_t);
                        inEpisode = true;
                        episode = new(t);
                        episode.StartBeat(t, direction);
                        episode.EndBeat(beat_end);
                        episodes.AddEpisode(episode);
                    }
                    else
                    {
                        episode.StartBeat(t, direction);
                        episode.EndBeat(beat_end);
                    }
                }
                last_t = beat_end;
            }
            episodes.Smooth(last_t);
            return episodes;
        }



        public static SwimmingEpisodes GenerateEpisodes(double[] TimeArray, List<int> spikeIndices, double episodeBreak)
        {
            SwimmingEpisodes episodes = new();
            bool inEpisode = false;
            SwimmingEpisode episode = null;
            double last_t = -1;
            int indexOfInd = 0;
            while (indexOfInd < spikeIndices.Count)
            {
                int ind = spikeIndices[indexOfInd++];
                double t = TimeArray[ind];
                double beat_end = t;
                while (indexOfInd < spikeIndices.Count)
                {
                    beat_end = TimeArray[spikeIndices[indexOfInd++]];
                    if (beat_end - t > episodeBreak)
                    {
                        indexOfInd -= 2;
                        beat_end = TimeArray[spikeIndices[indexOfInd++]];
                        break;
                    }
                }

                if (!inEpisode)
                {
                    inEpisode = true;
                    episode = new(t);
                    episode.StartBeat(t, SagittalPlane.Both);
                    episode.EndBeat(beat_end);
                    episodes.AddEpisode(episode);

                }
                else if (last_t > 0)
                {
                    if ((t - last_t) > episodeBreak)
                    {
                        episode.EndEpisode(last_t);
                        inEpisode = false;
                        episode = null;
                    }
                    else
                    {
                        episode.StartBeat(t, SagittalPlane.Both);
                        episode.EndBeat(beat_end);
                    }
                }
                last_t = beat_end;
            }
            episodes.Smooth(last_t);
            return episodes;
        }
    }
}
