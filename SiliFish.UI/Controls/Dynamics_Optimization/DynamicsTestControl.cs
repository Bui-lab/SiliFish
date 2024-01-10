
using Extensions;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.Firing;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Repositories;
using SiliFish.Services.Optimization;
using SiliFish.Services.Plotting;
using SiliFish.UI.EventArguments;
using SiliFish.UI.Extensions;
using SiliFish.UI.Services;
using System.ComponentModel;

namespace SiliFish.UI.Controls
{
    public partial class DynamicsTestControl : UserControl
    {
        private static string coreUnitFileDefaultFolder;
        private Random Random = new();
        private string coreType;
        private double deltaT;
        private string tempFolder;
        private string outputFolder;
        private event EventHandler contentChanged;
        public double DeltaT { get => deltaT; set { deltaT = value; gaControl.DeltaT = value; } }
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

        public event EventHandler ContentChanged
        {
            add
            {
                contentChanged += value;
                gaControl.ContentChanged += value;
            }
            remove
            {
                contentChanged -= value;
                gaControl.ContentChanged -= value;
            }
        }
        internal class UpdatedParamsEventArgs : EventArgs
        {
            internal string CoreType;
            internal Dictionary<string, Distribution> ParamsAsDistribution;
            internal Dictionary<string, double> ParamsAsDouble;
        }

