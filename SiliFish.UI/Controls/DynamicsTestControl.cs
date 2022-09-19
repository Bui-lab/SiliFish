using Extensions;
using SiliFish.DataTypes;
using SiliFish.DynamicUnits;
using SiliFish.ModelUnits;
using SiliFish.Services;
using System.Linq;

namespace SiliFish.UI.Controls
{
    public partial class DynamicsTestControl : UserControl
    {
        private Cell cell;
        private bool initialRun = false;

        private event EventHandler useUpdatedParams;
        public event EventHandler UseUpdatedParams { add => useUpdatedParams += value; remove => useUpdatedParams -= value; }

        public Cell Cell { get => cell;
            set
            {
                cell = value;
                if (cell == null)
                {
                    pParams.Controls.Clear();
                    ddParameter.Items.Clear();
                }
                else
                {
                    pParams.CreateNumericUpDownControlsForDictionary(cell?.Parameters);
                    foreach (Control control in pParams.Controls)
                    {
                        if (control is TextBox textBox)
                            textBox.TextChanged += TextBox_TextChanged;
                        else if (control is NumericUpDown numBox)
                            numBox.ValueChanged += NumBox_ValueChanged;
                    }
                    ddParameter.Items.Clear();
                    ddParameter.Items.Add("All Parameters");
                    foreach(var p in cell.Parameters)
                        ddParameter.Items.Add(p.Key);
                    FirstRun();
                }
            }
        }

        private void NumBox_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown numBox)
            {
                CalculateRheobase();
                DynamicsRun();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (double.TryParse(textBox.Text, out double _))
                {
                    CalculateRheobase();
                    DynamicsRun();
                }
            }
        }

        public DynamicsTestControl()
        {
            InitializeComponent();
            InitAsync();
            Wait();
        }

        private void FirstRun()
        {
            if (initialRun) return;
            if (Cell != null)
            {
                initialRun = true;
                CalculateRheobase();
                StimulusSettings stim = stimulusControl1.GetStimulus();
                if (double.TryParse(eRheobase.Text, out double d))
                {
                    stim.Value1 = d;
                    stimulusControl1.SetStimulus(stim);
                }
                DynamicsRun();
            }
        }
        private void DynamicsTestControl_Load(object sender, EventArgs e)
        {
            FirstRun();            
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

        private void DynamicsRun()
        {
            if (Cell == null) return;

            cell.Parameters = pParams.CreateDoubleDictionaryFromControls();

            decimal dt = edt.Value;
            RunParam.static_Skip = 0;
            RunParam.static_dt = (double)dt;
            int stimStart = (int)(eStepStartTime.Value / dt);
            int stimEnd = (int)(eStepEndTime.Value / dt);
            int plotEnd = (int)(ePlotEndTime.Value / dt);
            double[] I = new double[plotEnd + 1];
            double[] time = new double[plotEnd + 1];
            TimeLine tl = new();
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
            Dynamics dyn = Cell.DynamicsTest(I);
            List<ChartDataStruct> charts = new()
            {
                new ChartDataStruct
                {
                    Title = "V",
                    Color = Color.Purple,
                    xData = time,
                    yData = dyn.VList,
                    yLabel = "V (mV)"
                },
                new ChartDataStruct
                {
                    Title = cell is MuscleCell ? "Rel. Tension" : "u",
                    Color = Color.Blue,
                    xData = time,
                    yData = dyn.secList,
                    yLabel = cell is MuscleCell ? "Rel. Tension" : "u"
                },
                new ChartDataStruct
                {
                    Title = "I",
                    Color = Color.Red,
                    xData = time,
                    yData = I,
                    yLabel = "I (pA)" //add UoM selection
                }
            };
            string html = DyChartGenerator.PlotLineCharts(charts, 
                "Dynamics Test Results", synchronized: true,
                webViewPlots.ClientSize.Width, 
                (webViewPlots.ClientSize.Height - 150) / charts.Count);
            webViewPlots.NavigateToString(html);
        }
        private void btnDynamicsRun_Click(object sender, EventArgs e)
        {
            DynamicsRun();
        }

        private double CalculateRheobase()
        {
            if (Cell == null || Cell.GetType() != typeof(Neuron)) return 0;

            cell.Parameters = pParams.CreateDoubleDictionaryFromControls();

            //TODO muscle cell contraction - how to we handle it?
            Neuron neuron = Cell as Neuron;
            decimal limit = eRheobaseLimit.Value;
            decimal d = (decimal)neuron.CalculateRheoBase((double)edt.Value, (double)limit, Math.Pow(0.1, 3), (int)eRheobaseDuration.Value);
            if (d > 0)
                eRheobase.Text = d.ToString("0.###");
            else
                eRheobase.Text = "N/A";
            return Convert.ToDouble(d);
        }
        private void btnRheobase_Click(object sender, EventArgs e)
        {
            CalculateRheobase();
        }

        //update each parameter sequentially within the range [/10, *2]
        private void btnSensitivityAnalysis_Click(object sender, EventArgs e)
        {
            double limit = (double) eRheobaseLimit.Value;
            List<ChartDataStruct> charts = new();
            double dt = (double)edt.Value;
            Neuron neuron = Cell as Neuron;
            Dictionary<string, object> parameters =
                pParams.CreateDoubleDictionaryFromControls();
            if (ddParameter.SelectedIndex > 0)
            {
                string param = ddParameter.SelectedItem.ToString();
                parameters = parameters.Where(kvp => kvp.Key.ToString() == param).ToDictionary(kvp => kvp.Key, kvp => kvp.Value); 
            }
            foreach (string param in parameters.Keys)//change one parameter at a time
            {
                (double[] values, double[] rheos) = neuron.RheobaseSensitivityAnalysis(param, cbLogScale.Checked,
                    double.Parse(eMinMultiplier.Text), double.Parse(eMaxMultiplier.Text),
                    20/*num of points*/, dt, maxRheobase: limit, sensitivity: Math.Pow(0.1, 3), infinity: (int)eRheobaseDuration.Value);
                charts.Add(new ChartDataStruct
                {
                    Title = param,
                    Color = Color.Purple,
                    xData = values,
                    yData = rheos,
                    xLabel = param,
                    yLabel = "Rheobase",
                    drawPoints = true,
                    logScale = cbLogScale.Checked
                });
            }

            int height = (webViewPlots.ClientSize.Height - 150) / charts.Count;
            if (height < 200) height = 200;
            string html = DyChartGenerator.PlotLineCharts(charts, 
                "Rheobase Sensitivity Analysis", synchronized: false,
                webViewPlots.ClientSize.Width, 
                height);
            webViewPlots.NavigateToString(html);
        }
        private void linkUseUpdatedParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cell.Parameters = pParams.CreateDoubleDictionaryFromControls();
            useUpdatedParams?.Invoke(this, EventArgs.Empty);
        }
    }
}
