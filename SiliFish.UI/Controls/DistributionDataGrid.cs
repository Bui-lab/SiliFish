using SiliFish.DataTypes;
using SiliFish.Definitions;
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
            if (dgDynamics.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                DistributionControl distControl = new();
                distControl.AbsoluteEnforced = true;
                ControlContainer frmControl = new();
                frmControl.AddControl(distControl);
                frmControl.Text = dgDynamics[colField.Index, e.RowIndex].Value.ToString();
                if (dgDynamics[colValue.Index, e.RowIndex].Tag is Distribution dist)
                    distControl.SetDistribution(dist);
                else
                {
                    double val = double.Parse(dgDynamics[colValue.Index, e.RowIndex].Tag?.ToString() ?? dgDynamics[colValue.Index, e.RowIndex].Value?.ToString());
                    dist = new Constant_NoDistribution(val, absolute: true, angular: false, 0);
                    distControl.SetDistribution(dist);
                }
                if (frmControl.ShowDialog() == DialogResult.OK)
                {
                    dist = distControl.GetDistribution();
                    if (dist.DistType == typeof(Constant_NoDistribution).ToString() && (dist as Constant_NoDistribution).NoiseStdDev < Const.Epsilon)
                    {
                        dgDynamics[colValue.Index, rowind].Value = dist.RangeStart;
                        dgDynamics[colValue.Index, rowind].Tag = null;
                    }
                    else
                    {
                        dgDynamics[colValue.Index, rowind].Value = dist.ToString();
                        dgDynamics[colValue.Index, rowind].Tag = dist;
                    }
                }
            }
        }

        public void WriteToGrid(Dictionary<string, object> parameters)
        {
            parameters.WriteToGrid(dgDynamics, colField.Index, colValue.Index);
        }

        public Dictionary<string, object> ReadFromGrid()
        {
            Dictionary<string, object> paramDict = new();
            paramDict.ReadFromGrid(dgDynamics, colField.Index, colValue.Index);
            return paramDict;
        }
    }
}
