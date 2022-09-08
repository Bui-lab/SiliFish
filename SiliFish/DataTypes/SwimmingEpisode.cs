using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.DataTypes
{
    public class SwimmingEpisode
    {
        private readonly double episodeStart;
        private double episodeEnd;
        double lastBeatStart = -1;
        public bool InBeat { get { return lastBeatStart >= 0; } }
        List<(double beatStart, double beatEnd)> beats;
        public List<(double beatStart, double beatEnd)> Beats { get => beats; }
        public List<(double beatStart, double beatEnd)> InlierBeats
        { 
            get
            {
                double n_1 = beats.Count - 1;
                if (n_1 > 1)
                {
                    double avgDuration = beats.Average(b => b.beatEnd - b.beatStart);
                    double sumOfSquares = beats.Sum(b => (Math.Pow((b.beatEnd - b.beatStart - avgDuration), 2)));
                    double stdDev = Math.Sqrt(sumOfSquares / n_1);
                    double stdError = stdDev / Math.Sqrt(beats.Count);
                    double rangeMin = avgDuration - 1.96 * stdError;
                    double rangeMax = avgDuration + 1.96 * stdError;
                    return beats.Where(b => (b.beatEnd - b.beatStart) >= rangeMin && (b.beatEnd - b.beatStart) <= rangeMax).ToList();
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
            foreach (var (beatStart, beatEnd) in beats)
            {
                ret += $"\r\nBeat:{beatStart:0.##}-{beatEnd:0.##}";
            }
            return ret;
        }
        public void StartBeat(double start)
        {
            lastBeatStart = start;
        }
        public void EndBeat(double e)
        {
            if (lastBeatStart < 0) return;
            beats.Add((lastBeatStart, e));
            lastBeatStart = -1;
        }

        public void EndEpisode(double e)
        {
            EndBeat(e);
            episodeEnd = e;
        }

        public int NumOfBeats { get { return beats?.Count ?? 0; } }

        public double[] InlierInstantFequency { get { return InlierBeats?.Where(b => b.beatEnd > b.beatStart).Select(b => 1000 / (b.beatEnd - b.beatStart)).ToArray(); } }
        public double[] InstantFequency { get { return beats?.Where(b => b.beatEnd > b.beatStart).Select(b => 1000 / (b.beatEnd - b.beatStart)).ToArray(); } }
        public double BeatFrequency { get { return EpisodeDuration > 0 ? 1000 * NumOfBeats / EpisodeDuration : 0; } }
        public double EpisodeDuration { get { return episodeEnd - episodeStart; } }

        public double Start { get { return episodeStart; } }
        public double End { get { return episodeEnd; } }

    }
}
