using Microsoft.EntityFrameworkCore.Update.Internal;
using SiliFish.Helpers;
using SiliFish.Services;
using System.Text;

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
                    List<string> values = row.Cells.OfType<DataGridViewCell>().Select(c => c.Value?.ToString() ?? "").ToList();
                    string csvLine = CSVUtil.WriteCSVLine(values);
                    sb.AppendLine(csvLine);
                }
                return sb;
            }
            catch { return null; }
        }

        public static void LoadData(this DataGridView dataGrid, List<string> columnNames, List<List<string>> values, bool append, out int firstRow)
        {
            firstRow = 0;
            try 
            {
                if (!append)
                {
                    dataGrid.Rows.Clear();
                    firstRow = 0;
                }
                else 
                    firstRow = dataGrid.Rows.Count - 1;
                List<int> colIndices = [];
                foreach(string colName in columnNames)
                {
                    if (!dataGrid.Columns.Contains(colName))
                        dataGrid.Columns.Add(colName, colName);
                    colIndices.Add(dataGrid.Columns[colName].Index);
                }
                foreach (List<string> row in values)
                {
                    int colIndex = 0;
                    dataGrid.RowCount++;
                    int rowInd = dataGrid.RowCount - 1;
                    foreach (var item in row)
                    {
                        dataGrid[colIndices[colIndex++], rowInd].Value = item;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            
        }
    }
}
