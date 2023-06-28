using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    public abstract class PlotGeneratorBase
    {
        protected readonly double[] timeArray;
        protected readonly int iStart;
        protected readonly int iEnd;
        public List<Chart> charts { get; set; }
        public long NumOfDataPoints { get; set; } = 0;//TODO carry this to PlotGenerator to handle the sum rather than individual data points
        public string errorMessage { get; set; }
        public bool AddChart(Chart chart)
        {
            if (chart.NumOfDataPoints + NumOfDataPoints > GlobalSettings.PlotDataPointLimit)
            {
                errorMessage = $"The number of data points exceed {GlobalSettings.PlotDataPointLimit}. Not all charts are plotted. Please limit the selection or the time range.";
                if (charts.Count == 0)
                    charts.Add(chart);//add the first chart, even if it exceeds the limit
                return false;
            }
            charts.Add(chart);
            return true;
        }
        public PlotGeneratorBase(double[] timeArray, int iStart, int iEnd)
        {
            errorMessage = "";
            charts = new();
            this.timeArray = timeArray;
            this.iStart = iStart;
            this.iEnd = iEnd;
        }
        protected abstract void CreateCharts();
        public void CreateCharts(List<Chart> chartList, out string errorMessage)
        {
            CreateCharts();
            if (charts.Any())
                chartList.AddRange(charts);
            errorMessage = this.errorMessage;
        }
    }

}
