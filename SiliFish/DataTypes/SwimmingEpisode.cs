using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.DataTypes
{
    public struct Beat
    {
        public SagittalPlane Direction;
        public double BeatStart, BeatEnd;
    }
    public class SwimmingEpisode
    {
        private readonly double episodeStart;
        private double episodeEnd;
        double lastBeatStart = -1;
        SagittalPlane lastbeatDirection;
        public bool InBeat { get { return lastBeatStart >= 0; } }
        List<Beat> beats;
        public List<Beat> Beats { get => beats; }
        public List<Beat> InlierBeats
        {
            get
            {
                double n_1 = beats.Count - 1;
                if (n_1 > 1)
                {
                    double avgDuration = beats.Average(b => b.BeatEnd - b.BeatStart);
                    double sumOfSquares = beats.Sum(b => (Math.Pow((b.BeatEnd - b.BeatStart - avgDuration), 2)));
                    double stdDev = Math.Sqrt(sumOfSquares / n_1);
                    double stdError = stdDev / Math.Sqrt(beats.Count);
                    double rangeMin = avgDuration - 1.96 * stdError;
                    double rangeMax = avgDuration + 1.96 * stdError;
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
        }

        public int NumOfBeats { get { return beats?.Count ?? 0; } }

        public double[] InlierInstantFequency { get { return InlierBeats?.Select(b =>b.BeatEnd > b.BeatStart? 1000 / (b.BeatEnd - b.BeatStart) : 0).ToArray(); } }
        public double[] InstantFequency { get { return beats?.Select(b => b.BeatEnd > b.BeatStart? 1000 / (b.BeatEnd - b.BeatStart) : 0).ToArray(); } }
        public double BeatFrequency { get { return EpisodeDuration > 0 ? 1000 * NumOfBeats / EpisodeDuration : 0; } }
        public double EpisodeDuration { get { return episodeEnd - episodeStart; } }

        public double Start { get { return episodeStart; } }
        public double End { get { return episodeEnd; } }

        public static List<SwimmingEpisode> GenerateEpisodes(double[] TimeArray, List<int> leftIndices, List<int> rightIndices, double episodeBreak)
        {
            List<SwimmingEpisode> episodes = new();
            bool inEpisode = false;
            SwimmingEpisode episode = null;
            double last_t = -1;
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
                    }
                }

                if (!inEpisode)
                {
                    inEpisode = true;
                    episode = new(t);
                    episode.StartBeat(t, direction);
                    episode.EndBeat(beat_end);
                    episodes.Add(episode);
                    
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
                        episode.StartBeat(t, direction);
                        episode.EndBeat(beat_end);
                    }
                }
                last_t = beat_end;
            }
            if (inEpisode)//last episode is not completed
            {
                episodes.Last().EndEpisode(last_t);
            }
            return episodes;
        }
    }
}
