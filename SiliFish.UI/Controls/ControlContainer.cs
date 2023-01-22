namespace SiliFish.UI
{
    public partial class ControlContainer : Form
    {
        public ControlContainer()
        {
            InitializeComponent();
        }

        public bool SaveVisible
        {
            get { return btnSave.Visible; }
            set
            {
                btnSave.Visible = value;
                if (!value)
                {
                    btnCancel.Left = btnSave.Left;
                    btnCancel.Text = "Close";
                }
            }
        }
        public void AddControl(Control ctrl)
        {
            pMain.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
