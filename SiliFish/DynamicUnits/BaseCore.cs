using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits
{
    abstract public class BaseCore
    {





        [JsonIgnore, Browsable(false)]
        public Dictionary<string, double> Parameters
        {
            get { return GetParameters(); }
            set { SetParameters(value); }
        }
        public virtual Dictionary<string, double> GetParameters()
        {
            Dictionary<string, double> paramDict = new();

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                if (prop.GetCustomAttribute<BrowsableAttribute>()?.Equals(BrowsableAttribute.No) ?? false)
                    continue;
                if (prop.PropertyType.Name != typeof(double).Name)
                    continue;
                paramDict.Add(prop.Name, (double)prop.GetValue(this));
            }
            return paramDict;
        }

        public virtual void SetParameters(Dictionary<string, double> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            foreach (string key in paramExternal.Keys)
            {
                SetParameter(key, paramExternal[key]);
            }
        }
        public virtual void SetParameter(string name, double value)
        {
            this.SetPropertyValue(name, value);
        }

        public virtual bool CheckValues(ref List<string> errors)
        {
            errors ??= new();
            return errors.Count == 0;
        }



    }
}
