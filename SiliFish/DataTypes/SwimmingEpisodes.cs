using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.DataTypes
{
    public enum EpisodeStats { EpisodeDuration, BeatsPerEpisode, BeatFreq, InstantFreq, InlierInstantFreq, RollingFreq }
    public class SwimmingEpisodes
    {
        private List<SwimmingEpisode> episodes = [];
        public int EpisodeCount => episodes.Count;
        public bool HasEpisodes => episodes.Count != 0;

        public List<SwimmingEpisode> Episodes { get => episodes; }

        public SwimmingEpisode this[int index]
        {
            get
            {
                if (episodes?.Count > index)
                    return episodes[index];
                return null;
            }
        }
        public void RemoveEpisodes()
        {
            episodes.RemoveAll(e => e.End < 0);
        }
        public void AddEpisode(SwimmingEpisode episode)
        { episodes.Add(episode); }

        public void Smooth(double last_t)
        {
            if (episodes.Count == 0) return;
            episodes.ForEach(e => e.Smooth());
            if (!episodes[^1].EpisodeEnded)
            {
                if (episodes[^1].Beats.Count == 0)
                    episodes.RemoveAt(episodes.Count - 1);
                else
                    episodes[^1].EndEpisode(last_t);
            }
        }

        private (double[] keys, double[] values) GenerateEpisodeDurations()
        {
            return (episodes.Select(e => e.Start).ToArray(),
                episodes.Select(e => e.EpisodeDuration).ToArray());
        }
        private (double[] keys, double[] values) GenerateBeatsPerEpisode()
        {
            return (episodes.Select(e => e.Start).ToArray(),
                episodes.Select(e => (double)e.Beats.Count).ToArray());
        }
        private (double[] keys, double[] values) GenerateTBFPerEpisode()
        {
            return (episodes.Select(e => e.Start).ToArray(),
                episodes.Select(e => e.BeatFrequency).ToArray());
        }
        private (double[] keys, double[] values) GenerateRollingTBFPerEpisode()
        {
            return (episodes.SelectMany(e => e.RollingBeatFrequency.Keys).ToArray(),
              episodes.SelectMany(e => e.RollingBeatFrequency.Values).ToArray());
        }
        private (double[] keys, double[] values) GenerateInstantFrequency()
        {
            return (episodes.Select(e => e.Start).ToArray(),
                episodes.Select(e => e.BeatFrequency).ToArray());
        }
        private (double[] keys, double[] values) GenerateInlierInstantFrequency()
        {
            double[] xValuesFull = episodes.SelectMany(e => e.Beats.Select(b => b.BeatStart)).ToArray();
            double[] yValuesFull = episodes.SelectMany(e => e.InstantFequency).ToArray();
            double median = episodes.SelectMany(e => e.InstantFequency).Order().ToArray()[yValuesFull.Length / 2];
            double avgDuration = yValuesFull.Average();
            double stdDev = yValuesFull.StandardDeviation();
            double stdError = stdDev / Math.Sqrt(yValuesFull.Length);
            double rangeMin = avgDuration - 1.96 * stdError;
            double rangeMax = avgDuration + 1.96 * stdError;
            if (median < rangeMin)
                rangeMin = median - 1.96 * stdError;
            if (median > rangeMax)
                rangeMax = median + 1.96 * stdError;
            List<int> outliers = [];
            for (int i = 0; i < yValuesFull.Length; i++)
            {
                if (yValuesFull[i] < rangeMin || yValuesFull[i] > rangeMax)
                    outliers.Add(i);
            }
            outliers.Reverse();
            List<double> inlierX = [];
            List<double> inlierY = [];
            for (int i = 0; i < yValuesFull.Length; i++)
            {
                if (!outliers.Contains(i))
                {
                    inlierX.Add(xValuesFull[i]);
                    inlierY.Add(yValuesFull[i]);
                }
            }
            return (inlierX.ToArray(), inlierY.ToArray());
        }

        private (double[] xValues, double[] yValues) GetXYValues(EpisodeStats stat)
        {
            return stat switch
            {
                EpisodeStats.EpisodeDuration => GenerateEpisodeDurations(),
                EpisodeStats.BeatsPerEpisode => GenerateBeatsPerEpisode(),
                EpisodeStats.BeatFreq => GenerateTBFPerEpisode(),
                EpisodeStats.InstantFreq => GenerateInstantFrequency(),
                EpisodeStats.InlierInstantFreq => GenerateInlierInstantFrequency(),
                EpisodeStats.RollingFreq => GenerateRollingTBFPerEpisode(),
                _ => (null, null),
            };
        }

        public (double[] xValues, double[] yValues) GetXYValues(EpisodeStats stat, double tStart, double tEnd)
        {
            (double[] xValuesFull, double[] yValuesFull) = GetXYValues(stat);
            int len = xValuesFull.Count(x => x > tStart && x <= tEnd);
            double[] xValues = new double[len];
            double[] yValues = new double[len];
            int counter = 0;
            for (int i = 0; i < xValuesFull.Length; i++)
            {
                if (xValuesFull[i] < tStart) continue;
                if (xValuesFull[i] > tEnd) break;
                xValues[counter] = xValuesFull[i];
                yValues[counter++] = yValuesFull[i];
            }
            return (xValues, yValues);
        }


    }
}
