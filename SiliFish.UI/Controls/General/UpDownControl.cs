namespace SiliFish.UI.Controls.General
{
    public partial class UpDownControl : DomainUpDown
    {
        public event EventHandler DownClicked;
        public event EventHandler UpClicked;

        public UpDownControl()
        {
            InitializeComponent();
        }

        public override void UpButton()
        {
            base.UpButton();
            UpClicked?.Invoke(this, EventArgs.Empty);
        }

        public override void DownButton()
        {
            base.DownButton();
            DownClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
