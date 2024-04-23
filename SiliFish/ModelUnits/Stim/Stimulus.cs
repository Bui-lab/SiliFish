using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Stim
{

    public class Stimulus : StimulusBase, IDataExporterImporter
    {
        private double tangent; //valid only if mode==Ramp
        private double[] values = null;

        /// <summary>
        /// used for import/exports
        /// </summary>
        private string targetPool, targetCellID;

        [JsonIgnore]
        public Cell TargetCell { get; set; }

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
        [JsonIgnore]
        public override string Tooltip => $"{ToString()}\r\nTimeLine: {TimeLine_ms}";

        public override string ID => $"{TargetCell.ID} {Settings} {TimeLine_ms}";

        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames { get; } =
            ListBuilder.Build<string>("TargetPool", "TargetCell", StimulusSettings.ColumnNames, TimeLine.ColumnNames);

        public List<string> ExportValues()
        {
            return ListBuilder.Build<string>(TargetCell.CellPool.ID, TargetCell.ID, Settings.ExportValues(), TimeLine_ms.ExportValues());
        }
        public void ImportValues(List<string> values)
        {
            if (values.Count != ColumnNames.Count) return;
            targetPool= values[0];
            targetCellID = values[1];
            int lastSettingsCol = StimulusSettings.ColumnNames.Count + 2;
            Settings.ImportValues(values.Take(new Range(2, lastSettingsCol)).ToList());
            TimeLine_ms.ImportValues(values.Take(new Range(lastSettingsCol, values.Count)).ToList());
        }
        public void LinkObjects(RunningModel model)
        {
            TargetCell ??= model.GetCell(targetCellID);
            TargetCell.AddStimulus(this);
        }
        public Stimulus() { }
        public Stimulus(StimulusSettings settings, Cell cell, TimeLine tl)
        {
            Settings = settings.Clone();
            TargetCell = cell;
            TimeLine_ms = new(tl);
        }

        public void GenerateFromCSVRow(RunningModel Model, string row)
        {
            string[] values = row.Split(',');
            if (values.Length != ColumnNames.Count) return;
            TargetCell = Model.GetCell(values[1]);
            Settings.ImportValues(values.Take(new Range(2, (StimulusSettings.ColumnNames.Count + 1))).ToList());
            TimeLine_ms.ImportValues(values.Take(new Range(StimulusSettings.ColumnNames.Count + 2, values.Length)).ToList());
            TargetCell?.AddStimulus(this);
        }
        public override ModelUnitBase CreateCopy()
        {
            return new Stimulus(Settings, TargetCell, TimeLine_ms);
        }

        public override string ToString()
        {
            string timeline = TimeLine_ms != null && !TimeLine_ms.IsBlank() ? TimeLine_ms.ToString() : "";
            string active = Active ? "" : " (inactive)";
            return $"{TargetCell.ID}: {Settings} {timeline} {active}".Replace("  ", " ");
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

        #region Generate different modes of stimuli
        //TODO this is exactly the same as Gaussian - think
        private void GenerateStepStimulus(List<(double start, double end)> timeRanges, Random rand)
        {
            foreach (var (start, end) in timeRanges)
            {
                int iStart = RunParam.iIndex(start);
                int iEnd = RunParam.iIndex(end);
                for (int ind = iStart; ind < iEnd; ind++)
                {                    
                    values[ind] = Settings.Value2 > 0 ? rand.Gauss(Settings.Value1, Settings.Value2) : Settings.Value1;
                }
            }
        }

        private void GenerateRampStimulus(List<(double start, double end)> timeRanges)
        {
            double firstStart = timeRanges[0].start;
            double lastEnd = timeRanges[timeRanges.Count() - 1].end;

            if (lastEnd > firstStart)
                tangent = (Settings.Value2 - Settings.Value1) / ((lastEnd - firstStart) / RunParam.DeltaT);
            else tangent = 0;

            foreach (var (start, end) in timeRanges)
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

        private void GenerateGaussianStimulus(List<(double start, double end)> timeRanges, Random rand)
        {
            foreach (var (start, end) in timeRanges)
            {
                int iStart = RunParam.iIndex(start);
                int iEnd = RunParam.iIndex(end);
                for (int ind = iStart; ind < iEnd; ind++)
                {
                    values[ind] = rand.Gauss(Settings.Value1, Settings.Value2, Settings.Value1 - 3 * Settings.Value2, Settings.Value1 + 3 * Settings.Value2); // µ ± 3SD range
                }
            }
        }
        private void GenerateSinusoidalStimulus(List<(double start, double end)> timeRanges)
        {
            if (Settings.Frequency is null ||  Settings.Frequency <= 0) return;
            double sinCycle = 1000 / (double)Settings.Frequency;//the period of the sin wave (period in ms)
            double dt = RunParam.DeltaT;
            //for sinusoidal, the number of full cycles occur for each period
            foreach (var (start, end) in timeRanges)
            {
                int iStart = RunParam.iIndex(start);
                int iEnd = RunParam.iIndex(end);
                for (int ind = iStart; ind < iEnd; ind++)
                {
                    double sinValue = Math.Sin(2 * Math.PI * dt * (ind - iStart) / sinCycle);
                    values[ind] = Settings.Value1 * sinValue;
                }
            }
        }
        private void GeneratePulseStimulus(List<(double start, double end)> timeRanges)
        {
            if (Settings.Frequency is null || Settings.Frequency <= 0) return;
            if (Settings.Value2 <= GlobalSettings.Epsilon) return;
            double period = 1000 / (double)Settings.Frequency;//the period of each on+off (period in ms)
            foreach (var (tStart, tEnd) in timeRanges)
            {
                int ind = RunParam.iIndex(tStart);
                int iEnd = RunParam.iIndex(tEnd);
                double tPeriodStart = tStart;
                while (ind < iEnd-1)
                {
                    int iPeriodStart = RunParam.iIndex(tPeriodStart);
                    double tPeriodEnd = Math.Min(tPeriodStart + period, tEnd);
                    int iPeriodEnd = RunParam.iIndex(tPeriodEnd);
                    double tPulseEnd = Math.Min(Math.Min(tEnd, tPeriodStart + Settings.Value2), tPeriodEnd);
                    int iPulseEnd = RunParam.iIndex(tPulseEnd);
                    foreach (int i in Enumerable.Range(iPeriodStart, iPulseEnd - iPeriodStart))
                    {
                        values[i] = Settings.Value1;
                        ind = i;
                    }
                    foreach (int i in Enumerable.Range(iPulseEnd, iPeriodEnd - iPulseEnd))
                    {
                        values[i] = 0;
                        ind = i;
                    }
                    tPeriodStart = tPeriodEnd;
                }

            }
        }
        private void GenerateStimulus(List<(double start, double end)> timeRanges, Random rand)
        {
            switch (Settings.Mode)
            {
                case StimulusMode.Step:
                    GenerateStepStimulus(timeRanges, rand);
                    break;
                case StimulusMode.Ramp:
                    GenerateRampStimulus(timeRanges);
                    break;
                case StimulusMode.Gaussian:
                    GenerateGaussianStimulus(timeRanges, rand);
                    break;
                case StimulusMode.Sinusoidal:
                    GenerateSinusoidalStimulus(timeRanges);
                    break;
                case StimulusMode.Pulse:
                    GeneratePulseStimulus(timeRanges);
                    break;
            }
        }
        #endregion
        public void InitForSimulation(RunParam runParam, Random rand)
        {
            InitForSimulation(runParam.DeltaT);
            RunParam = runParam;
            values = new double[RunParam.iMax];
            List<(double start, double end)> timeRanges = new(TimeLine_ms.GetTimeLine());
            if (timeRanges.Count == 0)
            {
                timeRanges.Add((0, RunParam.MaxTime));
            }
            for (int i = 0; i < timeRanges.Count; i++)
            {
                if (timeRanges[i].end < 0)
                    timeRanges[i] = (timeRanges[i].start, RunParam.MaxTime);
            }
            GenerateStimulus(timeRanges, rand);
        }
    }
}
