using Extensions;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.Helpers;
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
                    ddParameter.SelectedIndex = 0;
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
            double[] time = new double[plotEnd + 1];
            TimeLine tl = new();
            tl.AddTimeRange((int)eStepStartTime.Value, (int)eStepEndTime.Value);
            List<ChartDataStruct> charts = new();
            foreach (int i in Enumerable.Range(0, plotEnd + 1))
                time[i] = i * (double)dt;
            if (rbManualEntryStimulus.Checked)
            {
                double[] I = new double[plotEnd + 1];
                Stimulus stim = new()
                {
                    StimulusSettings = stimulusControl1.GetStimulus(),
                    TimeSpan_ms = tl
                };
                foreach (int i in Enumerable.Range(stimStart, stimEnd - stimStart))
                    I[i] = stim.generateStimulus(i, SwimmingModel.rand);
                Dynamics dyn = Cell.DynamicsTest(I);
                charts.Add(new ChartDataStruct
                {
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
                    Title = "Stimulus",
                    Color = Color.Red,
                    xData = time,
                    yData = I,
                    yLabel = $"I ({Util.GetUoM(Const.UoM, Measure.Current)})"
                });
            }
            else
            {
                if (double.TryParse(eRheobase.Text, out double rheobase))
                {
                    double[,] I = new double[plotEnd + 1, Const.RheobaseTestMultipliers.Length];
                    int iter = 0;
                    List<string> columnNames = new();
                    foreach (double multiplier in Const.RheobaseTestMultipliers)
                    {
                        Stimulus stim = new()
                        {
                            StimulusSettings = new()
                            {
                                Mode = StimulusMode.Step,
                                Value1 = rheobase * multiplier,
                                Value2 = 0
                            },
                            TimeSpan_ms = tl
                        };
                        foreach (int i in Enumerable.Range(stimStart, stimEnd - stimStart))
                            I[i, iter] = stim.generateStimulus(i, SwimmingModel.rand);
                        Dynamics dyn = Cell.DynamicsTest(I.GetColumn(iter++));
                        columnNames.Add($"x {multiplier:0.##}");
                        charts.Add(new ChartDataStruct
                        {
                            Title = $"V - Rheobase x {multiplier:0.##}",
                            Color = Color.Purple,
                            xData = time,
                            yData = dyn.VList,
                            yLabel = "V (mV)"
                        });
                    }
                    charts.Add(new ChartDataStruct
                    {
                        Title = $"Stimulus",
                        Color = Color.Red,
                        xData = time,
                        yMultiData = I,
                        yLabel = string.Join(',', columnNames)
                    });
                }
            }
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
                eRheobase.Text = d.ToString(Const.DecimalPointFormat);
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

        private void rbManualEntryStimulus_CheckedChanged(object sender, EventArgs e)
        {
            stimulusControl1.Enabled = rbManualEntryStimulus.Checked;
        }
    }
}
