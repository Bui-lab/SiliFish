using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Architecture
{
    [JsonDerivedType(typeof(ModelBase), typeDiscriminator: "modelbase")]
    [JsonDerivedType(typeof(RunningModel), typeDiscriminator: "model")]
    [JsonDerivedType(typeof(ModelTemplate), typeDiscriminator: "modeltemplate")]
    public class ModelBase
    {
        public Random rand = new(0);

        public string Version { get; set; }
        public string ClassType => GetType().Name;
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }

        public ModelDimensions ModelDimensions { get; set; } = new();
        public ModelSettings Settings { get; set; } = new();

        [JsonPropertyOrder(2)]
        public KinemParam KinemParam { get; set; } = new();

        [JsonPropertyOrder(2)]
        public Dictionary<string, object> Parameters { get; set; }

        public ModelBase()
        {
        }

        public virtual bool CheckValues(ref List<string> errors) 
        {
            errors ??= new();
            if (!ModelDimensions.CheckConsistency(out string error))
            {
                errors.Add(error);
                return false;
            }
            return true;
        }

        public virtual void BackwardCompatibility_Stimulus()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void BackwardCompatibility()
        {
            if (string.Compare(Version, "2.2.4") < 0)//frequency to stimulus is added on 2.2.4
            {
                BackwardCompatibility_Stimulus();
            }

            if (Parameters == null || !Parameters.Any())
                return;
            if (Parameters.TryGetValue("General.Name", out object value))
            {
                ModelName = value.ToString();
                Parameters.Remove("General.Name");
            }
            if (Parameters.TryGetValue("General.Description", out value))
            {
                ModelDescription = value.ToString();
                Parameters.Remove("General.Description");
            }
            Parameters = ModelDimensions.BackwardCompatibility(Parameters);
            Settings.BackwardCompatibility(Parameters);
            KinemParam.BackwardCompatibility(Parameters);
        }
        public virtual void LinkObjects() { }

        public virtual List<CellPoolTemplate> GetCellPools()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual List<JunctionBase> GetGapProjections()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual List<JunctionBase> GetIncomingProjections()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual List<JunctionBase> GetOutgoingProjections()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual List<JunctionBase> GetChemicalProjections()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual List<StimulusBase> GetStimuli()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void AddStimulus(StimulusBase stim)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void RemoveStimulus(StimulusBase stim)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void UpdateStimulus(StimulusBase stim, StimulusBase stim2)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual bool AddCellPool(CellPoolTemplate cellPool)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual bool RemoveCellPool(CellPoolTemplate cellPool)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void SortCellPools()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual bool AddJunction(JunctionBase jnc)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual bool RemoveJunction(JunctionBase jnc)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void SortJunctions()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void SortJunctionsByType()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void SortJunctionsBySource()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void SortJunctionsByTarget()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual void CopyConnectionsOfCellPool(CellPoolTemplate poolSource, CellPoolTemplate poolTarget)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

    }
}
