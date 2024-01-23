using OfficeOpenXml;
using SiliFish.Database;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits.Firing;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using SiliFish.Services.Dynamics;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static OfficeOpenXml.ExcelErrorValue;

namespace SiliFish.Repositories
{
    public static class ModelStats
    {
        public static (List<string>, List<List<string>>) GenerateSpikeFreqStats(RunningModel model)
        {
            try
            {
                bool jncTracking = model.JunctionCurrentTrackingOn;
                List<string> columnNames = new() { "RecordID", "Cell Pool", "Sagittal", "Somite", "Seq", "Cell Name",
                "Stim Start", "Stim End", "Stim Details",
                "Spike Count", "First Spike", "Last Spike", "Spike Freq",
                 "Burst Count", "Burst Freq",
                "Vrest", "Avg V", "Avg V > Vrest", "Avg V < Vrest"};
                List<List<string>> values = new();
                double dt = model.RunParam.DeltaT;
                int counter = 0;
                foreach (CellPool pool in model.NeuronPools.Union(model.MusclePools).OrderBy(p => p.PositionLeftRight).ThenBy(p => p.CellGroup))
                {
                    foreach (Cell c in pool.GetCells())
                    {
                        foreach (var grStim in model.StimulusTemplates.GroupBy(s => (s.TimeLine_ms.Start, s.TimeLine_ms.End)))
                        {
                            (double start, double end) = grStim.Key;
                            if (start > model.RunParam.MaxTime)
                                continue;
                            string stimDetails = "";
                            foreach (StimulusTemplate stim in grStim)
                            {
                                stimDetails += stim.ToString() + "\r\n";
                            }
                            List<string> cellValues = new();
                            cellValues.AddRange(new List<string>() { (++counter).ToString(), pool.CellGroup, c.PositionLeftRight.ToString(), c.Somite.ToString(),"1", c.ID });
                            cellValues.AddRange(new List<string>() {
                            start.ToString(GlobalSettings.PlotDataFormat),
                            end.ToString(GlobalSettings.PlotDataFormat),
                            stimDetails});
                            int iStart = (int)(start / dt);
                            int iEnd = end < 0 ? -1 : (int)(end / dt);
                            (DynamicsStats dyn, (double AvgV, double AvgVPos, double AvgVNeg)) =
                                SpikeDynamics.GenerateSpikeStats(model.DynamicsParam, dt, c, iStart, iEnd);
                            dyn?.DefineSpikingPattern();
                            if (dyn != null && dyn.SpikeList.Any())
                                cellValues.AddRange(new List<string>()
                                {   dyn.SpikeList.Count.ToString(GlobalSettings.PlotDataFormat),
                                    (dyn.SpikeList[0]*dt).ToString(GlobalSettings.PlotDataFormat),
                                    (dyn.SpikeList[^1]*dt).ToString(GlobalSettings.PlotDataFormat),
                                    dyn.SpikeFrequency_Overall.ToString(GlobalSettings.PlotDataFormat) });
                            else
                                cellValues.AddRange(new List<string>() { "0", "", "", "0" });

                            if (dyn != null && dyn.BurstsOrSpikes.Any())
                                cellValues.AddRange(new List<string>()
                                {   dyn.BurstsOrSpikes.Count.ToString(),
                                    dyn.BurstingFrequency_Overall.ToString(GlobalSettings.PlotDataFormat) });
                            else
                                cellValues.AddRange(new List<string>() { "0", "0" });
                            cellValues.AddRange(new List<string>()
                            {
                                c.Core.Vr.ToString(GlobalSettings.PlotDataFormat),
                                AvgV.ToString(GlobalSettings.PlotDataFormat),
                                AvgVPos.ToString(GlobalSettings.PlotDataFormat),
                                AvgVNeg.ToString(GlobalSettings.PlotDataFormat)
                             });
                            values.Add(cellValues);
                        }
                    }
                }
                return (columnNames, values);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return (null, null);
            }
        }

        public static (List<string>, List<List<string>>) GenerateSpikes(RunningModel model)
        {
            try
            {
                List<string> columnNames = new() { "RecordID", "Cell Pool", "Sagittal", "Somite", "Seq", "Cell Name", "Spike Time" };
                List<List<string>> values = new();
                int counter = 0;
                foreach (Cell cell in model.GetCells())
                {
                    foreach (int spikeIndex in cell.GetSpikeIndices())
                    {
                        List<string> cellValues = new();
                        cellValues.AddRange(new List<string>() { 
                            (++counter).ToString(),
                            cell.CellGroup,
                            cell.PositionLeftRight.ToString(),
                            cell.Somite.ToString(),
                            cell.Sequence.ToString(),
                            cell.ID,
                            model.RunParam.GetTimeOfIndex(spikeIndex).ToString(GlobalSettings.PlotDataFormat)});
                        values.Add(cellValues);
                    }
                }
                return (columnNames, values);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return (null, null);
            }
        }

        public static (List<string>, List<List<string>>) GenerateTBFs(RunningModel model)
        {
            try
            {
                (Coordinate[] _, SwimmingEpisodes episodes) = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(model);
                List<string> columnNames = new() { "Tail_MN", "Somite", "Episode", "Start", "End", "BeatCount", "BeatFreq" };
                List<List<string>> values = new();

                int counter = 1;
                foreach (SwimmingEpisode episode in episodes.Episodes)
                {
                    List<string> episodeValues = new();
                    episodeValues.AddRange(new List<string>() { "Tail",
                        "",
                        counter++.ToString(),
                        episode.Start.ToString(),
                        episode.End.ToString(),
                        episode.NumOfBeats.ToString(),
                        episode.BeatFrequency.ToString()});
                    values.Add(episodeValues);
                }
                for (int somite = 1; somite <= model.ModelDimensions.NumberOfSomites; somite++)
                {
                    counter = 1;
                    (double[] _, SwimmingEpisodes episodes2) = SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(model, somite);
                    foreach (SwimmingEpisode episode in episodes2.Episodes)
                    {
                        List<string> episodeValues = new();
                        episodeValues.AddRange(new List<string>() { "MN",
                        somite.ToString(),
                        counter++.ToString(),
                        episode.Start.ToString(),
                        episode.End.ToString(),
                        episode.NumOfBeats.ToString(),
                        episode.BeatFrequency.ToString()});
                        values.Add(episodeValues);
                    }

                }
                return (columnNames, values);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return (null, null);
            }
        }

    }
}
