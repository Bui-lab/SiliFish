using System;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Parameters
{
    public struct RunParam
    {
        public int SkipDuration { get; set; } = 0;
        public int MaxTime { get; set; } = 1000;
        public double DeltaT { get; set; } = 0.1;//The step size
        public double DeltaTEuler { get; set; } = 0.1;//The step size for Euler method

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
        public RunParam() { }
    }
}
