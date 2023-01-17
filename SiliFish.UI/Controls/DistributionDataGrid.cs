using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Services;
using SiliFish.UI.Extensions;

namespace SiliFish.UI.Controls
{
    public partial class DistributionDataGrid : UserControl
    {
        public DistributionDataGrid()
        {
            InitializeComponent();
        }

        private void dgDynamics_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int rowind = e.RowIndex;
            if (e.ColumnIndex == colDistEditLink.Index)
            {
                DistributionControl distControl = new()
                {
                    AbsoluteEnforced = true
                };
                ControlContainer frmControl = new();
                frmControl.AddControl(distControl);
                frmControl.Text = dgDistribution[colField.Index, e.RowIndex].Value.ToString();
                if (dgDistribution.Rows[e.RowIndex].Tag is Distribution dist)
                    distControl.SetDistribution(dist);
                else
                {
                    double val = double.Parse(dgDistribution[colUniqueValue.Index, e.RowIndex].Tag?.ToString() ?? dgDistribution[colUniqueValue.Index, e.RowIndex].Value?.ToString());
                    dist = new Constant_NoDistribution(val, true, false, 0);
                    distControl.SetDistribution(dist);
                }
                if (frmControl.ShowDialog() == DialogResult.OK)
                {
                    dist = distControl.GetDistribution();
                    if (dist.DistType == nameof(Constant_NoDistribution) && (dist as Constant_NoDistribution).NoiseStdDev < CurrentSettings.Settings.Epsilon)
                        WriteNoDistToRow(null, dist.UniqueValue, rowind);
                    else
                        WriteDistToRow(null, dist, rowind);
                }
            }
        }
        private void dgDynamics_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgDistribution.Rows[e.RowIndex].Tag is Distribution dist)
            {
                if (dist.DistType == nameof(Constant_NoDistribution) && (dist as Constant_NoDistribution).NoiseStdDev < CurrentSettings.Settings.Epsilon)
                    dgDistribution[colUniqueValue.Index, e.RowIndex].ReadOnly = false;
                else
                    dgDistribution[colUniqueValue.Index, e.RowIndex].ReadOnly = true;
            }
            else
                dgDistribution[colUniqueValue.Index, e.RowIndex].ReadOnly = false;
        }
        private void dgDynamics_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgDistribution.Rows[e.RowIndex].Tag = null;
        }

        private void WriteDistToRow(string key, Distribution dist, int rowIndex)
        {
            if (dgDistribution.RowCount <= rowIndex || dist == null) return;
            if (key != null)
                dgDistribution[colField.Index, rowIndex].Value = key;
            dgDistribution.Rows[rowIndex].Tag = dist;
            dgDistribution[colUniqueValue.Index, rowIndex].Value = dist.UniqueValue.ToString("0.#####");
            dgDistribution[colRange.Index, rowIndex].Value = dist.RangeStr;
            dgDistribution[colDistribution.Index, rowIndex].Value = dist.DistType;
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
                        if (dist.DistType == nameof(Constant_NoDistribution) && (dist as Constant_NoDistribution).NoiseStdDev < CurrentSettings.Settings.Epsilon)
                            WriteNoDistToRow(key, dist.UniqueValue, rowIndex);
                        else
                            WriteDistToRow(key, dist, rowIndex);
                    rowIndex++;
                }
            }
            catch { }
        }

        public Dictionary<string, Distribution> ReadFromGrid()//TODO review
        {
            try
            {
                Dictionary<string, Distribution> paramDict = new();
                for (int rowIndex = 0; rowIndex < dgDistribution.RowCount; rowIndex++)
                {
                    if (dgDistribution.Rows[rowIndex].Tag is Distribution dist)
                        paramDict.Add(dgDistribution[colField.Index, rowIndex].Value.ToString(), dist);
                    else
                        paramDict.Add(dgDistribution[colField.Index, rowIndex].Value.ToString(), 
                            new Constant_NoDistribution( double.Parse(dgDistribution[colUniqueValue.Index, rowIndex].Value.ToString()), true, false, 0));
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
