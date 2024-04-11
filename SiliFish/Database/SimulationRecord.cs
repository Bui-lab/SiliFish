using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SiliFish.Database.SFDataContext;

namespace SiliFish.Database
{
    [PrimaryKey(nameof(Id))]
    public class SimulationRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ModelID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string RunParam { get; set; }
        public List<CellRecord> Cells { get; }

        public SimulationRecord() { }//required for SQLite
        public SimulationRecord(int modelID, DateTime start, DateTime end, string description, string runparam)
        {
            ModelID = modelID;
            Start = start;
            End = end;
            Description = description;
            RunParam = runparam;
            Cells = [];
        }
    }
}
