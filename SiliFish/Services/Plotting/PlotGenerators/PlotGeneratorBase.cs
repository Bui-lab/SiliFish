﻿using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal abstract class PlotGeneratorBase
    {
        protected PlotGenerator plotGenerator;
        protected readonly double[] timeArray;
        protected readonly int iStart;
        protected readonly int iEnd;
        public List<Chart> charts { get; set; }
        public bool AddChart(Chart chart)
        {
            if (chart.NumOfDataPoints + plotGenerator.NumOfDataPoints > GlobalSettings.PlotDataPointLimit)
            {
                plotGenerator.errorMessage = $"The number of data points exceed {GlobalSettings.PlotDataPointLimit}. Not all charts are plotted. Please limit the selection or the time range.";
                if (charts.Count == 0)
                    charts.Add(chart);//add the first chart, even if it exceeds the limit
                return false;
            }
            charts.Add(chart);
            return true;
        }
        public PlotGeneratorBase(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd)
        {
            charts = new();
            this.plotGenerator = plotGenerator;
            this.timeArray = timeArray;
            this.iStart = iStart;
            this.iEnd = iEnd;
        }
        protected abstract void CreateCharts();
        public void CreateCharts(List<Chart> chartList)
        {
            CreateCharts();
            if (charts.Any())
                chartList.AddRange(charts);
        }
    }

}