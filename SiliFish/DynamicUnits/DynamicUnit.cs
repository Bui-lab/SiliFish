﻿using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits
{
    public class DynamicUnitTemplate
    {
        private Dictionary<string, object> parameters; 
        public virtual Dictionary<string, object> GetParameters()
        {
            return parameters;
        }
        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            parameters = paramExternal; ;
        }
        /*            a = paramExternal.Read("Izhikevich_5P.a", a);
            b = paramExternal.Read("Izhikevich_5P.b", b);
            c = paramExternal.Read("Izhikevich_5P.c", c);
            d = paramExternal.Read("Izhikevich_5P.d", d);
            Vmax = paramExternal.Read("Izhikevich_5P.V_max", Vmax);
            V = Vr = paramExternal.Read("Izhikevich_5P.V_r", Vr);
*/
    }
    public class DynamicUnit
    {
        //the resting membrane potential
        public double Vr = -70;
        // vmax is the peak membrane potential of single action potentials
        public double Vmax = 30;

        [JsonIgnore]
        protected double V = -70;//Keeps the current value of V 

        private Dictionary<string, double> parametersObsolete; //Used for json only
        public CoreType CoreType { get; set; }
        public Dictionary<string, double> Parameters
        {
            get { return GetParametersDouble(); }
            set { SetParametersDouble(value); }
        }
        public static DynamicUnit GetOfDerivedType(string json)
        {
            DynamicUnit core = JsonSerializer.Deserialize<DynamicUnit>(json);
            if (core != null)
                return CreateCore(core.CoreType, core.Parameters);
            return core;
        }
        public static DynamicUnit CreateCore(CoreType coreType, Dictionary<string, double> parameters)
        {
            switch (coreType)
            {
                case CoreType.Izhikevich_5P:
                    return new Izhikevich_5P(parameters);
                case CoreType.Izhikevich_9P:
                    return new Izhikevich_9P(parameters);
                case CoreType.Leaky_Integrator:
                    return new Leaky_Integrator(parameters);
            }
            return null;
        }

        public virtual Dictionary<string, double> GetParametersDouble()
        {
            return parametersObsolete;
        }

        public virtual void SetParametersDouble(Dictionary<string, double> paramExternal)
        {
            parametersObsolete = paramExternal;
        }

        protected virtual void Initialize()
        {
            throw new NotImplementedException();
        }

        public virtual bool DoesSpike(double[] I, int warmup)
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

        public virtual (double[], double[]) RheobaseSensitivityAnalysis(string param, bool logScale, double minMultiplier, double maxMultiplier, int numOfPoints,
                    double dt, double maxRheobase = 100, double sensitivity = 0.001, int infinity = 300)
        {
            if (maxMultiplier < minMultiplier)
                (minMultiplier, maxMultiplier) = (maxMultiplier, minMultiplier);
            Dictionary<string, double> parameters = GetParametersDouble();
            double origValue = parameters[param];
            double[] values = new double[numOfPoints];
            if (!logScale)
            {
                double incMultiplier = (maxMultiplier - minMultiplier) / (numOfPoints - 1);
                foreach (int i in Enumerable.Range(0, numOfPoints))
                    values[i] = (incMultiplier * i + minMultiplier) * origValue;
            }
            else
            {
                double logMinMultiplier = Math.Log10(minMultiplier);
                double logMaxMultiplier = Math.Log10(maxMultiplier);
                double incMultiplier = (logMaxMultiplier - logMinMultiplier) / (numOfPoints - 1);
                foreach (int i in Enumerable.Range(0, numOfPoints))
                    values[i] = Math.Pow(10, incMultiplier * i + logMinMultiplier) * origValue;
            }
            double[] rheos = new double[numOfPoints];
            int counter = 0;
            foreach (double value in values)
            {
                parameters[param] = value;
                SetParametersDouble(parameters);
                rheos[counter++] = CalculateRheoBase(maxRheobase, sensitivity, infinity, dt);
            }
            parameters[param] = origValue;
            SetParametersDouble(parameters);
            return (values, rheos);
        }


        public virtual (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            throw new NotImplementedException();
        }

    }

}
