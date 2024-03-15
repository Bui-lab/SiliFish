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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorCurrentsOfCells(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
        List<Cell> cells, PlotSelectionInterface plotSelection,
        bool includeGap = true, bool includeChemIn = true, bool includeChemOut = true) : PlotGeneratorOfCells(plotGenerator, timeArray, iStart, iEnd, groupSeq, cells, plotSelection)
    {
        private readonly bool includeGap = includeGap;
        private readonly bool includeChemIn = includeChemIn;
        private readonly bool includeChemOut = includeChemOut;

        private void CreateIndividualJunctionsCharts()
        {
            UnitOfMeasure UoM = plotGenerator.UoM;
            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools: false, combineSomites: false, combineCells: false);
            List<GapJunction> gapJunctions = [];
            List<ChemicalSynapse> synapses = [];
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
                                PlotGeneratorCurrentsOfJunctions junctionPG = new(plotGenerator, timeArray, iStart, iEnd, 0, null, [synapse], true);
                                junctionPG.CreateCharts(charts);
                            }
                        }
                        else if (cell is MuscleCell muscleCell)
                        {
                            foreach (ChemicalSynapse synapse in muscleCell.EndPlates)
                            {
                                if (!synapse.InputCurrent.Any(c => c != 0))
                                    continue;
                                PlotGeneratorCurrentsOfJunctions junctionPG = new(plotGenerator, timeArray, iStart, iEnd, 0, null, [synapse], true);
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
                            PlotGeneratorCurrentsOfJunctions junctionPG = new(plotGenerator, timeArray, iStart, iEnd, 1, [gapJunction], null, gapJunction.Cell2 == cell);
                            junctionPG.CreateCharts(charts);
                        }
                    }
                    if (includeChemOut && cell is Neuron neuron1)
                    {
                        foreach (ChemicalSynapse synapse in neuron1.Terminals)
                        {
                            if (!synapse.InputCurrent.Any(c => c != 0))
                                continue;
                            PlotGeneratorCurrentsOfJunctions junctionPG = new(plotGenerator, timeArray, iStart, iEnd, 2, null, [synapse], false);
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
            if (cells == null || cells.Count == 0)
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
                Dictionary<string, List<double[]>> yMultiData = new() { { "SynIn", new() }, { "SynOut", new() }, { "Gap", new() } };
                Dictionary<string, double[]> yData = new() { { "SynIn", null }, { "SynOut", null }, { "Gap", null } };
                Dictionary<string, string> title = new() { { "SynIn", "Time," }, { "SynOut", "Time," }, { "Gap", "Time," } };
                Dictionary<string, List<string>> xData = new() {
                    { "SynIn", new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","))},
                    { "SynOut", new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","))},
                    { "Gap", new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","))} };
                Dictionary<string, List<Color>> colorPerChart = new() { { "SynIn", new() }, { "SynOut", new() }, { "Gap", new() } };
                Dictionary<string, bool> exists = new() { { "SynIn", false }, { "SynOut", false }, { "Gap", false } };

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
                            exists["SynIn"] = true;
                            if (combineCells)
                                title["SynIn"] += jnc.ID + ",";
                            else
                                title["SynIn"] += $"{jnc.PreNeuron.ID} {(useIdentifier ? "(" + jnc.Core.Identifier + ")" : "")} ,";
                            colorPerChart["SynIn"].Add(jnc.PreNeuron.CellPool.Color);
                            yMultiData["SynIn"].Add(jnc.InputCurrent[iStart..iEnd]);
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                xData["SynIn"][i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                        }
                    }
                    if (includeGap)
                    {
                        List<GapJunction> gapJunctions = cell.GapJunctions.Where(j => j.Cell2 == cell || j.Cell1 == cell).ToList();
                        bool useIdentifier = gapJunctions.GroupBy(j => j.ID).Count() != gapJunctions.Count;
                        foreach (GapJunction jnc in gapJunctions)
                        {
                            exists["Gap"] = true;
                            Cell otherCell = jnc.Cell1 == cell ? jnc.Cell2 : jnc.Cell1;
                            title["Gap"] += $"Gap: {jnc.ID} {(useIdentifier ? "(" + jnc.Core.Identifier + ")" : "")},";
                            colorPerChart["Gap"].Add(otherCell.CellPool.Color);
                            yMultiData["Gap"].Add(jnc.InputCurrent[iStart..iEnd]);
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                xData["Gap"][i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                        }
                    }
                    if (includeChemOut)
                    {
                        if (cell is Neuron neuron2)
                        {
                            bool useIdentifier = neuron2.Terminals.GroupBy(j => j.ID).Count() != neuron2.Terminals.Count;
                            foreach (ChemicalSynapse jnc in neuron2.Terminals)
                            {
                                exists["SynOut"] = true;
                                if (combineCells)
                                    title["SynOut"] += jnc.ID + ",";
                                else
                                    title["SynOut"] += $"{jnc.PostCell.ID} {(useIdentifier ? "(" + jnc.Core.Identifier + ")" : "")} ,";
                                colorPerChart["SynOut"].Add(jnc.PostCell.CellPool.Color);
                                yMultiData["SynOut"].Add(jnc.InputCurrent[iStart..iEnd]);
                                foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                    xData["SynOut"][i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                            }
                        }
                    }
                }

                double yMin = cells.Min(c => c.MinCurrentValue(includeGap, includeChemIn, includeChemOut, iStart, iEnd));
                double yMax = cells.Max(c => c.MaxCurrentValue(includeGap, includeChemIn, includeChemOut, iStart, iEnd));
                Util.SetYRange(ref yMin, ref yMax);
                foreach (string key in yMultiData.Keys)
                {
                    if (yMultiData[key].Count == 1)
                    {
                        yData[key] = yMultiData[key].FirstOrDefault();
                        yMultiData[key] = null;
                    }
                }

                if (exists["SynIn"])
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinSynInCurrentValue(iStart, iEnd));
                        yMax = cellGroup.Max(c => c.MaxSynInCurrentValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    string csvData = title["SynIn"][..^1] + "\n" + string.Join("\n", xData["SynIn"].Select(line => line[..^1]).ToArray());
                    Chart chart = new()
                    {
                        GroupSeq = GroupSeq,
                        CsvData = csvData,
                        Colors = colorPerChart["SynIn"],
                        Title = $"{cellGroup.Key} Incoming Synaptic Currents",
                        yLabel = $"Current ({Util.GetUoM(UoM, Measure.Current)})",
                        xMin = timeArray[iStart],
                        xMax = timeArray[iEnd] + 1,
                        xData = timeArray[iStart..iEnd],
                        yMin = yMin,
                        yMax = yMax,
                        yData = yData["SynIn"],
                        yMultiData = yMultiData["SynIn"]
                    };
                    if (!AddChart(chart)) return;
                }
                if (exists["Gap"])
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinGapCurrentValue(iStart, iEnd));
                        yMax = cellGroup.Max(c => c.MaxGapCurrentValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    string csvData = title["Gap"][..^1] + "\n" + string.Join("\n", xData["Gap"].Select(line => line[..^1]).ToArray());
                    Chart chart = new()
                    {
                        GroupSeq = GroupSeq + 1,
                        CsvData = csvData,
                        Colors = colorPerChart["Gap"],
                        Title = $"{cellGroup.Key} Gap Currents",
                        yLabel = $"Current ({Util.GetUoM(UoM, Measure.Current)})",
                        xMin = timeArray[iStart],
                        xMax = timeArray[iEnd] + 1,
                        xData = timeArray[iStart..iEnd],
                        yMin = yMin,
                        yMax = yMax,
                        yData = yData["Gap"],
                        yMultiData = yMultiData["Gap"]
                    };
                    if (!AddChart(chart)) return;
                }
                if (exists["SynOut"])
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinSynOutCurrentValue(iStart, iEnd));
                        yMax = cellGroup.Max(c => c.MaxSynOutCurrentValue(iStart, iEnd));
                        Util.SetYRange(ref yMin, ref yMax);
                    }

                    string csvData = title["SynOut"][..^1] + "\n" + string.Join("\n", xData["SynOut"].Select(line => line[..^1]).ToArray());
                    Chart chart = new()
                    {
                        GroupSeq = GroupSeq + 2,
                        CsvData = csvData,
                        Colors = colorPerChart["SynOut"],
                        Title = $"{cellGroup.Key} Outgoing Synaptic Currents",
                        yLabel = $"Current ({Util.GetUoM(UoM, Measure.Current)})",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = timeArray[iStart],
                        xMax = timeArray[iEnd] + 1,
                        xData = timeArray[iStart..iEnd],
                        yData = yData["SynOut"],
                        yMultiData = yMultiData["SynOut"]
                    };
                    if (!AddChart(chart)) return;
                }
            }
        }
    }
}
