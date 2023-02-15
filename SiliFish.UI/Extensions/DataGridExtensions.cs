namespace SiliFish.UI.Extensions
{
    public static class DataGridExtensions
    {
        /// <summary>
        /// Allows auto resize datagrids and user sizing simultaneously
        /// </summary>
        /// <param name="dg"></param>
        public static void AutoResizeCoplumns(this DataGridView dg)
        {
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            int[] colWidths = new int[dg.ColumnCount];
            for (int colind = 0; colind < dg.ColumnCount; colind++)
                colWidths[colind] = dg.Columns[colind].Width;
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            for (int colind = 0; colind < dg.ColumnCount; colind++)
                dg.Columns[colind].Width = colWidths[colind];
        }
    }
}
