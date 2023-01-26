using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiliFish.Repositories
{
    public static class ModelFile
    {
        private static bool RemoveOldParameters(ref string json)
        {
            if (json == null)
                return false;
            List<string> oldParamPatterns = new List<string>
            {
                "\"Dynamic.sigma_range\":.*",
                "\"Dynamic.sigma_gap\":.*",
                "\"Dynamic.sigma_chem\":.*",
                "\"Izhikevich_9P.V\": {(\\s+.*[^}]*?)}",
                "\"Izhikevich_9P.u\": {(\\s+.*[^}]*?)}",
                "\"Izhikevich_9P.U\": {(\\s+.*[^}]*?)}"
            };
            int prevLength = json.Length;
            foreach (string pattern in oldParamPatterns)
            {
                json = Regex.Replace(json, pattern + ",", "");
                json = Regex.Replace(json, pattern, "");
            }
            return prevLength != json.Length;
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
                    int curly = JsonUtil.FindOpeningBracket(json, ind);
                    json = json.Remove(ind, key.Length);
                    json = json.Insert(curly + 1, replace);
                    updated = true;
                }
            }
            return updated;
        }
        private static bool FixCellReachJson(ref string json)
        {
            bool updated = false;
            Regex regex = new("\"CellReach\": {(\\s+.*[^}]*?)}");
            Regex ascRegex = new("\"AscendingReach\":(.*),");
            Regex descRegex = new("\"DescendingReach\":(.*),");
            Regex minRegex = new("\"MinReach\":(.*),");
            MatchCollection matchCollection = regex.Matches(json);
            for (int i = matchCollection.Count - 1; i >= 0; i--)
            {
                Match match = matchCollection[i];
                int index = match.Groups[1].Index;
                string cellReach = match.Value;

                Match m = minRegex.Match(cellReach);
                double min = 0;
                if (m.Success && m.Groups?.Count >= 2)
                    double.TryParse(m.Groups[1].ToString(), out min);

                m = ascRegex.Match(cellReach);
                double asc = 0;
                if (m.Success && m.Groups?.Count >= 2)
                    double.TryParse(m.Groups[1].ToString(), out asc);
                double minAscReach = asc > 0 ? min : 0;
                string ascending = asc > 0 ? "true" : "false";

                m = descRegex.Match(cellReach);
                double desc = 0;
                if (m.Success && m.Groups?.Count >= 2)
                    double.TryParse(m.Groups[1].ToString(), out desc);
                double minDescReach = desc > 0 ? min : 0;
                string descending = desc > 0 ? "true" : "false";

                string newJson = $"\"Ascending\": {ascending}," +
                    $"\"Descending\": {descending}," +
                    $"\"MinAscReach\": {minAscReach}," +
                    $"\"MaxAscReach\": {asc}," +
                    $"\"MinDescReach\": {minDescReach}," +
                    $"\"MaxDescReach\": {desc},";
                json = json.Insert(index, newJson);
                updated = true;
            }
            return updated;
        }
        public static List<string> CheckJSONVersion(ref string json)
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
            if (FixCellReachJson(ref json))
                list.Add("connection ranges");
            json = json.Replace("\"StimulusSettings\":", "\"Settings\":");
            json = Regex.Replace(json, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            json = Regex.Replace(json, "},[\\s]*}", "} }");

            return list;
        }

        public static void Save(string fileName, ModelBase model)
        {
            JsonUtil.SaveToJsonFile(fileName, model);
        }

        public static ModelBase Load(string fileName, out List<string> issues)
        {
            string json = FileUtil.ReadFromFile(fileName);
            issues = CheckJSONVersion(ref json);
            if (json.Contains("\"ClassType\": \"RunningModel\","))
                return (RunningModel)JsonUtil.ToObject(typeof(RunningModel), json);
            else
                return (ModelTemplate)JsonUtil.ToObject(typeof(ModelTemplate), json);
        }
    }
}
