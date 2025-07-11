using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Definitions
{
    public class SimulationSettings
    {
        #region Simulation
        [Description("In ms - the default simulation duration."),
            DisplayName("Simulation Duration"),
            Category("Simulation")]
        public int SimulationEndTime { get; set; } = 1000;

        [Description("In ms - the default duration to skip."),
            DisplayName("Simulation Skip Duration"),
            Category("Simulation")]
        public int SimulationSkipTime { get; set; } = 0;

        [Description("In ms - the default time unit."),
            DisplayName("Default δt"),
            Category("Simulation")]
        public double SimulationDeltaT { get; set; } = 0.1;

        [Description("Whether junction level current tracking is on or off. For larger models turning it off is recommended."),
            DisplayName("Junction level current tracking"),
            Category("Simulation")]
        public bool JunctionLevelTracking { get; set; } = false;

        #endregion

        public RunParam GetRunParam()
        {
            RunParam param = new()
            {
                SkipDuration = SimulationSkipTime,
                MaxTime = SimulationEndTime,
                DeltaT = SimulationDeltaT,
                TrackJunctionCurrent = JunctionLevelTracking
            };
            return param;
        }
    }
}
