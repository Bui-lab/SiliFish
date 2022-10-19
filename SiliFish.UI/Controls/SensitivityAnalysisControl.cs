using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SiliFish.UI.Controls
{
    public partial class SensitivityAnalysisControl : UserControl
    {
        private event EventHandler runAnalysis;
        public event EventHandler RunAnalysis { add => runAnalysis += value; remove => runAnalysis -= value; }

        private double MinMultiplier
        {
            get
            {
                if (double.TryParse(eMinMultiplier.Text, out double d))
                    return d;
                return 0;
            }
        }
        private double MaxMultiplier
        {
            get
            {
                if (double.TryParse(eMaxMultiplier.Text, out double d))
                    return d;
                return 0;
            }
        }

        private int NumOfPoints
        {
            get
            {
                if (int.TryParse(eNumOfValues.Text, out int i))
                    return i;
                return 1;
            }
        }

        public bool LogScale { get => cbLogScale.Checked; }
        public string SelectedParam { get => ddParameter.Text; }
        public SensitivityAnalysisControl()
        {
            InitializeComponent();
        }

        public void SetParameters(bool includeAll, string[] parameters)
        {
            ddParameter.Items.Clear();
            if (parameters == null)
                return;
            if (includeAll)
                ddParameter.Items.Add("All Parameters");
            ddParameter.Items.AddRange(parameters);
            ddParameter.SelectedIndex = 0;
        }

        public double[] GetValues(double origValue)
        {
            return Util.GenerateValues(origValue, MinMultiplier, MaxMultiplier, NumOfPoints, cbLogScale.Checked);
        }
        private void btnRunAnalysis_Click(object sender, EventArgs e)
        {
            runAnalysis?.Invoke(this, EventArgs.Empty);
        }
    }
}
