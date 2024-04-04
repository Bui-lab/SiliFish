using Microsoft.EntityFrameworkCore.Diagnostics;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace SiliFish.DataTypes
{
    public enum EpisodeStats { EpisodeDuration, BeatsPerEpisode, BeatFreq, RollingFreq, 
        EpisodeMeanAmplitude, EpisodeMedianAmplitude, EpisodeMaxAmplitude
    }
    public class SwimmingEpisodes
    {
        private List<SwimmingEpisode> episodes = [];
        private Dictionary<string, Coordinate[]> spineCoordinates = [];
        private Dictionary<string, Coordinate[]> dictionary;


        public Coordinate[] TailTipCoordinates => spineCoordinates.Last().Value;
        public int EpisodeCount => episodes.Count;
        public bool HasEpisodes => episodes.Count != 0;

        public List<SwimmingEpisode> Episodes { get => episodes; }

        public SwimmingEpisodes() 
        { 
        }
        public SwimmingEpisodes(Dictionary<string, Coordinate[]> dictionary)
        {
            spineCoordinates = dictionary;
        }

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

        //mode: mean, median, or max
        private (double[] keys, double[] values) GenerateAmplitudePerEpisode(string mode)
        {
            double dt = 0.1; //TODO hardcoded dt
            double[] keys = new double[episodes.Count];
            double[] values = new double[episodes.Count];
            int counter = 0;
            Coordinate[] coordinates = TailTipCoordinates;
            foreach (SwimmingEpisode e in episodes)
            {
                List<int> times = e.Beats.Select(b => (int)(b.BeatPeak / dt)).ToList();
                keys[counter] = e.Start;
                List<double> amplitudes = [];
                foreach (int i in times)
                    amplitudes.Add(Math.Abs(coordinates[i].X));

                if (mode == "median")
                {
                    amplitudes.Sort();
                    values[counter] = amplitudes[(int)(amplitudes.Count / 2)];
                }
                else if (mode == "max")
                    values[counter] = amplitudes.Max();
                else
                    values[counter] = amplitudes.Average();
                counter++;
            }
            return (keys, values);
        }
        private (double[] xValues, double[] yValues) GetXYValues(EpisodeStats stat)
        {
            return stat switch
            {
                EpisodeStats.EpisodeDuration => GenerateEpisodeDurations(),
                EpisodeStats.BeatsPerEpisode => GenerateBeatsPerEpisode(),
                EpisodeStats.BeatFreq => GenerateTBFPerEpisode(),
                EpisodeStats.RollingFreq => GenerateRollingTBFPerEpisode(),
                EpisodeStats.EpisodeMeanAmplitude => GenerateAmplitudePerEpisode("mean"),
                EpisodeStats.EpisodeMedianAmplitude => GenerateAmplitudePerEpisode("median"),
                EpisodeStats.EpisodeMaxAmplitude => GenerateAmplitudePerEpisode("max"),
                _ => (null, null),
            };
        }

        public (double[] xValues, double[] yValues) GetXYValues(EpisodeStats stat, double tStart, double tEnd)
        {
            (double[] xValuesFull, double[] yValuesFull) = GetXYValues(stat);
            int len = xValuesFull.Count(x => x > tStart && x <= tEnd);
            if (len == 0)
               len = xValuesFull.Any(x => x < tStart) ? 1 : 0;
            double[] xValues = new double[len];
            double[] yValues = new double[len];
            int counter = 0;
            for (int i = 0; i < xValuesFull.Length; i++)
            {
                if (xValuesFull[i] < tStart)
                {
                    if (len == 1)//set the last episode information as the first record, in case there is no other
                    {
                        xValues[counter] = xValuesFull[i];
                        yValues[counter] = yValuesFull[i];
                    }
                    continue;
                }
                if (xValuesFull[i] > tEnd) break;
                xValues[counter] = xValuesFull[i];
                yValues[counter++] = yValuesFull[i];
            }
            return (xValues, yValues);
        }


    }
}
