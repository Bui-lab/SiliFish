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
                UtilWindows.SaveImage(saveImageDialog, pictureBox.Image);
        }
    }
}
