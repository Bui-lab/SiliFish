using GeneticSharp;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Repositories
{
    public class ModelSimulator(RunningModel runningModel, RunParam runParam, int numSimulations, bool parallelRun, Action<List<Simulation>, bool> completionAction)
    {
        private RunningModel runningModel = runningModel;
        private RunParam runParam = runParam;
        private int numSimulations = numSimulations;
        private int runSimulations = 0;
        private bool parallelRun = parallelRun;
        private string runmode => parallelRun ? "parallel" : "series";
        private DateTime startTime, endTime;
        public Action<List<Simulation>, bool> simulationCompletionAction = completionAction;
        public List<Simulation> SimulationList { get; private set; }
        public Simulation LastSimulation => SimulationList?.Count > 0 ? SimulationList.Last() : null;

        public bool ModelRun { get; set; } = false;
        public bool Cancelled { get; set; }

        public double GetProgress() => SimulationList?.Sum(s => s.GetProgress())/numSimulations ?? 0;
        public string Description => $"{numSimulations} simulations run in {runmode}. Total duration: {endTime - startTime}";
        private void RunMultipleSimulations()
        {
            try
            {
                startTime = DateTime.Now;
                for (int i = 0; i < numSimulations; i++)
                {
                    Simulation simulation = new(runningModel, runParam);
                    SimulationList.Add(simulation);
                    simulation.RunModel();
                }
                endTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                ModelRun = true;
                simulationCompletionAction?.Invoke(SimulationList, Cancelled);
            }
        }
        private void RunSingleSimulation(Simulation simulation)
        {
            try
            {
                simulation.RunModel();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                ModelRun = true;//is set to true even if a single simulation is completed
                if (++runSimulations == numSimulations)
                {
                    endTime = DateTime.Now;
                    simulationCompletionAction?.Invoke(SimulationList, Cancelled);
                }
            }
        }

        public void Run()
        {
            try
            {
                if (runningModel == null || numSimulations <= 0)
                    return;
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
                        Task.Run(() => RunSingleSimulation(simulation));
                    }
                }
                //Run multiple simulations with the same model - for runtime statistics
                else
                {
                    Task.Run(RunMultipleSimulations);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void CancelRun()
        {
            try
            {
                Cancelled = true;
                SimulationList.ForEach(simulation => simulation.SimulationCancelled = true);
                simulationCompletionAction?.Invoke(SimulationList, Cancelled);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
