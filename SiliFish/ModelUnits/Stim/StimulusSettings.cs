using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusSettings
    {
        public StimulusMode Mode { get; set; }
        //mode = Gaussian: value1 and value2 are mean and SD 
        //mode = Step: Value1:the stimulus value, Value2; obsolete
        //mode = Ramp: Value1: stimulus at time start, Value2: stimulus at time end
        //mode = Sinusoidal: Value1; the stimulus value, Value2; obsolete, Frequency: in kHz
        //mode = Pulse: Value1: stimulus value, Value2; duration in each pulse (in ms), Frequency: in kHz

        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double? Frequency { get; set; }

        public StimulusSettings()
        {
        }
        public StimulusSettings(StimulusMode mode, double value1, double value2, double freq)
        {
            Mode = mode;
            Value1 = value1;
            Value2 = value2;
            Frequency = freq; 
        }
        public StimulusSettings Clone()
        {
            return (StimulusSettings)MemberwiseClone();
        }
        public override string ToString()
        {
            return Mode switch
            {
                StimulusMode.Step => $"{Mode} {Value1}, Noise: {Value2}",
                StimulusMode.Gaussian => $"{Mode} µ: {Value1}, SD: {Value2}",
                StimulusMode.Ramp => $"{Mode} {Value1} - {Value2}",
                StimulusMode.Sinusoidal => $"{Mode} Amplitude: {Value1}, Freq: {Frequency}",
                StimulusMode.Pulse => $"{Mode} Amplitude: {Value1}, Duration: {Value2} ms, Freq: {Frequency}",
                _ => "",
            };
        }
    }

}
