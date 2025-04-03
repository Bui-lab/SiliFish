using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Services.Plotting.PlotSelection;
using System.Collections.Generic;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal abstract class PlotGeneratorBase(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq, PlotSelectionInterface plotSelection)
    {
        protected PlotGenerator plotGenerator = plotGenerator;
        readonly protected PlotSelectionInterface plotSelection = plotSelection;
        protected readonly double[] timeArray = timeArray;
        protected readonly int iStart = iStart;
        protected readonly int iEnd = iEnd;
        protected readonly int GroupSeq = groupSeq;
        public List<Chart> charts { get; set; } = [];
        public bool AddChart(Chart chart, int? chartSeq = null)
        {
            if (chart.NumOfDataPoints + plotGenerator.NumOfDataPoints > GlobalSettings.PlotDataPointLimit)
            {
                plotGenerator.errorMessage = $"The number of data points exceed {GlobalSettings.PlotDataPointLimit}. " +
                    $"Not all charts are plotted. Please limit the selection or the time range." +
                    $"(You can set the number of data points that triggers this warning through 'Settings')";
                if (charts.Count == 0)
                    charts.Add(chart);//add the first chart, even if it exceeds the limit
                return false;
            }
            if (chart.GroupSeq < GroupSeq)
                chart.GroupSeq = GroupSeq;
            chart.ChartSeq = chartSeq ?? charts.Count;
            charts.Add(chart);
            return true;
        }

        protected abstract void CreateCharts(PlotType plotType);
        protected abstract void CreateCharts();
        public void CreateCharts(List<Chart> chartList, PlotType plotType)
        {
            CreateCharts(plotType);
            if (charts.Count != 0)
                chartList.AddRange(charts);
        }
        public void CreateCharts(List<Chart> chartList)
        {
            CreateCharts();
            if (charts.Count != 0)
                chartList.AddRange(charts);
        }
    }
}
