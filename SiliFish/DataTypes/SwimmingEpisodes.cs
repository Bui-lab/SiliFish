using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.DataTypes
{
    public enum EpisodeStats { EpisodeDuration, BeatsPerEpisode, BeatFreq, RollingFreq, 
        EpisodeMeanAmplitude, EpisodeMedianAmplitude, EpisodeMaxAmplitude
    }
    public class SwimmingEpisodes
    {
        private List<SwimmingEpisode> episodes = [];
        private Dictionary<string, Coordinate[]> spineCoordinates = [];
        private Simulation simulation;

        public Coordinate[] TailTipCoordinates => spineCoordinates.Last().Value;
        public int EpisodeCount => episodes.Count;
        public bool HasEpisodes => episodes.Count != 0;

        public List<SwimmingEpisode> Episodes { get => episodes; }

        public SwimmingEpisodes() 
        { 
        }
        public SwimmingEpisodes(Simulation simulation, Dictionary<string, Coordinate[]> dictionary)
        {
            spineCoordinates = dictionary;
            this.simulation = simulation;
            GenerateEpisodes();
            GenerateEpisodeStats();          

        }
        private void GenerateEpisodes()
        {
            //We will only use the tip of the tail to determine tail beats (if the x coordinate of the tip is smaller (or more negative)
            //than the left bound or if the x coordinate of the tip is greater than the right bound, then detect as a tail beat
            RunningModel model = simulation.Model;
            Coordinate[] tail_tip_coord = TailTipCoordinates;
            double left_bound = -model.KinemParam.Boundary;
            double right_bound = model.KinemParam.Boundary;
            int side = 0;
            const int LEFT = -1;
            const int RIGHT = 1;
            int nMax = model.TimeArray.Length;
            double dt = simulation.RunParam.DeltaT;
            double offset = simulation.RunParam.SkipDuration;
            int delay = (int)(model.KinemParam.EpisodeBreak / dt);
            SwimmingEpisode lastEpisode = null;
            int i = (int)(offset / dt);
            int beat_peak = -1;
            while (i < nMax)
            {
                int iMax = Math.Min(i + delay, nMax);
                Coordinate[] window = tail_tip_coord[i..iMax];
                double t = model.TimeArray[i];
                if (!window.Any(coor => coor.X < left_bound || coor.X > right_bound))
                {
                    side = 0;
                    lastEpisode?.EndEpisode(t, model.TimeArray[beat_peak]);
                    lastEpisode = null;
                    i = iMax;
                    continue;
                }

                if (lastEpisode == null)
                {
                    if (tail_tip_coord[i].X < left_bound)//beginning an episode on the left
                    {
                        lastEpisode = new SwimmingEpisode(t);
                        beat_peak = i;
                        AddEpisode(lastEpisode);
                        side = LEFT;
                    }
                    else if (tail_tip_coord[i].X > right_bound) //beginning an episode on the right
                    {
                        lastEpisode = new SwimmingEpisode(t);
                        beat_peak = i;
                        AddEpisode(lastEpisode);
                        side = RIGHT;
                    }
                }
                else // During an episode
                {
                    if (tail_tip_coord[i].X < left_bound && side == RIGHT)
                    {
                        side = LEFT;
                        lastEpisode.EndBeat(t, model.TimeArray[beat_peak]);
                        lastEpisode.StartBeat(t, SagittalPlane.Left);
                    }
                    else if (tail_tip_coord[i].X > right_bound && side == LEFT)
                    {
                        side = RIGHT;
                        lastEpisode.EndBeat(t, model.TimeArray[beat_peak]);
                        lastEpisode.StartBeat(t, SagittalPlane.Right);
                    }
                    else if (side == RIGHT && tail_tip_coord[i].X > tail_tip_coord[beat_peak].X)
                        beat_peak = i;
                    else if (side == LEFT && tail_tip_coord[i].X < tail_tip_coord[beat_peak].X)//negative value
                        beat_peak = i;
                }
                i++;
            }
            Smooth(model.TimeArray[^1]);

        }

        private void GenerateEpisodeStats()
        {
            double dt = simulation.RunParam.DeltaT;
            int counter = 0;
            Coordinate[] coordinates = TailTipCoordinates;
            foreach (SwimmingEpisode e in episodes)
            {
                List<int> times = e.Beats.Select(b => (int)(b.BeatPeak / dt)).ToList();
                List<double> amplitudes = [];
                foreach (int i in times)
                    amplitudes.Add(Math.Abs(coordinates[i].X));
                
                amplitudes.Sort();
                double mean = amplitudes.Average();
                double median = amplitudes[amplitudes.Count / 2];
                double max = amplitudes[^1];//no need to call the Max() anymore, as it is already sorted
                e.SetAmplitude(mean, median, max);
                counter++;
            }
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

        private (double[] keys, double[] values) GetEpisodeDurations()
        {
            return (episodes.Select(e => e.Start).ToArray(),
                episodes.Select(e => e.EpisodeDuration).ToArray());
        }
        private (double[] keys, double[] values) GetBeatsPerEpisode()
        {
            return (episodes.Select(e => e.Start).ToArray(),
                episodes.Select(e => (double)e.Beats.Count).ToArray());
        }
        private (double[] keys, double[] values) GetTBFPerEpisode()
        {
            return (episodes.Select(e => e.Start).ToArray(),
                episodes.Select(e => e.BeatFrequency).ToArray());
        }
        private (double[] keys, double[] values) GetRollingTBFPerEpisode()
        {
            return (episodes.SelectMany(e => e.RollingBeatFrequency.Keys).ToArray(),
              episodes.SelectMany(e => e.RollingBeatFrequency.Values).ToArray());
        }

        private (double[] keys, double[] values) GetAmplitudePerEpisode(string mode)
        {
            double[] values = mode == "median" ? episodes.Select(e => e.MedianAmplitude).ToArray() :
                mode == "max" ? episodes.Select(e => e.MaxAmplitude).ToArray() :
                episodes.Select(e => e.MeanAmplitude).ToArray();
                
            return (episodes.Select(e => e.Start).ToArray(),values);
        }
        private (double[] xValues, double[] yValues) GetXYValues(EpisodeStats stat)
        {
            return stat switch
            {
                EpisodeStats.EpisodeDuration => GetEpisodeDurations(),
                EpisodeStats.BeatsPerEpisode => GetBeatsPerEpisode(),
                EpisodeStats.BeatFreq => GetTBFPerEpisode(),
                EpisodeStats.RollingFreq => GetRollingTBFPerEpisode(),
                EpisodeStats.EpisodeMeanAmplitude => GetAmplitudePerEpisode("mean"),
                EpisodeStats.EpisodeMedianAmplitude => GetAmplitudePerEpisode("median"),
                EpisodeStats.EpisodeMaxAmplitude => GetAmplitudePerEpisode("max"),
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
