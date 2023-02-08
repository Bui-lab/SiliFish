using OxyPlot;
using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Controls
{
    public partial class GlobalSettingsControl : UserControl
    {
        public GlobalSettingsProperties GSProperties;
        public GlobalSettingsControl(GlobalSettingsProperties gs)
        {
            InitializeComponent();
            GSProperties = gs;
            propSettings.SelectedObject = gs;
            eTemporaryFolder.Text = gs.TempFolder;
            eOutputFolder.Text = gs.OutputFolder;
        }

        private void eOutputFolder_Click(object sender, EventArgs e)
        {
            browseFolder.InitialDirectory = eOutputFolder.Text;
            if (browseFolder.ShowDialog() == DialogResult.OK)
            {
                GlobalSettings.OutputFolder =
                     eOutputFolder.Text = browseFolder.SelectedPath;
            }
        }

        private void eTemporaryFolder_Click(object sender, EventArgs e)
        {
            browseFolder.InitialDirectory = eTemporaryFolder.Text;
            if (browseFolder.ShowDialog() == DialogResult.OK)
            {
                GlobalSettings.TempFolder =
                    eTemporaryFolder.Text = browseFolder.SelectedPath;
            }
        }
    }
}
