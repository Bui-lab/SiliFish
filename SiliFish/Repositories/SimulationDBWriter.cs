﻿using SiliFish.Database;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using SiliFish.Services.Dynamics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace SiliFish.Repositories
{
    public class SimulationDBWriter(string dbName, Simulator modelSimulator, Action completionAction, Action abortAction)
    {
        private readonly string databaseName = dbName;
        private readonly Simulator modelSimulator = modelSimulator;
        public Action saveCompletionAction = completionAction;
        public Action saveAbortAction = abortAction;
        private double individualProgress = 0;
        private int simulationCount = 0;
        public List<int> SimulationIds { get; set; } = [];
        public List<string> ModelJsons { get; set; } = [];
        public double GetProgress() => Math.Max(0, (SimulationIds.Count - 1 + individualProgress) / simulationCount);
        internal SimulationRecord AddSimulationRecord(SFDataContext dataContext, Simulation simulation)
        {
            try
            {
                RunningModel model = simulation.Model;
                string now = DateTime.Now.ToString("yyMMddHHmmss");
                string modelJson = Path.Combine(Path.GetDirectoryName(dataContext.DbFileName), $"Model{now}") + ".json";
                ModelFile.SaveToJson(modelJson, model);
                string stats = $"{simulation.Model.GetNumberOfCells():n0} cells; {simulation.Model.GetNumberOfJunctions():n0} junctions/synapses";
                ModelRecord modelRecord = new(simulation.Model.ModelName, DateTime.Now, stats, modelJson);
                dataContext.Add(modelRecord);
                dataContext.SaveChanges();
                SimulationRecord sim = new(modelRecord.Id,
                                           simulation.Start,
                                           simulation.End,
                                           $"{simulation.Description} {modelSimulator.Description}",
                                           modelSimulator.RunParamDescription,
                                           model.KinemParam.Description);
                dataContext.Add(sim);
                dataContext.SaveChanges();
                SimulationIds.Add(sim.Id);
                ModelJsons.Add(modelJson);
                return sim;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }

        internal void AddEpisodeAndSpikeRecords(SFDataContext dataContext, Simulation simulation, int simRecordId)
        {
            try
            {
                RunningModel model = simulation.Model;

                SwimmingEpisodes episodes = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(simulation);
                int episodeCounter = 1;
                foreach (SwimmingEpisode episode in episodes.Episodes)
                {
                    EpisodeRecord episodeRecord = new(episodeCounter++, simRecordId, episode);
                    dataContext.Add(episodeRecord);

                    for (int i = 0; i < episode.RollingBeatFrequency.Keys.Length; i++)
                    {
                        double key = episode.RollingBeatFrequency.Keys[i];
                        double value = episode.RollingBeatFrequency.Values[i];
                        if ((key is double.NaN) || (value is double.NaN))
                            continue;
                        RollingTBFRecord rollingTBFRecord = new(simRecordId, key, value);
                        dataContext.Add(rollingTBFRecord);
                    }
                }
                dataContext.SaveChanges();

                double[] y_axis = episodes.TailTipCoordinates.Select(coordinate=>coordinate.X).ToArray();
                int minLength = Math.Min(model.TimeArray.Length, y_axis.Length);
                for (int i = 0; i < minLength; i++)
                {
                    TailMovementRecord tailMovementRecord = new(simRecordId, model.TimeArray[i], y_axis[i]);
                    dataContext.Add(tailMovementRecord);
                }
                dataContext.SaveChanges();


                int cellCounter = 0;
                int cellCount = simulation.Model.GetCells().Count;
                foreach (Cell cell in simulation.Model.GetCells())
                {
                    CellRecord unitRecord = new(simRecordId, cell);
                    dataContext.Add(unitRecord);
                    dataContext.SaveChanges();
                    cell.DbId = unitRecord.Id;
                    foreach (int spikeIndex in cell.GetSpikeIndices())
                    {
                        SpikeRecord spikeRecord = new(simRecordId, cell.DbId, simulation.RunParam.GetTimeOfIndex(spikeIndex));
                        dataContext.Add(spikeRecord);
                    }
                    if (GlobalSettings.DB_SaveMembranePotential)
                    {
                        for (int i = 0; i < minLength; i++)
                        {
                            CellValueRecord mpRecord = new(cell.DbId, "Membrane Potential", model.TimeArray[i], cell.V[i]);
                            dataContext.Add(mpRecord);
                        }
                    }
                    dataContext.SaveChanges();
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
            try
            {
                SimulationIds = [];
                ModelJsons = [];
                simulationCount = modelSimulator.SimulationList.Count;
                using SFDataContext dataContext = new(databaseName);
                dataContext.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (Simulation simulation in modelSimulator.SimulationList)
                {
                    SimulationRecord simRecord = AddSimulationRecord(dataContext, simulation);
                    AddEpisodeAndSpikeRecords(dataContext, simulation, simRecord.Id);
                }
                dataContext.SaveChanges();
                saveCompletionAction?.Invoke();
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
                saveAbortAction?.Invoke();
            }
        }
        public void Run()
        {
            Thread thread = new(SaveToDB);
            thread.Start();
        }

    }
}
