using SiliFish.Definitions;
using SiliFish.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SiliFish.DataTypes
{
    public struct Chart
    {
        private long numOfPoints = 0;
        private string csvData = "";
        public string CsvData
        {
            get
            {
                if (string.IsNullOrEmpty(csvData))
                    GenerateCsv();
                return csvData;
            }
            set
            {
                csvData = value;
                long numOfLines = csvData.LongCount(c => c == '\n');
                int firstLineEnd = csvData.IndexOf('\n');
                string firstLine = csvData[..firstLineEnd];
                int numOfColumns = firstLine.Count(c => c == ',');
                numOfPoints = numOfLines * numOfColumns;
            }
        }
        public readonly long NumOfDataPoints => numOfPoints;

        public string Title = "", xLabel = "Time (ms)", yLabel = "";
        public List<Color> Colors = [];
        public readonly string csvColors => string.Join(',', Colors.Select(c => c.ToRGBQuoted()).ToList());
        public int GroupSeq;
        public int ChartSeq;
        public double[] xData = null;
        public double[] yData = null;
        //If multiple lines are plotted on the same chart, yMultiData is used instead of yData
        public List<double[]> yMultiData = null;
        public bool drawPoints = false;
        public bool logScale = false;
        public bool ScatterPlot = false;

        private double? xMinSet = null, xMaxSet = null;
        private double? yMinSet = null, yMaxSet = null;
        public double xMin
        {
            readonly get { return xMinSet ?? xData.Min(); }
            set { xMinSet = value; }
        }
        public double xMax
        {
            readonly get { return xMaxSet ?? xData.Max(); }
            set { xMaxSet = value; }
        }
        public double yMin
        {
            readonly get
            {
                return yMinSet ??
                    yData?.Min() ??
                    yMultiData?.Min(sd => sd.Min()) ?? 
                    0;
            }
            set { yMinSet = value; }
        }
        public double yMax 
        {
            readonly get
            {
                return yMaxSet ??
                    yData?.Max() ??
                    yMultiData?.Max(sd => sd.Max()) ?? 
                    0;
            }
            set { yMaxSet = value; }
        }
        public Chart()
        {
        }

        public Chart(int groupSeq, int chartSeq)
        {           
            GroupSeq = groupSeq;
            ChartSeq = chartSeq;
        }

        private void GenerateCsv()
        {
            string columnTitles = $"{xLabel},{yLabel}";
            string dpf = GlobalSettings.PlotDataFormat;
            List<string> data = new(xData.Select(t => t.ToString(dpf) + ","));
            if (yData != null)
            {
                numOfPoints = yData.LongLength;
                foreach (int i in Enumerable.Range(0, yData.Length))
                    data[i] += yData[i].ToString(GlobalSettings.PlotDataFormat) + ",";
            }
            else
            {
                numOfPoints = 0;
                for (int colIndex = 0; colIndex < yMultiData.Count; colIndex++)
                {
                    double[] singleyData = yMultiData[colIndex];
                    numOfPoints += singleyData.LongLength;
                    foreach (int i in Enumerable.Range(0, singleyData.Length))
                        data[i] += singleyData[i].ToString(GlobalSettings.PlotDataFormat) + ",";
                }
            }
            csvData = $"{columnTitles}\n" + string.Join("\n", data.Select(line => line[..^1]).ToArray());
        }
    }

}
