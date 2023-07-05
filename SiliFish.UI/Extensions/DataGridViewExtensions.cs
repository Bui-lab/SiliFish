using SiliFish.Definitions;
using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.UI.Extensions
{
    public static class DataGridViewExtensions
    {
        public static StringBuilder ExportToStringBuilder(this DataGridView dataGrid)
        {
            try
            {
                StringBuilder sb = new();
                sb.AppendLine(string.Join(',', dataGrid.Columns.OfType<DataGridViewColumn>().Select(c => c.HeaderText)));
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    sb.AppendLine(string.Join(',', row.Cells.OfType<DataGridViewCell>().Select(c => c.Value)));
                }
                return sb;
            }
            catch { return null; }
        }
    }
}
