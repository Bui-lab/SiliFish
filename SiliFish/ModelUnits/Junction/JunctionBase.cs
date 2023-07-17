using OfficeOpenXml.Sparkline;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
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
        protected double[] inputCurrent; //Current array 

        [JsonIgnore, Browsable(false)]
        protected virtual int nMax => throw new NotImplementedException();

        [JsonIgnore, Browsable(false)]
        public double[] InputCurrent 
        {
            get
            {
                if (inputCurrent == null)
                    PopulateCurrentArray();
                return inputCurrent;
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
            Weight = jnc.Weight;
        }
        //if current tracking is Off, the current array can be populated after the simulation for an individual junction
        private void PopulateCurrentArray()
        {
            int nmax = nMax;
            if (inputCurrent != null)
                return;//Already populated
            InitForSimulation(nmax, true);
            if (!Active) return;
            foreach (var index in Enumerable.Range(1, nmax - 1))
            {
                NextStep(index);
            }
        }
        public virtual void InitForSimulation(int nmax, bool trackCurrent)
        {
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
