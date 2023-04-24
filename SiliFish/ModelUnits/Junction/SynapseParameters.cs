using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Junction
{
    public class SynapseParameters
    {
        public double TauD { get; set; }
        public double TauR { get; set; }
        public double Vth { get; set; }
        public double Erev { get; set; }

        [JsonIgnore, Browsable(false)]
        public static string CSVExportColumnNames => $"Tau Decay,Tau Rise, V Threshold, Reversal Potential";

        [JsonIgnore, Browsable(false)]
        internal static int CSVExportColumCount => CSVExportColumnNames.Split(',').Length;
        [JsonIgnore, Browsable(false)]
        public string CSVExportValues
        {
            get => $"{TauD},{TauR},{Vth},{Erev}";
            set
            {
                string[] values = value.Split(',');
                if (values.Length != CSVExportColumCount) return;
                try
                {
                    TauD = double.Parse(values[0]);
                    TauR = double.Parse(values[1]);
                    Vth = double.Parse(values[2]);
                    Erev = double.Parse(values[3]);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw;
                }
            }
        }
        public SynapseParameters() { }
        public SynapseParameters(SynapseParameters sp)
        {
            if (sp == null) return;
            TauD = sp.TauD;
            TauR = sp.TauR;
            Vth = sp.Vth;
            Erev = sp.Erev;
        }

        public bool CheckValues(ref List<string> errors)
        {
            errors ??= new();
            if (TauD < GlobalSettings.Epsilon || TauR < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Tau has 0 value.");
            return errors.Count == 0;
        }
        internal object GetTooltip()
        {
            return $"Tau D: {TauD:0.###}\r\n" +
                $"Tau R: {TauR:0.###}\r\n" +
                $"V thresh: {Vth:0.###}\r\n" +
                $"E rev: {Erev:0.###}";
        }
    }

}
