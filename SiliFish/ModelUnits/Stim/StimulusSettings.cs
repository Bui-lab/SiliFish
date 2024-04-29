using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusSettings: IDataExporterImporter
    {
        public StimulusMode Mode { get; set; }
        //mode = Gaussian: value1 and value2 are mean and SD 
        //mode = Step: Value1:the stimulus value, Value2: obsolete
        //mode = Ramp: Value1: stimulus at time start, Value2: stimulus at time end
        //mode = Sinusoidal: Value1; the stimulus value, Value2; obsolete, Frequency: in Hz
        //mode = Pulse: Value1: stimulus value, Value2; duration in each pulse (in ms), Frequency: in Hz

        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double? Frequency { get; set; }
        public TimeLine TimeLine_ms { get; set; } = new TimeLine();

        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames = ["Mode", "Value1", "Value2", "Frequency"];

        public List<string> ExportValues()
        {
            return ListBuilder.Build<string>(Mode,Value1,Value2,Frequency);
        }
        public void ImportValues(List<string> values)
        {
            if (values.Count < ColumnNames.Count - 1) return;//Frequency can be null and not included in the incoming string
            Mode = (StimulusMode)Enum.Parse(typeof(StimulusMode), values[0]);
            Value1 = double.Parse(values[1]);
            Value2 = double.Parse(values[2]);
            if (values.Count < 4) return;
            if (double.TryParse(values[3], out double f))
                Frequency = f;
        }
        public StimulusSettings()
        {
        }

        public StimulusSettings Clone()
        {
            return (StimulusSettings)MemberwiseClone();
        }
        public override string ToString()
        {
            return Mode switch
            {
                StimulusMode.Step => $"{Mode} {Value1}",
                StimulusMode.Gaussian => $"{Mode} µ: {Value1}, SD: {Value2}",
                StimulusMode.Ramp => $"{Mode} {Value1} - {Value2}",
                StimulusMode.Sinusoidal => $"{Mode} Amplitude: {Value1}, Freq: {Frequency}",
                StimulusMode.Pulse => $"{Mode} Amplitude: {Value1}, Duration: {Value2} ms, Freq: {Frequency}",
                _ => "",
            };
        }
    }

}
