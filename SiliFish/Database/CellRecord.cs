using Microsoft.EntityFrameworkCore;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Cells;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace SiliFish.Database
{
    [PrimaryKey(nameof(Id))]
    public class CellRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SimulationId { get; set; }
        public SimulationRecord Simulation { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Pool { get; set; }
        public string Position { get; set; }
        public string Sagittal { get; set; }
        public int Somite {  get; set; }
        public int Seq {  get; set; }
        public ICollection<CellValueRecord> CoreValues { get; set; } = [];

        public CellRecord() { }//required for SQLite
        public CellRecord(int simulationID, Cell cell) 
        { 
            SimulationId = simulationID;
            Type = cell.GetType().Name;
            Name = cell.ID;
            Pool = cell.CellGroup;
            Position = cell.CellPool.BodyLocation.GetDisplayName();
            Sagittal = cell.PositionLeftRight.GetDisplayName();
            Somite = cell.Somite;
            Seq = cell.Sequence;
        }
    }
}
