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
            components = new System.ComponentModel.Container();
            eRheobaseLimit = new NumericUpDown();
            lRheobaseLimit = new Label();
            btnRheobase = new Button();
            lPlotdt = new Label();
            edt = new NumericUpDown();
            ePlotEndTime = new NumericUpDown();
            eStepEndTime = new NumericUpDown();
            lStepStartTime = new Label();
            eStepStartTime = new NumericUpDown();
            lStepEndTime = new Label();
            lPlotEndTime = new Label();
            grRheobase = new GroupBox();
            lRheobaseWarning = new Label();
            eRheobaseDuration = new NumericUpDown();
            label1 = new Label();
            eRheobase = new TextBox();
            lRheobase = new Label();
            edtEuler = new NumericUpDown();
            stimulusControl1 = new StimulusSettingsControl();
            rbSingleEntryStimulus = new RadioButton();
            rbRheobaseBasedStimulus = new RadioButton();
            splitMain = new SplitContainer();
            splitLeft = new SplitContainer();
            pfParams = new FlowLayoutPanel();
            pCoreType = new Panel();
            pLineCoreType = new Panel();
            ddCoreType = new ComboBox();
            lCoreType = new Label();
            pRheobase = new Panel();
            pTop = new Panel();
            linkUseUpdatedParams = new LinkLabel();
            tabAnalysis = new TabControl();
            tTest = new TabPage();
            pFlow = new FlowLayoutPanel();
            pFlow1 = new Panel();
            pSep1 = new Panel();
            lms = new Label();
            lms2 = new Label();
            lEulerdt = new Label();
            pFlow2 = new Panel();
            eMultipleStimulus = new TextBox();
            rbMultipleEntry = new RadioButton();
            lMultipleEntryNote = new Label();
            pFlow5 = new Panel();
            btnDynamicsRun = new Button();
            cbAutoDrawPlots = new CheckBox();
            tSensitivityAnalysis = new TabPage();
            grFiring = new GroupBox();
            sensitivityAnalysisFiring = new SensitivityAnalysisControl();
            grRheoSens = new GroupBox();
            sensitivityAnalysisRheobase = new SensitivityAnalysisControl();
            pLoadSaveParams = new Panel();
            linkLoadCoreUnit = new LinkLabel();
            linkSaveCoreUnit = new LinkLabel();
            splitGAAndPlots = new SplitContainer();
            pOptimize = new Panel();
            gaControl = new GAControl();
            webViewPlots = new Microsoft.Web.WebView2.WinForms.WebView2();
            pPlots = new Panel();
            panel1 = new Panel();
            grPlotSelection = new GroupBox();
            cbV = new CheckBox();
            cbStimulus = new CheckBox();
            cbSecondaryLists = new CheckBox();
            cbSpikingFrequency = new CheckBox();
            cbTauRise = new CheckBox();
            cbInterval = new CheckBox();
            cbTauDecay = new CheckBox();
            linkSwitchToOptimization = new LinkLabel();
            saveFileJson = new SaveFileDialog();
            openFileJson = new OpenFileDialog();
            toolTip1 = new ToolTip(components);
            pDistinguisherLeft = new Panel();
            pDistinguisherTop = new Panel();
            pDistinguisherBottom = new Panel();
            pDistinguisherRight = new Panel();
            ((System.ComponentModel.ISupportInitialize)eRheobaseLimit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ePlotEndTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eStepEndTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eStepStartTime).BeginInit();
            grRheobase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)eRheobaseDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edtEuler).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitLeft).BeginInit();
            splitLeft.Panel1.SuspendLayout();
            splitLeft.Panel2.SuspendLayout();
            splitLeft.SuspendLayout();
            pCoreType.SuspendLayout();
            pRheobase.SuspendLayout();
            pTop.SuspendLayout();
            tabAnalysis.SuspendLayout();
            tTest.SuspendLayout();
            pFlow.SuspendLayout();
            pFlow1.SuspendLayout();
            pFlow2.SuspendLayout();
            pFlow5.SuspendLayout();
            tSensitivityAnalysis.SuspendLayout();
            grFiring.SuspendLayout();
            grRheoSens.SuspendLayout();
            pLoadSaveParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitGAAndPlots).BeginInit();
            splitGAAndPlots.Panel1.SuspendLayout();
            splitGAAndPlots.Panel2.SuspendLayout();
            splitGAAndPlots.SuspendLayout();
            pOptimize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewPlots).BeginInit();
            pPlots.SuspendLayout();
            grPlotSelection.SuspendLayout();
            SuspendLayout();
            // 
            // eRheobaseLimit
            // 
            eRheobaseLimit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            eRheobaseLimit.Location = new Point(70, 22);
            eRheobaseLimit.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            eRheobaseLimit.Name = "eRheobaseLimit";
            eRheobaseLimit.Size = new Size(54, 23);
            eRheobaseLimit.TabIndex = 15;
            eRheobaseLimit.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // lRheobaseLimit
            // 
            lRheobaseLimit.AutoSize = true;
            lRheobaseLimit.Location = new Point(3, 26);
            lRheobaseLimit.Name = "lRheobaseLimit";
            lRheobaseLimit.Size = new Size(34, 15);
            lRheobaseLimit.TabIndex = 14;
            lRheobaseLimit.Text = "Limit";
            // 
            // btnRheobase
            // 
            btnRheobase.BackColor = Color.FromArgb(96, 125, 139);
            btnRheobase.FlatAppearance.BorderColor = Color.LightGray;
            btnRheobase.FlatStyle = FlatStyle.Flat;
            btnRheobase.ForeColor = Color.White;
            btnRheobase.Location = new Point(128, 22);
            btnRheobase.Name = "btnRheobase";
            btnRheobase.Size = new Size(76, 25);
            btnRheobase.TabIndex = 13;
            btnRheobase.Text = "Calculate";
            btnRheobase.UseVisualStyleBackColor = false;
            btnRheobase.Click += btnRheobase_Click;
            // 
            // lPlotdt
            // 
            lPlotdt.AutoSize = true;
            lPlotdt.Location = new Point(4, 66);
            lPlotdt.Name = "lPlotdt";
            lPlotdt.Size = new Size(43, 15);
            lPlotdt.TabIndex = 10;
            lPlotdt.Text = "Plot Δt";
            // 
            // edt
            // 
            edt.DecimalPlaces = 2;
            edt.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            edt.Location = new Point(67, 62);
            edt.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            edt.Name = "edt";
            edt.Size = new Size(66, 23);
            edt.TabIndex = 11;
            edt.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // ePlotEndTime
            // 
            ePlotEndTime.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            ePlotEndTime.Location = new Point(67, 35);
            ePlotEndTime.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            ePlotEndTime.Name = "ePlotEndTime";
            ePlotEndTime.Size = new Size(66, 23);
            ePlotEndTime.TabIndex = 9;
            ePlotEndTime.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // eStepEndTime
            // 
            eStepEndTime.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            eStepEndTime.Location = new Point(170, 6);
            eStepEndTime.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            eStepEndTime.Name = "eStepEndTime";
            eStepEndTime.Size = new Size(66, 23);
            eStepEndTime.TabIndex = 8;
            eStepEndTime.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // lStepStartTime
            // 
            lStepStartTime.AutoSize = true;
            lStepStartTime.Location = new Point(4, 10);
            lStepStartTime.Name = "lStepStartTime";
            lStepStartTime.Size = new Size(61, 15);
            lStepStartTime.TabIndex = 4;
            lStepStartTime.Text = "Stim. Start";
            // 
            // eStepStartTime
            // 
            eStepStartTime.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            eStepStartTime.Location = new Point(67, 6);
            eStepStartTime.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            eStepStartTime.Name = "eStepStartTime";
            eStepStartTime.Size = new Size(66, 23);
            eStepStartTime.TabIndex = 7;
            eStepStartTime.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // lStepEndTime
            // 
            lStepEndTime.AutoSize = true;
            lStepEndTime.Location = new Point(139, 10);
            lStepEndTime.Name = "lStepEndTime";
            lStepEndTime.Size = new Size(27, 15);
            lStepEndTime.TabIndex = 5;
            lStepEndTime.Text = "End";
            // 
            // lPlotEndTime
            // 
            lPlotEndTime.AutoSize = true;
            lPlotEndTime.Location = new Point(4, 37);
            lPlotEndTime.Name = "lPlotEndTime";
            lPlotEndTime.Size = new Size(51, 15);
            lPlotEndTime.TabIndex = 6;
            lPlotEndTime.Text = "Plot End";
            // 
            // grRheobase
            // 
            grRheobase.Controls.Add(lRheobaseWarning);
            grRheobase.Controls.Add(eRheobaseDuration);
            grRheobase.Controls.Add(label1);
            grRheobase.Controls.Add(eRheobase);
            grRheobase.Controls.Add(lRheobase);
            grRheobase.Controls.Add(btnRheobase);
            grRheobase.Controls.Add(eRheobaseLimit);
            grRheobase.Controls.Add(lRheobaseLimit);
            grRheobase.Dock = DockStyle.Fill;
            grRheobase.Location = new Point(0, 0);
            grRheobase.Name = "grRheobase";
            grRheobase.Size = new Size(328, 108);
            grRheobase.TabIndex = 16;
            grRheobase.TabStop = false;
            grRheobase.Text = "Rheobase";
            // 
            // lRheobaseWarning
            // 
            lRheobaseWarning.AutoSize = true;
            lRheobaseWarning.ForeColor = Color.Red;
            lRheobaseWarning.Location = new Point(134, 77);
            lRheobaseWarning.Name = "lRheobaseWarning";
            lRheobaseWarning.Size = new Size(140, 15);
            lRheobaseWarning.TabIndex = 20;
            lRheobaseWarning.Text = "No rheobase within limit.";
            lRheobaseWarning.Visible = false;
            // 
            // eRheobaseDuration
            // 
            eRheobaseDuration.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            eRheobaseDuration.Location = new Point(70, 47);
            eRheobaseDuration.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            eRheobaseDuration.Name = "eRheobaseDuration";
            eRheobaseDuration.Size = new Size(54, 23);
            eRheobaseDuration.TabIndex = 19;
            eRheobaseDuration.Value = new decimal(new int[] { 400, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 49);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 18;
            label1.Text = "Duration";
            // 
            // eRheobase
            // 
            eRheobase.Location = new Point(70, 73);
            eRheobase.Name = "eRheobase";
            eRheobase.ReadOnly = true;
            eRheobase.Size = new Size(54, 23);
            eRheobase.TabIndex = 17;
            // 
            // lRheobase
            // 
            lRheobase.AutoSize = true;
            lRheobase.Location = new Point(3, 76);
            lRheobase.Name = "lRheobase";
            lRheobase.Size = new Size(58, 15);
            lRheobase.TabIndex = 16;
            lRheobase.Text = "Rheobase";
            // 
            // edtEuler
            // 
            edtEuler.DecimalPlaces = 2;
            edtEuler.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            edtEuler.Location = new Point(193, 62);
            edtEuler.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            edtEuler.Name = "edtEuler";
            edtEuler.Size = new Size(66, 23);
            edtEuler.TabIndex = 26;
            edtEuler.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // stimulusControl1
            // 
            stimulusControl1.AutoSize = true;
            stimulusControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            stimulusControl1.BackColor = Color.FromArgb(249, 249, 249);
            stimulusControl1.Location = new Point(3, 203);
            stimulusControl1.Margin = new Padding(3, 0, 3, 3);
            stimulusControl1.MinimumSize = new Size(200, 100);
            stimulusControl1.Name = "stimulusControl1";
            stimulusControl1.Size = new Size(217, 100);
            stimulusControl1.TabIndex = 25;
            stimulusControl1.StimulusChanged += stimulusControl1_StimulusChanged;
            // 
            // rbSingleEntryStimulus
            // 
            rbSingleEntryStimulus.AutoSize = true;
            rbSingleEntryStimulus.Checked = true;
            rbSingleEntryStimulus.Location = new Point(7, 74);
            rbSingleEntryStimulus.Name = "rbSingleEntryStimulus";
            rbSingleEntryStimulus.Size = new Size(87, 19);
            rbSingleEntryStimulus.TabIndex = 12;
            rbSingleEntryStimulus.TabStop = true;
            rbSingleEntryStimulus.Text = "Single Entry";
            rbSingleEntryStimulus.UseVisualStyleBackColor = true;
            rbSingleEntryStimulus.CheckedChanged += rbSingleEntryStimulus_CheckedChanged;
            // 
            // rbRheobaseBasedStimulus
            // 
            rbRheobaseBasedStimulus.AutoSize = true;
            rbRheobaseBasedStimulus.Location = new Point(7, 3);
            rbRheobaseBasedStimulus.Name = "rbRheobaseBasedStimulus";
            rbRheobaseBasedStimulus.Size = new Size(187, 19);
            rbRheobaseBasedStimulus.TabIndex = 13;
            rbRheobaseBasedStimulus.Text = "Rheobase based (x1, x1.1, x1.5)";
            toolTip1.SetToolTip(rbRheobaseBasedStimulus, "Step stimulus with 0 noise");
            rbRheobaseBasedStimulus.UseVisualStyleBackColor = true;
            rbRheobaseBasedStimulus.CheckedChanged += rbRheobaseBasedStimulus_CheckedChanged;
            // 
            // splitMain
            // 
            splitMain.BackColor = Color.Gray;
            splitMain.BorderStyle = BorderStyle.FixedSingle;
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(4, 4);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.BackColor = Color.White;
            splitMain.Panel1.Controls.Add(splitLeft);
            splitMain.Panel1.Controls.Add(pLoadSaveParams);
            splitMain.Panel1.Padding = new Padding(3);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.BackColor = Color.White;
            splitMain.Panel2.Controls.Add(splitGAAndPlots);
            splitMain.Panel2.Controls.Add(pPlots);
            splitMain.Size = new Size(1244, 675);
            splitMain.SplitterDistance = 336;
            splitMain.SplitterWidth = 2;
            splitMain.TabIndex = 17;
            // 
            // splitLeft
            // 
            splitLeft.BackColor = Color.Gray;
            splitLeft.Dock = DockStyle.Fill;
            splitLeft.Location = new Point(3, 3);
            splitLeft.Name = "splitLeft";
            splitLeft.Orientation = Orientation.Horizontal;
            // 
            // splitLeft.Panel1
            // 
            splitLeft.Panel1.BackColor = Color.White;
            splitLeft.Panel1.Controls.Add(pfParams);
            splitLeft.Panel1.Controls.Add(pCoreType);
            splitLeft.Panel1.Controls.Add(pRheobase);
            splitLeft.Panel1.Controls.Add(pTop);
            // 
            // splitLeft.Panel2
            // 
            splitLeft.Panel2.BackColor = Color.White;
            splitLeft.Panel2.Controls.Add(tabAnalysis);
            splitLeft.Size = new Size(328, 636);
            splitLeft.SplitterDistance = 286;
            splitLeft.TabIndex = 39;
            // 
            // pfParams
            // 
            pfParams.AutoScroll = true;
            pfParams.Dock = DockStyle.Fill;
            pfParams.Location = new Point(0, 63);
            pfParams.Name = "pfParams";
            pfParams.Size = new Size(328, 115);
            pfParams.TabIndex = 23;
            // 
            // pCoreType
            // 
            pCoreType.BackColor = Color.FromArgb(236, 239, 241);
            pCoreType.Controls.Add(pLineCoreType);
            pCoreType.Controls.Add(ddCoreType);
            pCoreType.Controls.Add(lCoreType);
            pCoreType.Dock = DockStyle.Top;
            pCoreType.Location = new Point(0, 23);
            pCoreType.Name = "pCoreType";
            pCoreType.Size = new Size(328, 40);
            pCoreType.TabIndex = 21;
            // 
            // pLineCoreType
            // 
            pLineCoreType.BackColor = Color.LightGray;
            pLineCoreType.Dock = DockStyle.Bottom;
            pLineCoreType.Location = new Point(0, 39);
            pLineCoreType.Name = "pLineCoreType";
            pLineCoreType.Size = new Size(328, 1);
            pLineCoreType.TabIndex = 24;
            // 
            // ddCoreType
            // 
            ddCoreType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCoreType.BackColor = Color.WhiteSmoke;
            ddCoreType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCoreType.FlatStyle = FlatStyle.Flat;
            ddCoreType.FormattingEnabled = true;
            ddCoreType.Location = new Point(71, 9);
            ddCoreType.Name = "ddCoreType";
            ddCoreType.Size = new Size(246, 23);
            ddCoreType.TabIndex = 23;
            ddCoreType.SelectedIndexChanged += ddCoreType_SelectedIndexChanged;
            // 
            // lCoreType
            // 
            lCoreType.AutoSize = true;
            lCoreType.Location = new Point(6, 12);
            lCoreType.Name = "lCoreType";
            lCoreType.Size = new Size(59, 15);
            lCoreType.TabIndex = 22;
            lCoreType.Text = "Core Type";
            // 
            // pRheobase
            // 
            pRheobase.Controls.Add(grRheobase);
            pRheobase.Dock = DockStyle.Bottom;
            pRheobase.Location = new Point(0, 178);
            pRheobase.Name = "pRheobase";
            pRheobase.Size = new Size(328, 108);
            pRheobase.TabIndex = 40;
            // 
            // pTop
            // 
            pTop.Controls.Add(linkUseUpdatedParams);
            pTop.Dock = DockStyle.Top;
            pTop.Location = new Point(0, 0);
            pTop.Name = "pTop";
            pTop.Size = new Size(328, 23);
            pTop.TabIndex = 24;
            pTop.Visible = false;
            // 
            // linkUseUpdatedParams
            // 
            linkUseUpdatedParams.AutoSize = true;
            linkUseUpdatedParams.LinkColor = Color.FromArgb(64, 64, 64);
            linkUseUpdatedParams.Location = new Point(4, 3);
            linkUseUpdatedParams.Name = "linkUseUpdatedParams";
            linkUseUpdatedParams.Size = new Size(136, 15);
            linkUseUpdatedParams.TabIndex = 19;
            linkUseUpdatedParams.TabStop = true;
            linkUseUpdatedParams.Text = "Use Updated Parameters";
            linkUseUpdatedParams.LinkClicked += linkUseUpdatedParams_LinkClicked;
            // 
            // tabAnalysis
            // 
            tabAnalysis.Controls.Add(tTest);
            tabAnalysis.Controls.Add(tSensitivityAnalysis);
            tabAnalysis.Dock = DockStyle.Fill;
            tabAnalysis.Location = new Point(0, 0);
            tabAnalysis.Name = "tabAnalysis";
            tabAnalysis.SelectedIndex = 0;
            tabAnalysis.Size = new Size(328, 346);
            tabAnalysis.TabIndex = 39;
            // 
            // tTest
            // 
            tTest.Controls.Add(pFlow);
            tTest.Controls.Add(pFlow5);
            tTest.Location = new Point(4, 24);
            tTest.Name = "tTest";
            tTest.Padding = new Padding(3);
            tTest.Size = new Size(320, 318);
            tTest.TabIndex = 0;
            tTest.Text = "Test";
            tTest.UseVisualStyleBackColor = true;
            // 
            // pFlow
            // 
            pFlow.AutoScroll = true;
            pFlow.Controls.Add(pFlow1);
            pFlow.Controls.Add(pFlow2);
            pFlow.Controls.Add(stimulusControl1);
            pFlow.Dock = DockStyle.Fill;
            pFlow.FlowDirection = FlowDirection.TopDown;
            pFlow.Location = new Point(3, 3);
            pFlow.Name = "pFlow";
            pFlow.Size = new Size(314, 282);
            pFlow.TabIndex = 38;
            pFlow.WrapContents = false;
            // 
            // pFlow1
            // 
            pFlow1.Controls.Add(lStepStartTime);
            pFlow1.Controls.Add(pSep1);
            pFlow1.Controls.Add(edt);
            pFlow1.Controls.Add(lPlotEndTime);
            pFlow1.Controls.Add(lStepEndTime);
            pFlow1.Controls.Add(eStepStartTime);
            pFlow1.Controls.Add(edtEuler);
            pFlow1.Controls.Add(ePlotEndTime);
            pFlow1.Controls.Add(lms);
            pFlow1.Controls.Add(lms2);
            pFlow1.Controls.Add(lPlotdt);
            pFlow1.Controls.Add(eStepEndTime);
            pFlow1.Controls.Add(lEulerdt);
            pFlow1.Location = new Point(3, 3);
            pFlow1.Name = "pFlow1";
            pFlow1.Size = new Size(277, 96);
            pFlow1.TabIndex = 0;
            // 
            // pSep1
            // 
            pSep1.BackColor = Color.DimGray;
            pSep1.Dock = DockStyle.Bottom;
            pSep1.Location = new Point(0, 93);
            pSep1.Name = "pSep1";
            pSep1.Size = new Size(277, 3);
            pSep1.TabIndex = 36;
            // 
            // lms
            // 
            lms.AutoSize = true;
            lms.Location = new Point(242, 10);
            lms.Name = "lms";
            lms.Size = new Size(31, 15);
            lms.TabIndex = 27;
            lms.Text = "(ms)";
            // 
            // lms2
            // 
            lms2.AutoSize = true;
            lms2.Location = new Point(140, 37);
            lms2.Name = "lms2";
            lms2.Size = new Size(31, 15);
            lms2.TabIndex = 28;
            lms2.Text = "(ms)";
            // 
            // lEulerdt
            // 
            lEulerdt.AutoSize = true;
            lEulerdt.Location = new Point(139, 66);
            lEulerdt.Name = "lEulerdt";
            lEulerdt.Size = new Size(48, 15);
            lEulerdt.TabIndex = 30;
            lEulerdt.Text = "Euler Δt";
            // 
            // pFlow2
            // 
            pFlow2.Controls.Add(rbSingleEntryStimulus);
            pFlow2.Controls.Add(eMultipleStimulus);
            pFlow2.Controls.Add(rbMultipleEntry);
            pFlow2.Controls.Add(lMultipleEntryNote);
            pFlow2.Controls.Add(rbRheobaseBasedStimulus);
            pFlow2.Location = new Point(3, 105);
            pFlow2.Name = "pFlow2";
            pFlow2.Size = new Size(277, 95);
            pFlow2.TabIndex = 37;
            // 
            // eMultipleStimulus
            // 
            eMultipleStimulus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eMultipleStimulus.Location = new Point(28, 45);
            eMultipleStimulus.Name = "eMultipleStimulus";
            eMultipleStimulus.ReadOnly = true;
            eMultipleStimulus.Size = new Size(245, 23);
            eMultipleStimulus.TabIndex = 33;
            eMultipleStimulus.Text = "example: 1;10";
            toolTip1.SetToolTip(eMultipleStimulus, "Step stimulus with 0 noise");
            eMultipleStimulus.Enter += eMultipleStimulus_Enter;
            eMultipleStimulus.Leave += eMultipleStimulus_Leave;
            // 
            // rbMultipleEntry
            // 
            rbMultipleEntry.AutoSize = true;
            rbMultipleEntry.Location = new Point(7, 25);
            rbMultipleEntry.Name = "rbMultipleEntry";
            rbMultipleEntry.Size = new Size(99, 19);
            rbMultipleEntry.TabIndex = 31;
            rbMultipleEntry.Text = "Multiple Entry";
            toolTip1.SetToolTip(rbMultipleEntry, "Step stimulus with 0 noise");
            rbMultipleEntry.UseVisualStyleBackColor = true;
            rbMultipleEntry.CheckedChanged += rbMultipleEntry_CheckedChanged;
            // 
            // lMultipleEntryNote
            // 
            lMultipleEntryNote.AutoSize = true;
            lMultipleEntryNote.Location = new Point(102, 27);
            lMultipleEntryNote.Name = "lMultipleEntryNote";
            lMultipleEntryNote.Size = new Size(174, 15);
            lMultipleEntryNote.TabIndex = 32;
            lMultipleEntryNote.Text = "(enter values seperated with \";\")";
            // 
            // pFlow5
            // 
            pFlow5.Controls.Add(btnDynamicsRun);
            pFlow5.Controls.Add(cbAutoDrawPlots);
            pFlow5.Dock = DockStyle.Bottom;
            pFlow5.Location = new Point(3, 285);
            pFlow5.Name = "pFlow5";
            pFlow5.Size = new Size(314, 30);
            pFlow5.TabIndex = 40;
            // 
            // btnDynamicsRun
            // 
            btnDynamicsRun.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDynamicsRun.BackColor = Color.FromArgb(96, 125, 139);
            btnDynamicsRun.FlatStyle = FlatStyle.Flat;
            btnDynamicsRun.ForeColor = Color.White;
            btnDynamicsRun.Location = new Point(105, 2);
            btnDynamicsRun.Name = "btnDynamicsRun";
            btnDynamicsRun.Size = new Size(76, 25);
            btnDynamicsRun.TabIndex = 35;
            btnDynamicsRun.Text = "Run";
            btnDynamicsRun.UseVisualStyleBackColor = false;
            btnDynamicsRun.Click += btnDynamicsRun_Click;
            // 
            // cbAutoDrawPlots
            // 
            cbAutoDrawPlots.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbAutoDrawPlots.AutoSize = true;
            cbAutoDrawPlots.Checked = true;
            cbAutoDrawPlots.CheckState = CheckState.Checked;
            cbAutoDrawPlots.Location = new Point(3, 8);
            cbAutoDrawPlots.Name = "cbAutoDrawPlots";
            cbAutoDrawPlots.Size = new Size(82, 19);
            cbAutoDrawPlots.TabIndex = 34;
            cbAutoDrawPlots.Text = "Auto Draw";
            cbAutoDrawPlots.UseVisualStyleBackColor = true;
            cbAutoDrawPlots.CheckedChanged += cbAutoDrawPlots_CheckedChanged;
            // 
            // tSensitivityAnalysis
            // 
            tSensitivityAnalysis.Controls.Add(grFiring);
            tSensitivityAnalysis.Controls.Add(grRheoSens);
            tSensitivityAnalysis.Location = new Point(4, 24);
            tSensitivityAnalysis.Name = "tSensitivityAnalysis";
            tSensitivityAnalysis.Padding = new Padding(3);
            tSensitivityAnalysis.Size = new Size(320, 318);
            tSensitivityAnalysis.TabIndex = 1;
            tSensitivityAnalysis.Text = "Sensitivity Analysis";
            tSensitivityAnalysis.UseVisualStyleBackColor = true;
            // 
            // grFiring
            // 
            grFiring.Controls.Add(sensitivityAnalysisFiring);
            grFiring.Dock = DockStyle.Top;
            grFiring.Location = new Point(3, 147);
            grFiring.Name = "grFiring";
            grFiring.Size = new Size(314, 138);
            grFiring.TabIndex = 19;
            grFiring.TabStop = false;
            grFiring.Text = "Firing Pattern";
            // 
            // sensitivityAnalysisFiring
            // 
            sensitivityAnalysisFiring.BackColor = Color.Transparent;
            sensitivityAnalysisFiring.Location = new Point(6, 18);
            sensitivityAnalysisFiring.Name = "sensitivityAnalysisFiring";
            sensitivityAnalysisFiring.Size = new Size(215, 120);
            sensitivityAnalysisFiring.TabIndex = 0;
            // 
            // grRheoSens
            // 
            grRheoSens.Controls.Add(sensitivityAnalysisRheobase);
            grRheoSens.Dock = DockStyle.Top;
            grRheoSens.Location = new Point(3, 3);
            grRheoSens.Name = "grRheoSens";
            grRheoSens.Size = new Size(314, 144);
            grRheoSens.TabIndex = 18;
            grRheoSens.TabStop = false;
            grRheoSens.Text = "Rheobase Sensitivity";
            // 
            // sensitivityAnalysisRheobase
            // 
            sensitivityAnalysisRheobase.BackColor = Color.Transparent;
            sensitivityAnalysisRheobase.Location = new Point(3, 22);
            sensitivityAnalysisRheobase.Name = "sensitivityAnalysisRheobase";
            sensitivityAnalysisRheobase.Size = new Size(215, 120);
            sensitivityAnalysisRheobase.TabIndex = 0;
            // 
            // pLoadSaveParams
            // 
            pLoadSaveParams.BackColor = Color.FromArgb(236, 239, 241);
            pLoadSaveParams.Controls.Add(linkLoadCoreUnit);
            pLoadSaveParams.Controls.Add(linkSaveCoreUnit);
            pLoadSaveParams.Dock = DockStyle.Bottom;
            pLoadSaveParams.Location = new Point(3, 639);
            pLoadSaveParams.Name = "pLoadSaveParams";
            pLoadSaveParams.Size = new Size(328, 31);
            pLoadSaveParams.TabIndex = 20;
            // 
            // linkLoadCoreUnit
            // 
            linkLoadCoreUnit.AutoSize = true;
            linkLoadCoreUnit.LinkColor = Color.FromArgb(64, 64, 64);
            linkLoadCoreUnit.Location = new Point(4, 8);
            linkLoadCoreUnit.Name = "linkLoadCoreUnit";
            linkLoadCoreUnit.Size = new Size(86, 15);
            linkLoadCoreUnit.TabIndex = 20;
            linkLoadCoreUnit.TabStop = true;
            linkLoadCoreUnit.Text = "Load Core Unit";
            linkLoadCoreUnit.LinkClicked += linkLoadCoreUnit_LinkClicked;
            // 
            // linkSaveCoreUnit
            // 
            linkSaveCoreUnit.AutoSize = true;
            linkSaveCoreUnit.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveCoreUnit.Location = new Point(96, 8);
            linkSaveCoreUnit.Name = "linkSaveCoreUnit";
            linkSaveCoreUnit.Size = new Size(84, 15);
            linkSaveCoreUnit.TabIndex = 21;
            linkSaveCoreUnit.TabStop = true;
            linkSaveCoreUnit.Text = "Save Core Unit";
            linkSaveCoreUnit.LinkClicked += linkSaveCoreUnit_LinkClicked;
            // 
            // splitGAAndPlots
            // 
            splitGAAndPlots.BackColor = Color.Gray;
            splitGAAndPlots.BorderStyle = BorderStyle.FixedSingle;
            splitGAAndPlots.Dock = DockStyle.Fill;
            splitGAAndPlots.Location = new Point(0, 65);
            splitGAAndPlots.Name = "splitGAAndPlots";
            splitGAAndPlots.Orientation = Orientation.Horizontal;
            // 
            // splitGAAndPlots.Panel1
            // 
            splitGAAndPlots.Panel1.BackColor = Color.White;
            splitGAAndPlots.Panel1.Controls.Add(pOptimize);
            // 
            // splitGAAndPlots.Panel2
            // 
            splitGAAndPlots.Panel2.BackColor = Color.White;
            splitGAAndPlots.Panel2.Controls.Add(webViewPlots);
            splitGAAndPlots.Size = new Size(906, 610);
            splitGAAndPlots.SplitterDistance = 346;
            splitGAAndPlots.SplitterWidth = 2;
            splitGAAndPlots.TabIndex = 2;
            // 
            // pOptimize
            // 
            pOptimize.Controls.Add(gaControl);
            pOptimize.Dock = DockStyle.Fill;
            pOptimize.Location = new Point(0, 0);
            pOptimize.Name = "pOptimize";
            pOptimize.Size = new Size(904, 344);
            pOptimize.TabIndex = 39;
            // 
            // gaControl
            // 
            gaControl.CoreType = null;
            gaControl.DeltaT = 0D;
            gaControl.DeltaTEuler = 0D;
            gaControl.Dock = DockStyle.Fill;
            gaControl.Location = new Point(0, 0);
            gaControl.Name = "gaControl";
            gaControl.Parameters = null;
            gaControl.Size = new Size(904, 344);
            gaControl.TabIndex = 0;
            // 
            // webViewPlots
            // 
            webViewPlots.AllowExternalDrop = true;
            webViewPlots.CreationProperties = null;
            webViewPlots.DefaultBackgroundColor = Color.White;
            webViewPlots.Dock = DockStyle.Fill;
            webViewPlots.Location = new Point(0, 0);
            webViewPlots.Name = "webViewPlots";
            webViewPlots.Size = new Size(904, 260);
            webViewPlots.TabIndex = 0;
            webViewPlots.ZoomFactor = 1D;
            webViewPlots.CoreWebView2InitializationCompleted += webViewPlots_CoreWebView2InitializationCompleted;
            // 
            // pPlots
            // 
            pPlots.Controls.Add(panel1);
            pPlots.Controls.Add(grPlotSelection);
            pPlots.Controls.Add(linkSwitchToOptimization);
            pPlots.Dock = DockStyle.Top;
            pPlots.Location = new Point(0, 0);
            pPlots.Name = "pPlots";
            pPlots.Size = new Size(906, 65);
            pPlots.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightGray;
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 64);
            panel1.Name = "panel1";
            panel1.Size = new Size(906, 1);
            panel1.TabIndex = 9;
            // 
            // grPlotSelection
            // 
            grPlotSelection.Controls.Add(cbV);
            grPlotSelection.Controls.Add(cbStimulus);
            grPlotSelection.Controls.Add(cbSecondaryLists);
            grPlotSelection.Controls.Add(cbSpikingFrequency);
            grPlotSelection.Controls.Add(cbTauRise);
            grPlotSelection.Controls.Add(cbInterval);
            grPlotSelection.Controls.Add(cbTauDecay);
            grPlotSelection.Location = new Point(10, 0);
            grPlotSelection.Name = "grPlotSelection";
            grPlotSelection.Size = new Size(522, 61);
            grPlotSelection.TabIndex = 8;
            grPlotSelection.TabStop = false;
            // 
            // cbV
            // 
            cbV.AutoSize = true;
            cbV.Checked = true;
            cbV.CheckState = CheckState.Checked;
            cbV.Location = new Point(6, 14);
            cbV.Name = "cbV";
            cbV.Size = new Size(152, 19);
            cbV.TabIndex = 0;
            cbV.Text = "Membrane Potential (V)";
            cbV.UseVisualStyleBackColor = true;
            cbV.CheckedChanged += cbPlotSelection_CheckedChanged;
            // 
            // cbStimulus
            // 
            cbStimulus.AutoSize = true;
            cbStimulus.Checked = true;
            cbStimulus.CheckState = CheckState.Checked;
            cbStimulus.Location = new Point(429, 14);
            cbStimulus.Name = "cbStimulus";
            cbStimulus.Size = new Size(72, 19);
            cbStimulus.TabIndex = 6;
            cbStimulus.Text = "Stimulus";
            cbStimulus.UseVisualStyleBackColor = true;
            cbStimulus.CheckedChanged += cbPlotSelection_CheckedChanged;
            // 
            // cbSecondaryLists
            // 
            cbSecondaryLists.AutoSize = true;
            cbSecondaryLists.Location = new Point(6, 34);
            cbSecondaryLists.Name = "cbSecondaryLists";
            cbSecondaryLists.Size = new Size(110, 19);
            cbSecondaryLists.TabIndex = 1;
            cbSecondaryLists.Text = "Other dynamics";
            toolTip1.SetToolTip(cbSecondaryLists, "depending on the model, can be feedback current, ion based currents, relative tension, etc");
            cbSecondaryLists.UseVisualStyleBackColor = true;
            cbSecondaryLists.CheckedChanged += cbPlotSelection_CheckedChanged;
            // 
            // cbSpikingFrequency
            // 
            cbSpikingFrequency.AutoSize = true;
            cbSpikingFrequency.Location = new Point(290, 31);
            cbSpikingFrequency.Name = "cbSpikingFrequency";
            cbSpikingFrequency.Size = new Size(123, 19);
            cbSpikingFrequency.TabIndex = 5;
            cbSpikingFrequency.Text = "Spiking Frequency";
            cbSpikingFrequency.UseVisualStyleBackColor = true;
            cbSpikingFrequency.CheckedChanged += cbPlotSelection_CheckedChanged;
            // 
            // cbTauRise
            // 
            cbTauRise.AutoSize = true;
            cbTauRise.Location = new Point(177, 14);
            cbTauRise.Name = "cbTauRise";
            cbTauRise.Size = new Size(68, 19);
            cbTauRise.TabIndex = 2;
            cbTauRise.Text = "Tau Rise";
            cbTauRise.UseVisualStyleBackColor = true;
            cbTauRise.CheckedChanged += cbPlotSelection_CheckedChanged;
            // 
            // cbInterval
            // 
            cbInterval.AutoSize = true;
            cbInterval.Location = new Point(290, 11);
            cbInterval.Name = "cbInterval";
            cbInterval.Size = new Size(65, 19);
            cbInterval.TabIndex = 4;
            cbInterval.Text = "Interval";
            cbInterval.UseVisualStyleBackColor = true;
            cbInterval.CheckedChanged += cbPlotSelection_CheckedChanged;
            // 
            // cbTauDecay
            // 
            cbTauDecay.AutoSize = true;
            cbTauDecay.Location = new Point(177, 34);
            cbTauDecay.Name = "cbTauDecay";
            cbTauDecay.Size = new Size(79, 19);
            cbTauDecay.TabIndex = 3;
            cbTauDecay.Text = "Tau Decay";
            cbTauDecay.UseVisualStyleBackColor = true;
            cbTauDecay.CheckedChanged += cbPlotSelection_CheckedChanged;
            // 
            // linkSwitchToOptimization
            // 
            linkSwitchToOptimization.AutoSize = true;
            linkSwitchToOptimization.LinkColor = Color.FromArgb(64, 64, 64);
            linkSwitchToOptimization.Location = new Point(538, 43);
            linkSwitchToOptimization.Name = "linkSwitchToOptimization";
            linkSwitchToOptimization.Size = new Size(110, 15);
            linkSwitchToOptimization.TabIndex = 7;
            linkSwitchToOptimization.TabStop = true;
            linkSwitchToOptimization.Text = "Optimization Mode";
            linkSwitchToOptimization.LinkClicked += linkSwitchToOptimization_LinkClicked;
            // 
            // saveFileJson
            // 
            saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // openFileJson
            // 
            openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // pDistinguisherLeft
            // 
            pDistinguisherLeft.BackColor = Color.FromArgb(255, 192, 192);
            pDistinguisherLeft.Dock = DockStyle.Left;
            pDistinguisherLeft.Location = new Point(0, 0);
            pDistinguisherLeft.Name = "pDistinguisherLeft";
            pDistinguisherLeft.Size = new Size(4, 683);
            pDistinguisherLeft.TabIndex = 18;
            // 
            // pDistinguisherTop
            // 
            pDistinguisherTop.BackColor = Color.FromArgb(255, 192, 192);
            pDistinguisherTop.Dock = DockStyle.Top;
            pDistinguisherTop.Location = new Point(4, 0);
            pDistinguisherTop.Name = "pDistinguisherTop";
            pDistinguisherTop.Size = new Size(1248, 4);
            pDistinguisherTop.TabIndex = 19;
            // 
            // pDistinguisherBottom
            // 
            pDistinguisherBottom.BackColor = Color.FromArgb(255, 192, 192);
            pDistinguisherBottom.Dock = DockStyle.Bottom;
            pDistinguisherBottom.Location = new Point(4, 679);
            pDistinguisherBottom.Name = "pDistinguisherBottom";
            pDistinguisherBottom.Size = new Size(1248, 4);
            pDistinguisherBottom.TabIndex = 20;
            // 
            // pDistinguisherRight
            // 
            pDistinguisherRight.BackColor = Color.FromArgb(255, 192, 192);
            pDistinguisherRight.Dock = DockStyle.Right;
            pDistinguisherRight.Location = new Point(1248, 4);
            pDistinguisherRight.Name = "pDistinguisherRight";
            pDistinguisherRight.Size = new Size(4, 675);
            pDistinguisherRight.TabIndex = 21;
            // 
            // DynamicsTestControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(splitMain);
            Controls.Add(pDistinguisherRight);
            Controls.Add(pDistinguisherBottom);
            Controls.Add(pDistinguisherTop);
            Controls.Add(pDistinguisherLeft);
            Name = "DynamicsTestControl";
            Size = new Size(1252, 683);
            Load += DynamicsTestControl_Load;
            ((System.ComponentModel.ISupportInitialize)eRheobaseLimit).EndInit();
            ((System.ComponentModel.ISupportInitialize)edt).EndInit();
            ((System.ComponentModel.ISupportInitialize)ePlotEndTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)eStepEndTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)eStepStartTime).EndInit();
            grRheobase.ResumeLayout(false);
            grRheobase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)eRheobaseDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)edtEuler).EndInit();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            splitLeft.Panel1.ResumeLayout(false);
            splitLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitLeft).EndInit();
            splitLeft.ResumeLayout(false);
            pCoreType.ResumeLayout(false);
            pCoreType.PerformLayout();
            pRheobase.ResumeLayout(false);
            pTop.ResumeLayout(false);
            pTop.PerformLayout();
            tabAnalysis.ResumeLayout(false);
            tTest.ResumeLayout(false);
            pFlow.ResumeLayout(false);
            pFlow.PerformLayout();
            pFlow1.ResumeLayout(false);
            pFlow1.PerformLayout();
            pFlow2.ResumeLayout(false);
            pFlow2.PerformLayout();
            pFlow5.ResumeLayout(false);
            pFlow5.PerformLayout();
            tSensitivityAnalysis.ResumeLayout(false);
            grFiring.ResumeLayout(false);
            grRheoSens.ResumeLayout(false);
            pLoadSaveParams.ResumeLayout(false);
            pLoadSaveParams.PerformLayout();
            splitGAAndPlots.Panel1.ResumeLayout(false);
            splitGAAndPlots.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitGAAndPlots).EndInit();
            splitGAAndPlots.ResumeLayout(false);
            pOptimize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webViewPlots).EndInit();
            pPlots.ResumeLayout(false);
            pPlots.PerformLayout();
            grPlotSelection.ResumeLayout(false);
            grPlotSelection.PerformLayout();
            ResumeLayout(false);
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
        private Panel pTop;
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
        private StimulusSettingsControl stimulusControl1;
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
        private Panel pDistinguisherLeft;
        private Panel pDistinguisherTop;
        private Panel pDistinguisherBottom;
        private Panel pDistinguisherRight;
        private FlowLayoutPanel pFlow;
        private Panel pFlow1;
        private Panel pSep1;
        private Panel pFlow2;
        private SplitContainer splitLeft;
        private Panel pFlow5;
        private Button btnDynamicsRun;
        private CheckBox cbAutoDrawPlots;
    }
}
