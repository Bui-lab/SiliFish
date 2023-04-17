using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    [JsonDerivedType(typeof(CellCoreUnit), typeDiscriminator: "cellcore")]
    [JsonDerivedType(typeof(HodgkinHuxley), typeDiscriminator: "hodgkinhuxley")]
    [JsonDerivedType(typeof(HodgkinHuxleyClassic), typeDiscriminator: "hodgkinhuxleyclassic")]
    [JsonDerivedType(typeof(Izhikevich_5P), typeDiscriminator: "izhikevich5p")]
    [JsonDerivedType(typeof(Izhikevich_9P), typeDiscriminator: "izhikevich9p")]
    [JsonDerivedType(typeof(Leaky_Integrator), typeDiscriminator: "leakyintegrator")]
    [JsonDerivedType(typeof(QuadraticIntegrateAndFire), typeDiscriminator: "qif")]
    [JsonDerivedType(typeof(LeakyIntegrateAndFire), typeDiscriminator: "leakyintegratefire")]
    public class CellCoreUnit
    {
        #region Static members and functions
        private static readonly Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => typeof(CellCoreUnit).IsAssignableFrom(type))
        .ToDictionary(type => type.Name, type => type);
        public static List<string> GetCoreTypes()
        {
            return typeMap.Keys.Where(k => k != nameof(CellCoreUnit) && k!=nameof(ContractibleCellCoreUnit)).ToList();
        }

        public static CellCoreUnit CreateCore(string coreType, Dictionary<string, double> parameters, double? dt_run = null, double? dt_euler = null)
        {
            CellCoreUnit core = (CellCoreUnit)Activator.CreateInstance(typeMap[coreType], parameters ?? new Dictionary<string, double>());
            if (dt_euler != null)
                core.deltaTEuler = (double)dt_euler;
            if (dt_run != null)
                core.deltaT = (double)dt_run;
            return core;
        }

        public static CellCoreUnit CreateCore(CellCoreUnit copyFrom)
        {
            CellCoreUnit core = (CellCoreUnit)Activator.CreateInstance(typeMap[copyFrom.CoreType], copyFrom.Parameters ?? new Dictionary<string, double>());
            core.deltaTEuler = copyFrom.deltaTEuler;
            core.deltaT = copyFrom.deltaT;
            return core;
        }
        /// <summary>
        /// static function to return dictionary of objects to keep the distributions
        /// </summary>
        /// <param name="coreType"></param>
        /// <returns></returns>
        public static Dictionary<string, Distribution> GetParameters(string coreType)
        {
            CellCoreUnit core = CreateCore(coreType, null);
            return core?.GetParameters().ToDictionary(kvp => kvp.Key, kvp => new Constant_NoDistribution(kvp.Value) as Distribution);
        }

        #endregion

        protected double deltaT, deltaTEuler;
        protected double V = -70;//Keeps the current value of V 

        [Description("The resting membrane potential.")]
        public double Vr { get; set; } = -70;

        [Description("The peak membrane potential of single spike.")]
        public double Vmax { get; set; } = 30;

        [JsonIgnore, Browsable(false)]
        public virtual double Vthreshold { get; set; }
        [JsonIgnore, Browsable(false)]
        public Dictionary<string, double> Parameters
        {
            get { return GetParameters(); }
            set { SetParameters(value); }
        }

        [JsonIgnore, Browsable(false)]
        public string CoreType => GetType().Name;

        [JsonIgnore, Browsable(false)]
        public static string CSVExportColumnNames => $"Core Type, Core Values";

        [JsonIgnore, Browsable(false)]
        private static int CSVExportColumCount => CSVExportColumnNames.Split(',').Length;
        [JsonIgnore, Browsable(false)]
        public virtual string CSVExportValues
        {
            get
            {
                return $"{CoreType}," + string.Join(',', GetParameters().Select(kvp => $"{kvp.Key}:{kvp.Value}").ToList());
            }
            set
            {
                string[] values = value.Split(',');
                Dictionary<string, double> parameters=new();
                foreach (string v in values[1..])
                {
                    string[] parameter = v.Split(':');
                    if (parameter.Length == 2 && double.TryParse(parameter[1], out double d))
                        parameters.Add(parameter[0], d);
                }
                SetParameters(parameters);
            }
        }

        public virtual void Initialize(double deltaT, double deltaTEuler)
        {
            this.deltaT = deltaT;
            this.deltaTEuler = deltaTEuler;
            Initialize();
        }
        protected virtual void Initialize()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual Dictionary<string, double> GetParameters()
        {
            Dictionary<string, double> paramDict = new();

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                if (prop.GetCustomAttribute<BrowsableAttribute>()?.Equals(BrowsableAttribute.No) ?? false)
                    continue;
                if (prop.PropertyType.Name != typeof(double).Name)
                    continue;
                paramDict.Add(prop.Name, (double)prop.GetValue(this));
            }
            return paramDict;
        }

        public virtual void SetParameters(Dictionary<string, double> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            foreach (string key in paramExternal.Keys)
            {
                SetParameter(key, paramExternal[key]);
            }
        }
        public virtual void SetParameter(string name, double value)
        {
            name = name.Replace(GetType().Name + ".", "");//TODO temporary replacement - version 2.2.4
            this.SetPropertyValue(name, value);
        }
        public virtual bool CheckValues(ref List<string> errors)
        {
            errors ??= new();
            return errors.Count == 0;
        }
        public static bool CheckValues(ref List<string> errors, string coreType, Dictionary<string, double> param)
        {
            errors ??= new();
            CellCoreUnit core = CreateCore(coreType, param);
            return core.CheckValues(ref errors);
        }


        public virtual void BackwardCompatibility(Dictionary<string, double> paramExternal)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        protected virtual bool DoesSpike(double[] I, int warmup)
        {
            bool spike = false;
            int tmax = I.Length;
            Initialize();
            for (int t = 0; t < tmax; t++)
            {
                GetNextVal(I[t], ref spike);
                if (t > warmup && spike) //ignore first little bit
                    break;
            }
            return spike;
        }
        public virtual double CalculateRheoBase(double maxRheobase, double sensitivity, double infinity_ms, double dt, double warmup_ms = 100, double cooldown_ms = 100)
        {
            Initialize();
            int infinity = (int)(infinity_ms / dt);
            int warmup = (int)(warmup_ms / dt);
            int cooldown = (int)(cooldown_ms / dt);
            int tmax = infinity + warmup + cooldown;
            double[] I = new double[tmax];
            double curI = maxRheobase;
            double minI = 0;
            double rheobase = -1;

            while (curI >= minI + sensitivity)
            {
                foreach (int i in Enumerable.Range(warmup, infinity))
                    I[i] = curI;
                if (DoesSpike(I, warmup))
                {
                    rheobase = curI;
                    curI = (curI + minI) / 2;
                }
                else //increment
                {
                    minI = curI;
                    curI = (curI + (rheobase > 0 ? rheobase : maxRheobase)) / 2;
                }
            }
            if (curI < minI + sensitivity)//test the minimum current as well - some neurons fire without any stimulus
            {
                foreach (int i in Enumerable.Range(warmup, infinity))
                    I[i] = minI;
                if (DoesSpike(I, warmup))
                    rheobase = minI;
            }
            return rheobase;
        }

        public virtual double GetNextVal(double I, ref bool spike)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual DynamicsStats SolveODE(double[] I)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual DynamicsStats DynamicsTest(double[] I)
        {
            return SolveODE(I);
        }

        public virtual DynamicsStats DynamicsTest(double IValue, int infinity, double dt, int? warmup = 100, bool includePostStimulus = false)
        {
            infinity = (int)(infinity / dt);
            warmup ??= 0;
            warmup = (int)(warmup / dt);
            int tmax = (includePostStimulus ? 2 : 1) * infinity + (int)warmup + 10;
            double[] I = new double[tmax];
            foreach (int i in Enumerable.Range((int)warmup, infinity))
            {
                I[i] = IValue;
            }

            if (includePostStimulus)
                foreach (int i in Enumerable.Range((int)warmup + infinity, infinity))
                {
                    I[i] = 0;
                }

            return DynamicsTest(I);
        }

        public virtual double[] RheobaseSensitivityAnalysis(string param, double[] values,
                    double dt, double maxRheobase = 100, double sensitivity = 0.001, int infinity = 300)
        {
            double origValue = Parameters[param];
            double[] rheos = new double[values.Length];
            int counter = 0;
            foreach (double value in values)
            {
                SetParameter(param, value);
                rheos[counter++] = CalculateRheoBase(maxRheobase, sensitivity, infinity, dt);
            }
            SetParameter(param, origValue);
            return rheos;
        }

        public DynamicsStats[] FiringAnalysis(string param, double[] values, double[] I)
        {
            double origValue = Parameters[param];
            DynamicsStats[] stats = new DynamicsStats[values.Length];
            int counter = 0;
            foreach (double value in values)
            {
                SetParameter(param, value);
                stats[counter++] = DynamicsTest(I);
            }
            SetParameter(param, origValue);
            return stats;
        }
        public virtual (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;

        }

    }

}
