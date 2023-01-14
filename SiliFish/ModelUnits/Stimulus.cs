using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

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
                case StimulusMode.Pulse:
                    return string.Format("{0} Amplitude: {1}, Freq: {2}", Mode.ToString(), Value1, Value2);
            }
            return "";
        }
    }

    public class Stimuli
    {
        public List<Stimulus> stimuli { get; set; }

        public Stimuli()
        {
            stimuli = new();
        }

        [JsonIgnore]
        public double MinValue { get { return stimuli.Any() ? stimuli.Min(s => s.MinValue) : 0; } }
        [JsonIgnore]
        public double MaxValue { get { return stimuli.Any() ? stimuli.Max(s => s.MaxValue) : 0; } }
        [JsonIgnore]
        public bool HasStimulus { get { return stimuli.Any(); } }
        public double GenerateStimulus(int timeIndex, Random rand)
        {
            return stimuli.Any() ? stimuli.Sum(s => s.generateStimulus(timeIndex, rand)) : 0;
        }

        public double GetStimulus(int timeIndex)
        {
            return stimuli.Any() ? stimuli.Sum(s => s[timeIndex]) : 0;
        }

        public double[] GetStimulusArray(int nmax)
        {
            double[] ret = new double[nmax];
            foreach (Stimulus s in stimuli)
                ret = ret.AddArray(s.getValues(nmax));
            return ret;
        }
        public virtual void InitDataVectors(int nmax)
        {
            foreach (Stimulus s in stimuli)
                s.InitDataVectors(nmax);
        }
        public void Add(Stimulus stim)
        {
            if (stim == null)
                return;
            stimuli.Add(stim);
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

            iEnd = (int)(TimeSpan_ms.End / RunParam.static_dt);
            if (iEnd < 0)
                iEnd = nMax;
            iStart = (int)(TimeSpan_ms.Start / RunParam.static_dt);
            if (StimulusSettings.Mode == StimulusMode.Ramp)
                if (iEnd > iStart)
                    tangent = (StimulusSettings.Value2 - StimulusSettings.Value1) / (iEnd - iStart);
                else tangent = 0;

            if (values != null && values.Length < iEnd)
            {
                double[] copyArr = new double[values.Length];
                values.CopyTo(copyArr, 0);
                values = new double[iEnd];
                copyArr.CopyTo(values, 0);
            }
            else if (values == null)
                values = new double[iEnd];
            initialized = true;
        }

        public double[] GenerateStimulus(int stimStart, int stimDuration, Random rand)
        {
            double[] stim = new double[stimStart + stimDuration];
            foreach (int i in Enumerable.Range(stimStart, stimDuration))
                stim[i] = generateStimulus(i, rand);
            return stim;
        }
        internal double generateStimulus(int tIndex, Random rand)
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
            double value = 0;
            switch (Mode)
            {
                case StimulusMode.Step:
                    double noise = Value2 > 0 ? rand.Gauss(1, Value2) : 1;
                    value = Value1 * noise;
                    break;
                case StimulusMode.Ramp:
                    double ramp = (tIndex - iStart) * tangent;
                    value = Value1 + ramp;
                    break;
                case StimulusMode.Gaussian:
                    value = rand.Gauss(Value1, Value2, Value1 - 3 * Value2, Value1 + 3 * Value2); // µ ± 3SD range
                    break;
                case StimulusMode.Sinusoidal:
                    double sinValue = Math.Sin(2 * Math.PI * Value2 * (t_ms - TimeSpan_ms.StartOf(t_ms)));
                    value = Value1 * sinValue;
                    break;
                case StimulusMode.Pulse:
                    value = 0;
                    if (Value2 > 0)//Value2 is the number of pulses
                    {
                        double timeRange = TimeSpan_ms.End > 0 ? TimeSpan_ms.End - TimeSpan_ms.Start : 1000;
                        double period = timeRange / Value2;
                        double t = t_ms - TimeSpan_ms.Start; //check only start and finish times of the full timeline - TimeSpan_ms.IsActive(t_ms) at the beginning of the function takes care of the filtering
                        while (t > period)
                            t -= period;
                        if (t <= period / 2)
                            value = Value1;
                    }
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
