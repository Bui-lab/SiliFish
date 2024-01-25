using System;
using System.Collections.Generic;
using static SiliFish.Database.SFDataContext;

namespace SiliFish.Database
{
    public class SimulationRecord
    {
        public int SimulationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ModelJson { get; set; } = string.Empty;
        public List<CoreRecord> Cores { get; } = [];
    }
}
