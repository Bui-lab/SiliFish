﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using SiliFish.DataTypes;

namespace SiliFish.Extensions
{
    public static class DictionaryExtensions
    {
        public static double ReadDouble(this Dictionary<string, object> dictionary, string key, double defaultValue = default)
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
                        val = valdt.GenerateNNumbers(1, valdt.Range)[0];
                    }
                    else if (val is JsonElement element)
                    {
                        var json = element.GetRawText();
                        if (element.ValueKind == JsonValueKind.String)
                        {
                            var str = JsonSerializer.Deserialize<string>(json);
                            return (T)Convert.ChangeType(str, typeof(T));
                        }
                        return JsonSerializer.Deserialize<T>(json);
                    }
                    return (T)Convert.ChangeType(val, typeof(T));
                }
                return defaultValue;
            }
            catch { return defaultValue; }
        }

        public static object GetNestedValue(this Dictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            foreach (var obj in dictionary.Values.Where(x=>x is Dictionary<string, object>))
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
    }
}
