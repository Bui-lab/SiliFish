using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Services;
using SiliFish.Services.Dynamics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    [JsonDerivedType(typeof(CellCore), typeDiscriminator: "cellcore")]
    [JsonDerivedType(typeof(HodgkinHuxley), typeDiscriminator: "hodgkinhuxley")]
    [JsonDerivedType(typeof(HodgkinHuxleyClassic), typeDiscriminator: "hodgkinhuxleyclassic")]
    [JsonDerivedType(typeof(Izhikevich_5P), typeDiscriminator: "izhikevich5p")]
    [JsonDerivedType(typeof(Izhikevich_9P), typeDiscriminator: "izhikevich9p")]
    [JsonDerivedType(typeof(Leaky_Integrator), typeDiscriminator: "leakyintegrator")]
    [JsonDerivedType(typeof(QuadraticIntegrateAndFire), typeDiscriminator: "qif")]
    [JsonDerivedType(typeof(ExponentialIntegrateAndFire), typeDiscriminator: "expif")]
    [JsonDerivedType(typeof(LeakyIntegrateAndFire), typeDiscriminator: "leakyintegratefire")]
    public class CellCore : BaseCore
    {
        #region Static members and functions
        private static readonly Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => typeof(CellCore).IsAssignableFrom(type))
        .ToDictionary(type => type.Name, type => type);

        public static int CoreParamMaxCount = 10;

        public static List<string> GetCoreTypes()
        {
            return typeMap.Keys.Where(k => k != nameof(CellCore) && k != nameof(ContractibleCellCore)).ToList();
        }

        public static CellCore CreateCore(string coreType, Dictionary<string, double> parameters, double dt_run)
        {
            if (string.IsNullOrEmpty(coreType)) return null;
            CellCore core = (CellCore)Activator.CreateInstance(typeMap[coreType], parameters ?? []);
            core.deltaT = dt_run;
            return core;
        }

        public static CellCore CreateCore(CellCore copyFrom)
        {
            CellCore core = (CellCore)Activator.CreateInstance(typeMap[copyFrom.CoreType], copyFrom.Parameters ?? []);
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
            CellCore core = CreateCore(coreType, null, 0);
            return core?.GetParameters().ToDictionary(kvp => kvp.Key, kvp => new Constant_NoDistribution(kvp.Value) as Distribution);
        }

        #endregion

        protected double deltaT;
        protected double V = -70;//Keeps the current value of V 
        private double? rheobase;

        [Browsable(false)]
        public double Rheobase
        {
            get
            {
                rheobase ??= CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: GlobalSettings.RheobaseInfinity, dt: 0.1);
                return (double)rheobase;
            }

            set => rheobase = value;
        }

        [JsonIgnore, Browsable(false)]
        public virtual double VMomentary { get => V; }
        [JsonIgnore, Browsable(false)]
        public virtual double VSpikeThreshold { get => Vthreshold; }//to determine whether there is a spike or not

        [Description("The resting membrane potential.")]
        public double Vr { get; set; } = -70;


        [Description("The peak membrane potential of single spike.")]
        public double Vmax { get; set; } = 30;

        [JsonIgnore, Browsable(false)]
        public virtual double Vthreshold { get; set; }

        [JsonIgnore, Browsable(false)]
        public virtual double Vreset { get => Vr; set => Vr = value; }

        public virtual void Initialize(double deltaT, ref int uniqueID)
        {
            UniqueId = uniqueID++;
            this.deltaT = deltaT;
            Initialize();
        }
        protected virtual void Initialize()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public override void SetParameters(Dictionary<string, double> paramExternal)
        {
            base.SetParameters(paramExternal);
            rheobase = null;//initialize rheobase
        }


        public virtual void BackwardCompatibility(Dictionary<string, double> paramExternal)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
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
        public virtual double CalculateRheoBase(double maxRheobase, double sensitivity, double infinity_ms, double dt, double warmup_ms = 10, double cooldown_ms = 10)
        {
            Initialize();
            int infinity = (int)(infinity_ms / dt);
            int warmup = (int)(warmup_ms / dt);
            int cooldown = (int)(cooldown_ms / dt);
            int tmax = infinity + warmup + cooldown;
            double[] I = new double[tmax];
            double curI = maxRheobase;
            double minI = 0;
            rheobase = -1;

            while (curI >= minI + sensitivity)
            {
                foreach (int i in Enumerable.Range(warmup, infinity))
                    I[i] = curI;
                if (DoesSpike(I, warmup - 1))
                {
                    rheobase = curI;
                    curI = (curI + minI) / 2;
                }
                else //increment
                {
                    minI = curI;
                    curI = (double)((curI + (rheobase > 0 ? rheobase : maxRheobase)) / 2);
                }
            }
            if (curI < minI + sensitivity)//test the minimum current as well - some neurons fire without any stimulus
            {
                foreach (int i in Enumerable.Range(warmup, infinity))
                    I[i] = minI;
                if (DoesSpike(I, warmup - 1))
                    rheobase = minI;
            }
            return (double)rheobase;
        }

        public virtual double GetNextVal(double I, ref bool spike)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual DynamicsStats CreateDynamicsStats(double[] I)
        {
            DynamicsStats dyn = new(null, I, deltaT);
            return dyn;
        }
        public virtual void UpdateAdditionalDynamicStats(DynamicsStats dyn, int tIndex)
        {
        }
        public virtual DynamicsStats SolveODE(double[] I)
        {
            Initialize();
            int iMax = I.Length;
            DynamicsStats dyn = CreateDynamicsStats(I);

            bool spike = false;
            for (int tIndex = 0; tIndex < iMax; tIndex++)
            {
                GetNextVal(I[tIndex], ref spike);
                dyn.VList[tIndex] = V;
                if (spike)
                    dyn.SpikeList.Add(tIndex);
                UpdateAdditionalDynamicStats(dyn, tIndex);
            }
            dyn.SpikesCreated = true;
            dyn.CalculateTaus(this, deltaT);
            dyn.DefineSpikingPattern();

            return dyn;
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
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;

        }

        public static bool CheckValues(ref List<string> errors, ref List<string> warnings, string coreType, Dictionary<string, double> param)
        {
            errors ??= [];
            warnings ??= [];
            BaseCore core = CreateCore(coreType, param, 0);
            return core.CheckValues(ref errors, ref warnings);
        }

    }

}
