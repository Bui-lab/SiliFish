using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services
{
    public static class ExceptionHandler
    {
        private static string logFile;
        public static void CompleteLogging()
        {
            if (logFile == null) return;
            string newFileName = logFile.Replace(".log", $"{DateTime.Now:s}.log");
            File.Move(logFile, newFileName);
        }

        private static void LogException(string name, Exception ex)
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
                File.Create(logFile) ;
            string logMsg = $"{DateTime.Now:g}:{name}/{ex.Message}\r\n";
            File.AppendAllText(logFile, logMsg);
        }

        public static void ExceptionHandling(string name, Exception ex)
        {
            LogException(name, ex);
        }

        //ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
    }
}
