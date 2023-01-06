using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SiliFish.Helpers
{
    //https://stackoverflow.com/questions/56370834/is-there-a-way-to-display-static-properties-in-a-propertygrid
    public class StaticPropertyDescriptor : PropertyDescriptor
    {
        PropertyInfo p;
        Type ownerType;
        public StaticPropertyDescriptor(PropertyInfo pi, Type ownerType)
            : base(pi.Name,
                  pi.GetCustomAttributes().Cast<Attribute>().ToArray())
        {
            p = pi;
            this.ownerType = ownerType;
        }
        public override bool CanResetValue(object c) => false;
        public override object GetValue(object c) => p.GetValue(null);
        public override void ResetValue(object c) { }
        public override void SetValue(object c, object v) => p.SetValue(null, v);
        public override bool ShouldSerializeValue(object c) => false;
        public override Type ComponentType { get { return ownerType; } }
        public override bool IsReadOnly { get { return !p.CanWrite; } }
        public override Type PropertyType { get { return p.PropertyType; } }
    }
}
