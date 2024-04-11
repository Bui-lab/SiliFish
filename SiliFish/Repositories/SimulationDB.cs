using SiliFish.Database;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using SiliFish.Services.Dynamics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static OfficeOpenXml.ExcelErrorValue;

namespace SiliFish.Repositories
{
    public static class SimulationDB
    {
        private static void AddSimulationRecord(SFDataContext sFDataContext, ModelSimulator modelSimulator, Simulation simulation)
        {
            try
            {
                RunningModel model = simulation.Model;
                if (model.DbId == 0)
                {
                    string stats = $"{simulation.Model.GetNumberOfCells():n0} cells; {simulation.Model.GetNumberOfConnections():n0} connections";
                    ModelRecord modelRecord = new(simulation.Model.ModelName, DateTime.Now, stats, "");//TODO ability to save the json file and include the filename
                    sFDataContext.Add(modelRecord);
                    sFDataContext.SaveChanges();
                    model.DbId = modelRecord.Id;
                }
                SimulationRecord sim = new(simulation.Model.DbId,
                                           simulation.Start,
                                           simulation.End,
                                           $"{simulation.Description} {modelSimulator.Description}",
                                           modelSimulator.RunParamDescription);
                sFDataContext.Add(sim);
                sFDataContext.SaveChanges();

                SwimmingEpisodes episodes = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(simulation);
                int episodeCounter = 1;
                foreach(SwimmingEpisode episode in episodes.Episodes)
                {
                    EpisodeRecord episodeRecord = new(episodeCounter++, sim.Id, episode);
                    sFDataContext.Add(episodeRecord);
                    sFDataContext.SaveChanges();
                }

                foreach (Cell cell in simulation.Model.GetCells())
                {
                    CellRecord unitRecord = new(sim.Id, cell);
                    sFDataContext.Add(unitRecord);
                    sFDataContext.SaveChanges();
                    cell.DbId = unitRecord.Id;
                    foreach (int spikeIndex in cell.GetSpikeIndices())
                    {
                        SpikeRecord spikeRecord = new(sim.Id, cell.DbId, simulation.RunParam.GetTimeOfIndex(spikeIndex));
                        sFDataContext.Add(spikeRecord);
                    }
                    sFDataContext.SaveChanges();
                }
            }            
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        public static void SaveToDB(ModelSimulator modelSimulator)
        {
            SFDataContext sFDataContext = new();
            foreach (Simulation simulation in modelSimulator.SimulationList)
            {
                AddSimulationRecord(sFDataContext, modelSimulator, simulation);
            }
            sFDataContext.SaveChanges();
        }

    }
}
