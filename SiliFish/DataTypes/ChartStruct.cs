using System.Drawing;
using System.Linq;
using SiliFish.Extensions;

namespace SiliFish.DataTypes
{
    public struct ChartDataStruct
    {
        public string Title = "", xLabel = "Time (ms)", yLabel = "";
        public Color Color = Color.Red;
        public double[] xData = null;
        public double[] yData = null;
        //If multiple lines are plotted on the same chart, yMuliData is used instead of yData
        public double[,] yMultiData = null;
        public bool drawPoints = false;
        public bool logScale = false;

        private double? xMinSet = null, xMaxSet = null;
        public double xMin
        {
            get { return xMinSet ?? xData.Min(); }
            set { xMinSet = value; }
        }
        public double xMax
        {
            get { return xMaxSet ?? xData.Max(); }
            set { xMaxSet = value; }
        }
        public double yMin { get { return yData?.Min() ?? yMultiData?.Min() ?? 0; } }
        public double yMax { get { return yData?.Max() ?? yMultiData?.Max() ?? 0; } }
        public ChartDataStruct()
        {
        }
    }
}
