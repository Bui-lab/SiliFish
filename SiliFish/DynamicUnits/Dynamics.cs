using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits
{
    public class Dynamics
    {
        public Dictionary<double, double> tauDecay, tauRise;
        public double[] VList;
        public double[] secList; //u for neurons and rel. tension for muscle cells
        public Dynamics(int iMax)
        {
            VList = new double[iMax];
            secList = new double[iMax];
            tauDecay = new();
            tauRise = new();
        }
    }
}
