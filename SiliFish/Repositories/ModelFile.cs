using GeneticSharp;
using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
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
            List<string> oldParamPatterns = new()
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
                    _ = double.TryParse(m.Groups[1].ToString(), out min);

                m = ascRegex.Match(cellReach);
                double asc = 0;
                if (m.Success && m.Groups?.Count >= 2)
                    _ = double.TryParse(m.Groups[1].ToString(), out asc);
                double minAscReach = asc > 0 ? min : 0;
                string ascending = asc > 0 ? "true" : "false";

                m = descRegex.Match(cellReach);
                double desc = 0;
                if (m.Success && m.Groups?.Count >= 2)
                    _ = double.TryParse(m.Groups[1].ToString(), out desc);
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

        private static bool FixCoreTypeJson(ref string json)
        {
            bool updated = false;
            if (json.Contains("\"CoreType\": 0") || json.Contains("\"CoreType\": 1") || json.Contains("\"CoreType\": 2"))
            {
                json = json.Replace("\"CoreType\": 0", "\"CoreType\": \"Izhikevich_5P\"")
                .Replace("\"CoreType\": 1", "\"CoreType\": \"Izhikevich_9P\"")
                .Replace("\"CoreType\": 2", "\"CoreType\": \"Leaky_Integrator\"");
                updated = true;
            }
            return updated;
        }
        private static bool FixCoreParametersJson(ref string json)
        {
            bool updated = false;
            bool checkTau = false;

            if (json.Contains("\"ClassType\": \"RunningModel\""))
            {
                Regex coreRegex = new("\"Core\": {(\\s+.*[^}]*?)}");
                MatchCollection matchCollection = coreRegex.Matches(json);
                for (int i = matchCollection.Count - 1; i >= 0; i--)
                {
                    Match match = matchCollection[i];
                    int index = match.Groups[0].Index;
                    string core = match.Value;
                    Regex paramRegex = new("\"Parameters\": {(\\s+.*[^}]*?)}");
                    Match parMatch = paramRegex.Match(core);
                    if (parMatch.Success)
                    {
                        string newJson = "";
                        Regex singleRegex = new("\"(.*\\.)(.*\":.*,)");
                        MatchCollection singleMatch = singleRegex.Matches(parMatch.Value);
                        for (int j = 0; j < singleMatch.Count; j++)
                        {
                            Match singleParam = singleMatch[j];
                            newJson += $"\"{singleParam.Groups[2]}\r\n";
                        }
                        json = json.Remove(index + parMatch.Index, parMatch.Value.Length);
                        newJson = newJson.Replace("V_", "V");//change V_r, V_t, V_max to Vr, Vt, Vmax
                        json = json.Insert(index + parMatch.Index, newJson);
                    }
                    else
                    {
                        checkTau = true;
                    }
                    updated = true;
                }
            }
            else
            {
                if (json.Contains(".V_"))
                {
                    json = json.Replace("V_", "V");
                    updated = true;
                }
            }
            if (checkTau && json.Contains("\"taur\""))
            {
                json = json.Replace("\"taur\"", "\"TauR\"");
                updated = true;
            }

            return updated;
        }

        public static List<string> CheckJSONVersion(ref string json)
        {
            Regex versionRegex = new("\"Version\": (.*),");
            Match version = versionRegex.Match(json);
            List<string> list = new();
            if (!version.Success)//Version is added on 2.2.3
            {
                //Compare to Version 0.1
                if (json.Contains("\"TimeLine\""))
                {
                    //created by old version 
                    json = json.Replace("\"TimeLine\"", "\"TimeLine_ms\"");
                    list.Add("Stimulus parameters");
                }
                if (FixCoreTypeJson(ref json))
                    list.Add("Cell core parameters");
                if (FixCoreParametersJson(ref json))
                    list.Add("Cell core parameters");
                RemoveOldParameters(ref json);
                if (FixDistributionJson(ref json))
                    list.Add("Spatial distributions");
                if (FixCellReachJson(ref json))
                    list.Add("connection ranges");
                json = json.Replace("\"StimulusSettings\":", "\"Settings\":");
                json = Regex.Replace(json, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                json = Regex.Replace(json, ",[\\s]*}", " }");
            }
            return list;
        }

        public static void Save(string fileName, ModelBase model)
        {
            model.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
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

        public static void SaveModelDynamicsToCSV(string filename, double[] Time, Dictionary<string, double[]> Values)
        {
            if (filename == null || Time == null || Values == null)
                return;

            using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
            using StreamWriter sw = new(fs);
            sw.WriteLine("Time," + string.Join(',', Values.Keys));

            for (int t = 0; t < Time.Length; t++)
            {
                string row = Time[t].ToString() + ',' +
                    string.Join(',', Values.Select(item => item.Value[t]));
                sw.WriteLine(row);
            }
        }

        public static void SaveTailMovementToCSV(string filename, double[] Time, Coordinate[] tail_tip_coord)
        {
            if (filename == null || tail_tip_coord == null)
                return;

            string tailFilename = Path.ChangeExtension(filename, "TailTip.csv");
            using FileStream fsTail = File.Open(tailFilename, FileMode.Create, FileAccess.Write);
            using StreamWriter swTail = new(fsTail);
            swTail.WriteLine("Time,X,Y,Z");
            for (int t = 0; t < Time.Length; t++)
            {
                if (t >= tail_tip_coord.Length)
                    break;
                string row = $"{Time[t]:0.##},{tail_tip_coord[t].X:0.000},{tail_tip_coord[t].Y:0.000},{tail_tip_coord[t].Z:0.000}";
                swTail.WriteLine(row);
            }
        }

        public static void SaveEpisodesToCSV(string filename, int run, List<SwimmingEpisode> episodes)
        {
            if (filename == null || episodes == null)
                return;

            string episodeFilename = Path.ChangeExtension(filename, "Episodes.csv");
            string beatsFilename = Path.ChangeExtension(filename, "Beats.csv");
            using FileStream fsEpisode = File.Open(episodeFilename, FileMode.Append, FileAccess.Write);
            using FileStream fsBeats = File.Open(beatsFilename, FileMode.Append, FileAccess.Write);
            using StreamWriter swEpisode = new(fsEpisode);
            using StreamWriter swBeats = new(fsBeats);
            if (run == 1)
            {
                swEpisode.WriteLine("Run,Episode,Start,Finish,Duration,Time to Next,Tail Beat Freq.,# of Tail Beats");
                swBeats.WriteLine("Run,Episode,Beat,Start,Finish,Instan.Freq.");
            }
            int epiCounter = 1;
            foreach (SwimmingEpisode episode in episodes)
            {
                double? timeToNext = null;
                if (epiCounter < episodes.Count - 2)
                {
                    SwimmingEpisode nextEpisode = episodes[epiCounter];//1-based index is used
                    timeToNext = nextEpisode.Start - episode.End;
                }
                string epiRow = $"{run}," +
                    $"{epiCounter}," +
                    $"{episode.Start:0.####}," +
                    $"{episode.End:0.####}," +
                    $"{episode.EpisodeDuration:0.####}," +
                    $"{timeToNext?.ToString() ?? ""}," +
                    $"{episode.BeatFrequency}," +
                    $"{episode.Beats.Count}";
                swEpisode.WriteLine(epiRow);
                int beatCounter = 1;
                foreach ((double beatStart, double beatEnd) in episode.Beats)
                {
                    string beatRow = $"{run}," +
                        $"{epiCounter}," +
                        $"{beatCounter++}," +
                        $"{beatStart:0.####}," +
                        $"{beatEnd:0.####}," +
                        $"{1000 / (beatEnd - beatStart):0.####}";
                    swBeats.WriteLine(beatRow);
                }
                epiCounter++;
            }
        }

        public static void SaveAnimation(string filename, Dictionary<string, Coordinate[]> somiteCoordinates, double[] Time, int startIndex)
        {
            using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
            using StreamWriter sw = new(fs);
            string columnHeaders = string.Join(',', somiteCoordinates.Keys.Select(k => k + "-X," + k + "-Y'"));
            sw.WriteLine(columnHeaders);
            int nmax = Time.Length - 1;
            foreach (var i in Enumerable.Range(1, nmax))
            {
                string rowStart = (Time[startIndex + i - 1]).ToString("0.##");
                string row = rowStart + string.Join(',', somiteCoordinates.Select(item => item.Value[i].X + "," + item.Value[i].X));
                sw.WriteLine(row);
            }
        }

        public static bool SaveStimulusToCSV(string filename, ModelBase model, ModelUnitBase selectedUnit)
        {
            if (filename == null || model == null)
                return false;

            try
            {
                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                if (model is ModelTemplate mt)
                {
                    List<StimulusTemplate> stimulusTemplates =
                        (selectedUnit is CellPoolTemplate cellPoolTemplate) ?
                        mt.AppliedStimuli.Where(stim => stim.TargetPool == cellPoolTemplate.CellGroup).ToList() :
                        mt.AppliedStimuli;
                    sw.WriteLine(StimulusTemplate.CSVExportColumnNames);
                    foreach (StimulusTemplate stim in stimulusTemplates)
                    {
                        sw.WriteLine(stim.CSVExportValues);
                    }
                }
                else if (model is RunningModel rm)
                {
                    List<Stimulus> stimuli =
                        (List<Stimulus>)((selectedUnit is CellPool cellPool) ?
                            cellPool.GetStimuli().Select(stim => stim as Stimulus).ToList() :
                        (selectedUnit is Cell cell) ?
                            cell.Stimuli.ListOfStimulus :
                        rm.GetStimuli().Select(stim => stim as Stimulus)).ToList();
                    sw.WriteLine(Stimulus.CSVExportColumnNames);
                    foreach (Stimulus stim in stimuli)
                    {
                        sw.WriteLine(stim.CSVExportValues);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static bool ReadStimulusFromCSV(string filename, ModelBase model)
        {
            if (filename == null || model == null)
                return false;
            try
            {
                string[] contents = FileUtil.ReadLinesFromFile(filename);
                if (contents.Length <= 1) return false;
                string columns = contents[0];
                int iter = 1;
                if (model is ModelTemplate mt)
                {
                    if (columns != StimulusTemplate.CSVExportColumnNames)
                        return false;
                    mt.ClearStimuli();
                    while (iter < contents.Length)
                    {
                        StimulusTemplate stim = new()
                        {
                            CSVExportValues = contents[iter++]
                        };
                        mt.AppliedStimuli.Add(stim);
                    }
                }
                else if (model is RunningModel rm)
                {
                    if (columns != Stimulus.CSVExportColumnNames)
                        return false;
                    rm.ClearStimuli();
                    while (iter < contents.Length)
                    {
                        Stimulus stim = new();
                        stim.GenerateFromCSVRow(rm, contents[iter++]);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static bool ReadStimulusFromCSV(string filename, ModelBase model, ModelUnitBase SelectedUnit)
        {
            if (filename == null || model == null)
                return false;
            if (SelectedUnit is null)
                return ReadStimulusFromCSV(filename, model);
            try
            {
                string[] contents = FileUtil.ReadLinesFromFile(filename);
                if (contents.Length <= 1) return false;
                string columns = contents[0];
                int iter = 1;
                if (SelectedUnit is Cell cell)
                {
                    if (columns != Stimulus.CSVExportColumnNames)
                        return false;
                    cell.ClearStimuli();
                    while (iter < contents.Length)
                    {
                        Stimulus stim = new();
                        stim.GenerateFromCSVRow(model as RunningModel, contents[iter++]);
                        if (stim.TargetCell == cell)
                            cell.AddStimulus(stim);
                    }
                }
                else if (SelectedUnit is CellPool cellPool)
                {
                    if (columns != Stimulus.CSVExportColumnNames)
                        return false;
                    cellPool.ClearStimuli();
                    while (iter < contents.Length)
                    {
                        Stimulus stim = new();
                        stim.GenerateFromCSVRow(model as RunningModel, contents[iter++]);
                        if (stim.TargetCell.CellPool == cellPool)
                            stim.TargetCell.AddStimulus(stim);
                    }
                }
                else if (SelectedUnit is CellPoolTemplate cellPoolTemp)
                {
                    ModelTemplate mt = model as ModelTemplate;
                    if (columns != StimulusTemplate.CSVExportColumnNames)
                        return false;
                    mt.ClearStimuli(cellPoolTemp.CellGroup);
                    while (iter < contents.Length)
                    {
                        StimulusTemplate stim = new()
                        {
                            CSVExportValues = contents[iter++]
                        };
                        if (stim.TargetPool == cellPoolTemp.CellGroup)
                            mt.AppliedStimuli.Add(stim);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
    }
}
