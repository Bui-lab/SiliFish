using SiliFish.Definitions;

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

        private void eDatabaseFile_Click(object sender, EventArgs e)
        {
            browseFile.InitialDirectory = eDatabaseFile.Text;
            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                GlobalSettings.DatabaseName =
                    eDatabaseFile.Text = browseFile.FileName;
            }
        }
    }
}