        public event EventHandler UseUpdatedParametersRequested;
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
                GenerateCoreControls();
            }
        }

        private void GenerateCoreControls()
        {
            if (parameters == null)
            {
                pfParams.Controls.Clear();
                sensitivityAnalysisRheobase.SetParameters(false, null);
                sensitivityAnalysisFiring.SetParameters(false, null);
            }
            else
            {
                CellCore core = CellCore.CreateCore(coreType, null);
                Dictionary<string, string> descDict = core.GetParameterDescriptions();
                pfParams.CreateNumericUpDownControlsForDictionary(parameters, descDict, toolTip1);
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
                ddCoreType.Items.AddRange(CellCore.GetCoreTypes().ToArray());
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
            pTop.Visible = !testMode;

            rbRheobaseBasedStimulus.Text = $"Use Rheobase ({string.Join(", ", GlobalSettings.RheobaseTestMultipliers.Select(mult => "x" + mult.ToString()))})";
            eRheobaseLimit.Value = GlobalSettings.RheobaseLimit;
            eRheobaseDuration.Value = GlobalSettings.RheobaseInfinity;
            eStepEndTime.Value = GlobalSettings.RheobaseInfinity + eStepStartTime.Value;
            splitGAAndPlots.Panel1Collapsed = true;
            gaControl.OnCompleteOptimization += GaControl_OnCompleteOptimization;
            gaControl.OnTriggerOptimization += GaControl_OnTriggerOptimization;
            gaControl.OnLoadParams += GaControl_OnLoadParams;
            gaControl.OnGetParams += GaControl_OnGetParams;
            sensitivityAnalysisRheobase.RunAnalysis += SensitivityAnalysisRheobase_RunAnalysis;
            sensitivityAnalysisFiring.RunAnalysis += SensitivityAnalysisFiring_RunAnalysis;
            if (string.IsNullOrEmpty(tempFolder))
                tempFolder = Path.GetTempPath() + "SiliFish";
            if (string.IsNullOrEmpty(outputFolder))
                outputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
        }


        private void SensitivityAnalysisFiring_RunAnalysis(object sender, EventArgs e)
        {
            List<Chart> charts = new();
            ReadParameters();
            string param = sensitivityAnalysisFiring.SelectedParam;
            CellCore core = CellCore.CreateCore(CoreType, parameters, deltaT);

            double[] values = sensitivityAnalysisFiring.GetValues(parameters[param]);
            double[] I = GenerateStimulus(stimulusControl1.GetStimulusSettings());
            DynamicsStats[] stats = core.FiringAnalysis(param, values, I);
            for (int iter = 0; iter < values.Length; iter++)
            {
                DynamicsStats stat = stats[iter];
                charts.Add(new Chart
                {
                    Title = values[iter].ToString("0.###"),
                    Colors = new() { Color.Purple },
                    xData = TimeArray,
                    yData = stat.VList,
                    xLabel = param,
                    yLabel = "V (mV)"
                });
            }
            int height = (webViewPlots.ClientSize.Height - 150) / charts.Count;
            if (height < 200) height = 200;
            string html = DyChartGenerator.PlotLineCharts(charts,
                "Firing Analysis", synchronized: true, showZeroValues: GlobalSettings.ShowZeroValues,
                webViewPlots.ClientSize.Width,
                height);
            string tempFile = "";
            bool navigated = false;
            webViewPlots.NavigateTo(html, "RheobaseAnalysis", tempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
        }

        private void SensitivityAnalysisRheobase_RunAnalysis(object sender, EventArgs e)
        {
            double limit = (double)eRheobaseLimit.Value;
            List<Chart> charts = new();
            ReadParameters();
            Dictionary<string, double> paramToTest = parameters;
            string selectedParam = sensitivityAnalysisRheobase.SelectedParam;
            if (parameters.ContainsKey(selectedParam))
                paramToTest = parameters.Where(kvp => kvp.Key.ToString() == selectedParam).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            CellCore core = CellCore.CreateCore(CoreType, parameters, DeltaT);

            foreach (string param in paramToTest.Keys)//change one parameter at a time
            {
                double[] values = sensitivityAnalysisRheobase.GetValues(parameters[param]);

                double[] rheos = core.RheobaseSensitivityAnalysis(param, values, deltaT, maxRheobase: limit, sensitivity: Math.Pow(0.1, 3), infinity: (int)eRheobaseDuration.Value);
                charts.Add(new Chart
                {
                    Title = param,
                    Colors = new() { Color.Purple },
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
                "Rheobase Sensitivity Analysis", synchronized: false, showZeroValues: GlobalSettings.ShowZeroValues,
                webViewPlots.ClientSize.Width,
                height);
            string tempFile = "";
            bool navigated = false;
            webViewPlots.NavigateTo(html, "FiringAnalysis", tempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);

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

        private void GaControl_OnTriggerOptimization(object sender, EventArgs e)
        {
            Parameters = gaControl.Parameters;
        }

        private void FirstRun()
        {
            if (parameters != null)
            {
                CalculateRheobase();
                StimulusSettings stim = stimulusControl1.GetStimulusSettings();
                if (double.TryParse(eRheobase.Text, out double d))
                {
                    stim.Value1 = d;
                    stimulusControl1.SetStimulusSettings(stim);
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
            DeltaT = (double)edt.Value;
        }

        #region Plotting
        private void CreatePlots()
        {
            if (dynamics == null)
                return;
            List<Chart> charts = new();
            string firingPattern = dynamics.FiringPattern.ToString();
            string firingRhythm = dynamics.FiringRhythm.ToString();
            if (cbV.Checked)
            {
                charts.Add(new Chart
                {
                    Title = $"V ({firingRhythm} {firingPattern})",
                    Colors = new() { Color.Purple },
                    xData = TimeArray,
                    yData = dynamics.VList,
                    yLabel = "V (mV)"
                });
            }
            if (cbSecondaryLists.Checked)
            {
                foreach (string key in dynamics.SecLists.Keys)
                {
                    charts.Add(new Chart
                    {
                        Title = key,
                        Colors = new() { Color.Blue },
                        xData = TimeArray,
                        yData = dynamics.SecLists[key],
                        yLabel = key
                    });
                }
            }

            if (cbTauRise.Checked && dynamics.TauRise.Any())
            {
                charts.Add(new Chart
                {
                    Title = "Tau rise",
                    Colors = new() { Color.Green },
                    xData = dynamics.TauRise.Keys.ToArray(),
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yMin = 0,
                    yData = dynamics.TauRise.Values.ToArray(),
                    yLabel = "Tau (ms)",
                    drawPoints = true
                });
            }
            if (cbTauDecay.Checked && dynamics.TauDecay.Any())
            {
                charts.Add(new Chart
                {
                    Title = "Tau decay",
                    Colors = new() { Color.Green },
                    xData = dynamics.TauDecay.Keys.ToArray(),
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yMin = 0,
                    yData = dynamics.TauDecay.Values.ToArray(),
                    yLabel = "Tau (ms)",
                    drawPoints = true
                });
            }
            if (cbInterval.Checked && dynamics.Intervals_ms != null && dynamics.Intervals_ms.Any())
            {
                charts.Add(new Chart
                {
                    Title = "Intervals",
                    Colors = new() { Color.Blue },
                    xData = dynamics.Intervals_ms.Keys.ToArray(),
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yMin = 0,
                    yData = dynamics.Intervals_ms.Values.ToArray(),
                    yLabel = "Interval (ms)",
                    drawPoints = true
                });
            }
            if (cbSpikingFrequency.Checked)
            {
                if (dynamics.SpikeFrequency_Grouped != null && dynamics.SpikeFrequency_Grouped.Any())
                    charts.Add(new Chart
                    {
                        Title = "Spiking Freq.",
                        Colors = new() { Color.Blue },
                        xData = dynamics.SpikeFrequency_Grouped.Keys.Select(ff => ff).ToArray(),
                        xMin = 0,
                        xMax = TimeArray[^1],
                        yMin = 0,
                        yData = dynamics.SpikeFrequency_Grouped.Values.Select(ff => ff.Freq).ToArray(),
                        yLabel = "Freq (Hz)",
                        drawPoints = true
                    });
                if (dynamics.BurstingFrequency_Grouped != null && dynamics.BurstingFrequency_Grouped.Any())
                    charts.Add(new Chart
                    {
                        Title = "Burst Freq.",
                        Colors = new() { Color.Blue },
                        xData = dynamics.BurstingFrequency_Grouped.Keys.Select(ff => ff).ToArray(),
                        xMin = 0,
                        xMax = TimeArray[^1],
                        yMin = 0,
                        yData = dynamics.BurstingFrequency_Grouped.Values.Select(ff => ff.Freq).ToArray(),
                        yLabel = "Freq (Hz)",
                        drawPoints = true
                    });

            }
            if (cbStimulus.Checked)
            {
                charts.Add(new Chart
                {
                    Title = "Stimulus",
                    Colors = new() { Color.Red },
                    xData = TimeArray,
                    yData = dynamics.StimulusArray,
                    yLabel = $"I ({Util.GetUoM(UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens, Measure.Current)})"
                });
            }
            int numCharts = charts.Any() ? charts.Count : 1;
            string mainTitle = "Dynamics Test Results";
            string html = DyChartGenerator.PlotLineCharts(charts,
                mainTitle, synchronized: true, showZeroValues: GlobalSettings.ShowZeroValues,
                webViewPlots.ClientSize.Width,
                (webViewPlots.ClientSize.Height - 150) / numCharts);
            string tempFile = "";
            bool navigated = false;
            webViewPlots.NavigateTo(html, mainTitle, tempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
        }
        private void CreatePlots(Dictionary<string, DynamicsStats> dynamicsList, List<string> columnNames, List<double[]> I)
        {
            List<Chart> charts = new();
            foreach (string key in dynamicsList.Keys)
            {
                DynamicsStats dynamics = dynamicsList[key];
                string firingPattern = dynamics.FiringPattern.ToString();
                string firingRhythm = dynamics.FiringRhythm.ToString();

                charts.Add(new Chart
                {
                    Title = $"{key} ({firingRhythm} {firingPattern})",
                    Colors = new() { Color.Purple },
                    xData = TimeArray,
                    yData = dynamics.VList,
                    yLabel = "V (mV)"
                });
            }
            charts.Add(new Chart
            {
                Title = $"Stimulus",
                Colors = new() { Color.Red },
                xData = TimeArray,
                yMultiData = I,
                yLabel = string.Join(',', columnNames)
            });
            int numCharts = charts.Any() ? charts.Count : 1;
            string mainTitle = "Dynamics Test Results";
            string html = DyChartGenerator.PlotLineCharts(charts,
                mainTitle, synchronized: true, showZeroValues: GlobalSettings.ShowZeroValues,
                webViewPlots.ClientSize.Width,
                (webViewPlots.ClientSize.Height - 150) / numCharts);
            string tempFile = "";
            bool navigated = false;
            webViewPlots.NavigateTo(html, mainTitle, tempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
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
            Parameters = CellCore.GetParameters(CoreType).ToDictionary(kvp => kvp.Key, kvp => double.Parse(kvp.Value.ToString()));
        }

        private void stimulusControl1_StimulusChanged(object sender, EventArgs e)
        {
            if (cbAutoDrawPlots.Checked)
                DynamicsRun();
        }

        private double[] GenerateStimulus(StimulusSettings stimulusSettings)
        {
            decimal ddt = edt.Value;
            DeltaT = (double)ddt;
            int plotEnd_ms = (int)ePlotEndTime.Value;
            int plotEnd = (int)(plotEnd_ms / ddt);
            TimeArray = new double[plotEnd + 1];
            TimeLine tl = new();
            tl.AddTimeRange((int)eStepStartTime.Value, (int)eStepEndTime.Value);
            foreach (int i in Enumerable.Range(0, plotEnd + 1))
                TimeArray[i] = i * deltaT;
            RunParam runParam = new()
            {
                MaxTime = plotEnd_ms,
                DeltaT = DeltaT
            };
            Stimulus stim = new()
            {
                Settings = stimulusSettings,
                TimeLine_ms = tl
            };
            stim.InitForSimulation(runParam, Random);
            double[] I = stim.GetValues(plotEnd);
            return I;
        }
        private void DynamicsRun()
        {
            ReadParameters();
            CellCore core = CellCore.CreateCore(CoreType, parameters, deltaT);
            if (core != null)
            {
                if (rbSingleEntryStimulus.Checked)
                {
                    double[] I = GenerateStimulus(stimulusControl1.GetStimulusSettings());
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
                            stimValues = GlobalSettings.RheobaseTestMultipliers.Select(m => m * rheobase).ToList();
                            columnNames.AddRange(GlobalSettings.RheobaseTestMultipliers.Select(m => $"x {m:0.##}").ToList());
                        }
                    }
                    else //if (rbMultipleEntry.Checked)
                    {
                        string[] multValues = eMultipleStimulus.Text.Split(new char[] { ';', ' ', ':' });
                        foreach (string s in multValues)
                            if (double.TryParse(s, out double d))
                            {
                                stimValues.Add(d);
                                columnNames.Add($"Stim_{columnNames.Count + 1}");
                            }
                    }
                    if (stimValues.Any())
                    {
                        List<double[]> I = new();
                        int iter = 0;
                        foreach (double stim in stimValues.Distinct())
                        {
                            StimulusSettings stimulusSettings = new()
                            {
                                Mode = StimulusMode.Step,
                                Value1 = stim,
                                Value2 = 0
                            };
                            I.Add(GenerateStimulus(stimulusSettings));
                            dynamicsList.Add($"Stimulus: {stim:0.#####}", core.DynamicsTest(I[iter++]));
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
            CellCore core = CellCore.CreateCore(CoreType, parameters, deltaT);
            decimal limit = eRheobaseLimit.Value;
            decimal d = (decimal)core.CalculateRheoBase((double)limit, Math.Pow(0.1, 3), (int)eRheobaseDuration.Value, (double)edt.Value);
            if (d >= 0)
            {
                eRheobase.Text = d.ToString(GlobalSettings.PlotDataFormat);
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

        private void rbSingleEntryStimulus_CheckedChanged(object sender, EventArgs e)
        {
            stimulusControl1.Enabled = rbSingleEntryStimulus.Checked;
            grPlotSelection.Enabled = rbSingleEntryStimulus.Checked && !OptimizationMode;
            if (rbSingleEntryStimulus.Checked && cbAutoDrawPlots.Checked)
                DynamicsRun();
        }

        private void rbMultipleEntry_CheckedChanged(object sender, EventArgs e)
        {
            eMultipleStimulus.ReadOnly = !rbMultipleEntry.Checked;
            if (rbMultipleEntry.Checked && eMultipleStimulus.Text == "example: 1;10")
                eMultipleStimulus.Text = "";
            else if (!rbMultipleEntry.Checked && string.IsNullOrEmpty(eMultipleStimulus.Text))
                eMultipleStimulus.Text = "example: 1;10";
            if (rbMultipleEntry.Checked && cbAutoDrawPlots.Checked)
                DynamicsRun();
        }

        private void rbRheobaseBasedStimulus_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRheobaseBasedStimulus.Checked && cbAutoDrawPlots.Checked)
                DynamicsRun();
        }

        private string lastMultipleStimulus = null;
        private void eMultipleStimulus_Enter(object sender, EventArgs e)
        {
            lastMultipleStimulus = eMultipleStimulus.Text;
        }

        private void eMultipleStimulus_Leave(object sender, EventArgs e)
        {
            if (lastMultipleStimulus != eMultipleStimulus.Text && cbAutoDrawPlots.Checked)
                DynamicsRun();
        }

        #region Load/Save
        private void linkUseUpdatedParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReadParameters();
            UpdatedParamsEventArgs args = new()
            {
                CoreType = CoreType,
                ParamsAsDistribution = Parameters.ToDictionary(kvp => kvp.Key, kvp => new Constant_NoDistribution(kvp.Value) as Distribution),
                ParamsAsDouble = Parameters
            };
            UseUpdatedParametersRequested?.Invoke(this, args);
        }
        private void linkLoadCoreUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileJson.InitialDirectory = coreUnitFileDefaultFolder;
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileJson.FileName;
                coreUnitFileDefaultFolder = Path.GetDirectoryName(fileName);
                CellCore core = CellCoreUnitFile.Load(fileName);
                if (core != null)
                {
                    skipCoreTypeChange = true;
                    ddCoreType.Text = core.CoreType;
                    skipCoreTypeChange = false;
                    CoreType = core.CoreType;
                    Parameters = core.GetParameters();
                    ContentChangedArgs args = new()
                    { Caption = $"Core File: {Path.GetFileNameWithoutExtension(fileName)}" };
                    contentChanged?.Invoke(this, args);
                }
                else
                {
                    MessageBox.Show("Selected file is not a valid Core file.", "Error");
                }
            }
        }


        private void linkSaveCoreUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReadParameters();
            CellCore core = CellCore.CreateCore(CoreType, parameters, deltaT);
            saveFileJson.InitialDirectory = coreUnitFileDefaultFolder;
            if (saveFileJson.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileJson.FileName;
                CellCoreUnitFile.Save(fileName, core);
                coreUnitFileDefaultFolder = Path.GetDirectoryName(fileName);
                ContentChangedArgs args = new()
                { Caption = $"Core File: {Path.GetFileNameWithoutExtension(fileName)}" };
                contentChanged?.Invoke(this, args);
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


    }
}
