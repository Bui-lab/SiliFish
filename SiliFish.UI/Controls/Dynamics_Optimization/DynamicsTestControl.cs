
using Extensions;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Stim;
using SiliFish.Repositories;
using SiliFish.Services;
using SiliFish.Services.Dynamics;
using SiliFish.Services.Plotting;
using SiliFish.UI.EventArguments;
using SiliFish.UI.Extensions;
using SiliFish.UI.Services;

namespace SiliFish.UI.Controls
{
    public partial class DynamicsTestControl : UserControl
    {
        private static string coreUnitFileDefaultFolder;
        private Random Random = new();
        private string coreType;
        private string tempFolder;
        private string outputFolder;
        private event EventHandler contentChanged;
        public double DeltaT
        {
            get => (double)eDeltaT.Value;
            set
            {
                eDeltaT.Value = (decimal)value;
                SetControlDeltaTs(value);
            }
        }
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
                CellCore core = CellCore.CreateCore(coreType, null, DeltaT);
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
                    sensitivityAnalysisRheobase.SetParameters(true, [.. parameters.Keys]);
                    sensitivityAnalysisFiring.SetParameters(false, [.. parameters.Keys]);
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
                ddCoreType.Items.AddRange([.. CellCore.GetCoreTypes()]);
            if (!string.IsNullOrEmpty(def))
                ddCoreType.Text = def;
            if (ddCoreType.SelectedIndex < 0)
                ddCoreType.SelectedIndex = 0;
        }

        private void SetControlDeltaTs(double dt)
        {
            sensitivityAnalysisDeltaT.SetParameter("Delta t (ms)", dt);
            gaControl.DeltaT = dt;
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
            sensitivityAnalysisDeltaT.RunAnalysis += SensitivityAnalysisDeltaT_RunAnalysis;
            sensitivityAnalysisDeltaT.SetParameter("Delta t (ms)", DeltaT);
            if (string.IsNullOrEmpty(tempFolder))
                tempFolder = Path.GetTempPath() + "SiliFish";
            if (string.IsNullOrEmpty(outputFolder))
                outputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
            SetControlDeltaTs(DeltaT);
            pgSettings.SelectedObject = new DynamicsStatsParams();
        }

