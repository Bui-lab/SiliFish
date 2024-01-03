using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiliFish.UI.Services
{
    public static class TextDisplayer
    {
        public static void Display(string caption, List<string> text, SaveFileDialog saveFileDialog = null)
        {
            Display(caption, string.Join("\r\n", text), saveFileDialog);
        }
        public static void Display(string caption, string text, SaveFileDialog saveFileDialog = null)
        {
            ControlContainer controlContainer = new()
            {
                Text = caption
            };
            RichTextBox richTextBox = new()
            {
                Text = text
            };
            controlContainer.AddControl(richTextBox, null);
            if (saveFileDialog != null)
            {
                if (controlContainer.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileUtil.SaveToFile(saveFileDialog.FileName, richTextBox.Text);
                    }
                }
            }
            else
            {
                controlContainer.SaveVisible = false;
                controlContainer.Show();
            }
        }
    }
}
