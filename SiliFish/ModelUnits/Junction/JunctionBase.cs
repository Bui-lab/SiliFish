﻿using OfficeOpenXml.Sparkline;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiliFish.ModelUnits.Junction
{
    public class JunctionBase : InterPoolBase
    {
        public JunctionCore Core { get; set; }

        public virtual int iDuration => throw new NotImplementedException();

        protected double[] inputCurrent; //Current array 

        [JsonIgnore, Browsable(false)]
        protected virtual int nMax => throw new NotImplementedException();

        [JsonIgnore, Browsable(false)]
        protected virtual double dt => throw new NotImplementedException();

        [JsonIgnore, Browsable(false)]
        protected virtual int iMax => throw new NotImplementedException();


        [JsonIgnore, Browsable(false)]
        public double[] InputCurrent 
        {
            get
            {
                if (inputCurrent == null)
                    PopulateCurrentArray(dt);
                return inputCurrent;
            }
        }
        [JsonIgnore, Browsable(false)]
        protected List<string> csvExportCoreValues
        {
            get
            {
                List<string> paramValues = Core.Parameters
                    .Take(JunctionCore.CoreParamMaxCount)
                    .OrderBy(kv => kv.Key)
                    .SelectMany(kv => new[] { kv.Key, kv.Value.ToString() }).ToList();
                for (int i = Core.Parameters.Count; i < JunctionCore.CoreParamMaxCount; i++)
                {
                    paramValues.Add(string.Empty);
                    paramValues.Add(string.Empty);
                }
                return paramValues;
            }
        }
        public JunctionBase()
        { }
        public JunctionBase(JunctionBase jnc)
        {
            Target = jnc.Target;
            DistanceMode = jnc.DistanceMode;
            FixedDuration_ms = jnc.FixedDuration_ms;
            Delay_ms = jnc.Delay_ms;
        }
        public void SetFixedDuration(double dur)
        {
            FixedDuration_ms = dur;
        }
        public void SetDelay(double? delay)
        {
            Delay_ms = delay;
        }
        public void SetTimeLine(TimeLine span)
        {
            TimeLine_ms = span;
        }

        //if current tracking is Off, the current array can be populated after the simulation for an individual junction
        private void PopulateCurrentArray(double dt)
        {
            if (inputCurrent != null)
                return;//Already populated
            int uniqueID = 0;
            RunParam runParam = new()
            {
                DeltaT = dt,
                TrackJunctionCurrent = true,
                MaxTime = nMax,
                SkipDuration = 0//runparam is temporarily used for the above parameters, skip duration is not relevant
            };
            InitForSimulation(runParam, ref uniqueID);
            if (!Active) return;
            foreach (var index in Enumerable.Range(1, nMax - 1))
            {
                NextStep(index);
            }
        }

        public virtual void FinalizeSimulation(RunParam runParam, DBLink dbLink)
        {

        }
        public virtual void MemoryAllocation(RunParam runParam, DBLink dbLink)
        {

        }

        public virtual void MemoryFlush()
        {
            inputCurrent = null;
        }

        public virtual void InitForSimulation(RunParam runParam, ref int _)
        {
            InitForSimulation(runParam.DeltaT);
            if (runParam.TrackJunctionCurrent)
            {
                inputCurrent = new double[runParam.iMax];
                inputCurrent[0] = 0;
            }
            else
                inputCurrent = null;
        }
    }
}
