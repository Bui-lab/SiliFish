using SiliFish.DataTypes;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
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
        

        public virtual int CompareTo(ModelUnitBase other)
        {
            throw new NotImplementedException();
        }

        public virtual bool CheckValues(ref List<string> errors)
        {
            errors ??= new();
            return errors.Count == 0;
        }
    }
}
