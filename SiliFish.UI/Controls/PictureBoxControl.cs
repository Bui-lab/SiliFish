namespace SiliFish.UI.Controls
{
    public partial class PictureBoxControl : UserControl
    {
        public PictureBoxControl()
        {
            InitializeComponent();
        }

        public Image Image
        {
            get
            {
                return pictureBox.Image;
            }
            set
            {
                pictureBox.Image = value;
            }
        }

        private void miSaveImage_Click(object sender, EventArgs e)
        {
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                UtilWindows.SaveImage(saveImageDialog.FileName, pictureBox.Image);
            }
        }
    }
}
