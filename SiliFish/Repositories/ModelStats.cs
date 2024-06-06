﻿using GeneticSharp;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using SiliFish.Services.Dynamics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace SiliFish.Repositories
{
    public static class ModelStats
    {
        public static (List<string>, List<List<string>>) GenerateSpikeCounts(Simulation simulation)
        {
            try
            {
                RunningModel model = simulation.Model;
                List<string> columnNames = ["RecordID", "Cell Pool", "Period Start", "Period End", "Stim Details", "Spike Count"];
                List<List<string>> values = [];
                double dt = simulation.RunParam.DeltaT;
                int counter = 0;
                var stims = model.StimulusTemplates.GroupBy(s => (s.TimeLine_ms.Start, s.TimeLine_ms.End));
                
                for (int stimCounter = 0; stimCounter < stims.Count(); stimCounter++)
                {
                    var grStim = stims.ElementAt(stimCounter);
                    (double start, double end) = grStim.Key;
                    if (start > simulation.RunParam.MaxTime)
                        continue;
                    int iStart = (int)(start / dt);
                    if (stimCounter < stims.Count() - 1)
                    {
                        var nextStim = stims.ElementAt(stimCounter + 1);
                        (double nextStart, double _) = nextStim.Key;
                        if (nextStart > end)
                            end = nextStart - 1;
                    }
                    else //last stimulus - check till the end
                        end = simulation.RunParam.MaxTime;
                    int iEnd = end < 0 ? -1 : (int)(end / dt);
                    string stimDetails = "";
                    foreach (StimulusTemplate stim in grStim)
                    {
                        stimDetails += stim.ToString() + "\r\n";
                    }
                    foreach (var grCellPool in model.NeuronPools.Union(model.MusclePools).GroupBy(p => p.CellGroup))
                    {
                        List<string> cellValues =
                        [
                         .. new List<string>() { (++counter).ToString(), grCellPool.Key},
                                .. new List<string>() {
                                start.ToString(GlobalSettings.PlotDataFormat),
                                end.ToString(GlobalSettings.PlotDataFormat),
                                stimDetails},
                        ];
                        int sum = 0;
                        foreach (CellPool pool in grCellPool)
                        {
                            foreach (Cell c in pool.GetCells())
                            {
                                sum += c.GetSpikeIndices(iStart, iEnd)?.Count ?? 0;
                            }
                        }
                        cellValues.AddRange([sum.ToString("0")]);
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
        public static (List<string>, List<List<string>>) GenerateSpikeFreqStats(Simulation simulation)
        {
            try
            {
                RunningModel model = simulation.Model;
                bool jncTracking = model.Settings.JunctionLevelTracking;
                List<string> columnNames = [ "RecordID", "Cell Pool", "Sagittal", "Somite", "Seq", "Cell Name",
                "Stim Start", "Stim End", "Stim Details",
                "Spike Count", "First Spike", "Last Spike", "Spike Freq",
                 "Burst Count", "Burst Freq",
                "Vrest", "Avg V", "Avg V > Vrest", "Avg V < Vrest"];
                List<List<string>> values = [];
                double dt = simulation.RunParam.DeltaT;
                int counter = 0;
                foreach (CellPool pool in model.NeuronPools.Union(model.MusclePools).OrderBy(p => p.PositionLeftRight).ThenBy(p => p.CellGroup))
                {
                    foreach (Cell c in pool.GetCells())
                    {
                        foreach (var grStim in model.StimulusTemplates.GroupBy(s => (s.TimeLine_ms.Start, s.TimeLine_ms.End)))
                        {
                            (double start, double end) = grStim.Key;
                            if (start > simulation.RunParam.MaxTime)
                                continue;
                            string stimDetails = "";
                            foreach (StimulusTemplate stim in grStim)
                            {
                                stimDetails += stim.ToString() + "\r\n";
                            }
                            List<string> cellValues =
                            [
                                .. new List<string>() { (++counter).ToString(), pool.CellGroup, c.PositionLeftRight.ToString(), c.Somite.ToString(),"1", c.ID },
                                .. new List<string>() {
                                start.ToString(GlobalSettings.PlotDataFormat),
                                end.ToString(GlobalSettings.PlotDataFormat),
                                stimDetails},
                            ];
                            int iStart = (int)(start / dt);
                            int iEnd = end < 0 ? -1 : (int)(end / dt);
                            (DynamicsStats dyn, (double AvgV, double AvgVPos, double AvgVNeg)) =
                                SpikeDynamics.GenerateSpikeStats(model.DynamicsParam, dt, c, iStart, iEnd);
                            dyn?.DefineSpikingPattern();
                            if (dyn != null && dyn.SpikeList.Count != 0)
                                cellValues.AddRange([   
                                    dyn.SpikeList.Count.ToString(GlobalSettings.PlotDataFormat),
                                    (dyn.SpikeList[0]*dt).ToString(GlobalSettings.PlotDataFormat),
                                    (dyn.SpikeList[^1]*dt).ToString(GlobalSettings.PlotDataFormat),
                                    dyn.SpikeFrequency_Overall.ToString(GlobalSettings.PlotDataFormat) 
                                    ]);
                            else
                                cellValues.AddRange(["0", "", "", "0"]);

                            if (dyn != null && dyn.BurstsOrSpikes.Count != 0)
                                cellValues.AddRange([
                                    dyn.BurstsOrSpikes.Count.ToString(),
                                    dyn.BurstingFrequency_Overall.ToString(GlobalSettings.PlotDataFormat)
                                    ]);
                            else
                                cellValues.AddRange(["0", "0"]);
                            cellValues.AddRange(
                            [
                                c.Core.Vr.ToString(GlobalSettings.PlotDataFormat),
                                AvgV.ToString(GlobalSettings.PlotDataFormat),
                                AvgVPos.ToString(GlobalSettings.PlotDataFormat),
                                AvgVNeg.ToString(GlobalSettings.PlotDataFormat)
                             ]);
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

        public static (List<string>, List<List<string>>) GenerateSpikes(Simulation simulation, List<Cell> cells = null, int spikeStart = 0, int spikeEnd = -1)
        {
            try
            {
                List<string> columnNames = ["RecordID", "Cell Pool", "Sagittal", "Somite", "Seq", "Cell Name", "Spike Time"];
                if (simulation == null) return (columnNames, null);
                List<List<string>> values = [];
                int counter = 0;
                cells ??= simulation.Model.GetCells();
                foreach (Cell cell in cells)
                {
                    foreach (int spikeIndex in cell.GetSpikeIndices(spikeStart, spikeEnd))
                    {
                        List<string> cellValues =
                        [
                            .. new List<string>() { 
                                (++counter).ToString(),
                                cell.CellGroup,
                                cell.PositionLeftRight.ToString(),
                                cell.Somite.ToString(),
                                cell.Sequence.ToString(),
                                cell.ID,
                                simulation.RunParam.GetTimeOfIndex(spikeIndex).ToString(GlobalSettings.PlotDataFormat)},
                        ];
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


        public static (List<string>, List<List<string>>) GenerateEpisodes(Simulation simulation)
        {
            try
            {
                SwimmingEpisodes episodes = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(simulation);
                List<string> columnNames = ["Tail_MN", "Somite", "Episode", "Start", "End", "BeatCount", "BeatFreq", "Mean Ampl.", "Median Ampl.", "Max Ampl.", "Duration"];
                List<List<string>> values = [];

                int counter = 1;
                foreach (SwimmingEpisode episode in episodes.Episodes)
                {
                    List<string> episodeValues =
                    [
                        .. new List<string>() { "Tail",
                            "",
                            counter++.ToString(),
                            episode.Start.ToString(),
                            episode.End.ToString(),
                            episode.NumOfBeats.ToString(),
                            episode.BeatFrequency.ToString(),
                            episode.MeanAmplitude.ToString(),
                            episode.MedianAmplitude.ToString(),
                            episode.MaxAmplitude.ToString(),
                            episode.EpisodeDuration.ToString()
                        },
                    ];
                    values.Add(episodeValues);
                }
                for (int somite = 1; somite <= simulation.Model.ModelDimensions.NumberOfSomites; somite++)
                {
                    counter = 1;
                    (double[] _, SwimmingEpisodes episodes2) = SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(simulation, somite);
                    foreach (SwimmingEpisode episode in episodes2.Episodes)
                    {
                        List<string> episodeValues =
                        [
                            .. new List<string>() { "MN",
                            somite.ToString(),
                            counter++.ToString(),
                            episode.Start.ToString(),
                            episode.End.ToString(),
                            episode.NumOfBeats.ToString(),
                            episode.BeatFrequency.ToString(),
                            "","","",
                            episode.EpisodeDuration.ToString()
                            },
                        ];
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simulation"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>CellSpikes and CellPoolSpikes</returns>
        public static (Dictionary<string, int>, Dictionary<string, int>) GenerateSpikeSummary(Simulation simulation, double startTime, double endTime)
        {
            Dictionary<string, int> CellSpikes = [];
            Dictionary<string, int> CellPoolSpikes = [];
            int iSpikeStart = simulation.RunParam.iIndex(startTime);
            int iSpikeEnd = simulation.RunParam.iIndex(endTime);

            foreach (CellPool cellPool in simulation.Model.GetCellPools().Cast<CellPool>())
            {
                int sum = 0;
                foreach (Cell cell in cellPool.Cells)
                {
                    int spikeCount = cell.GetSpikeIndices(iSpikeStart, iSpikeEnd).Count;
                    CellSpikes[cell.ID] = spikeCount;
                    sum += spikeCount;
                }
                if (CellPoolSpikes.ContainsKey(cellPool.ID))
                    CellPoolSpikes[cellPool.ID] += sum;
                else
                    CellPoolSpikes[cellPool.ID] = sum;
            }
            return (CellSpikes, CellPoolSpikes);
        }
    }
}
