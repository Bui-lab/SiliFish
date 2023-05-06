using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Helpers;
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
    public class SynapseParameters: IDataExporterImporter
    {
        public double TauD { get; set; }
        public double TauR { get; set; }
        public double Vth { get; set; }
        public double Erev { get; set; }

        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames = new() { "Tau Decay", "Tau Rise", "V Thresh", "Rev. Pot." };

        public static List<string> ExportBlankValues()
        {
            List<string> result = new();
            for (int i = 0; i < ColumnNames.Count; i++)
                result.Add(string.Empty);
            return result;
        }

        public List<string> ExportValues() => ListBuilder.Build<string>(TauD, TauR, Vth, Erev);
        public void ImportValues(List<string> values)
        {
            if (values.Count != ColumnNames.Count) return;
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
