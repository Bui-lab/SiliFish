using SiliFish.DataTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiliFish.Database
{
    public class TailMovementRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SimulationId { get; set; }
        public double Time { get; set; }
        public double Amplitude { get; set; }

        public TailMovementRecord() { }//required for SQLite
        public TailMovementRecord(int simulationID, double time, double amplitude)
        {
            SimulationId = simulationID;
            Time = time;
            Amplitude = amplitude;
        }
    }
}
