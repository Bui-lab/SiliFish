using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Junction;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorCurrentsOfJunctions : PlotGeneratorBase
    {
        readonly List<GapJunction> gapJunctions;
        readonly List<ChemicalSynapse> synapses;
        public PlotGeneratorCurrentsOfJunctions(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
            List<GapJunction> gapJunctions, List<ChemicalSynapse> synapses) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, plotSelection: null)
        {
            this.gapJunctions = gapJunctions;
            this.synapses = synapses;
        }
        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }
        protected override void CreateCharts()
        {
            UnitOfMeasure UoM = plotGenerator.UoM;

            List<Chart> gapCharts = new();
            List<Chart> synCharts = new();

            double yMin = double.MaxValue;
            double yMax = double.MinValue;
            if (gapJunctions?.Count > 0)
            {
                yMin = gapJunctions.Min(jnc => jnc.InputCurrent?.Min() ?? 0);
                yMax = gapJunctions.Max(jnc => jnc.InputCurrent?.Max() ?? 0);
            }
            if (synapses?.Count > 0)
            {
                yMin = Math.Min(yMin, synapses.Min(jnc => jnc.InputCurrent?.Min() ?? 0));
                yMax = Math.Max(yMax, synapses.Max(jnc => jnc.InputCurrent?.Max() ?? 0));
            }

            Util.SetYRange(ref yMin, ref yMax);
            List<double[]> yMultiData = [];
            double[] yData = null;
            string columnTitles = "Time,";
            List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
            List<Color> colorPerChart = [];

            if (gapJunctions != null)
            {
                bool useIdentifier = gapJunctions.GroupBy(j => j.ID).Count() != gapJunctions.Count;
                foreach (GapJunction jnc in gapJunctions)
                {
                    colorPerChart.Add(jnc.Cell1.CellPool.Color);
                    yMultiData.Add(jnc.InputCurrent[iStart..iEnd]);
                    columnTitles += $"{jnc.ID} {(useIdentifier ? "(" + jnc.Core.Identifier + ")" : "")},";
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += jnc.InputCurrent?[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                }
            }
            if (synapses != null)
            {
                bool useIdentifier = synapses.GroupBy(j => j.ID).Count() != synapses.Count;
                foreach (ChemicalSynapse jnc in synapses)
                {
                    colorPerChart.Add(jnc.PreNeuron.CellPool.Color);
                    yMultiData.Add(jnc.InputCurrent[iStart..iEnd]);

                    columnTitles += $"{jnc.ID} {(useIdentifier ? "(" + jnc.Core.Identifier + ")" : "")},";
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += jnc.InputCurrent?[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                }
            }

            string csvData = columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray());
            string ampere = UoM == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens ? "pA" :
                UoM == UnitOfMeasure.milliVolt_nanoAmpere_MegaOhm_nanoFarad_microSiemens ? "nA" : "";
            string title = $"{columnTitles[5..].TrimEnd(',')}";
            if (title.Length > 50)
            {
                if (columnTitles.Split(',').Length > 2)
                    title = "Multiple currents";
                else
                    title = title[..50] + "...";
            }
            if (yMultiData.Count == 1)
            {
                yData = yMultiData.FirstOrDefault();
                yMultiData = null;
            }
            Chart chart = new()
            {
                CsvData = csvData,
                Title = title,
                Colors = colorPerChart,
                yLabel = $"Current ({ampere})",
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
