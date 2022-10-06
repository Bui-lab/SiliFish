using Extensions;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.Services;
using SiliFish.Services.Optimization;

namespace SiliFish.UI.Controls
{
    public partial class DynamicsTestControl : UserControl
    {
        public string coreType;
        private bool updateParamNames = true;
        DynamicsStats dynamics;
        private double[] TimeArray;

        private event EventHandler useUpdatedParams;
        public event EventHandler UseUpdatedParams { add => useUpdatedParams += value; remove => useUpdatedParams -= value; }
        private Dictionary<string, double> parameters;
        private Dictionary<string, double> minValues;
        private Dictionary<string, double> maxValues;
        Dictionary<double, FiringPattern> firingPatterns;
        Dictionary<double, FiringRhythm> firingRhythms;

        private CoreSolver Solver;
        Dictionary<string, double> SolverBestValues;
        private bool OptimizationMode
        {
            get { return linkSwitchToOptimization.Text != "Optimization Mode"; }
            set
            {
                grPlotSelection.Enabled = rbManualEntryStimulus.Checked && !value;
                splitRight.Panel1Collapsed = !value;
                linkSwitchToOptimization.Text = value ? "Quit Optimization Mode" : "Optimization Mode";
            }
        }
        public Dictionary<string, double> Parameters
        {
            get => parameters;
            set
            {
                parameters = value;
                if (parameters == null)
                {
                    pParams.Controls.Clear();
                    ddParameter.Items.Clear();
                }
                else
                {
                    pParams.CreateNumericUpDownControlsForDictionary(parameters);
                    foreach (Control control in pParams.Controls)
                    {
                        if (control is TextBox textBox)
                            textBox.TextChanged += TextBox_TextChanged;
                        else if (control is NumericUpDown numBox)
                            numBox.ValueChanged += NumBox_ValueChanged;
                    }
                    if (updateParamNames)
                    {
                        ddParameter.Items.Clear();
                        ddParameter.Items.Add("All Parameters");
                        foreach (var p in parameters)
                            ddParameter.Items.Add(p.Key);
                        ddParameter.SelectedIndex = 0;

                        dgMinMaxValues.Rows.Clear();
                        dgMinMaxValues.RowCount = parameters.Count;
                        int rowIndex = 0;
                        foreach (string key in parameters.Keys)
                        {
                            dgMinMaxValues[colParameter.Index, rowIndex++].Value = key;
                        }
                    }
                    FirstRun();
                }
            }
        }

        private void NumBox_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown)
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

        public DynamicsTestControl(string coreType, Dictionary<string, double> parameters, bool testMode)
        {
            InitializeComponent();
            InitAsync();
            Wait();
            this.coreType = coreType;
            Parameters = parameters;
            pBottomBottom.Visible = !testMode;
            rbRheobaseBasedStimulus.Text = $"Use Rheobase ({string.Join(", ", Const.RheobaseTestMultipliers.Select(mult => "x" + mult.ToString()))})";
            cbGASelection.Items.AddRange(GeneticAlgorithmExtension.GetSelectionBases().ToArray());
            cbGASelection.SelectedIndex = 0;
            cbGACrossOver.Items.AddRange(GeneticAlgorithmExtension.GetCrossoverBases().ToArray());
            cbGACrossOver.SelectedIndex = 0;
            cbGAMutation.Items.AddRange(GeneticAlgorithmExtension.GetMutationBases().ToArray());
            cbGAMutation.SelectedIndex = 0;
            cbGATermination.Items.AddRange(GeneticAlgorithmExtension.GetTerminationBases().ToArray());
            cbGATermination.SelectedIndex = 0;
            splitRight.Panel1Collapsed = true;
            colFitnessFiringRhythm.DataSource = Enum.GetValues(typeof(FiringRhythm));
            colFitnessFiringMode.DataSource = Enum.GetValues(typeof(FiringPattern));
        }

