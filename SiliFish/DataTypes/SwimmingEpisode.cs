﻿using System.Collections.Generic;

namespace SiliFish.DataTypes
{
    public class SwimmingEpisode
    {
        private readonly double episodeStart;
        private double episodeEnd;
        double lastBeatStart = -1;
        List<(double beatStart, double beatEnd)> beats;
        public SwimmingEpisode(double s)
        {
            episodeStart = s;
            beats = new();
            lastBeatStart = s;
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

        public int NumOfBeats { get { return beats.Count; } }
        public double EpisodeDuration { get { return episodeEnd - episodeStart; } }
    }
}
