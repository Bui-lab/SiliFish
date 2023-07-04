using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SiliFish.UI
{
    public partial class ProgressForm : Form
    {
        private event EventHandler stopRunClicked;
        public event EventHandler StopRunClicked
        {
            add
            {
                stopRunClicked += value;
            }
            remove
            {
                stopRunClicked -= value;
            }
        }

        public double Progress
        {
            set
            {
                lProgressValue.Text = value < 0 ? "N/A" : $"{value:0.##}%";
                progressBar.Value = value < 0 ? 50 : (int)Math.Floor(value);
            }
        }

        public string ProgressText
        {
            set
            {
                lProgressDetails.Text = value;
            }
        }
        public ProgressForm()
        {
            InitializeComponent();
        }

        private void linkStopRun_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            stopRunClicked?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            linkStopRun.Visible = stopRunClicked != null;
        }
    }
}
