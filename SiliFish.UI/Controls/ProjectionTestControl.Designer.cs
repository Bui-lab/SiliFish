namespace SiliFish.UI.Controls
{
    partial class ProjectionTestControl
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
            this.lPlotdt = new System.Windows.Forms.Label();
            this.edt = new System.Windows.Forms.NumericUpDown();
            this.ePlotEndTime = new System.Windows.Forms.NumericUpDown();
            this.eStepEndTime = new System.Windows.Forms.NumericUpDown();
            this.lStepStartTime = new System.Windows.Forms.Label();
            this.eStepStartTime = new System.Windows.Forms.NumericUpDown();
            this.lStepEndTime = new System.Windows.Forms.Label();
            this.lPlotEndTime = new System.Windows.Forms.Label();
            this.edtEuler = new System.Windows.Forms.NumericUpDown();
            this.stimulusControl1 = new SiliFish.UI.Controls.StimulusControl();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pfParamsSource = new System.Windows.Forms.FlowLayoutPanel();
            this.pCoreType = new System.Windows.Forms.Panel();
            this.pLineCoreType = new System.Windows.Forms.Panel();
            this.lSource = new System.Windows.Forms.Label();
            this.pfParamsTarget = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lTarget = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pRheobase = new System.Windows.Forms.Panel();
            this.btnDynamicsRun = new System.Windows.Forms.Button();
            this.cbAutoDrawPlots = new System.Windows.Forms.CheckBox();
            this.lEulerdt = new System.Windows.Forms.Label();
            this.lms2 = new System.Windows.Forms.Label();
            this.lms = new System.Windows.Forms.Label();
            this.pLoadSaveParams = new System.Windows.Forms.Panel();
            this.linkLoadCoreUnit = new System.Windows.Forms.LinkLabel();
            this.linkSaveCoreUnit = new System.Windows.Forms.LinkLabel();
            this.pBottomBottom = new System.Windows.Forms.Panel();
            this.linkUseUpdatedParams = new System.Windows.Forms.LinkLabel();
            this.splitGAAndPlots = new System.Windows.Forms.SplitContainer();
            this.webViewPlots = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pPlots = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grPlotSelection = new System.Windows.Forms.GroupBox();
            this.cbV = new System.Windows.Forms.CheckBox();
            this.cbStimulus = new System.Windows.Forms.CheckBox();
            this.cbSecondaryLists = new System.Windows.Forms.CheckBox();
            this.saveFileJson = new System.Windows.Forms.SaveFileDialog();
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.eSource = new System.Windows.Forms.TextBox();
            this.eTarget = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEuler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pCoreType.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pRheobase.SuspendLayout();
            this.pLoadSaveParams.SuspendLayout();
            this.pBottomBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGAAndPlots)).BeginInit();
            this.splitGAAndPlots.Panel2.SuspendLayout();
            this.splitGAAndPlots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlots)).BeginInit();
            this.pPlots.SuspendLayout();
            this.grPlotSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // lPlotdt
            // 
            this.lPlotdt.AutoSize = true;
            this.lPlotdt.Location = new System.Drawing.Point(10, 73);
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
            this.edt.Location = new System.Drawing.Point(73, 69);
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
            this.ePlotEndTime.Location = new System.Drawing.Point(73, 42);
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
            this.eStepEndTime.Location = new System.Drawing.Point(176, 13);
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
            this.lStepStartTime.Location = new System.Drawing.Point(10, 17);
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
            this.eStepStartTime.Location = new System.Drawing.Point(73, 13);
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
            this.lStepEndTime.Location = new System.Drawing.Point(145, 17);
            this.lStepEndTime.Name = "lStepEndTime";
            this.lStepEndTime.Size = new System.Drawing.Size(27, 15);
            this.lStepEndTime.TabIndex = 5;
            this.lStepEndTime.Text = "End";
            // 
            // lPlotEndTime
            // 
            this.lPlotEndTime.AutoSize = true;
            this.lPlotEndTime.Location = new System.Drawing.Point(10, 44);
            this.lPlotEndTime.Name = "lPlotEndTime";
            this.lPlotEndTime.Size = new System.Drawing.Size(51, 15);
            this.lPlotEndTime.TabIndex = 6;
            this.lPlotEndTime.Text = "Plot End";
            // 
            // edtEuler
            // 
            this.edtEuler.DecimalPlaces = 2;
            this.edtEuler.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edtEuler.Location = new System.Drawing.Point(199, 69);
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
            this.stimulusControl1.Location = new System.Drawing.Point(10, 98);
            this.stimulusControl1.Name = "stimulusControl1";
            this.stimulusControl1.Size = new System.Drawing.Size(176, 93);
            this.stimulusControl1.TabIndex = 25;
            this.stimulusControl1.StimulusChanged += new System.EventHandler(this.stimulusControl1_StimulusChanged);
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
            this.splitMain.Panel1.Controls.Add(this.splitContainer1);
            this.splitMain.Panel1.Controls.Add(this.pRheobase);
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(299, 390);
            this.splitContainer1.SplitterDistance = 195;
            this.splitContainer1.TabIndex = 41;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pfParamsSource);
            this.splitContainer2.Panel1.Controls.Add(this.pCoreType);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pfParamsTarget);
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(299, 195);
            this.splitContainer2.SplitterDistance = 140;
            this.splitContainer2.TabIndex = 0;
            // 
            // pfParamsSource
            // 
            this.pfParamsSource.AutoScroll = true;
            this.pfParamsSource.AutoSize = true;
            this.pfParamsSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfParamsSource.Location = new System.Drawing.Point(0, 29);
            this.pfParamsSource.Name = "pfParamsSource";
            this.pfParamsSource.Size = new System.Drawing.Size(140, 166);
            this.pfParamsSource.TabIndex = 23;
            // 
            // pCoreType
            // 
            this.pCoreType.Controls.Add(this.eSource);
            this.pCoreType.Controls.Add(this.pLineCoreType);
            this.pCoreType.Controls.Add(this.lSource);
            this.pCoreType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCoreType.Location = new System.Drawing.Point(0, 0);
            this.pCoreType.Name = "pCoreType";
            this.pCoreType.Size = new System.Drawing.Size(140, 29);
            this.pCoreType.TabIndex = 21;
            // 
            // pLineCoreType
            // 
            this.pLineCoreType.BackColor = System.Drawing.Color.LightGray;
            this.pLineCoreType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineCoreType.Location = new System.Drawing.Point(0, 28);
            this.pLineCoreType.Name = "pLineCoreType";
            this.pLineCoreType.Size = new System.Drawing.Size(140, 1);
            this.pLineCoreType.TabIndex = 24;
            // 
            // lSource
            // 
            this.lSource.AutoSize = true;
            this.lSource.Location = new System.Drawing.Point(6, 9);
            this.lSource.Name = "lSource";
            this.lSource.Size = new System.Drawing.Size(43, 15);
            this.lSource.TabIndex = 22;
            this.lSource.Text = "Source";
            // 
            // pfParamsTarget
            // 
            this.pfParamsTarget.AutoScroll = true;
            this.pfParamsTarget.AutoSize = true;
            this.pfParamsTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfParamsTarget.Location = new System.Drawing.Point(0, 29);
            this.pfParamsTarget.Name = "pfParamsTarget";
            this.pfParamsTarget.Size = new System.Drawing.Size(155, 166);
            this.pfParamsTarget.TabIndex = 25;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.eTarget);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.lTarget);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(155, 29);
            this.panel2.TabIndex = 24;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightGray;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(155, 1);
            this.panel3.TabIndex = 24;
            // 
            // lTarget
            // 
            this.lTarget.AutoSize = true;
            this.lTarget.Location = new System.Drawing.Point(6, 9);
            this.lTarget.Name = "lTarget";
            this.lTarget.Size = new System.Drawing.Size(39, 15);
            this.lTarget.TabIndex = 22;
            this.lTarget.Text = "Target";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 29);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(299, 162);
            this.flowLayoutPanel2.TabIndex = 25;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.comboBox2);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(299, 29);
            this.panel4.TabIndex = 24;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightGray;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 28);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(299, 1);
            this.panel5.TabIndex = 24;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(71, 6);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(219, 23);
            this.comboBox2.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "Core Type";
            // 
            // pRheobase
            // 
            this.pRheobase.Controls.Add(this.btnDynamicsRun);
            this.pRheobase.Controls.Add(this.lStepStartTime);
            this.pRheobase.Controls.Add(this.cbAutoDrawPlots);
            this.pRheobase.Controls.Add(this.edt);
            this.pRheobase.Controls.Add(this.lPlotdt);
            this.pRheobase.Controls.Add(this.lPlotEndTime);
            this.pRheobase.Controls.Add(this.lEulerdt);
            this.pRheobase.Controls.Add(this.lStepEndTime);
            this.pRheobase.Controls.Add(this.eStepEndTime);
            this.pRheobase.Controls.Add(this.stimulusControl1);
            this.pRheobase.Controls.Add(this.lms2);
            this.pRheobase.Controls.Add(this.eStepStartTime);
            this.pRheobase.Controls.Add(this.edtEuler);
            this.pRheobase.Controls.Add(this.lms);
            this.pRheobase.Controls.Add(this.ePlotEndTime);
            this.pRheobase.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pRheobase.Location = new System.Drawing.Point(3, 393);
            this.pRheobase.Name = "pRheobase";
            this.pRheobase.Size = new System.Drawing.Size(299, 231);
            this.pRheobase.TabIndex = 40;
            // 
            // btnDynamicsRun
            // 
            this.btnDynamicsRun.Location = new System.Drawing.Point(111, 189);
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
            this.cbAutoDrawPlots.Location = new System.Drawing.Point(13, 193);
            this.cbAutoDrawPlots.Name = "cbAutoDrawPlots";
            this.cbAutoDrawPlots.Size = new System.Drawing.Size(82, 19);
            this.cbAutoDrawPlots.TabIndex = 34;
            this.cbAutoDrawPlots.Text = "Auto Draw";
            this.cbAutoDrawPlots.UseVisualStyleBackColor = true;
            this.cbAutoDrawPlots.CheckedChanged += new System.EventHandler(this.cbAutoDrawPlots_CheckedChanged);
            // 
            // lEulerdt
            // 
            this.lEulerdt.AutoSize = true;
            this.lEulerdt.Location = new System.Drawing.Point(145, 73);
            this.lEulerdt.Name = "lEulerdt";
            this.lEulerdt.Size = new System.Drawing.Size(48, 15);
            this.lEulerdt.TabIndex = 30;
            this.lEulerdt.Text = "Euler Δt";
            // 
            // lms2
            // 
            this.lms2.AutoSize = true;
            this.lms2.Location = new System.Drawing.Point(146, 44);
            this.lms2.Name = "lms2";
            this.lms2.Size = new System.Drawing.Size(31, 15);
            this.lms2.TabIndex = 28;
            this.lms2.Text = "(ms)";
            // 
            // lms
            // 
            this.lms.AutoSize = true;
            this.lms.Location = new System.Drawing.Point(252, 17);
            this.lms.Name = "lms";
            this.lms.Size = new System.Drawing.Size(31, 15);
            this.lms.TabIndex = 27;
            this.lms.Text = "(ms)";
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
            this.splitGAAndPlots.Panel1Collapsed = true;
            // 
            // splitGAAndPlots.Panel2
            // 
            this.splitGAAndPlots.Panel2.Controls.Add(this.webViewPlots);
            this.splitGAAndPlots.Size = new System.Drawing.Size(941, 618);
            this.splitGAAndPlots.SplitterDistance = 309;
            this.splitGAAndPlots.TabIndex = 2;
            // 
            // webViewPlots
            // 
            this.webViewPlots.AllowExternalDrop = true;
            this.webViewPlots.CreationProperties = null;
            this.webViewPlots.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewPlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewPlots.Location = new System.Drawing.Point(0, 0);
            this.webViewPlots.Name = "webViewPlots";
            this.webViewPlots.Size = new System.Drawing.Size(939, 616);
            this.webViewPlots.TabIndex = 0;
            this.webViewPlots.ZoomFactor = 1D;
            this.webViewPlots.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webViewPlots_CoreWebView2InitializationCompleted);
            // 
            // pPlots
            // 
            this.pPlots.Controls.Add(this.panel1);
            this.pPlots.Controls.Add(this.grPlotSelection);
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
            this.cbStimulus.Location = new System.Drawing.Point(164, 14);
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
            // saveFileJson
            // 
            this.saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // openFileJson
            // 
            this.openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // eSource
            // 
            this.eSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eSource.Location = new System.Drawing.Point(55, 3);
            this.eSource.Name = "eSource";
            this.eSource.ReadOnly = true;
            this.eSource.Size = new System.Drawing.Size(82, 23);
            this.eSource.TabIndex = 25;
            // 
            // eTarget
            // 
            this.eTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eTarget.Location = new System.Drawing.Point(51, 3);
            this.eTarget.Name = "eTarget";
            this.eTarget.ReadOnly = true;
            this.eTarget.Size = new System.Drawing.Size(101, 23);
            this.eTarget.TabIndex = 26;
            // 
            // ProjectionTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitMain);
            this.Name = "ProjectionTestControl";
            this.Size = new System.Drawing.Size(1252, 683);
            this.Load += new System.EventHandler(this.DynamicsTestControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.edt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEuler)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pCoreType.ResumeLayout(false);
            this.pCoreType.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pRheobase.ResumeLayout(false);
            this.pRheobase.PerformLayout();
            this.pLoadSaveParams.ResumeLayout(false);
            this.pLoadSaveParams.PerformLayout();
            this.pBottomBottom.ResumeLayout(false);
            this.pBottomBottom.PerformLayout();
            this.splitGAAndPlots.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGAAndPlots)).EndInit();
            this.splitGAAndPlots.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlots)).EndInit();
            this.pPlots.ResumeLayout(false);
            this.grPlotSelection.ResumeLayout(false);
            this.grPlotSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Label lPlotdt;
        private NumericUpDown edt;
        private NumericUpDown ePlotEndTime;
        private NumericUpDown eStepEndTime;
        private Label lStepStartTime;
        private NumericUpDown eStepStartTime;
        private Label lStepEndTime;
        private Label lPlotEndTime;
        private SplitContainer splitMain;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewPlots;
        private FlowLayoutPanel pfParamsSource;
        private Panel pCoreType;
        private LinkLabel linkSaveCoreUnit;
        private LinkLabel linkLoadCoreUnit;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private Panel pPlots;
        private CheckBox cbStimulus;
        private CheckBox cbSecondaryLists;
        private CheckBox cbV;
        private Panel pBottomBottom;
        private LinkLabel linkUseUpdatedParams;
        private ToolTip toolTip1;
        private GroupBox grPlotSelection;
        private SplitContainer splitGAAndPlots;
        private Label lSource;
        private Panel pLoadSaveParams;
        private StimulusControl stimulusControl1;
        private Panel pLineCoreType;
        private Panel panel1;
        private NumericUpDown edtEuler;
        private Label lms2;
        private Label lms;
        private Panel pRheobase;
        private Label lEulerdt;
        private CheckBox cbAutoDrawPlots;
        private Button btnDynamicsRun;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private FlowLayoutPanel pfParamsTarget;
        private Panel panel2;
        private Panel panel3;
        private Label lTarget;
        private FlowLayoutPanel flowLayoutPanel2;
        private Panel panel4;
        private Panel panel5;
        private ComboBox comboBox2;
        private Label label2;
        private TextBox eSource;
        private TextBox eTarget;
    }
}
