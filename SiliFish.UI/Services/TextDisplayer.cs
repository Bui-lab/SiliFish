using SiliFish.DataTypes;
using SiliFish.Helpers;

namespace SiliFish.UI.Services
{
    public static class TextDisplayer
    {
        public static void Display(string caption, List<Difference> differences, SaveFileDialog saveFileDialog = null)
        {
            int tab1 = differences.Where(d => !d.SingleString).Max(d => d.Item.Length);
            int tab2 = differences.Max(d => d.Parameter?.Length ?? 0);
            int tab3 = differences.Max(d => d.Value1?.Length ?? 0);
            int tab4 = differences.Max(d => d.Value2?.Length ?? 0);
            tab1 = Math.Max(300, 5 * tab1);
            tab2 = Math.Max(200, 4 * tab2) + tab1;
            tab3 = Math.Max(200, 4 * tab3) + tab2;
            tab4 = Math.Max(200, 4 * tab4) + tab3;

            string text = string.Empty;
            foreach (Difference difference in differences)
            {
                text += $"{difference.Item}\t{difference.Parameter}\t{difference.Value1}\t{difference.Value2}\r\n";
            }

            ControlContainer controlContainer = new()
            {
                Text = caption
            };
            RichTextBox richTextBox = new()
            {
                SelectionTabs = [tab1, tab2, tab3, tab4],
                AcceptsTab = true,
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
