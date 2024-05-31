using SiliFish.Database;
using SiliFish.DataTypes;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using SiliFish.Services.Dynamics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SiliFish.Repositories
{
    public class SimulationDBWriter(ModelSimulator modelSimulator, Action completionAction)
    {
        private readonly ModelSimulator modelSimulator = modelSimulator;
        public Action saveCompletionAction = completionAction;
        private double individualProgress = 0;
        private int simulationCount = 0;
        public List<int> SimulationIds { get; set; } = [];
        public double GetProgress() => Math.Max(0, (SimulationIds.Count - 1 + individualProgress) / simulationCount);
        private void AddSimulationRecord(SFDataContext sFDataContext, ModelSimulator modelSimulator, Simulation simulation)
        {
            try
            {
                RunningModel model = simulation.Model;
                if (model.DbId == 0)
                {
                    string stats = $"{simulation.Model.GetNumberOfCells():n0} cells; {simulation.Model.GetNumberOfJunctions():n0} junctions/synapses";
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
                SimulationIds.Add(sim.Id);

                SwimmingEpisodes episodes = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(simulation);
                int episodeCounter = 1;
                foreach(SwimmingEpisode episode in episodes.Episodes)
                {
                    EpisodeRecord episodeRecord = new(episodeCounter++, sim.Id, episode);
                    sFDataContext.Add(episodeRecord);
                }
                sFDataContext.SaveChanges();

                int cellCounter = 0;
                int cellCount = simulation.Model.GetCells().Count;
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
                    individualProgress = (double)++cellCounter / cellCount;
                }
            }            
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void SaveToDB()
        {
            SimulationIds = [];
            simulationCount = modelSimulator.SimulationList.Count;
            using SFDataContext sFDataContext = new();
            sFDataContext.ChangeTracker.AutoDetectChangesEnabled = false;
            foreach (Simulation simulation in modelSimulator.SimulationList)
            {
                AddSimulationRecord(sFDataContext, modelSimulator, simulation);
            }
            sFDataContext.SaveChanges();
            saveCompletionAction?.Invoke();
        }
        public void Run()
        {
            Thread thread = new(SaveToDB);
            thread.Start();
        }

    }
}
