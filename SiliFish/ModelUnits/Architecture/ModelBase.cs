﻿using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
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

        public virtual void BackwardCompatibility()
        {
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

        public virtual List<CellPoolTemplate> GetCellPools() { throw new NotImplementedException(); }
        public virtual List<JunctionBase> GetProjections() { throw new NotImplementedException(); }
        public virtual List<StimulusBase> GetStimuli() { throw new NotImplementedException(); }
        public virtual void AddStimulus(StimulusBase stim) { throw new NotImplementedException(); }
        public virtual void RemoveStimulus(StimulusBase stim) { throw new NotImplementedException(); }
        public virtual void UpdateStimulus(StimulusBase stim, StimulusBase stim2) { throw new NotImplementedException(); }

        public virtual bool AddCellPool(CellPoolTemplate cellPool) { throw new NotImplementedException(); }
        public virtual bool RemoveCellPool(CellPoolTemplate cellPool) { throw new NotImplementedException(); }
        public virtual void SortCellPools() { throw new NotImplementedException(); }

        public virtual bool AddJunction(JunctionBase jnc) { throw new NotImplementedException(); }
        public virtual bool RemoveJunction(JunctionBase jnc) { throw new NotImplementedException(); }
        public virtual void SortJunctions() { throw new NotImplementedException(); }
        public virtual void SortJunctionsByType() { throw new NotImplementedException(); }
        public virtual void SortJunctionsBySource() { throw new NotImplementedException(); }
        public virtual void SortJunctionsByTarget() { throw new NotImplementedException(); }

        public virtual void CopyConnectionsOfCellPool(CellPoolTemplate poolSource, CellPoolTemplate poolTarget)
        {
            throw new NotImplementedException();
        }

    }
}
