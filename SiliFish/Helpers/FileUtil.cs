﻿using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SiliFish.Helpers
{
    public class FileUtil
    {
        //https://stackoverflow.com/questions/12744725/how-do-i-perform-file-readalllines-on-a-file-that-is-also-open-in-excel
        private static string[] ReadAllLines(string path)
        {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(file);
            List<string> lines = [];
            while (!sr.EndOfStream)
            {
                lines.Add(sr.ReadLine());
            }
            return [.. lines];
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
            string filename = Path.GetFileName(path);
            string newfilename= string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
            if (!filename.Equals(newfilename))
                path = path.Replace(filename, newfilename);
            path = string.Join("_", path.Split(Path.GetInvalidPathChars()));

            File.WriteAllText(path, content);
        }

        public static void SaveToCSVFile(string filename, List<string> ColumnNames, List<List<string>> Values)
        {
            if (filename == null || ColumnNames == null || Values == null)
                return;

            using FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write);
            using StreamWriter sw = new(fs);
            sw.WriteLine(string.Join(',', ColumnNames));

            foreach (List<string> words in Values)
            {
                string row = string.Join(',', words.Select(w => Util.CSVEncode(w)).ToList());
                sw.WriteLine(row);
            }
        }
        public static string SaveToTempFolder(string filename, string content)
        {
            filename = string.Concat("~", filename.AsSpan(1));
            if (!Directory.Exists(GlobalSettings.TempFolder))
            {
                try
                {
                    Directory.CreateDirectory(GlobalSettings.TempFolder);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                    throw;
                }
            }
            string path = $"{GlobalSettings.TempFolder}\\{filename}";
            GlobalSettings.TempFiles.Add(path);
            File.WriteAllText(path, content);
            return path;
        }

        public static string AppendToFileName(string filename, string postfix)
        {
            string ext = Path.GetExtension(filename);
            string path=Path.GetDirectoryName(filename);    
            string prefix = Path.GetFileNameWithoutExtension(filename);
            filename = Path.Combine(path, prefix + postfix+ext);
            return filename;
        }
        private static string GetUniqueFileName(string filename, string path)
        {
            if (string.IsNullOrEmpty(filename))
            {
                filename = "Temp";
            }
            string ext = Path.GetExtension(filename);
            string prefix = Path.GetFileNameWithoutExtension(filename);
            filename = Path.Combine(path, prefix + ext);
            int suffix = 0;
            while (File.Exists(filename))
                filename = Path.Combine(path, prefix + (suffix++).ToString() + ext);
            return filename;
        }
        public static string SaveToOutputFolder(string filename, string content)
        {
            filename = GetUniqueFileName(filename, GlobalSettings.OutputFolder);
            SaveToFile(filename, content);
            return filename;
        }

        public static string CopyFileToOutputFolder(string tempFile, string target)
        {
            string ext = Path.GetExtension(tempFile);
            target = Path.ChangeExtension(target, ext);
            target = GetUniqueFileName(target, GlobalSettings.OutputFolder);
            File.Copy(tempFile, target);
            return target;
        }

        public static void ShowFile(string filename)
        {
            Process p = new()
            {
                StartInfo = new ProcessStartInfo(filename)
                {
                    UseShellExecute = true
                }
            };
            p.Start();

        }
        public static string GetUniqueFileName()
        {
            string filename = Guid.NewGuid().ToString();
            filename = string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
            return filename;
        }
    }
}
