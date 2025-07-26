using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiliFish.Database
{
    [Table("CellValues")]
    [PrimaryKey(nameof(Id))]
    //slows down drastically - [Microsoft.EntityFrameworkCore.Index(nameof(ValueType), nameof(TimeIndex))]
    public class CellValueRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CellID { get; set; }
        public string ValueType { get; set; }
        public double Time { get; set; }
        public double Value { get; set; }
        public CellValueRecord() { }//required for SQLite
        public CellValueRecord(int cellID, string valueType, double time, double value) 
        {
            CellID = cellID;
            ValueType = valueType;
            Time = time;
            Value = value;
        }

    }
}
