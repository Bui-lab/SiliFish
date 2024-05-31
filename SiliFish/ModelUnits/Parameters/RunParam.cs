using SiliFish.Definitions;
using SiliFish.Extensions;
using System;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Parameters
{
    public class RunParam
    {
        public bool TrackJunctionCurrent { get; set; } = true;
        public int SkipDuration { get; set; } 
        public int MaxTime { get; set; } 
        public double DeltaT { get; set; }
        public int iIndex(double t)
        {
            int i = (int)((t + SkipDuration) / DeltaT);
            if (i < 0) i = 0;
            if (i >= iMax) i = iMax - 1;
            return i;
        }
        [JsonIgnore]
        public int iMax { get { return Convert.ToInt32((MaxTime + SkipDuration) / DeltaT + 1); } }
        public double GetTimeOfIndex(int index)
        { return Math.Round(DeltaT * index - SkipDuration, 2); }

        [JsonIgnore]
        public string Description => this.GetProperties("; ");
        public RunParam() { }
    }
}
