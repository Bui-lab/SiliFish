using SiliFish.Database;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Repositories;
using System;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SiliFish.ModelUnits.Parameters
{
    public class RunParam
    {
        public bool TrackJunctionCurrent { get; set; } = true;
        public int SkipDuration { get; set; } = GlobalSettings.SimulationSkipTime;
        public int MaxTime { get; set; } = GlobalSettings.SimulationEndTime;
        public double DeltaT { get; set; } = GlobalSettings.SimulationDeltaT;//The step size
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
