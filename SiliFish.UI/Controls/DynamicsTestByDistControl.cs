using SiliFish.DataTypes;
using SiliFish.DynamicUnits;
using SiliFish.ModelUnits;
using SiliFish.Services;

namespace SiliFish.UI.Controls
{
    public partial class DynamicsTestByDistControl : UserControl
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
        public DynamicsTestByDistControl()
        {
            InitializeComponent();
            InitAsync();
            Wait();
        }

        private async void InitAsync()
        {
            await webViewPlots.EnsureCoreWebView2Async();
            }

        private void Wait()
        {
            while (webViewPlots.Tag == null)
                Application.DoEvents();
        }
        private void webViewPlots_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            (sender as Control).Tag = true;
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
            double[] time = new double[plotEnd + 1];
            TimeLine tl = new ();
            tl.AddTimeRange((int)eStepStartTime.Value, (int)eStepEndTime.Value);
            Stimulus stim = new()
            {
                StimulusSettings = stimulusControl1.GetStimulus(),
                TimeSpan_ms = tl
            };
            foreach (int i in Enumerable.Range(0, plotEnd + 1))
                time[i] = i * (double)dt;
            foreach (int i in Enumerable.Range(stimStart, stimEnd - stimStart))
                I[i] = stim.generateStimulus(i, SwimmingModel.rand);
            eInstanceParams.Text = Cell.GetInstanceParams();
            Dynamics dyn = Cell.DynamicsTest(I);
            if (dyn.tauRise.Any())
                eInstanceParams.Text += "\r\nτ rise: " + string.Join(',', dyn.tauRise.Select(d => d.ToString()).ToArray());
            if (dyn.tauDecay.Any())
                eInstanceParams.Text += "\r\nτ decay: " + string.Join(',', dyn.tauDecay.Select(d => d.ToString()).ToArray());
            List<ChartDataStruct> charts = new();
            charts.Add(new ChartDataStruct { 
                Title = "V", 
                Color = Color.Purple,
                xData = time,
                yData = dyn.VList,
                yLabel = "V (mV)"
            });
            charts.Add(new ChartDataStruct
            {
                Title = cell is MuscleCell ? "Rel. Tension" : "u",
                Color = Color.Blue,
                xData = time,
                yData = dyn.secList,
                yLabel = cell is MuscleCell ? "Rel. Tension" : "u"
            });
            charts.Add(new ChartDataStruct
            {
                Title = "I",
                Color = Color.Red,
                xData = time,
                yData = I,
                yLabel = "I (pA)" //TODO add UoM selection
            });
            string html = DyChartGenerator.PlotLineCharts(charts, "Dynamics Test Results", synchronized: true, webViewPlots.ClientSize.Width, webViewPlots.ClientSize.Height);
            webViewPlots.NavigateToString(html);
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
            if (d > 0)
                eRheobase.Text = d.ToString("0.###");
            else
                eInstanceParams.Text = "No rheobase below " + eRheobaseLimit.Value + ".\r\n" + eInstanceParams.Text;

        }


        private void linkUseUpdatedParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cell.Parameters = dgDynamics.ReadFromGrid();
            useUpdatedParams?.Invoke(this, EventArgs.Empty);
        }

    }
}
