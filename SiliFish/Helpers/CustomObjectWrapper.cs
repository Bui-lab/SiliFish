using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SiliFish.Helpers
{
    //https://stackoverflow.com/questions/56370834/is-there-a-way-to-display-static-properties-in-a-propertygrid
    public class CustomObjectWrapper : CustomTypeDescriptor
    {
        public object WrappedObject { get; private set; }
        private IEnumerable<PropertyDescriptor> staticProperties;
        private bool staticOnly = false;
        public CustomObjectWrapper(object o, bool staticOnly)
            : base(TypeDescriptor.GetProvider(o).GetTypeDescriptor(o))
        {
            WrappedObject = o;
            this.staticOnly = staticOnly;
        }
        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            var instanceProperties = base.GetProperties(attributes)
                .Cast<PropertyDescriptor>();
            staticProperties = WrappedObject.GetType()
                .GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Select(p => new StaticPropertyDescriptor(p, WrappedObject.GetType()));
            if (staticOnly)
                return new PropertyDescriptorCollection(staticProperties.ToArray());
            return new PropertyDescriptorCollection(
                instanceProperties.Union(staticProperties).ToArray());
        }
    }
}
