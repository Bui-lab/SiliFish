using SiliFish.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace SiliFish.Extensions
{
    public static class DictionaryExtensions
    {
        public static int ReadIntegerAndRemoveKey(this Dictionary<string, object> dictionary, string key, int defaultValue = default)
        {
            int ret = ReadInteger(dictionary, key, defaultValue);
            try
            {
                dictionary.Remove(key);
            }
            catch {  }
            return ret;
        }
        public static double ReadDoubleAndRemoveKey(this Dictionary<string, object> dictionary, string key, double defaultValue = default)
        {
            double ret = ReadDouble(dictionary, key, defaultValue);
            try
            {
                dictionary.Remove(key);
            }
            catch { }
            return ret;
        }
        public static int ReadInteger(this Dictionary<string, object> dictionary, string key, int defaultValue = default)
        {
            try
            {
                return Convert.ToInt32(Read(dictionary, key, defaultValue));
            }
            catch { return defaultValue; }
        }

        public static double ReadDouble(this Dictionary<string, object> dictionary, string key, double defaultValue = default)
        {
            try
            {
                return Convert.ToDouble(Read(dictionary, key, defaultValue));
            }
            catch { return defaultValue; }
        }

        public static double ReadDouble(this Dictionary<string, Distribution> dictionary, string key, double defaultValue = default)
        {
            try
            {
                return Convert.ToDouble(Read(dictionary, key, defaultValue));
            }
            catch { return defaultValue; }
        }

        public static T Read<T>(this Dictionary<string, object> dictionary, string key, T defaultValue = default)
        {
            try
            {
                if (dictionary.TryGetValue(key, out var val))
                {
                    if (val is Distribution valdt)
                    {
                        val = valdt.GenerateNNumbers(1, valdt.Range, ordered: false)[0];
                    }
                    else if (val is JsonElement element)
                    {
                        var json = element.GetRawText();
                        if (element.ValueKind == JsonValueKind.String)
                        {
                            var str = JsonSerializer.Deserialize<string>(json);
                            return (T)Convert.ChangeType(str, typeof(T));
                        }
                        //Distribution dist = Distribution.GetOfDerivedType(json);
                        return JsonSerializer.Deserialize<T>(json);
                    }
                    return (T)Convert.ChangeType(val, typeof(T));
                }
                return defaultValue;
            }
            catch { return defaultValue; }
        }
        public static T Read<T>(this Dictionary<string, Distribution> dictionary, string key, T defaultValue = default)
        {
            try
            {
                if (dictionary.TryGetValue(key, out var val))
                {
                        var v = val.GenerateNNumbers(1, val.Range, ordered: false)[0];
                    return (T)Convert.ChangeType(v, typeof(T));
                }
                return defaultValue;
            }
            catch { return defaultValue; }
        }
        public static object GetNestedValue(this Dictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            foreach (var obj in dictionary.Values.Where(x => x is Dictionary<string, object>))
            {
                var ret = ((Dictionary<string, object>)obj).GetNestedValue(key);
                if (ret != null)
                    return ret;
            }
            return null;
        }

        public static void AddObject<T>(this Dictionary<string, T> dictionary, string key, T value, bool skipIfExists = false)
        {
            if (skipIfExists && dictionary.ContainsKey(key))
                return;
            while (dictionary.ContainsKey(key))
                key += "'";
            dictionary.Add(key, value);
        }
        public static void AddOrUpdateObject<T>(this Dictionary<string, T> dictionary, string key, T value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        public static void AddObject<T>(this Dictionary<int, T> dictionary, int key, T value, bool skipIfExists = false)
        {
            if (skipIfExists && dictionary.ContainsKey(key))
                return;
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }
        public static bool SameAs(this Dictionary<string, object> dictionary, Dictionary<string, object> dic2)
        {
            if (dictionary?.Count != dic2?.Count) return false;
            foreach (var key in dictionary.Keys)
            {
                if (!dic2.ContainsKey(key))
                    return false;
                string s1 = dictionary[key]?.ToString() ?? "";
                string s2 = dic2[key]?.ToString() ?? "";
                if (s1 != s2)
                    return false;
            }
            return true;
        }

        public static bool SameAs(this Dictionary<string, Distribution> dictionary, Dictionary<string, Distribution> dic2, out List<Difference> diff)
        {
            diff = [];
            if (dictionary?.Count != dic2?.Count)
            {
                diff.Add(new Difference("Number of items", dictionary?.Count, dic2?.Count));
                return false;
            }
            foreach (var key in dictionary.Keys)
            {
                if (!dic2.TryGetValue(key, out Distribution value))
                {
                    diff.Add(new Difference("Missing value", null, key));
                    return false;
                }
                string s1 = dictionary[key]?.ToString() ?? "";
                string s2 = value?.ToString() ?? "";
                if (s1 != s2)
                {
                    bool similar = false;
                    if (value.UniqueValue == dictionary[key].UniqueValue)
                        similar = true;
                    diff.Add(new Difference( key+(similar?" (similar)":""), s1, s2));
                }
            }
            return diff.Count == 0;
        }
        public static Dictionary<string, double> GenerateSingleInstanceValues(this Dictionary<string, Distribution> dictionary)
        {
            Dictionary<string, double> valuesArray = [];
            foreach (string key in dictionary.Keys)
            {
                Distribution distribution = dictionary[key];
                valuesArray.Add(key, distribution.GenerateNNumbers(1, distribution.Range, ordered: false)[0]);
            }
            return valuesArray;
        }


        public static Dictionary<string, double[]> GenerateMultipleInstanceValues(this Dictionary<string, Distribution> dictionary, int number, bool ordered)
        {
            Dictionary<string, double[]> valuesArray = [];
            if (dictionary == null)
                return valuesArray;
            foreach (string key in dictionary.Keys)
            {
                Distribution distribution = dictionary[key];
                valuesArray.Add(key, distribution.GenerateNNumbers(number, distribution.Range, ordered));
            }
            return valuesArray;
        }
    }
}
