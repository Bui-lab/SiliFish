using System;
using System.Linq;
using System.Text.Json.Serialization;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;

namespace SiliFish.ModelUnits
{
    public class StimulusSettings
    {
        public StimulusMode Mode { get; set; }
        //mode = Gaussian: value1 and value2 are mean and SD 
        //mode = Step: Value1 is the stimulus value, Value2 is obsolete
        //mode = Ramp: Value1: stimulus at time start, Value2: stimulus at time end
        public double Value1 { get; set; }
        public double Value2 { get; set; }

        public StimulusSettings()
        {
        }
        public StimulusSettings(StimulusMode mode, double value1, double value2)
        {
            Mode = mode;
            Value1 = value1;
            Value2 = value2;
        }
        public override string ToString()
        {
            switch (Mode)
            {
                case StimulusMode.Step:
                    return string.Format("{0} {1}, Noise: {2}", Mode.ToString(), Value1, Value2);
                case StimulusMode.Gaussian:
                    return string.Format("{0} µ: {1}, SD: {2}", Mode.ToString(), Value1, Value2);
                case StimulusMode.Ramp:
                    return string.Format("{0} {1} - {2}", Mode.ToString(), Value1, Value2);
                case StimulusMode.Sinusoidal:
                    return string.Format("{0} Amplitude: {1}, Freq: {2}", Mode.ToString(), Value1, Value2);
            }
            return "";
        }
    }
    public class Stimulus
    {
        public static int nMax; //in time increments

        public StimulusSettings StimulusSettings { get; set; }
        public StimulusMode Mode 
        { 
            get { return StimulusSettings.Mode; }
            set { StimulusSettings.Mode = value; } 
        }
        public double Value1
        {
            get { return StimulusSettings.Value1; }
            set { StimulusSettings.Value1 = value; }
        }
        public double Value2
        {
            get { return StimulusSettings.Value2; }
            set { StimulusSettings.Value2 = value; }
        }

        public TimeLine TimeSpan_ms { get; set; } //in  milliseconds
        private double tangent; //valid only if mode==Ramp
        private int iStart, iEnd;
        private double[] values = null;
        private bool initialized = false;
        public double MinValue { get { return values?.Min() ?? 0; } }
        public double MaxValue { get { return values?.Max() ?? 0; } }

        public Stimulus() { }
        public Stimulus(StimulusSettings settings, TimeLine tl)
        {
            StimulusSettings = settings;
            TimeSpan_ms = new(tl);
        }
        public double this[int index]
        {
            get 
            { 
                if (values?.Length > index) 
                    return values[index];
                return 0;
            }
        }
        public override string ToString()
        {
            return StimulusSettings.ToString();
        }

        public string GetTooltip()
        {
            return $"{ToString()}\r\nTimeLine: {TimeSpan_ms}";
        }
        private void Initialize()
        {
            if (initialized)
                return;

            iEnd = (int)(TimeSpan_ms.End / RunParam.static_dt );
            if (iEnd < 0)
                iEnd = nMax;
            iStart = (int)(TimeSpan_ms.Start / RunParam.static_dt );
            if (StimulusSettings.Mode == StimulusMode.Ramp)
                if (iEnd > iStart)
                    tangent = (StimulusSettings.Value2 - StimulusSettings.Value1) / (iEnd - iStart);
                else tangent = 0;

            if (values != null)
            {
                double[] copyArr = new double[values.Length];
                values.CopyTo(copyArr, 0);
                values = new double[iEnd];
                copyArr.CopyTo(values, 0);
            }
            else
                values = new double[iEnd];
            initialized = true;
        }
        public double getStimulus(int tIndex, Random rand)
        {
            double t_ms = RunParam.GetTimeOfIndex(tIndex);
            if (!TimeSpan_ms.IsActive(t_ms))
                return 0;
            if (!initialized)
                Initialize();
            if (values.Length <= tIndex)
            {
                double[] copyArr = new double[nMax];  
                values.CopyTo(copyArr, 0);
                values = copyArr;
            }
            double ramp = (tIndex - iStart) * tangent;
            double value = 0;
            switch (Mode)
            {
                case StimulusMode.Step:
                    double noise = Value2 > 0 ? rand.Gauss(1, Value2) : 1;
                    value = Value1 * noise;
                    break;
                case StimulusMode.Ramp:
                    value = Value1 + ramp;
                    break;
                case StimulusMode.Gaussian:
                    value = rand.Gauss(Value1, Value2, Value1 - 3 * Value2, Value1 + 3 * Value2); // µ ± 3SD range
                    break;
                case StimulusMode.Sinusoidal:
                    value = Value1 * Math.Sin(2 * Math.PI * Value2 * t_ms);
                    break;
            }

            values[tIndex] = value;
            return value;
        }

        public double[] getValues(int nMax)
        {
            if (values?.Length < nMax)
            {
                double[] values2 = new double[nMax];
                Array.Copy(values, values2, values.Length);
                values = values2;
            }
            return values;
        }

        public void InitDataVectors(int nmax)
        {
            values = new double[nmax];
        }
    }

}
