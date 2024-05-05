using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Services;


namespace SiliFish.UI.Controls
{
    public partial class DistributionDataGrid : UserControl
    {
        public DistributionDataGrid()
        {
            InitializeComponent();
        }

        private void OpenDistributionDialog(int rowind)
        {
            DistributionControl distControl = new()
            {
                AbsoluteEnforced = true
            };
            ControlContainer frmControl = new();
            frmControl.AddControl(distControl, null);
            frmControl.Text = dgDistribution[colField.Index, rowind].Value.ToString();
            if (dgDistribution.Rows[rowind].Tag is Distribution dist)
                distControl.SetDistribution(dist);
            else
            {
                double val = double.Parse(dgDistribution[colUniqueValue.Index, rowind].Tag?.ToString() ?? dgDistribution[colUniqueValue.Index, rowind].Value?.ToString());
                dist = new Constant_NoDistribution(val);
                distControl.SetDistribution(dist);
            }
            if (frmControl.ShowDialog() == DialogResult.OK)
            {
                dist = distControl.GetDistribution();
                if ((dist is Constant_NoDistribution constant_NoDistribution) && constant_NoDistribution.NoiseStdDev < GlobalSettings.Epsilon)
                    WriteNoDistToRow(null, dist.UniqueValue, rowind);
                else
                    WriteDistToRow(null, dist, rowind);
            }
        }
        private void dgDistribution_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == colDistEditLink.Index)
            {
                OpenDistributionDialog(e.RowIndex);
            }
        }
        private void dgDistribution_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgDistribution.Rows[e.RowIndex].Tag is Distribution dist)
            {
                if ((dist is Constant_NoDistribution constant_NoDistribution) && constant_NoDistribution.NoiseStdDev < GlobalSettings.Epsilon)
                    dgDistribution[colUniqueValue.Index, e.RowIndex].ReadOnly = false;
                else
                    dgDistribution[colUniqueValue.Index, e.RowIndex].ReadOnly = true;
            }
            else
                dgDistribution[colUniqueValue.Index, e.RowIndex].ReadOnly = false;
        }
        private void dgDistribution_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgDistribution.Rows[e.RowIndex].Tag = null;
        }
        private void dgDistribution_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                dgDistribution.AutoResizeColumns();
                return;
            }
            OpenDistributionDialog(e.RowIndex);
        }

        private void WriteDistToRow(string key, Distribution dist, int rowIndex)
        {
            if (dgDistribution.RowCount <= rowIndex || dist == null) return;
            if (key != null)
                dgDistribution[colField.Index, rowIndex].Value = key;
            dgDistribution.Rows[rowIndex].Tag = dist;
            dgDistribution[colUniqueValue.Index, rowIndex].Value = dist.UniqueValue.ToString("0.#####");
            dgDistribution[colRange.Index, rowIndex].Value = dist.RangeStr;
            dgDistribution[colDistribution.Index, rowIndex].Value = dist.Discriminator;
            dgDistribution[colDistDetails.Index, rowIndex].Value = dist.ToString();
        }

        private void WriteNoDistToRow(string key, double d, int rowIndex)
        {
            if (dgDistribution.RowCount <= rowIndex) return;
            if (key != null)
                dgDistribution[colField.Index, rowIndex].Value = key;
            dgDistribution.Rows[rowIndex].Tag = null;
            dgDistribution[colUniqueValue.Index, rowIndex].Value = d.ToString("0.#####");
            dgDistribution[colRange.Index, rowIndex].Value = "";
            dgDistribution[colDistribution.Index, rowIndex].Value = "";
            dgDistribution[colDistDetails.Index, rowIndex].Value = "";
        }
        public void WriteToGrid(Dictionary<string, double> parameters)
        {
            try
            {
                dgDistribution.RowCount = parameters?.Count ?? 0;
                if (parameters == null)
                    return;
                int rowIndex = 0;
                foreach (string key in parameters.Keys)
                {
                    WriteNoDistToRow(key, parameters[key], rowIndex);
                    rowIndex++;
                }
            }
            catch { }
        }

        public void WriteToGrid(Dictionary<string, Distribution> parameters)
        {
            try
            {
                dgDistribution.RowCount = parameters?.Count ?? 0;
                if (parameters == null)
                    return;
                int rowIndex = 0;
                foreach (string key in parameters.Keys)
                {
                    Distribution dist = parameters[key];
                        if ((dist is Constant_NoDistribution constant_NoDistribution) && constant_NoDistribution.NoiseStdDev < GlobalSettings.Epsilon)
                            WriteNoDistToRow(key, dist.UniqueValue, rowIndex);
                        else
                            WriteDistToRow(key, dist, rowIndex);
                    rowIndex++;
                }
            }
            catch { }
        }

        public Dictionary<string, Distribution> ReadFromGrid()
        {
            try
            {
                Dictionary<string, Distribution> paramDict = [];
                for (int rowIndex = 0; rowIndex < dgDistribution.RowCount; rowIndex++)
                {
                    string field = dgDistribution[colField.Index, rowIndex].Value.ToString();
                    if (dgDistribution.Rows[rowIndex].Tag is Distribution dist)
                        paramDict.Add(field, dist);
                    else
                    {
                        double value = double.Parse(dgDistribution[colUniqueValue.Index, rowIndex].Value.ToString());
                        paramDict.Add(field,new Constant_NoDistribution(value)); 
                    }
                }
                return paramDict;
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
                return null; 
            }
        }

    }
}
