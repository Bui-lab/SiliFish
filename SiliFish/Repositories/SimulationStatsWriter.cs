using OfficeOpenXml;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;

namespace SiliFish.Repositories
{
    public class SimulationStatsWriter(string fileName, Simulation simulation, Action completionAction, Action abortAction, bool singleFile = false)
    {
        public string filename = fileName;
        public FileSaveMode mode;
        private readonly Simulation simulation = simulation;
        private readonly bool singleFile = singleFile;
        public Action saveCompletionAction = completionAction;
        public Action saveAbortAction = abortAction;
        private double progress = 0;
        public double GetProgress() => progress;

        private void SetProgress(double progress)
        {
            this.progress = progress;
        }
        private void SaveSpikeFreqStats()
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateSpikeFreqStats(simulation);
                FileUtil.SaveToCSVFile(filename: fileName, columnNames, values, SetProgress);
                saveCompletionAction?.Invoke();
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exc);
                saveAbortAction?.Invoke();
            }
        }
        private void SaveSpikeCounts()
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateSpikeCounts(simulation);
                FileUtil.SaveToCSVFile(filename: fileName, columnNames, values, SetProgress);
                saveCompletionAction?.Invoke();
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exc);
                saveAbortAction?.Invoke();
            }
        }
        private void SaveSpikes()
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateSpikesForCSV(simulation);
                FileUtil.SaveToCSVFile(filename: fileName, columnNames, values, SetProgress);
                saveCompletionAction?.Invoke();
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exc);
                saveAbortAction?.Invoke();
            }
        }

        private void SaveEpisodes()
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateEpisodes(simulation);
                FileUtil.SaveToCSVFile(filename: fileName, columnNames, values, SetProgress);
                saveCompletionAction?.Invoke();
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exc);
                saveAbortAction?.Invoke();
            }
        }

        private void SaveMembranePotentials()
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateMembranePotentialsForCSV(simulation);
                FileUtil.SaveToCSVFile(filename: fileName, columnNames, values, SetProgress);
                saveCompletionAction?.Invoke();
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exc);
                saveAbortAction?.Invoke();
            }
        }
        private void SaveCurrents()
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateCurrentsForCSV(simulation);
                FileUtil.SaveToCSVFile(filename: fileName, columnNames, values, SetProgress);
                saveCompletionAction?.Invoke();
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exc);
                saveAbortAction?.Invoke();
            }
        }
        private void SaveFullStats()
        {
            try
            {
                using ExcelPackage package = ExcelUtil.CreateWorkBook(filename);
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add("Simulation");
                int rowindex = 1;
                workSheet.Cells[rowindex, 1].Value = "Model Name";
                workSheet.Cells[rowindex++, 2].Value = simulation.Model.ModelName;
                workSheet.Cells[rowindex, 1].Value = "Version";
                workSheet.Cells[rowindex++, 2].Value = simulation.Model.Version;
                workSheet.Cells[rowindex, 1].Value = "Description";
                workSheet.Cells[rowindex++, 2].Value = simulation.Model.ModelDescription;
                workSheet.Cells[rowindex, 1].Value = "Simulation Started On";
                workSheet.Cells[rowindex++, 2].Value = simulation.Start.ToString("g");
                workSheet.Cells[rowindex, 1].Value = "Simulation Time";
                workSheet.Cells[rowindex++, 2].Value = simulation.RunParam.MaxTime;
                workSheet.Cells[rowindex, 1].Value = "Delta t";
                workSheet.Cells[rowindex++, 2].Value = simulation.RunParam.DeltaT;
                List<string> errorList = [];
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateSpikeFreqStats(simulation);
                ExcelUtil.AddWorksheet(package, "Spike Freq", columnNames, values, errorList);

                (columnNames, values) = SimulationStats.GenerateSpikeCounts(simulation);
                ExcelUtil.AddWorksheet(package, "Spike Counts", columnNames, values, errorList);

                (columnNames, values) = SimulationStats.GenerateSpikesForCSV(simulation);
                ExcelUtil.AddWorksheet(package, "Spikes", columnNames, values, errorList);

                (columnNames, values) = SimulationStats.GenerateEpisodes(simulation);
                ExcelUtil.AddWorksheet(package, "Episodes", columnNames, values, errorList);

                if (simulation.SimulationRun)
                {
                    (columnNames, values) = SimulationStats.GenerateMembranePotentialsForCSV(simulation);
                    if (singleFile)
                        ExcelUtil.AddWorksheet(package, "Membrane Potentials", columnNames, values, errorList);
                    else
                    {
                        filename = Path.ChangeExtension(filename, ".csv");
                        string path = FileUtil.AppendToFileName(filename, "_MembranePotentials");
                        FileUtil.SaveToCSVFile(path, columnNames, values, SetProgress);
                    }
                    if (simulation.Model.SimulationSettings.JunctionLevelTracking)
                    {
                        (columnNames, values) = SimulationStats.GenerateCurrentsForCSV(simulation);
                        if (singleFile)
                            ExcelUtil.AddWorksheet(package, "Currents", columnNames, values, errorList);
                        else
                        {
                            filename = Path.ChangeExtension(filename, ".csv");
                            string path = FileUtil.AppendToFileName(filename, "_Currents");
                            FileUtil.SaveToCSVFile(path, columnNames, values, SetProgress);
                        }
                    }
                }

                package.Save();
                saveCompletionAction?.Invoke();
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
                saveAbortAction?.Invoke();
            }
        }

        public void Run(FileSaveMode mode)
        {
            this.mode = mode;
            Thread thread = mode switch
            {
                FileSaveMode.SpikeFreq => new(SaveSpikeFreqStats),
                FileSaveMode.SpikeCounts => new(SaveSpikeCounts),
                FileSaveMode.Spikes => new(SaveSpikes),
                FileSaveMode.Episodes => new(SaveEpisodes),
                FileSaveMode.MembranePotentials => new(SaveMembranePotentials),
                FileSaveMode.Currents => new(SaveCurrents),
                FileSaveMode.FullStats => new(SaveFullStats),
                _ => throw new ArgumentException($"Unknown mode: {mode}"),
            };
            thread.Start();
        }
    }
}
