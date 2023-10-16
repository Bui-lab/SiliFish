using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Junction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorCurrentsOfJunctions : PlotGeneratorBase
    {
        readonly List<GapJunction> gapJunctions;
        readonly List<ChemicalSynapse> synapses;
        private readonly UnitOfMeasure uoM;
        public PlotGeneratorCurrentsOfJunctions(PlotGenerator plotGenerator, List<GapJunction> gapJunctions, List<ChemicalSynapse> synapses,
            double[] timeArray, int iStart, int iEnd, UnitOfMeasure uoM) :
            base(plotGenerator, timeArray, iStart, iEnd)
        {
            this.gapJunctions = gapJunctions;
            this.synapses = synapses;
            this.uoM = uoM;
        }
        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }
        protected override void CreateCharts()
        {
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

            string columnTitles = "Time,";
            List<string> data = new(timeArray.Skip(iStart).Take(iEnd - iStart + 1).Select(t => t.ToString(GlobalSettings.PlotDataFormat) + ","));
            List<string> colorPerChart = new();

            if (gapJunctions != null)
            {
                bool useIdentifier = gapJunctions.GroupBy(j => j.ID).Count() != gapJunctions.Count;
                foreach (GapJunction jnc in gapJunctions)
                {
                    colorPerChart.Add(jnc.Cell1.CellPool.Color.ToRGBQuoted());
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
                    colorPerChart.Add(jnc.PreNeuron.CellPool.Color.ToRGBQuoted());
                    columnTitles += $"{jnc.ID} {(useIdentifier ? "(" + jnc.Core.Identifier + ")" : "")},";
                    foreach (int i in Enumerable.Range(0, iEnd - iStart + 1))
                        data[i] += jnc.InputCurrent?[iStart + i].ToString(GlobalSettings.PlotDataFormat) + ",";
                }
            }

            string csvData = "`" + columnTitles[..^1] + "\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray()) + "`";
            string ampere = uoM == UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens ? "pA" :
                uoM == UnitOfMeasure.milliVolt_nanoAmpere_MegaOhm_nanoFarad_microSiemens ? "nA" : "";
            string title = $"`{columnTitles[5..].TrimEnd(',')}`";
            if (title.Length > 50)
            {
                if (columnTitles.Split(',').Length > 2)
                    title = "Multiple currents";
                else
                    title = title[..50] + "...";
            }
            Chart chart = new()
            {
                CsvData = csvData,
                Title = title,
                Color = string.Join(',', colorPerChart),
                yLabel = $"`Current ({ampere})`",
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
