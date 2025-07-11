using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Stim;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SiliFish.Services.Dynamics
{
    public class DynamicsTest
    {
        public static (double[] TimeArray, double[] I) GenerateStimulus(StimulusSettings stimulusSettings,
            double statEnd_ms, double dt, Random random)
        {
            int endTime = (int)Math.Round(statEnd_ms / dt);
            double[] TimeArray = new double[endTime + 1];
            foreach (int i in Enumerable.Range(0, endTime + 1))
                TimeArray[i] = i * dt;
            RunParam runParam = new()
            {
                MaxTime = (int)statEnd_ms,
                DeltaT = dt
            };
            Stimulus stim = new()
            {
                Settings = stimulusSettings,
                TimeLine_ms = stimulusSettings.TimeLine_ms
            };
            stim.InitForSimulation(runParam, random);
            double[] I = stim.GetValues(endTime);
            return (TimeArray, I);
        }
        public static List<Chart> DeltaTAnalysis(DynamicsParam dynamicsParam, string coreType, Dictionary<string, double> parameters, StimulusSettings stimulusSettings,
            double statEnd_ms, double[] dtValues, Random random)
        {
            try
            {
                List<Chart> charts = [];
                string param = "Delta t (ms)";
                foreach (double dt in dtValues)
                {
                    (double[] TimeArray, double[] I) = GenerateStimulus(stimulusSettings, statEnd_ms, dt, random);
                    CellCore core = CellCore.CreateCore(coreType, parameters, dt);
                    DynamicsStats stat = core.DynamicsTest(dynamicsParam, I);
                    charts.Add(new Chart
                    {
                        Title = dt.ToString("0.###"),
                        Colors = [Color.Purple],
                        xData = TimeArray,
                        yData = stat.VList,
                        xLabel = param,
                        yLabel = "V (mV)"
                    });
                }
                return charts;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }

        public static List<Chart> FiringAnalysis(DynamicsParam dynamicsParam, string coreType,
            Dictionary<string, double> parameters, string param,
            NumberRangeDefinition rangeDefinition,
            StimulusSettings stimulusSettings,
            double statEnd_ms, double dt, Random random)
        {
            List<Chart> charts = [];
            CellCore core = CellCore.CreateCore(coreType, parameters, dt);
            double[] paramValues = Util.GenerateValues(parameters[param], rangeDefinition);
            (double[] TimeArray, double[] I) = GenerateStimulus(stimulusSettings, statEnd_ms, dt, random);
            DynamicsStats[] stats = core.FiringAnalysis(dynamicsParam, param, paramValues, I);
            for (int iter = 0; iter < paramValues.Length; iter++)
            {
                DynamicsStats stat = stats[iter];
                charts.Add(new Chart
                {
                    Title = paramValues[iter].ToString("0.###"),
                    Colors = [Color.Purple],
                    xData = TimeArray,
                    yData = stat.VList,
                    xLabel = param,
                    yLabel = "V (mV)"
                });
            }
            return charts;
        }

        public static List<Chart> RheobaseAnalysis(string coreType,
            Dictionary<string, double> parameters,
            double maxRheobase, int maxDuration,
            NumberRangeDefinition rangeDefinition,
            double dt)
        {
            List<Chart> charts = [];
            CellCore core = CellCore.CreateCore(coreType, parameters, dt);

            foreach (string param in parameters.Keys)//change one parameter at a time
            {
                double[] values = Util.GenerateValues(parameters[param], rangeDefinition);

                double[] rheos = core.RheobaseSensitivityAnalysis(param, values, dt, maxRheobase, sensitivity: Math.Pow(0.1, 3), infinity: maxDuration);
                charts.Add(new Chart
                {
                    Title = param,
                    Colors = [Color.Purple],
                    xData = values,
                    yData = rheos,
                    xLabel = param,
                    yLabel = "Rheobase",
                    drawPoints = true,
                    logScale = rangeDefinition.LogScale
                });
            }
            return charts;
        }

    }
}
