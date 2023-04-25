﻿using SiliFish.DataTypes;
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
    public class JunctionBase : ModelUnitBase
    {
        public DistanceMode DistanceMode { get; set; } = DistanceMode.Euclidean;
        public double? FixedDuration_ms { get; set; } = null;// in ms
        public double Delay_ms { get; set; } = 0;//in ms
        public double Weight { get; set; }

        /// <summary>
        /// used for JSON
        /// </summary>
        public string Target{ get; set; }


        [JsonIgnore, Browsable(false)]
        public static string CSVExportColumnNames => $"Connection Type,Source,Target," +
                $"Distance Mode," +
                $"{SynapseParameters.CSVExportColumnNames}," +
                $"Weight,Fixed Duration (ms),Delay (ms)," +
                $"Active," +
                $"{TimeLine.CSVExportColumnNames}";

        [JsonIgnore, Browsable(false)]
        protected static int CSVExportColumCount => CSVExportColumnNames.Split(',').Length;

        [JsonIgnore, Browsable(false)]
        public virtual string CSVExportValues
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
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
        public virtual void UnlinkObjects()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }


    }
}
