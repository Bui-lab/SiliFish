using Services;
using SiliFish;
using SiliFish.DataTypes;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.UI;

namespace Workspace
{
    public partial class Workspace : Form
    {
        SwimmingModel model = null;
        public Workspace()
        {
            InitializeComponent();
        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            if (openFileCSV.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, double[]> data = Util.ReadFromCSV(openFileCSV.FileName);
                model = new(data);
            }
        }

        private void btnPlotEpisodes_Click(object sender, EventArgs e)
        {
            if (model == null) return;
            (List<Image> images, _ )= WindowsPlotGenerator.Plot(PlotType.Episodes, model, null, null, new CellSelectionStruct(), 0.1, tSkip: 0);
            pictureBox1.Image = ImageHelperWindows.MergeImages(images, images.Count, 1);
        }
    }
}