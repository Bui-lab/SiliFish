using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace SiliFish.Repositories
{
    public class Simulator(RunningModel runningModel, RunParam runParam, int numSimulations, bool parallelRun, Action<List<Simulation>, int> completionAction)
    {
        private RunningModel runningModel = runningModel;
        private RunParam runParam = runParam;
        private int numSimulations = numSimulations;
        private int runSimulations = 0;
        private bool parallelRun = parallelRun;
        private string runmode => parallelRun ? "parallel" : "series";
        private DateTime startTime, endTime;
        public Action<List<Simulation>, int> simulationCompletionAction = completionAction;
        public List<Simulation> SimulationList { get; private set; }
        public Simulation LastSimulation => SimulationList?.Count > 0 ? SimulationList.Last() : null;

        public bool ModelRun { get; set; } = false;
        public bool Cancelled { get; set; }
        public bool Interrupted { get; set; }


        public double GetProgress() => SimulationList?.Sum(s => s.GetProgress())/numSimulations ?? 0;
        public string GetStatus()
        {
            string state = string.Empty;
            SimulationState latestState = SimulationState.Completed;
            foreach (Simulation simulation in SimulationList)
            {
                if (simulation.state <= latestState)
                {
                    latestState = simulation.state;
                    state = latestState.GetDisplayName();
                }
            }
            return state;
        }
        public string Description => $"{numSimulations} simulations run in {runmode}. Total duration: {endTime - startTime}";
        public string RunParamDescription => runParam.Description;
        private void RunMultipleSimulations()
        {
            try
            {
                startTime = DateTime.Now;
                for (int i = 0; i < numSimulations; i++)
                {
                    Simulation simulation = new(runningModel, runParam);
                    SimulationList.Add(simulation);
                    simulation.RunSimulation(this);
                }
                endTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                ModelRun = true;
                if (SimulationList.Any(s => s.state == SimulationState.Completed))
                    simulationCompletionAction?.Invoke(SimulationList, 1 /*completed*/);
            }
        }
        private void RunSingleSimulation(Simulation simulation)
        {
            try
            {
                simulation.RunSimulation(this);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                ModelRun = true;//is set to true even if a single simulation is completed
                if (++runSimulations == numSimulations)
                {
                    endTime = DateTime.Now;
                    if (simulation.state == SimulationState.Completed)
                        simulationCompletionAction?.Invoke(SimulationList, 1/*Completed*/);
                }
            }
        }

        public void Run()
        {
            try
            {
                if (runningModel == null || numSimulations <= 0)
                    return;

                List<Thread> threadList = [];

                ModelRun = false;
                SimulationList = [];
                runSimulations = 0;
                //Run multiple simulations with a different seed each time - for model statistics
                if (parallelRun)
                {
                    startTime = DateTime.Now;
                    Random rand = new(runningModel.Settings.Seed);
                    for (int i = 0; i < numSimulations; i++)
                    {
                        RunningModel running = runningModel.CreateCopy();
                        Simulation simulation = new(running, runParam);
                        SimulationList.Add(simulation);
                        running.Settings.Seed = rand.Next();
                        Thread thread = new(() => RunSingleSimulation(simulation));
                        thread.Start();
                        threadList.Add(thread);
                    }
                }
                //Run multiple simulations with the same model - for runtime statistics
                else
                {
                    Thread thread = new(RunMultipleSimulations);
                    thread.Start();
                    threadList.Add(thread);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        public void InterruptRun()
        {
            try
            {
                Interrupted = true;
                SimulationList.ForEach(simulation => simulation.SimulationInterrupted = true);
                simulationCompletionAction?.Invoke(SimulationList, 0/*Interrupted*/);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void CancelRun()
        {
            try
            {
                Cancelled = true;
                SimulationList.ForEach(simulation => simulation.SimulationCancelled = true);
                simulationCompletionAction?.Invoke(SimulationList, -1/*Cancelled*/);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
