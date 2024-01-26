using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SiliFish.Database.SFDataContext;

namespace SiliFish.Database
{
    public class SimulationRecord
    {
        [Key]
        public int SimulationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ModelJson { get; set; } = string.Empty;
        public List<CoreRecord> Cores { get; } = [];
        public SimulationRecord(DateTime start, DateTime end, string description, string modelJson) 
        {
            Start = start;
            End = end;
            Description = description;
            ModelJson = modelJson;
        }
    }
}
