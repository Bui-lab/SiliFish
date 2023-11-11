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
using SiliFish.Services.Kinematics;
using SiliFish.ModelUnits.Parameters;

namespace SiliFish.Services.Plotting
{
    public class PlotGenerator
    {
        public long NumOfDataPoints { get; set; } = 0;
        public string errorMessage { get; set; }

        private List<Chart> CreateFullDynamicsCharts(double[] TimeArray, List<Cell> cells,
            KinemParam kinemParam, double dt,
            bool combinePools, bool combineSomites, bool combineCells,
            int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<Chart> charts = new();

            PlotGeneratorMembranePotentials plotGeneratorMP = new(this, TimeArray, iStart, iEnd, 0,
                cells, combinePools, combineSomites: true, combineCells: true);
            plotGeneratorMP.CreateCharts(charts);
            if (!string.IsNullOrEmpty(errorMessage))
                return charts;

            PlotGeneratorCurrentsOfCells plotGeneratorCurrent = new(this, TimeArray,iStart, iEnd, 1,
                 cells,combinePools, combineSomites, combineCells,  UoM);
            plotGeneratorCurrent.CreateCharts(charts);
            if (!string.IsNullOrEmpty(errorMessage))
                return charts;

            PlotGeneratorStimuli plotGeneratorStimuli = new(this, TimeArray, iStart, iEnd, 2,
                cells, combinePools, combineSomites, combineCells, UoM);
            plotGeneratorStimuli.CreateCharts(charts);
            if (!string.IsNullOrEmpty(errorMessage))
                return charts;

            List<Cell> muscleCells = cells?.Where(c => c is MuscleCell).ToList();
            if (muscleCells != null && muscleCells.Any())
            {
                PlotGeneratorTension plotGeneratorTension = new(this, TimeArray, iStart, iEnd, 3,
                    cells,
                    combinePools, combineSomites, combineCells);
                plotGeneratorTension.CreateCharts(charts);
            }
            return charts;
        }

        private List<Chart> CreateJunctionCharts(double[] TimeArray, List<JunctionBase> jncList,
            KinemParam kinemParam, double dt,
            int iStart, int iEnd, UnitOfMeasure UoM)
        {
            List<Chart> charts = new();
            foreach (var jncGroup in jncList.GroupBy(j => j is GapJunction gj ? gj.Cell2.ID : j is ChemicalSynapse syn ? syn.PostCell.ID : ""))
            {
                List<Cell> cells = new();
                List<GapJunction> gapJunctions = new();
                List<ChemicalSynapse> synapses = new();
                foreach (JunctionBase junction in jncGroup)
                {
                    foreach (JunctionBase jnc in jncList)
                    {
                        if (jnc is GapJunction gj)
                        {
                            cells.Add(gj.Cell1);
                            cells.Add(gj.Cell2);
                            gapJunctions.Add(gj);
                        }
                        else if (jnc is ChemicalSynapse syn)
                        {
                            cells.Add(syn.PreNeuron);
                            cells.Add(syn.PostCell);
                            synapses.Add(syn);
                        }
                    }
                }
                PlotGeneratorMembranePotentials plotGeneratorMP = new(this, TimeArray, iStart, iEnd, 0,
                    cells.DistinctBy(c => c.ID).ToList(),
                    combinePools: false, combineSomites: true, combineCells: false);
                plotGeneratorMP.CreateCharts(charts);
                if (!string.IsNullOrEmpty(errorMessage))
                    return charts;

                PlotGeneratorCurrentsOfJunctions plotGeneratorCurrentsOfJunctions = new(this, TimeArray, iStart, iEnd, 1, gapJunctions, synapses, UoM);
                plotGeneratorCurrentsOfJunctions.CreateCharts(charts);
                if (!string.IsNullOrEmpty(errorMessage))
                    return charts;
            }
            return charts;
        }

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

            UnitOfMeasure UoM = model.Settings.UoM;
            bool combineCells = false;
            bool combineSomites = false;
            bool combinePools = false;
            bool combineJunctions = false;

            if (Plot.Selection is PlotSelectionUnits unitSelection)
            {
                List<JunctionBase> junctions = unitSelection.Units.Where(x => x is JunctionBase).Cast<JunctionBase>().ToList();
                if (junctions.Any())
                {
                    charts = CreateJunctionCharts(model.TimeArray, junctions, model.KinemParam, model.RunParam.DeltaT, iStart, iEnd, UoM);
                    return ("Junction", charts);
                }

                List<Stimulus> stims = unitSelection.Units.Where(x => x is Stimulus).Cast<Stimulus>().ToList();
                if (stims.Any())
                {
                    List<Cell> targetCells = stims.Select(s => s.TargetCell).ToList();
                    PlotGeneratorStimuli plotGeneratorStimuli = new(this, model.TimeArray, iStart, iEnd, 1, targetCells, combinePools, combineSomites, combineCells, UoM);
                    plotGeneratorStimuli.CreateCharts(charts);
                    return ("Stimuli", charts);
                }
                //All other cases: Cells and Pools are pre-populated, and Plot.PlotType is taken into consideration
            }

