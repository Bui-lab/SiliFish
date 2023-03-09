using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiliFish.Repositories
{
    public static class CellCoreUnitFile
    {

        private static bool FixCoreTypeJson(ref string json)
        {
            bool updated = false;
            Dictionary<string, string> cores = new()
            {
                { "\"CoreType\": \"HodgkinHuxley\"", "\"$type\": \"hodgkinhuxley\"," },
                { "\"CoreType\": \"HodgkinHuxleyClassic\"", "\"$type\": \"hodgkinhuxleyclassic\"," },
                { "\"CoreType\": \"Izhikevich_5P\"", "\"$type\": \"izhikevich5p\"," },
                { "\"CoreType\": \"Izhikevich_9P\"", "\"$type\": \"izhikevich9p\"," },
                { "\"CoreType\": \"Leaky_Integrator\"", "\"$type\": \"leakyintegrator\"," },
                { "\"CoreType\": \"QuadraticIntegrateAndFire\"", "\"$type\": \"qif\"," }
            };
            foreach (string key in cores.Keys)
            {
                string replace = cores[key];
                while (json.Contains(key))
                {
                    int ind = json.IndexOf(key);
                    int curly = JsonUtil.FindOpeningBracket(json, ind);
                    json = json.Remove(ind, key.Length);
                    json = json.Insert(curly + 1, replace);
                    updated = true;
                }
            }
            return updated;
        }
        private static bool FixParametersJson(ref string json)
        {
            bool updated = false;
            Regex paramRegex = new("\"Parameters\": {(\\s+.*[^}]*?)}");
            MatchCollection parMatch = paramRegex.Matches(json);
            if (parMatch.Count > 0)
            {
                string newJson = "";
                Regex singleRegex = new("\"(.*\\.)(.*\":.*,)");
                MatchCollection singleMatch = singleRegex.Matches(parMatch[0].Value);
                for (int j = 0; j < singleMatch.Count; j++)
                {
                    Match singleParam = singleMatch[j];
                    newJson += $"\"{singleParam.Groups[2]}\r\n";
                }
                json = json.Remove(parMatch[0].Index, parMatch[0].Value.Length);
                newJson = newJson.Replace("V_", "V");//change V_r, V_t, V_max to Vr, Vt, Vmax
                json = json.Insert(parMatch[0].Index, newJson);
                updated = true;
            }

            return updated;
        }
        public static List<string> CheckJSONVersion(ref string json)
        {
            List<string> list = new();
            //Compare to Version 0.1
            if (json.Contains("\"CoreType\":") && FixCoreTypeJson(ref json))
                list.Add("Core type");
            if (json.Contains("\"Parameters\":") && FixParametersJson(ref json))
                list.Add("Cell parameters");
            if (!json.StartsWith("["))
                json = $"[\r\n{json}\r\n]";
            json = JsonUtil.CleanUp(json);
            return list;
        }

        public static void Save(string fileName, CellCoreUnit core)
        {
            CellCoreUnit[] arr = new CellCoreUnit[] { core };
            //the core is saved as an array to benefit from $type tag added by the JsonSerializer
            JsonUtil.SaveToJsonFile(fileName, arr);
        }


        public static CellCoreUnit Load(string fileName)
        {
            string JSONString = FileUtil.ReadFromFile(fileName);
            if (string.IsNullOrEmpty(JSONString))
                return null;
            CheckJSONVersion(ref JSONString);
            //the core is saved as an array to benefit from $type tag added by the JsonSerializer
            CellCoreUnit[] arr = (CellCoreUnit[])JsonUtil.ToObject(typeof(CellCoreUnit[]), JSONString);
            if (arr != null && arr.Any())
            {
                CellCoreUnit core = arr[0];
                return core;
            }
            return null;
        }
    }
}
