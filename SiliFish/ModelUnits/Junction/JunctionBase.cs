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
    public class JunctionBase : ModelUnitBase, IDataExporterImporter
    {
        protected double[] inputCurrent; //Current array 

        [JsonIgnore, Browsable(false)] 
        protected virtual int nMax => throw new NotImplementedException();
        public DistanceMode DistanceMode { get; set; } = DistanceMode.Euclidean;
        public double? FixedDuration_ms { get; set; } = null;// in ms
        public double Delay_ms { get; set; } = 0;//in ms
        public double Weight { get; set; }


        /// <summary>
        /// used for import/exports
        /// </summary>
        public string Source { get; set; }
        public string Target { get; set; }


        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames { get; } = 
            ListBuilder.Build<string>("Connection Type", "Source", "Target", 
                "Distance Mode", 
                SynapseParameters.ColumnNames, 
                "Weight", "Fixed Duration (ms)", "Delay (ms)",
                "Active", 
                TimeLine.ColumnNames);

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

        public virtual List<string> ExportValues()
        {
            throw new NotImplementedException();
        }
        public virtual void ImportValues(List<string> values)
        {
            throw new NotImplementedException();
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
        public virtual void LinkObjects()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void LinkObjects(RunningModel runningModel)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void UnlinkObjects()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
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
        public virtual void NextStep(int tIndex)
        {
            throw new NotImplementedException();
        }


    }
}
