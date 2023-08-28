using SiliFish.Definitions;
using SiliFish.UI.EventArguments;

namespace SiliFish.UI
{
    public partial class ControlContainer : Form
    {
        private event EventHandler CheckValues;
        public ControlContainer()
        {
            InitializeComponent();
        }

        public ControlContainer(Point parentCorner)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(parentCorner.X + 10, parentCorner.Y + 10);
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
        public void AddControl(Control ctrl, EventHandler checkValues)
        {
            pMain.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
            if (checkValues != null)
                CheckValues += checkValues;
        }

        public void ChangeCaption(object sender, EventArgs args)
        {
            ContentChangedArgs ccargs = args as ContentChangedArgs;
            Text= ccargs.Caption;
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
                MessageBox.Show($"There are errors on the form.\r\n{string.Join("\r\n", args.Errors)}", "Error");
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
