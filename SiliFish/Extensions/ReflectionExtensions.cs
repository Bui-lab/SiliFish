using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static List<string> DiffersFrom(this object obj, object other)
        {
            if (other.GetType() != obj.GetType()) 
                return new() { "Incompatible classes." };
            List<string> differences = new();
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                var v1 = prop.GetValue(obj, null);
                var v2 = prop.GetValue(other, null);
                if (v1?.ToString() != v2?.ToString())
                    differences.Add($"{prop.Name}: {v1} vs {v2}");
            }
            if (differences.Any())
                return differences;
            return null;
        }
    }
}
