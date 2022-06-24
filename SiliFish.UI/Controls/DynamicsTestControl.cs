using SiliFish.ModelUnits;
using SiliFish.UI.Extensions;

namespace SiliFish.UI.Controls
{
    public partial class DynamicsTestControl : UserControl
    {
        private Cell cell;

        private event EventHandler useUpdatedParams;
        public event EventHandler UseupdatedParams { add => useUpdatedParams += value; remove => useUpdatedParams -= value; }

        public Cell Cell { get => cell;
            set
            {
                cell = value;
                dgDynamics.WriteToGrid(cell?.Parameters);
            }
        }
        public DynamicsTestControl()
        {
            InitializeComponent();
        }

        private void btnDynamicsRun_Click(object sender, EventArgs e)
        {
            if (Cell == null) return;

            cell.Parameters = dgDynamics.ReadFromGrid();

            decimal dt = edt.Value;
            int stimStart = (int)(eStepStartTime.Value / dt);
            int stimEnd = (int)(eStepEndTime.Value / dt);
            int plotEnd = (int)(ePlotEndTime.Value / dt);
            double[] I = new double[plotEnd];
            double[] t = new double[plotEnd];
            double val = (double)eInput.Value;
            foreach (int i in Enumerable.Range(0, plotEnd))
                t[i] = i * (double)dt;
            foreach (int i in Enumerable.Range(stimStart, stimEnd - stimStart))
                I[i] = val;
            eInstanceParams.Text = Cell.GetInstanceParams();
            (double[] vList, double[] uList) = Cell.DynamicsTest(I);
            picV.Image = UtilWindows.CreateLinePlot("V", vList, t, 0, plotEnd, Color.Purple);
            if (uList == null)
                picu.Visible = false;
            else picu.Image = UtilWindows.CreateLinePlot("u", uList, t, 0, plotEnd, Color.Blue);
            picI.Image = UtilWindows.CreateLinePlot("I", I, t, 0, plotEnd, Color.Red);

        }

        private void btnRheobase_Click(object sender, EventArgs e)
        {
            if (Cell == null || Cell.GetType() != typeof(Neuron)) return;

            cell.Parameters = dgDynamics.ReadFromGrid();

            //TODO muscle cell contraction - how to we handle it?
            Neuron neuron = Cell as Neuron;
            eInstanceParams.Text = neuron.GetInstanceParams();
            decimal limit = eRheobaseLimit.Value;
            if (limit > eInput.Maximum)
                eInput.Maximum = limit;
            decimal d = (decimal)neuron.CalculateRheoBase((double)edt.Value, (double)limit, Math.Pow(0.1, eInput.DecimalPlaces));
            if (d <= eInput.Maximum && d >= eInput.Minimum)
                eRheobase.Text = d.ToString("0.###");
            else
                eInstanceParams.Text = "No rheobase below " + eInput.Maximum + ".\r\n" + eInstanceParams.Text;

        }

        private void splitMain_Panel2_Resize(object sender, EventArgs e)
        {
            picu.Height = (splitMain.Panel2.ClientSize.Height - picI.Height) / 2;

        }

        private void linkUseUpdatedParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cell.Parameters = dgDynamics.ReadFromGrid();
            useUpdatedParams?.Invoke(this, EventArgs.Empty);
        }
    }
}
