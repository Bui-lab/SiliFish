using SiliFish.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.Extensions
{
    public static class ReflectionExtensions
    {
        public static void SetPropertyValue<T>(this object obj, string propertyName, T value)
        {
            PropertyInfo pi = obj.GetType().GetProperty(propertyName);
            if (pi == null) return;
            if (pi.SetMethod == null) return;
            if (pi.PropertyType.IsEnum)
                pi.SetValue(obj, Enum.Parse(pi.PropertyType, value?.ToString()));
            else
                pi.SetValue(obj, Convert.ChangeType(value, pi.PropertyType));
        }

        public static (T value, bool exists) GetPropertyValue<T>(this object obj, string propertyName, T defValue = default)
        {
            try
            {
                PropertyInfo pi = obj.GetType().GetProperty(propertyName);
                return pi != null ? ((T)Convert.ChangeType(pi?.GetValue(obj), typeof(T)), true) : (defValue, false);
            }
            catch { return (defValue, true); }
        }

        public static List<Difference> DiffersFrom(this object obj, object other)
        {
            if (other.GetType() != obj.GetType())
                return [new Difference("Incompatible classes.", null, null)];
            List<Difference> differences = [];
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                var v1 = prop.GetValue(obj, null);
                var v2 = prop.GetValue(other, null);
                if (v1?.ToString() != v2?.ToString())
                    differences.Add(new Difference(prop.Name, v1, v2));
            }
            if (differences.Count != 0)
                return differences;
            return null;
        }


        //https://stackoverflow.com/questions/39201271/get-all-properties-marked-with-jsonignore-attribute
        public static string GetProperties(this object obj, string seperator)

        {
            List<string> properties = [];
            foreach (PropertyInfo prop in obj.GetType().GetProperties()
                .Where(property => !property.GetCustomAttributes(false).OfType<JsonIgnoreAttribute>().Any()))
            {
                properties.Add($"{prop.Name}: {prop.GetValue(obj)}");
            }
            return string.Join(seperator, [.. properties]);
        }
    }
}
