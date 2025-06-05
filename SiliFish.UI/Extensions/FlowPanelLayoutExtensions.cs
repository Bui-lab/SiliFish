using SiliFish.Extensions;

namespace Extensions
{
    public static class FlowPanelLayoutExtensions
    {
        public static void CreateTextBoxControlsForDictionary(this FlowLayoutPanel flowPanel, Dictionary<string, object> ParamDict)
        {
            int maxLen = ParamDict.Keys.Select(k => k.Length).Max();
            int tabIndex = 1;
            flowPanel.FlowDirection = FlowDirection.LeftToRight;
            foreach (KeyValuePair<string, object> kvp in ParamDict)
            {
                Label lbl = new()
                {
                    Text = kvp.Key,
                    Height = 23,
                    Width = 6 * maxLen + 5,
                    TextAlign = ContentAlignment.MiddleLeft,
                    TabIndex = tabIndex++
                };
                flowPanel.Controls.Add(lbl);
                TextBox textBox = new()
                {
                    Tag = kvp.Value,
                    Height = 23,
                    Width = 60,
                    Text = kvp.Value?.ToString(),
                    TabIndex = tabIndex++
                };
                flowPanel.Controls.Add(textBox);
                flowPanel.SetFlowBreak(textBox, true);
            }
        }

        public static void CreateNumericUpDownControlsForDictionary(this FlowLayoutPanel flowPanel,
            Dictionary<string, double> ParamDict, Dictionary<string, string> DescDict,
            ToolTip toolTip)
        {
            try
            {
                int maxLen = ParamDict.Keys.Select(k => k.Length).Max();
                int tabIndex = 1;
                flowPanel.Controls.Clear();
                flowPanel.FlowDirection = FlowDirection.LeftToRight;
                foreach (string key in ParamDict.Keys)
                {
                    Label lbl = new()
                    {
                        Text = key,
                        Height = 23,
                        Width = 8 * maxLen + 5,
                        TextAlign = ContentAlignment.BottomLeft,
                        TabIndex = tabIndex++
                    };
                    flowPanel.Controls.Add(lbl);
                    decimal val = Convert.ToDecimal(ParamDict[key]);
                    int[] bits = Decimal.GetBits(val);
                    int decPoints = (bits[3] >> 16) & 0x7F;//https://docs.microsoft.com/en-us/dotnet/api/system.decimal.getbits?view=net-6.0
                    decimal inc = decPoints == 0 ? 1 : (decimal)(1 / (Math.Pow(10, decPoints)));
                    NumericUpDown numBox = new()
                    {
                        Tag = val,
                        Height = 23,
                        Width = 60,
                        Minimum = decimal.MinValue,
                        Maximum = decimal.MaxValue,
                        Value = val,
                        DecimalPlaces = decPoints + 2,
                        Increment = inc,
                        TabIndex = tabIndex++
                    };
                    flowPanel.Controls.Add(numBox);
                    if (DescDict.TryGetValue(key, out string tooltip))
                    {
                        toolTip.SetToolTip(lbl, tooltip);
                        toolTip.SetToolTip(numBox, tooltip);
                    }
                    flowPanel.SetFlowBreak(numBox, true);
                }
            }
            catch (Exception exc)
            { }
        }

        public static Dictionary<string, double> CreateDoubleDictionaryFromControls(this FlowLayoutPanel flowPanel)
        {
            Dictionary<string, double> ParamDict = [];
            string lastKey = "";
            foreach (Control control in flowPanel.Controls)
            {
                if (control is Label lbl)
                    lastKey = lbl.Text;
                else if (!string.IsNullOrEmpty(lastKey))
                {
                    if (control is TextBox textBox)
                    {
                        if (double.TryParse(textBox.Text, out double d))
                            ParamDict.Add(lastKey, d);
                    }
                    else if (control is NumericUpDown numBox)
                    {
                        ParamDict.Add(lastKey, (double)numBox.Value);
                    }
                    lastKey = "";
                }
            }
            return ParamDict;
        }
    }
}
