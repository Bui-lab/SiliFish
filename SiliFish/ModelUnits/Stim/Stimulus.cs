using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Stim
{

    public class Stimulus : StimulusBase
    {
        private double tangent; //valid only if mode==Ramp
        private int iStart, iEnd;
        private double[] values = null;
        private bool initialized = false;

        public static int nMax; //in time increments
        private Cell targetCell;

        [JsonIgnore]
        public Cell TargetCell
        {
            get => targetCell;
            set
            {
                targetCell = value;
                RunParam = targetCell.Model.RunParam;
            }
        }

        [JsonIgnore]
        public RunParam RunParam { get; set; }
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
            return TargetCell.ID + ": " + Settings.ToString() + (Active ? "" : " (inactive)");
        }

        public string GetTooltip()
        {
            return $"{ToString()}\r\nTimeLine: {TimeLine_ms}";
        }
        private void Initialize()
        {
            if (initialized)
                return;

            iEnd = (int)(TimeLine_ms.End / RunParam.DeltaT);
            if (iEnd < 0)
                iEnd = nMax;
            iStart = (int)(TimeLine_ms.Start / RunParam.DeltaT);
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

        public double[] GenerateStimulus(int stimStart, int stimDuration, Random rand)
        {
            List<(int start, int end)> periods = new();
            nMax = stimStart + stimDuration;
            values = new double[nMax];
            periods.Add(((int start, int end))(stimStart, RunParam.GetTimeOfIndex(nMax)));
            GenerateStimulus(periods, rand);
            return values;
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

        private void GenerateStepStimulus(List<(int start, int end)> periods, Random rand)//TODO implement functions for other stimulus types and generate values at the beginning
        {
            foreach (var (start, end) in periods)
            {
                int iStart = RunParam.iIndex(start);
                int iEnd = RunParam.iIndex(end);
                for (int ind = iStart; ind < iEnd; ind++)
                {
                    double noise = Settings.Value2 > 0 ? rand.Gauss(1, Settings.Value2) : 1;
                    values[ind] = Settings.Value1 * noise;//TODO is noise multipler or a deviation?
                }
            }
        }

        private void GenerateRampStimulus(List<(int start, int end)> periods, Random rand)
        {
            int firstStart = periods[0].start;
            int lastEnd = periods[periods.Count() - 1].end;

            if (lastEnd > firstStart)
                tangent = (Settings.Value2 - Settings.Value1) / ((lastEnd - firstStart) / RunParam.DeltaT);
            else tangent = 0;

            foreach (var (start, end) in periods)
            {
                int iStart = RunParam.iIndex(start);
                int iEnd = RunParam.iIndex(end);
                for (int ind = iStart; ind < iEnd; ind++)
                {
                    double ramp = (ind - iStart) * tangent;
                    values[ind] = Settings.Value1 + ramp;
                }
            }
        }

        private void GenerateGaussianStimulus(List<(int start, int end)> periods, Random rand)
        {
            foreach (var (start, end) in periods)
            {
                int iStart = RunParam.iIndex(start);
                int iEnd = RunParam.iIndex(end);
                for (int ind = iStart; ind < iEnd; ind++)
                {
                    values[ind] = rand.Gauss(Settings.Value1, Settings.Value2, Settings.Value1 - 3 * Settings.Value2, Settings.Value1 + 3 * Settings.Value2); // µ ± 3SD range
                }
            }
        }
        private void GenerateSinusoidalStimulus(List<(int start, int end)> periods, Random rand)//TODO 
        {
            if (Settings.Value2 <= 0) return;
            //for sinusoidal, the number of full cycles occur for each period
            foreach (var (start, end) in periods)
            {
                int iStart = RunParam.iIndex(start);
                int iEnd = RunParam.iIndex(end);
                //the period of the sin wave
                int sinCycle = (int)((iEnd - iStart) / Settings.Value2);
                for (int ind = iStart; ind < iEnd; ind++)
                {
                    Math.DivRem((ind-iStart), sinCycle, out int rem);
                    double sinValue = Math.Sin(2 * Math.PI * rem/sinCycle);
                    values[ind] = Settings.Value1 * sinValue;
                }
            }
        }
        private void GeneratePulseStimulus(List<(int start, int end)> periods, Random rand)
        {
            int firstStart = periods[0].start;
            int lastEnd = periods[periods.Count() - 1].end;
            foreach (var (start, end) in periods)
            {
                int iStart = RunParam.iIndex(start);
                int iEnd = RunParam.iIndex(end);
                for (int ind = iStart; ind < iEnd; ind++)
                {
                    if (Settings.Value2 > 0)//Value2 is the number of pulses
                    {
                        double t_ms = RunParam.GetTimeOfIndex(ind);
                        double timeRange = lastEnd > 0 ? lastEnd - firstStart : 1000;
                        double period = timeRange / Settings.Value2;
                        double t = t_ms - firstStart; //check only start and finish times of the full timeline - TimeSpan_ms.IsActive(t_ms) at the beginning of the function takes care of the filtering
                        while (t > period)
                            t -= period;
                        if (t <= period / 2)
                            values[ind] = Settings.Value1;
                    }
                }
            }
        }
        private void GenerateStimulus(List<(int start, int end)> periods, Random rand)
        {
            switch (Settings.Mode)
            {
                case StimulusMode.Step:
                    GenerateStepStimulus(periods, rand);
                    break;
                case StimulusMode.Ramp:
                    GenerateRampStimulus(periods, rand);
                    break;
                case StimulusMode.Gaussian:
                    GenerateGaussianStimulus(periods, rand);
                    break;
                case StimulusMode.Sinusoidal:
                    GenerateSinusoidalStimulus(periods, rand);
                    break;
                case StimulusMode.Pulse:
                    GeneratePulseStimulus(periods, rand);
                    break;
            }
        }
        public void InitForSimulation(int nmax, Random rand)
        {
            values = new double[nmax];
            List<(int start, int end)> periods = TimeLine_ms.GetTimeLine();
            if (!periods.Any())
            {
                int nMax = values.Length;
                periods.Add(((int start, int end))(0, RunParam.GetTimeOfIndex(nMax)));
            }
            for (int i = 0; i < periods.Count; i++)
            {
                if (periods[i].end < 0)
                    periods[i] = (periods[i].start, (int)RunParam.GetTimeOfIndex(nMax));
            }
            GenerateStimulus(periods, rand);
        }
    }

}
