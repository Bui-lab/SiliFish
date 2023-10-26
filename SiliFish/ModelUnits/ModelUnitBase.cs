using SiliFish.DataTypes;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using SiliFish.Services.Plotting;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits
{
    public abstract class ModelUnitBase : IComparable<ModelUnitBase>
    {
        [JsonIgnore]
        public virtual string ID { get; }
        [JsonIgnore]
        public virtual string Tooltip { get { return ID; } }
        private bool _Active = true;
        public virtual bool Active { get => _Active; set => _Active = value; }
        public TimeLine TimeLine_ms { get; set; } = new TimeLine();
        public TimeLine TimeLine_ind { get; set; } = null;

        public virtual List<string> DiffersFrom(ModelUnitBase other)
        {
            return null;
        }
        public virtual int CompareTo(ModelUnitBase other)
        {
            return ID.CompareTo(other.ID);
        }

        public virtual bool CheckValues(ref List<string> errors)
        {
            errors ??= new();
            return errors.Count == 0;
        }

        public virtual ModelUnitBase CreateCopy()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual bool HasStimulus()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual void InitForSimulation(double dt)
        {
            TimeLine_ind = new TimeLine(TimeLine_ms, 1/dt);
        }
        public virtual bool IsActive(int timepoint)
        {
            return TimeLine_ind?.IsActive(timepoint) ?? true;
        }

    }
}
