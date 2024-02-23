using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace SiliFish.Database
{
    [PrimaryKey(nameof(Id))]
    public class UnitRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SimulationId { get; set; }
        public SimulationRecord Simulation { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Json { get; set; }
        public ICollection<UnitValueRecord> CoreValues { get; set; } = [];

        public UnitRecord() { }
        public UnitRecord(int simulationID, string type, string name, string json) 
        { 
            SimulationId = simulationID;
            Type = type;
            Name = name;
            Json = json;
        }
    }
}
