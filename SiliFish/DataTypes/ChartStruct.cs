using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.DataTypes
{
    public struct ChartDataStruct
    {
        public string Title = "", xLabel = "Time (ms)", yLabel = "";
        public Color Color = Color.Red;
        public double[] xData = null;
        public double[] yData = null;
        public bool drawPoints = false;
        public bool logScale = false;
        public ChartDataStruct()
        {
        }
    }
}
