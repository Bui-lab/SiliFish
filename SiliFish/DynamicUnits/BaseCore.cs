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
        public int UniqueId { get; set; } = -1;//Unique ID to be saved to the database
        [JsonIgnore, Browsable(false)]
        public string CoreType => GetType().Name;

        [JsonIgnore, Browsable(false)]
        public Dictionary<string, double> Parameters
        {
            get { return GetParameters(); }
            set { SetParameters(value); }
        }
        [JsonIgnore, Browsable(false)]
        public Dictionary<string, string> ParameterDescriptions
        {
            get { return GetParameterDescriptions(); }
        }
        public virtual Dictionary<string, string> GetParameterDescriptions()
        {
            Dictionary<string, string> descDict = [];

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                if (prop.GetCustomAttribute<BrowsableAttribute>()?.Equals(BrowsableAttribute.No) ?? false)
                    continue;
                string desc = prop.GetCustomAttribute<DescriptionAttribute>()?.Description;
                if (!string.IsNullOrEmpty(desc))
                    descDict[prop.Name] = desc;
            }
            return descDict;
        }
        public virtual Dictionary<string, double> GetParameters()
        {
            Dictionary<string, double> paramDict = [];

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
            errors ??= [];
            return errors.Count == 0;
        }



    }
}
