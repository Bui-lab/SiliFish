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
    public DbSet<ModelRecord> Models { get; set; }
    public DbSet<CellRecord> Cells { get; set; }
    public DbSet<CellValueRecord> Values { get; set; }
    public DbSet<SpikeRecord> Spikes { get; set; }
    public DbSet<EpisodeRecord> Episodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite($"Data Source={DbFileName}");
    }

    public SFDataContext(bool temp = false)
    {
        if (temp)
        {
            DbFileName = $"{GlobalSettings.TempFolder}\\{FileUtil.GetUniqueFileName()}.sqlite";
            GlobalSettings.TempFiles.Add(DbFileName);//to ensure deletion at the end
        }
    }

    public SFDataContext(string dbName)
    {
        DbFileName = dbName;
    }
}