        private void FirstRun()
        {
            if (parameters != null)
            {
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
            //FirstRun();            
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

        private void ReadParameters()
        {
            parameters = pParams.CreateDoubleDictionaryFromControls();
        }

        private void CreatePlots()
        {
            if (dynamics == null)
                return;
            List<ChartDataStruct> charts = new();
            string firingPattern = dynamics.FiringPattern.ToString();
            string firingRhythm = dynamics.FiringRhythm.ToString();
            if (cbV.Checked)
            {
                charts.Add(new ChartDataStruct
                {
                    Title = $"V ({firingRhythm} {firingPattern})",
                    Color = Color.Purple,
                    xData = TimeArray,
                    yData = dynamics.VList,
                    yLabel = "V (mV)"
                });
            }
            if (cbURelTension.Checked)
            {
                charts.Add(new ChartDataStruct
                {
                    Title = coreType == "Leaky_Integrator" ? "Rel. Tension" : "u", //TODO hardcoded leaky integrator - add a function to dynamic unit
                    Color = Color.Blue,
                    xData = TimeArray,
                    yData = dynamics.SecList,
                    yLabel = coreType == "Leaky_Integrator" ? "Rel. Tension" : "u" //TODO hardcoded leaky integrator - add a function to dynamic unit
                });
            }

            if (cbTauRise.Checked && dynamics.TauRise.Any())
            {
                charts.Add(new ChartDataStruct
                {
                    Title = "Tau rise",
                    Color = Color.Green,
                    xData = dynamics.TauRise.Keys.ToArray(),
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yData = dynamics.TauRise.Values.ToArray(),
                    yLabel = "Tau (ms)",
                    drawPoints = true
                });
            }
            if (cbTauDecay.Checked && dynamics.TauDecay.Any())
            {
                charts.Add(new ChartDataStruct
                {
                    Title = "Tau decay",
                    Color = Color.Green,
                    xData = dynamics.TauDecay.Keys.ToArray(),
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yData = dynamics.TauDecay.Values.ToArray(),
                    yLabel = "Tau (ms)",
                    drawPoints = true
                });
            }
            if (cbInterval.Checked && dynamics.Intervals_ms != null && dynamics.Intervals_ms.Any())
            {
                charts.Add(new ChartDataStruct
                {
                    Title = "Intervals",
                    Color = Color.Blue,
                    xData = dynamics.Intervals_ms.Keys.ToArray(),
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yData = dynamics.Intervals_ms.Values.ToArray(),
                    yLabel = "Interval (ms)",
                    drawPoints = true
                });
            }
            if (cbSpikingFrequency.Checked && dynamics.FiringFrequency != null && dynamics.FiringFrequency.Any())
            {
                charts.Add(new ChartDataStruct
                {
                    Title = "Spiking Freq.",
                    Color = Color.Blue,
                    xData = dynamics.FiringFrequency.Keys.ToArray(),
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yData = dynamics.FiringFrequency.Values.ToArray(),
                    yLabel = "Freq (Hz)",
                    drawPoints = true
                });
            }
            if (cbStimulus.Checked)
            {
                charts.Add(new ChartDataStruct
                {
                    Title = "Stimulus",
                    Color = Color.Red,
                    xData = TimeArray,
                    yData = dynamics.IList,
                    yLabel = $"I ({Util.GetUoM(Const.UoM, Measure.Current)})"
                });
            }
            int numCharts = charts.Any() ? charts.Count : 1;
            string html = DyChartGenerator.PlotLineCharts(charts,
                "Dynamics Test Results", synchronized: true,
                webViewPlots.ClientSize.Width,
                (webViewPlots.ClientSize.Height - 150) / numCharts);
            webViewPlots.NavigateToString(html);
        }
        private void CreatePlots(Dictionary<string, DynamicsStats> dynamicsList, List<string> columnNames, double[,] I)
        {
            List<ChartDataStruct> charts = new();
            foreach (string key in dynamicsList.Keys)
            {
                DynamicsStats dynamics = dynamicsList[key];
                string firingPattern = dynamics.FiringPattern.ToString();
                string firingRhythm = dynamics.FiringRhythm.ToString();

                charts.Add(new ChartDataStruct
                {
                    Title = $"{key} ({firingRhythm} {firingPattern})",
                    Color = Color.Purple,
                    xData = TimeArray,
                    yData = dynamics.VList,
                    yLabel = "V (mV)"
                });
            }
            charts.Add(new ChartDataStruct
            {
                Title = $"Stimulus",
                Color = Color.Red,
                xData = TimeArray,
                yMultiData = I,
                yLabel = string.Join(',', columnNames)
            });
            int numCharts = charts.Any() ? charts.Count : 1;
            string html = DyChartGenerator.PlotLineCharts(charts,
                "Dynamics Test Results", synchronized: true,
                webViewPlots.ClientSize.Width,
                (webViewPlots.ClientSize.Height - 150) / numCharts);
            webViewPlots.NavigateToString(html);
        }
        private void DynamicsRun()
        {
            ReadParameters();

            decimal dt = edt.Value;
            RunParam.static_Skip = 0;
            RunParam.static_dt = (double)dt;
            int stimStart = (int)(eStepStartTime.Value / dt);
            int stimEnd = (int)(eStepEndTime.Value / dt);
            int plotEnd = (int)(ePlotEndTime.Value / dt);
            TimeArray = new double[plotEnd + 1];
            TimeLine tl = new();
            tl.AddTimeRange((int)eStepStartTime.Value, (int)eStepEndTime.Value);
            foreach (int i in Enumerable.Range(0, plotEnd + 1))
                TimeArray[i] = i * (double)dt;
            DynamicUnit core = DynamicUnit.CreateCore(coreType, parameters);
            if (core != null)
            {
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
                    dynamics = core.DynamicsTest(I);
                    CreatePlots();
                }
                else
                {
                    dynamics = null;
                    Dictionary<string, DynamicsStats> dynamicsList = new();
                    List<string> columnNames = new();
                    if (double.TryParse(eRheobase.Text, out double rheobase))
                    {
                        double[,] I = new double[plotEnd + 1, Const.RheobaseTestMultipliers.Length];
                        int iter = 0;
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
                            //cell.InitDataVectors(plotEnd + 1);
                            foreach (int i in Enumerable.Range(stimStart, stimEnd - stimStart))
                                I[i, iter] = stim.generateStimulus(i, SwimmingModel.rand);
                            dynamicsList.Add($"V - Rheobase x {multiplier:0.##}", core.DynamicsTest(I.GetColumn(iter++)));
                            columnNames.Add($"x {multiplier:0.##}");
                        }
                        CreatePlots(dynamicsList, columnNames, I);
                    }
                    else
                        MessageBox.Show("Invalid rheobase. Check the upper limit of rheobase and try again.");
                }
            }
        }
        private void btnDynamicsRun_Click(object sender, EventArgs e)
        {
            DynamicsRun();
        }

