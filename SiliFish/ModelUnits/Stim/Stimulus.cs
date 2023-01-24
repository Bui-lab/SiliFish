using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Stim
{

    public class Stimulus: StimulusBase
    {
        private double tangent; //valid only if mode==Ramp
        private int iStart, iEnd;
        private double[] values = null;
        private bool initialized = false;

        public static int nMax; //in time increments
        public Cell TargetCell;
        
        [JsonIgnore]
        public double MinValue { get { return values?.Min() ?? 0; } }
        [JsonIgnore]
        public double MaxValue { get { return values?.Max() ?? 0; } }

        public double this[int index]
        {
            get
            {
                if (values?.Length > index)
                    return values[index];
                return 0;
            }
        }

        public Stimulus() { }
        public Stimulus(StimulusSettings settings, Cell cell, TimeLine tl)
        {
            Settings = settings;
            TargetCell = cell;
            TimeLine_ms = new(tl);
        }

        public override StimulusBase CreateCopy() 
        {
            return new Stimulus(Settings, TargetCell, TimeLine_ms);
        }

        public override string ToString()
        {
            return TargetCell.ID + ": " +Settings.ToString();
        }

        public string GetTooltip()
        {
            return $"{ToString()}\r\nTimeLine: {TimeLine_ms}";
        }
        private void Initialize()
        {
            if (initialized)
                return;

            iEnd = (int)(TimeLine_ms.End / TargetCell.Model.RunParam.DeltaT);
            if (iEnd < 0)
                iEnd = nMax;
            iStart = (int)(TimeLine_ms.Start / TargetCell.Model.RunParam.DeltaT);
            if (Settings.Mode == StimulusMode.Ramp)
                if (iEnd > iStart)
                    tangent = (Settings.Value2 - Settings.Value1) / (iEnd - iStart);
                else tangent = 0;

            if (values != null && values.Length < iEnd)
            {
                double[] copyArr = new double[values.Length];
                values.CopyTo(copyArr, 0);
                values = new double[iEnd];
                copyArr.CopyTo(values, 0);
            }
            else values ??= new double[iEnd];
            initialized = true;
        }

        internal double GenerateStimulus(int tIndex, Random rand)
        {
            double t_ms = TargetCell.Model.RunParam.GetTimeOfIndex(tIndex);
            if (!TimeLine_ms.IsActive(t_ms))
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
            switch (Settings.Mode)
            {
                case StimulusMode.Step:
                    double noise = Settings.Value2 > 0 ? rand.Gauss(1, Settings.Value2) : 1;
                    value = Settings.Value1 * noise;
                    break;
                case StimulusMode.Ramp:
                    double ramp = (tIndex - iStart) * tangent;
                    value = Settings.Value1 + ramp;
                    break;
                case StimulusMode.Gaussian:
                    value = rand.Gauss(Settings.Value1, Settings.Value2, Settings.Value1 - 3 * Settings.Value2, Settings.Value1 + 3 * Settings.Value2); // µ ± 3SD range
                    break;
                case StimulusMode.Sinusoidal:
                    double sinValue = Math.Sin(2 * Math.PI * Settings.Value2 * (t_ms - TimeLine_ms.StartOf(t_ms)));
                    value = Settings.Value1 * sinValue;
                    break;
                case StimulusMode.Pulse:
                    value = 0;
                    if (Settings.Value2 > 0)//Value2 is the number of pulses
                    {
                        double timeRange = TimeLine_ms.End > 0 ? TimeLine_ms.End - TimeLine_ms.Start : 1000;
                        double period = timeRange / Settings.Value2;
                        double t = t_ms - TimeLine_ms.Start; //check only start and finish times of the full timeline - TimeSpan_ms.IsActive(t_ms) at the beginning of the function takes care of the filtering
                        while (t > period)
                            t -= period;
                        if (t <= period / 2)
                            value = Settings.Value1;
                    }
                    break;
            }

            values[tIndex] = value;
            return value;
        }
        public double[] GenerateStimulus(int stimStart, int stimDuration, Random rand)
        {
            double[] stim = new double[stimStart + stimDuration];
            foreach (int i in Enumerable.Range(stimStart, stimDuration))
                stim[i] = GenerateStimulus(i, rand);
            return stim;
        }
        
        public double[] GetValues(int nMax)
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
