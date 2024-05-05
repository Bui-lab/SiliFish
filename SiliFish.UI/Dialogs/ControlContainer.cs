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
            Text = ccargs.Caption;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CheckValuesArgs args = new();

            CheckValues?.Invoke(this, args);
            if (args.Errors != null && args.Errors.Count != 0)
            {
                MessageBox.Show($"There are errors on the form.\r\n{string.Join("\r\n", args.Errors)}", "Error");
                this.DialogResult = DialogResult.None;
            }
        }

        private (int, int) GetLimits(Control ctrl)
        {
            int maxX = 0; int maxY = 0;
            if (ctrl.Controls.Count > 0)
            {
                foreach (Control sub in ctrl.Controls)
                {
                    (int curX, int curY) = GetLimits(sub);
                    maxX = Math.Max(maxX, ctrl.Left + curX);
                    maxY = Math.Max(maxY, ctrl.Top + curY);
                }
            }
            else return (ctrl.Right, ctrl.Bottom);
            return (maxX, maxY);
        }

        private void ControlContainer_Load(object sender, EventArgs e)
        {
            (int curX, int curY) = GetLimits(pMain);
            Width = curX + 40;
            Height = curY + 2 * pBottom.Height + 10;
        }
    }
}
