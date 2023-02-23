﻿using SiliFish.DataTypes;
using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusSettings
    {
        public StimulusMode Mode { get; set; }
        //mode = Gaussian: value1 and value2 are mean and SD 
        //mode = Step: Value1:the stimulus value, Value2; obsolete
        //mode = Ramp: Value1: stimulus at time start, Value2: stimulus at time end
        //mode = Sinusoidal: Value1; the stimulus value, Value2; obsolete, Frequency: in Hz
        //mode = Pulse: Value1: stimulus value, Value2; duration in each pulse (in ms), Frequency: in Hz

        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double? Frequency { get; set; }

        [JsonIgnore]
        public static string CSVExportColumnNames => $"Mode,Value1,Value2,Frequency";
        [JsonIgnore]
        public static int CSVExportColumCount => CSVExportColumnNames.Split(',').Length;
        [JsonIgnore]
        public string CSVExportValues
        {
            get => $"{Mode},{Value1},{Value2},{Frequency}";
            set
            {
                string[] values = value.Split(',');
                if (values.Length != CSVExportColumCount) return;
                Mode = (StimulusMode)int.Parse(values[0]);
                Value1 = double.Parse(values[1]);
                Value2 = double.Parse(values[2]);
                Frequency = double.Parse(values[3]);
            }
        }
        

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
