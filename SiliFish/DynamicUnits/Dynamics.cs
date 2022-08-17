using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits
{
    public class Dynamics
    {
        public List<double> tauDecay, tauRise;
        public double[] Vlist;
        public double[] ulist;
        public Dynamics(int iMax)
        {
            Vlist = new double[iMax];
            ulist = new double[iMax];
            tauDecay = new();
            tauRise = new();
        }
    }
}
