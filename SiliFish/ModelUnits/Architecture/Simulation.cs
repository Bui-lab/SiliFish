using GeneticSharp;
using SiliFish.Database;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Repositories;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Architecture
{
    public class DBLink(int simulationID, string dbName)
    {
        public readonly int SimulationID = simulationID;
        public readonly string DBName = dbName;
        public DatabaseDumper DatabaseDumper { get; set; } = new(dbName);
        public int RollingWindow { get; internal set; }
        public int WindowMultiplier { get; internal set; }
    }
    public class Simulation(RunningModel runningModel, RunParam runParam)
    {
        [JsonIgnore]
        public Random randomNumGenerator { get; private set; } = null;
        private int iMax => RunParam.iMax;
        private int iProgress = 0;
        public SimulationState state { get; private set; } = SimulationState.NotStarted;

        [JsonPropertyOrder(2)]
        public RunParam RunParam { get; set; } = runParam;
        [JsonIgnore]
        [Browsable(false)]
        public bool SimulationRun { get { return state == SimulationState.Completed; } }

        [JsonIgnore]
        [Browsable(false)]
        public bool SimulationCancelled { get; set; } = false;
        public double GetProgress() => iMax > 0 ? (double)iProgress / iMax : 0;

        [JsonIgnore]
        public DBLink DBLink { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan RunTime => End.Subtract(Start);
        public RunningModel Model { get; set; } = runningModel;
        public string Description => GlobalSettings.UseDBForMemory ? "DB Mode" : "Regular Mode";

        public void StartSimulation()
        {
            try
            {
                state = SimulationState.Initializing;
                Start = DateTime.Now;
                if (GlobalSettings.UseDBForMemory)
                {
                    SFDataContext dataContext = new(temp: true);
                    dataContext.Database.EnsureCreated();
                    if (Model.DbId == 0)
                    {
                        string stats = $"{Model.GetNumberOfCells():n0} cells; {Model.GetNumberOfConnections():n0} connections";
                        ModelRecord modelRecord = new(Model.ModelName, DateTime.Now, stats);
                        dataContext.Add(modelRecord);
                        dataContext.SaveChanges();
                        Model.DbId = modelRecord.Id;
                    }
                    SimulationRecord sim = new(Model.DbId,
                               DateTime.Now,
                               DateTime.Now,
                               Description,
                               RunParam.Description);
                    dataContext.Add(sim);
                    dataContext.SaveChanges();
                    DBLink = new DBLink(sim.Id, dataContext.DbFileName);
                }
                iProgress = 0;
                randomNumGenerator ??= new Random(Model.Settings.Seed);

                DBLink dB = DBLink;
                Model.InitForSimulation(RunParam, ref dB, randomNumGenerator);
                if (dB == null)
                    DBLink = null;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        public async void EndSimulation()
        {
            state = SimulationState.Finalizing;
            if (DBLink != null)
            {
                Model.FinalizeSimulation(RunParam, DBLink);
                state = SimulationState.DataDump;
                while (DBLink.DatabaseDumper.HasToDump())
                    await Task.Delay(100);
                DBLink.DatabaseDumper.Dispose();
                DBLink.DatabaseDumper = null;
            }
            End = DateTime.Now;
            state = SimulationState.Completed;
        }

        private void CalculateCellularOutputs(int t)
        {
            try
            {
                foreach (CellPool pool in Model.CellPools.Where(cp => cp.IsActive(t)))
                {
                    foreach (Cell cell in pool.GetCells().Where(c => c.IsActive(t)))
                        cell.CalculateCellularOutputs(t);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void CalculateMembranePotentialsFromCurrents(int timeIndex)
        {
            foreach (CellPool pool in Model.CellPools.Where(cp => cp.IsActive(timeIndex)))
            {
                foreach (Cell cell in pool.GetCells().Where(c => c.Active))
                    cell.CalculateMembranePotential(timeIndex);
            }
        }
        public virtual void RunSimulation()
        {
            try
            {
                StartSimulation();
                state = SimulationState.Running;
                if (SimulationCancelled)
                {
                    state = SimulationState.Cancelled;
                    return;
                }
                foreach (var index in Enumerable.Range(1, iMax - 1))
                {
                    iProgress = index;
                    if (SimulationCancelled)
                    {
                        SimulationCancelled = false;
                        state = SimulationState.Cancelled;
                        break;
                    }
                    CalculateCellularOutputs(index);
                    CalculateMembranePotentialsFromCurrents(index);
                }
                EndSimulation();
                while (state != SimulationState.Completed)
                    _ = Task.Delay(100);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

    }
}
