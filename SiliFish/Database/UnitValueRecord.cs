using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiliFish.Database
{
    [PrimaryKey(nameof(Id))]
    //TODO slower - don't include for temp table [Microsoft.EntityFrameworkCore.Index(nameof(ValueType), nameof(TimeIndex))]
    public class UnitValueRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Unit")]
        public int UnitID { get; set; }
        public UnitRecord Unit { get; set; }
        public string ValueType { get; set; }
        public int TimeIndex { get; set; }
        public double Value { get; set; }
        public UnitValueRecord() { }
        public UnitValueRecord(int coreID, string valueType, int timeIndex, double value) 
        {
            UnitID = coreID;
            ValueType = valueType;
            TimeIndex = timeIndex;
            Value = value;
        }
    }
}
