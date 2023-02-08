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
        private static void LogException(string name, Exception ex)
        {
            string logFile = "SiliFish.log";
            if (!File.Exists(logFile))
                File.Create(logFile) ;
            string logMsg = $"{DateTime.Now:g}:{name}/{ex.Message}";
            File.AppendAllText(logFile, logMsg);
        }

        public static void ExceptionHandling(string name, Exception ex)
        {
            LogException(name, ex);
        }

        //ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
    }
}
