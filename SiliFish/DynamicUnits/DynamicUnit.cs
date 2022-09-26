﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits
{
    public class DynamicUnit
    {
        public virtual double CalculateRheoBase(double maxRheobase, double sensitivity, int infinity, double dt, int warmup = 100)
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
            throw new NotImplementedException();
        }

        public virtual (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) GetSuggestedMinMaxValues()
        {
            throw new NotImplementedException();
        }

    }
}