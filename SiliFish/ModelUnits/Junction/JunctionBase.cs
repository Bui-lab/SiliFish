using OfficeOpenXml.Sparkline;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [JsonIgnore]

        public JunctionCore Core { get; set; }

        protected double[] inputCurrent; //Current array 

        [JsonIgnore, Browsable(false)]
        protected virtual int nMax => throw new NotImplementedException();

        [JsonIgnore, Browsable(false)]
        public double[] InputCurrent 
        {
            get
            {
                if (inputCurrent == null)
                    PopulateCurrentArray(0.1);//TODO added a constant value temporarily
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
            int nmax = nMax;
            if (inputCurrent != null)
                return;//Already populated
            InitForSimulation(nmax, true, dt);
            if (!Active) return;
            foreach (var index in Enumerable.Range(1, nmax - 1))
            {
                NextStep(index);
            }
        }
        public virtual void InitForSimulation(int nmax, bool trackCurrent, double dt)
        {
            InitForSimulation(dt);
            if (trackCurrent)
            {
                inputCurrent = new double[nmax];
                inputCurrent[0] = 0;
            }
            else
                inputCurrent = null;
        }

    }
}
