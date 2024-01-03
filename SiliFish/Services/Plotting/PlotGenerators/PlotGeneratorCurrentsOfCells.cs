using GeneticSharp;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorCurrentsOfCells : PlotGeneratorOfCells
    {
        private readonly bool includeGap;
        private readonly bool includeChemIn;
        private readonly bool includeChemOut;

        public PlotGeneratorCurrentsOfCells(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
            List<Cell> cells, PlotSelectionInterface plotSelection,
            bool includeGap = true, bool includeChemIn = true, bool includeChemOut = true) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, cells, plotSelection)
        {
            this.includeGap = includeGap;
            this.includeChemIn = includeChemIn;
            this.includeChemOut = includeChemOut;
        }

        private void CreateIndividualJunctionsCharts()
        {
            UnitOfMeasure UoM = plotGenerator.UoM;
            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools: false, combineSomites: false, combineCells: false);
            List<GapJunction> gapJunctions = new();
            List<ChemicalSynapse> synapses = new();
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                foreach (Cell cell in cellGroup)
                {
                    if (includeChemIn)
                    {
                        if (cell is Neuron neuron)
                        {
                            foreach (ChemicalSynapse synapse in neuron.Synapses)
                            {
                                if (!synapse.InputCurrent.Any(c => c != 0))
                                    continue;
                                PlotGeneratorCurrentsOfJunctions junctionPG = new(plotGenerator, timeArray, iStart, iEnd, 0, null, new() { synapse });
                                junctionPG.CreateCharts(charts);
                            }
                        }
                        else if (cell is MuscleCell muscleCell)
                        {
                            foreach (ChemicalSynapse synapse in muscleCell.EndPlates)
                            {
                                if (!synapse.InputCurrent.Any(c => c != 0))
                                    continue;
                                PlotGeneratorCurrentsOfJunctions junctionPG = new(plotGenerator, timeArray, iStart, iEnd, 0, null, new() { synapse });
                                junctionPG.CreateCharts(charts);
                            }
                        }
                    }
                    if (includeGap)
                    {
                        foreach (GapJunction gapJunction in cell.GapJunctions.Where(j => j.Cell2 == cell || j.Cell1 == cell))
                        {
                            if (!gapJunction.InputCurrent.Any(c => c != 0))
                                continue;
                            PlotGeneratorCurrentsOfJunctions junctionPG = new(plotGenerator, timeArray, iStart, iEnd, 1, new() { gapJunction }, null);
                            junctionPG.CreateCharts(charts);
                        }
                    }
                    if (includeChemOut && cell is Neuron neuron1)
                    {
                        foreach (ChemicalSynapse synapse in neuron1.Terminals)
                        {
                            if (!synapse.InputCurrent.Any(c => c != 0))
                                continue;
                            PlotGeneratorCurrentsOfJunctions junctionPG = new(plotGenerator, timeArray, iStart, iEnd, 2, null, new() { synapse });
                            junctionPG.CreateCharts(charts);
                        }
                    }
                }
            }
        }
        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }

        protected override void CreateCharts()
        {
            if (cells == null || !cells.Any())
                return;
            if (!combineJunctions)
            {
                CreateIndividualJunctionsCharts();
                return;
            }
            UnitOfMeasure UoM = plotGenerator.UoM;

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string gapTitle = "Time,";
                string synInTitle = "Time,";
                string synOutTitle = "Time,";
                List<string> gapData = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> synInData = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> synOutData = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> colorPerGapChart = new();
                List<string> colorPerInSynChart = new();
                List<string> colorPerOutSynChart = new();
                bool gapExists = false;
                bool synInExists = false;
                bool synOutExists = false;

                foreach (Cell cell in cellGroup)
                {
                    if (includeChemIn)
                    {
                        List<ChemicalSynapse> synapses = null;
                        if (cell is Neuron neuron) synapses = neuron.Synapses;
                        else if (cell is MuscleCell muscleCell) synapses = muscleCell.EndPlates;
                        bool useIdentifier = synapses.GroupBy(j => j.ID).Count() != synapses.Count;
                        foreach (ChemicalSynapse jnc in synapses)
                        {
                            synInExists = true;
                            if (combineCells)
                                synInTitle += jnc.ID + ",";
                            else
                                synInTitle += $"{jnc.PreNeuron.ID} {(useIdentifier ? "(" + jnc.Core.Identifier + ")" : "")} ,";
                            colorPerInSynChart.Add(jnc.PreNeuron.CellPool.Color.ToRGBQuoted());
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                synInData[i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                        }
                    }
                    if (includeGap)
                    {
                        List<GapJunction> gapJunctions = cell.GapJunctions.Where(j => j.Cell2 == cell || j.Cell1 == cell).ToList();
                        bool useIdentifier = gapJunctions.GroupBy(j => j.ID).Count() != gapJunctions.Count;
                        foreach (GapJunction jnc in gapJunctions)
                        {
                            gapExists = true;
                            Cell otherCell = jnc.Cell1 == cell ? jnc.Cell2 : jnc.Cell1;
                            gapTitle += $"Gap: {jnc.ID} {(useIdentifier ? "(" + jnc.Core.Identifier + ")" : "")},";
                            colorPerGapChart.Add(otherCell.CellPool.Color.ToRGBQuoted());
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                gapData[i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                        }
                    }
                    if (includeChemOut)
                    {
                        if (cell is Neuron neuron2)
                        {
                            bool useIdentifier = neuron2.Terminals.GroupBy(j => j.ID).Count() != neuron2.Terminals.Count;
                            foreach (ChemicalSynapse jnc in neuron2.Terminals)
                            {
                                synOutExists = true;
                                if (combineCells)
                                    synOutTitle += jnc.ID + ",";
                                else 
                                    synOutTitle += $"{jnc.PostCell.ID} {(useIdentifier ? "(" + jnc.Core.Identifier + ")" : "")} ,";
                                colorPerOutSynChart.Add(jnc.PostCell.CellPool.Color.ToRGBQuoted());
                                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                    synOutData[i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                            }
                        }
                    }
                }

                double yMin = cells.Min(c => c.MinCurrentValue(includeGap, includeChemIn, includeChemOut, iStart, iEnd));
                double yMax = cells.Max(c => c.MaxCurrentValue(includeGap, includeChemIn, includeChemOut, iStart, iEnd));
                Util.SetYRange(ref yMin, ref yMax);


                if (synInExists)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinSynInCurrentValue(iStart, iEnd));
                        yMax = cellGroup.Max(c => c.MaxSynInCurrentValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    string csvData = "`" + synInTitle[..^1] + "\n" + string.Join("\n", synInData.Select(line => line[..^1]).ToArray()) + "`";
                    Chart chart = new()
                    {
                        GroupSeq = GroupSeq, 
                        CsvData = csvData,
                        Color = string.Join(',', colorPerInSynChart),
                        Title = $"`{cellGroup.Key} Incoming Synaptic Currents`",
                        yLabel = $"`Current ({Util.GetUoM(UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = timeArray[iStart],
                        xMax = timeArray[iEnd] + 1
                    };
                    if (!AddChart(chart)) return;
                }
                 if (gapExists)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinGapCurrentValue(iStart, iEnd));
                        yMax = cellGroup.Max(c => c.MaxGapCurrentValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    string csvData = "`" + gapTitle[..^1] + "\n" + string.Join("\n", gapData.Select(line => line[..^1]).ToArray()) + "`";
                    Chart chart = new()
                    {
                        GroupSeq = GroupSeq + 1,
                        CsvData = csvData,
                        Color = string.Join(',', colorPerGapChart),
                        Title = $"`{cellGroup.Key} Gap Currents`",
                        yLabel = $"`Current ({Util.GetUoM(UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = timeArray[iStart],
                        xMax = timeArray[iEnd] + 1
                    };
                    if (!AddChart(chart)) return;
                }               
                if (synOutExists)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinSynOutCurrentValue(iStart, iEnd));
                        yMax = cellGroup.Max(c => c.MaxSynOutCurrentValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    string csvData = "`" + synOutTitle[..^1] + "\n" + string.Join("\n", synOutData.Select(line => line[..^1]).ToArray()) + "`";
                    Chart chart = new()
                    {
                        GroupSeq = GroupSeq + 2,
                        CsvData = csvData,
                        Color = string.Join(',', colorPerOutSynChart),
                        Title = $"`{cellGroup.Key} Outgoing Synaptic Currents`",
                        yLabel = $"`Current ({Util.GetUoM(UoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = timeArray[iStart],
                        xMax = timeArray[iEnd] + 1
                    };
                    if (!AddChart(chart)) return;
                }
            }
        }
    }
}
