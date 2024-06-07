using SiliFish.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Database
{
    public class SimulationDBLink(int simulationID, string dbName)
    {
        public readonly int SimulationID = simulationID;
        public readonly string DatabaseName = dbName;
        public DatabaseDumper DatabaseDumper { get; set; } = new(dbName);
        public int RollingWindow { get; internal set; }
        public int WindowMultiplier { get; internal set; }
    }
}
