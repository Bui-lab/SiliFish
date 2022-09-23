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
            this.eRheobaseDuration = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.eRheobase = new System.Windows.Forms.TextBox();
            this.lRheobase = new System.Windows.Forms.Label();
            this.btnSensitivityAnalysis = new System.Windows.Forms.Button();
            this.grTest = new System.Windows.Forms.GroupBox();
            this.rbManualEntryStimulus = new System.Windows.Forms.RadioButton();
            this.stimulusControl1 = new SiliFish.UI.Controls.StimulusControl();
            this.rbRheobaseBasedStimulus = new System.Windows.Forms.RadioButton();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitLeft = new System.Windows.Forms.SplitContainer();
            this.pParams = new System.Windows.Forms.FlowLayoutPanel();
            this.tabLeft = new System.Windows.Forms.TabControl();
            this.tTest = new System.Windows.Forms.TabPage();
            this.tOptimize = new System.Windows.Forms.TabPage();
            this.pOptimization = new System.Windows.Forms.Panel();
            this.dgMinMaxValues = new System.Windows.Forms.DataGridView();
            this.colParameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pMinMax = new System.Windows.Forms.Panel();
            this.pOptimize = new System.Windows.Forms.Panel();
            this.eTargetRheobase = new System.Windows.Forms.TextBox();
            this.lTargetRheobase = new System.Windows.Forms.Label();
            this.linkOptimize = new System.Windows.Forms.LinkLabel();
            this.eOptimizationOutput = new System.Windows.Forms.RichTextBox();
            this.grSensitivity = new System.Windows.Forms.GroupBox();
            this.cbLogScale = new System.Windows.Forms.CheckBox();
            this.eMaxMultiplier = new System.Windows.Forms.TextBox();
            this.lDash = new System.Windows.Forms.Label();
            this.eMinMultiplier = new System.Windows.Forms.TextBox();
            this.lMultiplierRange = new System.Windows.Forms.Label();
            this.ddParameter = new System.Windows.Forms.ComboBox();
            this.lParameter = new System.Windows.Forms.Label();
            this.pLoadSaveParams = new System.Windows.Forms.Panel();
            this.linkSaveParams = new System.Windows.Forms.LinkLabel();
            this.linkLoadParams = new System.Windows.Forms.LinkLabel();
            this.pBottomBottom = new System.Windows.Forms.Panel();
            this.linkUseUpdatedParams = new System.Windows.Forms.LinkLabel();
            this.webViewPlots = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pPlots = new System.Windows.Forms.Panel();
            this.cbStimulus = new System.Windows.Forms.CheckBox();
            this.cbSpikingFrequency = new System.Windows.Forms.CheckBox();
            this.cbInterval = new System.Windows.Forms.CheckBox();
            this.cbTauDecay = new System.Windows.Forms.CheckBox();
            this.cbTauRise = new System.Windows.Forms.CheckBox();
            this.cbURelTension = new System.Windows.Forms.CheckBox();
            this.cbV = new System.Windows.Forms.CheckBox();
            this.saveFileJson = new System.Windows.Forms.SaveFileDialog();
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            this.linkSuggestMinMax = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            ((System.ComponentModel.ISupportInitialize)(this.splitLeft)).BeginInit();
            this.splitLeft.Panel1.SuspendLayout();
            this.splitLeft.Panel2.SuspendLayout();
            this.splitLeft.SuspendLayout();
            this.tabLeft.SuspendLayout();
            this.tTest.SuspendLayout();
            this.tOptimize.SuspendLayout();
            this.pOptimization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMinMaxValues)).BeginInit();
            this.pMinMax.SuspendLayout();
            this.pOptimize.SuspendLayout();
            this.grSensitivity.SuspendLayout();
            this.pLoadSaveParams.SuspendLayout();
            this.pBottomBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlots)).BeginInit();
            this.pPlots.SuspendLayout();
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
            this.btnRheobase.Location = new System.Drawing.Point(128, 22);
            this.btnRheobase.Name = "btnRheobase";
            this.btnRheobase.Size = new System.Drawing.Size(66, 23);
            this.btnRheobase.TabIndex = 13;
            this.btnRheobase.Text = "Calculate";
            this.btnRheobase.UseVisualStyleBackColor = true;
            this.btnRheobase.Click += new System.EventHandler(this.btnRheobase_Click);
            // 
            // ldt
            // 
            this.ldt.AutoSize = true;
            this.ldt.Location = new System.Drawing.Point(9, 243);
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
            this.edt.Location = new System.Drawing.Point(100, 241);
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
            this.ePlotEndTime.Location = new System.Drawing.Point(100, 216);
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
            this.btnDynamicsRun.Location = new System.Drawing.Point(99, 270);
            this.btnDynamicsRun.Name = "btnDynamicsRun";
            this.btnDynamicsRun.Size = new System.Drawing.Size(67, 23);
            this.btnDynamicsRun.TabIndex = 3;
            this.btnDynamicsRun.Text = "Run";
            this.btnDynamicsRun.UseVisualStyleBackColor = true;
            this.btnDynamicsRun.Click += new System.EventHandler(this.btnDynamicsRun_Click);
            // 
            // eStepEndTime
            // 
            this.eStepEndTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eStepEndTime.Location = new System.Drawing.Point(100, 192);
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
            this.lStepStartTime.Location = new System.Drawing.Point(9, 170);
            this.lStepStartTime.Name = "lStepStartTime";
            this.lStepStartTime.Size = new System.Drawing.Size(60, 15);
            this.lStepStartTime.TabIndex = 4;
            this.lStepStartTime.Text = "Start Time";
            // 
            // eStepStartTime
            // 
            this.eStepStartTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eStepStartTime.Location = new System.Drawing.Point(100, 166);
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
            this.lStepEndTime.Location = new System.Drawing.Point(9, 194);
            this.lStepEndTime.Name = "lStepEndTime";
            this.lStepEndTime.Size = new System.Drawing.Size(56, 15);
            this.lStepEndTime.TabIndex = 5;
            this.lStepEndTime.Text = "End Time";
            // 
            // lPlotEndTime
            // 
            this.lPlotEndTime.AutoSize = true;
            this.lPlotEndTime.Location = new System.Drawing.Point(9, 220);
            this.lPlotEndTime.Name = "lPlotEndTime";
            this.lPlotEndTime.Size = new System.Drawing.Size(80, 15);
            this.lPlotEndTime.TabIndex = 6;
            this.lPlotEndTime.Text = "Plot End Time";
            // 
            // grRheobase
            // 
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
            this.grRheobase.Size = new System.Drawing.Size(202, 114);
            this.grRheobase.TabIndex = 16;
            this.grRheobase.TabStop = false;
            this.grRheobase.Text = "Rheobase";
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
            this.btnSensitivityAnalysis.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSensitivityAnalysis.Location = new System.Drawing.Point(131, 75);
            this.btnSensitivityAnalysis.Name = "btnSensitivityAnalysis";
            this.btnSensitivityAnalysis.Size = new System.Drawing.Size(63, 23);
            this.btnSensitivityAnalysis.TabIndex = 20;
            this.btnSensitivityAnalysis.Text = "Run";
            this.btnSensitivityAnalysis.UseVisualStyleBackColor = true;
            this.btnSensitivityAnalysis.Click += new System.EventHandler(this.btnSensitivityAnalysis_Click);
            // 
            // grTest
            // 
            this.grTest.Controls.Add(this.rbManualEntryStimulus);
            this.grTest.Controls.Add(this.stimulusControl1);
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
            this.grTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grTest.Location = new System.Drawing.Point(3, 117);
            this.grTest.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.grTest.Name = "grTest";
            this.grTest.Size = new System.Drawing.Size(202, 36);
            this.grTest.TabIndex = 17;
            this.grTest.TabStop = false;
            this.grTest.Text = "Test";
            // 
            // rbManualEntryStimulus
            // 
            this.rbManualEntryStimulus.AutoSize = true;
            this.rbManualEntryStimulus.Checked = true;
            this.rbManualEntryStimulus.Location = new System.Drawing.Point(9, 22);
            this.rbManualEntryStimulus.Name = "rbManualEntryStimulus";
            this.rbManualEntryStimulus.Size = new System.Drawing.Size(117, 19);
            this.rbManualEntryStimulus.TabIndex = 12;
            this.rbManualEntryStimulus.TabStop = true;
            this.rbManualEntryStimulus.Text = "Use Manual Entry";
            this.rbManualEntryStimulus.UseVisualStyleBackColor = true;
            this.rbManualEntryStimulus.CheckedChanged += new System.EventHandler(this.rbManualEntryStimulus_CheckedChanged);
            // 
            // stimulusControl1
            // 
            this.stimulusControl1.Location = new System.Drawing.Point(4, 42);
            this.stimulusControl1.Name = "stimulusControl1";
            this.stimulusControl1.Size = new System.Drawing.Size(212, 93);
            this.stimulusControl1.TabIndex = 20;
            // 
            // rbRheobaseBasedStimulus
            // 
            this.rbRheobaseBasedStimulus.AutoSize = true;
            this.rbRheobaseBasedStimulus.Location = new System.Drawing.Point(9, 141);
            this.rbRheobaseBasedStimulus.Name = "rbRheobaseBasedStimulus";
            this.rbRheobaseBasedStimulus.Size = new System.Drawing.Size(175, 19);
            this.rbRheobaseBasedStimulus.TabIndex = 13;
            this.rbRheobaseBasedStimulus.Text = "Use Rheobase (x1, x1.1, x1.5)";
            this.rbRheobaseBasedStimulus.UseVisualStyleBackColor = true;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.splitLeft);
            this.splitMain.Panel1.Controls.Add(this.pLoadSaveParams);
            this.splitMain.Panel1.Controls.Add(this.pBottomBottom);
            this.splitMain.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.webViewPlots);
            this.splitMain.Panel2.Controls.Add(this.pPlots);
            this.splitMain.Size = new System.Drawing.Size(974, 683);
            this.splitMain.SplitterDistance = 222;
            this.splitMain.TabIndex = 17;
            // 
            // splitLeft
            // 
            this.splitLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLeft.Location = new System.Drawing.Point(3, 34);
            this.splitLeft.Name = "splitLeft";
            this.splitLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitLeft.Panel1
            // 
            this.splitLeft.Panel1.Controls.Add(this.pParams);
            // 
            // splitLeft.Panel2
            // 
            this.splitLeft.Panel2.Controls.Add(this.tabLeft);
            this.splitLeft.Size = new System.Drawing.Size(216, 623);
            this.splitLeft.SplitterDistance = 158;
            this.splitLeft.TabIndex = 25;
            // 
            // pParams
            // 
            this.pParams.AutoScroll = true;
            this.pParams.AutoSize = true;
            this.pParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pParams.Location = new System.Drawing.Point(0, 0);
            this.pParams.Name = "pParams";
            this.pParams.Size = new System.Drawing.Size(216, 158);
            this.pParams.TabIndex = 23;
            // 
            // tabLeft
            // 
            this.tabLeft.Controls.Add(this.tTest);
            this.tabLeft.Controls.Add(this.tOptimize);
            this.tabLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLeft.Location = new System.Drawing.Point(0, 0);
            this.tabLeft.Name = "tabLeft";
            this.tabLeft.SelectedIndex = 0;
            this.tabLeft.Size = new System.Drawing.Size(216, 461);
            this.tabLeft.TabIndex = 3;
            // 
            // tTest
            // 
            this.tTest.Controls.Add(this.grTest);
            this.tTest.Controls.Add(this.grRheobase);
            this.tTest.Location = new System.Drawing.Point(4, 24);
            this.tTest.Name = "tTest";
            this.tTest.Padding = new System.Windows.Forms.Padding(3);
            this.tTest.Size = new System.Drawing.Size(208, 156);
            this.tTest.TabIndex = 0;
            this.tTest.Text = "Test";
            this.tTest.UseVisualStyleBackColor = true;
            // 
            // tOptimize
            // 
            this.tOptimize.Controls.Add(this.pOptimization);
            this.tOptimize.Controls.Add(this.grSensitivity);
            this.tOptimize.Location = new System.Drawing.Point(4, 24);
            this.tOptimize.Name = "tOptimize";
            this.tOptimize.Padding = new System.Windows.Forms.Padding(3);
            this.tOptimize.Size = new System.Drawing.Size(208, 433);
            this.tOptimize.TabIndex = 1;
            this.tOptimize.Text = "Optimize";
            this.tOptimize.UseVisualStyleBackColor = true;
            // 
            // pOptimization
            // 
            this.pOptimization.Controls.Add(this.dgMinMaxValues);
            this.pOptimization.Controls.Add(this.pMinMax);
            this.pOptimization.Controls.Add(this.pOptimize);
            this.pOptimization.Controls.Add(this.eOptimizationOutput);
            this.pOptimization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pOptimization.Location = new System.Drawing.Point(3, 112);
            this.pOptimization.Name = "pOptimization";
            this.pOptimization.Size = new System.Drawing.Size(202, 318);
            this.pOptimization.TabIndex = 28;
            // 
            // dgMinMaxValues
            // 
            this.dgMinMaxValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMinMaxValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParameter,
            this.colMinValue,
            this.colMaxValue});
            this.dgMinMaxValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMinMaxValues.Location = new System.Drawing.Point(0, 30);
            this.dgMinMaxValues.Name = "dgMinMaxValues";
            this.dgMinMaxValues.RowHeadersVisible = false;
            this.dgMinMaxValues.RowTemplate.Height = 25;
            this.dgMinMaxValues.Size = new System.Drawing.Size(202, 120);
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
            this.pMinMax.Size = new System.Drawing.Size(202, 30);
            this.pMinMax.TabIndex = 28;
            // 
            // pOptimize
            // 
            this.pOptimize.Controls.Add(this.eTargetRheobase);
            this.pOptimize.Controls.Add(this.linkOptimize);
            this.pOptimize.Controls.Add(this.lTargetRheobase);
            this.pOptimize.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pOptimize.Location = new System.Drawing.Point(0, 150);
            this.pOptimize.Name = "pOptimize";
            this.pOptimize.Size = new System.Drawing.Size(202, 39);
            this.pOptimize.TabIndex = 26;
            // 
            // eTargetRheobase
            // 
            this.eTargetRheobase.Location = new System.Drawing.Point(100, 12);
            this.eTargetRheobase.Name = "eTargetRheobase";
            this.eTargetRheobase.Size = new System.Drawing.Size(35, 23);
            this.eTargetRheobase.TabIndex = 2;
            // 
            // lTargetRheobase
            // 
            this.lTargetRheobase.AutoSize = true;
            this.lTargetRheobase.Location = new System.Drawing.Point(3, 15);
            this.lTargetRheobase.Name = "lTargetRheobase";
            this.lTargetRheobase.Size = new System.Drawing.Size(93, 15);
            this.lTargetRheobase.TabIndex = 1;
            this.lTargetRheobase.Text = "Target Rheobase";
            // 
            // linkOptimize
            // 
            this.linkOptimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkOptimize.AutoSize = true;
            this.linkOptimize.Location = new System.Drawing.Point(141, 15);
            this.linkOptimize.Name = "linkOptimize";
            this.linkOptimize.Size = new System.Drawing.Size(55, 15);
            this.linkOptimize.TabIndex = 0;
            this.linkOptimize.TabStop = true;
            this.linkOptimize.Text = "Optimize";
            this.linkOptimize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOptimize_LinkClicked);
            // 
            // eOptimizationOutput
            // 
            this.eOptimizationOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.eOptimizationOutput.Location = new System.Drawing.Point(0, 189);
            this.eOptimizationOutput.Name = "eOptimizationOutput";
            this.eOptimizationOutput.Size = new System.Drawing.Size(202, 129);
            this.eOptimizationOutput.TabIndex = 25;
            this.eOptimizationOutput.Text = "";
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
            this.grSensitivity.Dock = System.Windows.Forms.DockStyle.Top;
            this.grSensitivity.Location = new System.Drawing.Point(3, 3);
            this.grSensitivity.Name = "grSensitivity";
            this.grSensitivity.Size = new System.Drawing.Size(202, 109);
            this.grSensitivity.TabIndex = 18;
            this.grSensitivity.TabStop = false;
            this.grSensitivity.Text = "Sensitivity Analysis";
            // 
            // cbLogScale
            // 
            this.cbLogScale.AutoSize = true;
            this.cbLogScale.Location = new System.Drawing.Point(6, 75);
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
            // pLoadSaveParams
            // 
            this.pLoadSaveParams.Controls.Add(this.linkSaveParams);
            this.pLoadSaveParams.Controls.Add(this.linkLoadParams);
            this.pLoadSaveParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.pLoadSaveParams.Location = new System.Drawing.Point(3, 3);
            this.pLoadSaveParams.Name = "pLoadSaveParams";
            this.pLoadSaveParams.Size = new System.Drawing.Size(216, 31);
            this.pLoadSaveParams.TabIndex = 21;
            // 
            // linkSaveParams
            // 
            this.linkSaveParams.AutoSize = true;
            this.linkSaveParams.Location = new System.Drawing.Point(87, 9);
            this.linkSaveParams.Name = "linkSaveParams";
            this.linkSaveParams.Size = new System.Drawing.Size(73, 15);
            this.linkSaveParams.TabIndex = 21;
            this.linkSaveParams.TabStop = true;
            this.linkSaveParams.Text = "Save Params";
            this.linkSaveParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveCell_LinkClicked);
            // 
            // linkLoadParams
            // 
            this.linkLoadParams.AutoSize = true;
            this.linkLoadParams.Location = new System.Drawing.Point(6, 9);
            this.linkLoadParams.Name = "linkLoadParams";
            this.linkLoadParams.Size = new System.Drawing.Size(75, 15);
            this.linkLoadParams.TabIndex = 20;
            this.linkLoadParams.TabStop = true;
            this.linkLoadParams.Text = "Load Params";
            this.linkLoadParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLoadCell_LinkClicked);
            // 
            // pBottomBottom
            // 
            this.pBottomBottom.Controls.Add(this.linkUseUpdatedParams);
            this.pBottomBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottomBottom.Location = new System.Drawing.Point(3, 657);
            this.pBottomBottom.Name = "pBottomBottom";
            this.pBottomBottom.Size = new System.Drawing.Size(216, 23);
            this.pBottomBottom.TabIndex = 24;
            this.pBottomBottom.Visible = false;
            // 
            // linkUseUpdatedParams
            // 
            this.linkUseUpdatedParams.AutoSize = true;
            this.linkUseUpdatedParams.Location = new System.Drawing.Point(6, 5);
            this.linkUseUpdatedParams.Name = "linkUseUpdatedParams";
            this.linkUseUpdatedParams.Size = new System.Drawing.Size(136, 15);
            this.linkUseUpdatedParams.TabIndex = 19;
            this.linkUseUpdatedParams.TabStop = true;
            this.linkUseUpdatedParams.Text = "Use Updated Parameters";
            this.linkUseUpdatedParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUseUpdatedParams_LinkClicked);
            // 
            // webViewPlots
            // 
            this.webViewPlots.AllowExternalDrop = true;
            this.webViewPlots.CreationProperties = null;
            this.webViewPlots.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewPlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewPlots.Location = new System.Drawing.Point(0, 54);
            this.webViewPlots.Name = "webViewPlots";
            this.webViewPlots.Size = new System.Drawing.Size(748, 629);
            this.webViewPlots.TabIndex = 0;
            this.webViewPlots.ZoomFactor = 1D;
            this.webViewPlots.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webViewPlots_CoreWebView2InitializationCompleted);
            // 
            // pPlots
            // 
            this.pPlots.Controls.Add(this.cbStimulus);
            this.pPlots.Controls.Add(this.cbSpikingFrequency);
            this.pPlots.Controls.Add(this.cbInterval);
            this.pPlots.Controls.Add(this.cbTauDecay);
            this.pPlots.Controls.Add(this.cbTauRise);
            this.pPlots.Controls.Add(this.cbURelTension);
            this.pPlots.Controls.Add(this.cbV);
            this.pPlots.Dock = System.Windows.Forms.DockStyle.Top;
            this.pPlots.Location = new System.Drawing.Point(0, 0);
            this.pPlots.Name = "pPlots";
            this.pPlots.Size = new System.Drawing.Size(748, 54);
            this.pPlots.TabIndex = 1;
            // 
            // cbStimulus
            // 
            this.cbStimulus.AutoSize = true;
            this.cbStimulus.Checked = true;
            this.cbStimulus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStimulus.Location = new System.Drawing.Point(436, 12);
            this.cbStimulus.Name = "cbStimulus";
            this.cbStimulus.Size = new System.Drawing.Size(72, 19);
            this.cbStimulus.TabIndex = 6;
            this.cbStimulus.Text = "Stimulus";
            this.cbStimulus.UseVisualStyleBackColor = true;
            this.cbStimulus.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbSpikingFrequency
            // 
            this.cbSpikingFrequency.AutoSize = true;
            this.cbSpikingFrequency.Location = new System.Drawing.Point(297, 29);
            this.cbSpikingFrequency.Name = "cbSpikingFrequency";
            this.cbSpikingFrequency.Size = new System.Drawing.Size(123, 19);
            this.cbSpikingFrequency.TabIndex = 5;
            this.cbSpikingFrequency.Text = "Spiking Frequency";
            this.cbSpikingFrequency.UseVisualStyleBackColor = true;
            this.cbSpikingFrequency.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbInterval
            // 
            this.cbInterval.AutoSize = true;
            this.cbInterval.Location = new System.Drawing.Point(297, 9);
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
            this.cbTauDecay.Location = new System.Drawing.Point(184, 32);
            this.cbTauDecay.Name = "cbTauDecay";
            this.cbTauDecay.Size = new System.Drawing.Size(79, 19);
            this.cbTauDecay.TabIndex = 3;
            this.cbTauDecay.Text = "Tau Decay";
            this.cbTauDecay.UseVisualStyleBackColor = true;
            this.cbTauDecay.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbTauRise
            // 
            this.cbTauRise.AutoSize = true;
            this.cbTauRise.Location = new System.Drawing.Point(184, 12);
            this.cbTauRise.Name = "cbTauRise";
            this.cbTauRise.Size = new System.Drawing.Size(68, 19);
            this.cbTauRise.TabIndex = 2;
            this.cbTauRise.Text = "Tau Rise";
            this.cbTauRise.UseVisualStyleBackColor = true;
            this.cbTauRise.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbURelTension
            // 
            this.cbURelTension.AutoSize = true;
            this.cbURelTension.Location = new System.Drawing.Point(13, 32);
            this.cbURelTension.Name = "cbURelTension";
            this.cbURelTension.Size = new System.Drawing.Size(128, 19);
            this.cbURelTension.TabIndex = 1;
            this.cbURelTension.Text = "u / Relative Tension";
            this.cbURelTension.UseVisualStyleBackColor = true;
            this.cbURelTension.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // cbV
            // 
            this.cbV.AutoSize = true;
            this.cbV.Checked = true;
            this.cbV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbV.Location = new System.Drawing.Point(13, 12);
            this.cbV.Name = "cbV";
            this.cbV.Size = new System.Drawing.Size(152, 19);
            this.cbV.TabIndex = 0;
            this.cbV.Text = "Membrane Potential (V)";
            this.cbV.UseVisualStyleBackColor = true;
            this.cbV.CheckedChanged += new System.EventHandler(this.cbPlotSelection_CheckedChanged);
            // 
            // saveFileJson
            // 
            this.saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // openFileJson
            // 
            this.openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // linkSuggestMinMax
            // 
            this.linkSuggestMinMax.AutoSize = true;
            this.linkSuggestMinMax.Location = new System.Drawing.Point(6, 9);
            this.linkSuggestMinMax.Name = "linkSuggestMinMax";
            this.linkSuggestMinMax.Size = new System.Drawing.Size(137, 15);
            this.linkSuggestMinMax.TabIndex = 0;
            this.linkSuggestMinMax.TabStop = true;
            this.linkSuggestMinMax.Text = "Suggest Min/Max Values";
            this.toolTip1.SetToolTip(this.linkSuggestMinMax, "If not entered, current values will be used as min/max.");
            this.linkSuggestMinMax.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSuggestMinMax_LinkClicked);
            // 
            // DynamicsTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Name = "DynamicsTestControl";
            this.Size = new System.Drawing.Size(974, 683);
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
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitLeft.Panel1.ResumeLayout(false);
            this.splitLeft.Panel1.PerformLayout();
            this.splitLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLeft)).EndInit();
            this.splitLeft.ResumeLayout(false);
            this.tabLeft.ResumeLayout(false);
            this.tTest.ResumeLayout(false);
            this.tOptimize.ResumeLayout(false);
            this.pOptimization.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMinMaxValues)).EndInit();
            this.pMinMax.ResumeLayout(false);
            this.pMinMax.PerformLayout();
            this.pOptimize.ResumeLayout(false);
            this.pOptimize.PerformLayout();
            this.grSensitivity.ResumeLayout(false);
            this.grSensitivity.PerformLayout();
            this.pLoadSaveParams.ResumeLayout(false);
            this.pLoadSaveParams.PerformLayout();
            this.pBottomBottom.ResumeLayout(false);
            this.pBottomBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlots)).EndInit();
            this.pPlots.ResumeLayout(false);
            this.pPlots.PerformLayout();
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
        private StimulusControl stimulusControl1;
        private RadioButton rbRheobaseBasedStimulus;
        private FlowLayoutPanel pParams;
        private Panel pLoadSaveParams;
        private LinkLabel linkSaveParams;
        private LinkLabel linkLoadParams;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private Panel pPlots;
        private CheckBox cbStimulus;
        private CheckBox cbSpikingFrequency;
        private CheckBox cbInterval;
        private CheckBox cbTauDecay;
        private CheckBox cbTauRise;
        private CheckBox cbURelTension;
        private CheckBox cbV;
        private Panel pBottomBottom;
        private LinkLabel linkUseUpdatedParams;
        private TabControl tabLeft;
        private TabPage tTest;
        private TabPage tOptimize;
        private Panel pOptimize;
        private LinkLabel linkOptimize;
        private RichTextBox eOptimizationOutput;
        private DataGridView dgMinMaxValues;
        private Panel pOptimization;
        private TextBox eTargetRheobase;
        private Label lTargetRheobase;
        private DataGridViewTextBoxColumn colParameter;
        private DataGridViewTextBoxColumn colMinValue;
        private DataGridViewTextBoxColumn colMaxValue;
        private Panel pMinMax;
        private SplitContainer splitLeft;
        private LinkLabel linkSuggestMinMax;
        private ToolTip toolTip1;
    }
}