        private void CalculateRheobase()
        {
            ReadParameters();

            //TODO muscle cell contraction - how to we handle it?
            DynamicUnit core = DynamicUnit.CreateCore(coreType, parameters);
            decimal limit = eRheobaseLimit.Value;
            decimal d = (decimal)core.CalculateRheoBase((double)limit, Math.Pow(0.1, 3), (int)eRheobaseDuration.Value, (double)edt.Value);
            if (d > 0)
                eRheobase.Text = d.ToString(Const.DecimalPointFormat);
            else
                eRheobase.Text = "N/A";
        }
        private void btnRheobase_Click(object sender, EventArgs e)
        {
            CalculateRheobase();
        }

        //update each parameter sequentially within the range [/10, *2]
        private void btnSensitivityAnalysis_Click(object sender, EventArgs e)
        {
            double limit = (double)eRheobaseLimit.Value;
            List<ChartDataStruct> charts = new();
            double dt = (double)edt.Value;
            ReadParameters();
            Dictionary<string, double> paramToTest = parameters;
            if (ddParameter.SelectedIndex > 0)
            {
                string param = ddParameter.SelectedItem.ToString();
                paramToTest = parameters.Where(kvp => kvp.Key.ToString() == param).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
            DynamicUnit core = DynamicUnit.CreateCore(coreType, parameters);

            foreach (string param in paramToTest.Keys)//change one parameter at a time
            {
                (double[] values, double[] rheos) = core.RheobaseSensitivityAnalysis(param, cbLogScale.Checked,
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
            //TODO muscle cell rheobase 
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
            ReadParameters();
            useUpdatedParams?.Invoke(this, EventArgs.Empty);
        }

        private void rbManualEntryStimulus_CheckedChanged(object sender, EventArgs e)
        {
            stimulusControl1.Enabled = rbManualEntryStimulus.Checked;
            grPlotSelection.Enabled = rbManualEntryStimulus.Checked && !OptimizationMode;
            DynamicsRun();
        }

        private void linkLoadCoreUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                string JSONString = Util.ReadFromFile(openFileJson.FileName);
                if (string.IsNullOrEmpty(JSONString))
                    return;
                DynamicUnit core = DynamicUnit.GetOfDerivedType(JSONString);
                if (core != null)
                {
                    coreType = core.CoreType;
                    Parameters = core.GetParameters();
                }
            }
        }

        private void linkSaveCoreUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReadParameters();
            DynamicUnit core = DynamicUnit.CreateCore(coreType, parameters);
            string JSONString = Util.CreateJSONFromObject(core);
            if (saveFileJson.ShowDialog() == DialogResult.OK)
            {
                Util.SaveToFile(saveFileJson.FileName, JSONString);
            }
        }

