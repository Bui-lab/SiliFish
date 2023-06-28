using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    public class PlotGeneratorCurrentsOfCells : PlotGeneratorOfCells
    {
        private readonly UnitOfMeasure uoM;
        private readonly bool includeGap;
        private readonly bool includeChemIn;
        private readonly bool includeChemOut;

        public PlotGeneratorCurrentsOfCells(List<Cell> cells, double[] timeArray, bool combinePools, bool combineSomites, bool combineCells, int iStart, int iEnd,
            UnitOfMeasure uoM, bool includeGap = true, bool includeChemIn = true, bool includeChemOut = true) :
            base(timeArray, iStart, iEnd, cells, combinePools, combineSomites, combineCells)
        {
            this.uoM = uoM;
            this.includeGap = includeGap;
            this.includeChemIn = includeChemIn;
            this.includeChemOut = includeChemOut;
        }
        protected override void CreateCharts()
        {
            if (cells == null || !cells.Any())
                return;
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
                    if (includeGap)
                    {
                        foreach (GapJunction jnc in cell.GapJunctions.Where(j => j.Cell2 == cell || j.Cell1 == cell))
                        {
                            jnc.PopulateCurrentArray(timeArray.Length);
                            gapExists = true;
                            Cell otherCell = jnc.Cell1 == cell ? jnc.Cell2 : jnc.Cell1;
                            string cellID = combineCells ? cell.ID + "↔" : "";
                            gapTitle += $"Gap: {cellID}{otherCell.ID},";
                            colorPerGapChart.Add(otherCell.CellPool.Color.ToRGBQuoted());
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                gapData[i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                        }
                    }
                    if (includeChemIn)
                    {
                        List<ChemicalSynapse> synapses = null;
                        if (cell is Neuron neuron) synapses = neuron.Synapses;
                        else if (cell is MuscleCell muscleCell) synapses = muscleCell.EndPlates;
                        foreach (ChemicalSynapse jnc in synapses)
                        {
                            jnc.PopulateCurrentArray(timeArray.Length);
                            synInExists = true;
                            if (combineCells)
                                synInTitle += cell.ID + "←";
                            synInTitle += jnc.PreNeuron.ID + ",";
                            colorPerInSynChart.Add(jnc.PreNeuron.CellPool.Color.ToRGBQuoted());
                            foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                                synInData[i] += jnc.InputCurrent[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                        }
                    }
                    if (includeChemOut)
                    {
                        if (cell is Neuron neuron2)
                        {
                            foreach (ChemicalSynapse jnc in neuron2.Terminals)
                            {
                                jnc.PopulateCurrentArray(timeArray.Length);
                                synOutExists = true;
                                if (combineCells)
                                    synOutTitle += cell.ID + "→";
                                synOutTitle += jnc.PostCell.ID + ",";
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
                        CsvData = csvData,
                        Color = string.Join(',', colorPerGapChart),
                        Title = $"`{cellGroup.Key} Gap Currents`",
                        yLabel = $"`Current ({Util.GetUoM(uoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = timeArray[iStart],
                        xMax = timeArray[iEnd] + 1
                    };
                    if (!AddChart(chart)) return;
                }
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
                        CsvData = csvData,
                        Color = string.Join(',', colorPerInSynChart),
                        Title = $"`{cellGroup.Key} Incoming Synaptic Currents`",
                        yLabel = $"`Current ({Util.GetUoM(uoM, Measure.Current)})`",
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
                        CsvData = csvData,
                        Color = string.Join(',', colorPerOutSynChart),
                        Title = $"`{cellGroup.Key} Outgoing Synaptic Currents`",
                        yLabel = $"`Current ({Util.GetUoM(uoM, Measure.Current)})`",
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
