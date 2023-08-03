using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorStimuli : PlotGeneratorOfCells
    {
        private readonly UnitOfMeasure uoM;
        public PlotGeneratorStimuli(PlotGenerator plotGenerator, List<Cell> cells, double[] timeArray, bool combinePools, bool combineSomites, bool combineCells, int iStart, int iEnd, UnitOfMeasure uoM) :
            base(plotGenerator, timeArray, iStart, iEnd, cells, combinePools, combineSomites, combineCells)
        {
            this.uoM = uoM;
        }
        protected override void CreateCharts()
        {
            if (cells == null || !cells.Any())
                return;
            double yMin = cells.Min(c => c.MinStimulusValue());
            double yMax = cells.Max(c => c.MaxStimulusValue());
            Util.SetYRange(ref yMin, ref yMax);

            IEnumerable<IGrouping<string, Cell>> cellGroups = PlotSelectionMultiCells.GroupCells(cells, combinePools, combineSomites, combineCells);
            foreach (IGrouping<string, Cell> cellGroup in cellGroups)
            {
                string columnTitles = "Time,";
                List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
                List<string> colorPerChart = new();
                bool stimExists = false;


                foreach (Cell cell in cellGroup.Where(c => c.Stimuli.HasStimulus))
                {
                    stimExists = true;
                    columnTitles += cell.ID + ",";
                    colorPerChart.Add(cell.CellPool.Color.ToRGBQuoted());
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += cell.Stimuli.GetStimulus(iStart + i).ToString(GlobalSettings.PlotDataFormat) + ",";
                }
                if (stimExists)
                {
                    if (!GlobalSettings.SameYAxis)
                    {
                        yMin = cellGroup.Min(c => c.MinStimulusValue());
                        yMax = cellGroup.Max(c => c.MaxStimulusValue());
                        Util.SetYRange(ref yMin, ref yMax);
                    }
                    string csvData = "`" + columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
                    Chart chart = new()
                    {
                        CsvData = csvData,
                        Color = string.Join(',', colorPerChart),
                        Title = $"`{cellGroup.Key} Applied Stimuli`",
                        yLabel = $"`Stimulus ({Util.GetUoM(uoM, Measure.Current)})`",
                        yMin = yMin,
                        yMax = yMax,
                        xMin = timeArray[iStart],
                        xMax = timeArray[iEnd] + 1
                    };
                    if (!AddChart(chart))
                        return;
                }
            }
        }

    }


}
