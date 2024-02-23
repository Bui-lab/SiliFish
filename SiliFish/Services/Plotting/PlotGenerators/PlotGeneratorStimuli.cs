using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorStimuli : PlotGeneratorOfCells
    {
        public PlotGeneratorStimuli(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq, 
            List<Cell> cells, PlotSelectionInterface plotSelection) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, cells, plotSelection)
        {
        }
        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }
        protected override void CreateCharts()
        {
            if (cells == null || cells.Count == 0)
                return;
            UnitOfMeasure UoM = plotGenerator.UoM;

            double yMin = cells.Min(c => c.MinStimulusValue());
            double yMax = cells.Max(c => c.MaxStimulusValue());
            Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                List<double[]> yMultiData = [];
                double[] yData = null;
                string columnTitles = "Time,";
                List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<Color> colorPerChart = [];
                bool stimExists = false;


                foreach (Cell cell in cellGroup.Where(c => c.Stimuli.HasStimulus))
                {
                    stimExists = true;
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color);
                    yData = new double[iEnd - iStart + 1];
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                    {
                        yData[i] = cell.Stimuli.GetStimulus(iStart + i);
                        data[i] += yData[i].ToString(GlobalSettings.PlotDataFormat) + ",";
                    }
                    yMultiData.Add(yData);
                }
                if (stimExists)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinStimulusValue());
                        yMax = cellGroup.Max(c => c.MaxStimulusValue());
                        Util.SetYRange(ref yMin, ref yMax);
                    }
                if (yMultiData.Count == 1)
                {
                    yData = yMultiData.FirstOrDefault();
                    yMultiData = null;
                }
                    string csvData = columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray());
                    Chart chart = new()
                    {
                        CsvData = csvData,
                        Colors = colorPerChart,
                        Title = $"{cellGroup.Key} Applied Stimuli",
                        yLabel = $"Stimulus ({Util.GetUoM(UoM, Measure.Current)})",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = timeArray[iStart],
                    xMax = timeArray[iEnd] + 1,
                    xData = timeArray[iStart..iEnd],
                    yData = yData,
                    yMultiData = yMultiData
                    };
                    if (!AddChart(chart))
                        return;
                }
            }
        }

    }


}
