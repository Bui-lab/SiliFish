using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Services.Optimization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiliFish.Repositories
{
    public static class GeneticAlgorithmFile
    {
        public static List<string> CheckJSONVersion(ref string json)
        {
            List<string> list = new();
            Regex versionRegex = new("\"Version\": (.*),");
            Match version = versionRegex.Match(json);
            if (!version.Success)//Version is added on 2.2.3
            {
                json = json.Replace("V_", "V");;//change V_r, V_t, V_max to Vr, Vt, Vmax
            }
            Regex paramRegex = new("\"ParamValues\": {(\\s+.*[^}]*?)}");
            MatchCollection parMatch = paramRegex.Matches(json);
            if (parMatch.Count > 0)
            {
                string newJson = "\"ParamValues\": {";
                Regex singleRegex = new("\"(.*\\.)(.*\":.*,)");
                MatchCollection singleMatch = singleRegex.Matches(parMatch[0].Value);
                if (singleMatch.Count > 0)
                {
                    for (int j = 0; j < singleMatch.Count; j++)
                    {
                        Match singleParam = singleMatch[j];
                        newJson += $"\"{singleParam.Groups[2]}\r\n";
                    }
                    newJson += "}";
                    json = json.Remove(parMatch[0].Index, parMatch[0].Value.Length);
                    json = json.Insert(parMatch[0].Index, newJson);
                    json = JsonUtil.CleanUp(json);
                }
            }
            return list;
        }

        public static void Save(string fileName, CoreSolverSettings coreSolver)
        {
            coreSolver.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            JsonUtil.SaveToJsonFile(fileName, coreSolver);
        }


        public static CoreSolverSettings Load(string fileName)
        {
            string JSONString = FileUtil.ReadFromFile(fileName);
            if (string.IsNullOrEmpty(JSONString))
                return null;
            CheckJSONVersion(ref JSONString);
            CoreSolverSettings css = (CoreSolverSettings)JsonUtil.ToObject(typeof(CoreSolverSettings), JSONString);
            return css;
        }
    }
}