        private void PlotCharts(List<Chart> charts, string title, bool synchronized = true)
        {
            int height = (webViewPlots.ClientSize.Height - 150) / charts.Count;
            if (height < 200) height = 200;
            string html = DyChartGenerator.PlotLineCharts(charts,
                title, webViewPlots.ClientSize.Width, height, 
                synchronized: synchronized, showZeroValues: GlobalSettings.ShowZeroValues);
            string tempFile = "";
            bool navigated = false;
            webViewPlots.NavigateTo(html, title, tempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
        }
        private void SensitivityAnalysisDeltaT_RunAnalysis(object sender, EventArgs e)
        {
            try
            {
                ReadParameters();
                double[] dtValues = sensitivityAnalysisDeltaT.GetValues();
                List<Chart> charts = DynamicsTest.DeltaTAnalysis(coreType, Parameters,
                    ReadStimulusSettings(), (double)ePlotEndTime.Value, dtValues, Random);
                PlotCharts(charts, "Δt Sensitivity Analysis");
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void SensitivityAnalysisFiring_RunAnalysis(object sender, EventArgs e)
        {
            ReadParameters();
            List<Chart> charts = DynamicsTest.FiringAnalysis(coreType,
                Parameters, sensitivityAnalysisFiring.SelectedParam,
                sensitivityAnalysisFiring.Range,
                ReadStimulusSettings(), (double)ePlotEndTime.Value, DeltaT, Random);
            PlotCharts(charts, "Firing Analysis");
        }

        private void SensitivityAnalysisRheobase_RunAnalysis(object sender, EventArgs e)
        {
            ReadParameters();
            Dictionary<string, double> paramToTest = parameters;
            string selectedParam = sensitivityAnalysisRheobase.SelectedParam;
            if (parameters.ContainsKey(selectedParam))
                paramToTest = parameters.Where(kvp => kvp.Key.ToString() == selectedParam).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            List<Chart> charts = DynamicsTest.RheobaseAnalysis(coreType, paramToTest,
                maxRheobase: (double)eRheobaseLimit.Value, maxDuration: (int)eRheobaseDuration.Value,
                sensitivityAnalysisRheobase.Range, DeltaT);

            PlotCharts(charts, "Rheobase Sensitivity Analysis", synchronized: false);
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

        private StimulusSettings ReadStimulusSettings()
        {
            TimeLine tl = new();
            tl.AddTimeRange((int)eStepStartTime.Value, (int)eStepEndTime.Value);
            StimulusSettings stim = stimulusSettingsControl1.GetStimulusSettings();
            stim.TimeLine_ms = tl;
            return stim;
        }
        private void FirstRun()
        {
            if (parameters != null)
            {
                CalculateRheobase();
                StimulusSettings stim = ReadStimulusSettings(); ;
                if (double.TryParse(eRheobase.Text, out double d))
                {
                    stim.Value1 = d;
                    stimulusSettingsControl1.SetStimulusSettings(stim);
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
            while (!webViewPlots.Initialized)
                Application.DoEvents();

        }
        private void ReadParameters()
        {
            parameters = pfParams.CreateDoubleDictionaryFromControls();
            gaControl.Parameters = parameters;
        }

        #region Plotting
        private void CreatePlots()
        {
            if (dynamics == null)
                return;
            List<Chart> charts = [];
            string firingPattern = dynamics.FiringPattern.ToString();
            string firingRhythm = dynamics.FiringRhythm.ToString();
            if (cbV.Checked)
            {
                charts.Add(new Chart
                {
                    Title = $"V ({firingRhythm} {firingPattern})",
                    Colors = [Color.Purple],
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
                        Colors = [Color.Blue],
                        xData = TimeArray,
                        yData = dynamics.SecLists[key],
                        yLabel = key
                    });
                }
            }

            if (cbTauRise.Checked && dynamics.TauRise.Count != 0)
            {
                charts.Add(new Chart
                {
                    Title = "Tau rise",
                    Colors = [Color.Green],
                    xData = [.. dynamics.TauRise.Keys],
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yMin = 0,
                    yData = [.. dynamics.TauRise.Values],
                    yLabel = "Tau (ms)",
                    drawPoints = true
                });
            }
            if (cbTauDecay.Checked && dynamics.TauDecay.Count != 0)
            {
                charts.Add(new Chart
                {
                    Title = "Tau decay",
                    Colors = [Color.Green],
                    xData = [.. dynamics.TauDecay.Keys],
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yMin = 0,
                    yData = [.. dynamics.TauDecay.Values],
                    yLabel = "Tau (ms)",
                    drawPoints = true
                });
            }
            if (cbInterval.Checked && dynamics.Intervals_ms != null && dynamics.Intervals_ms.Count != 0)
            {
                charts.Add(new Chart
                {
                    Title = "Intervals",
                    Colors = [Color.Blue],
                    xData = [.. dynamics.Intervals_ms.Keys],
                    xMin = 0,
                    xMax = TimeArray[^1],
                    yMin = 0,
                    yData = [.. dynamics.Intervals_ms.Values],
                    yLabel = "Interval (ms)",
                    drawPoints = true
                });
            }
            if (cbSpikingFrequency.Checked)
            {
                if (dynamics.SpikeFrequency_Grouped != null && dynamics.SpikeFrequency_Grouped.Count != 0)
                    charts.Add(new Chart
                    {
                        Title = "Spiking Freq.",
                        Colors = [Color.Blue],
                        xData = dynamics.SpikeFrequency_Grouped.Keys.Select(ff => ff).ToArray(),
                        xMin = 0,
                        xMax = TimeArray[^1],
                        yMin = 0,
                        yData = dynamics.SpikeFrequency_Grouped.Values.Select(ff => ff.Freq).ToArray(),
                        yLabel = "Freq (Hz)",
                        drawPoints = true
                    });
                if (dynamics.BurstingFrequency_Grouped != null && dynamics.BurstingFrequency_Grouped.Count != 0)
                    charts.Add(new Chart
                    {
                        Title = "Burst Freq.",
                        Colors = [Color.Blue],
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
                    Colors = [Color.Red],
                    xData = TimeArray,
                    yData = dynamics.StimulusArray,
                    yLabel = $"I ({Util.GetUoM(UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad_nanoSiemens, Measure.Current)})"
                });
            }
            int numCharts = charts.Count != 0 ? charts.Count : 1;
            string mainTitle = "Dynamics Test Results";
            string html = DyChartGenerator.PlotLineCharts(charts,
                mainTitle, webViewPlots.ClientSize.Width,
                (webViewPlots.ClientSize.Height - 150) / numCharts,
                synchronized: true, showZeroValues: GlobalSettings.ShowZeroValues);
            string tempFile = "";
            bool navigated = false;
            webViewPlots.NavigateTo(html, mainTitle, tempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
        }
        private void CreatePlots(Dictionary<string, DynamicsStats> dynamicsList, List<string> columnNames, List<double[]> I)
        {
            List<Chart> charts = [];
            foreach (string key in dynamicsList.Keys)
            {
                DynamicsStats dynamics = dynamicsList[key];
                string firingPattern = dynamics.FiringPattern.ToString();
                string firingRhythm = dynamics.FiringRhythm.ToString();

                charts.Add(new Chart
                {
                    Title = $"{key} ({firingRhythm} {firingPattern})",
                    Colors = [Color.Purple],
                    xData = TimeArray,
                    yData = dynamics.VList,
                    yLabel = "V (mV)"
                });
            }
            charts.Add(new Chart
            {
                Title = $"Stimulus",
                Colors = [Color.Red],
                xData = TimeArray,
                yMultiData = I,
                yLabel = string.Join(',', columnNames)
            });
            int numCharts = charts.Count != 0 ? charts.Count : 1;
            string mainTitle = "Dynamics Test Results";
            string html = DyChartGenerator.PlotLineCharts(charts,
                mainTitle,
                webViewPlots.ClientSize.Width,
                (webViewPlots.ClientSize.Height - 150) / numCharts,
                synchronized: true, showZeroValues: GlobalSettings.ShowZeroValues);
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

        private double[] GenerateStimulus(StimulusSettings settings)
        {
            int plotEnd_ms = (int)ePlotEndTime.Value;
            (TimeArray, double[] I) = DynamicsTest.GenerateStimulus(settings, plotEnd_ms, DeltaT, Random);
            return I;
        }
        private void DynamicsRun()
        {
            ReadParameters();
            double dt = DeltaT;
            CellCore core = CellCore.CreateCore(CoreType, parameters, DeltaT);
            if (core != null)
            {
                if (rbSingleEntryStimulus.Checked)
                {
                    double[] I = GenerateStimulus(ReadStimulusSettings());
                    dynamics = core.DynamicsTest(I);
                    CreatePlots();
                }
                else
                {
                    dynamics = null;
                    Dictionary<string, DynamicsStats> dynamicsList = [];
                    List<string> columnNames = [];
                    List<double> stimValues = [];
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
                        string[] multValues = eMultipleStimulus.Text.Split([';', ' ', ':']);
                        foreach (string s in multValues)
                            if (double.TryParse(s, out double d))
                            {
                                stimValues.Add(d);
                                columnNames.Add($"Stim_{columnNames.Count + 1}");
                            }
                    }
                    if (stimValues.Count != 0)
                    {
                        List<double[]> I = [];
                        int iter = 0;
                        TimeLine tl = new();
                        tl.AddTimeRange((int)eStepStartTime.Value, (int)eStepEndTime.Value);
                        foreach (double stim in stimValues.Distinct())
                        {
                            StimulusSettings stimulusSettings = new()
                            {
                                Mode = StimulusMode.Step,
                                Value1 = stim,
                                Value2 = 0,
                                TimeLine_ms = tl
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
            double dt = DeltaT;
            CellCore core = CellCore.CreateCore(CoreType, parameters, dt);
            decimal limit = eRheobaseLimit.Value;
            decimal d = (decimal)core.CalculateRheoBase((double)limit, Math.Pow(0.1, 3), (int)eRheobaseDuration.Value, dt);
            if (d >= 0)
            {
                string sRheo = d.ToString(GlobalSettings.PlotDataFormat);
                eRheobase.Text = sRheo;
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
            stimulusSettingsControl1.Enabled = rbSingleEntryStimulus.Checked;
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
            CellCore core = CellCore.CreateCore(CoreType, parameters, DeltaT);
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

        private void eDeltaT_ValueChanged(object sender, EventArgs e)
        {
            if (eDeltaT.Focused)
                SetControlDeltaTs(DeltaT);
        }
    }
}