        private void cbPlotSelection_CheckedChanged(object sender, EventArgs e)
        {
            CreatePlots();
        }

        private void ReadMinMaxParamValues()
        {
            minValues = new();
            maxValues = new();
            foreach (DataGridViewRow row in dgMinMaxValues.Rows)
            {
                string param = row.Cells[colParameter.Index].Value?.ToString();
                double value = parameters[param];
                if (!string.IsNullOrEmpty(row.Cells[colMinValue.Index].Value?.ToString())
                    && double.TryParse(row.Cells[colMinValue.Index].Value.ToString(), out double dmin))
                    minValues.Add(param, dmin);
                else minValues.Add(param, value);
                if (!string.IsNullOrEmpty(row.Cells[colMaxValue.Index].Value?.ToString())
                    && double.TryParse(row.Cells[colMaxValue.Index].Value.ToString(), out double dmax))
                    maxValues.Add(param, dmax);
                else maxValues.Add(param, value);
            }
        }

        private void ReadFitnessValues()
        {
            firingRhythms = new();
            firingPatterns = new();
            foreach (DataGridViewRow row in dgFitnessParams.Rows)
            {
                if (double.TryParse(row.Cells[colFitnessRheobaseMultiplier.Index].Value?.ToString(), out double mult))
                {
                    if (Enum.TryParse(row.Cells[colFitnessFiringMode.Index].Value?.ToString(), out FiringPattern pattern))
                        firingPatterns.Add(mult, pattern);
                    if (Enum.TryParse(row.Cells[colFitnessFiringRhythm.Index].Value?.ToString(), out FiringRhythm rhythm))
                        firingRhythms.Add(mult, rhythm);
                }
            }
        }
        private void CreateSolver()
        {
            Solver = null;
            if (string.IsNullOrEmpty(eMinTargetRheobase.Text) && !string.IsNullOrEmpty(eMaxTargetRheobase.Text))
                eMinTargetRheobase.Text = eMaxTargetRheobase.Text;
            else if (!string.IsNullOrEmpty(eMinTargetRheobase.Text) && string.IsNullOrEmpty(eMaxTargetRheobase.Text))
                eMaxTargetRheobase.Text = eMinTargetRheobase.Text;

            if (!double.TryParse(eMinTargetRheobase.Text, out double targetRheobaseMin))
            {
                eMinTargetRheobase.Focus();
                MessageBox.Show("Please enter a valid target rheobase value.");
                return;
            }
            if (!double.TryParse(eMaxTargetRheobase.Text, out double targetRheobaseMax))
            {
                eMaxTargetRheobase.Focus();
                MessageBox.Show("Please enter a valid target rheobase value.");
                return;
            }
            ReadParameters();
            ReadMinMaxParamValues();
            ReadFitnessValues();
            if (!int.TryParse(eMinChromosome.Text, out int minPopulation))
                minPopulation = 50;
            if (!int.TryParse(eMaxChromosome.Text, out int maxPopulation))
                maxPopulation = 50;
            Solver = new((Type)cbGASelection.SelectedItem,
                (Type)cbGACrossOver.SelectedItem,
                (Type)cbGAMutation.SelectedItem,
                (Type)cbGATermination.SelectedItem);
            Solver.SetOptimizationSettings(minPopulation, maxPopulation,
                coreType,
                parameters,
                targetRheobaseMin, targetRheobaseMax,
                firingPatterns, firingRhythms,
                minValues, maxValues);

        }
        private void RunOptimize()
        {
            if (Solver == null) return;
            SolverBestValues = Solver.Optimize();
            Invoke(CompleteOptimization);
        }

