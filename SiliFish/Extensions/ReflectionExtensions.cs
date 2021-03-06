using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Extensions
{
    public static class ReflectionExtensions
    {
        public static void SetPropertyValue<T>(this object obj, string propertyName, T value)
        {
            PropertyInfo pi = obj.GetType().GetProperty(propertyName);
            pi?.SetValue(obj, value);
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
