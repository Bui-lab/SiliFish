using GeneticSharp;
using SiliFish.Database;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
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
    public class Simulation(RunningModel runningModel, RunParam runParam)
    {
        [JsonIgnore]
        public Random randomNumGenerator { get; private set; } = null;
        private int iMax => RunParam.iMax;
        private int iProgress = 0;
        protected bool model_run = false;

        [JsonPropertyOrder(2)]
        public RunParam RunParam { get; set; } = runParam;
        [JsonIgnore]
        [Browsable(false)]
        public bool SimulationRun { get { return model_run; } }

        [JsonIgnore]
        [Browsable(false)]
        public bool SimulationCancelled { get; set; } = false;
        public double GetProgress() => iMax > 0 ? (double)iProgress / iMax : 0;

        public SFDataContext dataContext { get; set; } = new(temp: true);
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan RunTime => End.Subtract(Start);
        public RunningModel Model { get; set; } = runningModel;

        public void StartSimulation()=>Start = DateTime.Now;
        public void EndSimulation()=>End = DateTime.Now;

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
        public virtual void RunModel()
        {
            try
            {
                StartSimulation();
                iProgress = 0;
                model_run = false;
                randomNumGenerator ??= new Random(Model.Settings.Seed);

                if (!Model.InitForSimulation(RunParam, randomNumGenerator)) 
                    return;
                if (SimulationCancelled)
                    return;
                foreach (var index in Enumerable.Range(1, iMax - 1))
                {
                    iProgress = index;
                    if (SimulationCancelled)
                    {
                        SimulationCancelled = false;
                        break;
                    }
                    CalculateCellularOutputs(index);
                    CalculateMembranePotentialsFromCurrents(index);
                }
                model_run = true;
                EndSimulation();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

    }
}
