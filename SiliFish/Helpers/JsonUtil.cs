using SiliFish.Helpers.JsonConverters;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SiliFish.Helpers
{

    public static class JsonUtil
    {
        public static JsonSerializerOptions SerializerOptions = new()
            {
                Converters = { new ColorJsonConverter() },
                WriteIndented = true
            };
        public static string ToJson(object content)
        {
            string jsonstring = JsonSerializer.Serialize(content, SerializerOptions);
            return jsonstring;
        }
        public static object ToObject(Type content, string jsonstring)
        {
            try
            {
                return JsonSerializer.Deserialize(jsonstring, content, SerializerOptions);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static void SaveToJsonFile(string path, object content)
        {
            string jsonstring = JsonSerializer.Serialize(content, SerializerOptions);
            FileUtil.SaveToFile(path, jsonstring);
        }
        public static int FindOpeningBracket(string json, int searchend)
        {
            int endCounter = 0;
            char[] curlies = ['{', '}'];
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
