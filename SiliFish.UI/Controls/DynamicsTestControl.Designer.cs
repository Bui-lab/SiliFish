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
            this.lPlotdt = new System.Windows.Forms.Label();
            this.edt = new System.Windows.Forms.NumericUpDown();
            this.ePlotEndTime = new System.Windows.Forms.NumericUpDown();
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
            this.edtEuler = new System.Windows.Forms.NumericUpDown();
            this.stimulusControl1 = new SiliFish.UI.Controls.StimulusControl();
            this.rbSingleEntryStimulus = new System.Windows.Forms.RadioButton();
            this.rbRheobaseBasedStimulus = new System.Windows.Forms.RadioButton();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pfParams = new System.Windows.Forms.FlowLayoutPanel();
            this.pRheobase = new System.Windows.Forms.Panel();
            this.tabAnalysis = new System.Windows.Forms.TabControl();
            this.tTest = new System.Windows.Forms.TabPage();
            this.pSep2 = new System.Windows.Forms.Panel();
            this.pSep1 = new System.Windows.Forms.Panel();
            this.btnDynamicsRun = new System.Windows.Forms.Button();
            this.cbAutoDrawPlots = new System.Windows.Forms.CheckBox();
            this.eMultipleStimulus = new System.Windows.Forms.TextBox();
            this.lMultipleEntryNote = new System.Windows.Forms.Label();
            this.rbMultipleEntry = new System.Windows.Forms.RadioButton();
            this.lEulerdt = new System.Windows.Forms.Label();
            this.lms2 = new System.Windows.Forms.Label();
            this.lms = new System.Windows.Forms.Label();
            this.tSensitivityAnalysis = new System.Windows.Forms.TabPage();
            this.grFiring = new System.Windows.Forms.GroupBox();
            this.sensitivityAnalysisFiring = new SiliFish.UI.Controls.SensitivityAnalysisControl();
            this.grRheoSens = new System.Windows.Forms.GroupBox();
            this.sensitivityAnalysisRheobase = new SiliFish.UI.Controls.SensitivityAnalysisControl();
            this.pCoreType = new System.Windows.Forms.Panel();
            this.pLineCoreType = new System.Windows.Forms.Panel();
            this.ddCoreType = new System.Windows.Forms.ComboBox();
            this.lCoreType = new System.Windows.Forms.Label();
            this.pLoadSaveParams = new System.Windows.Forms.Panel();
            this.linkLoadCoreUnit = new System.Windows.Forms.LinkLabel();
            this.linkSaveCoreUnit = new System.Windows.Forms.LinkLabel();
            this.pBottomBottom = new System.Windows.Forms.Panel();
            this.linkUseUpdatedParams = new System.Windows.Forms.LinkLabel();
            this.splitGAAndPlots = new System.Windows.Forms.SplitContainer();
            this.pOptimize = new System.Windows.Forms.Panel();
            this.gaControl = new SiliFish.UI.Controls.GAControl();
            this.webViewPlots = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pPlots = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
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
            ((System.ComponentModel.ISupportInitialize)(this.eRheobaseLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepStartTime)).BeginInit();
            this.grRheobase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eRheobaseDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEuler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pRheobase.SuspendLayout();
            this.tabAnalysis.SuspendLayout();
            this.tTest.SuspendLayout();
            this.tSensitivityAnalysis.SuspendLayout();
            this.grFiring.SuspendLayout();
            this.grRheoSens.SuspendLayout();
            this.pCoreType.SuspendLayout();
            this.pLoadSaveParams.SuspendLayout();
            this.pBottomBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGAAndPlots)).BeginInit();
            this.splitGAAndPlots.Panel1.SuspendLayout();
            this.splitGAAndPlots.Panel2.SuspendLayout();
            this.splitGAAndPlots.SuspendLayout();
            this.pOptimize.SuspendLayout();
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
            // lPlotdt
            // 
            this.lPlotdt.AutoSize = true;
            this.lPlotdt.Location = new System.Drawing.Point(6, 69);
            this.lPlotdt.Name = "lPlotdt";
            this.lPlotdt.Size = new System.Drawing.Size(43, 15);
            this.lPlotdt.TabIndex = 10;
            this.lPlotdt.Text = "Plot Δt";
            // 
            // edt
            // 
            this.edt.DecimalPlaces = 2;
            this.edt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edt.Location = new System.Drawing.Point(69, 65);
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
            this.ePlotEndTime.Location = new System.Drawing.Point(69, 38);
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
            // eStepEndTime
            // 
            this.eStepEndTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eStepEndTime.Location = new System.Drawing.Point(172, 9);
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
            this.lStepStartTime.Location = new System.Drawing.Point(6, 13);
            this.lStepStartTime.Name = "lStepStartTime";
            this.lStepStartTime.Size = new System.Drawing.Size(61, 15);
            this.lStepStartTime.TabIndex = 4;
            this.lStepStartTime.Text = "Stim. Start";
            // 
            // eStepStartTime
            // 
            this.eStepStartTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eStepStartTime.Location = new System.Drawing.Point(69, 9);
            this.eStepStartTime.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
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
            this.lStepEndTime.Location = new System.Drawing.Point(141, 13);
            this.lStepEndTime.Name = "lStepEndTime";
            this.lStepEndTime.Size = new System.Drawing.Size(27, 15);
            this.lStepEndTime.TabIndex = 5;
            this.lStepEndTime.Text = "End";
            // 
            // lPlotEndTime
            // 
            this.lPlotEndTime.AutoSize = true;
            this.lPlotEndTime.Location = new System.Drawing.Point(6, 40);
            this.lPlotEndTime.Name = "lPlotEndTime";
            this.lPlotEndTime.Size = new System.Drawing.Size(51, 15);
            this.lPlotEndTime.TabIndex = 6;
            this.lPlotEndTime.Text = "Plot End";
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
            this.grRheobase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grRheobase.Location = new System.Drawing.Point(0, 0);
            this.grRheobase.Name = "grRheobase";
            this.grRheobase.Size = new System.Drawing.Size(299, 124);
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
            // edtEuler
            // 
            this.edtEuler.DecimalPlaces = 2;
            this.edtEuler.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edtEuler.Location = new System.Drawing.Point(195, 65);
            this.edtEuler.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edtEuler.Name = "edtEuler";
            this.edtEuler.Size = new System.Drawing.Size(66, 23);
            this.edtEuler.TabIndex = 26;
            this.edtEuler.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // stimulusControl1
            // 
            this.stimulusControl1.BackColor = System.Drawing.Color.White;
            this.stimulusControl1.Location = new System.Drawing.Point(6, 191);
            this.stimulusControl1.Name = "stimulusControl1";
            this.stimulusControl1.Size = new System.Drawing.Size(176, 93);
            this.stimulusControl1.TabIndex = 25;
            this.stimulusControl1.StimulusChanged += new System.EventHandler(this.stimulusControl1_StimulusChanged);
            // 
            // rbSingleEntryStimulus
            // 
            this.rbSingleEntryStimulus.AutoSize = true;
            this.rbSingleEntryStimulus.Checked = true;
            this.rbSingleEntryStimulus.Location = new System.Drawing.Point(6, 174);
            this.rbSingleEntryStimulus.Name = "rbSingleEntryStimulus";
            this.rbSingleEntryStimulus.Size = new System.Drawing.Size(87, 19);
            this.rbSingleEntryStimulus.TabIndex = 12;
            this.rbSingleEntryStimulus.TabStop = true;
            this.rbSingleEntryStimulus.Text = "Single Entry";
            this.rbSingleEntryStimulus.UseVisualStyleBackColor = true;
            this.rbSingleEntryStimulus.CheckedChanged += new System.EventHandler(this.rbManualEntryStimulus_CheckedChanged);
            // 
            // rbRheobaseBasedStimulus
            // 
            this.rbRheobaseBasedStimulus.AutoSize = true;
            this.rbRheobaseBasedStimulus.Location = new System.Drawing.Point(6, 94);
            this.rbRheobaseBasedStimulus.Name = "rbRheobaseBasedStimulus";
            this.rbRheobaseBasedStimulus.Size = new System.Drawing.Size(187, 19);
            this.rbRheobaseBasedStimulus.TabIndex = 13;
            this.rbRheobaseBasedStimulus.Text = "Rheobase based (x1, x1.1, x1.5)";
            this.toolTip1.SetToolTip(this.rbRheobaseBasedStimulus, "Step stimulus with 0 noise");
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
            this.splitMain.Panel1.Controls.Add(this.pRheobase);
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
            this.pfParams.Location = new System.Drawing.Point(3, 43);
            this.pfParams.Name = "pfParams";
            this.pfParams.Size = new System.Drawing.Size(299, 118);
            this.pfParams.TabIndex = 23;
            // 
            // pRheobase
            // 
            this.pRheobase.Controls.Add(this.grRheobase);
            this.pRheobase.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pRheobase.Location = new System.Drawing.Point(3, 161);
            this.pRheobase.Name = "pRheobase";
            this.pRheobase.Size = new System.Drawing.Size(299, 124);
            this.pRheobase.TabIndex = 40;
            // 
            // tabAnalysis
            // 
            this.tabAnalysis.Controls.Add(this.tTest);
            this.tabAnalysis.Controls.Add(this.tSensitivityAnalysis);
            this.tabAnalysis.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabAnalysis.Location = new System.Drawing.Point(3, 285);
            this.tabAnalysis.Name = "tabAnalysis";
            this.tabAnalysis.SelectedIndex = 0;
            this.tabAnalysis.Size = new System.Drawing.Size(299, 339);
            this.tabAnalysis.TabIndex = 39;
            // 
            // tTest
            // 
            this.tTest.Controls.Add(this.pSep2);
            this.tTest.Controls.Add(this.pSep1);
            this.tTest.Controls.Add(this.btnDynamicsRun);
            this.tTest.Controls.Add(this.cbAutoDrawPlots);
            this.tTest.Controls.Add(this.eMultipleStimulus);
            this.tTest.Controls.Add(this.lMultipleEntryNote);
            this.tTest.Controls.Add(this.rbRheobaseBasedStimulus);
            this.tTest.Controls.Add(this.rbMultipleEntry);
            this.tTest.Controls.Add(this.lPlotdt);
            this.tTest.Controls.Add(this.lEulerdt);
            this.tTest.Controls.Add(this.eStepEndTime);
            this.tTest.Controls.Add(this.lms2);
            this.tTest.Controls.Add(this.lStepStartTime);
            this.tTest.Controls.Add(this.lms);
            this.tTest.Controls.Add(this.ePlotEndTime);
            this.tTest.Controls.Add(this.edtEuler);
            this.tTest.Controls.Add(this.eStepStartTime);
            this.tTest.Controls.Add(this.stimulusControl1);
            this.tTest.Controls.Add(this.lStepEndTime);
            this.tTest.Controls.Add(this.lPlotEndTime);
            this.tTest.Controls.Add(this.rbSingleEntryStimulus);
            this.tTest.Controls.Add(this.edt);
            this.tTest.Location = new System.Drawing.Point(4, 24);
            this.tTest.Name = "tTest";
            this.tTest.Padding = new System.Windows.Forms.Padding(3);
            this.tTest.Size = new System.Drawing.Size(291, 311);
            this.tTest.TabIndex = 0;
            this.tTest.Text = "Test";
            this.tTest.UseVisualStyleBackColor = true;
            // 
            // pSep2
            // 
            this.pSep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pSep2.BackColor = System.Drawing.Color.DimGray;
            this.pSep2.Location = new System.Drawing.Point(6, 167);
            this.pSep2.Name = "pSep2";
            this.pSep2.Size = new System.Drawing.Size(279, 3);
            this.pSep2.TabIndex = 37;
            // 
            // pSep1
            // 
            this.pSep1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pSep1.BackColor = System.Drawing.Color.DimGray;
            this.pSep1.Location = new System.Drawing.Point(6, 115);
            this.pSep1.Name = "pSep1";
            this.pSep1.Size = new System.Drawing.Size(279, 3);
            this.pSep1.TabIndex = 36;
            // 
            // btnDynamicsRun
            // 
            this.btnDynamicsRun.Location = new System.Drawing.Point(107, 282);
            this.btnDynamicsRun.Name = "btnDynamicsRun";
            this.btnDynamicsRun.Size = new System.Drawing.Size(66, 23);
            this.btnDynamicsRun.TabIndex = 35;
            this.btnDynamicsRun.Text = "Run";
            this.btnDynamicsRun.UseVisualStyleBackColor = true;
            this.btnDynamicsRun.Click += new System.EventHandler(this.btnDynamicsRun_Click);
            // 
            // cbAutoDrawPlots
            // 
            this.cbAutoDrawPlots.AutoSize = true;
            this.cbAutoDrawPlots.Location = new System.Drawing.Point(9, 286);
            this.cbAutoDrawPlots.Name = "cbAutoDrawPlots";
            this.cbAutoDrawPlots.Size = new System.Drawing.Size(82, 19);
            this.cbAutoDrawPlots.TabIndex = 34;
            this.cbAutoDrawPlots.Text = "Auto Draw";
            this.cbAutoDrawPlots.UseVisualStyleBackColor = true;
            this.cbAutoDrawPlots.CheckedChanged += new System.EventHandler(this.cbAutoDrawPlots_CheckedChanged);
            // 
            // eMultipleStimulus
            // 
            this.eMultipleStimulus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eMultipleStimulus.Location = new System.Drawing.Point(8, 141);
            this.eMultipleStimulus.Name = "eMultipleStimulus";
            this.eMultipleStimulus.ReadOnly = true;
            this.eMultipleStimulus.Size = new System.Drawing.Size(276, 23);
            this.eMultipleStimulus.TabIndex = 33;
            this.eMultipleStimulus.Text = "example: 1;10";
            this.toolTip1.SetToolTip(this.eMultipleStimulus, "Step stimulus with 0 noise");
            // 
            // lMultipleEntryNote
            // 
            this.lMultipleEntryNote.AutoSize = true;
            this.lMultipleEntryNote.Location = new System.Drawing.Point(105, 121);
            this.lMultipleEntryNote.Name = "lMultipleEntryNote";
            this.lMultipleEntryNote.Size = new System.Drawing.Size(174, 15);
            this.lMultipleEntryNote.TabIndex = 32;
            this.lMultipleEntryNote.Text = "(enter values seperated with \";\")";
            // 
            // rbMultipleEntry
            // 
            this.rbMultipleEntry.AutoSize = true;
            this.rbMultipleEntry.Location = new System.Drawing.Point(6, 119);
            this.rbMultipleEntry.Name = "rbMultipleEntry";
            this.rbMultipleEntry.Size = new System.Drawing.Size(99, 19);
            this.rbMultipleEntry.TabIndex = 31;
            this.rbMultipleEntry.Text = "Multiple Entry";
            this.toolTip1.SetToolTip(this.rbMultipleEntry, "Step stimulus with 0 noise");
            this.rbMultipleEntry.UseVisualStyleBackColor = true;
            this.rbMultipleEntry.CheckedChanged += new System.EventHandler(this.rbMultipleEntry_CheckedChanged);
            // 
            // lEulerdt
            // 
            this.lEulerdt.AutoSize = true;
            this.lEulerdt.Location = new System.Drawing.Point(141, 69);
            this.lEulerdt.Name = "lEulerdt";
            this.lEulerdt.Size = new System.Drawing.Size(48, 15);
            this.lEulerdt.TabIndex = 30;
            this.lEulerdt.Text = "Euler Δt";
            // 
            // lms2
            // 
            this.lms2.AutoSize = true;
            this.lms2.Location = new System.Drawing.Point(142, 40);
            this.lms2.Name = "lms2";
            this.lms2.Size = new System.Drawing.Size(31, 15);
            this.lms2.TabIndex = 28;
            this.lms2.Text = "(ms)";
            // 
            // lms
            // 
            this.lms.AutoSize = true;
            this.lms.Location = new System.Drawing.Point(248, 13);
            this.lms.Name = "lms";
            this.lms.Size = new System.Drawing.Size(31, 15);
            this.lms.TabIndex = 27;
            this.lms.Text = "(ms)";
            // 
            // tSensitivityAnalysis
            // 
            this.tSensitivityAnalysis.Controls.Add(this.grFiring);
            this.tSensitivityAnalysis.Controls.Add(this.grRheoSens);
            this.tSensitivityAnalysis.Location = new System.Drawing.Point(4, 24);
            this.tSensitivityAnalysis.Name = "tSensitivityAnalysis";
            this.tSensitivityAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tSensitivityAnalysis.Size = new System.Drawing.Size(291, 311);
            this.tSensitivityAnalysis.TabIndex = 1;
            this.tSensitivityAnalysis.Text = "Sensitivity Analysis";
            this.tSensitivityAnalysis.UseVisualStyleBackColor = true;
            // 
            // grFiring
            // 
            this.grFiring.Controls.Add(this.sensitivityAnalysisFiring);
            this.grFiring.Dock = System.Windows.Forms.DockStyle.Top;
            this.grFiring.Location = new System.Drawing.Point(3, 147);
            this.grFiring.Name = "grFiring";
            this.grFiring.Size = new System.Drawing.Size(285, 138);
            this.grFiring.TabIndex = 19;
            this.grFiring.TabStop = false;
            this.grFiring.Text = "Firing Pattern";
            // 
            // sensitivityAnalysisFiring
            // 
            this.sensitivityAnalysisFiring.Location = new System.Drawing.Point(6, 18);
            this.sensitivityAnalysisFiring.Name = "sensitivityAnalysisFiring";
            this.sensitivityAnalysisFiring.Size = new System.Drawing.Size(215, 120);
            this.sensitivityAnalysisFiring.TabIndex = 0;
            // 
            // grRheoSens
            // 
            this.grRheoSens.Controls.Add(this.sensitivityAnalysisRheobase);
            this.grRheoSens.Dock = System.Windows.Forms.DockStyle.Top;
            this.grRheoSens.Location = new System.Drawing.Point(3, 3);
            this.grRheoSens.Name = "grRheoSens";
            this.grRheoSens.Size = new System.Drawing.Size(285, 144);
            this.grRheoSens.TabIndex = 18;
            this.grRheoSens.TabStop = false;
            this.grRheoSens.Text = "Rheobase Sensitivity";
            // 
            // sensitivityAnalysisRheobase
            // 
            this.sensitivityAnalysisRheobase.Location = new System.Drawing.Point(3, 22);
            this.sensitivityAnalysisRheobase.Name = "sensitivityAnalysisRheobase";
            this.sensitivityAnalysisRheobase.Size = new System.Drawing.Size(215, 120);
            this.sensitivityAnalysisRheobase.TabIndex = 0;
            // 
            // pCoreType
            // 
            this.pCoreType.Controls.Add(this.pLineCoreType);
            this.pCoreType.Controls.Add(this.ddCoreType);
            this.pCoreType.Controls.Add(this.lCoreType);
            this.pCoreType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCoreType.Location = new System.Drawing.Point(3, 3);
            this.pCoreType.Name = "pCoreType";
            this.pCoreType.Size = new System.Drawing.Size(299, 40);
            this.pCoreType.TabIndex = 21;
            // 
            // pLineCoreType
            // 
            this.pLineCoreType.BackColor = System.Drawing.Color.LightGray;
            this.pLineCoreType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineCoreType.Location = new System.Drawing.Point(0, 39);
            this.pLineCoreType.Name = "pLineCoreType";
            this.pLineCoreType.Size = new System.Drawing.Size(299, 1);
            this.pLineCoreType.TabIndex = 24;
            // 
            // ddCoreType
            // 
            this.ddCoreType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddCoreType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddCoreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCoreType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddCoreType.FormattingEnabled = true;
            this.ddCoreType.Location = new System.Drawing.Point(71, 6);
            this.ddCoreType.Name = "ddCoreType";
            this.ddCoreType.Size = new System.Drawing.Size(221, 23);
            this.ddCoreType.TabIndex = 23;
            this.ddCoreType.SelectedIndexChanged += new System.EventHandler(this.ddCoreType_SelectedIndexChanged);
            // 
            // lCoreType
            // 
            this.lCoreType.AutoSize = true;
            this.lCoreType.Location = new System.Drawing.Point(6, 9);
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
            this.splitGAAndPlots.Location = new System.Drawing.Point(0, 65);
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
            this.splitGAAndPlots.Size = new System.Drawing.Size(941, 618);
            this.splitGAAndPlots.SplitterDistance = 309;
            this.splitGAAndPlots.TabIndex = 2;
            // 
            // pOptimize
            // 
            this.pOptimize.Controls.Add(this.gaControl);
            this.pOptimize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pOptimize.Location = new System.Drawing.Point(0, 0);
            this.pOptimize.Name = "pOptimize";
            this.pOptimize.Size = new System.Drawing.Size(939, 307);
            this.pOptimize.TabIndex = 39;
            // 
            // gaControl
            // 
            this.gaControl.CoreType = null;
            this.gaControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gaControl.Location = new System.Drawing.Point(0, 0);
            this.gaControl.Name = "gaControl";
            this.gaControl.Parameters = null;
            this.gaControl.Size = new System.Drawing.Size(939, 307);
            this.gaControl.TabIndex = 0;
            // 
            // webViewPlots
            // 
            this.webViewPlots.AllowExternalDrop = true;
            this.webViewPlots.CreationProperties = null;
            this.webViewPlots.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewPlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewPlots.Location = new System.Drawing.Point(0, 0);
            this.webViewPlots.Name = "webViewPlots";
            this.webViewPlots.Size = new System.Drawing.Size(939, 303);
            this.webViewPlots.TabIndex = 0;
            this.webViewPlots.ZoomFactor = 1D;
            this.webViewPlots.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webViewPlots_CoreWebView2InitializationCompleted);
            // 
            // pPlots
            // 
            this.pPlots.Controls.Add(this.panel1);
            this.pPlots.Controls.Add(this.grPlotSelection);
            this.pPlots.Controls.Add(this.linkSwitchToOptimization);
            this.pPlots.Dock = System.Windows.Forms.DockStyle.Top;
            this.pPlots.Location = new System.Drawing.Point(0, 0);
            this.pPlots.Name = "pPlots";
            this.pPlots.Size = new System.Drawing.Size(941, 65);
            this.pPlots.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(941, 1);
            this.panel1.TabIndex = 9;
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
            this.grPlotSelection.Location = new System.Drawing.Point(10, 0);
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
            this.linkSwitchToOptimization.Location = new System.Drawing.Point(538, 43);
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
            ((System.ComponentModel.ISupportInitialize)(this.edtEuler)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel1.PerformLayout();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pRheobase.ResumeLayout(false);
            this.tabAnalysis.ResumeLayout(false);
            this.tTest.ResumeLayout(false);
            this.tTest.PerformLayout();
            this.tSensitivityAnalysis.ResumeLayout(false);
            this.grFiring.ResumeLayout(false);
            this.grRheoSens.ResumeLayout(false);
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
        private Label lPlotdt;
        private NumericUpDown edt;
        private NumericUpDown ePlotEndTime;
        private NumericUpDown eStepEndTime;
        private Label lStepStartTime;
        private NumericUpDown eStepStartTime;
        private Label lStepEndTime;
        private Label lPlotEndTime;
        private GroupBox grRheobase;
        private TextBox eRheobase;
        private Label lRheobase;
        private SplitContainer splitMain;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewPlots;
        private NumericUpDown eRheobaseDuration;
        private Label label1;
        private GroupBox grRheoSens;
        private RadioButton rbSingleEntryStimulus;
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
        private ToolTip toolTip1;
        private LinkLabel linkSwitchToOptimization;
        private GroupBox grPlotSelection;
        private SplitContainer splitGAAndPlots;
        private Panel pOptimize;
        private ComboBox ddCoreType;
        private Label lCoreType;
        private Panel pLoadSaveParams;
        private TabControl tabAnalysis;
        private TabPage tTest;
        private TabPage tSensitivityAnalysis;
        private StimulusControl stimulusControl1;
        private Label lRheobaseWarning;
        private Panel pLineCoreType;
        private Panel panel1;
        private Controls.GAControl gaControl;
        private GroupBox grFiring;
        private Controls.SensitivityAnalysisControl sensitivityAnalysisFiring;
        private Controls.SensitivityAnalysisControl sensitivityAnalysisRheobase;
        private NumericUpDown edtEuler;
        private Label lms2;
        private Label lms;
        private Panel pRheobase;
        private Label lEulerdt;
        private TextBox eMultipleStimulus;
        private Label lMultipleEntryNote;
        private RadioButton rbMultipleEntry;
        private CheckBox cbAutoDrawPlots;
        private Panel pSep2;
        private Panel pSep1;
        private Button btnDynamicsRun;
    }
}
