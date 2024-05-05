using System.ComponentModel.DataAnnotations.Schema;

namespace SiliFish.Database
{
    public class SpikeRecord
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SimulationId { get; set; } 
        public int CellId { get; set; } 
        public double SpikeTime { get; set; } 
        public SpikeRecord() { }//required for SQLite
        public SpikeRecord(int simulationId, int cellId, double spikeTime)
        {
            SimulationId = simulationId;
            CellId = cellId;
            SpikeTime = spikeTime;
        }
    }
}
