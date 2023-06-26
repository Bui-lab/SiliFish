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
        public DistanceMode DistanceMode { get; set; } = DistanceMode.Euclidean;
        public double? FixedDuration_ms { get; set; } = null;// in ms
        public double Delay_ms { get; set; } = 0;//in ms
        public double Weight { get; set; }

        public double[] InputCurrent; //Current array 

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
                InputCurrent = new double[nmax];
                InputCurrent[0] = 0;
            }
            else
                InputCurrent = null;
        }
        public virtual void NextStep(int tIndex)
        {
            throw new NotImplementedException();
        }
        //if current tracking is Off, the current array can be populated after the simulation for an individual junction
        public void PopulateCurrentArray(int nmax)
        {
            if (InputCurrent != null)
                return;//Already populated
            InitForSimulation(nmax, true);
            foreach (var index in Enumerable.Range(1, nmax - 1))
            {
                NextStep(index);
            }
        }

    }
}
