using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SiliFish.Helpers
{
    public class FileUtil
    {
        //https://stackoverflow.com/questions/12744725/how-do-i-perform-file-readalllines-on-a-file-that-is-also-open-in-excel
        private static string[] ReadAllLines(string path)
        {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(file);
            List<string> lines = new();
            while (!sr.EndOfStream)
            {
                lines.Add(sr.ReadLine());
            }
            return lines.ToArray();
        }
        public static string ReadFromFile(string path)
        {
            return File.ReadAllText(path);
        }
        public static string[] ReadLinesFromFile(string path)
        {
            return ReadAllLines(path);
        }

        public static void SaveToFile(string path, string content)
        {
            path = string.Join("_", path.Split(Path.GetInvalidPathChars()));
            path = string.Join("_", path.Split(Path.GetInvalidFileNameChars()));
            File.WriteAllText(path, content);
        }

        public static string SaveToTempFolder(string filename, string content)
        {
            filename = string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
            filename = string.Concat("~", filename.AsSpan(1));
            if (!Directory.Exists(GlobalSettings.TempFolder))
            {
                try
                {
                    Directory.CreateDirectory(GlobalSettings.TempFolder);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw;
                }
            }
            string path = $"{GlobalSettings.TempFolder}\\{filename}";
            GlobalSettings.TempFiles.Add(path);
            File.WriteAllText(path, content);
            return path;
        }



    }
}
