using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.Definitions
{
    public class GlobalSettings
    {
        public static double Epsilon = 0.00001;

        [Description(""), DisplayName("Number Format"), Category("Plotting")]
        public static string DecimalPointFormat { get; set; } = "0.0###";
        [Description("In ms. The duration used to calculate rheobase values."),
            DisplayName("Rheobase Infinity"),
            Category("Dynamics")]
        public static int RheobaseInfinity { get; set; } = 500;
        [Description("The rheobase multipliers displayed on 'Test Dynamics' user interface."),
            DisplayName("Rheobase multipliers"),
            Category("Dynamics")]
        public static double[] RheobaseTestMultipliers { get; set; } = new double[] { 1, 1.1, 1.5 };//, 2 };

        [JsonIgnore, Browsable(false)]
        public static List<string> TempFiles = new();

    }

}