        private void CompleteOptimization()
        {
            timerOptimization.Enabled = false;
            linkOptimize.Visible = true;
            linkStopOptimization.Visible = false;
            updateParamNames = false;
            Parameters = SolverBestValues;
            updateParamNames = true;
        }
        private void linkOptimize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateSolver();
            timerOptimization.Enabled = true;
            linkOptimize.Visible = false;
            linkStopOptimization.Visible = true;
            eOptimizationOutput.Text = "";
            Task.Run(RunOptimize);
        }
        private void linkStopOptimization_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timerOptimization.Enabled = false;
            Solver?.CancelOptimization();
            linkOptimize.Visible = true;
            linkStopOptimization.Visible = false;
        }

        private void linkSuggestMinMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DynamicUnit core = DynamicUnit.CreateCore(coreType, parameters);

            if (core == null) return;

            (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) = core.GetSuggestedMinMaxValues();

            dgMinMaxValues.Rows.Clear();
            dgMinMaxValues.RowCount = parameters.Count;
            int rowIndex = 0;
            foreach (string key in parameters.Keys)
            {
                dgMinMaxValues[colParameter.Index, rowIndex].Value = key;
                dgMinMaxValues[colMinValue.Index, rowIndex].Value = MinValues[key];
                dgMinMaxValues[colMaxValue.Index, rowIndex].Value = MaxValues[key];
                rowIndex++;
            }
        }

        private void linkSwitchToOptimization_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OptimizationMode = !OptimizationMode;
        }

        private void timerOptimization_Tick(object sender, EventArgs e)
        {
            if (Solver == null) return;
            eOptimizationOutput.Text += Solver.GetProgress() + "\r\n";
        }

        private void dgFitnessParams_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        //https://stackoverflow.com/questions/19406042/databinding-datagridviewcomboboxcolumn-with-enum
        private void dgFitnessParams_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == colFitnessFiringMode.Index)
                    dgFitnessParams[colFitnessFiringMode.Index, e.RowIndex].Value = (FiringPattern)Enum.Parse(typeof(FiringPattern), dgFitnessParams[colFitnessFiringMode.Index, e.RowIndex].Value.ToString());
                else if (e.ColumnIndex == colFitnessFiringRhythm.Index)
                    dgFitnessParams[colFitnessFiringRhythm.Index, e.RowIndex].Value = (FiringRhythm)Enum.Parse(typeof(FiringRhythm), dgFitnessParams[colFitnessFiringRhythm.Index, e.RowIndex].Value.ToString());
            }
            catch { }
        }


        private void dgFitnessParams_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                if (e.Row.DataBoundItem != null)
                {
                    e.Row.Cells[colFitnessFiringRhythm.Index].Value = (FiringRhythm)Enum.Parse(typeof(FiringRhythm), e.Row.Cells[colFitnessFiringRhythm.Index].Value.ToString());
                    e.Row.Cells[colFitnessFiringMode.Index].Value = (FiringPattern)Enum.Parse(typeof(FiringPattern), e.Row.Cells[colFitnessFiringMode.Index].Value.ToString());
                }
            }
            catch { }
        }
    }
}
