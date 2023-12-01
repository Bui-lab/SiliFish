using SiliFish.DynamicUnits;
using SiliFish.Helpers.JsonConverters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
            try
            {
                var options = new JsonSerializerOptions()
                {
                    Converters = { new ColorJsonConverter() }
                };
                return JsonSerializer.Deserialize(jsonstring, content, options);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static void SaveToJsonFile(string path, object content)
        {
            var options = new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() },

                WriteIndented = true
            };
            string jsonstring = JsonSerializer.Serialize(content, options);
            FileUtil.SaveToFile(path, jsonstring);
        }
        public static int FindOpeningBracket(string json, int searchend)
        {
            int endCounter = 0;
            char[] curlies = new char[] { '{', '}' };
            while (searchend > 0)
            {
                int curly = json.LastIndexOfAny(curlies, searchend - 1);
                if (curly < 0)
                    return -1;
                if (json[curly] == '}')
                    endCounter++;
                else if (endCounter == 0)
                    return curly;
                else endCounter--;
                searchend = curly;
            }
            return -1;
        }

        public static string CleanUp(string json)
        {
            json = Regex.Replace(json, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            json = Regex.Replace(json, ",[\\s]*,", " ,");//remove consecutive commas
            json = Regex.Replace(json, ",[\\s]*}", " }");//remove commas before curly bracket end
            return json;
        }
    }
}
