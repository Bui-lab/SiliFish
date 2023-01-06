
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
    public partial class ProjectionTestControl : UserControl
    {
        private static string coreUnitFileDefaultFolder;
        private string coreTypeSource;
        public string CoreTypeSource
        {
            get { return coreTypeSource; }
            set 
            { 
                coreTypeSource = value;
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

        public Dictionary<string, double> Parameters
        {
            get => parameters;
            set
            {
                parameters = value;
                if (parameters == null)
                {
                    pfParamsSource.Controls.Clear();

                }
                else
                {
                    pfParamsSource.CreateNumericUpDownControlsForDictionary(parameters);
                    foreach (Control control in pfParamsSource.Controls)
                    {
                        if (control is TextBox textBox)
                            textBox.TextChanged += TextBox_TextChanged;
                        else if (control is NumericUpDown numBox)
                            numBox.ValueChanged += NumBox_ValueChanged;
                    }
                    if (updateParamNames)
                    {
                        updateParamNames = false;

                    }
                    FirstRun();
                }
            }
        }

        private void NumBox_ValueChanged(object sender, EventArgs e)
        {
            if (cbAutoDrawPlots.Checked && sender is NumericUpDown)
            {
                DynamicsRun();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (cbAutoDrawPlots.Checked && sender is TextBox textBox)
            {
                if (double.TryParse(textBox.Text, out double _))
                {
                    DynamicsRun();
                }
            }
        }

        private void FillCoreTypes(string def = null)
        {
            /*if (ddCoreType.Items.Count == 0)
                ddCoreType.Items.AddRange(CellCoreUnit.GetCoreTypes().ToArray());
            if (!string.IsNullOrEmpty(def))
                ddCoreType.Text = def;
            if (ddCoreType.SelectedIndex < 0)
                ddCoreType.SelectedIndex = 0;*/
        }
        public ProjectionTestControl(string coreType, Dictionary<string, double> parameters, bool testMode)
        {
            InitializeComponent();
            InitAsync();
            Wait();
            FillCoreTypes(coreType);
            if (parameters != null)
                Parameters = parameters;
            pBottomBottom.Visible = !testMode;
        }

        private void FirstRun()
        {
            if (parameters != null)
            {
                StimulusSettings stim = stimulusControl1.GetStimulus();
                    stimulusControl1.SetStimulus(stim);
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
            parameters = pfParamsSource.CreateDoubleDictionaryFromControls();
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
            //TODO CoreTypeSource = ddCoreType.Text;
            updateParamNames = true;
            Parameters = CellCoreUnit.GetParameters(CoreTypeSource).ToDictionary(kvp => kvp.Key, kvp => double.Parse(kvp.Value.ToString()));
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
            CellCoreUnit core = CellCoreUnit.CreateCore(CoreTypeSource, parameters);
            if (core != null)
            {
                double[] I = GenerateStimulus();
                dynamics = core.DynamicsTest(I);
                CreatePlots();
            }
        }
        private void btnDynamicsRun_Click(object sender, EventArgs e)
        {
            DynamicsRun();
        }

        #region Load/Save
        private void linkUseUpdatedParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReadParameters();
            UpdatedParamsEventArgs args = new()
            {
                CoreType = CoreTypeSource,
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
                    //TODO ddCoreType.Text = core.CoreType;
                    skipCoreTypeChange = false;
                    CoreTypeSource = core.CoreType;
                    Parameters = core.GetParameters();
                }
            }
        }

        private void linkSaveCoreUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReadParameters();
            CellCoreUnit core = CellCoreUnit.CreateCore(CoreTypeSource, parameters);
            string JSONString = JsonUtil.ToJson(core);
            saveFileJson.InitialDirectory = coreUnitFileDefaultFolder;
            if (saveFileJson.ShowDialog() == DialogResult.OK)
            {
                FileUtil.SaveToFile(saveFileJson.FileName, JSONString);
                coreUnitFileDefaultFolder = Path.GetDirectoryName(saveFileJson.FileName);
            }
        }

        #endregion

        private void cbAutoDrawPlots_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoDrawPlots.Checked)
                DynamicsRun();
        }
    }
}
