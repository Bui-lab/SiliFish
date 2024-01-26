using System.ComponentModel.DataAnnotations;

namespace SiliFish.Database
{
    public class CoreValueRecord
    {
        [Key]
        public int CoreValueId { get; set; }
        public int CoreID { get; set; }
        public string ValueType { get; set; }
        public int TimeIndex { get; set; }
        public double Value { get; set; }
    }
}
