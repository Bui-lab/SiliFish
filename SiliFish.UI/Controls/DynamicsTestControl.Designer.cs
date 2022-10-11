namespace SiliFish.UI.Controls
{
    partial class DynamicsTestControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.eRheobaseLimit = new System.Windows.Forms.NumericUpDown();
            this.lRheobaseLimit = new System.Windows.Forms.Label();
            this.btnRheobase = new System.Windows.Forms.Button();
            this.ldt = new System.Windows.Forms.Label();
            this.edt = new System.Windows.Forms.NumericUpDown();
            this.ePlotEndTime = new System.Windows.Forms.NumericUpDown();
            this.btnDynamicsRun = new System.Windows.Forms.Button();
            this.eStepEndTime = new System.Windows.Forms.NumericUpDown();
            this.lStepStartTime = new System.Windows.Forms.Label();
            this.eStepStartTime = new System.Windows.Forms.NumericUpDown();
            this.lStepEndTime = new System.Windows.Forms.Label();
            this.lPlotEndTime = new System.Windows.Forms.Label();
            this.grRheobase = new System.Windows.Forms.GroupBox();
            this.lRheobaseWarning = new System.Windows.Forms.Label();
            this.eRheobaseDuration = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.eRheobase = new System.Windows.Forms.TextBox();
            this.lRheobase = new System.Windows.Forms.Label();
            this.btnSensitivityAnalysis = new System.Windows.Forms.Button();
            this.grTest = new System.Windows.Forms.GroupBox();
            this.stimulusControl1 = new SiliFish.UI.Controls.StimulusControl();
            this.cbAutoDrawPlots = new System.Windows.Forms.CheckBox();
            this.rbManualEntryStimulus = new System.Windows.Forms.RadioButton();
            this.rbRheobaseBasedStimulus = new System.Windows.Forms.RadioButton();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pfParams = new System.Windows.Forms.FlowLayoutPanel();
            this.tabAnalysis = new System.Windows.Forms.TabControl();
            this.tTest = new System.Windows.Forms.TabPage();
            this.tSensitivityAnalysis = new System.Windows.Forms.TabPage();
            this.grSensitivity = new System.Windows.Forms.GroupBox();
            this.cbLogScale = new System.Windows.Forms.CheckBox();
            this.eMaxMultiplier = new System.Windows.Forms.TextBox();
            this.lDash = new System.Windows.Forms.Label();
            this.eMinMultiplier = new System.Windows.Forms.TextBox();
            this.lMultiplierRange = new System.Windows.Forms.Label();
            this.ddParameter = new System.Windows.Forms.ComboBox();
            this.lParameter = new System.Windows.Forms.Label();
            this.pCoreType = new System.Windows.Forms.Panel();
            this.ddCoreType = new System.Windows.Forms.ComboBox();
            this.lCoreType = new System.Windows.Forms.Label();
            this.pLoadSaveParams = new System.Windows.Forms.Panel();
            this.linkLoadCoreUnit = new System.Windows.Forms.LinkLabel();
            this.linkSaveCoreUnit = new System.Windows.Forms.LinkLabel();
            this.pBottomBottom = new System.Windows.Forms.Panel();
            this.linkUseUpdatedParams = new System.Windows.Forms.LinkLabel();
            this.splitGAAndPlots = new System.Windows.Forms.SplitContainer();
            this.pOptimize = new System.Windows.Forms.Panel();
            this.splitGA = new System.Windows.Forms.SplitContainer();
            this.tabOptimization = new System.Windows.Forms.TabControl();
            this.tGAParameters = new System.Windows.Forms.TabPage();
            this.splitGAParams = new System.Windows.Forms.SplitContainer();
            this.dgMinMaxValues = new System.Windows.Forms.DataGridView();
            this.colParameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pMinMax = new System.Windows.Forms.Panel();
            this.linkSuggestMinMax = new System.Windows.Forms.LinkLabel();
            this.pfGAParams = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lFitnessFunctions = new System.Windows.Forms.Label();
            this.linkAddFitnessFunction = new System.Windows.Forms.LinkLabel();
            this.tGAOutput = new System.Windows.Forms.TabPage();
            this.eOptimizationOutput = new System.Windows.Forms.RichTextBox();
            this.lGASelection = new System.Windows.Forms.Label();
            this.cbGACrossOver = new System.Windows.Forms.ComboBox();
            this.eMaxChromosome = new System.Windows.Forms.TextBox();
            this.lGAMutation = new System.Windows.Forms.Label();
            this.cbGAMutation = new System.Windows.Forms.ComboBox();
            this.lGAMinMaxChromosome = new System.Windows.Forms.Label();
            this.cbGATermination = new System.Windows.Forms.ComboBox();
            this.cbGASelection = new System.Windows.Forms.ComboBox();
            this.linkOptimize = new System.Windows.Forms.LinkLabel();
            this.eMinChromosome = new System.Windows.Forms.TextBox();
            this.lGACrossOver = new System.Windows.Forms.Label();
            this.lMinMaxSeperator = new System.Windows.Forms.Label();
            this.lGATermination = new System.Windows.Forms.Label();
            this.webViewPlots = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pPlots = new System.Windows.Forms.Panel();
            this.grPlotSelection = new System.Windows.Forms.GroupBox();
            this.cbV = new System.Windows.Forms.CheckBox();
            this.cbStimulus = new System.Windows.Forms.CheckBox();
            this.cbSecondaryLists = new System.Windows.Forms.CheckBox();
            this.cbSpikingFrequency = new System.Windows.Forms.CheckBox();
            this.cbTauRise = new System.Windows.Forms.CheckBox();
            this.cbInterval = new System.Windows.Forms.CheckBox();
            this.cbTauDecay = new System.Windows.Forms.CheckBox();
            this.linkSwitchToOptimization = new System.Windows.Forms.LinkLabel();
            this.saveFileJson = new System.Windows.Forms.SaveFileDialog();
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerOptimization = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.eRheobaseLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepStartTime)).BeginInit();
            this.grRheobase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eRheobaseDuration)).BeginInit();
            this.grTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tabAnalysis.SuspendLayout();
            this.tTest.SuspendLayout();
            this.tSensitivityAnalysis.SuspendLayout();
            this.grSensitivity.SuspendLayout();
            this.pCoreType.SuspendLayout();
            this.pLoadSaveParams.SuspendLayout();
            this.pBottomBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGAAndPlots)).BeginInit();
            this.splitGAAndPlots.Panel1.SuspendLayout();
            this.splitGAAndPlots.Panel2.SuspendLayout();
            this.splitGAAndPlots.SuspendLayout();
            this.pOptimize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGA)).BeginInit();
            this.splitGA.Panel1.SuspendLayout();
            this.splitGA.Panel2.SuspendLayout();
            this.splitGA.SuspendLayout();
            this.tabOptimization.SuspendLayout();
            this.tGAParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGAParams)).BeginInit();
            this.splitGAParams.Panel1.SuspendLayout();
            this.splitGAParams.Panel2.SuspendLayout();
            this.splitGAParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMinMaxValues)).BeginInit();
            this.pMinMax.SuspendLayout();
            this.pfGAParams.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tGAOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlots)).BeginInit();
            this.pPlots.SuspendLayout();
            this.grPlotSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // eRheobaseLimit
            // 
            this.eRheobaseLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.eRheobaseLimit.Location = new System.Drawing.Point(70, 22);
            this.eRheobaseLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.eRheobaseLimit.Name = "eRheobaseLimit";
            this.eRheobaseLimit.Size = new System.Drawing.Size(54, 23);
            this.eRheobaseLimit.TabIndex = 15;
            this.eRheobaseLimit.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lRheobaseLimit
            // 
            this.lRheobaseLimit.AutoSize = true;
            this.lRheobaseLimit.Location = new System.Drawing.Point(3, 26);
            this.lRheobaseLimit.Name = "lRheobaseLimit";
            this.lRheobaseLimit.Size = new System.Drawing.Size(34, 15);
            this.lRheobaseLimit.TabIndex = 14;
            this.lRheobaseLimit.Text = "Limit";
            // 
            // btnRheobase
            // 
            this.btnRheobase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRheobase.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnRheobase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRheobase.Location = new System.Drawing.Point(128, 22);
            this.btnRheobase.Name = "btnRheobase";
            this.btnRheobase.Size = new System.Drawing.Size(66, 23);
            this.btnRheobase.TabIndex = 13;
            this.btnRheobase.Text = "Calculate";
            this.btnRheobase.UseVisualStyleBackColor = false;
            this.btnRheobase.Click += new System.EventHandler(this.btnRheobase_Click);
            // 
            // ldt
            // 
            this.ldt.AutoSize = true;
            this.ldt.Location = new System.Drawing.Point(9, 98);
            this.ldt.Name = "ldt";
            this.ldt.Size = new System.Drawing.Size(18, 15);
            this.ldt.TabIndex = 10;
            this.ldt.Text = "dt";
            // 
            // edt
            // 
            this.edt.DecimalPlaces = 2;
            this.edt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edt.Location = new System.Drawing.Point(100, 96);
            this.edt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edt.Name = "edt";
            this.edt.Size = new System.Drawing.Size(66, 23);
            this.edt.TabIndex = 11;
            this.edt.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // ePlotEndTime
            // 
            this.ePlotEndTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ePlotEndTime.Location = new System.Drawing.Point(100, 71);
            this.ePlotEndTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.ePlotEndTime.Name = "ePlotEndTime";
            this.ePlotEndTime.Size = new System.Drawing.Size(66, 23);
            this.ePlotEndTime.TabIndex = 9;
            this.ePlotEndTime.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnDynamicsRun
            // 
            this.btnDynamicsRun.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDynamicsRun.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDynamicsRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDynamicsRun.Location = new System.Drawing.Point(4, 271);
            this.btnDynamicsRun.Name = "btnDynamicsRun";
            this.btnDynamicsRun.Size = new System.Drawing.Size(67, 23);
            this.btnDynamicsRun.TabIndex = 3;
            this.btnDynamicsRun.Text = "Run";
            this.btnDynamicsRun.UseVisualStyleBackColor = false;
            this.btnDynamicsRun.Click += new System.EventHandler(this.btnDynamicsRun_Click);
            // 
            // eStepEndTime
            // 
            this.eStepEndTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eStepEndTime.Location = new System.Drawing.Point(100, 47);
            this.eStepEndTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.eStepEndTime.Name = "eStepEndTime";
            this.eStepEndTime.Size = new System.Drawing.Size(66, 23);
            this.eStepEndTime.TabIndex = 8;
            this.eStepEndTime.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lStepStartTime
            // 
            this.lStepStartTime.AutoSize = true;
            this.lStepStartTime.Location = new System.Drawing.Point(9, 25);
            this.lStepStartTime.Name = "lStepStartTime";
            this.lStepStartTime.Size = new System.Drawing.Size(90, 15);
            this.lStepStartTime.TabIndex = 4;
            this.lStepStartTime.Text = "Stim. Start Time";
            // 
            // eStepStartTime
            // 
            this.eStepStartTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eStepStartTime.Location = new System.Drawing.Point(100, 21);
            this.eStepStartTime.Name = "eStepStartTime";
            this.eStepStartTime.Size = new System.Drawing.Size(66, 23);
            this.eStepStartTime.TabIndex = 7;
            this.eStepStartTime.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lStepEndTime
            // 
            this.lStepEndTime.AutoSize = true;
            this.lStepEndTime.Location = new System.Drawing.Point(9, 49);
            this.lStepEndTime.Name = "lStepEndTime";
            this.lStepEndTime.Size = new System.Drawing.Size(86, 15);
            this.lStepEndTime.TabIndex = 5;
            this.lStepEndTime.Text = "Stim. End Time";
            // 
            // lPlotEndTime
            // 
            this.lPlotEndTime.AutoSize = true;
            this.lPlotEndTime.Location = new System.Drawing.Point(9, 75);
            this.lPlotEndTime.Name = "lPlotEndTime";
            this.lPlotEndTime.Size = new System.Drawing.Size(80, 15);
            this.lPlotEndTime.TabIndex = 6;
            this.lPlotEndTime.Text = "Plot End Time";
            // 
            // grRheobase
            // 
            this.grRheobase.Controls.Add(this.lRheobaseWarning);
            this.grRheobase.Controls.Add(this.eRheobaseDuration);
            this.grRheobase.Controls.Add(this.label1);
            this.grRheobase.Controls.Add(this.eRheobase);
            this.grRheobase.Controls.Add(this.lRheobase);
            this.grRheobase.Controls.Add(this.btnRheobase);
            this.grRheobase.Controls.Add(this.eRheobaseLimit);
            this.grRheobase.Controls.Add(this.lRheobaseLimit);
            this.grRheobase.Dock = System.Windows.Forms.DockStyle.Top;
            this.grRheobase.Location = new System.Drawing.Point(3, 3);
            this.grRheobase.Name = "grRheobase";
            this.grRheobase.Size = new System.Drawing.Size(285, 114);
            this.grRheobase.TabIndex = 16;
            this.grRheobase.TabStop = false;
            this.grRheobase.Text = "Rheobase";
            // 
            // lRheobaseWarning
            // 
            this.lRheobaseWarning.AutoSize = true;
            this.lRheobaseWarning.ForeColor = System.Drawing.Color.Red;
            this.lRheobaseWarning.Location = new System.Drawing.Point(134, 77);
            this.lRheobaseWarning.Name = "lRheobaseWarning";
            this.lRheobaseWarning.Size = new System.Drawing.Size(140, 15);
            this.lRheobaseWarning.TabIndex = 20;
            this.lRheobaseWarning.Text = "No rheobase within limit.";
            this.lRheobaseWarning.Visible = false;
            // 
            // eRheobaseDuration
            // 
            this.eRheobaseDuration.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eRheobaseDuration.Location = new System.Drawing.Point(70, 47);
            this.eRheobaseDuration.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.eRheobaseDuration.Name = "eRheobaseDuration";
            this.eRheobaseDuration.Size = new System.Drawing.Size(54, 23);
            this.eRheobaseDuration.TabIndex = 19;
            this.eRheobaseDuration.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Duration";
            // 
            // eRheobase
            // 
            this.eRheobase.Location = new System.Drawing.Point(70, 73);
            this.eRheobase.Name = "eRheobase";
            this.eRheobase.ReadOnly = true;
            this.eRheobase.Size = new System.Drawing.Size(54, 23);
            this.eRheobase.TabIndex = 17;
            // 
            // lRheobase
            // 
            this.lRheobase.AutoSize = true;
            this.lRheobase.Location = new System.Drawing.Point(3, 76);
            this.lRheobase.Name = "lRheobase";
            this.lRheobase.Size = new System.Drawing.Size(58, 15);
            this.lRheobase.TabIndex = 16;
            this.lRheobase.Text = "Rheobase";
            // 
            // btnSensitivityAnalysis
            // 
            this.btnSensitivityAnalysis.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSensitivityAnalysis.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSensitivityAnalysis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSensitivityAnalysis.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSensitivityAnalysis.Location = new System.Drawing.Point(131, 80);
            this.btnSensitivityAnalysis.Name = "btnSensitivityAnalysis";
            this.btnSensitivityAnalysis.Size = new System.Drawing.Size(63, 23);
            this.btnSensitivityAnalysis.TabIndex = 20;
            this.btnSensitivityAnalysis.Text = "Run";
            this.btnSensitivityAnalysis.UseVisualStyleBackColor = false;
            this.btnSensitivityAnalysis.Click += new System.EventHandler(this.btnSensitivityAnalysis_Click);
            // 
            // grTest
            // 
            this.grTest.Controls.Add(this.stimulusControl1);
            this.grTest.Controls.Add(this.cbAutoDrawPlots);
            this.grTest.Controls.Add(this.rbManualEntryStimulus);
            this.grTest.Controls.Add(this.rbRheobaseBasedStimulus);
            this.grTest.Controls.Add(this.edt);
            this.grTest.Controls.Add(this.lPlotEndTime);
            this.grTest.Controls.Add(this.lStepEndTime);
            this.grTest.Controls.Add(this.eStepStartTime);
            this.grTest.Controls.Add(this.ePlotEndTime);
            this.grTest.Controls.Add(this.lStepStartTime);
            this.grTest.Controls.Add(this.btnDynamicsRun);
            this.grTest.Controls.Add(this.eStepEndTime);
            this.grTest.Controls.Add(this.ldt);
            this.grTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.grTest.Location = new System.Drawing.Point(3, 117);
            this.grTest.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.grTest.Name = "grTest";
            this.grTest.Size = new System.Drawing.Size(285, 300);
            this.grTest.TabIndex = 17;
            this.grTest.TabStop = false;
            this.grTest.Text = "Test";
            // 
            // stimulusControl1
            // 
            this.stimulusControl1.BackColor = System.Drawing.Color.White;
            this.stimulusControl1.Location = new System.Drawing.Point(9, 148);
            this.stimulusControl1.Name = "stimulusControl1";
            this.stimulusControl1.Size = new System.Drawing.Size(176, 93);
            this.stimulusControl1.TabIndex = 25;
            this.stimulusControl1.StimulusChanged += new System.EventHandler(this.stimulusControl1_StimulusChanged);
            // 
            // cbAutoDrawPlots
            // 
            this.cbAutoDrawPlots.AutoSize = true;
            this.cbAutoDrawPlots.Location = new System.Drawing.Point(83, 275);
            this.cbAutoDrawPlots.Name = "cbAutoDrawPlots";
            this.cbAutoDrawPlots.Size = new System.Drawing.Size(111, 19);
            this.cbAutoDrawPlots.TabIndex = 24;
            this.cbAutoDrawPlots.Text = "Auto Draw Plots";
            this.cbAutoDrawPlots.UseVisualStyleBackColor = true;
            // 
            // rbManualEntryStimulus
            // 
            this.rbManualEntryStimulus.AutoSize = true;
            this.rbManualEntryStimulus.Checked = true;
            this.rbManualEntryStimulus.Location = new System.Drawing.Point(9, 128);
            this.rbManualEntryStimulus.Name = "rbManualEntryStimulus";
            this.rbManualEntryStimulus.Size = new System.Drawing.Size(117, 19);
            this.rbManualEntryStimulus.TabIndex = 12;
            this.rbManualEntryStimulus.TabStop = true;
            this.rbManualEntryStimulus.Text = "Use Manual Entry";
            this.rbManualEntryStimulus.UseVisualStyleBackColor = true;
            this.rbManualEntryStimulus.CheckedChanged += new System.EventHandler(this.rbManualEntryStimulus_CheckedChanged);
            // 
            // rbRheobaseBasedStimulus
            // 
            this.rbRheobaseBasedStimulus.AutoSize = true;
            this.rbRheobaseBasedStimulus.Location = new System.Drawing.Point(9, 247);
            this.rbRheobaseBasedStimulus.Name = "rbRheobaseBasedStimulus";
            this.rbRheobaseBasedStimulus.Size = new System.Drawing.Size(175, 19);
            this.rbRheobaseBasedStimulus.TabIndex = 13;
            this.rbRheobaseBasedStimulus.Text = "Use Rheobase (x1, x1.1, x1.5)";
            this.rbRheobaseBasedStimulus.UseVisualStyleBackColor = true;
            // 
            // splitMain
            // 
            this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pfParams);
            this.splitMain.Panel1.Controls.Add(this.tabAnalysis);
            this.splitMain.Panel1.Controls.Add(this.pCoreType);
            this.splitMain.Panel1.Controls.Add(this.pLoadSaveParams);
            this.splitMain.Panel1.Controls.Add(this.pBottomBottom);
            this.splitMain.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitGAAndPlots);
            this.splitMain.Panel2.Controls.Add(this.pPlots);
            this.splitMain.Size = new System.Drawing.Size(1252, 683);
            this.splitMain.SplitterDistance = 307;
            this.splitMain.TabIndex = 17;
            // 
            // pfParams
            // 
            this.pfParams.AutoScroll = true;
            this.pfParams.AutoSize = true;
            this.pfParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfParams.Location = new System.Drawing.Point(3, 33);
            this.pfParams.Name = "pfParams";
            this.pfParams.Size = new System.Drawing.Size(299, 146);
            this.pfParams.TabIndex = 23;
            // 
            // tabAnalysis
            // 
            this.tabAnalysis.Controls.Add(this.tTest);
            this.tabAnalysis.Controls.Add(this.tSensitivityAnalysis);
            this.tabAnalysis.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabAnalysis.Location = new System.Drawing.Point(3, 179);
            this.tabAnalysis.Name = "tabAnalysis";
            this.tabAnalysis.SelectedIndex = 0;
            this.tabAnalysis.Size = new System.Drawing.Size(299, 445);
            this.tabAnalysis.TabIndex = 39;
            // 
            // tTest
            // 
            this.tTest.Controls.Add(this.grTest);
            this.tTest.Controls.Add(this.grRheobase);
            this.tTest.Location = new System.Drawing.Point(4, 24);
            this.tTest.Name = "tTest";
            this.tTest.Padding = new System.Windows.Forms.Padding(3);
            this.tTest.Size = new System.Drawing.Size(291, 417);
            this.tTest.TabIndex = 0;
            this.tTest.Text = "Test";
            this.tTest.UseVisualStyleBackColor = true;
            // 
            // tSensitivityAnalysis
            // 
            this.tSensitivityAnalysis.Controls.Add(this.grSensitivity);
            this.tSensitivityAnalysis.Location = new System.Drawing.Point(4, 24);
            this.tSensitivityAnalysis.Name = "tSensitivityAnalysis";
            this.tSensitivityAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tSensitivityAnalysis.Size = new System.Drawing.Size(291, 417);
            this.tSensitivityAnalysis.TabIndex = 1;
            this.tSensitivityAnalysis.Text = "Sensitivity Analysis";
            this.tSensitivityAnalysis.UseVisualStyleBackColor = true;
            // 
            // grSensitivity
            // 
            this.grSensitivity.Controls.Add(this.cbLogScale);
            this.grSensitivity.Controls.Add(this.eMaxMultiplier);
            this.grSensitivity.Controls.Add(this.lDash);
            this.grSensitivity.Controls.Add(this.eMinMultiplier);
            this.grSensitivity.Controls.Add(this.lMultiplierRange);
            this.grSensitivity.Controls.Add(this.ddParameter);
            this.grSensitivity.Controls.Add(this.lParameter);
            this.grSensitivity.Controls.Add(this.btnSensitivityAnalysis);
            this.grSensitivity.Location = new System.Drawing.Point(6, 6);
            this.grSensitivity.Name = "grSensitivity";
            this.grSensitivity.Size = new System.Drawing.Size(301, 109);
            this.grSensitivity.TabIndex = 18;
            this.grSensitivity.TabStop = false;
            this.grSensitivity.Text = "Sensitivity Analysis";
            // 
            // cbLogScale
            // 
            this.cbLogScale.AutoSize = true;
            this.cbLogScale.Location = new System.Drawing.Point(6, 80);
            this.cbLogScale.Name = "cbLogScale";
            this.cbLogScale.Size = new System.Drawing.Size(76, 19);
            this.cbLogScale.TabIndex = 28;
            this.cbLogScale.Text = "Log Scale";
            this.cbLogScale.UseVisualStyleBackColor = true;
            // 
            // eMaxMultiplier
            // 
            this.eMaxMultiplier.Location = new System.Drawing.Point(152, 51);
            this.eMaxMultiplier.Name = "eMaxMultiplier";
            this.eMaxMultiplier.Size = new System.Drawing.Size(41, 23);
            this.eMaxMultiplier.TabIndex = 26;
            this.eMaxMultiplier.Text = "10";
            // 
            // lDash
            // 
            this.lDash.AutoSize = true;
            this.lDash.Location = new System.Drawing.Point(141, 54);
            this.lDash.Name = "lDash";
            this.lDash.Size = new System.Drawing.Size(12, 15);
            this.lDash.TabIndex = 27;
            this.lDash.Text = "-";
            // 
            // eMinMultiplier
            // 
            this.eMinMultiplier.Location = new System.Drawing.Point(100, 51);
            this.eMinMultiplier.Name = "eMinMultiplier";
            this.eMinMultiplier.Size = new System.Drawing.Size(41, 23);
            this.eMinMultiplier.TabIndex = 24;
            this.eMinMultiplier.Text = "0.1";
            // 
            // lMultiplierRange
            // 
            this.lMultiplierRange.AutoSize = true;
            this.lMultiplierRange.Location = new System.Drawing.Point(6, 54);
            this.lMultiplierRange.Name = "lMultiplierRange";
            this.lMultiplierRange.Size = new System.Drawing.Size(94, 15);
            this.lMultiplierRange.TabIndex = 23;
            this.lMultiplierRange.Text = "Multiplier Range";
            // 
            // ddParameter
            // 
            this.ddParameter.FormattingEnabled = true;
            this.ddParameter.Location = new System.Drawing.Point(73, 22);
            this.ddParameter.Name = "ddParameter";
            this.ddParameter.Size = new System.Drawing.Size(121, 23);
            this.ddParameter.TabIndex = 22;
            // 
            // lParameter
            // 
            this.lParameter.AutoSize = true;
            this.lParameter.Location = new System.Drawing.Point(6, 28);
            this.lParameter.Name = "lParameter";
            this.lParameter.Size = new System.Drawing.Size(61, 15);
            this.lParameter.TabIndex = 21;
            this.lParameter.Text = "Parameter";
            // 
            // pCoreType
            // 
            this.pCoreType.Controls.Add(this.ddCoreType);
            this.pCoreType.Controls.Add(this.lCoreType);
            this.pCoreType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCoreType.Location = new System.Drawing.Point(3, 3);
            this.pCoreType.Name = "pCoreType";
            this.pCoreType.Size = new System.Drawing.Size(299, 30);
            this.pCoreType.TabIndex = 21;
            // 
            // ddCoreType
            // 
            this.ddCoreType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddCoreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCoreType.FormattingEnabled = true;
            this.ddCoreType.Location = new System.Drawing.Point(70, 4);
            this.ddCoreType.Name = "ddCoreType";
            this.ddCoreType.Size = new System.Drawing.Size(226, 23);
            this.ddCoreType.TabIndex = 23;
            this.ddCoreType.SelectedIndexChanged += new System.EventHandler(this.ddCoreType_SelectedIndexChanged);
            // 
            // lCoreType
            // 
            this.lCoreType.AutoSize = true;
            this.lCoreType.Location = new System.Drawing.Point(6, 8);
            this.lCoreType.Name = "lCoreType";
            this.lCoreType.Size = new System.Drawing.Size(59, 15);
            this.lCoreType.TabIndex = 22;
            this.lCoreType.Text = "Core Type";
            // 
            // pLoadSaveParams
            // 
            this.pLoadSaveParams.Controls.Add(this.linkLoadCoreUnit);
            this.pLoadSaveParams.Controls.Add(this.linkSaveCoreUnit);
            this.pLoadSaveParams.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLoadSaveParams.Location = new System.Drawing.Point(3, 624);
            this.pLoadSaveParams.Name = "pLoadSaveParams";
            this.pLoadSaveParams.Size = new System.Drawing.Size(299, 31);
            this.pLoadSaveParams.TabIndex = 20;
            // 
            // linkLoadCoreUnit
            // 
            this.linkLoadCoreUnit.AutoSize = true;
            this.linkLoadCoreUnit.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLoadCoreUnit.Location = new System.Drawing.Point(4, 8);
            this.linkLoadCoreUnit.Name = "linkLoadCoreUnit";
            this.linkLoadCoreUnit.Size = new System.Drawing.Size(86, 15);
            this.linkLoadCoreUnit.TabIndex = 20;
            this.linkLoadCoreUnit.TabStop = true;
            this.linkLoadCoreUnit.Text = "Load Core Unit";
            this.linkLoadCoreUnit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLoadCoreUnit_LinkClicked);
            // 
            // linkSaveCoreUnit
            // 
            this.linkSaveCoreUnit.AutoSize = true;
            this.linkSaveCoreUnit.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveCoreUnit.Location = new System.Drawing.Point(96, 8);
            this.linkSaveCoreUnit.Name = "linkSaveCoreUnit";
            this.linkSaveCoreUnit.Size = new System.Drawing.Size(84, 15);
            this.linkSaveCoreUnit.TabIndex = 21;
            this.linkSaveCoreUnit.TabStop = true;
            this.linkSaveCoreUnit.Text = "Save Core Unit";
            this.linkSaveCoreUnit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveCoreUnit_LinkClicked);
            // 
            // pBottomBottom
            // 
            this.pBottomBottom.Controls.Add(this.linkUseUpdatedParams);
            this.pBottomBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottomBottom.Location = new System.Drawing.Point(3, 655);
            this.pBottomBottom.Name = "pBottomBottom";
            this.pBottomBottom.Size = new System.Drawing.Size(299, 23);
            this.pBottomBottom.TabIndex = 24;
            this.pBottomBottom.Visible = false;
            // 
            // linkUseUpdatedParams
            // 
            this.linkUseUpdatedParams.AutoSize = true;
            this.linkUseUpdatedParams.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkUseUpdatedParams.Location = new System.Drawing.Point(6, 5);
            this.linkUseUpdatedParams.Name = "linkUseUpdatedParams";
            this.linkUseUpdatedParams.Size = new System.Drawing.Size(136, 15);
            this.linkUseUpdatedParams.TabIndex = 19;
            this.linkUseUpdatedParams.TabStop = true;
            this.linkUseUpdatedParams.Text = "Use Updated Parameters";
            this.linkUseUpdatedParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUseUpdatedParams_LinkClicked);
            // 
            // splitGAAndPlots
            // 
            this.splitGAAndPlots.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitGAAndPlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGAAndPlots.Location = new System.Drawing.Point(0, 61);
            this.splitGAAndPlots.Name = "splitGAAndPlots";
            this.splitGAAndPlots.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitGAAndPlots.Panel1
            // 
            this.splitGAAndPlots.Panel1.Controls.Add(this.pOptimize);
            // 
            // splitGAAndPlots.Panel2
            // 
            this.splitGAAndPlots.Panel2.Controls.Add(this.webViewPlots);
            this.splitGAAndPlots.Size = new System.Drawing.Size(941, 622);
            this.splitGAAndPlots.SplitterDistance = 311;
            this.splitGAAndPlots.TabIndex = 2;
            // 
            // pOptimize
            // 
            this.pOptimize.Controls.Add(this.splitGA);
            this.pOptimize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pOptimize.Location = new System.Drawing.Point(0, 0);
            this.pOptimize.Name = "pOptimize";
            this.pOptimize.Size = new System.Drawing.Size(939, 309);
            this.pOptimize.TabIndex = 39;
            // 
            // splitGA
            // 
            this.splitGA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitGA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGA.Location = new System.Drawing.Point(0, 0);
            this.splitGA.Name = "splitGA";
            // 
            // splitGA.Panel1
            // 
            this.splitGA.Panel1.Controls.Add(this.tabOptimization);
            // 
            // splitGA.Panel2
            // 
            this.splitGA.Panel2.Controls.Add(this.lGASelection);
            this.splitGA.Panel2.Controls.Add(this.cbGACrossOver);
            this.splitGA.Panel2.Controls.Add(this.eMaxChromosome);
            this.splitGA.Panel2.Controls.Add(this.lGAMutation);
            this.splitGA.Panel2.Controls.Add(this.cbGAMutation);
            this.splitGA.Panel2.Controls.Add(this.lGAMinMaxChromosome);
            this.splitGA.Panel2.Controls.Add(this.cbGATermination);
            this.splitGA.Panel2.Controls.Add(this.cbGASelection);
            this.splitGA.Panel2.Controls.Add(this.linkOptimize);
            this.splitGA.Panel2.Controls.Add(this.eMinChromosome);
            this.splitGA.Panel2.Controls.Add(this.lGACrossOver);
            this.splitGA.Panel2.Controls.Add(this.lMinMaxSeperator);
            this.splitGA.Panel2.Controls.Add(this.lGATermination);
            this.splitGA.Size = new System.Drawing.Size(939, 309);
            this.splitGA.SplitterDistance = 653;
            this.splitGA.TabIndex = 38;
            // 
            // tabOptimization
            // 
            this.tabOptimization.Controls.Add(this.tGAParameters);
            this.tabOptimization.Controls.Add(this.tGAOutput);
            this.tabOptimization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOptimization.Location = new System.Drawing.Point(0, 0);
            this.tabOptimization.Name = "tabOptimization";
            this.tabOptimization.SelectedIndex = 0;
            this.tabOptimization.Size = new System.Drawing.Size(651, 307);
            this.tabOptimization.TabIndex = 30;
            // 
            // tGAParameters
            // 
            this.tGAParameters.Controls.Add(this.splitGAParams);
            this.tGAParameters.Location = new System.Drawing.Point(4, 24);
            this.tGAParameters.Name = "tGAParameters";
            this.tGAParameters.Padding = new System.Windows.Forms.Padding(3);
            this.tGAParameters.Size = new System.Drawing.Size(643, 279);
            this.tGAParameters.TabIndex = 0;
            this.tGAParameters.Text = "Optimization Parameters";
            this.tGAParameters.UseVisualStyleBackColor = true;
            // 
            // splitGAParams
            // 
            this.splitGAParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitGAParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGAParams.Location = new System.Drawing.Point(3, 3);
            this.splitGAParams.Name = "splitGAParams";
            // 
            // splitGAParams.Panel1
            // 
            this.splitGAParams.Panel1.Controls.Add(this.dgMinMaxValues);
            this.splitGAParams.Panel1.Controls.Add(this.pMinMax);
            // 
            // splitGAParams.Panel2
            // 
            this.splitGAParams.Panel2.Controls.Add(this.pfGAParams);
            this.splitGAParams.Size = new System.Drawing.Size(637, 273);
            this.splitGAParams.SplitterDistance = 211;
            this.splitGAParams.TabIndex = 29;
            // 
            // dgMinMaxValues
            // 
            this.dgMinMaxValues.AllowUserToAddRows = false;
            this.dgMinMaxValues.AllowUserToDeleteRows = false;
            this.dgMinMaxValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMinMaxValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParameter,
            this.colMinValue,
            this.colMaxValue});
            this.dgMinMaxValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMinMaxValues.Location = new System.Drawing.Point(0, 29);
            this.dgMinMaxValues.Name = "dgMinMaxValues";
            this.dgMinMaxValues.RowHeadersVisible = false;
            this.dgMinMaxValues.RowTemplate.Height = 25;
            this.dgMinMaxValues.Size = new System.Drawing.Size(209, 242);
            this.dgMinMaxValues.TabIndex = 27;
            // 
            // colParameter
            // 
            this.colParameter.Frozen = true;
            this.colParameter.HeaderText = "Parameter";
            this.colParameter.MinimumWidth = 100;
            this.colParameter.Name = "colParameter";
            this.colParameter.ReadOnly = true;
            this.colParameter.Width = 200;
            // 
            // colMinValue
            // 
            this.colMinValue.HeaderText = "Min Value";
            this.colMinValue.Name = "colMinValue";
            // 
            // colMaxValue
            // 
            this.colMaxValue.HeaderText = "Max Value";
            this.colMaxValue.Name = "colMaxValue";
            // 
            // pMinMax
            // 
            this.pMinMax.Controls.Add(this.linkSuggestMinMax);
            this.pMinMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMinMax.Location = new System.Drawing.Point(0, 0);
            this.pMinMax.Name = "pMinMax";
            this.pMinMax.Size = new System.Drawing.Size(209, 29);
            this.pMinMax.TabIndex = 28;
            // 
            // linkSuggestMinMax
            // 
            this.linkSuggestMinMax.AutoSize = true;
            this.linkSuggestMinMax.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSuggestMinMax.Location = new System.Drawing.Point(6, 9);
            this.linkSuggestMinMax.Name = "linkSuggestMinMax";
            this.linkSuggestMinMax.Size = new System.Drawing.Size(137, 15);
            this.linkSuggestMinMax.TabIndex = 0;
            this.linkSuggestMinMax.TabStop = true;
            this.linkSuggestMinMax.Text = "Suggest Min/Max Values";
            this.toolTip1.SetToolTip(this.linkSuggestMinMax, "If not entered, current values will be used as min/max.");
            this.linkSuggestMinMax.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSuggestMinMax_LinkClicked);
            // 
            // pfGAParams
            // 
            this.pfGAParams.AutoScroll = true;
            this.pfGAParams.Controls.Add(this.panel1);
            this.pfGAParams.Controls.Add(this.linkAddFitnessFunction);
            this.pfGAParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfGAParams.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pfGAParams.Location = new System.Drawing.Point(0, 0);
            this.pfGAParams.Name = "pfGAParams";
            this.pfGAParams.Size = new System.Drawing.Size(420, 271);
            this.pfGAParams.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lFitnessFunctions);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 29);
            this.panel1.TabIndex = 29;
            // 
            // lFitnessFunctions
            // 
            this.lFitnessFunctions.AutoSize = true;
            this.lFitnessFunctions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lFitnessFunctions.Location = new System.Drawing.Point(13, 6);
            this.lFitnessFunctions.Name = "lFitnessFunctions";
            this.lFitnessFunctions.Size = new System.Drawing.Size(101, 15);
            this.lFitnessFunctions.TabIndex = 2;
            this.lFitnessFunctions.Text = "Fitness Functions";
            // 
            // linkAddFitnessFunction
            // 
            this.linkAddFitnessFunction.AutoSize = true;
            this.linkAddFitnessFunction.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkAddFitnessFunction.Location = new System.Drawing.Point(3, 35);
            this.linkAddFitnessFunction.Name = "linkAddFitnessFunction";
            this.linkAddFitnessFunction.Padding = new System.Windows.Forms.Padding(10);
            this.linkAddFitnessFunction.Size = new System.Drawing.Size(138, 35);
            this.linkAddFitnessFunction.TabIndex = 1;
            this.linkAddFitnessFunction.TabStop = true;
            this.linkAddFitnessFunction.Text = "Add Fitness Function";
            this.linkAddFitnessFunction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAddFitnessFunction_LinkClicked);
            // 
            // tGAOutput
            // 
            this.tGAOutput.Controls.Add(this.eOptimizationOutput);
            this.tGAOutput.Location = new System.Drawing.Point(4, 24);
            this.tGAOutput.Name = "tGAOutput";
            this.tGAOutput.Size = new System.Drawing.Size(643, 279);
            this.tGAOutput.TabIndex = 2;
            this.tGAOutput.Text = "Output";
            this.tGAOutput.UseVisualStyleBackColor = true;
            // 
            // eOptimizationOutput
            // 
            this.eOptimizationOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eOptimizationOutput.Location = new System.Drawing.Point(0, 0);
            this.eOptimizationOutput.Name = "eOptimizationOutput";
            this.eOptimizationOutput.Size = new System.Drawing.Size(643, 279);
            this.eOptimizationOutput.TabIndex = 25;
            this.eOptimizationOutput.Text = "";
            // 
            // lGASelection
            // 
            this.lGASelection.AutoSize = true;
            this.lGASelection.Location = new System.Drawing.Point(3, 15);
            this.lGASelection.Name = "lGASelection";
            this.lGASelection.Size = new System.Drawing.Size(55, 15);
            this.lGASelection.TabIndex = 26;
            this.lGASelection.Text = "Selection";
            // 
            // cbGACrossOver
            // 
            this.cbGACrossOver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGACrossOver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGACrossOver.FormattingEnabled = true;
            this.cbGACrossOver.Location = new System.Drawing.Point(79, 41);
            this.cbGACrossOver.Name = "cbGACrossOver";
            this.cbGACrossOver.Size = new System.Drawing.Size(191, 23);
            this.cbGACrossOver.TabIndex = 31;
            // 
            // eMaxChromosome
            // 
            this.eMaxChromosome.Location = new System.Drawing.Point(200, 125);
            this.eMaxChromosome.Name = "eMaxChromosome";
            this.eMaxChromosome.Size = new System.Drawing.Size(35, 23);
            this.eMaxChromosome.TabIndex = 36;
            this.eMaxChromosome.Text = "100";
            // 
            // lGAMutation
            // 
            this.lGAMutation.AutoSize = true;
            this.lGAMutation.Location = new System.Drawing.Point(3, 73);
            this.lGAMutation.Name = "lGAMutation";
            this.lGAMutation.Size = new System.Drawing.Size(56, 15);
            this.lGAMutation.TabIndex = 28;
            this.lGAMutation.Text = "Mutation";
            // 
            // cbGAMutation
            // 
            this.cbGAMutation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGAMutation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGAMutation.FormattingEnabled = true;
            this.cbGAMutation.Location = new System.Drawing.Point(79, 70);
            this.cbGAMutation.Name = "cbGAMutation";
            this.cbGAMutation.Size = new System.Drawing.Size(191, 23);
            this.cbGAMutation.TabIndex = 32;
            // 
            // lGAMinMaxChromosome
            // 
            this.lGAMinMaxChromosome.AutoSize = true;
            this.lGAMinMaxChromosome.Location = new System.Drawing.Point(3, 130);
            this.lGAMinMaxChromosome.Name = "lGAMinMaxChromosome";
            this.lGAMinMaxChromosome.Size = new System.Drawing.Size(142, 15);
            this.lGAMinMaxChromosome.TabIndex = 34;
            this.lGAMinMaxChromosome.Text = "Min/Max Chromosome #";
            // 
            // cbGATermination
            // 
            this.cbGATermination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGATermination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGATermination.FormattingEnabled = true;
            this.cbGATermination.Location = new System.Drawing.Point(79, 98);
            this.cbGATermination.Name = "cbGATermination";
            this.cbGATermination.Size = new System.Drawing.Size(191, 23);
            this.cbGATermination.TabIndex = 33;
            // 
            // cbGASelection
            // 
            this.cbGASelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGASelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGASelection.FormattingEnabled = true;
            this.cbGASelection.Location = new System.Drawing.Point(79, 12);
            this.cbGASelection.Name = "cbGASelection";
            this.cbGASelection.Size = new System.Drawing.Size(191, 23);
            this.cbGASelection.TabIndex = 30;
            // 
            // linkOptimize
            // 
            this.linkOptimize.AutoSize = true;
            this.linkOptimize.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkOptimize.Location = new System.Drawing.Point(9, 158);
            this.linkOptimize.Name = "linkOptimize";
            this.linkOptimize.Size = new System.Drawing.Size(55, 15);
            this.linkOptimize.TabIndex = 0;
            this.linkOptimize.TabStop = true;
            this.linkOptimize.Text = "Optimize";
            this.linkOptimize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOptimize_LinkClicked);
            // 
            // eMinChromosome
            // 
            this.eMinChromosome.Location = new System.Drawing.Point(146, 125);
            this.eMinChromosome.Name = "eMinChromosome";
            this.eMinChromosome.Size = new System.Drawing.Size(35, 23);
            this.eMinChromosome.TabIndex = 35;
            this.eMinChromosome.Text = "50";
            // 
            // lGACrossOver
            // 
            this.lGACrossOver.AutoSize = true;
            this.lGACrossOver.Location = new System.Drawing.Point(3, 44);
            this.lGACrossOver.Name = "lGACrossOver";
            this.lGACrossOver.Size = new System.Drawing.Size(61, 15);
            this.lGACrossOver.TabIndex = 27;
            this.lGACrossOver.Text = "CrossOver";
            // 
            // lMinMaxSeperator
            // 
            this.lMinMaxSeperator.AutoSize = true;
            this.lMinMaxSeperator.Location = new System.Drawing.Point(187, 131);
            this.lMinMaxSeperator.Name = "lMinMaxSeperator";
            this.lMinMaxSeperator.Size = new System.Drawing.Size(12, 15);
            this.lMinMaxSeperator.TabIndex = 37;
            this.lMinMaxSeperator.Text = "/";
            // 
            // lGATermination
            // 
            this.lGATermination.AutoSize = true;
            this.lGATermination.Location = new System.Drawing.Point(3, 102);
            this.lGATermination.Name = "lGATermination";
            this.lGATermination.Size = new System.Drawing.Size(70, 15);
            this.lGATermination.TabIndex = 29;
            this.lGATermination.Text = "Termination";
            // 
            // webViewPlots
            // 
            this.webViewPlots.AllowExternalDrop = true;
            this.webViewPlots.CreationProperties = null;
            this.webViewPlots.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewPlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewPlots.Location = new System.Drawing.Point(0, 0);
            this.webViewPlots.Name = "webViewPlots";
            this.webViewPlots.Size = new System.Drawing.Size(939, 305);
            this.webViewPlots.TabIndex = 0;
            this.webViewPlots.ZoomFactor = 1D;
            this.webViewPlots.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webViewPlots_CoreWebView2InitializationCompleted);
            // 
            // pPlots
            // 
            this.pPlots.Controls.Add(this.grPlotSelection);
            this.pPlots.Controls.Add(this.linkSwitchToOptimization);
            this.pPlots.Dock = System.Windows.Forms.DockStyle.Top;
            this.pPlots.Location = new System.Drawing.Point(0, 0);
            this.pPlots.Name = "pPlots";
            this.pPlots.Size = new System.Drawing.Size(941, 61);
            this.pPlots.TabIndex = 1;
            // 
            // grPlotSelection
            // 
            this.grPlotSelection.Controls.Add(this.cbV);
            this.grPlotSelection.Controls.Add(this.cbStimulus);
            this.grPlotSelection.Controls.Add(this.cbSecondaryLists);
            this.grPlotSelection.Controls.Add(this.cbSpikingFrequency);
            this.grPlotSelection.Controls.Add(this.cbTauRise);
            this.grPlotSelection.Controls.Add(this.cbInterval);
            this.grPlotSelection.Controls.Add(this.cbTauDecay);
            this.grPlotSelection.Dock = System.Windows.Forms.DockStyle.Left;
            this.grPlotSelection.Location = new System.Drawing.Point(0, 0);
            this.grPlotSelection.Name = "grPlotSelection";
            this.grPlotSelection.Size = new System.Drawing.Size(522, 61);
            this.grPlotSelection.TabIndex = 8;
            this.grPlotSelection.TabStop = false;
            // 
            // cbV
            // 
            this.cbV.AutoSize = true;
            this.cbV.Checked = true;
            this.cbV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbV.Location = new System.Drawing.Point(6, 14);
            this.cbV.Name = "cbV";
            this.cbV.Size = new System.Drawing.Size(152, 19);
            this.cbV.TabIndex = 0;
            this.cbV.Text = "Membrane Potential (V)";
            this.cbV.UseVisualStyleBackColor = true;
            this.cbV.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbStimulus
            // 
            this.cbStimulus.AutoSize = true;
            this.cbStimulus.Checked = true;
            this.cbStimulus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStimulus.Location = new System.Drawing.Point(429, 14);
            this.cbStimulus.Name = "cbStimulus";
            this.cbStimulus.Size = new System.Drawing.Size(72, 19);
            this.cbStimulus.TabIndex = 6;
            this.cbStimulus.Text = "Stimulus";
            this.cbStimulus.UseVisualStyleBackColor = true;
            this.cbStimulus.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbSecondaryLists
            // 
            this.cbSecondaryLists.AutoSize = true;
            this.cbSecondaryLists.Location = new System.Drawing.Point(6, 34);
            this.cbSecondaryLists.Name = "cbSecondaryLists";
            this.cbSecondaryLists.Size = new System.Drawing.Size(110, 19);
            this.cbSecondaryLists.TabIndex = 1;
            this.cbSecondaryLists.Text = "Other dynamics";
            this.toolTip1.SetToolTip(this.cbSecondaryLists, "depending on the model, can be feedback current, ion based currents, relative ten" +
        "sion, etc");
            this.cbSecondaryLists.UseVisualStyleBackColor = true;
            this.cbSecondaryLists.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbSpikingFrequency
            // 
            this.cbSpikingFrequency.AutoSize = true;
            this.cbSpikingFrequency.Location = new System.Drawing.Point(290, 31);
            this.cbSpikingFrequency.Name = "cbSpikingFrequency";
            this.cbSpikingFrequency.Size = new System.Drawing.Size(123, 19);
            this.cbSpikingFrequency.TabIndex = 5;
            this.cbSpikingFrequency.Text = "Spiking Frequency";
            this.cbSpikingFrequency.UseVisualStyleBackColor = true;
            this.cbSpikingFrequency.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbTauRise
            // 
            this.cbTauRise.AutoSize = true;
            this.cbTauRise.Location = new System.Drawing.Point(177, 14);
            this.cbTauRise.Name = "cbTauRise";
            this.cbTauRise.Size = new System.Drawing.Size(68, 19);
            this.cbTauRise.TabIndex = 2;
            this.cbTauRise.Text = "Tau Rise";
            this.cbTauRise.UseVisualStyleBackColor = true;
            this.cbTauRise.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbInterval
            // 
            this.cbInterval.AutoSize = true;
            this.cbInterval.Location = new System.Drawing.Point(290, 11);
            this.cbInterval.Name = "cbInterval";
            this.cbInterval.Size = new System.Drawing.Size(65, 19);
            this.cbInterval.TabIndex = 4;
            this.cbInterval.Text = "Interval";
            this.cbInterval.UseVisualStyleBackColor = true;
            this.cbInterval.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbTauDecay
            // 
            this.cbTauDecay.AutoSize = true;
            this.cbTauDecay.Location = new System.Drawing.Point(177, 34);
            this.cbTauDecay.Name = "cbTauDecay";
            this.cbTauDecay.Size = new System.Drawing.Size(79, 19);
            this.cbTauDecay.TabIndex = 3;
            this.cbTauDecay.Text = "Tau Decay";
            this.cbTauDecay.UseVisualStyleBackColor = true;
            this.cbTauDecay.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // linkSwitchToOptimization
            // 
            this.linkSwitchToOptimization.AutoSize = true;
            this.linkSwitchToOptimization.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSwitchToOptimization.Location = new System.Drawing.Point(528, 38);
            this.linkSwitchToOptimization.Name = "linkSwitchToOptimization";
            this.linkSwitchToOptimization.Size = new System.Drawing.Size(110, 15);
            this.linkSwitchToOptimization.TabIndex = 7;
            this.linkSwitchToOptimization.TabStop = true;
            this.linkSwitchToOptimization.Text = "Optimization Mode";
            this.linkSwitchToOptimization.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSwitchToOptimization_LinkClicked);
            // 
            // saveFileJson
            // 
            this.saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // openFileJson
            // 
            this.openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // timerOptimization
            // 
            this.timerOptimization.Tick += new System.EventHandler(this.timerOptimization_Tick);
            // 
            // DynamicsTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitMain);
            this.Name = "DynamicsTestControl";
            this.Size = new System.Drawing.Size(1252, 683);
            this.Load += new System.EventHandler(this.DynamicsTestControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eRheobaseLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepStartTime)).EndInit();
            this.grRheobase.ResumeLayout(false);
            this.grRheobase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eRheobaseDuration)).EndInit();
            this.grTest.ResumeLayout(false);
            this.grTest.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel1.PerformLayout();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tabAnalysis.ResumeLayout(false);
            this.tTest.ResumeLayout(false);
            this.tSensitivityAnalysis.ResumeLayout(false);
            this.grSensitivity.ResumeLayout(false);
            this.grSensitivity.PerformLayout();
            this.pCoreType.ResumeLayout(false);
            this.pCoreType.PerformLayout();
            this.pLoadSaveParams.ResumeLayout(false);
            this.pLoadSaveParams.PerformLayout();
            this.pBottomBottom.ResumeLayout(false);
            this.pBottomBottom.PerformLayout();
            this.splitGAAndPlots.Panel1.ResumeLayout(false);
            this.splitGAAndPlots.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGAAndPlots)).EndInit();
            this.splitGAAndPlots.ResumeLayout(false);
            this.pOptimize.ResumeLayout(false);
            this.splitGA.Panel1.ResumeLayout(false);
            this.splitGA.Panel2.ResumeLayout(false);
            this.splitGA.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGA)).EndInit();
            this.splitGA.ResumeLayout(false);
            this.tabOptimization.ResumeLayout(false);
            this.tGAParameters.ResumeLayout(false);
            this.splitGAParams.Panel1.ResumeLayout(false);
            this.splitGAParams.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGAParams)).EndInit();
            this.splitGAParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMinMaxValues)).EndInit();
            this.pMinMax.ResumeLayout(false);
            this.pMinMax.PerformLayout();
            this.pfGAParams.ResumeLayout(false);
            this.pfGAParams.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tGAOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlots)).EndInit();
            this.pPlots.ResumeLayout(false);
            this.pPlots.PerformLayout();
            this.grPlotSelection.ResumeLayout(false);
            this.grPlotSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private NumericUpDown eRheobaseLimit;
        private Label lRheobaseLimit;
        private Button btnRheobase;
        private Label ldt;
        private NumericUpDown edt;
        private NumericUpDown ePlotEndTime;
        private Button btnDynamicsRun;
        private NumericUpDown eStepEndTime;
        private Label lStepStartTime;
        private NumericUpDown eStepStartTime;
        private Label lStepEndTime;
        private Label lPlotEndTime;
        private GroupBox grRheobase;
        private TextBox eRheobase;
        private Label lRheobase;
        private GroupBox grTest;
        private SplitContainer splitMain;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewPlots;
        private NumericUpDown eRheobaseDuration;
        private Label label1;
        private Button btnSensitivityAnalysis;
        private GroupBox grSensitivity;
        private TextBox eMaxMultiplier;
        private Label lDash;
        private TextBox eMinMultiplier;
        private Label lMultiplierRange;
        private ComboBox ddParameter;
        private Label lParameter;
        private CheckBox cbLogScale;
        private RadioButton rbManualEntryStimulus;
        private RadioButton rbRheobaseBasedStimulus;
        private FlowLayoutPanel pfParams;
        private Panel pCoreType;
        private LinkLabel linkSaveCoreUnit;
        private LinkLabel linkLoadCoreUnit;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private Panel pPlots;
        private CheckBox cbStimulus;
        private CheckBox cbSpikingFrequency;
        private CheckBox cbInterval;
        private CheckBox cbTauDecay;
        private CheckBox cbTauRise;
        private CheckBox cbSecondaryLists;
        private CheckBox cbV;
        private Panel pBottomBottom;
        private LinkLabel linkUseUpdatedParams;
        private LinkLabel linkOptimize;
        private RichTextBox eOptimizationOutput;
        private DataGridView dgMinMaxValues;
        private DataGridViewTextBoxColumn colParameter;
        private DataGridViewTextBoxColumn colMinValue;
        private DataGridViewTextBoxColumn colMaxValue;
        private Panel pMinMax;
        private LinkLabel linkSuggestMinMax;
        private ToolTip toolTip1;
        private Label lGASelection;
        private ComboBox cbGATermination;
        private ComboBox cbGAMutation;
        private ComboBox cbGACrossOver;
        private ComboBox cbGASelection;
        private Label lGATermination;
        private Label lGAMutation;
        private Label lGACrossOver;
        private Label lMinMaxSeperator;
        private TextBox eMaxChromosome;
        private TextBox eMinChromosome;
        private Label lGAMinMaxChromosome;
        private LinkLabel linkSwitchToOptimization;
        private GroupBox grPlotSelection;
        private System.Windows.Forms.Timer timerOptimization;
        private SplitContainer splitGAAndPlots;
        private Panel pOptimize;
        private TabControl tabOptimization;
        private TabPage tGAParameters;
        private TabPage tGAOutput;
        private ComboBox ddCoreType;
        private Label lCoreType;
        private Panel pLoadSaveParams;
        private CheckBox cbAutoDrawPlots;
        private SplitContainer splitGAParams;
        private FlowLayoutPanel pfGAParams;
        private LinkLabel linkAddFitnessFunction;
        private TabControl tabAnalysis;
        private TabPage tTest;
        private TabPage tSensitivityAnalysis;
        private StimulusControl stimulusControl1;
        private Label lRheobaseWarning;
        private SplitContainer splitGA;
        private Panel panel1;
        private Label lFitnessFunctions;
    }
}
