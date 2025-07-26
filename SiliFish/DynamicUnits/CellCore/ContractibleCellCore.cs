using SiliFish.Database;
using System;
using System.ComponentModel;
using System.Linq;

namespace SiliFish.DynamicUnits
{
    public class ContractibleCellCore : CellCore
    {
        [Description("Vm when tension is half of Tmax/2")]
        public double Va { get; set; }

        [Description("Maximum tension")]
        public double Tmax { get; set; } = 1;

        [Description("Slope factor")]// [Dulhunty 1992 (Prog. Biophys)]
        public double ka { get; set; } = 1;

        //formula from [Dulhunty 1992 (Prog. Biophys)]
        public double CalculateRelativeTension(double? Vm = null) //if Vm is null, current V value is used
        {
            //T_a = T_max / (1 + exp(V_a - V_m) / k_a
            double dv = Va - (Vm ?? V);
            return 1 / (1 + Math.Exp(dv / ka));
        }

        //formula from [Dulhunty 1992 (Prog. Biophys)]
        public double CalculateTension(double? Vm = null) //if Vm is null, current V value is used
        {
            return Tmax * CalculateRelativeTension(Vm);
        }

        public double[] CalculateRelativeTension(double[] V)
        {
            return V.Select(v => CalculateRelativeTension(v)).ToArray();
        }
        public double[] CalculateTension(double[] V)
        {
            return V.Select(v => CalculateTension(v)).ToArray();
        }


    }

}
