using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.UI.Services
{
    public static class Warner
    {
        public static void LargeFileWarning(string filename) 
        {
            double limit = GlobalSettings.FileSizeWarningLimit;
            string message = $"The size of the file to be displayed is larger than {limit:0.##} MB. It is saved as {filename}.\r\n" +
                $"(You can set this limit through the 'Settings' button above) ";
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK);
            Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", Path.GetDirectoryName(filename));
        }
    }
}
