using SiliFish.DataTypes;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SiliFish.Helpers
{
    public class FileUtil
    {
        public static string ReadFromFile(string path)
        {
            return File.ReadAllText(path);
        }

        public static void SaveToFile(string path, string content)
        {
            File.WriteAllText(path, content);
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
        public static Dictionary<string, double[]> ReadFromCSV(string filename)
        {
            if (filename == null)
                return null;
            Dictionary<string, double[]> data = new();
            Dictionary<string, List<double>> tempdata = new();
            using StreamReader sr = new(filename);
            string line = sr.ReadLine();
            if (line == null) return null;
            string[] columns = line.Split(",");
            foreach (string column in columns)
                tempdata.Add(column, new List<double>());
            int counter = 0;
            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split(",");
                for (int i = 0; i < values.Length; i++)
                    tempdata[columns[i]].Add(double.Parse(values[i]));
                counter++;
                //if (counter > 10000) break;
            }
            foreach (string column in columns)
                data.Add(column, tempdata[column].ToArray());

            return data;
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

    }
}
