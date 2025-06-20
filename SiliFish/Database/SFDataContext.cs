using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.Services;
using System;


namespace SiliFish.Database;

public class SFDataContext: DbContext
{
    private static bool compatibilityChecked = false;
    public string DbFileName { get; set; } = GlobalSettings.DatabaseName;
    public DbSet<SimulationRecord> Simulations { get; set; }
    public DbSet<ModelRecord> Models { get; set; }
    public DbSet<CellRecord> Cells { get; set; }
    public DbSet<CellValueRecord> Values { get; set; }
    public DbSet<SpikeRecord> Spikes { get; set; }
    public DbSet<EpisodeRecord> Episodes { get; set; }
    public DbSet<RollingTBFRecord> RollingTBFs { get; set; }
    public DbSet<TailMovementRecord> TailMovements { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite($"Data Source={DbFileName}");
    }
    public override void Dispose()
    {
        base.Database.CloseConnection();
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    public SFDataContext(bool temp = false)
    {
        if (temp)
        {
            DbFileName = $"{GlobalSettings.TempFolder}\\{FileUtil.GetUniqueFileName()}.sqlite";
            GlobalSettings.AddTempFile(DbFileName);//to ensure deletion at the end
            GlobalSettings.AddTempFile(DbFileName + "-shm");
            GlobalSettings.AddTempFile(DbFileName + "-wal");
        }
        BackwardCompatibility();
    }

    public SFDataContext(string dbName)
    {
        DbFileName = dbName;
        BackwardCompatibility();
    }

    private void BackwardCompatibility()
    {
        try
        {
            if (compatibilityChecked) return;
            //KinemParam to Simulations table are added in version 3.0.5
            using var connection = new SqliteConnection(Database.GetDbConnection().ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "PRAGMA table_info(Simulations);";
            using var reader = command.ExecuteReader();
            bool kinemParamColumnExists = false;
            while (reader.Read())
            {
                if (reader.GetString(reader.GetOrdinal("name")) == "KinemParam")
                {
                    kinemParamColumnExists = true;
                    break;
                }
            }
            reader.Close();
            if (!kinemParamColumnExists)
            {
                command.CommandText = "ALTER TABLE Simulations ADD COLUMN KinemParam TEXT";
                command.ExecuteNonQuery();
            }

            //Check if TailMovement table exists
            command.CommandText = "PRAGMA table_info(TailMovements);";
            using var readerTM = command.ExecuteReader();
            bool tailMovementExists = readerTM.HasRows;
            readerTM.Close();
            if (!tailMovementExists)
            {
                command.CommandText = "CREATE TABLE TailMovements ( " +
                            "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "SimulationID INT NOT NULL," +
                            "Time REAL NOT NULL," +
                            "Amplitude REAL NOT NULL)";
                command.ExecuteNonQuery();
            }

            //Check if RollingTBF table exists
            command.CommandText = "PRAGMA table_info(RollingTBFs);";
            using var readerRT = command.ExecuteReader();
            bool rollingTBFExists = readerRT.HasRows;
            readerRT.Close();
            if (!rollingTBFExists)
            {
                command.CommandText = "CREATE TABLE RollingTBFs ( " +
                            "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "SimulationID INT NOT NULL," +
                            "Time REAL NOT NULL," +
                            "TBF REAL NOT NULL)";
                command.ExecuteNonQuery();
            }
            compatibilityChecked = true;
        }
        catch (Exception ex)
        {
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        }
    }
}
