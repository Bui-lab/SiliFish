using SiliFish.DataTypes;
using System;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits
{
    public abstract class ModelUnitBase : IComparable<ModelUnitBase>
    {
        [JsonIgnore]
        public virtual string Distinguisher { get; }
        [JsonIgnore]
        public virtual string Tooltip { get { return Distinguisher; } }
        private bool _Active = true;
        public virtual bool Active { get => _Active; set => _Active = value; }
        public TimeLine TimeLine_ms { get; set; } = new TimeLine();

        public virtual int CompareTo(ModelUnitBase other)
        {
            throw new NotImplementedException();
        }
    }
}
