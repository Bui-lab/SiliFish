using System;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Parameters
{
    public class RunParam
    {
        public int tSkip_ms { get; set; } = 0;
        public int tMax { get; set; } = 1000;
        public double dt { get; set; } = 0.1;//The step size
        public double dtEuler { get; set; } = 0.01;//The step size for Euler method

        public int iIndex(double t)
        {
            int i = (int)((t + tSkip_ms) / dt);
            if (i < 0) i = 0;
            if (i >= iMax) i = iMax - 1;
            return i;
        }
        [JsonIgnore]
        public int iMax { get { return Convert.ToInt32((tMax + tSkip_ms) / dt + 1); } }
        [JsonIgnore]
        public static double static_Skip { get; set; } = 0;//Used from data structures that don't have direct access to the model
        [JsonIgnore]
        public static double static_dt { get; set; } = 0.1;//Used from data structures that don't have direct access to the model
        [JsonIgnore]
        public static double static_dt_Euler { get; set; } = 0.1;//Used by the Euler method differential equation solutions

        public static double GetTimeOfIndex(int index)
        { return Math.Round(static_dt * index - static_Skip, 2); }
        public RunParam() { }
    }
}
