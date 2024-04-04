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

        public (string, List<Chart>) GetPlotData(PlotDefinition Plot, Simulation simulation, List<Cell> Cells, List<CellPool> Pools,
            int tStart = 0, int tEnd = -1)
        {
            errorMessage = string.Empty;
            List<Chart> charts = [];
            if (tEnd < 0) tEnd = simulation.RunParam.MaxTime;
            int iStart = simulation.RunParam.iIndex(tStart);
            int iEnd = simulation.RunParam.iIndex(tEnd);
            if (iEnd < iStart || iEnd >= simulation.Model.TimeArray.Length)
                iEnd = simulation.Model.TimeArray.Length - 1;
            if (iStart >= iEnd) return ("Invalid time range", null);

            UoM = simulation.Model.Settings.UoM;

            if (Plot.Selection is PlotSelectionUnits unitSelection)
            {
                List<JunctionBase> junctions = unitSelection.Units.Where(x => x is JunctionBase).Cast<JunctionBase>().ToList();
                if (junctions.Count != 0)
                {
                    PlotGeneratorJunctions plotGeneratorJunctions = new(this, simulation.Model.TimeArray, iStart, iEnd, 0,
                        junctions, Plot.Selection);
                    plotGeneratorJunctions.CreateCharts(charts);
                    return ("Junction", charts);
                }

                List<InterPool> interpools = unitSelection.Units.Where(x => x is InterPool).Cast<InterPool>().ToList();
                if (interpools.Count != 0)
                {
                    PlotGeneratorJunctions plotGeneratorJunctions = new(this, simulation.Model.TimeArray, iStart, iEnd, 0,
                        interpools, Pools, Plot.Selection);
                    plotGeneratorJunctions.CreateCharts(charts);
                    return ("Junction", charts);
                }
                List<Stimulus> stims = unitSelection.Units.Where(x => x is Stimulus).Cast<Stimulus>().ToList();
                if (stims.Count != 0)
                {
                    List<Cell> targetCells = stims.Select(s => s.TargetCell).ToList();
                    PlotGeneratorStimuli plotGeneratorStimuli = new(this, simulation.Model.TimeArray, iStart, iEnd, 1, targetCells, Plot.Selection);
                    plotGeneratorStimuli.CreateCharts(charts);
                    return ("Stimuli", charts);
                }
                //All other cases: Cells and Pools are pre-populated, and Plot.PlotType is taken into consideration
            }

            if (Plot.PlotType.GetGroup() != "episode" &&
                (Cells == null || Cells.Count == 0) &&
                (Pools == null || Pools.Count == 0))
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
                        PlotGeneratorSpikeBurstFrequency pg00 = new(this, Cells, simulation.Model.TimeArray,
                            simulation.Model.DynamicsParam, simulation.RunParam.DeltaT,
                            Plot.Selection, iStart, iEnd, 0, burst: false);
                        pg00.CreateCharts(charts);

                        PlotGeneratorSpikeBurstFrequency pg01 = new(this, Cells, simulation.Model.TimeArray,
                            simulation.Model.DynamicsParam, simulation.RunParam.DeltaT,
                            Plot.Selection, iStart, iEnd, 1, burst: true);
                        pg01.CreateCharts(charts);
                    }
                    PlotGeneratorMembranePotentials pg0 = new(this, simulation.Model.TimeArray, iStart, iEnd, 2,
                        Cells, Plot.Selection);
                    pg0.CreateCharts(charts);
                    charts = [.. charts.OrderBy(chart => chart.ChartSeq).ThenBy(chart => chart.GroupSeq)];
                    break;
                case PlotType.SpikeFreq:
                    Title = "Spike Freq";
                    PlotGeneratorSpikeBurstFrequency pg02 = new(this, Cells, simulation.Model.TimeArray,
                        simulation.Model.DynamicsParam, simulation.RunParam.DeltaT,
                        Plot.Selection, iStart, iEnd, 0, burst: false);
                    pg02.CreateCharts(charts);

                    PlotGeneratorSpikeBurstFrequency pg03 = new(this, Cells, simulation.Model.TimeArray,
                        simulation.Model.DynamicsParam, simulation.RunParam.DeltaT,
                        Plot.Selection, iStart, iEnd, 1, burst: true);
                    pg03.CreateCharts(charts);
                    charts = [.. charts.OrderBy(chart => chart.ChartSeq).ThenBy(chart => chart.GroupSeq)];
                    break;
                case PlotType.Current:
                    Title = "Incoming Currents";
                    PlotGeneratorCurrentsOfCells pg1 = new(this, simulation.Model.TimeArray, iStart, iEnd, 0, Cells, Plot.Selection, 
                        includeGap: true, includeChemIn: true, includeChemOut: false);
                    pg1.CreateCharts(charts);
                    break;
                case PlotType.GapCurrent:
                    Title = "Incoming Gap Currents";
                    PlotGeneratorCurrentsOfCells pg2 = new(this, simulation.Model.TimeArray, iStart, iEnd, 0, Cells, Plot.Selection, 
                        includeGap: true, includeChemIn: false, includeChemOut: false);
                    pg2.CreateCharts(charts);
                    break;
                case PlotType.ChemCurrent:
                    Title = "Incoming Synaptic Currents";
                    PlotGeneratorCurrentsOfCells pg3 = new(this, simulation.Model.TimeArray, iStart, iEnd, 0, Cells, Plot.Selection, 
                        includeGap: false, includeChemIn: true, includeChemOut: false);
                    pg3.CreateCharts(charts);
                    break;
                case PlotType.ChemOutCurrent:
                    Title = "Outgoing Synaptic Currents";
                    PlotGeneratorCurrentsOfCells pg4 = new(this, simulation.Model.TimeArray, iStart, iEnd, 0,
                        Cells, Plot.Selection,
                        includeGap: false, includeChemIn: false, includeChemOut: true);
                    pg4.CreateCharts(charts);
                    break;
                case PlotType.Stimuli:
                    Title = "Applied Stimulus";
                    PlotGeneratorStimuli pg5 = new(this, simulation.Model.TimeArray, iStart, iEnd, 0,
                        Cells, Plot.Selection);
                    pg5.CreateCharts(charts);
                    break;
                case PlotType.FullDyn:
                    Title = "Full Dynamics";
                    PlotGeneratorFullDynamics pg6 = new(this, simulation.Model.TimeArray, iStart, iEnd,
                        Cells, Plot.Selection);
                    pg6.CreateCharts(charts);
                    charts = [.. charts.OrderBy(chart => chart.GroupSeq).ThenBy(chart => chart.ChartSeq)];
                    break;
                case PlotType.Tension:
                    Title = "Muscle Tension";
                    List<Cell> muscleCells = Cells?.Where(c => c is MuscleCell).ToList();
                    if (muscleCells != null && muscleCells.Count != 0)
                    {
                        PlotGeneratorTension plotGeneratorTension = new(this, simulation.Model.TimeArray, iStart, iEnd, 0,
                            muscleCells, Plot.Selection);
                        plotGeneratorTension.CreateCharts(charts);
                    }
                    break;
                case PlotType.EpisodesTail:
                    Title = "Tail Beat Episodes";
                    SwimmingEpisodes episodes = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(simulation);
                    PlotGeneratorEpisodesOfTail plotGeneratorEpisodesTail = new(this, simulation.Model.TimeArray, iStart, iEnd, 0, episodes);
                    plotGeneratorEpisodesTail.CreateCharts(charts);
                    break;
                case PlotType.EpisodesMN:
                    Title = "Motoneuron Episodes";
                    int somite = Plot.Selection is PlotSelectionSomite pss ? pss.Somite : simulation.Model.ModelDimensions.NumberOfSomites;
                    (double[] mnMaxPotentials, SwimmingEpisodes episodesMN) = SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(simulation, somite);
                    PlotGeneratorEpisodesOfMN plotGeneratorEpisodesMN = new(this, simulation.Model.TimeArray, iStart, iEnd, 0, mnMaxPotentials, episodesMN, somite);
                    plotGeneratorEpisodesMN.CreateCharts(charts);
                    break;
                case PlotType.TailMovement:
                case PlotType.TailMovementFreq:
                    if (simulation.Model.MusclePools.Count == 0)
                    {
                        charts = null;
                        break;
                    }
                    Title = Plot.PlotType.GetDisplayName();
                    SwimmingEpisodes episodes2 = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(simulation);
                    PlotGeneratorEpisodesOfTail plotGeneratorEpisodesTail2 = new(this, simulation.Model.TimeArray, iStart, iEnd, 0, episodes2);
                    plotGeneratorEpisodesTail2.CreateCharts(charts, Plot.PlotType);
                    List<int> somites = simulation.Model.MotoNeuronPools[0].GetSomites((PlotSelectionMultiCells)Plot.Selection).ToList();
                    foreach (int s in somites)
                    {
                        (double[] mnMaxPotentials2, SwimmingEpisodes episodesMN2) = SwimmingKinematics.GetSwimmingEpisodesUsingMotoNeurons(simulation, s);
                        PlotGeneratorEpisodesOfMN plotGeneratorEpisodesMN2 = new(this, simulation.Model.TimeArray, iStart, iEnd, 1, mnMaxPotentials2, episodesMN2, s);
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