using SiliFish.DataTypes;
using SiliFish.DynamicUnits;
using SiliFish.ModelUnits;

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
            RunParam.static_Skip = 0;
            RunParam.static_dt = (double)dt;
            int stimStart = (int)(eStepStartTime.Value / dt);
            int stimEnd = (int)(eStepEndTime.Value / dt);
            int plotEnd = (int)(ePlotEndTime.Value / dt);
            double[] I = new double[plotEnd + 1];
            double[] t = new double[plotEnd + 1];
            TimeLine tl = new ();
            tl.AddTimeRange((int)eStepStartTime.Value, (int)eStepEndTime.Value);
            Stimulus stim = new()
            {
                StimulusSettings = stimulusControl1.GetStimulus(),
                TimeSpan_ms = tl
            };
            foreach (int i in Enumerable.Range(0, plotEnd + 1))
                t[i] = i * (double)dt;
            foreach (int i in Enumerable.Range(stimStart, stimEnd - stimStart))
                I[i] = stim.getStimulus(i, SwimmingModel.rand);
            eInstanceParams.Text = Cell.GetInstanceParams();
            Dynamics dyn = Cell.DynamicsTest(I);
            if (dyn.tauRise.Any())
                eInstanceParams.Text += "\r\nτ rise: " + string.Join(',', dyn.tauRise.Select(d => d.ToString()).ToArray());
            if (dyn.tauDecay.Any())
                eInstanceParams.Text += "\r\nτ decay: " + string.Join(',', dyn.tauDecay.Select(d => d.ToString()).ToArray());
            picV.Image = UtilWindows.CreateLinePlot("V", dyn.Vlist, t, 0, plotEnd, "V", null, null, Color.Purple);
            if (dyn.ulist == null)
                picu.Visible = false;
            else picu.Image = UtilWindows.CreateLinePlot("u", dyn.ulist, t, 0, plotEnd, "u", null, null, Color.Blue);
            picI.Image = UtilWindows.CreateLinePlot("I", I, t, 0, plotEnd, "I", null, null, Color.Red);

        }

        private void btnRheobase_Click(object sender, EventArgs e)
        {
            if (Cell == null || Cell.GetType() != typeof(Neuron)) return;

            cell.Parameters = dgDynamics.ReadFromGrid();

            //TODO muscle cell contraction - how to we handle it?
            Neuron neuron = Cell as Neuron;
            eInstanceParams.Text = neuron.GetInstanceParams();
            decimal limit = eRheobaseLimit.Value;
            decimal d = (decimal)neuron.CalculateRheoBase((double)edt.Value, (double)limit, Math.Pow(0.1, 3));
            if (d>0)
                eRheobase.Text = d.ToString("0.###");
            else
                eInstanceParams.Text = "No rheobase below " + eRheobaseLimit.Value + ".\r\n" + eInstanceParams.Text;

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
