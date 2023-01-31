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
            json = json.Replace("\"$type\": \"uniform\"", "\"$type\": \"Uniform\"");
            json = json.Replace("\"$type\": \"constant\"", "\"$type\": \"Constant\"");
            json = json.Replace("\"$type\": \"spaced\"", "\"$type\": \"Equally Spaced\"");
            json = json.Replace("\"$type\": \"gaussian\"", "\"$type\": \"Gaussian\"");
            json = json.Replace("\"$type\": \"bimodal\"", "\"$type\": \"Bimodal\"");

            bool updated = false;
            Dictionary<string, string> dist = new()
            {
                { "\"DistType\": \"SiliFish.DataTypes.UniformDistribution\",", "\"$type\": \"Uniform\"," },
                { "\"DistType\": \"SiliFish.DataTypes.Constant_NoDistribution\",", "\"$type\": \"Constant\"," },
                { "\"DistType\": \"SiliFish.DataTypes.SpacedDistribution\",", "\"$type\": \"Equally Spaced\"," },
                { "\"DistType\": \"SiliFish.DataTypes.GaussianDistribution\",", "\"$type\": \"Gaussian\"," },
                { "\"DistType\": \"SiliFish.DataTypes.BimodalDistribution\",", "\"$type\": \"Bimodal\"," },
                { "\"DistType\": \"UniformDistribution\",", "\"$type\": \"Uniform\"," },
                { "\"DistType\": \"Constant_NoDistribution\",", "\"$type\": \"Constant\"," },
                { "\"DistType\": \"SpacedDistribution\",", "\"$type\": \"Equally Spaced\"," },
                { "\"DistType\": \"GaussianDistribution\",", "\"$type\": \"Gaussian\"," },
                { "\"DistType\": \"BimodalDistribution\",", "\"$type\": \"Bimodal\"," }
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
            /*
            "DistanceMode": 0,
        "FixedDuration_ms": null,
        "Delay_ms": 0,
        "Weight": 0.04,*/
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

                string newJson = $"\r\n\"Ascending\": {ascending}," +
                    $"\"Descending\": {descending}," +
                    $"\"MinAscReach\": {minAscReach}," +
                    $"\"MaxAscReach\": {asc}," +
                    $"\"MinDescReach\": {minDescReach}," +
                    $"\"MaxDescReach\": {desc},";
                json = json.Insert(index, newJson);
                updated = true;
            }


            MatchCollection matchCollection2 = regex.Matches(json);//move DistanceMode, FixedDuration_ms, Delay_ms and Weight outside
            Regex distRegex = new("\"DistanceMode\":(.*),");
            Regex durRegex = new("\"FixedDuration_ms\":(.*),");
            Regex delayRegex = new("\"Delay_ms\":(.*),");
            Regex weightRegex = new("\"Weight\":(.*),");

            for (int i = matchCollection2.Count - 1; i >= 0; i--)
            {
                string newJson = "";
                Match match = matchCollection2[i];
                int index = json.LastIndexOf("\"CellReach\"", match.Groups[1].Index);
                string cellReach = match.Value;

                Match m = distRegex.Match(cellReach);
                if (m.Success && m.Groups?.Count >= 2)
                    newJson += m.Value;

                m = durRegex.Match(cellReach);
                if (m.Success && m.Groups?.Count >= 2)
                    newJson += m.Value;
             
                m = delayRegex.Match(cellReach);
                if (m.Success && m.Groups?.Count >= 2)
                    newJson += m.Value;

                m = weightRegex.Match(cellReach);
                if (m.Success && m.Groups?.Count >= 2)
                    newJson += m.Value;

                if (!string.IsNullOrEmpty(newJson))
                {
                    json = json.Insert(index, newJson);
                    updated = true;
                }
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
            return ReadFromJson(json, out issues);
        }

        public static ModelBase ReadFromJson(string json, out List<string> issues)
        {
            issues = CheckJSONVersion(ref json);
            ModelBase mb;
            if (json.Contains("\"ClassType\": \"RunningModel\","))
                mb = (RunningModel)JsonUtil.ToObject(typeof(RunningModel), json);
            else
                mb = (ModelTemplate)JsonUtil.ToObject(typeof(ModelTemplate), json);
            mb.BackwardCompatibility();
            mb.LinkObjects();
            return mb;
        }
    }
}
