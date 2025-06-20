using OfficeOpenXml;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SiliFish.Repositories
{
    public class SimulationFile
    {
        public static bool SaveSpikeFreqStats(Simulation simulation, string filename)
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateSpikeFreqStats(simulation);
                FileUtil.SaveToCSVFile(filename: filename, columnNames, values);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static bool SaveSpikeCounts(Simulation simulation, string filename)
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateSpikeCounts(simulation);
                FileUtil.SaveToCSVFile(filename: filename, columnNames, values);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static bool SaveSpikes(Simulation simulation, string filename)
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateSpikesForCSV(simulation);
                FileUtil.SaveToCSVFile(filename: filename, columnNames, values);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool SaveEpisodes(Simulation simulation, string filename)
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateEpisodes(simulation);
                FileUtil.SaveToCSVFile(filename: filename, columnNames, values);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool SaveMembranePotentials(Simulation simulation, string filename)
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateMembranePotentialsForCSV(simulation);
                FileUtil.SaveToCSVFile(filename: filename, columnNames, values);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static bool SaveCurrents(Simulation simulation, string filename)
        {
            try
            {
                (List<string> columnNames, List<List<string>> values) = SimulationStats.GenerateCurrentsForCSV(simulation);
                FileUtil.SaveToCSVFile(filename: filename, columnNames, values);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static bool SaveFullStats(Simulation simulation, string filename, bool singleFile = false)
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
                        FileUtil.SaveToCSVFile(path, columnNames, values);
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
                            FileUtil.SaveToCSVFile(path, columnNames, values);
                        }
                    }
                }
            
                package.Save();
                return true;
            }

            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
    }
}
