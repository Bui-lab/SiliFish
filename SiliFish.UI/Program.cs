using OfficeOpenXml;
using SiliFish.Definitions;
using SiliFish.Services;
using System.Reflection;

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
            try
            {
                MainForm = new MainForm();
                GlobalSettingsProperties gs = new();
                gs = GlobalSettingsProperties.Load();//reload from global.settings

                // Update the line causing the error by creating an instance of EPPlusLicense and calling the method on it.
                EPPlusLicense license = new();
                license.SetNonCommercialPersonal("Emine Topcu @ Bui Lab - uOttawa");

                Application.Run(MainForm);
                foreach (string f in GlobalSettings.TempFiles)
                {
                    try { File.Delete(f); }
                    catch { }
                }
                ExceptionHandler.CompleteLogging();
            }
            catch(Exception exc) 
            {
                MessageBox.Show(exc.Message + exc.StackTrace);
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ExceptionHandler.ExceptionHandling("Uncaught exception", e.Exception);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandler.ExceptionHandling("Unhandled exception", e.ExceptionObject as Exception);
        }

        
    }
}