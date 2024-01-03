using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using SiliFish.DataTypes;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.Extensions;
using SiliFish.Definitions;
using System.Windows.Forms;
using System.IO;

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
                    FileUtil.ShowFile(saveFileCSV.FileName);
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
