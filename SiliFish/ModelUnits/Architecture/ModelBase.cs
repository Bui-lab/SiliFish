﻿using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Architecture
{
    [JsonDerivedType(typeof(ModelBase), typeDiscriminator: "modelbase")]
    [JsonDerivedType(typeof(RunningModel), typeDiscriminator: "model")]
    [JsonDerivedType(typeof(ModelTemplate), typeDiscriminator: "modeltemplate")]
    public class ModelBase
    {
        private ModelSettings settings = new();
        private SimulationSettings simulationSettings = new();

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
        public SimulationSettings SimulationSettings
        {
            get => simulationSettings;
            set
            {
                simulationSettings = value;
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
        public (int startSomite, int endSomite) GetMidBodySomiteRange()
        {
            int startSomite = (int)Math.Floor((double)ModelDimensions.NumberOfSomites / 4);
            int endSomite = (int)Math.Ceiling((double)ModelDimensions.NumberOfSomites * 3 / 4);
            if (!string.IsNullOrEmpty(GlobalSettings.Plotting_BodyMidRange))
            {
                (double d1, double d2) = GlobalSettings.Plotting_BodyMidRange.ParseRange(startSomite, endSomite);
                startSomite = (int)d1;
                endSomite = (int)d2;
                if (endSomite == 0)
                    endSomite = ModelDimensions.NumberOfSomites;
            }
            return (startSomite, endSomite);
        }
        public virtual List<Difference> DiffersFrom(ModelBase other)
        {
            List<Difference> differences = [];
            if (Version != other.Version)
                differences.Add(new Difference("Version", Version, other.Version));
            if (ModelName != other.ModelName)
                differences.Add(new Difference("Name", ModelName, other.ModelName));
            if (ModelDescription != other.ModelDescription)
                differences.Add(new Difference("Description", ModelDescription, other.ModelDescription));
            if (Settings.DiffersFrom(other.Settings) != null)
                differences.AddRange(Settings.DiffersFrom(other.Settings));
            if (SimulationSettings.DiffersFrom(other.SimulationSettings) != null)
                differences.AddRange(SimulationSettings.DiffersFrom(other.SimulationSettings));
            List<Difference> diffs = ModelDimensions.DiffersFrom(other.ModelDimensions);
            if (diffs != null) differences.AddRange(diffs);

            diffs = KinemParam.DiffersFrom(other.KinemParam);
            if (diffs != null) differences.AddRange(diffs);

            diffs = DynamicsParam.DiffersFrom(other.DynamicsParam);
            if (diffs != null) differences.AddRange(diffs);

            return differences;
        }
        public virtual bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            errors ??= [];
            warnings ??= [];
            int preCount = errors.Count + warnings.Count;
            if (ModelDimensions.NumberOfSomites <= 0)
                errors.Add("Number of somites has to be greater than 0.");
            if (!ModelDimensions.CheckConsistency(out string error))
                errors.Add(error);
            return errors.Count + warnings.Count == preCount;
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
        public virtual List<InterPoolBase> GetGapJunctions()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual List<InterPoolBase> GetChemicalJunctions()
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
        public virtual bool UpdateCellPool(CellPoolTemplate cellPool)
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

    }
}
