using SiliFish.Database;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Repositories
{
    public static class SimulationDB
    {
        public static void SaveToDB(ModelSimulator modelSimulator)
        {
            SFDataContext sFDataContext = new();
            foreach (Simulation simulation in modelSimulator.SimulationList)
            {
                RunningModel model = simulation.Model;
                if (model.DbId == 0)
                {
                    string stats = $"{simulation.Model.GetNumberOfCells():n0} cells; {simulation.Model.GetNumberOfConnections():n0} connections";
                    ModelRecord modelRecord = new(simulation.Model.ModelName, DateTime.Now, stats);
                    sFDataContext.Add(modelRecord);
                    sFDataContext.SaveChanges();
                    model.DbId = modelRecord.Id;
                }
                SimulationRecord sim = new(model.DbId,
                                           simulation.Start,
                                           simulation.End,
                                           $"{simulation.Description} {modelSimulator.Description}",
                                           modelSimulator.RunParamDescription);
                sFDataContext.Add(sim);
            }
            sFDataContext.SaveChanges();
        }

    }
}
