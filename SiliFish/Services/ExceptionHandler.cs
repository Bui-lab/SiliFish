using System;
using System.Diagnostics;
using System.IO;

namespace SiliFish.Services
{
    public static class ExceptionHandler
    {
        private static string logFile;
        public static void CompleteLogging()
        {
            if (logFile == null) return;
            string newFileName = logFile.Replace(".log", $"_{DateTime.Now:yyMMdd_HHmm}.log");
            File.Copy(logFile, newFileName);
        }

        private static void LogException(string name, Exception ex)
        {
            if (string.IsNullOrEmpty(logFile))
            {
                string logpath = Path.GetTempPath() + "SiliFish";
                if (!Directory.Exists(logpath))
                {
                    try
                    {
                        Directory.CreateDirectory(logpath);
                    }
                    catch { logpath = ""; }
                }
                logFile = $"{logpath}\\SiliFish.log";
                if (!File.Exists(logFile))
                    File.Create(logFile);
                else
                    File.WriteAllText(logFile, string.Empty);
            }
            string stackTrace = "";
            try
            {
                stackTrace = (new StackTrace(ex, true)).ToString();
            }
            catch { }
            string logMsg = $"{DateTime.Now:g}:{name}/{ex.Message}\r\n{stackTrace}";
            try
            {
                File.AppendAllText(logFile, logMsg);
            }
            catch { }
        }

        /// <summary>
        ///ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex); 
        /// </summary>
        public static void ExceptionHandling(string name, Exception ex)
        {
            LogException(name, ex);
        }
    }
}

//Exception exception = new NotImplementedException();
//ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
//throw exception;
