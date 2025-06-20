using SiliFish.DataTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiliFish.Database
{
    public class RollingTBFRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SimulationId { get; set; }
        public double Time { get; set; }
        public double TBF { get; set; }

        public RollingTBFRecord() { }//required for SQLite
        public RollingTBFRecord(int simulationID, double time, double tbf)
        {
            SimulationId = simulationID;
            Time = time;
            TBF = tbf;
        }
    }
}
