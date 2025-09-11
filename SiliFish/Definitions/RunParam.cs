using SiliFish.Extensions;
using System;
using System.Text.Json.Serialization;

namespace SiliFish.Definitions
{
    public class RunParam
    {
        public bool TrackJunctionCurrent { get; set; } = true;
        public int MaxTime { get; set; }
        public double DeltaT { get; set; }
        public int iIndex(double t)
        {
            int i = (int)(t / DeltaT);
            if (i < 0) i = 0;
            if (i >= iMax) i = iMax - 1;
            return i;
        }
        [JsonIgnore]
        public int iMax { get { return Convert.ToInt32((MaxTime) / DeltaT + 1); } }
        public double GetTimeOfIndex(int index)
        { return Math.Round(DeltaT * index, 2); }

        [JsonIgnore]
        public string Description => this.GetProperties("; ");
        public RunParam() { }
    }
}
