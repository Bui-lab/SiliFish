using System.ComponentModel.DataAnnotations;

namespace SiliFish.Database
{
    public class CoreRecord
    {
        [Key]
        public int CoreId { get; set; }
        public int SimulationId { get; set; }
        public string CoreType { get; set; }
        public string CoreName { get; set; }
        public string CoreJson { get; set; }
    }
}
