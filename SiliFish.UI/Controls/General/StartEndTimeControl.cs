namespace SiliFish.UI.Controls.General
{
    public partial class StartEndTimeControl : UserControl
    {
        public int EndTime { get => (int)eEndTime.Value; set => eEndTime.Value = value; }
        public int StartTime { get => (int)eStartTime.Value; set => eStartTime.Value = value; }

        public StartEndTimeControl()
        {
            InitializeComponent();
        }

    }
}
