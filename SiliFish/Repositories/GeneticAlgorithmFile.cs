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
                json= json.Replace("V_", "V");//change V_r, V_t, V_max to Vr, Vt, Vmax
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
            return (CoreSolverSettings)JsonUtil.ToObject(typeof(CoreSolverSettings), JSONString);
        }
    }
}
