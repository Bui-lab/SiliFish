using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class ContractibleCellCore: CellCore
    {
        [Description("Vm when tension is half of Tmax/2")]
        public double Va { get; set; }

        [Description("Maximum tension")]
        public double Tmax { get; set; }

        [Description("Contraction threshold potential to determine rheobase for non-spiking cores")]
        public double Vcontraction { get; set; } = GlobalSettings.BiologicalMaxPotential;

        [Description("Slope factor")]// [Dulhunty 1992 (Prog. Biophys)]
        public double ka { get; set; }

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
