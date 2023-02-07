using SiliFish.Definitions;
using System.Configuration;

namespace SiliFish.UI
{
    internal static class Program
    {
        /// <summary>
        /// The pointer to the first form is kept to prevent accidental program exit
        /// </summary>
        public static MainForm MainForm { get; set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            MainForm = new MainForm();
            GlobalSettingsProperties gs = new();
            gs = GlobalSettingsProperties.Load();//reload from global.settings
            Application.Run(MainForm);
            foreach (string f in GlobalSettings.TempFiles)
            {
                File.Delete(f);
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine((e.ExceptionObject as Exception).Message);
        }

        
    }
}