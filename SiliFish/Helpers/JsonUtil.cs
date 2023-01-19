using SiliFish.Helpers.JsonConverters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SiliFish.Helpers
{
    public static class JsonUtil
    {
        public static string ToJson(object content)
        {
            var options = new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() },
                WriteIndented = true
            };
            string jsonstring = JsonSerializer.Serialize(content, options);
            return jsonstring;
        }
        public static object ToObject(Type content, string jsonstring)
        {
            var options = new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() }
            };
            return JsonSerializer.Deserialize(jsonstring, content, options);
        }

        public static void SaveToJsonFile(string path, object content)
        {
            var options = new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() },
                
                WriteIndented = true
            };
            string jsonstring = JsonSerializer.Serialize(content, options);
            File.WriteAllText(path, jsonstring);
        }

        public static Dictionary<string, object> ReadDictionaryFromJsonFile(string path)
        {
            string jsonstring = File.ReadAllText(path);
            var options = new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() }
            };
            return (Dictionary<string, object>)JsonSerializer.Deserialize(jsonstring, typeof(Dictionary<string, object>), options);
        }

        private static bool RemoveOldParameters(ref string json)
        {
            if (json == null)
                return false;
            List<string> oldParamPatterns = new List<string>
            {
                "\"Dynamic.sigma_range\":.*",
                "\"Dynamic.sigma_gap\":.*",
                "\"Dynamic.sigma_chem\":.*"
            };
            int prevLength = json.Length;
            foreach (string pattern in oldParamPatterns)
            {
                json = Regex.Replace(json, pattern, "");
            }
            return prevLength!=json.Length;
        }
        private static bool FixDistributionJson(ref string json)
        {
            bool updated = false;
            Dictionary<string, string> dist = new()
            {
                { "\"DistType\": \"SiliFish.DataTypes.UniformDistribution\",", "\"$type\": \"uniform\"," },
                { "\"DistType\": \"SiliFish.DataTypes.Constant_NoDistribution\",", "\"$type\": \"constant\"," },
                { "\"DistType\": \"SiliFish.DataTypes.SpacedDistribution\",", "\"$type\": \"spaced\"," },
                { "\"DistType\": \"SiliFish.DataTypes.GaussianDistribution\",", "\"$type\": \"gaussian\"," },
                { "\"DistType\": \"SiliFish.DataTypes.BimodalDistribution\",", "\"$type\": \"bimodal\"," },
                { "\"DistType\": \"UniformDistribution\",", "\"$type\": \"uniform\"," },
                { "\"DistType\": \"Constant_NoDistribution\",", "\"$type\": \"constant\"," },
                { "\"DistType\": \"SpacedDistribution\",", "\"$type\": \"spaced\"," },
                { "\"DistType\": \"GaussianDistribution\",", "\"$type\": \"gaussian\"," },
                { "\"DistType\": \"BimodalDistribution\",", "\"$type\": \"bimodal\"," }
            };
            foreach (string key in dist.Keys)
            {
                string replace = dist[key];
                while (json.Contains(key))
                {
                    int ind = json.IndexOf(key);
                    int curly = json.LastIndexOf("{", ind - 1);
                    json = json.Remove(ind, key.Length);
                    json = json.Insert(curly + 1, replace);
                    updated = true;
                }    
            }
            return updated;
        }

        public static List<string> CheckJsonVersion(ref string json)
        {
            List<string> list = new();
            //Compare to Version 0.1
            if (json.Contains("\"TimeLine\""))
            {
                //created by old version 
                json = json.Replace("\"TimeLine\"", "\"TimeLine_ms\"");
                list.Add("Stimulus parameters");
            }
            if (json.Contains("\"CoreType\": 0") || json.Contains("\"CoreType\": 1") || json.Contains("\"CoreType\": 2"))
            {
                json = json.Replace("\"CoreType\": 0", "\"CoreType\": \"Izhikevich_5P\"")
                .Replace("\"CoreType\": 1", "\"CoreType\": \"Izhikevich_9P\"")
                .Replace("\"CoreType\": 2", "\"CoreType\": \"Leaky_Integrator\"");
                list.Add("Core types");
            }
            RemoveOldParameters(ref json);
            if (FixDistributionJson(ref json))
                list.Add("Spatial distributions");
            json = json.Replace("\"StimulusSettings\":", "\"Settings\":");
            return list;
        }

    }
}
