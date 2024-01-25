﻿using GeneticSharp;
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
    public class ModelSimulator
    {
        private List<Simulation> simulationList;
        private RunningModel runningModel;
        private int numSimulations;
        private int runSimulations = 0;
        private bool parallelRun = false;
        public Action<List<Simulation>, bool> simulationCompletionAction;
        public Simulation LastSimulation => simulationList?.Count > 0 ? simulationList.Last() : null;

        public bool ModelRun { get; set; }
        public bool Cancelled { get; set; }

        public double GetProgress() => simulationList?.Average(s => s.GetProgress()) ?? 0;

        public ModelSimulator(RunningModel runningModel, int numSimulations, bool parallelRun, Action<List<Simulation>, bool> completionAction) 
        { 
            this.runningModel = runningModel;
            this.numSimulations = numSimulations;
            this.parallelRun = parallelRun;
            ModelRun = false;
            simulationCompletionAction = completionAction;
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
                    simulationCompletionAction?.Invoke(simulationList, Cancelled);
            }
        }

        public void Run()
        {
            try
            {
                if (runningModel == null || numSimulations <= 0)
                    return;
                ModelRun = false;
                simulationList = [];
                runSimulations = 0;
                //Run multiple simulations with a different seed each time - for model statistics
                if (parallelRun)
                {
                    Random rand = new(runningModel.Settings.Seed);
                    for (int i = 0; i < numSimulations; i++)
                    {
                        RunningModel running = runningModel.CreateCopy();
                        Simulation simulation = new(running);
                        simulationList.Add(simulation);
                        running.Settings.Seed = rand.Next();
                        Task.Run(() => RunSingleSimulation(simulation));
                    }
                }
                //Run multiple simulations with the same model - for runtime statistics
                else
                {
                    for (int i = 0; i < numSimulations; i++)
                    {
                        Simulation simulation = new(runningModel);
                        simulationList.Add(simulation);
                        simulation.RunModel();
                    }
                }
            }
            finally
            {
                if (!parallelRun)
                {
                    ModelRun = true;
                    simulationCompletionAction?.Invoke(simulationList, Cancelled);
                }
            }
        }

        public void CancelRun()
        {
            try
            {
                Cancelled = true;
                simulationList.ForEach(simulation => simulation.SimulationCancelled = true);
                simulationCompletionAction?.Invoke(simulationList, Cancelled);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
