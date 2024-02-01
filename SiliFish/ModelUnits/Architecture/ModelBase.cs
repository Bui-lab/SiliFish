using SiliFish.DataTypes;
using SiliFish.Extensions;
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
        private ModelSettings settings = new();

        public string Version { get; set; }
        public string ClassType => GetType().Name;
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }

        public ModelDimensions ModelDimensions { get; set; } = new();
        public ModelSettings Settings 
        { 
            get => settings;
            set
            {
                settings = value;
                Distribution.Random = new Random(settings.Seed);
            }
        }
        [JsonPropertyOrder(2)]
        public KinemParam KinemParam { get; set; } = new();
        [JsonPropertyOrder(2)]
        public DynamicsParam DynamicsParam { get; set; } = new();

        [JsonPropertyOrder(2)]
        public Dictionary<string, object> Parameters { get; set; } = [];

        public ModelBase()
        {
        }

        public virtual List<string> DiffersFrom(ModelBase other)
        {
            List<string> differences = [];
            if (Version != other.Version)
                differences.Add($"Version: {Version} vs {other.Version}");
            if (ModelName != other.ModelName)
                differences.Add($"Name: {ModelName} vs {other.ModelName}");
            if (ModelDescription != other.ModelDescription)
                differences.Add($"Description: {ModelDescription} vs {other.ModelDescription}");
            if (Settings.DiffersFrom(other.settings) != null)
                differences.AddRange(Settings.DiffersFrom(other.settings));
            List<string> diffs = ModelDimensions.DiffersFrom(other.ModelDimensions);
            if (diffs != null) differences.AddRange(diffs);

            diffs = KinemParam.DiffersFrom(other.KinemParam);
            if (diffs != null) differences.AddRange(diffs);

            diffs = DynamicsParam.DiffersFrom(other.DynamicsParam);
            if (diffs != null) differences.AddRange(diffs);

            return differences;
        }
        public virtual bool CheckValues(ref List<string> errors) 
        {
            errors ??= [];
            if (ModelDimensions.NumberOfSomites <= 0)
            {
                errors.Add("Number of somites has to be greater than 0.");
                return false;
            }
            if (!ModelDimensions.CheckConsistency(out string error))
            {
                errors.Add(error);
                return false;
            }
            return true;
        }

        public virtual void BackwardCompatibility()
        {
            if (Parameters == null || Parameters.Count == 0)
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
        public virtual void BackwardCompatibilityAfterLinkObjects()
        {
        }
        public virtual void LinkObjects() { }

        public virtual List<CellPoolTemplate> GetCellPools()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual List<InterPoolBase> GetGapProjections()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual List<InterPoolBase> GetChemicalProjections()
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

        public virtual bool AddJunction(InterPoolBase jnc)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual bool RemoveJunction(InterPoolBase jnc)
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
