using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Junction
{
    public class InterPoolBase : ModelUnitBase, IDataExporterImporter
    {
        public DistanceMode DistanceMode { get; set; } = DistanceMode.Euclidean;
        public double? FixedDuration_ms { get; set; } = null;// in ms
        public double? Delay_ms { get; set; } = null;//in ms

        /// <summary>
        /// used for import/exports
        /// </summary>
        public string Source { get; set; }
        public string Target { get; set; }

        public virtual string SourcePool { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public virtual string TargetPool { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames { get; } = 
            ListBuilder.Build<string>(
                "Source", "Target",
                "Connection Type", "Core Type",
               Enumerable.Range(1, JunctionCore.CoreParamMaxCount).SelectMany(i => new[] { $"Param{i}", $"Value{i}" }),
                "Distance Mode", 
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

        public InterPoolBase()
        { }
        public InterPoolBase(InterPoolBase jnc)
        {
            Target = jnc.Target;
            DistanceMode = jnc.DistanceMode;
            FixedDuration_ms = jnc.FixedDuration_ms;
            Delay_ms = jnc.Delay_ms;
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

        public virtual void NextStep(int tIndex)
        {
            throw new NotImplementedException();
        }


    }
}
