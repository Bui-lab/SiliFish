
using Extensions;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.Services;
using SiliFish.Services.Optimization;
using SiliFish.UI.Extensions;

namespace SiliFish.UI.Controls
{
    public partial class DynamicsTestControl : UserControl
    {
        private static string coreUnitFileDefaultFolder;
        private string coreType;
        public string CoreType
        {
            get { return coreType; }
            set 
            { 
                coreType = value;
                gaControl.CoreType = coreType;
            }
        }
        private bool updateParamNames = true;
        private bool skipCoreTypeChange = false;
        DynamicsStats dynamics;
        private double[] TimeArray;

        internal class UpdatedParamsEventArgs : EventArgs
        {
            internal string CoreType;
            internal Dictionary<string, object> ParamsAsObject;
            internal Dictionary<string, double> ParamsAsDouble;
        }
        private event EventHandler useUpdatedParams;
        public event EventHandler UseUpdatedParams { add => useUpdatedParams += value; remove => useUpdatedParams -= value; }
        private Dictionary<string, double> parameters;


        private bool OptimizationMode
        {
            get { return linkSwitchToOptimization.Text != "Optimization Mode"; }
            set
            {
                grPlotSelection.Enabled = rbSingleEntryStimulus.Checked && !value;
                splitGAAndPlots.Panel1Collapsed = !value;
                linkSwitchToOptimization.Text = value ? "Quit Optimization Mode" : "Optimization Mode";
            }
        }
        public Dictionary<string, double> Parameters
        {
            get => parameters;
            set
            {
                parameters = value;
                gaControl.Parameters = parameters;
                if (parameters == null)
                {
                    pfParams.Controls.Clear();
                    sensitivityAnalysisRheobase.SetParameters(false, null);
                    sensitivityAnalysisFiring.SetParameters(false, null);
                }
                else
                {
                    pfParams.CreateNumericUpDownControlsForDictionary(parameters);
                    foreach (Control control in pfParams.Controls)
                    {
                        if (control is TextBox textBox)
                            textBox.TextChanged += TextBox_TextChanged;
                        else if (control is NumericUpDown numBox)
                            numBox.ValueChanged += NumBox_ValueChanged;
                    }
                    if (updateParamNames)
                    {
                        updateParamNames = false;
                        sensitivityAnalysisRheobase.SetParameters(true, parameters.Keys.ToArray());
                        sensitivityAnalysisFiring.SetParameters(false, parameters.Keys.ToArray());
                        gaControl.ResetParameters(parameters);
                    }
                    FirstRun();
                }
            }
        }

