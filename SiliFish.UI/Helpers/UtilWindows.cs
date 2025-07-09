using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.UI.Services;
using System.Diagnostics;

namespace SiliFish.UI
{
    public class UtilWindows
    {
        public static void DisplaySavedFile(string filename)
        {
            if (GlobalSettings.ShowFileFolderAfterSave)
            {
                FileInfo fileInfo = new(filename);
                double fileLength = fileInfo.Length / (1024 * 1024); // in MB
                if (fileLength > GlobalSettings.FileSizeWarningLimit)
                {
                    MessageBox.Show($"File {filename} is saved.", "Information");
                    Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", Path.GetDirectoryName(filename));
                }
                else
                    FileUtil.ShowFile(filename); 
            }
            else
                MessageBox.Show($"File {filename} is saved.", "Information");
        }
        public static void SaveImage(SaveFileDialog saveFileDialog, Image img)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                img.Save(saveFileDialog.FileName);
            }
        }

        public static void SaveTextFile(SaveFileDialog saveFileCSV, string text)
        {
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                bool saved = false;
                try
                {
                    FileUtil.SaveToFile(saveFileCSV.FileName, text);
                    saved = true;
                    string filename = saveFileCSV.FileName;
                    DisplaySavedFile(filename);
                }
                catch (Exception exc)
                {
                    if (!saved)
                    {
                        MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
                    }
                    else
                    {
                        MessageBox.Show($"File {saveFileCSV.FileName} is saved.", "Information");
                    }
                }
            }

        }

        public static void FillCells(ComboBox dd, CellPool pool, bool allCells = true)
        {
            dd.Items.Clear();
            if (pool.GetCells().Any())
            {
                if (allCells)
                    dd.Items.Add("All Cells");
                dd.Items.AddRange(pool.GetCells().ToArray());
                dd.SelectedIndex = 0;

            }
        }
    }
}
