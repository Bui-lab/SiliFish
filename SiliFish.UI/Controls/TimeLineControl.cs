using SiliFish.DataTypes;

namespace SiliFish.UI.Controls
{
    public partial class TimeLineControl : UserControl
    {
        private event EventHandler timeLineChanged;
        public event EventHandler TimeLineChanged { add => timeLineChanged += value; remove => timeLineChanged -= value; }
        public TimeLineControl()
        {
            InitializeComponent();
        }

        public void ClearTimeLine()
        {
            dgTimeLine.Rows.Clear();
            dgTimeLine.RowCount = 1;
        }
        public void SetTimeLine(int start, int end)
        {
            dgTimeLine.RowCount = 1;
            dgTimeLine[colStartTime.Index, 0].Value = start;
            dgTimeLine[colEndTime.Index, 0].Value = end;
        }
        public void SetTimeLine(TimeLine timeline)
        {
            if (timeline == null)
            {
                ClearTimeLine();
                return;
            }
            dgTimeLine.RowCount = timeline.GetTimeLine().Count + 1;
            int rowIndex = 0;
            foreach (var period in timeline.GetTimeLine())
            {
                dgTimeLine[colStartTime.Index, rowIndex].Value = period.start;
                dgTimeLine[colEndTime.Index, rowIndex++].Value = period.end;
            }
        }

        public TimeLine GetTimeLine()
        {
            TimeLine tl = new();
            for(int rowIndex = 0; rowIndex <dgTimeLine.Rows.Count; rowIndex++)
            {
                string sStart = dgTimeLine[colStartTime.Index, rowIndex].Value?.ToString();
                string sEnd = dgTimeLine[colEndTime.Index, rowIndex].Value?.ToString();
                if (string.IsNullOrEmpty(sStart) && string.IsNullOrEmpty(sEnd))
                    continue;
                if (!int.TryParse(sStart, out int start))
                    start = 0;
                if (!int.TryParse(sEnd, out int end))
                    end = -1;
                tl.AddTimeRange(start, end > 0 ? end : null);
            }
            return tl;
        }

        private void dgTimeLine_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgTimeLine.Focused)
                timeLineChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
