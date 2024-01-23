using Microsoft.EntityFrameworkCore;
using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SiliFish.Database;

internal class DataContext: DbContext
{
    public string MainDB { get; set; } = $"{GlobalSettings.OutputFolder}//SiliFish.DB";
    public DbSet<SimulationRecord> Simulations { get; set; }
    public DbSet<CoreRecord> Cores { get; set; }
    public DbSet<CoreValue> Values { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite(MainDB);
    }

    public class SimulationRecord
    {
        public int SimulationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ModelJson { get; set; } = string.Empty;

    }

    public class CoreRecord
    {
        public int CoreId { get; set; }
        public int SimulationId { get; set; }
        public string CoreName { get; set; }
        public string CoreJson { get; set; }
    }

    public class CoreValue
    {
        public int CoreValueId { get; set; }
        public int CoreID { get; set; }
        public string ValueType { get; set; }
        public int TimeIndex { get; set; }
        public double Value { get; set; }

    }

    public class SynapseRecord
    {
        public int SynapseId { get; set; }
        public int CellId { get; set; }
        public string Name { get; set; }
    }

    public class SynapseValue
    {
        public int SynapseValueId { get; set; }
        public int SynapseID { get; set; }
        public string ValueType { get; set; }
        public int TimeIndex { get; set; }
        public double Value { get; set; }

    }

}
