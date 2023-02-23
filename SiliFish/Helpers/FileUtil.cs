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
        public static string ReadFromFile(string path)
        {
            File.ReadAllLines(path);
            return File.ReadAllText(path);
        }
        public static string[] ReadLinesFromFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public static void SaveToFile(string path, string content)
        {
            path = string.Join("_", path.Split(Path.GetInvalidPathChars()));
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
