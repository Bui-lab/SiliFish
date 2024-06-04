using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using OfficeOpenXml;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SiliFish.Repositories
{
    public static class ModelFile
    {
        #region JSON related functions
        private static bool RemoveOldParameters(ref string json)
        {
            if (json == null)
                return false;
            List<string> oldParamPatterns =
            [
                "\"Dynamic.sigma_range\":.*",
                "\"Dynamic.sigma_gap\":.*",
                "\"Dynamic.sigma_chem\":.*",
                "\"Izhikevich_9P.V\": {(\\s+.*[^}]*?)}",
                "\"Izhikevich_9P.u\": {(\\s+.*[^}]*?)}",
                "\"Izhikevich_9P.U\": {(\\s+.*[^}]*?)}"
            ];
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
                json = json.Replace("\"taud\"", "\"TauD\"");
                json = json.Replace("\"vth\"", "\"Vth\"");
                json = json.Replace("\"E_rev\"", "\"Erev\"");
                updated = true;
            }

            return updated;
        }

        private static bool FixSynapseParametersJson(ref string json)
        {
            bool updated = false;
            /*Convert 
            ...
            "Weight": 1,
            ...
            "SynapseParameters": {
                            "TauD": 1,
                            "TauR": 0,
                            "Vth": 0,
                            "Erev": 0
                          },

            to 
            "Parameters": {
                        "TauD": {
                          "$type": "Constant",
                          "UniqueValue": 1,
                          "NoiseStdDev": 0,
                          "Angular": false,
                          "Absolute": true,
                          "RangeStart": 1,
                          "RangeEnd": 1,
                          "Range": 0
                        },
                        ...
                        "Conductance": {
                          "$type": "Constant",
                          "UniqueValue": 1,
                          "NoiseStdDev": 0,
                          "Angular": false,
                          "Absolute": true,
                          "RangeStart": 1,
                          "RangeEnd": 1,
                          "Range": 0
                        }
            ... etc*/
            if (!json.Contains("\"SynapseParameters\": ")) return false;
            //chemical junctions
            Regex coreRegex = new("\"SynapseParameters\": {(\\s+.*[^}]*?)}");
            MatchCollection matchCollection = coreRegex.Matches(json);
            for (int i = matchCollection.Count - 1; i >= 0; i--)
            {
                Match match = matchCollection[i];
                int weightPos = json.LastIndexOf("\"Weight\":", match.Index);
                int weightEndPos = json.IndexOf(',', weightPos);
                double weight = double.Parse(json.Substring(weightPos + 9, weightEndPos - weightPos - 9));
                string paramList = match.Groups[1].Value;
                string newJson = "\"Parameters\": {";
                Regex singleRegex = new("\\s+(\".*\"):(.*)[,\n]");
                MatchCollection singleMatch = singleRegex.Matches(paramList);
                for (int j = 0; j < singleMatch.Count; j++)
                {
                    Match singleParam = singleMatch[j];
                    if (!double.TryParse(singleParam.Groups[2].Value, out double val))
                        val = double.Parse(singleParam.Groups[2].Value.Replace(',', ' '));
                    Constant_NoDistribution constD = new(val);
                    string distJson = JsonUtil.ToJson(new Distribution[] { constD });
                    distJson = distJson[1..^1];
                    newJson += $"{singleParam.Groups[1].Value}: {distJson},\r\n";
                }
                Constant_NoDistribution conductance = new(weight);
                string conductanceJson = JsonUtil.ToJson(new Distribution[] { conductance });
                conductanceJson = conductanceJson[1..^1];
                newJson += $"\"Conductance\": {conductanceJson} \r\n";
                newJson += "}";
                newJson = newJson.Replace("\"Erev\"", "\"ERev\"");
                json = json.Remove(match.Index, match.Value.Length);
                json = json.Insert(match.Index, newJson);

                updated = true;
            }
            //gap junctions
            coreRegex = new("\"SynapseParameters\": null,");
            matchCollection = coreRegex.Matches(json);
            for (int i = matchCollection.Count - 1; i >= 0; i--)
            {
                Match match = matchCollection[i];
                int weightPos = json.LastIndexOf("\"Weight\":", match.Index);
                int weightEndPos = json.IndexOf(',', weightPos);
                double weight = double.Parse(json.Substring(weightPos + 9, weightEndPos - weightPos - 9));
                string newJson = "\"Parameters\": {";
                Constant_NoDistribution constD = new(weight);
                string conductanceJson = JsonUtil.ToJson(new Distribution[] { constD });
                conductanceJson = conductanceJson[1..^1];
                newJson += $"\"Conductance\": {conductanceJson} \r\n";
                newJson += "},";
                json = json.Remove(match.Index, match.Value.Length);
                json = json.Insert(match.Index, newJson);

                updated = true;
            }

            return updated;
        }

        public static List<string> CheckJSONVersion(ref string json)
        {
            Regex versionRegex = new("\"Version\": (.*),");
            Match version = versionRegex.Match(json);
            List<string> list = [];
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
                    list.Add("junction ranges");
                json = json.Replace("\"StimulusSettings\":", "\"Settings\":");
                json = JsonUtil.CleanUp(json);
            }
            else
            {
                if (version.Groups[1].Value.CompareTo("\"2.5.1.0\"") < 0)
                {
                    if (json.Contains("\"AppliedStimuli\": ["))
                        json = json.Replace("\"AppliedStimuli\": [", "\"StimulusTemplates\": [");
                }
                if (version.Groups[1].Value.CompareTo("\"2.5.2.0\"") < 0)
                {
                    if (json.Contains("\"Delay_ms\": 0,"))
                        json = json.Replace("\"Delay_ms\": 0,", "\"Delay_ms\": null,");
                }
                if (version.Groups[1].Value.CompareTo("\"2.6.1\"") < 0)
                {
                    if (FixSynapseParametersJson(ref json))
                        list.Add("Synapse core parameters");
                }
                if (version.Groups[1].Value.CompareTo("\"2.6.2.0\"") < 0)
                {
                    if (json.Contains("\"PoolSource\":"))
                        json = json.Replace("\"PoolSource\":", "\"SourcePool\":");
                    if (json.Contains("\"PoolTarget\":"))
                        json = json.Replace("\"PoolTarget\":", "\"TargetPool\":");
                }
                if (version.Groups[1].Value.CompareTo("\"2.7.3.0\"") < 0)
                {
                    if (json.Contains("\"ConnectionType\":"))
                        json = json.Replace("\"ConnectionType\":", "\"JunctionType\":");
                }
            }
            return list;
        }
        public static void SaveToJson(string fileName, ModelBase model)
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
            mb?.BackwardCompatibility();
            mb?.LinkObjects();
            mb?.BackwardCompatibilityAfterLinkObjects();
            return mb;
        }
        #endregion

        #region CSV Functions

        public static bool SaveDataToFile(RunningModel model, string filename)
        {
            try
            {
                bool jncTracking = model.Settings.JunctionLevelTracking;

                Dictionary<string, double[]> Vdata_list = [];
                Dictionary<string, double[]> Gapdata_list = [];
                Dictionary<string, double[]> Syndata_list = [];
                foreach (CellPool pool in model.NeuronPools.Union(model.MusclePools).OrderBy(p => p.PositionLeftRight).ThenBy(p => p.CellGroup))
                {
                    //pool.GetCells().Select(c => Vdata_list.TryAdd(c.ID, c.V));
                    foreach (Cell c in pool.GetCells())
                    {
                        Vdata_list.Add(c.ID, c.V.AsArray());
                        if (jncTracking)
                        {
                            c.GapJunctions.Where(jnc => jnc.Cell2 == c).ToList().ForEach(jnc => Gapdata_list.TryAdd(jnc.ID, jnc.InputCurrent));
                            if (c is MuscleCell)
                            {
                                (c as MuscleCell).EndPlates.ForEach(jnc => Syndata_list.TryAdd(jnc.ID, jnc.InputCurrent));
                            }
                            else if (c is Neuron)
                            {
                                (c as Neuron).Synapses.ForEach(jnc => Syndata_list.TryAdd(jnc.ID, jnc.InputCurrent));
                            }
                        }
                    }

                }
                if (jncTracking)
                {
                    string Vfilename = Path.ChangeExtension(filename, "V.csv");
                    SaveModelDynamicsToCSV(filename: Vfilename, Time: model.TimeArray, Values: Vdata_list);
                    string Gapfilename = Path.ChangeExtension(filename, "Gap.csv");
                    string Synfilename = Path.ChangeExtension(filename, "Syn.csv");
                    SaveModelDynamicsToCSV(filename: Gapfilename, Time: model.TimeArray, Values: Gapdata_list);
                    SaveModelDynamicsToCSV(filename: Synfilename, Time: model.TimeArray, Values: Syndata_list);
                }
                else
                {
                    SaveModelDynamicsToCSV(filename: filename, Time: model.TimeArray, Values: Vdata_list);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool SaveModelDynamicsToCSV(string filename, double[] Time, Dictionary<string, double[]> Values)
        {
            try
            {
                if (filename == null || Time == null || Values == null)
                    return false;

                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                sw.WriteLine("Time," + string.Join(',', Values.Keys));

                for (int t = 0; t < Time.Length; t++)
                {
                    string row = Time[t].ToString() + ',' +
                        string.Join(',', Values.Select(item => item.Value[t].ToString(GlobalSettings.PlotDataFormat)));
                    sw.WriteLine(row);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static void SaveEpisodesToCSV(string filename, int run, SwimmingEpisodes episodes)
        {
            if (filename == null || episodes == null)
                return;

            try
            {
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
                foreach (SwimmingEpisode episode in episodes.Episodes)
                {
                    double? timeToNext = null;
                    if (epiCounter < episodes.EpisodeCount - 2)
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
                    foreach (Beat b in episode.Beats)
                    {
                        string beatRow = $"{run}," +
                            $"{epiCounter}," +
                            $"{beatCounter++}," +
                            $"{b.BeatStart:0.####}," +
                            $"{b.BeatEnd:0.####}," +
                            $"{1000 / (b.BeatEnd - b.BeatStart):0.####}";
                        swBeats.WriteLine(beatRow);
                    }
                    epiCounter++;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static void SaveAnimation(string filename, Dictionary<string, Coordinate[]> somiteCoordinates, double[] Time, int startIndex)
        {
            try
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
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        #region Stimulus-CSV
        public static bool SaveStimulusToCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits)
        {
            if (filename == null || model == null)
                return false;

            try
            {
                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                if (model is ModelTemplate mt)
                {
                    sw.WriteLine(string.Join(",", StimulusTemplate.ColumnNames));
                    if (selectedUnits?.Count > 0)
                    {
                        foreach (ModelUnitBase unit in selectedUnits)
                        {
                            List<StimulusTemplate> stimulusTemplates =
                                (unit is CellPoolTemplate cellPoolTemplate) ?
                                mt.StimulusTemplates.Where(stim => stim.TargetPool == cellPoolTemplate.CellGroup).ToList() :
                                mt.StimulusTemplates;
                            foreach (StimulusTemplate stim in stimulusTemplates)
                            {
                                sw.WriteLine(CSVUtil.WriteCSVLine(stim.ExportValues()));
                            }
                        }
                    }
                    else
                    {
                        foreach (StimulusTemplate stim in mt.StimulusTemplates)
                        {
                            sw.WriteLine(CSVUtil.WriteCSVLine(stim.ExportValues()));
                        }
                    }
                }
                else if (model is RunningModel rm)
                {
                    sw.WriteLine(string.Join(",", Stimulus.ColumnNames));
                    if (selectedUnits?.Count > 0)
                    {
                        foreach (ModelUnitBase unit in selectedUnits)
                        {
                            List<Stimulus> stimuli =
                            ((unit is CellPool cellPool) ?
                                cellPool.GetStimuli().Select(stim => stim as Stimulus).ToList() :
                            (unit is Cell cell) ?
                                cell.Stimuli.ListOfStimulus :
                            rm.GetStimuli().Select(stim => stim as Stimulus)).ToList();
                            foreach (Stimulus stim in stimuli)
                            {
                                sw.WriteLine(CSVUtil.WriteCSVLine(stim.ExportValues()));
                            }
                        }
                    }
                    else 
                    {
                        foreach (Stimulus stim in rm.GetStimuli().Select(stim => stim as Stimulus).ToList())
                        {
                            sw.WriteLine(CSVUtil.WriteCSVLine(stim.ExportValues()));
                        }
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
                    if (columns != string.Join(",", StimulusTemplate.ColumnNames))
                        return false;
                    mt.ClearStimuli();
                    while (iter < contents.Length)
                    {
                        StimulusTemplate stim = new();
                        stim.ImportValues([.. contents[iter++].Split(",")]);
                        mt.StimulusTemplates.Add(stim);
                    }
                    mt.LinkObjects();
                }
                else if (model is RunningModel rm)
                {
                    if (columns != string.Join(",", Stimulus.ColumnNames))
                        return false;
                    rm.ClearStimuli();
                    while (iter < contents.Length)
                    {
                        Stimulus stim = new();
                        stim.GenerateFromCSVRow(rm, contents[iter++]);
                    }
                    rm.LinkObjects();
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static bool ReadStimulusFromCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits)
        {
            if (filename == null || model == null)
                return false;
            if (selectedUnits is null || selectedUnits.Count == 0)
                return ReadStimulusFromCSV(filename, model);
            try
            {
                string[] contents = FileUtil.ReadLinesFromFile(filename);
                if (contents.Length <= 1) return false;
                string columns = contents[0];
                int iter = 1;
                foreach (ModelUnitBase unit in selectedUnits)
                {
                    if (unit is Cell cell)
                    {
                        if (columns != string.Join(",", Stimulus.ColumnNames))
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
                    else if (unit is CellPool cellPool)
                    {
                        if (columns != string.Join(",", Stimulus.ColumnNames))
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
                    else if (unit is CellPoolTemplate cellPoolTemp)
                    {
                        ModelTemplate mt = model as ModelTemplate;
                        if (columns != string.Join(",", StimulusTemplate.ColumnNames))
                            return false;
                        mt.ClearStimuli(cellPoolTemp.CellGroup);
                        while (iter < contents.Length)
                        {
                            StimulusTemplate stim = new();
                            stim.ImportValues([.. contents[iter++].Split(",")]);
                            if (stim.TargetPool == cellPoolTemp.CellGroup)
                                mt.StimulusTemplates.Add(stim);
                        }
                    }
                }
                model.LinkObjects();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        #endregion

        #region Cells
        public static bool SaveCellsToCSV(string filename, RunningModel model, List<ModelUnitBase> selectedUnits)
        {
            if (filename == null || model == null)
                return false;
            try
            {
                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                sw.WriteLine(string.Join(",", Cell.ColumnNames));
                List<Cell> cells = [];
                if (selectedUnits?.Count > 0)
                {
                    foreach (ModelUnitBase unit in selectedUnits)
                    {
                        if (unit is CellPool cp)
                            cells.AddRange([.. cp.Cells]);
                        else if (unit is Cell cell)
                            cells.Add(cell);
                    }
                }
                else cells = model.GetCells();
                foreach (Cell cell in cells)
                {
                    sw.WriteLine(CSVUtil.WriteCSVLine(cell.ExportValues()));
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool ReplaceCellsFromCSV(string filename, RunningModel model, List<Cell> cellsToReplace)
        {
            if (filename == null || model == null)
                return false;
            try
            {
                string[] contents = FileUtil.ReadLinesFromFile(filename);
                if (contents.Length <= 1) return false;
                string columns = contents[0];
                int iter = 1;
                if (columns != string.Join(",", Cell.ColumnNames))
                    return false;
                while (iter < contents.Length)
                {
                    string row = contents[iter++];
                    Cell cell = Cell.GenerateFromCSVRow(row);
                    CellPool cellPool = model.CellPools.FirstOrDefault(cp => cp.CellGroup == cell.CellGroup && cp.PositionLeftRight == cell.PositionLeftRight);
                    if (cellPool == null) continue;
                    Cell origCell = cellPool.Cells.FirstOrDefault(c => c.ID == cell.ID);
                    if (origCell != null)
                    {
                        cellsToReplace.Remove(origCell);
                        origCell.ReadFromCSVRow(row);//newly generated cell becomes obsolete - to keep the links
                    }
                    else
                        cellPool.AddCell(cell);
                }
                while (cellsToReplace.Count > 0)
                {
                    Cell cell = cellsToReplace[0];
                    model.CellPools.FirstOrDefault(cp => cp.ID == cell.CellPool.ID)?.RemoveCell(cell);
                    cellsToReplace.Remove(cell);
                }
                model.LinkObjects();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool ReadCellsFromCSV(string filename, RunningModel model, List<ModelUnitBase> selectedUnits)
        {
            if (filename == null || model == null)
                return false;
            try
            {
                List<Cell> cellsToReplace;
                if (selectedUnits?.First() is CellPool cp)
                    cellsToReplace = selectedUnits.Cast<CellPool>().ToList().SelectMany(x => x.Cells).ToList();
                else if (selectedUnits?.First() is Cell c)
                    cellsToReplace = selectedUnits.Cast<Cell>().ToList();
                else
                    cellsToReplace = model.CellPools.SelectMany(x => x.Cells).ToList();
                return ReplaceCellsFromCSV(filename, model, cellsToReplace);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        #endregion

        #region CellPools
        public static bool SaveCellPoolsToCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits)
        {
            if (filename == null || model == null)
                return false;

            try
            {
                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                List<CellPoolTemplate> cellPools;
                if (selectedUnits?.Count > 0)
                    cellPools = selectedUnits.Cast<CellPoolTemplate>().ToList();
                else
                    cellPools = model is ModelTemplate modelTemplate ? modelTemplate.GetCellPools() :
                        model is RunningModel modelRunning ? modelRunning.GetCellPools() : [];
                sw.WriteLine(string.Join(",", CellPoolTemplate.ColumnNames));
                foreach (CellPoolTemplate cpt in cellPools)
                {
                    sw.WriteLine(CSVUtil.WriteCSVLine(cpt.ExportValues()));
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }


        public static bool ReadCellPoolsFromCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits)
        {
            if (filename == null || model == null)
                return false;
            try
            {
                string[] contents = FileUtil.ReadLinesFromFile(filename);
                if (contents.Length <= 1) return false;
                string columns = contents[0];
                int iter = 1;
                if (columns != string.Join(",", CellPoolTemplate.ColumnNames))//same columns are used for cell pool templates and cell pools
                    return false;
                if (model is ModelTemplate modelTemplate)
                {
                    if (selectedUnits?.Count > 0)
                        modelTemplate.CellPoolTemplates.RemoveAll(selectedUnits.Contains);
                    else
                        modelTemplate.CellPoolTemplates.Clear();
                    while (iter < contents.Length)
                    {
                        CellPoolTemplate cpt = new();
                        cpt.ImportValues([.. contents[iter++].Split(",")]);
                        modelTemplate.AddCellPool(cpt);
                    }
                }
                else if (model is RunningModel modelRun)
                {
                    if (selectedUnits?.Count > 0)
                        modelRun.CellPools.RemoveAll(selectedUnits.Contains);
                    else
                        modelRun.CellPools.Clear();
                    while (iter < contents.Length)
                    {
                        CellPool cp = new();
                        cp.ImportValues([.. contents[iter++].Split(",")]);
                        modelRun.AddCellPool(cp);
                    }
                }
                model.LinkObjects();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        #endregion

        #region Junctions
        public static bool SaveJunctionsToCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits, bool gap, bool chemin, bool chemout)
        {
            if (model is ModelTemplate)
                return SaveInterPoolTemplatesToCSV(filename, model, selectedUnits, gap, chemin, chemout);
            else if (model is RunningModel)
                return SaveGapChemJunctionsToCSV(filename, model, selectedUnits, gap, chemin, chemout);
            return false;
        }

        public static bool ReadJunctionsFromCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits, bool gap, bool chemin, bool chemout)
        {
            if (model is ModelTemplate)
                return ReadInterPoolTemplatesFromCSV(filename, model, selectedUnits, gap, chemin, chemout);
            else if (model is RunningModel)
                return ReadGapChemJunctionsFromCSV(filename, model, selectedUnits, gap, chemin, chemout);
            return false;
        }
        private static bool SaveInterPoolTemplatesToCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits, bool gap, bool chemin, bool chemout)
        {
            if (filename == null || model == null)
                return false;

            try
            {
                if (model is not ModelTemplate modelTemplate) return false;
                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                sw.WriteLine(string.Join(",", InterPoolTemplate.ColumnNames));
                List<InterPoolTemplate> list = [];
                if (selectedUnits?.Count > 0)
                {
                    foreach (ModelUnitBase unit in selectedUnits)
                    {
                        if (unit is CellPoolTemplate cpt)
                            list.AddRange(modelTemplate.InterPoolTemplates.Where(jnc =>
                                (gap && jnc.JunctionType == JunctionType.Gap && (jnc.SourcePool == cpt.CellGroup || jnc.TargetPool == cpt.CellGroup)) ||
                                (chemout && jnc.JunctionType != JunctionType.Gap && jnc.SourcePool == cpt.CellGroup) ||
                                (chemin && jnc.JunctionType != JunctionType.Gap && jnc.TargetPool == cpt.CellGroup))
                                .ToList());
                        else
                        {
                            list.AddRange(modelTemplate.InterPoolTemplates.Where(ipt =>
                                gap && ipt.JunctionType == JunctionType.Gap ||
                                (chemout || chemin) && (ipt.JunctionType == JunctionType.Synapse || ipt.JunctionType == JunctionType.NMJ))
                                .ToList());
                        }
                    }
                }
                else
                    list = modelTemplate.InterPoolTemplates;
                foreach (InterPoolTemplate ipt in list)
                {
                    sw.WriteLine(CSVUtil.WriteCSVLine(ipt.ExportValues()));
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static bool ReadInterPoolTemplatesFromCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits, bool gap, bool chemin, bool chemout)
        {
            if (filename == null || model == null)
                return false;
            try
            {
                if (model is not ModelTemplate modelTemplate) return false;
                string[] contents = FileUtil.ReadLinesFromFile(filename);
                if (contents.Length <= 1) return false;
                string columns = contents[0];
                int iter = 1;
                if (columns != string.Join(",", InterPoolTemplate.ColumnNames))
                    return false;
                if (selectedUnits?.Count > 0)
                {
                    foreach (ModelUnitBase unit in selectedUnits)
                    {
                        CellPoolTemplate cpt = unit as CellPoolTemplate;
                        InterPoolTemplate ipt = unit as InterPoolTemplate;
                        if (cpt != null)
                            modelTemplate.RemoveJunctionsOf(cpt, gap, chemin, chemout);
                        else if (ipt != null)
                            modelTemplate.RemoveJunction(ipt);
                        while (iter < contents.Length)
                        {
                            ipt = new();
                            ipt.ImportValues([.. contents[iter++].Split(",")]);
                            //check whether it is part of what 
                            bool jncCheck = gap && ipt.JunctionType == JunctionType.Gap &&
                                    (cpt == null || ipt.SourcePool == cpt.CellGroup || ipt.TargetPool == cpt.CellGroup);
                            if (!jncCheck)
                                jncCheck = chemout && ipt.JunctionType != JunctionType.Gap &&
                                    (cpt == null || ipt.SourcePool == cpt.CellGroup);
                            if (!jncCheck)
                                jncCheck = chemin && ipt.JunctionType != JunctionType.Gap &&
                                    (cpt == null || ipt.TargetPool == cpt.CellGroup);
                            if (jncCheck)
                                modelTemplate.AddJunction(ipt);
                        }
                        iter = 1;
                    }
                }
                else 
                {
                    modelTemplate.RemoveJunctionsOf(null, gap, chemin, chemout);
                    while (iter < contents.Length)
                    {
                        InterPoolTemplate ipt = new();
                        ipt.ImportValues([.. contents[iter++].Split(",")]);
                        //check whether it is part of what 
                        bool jncCheck = gap && ipt.JunctionType == JunctionType.Gap ||
                            (chemout || chemin) && ipt.JunctionType != JunctionType.Gap;
                        if (jncCheck)
                            modelTemplate.AddJunction(ipt);
                    }
                }
                model.LinkObjects();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static bool SaveGapChemJunctionsToCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits, bool gap, bool chemin, bool chemout)
        {
            if (filename == null || model == null)
                return false;

            try
            {
                if (model is not RunningModel runningModel) return false;
                bool chem = chemout || chemin;
                if (!gap && !chem) return false;
                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                sw.WriteLine(string.Join(",", InterPoolBase.ColumnNames));
                List<JunctionBase> list = [];
                if (selectedUnits?.Count > 0)
                {
                    foreach (ModelUnitBase unit in selectedUnits)
                    {
                        if (unit is CellPool cp)
                            list.AddRange(cp.Junctions.Where(jnc =>
                                gap && jnc is GapJunction ||
                                (chemout && jnc is ChemicalSynapse syn && syn.PreNeuron.CellGroup == cp.CellGroup) ||
                                (chemin && jnc is ChemicalSynapse syn2 && syn2.PostCell.CellGroup == cp.CellGroup))
                                .ToList());
                        else if (unit is Cell cell)
                            list.AddRange(cell.Junctions.Where(jnc =>
                                gap && jnc is GapJunction ||
                                (chemout && jnc is ChemicalSynapse syn && syn.PreNeuron == cell) ||
                                (chemin && jnc is ChemicalSynapse syn2 && syn2.PostCell == cell))
                                .ToList());
                    }
                }
                else
                {
                    if (gap && chem)
                        list = runningModel.GetChemicalJunctions().Concat(runningModel.GetGapJunctions()).Cast<JunctionBase>().ToList();
                    else if (gap)
                        list = runningModel.GetGapJunctions().Cast<JunctionBase>().ToList();
                    else
                        list = runningModel.GetChemicalJunctions().Cast<JunctionBase>().ToList();
                }
                foreach (JunctionBase jnc in list)
                {
                    sw.WriteLine(CSVUtil.WriteCSVLine(jnc.ExportValues()));
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static bool ReadGapChemJunctionsFromCSV(string filename, ModelBase model, List<ModelUnitBase> selectedUnits, bool gap, bool chemin, bool chemout)
        {
            if (filename == null || model == null)
                return false;
            try
            {
                if (model is not RunningModel runningModel) return false;
                string[] contents = FileUtil.ReadLinesFromFile(filename);
                if (contents.Length <= 1) return false;
                string columns = contents[0];
                int iter = 1;
                if (columns != string.Join(",", InterPoolBase.ColumnNames))
                    return false;
                if (selectedUnits?.Count > 0)
                {
                    foreach (ModelUnitBase unit in selectedUnits)
                    {
                        CellPool cellPool = unit as CellPool;
                        Cell cell = unit as Cell;
                        if (unit is JunctionBase jnc)
                            runningModel.RemoveJunction(jnc);
                        else
                            runningModel.RemoveJunctionsOf(cellPool, cell, gap, chemin, chemout);
                        while (iter < contents.Length)
                        {
                            string line = contents[iter++];
                            string[] csvCells = line.Split(',');
                            if (csvCells.Length <= 0) continue;
                            JunctionBase jb = null;
                            if (csvCells[2] == JunctionType.Gap.ToString())//1st and 2nd cells are for Source and Target
                            {
                                if (gap)
                                    jb = new GapJunction();
                            }
                            else
                            {
                                if (chemin || chemout)
                                    jb = new ChemicalSynapse();
                            }
                            if (jb == null) continue;
                            jb.ImportValues([.. line.Split(",")]);
                            //check whether it is part of what needs to be imported
                            bool jncCheck = jb is GapJunction gapjnc &&
                                    (cellPool != null && (cellPool.Cells.FirstOrDefault(c => c.ID == jb.Source) != null || cellPool.Cells.FirstOrDefault(c => c.ID == jb.Target) != null) ||
                                     cell != null && (jb.Source == cell.ID || jb.Target == cell.ID) ||
                                     cellPool == null && cell == null);
                            if (!jncCheck && jb is ChemicalSynapse syn)
                            {
                                jncCheck = chemout &&
                                   (cellPool != null && cellPool.Cells.FirstOrDefault(c => c.ID == jb.Source) != null ||
                                     cell != null && jb.Source == cell.ID ||
                                     cellPool == null && cell == null);
                                if (!jncCheck)
                                    jncCheck = chemin &&
                                       (cellPool != null && cellPool.Cells.FirstOrDefault(c => c.ID == jb.Target) != null ||
                                         cell != null && jb.Target == cell.ID ||
                                         cellPool == null && cell == null);
                            }
                            if (jncCheck)
                                jb.LinkObjects(runningModel);
                        }
                        iter = 1;
                    }
                }
                else
                {
                    runningModel.RemoveJunctionsOf(null, null, gap, chemin, chemout);
                    while (iter < contents.Length)
                    {
                        string line = contents[iter++];
                        string[] csvCells = line.Split(',');
                        if (csvCells.Length <= 0) continue;
                        JunctionBase jb = null;
                        if (csvCells[2] == JunctionType.Gap.ToString())//1st and 2nd cells are for Source and Target
                        {
                            if (gap)
                                jb = new GapJunction();
                        }
                        else
                        {
                            if (chemin || chemout)
                                jb = new ChemicalSynapse();
                        }
                        if (jb == null) continue;
                        jb.ImportValues([.. line.Split(",")]);
                        //check whether it is part of what needs to be imported
                        bool jncCheck = gap && jb is GapJunction gapjnc ||
                            (chemin || chemout) && jb is ChemicalSynapse syn;
                        if (jncCheck)
                            jb.LinkObjects(runningModel);
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
        #endregion
        #endregion

        #region Excel Functions
        public static bool CreateCellPoolsWorkSheet(ModelBase model, ExcelWorkbook workbook, List<string> errorList)
        {
            try
            {
                List<CellPoolTemplate> cellPools = model is ModelTemplate modelTemplate ? modelTemplate.GetCellPools() :
                    model is RunningModel modelRunning ? modelRunning.GetCellPools() : [];
                return ExcelUtil.CreateWorkSheet(workbook, "Cell Pools", CellPoolTemplate.ColumnNames, cellPools.Cast<IDataExporterImporter>().ToList(), errorList);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static bool ReadCellPoolsFromWorksheet(ExcelWorkbook workbook, ModelBase model)
        {
            try
            {
                ExcelWorksheet worksheet = workbook.Worksheets["Cell Pools"];
                if (worksheet == null) return false;
                List<string> contents = ExcelUtil.ReadXLCellsFromLine(worksheet, 1);
                if (!contents.Equivalent(CellPoolTemplate.ColumnNames))
                    return false;
                int colCount = contents.Count;
                int rowInd = 2;
                if (model is ModelTemplate modelTemplate)
                {
                    modelTemplate.CellPoolTemplates.Clear();
                    while (true)
                    {
                        contents = ExcelUtil.ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                        if (contents.IsEmpty())
                            break;
                        CellPoolTemplate cpt = new();
                        cpt.ImportValues(contents);
                        modelTemplate.AddCellPool(cpt);
                    }
                }
                else if (model is RunningModel modelRun)
                {
                    modelRun.CellPools.Clear();
                    while (true)
                    {
                        contents = ExcelUtil.ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                        if (contents.IsEmpty())
                            break;
                        CellPool cp = new();
                        cp.ImportValues(contents);
                        modelRun.AddCellPool(cp);
                    }
                }
                model.LinkObjects();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static bool CreateCellsWorkSheet(ModelBase model, ExcelWorkbook workbook, List<string> errorList)
        {
            try
            {
                if (model is not RunningModel modelRunning) { return false; }
                return ExcelUtil.CreateWorkSheet(workbook, "Cells", Cell.ColumnNames, modelRunning.GetCells().Cast<IDataExporterImporter>().ToList(), errorList);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool ReadCellsFromWorksheet(ExcelWorkbook workbook, ModelBase model)
        {
            try
            {
                if (model is not RunningModel modelRun)
                    return false;
                ExcelWorksheet worksheet = workbook.Worksheets["Cells"];
                if (worksheet == null) return false;
                List<string> contents = ExcelUtil.ReadXLCellsFromLine(worksheet, 1);
                if (!contents.Equivalent(Cell.ColumnNames))
                    return false;
                int colCount = contents.Count;
                int rowInd = 2;
                modelRun.ClearCells();
                while (true)
                {
                    contents = ExcelUtil.ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                    if (contents.IsEmpty())
                        break;
                    string discriminator = contents[0];
                    Cell cell = Cell.CreateCell(discriminator);
                    cell.ImportValues(contents);
                    modelRun.AddCell(cell);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        private static bool CreateJunctionsWorkSheet(ModelBase model, ExcelWorkbook workbook, List<string> errorList)
        {
            try
            {
                if (model is RunningModel modelRunning)
                {
                    List<IDataExporterImporter> objList = modelRunning
                        .GetChemicalJunctions()
                        .Concat(modelRunning.GetGapJunctions())
                        .Cast<IDataExporterImporter>().ToList();
                    return ExcelUtil.CreateWorkSheet(workbook, "Junctions", InterPoolBase.ColumnNames, objList, errorList);
                }
                else if (model is ModelTemplate modelTemplate)
                {
                    List<IDataExporterImporter> objList = modelTemplate
                        .InterPoolTemplates
                        .Cast<IDataExporterImporter>().ToList();
                    return ExcelUtil.CreateWorkSheet(workbook, "Junctions", InterPoolTemplate.ColumnNames, objList, errorList);
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool ReadJunctionsFromWorksheet(ExcelWorkbook workbook, ModelBase model)
        {
            try
            {
                int sheetIndex = 0;
                string sheetName = "Junctions";
                while (true)
                {
                    ExcelWorksheet worksheet = workbook.Worksheets[sheetName];
                    if (worksheet == null) return sheetIndex > 0;
                    List<string> contents = ExcelUtil.ReadXLCellsFromLine(worksheet, 1);
                    int rowInd = 2;
                    if (model is ModelTemplate modelTemplate)
                    {
                        if (!contents.Equivalent(InterPoolTemplate.ColumnNames))
                            return false;
                        int colCount = contents.Count;
                        if (sheetIndex == 0)
                            modelTemplate.InterPoolTemplates.Clear();
                        while (true)
                        {
                            contents = ExcelUtil.ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                            if (contents.IsEmpty())
                                break;
                            InterPoolTemplate ipt = new();
                            ipt.ImportValues(contents);
                            modelTemplate.AddJunction(ipt);
                        }
                    }
                    else if (model is RunningModel modelRun)
                    {
                        if (!contents.Equivalent(InterPoolBase.ColumnNames))
                            return false;
                        int colCount = contents.Count;
                        if (sheetIndex == 0)
                            modelRun.RemoveJunctionsOf(null, null, true, true, true);
                        while (true)
                        {
                            contents = ExcelUtil.ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                            if (contents.IsEmpty())
                                break;
                            JunctionBase jb = null;
                            if (contents[0] == JunctionType.Gap.ToString())
                            {
                                jb = new GapJunction();
                            }
                            else
                            {
                                jb = new ChemicalSynapse();
                            }
                            if (jb == null) continue;
                            jb.ImportValues(contents);
                            jb.LinkObjects(modelRun);
                        }
                    }

                    sheetName = "JunctionsExt_" + ++sheetIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        private static bool CreateStimuliWorkSheet(ModelBase model, ExcelWorkbook workbook, List<string> errorList)
        {
            try
            {
                if (model is RunningModel modelRunning)
                {
                    List<IDataExporterImporter> objList = modelRunning
                        .GetStimuli().Select(stim => stim as Stimulus)
                        .Cast<IDataExporterImporter>().ToList();
                    return ExcelUtil.CreateWorkSheet(workbook, "Stimuli", Stimulus.ColumnNames, objList, errorList);
                }
                else if (model is ModelTemplate modelTemplate)
                {
                    List<IDataExporterImporter> objList = modelTemplate
                        .StimulusTemplates
                        .Cast<IDataExporterImporter>().ToList();
                    return ExcelUtil.CreateWorkSheet(workbook, "Stimuli", StimulusTemplate.ColumnNames, objList, errorList);
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool ReadStimuliFromWorksheet(ExcelWorkbook workbook, ModelBase model)
        {
            try
            {
                ExcelWorksheet worksheet = workbook.Worksheets["Stimuli"];
                if (worksheet == null) return false;
                List<string> contents = ExcelUtil.ReadXLCellsFromLine(worksheet, 1);
                int rowInd = 2;
                if (model is ModelTemplate modelTemplate)
                {
                    if (!contents.Equivalent(StimulusTemplate.ColumnNames))
                        return false;
                    int colCount = contents.Count;
                    modelTemplate.ClearStimuli();
                    while (true)
                    {
                        contents = ExcelUtil.ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                        if (contents.IsEmpty())
                            break;
                        StimulusTemplate stim = new();
                        stim.ImportValues(contents);
                        modelTemplate.StimulusTemplates.Add(stim);
                    }
                }
                else if (model is RunningModel modelRun)
                {
                    if (!contents.Equivalent(Stimulus.ColumnNames))
                        return false;
                    int colCount = contents.Count;
                    modelRun.ClearStimuli();
                    while (true)
                    {
                        contents = ExcelUtil.ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                        if (contents.IsEmpty())
                            break;
                        Stimulus stim = new();
                        stim.ImportValues(contents);
                        stim.LinkObjects(modelRun);
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
        public static void SaveToExcel(string fileName, ModelBase model, List<string> errorList)
        {
            try
            {
                errorList ??= [];
                model.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                using ExcelPackage package = ExcelUtil.CreateWorkBook(fileName);
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add("Model");
                int rowindex = 1;
                workSheet.Cells[rowindex, 1].Value = "Model";
                workSheet.Cells[rowindex++, 2].Value = model is RunningModel ? "Running Model" : "Model Template";
                workSheet.Cells[rowindex, 1].Value = "Version";
                workSheet.Cells[rowindex++, 2].Value = model.Version;
                workSheet.Cells[rowindex, 1].Value = "Name";
                workSheet.Cells[rowindex++, 2].Value = model.ModelName;
                workSheet.Cells[rowindex, 1].Value = "Description";
                workSheet.Cells[rowindex++, 2].Value = model.ModelDescription;

                workSheet = package.Workbook.Worksheets.Add("Model Dimensions");
                rowindex = 1;
                foreach (var prop in model.ModelDimensions.GetType().GetProperties())
                {
                    workSheet.Cells[rowindex, 1].Value = prop.Name;
                    workSheet.Cells[rowindex++, 2].Value = prop.GetValue(model.ModelDimensions);
                }

                workSheet = package.Workbook.Worksheets.Add("Model Settings");
                rowindex = 1;
                foreach (var prop in model.Settings.GetType().GetProperties())
                {
                    workSheet.Cells[rowindex, 1].Value = prop.Name;
                    workSheet.Cells[rowindex++, 2].Value = prop.GetValue(model.Settings);
                }

                workSheet = package.Workbook.Worksheets.Add("Kinem Params");
                rowindex = 1;
                foreach (var prop in model.Settings.GetType().GetProperties())
                {
                    workSheet.Cells[rowindex, 1].Value = prop.Name;
                    workSheet.Cells[rowindex++, 2].Value = prop.GetValue(model.Settings);
                }

                if (model.Parameters != null && model.Parameters.Count != 0)
                {
                    workSheet = package.Workbook.Worksheets.Add("Parameters");
                    rowindex = 1;
                    foreach (var prop in model.Parameters)
                    {
                        workSheet.Cells[rowindex, 1].Value = prop.Key;
                        workSheet.Cells[rowindex++, 2].Value = prop.Value;
                    }
                }
                if (!CreateCellPoolsWorkSheet(model, package.Workbook, errorList)) return;
                if (!CreateCellsWorkSheet(model, package.Workbook, errorList)) return;
                if (!CreateJunctionsWorkSheet(model, package.Workbook, errorList)) return;
                if (!CreateStimuliWorkSheet(model, package.Workbook, errorList)) return;
                package.Save();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static bool ReadPropertiesFromSheet(ExcelWorkbook workbook, string sheetName, object obj)
        {
            try
            {
                ExcelWorksheet workSheet = workbook.Worksheets[sheetName];
                if (workSheet == null)
                    return false;
                int rowindex = 1;
                while (true)
                {
                    string prop = workSheet.Cells[rowindex, 1].Value?.ToString();
                    if (string.IsNullOrEmpty(prop))
                        break;
                    obj.SetPropertyValue(prop, workSheet.Cells[rowindex, 2].Value);
                    rowindex++;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static ModelBase ReadFromExcel(string fileName)
        {
            using ExcelPackage package = new(fileName);
            if (package.Workbook == null ||
                !package.Workbook.Worksheets.Any(ws => ws.Name == "Model"))
                return null;
            ExcelWorksheet workSheet = package.Workbook.Worksheets["Model"];
            int rowindex = 1;
            if (workSheet.Cells[rowindex, 1].Value.ToString() != "Model")
                return null;
            string modelMode = workSheet.Cells[rowindex++, 2].Value.ToString();
            ModelBase model = null;
            if (modelMode == "Running Model")
                model = new RunningModel();
            else if (modelMode == "Model Template")
                model = new ModelTemplate();
            else return null;
            model.Version = workSheet.Cells[rowindex++, 2].Value.ToString();
            model.ModelName = workSheet.Cells[rowindex++, 2].Value.ToString();
            model.ModelDescription = workSheet.Cells[rowindex++, 2].Value.ToString();

            ReadPropertiesFromSheet(package.Workbook, "Model Dimensions", model.ModelDimensions);
            ReadPropertiesFromSheet(package.Workbook, "Model Settings", model.Settings);
            ReadPropertiesFromSheet(package.Workbook, "Kinem Params", model.KinemParam);
            ReadPropertiesFromSheet(package.Workbook, "Parameters", model.Parameters);

            ReadCellPoolsFromWorksheet(package.Workbook, model);
            ReadCellsFromWorksheet(package.Workbook, model);
            ReadJunctionsFromWorksheet(package.Workbook, model);
            ReadStimuliFromWorksheet(package.Workbook, model);
            model.LinkObjects();
            return model;
        }

        #endregion
    }
}
