using System;
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

    }
}
