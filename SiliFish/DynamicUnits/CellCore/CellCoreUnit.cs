﻿using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class CellCoreUnit
    {
        private static Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => typeof(CellCoreUnit).IsAssignableFrom(type))
        .ToDictionary(type => type.Name, type => type);

        //the resting membrane potential
        public double Vr = -70;
        // vmax is the peak membrane potential of single action potentials
        public double Vmax = 30;

        [JsonIgnore]
        protected double V = -70;//Keeps the current value of V 

        private Dictionary<string, double> parametersObsolete; //Used for json only
        public Dictionary<string, double> Parameters
        {
            get { return GetParameters(); }
            set { SetParameters(value); }
        }

        private string coreType;
        public string CoreType 
        {
            get
            {
                if (string.IsNullOrEmpty(coreType))
                    coreType = this.GetType().Name;
                return coreType;
            }
            set { coreType = value; } //for json
        }

        [JsonIgnore]
        public virtual string VThresholdParamName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [JsonIgnore]
        public virtual string VReversalParamName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static List<string> GetCoreTypes()
        {
            return typeMap.Keys.Where(k => k != nameof(CellCoreUnit)).ToList();
        }
        public static CellCoreUnit GetOfDerivedType(string json)
        {
            CellCoreUnit core = JsonSerializer.Deserialize<CellCoreUnit>(json);
            if (core != null)
                return CreateCore(core.CoreType, core.Parameters);
            return core;
        }
        public static CellCoreUnit CreateCore(string coreType, Dictionary<string, double> parameters)
        {
            return (CellCoreUnit)Activator.CreateInstance(typeMap[coreType], parameters ?? new Dictionary<string, double>());
        }

        /// <summary>
        /// static function to return dictionary of objects to keep the distributions
        /// </summary>
        /// <param name="coreType"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetParameters(string coreType)
        {
            CellCoreUnit core = CreateCore(coreType, null);
            return core?.GetParameters().ToDictionary(kvp => kvp.Key, kvp => kvp.Value as object);
        }


        public virtual Dictionary<string, double> GetParameters()
        {
            return parametersObsolete;
        }

        public virtual void SetParameters(Dictionary<string, double> paramExternal)
        {
            parametersObsolete = paramExternal;
        }

        protected virtual void Initialize()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public virtual DynamicsStats SolveODE(double[] I)
        {
            throw new NotImplementedException();
        }

        public virtual DynamicsStats DynamicsTest(double[] I)
        {
            return SolveODE(I);
        }

        public virtual DynamicsStats DynamicsTest(double IValue, int infinity, double dt, int warmup = 100)
        {
            infinity = (int)(infinity / dt);
            warmup = (int)(warmup / dt);
            int tmax = infinity + warmup + 10;
            double[] I = new double[tmax];
            foreach (int i in Enumerable.Range(warmup, infinity))
                I[i] = IValue;
            return DynamicsTest(I);
        }

        public virtual double[] RheobaseSensitivityAnalysis(string param, double[] values,
                    double dt, double maxRheobase = 100, double sensitivity = 0.001, int infinity = 300)
        {
            Dictionary<string, double> parameters = GetParameters();
            double origValue = parameters[param];
            double[] rheos = new double[values.Length];
            int counter = 0;
            foreach (double value in values)
            {
                parameters[param] = value;
                SetParameters(parameters);
                rheos[counter++] = CalculateRheoBase(maxRheobase, sensitivity, infinity, dt);
            }
            parameters[param] = origValue;
            SetParameters(parameters);
            return rheos;
        }

        public DynamicsStats[] FiringAnalysis(string param, double[] values, double[] I)
        {
            Dictionary<string, double> parameters = GetParameters();
            double origValue = parameters[param];
            DynamicsStats[] stats = new DynamicsStats[values.Length];
            int counter = 0;
            foreach (double value in values)
            {
                parameters[param] = value;
                SetParameters(parameters);
                stats[counter++] = DynamicsTest(I);
            }
            parameters[param] = origValue;
            SetParameters(parameters);
            return stats;
        }
        public virtual (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            throw new NotImplementedException();
        }

    }

}