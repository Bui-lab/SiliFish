using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiliFish.Database
{
    [PrimaryKey(nameof(Id))]
    //TODO slower - don't include for temp table [Microsoft.EntityFrameworkCore.Index(nameof(ValueType), nameof(TimeIndex))]
    public class CellValueRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Cell")]
        public int CellID { get; set; }
        public CellRecord Unit { get; set; }
        public string ValueType { get; set; }
        public int TimeIndex { get; set; }
        public double Value { get; set; }
        public CellValueRecord() { }//required for SQLite
        public CellValueRecord(int cellID, string valueType, int timeIndex, double value) 
        {
            CellID = cellID;
            ValueType = valueType;
            TimeIndex = timeIndex;
            Value = value;
        }
    }
}
