using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using System.Collections.Generic;
using System.Linq;
using SiliFish.ModelUnits.Junction;
using System;
using System.Runtime.InteropServices;
using SiliFish.ModelUnits.Stim;
using System.Reflection;
using SiliFish.Services.Plotting.PlotGenerators;
using SiliFish.Services.Plotting.PlotSelection;
using SiliFish.Services.Dynamics;
using SiliFish.ModelUnits.Parameters;

namespace SiliFish.Services.Plotting
{
    public class PlotGenerator
    {
        public UnitOfMeasure UoM { get; set; }
        public long NumOfDataPoints { get; set; } = 0;
        public string errorMessage { get; set; }

        public (string, List<Chart>) GetPlotData(PlotDefinition Plot, RunningModel model, List<Cell> Cells, List<CellPool> Pools,
            int tStart = 0, int tEnd = -1)
        {
            errorMessage = string.Empty;
            List<Chart> charts = new();
            if (tEnd < 0) tEnd = model.RunParam.MaxTime;
            int iStart = model.RunParam.iIndex(tStart);
            int iEnd = model.RunParam.iIndex(tEnd);
            if (iEnd < iStart || iEnd >= model.TimeArray.Length)
                iEnd = model.TimeArray.Length - 1;
            if (iStart >= iEnd) return ("Invalid time range", null);

            UoM = model.Settings.UoM;

            if (Plot.Selection is PlotSelectionUnits unitSelection)
            {
                List<JunctionBase> junctions = unitSelection.Units.Where(x => x is JunctionBase).Cast<JunctionBase>().ToList();
                if (junctions.Any())
                {
                    PlotGeneratorJunctions plotGeneratorJunctions = new(this, model.TimeArray, iStart, iEnd, 0,
                        junctions, Plot.Selection);
                    plotGeneratorJunctions.CreateCharts(charts);
                    return ("Junction", charts);
                }

                List<Stimulus> stims = unitSelection.Units.Where(x => x is Stimulus).Cast<Stimulus>().ToList();
                if (stims.Any())
                {
                    List<Cell> targetCells = stims.Select(s => s.TargetCell).ToList();
                    PlotGeneratorStimuli plotGeneratorStimuli = new(this, model.TimeArray, iStart, iEnd, 1, targetCells, Plot.Selection);
                    plotGeneratorStimuli.CreateCharts(charts);
                    return ("Stimuli", charts);
                }
                //All other cases: Cells and Pools are pre-populated, and Plot.PlotType is taken into consideration
            }

            if (Plot.PlotType.GetGroup() != "episode" &&
                (Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return (null, null);
            string Title = "";
            switch (Plot.PlotType)
            {
                case PlotType.MembPotential:
                case PlotType.MembPotentialWithSpikeFreq:
                    Title = "Membrane Potentials";
                    if (Plot.PlotType == PlotType.MembPotentialWithSpikeFreq)
                    {
                        Title = "Membrane Potentials w/Spike Freq";
                        PlotGeneratorSpikeBurstFrequency pg00 = new(this, Cells, model.TimeArray,
                            model.DynamicsParam, model.RunParam.DeltaT,
                            Plot.Selection, iStart, iEnd, 0, burst: false);
                        pg00.CreateCharts(charts);

                        PlotGeneratorSpikeBurstFrequency pg01 = new(this, Cells, model.TimeArray,
                            model.DynamicsParam, model.RunParam.DeltaT,
                            Plot.Selection, iStart, iEnd, 1, burst: true);
                        pg01.CreateCharts(charts);
                    }
                    PlotGeneratorMembranePotentials pg0 = new(this, model.TimeArray, iStart, iEnd, 2,
                        Cells, Plot.Selection);
                    pg0.CreateCharts(charts);
                    charts = charts.OrderBy(chart => chart.ChartSeq).ThenBy(chart => chart.GroupSeq).ToList();
                    break;
                case PlotType.Current:
                    Title = "Incoming Currents";
                    PlotGeneratorCurrentsOfCells pg1 = new(this, model.TimeArray, iStart, iEnd, 0, Cells, Plot.Selection, 
                        includeGap: true, includeChemIn: true, includeChemOut: false);
                    pg1.CreateCharts(charts);
                    break;
                case PlotType.GapCurrent:
                    Title = "Incoming Gap Currents";
                    PlotGeneratorCurrentsOfCells pg2 = new(this, model.TimeArray, iStart, iEnd, 0, Cells, Plot.Selection, 
                        includeGap: true, includeChemIn: false, includeChemOut: false);
                    pg2.CreateCharts(charts);
                    break;
                case PlotType.ChemCurrent:
                    Title = "Incoming Synaptic Currents";
                    PlotGeneratorCurrentsOfCells pg3 = new(this, model.TimeArray, iStart, iEnd, 0, Cells, Plot.Selection, 
                        includeGap: false, includeChemIn: true, includeChemOut: false);
                    pg3.CreateCharts(charts);
                    break;
                case PlotType.ChemOutCurrent:
                    Title = "Outgoing Synaptic Currents";
                    PlotGeneratorCurrentsOfCells pg4 = new(this, model.TimeArray, iStart, iEnd, 0,
                        Cells, Plot.Selection,
                        includeGap: false, includeChemIn: false, includeChemOut: true);
                    pg4.CreateCharts(charts);
                    break;
                case PlotType.Stimuli:
                    Title = "Applied Stimulus";
                    PlotGeneratorStimuli pg5 = new(this, model.TimeArray, iStart, iEnd, 0,
                        Cells, Plot.Selection);
                    pg5.CreateCharts(charts);
                    break;
                case PlotType.FullDyn:
                    Title = "Full Dynamics";
                    PlotGeneratorFullDynamics pg6 = new(this, model.TimeArray, iStart, iEnd,
                        Cells, Plot.Selection);
                    pg6.CreateCharts(charts);
                    charts = charts.OrderBy(chart => chart.GroupSeq).ThenBy(chart => chart.ChartSeq).ToList();
                    break;
                case PlotType.Tension:
                    Title = "Muscle Tension";
                    List<Cell> muscleCells = Cells?.Where(c => c is MuscleCell).ToList();
                    if (muscleCells != null && muscleCells.Any())
                    {
                        PlotGeneratorTension plotGeneratorTension = new(this, model.TimeArray, iStart, iEnd, 0,
                            muscleCells, Plot.Selection);
                        plotGeneratorTension.CreateCharts(charts);
                    }
                    break;
                case PlotType.EpisodesTail:
                    Title = "Tail Beat Episodes";
                    (Coordinate[] tail_tip_coord, SwimmingEpisodes episodes) = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(model);
                    PlotGeneratorEpisodesOfTail plotGeneratorEpisodesTail = new(this, model.TimeArray, iStart, iEnd, 0, tail_tip_coord, episodes);
                    plotGeneratorEpisodesTail.CreateCharts(charts);
                    break;
                case PlotType.EpisodesMN:
                    Title = "Motoneuron Episodes";
                    int somite = Plot.Selection is PlotSelectionSomite pss ? pss.Somite : model.ModelDimensions.NumberOfSomites;
                    (double[] mnMaxPotentials, SwimmingEpisodes episodesMN) = SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(model, somite);
                    PlotGeneratorEpisodesOfMN plotGeneratorEpisodesMN = new(this, model.TimeArray, iStart, iEnd, 0, mnMaxPotentials, episodesMN, somite);
                    plotGeneratorEpisodesMN.CreateCharts(charts);
                    break;
                case PlotType.TailMovement:
                case PlotType.TailMovementFreq:
                case PlotType.EpisodeDuration:
                case PlotType.InstFreq:
                case PlotType.TailBeatFreq:
                case PlotType.TailBeatPerEpisode:
                    if (!model.MusclePools.Any())
                    {
                        charts = null;
                        break;
                    }
                    Title = Plot.PlotType.ToString();
                    (Coordinate[] tail_tip_coord2, SwimmingEpisodes episodes2) = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(model);
                    PlotGeneratorEpisodesOfTail plotGeneratorEpisodesTail2 = new(this, model.TimeArray, iStart, iEnd, 0, tail_tip_coord2, episodes2);
                    plotGeneratorEpisodesTail2.CreateCharts(charts, Plot.PlotType);
                    List<int> somites = model.MotoNeuronPools[0].GetSomites((PlotSelectionMultiCells)Plot.Selection).ToList();
                    foreach (int s in somites)
                    {
                        (double[] mnMaxPotentials2, SwimmingEpisodes episodesMN2) = SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(model, s);
                        PlotGeneratorEpisodesOfMN plotGeneratorEpisodesMN2 = new(this, model.TimeArray, iStart, iEnd, 1, mnMaxPotentials2, episodesMN2, s);
                        plotGeneratorEpisodesMN2.CreateCharts(charts, Plot.PlotType);
                    }
                    break;
                default:
                    charts = null;
                    break;
            }
            return (Title, charts);
        }

    }
}