        private void NumBox_ValueChanged(object sender, EventArgs e)
        {
            if (cbAutoDrawPlots.Checked && sender is NumericUpDown)
            {
                CalculateRheobase();
                DynamicsRun();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (cbAutoDrawPlots.Checked && sender is TextBox textBox)
            {
                if (double.TryParse(textBox.Text, out double _))
                {
                    CalculateRheobase();
                    DynamicsRun();
                }
            }
        }

        private void FillCoreTypes(string def = null)
        {
            if (ddCoreType.Items.Count == 0)
                ddCoreType.Items.AddRange(CellCoreUnit.GetCoreTypes().ToArray());
            if (!string.IsNullOrEmpty(def))
                ddCoreType.Text = def;
            if (ddCoreType.SelectedIndex < 0)
                ddCoreType.SelectedIndex = 0;
        }
        public DynamicsTestControl(string coreType, Dictionary<string, double> parameters, bool testMode)
        {
            InitializeComponent();
            InitAsync();
            Wait();
            FillCoreTypes(coreType);
            if (parameters != null)
                Parameters = parameters;
            pBottomBottom.Visible = !testMode;
            rbRheobaseBasedStimulus.Text = $"Use Rheobase ({string.Join(", ", Settings.RheobaseTestMultipliers.Select(mult => "x" + mult.ToString()))})";
          
            splitGAAndPlots.Panel1Collapsed = true;
            gaControl.OnCompleteOptimization += GaControl_OnCompleteOptimization;
            gaControl.OnLoadParams += GaControl_OnLoadParams;
            gaControl.OnGetParams += GaControl_OnGetParams;
            sensitivityAnalysisRheobase.RunAnalysis += SensitivityAnalysisRheobase_RunAnalysis;
            sensitivityAnalysisFiring.RunAnalysis += SensitivityAnalysisFiring_RunAnalysis;
        }

        private void SensitivityAnalysisFiring_RunAnalysis(object sender, EventArgs e)
        {
            List<ChartDataStruct> charts = new();
            ReadParameters();
            string param = sensitivityAnalysisFiring.SelectedParam;
            CellCoreUnit core = CellCoreUnit.CreateCore(CoreType, parameters);

            double[] values = sensitivityAnalysisFiring.GetValues(parameters[param]);
            double[] I  = GenerateStimulus();
            DynamicsStats[] stats = core.FiringAnalysis(param, values, I);
            for (int iter = 0; iter < values.Length; iter++)
            {
                DynamicsStats stat = stats[iter];
                charts.Add(new ChartDataStruct
                {
                    Title = values[iter].ToString("0.###"),
                    Color = Color.Purple.ToRGBQuoted(),
                    xData = TimeArray,
                    yData = stat.VList,
                    xLabel = param,
                    yLabel = "V (mV)"
                });
            }
            int height = (webViewPlots.ClientSize.Height - 150) / charts.Count;
            if (height < 200) height = 200;
            string html = DyChartGenerator.PlotLineCharts(charts,
                "Firing Analysis", synchronized: true,
                webViewPlots.ClientSize.Width,
                height);
            string tempFile = "";
            webViewPlots.NavigateTo(html, Settings.TempFolder, ref tempFile);
        }

        private void SensitivityAnalysisRheobase_RunAnalysis(object sender, EventArgs e)
        {
            double limit = (double)eRheobaseLimit.Value;
            List<ChartDataStruct> charts = new();
            double dt = (double)edt.Value;
            ReadParameters();
            Dictionary<string, double> paramToTest = parameters;
            string selectedParam = sensitivityAnalysisRheobase.SelectedParam;
            if (parameters.ContainsKey(selectedParam))
                paramToTest = parameters.Where(kvp => kvp.Key.ToString() == selectedParam).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            CellCoreUnit core = CellCoreUnit.CreateCore(CoreType, parameters);

            foreach (string param in paramToTest.Keys)//change one parameter at a time
            {
                double[] values = sensitivityAnalysisRheobase.GetValues(parameters[param]);

                double[] rheos = core.RheobaseSensitivityAnalysis(param, values, dt, maxRheobase: limit, sensitivity: Math.Pow(0.1, 3), infinity: (int)eRheobaseDuration.Value);
                charts.Add(new ChartDataStruct
                {
                    Title = param,
                    Color = Color.Purple.ToRGBQuoted(),
                    xData = values,
                    yData = rheos,
                    xLabel = param,
                    yLabel = "Rheobase",
                    drawPoints = true,
                    logScale = sensitivityAnalysisRheobase.LogScale
                });
            }
            int height = (webViewPlots.ClientSize.Height - 150) / charts.Count;
            if (height < 200) height = 200;
            string html = DyChartGenerator.PlotLineCharts(charts,
                "Rheobase Sensitivity Analysis", synchronized: false,
                webViewPlots.ClientSize.Width,
                height);
            string tempFile = "";
            webViewPlots.NavigateTo(html, Settings.TempFolder, ref tempFile);
        }

        private void GaControl_OnLoadParams(object sender, EventArgs e)
        {
            skipCoreTypeChange = true;
            CoreType = gaControl.CoreType;
            ddCoreType.Text = CoreType;
            Parameters = gaControl.Parameters;
            skipCoreTypeChange = false;
        }

        private void GaControl_OnGetParams(object sender, EventArgs e)
        {
            gaControl.CoreType = CoreType;
            gaControl.Parameters = Parameters;
        }

        private void GaControl_OnCompleteOptimization(object sender, EventArgs e)
        {
            Parameters = gaControl.Parameters;
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
            parameters = pfParams.CreateDoubleDictionaryFromControls();
            gaControl.Parameters = parameters;
            double dt = (double)edt.Value;
            RunParam.static_Skip = 0;
            RunParam.static_dt = dt;
            RunParam.static_dt_Euler = (double)edtEuler.Value;
            if (dt < RunParam.static_dt_Euler)
                RunParam.static_dt_Euler = dt;
        }

        #region Plotting
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
                    Color = Color.Purple.ToRGBQuoted(),
                    xData = TimeArray,
                    yData = dynamics.VList,
                    yLabel = "V (mV)"
                });
            }
            if (cbSecondaryLists.Checked)
            {
                foreach (string key in dynamics.SecLists.Keys)
                {
                    charts.Add(new ChartDataStruct
                    {
                        Title = key,
                        Color = Color.Blue.ToRGBQuoted(),
                        xData = TimeArray,
                        yData = dynamics.SecLists[key],
                        yLabel = key
                    });
                }
            }

            if (cbTauRise.Checked && dynamics.TauRise.Any())
            {
                charts.Add(new ChartDataStruct
                {
                    Title = "Tau rise",
                    Color = Color.Green.ToRGBQuoted(),
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
                    Color = Color.Green.ToRGBQuoted(),
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
                    Color = Color.Blue.ToRGBQuoted(),
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
                    Color = Color.Blue.ToRGBQuoted(),
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
                    Color = Color.Red.ToRGBQuoted(),
                    xData = TimeArray,
                    yData = dynamics.StimulusArray,
                    yLabel = $"I ({Util.GetUoM(Settings.UoM, Measure.Current)})"
                });
            }
            int numCharts = charts.Any() ? charts.Count : 1;
            string html = DyChartGenerator.PlotLineCharts(charts,
                "Dynamics Test Results", synchronized: true,
                webViewPlots.ClientSize.Width,
                (webViewPlots.ClientSize.Height - 150) / numCharts);
            string tempFile = "";
            webViewPlots.NavigateTo(html, Settings.TempFolder, ref tempFile);
        }
        private void CreatePlots(Dictionary<string, DynamicsStats> dynamicsList, List<string> columnNames, List<double[]> I)
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
                    Color = Color.Purple.ToRGBQuoted(),
                    xData = TimeArray,
                    yData = dynamics.VList,
                    yLabel = "V (mV)"
                });
            }
            charts.Add(new ChartDataStruct
            {
                Title = $"Stimulus",
                Color = Color.Red.ToRGBQuoted(),
                xData = TimeArray,
                yMultiData = I,
                yLabel = string.Join(',', columnNames)
            });
            int numCharts = charts.Any() ? charts.Count : 1;
            string html = DyChartGenerator.PlotLineCharts(charts,
                "Dynamics Test Results", synchronized: true,
                webViewPlots.ClientSize.Width,
                (webViewPlots.ClientSize.Height - 150) / numCharts);
            string tempFile = "";
            webViewPlots.NavigateTo(html, Settings.TempFolder, ref tempFile);
        }

        private void cbPlotSelection_CheckedChanged(object sender, EventArgs e)
        {
            CreatePlots();
        }
        #endregion
        private void ddCoreType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipCoreTypeChange) return;
            CoreType = ddCoreType.Text;
            updateParamNames = true;
            Parameters = CellCoreUnit.GetParameters(CoreType).ToDictionary(kvp => kvp.Key, kvp => double.Parse(kvp.Value.ToString()));
        }

        private void stimulusControl1_StimulusChanged(object sender, EventArgs e)
        {
            if (cbAutoDrawPlots.Checked)
                DynamicsRun();
        }

        private double[] GenerateStimulus()
        {
            decimal dt = edt.Value;
            RunParam.static_Skip = 0;
            RunParam.static_dt = (double)dt;
            int stimStart = (int)(eStepStartTime.Value / dt);
            int stimEnd = (int)(eStepEndTime.Value / dt);
            int plotEnd = (int)(ePlotEndTime.Value / dt);
            if (stimEnd > plotEnd)
                stimEnd = plotEnd;
            TimeArray = new double[plotEnd + 1];
            TimeLine tl = new();
            tl.AddTimeRange((int)eStepStartTime.Value, (int)eStepEndTime.Value);
            foreach (int i in Enumerable.Range(0, plotEnd + 1))
                TimeArray[i] = i * (double)dt;
            Stimulus stim = new()
            {
                StimulusSettings = stimulusControl1.GetStimulus(),
                TimeSpan_ms = tl
            };
            double[] I = stim.GenerateStimulus(stimStart, stimEnd - stimStart, SwimmingModel.rand).Concat(new double[plotEnd+1 - stimEnd]).ToArray();
            return I;
        }
        private void DynamicsRun()
        {
            ReadParameters();

            decimal dt = edt.Value;

            int stimStart = (int)(eStepStartTime.Value / dt);
            int stimEnd = (int)(eStepEndTime.Value / dt);
            int plotEnd = (int)(ePlotEndTime.Value / dt);
            TimeArray = new double[plotEnd + 1];
            TimeLine tl = new();
            tl.AddTimeRange((int)eStepStartTime.Value, (int)eStepEndTime.Value);
            foreach (int i in Enumerable.Range(0, plotEnd + 1))
                TimeArray[i] = i * (double)dt;
            CellCoreUnit core = CellCoreUnit.CreateCore(CoreType, parameters);
            if (core != null)
            {
                if (rbSingleEntryStimulus.Checked)
                {
                    double[] I = GenerateStimulus();
                    dynamics = core.DynamicsTest(I);
                    CreatePlots();
                }
                else
                {
                    dynamics = null;
                    Dictionary<string, DynamicsStats> dynamicsList = new();
                    List<string> columnNames = new();
                    List<double> stimValues = new();
                    if (rbRheobaseBasedStimulus.Checked)
                    {
                        if (double.TryParse(eRheobase.Text, out double rheobase))
                        {
                            stimValues = Settings.RheobaseTestMultipliers.Select(m => m * rheobase).ToList();
                            columnNames.AddRange(Settings.RheobaseTestMultipliers.Select(m => $"x {m:0.##}").ToList());
                        }
                    }
                    else //if (rbMultipleEntry.Checked)
                    {
                        string[] multValues = eMultipleStimulus.Text.Split(new char[] { ';', ' ', ':', '-' });
                        foreach (string s in multValues)
                            if (double.TryParse(s, out double d))
                            {
                                stimValues.Add(d);
                                columnNames.Add($"Stim_{columnNames.Count+1}");
                            }
                    }
                    if (stimValues.Any())
                    {
                        List<double[]> I = new();
                        int iter = 0;
                        foreach (double stim in stimValues.Distinct())
                        {
                            Stimulus stimulus = new()
                            {
                                StimulusSettings = new()
                                {
                                    Mode = StimulusMode.Step,
                                    Value1 = stim,
                                    Value2 = 0
                                },
                                TimeSpan_ms = tl
                            };
                            I.Add(stimulus.GenerateStimulus(stimStart, stimEnd - stimStart, SwimmingModel.rand).Concat(new double[plotEnd + 1 - stimEnd]).ToArray());
                            dynamicsList.Add($"Stimulus: {stim:0.##}", core.DynamicsTest(I[iter++]));
                        }
                        CreatePlots(dynamicsList, columnNames, I);
                    }
                    else
                    {
                        webViewPlots.NavigateToString("No rheobase within limit.");
                    }
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
            CellCoreUnit core = CellCoreUnit.CreateCore(CoreType, parameters);
            decimal limit = eRheobaseLimit.Value;
            decimal d = (decimal)core.CalculateRheoBase((double)limit, Math.Pow(0.1, 3), (int)eRheobaseDuration.Value, (double)edt.Value);
            if (d >= 0)
            {
                eRheobase.Text = d.ToString(Settings.DecimalPointFormat);
                lRheobaseWarning.Visible = false;
                rbRheobaseBasedStimulus.ForeColor = Color.Black;
            }
            else
            {
                eRheobase.Text = "N/A";
                lRheobaseWarning.Visible = true;
                rbRheobaseBasedStimulus.ForeColor = Color.Red;
            }
        }
        private void btnRheobase_Click(object sender, EventArgs e)
        {
            CalculateRheobase();
        }



        private void rbManualEntryStimulus_CheckedChanged(object sender, EventArgs e)
        {
            stimulusControl1.Enabled = rbSingleEntryStimulus.Checked;
            grPlotSelection.Enabled = rbSingleEntryStimulus.Checked && !OptimizationMode;
            if (cbAutoDrawPlots.Checked)
                DynamicsRun();
        }

        #region Load/Save
        private void linkUseUpdatedParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReadParameters();
            UpdatedParamsEventArgs args = new()
            {
                CoreType = CoreType,
                ParamsAsObject = Parameters.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value),
                ParamsAsDouble = Parameters
            };
            useUpdatedParams?.Invoke(this, args);
        }
        private void linkLoadCoreUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileJson.InitialDirectory = coreUnitFileDefaultFolder;
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                coreUnitFileDefaultFolder = Path.GetDirectoryName(openFileJson.FileName);

                string JSONString = FileUtil.ReadFromFile(openFileJson.FileName);
                if (string.IsNullOrEmpty(JSONString))
                    return;
                CellCoreUnit core = CellCoreUnit.GetOfDerivedType(JSONString);
                if (core != null)
                {
                    skipCoreTypeChange = true;
                    ddCoreType.Text = core.CoreType;
                    skipCoreTypeChange = false;
                    CoreType = core.CoreType;
                    Parameters = core.GetParameters();
                }
            }
        }

        private void linkSaveCoreUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReadParameters();
            CellCoreUnit core = CellCoreUnit.CreateCore(CoreType, parameters);
            string JSONString = JsonUtil.ToJson(core);
            saveFileJson.InitialDirectory = coreUnitFileDefaultFolder;
            if (saveFileJson.ShowDialog() == DialogResult.OK)
            {
                FileUtil.SaveToFile(saveFileJson.FileName, JSONString);
                coreUnitFileDefaultFolder = Path.GetDirectoryName(saveFileJson.FileName);
            }
        }

        #endregion

        #region Optimization


        private void linkSwitchToOptimization_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OptimizationMode = !OptimizationMode;
        }



        #endregion

        private void cbAutoDrawPlots_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoDrawPlots.Checked)
                DynamicsRun();
        }

        private void rbMultipleEntry_CheckedChanged(object sender, EventArgs e)
        {
            eMultipleStimulus.ReadOnly = !rbMultipleEntry.Checked;
        }
    }
}