            if (Plot.PlotType.GetGroup() != "episode" &&
                (Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return (null, null);
            if (Plot.Selection is PlotSelectionMultiCells cellSelection)
            {
                combinePools = cellSelection.CombinePools;
                combineCells = cellSelection.CombineCells;
                combineSomites = model.ModelDimensions.NumberOfSomites > 0 && cellSelection.CombineSomites;
                combineJunctions = cellSelection.CombineJunctions;
            }
            string Title = "";
            switch (Plot.PlotType)
            {
                case PlotType.MembPotential:
                case PlotType.MembPotentialWithSpikeFreq:
                    Title = "Membrane Potentials";
                    if (Plot.PlotType == PlotType.MembPotentialWithSpikeFreq)
                    {
                        Title = "Membrane Potentials w/Spike Freq";
                        PlotGeneratorSpikeFrequency pg00 = new(this, Cells, model.TimeArray,
                            model.KinemParam, model.RunParam.DeltaT,
                            combinePools, combineSomites, combineCells, iStart, iEnd, 0);
                        pg00.CreateCharts(charts);

                        PlotGeneratorBurstFrequency pg01 = new(this, Cells, model.TimeArray,
                            model.KinemParam, model.RunParam.DeltaT,
                            combinePools, combineSomites, combineCells, iStart, iEnd, 1);
                        pg01.CreateCharts(charts);
                    }
                    PlotGeneratorMembranePotentials pg0 = new(this, model.TimeArray, iStart, iEnd, 2,
                        Cells, combinePools, combineSomites, combineCells);
                    pg0.CreateCharts(charts);
                    charts = charts.OrderBy(chart => chart.ChartSeq).ThenBy(chart => chart.GroupSeq).ToList();
                    break;
                case PlotType.Current:
                    Title = "Incoming Currents";
                    PlotGeneratorCurrentsOfCells pg1 = new(this, model.TimeArray, iStart, iEnd, 0, Cells, combinePools, combineSomites, combineCells, UoM,
                        includeGap: true, includeChemIn: true, includeChemOut: false, combineJunctions: combineJunctions);
                    pg1.CreateCharts(charts);
                    break;
                case PlotType.GapCurrent:
                    Title = "Incoming Gap Currents";
                    PlotGeneratorCurrentsOfCells pg2 = new(this, model.TimeArray, iStart, iEnd, 0, Cells, combinePools, combineSomites, combineCells, UoM,
                        includeGap: true, includeChemIn: false, includeChemOut: false, combineJunctions: combineJunctions);
                    pg2.CreateCharts(charts);
                    break;
                case PlotType.ChemCurrent:
                    Title = "Incoming Synaptic Currents";
                    PlotGeneratorCurrentsOfCells pg3 = new(this, model.TimeArray, iStart, iEnd, 0, Cells, combinePools, combineSomites, combineCells, UoM,
                        includeGap: false, includeChemIn: true, includeChemOut: false, combineJunctions: combineJunctions);
                    pg3.CreateCharts(charts);
                    break;
                case PlotType.ChemOutCurrent:
                    Title = "Outgoing Synaptic Currents";
                    PlotGeneratorCurrentsOfCells pg4 = new(this, model.TimeArray, iStart, iEnd, 0,
                        Cells, combinePools, combineSomites, combineCells, UoM,
                        includeGap: false, includeChemIn: false, includeChemOut: true, combineJunctions: combineJunctions);
                    pg4.CreateCharts(charts);
                    break;
                case PlotType.Stimuli:
                    Title = "Applied Stimulus";
                    PlotGeneratorStimuli pg5 = new(this, model.TimeArray, iStart, iEnd, 0,
                        Cells, combinePools, combineSomites, combineCells, UoM);
                    pg5.CreateCharts(charts);
                    break;
                case PlotType.FullDyn:
                    Title = "Full Dynamics";
                    charts = CreateFullDynamicsCharts(model.TimeArray, Cells,
                        model.KinemParam, model.RunParam.DeltaT,
                        combinePools, combineSomites, combineCells, iStart, iEnd, UoM);
                    break;
                case PlotType.Tension:
                    Title = "Muscle Tension";
                    List<Cell> muscleCells = Cells?.Where(c => c is MuscleCell).ToList();
                    if (muscleCells != null && muscleCells.Any())
                    {
                        PlotGeneratorTension plotGeneratorTension = new(this, model.TimeArray, iStart, iEnd, 0,
                            muscleCells, combinePools, combineSomites, combineCells);
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