using GeneticSharp;
using OfficeOpenXml;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static OfficeOpenXml.ExcelErrorValue;

namespace SiliFish.Repositories
{
    public static class ModelFile
    {
        #region JSON related functions
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
                json = json.Replace("\"taud\"", "\"TauD\"");
                json = json.Replace("\"vth\"", "\"Vth\"");
                json = json.Replace("\"E_rev\"", "\"Erev\"");
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
                json = JsonUtil.CleanUp(json);
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

        public static void SaveToFile(RunningModel model, string filename)
        {
            if (!model.ModelRun)
            {
                return;
            }
            bool jncTracking = model.JunctionCurrentTrackingOn;
            List<string> cell_names = new();
            cell_names.AddRange(model.NeuronPools.OrderBy(np => np.CellGroup).Select(np => np.CellGroup).Distinct());
            cell_names.AddRange(model.MusclePools.Select(k => k.CellGroup).Distinct());

            Dictionary<string, double[]> Vdata_list = new();
            Dictionary<string, double[]> Gapdata_list = new();
            Dictionary<string, double[]> Syndata_list = new();
            foreach (CellPool pool in model.NeuronPools.Union(model.MusclePools).OrderBy(p => p.PositionLeftRight).ThenBy(p => p.CellGroup))
            {
                //pool.GetCells().Select(c => Vdata_list.TryAdd(c.ID, c.V));
                foreach (Cell c in pool.GetCells())
                {
                    Vdata_list.Add(c.ID, c.V);
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
                    string.Join(',', Values.Select(item => item.Value[t].ToString(GlobalSettings.PlotDataFormat)));
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
                    sw.WriteLine(string.Join(",", StimulusTemplate.ColumnNames));
                    foreach (StimulusTemplate stim in stimulusTemplates)
                    {
                        sw.WriteLine(CSVUtil.GetCSVLine(stim.ExportValues()));
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
                    sw.WriteLine(string.Join(",", Stimulus.ColumnNames));
                    foreach (Stimulus stim in stimuli)
                    {
                        sw.WriteLine(CSVUtil.GetCSVLine(stim.ExportValues()));
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
                        stim.ImportValues(contents[iter++].Split(",").ToList());
                        mt.AppliedStimuli.Add(stim);
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
                else if (SelectedUnit is CellPool cellPool)
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
                else if (SelectedUnit is CellPoolTemplate cellPoolTemp)
                {
                    ModelTemplate mt = model as ModelTemplate;
                    if (columns != string.Join(",", StimulusTemplate.ColumnNames))
                        return false;
                    mt.ClearStimuli(cellPoolTemp.CellGroup);
                    while (iter < contents.Length)
                    {
                        StimulusTemplate stim = new();
                        stim.ImportValues(contents[iter++].Split(",").ToList());
                        if (stim.TargetPool == cellPoolTemp.CellGroup)
                            mt.AppliedStimuli.Add(stim);
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

        public static bool SaveCellsToCSV(string filename, RunningModel model, CellPool cellPool)
        {
            if (filename == null || model == null)
                return false;

            try
            {
                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                List<Cell> cells =
                        cellPool?.GetCells().ToList() ??
                        model.GetCells();
                sw.WriteLine(string.Join(",", Cell.ColumnNames));
                foreach (Cell cell in cells)
                {
                    sw.WriteLine(CSVUtil.GetCSVLine(cell.ExportValues()));
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static bool ReadCellsFromCSV(string filename, RunningModel model)
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
                model.ClearCells();
                while (iter < contents.Length)
                {
                    Cell cell = Cell.GenerateFromCSVRow(contents[iter++]);
                    model.AddCell(cell);
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
        public static bool ReadCellsFromCSV(string filename, RunningModel model, CellPool cellPool)
        {
            if (filename == null || model == null)
                return false;
            if (cellPool is null)
                return ReadCellsFromCSV(filename, model);
            try
            {
                string[] contents = FileUtil.ReadLinesFromFile(filename);
                if (contents.Length <= 1) return false;
                string columns = contents[0];
                int iter = 1;
                if (columns != string.Join(",", Cell.ColumnNames))
                    return false;
                cellPool.Cells.Clear();
                while (iter < contents.Length)
                {
                    Cell cell = Cell.GenerateFromCSVRow(contents[iter++]);
                    if (cell.CellGroup == cellPool.ID)
                        cellPool.AddCell(cell);
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

        public static bool SaveCellPoolsToCSV(string filename, ModelBase model)
        {
            if (filename == null || model == null)
                return false;

            try
            {
                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                List<CellPoolTemplate> cellPools = model is ModelTemplate modelTemplate ? modelTemplate.GetCellPools() :
                        model is RunningModel modelRunning ? modelRunning.GetCellPools() : new();
                sw.WriteLine(string.Join(",", CellPoolTemplate.ColumnNames));
                foreach (CellPoolTemplate cpt in cellPools)
                {
                    sw.WriteLine(CSVUtil.GetCSVLine(cpt.ExportValues()));
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool ReadCellPoolsFromCSV(string filename, ModelBase model)
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
                    modelTemplate.CellPoolTemplates.Clear();
                    while (iter < contents.Length)
                    {
                        CellPoolTemplate cpt = new();
                        cpt.ImportValues(contents[iter++].Split(",").ToList());
                        modelTemplate.AddCellPool(cpt);
                    }
                }
                else if (model is RunningModel modelRun)
                {
                    modelRun.CellPools.Clear();
                    while (iter < contents.Length)
                    {
                        CellPool cp = new();
                        cp.ImportValues(contents[iter++].Split(",").ToList());
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
        public static bool SaveConnectionsToCSV(string filename, ModelBase model, ModelUnitBase selectedUnit, bool gap, bool chemin, bool chemout)
        {
            if (model is ModelTemplate)
                return SaveInterPoolTemplatesToCSV(filename, model, selectedUnit, gap, chemin, chemout);
            else if (model is RunningModel)
                return SaveJunctionsToCSV(filename, model, selectedUnit, gap, chemin, chemout);
            return false;
        }

        public static bool ReadConnectionsFromCSV(string filename, ModelBase model, ModelUnitBase selectedUnit, bool gap, bool chemin, bool chemout)
        {
            if (model is ModelTemplate)
                return ReadInterPoolTemplatesFromCSV(filename, model, selectedUnit, gap, chemin, chemout);
            else if (model is RunningModel)
                return ReadJunctionsFromCSV(filename, model, selectedUnit, gap, chemin, chemout);
            return false;
        }
        private static bool SaveInterPoolTemplatesToCSV(string filename, ModelBase model, ModelUnitBase selectedUnit, bool gap, bool chemin, bool chemout)
        {
            if (filename == null || model == null)
                return false;

            try
            {
                if (model is not ModelTemplate modelTemplate) return false;
                using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                sw.WriteLine(string.Join(",", InterPoolTemplate.ColumnNames));
                List<InterPoolTemplate> list;
                if (selectedUnit is CellPoolTemplate cpt)
                    list = modelTemplate.InterPoolTemplates.Where(jnc =>
                        (gap && jnc.ConnectionType == Definitions.ConnectionType.Gap && (jnc.PoolSource == cpt.CellGroup || jnc.PoolTarget == cpt.CellGroup)) ||
                        (chemout && jnc.ConnectionType != Definitions.ConnectionType.Gap && jnc.PoolSource == cpt.CellGroup) ||
                        (chemin && jnc.ConnectionType != Definitions.ConnectionType.Gap && jnc.PoolTarget == cpt.CellGroup))
                        .ToList();
                else
                {
                    list = modelTemplate.InterPoolTemplates.Where(ipt =>
                        gap && ipt.ConnectionType == Definitions.ConnectionType.Gap ||
                        (chemout || chemin) && (ipt.ConnectionType == Definitions.ConnectionType.Synapse || ipt.ConnectionType == Definitions.ConnectionType.NMJ))
                        .ToList();
                }
                foreach (InterPoolTemplate ipt in list)
                {
                    sw.WriteLine(CSVUtil.GetCSVLine(ipt.ExportValues()));
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static bool ReadInterPoolTemplatesFromCSV(string filename, ModelBase model, ModelUnitBase selectedUnit, bool gap, bool chemin, bool chemout)
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
                CellPoolTemplate cpt = selectedUnit as CellPoolTemplate;
                modelTemplate.RemoveJunctionsOf(cpt, gap, chemin, chemout);
                while (iter < contents.Length)
                {
                    InterPoolTemplate ipt = new();
                    ipt.ImportValues(contents[iter++].Split(",").ToList());
                    //check whether it is part of what 
                    bool jncCheck = gap && ipt.ConnectionType == ConnectionType.Gap &&
                            (cpt == null || ipt.PoolSource == cpt.CellGroup || ipt.PoolTarget == cpt.CellGroup);
                    if (!jncCheck)
                        jncCheck = chemout && ipt.ConnectionType != ConnectionType.Gap &&
                            (cpt == null || ipt.PoolSource == cpt.CellGroup);
                    if (!jncCheck)
                        jncCheck = chemin && ipt.ConnectionType != ConnectionType.Gap &&
                            (cpt == null || ipt.PoolTarget == cpt.CellGroup);
                    if (jncCheck)
                        modelTemplate.AddJunction(ipt);
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

        private static bool SaveJunctionsToCSV(string filename, ModelBase model, ModelUnitBase selectedUnit, bool gap, bool chemin, bool chemout)
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
                sw.WriteLine(string.Join(",", JunctionBase.ColumnNames));
                List<JunctionBase> list;

                if (selectedUnit is CellPool cp)
                    list = cp.Projections.Where(jnc =>
                        gap && jnc is GapJunction ||
                        (chemout && jnc is ChemicalSynapse syn && syn.PreNeuron.CellGroup == cp.CellGroup) ||
                        (chemin && jnc is ChemicalSynapse syn2 && syn2.PostCell.CellGroup == cp.CellGroup))
                        .ToList();
                else if (selectedUnit is Cell cell)
                    list = cell.Projections.Where(jnc =>
                        gap && jnc is GapJunction ||
                        (chemout && jnc is ChemicalSynapse syn && syn.PreNeuron.CellGroup == cell.CellGroup) ||
                        (chemin && jnc is ChemicalSynapse syn2 && syn2.PostCell.CellGroup == cell.CellGroup))
                        .ToList();
                else
                {
                    if (gap && chem)
                        list = runningModel.GetChemicalProjections().Concat(runningModel.GetGapProjections()).ToList();
                    else if (gap)
                        list = runningModel.GetGapProjections().ToList();
                    else
                        list = runningModel.GetChemicalProjections().ToList();
                }
                foreach (JunctionBase jnc in list)
                {
                    sw.WriteLine(CSVUtil.GetCSVLine(jnc.ExportValues()));
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static bool ReadJunctionsFromCSV(string filename, ModelBase model, ModelUnitBase selectedUnit, bool gap, bool chemin, bool chemout)
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
                if (columns != string.Join(",", JunctionBase.ColumnNames))
                    return false;
                CellPool cellPool = selectedUnit as CellPool;
                Cell cell = selectedUnit as Cell;
                runningModel.RemoveJunctionsOf(cellPool, cell, gap, chemin, chemout);
                while (iter < contents.Length)
                {
                    string line = contents[iter++];
                    string[] csvCells = line.Split(',');
                    if (csvCells.Length <= 0) continue;
                    JunctionBase jb = null;
                    if (csvCells[0] == ConnectionType.Gap.ToString())
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
                    jb.ImportValues(line.Split(",").ToList());
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
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        #endregion

        #region Excel Functions
        public static bool CreateCellPoolsWorkSheet(ModelBase model, ExcelWorkbook workbook)
        {
            try
            {
                List<CellPoolTemplate> cellPools = model is ModelTemplate modelTemplate ? modelTemplate.GetCellPools() :
                    model is RunningModel modelRunning ? modelRunning.GetCellPools() : new();
                CreateWorkSheet(workbook, "Cell Pools", CellPoolTemplate.ColumnNames, cellPools.Cast<IDataExporterImporter>().ToList());
                return true;
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
                List<string> contents = ReadXLCellsFromLine(worksheet, 1);
                if (!contents.Equivalent(CellPoolTemplate.ColumnNames))
                    return false;
                int colCount = contents.Count;
                int rowInd = 2;
                if (model is ModelTemplate modelTemplate)
                {
                    modelTemplate.CellPoolTemplates.Clear();
                    while (true)
                    {
                        contents = ReadXLCellsFromLine(worksheet, rowInd++, colCount);
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
                        contents = ReadXLCellsFromLine(worksheet, rowInd++, colCount);
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
        public static bool CreateCellsWorkSheet(ModelBase model, ExcelWorkbook workbook)
        {
            try
            {
                if (model is not RunningModel modelRunning) { return false; }
                CreateWorkSheet(workbook, "Cells", Cell.ColumnNames, modelRunning.GetCells().Cast<IDataExporterImporter>().ToList());
                return true;
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
                List<string> contents = ReadXLCellsFromLine(worksheet, 1);
                if (!contents.Equivalent(Cell.ColumnNames))
                    return false;
                int colCount = contents.Count;
                int rowInd = 2;
                modelRun.ClearCells();
                while (true)
                {
                    contents = ReadXLCellsFromLine(worksheet, rowInd++, colCount);
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
        private static bool CreateJunctionsWorkSheet(ModelBase model, ExcelWorkbook workbook)
        {
            try
            {
                if (model is RunningModel modelRunning)
                {
                    List<IDataExporterImporter> objList = modelRunning
                        .GetChemicalProjections()
                        .Concat(modelRunning.GetGapProjections())
                        .Cast<IDataExporterImporter>().ToList();
                    CreateWorkSheet(workbook, "Junctions", JunctionBase.ColumnNames, objList);
                    return true;
                }
                else if (model is ModelTemplate modelTemplate)
                {
                    List<IDataExporterImporter> objList = modelTemplate
                        .InterPoolTemplates
                        .Cast<IDataExporterImporter>().ToList();
                    CreateWorkSheet(workbook, "Junctions", InterPoolTemplate.ColumnNames, objList);
                    return true;
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
                    List<string> contents = ReadXLCellsFromLine(worksheet, 1);
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
                            contents = ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                            if (contents.IsEmpty())
                                break;
                            InterPoolTemplate ipt = new();
                            ipt.ImportValues(contents);
                            modelTemplate.AddJunction(ipt);
                        }
                    }
                    else if (model is RunningModel modelRun)
                    {
                        if (!contents.Equivalent(JunctionBase.ColumnNames))
                            return false;
                        int colCount = contents.Count;
                        if (sheetIndex == 0)
                            modelRun.RemoveJunctionsOf(null, null, true, true, true);
                        while (true)
                        {
                            contents = ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                            if (contents.IsEmpty())
                                break;
                            JunctionBase jb = null;
                            if (contents[0] == ConnectionType.Gap.ToString())
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
        private static bool CreateStimuliWorkSheet(ModelBase model, ExcelWorkbook workbook)
        {
            try
            {
                if (model is RunningModel modelRunning)
                {
                    List<IDataExporterImporter> objList = modelRunning
                        .GetStimuli().Select(stim => stim as Stimulus)
                        .Cast<IDataExporterImporter>().ToList();
                    CreateWorkSheet(workbook, "Stimuli", Stimulus.ColumnNames, objList);
                    return true;
                }
                else if (model is ModelTemplate modelTemplate)
                {
                    List<IDataExporterImporter> objList = modelTemplate
                        .AppliedStimuli
                        .Cast<IDataExporterImporter>().ToList();
                    CreateWorkSheet(workbook, "Stimuli", StimulusTemplate.ColumnNames, objList);
                    return true;
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
                List<string> contents = ReadXLCellsFromLine(worksheet, 1);
                int rowInd = 2;
                if (model is ModelTemplate modelTemplate)
                {
                    if (!contents.Equivalent(StimulusTemplate.ColumnNames))
                        return false;
                    int colCount = contents.Count;
                    modelTemplate.ClearStimuli();
                    while (true)
                    {
                        contents = ReadXLCellsFromLine(worksheet, rowInd++, colCount);
                        if (contents.IsEmpty())
                            break;
                        StimulusTemplate stim = new();
                        stim.ImportValues(contents);
                        modelTemplate.AppliedStimuli.Add(stim);
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
                        contents = ReadXLCellsFromLine(worksheet, rowInd++, colCount);
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
        private static bool CreateWorkSheet(ExcelWorkbook workbook, string sheetName, List<string> columnNames, List<IDataExporterImporter> objList)
        {
            try
            {
                ExcelWorksheet workSheet = workbook.Worksheets.Add(sheetName);
                int rowindex = 1;
                int colindex = 1;
                foreach (string s in columnNames)
                    workSheet.Cells[rowindex, colindex++].Value = s;
                foreach (IDataExporterImporter jnc in objList)
                {
                    colindex = 1;
                    rowindex++;
                    if (rowindex > 1048576) //max number of rows excel allows
                    {
                        Regex regex = new("(.*)Ext_(.*)");
                        Match parMatch = regex.Match(sheetName);
                        if (!parMatch.Success)
                            sheetName += "Ext_1";
                        else
                        {
                            int extension = 0;
                            if (int.TryParse(parMatch.Groups[2].Value, out extension))
                                extension++;
                            sheetName = parMatch.Groups[1].Value + "Ext_" + extension;
                        }
                        return CreateWorkSheet(workbook, sheetName, columnNames, objList.Skip(rowindex - 1).ToList());
                    }
                    foreach (string s in jnc.ExportValues())
                        workSheet.Cells[rowindex, colindex++].Value = s;
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static void SaveToExcel(string fileName, ModelBase model)
        {
            model.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (File.Exists(fileName))
                File.Delete(fileName);
            using ExcelPackage package = new(fileName);
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

            if (model.Parameters != null && model.Parameters.Any())
            {
                workSheet = package.Workbook.Worksheets.Add("Parameters");
                rowindex = 1;
                foreach (var prop in model.Parameters)
                {
                    workSheet.Cells[rowindex, 1].Value = prop.Key;
                    workSheet.Cells[rowindex++, 2].Value = prop.Value;
                }
            }
            CreateCellPoolsWorkSheet(model, package.Workbook);
            CreateCellsWorkSheet(model, package.Workbook);
            CreateJunctionsWorkSheet(model, package.Workbook);
            CreateStimuliWorkSheet(model, package.Workbook);
            package.Save();
        }

        public static List<string> ReadXLCellsFromLine(ExcelWorksheet worksheet, int line, int maxCol = -1)
        {
            List<string> cells = new();
            int colInd = 1;
            while (true)
            {
                string s = worksheet.Cells[line, colInd].Value?.ToString();
                if (string.IsNullOrEmpty(s) && maxCol < 0) break;
                cells.Add(s);
                if (colInd == maxCol) break;
                colInd++;
            }
            return cells;
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
