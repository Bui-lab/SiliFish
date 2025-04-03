using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.DataTypes
{
    public struct XYDataSet
    {
        public string Title;
        public string[] XValues;
        public double[] YValues;

        public XYDataSet(string TitleIn, string[] xData, double[] yData)
        {
            Title = TitleIn;
            XValues = xData;
            YValues = yData;
        }
        public XYDataSet(string TitleIn, List<double> xData, IEnumerable<int> yData) 
        {
            Title = TitleIn;
            XValues = xData.Select(x => x.ToString()).ToArray();
            YValues = yData.Select(x => (double)x).ToArray();
        }
    }
}
