using Microsoft.EntityFrameworkCore;
using SiliFish.Definitions;
using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SiliFish.Database;

public class SFDataContext: DbContext
{
    public string DbFileName { get; set; } = $"{GlobalSettings.OutputFolder}//SiliFish.sqlite";
    public DbSet<SimulationRecord> Simulations { get; set; }
    public DbSet<CoreRecord> Cores { get; set; }
    public DbSet<CoreValueRecord> Values { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite($"Data Source={DbFileName}");
    }

    public override int SaveChanges()
    {
        Database.EnsureCreated();
        return base.SaveChanges();
    }
    public SFDataContext(bool temp = false)
    {
        if (temp)
        {
            DbFileName = $"{GlobalSettings.TempFolder}\\{FileUtil.GetUniqueFileName()}.sqlite";
        }
    }
}
