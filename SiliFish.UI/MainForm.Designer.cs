using SiliFish.UI.Controls;

namespace SiliFish.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.rbSingleCoil = new System.Windows.Forms.RadioButton();
            this.rbDoubleCoil = new System.Windows.Forms.RadioButton();
            this.rbBeatGlide = new System.Windows.Forms.RadioButton();
            this.pTop = new System.Windows.Forms.Panel();
            this.linkClearModel = new System.Windows.Forms.LinkLabel();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.linkSaveHTML = new System.Windows.Forms.LinkLabel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tabParams = new System.Windows.Forms.TabControl();
            this.tabCellPools = new System.Windows.Forms.TabPage();
            this.splitCellPoolsAndJunctions = new System.Windows.Forms.SplitContainer();
            this.listCellPool = new SiliFish.UI.Controls.ListBoxControl();
            this.listJunctions = new SiliFish.UI.Controls.ListBoxControl();
            this.tabStimuli = new System.Windows.Forms.TabPage();
            this.listStimuli = new SiliFish.UI.Controls.ListBoxControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.pBodyDiagrams = new System.Windows.Forms.Panel();
            this.picRostroCaudal = new System.Windows.Forms.PictureBox();
            this.picCrossSection = new System.Windows.Forms.PictureBox();
            this.grSpinalCord = new System.Windows.Forms.GroupBox();
            this.eSpinalBodyPosition = new System.Windows.Forms.NumericUpDown();
            this.lSpinalBodyPosition = new System.Windows.Forms.Label();
            this.eSpinalRostraoCaudal = new System.Windows.Forms.NumericUpDown();
            this.lSpinalRostroCaudal = new System.Windows.Forms.Label();
            this.eSpinalMedialLateral = new System.Windows.Forms.NumericUpDown();
            this.lSpinalMedialLateral = new System.Windows.Forms.Label();
            this.eSpinalDorsalVentral = new System.Windows.Forms.NumericUpDown();
            this.lSpinalDorsalVentral = new System.Windows.Forms.Label();
            this.grBody = new System.Windows.Forms.GroupBox();
            this.eBodyMedialLateral = new System.Windows.Forms.NumericUpDown();
            this.lBodyMedialLateral = new System.Windows.Forms.Label();
            this.eBodyDorsalVentral = new System.Windows.Forms.NumericUpDown();
            this.lBodyDorsalVentral = new System.Windows.Forms.Label();
            this.eModelDescription = new System.Windows.Forms.TextBox();
            this.eModelName = new System.Windows.Forms.TextBox();
            this.lModelDescription = new System.Windows.Forms.Label();
            this.lModelName = new System.Windows.Forms.Label();
            this.pMain = new System.Windows.Forms.Panel();
            this.linkLoadParam = new System.Windows.Forms.LinkLabel();
            this.lParameters = new System.Windows.Forms.Label();
            this.linkSaveParam = new System.Windows.Forms.LinkLabel();
            this.tabOutputs = new System.Windows.Forms.TabControl();
            this.tabPlot = new System.Windows.Forms.TabPage();
            this.splitWindows = new System.Windows.Forms.SplitContainer();
            this.pictureBoxLeft = new SiliFish.UI.Controls.PictureBoxControl();
            this.pictureBoxRight = new SiliFish.UI.Controls.PictureBoxControl();
            this.pPlotWindows = new System.Windows.Forms.Panel();
            this.lCellNumber = new System.Windows.Forms.Label();
            this.cbSample = new System.Windows.Forms.CheckBox();
            this.enSample = new System.Windows.Forms.NumericUpDown();
            this.lPlotWindowsPool = new System.Windows.Forms.Label();
            this.ddCellPoolsWindows = new System.Windows.Forms.ComboBox();
            this.lPlotWindowsStart = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lPlotWindowsEnd = new System.Windows.Forms.Label();
            this.ePlotWindowsEnd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ePlotWindowsStart = new System.Windows.Forms.TextBox();
            this.lPlotWindowsPlot = new System.Windows.Forms.Label();
            this.btnPlotWindows = new System.Windows.Forms.Button();
            this.ddPlotWindows = new System.Windows.Forms.ComboBox();
            this.tab2DModel = new System.Windows.Forms.TabPage();
            this.webView2DModel = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.p2DModel = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnGenerate2DModel = new System.Windows.Forms.Button();
            this.tab3DModel = new System.Windows.Forms.TabPage();
            this.webView3DModel = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.p3DModel = new System.Windows.Forms.Panel();
            this.dd3DModelType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnGenerate3DModel = new System.Windows.Forms.Button();
            this.tabHTMLPlot = new System.Windows.Forms.TabPage();
            this.webViewPlot = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pPlot = new System.Windows.Forms.Panel();
            this.cbSampleHTML = new System.Windows.Forms.CheckBox();
            this.nbSample = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.lCellsPools = new System.Windows.Forms.Label();
            this.ddCellsPools = new System.Windows.Forms.ComboBox();
            this.lPlotStart = new System.Windows.Forms.Label();
            this.lms3 = new System.Windows.Forms.Label();
            this.lPlotEnd = new System.Windows.Forms.Label();
            this.ePlotEnd = new System.Windows.Forms.TextBox();
            this.lms4 = new System.Windows.Forms.Label();
            this.ePlotStart = new System.Windows.Forms.TextBox();
            this.lGrouping = new System.Windows.Forms.Label();
            this.ddGrouping = new System.Windows.Forms.ComboBox();
            this.lPlot = new System.Windows.Forms.Label();
            this.btnPlot = new System.Windows.Forms.Button();
            this.ddPlot = new System.Windows.Forms.ComboBox();
            this.tabAnimation = new System.Windows.Forms.TabPage();
            this.webViewAnimation = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pAnimation = new System.Windows.Forms.Panel();
            this.linkSaveAnimation = new System.Windows.Forms.LinkLabel();
            this.btnAnimate = new System.Windows.Forms.Button();
            this.lAnimationStart = new System.Windows.Forms.Label();
            this.lms5 = new System.Windows.Forms.Label();
            this.lAnimationEnd = new System.Windows.Forms.Label();
            this.eAnimationEnd = new System.Windows.Forms.TextBox();
            this.lms6 = new System.Windows.Forms.Label();
            this.eAnimationStart = new System.Windows.Forms.TextBox();
            this.tabTextOutput = new System.Windows.Forms.TabPage();
            this.eModelSummary = new System.Windows.Forms.RichTextBox();
            this.pParamBottom = new System.Windows.Forms.Panel();
            this.lStimulus = new System.Windows.Forms.Label();
            this.ddStimulusMode = new System.Windows.Forms.ComboBox();
            this.linkSaveRun = new System.Windows.Forms.LinkLabel();
            this.progressBarRun = new System.Windows.Forms.ProgressBar();
            this.eSkip = new System.Windows.Forms.TextBox();
            this.eTimeEnd = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.lms2 = new System.Windows.Forms.Label();
            this.lSkip = new System.Windows.Forms.Label();
            this.lTimeEnd = new System.Windows.Forms.Label();
            this.lRunParameters = new System.Windows.Forms.Label();
            this.timerRun = new System.Windows.Forms.Timer(this.components);
            this.saveFileHTML = new System.Windows.Forms.SaveFileDialog();
            this.saveFileJson = new System.Windows.Forms.SaveFileDialog();
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            this.saveFileCSV = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileText = new System.Windows.Forms.SaveFileDialog();
            this.pTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tabParams.SuspendLayout();
            this.tabCellPools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCellPoolsAndJunctions)).BeginInit();
            this.splitCellPoolsAndJunctions.Panel1.SuspendLayout();
            this.splitCellPoolsAndJunctions.Panel2.SuspendLayout();
            this.splitCellPoolsAndJunctions.SuspendLayout();
            this.tabStimuli.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.pBodyDiagrams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRostroCaudal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrossSection)).BeginInit();
            this.grSpinalCord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalBodyPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalRostraoCaudal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalMedialLateral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalDorsalVentral)).BeginInit();
            this.grBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eBodyMedialLateral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eBodyDorsalVentral)).BeginInit();
            this.pMain.SuspendLayout();
            this.tabOutputs.SuspendLayout();
            this.tabPlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitWindows)).BeginInit();
            this.splitWindows.Panel1.SuspendLayout();
            this.splitWindows.Panel2.SuspendLayout();
            this.splitWindows.SuspendLayout();
            this.pPlotWindows.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enSample)).BeginInit();
            this.tab2DModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView2DModel)).BeginInit();
            this.p2DModel.SuspendLayout();
            this.tab3DModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView3DModel)).BeginInit();
            this.p3DModel.SuspendLayout();
            this.tabHTMLPlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlot)).BeginInit();
            this.pPlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbSample)).BeginInit();
            this.tabAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewAnimation)).BeginInit();
            this.pAnimation.SuspendLayout();
            this.tabTextOutput.SuspendLayout();
            this.pParamBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbSingleCoil
            // 
            this.rbSingleCoil.AutoSize = true;
            this.rbSingleCoil.Location = new System.Drawing.Point(12, 12);
            this.rbSingleCoil.Name = "rbSingleCoil";
            this.rbSingleCoil.Size = new System.Drawing.Size(81, 19);
            this.rbSingleCoil.TabIndex = 0;
            this.rbSingleCoil.Text = "Single Coil";
            this.rbSingleCoil.UseVisualStyleBackColor = true;
            this.rbSingleCoil.CheckedChanged += new System.EventHandler(this.rbSingleCoil_CheckedChanged);
            // 
            // rbDoubleCoil
            // 
            this.rbDoubleCoil.AutoSize = true;
            this.rbDoubleCoil.Location = new System.Drawing.Point(128, 12);
            this.rbDoubleCoil.Name = "rbDoubleCoil";
            this.rbDoubleCoil.Size = new System.Drawing.Size(87, 19);
            this.rbDoubleCoil.TabIndex = 1;
            this.rbDoubleCoil.Text = "Double Coil";
            this.rbDoubleCoil.UseVisualStyleBackColor = true;
            this.rbDoubleCoil.CheckedChanged += new System.EventHandler(this.rbDoubleCoil_CheckedChanged);
            // 
            // rbBeatGlide
            // 
            this.rbBeatGlide.AutoSize = true;
            this.rbBeatGlide.Location = new System.Drawing.Point(250, 12);
            this.rbBeatGlide.Name = "rbBeatGlide";
            this.rbBeatGlide.Size = new System.Drawing.Size(101, 19);
            this.rbBeatGlide.TabIndex = 2;
            this.rbBeatGlide.Text = "Beat and Glide";
            this.rbBeatGlide.UseVisualStyleBackColor = true;
            this.rbBeatGlide.CheckedChanged += new System.EventHandler(this.rbBeatGlide_CheckedChanged);
            // 
            // pTop
            // 
            this.pTop.Controls.Add(this.linkClearModel);
            this.pTop.Controls.Add(this.rbCustom);
            this.pTop.Controls.Add(this.linkSaveHTML);
            this.pTop.Controls.Add(this.rbSingleCoil);
            this.pTop.Controls.Add(this.rbBeatGlide);
            this.pTop.Controls.Add(this.rbDoubleCoil);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1071, 39);
            this.pTop.TabIndex = 3;
            this.pTop.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pTop_MouseDoubleClick);
            // 
            // linkClearModel
            // 
            this.linkClearModel.AutoSize = true;
            this.linkClearModel.Location = new System.Drawing.Point(457, 14);
            this.linkClearModel.Name = "linkClearModel";
            this.linkClearModel.Size = new System.Drawing.Size(71, 15);
            this.linkClearModel.TabIndex = 5;
            this.linkClearModel.TabStop = true;
            this.linkClearModel.Text = "Clear Model";
            this.linkClearModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClearModel_LinkClicked);
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(371, 12);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(67, 19);
            this.rbCustom.TabIndex = 4;
            this.rbCustom.Text = "Custom";
            this.rbCustom.UseVisualStyleBackColor = true;
            this.rbCustom.CheckedChanged += new System.EventHandler(this.rbCustom_CheckedChanged);
            // 
            // linkSaveHTML
            // 
            this.linkSaveHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveHTML.AutoSize = true;
            this.linkSaveHTML.Location = new System.Drawing.Point(999, 12);
            this.linkSaveHTML.Name = "linkSaveHTML";
            this.linkSaveHTML.Size = new System.Drawing.Size(66, 15);
            this.linkSaveHTML.TabIndex = 3;
            this.linkSaveHTML.TabStop = true;
            this.linkSaveHTML.Text = "Save HTML";
            this.linkSaveHTML.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveHTML_LinkClicked);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 39);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.tabParams);
            this.splitMain.Panel1.Controls.Add(this.pMain);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.tabOutputs);
            this.splitMain.Panel2.Controls.Add(this.pParamBottom);
            this.splitMain.Size = new System.Drawing.Size(1071, 672);
            this.splitMain.SplitterDistance = 437;
            this.splitMain.TabIndex = 5;
            // 
            // tabParams
            // 
            this.tabParams.Controls.Add(this.tabCellPools);
            this.tabParams.Controls.Add(this.tabStimuli);
            this.tabParams.Controls.Add(this.tabGeneral);
            this.tabParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabParams.Location = new System.Drawing.Point(0, 34);
            this.tabParams.Name = "tabParams";
            this.tabParams.SelectedIndex = 0;
            this.tabParams.Size = new System.Drawing.Size(437, 638);
            this.tabParams.TabIndex = 2;
            this.tabParams.Tag = "";
            // 
            // tabCellPools
            // 
            this.tabCellPools.Controls.Add(this.splitCellPoolsAndJunctions);
            this.tabCellPools.Location = new System.Drawing.Point(4, 24);
            this.tabCellPools.Name = "tabCellPools";
            this.tabCellPools.Size = new System.Drawing.Size(429, 610);
            this.tabCellPools.TabIndex = 1;
            this.tabCellPools.Text = "Cell Pools & Junctions";
            this.tabCellPools.UseVisualStyleBackColor = true;
            // 
            // splitCellPoolsAndJunctions
            // 
            this.splitCellPoolsAndJunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCellPoolsAndJunctions.Location = new System.Drawing.Point(0, 0);
            this.splitCellPoolsAndJunctions.Name = "splitCellPoolsAndJunctions";
            // 
            // splitCellPoolsAndJunctions.Panel1
            // 
            this.splitCellPoolsAndJunctions.Panel1.Controls.Add(this.listCellPool);
            this.splitCellPoolsAndJunctions.Panel1MinSize = 200;
            // 
            // splitCellPoolsAndJunctions.Panel2
            // 
            this.splitCellPoolsAndJunctions.Panel2.Controls.Add(this.listJunctions);
            this.splitCellPoolsAndJunctions.Panel2MinSize = 200;
            this.splitCellPoolsAndJunctions.Size = new System.Drawing.Size(429, 610);
            this.splitCellPoolsAndJunctions.SplitterDistance = 200;
            this.splitCellPoolsAndJunctions.TabIndex = 5;
            // 
            // listCellPool
            // 
            this.listCellPool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCellPool.Location = new System.Drawing.Point(0, 0);
            this.listCellPool.Name = "listCellPool";
            this.listCellPool.SelectedIndex = -1;
            this.listCellPool.SelectedItem = null;
            this.listCellPool.Size = new System.Drawing.Size(200, 610);
            this.listCellPool.TabIndex = 5;
            this.listCellPool.AddItem += new System.EventHandler(this.listCellPool_AddItem);
            this.listCellPool.DeleteItem += new System.EventHandler(this.listCellPool_DeleteItem);
            this.listCellPool.CopyItem += new System.EventHandler(this.listCellPool_CopyItem);
            this.listCellPool.ViewItem += new System.EventHandler(this.listCellPool_ViewItem);
            this.listCellPool.ActivateItem += new System.EventHandler(this.listCellPool_ActivateItem);
            this.listCellPool.SortItems += new System.EventHandler(this.listCellPool_SortItems);
            // 
            // listJunctions
            // 
            this.listJunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listJunctions.Location = new System.Drawing.Point(0, 0);
            this.listJunctions.Name = "listJunctions";
            this.listJunctions.SelectedIndex = -1;
            this.listJunctions.SelectedItem = null;
            this.listJunctions.Size = new System.Drawing.Size(225, 610);
            this.listJunctions.TabIndex = 6;
            this.listJunctions.AddItem += new System.EventHandler(this.listJunctions_AddItem);
            this.listJunctions.DeleteItem += new System.EventHandler(this.listJunctions_DeleteItem);
            this.listJunctions.CopyItem += new System.EventHandler(this.listJunctions_CopyItem);
            this.listJunctions.ViewItem += new System.EventHandler(this.listJunctions_ViewItem);
            this.listJunctions.ActivateItem += new System.EventHandler(this.listJunctions_ActivateItem);
            this.listJunctions.SortItems += new System.EventHandler(this.listJunctions_SortItems);
            // 
            // tabStimuli
            // 
            this.tabStimuli.Controls.Add(this.listStimuli);
            this.tabStimuli.Location = new System.Drawing.Point(4, 24);
            this.tabStimuli.Name = "tabStimuli";
            this.tabStimuli.Size = new System.Drawing.Size(429, 610);
            this.tabStimuli.TabIndex = 3;
            this.tabStimuli.Text = "Stimuli";
            this.tabStimuli.UseVisualStyleBackColor = true;
            // 
            // listStimuli
            // 
            this.listStimuli.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listStimuli.Location = new System.Drawing.Point(0, 0);
            this.listStimuli.Name = "listStimuli";
            this.listStimuli.SelectedIndex = -1;
            this.listStimuli.SelectedItem = null;
            this.listStimuli.Size = new System.Drawing.Size(429, 610);
            this.listStimuli.TabIndex = 6;
            this.listStimuli.AddItem += new System.EventHandler(this.listStimuli_AddItem);
            this.listStimuli.DeleteItem += new System.EventHandler(this.listStimuli_DeleteItem);
            this.listStimuli.CopyItem += new System.EventHandler(this.listStimuli_CopyItem);
            this.listStimuli.ViewItem += new System.EventHandler(this.listStimuli_ViewItem);
            this.listStimuli.ActivateItem += new System.EventHandler(this.listStimuli_ActivateItem);
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.pBodyDiagrams);
            this.tabGeneral.Controls.Add(this.grSpinalCord);
            this.tabGeneral.Controls.Add(this.grBody);
            this.tabGeneral.Controls.Add(this.eModelDescription);
            this.tabGeneral.Controls.Add(this.eModelName);
            this.tabGeneral.Controls.Add(this.lModelDescription);
            this.tabGeneral.Controls.Add(this.lModelName);
            this.tabGeneral.Location = new System.Drawing.Point(4, 24);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(429, 610);
            this.tabGeneral.TabIndex = 4;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // pBodyDiagrams
            // 
            this.pBodyDiagrams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBodyDiagrams.AutoScroll = true;
            this.pBodyDiagrams.Controls.Add(this.picRostroCaudal);
            this.pBodyDiagrams.Controls.Add(this.picCrossSection);
            this.pBodyDiagrams.Location = new System.Drawing.Point(192, 77);
            this.pBodyDiagrams.Name = "pBodyDiagrams";
            this.pBodyDiagrams.Size = new System.Drawing.Size(237, 316);
            this.pBodyDiagrams.TabIndex = 15;
            this.pBodyDiagrams.SizeChanged += new System.EventHandler(this.pBodyDiagrams_SizeChanged);
            // 
            // picRostroCaudal
            // 
            this.picRostroCaudal.Image = ((System.Drawing.Image)(resources.GetObject("picRostroCaudal.Image")));
            this.picRostroCaudal.Location = new System.Drawing.Point(3, 237);
            this.picRostroCaudal.Name = "picRostroCaudal";
            this.picRostroCaudal.Size = new System.Drawing.Size(231, 75);
            this.picRostroCaudal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRostroCaudal.TabIndex = 2;
            this.picRostroCaudal.TabStop = false;
            // 
            // picCrossSection
            // 
            this.picCrossSection.Image = ((System.Drawing.Image)(resources.GetObject("picCrossSection.Image")));
            this.picCrossSection.Location = new System.Drawing.Point(3, 3);
            this.picCrossSection.Name = "picCrossSection";
            this.picCrossSection.Size = new System.Drawing.Size(231, 228);
            this.picCrossSection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCrossSection.TabIndex = 12;
            this.picCrossSection.TabStop = false;
            // 
            // grSpinalCord
            // 
            this.grSpinalCord.Controls.Add(this.eSpinalBodyPosition);
            this.grSpinalCord.Controls.Add(this.lSpinalBodyPosition);
            this.grSpinalCord.Controls.Add(this.eSpinalRostraoCaudal);
            this.grSpinalCord.Controls.Add(this.lSpinalRostroCaudal);
            this.grSpinalCord.Controls.Add(this.eSpinalMedialLateral);
            this.grSpinalCord.Controls.Add(this.lSpinalMedialLateral);
            this.grSpinalCord.Controls.Add(this.eSpinalDorsalVentral);
            this.grSpinalCord.Controls.Add(this.lSpinalDorsalVentral);
            this.grSpinalCord.Location = new System.Drawing.Point(8, 183);
            this.grSpinalCord.Name = "grSpinalCord";
            this.grSpinalCord.Size = new System.Drawing.Size(178, 140);
            this.grSpinalCord.TabIndex = 14;
            this.grSpinalCord.TabStop = false;
            this.grSpinalCord.Text = "Spinal Cord";
            // 
            // eSpinalBodyPosition
            // 
            this.eSpinalBodyPosition.DecimalPlaces = 1;
            this.eSpinalBodyPosition.Location = new System.Drawing.Point(98, 110);
            this.eSpinalBodyPosition.Name = "eSpinalBodyPosition";
            this.eSpinalBodyPosition.Size = new System.Drawing.Size(63, 23);
            this.eSpinalBodyPosition.TabIndex = 18;
            this.eSpinalBodyPosition.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lSpinalBodyPosition
            // 
            this.lSpinalBodyPosition.AutoSize = true;
            this.lSpinalBodyPosition.Location = new System.Drawing.Point(6, 110);
            this.lSpinalBodyPosition.Name = "lSpinalBodyPosition";
            this.lSpinalBodyPosition.Size = new System.Drawing.Size(80, 15);
            this.lSpinalBodyPosition.TabIndex = 17;
            this.lSpinalBodyPosition.Text = "Body Position";
            // 
            // eSpinalRostraoCaudal
            // 
            this.eSpinalRostraoCaudal.DecimalPlaces = 1;
            this.eSpinalRostraoCaudal.Location = new System.Drawing.Point(98, 82);
            this.eSpinalRostraoCaudal.Name = "eSpinalRostraoCaudal";
            this.eSpinalRostraoCaudal.Size = new System.Drawing.Size(63, 23);
            this.eSpinalRostraoCaudal.TabIndex = 16;
            this.eSpinalRostraoCaudal.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lSpinalRostroCaudal
            // 
            this.lSpinalRostroCaudal.AutoSize = true;
            this.lSpinalRostroCaudal.Location = new System.Drawing.Point(6, 84);
            this.lSpinalRostroCaudal.Name = "lSpinalRostroCaudal";
            this.lSpinalRostroCaudal.Size = new System.Drawing.Size(83, 15);
            this.lSpinalRostroCaudal.TabIndex = 15;
            this.lSpinalRostroCaudal.Text = "Rostro-Caudal";
            // 
            // eSpinalMedialLateral
            // 
            this.eSpinalMedialLateral.DecimalPlaces = 1;
            this.eSpinalMedialLateral.Location = new System.Drawing.Point(98, 55);
            this.eSpinalMedialLateral.Name = "eSpinalMedialLateral";
            this.eSpinalMedialLateral.Size = new System.Drawing.Size(63, 23);
            this.eSpinalMedialLateral.TabIndex = 14;
            this.eSpinalMedialLateral.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lSpinalMedialLateral
            // 
            this.lSpinalMedialLateral.AutoSize = true;
            this.lSpinalMedialLateral.Location = new System.Drawing.Point(6, 55);
            this.lSpinalMedialLateral.Name = "lSpinalMedialLateral";
            this.lSpinalMedialLateral.Size = new System.Drawing.Size(83, 15);
            this.lSpinalMedialLateral.TabIndex = 13;
            this.lSpinalMedialLateral.Text = "Medial-Lateral";
            // 
            // eSpinalDorsalVentral
            // 
            this.eSpinalDorsalVentral.DecimalPlaces = 1;
            this.eSpinalDorsalVentral.Location = new System.Drawing.Point(98, 27);
            this.eSpinalDorsalVentral.Name = "eSpinalDorsalVentral";
            this.eSpinalDorsalVentral.Size = new System.Drawing.Size(63, 23);
            this.eSpinalDorsalVentral.TabIndex = 12;
            this.eSpinalDorsalVentral.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lSpinalDorsalVentral
            // 
            this.lSpinalDorsalVentral.AutoSize = true;
            this.lSpinalDorsalVentral.Location = new System.Drawing.Point(6, 29);
            this.lSpinalDorsalVentral.Name = "lSpinalDorsalVentral";
            this.lSpinalDorsalVentral.Size = new System.Drawing.Size(81, 15);
            this.lSpinalDorsalVentral.TabIndex = 11;
            this.lSpinalDorsalVentral.Text = "Dorsal-Ventral";
            // 
            // grBody
            // 
            this.grBody.Controls.Add(this.eBodyMedialLateral);
            this.grBody.Controls.Add(this.lBodyMedialLateral);
            this.grBody.Controls.Add(this.eBodyDorsalVentral);
            this.grBody.Controls.Add(this.lBodyDorsalVentral);
            this.grBody.Location = new System.Drawing.Point(8, 77);
            this.grBody.Name = "grBody";
            this.grBody.Size = new System.Drawing.Size(178, 100);
            this.grBody.TabIndex = 13;
            this.grBody.TabStop = false;
            this.grBody.Text = "Body";
            // 
            // eBodyMedialLateral
            // 
            this.eBodyMedialLateral.DecimalPlaces = 1;
            this.eBodyMedialLateral.Location = new System.Drawing.Point(98, 55);
            this.eBodyMedialLateral.Name = "eBodyMedialLateral";
            this.eBodyMedialLateral.Size = new System.Drawing.Size(63, 23);
            this.eBodyMedialLateral.TabIndex = 14;
            this.eBodyMedialLateral.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lBodyMedialLateral
            // 
            this.lBodyMedialLateral.AutoSize = true;
            this.lBodyMedialLateral.Location = new System.Drawing.Point(6, 55);
            this.lBodyMedialLateral.Name = "lBodyMedialLateral";
            this.lBodyMedialLateral.Size = new System.Drawing.Size(83, 15);
            this.lBodyMedialLateral.TabIndex = 13;
            this.lBodyMedialLateral.Text = "Medial-Lateral";
            // 
            // eBodyDorsalVentral
            // 
            this.eBodyDorsalVentral.DecimalPlaces = 1;
            this.eBodyDorsalVentral.Location = new System.Drawing.Point(98, 27);
            this.eBodyDorsalVentral.Name = "eBodyDorsalVentral";
            this.eBodyDorsalVentral.Size = new System.Drawing.Size(63, 23);
            this.eBodyDorsalVentral.TabIndex = 12;
            this.eBodyDorsalVentral.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lBodyDorsalVentral
            // 
            this.lBodyDorsalVentral.AutoSize = true;
            this.lBodyDorsalVentral.Location = new System.Drawing.Point(6, 29);
            this.lBodyDorsalVentral.Name = "lBodyDorsalVentral";
            this.lBodyDorsalVentral.Size = new System.Drawing.Size(81, 15);
            this.lBodyDorsalVentral.TabIndex = 11;
            this.lBodyDorsalVentral.Text = "Dorsal-Ventral";
            // 
            // eModelDescription
            // 
            this.eModelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eModelDescription.Location = new System.Drawing.Point(131, 30);
            this.eModelDescription.Multiline = true;
            this.eModelDescription.Name = "eModelDescription";
            this.eModelDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eModelDescription.Size = new System.Drawing.Size(289, 41);
            this.eModelDescription.TabIndex = 7;
            // 
            // eModelName
            // 
            this.eModelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eModelName.Location = new System.Drawing.Point(131, 3);
            this.eModelName.Name = "eModelName";
            this.eModelName.Size = new System.Drawing.Size(289, 23);
            this.eModelName.TabIndex = 6;
            // 
            // lModelDescription
            // 
            this.lModelDescription.AutoSize = true;
            this.lModelDescription.Location = new System.Drawing.Point(8, 35);
            this.lModelDescription.Name = "lModelDescription";
            this.lModelDescription.Size = new System.Drawing.Size(67, 15);
            this.lModelDescription.TabIndex = 1;
            this.lModelDescription.Text = "Description";
            // 
            // lModelName
            // 
            this.lModelName.AutoSize = true;
            this.lModelName.Location = new System.Drawing.Point(8, 8);
            this.lModelName.Name = "lModelName";
            this.lModelName.Size = new System.Drawing.Size(76, 15);
            this.lModelName.TabIndex = 0;
            this.lModelName.Text = "Model Name";
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.linkLoadParam);
            this.pMain.Controls.Add(this.lParameters);
            this.pMain.Controls.Add(this.linkSaveParam);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(437, 34);
            this.pMain.TabIndex = 4;
            // 
            // linkLoadParam
            // 
            this.linkLoadParam.AutoSize = true;
            this.linkLoadParam.Location = new System.Drawing.Point(247, 10);
            this.linkLoadParam.Name = "linkLoadParam";
            this.linkLoadParam.Size = new System.Drawing.Size(95, 15);
            this.linkLoadParam.TabIndex = 4;
            this.linkLoadParam.TabStop = true;
            this.linkLoadParam.Text = "Load Parameters";
            this.linkLoadParam.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLoadParam_LinkClicked);
            // 
            // lParameters
            // 
            this.lParameters.AutoSize = true;
            this.lParameters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lParameters.Location = new System.Drawing.Point(0, 10);
            this.lParameters.Name = "lParameters";
            this.lParameters.Size = new System.Drawing.Size(109, 15);
            this.lParameters.TabIndex = 0;
            this.lParameters.Text = "Model Parameters";
            // 
            // linkSaveParam
            // 
            this.linkSaveParam.AutoSize = true;
            this.linkSaveParam.Location = new System.Drawing.Point(135, 10);
            this.linkSaveParam.Name = "linkSaveParam";
            this.linkSaveParam.Size = new System.Drawing.Size(93, 15);
            this.linkSaveParam.TabIndex = 3;
            this.linkSaveParam.TabStop = true;
            this.linkSaveParam.Text = "Save Parameters";
            this.linkSaveParam.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveParam_LinkClicked);
            // 
            // tabOutputs
            // 
            this.tabOutputs.Controls.Add(this.tabPlot);
            this.tabOutputs.Controls.Add(this.tab2DModel);
            this.tabOutputs.Controls.Add(this.tab3DModel);
            this.tabOutputs.Controls.Add(this.tabHTMLPlot);
            this.tabOutputs.Controls.Add(this.tabAnimation);
            this.tabOutputs.Controls.Add(this.tabTextOutput);
            this.tabOutputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOutputs.Location = new System.Drawing.Point(0, 88);
            this.tabOutputs.Name = "tabOutputs";
            this.tabOutputs.SelectedIndex = 0;
            this.tabOutputs.Size = new System.Drawing.Size(630, 584);
            this.tabOutputs.TabIndex = 1;
            this.tabOutputs.SelectedIndexChanged += new System.EventHandler(this.tabOutputs_SelectedIndexChanged);
            // 
            // tabPlot
            // 
            this.tabPlot.Controls.Add(this.splitWindows);
            this.tabPlot.Controls.Add(this.pPlotWindows);
            this.tabPlot.Location = new System.Drawing.Point(4, 24);
            this.tabPlot.Name = "tabPlot";
            this.tabPlot.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlot.Size = new System.Drawing.Size(622, 556);
            this.tabPlot.TabIndex = 4;
            this.tabPlot.Text = "Plots";
            this.tabPlot.UseVisualStyleBackColor = true;
            // 
            // splitWindows
            // 
            this.splitWindows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitWindows.Location = new System.Drawing.Point(3, 94);
            this.splitWindows.Name = "splitWindows";
            // 
            // splitWindows.Panel1
            // 
            this.splitWindows.Panel1.Controls.Add(this.pictureBoxLeft);
            // 
            // splitWindows.Panel2
            // 
            this.splitWindows.Panel2.Controls.Add(this.pictureBoxRight);
            this.splitWindows.Size = new System.Drawing.Size(616, 459);
            this.splitWindows.SplitterDistance = 287;
            this.splitWindows.TabIndex = 0;
            this.splitWindows.DoubleClick += new System.EventHandler(this.splitWindows_DoubleClick);
            // 
            // pictureBoxLeft
            // 
            this.pictureBoxLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLeft.Image = null;
            this.pictureBoxLeft.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.Size = new System.Drawing.Size(285, 457);
            this.pictureBoxLeft.TabIndex = 0;
            // 
            // pictureBoxRight
            // 
            this.pictureBoxRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxRight.Image = null;
            this.pictureBoxRight.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.Size = new System.Drawing.Size(323, 457);
            this.pictureBoxRight.TabIndex = 0;
            // 
            // pPlotWindows
            // 
            this.pPlotWindows.BackColor = System.Drawing.SystemColors.Control;
            this.pPlotWindows.Controls.Add(this.lCellNumber);
            this.pPlotWindows.Controls.Add(this.cbSample);
            this.pPlotWindows.Controls.Add(this.enSample);
            this.pPlotWindows.Controls.Add(this.lPlotWindowsPool);
            this.pPlotWindows.Controls.Add(this.ddCellPoolsWindows);
            this.pPlotWindows.Controls.Add(this.lPlotWindowsStart);
            this.pPlotWindows.Controls.Add(this.label7);
            this.pPlotWindows.Controls.Add(this.lPlotWindowsEnd);
            this.pPlotWindows.Controls.Add(this.ePlotWindowsEnd);
            this.pPlotWindows.Controls.Add(this.label9);
            this.pPlotWindows.Controls.Add(this.ePlotWindowsStart);
            this.pPlotWindows.Controls.Add(this.lPlotWindowsPlot);
            this.pPlotWindows.Controls.Add(this.btnPlotWindows);
            this.pPlotWindows.Controls.Add(this.ddPlotWindows);
            this.pPlotWindows.Dock = System.Windows.Forms.DockStyle.Top;
            this.pPlotWindows.Location = new System.Drawing.Point(3, 3);
            this.pPlotWindows.Name = "pPlotWindows";
            this.pPlotWindows.Size = new System.Drawing.Size(616, 91);
            this.pPlotWindows.TabIndex = 5;
            // 
            // lCellNumber
            // 
            this.lCellNumber.AutoSize = true;
            this.lCellNumber.Location = new System.Drawing.Point(217, 64);
            this.lCellNumber.Name = "lCellNumber";
            this.lCellNumber.Size = new System.Drawing.Size(74, 15);
            this.lCellNumber.TabIndex = 33;
            this.lCellNumber.Text = "Cell Number";
            this.lCellNumber.Visible = false;
            // 
            // cbSample
            // 
            this.cbSample.AutoSize = true;
            this.cbSample.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSample.Location = new System.Drawing.Point(217, 62);
            this.cbSample.Name = "cbSample";
            this.cbSample.Size = new System.Drawing.Size(65, 19);
            this.cbSample.TabIndex = 32;
            this.cbSample.Text = "Sample";
            this.cbSample.UseVisualStyleBackColor = true;
            this.cbSample.CheckedChanged += new System.EventHandler(this.cbSample_CheckedChanged);
            // 
            // enSample
            // 
            this.enSample.Location = new System.Drawing.Point(303, 60);
            this.enSample.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.enSample.Name = "enSample";
            this.enSample.Size = new System.Drawing.Size(56, 23);
            this.enSample.TabIndex = 31;
            this.enSample.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.enSample.Visible = false;
            // 
            // lPlotWindowsPool
            // 
            this.lPlotWindowsPool.AutoSize = true;
            this.lPlotWindowsPool.Location = new System.Drawing.Point(217, 8);
            this.lPlotWindowsPool.Name = "lPlotWindowsPool";
            this.lPlotWindowsPool.Size = new System.Drawing.Size(31, 15);
            this.lPlotWindowsPool.TabIndex = 26;
            this.lPlotWindowsPool.Text = "Pool";
            // 
            // ddCellPoolsWindows
            // 
            this.ddCellPoolsWindows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCellPoolsWindows.FormattingEnabled = true;
            this.ddCellPoolsWindows.Location = new System.Drawing.Point(303, 3);
            this.ddCellPoolsWindows.Name = "ddCellPoolsWindows";
            this.ddCellPoolsWindows.Size = new System.Drawing.Size(137, 23);
            this.ddCellPoolsWindows.TabIndex = 27;
            this.ddCellPoolsWindows.SelectedIndexChanged += new System.EventHandler(this.ddCellPoolsWindows_SelectedIndexChanged);
            // 
            // lPlotWindowsStart
            // 
            this.lPlotWindowsStart.AutoSize = true;
            this.lPlotWindowsStart.Location = new System.Drawing.Point(12, 8);
            this.lPlotWindowsStart.Name = "lPlotWindowsStart";
            this.lPlotWindowsStart.Size = new System.Drawing.Size(55, 15);
            this.lPlotWindowsStart.TabIndex = 11;
            this.lPlotWindowsStart.Text = "Plot Start";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "(ms)";
            // 
            // lPlotWindowsEnd
            // 
            this.lPlotWindowsEnd.AutoSize = true;
            this.lPlotWindowsEnd.Location = new System.Drawing.Point(12, 31);
            this.lPlotWindowsEnd.Name = "lPlotWindowsEnd";
            this.lPlotWindowsEnd.Size = new System.Drawing.Size(51, 15);
            this.lPlotWindowsEnd.TabIndex = 14;
            this.lPlotWindowsEnd.Text = "Plot End";
            // 
            // ePlotWindowsEnd
            // 
            this.ePlotWindowsEnd.Location = new System.Drawing.Point(74, 28);
            this.ePlotWindowsEnd.Name = "ePlotWindowsEnd";
            this.ePlotWindowsEnd.Size = new System.Drawing.Size(100, 23);
            this.ePlotWindowsEnd.TabIndex = 25;
            this.ePlotWindowsEnd.Text = "3000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(180, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "(ms)";
            // 
            // ePlotWindowsStart
            // 
            this.ePlotWindowsStart.Location = new System.Drawing.Point(74, 3);
            this.ePlotWindowsStart.Name = "ePlotWindowsStart";
            this.ePlotWindowsStart.Size = new System.Drawing.Size(100, 23);
            this.ePlotWindowsStart.TabIndex = 24;
            this.ePlotWindowsStart.Text = "0";
            // 
            // lPlotWindowsPlot
            // 
            this.lPlotWindowsPlot.AutoSize = true;
            this.lPlotWindowsPlot.Location = new System.Drawing.Point(217, 35);
            this.lPlotWindowsPlot.Name = "lPlotWindowsPlot";
            this.lPlotWindowsPlot.Size = new System.Drawing.Size(28, 15);
            this.lPlotWindowsPlot.TabIndex = 19;
            this.lPlotWindowsPlot.Text = "Plot";
            // 
            // btnPlotWindows
            // 
            this.btnPlotWindows.Location = new System.Drawing.Point(365, 60);
            this.btnPlotWindows.Name = "btnPlotWindows";
            this.btnPlotWindows.Size = new System.Drawing.Size(75, 23);
            this.btnPlotWindows.TabIndex = 30;
            this.btnPlotWindows.Text = "Plot";
            this.btnPlotWindows.UseVisualStyleBackColor = true;
            this.btnPlotWindows.Click += new System.EventHandler(this.btnPlotWindows_Click);
            // 
            // ddPlotWindows
            // 
            this.ddPlotWindows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotWindows.FormattingEnabled = true;
            this.ddPlotWindows.Items.AddRange(new object[] {
            "Memb. Potentials",
            "Gap Currents",
            "Syn Currents",
            "Stimulus",
            "Full Dynamics"});
            this.ddPlotWindows.Location = new System.Drawing.Point(303, 31);
            this.ddPlotWindows.Name = "ddPlotWindows";
            this.ddPlotWindows.Size = new System.Drawing.Size(137, 23);
            this.ddPlotWindows.TabIndex = 28;
            this.ddPlotWindows.SelectedIndexChanged += new System.EventHandler(this.ddPlotWindows_SelectedIndexChanged);
            // 
            // tab2DModel
            // 
            this.tab2DModel.Controls.Add(this.webView2DModel);
            this.tab2DModel.Controls.Add(this.p2DModel);
            this.tab2DModel.Location = new System.Drawing.Point(4, 24);
            this.tab2DModel.Name = "tab2DModel";
            this.tab2DModel.Size = new System.Drawing.Size(622, 556);
            this.tab2DModel.TabIndex = 2;
            this.tab2DModel.Text = "2D Model";
            this.tab2DModel.UseVisualStyleBackColor = true;
            // 
            // webView2DModel
            // 
            this.webView2DModel.AllowExternalDrop = true;
            this.webView2DModel.CreationProperties = null;
            this.webView2DModel.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2DModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView2DModel.Location = new System.Drawing.Point(0, 30);
            this.webView2DModel.Name = "webView2DModel";
            this.webView2DModel.Size = new System.Drawing.Size(622, 526);
            this.webView2DModel.TabIndex = 1;
            this.webView2DModel.ZoomFactor = 1D;
            this.webView2DModel.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView2DModel_CoreWebView2InitializationCompleted);
            // 
            // p2DModel
            // 
            this.p2DModel.BackColor = System.Drawing.SystemColors.Control;
            this.p2DModel.Controls.Add(this.label10);
            this.p2DModel.Controls.Add(this.btnGenerate2DModel);
            this.p2DModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.p2DModel.Location = new System.Drawing.Point(0, 0);
            this.p2DModel.Name = "p2DModel";
            this.p2DModel.Size = new System.Drawing.Size(622, 30);
            this.p2DModel.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(137, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(294, 15);
            this.label10.TabIndex = 23;
            this.label10.Text = "Generated for proof of concept - requires optimization";
            // 
            // btnGenerate2DModel
            // 
            this.btnGenerate2DModel.AutoSize = true;
            this.btnGenerate2DModel.Location = new System.Drawing.Point(3, 3);
            this.btnGenerate2DModel.Name = "btnGenerate2DModel";
            this.btnGenerate2DModel.Size = new System.Drawing.Size(118, 25);
            this.btnGenerate2DModel.TabIndex = 22;
            this.btnGenerate2DModel.Text = "Generate 2D Model";
            this.btnGenerate2DModel.UseVisualStyleBackColor = true;
            this.btnGenerate2DModel.Click += new System.EventHandler(this.btnGenerate2DModel_Click);
            // 
            // tab3DModel
            // 
            this.tab3DModel.Controls.Add(this.webView3DModel);
            this.tab3DModel.Controls.Add(this.p3DModel);
            this.tab3DModel.Location = new System.Drawing.Point(4, 24);
            this.tab3DModel.Name = "tab3DModel";
            this.tab3DModel.Padding = new System.Windows.Forms.Padding(3);
            this.tab3DModel.Size = new System.Drawing.Size(622, 556);
            this.tab3DModel.TabIndex = 0;
            this.tab3DModel.Text = "3D Model";
            this.tab3DModel.UseVisualStyleBackColor = true;
            // 
            // webView3DModel
            // 
            this.webView3DModel.AllowExternalDrop = true;
            this.webView3DModel.CreationProperties = null;
            this.webView3DModel.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView3DModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView3DModel.Location = new System.Drawing.Point(3, 33);
            this.webView3DModel.Name = "webView3DModel";
            this.webView3DModel.Size = new System.Drawing.Size(616, 520);
            this.webView3DModel.TabIndex = 0;
            this.webView3DModel.ZoomFactor = 1D;
            this.webView3DModel.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView3DModel_CoreWebView2InitializationCompleted);
            // 
            // p3DModel
            // 
            this.p3DModel.BackColor = System.Drawing.SystemColors.Control;
            this.p3DModel.Controls.Add(this.dd3DModelType);
            this.p3DModel.Controls.Add(this.label12);
            this.p3DModel.Controls.Add(this.btnGenerate3DModel);
            this.p3DModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.p3DModel.Location = new System.Drawing.Point(3, 3);
            this.p3DModel.Name = "p3DModel";
            this.p3DModel.Size = new System.Drawing.Size(616, 30);
            this.p3DModel.TabIndex = 3;
            // 
            // dd3DModelType
            // 
            this.dd3DModelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dd3DModelType.FormattingEnabled = true;
            this.dd3DModelType.Items.AddRange(new object[] {
            "Gap Jnc",
            "Chem Jnc",
            "Gap+Chem",
            "Gap/Chem"});
            this.dd3DModelType.Location = new System.Drawing.Point(127, 4);
            this.dd3DModelType.Name = "dd3DModelType";
            this.dd3DModelType.Size = new System.Drawing.Size(121, 23);
            this.dd3DModelType.TabIndex = 25;
            this.dd3DModelType.SelectedIndexChanged += new System.EventHandler(this.dd3DModelType_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(254, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(294, 15);
            this.label12.TabIndex = 24;
            this.label12.Text = "Generated for proof of concept - requires optimization";
            // 
            // btnGenerate3DModel
            // 
            this.btnGenerate3DModel.AutoSize = true;
            this.btnGenerate3DModel.Location = new System.Drawing.Point(3, 2);
            this.btnGenerate3DModel.Name = "btnGenerate3DModel";
            this.btnGenerate3DModel.Size = new System.Drawing.Size(118, 25);
            this.btnGenerate3DModel.TabIndex = 22;
            this.btnGenerate3DModel.Text = "Generate 3D Model";
            this.btnGenerate3DModel.UseVisualStyleBackColor = true;
            this.btnGenerate3DModel.Click += new System.EventHandler(this.btnGenerate3DModel_Click);
            // 
            // tabHTMLPlot
            // 
            this.tabHTMLPlot.Controls.Add(this.webViewPlot);
            this.tabHTMLPlot.Controls.Add(this.pPlot);
            this.tabHTMLPlot.Location = new System.Drawing.Point(4, 24);
            this.tabHTMLPlot.Name = "tabHTMLPlot";
            this.tabHTMLPlot.Padding = new System.Windows.Forms.Padding(3);
            this.tabHTMLPlot.Size = new System.Drawing.Size(622, 556);
            this.tabHTMLPlot.TabIndex = 1;
            this.tabHTMLPlot.Text = "HTML Plots";
            this.tabHTMLPlot.UseVisualStyleBackColor = true;
            // 
            // webViewPlot
            // 
            this.webViewPlot.AllowExternalDrop = true;
            this.webViewPlot.CreationProperties = null;
            this.webViewPlot.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewPlot.Location = new System.Drawing.Point(3, 108);
            this.webViewPlot.Name = "webViewPlot";
            this.webViewPlot.Size = new System.Drawing.Size(616, 445);
            this.webViewPlot.TabIndex = 1;
            this.webViewPlot.ZoomFactor = 1D;
            this.webViewPlot.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webViewPlot_CoreWebView2InitializationCompleted);
            // 
            // pPlot
            // 
            this.pPlot.BackColor = System.Drawing.SystemColors.Control;
            this.pPlot.Controls.Add(this.cbSampleHTML);
            this.pPlot.Controls.Add(this.nbSample);
            this.pPlot.Controls.Add(this.label13);
            this.pPlot.Controls.Add(this.lCellsPools);
            this.pPlot.Controls.Add(this.ddCellsPools);
            this.pPlot.Controls.Add(this.lPlotStart);
            this.pPlot.Controls.Add(this.lms3);
            this.pPlot.Controls.Add(this.lPlotEnd);
            this.pPlot.Controls.Add(this.ePlotEnd);
            this.pPlot.Controls.Add(this.lms4);
            this.pPlot.Controls.Add(this.ePlotStart);
            this.pPlot.Controls.Add(this.lGrouping);
            this.pPlot.Controls.Add(this.ddGrouping);
            this.pPlot.Controls.Add(this.lPlot);
            this.pPlot.Controls.Add(this.btnPlot);
            this.pPlot.Controls.Add(this.ddPlot);
            this.pPlot.Dock = System.Windows.Forms.DockStyle.Top;
            this.pPlot.Location = new System.Drawing.Point(3, 3);
            this.pPlot.Name = "pPlot";
            this.pPlot.Size = new System.Drawing.Size(616, 105);
            this.pPlot.TabIndex = 4;
            // 
            // cbSampleHTML
            // 
            this.cbSampleHTML.AutoSize = true;
            this.cbSampleHTML.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSampleHTML.Location = new System.Drawing.Point(455, 4);
            this.cbSampleHTML.Name = "cbSampleHTML";
            this.cbSampleHTML.Size = new System.Drawing.Size(65, 19);
            this.cbSampleHTML.TabIndex = 35;
            this.cbSampleHTML.Text = "Sample";
            this.cbSampleHTML.UseVisualStyleBackColor = true;
            // 
            // nbSample
            // 
            this.nbSample.Location = new System.Drawing.Point(526, 3);
            this.nbSample.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbSample.Name = "nbSample";
            this.nbSample.Size = new System.Drawing.Size(56, 23);
            this.nbSample.TabIndex = 34;
            this.nbSample.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbSample.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(294, 15);
            this.label13.TabIndex = 24;
            this.label13.Text = "Generated for proof of concept - requires optimization";
            // 
            // lCellsPools
            // 
            this.lCellsPools.AutoSize = true;
            this.lCellsPools.Location = new System.Drawing.Point(217, 34);
            this.lCellsPools.Name = "lCellsPools";
            this.lCellsPools.Size = new System.Drawing.Size(56, 15);
            this.lCellsPools.TabIndex = 26;
            this.lCellsPools.Text = "Cell/Pool";
            // 
            // ddCellsPools
            // 
            this.ddCellsPools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCellsPools.FormattingEnabled = true;
            this.ddCellsPools.Location = new System.Drawing.Point(303, 29);
            this.ddCellsPools.Name = "ddCellsPools";
            this.ddCellsPools.Size = new System.Drawing.Size(137, 23);
            this.ddCellsPools.TabIndex = 27;
            this.ddCellsPools.SelectedIndexChanged += new System.EventHandler(this.ddCellsPools_SelectedIndexChanged);
            this.ddCellsPools.Enter += new System.EventHandler(this.ddCellsPools_Enter);
            // 
            // lPlotStart
            // 
            this.lPlotStart.AutoSize = true;
            this.lPlotStart.Location = new System.Drawing.Point(12, 8);
            this.lPlotStart.Name = "lPlotStart";
            this.lPlotStart.Size = new System.Drawing.Size(55, 15);
            this.lPlotStart.TabIndex = 11;
            this.lPlotStart.Text = "Plot Start";
            // 
            // lms3
            // 
            this.lms3.AutoSize = true;
            this.lms3.Location = new System.Drawing.Point(180, 8);
            this.lms3.Name = "lms3";
            this.lms3.Size = new System.Drawing.Size(31, 15);
            this.lms3.TabIndex = 13;
            this.lms3.Text = "(ms)";
            // 
            // lPlotEnd
            // 
            this.lPlotEnd.AutoSize = true;
            this.lPlotEnd.Location = new System.Drawing.Point(12, 31);
            this.lPlotEnd.Name = "lPlotEnd";
            this.lPlotEnd.Size = new System.Drawing.Size(51, 15);
            this.lPlotEnd.TabIndex = 14;
            this.lPlotEnd.Text = "Plot End";
            // 
            // ePlotEnd
            // 
            this.ePlotEnd.Location = new System.Drawing.Point(74, 28);
            this.ePlotEnd.Name = "ePlotEnd";
            this.ePlotEnd.Size = new System.Drawing.Size(100, 23);
            this.ePlotEnd.TabIndex = 25;
            this.ePlotEnd.Text = "3000";
            // 
            // lms4
            // 
            this.lms4.AutoSize = true;
            this.lms4.Location = new System.Drawing.Point(180, 37);
            this.lms4.Name = "lms4";
            this.lms4.Size = new System.Drawing.Size(31, 15);
            this.lms4.TabIndex = 16;
            this.lms4.Text = "(ms)";
            // 
            // ePlotStart
            // 
            this.ePlotStart.Location = new System.Drawing.Point(74, 3);
            this.ePlotStart.Name = "ePlotStart";
            this.ePlotStart.Size = new System.Drawing.Size(100, 23);
            this.ePlotStart.TabIndex = 24;
            this.ePlotStart.Text = "0";
            // 
            // lGrouping
            // 
            this.lGrouping.AutoSize = true;
            this.lGrouping.Location = new System.Drawing.Point(217, 8);
            this.lGrouping.Name = "lGrouping";
            this.lGrouping.Size = new System.Drawing.Size(57, 15);
            this.lGrouping.TabIndex = 17;
            this.lGrouping.Text = "Grouping";
            // 
            // ddGrouping
            // 
            this.ddGrouping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddGrouping.FormattingEnabled = true;
            this.ddGrouping.Items.AddRange(new object[] {
            "Full",
            "Individual Cell",
            "Cells in a Pool",
            "Individual Pool",
            "Pools on Opposite Sides"});
            this.ddGrouping.Location = new System.Drawing.Point(303, 3);
            this.ddGrouping.Name = "ddGrouping";
            this.ddGrouping.Size = new System.Drawing.Size(137, 23);
            this.ddGrouping.TabIndex = 26;
            this.ddGrouping.SelectedIndexChanged += new System.EventHandler(this.ddGrouping_SelectedIndexChanged);
            this.ddGrouping.Enter += new System.EventHandler(this.ddGrouping_Enter);
            // 
            // lPlot
            // 
            this.lPlot.AutoSize = true;
            this.lPlot.Location = new System.Drawing.Point(217, 60);
            this.lPlot.Name = "lPlot";
            this.lPlot.Size = new System.Drawing.Size(28, 15);
            this.lPlot.TabIndex = 19;
            this.lPlot.Text = "Plot";
            // 
            // btnPlot
            // 
            this.btnPlot.Enabled = false;
            this.btnPlot.Location = new System.Drawing.Point(507, 33);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(75, 23);
            this.btnPlot.TabIndex = 30;
            this.btnPlot.Text = "Plot";
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // ddPlot
            // 
            this.ddPlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlot.FormattingEnabled = true;
            this.ddPlot.Items.AddRange(new object[] {
            "Memb. Potentials",
            "Gap Currents",
            "Syn Currents",
            "Currents",
            "Stimuli",
            "Full Dynamics"});
            this.ddPlot.Location = new System.Drawing.Point(303, 57);
            this.ddPlot.Name = "ddPlot";
            this.ddPlot.Size = new System.Drawing.Size(137, 23);
            this.ddPlot.TabIndex = 28;
            this.ddPlot.SelectedIndexChanged += new System.EventHandler(this.ddPlot_SelectedIndexChanged);
            this.ddPlot.Enter += new System.EventHandler(this.ddPlot_Enter);
            // 
            // tabAnimation
            // 
            this.tabAnimation.Controls.Add(this.webViewAnimation);
            this.tabAnimation.Controls.Add(this.pAnimation);
            this.tabAnimation.Location = new System.Drawing.Point(4, 24);
            this.tabAnimation.Name = "tabAnimation";
            this.tabAnimation.Size = new System.Drawing.Size(622, 556);
            this.tabAnimation.TabIndex = 3;
            this.tabAnimation.Text = "Animation";
            this.tabAnimation.UseVisualStyleBackColor = true;
            // 
            // webViewAnimation
            // 
            this.webViewAnimation.AllowExternalDrop = true;
            this.webViewAnimation.CreationProperties = null;
            this.webViewAnimation.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewAnimation.Location = new System.Drawing.Point(0, 67);
            this.webViewAnimation.Name = "webViewAnimation";
            this.webViewAnimation.Size = new System.Drawing.Size(622, 489);
            this.webViewAnimation.TabIndex = 2;
            this.webViewAnimation.ZoomFactor = 1D;
            this.webViewAnimation.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webViewAnimation_CoreWebView2InitializationCompleted);
            // 
            // pAnimation
            // 
            this.pAnimation.BackColor = System.Drawing.SystemColors.Control;
            this.pAnimation.Controls.Add(this.linkSaveAnimation);
            this.pAnimation.Controls.Add(this.btnAnimate);
            this.pAnimation.Controls.Add(this.lAnimationStart);
            this.pAnimation.Controls.Add(this.lms5);
            this.pAnimation.Controls.Add(this.lAnimationEnd);
            this.pAnimation.Controls.Add(this.eAnimationEnd);
            this.pAnimation.Controls.Add(this.lms6);
            this.pAnimation.Controls.Add(this.eAnimationStart);
            this.pAnimation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pAnimation.Location = new System.Drawing.Point(0, 0);
            this.pAnimation.Name = "pAnimation";
            this.pAnimation.Size = new System.Drawing.Size(622, 67);
            this.pAnimation.TabIndex = 5;
            // 
            // linkSaveAnimation
            // 
            this.linkSaveAnimation.Enabled = false;
            this.linkSaveAnimation.Location = new System.Drawing.Point(345, 8);
            this.linkSaveAnimation.Name = "linkSaveAnimation";
            this.linkSaveAnimation.Size = new System.Drawing.Size(124, 15);
            this.linkSaveAnimation.TabIndex = 31;
            this.linkSaveAnimation.TabStop = true;
            this.linkSaveAnimation.Text = "Save Animation";
            this.linkSaveAnimation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveAnimation_LinkClicked);
            // 
            // btnAnimate
            // 
            this.btnAnimate.Enabled = false;
            this.btnAnimate.Location = new System.Drawing.Point(264, 4);
            this.btnAnimate.Name = "btnAnimate";
            this.btnAnimate.Size = new System.Drawing.Size(75, 23);
            this.btnAnimate.TabIndex = 29;
            this.btnAnimate.Text = "Animate";
            this.btnAnimate.UseVisualStyleBackColor = true;
            this.btnAnimate.Click += new System.EventHandler(this.btnAnimate_Click);
            // 
            // lAnimationStart
            // 
            this.lAnimationStart.AutoSize = true;
            this.lAnimationStart.Location = new System.Drawing.Point(12, 8);
            this.lAnimationStart.Name = "lAnimationStart";
            this.lAnimationStart.Size = new System.Drawing.Size(90, 15);
            this.lAnimationStart.TabIndex = 11;
            this.lAnimationStart.Text = "Animation Start";
            // 
            // lms5
            // 
            this.lms5.AutoSize = true;
            this.lms5.Location = new System.Drawing.Point(214, 8);
            this.lms5.Name = "lms5";
            this.lms5.Size = new System.Drawing.Size(31, 15);
            this.lms5.TabIndex = 13;
            this.lms5.Text = "(ms)";
            // 
            // lAnimationEnd
            // 
            this.lAnimationEnd.AutoSize = true;
            this.lAnimationEnd.Location = new System.Drawing.Point(12, 37);
            this.lAnimationEnd.Name = "lAnimationEnd";
            this.lAnimationEnd.Size = new System.Drawing.Size(86, 15);
            this.lAnimationEnd.TabIndex = 14;
            this.lAnimationEnd.Text = "Animation End";
            // 
            // eAnimationEnd
            // 
            this.eAnimationEnd.Location = new System.Drawing.Point(108, 34);
            this.eAnimationEnd.Name = "eAnimationEnd";
            this.eAnimationEnd.Size = new System.Drawing.Size(100, 23);
            this.eAnimationEnd.TabIndex = 25;
            this.eAnimationEnd.Text = "3000";
            // 
            // lms6
            // 
            this.lms6.AutoSize = true;
            this.lms6.Location = new System.Drawing.Point(214, 37);
            this.lms6.Name = "lms6";
            this.lms6.Size = new System.Drawing.Size(31, 15);
            this.lms6.TabIndex = 16;
            this.lms6.Text = "(ms)";
            // 
            // eAnimationStart
            // 
            this.eAnimationStart.Location = new System.Drawing.Point(108, 5);
            this.eAnimationStart.Name = "eAnimationStart";
            this.eAnimationStart.Size = new System.Drawing.Size(100, 23);
            this.eAnimationStart.TabIndex = 24;
            this.eAnimationStart.Text = "0";
            // 
            // tabTextOutput
            // 
            this.tabTextOutput.Controls.Add(this.eModelSummary);
            this.tabTextOutput.Location = new System.Drawing.Point(4, 24);
            this.tabTextOutput.Name = "tabTextOutput";
            this.tabTextOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabTextOutput.Size = new System.Drawing.Size(622, 556);
            this.tabTextOutput.TabIndex = 5;
            this.tabTextOutput.Text = "Text Output";
            this.tabTextOutput.UseVisualStyleBackColor = true;
            // 
            // eModelSummary
            // 
            this.eModelSummary.AutoWordSelection = true;
            this.eModelSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eModelSummary.Location = new System.Drawing.Point(3, 3);
            this.eModelSummary.Name = "eModelSummary";
            this.eModelSummary.Size = new System.Drawing.Size(616, 550);
            this.eModelSummary.TabIndex = 0;
            this.eModelSummary.Text = "";
            // 
            // pParamBottom
            // 
            this.pParamBottom.Controls.Add(this.lStimulus);
            this.pParamBottom.Controls.Add(this.ddStimulusMode);
            this.pParamBottom.Controls.Add(this.linkSaveRun);
            this.pParamBottom.Controls.Add(this.progressBarRun);
            this.pParamBottom.Controls.Add(this.eSkip);
            this.pParamBottom.Controls.Add(this.eTimeEnd);
            this.pParamBottom.Controls.Add(this.btnRun);
            this.pParamBottom.Controls.Add(this.lms2);
            this.pParamBottom.Controls.Add(this.lSkip);
            this.pParamBottom.Controls.Add(this.lTimeEnd);
            this.pParamBottom.Controls.Add(this.lRunParameters);
            this.pParamBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.pParamBottom.Location = new System.Drawing.Point(0, 0);
            this.pParamBottom.Name = "pParamBottom";
            this.pParamBottom.Size = new System.Drawing.Size(630, 88);
            this.pParamBottom.TabIndex = 1;
            // 
            // lStimulus
            // 
            this.lStimulus.AutoSize = true;
            this.lStimulus.Location = new System.Drawing.Point(372, 34);
            this.lStimulus.Name = "lStimulus";
            this.lStimulus.Size = new System.Drawing.Size(53, 15);
            this.lStimulus.TabIndex = 29;
            this.lStimulus.Text = "Stimulus";
            // 
            // ddStimulusMode
            // 
            this.ddStimulusMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddStimulusMode.FormattingEnabled = true;
            this.ddStimulusMode.Items.AddRange(new object[] {
            "Step",
            "Gaussian",
            "Ramp"});
            this.ddStimulusMode.Location = new System.Drawing.Point(431, 31);
            this.ddStimulusMode.Name = "ddStimulusMode";
            this.ddStimulusMode.Size = new System.Drawing.Size(103, 23);
            this.ddStimulusMode.TabIndex = 30;
            // 
            // linkSaveRun
            // 
            this.linkSaveRun.AutoSize = true;
            this.linkSaveRun.Location = new System.Drawing.Point(558, 10);
            this.linkSaveRun.Name = "linkSaveRun";
            this.linkSaveRun.Size = new System.Drawing.Size(55, 15);
            this.linkSaveRun.TabIndex = 27;
            this.linkSaveRun.TabStop = true;
            this.linkSaveRun.Text = "Save Run";
            this.linkSaveRun.Visible = false;
            this.linkSaveRun.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveRun_LinkClicked);
            // 
            // progressBarRun
            // 
            this.progressBarRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarRun.Location = new System.Drawing.Point(0, 65);
            this.progressBarRun.Name = "progressBarRun";
            this.progressBarRun.Size = new System.Drawing.Size(630, 23);
            this.progressBarRun.TabIndex = 26;
            this.progressBarRun.Visible = false;
            // 
            // eSkip
            // 
            this.eSkip.Location = new System.Drawing.Point(224, 32);
            this.eSkip.Name = "eSkip";
            this.eSkip.Size = new System.Drawing.Size(100, 23);
            this.eSkip.TabIndex = 23;
            this.eSkip.Text = "0";
            // 
            // eTimeEnd
            // 
            this.eTimeEnd.Location = new System.Drawing.Point(74, 32);
            this.eTimeEnd.Name = "eTimeEnd";
            this.eTimeEnd.Size = new System.Drawing.Size(100, 23);
            this.eTimeEnd.TabIndex = 22;
            this.eTimeEnd.Text = "1000";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(538, 31);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 9;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lms2
            // 
            this.lms2.AutoSize = true;
            this.lms2.Location = new System.Drawing.Point(335, 35);
            this.lms2.Name = "lms2";
            this.lms2.Size = new System.Drawing.Size(31, 15);
            this.lms2.TabIndex = 8;
            this.lms2.Text = "(ms)";
            // 
            // lSkip
            // 
            this.lSkip.AutoSize = true;
            this.lSkip.Location = new System.Drawing.Point(189, 35);
            this.lSkip.Name = "lSkip";
            this.lSkip.Size = new System.Drawing.Size(29, 15);
            this.lSkip.TabIndex = 6;
            this.lSkip.Text = "Skip";
            // 
            // lTimeEnd
            // 
            this.lTimeEnd.AutoSize = true;
            this.lTimeEnd.Location = new System.Drawing.Point(12, 35);
            this.lTimeEnd.Name = "lTimeEnd";
            this.lTimeEnd.Size = new System.Drawing.Size(56, 15);
            this.lTimeEnd.TabIndex = 3;
            this.lTimeEnd.Text = "Time End";
            // 
            // lRunParameters
            // 
            this.lRunParameters.AutoSize = true;
            this.lRunParameters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lRunParameters.Location = new System.Drawing.Point(0, 0);
            this.lRunParameters.Name = "lRunParameters";
            this.lRunParameters.Padding = new System.Windows.Forms.Padding(10);
            this.lRunParameters.Size = new System.Drawing.Size(116, 35);
            this.lRunParameters.TabIndex = 1;
            this.lRunParameters.Text = "Run Parameters";
            // 
            // timerRun
            // 
            this.timerRun.Interval = 1000;
            this.timerRun.Tick += new System.EventHandler(this.timerRun_Tick);
            // 
            // saveFileHTML
            // 
            this.saveFileHTML.Filter = "HTML files(*.html)|*.html";
            // 
            // saveFileJson
            // 
            this.saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // openFileJson
            // 
            this.openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // saveFileCSV
            // 
            this.saveFileCSV.Filter = "CSV files(*.csv)|*.csv";
            // 
            // saveFileText
            // 
            this.saveFileText.Filter = "Text files(*.txt)|*.txt";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 711);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pTop);
            this.Name = "MainForm";
            this.Text = "SiliFish";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tabParams.ResumeLayout(false);
            this.tabCellPools.ResumeLayout(false);
            this.splitCellPoolsAndJunctions.Panel1.ResumeLayout(false);
            this.splitCellPoolsAndJunctions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCellPoolsAndJunctions)).EndInit();
            this.splitCellPoolsAndJunctions.ResumeLayout(false);
            this.tabStimuli.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.pBodyDiagrams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRostroCaudal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrossSection)).EndInit();
            this.grSpinalCord.ResumeLayout(false);
            this.grSpinalCord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalBodyPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalRostraoCaudal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalMedialLateral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalDorsalVentral)).EndInit();
            this.grBody.ResumeLayout(false);
            this.grBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eBodyMedialLateral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eBodyDorsalVentral)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            this.tabOutputs.ResumeLayout(false);
            this.tabPlot.ResumeLayout(false);
            this.splitWindows.Panel1.ResumeLayout(false);
            this.splitWindows.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitWindows)).EndInit();
            this.splitWindows.ResumeLayout(false);
            this.pPlotWindows.ResumeLayout(false);
            this.pPlotWindows.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enSample)).EndInit();
            this.tab2DModel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView2DModel)).EndInit();
            this.p2DModel.ResumeLayout(false);
            this.p2DModel.PerformLayout();
            this.tab3DModel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView3DModel)).EndInit();
            this.p3DModel.ResumeLayout(false);
            this.p3DModel.PerformLayout();
            this.tabHTMLPlot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlot)).EndInit();
            this.pPlot.ResumeLayout(false);
            this.pPlot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbSample)).EndInit();
            this.tabAnimation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewAnimation)).EndInit();
            this.pAnimation.ResumeLayout(false);
            this.pAnimation.PerformLayout();
            this.tabTextOutput.ResumeLayout(false);
            this.pParamBottom.ResumeLayout(false);
            this.pParamBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RadioButton rbSingleCoil;
        private RadioButton rbDoubleCoil;
        private RadioButton rbBeatGlide;
        private Panel pTop;
        private SplitContainer splitMain;
        private Panel pParamBottom;
        private Label lParameters;
        private Label lRunParameters;
        private Button btnRun;
        private Label lms2;
        private Label lSkip;
        private Label lTimeEnd;
        private Button btnPlot;
        private ComboBox ddPlot;
        private Label lPlot;
        private Label lms4;
        private Label lPlotEnd;
        private Label lms3;
        private Label lPlotStart;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView3DModel;
        private TabControl tabOutputs;
        private TabPage tab3DModel;
        private TabPage tabHTMLPlot;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewPlot;
        private TextBox ePlotEnd;
        private TextBox ePlotStart;
        private TextBox eSkip;
        private TextBox eTimeEnd;
        private ProgressBar progressBarRun;
        private System.Windows.Forms.Timer timerRun;
        private LinkLabel linkSaveHTML;
        private SaveFileDialog saveFileHTML;
        private TabPage tab2DModel;
        private Button btnGenerate2DModel;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2DModel;
        private TabPage tabAnimation;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewAnimation;
        private Button btnAnimate;
        private Panel p2DModel;
        private Panel p3DModel;
        private Button btnGenerate3DModel;
        private Panel pPlot;
        private Panel pAnimation;
        private Label lAnimationStart;
        private Label lms5;
        private Label lAnimationEnd;
        private TextBox eAnimationEnd;
        private Label lms6;
        private TextBox eAnimationStart;
        private TabControl tabParams;
        private LinkLabel linkSaveParam;
        private Panel pMain;
        private LinkLabel linkLoadParam;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private LinkLabel linkSaveRun;
        private SaveFileDialog saveFileCSV;
        private ToolTip toolTip;
        private Label lCellsPools;
        private ComboBox ddCellsPools;
        private TabPage tabPlot;
        private SplitContainer splitWindows;
        private Panel pPlotWindows;
        private Label lPlotWindowsPool;
        private ComboBox ddCellPoolsWindows;
        private Label lPlotWindowsStart;
        private Label label7;
        private Label lPlotWindowsEnd;
        private TextBox ePlotWindowsEnd;
        private Label label9;
        private TextBox ePlotWindowsStart;
        private Label lPlotWindowsPlot;
        private Button btnPlotWindows;
        private ComboBox ddPlotWindows;
        private Label lGrouping;
        private ComboBox ddGrouping;
        private Label label10;
        private Label label12;
        private Label label13;
        private CheckBox cbSample;
        private NumericUpDown enSample;
        private Label lStimulus;
        private ComboBox ddStimulusMode;
        private RadioButton rbCustom;
        private TabPage tabCellPools;
        private TabPage tabStimuli;
        private ComboBox dd3DModelType;
        private LinkLabel linkClearModel;
        private TabPage tabGeneral;
        private PictureBox picRostroCaudal;
        private Label lModelDescription;
        private Label lModelName;
        private TabPage tabTextOutput;
        private RichTextBox eModelSummary;
        private SaveFileDialog saveFileText;
        private TextBox eModelDescription;
        private TextBox eModelName;
        private GroupBox grSpinalCord;
        private NumericUpDown eSpinalBodyPosition;
        private Label lSpinalBodyPosition;
        private NumericUpDown eSpinalRostraoCaudal;
        private Label lSpinalRostroCaudal;
        private NumericUpDown eSpinalMedialLateral;
        private Label lSpinalMedialLateral;
        private NumericUpDown eSpinalDorsalVentral;
        private Label lSpinalDorsalVentral;
        private GroupBox grBody;
        private NumericUpDown eBodyMedialLateral;
        private Label lBodyMedialLateral;
        private NumericUpDown eBodyDorsalVentral;
        private Label lBodyDorsalVentral;
        private PictureBox picCrossSection;
        private SplitContainer splitCellPoolsAndJunctions;
        private Label lCellNumber;
        private ListBoxControl listCellPool;
        private ListBoxControl listJunctions;
        private ListBoxControl listStimuli;
        private Panel pBodyDiagrams;
        private LinkLabel linkSaveAnimation;
        private Controls.PictureBoxControl pictureBoxLeft;
        private Controls.PictureBoxControl pictureBoxRight;
        private CheckBox cbSampleHTML;
        private NumericUpDown nbSample;
    }
}