using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;

namespace SiliFish.UI
{
    public class UtilWindows
    {
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
                    if (GlobalSettings.ShowFileFolderAfterSave)
                        FileUtil.ShowFile(filename);
                    else
                        MessageBox.Show($"File {filename} is saved.", "Information");

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
