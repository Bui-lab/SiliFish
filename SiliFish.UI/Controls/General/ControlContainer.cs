namespace SiliFish.UI
{
    public partial class ControlContainer : Form
    {
        private event EventHandler CheckValues;
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
        public void AddControl(Control ctrl, EventHandler eventHandler)
        {
            pMain.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
            if (eventHandler != null)
                CheckValues += eventHandler;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CheckValuesArgs args = new();

            CheckValues?.Invoke(this, args);
            if (args.Errors != null && args.Errors.Any())
            {
                MessageBox.Show($"There are errors on the form.\r\n{string.Join("\r\n", args.Errors)}");
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
