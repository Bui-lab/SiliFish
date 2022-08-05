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
            this.linkBrowseToTempFolder = new System.Windows.Forms.LinkLabel();
            this.linkBrowseToOutputFolder = new System.Windows.Forms.LinkLabel();
            this.linkClearModel = new System.Windows.Forms.LinkLabel();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.linkSaveHTMLPlots = new System.Windows.Forms.LinkLabel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tabParams = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.pBodyDiagrams = new System.Windows.Forms.Panel();
            this.picRostroCaudal = new System.Windows.Forms.PictureBox();
            this.picCrossSection = new System.Windows.Forms.PictureBox();
            this.grSpinalCord = new System.Windows.Forms.GroupBox();
            this.eNumSomites = new System.Windows.Forms.NumericUpDown();
            this.lNoOfSomites = new System.Windows.Forms.Label();
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
            this.tabCellPools = new System.Windows.Forms.TabPage();
            this.splitCellPoolsAndConnections = new System.Windows.Forms.SplitContainer();
            this.listCellPool = new SiliFish.UI.Controls.ListBoxControl();
            this.listConnections = new SiliFish.UI.Controls.ListBoxControl();
            this.tabStimuli = new System.Windows.Forms.TabPage();
            this.listStimuli = new SiliFish.UI.Controls.ListBoxControl();
            this.pMain = new System.Windows.Forms.Panel();
            this.linkLoadParam = new System.Windows.Forms.LinkLabel();
            this.lParameters = new System.Windows.Forms.Label();
            this.linkSaveParam = new System.Windows.Forms.LinkLabel();
            this.tabOutputs = new System.Windows.Forms.TabControl();
            this.tabPlot = new System.Windows.Forms.TabPage();
            this.tabPlotSub = new System.Windows.Forms.TabControl();
            this.tPlotWindows = new System.Windows.Forms.TabPage();
            this.splitPlotWindows = new System.Windows.Forms.SplitContainer();
            this.pictureBoxLeft = new System.Windows.Forms.PictureBox();
            this.pictureBoxRight = new System.Windows.Forms.PictureBox();
            this.tPlotHTML = new System.Windows.Forms.TabPage();
            this.webViewPlot = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pPlot = new System.Windows.Forms.Panel();
            this.linkSavePlots = new System.Windows.Forms.LinkLabel();
            this.btnPlotHTML = new System.Windows.Forms.Button();
            this.ddPlotCellSelection = new System.Windows.Forms.ComboBox();
            this.lPlotCells = new System.Windows.Forms.Label();
            this.ePlotCellSelection = new System.Windows.Forms.NumericUpDown();
            this.ddPlotSomiteSelection = new System.Windows.Forms.ComboBox();
            this.lPlotSomites = new System.Windows.Forms.Label();
            this.ePlotEnd = new System.Windows.Forms.NumericUpDown();
            this.ePlotStart = new System.Windows.Forms.NumericUpDown();
            this.lGroupingWindows = new System.Windows.Forms.Label();
            this.ddGroupingWindows = new System.Windows.Forms.ComboBox();
            this.ePlotSomiteSelection = new System.Windows.Forms.NumericUpDown();
            this.lPlotPool = new System.Windows.Forms.Label();
            this.ddCellsPools = new System.Windows.Forms.ComboBox();
            this.lPlotStart = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lPlotEnd = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lPlotPlot = new System.Windows.Forms.Label();
            this.btnPlotWindows = new System.Windows.Forms.Button();
            this.ddPlot = new System.Windows.Forms.ComboBox();
            this.tab2DModel = new System.Windows.Forms.TabPage();
            this.webView2DModel = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.p2DModel = new System.Windows.Forms.Panel();
            this.linkSaveHTML2D = new System.Windows.Forms.LinkLabel();
            this.btnGenerate2DModel = new System.Windows.Forms.Button();
            this.tab3DModel = new System.Windows.Forms.TabPage();
            this.webView3DModel = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.p3DModel = new System.Windows.Forms.Panel();
            this.linkSaveHTML3D = new System.Windows.Forms.LinkLabel();
            this.dd3DModelType = new System.Windows.Forms.ComboBox();
            this.btnGenerate3DModel = new System.Windows.Forms.Button();
            this.tabAnimation = new System.Windows.Forms.TabPage();
            this.webViewAnimation = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pAnimation = new System.Windows.Forms.Panel();
            this.linkSaveAnimationCSV = new System.Windows.Forms.LinkLabel();
            this.lAnimationTime = new System.Windows.Forms.Label();
            this.eAnimationEnd = new System.Windows.Forms.NumericUpDown();
            this.eAnimationStart = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.linkSaveAnimationHTML = new System.Windows.Forms.LinkLabel();
            this.btnAnimate = new System.Windows.Forms.Button();
            this.lAnimationStart = new System.Windows.Forms.Label();
            this.lms5 = new System.Windows.Forms.Label();
            this.lAnimationEnd = new System.Windows.Forms.Label();
            this.lms6 = new System.Windows.Forms.Label();
            this.pParamBottom = new System.Windows.Forms.Panel();
            this.cbMultiple = new System.Windows.Forms.CheckBox();
            this.eRunNumber = new System.Windows.Forms.NumericUpDown();
            this.eSkip = new System.Windows.Forms.NumericUpDown();
            this.lSkip = new System.Windows.Forms.Label();
            this.edt = new System.Windows.Forms.NumericUpDown();
            this.eTimeEnd = new System.Windows.Forms.NumericUpDown();
            this.ldt = new System.Windows.Forms.Label();
            this.lRunTime = new System.Windows.Forms.Label();
            this.lStimulus = new System.Windows.Forms.Label();
            this.ddStimulusMode = new System.Windows.Forms.ComboBox();
            this.linkSaveRun = new System.Windows.Forms.LinkLabel();
            this.progressBarRun = new System.Windows.Forms.ProgressBar();
            this.btnRun = new System.Windows.Forms.Button();
            this.lTimeEnd = new System.Windows.Forms.Label();
            this.lRunParameters = new System.Windows.Forms.Label();
            this.timerRun = new System.Windows.Forms.Timer(this.components);
            this.saveFileHTML = new System.Windows.Forms.SaveFileDialog();
            this.saveFileJson = new System.Windows.Forms.SaveFileDialog();
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            this.saveFileCSV = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileText = new System.Windows.Forms.SaveFileDialog();
            this.saveFileImage = new System.Windows.Forms.SaveFileDialog();
            this.pTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tabParams.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.pBodyDiagrams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRostroCaudal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrossSection)).BeginInit();
            this.grSpinalCord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eNumSomites)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalBodyPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalRostraoCaudal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalMedialLateral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalDorsalVentral)).BeginInit();
            this.grBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eBodyMedialLateral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eBodyDorsalVentral)).BeginInit();
            this.tabCellPools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCellPoolsAndConnections)).BeginInit();
            this.splitCellPoolsAndConnections.Panel1.SuspendLayout();
            this.splitCellPoolsAndConnections.Panel2.SuspendLayout();
            this.splitCellPoolsAndConnections.SuspendLayout();
            this.tabStimuli.SuspendLayout();
            this.pMain.SuspendLayout();
            this.tabOutputs.SuspendLayout();
            this.tabPlot.SuspendLayout();
            this.tabPlotSub.SuspendLayout();
            this.tPlotWindows.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPlotWindows)).BeginInit();
            this.splitPlotWindows.Panel1.SuspendLayout();
            this.splitPlotWindows.Panel2.SuspendLayout();
            this.splitPlotWindows.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).BeginInit();
            this.tPlotHTML.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlot)).BeginInit();
            this.pPlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotCellSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotSomiteSelection)).BeginInit();
            this.tab2DModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView2DModel)).BeginInit();
            this.p2DModel.SuspendLayout();
            this.tab3DModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView3DModel)).BeginInit();
            this.p3DModel.SuspendLayout();
            this.tabAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewAnimation)).BeginInit();
            this.pAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationStart)).BeginInit();
            this.pParamBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eRunNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eTimeEnd)).BeginInit();
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
            this.pTop.Controls.Add(this.linkBrowseToTempFolder);
            this.pTop.Controls.Add(this.linkBrowseToOutputFolder);
            this.pTop.Controls.Add(this.linkClearModel);
            this.pTop.Controls.Add(this.rbCustom);
            this.pTop.Controls.Add(this.rbSingleCoil);
            this.pTop.Controls.Add(this.rbBeatGlide);
            this.pTop.Controls.Add(this.rbDoubleCoil);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1348, 39);
            this.pTop.TabIndex = 3;
            this.pTop.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pTop_MouseDoubleClick);
            // 
            // linkBrowseToTempFolder
            // 
            this.linkBrowseToTempFolder.AutoSize = true;
            this.linkBrowseToTempFolder.Location = new System.Drawing.Point(693, 14);
            this.linkBrowseToTempFolder.Name = "linkBrowseToTempFolder";
            this.linkBrowseToTempFolder.Size = new System.Drawing.Size(127, 15);
            this.linkBrowseToTempFolder.TabIndex = 7;
            this.linkBrowseToTempFolder.TabStop = true;
            this.linkBrowseToTempFolder.Text = "Browse to Temp Folder";
            this.toolTip.SetToolTip(this.linkBrowseToTempFolder, "The contents of the temp folder is cleared at exit.");
            this.linkBrowseToTempFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBrowseToTempFolder_LinkClicked);
            // 
            // linkBrowseToOutputFolder
            // 
            this.linkBrowseToOutputFolder.AutoSize = true;
            this.linkBrowseToOutputFolder.Location = new System.Drawing.Point(551, 14);
            this.linkBrowseToOutputFolder.Name = "linkBrowseToOutputFolder";
            this.linkBrowseToOutputFolder.Size = new System.Drawing.Size(136, 15);
            this.linkBrowseToOutputFolder.TabIndex = 6;
            this.linkBrowseToOutputFolder.TabStop = true;
            this.linkBrowseToOutputFolder.Text = "Browse to Output Folder";
            this.toolTip.SetToolTip(this.linkBrowseToOutputFolder, "If there is a problem in creating html files, they will be saved in this folder.\r" +
        "\n");
            this.linkBrowseToOutputFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBrowseToOutputFolder_LinkClicked);
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
            // linkSaveHTMLPlots
            // 
            this.linkSaveHTMLPlots.AutoSize = true;
            this.linkSaveHTMLPlots.Enabled = false;
            this.linkSaveHTMLPlots.Location = new System.Drawing.Point(586, 82);
            this.linkSaveHTMLPlots.Name = "linkSaveHTMLPlots";
            this.linkSaveHTMLPlots.Size = new System.Drawing.Size(66, 15);
            this.linkSaveHTMLPlots.TabIndex = 3;
            this.linkSaveHTMLPlots.TabStop = true;
            this.linkSaveHTMLPlots.Text = "Save HTML";
            this.linkSaveHTMLPlots.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveHTMLPlots_LinkClicked);
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
            this.splitMain.Size = new System.Drawing.Size(1348, 672);
            this.splitMain.SplitterDistance = 550;
            this.splitMain.TabIndex = 5;
            // 
            // tabParams
            // 
            this.tabParams.Controls.Add(this.tabGeneral);
            this.tabParams.Controls.Add(this.tabCellPools);
            this.tabParams.Controls.Add(this.tabStimuli);
            this.tabParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabParams.Location = new System.Drawing.Point(0, 34);
            this.tabParams.Name = "tabParams";
            this.tabParams.SelectedIndex = 0;
            this.tabParams.Size = new System.Drawing.Size(550, 638);
            this.tabParams.TabIndex = 2;
            this.tabParams.Tag = "";
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
            this.tabGeneral.Size = new System.Drawing.Size(542, 610);
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
            this.pBodyDiagrams.Size = new System.Drawing.Size(350, 316);
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
            this.grSpinalCord.Controls.Add(this.eNumSomites);
            this.grSpinalCord.Controls.Add(this.lNoOfSomites);
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
            this.grSpinalCord.Size = new System.Drawing.Size(178, 162);
            this.grSpinalCord.TabIndex = 14;
            this.grSpinalCord.TabStop = false;
            this.grSpinalCord.Text = "Spinal Cord";
            // 
            // eNumSomites
            // 
            this.eNumSomites.Location = new System.Drawing.Point(98, 16);
            this.eNumSomites.Name = "eNumSomites";
            this.eNumSomites.Size = new System.Drawing.Size(63, 23);
            this.eNumSomites.TabIndex = 11;
            this.eNumSomites.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lNoOfSomites
            // 
            this.lNoOfSomites.AutoSize = true;
            this.lNoOfSomites.Location = new System.Drawing.Point(6, 18);
            this.lNoOfSomites.Name = "lNoOfSomites";
            this.lNoOfSomites.Size = new System.Drawing.Size(73, 15);
            this.lNoOfSomites.TabIndex = 19;
            this.lNoOfSomites.Text = "# of Somites";
            // 
            // eSpinalBodyPosition
            // 
            this.eSpinalBodyPosition.DecimalPlaces = 1;
            this.eSpinalBodyPosition.Location = new System.Drawing.Point(98, 128);
            this.eSpinalBodyPosition.Name = "eSpinalBodyPosition";
            this.eSpinalBodyPosition.Size = new System.Drawing.Size(63, 23);
            this.eSpinalBodyPosition.TabIndex = 18;
            this.eSpinalBodyPosition.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.eSpinalBodyPosition.ValueChanged += new System.EventHandler(this.eSpinalBodyPosition_ValueChanged);
            this.eSpinalBodyPosition.Enter += new System.EventHandler(this.eSpinalBodyPosition_Enter);
            // 
            // lSpinalBodyPosition
            // 
            this.lSpinalBodyPosition.AutoSize = true;
            this.lSpinalBodyPosition.Location = new System.Drawing.Point(6, 130);
            this.lSpinalBodyPosition.Name = "lSpinalBodyPosition";
            this.lSpinalBodyPosition.Size = new System.Drawing.Size(80, 15);
            this.lSpinalBodyPosition.TabIndex = 17;
            this.lSpinalBodyPosition.Text = "Body Position";
            // 
            // eSpinalRostraoCaudal
            // 
            this.eSpinalRostraoCaudal.DecimalPlaces = 1;
            this.eSpinalRostraoCaudal.Location = new System.Drawing.Point(98, 100);
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
            this.lSpinalRostroCaudal.Location = new System.Drawing.Point(6, 102);
            this.lSpinalRostroCaudal.Name = "lSpinalRostroCaudal";
            this.lSpinalRostroCaudal.Size = new System.Drawing.Size(83, 15);
            this.lSpinalRostroCaudal.TabIndex = 15;
            this.lSpinalRostroCaudal.Text = "Rostro-Caudal";
            // 
            // eSpinalMedialLateral
            // 
            this.eSpinalMedialLateral.DecimalPlaces = 1;
            this.eSpinalMedialLateral.Location = new System.Drawing.Point(98, 72);
            this.eSpinalMedialLateral.Name = "eSpinalMedialLateral";
            this.eSpinalMedialLateral.Size = new System.Drawing.Size(63, 23);
            this.eSpinalMedialLateral.TabIndex = 14;
            this.eSpinalMedialLateral.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.eSpinalMedialLateral.ValueChanged += new System.EventHandler(this.eSpinalMedialLateral_ValueChanged);
            this.eSpinalMedialLateral.Enter += new System.EventHandler(this.eSpinalMedialLateral_Enter);
            // 
            // lSpinalMedialLateral
            // 
            this.lSpinalMedialLateral.AutoSize = true;
            this.lSpinalMedialLateral.Location = new System.Drawing.Point(6, 74);
            this.lSpinalMedialLateral.Name = "lSpinalMedialLateral";
            this.lSpinalMedialLateral.Size = new System.Drawing.Size(83, 15);
            this.lSpinalMedialLateral.TabIndex = 13;
            this.lSpinalMedialLateral.Text = "Medial-Lateral";
            // 
            // eSpinalDorsalVentral
            // 
            this.eSpinalDorsalVentral.DecimalPlaces = 1;
            this.eSpinalDorsalVentral.Location = new System.Drawing.Point(98, 44);
            this.eSpinalDorsalVentral.Name = "eSpinalDorsalVentral";
            this.eSpinalDorsalVentral.Size = new System.Drawing.Size(63, 23);
            this.eSpinalDorsalVentral.TabIndex = 12;
            this.eSpinalDorsalVentral.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.eSpinalDorsalVentral.ValueChanged += new System.EventHandler(this.eSpinalDorsalVentral_ValueChanged);
            this.eSpinalDorsalVentral.Enter += new System.EventHandler(this.eSpinalDorsalVentral_Enter);
            // 
            // lSpinalDorsalVentral
            // 
            this.lSpinalDorsalVentral.AutoSize = true;
            this.lSpinalDorsalVentral.Location = new System.Drawing.Point(6, 46);
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
            10,
            0,
            0,
            0});
            this.eBodyMedialLateral.ValueChanged += new System.EventHandler(this.eBodyMedialLateral_ValueChanged);
            this.eBodyMedialLateral.Enter += new System.EventHandler(this.eBodyMedialLateral_Enter);
            // 
            // lBodyMedialLateral
            // 
            this.lBodyMedialLateral.AutoSize = true;
            this.lBodyMedialLateral.Location = new System.Drawing.Point(6, 57);
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
            15,
            0,
            0,
            0});
            this.eBodyDorsalVentral.ValueChanged += new System.EventHandler(this.eBodyDorsalVentral_ValueChanged);
            this.eBodyDorsalVentral.Enter += new System.EventHandler(this.eBodyDorsalVentral_Enter);
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
            this.eModelDescription.Size = new System.Drawing.Size(402, 41);
            this.eModelDescription.TabIndex = 7;
            // 
            // eModelName
            // 
            this.eModelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eModelName.Location = new System.Drawing.Point(131, 3);
            this.eModelName.Name = "eModelName";
            this.eModelName.Size = new System.Drawing.Size(402, 23);
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
            // tabCellPools
            // 
            this.tabCellPools.Controls.Add(this.splitCellPoolsAndConnections);
            this.tabCellPools.Location = new System.Drawing.Point(4, 24);
            this.tabCellPools.Name = "tabCellPools";
            this.tabCellPools.Size = new System.Drawing.Size(542, 610);
            this.tabCellPools.TabIndex = 1;
            this.tabCellPools.Text = "Cell Pools & Connections";
            this.tabCellPools.UseVisualStyleBackColor = true;
            // 
            // splitCellPoolsAndConnections
            // 
            this.splitCellPoolsAndConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCellPoolsAndConnections.Location = new System.Drawing.Point(0, 0);
            this.splitCellPoolsAndConnections.Name = "splitCellPoolsAndConnections";
            // 
            // splitCellPoolsAndConnections.Panel1
            // 
            this.splitCellPoolsAndConnections.Panel1.Controls.Add(this.listCellPool);
            this.splitCellPoolsAndConnections.Panel1MinSize = 200;
            // 
            // splitCellPoolsAndConnections.Panel2
            // 
            this.splitCellPoolsAndConnections.Panel2.Controls.Add(this.listConnections);
            this.splitCellPoolsAndConnections.Panel2MinSize = 200;
            this.splitCellPoolsAndConnections.Size = new System.Drawing.Size(542, 610);
            this.splitCellPoolsAndConnections.SplitterDistance = 252;
            this.splitCellPoolsAndConnections.TabIndex = 5;
            // 
            // listCellPool
            // 
            this.listCellPool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCellPool.Location = new System.Drawing.Point(0, 0);
            this.listCellPool.Name = "listCellPool";
            this.listCellPool.SelectedIndex = -1;
            this.listCellPool.SelectedItem = null;
            this.listCellPool.Size = new System.Drawing.Size(252, 610);
            this.listCellPool.TabIndex = 5;
            this.listCellPool.AddItem += new System.EventHandler(this.listCellPool_AddItem);
            this.listCellPool.DeleteItem += new System.EventHandler(this.listCellPool_DeleteItem);
            this.listCellPool.CopyItem += new System.EventHandler(this.listCellPool_CopyItem);
            this.listCellPool.ViewItem += new System.EventHandler(this.listCellPool_ViewItem);
            this.listCellPool.ActivateItem += new System.EventHandler(this.listCellPool_ActivateItem);
            this.listCellPool.SortItems += new System.EventHandler(this.listCellPool_SortItems);
            // 
            // listConnections
            // 
            this.listConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listConnections.Location = new System.Drawing.Point(0, 0);
            this.listConnections.Name = "listConnections";
            this.listConnections.SelectedIndex = -1;
            this.listConnections.SelectedItem = null;
            this.listConnections.Size = new System.Drawing.Size(286, 610);
            this.listConnections.TabIndex = 6;
            this.listConnections.AddItem += new System.EventHandler(this.listConnections_AddItem);
            this.listConnections.DeleteItem += new System.EventHandler(this.listConnections_DeleteItem);
            this.listConnections.CopyItem += new System.EventHandler(this.listConnections_CopyItem);
            this.listConnections.ViewItem += new System.EventHandler(this.listConnections_ViewItem);
            this.listConnections.ActivateItem += new System.EventHandler(this.listConnections_ActivateItem);
            this.listConnections.SortItems += new System.EventHandler(this.listConnections_SortItems);
            // 
            // tabStimuli
            // 
            this.tabStimuli.Controls.Add(this.listStimuli);
            this.tabStimuli.Location = new System.Drawing.Point(4, 24);
            this.tabStimuli.Name = "tabStimuli";
            this.tabStimuli.Size = new System.Drawing.Size(542, 610);
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
            this.listStimuli.Size = new System.Drawing.Size(542, 610);
            this.listStimuli.TabIndex = 6;
            this.listStimuli.AddItem += new System.EventHandler(this.listStimuli_AddItem);
            this.listStimuli.DeleteItem += new System.EventHandler(this.listStimuli_DeleteItem);
            this.listStimuli.CopyItem += new System.EventHandler(this.listStimuli_CopyItem);
            this.listStimuli.ViewItem += new System.EventHandler(this.listStimuli_ViewItem);
            this.listStimuli.ActivateItem += new System.EventHandler(this.listStimuli_ActivateItem);
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.linkLoadParam);
            this.pMain.Controls.Add(this.lParameters);
            this.pMain.Controls.Add(this.linkSaveParam);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(550, 34);
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
            this.tabOutputs.Controls.Add(this.tabAnimation);
            this.tabOutputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOutputs.Location = new System.Drawing.Point(0, 133);
            this.tabOutputs.Name = "tabOutputs";
            this.tabOutputs.SelectedIndex = 0;
            this.tabOutputs.Size = new System.Drawing.Size(794, 539);
            this.tabOutputs.TabIndex = 1;
            // 
            // tabPlot
            // 
            this.tabPlot.Controls.Add(this.tabPlotSub);
            this.tabPlot.Controls.Add(this.pPlot);
            this.tabPlot.Location = new System.Drawing.Point(4, 24);
            this.tabPlot.Name = "tabPlot";
            this.tabPlot.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlot.Size = new System.Drawing.Size(786, 511);
            this.tabPlot.TabIndex = 4;
            this.tabPlot.Text = "Plots";
            this.tabPlot.UseVisualStyleBackColor = true;
            // 
            // tabPlotSub
            // 
            this.tabPlotSub.Controls.Add(this.tPlotWindows);
            this.tabPlotSub.Controls.Add(this.tPlotHTML);
            this.tabPlotSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPlotSub.Location = new System.Drawing.Point(3, 110);
            this.tabPlotSub.Name = "tabPlotSub";
            this.tabPlotSub.SelectedIndex = 0;
            this.tabPlotSub.Size = new System.Drawing.Size(780, 398);
            this.tabPlotSub.TabIndex = 6;
            // 
            // tPlotWindows
            // 
            this.tPlotWindows.Controls.Add(this.splitPlotWindows);
            this.tPlotWindows.Location = new System.Drawing.Point(4, 24);
            this.tPlotWindows.Name = "tPlotWindows";
            this.tPlotWindows.Padding = new System.Windows.Forms.Padding(3);
            this.tPlotWindows.Size = new System.Drawing.Size(772, 370);
            this.tPlotWindows.TabIndex = 0;
            this.tPlotWindows.Text = "Image Plots";
            // 
            // splitPlotWindows
            // 
            this.splitPlotWindows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitPlotWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPlotWindows.Location = new System.Drawing.Point(3, 3);
            this.splitPlotWindows.Name = "splitPlotWindows";
            // 
            // splitPlotWindows.Panel1
            // 
            this.splitPlotWindows.Panel1.AutoScroll = true;
            this.splitPlotWindows.Panel1.Controls.Add(this.pictureBoxLeft);
            // 
            // splitPlotWindows.Panel2
            // 
            this.splitPlotWindows.Panel2.AutoScroll = true;
            this.splitPlotWindows.Panel2.Controls.Add(this.pictureBoxRight);
            this.splitPlotWindows.Size = new System.Drawing.Size(766, 364);
            this.splitPlotWindows.SplitterDistance = 356;
            this.splitPlotWindows.TabIndex = 0;
            this.splitPlotWindows.DoubleClick += new System.EventHandler(this.splitWindows_DoubleClick);
            // 
            // pictureBoxLeft
            // 
            this.pictureBoxLeft.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.Size = new System.Drawing.Size(361, 412);
            this.pictureBoxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxLeft.TabIndex = 0;
            this.pictureBoxLeft.TabStop = false;
            // 
            // pictureBoxRight
            // 
            this.pictureBoxRight.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.Size = new System.Drawing.Size(411, 412);
            this.pictureBoxRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRight.TabIndex = 0;
            this.pictureBoxRight.TabStop = false;
            // 
            // tPlotHTML
            // 
            this.tPlotHTML.Controls.Add(this.webViewPlot);
            this.tPlotHTML.Location = new System.Drawing.Point(4, 24);
            this.tPlotHTML.Name = "tPlotHTML";
            this.tPlotHTML.Padding = new System.Windows.Forms.Padding(3);
            this.tPlotHTML.Size = new System.Drawing.Size(772, 370);
            this.tPlotHTML.TabIndex = 1;
            this.tPlotHTML.Text = "HTML Plots";
            this.tPlotHTML.UseVisualStyleBackColor = true;
            // 
            // webViewPlot
            // 
            this.webViewPlot.AllowExternalDrop = true;
            this.webViewPlot.CreationProperties = null;
            this.webViewPlot.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewPlot.Location = new System.Drawing.Point(3, 3);
            this.webViewPlot.Name = "webViewPlot";
            this.webViewPlot.Size = new System.Drawing.Size(766, 364);
            this.webViewPlot.TabIndex = 1;
            this.webViewPlot.ZoomFactor = 1D;
            this.webViewPlot.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webViewPlot_CoreWebView2InitializationCompleted);
            // 
            // pPlot
            // 
            this.pPlot.BackColor = System.Drawing.SystemColors.Control;
            this.pPlot.Controls.Add(this.linkSavePlots);
            this.pPlot.Controls.Add(this.btnPlotHTML);
            this.pPlot.Controls.Add(this.ddPlotCellSelection);
            this.pPlot.Controls.Add(this.lPlotCells);
            this.pPlot.Controls.Add(this.ePlotCellSelection);
            this.pPlot.Controls.Add(this.linkSaveHTMLPlots);
            this.pPlot.Controls.Add(this.ddPlotSomiteSelection);
            this.pPlot.Controls.Add(this.lPlotSomites);
            this.pPlot.Controls.Add(this.ePlotEnd);
            this.pPlot.Controls.Add(this.ePlotStart);
            this.pPlot.Controls.Add(this.lGroupingWindows);
            this.pPlot.Controls.Add(this.ddGroupingWindows);
            this.pPlot.Controls.Add(this.ePlotSomiteSelection);
            this.pPlot.Controls.Add(this.lPlotPool);
            this.pPlot.Controls.Add(this.ddCellsPools);
            this.pPlot.Controls.Add(this.lPlotStart);
            this.pPlot.Controls.Add(this.label7);
            this.pPlot.Controls.Add(this.lPlotEnd);
            this.pPlot.Controls.Add(this.label9);
            this.pPlot.Controls.Add(this.lPlotPlot);
            this.pPlot.Controls.Add(this.btnPlotWindows);
            this.pPlot.Controls.Add(this.ddPlot);
            this.pPlot.Dock = System.Windows.Forms.DockStyle.Top;
            this.pPlot.Location = new System.Drawing.Point(3, 3);
            this.pPlot.Name = "pPlot";
            this.pPlot.Size = new System.Drawing.Size(780, 107);
            this.pPlot.TabIndex = 5;
            // 
            // linkSavePlots
            // 
            this.linkSavePlots.AutoSize = true;
            this.linkSavePlots.Enabled = false;
            this.linkSavePlots.Location = new System.Drawing.Point(586, 57);
            this.linkSavePlots.Name = "linkSavePlots";
            this.linkSavePlots.Size = new System.Drawing.Size(96, 15);
            this.linkSavePlots.TabIndex = 51;
            this.linkSavePlots.TabStop = true;
            this.linkSavePlots.Text = "Save Plot Images";
            this.linkSavePlots.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSavePlots_LinkClicked);
            // 
            // btnPlotHTML
            // 
            this.btnPlotHTML.Enabled = false;
            this.btnPlotHTML.Location = new System.Drawing.Point(505, 78);
            this.btnPlotHTML.Name = "btnPlotHTML";
            this.btnPlotHTML.Size = new System.Drawing.Size(75, 23);
            this.btnPlotHTML.TabIndex = 30;
            this.btnPlotHTML.Text = "Plot HTML";
            this.toolTip.SetToolTip(this.btnPlotHTML, "Creates interactive plots - Needs Optimization");
            this.btnPlotHTML.UseVisualStyleBackColor = true;
            this.btnPlotHTML.Click += new System.EventHandler(this.btnPlotHTML_Click);
            // 
            // ddPlotCellSelection
            // 
            this.ddPlotCellSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotCellSelection.FormattingEnabled = true;
            this.ddPlotCellSelection.Location = new System.Drawing.Point(505, 28);
            this.ddPlotCellSelection.Name = "ddPlotCellSelection";
            this.ddPlotCellSelection.Size = new System.Drawing.Size(123, 23);
            this.ddPlotCellSelection.TabIndex = 41;
            this.ddPlotCellSelection.SelectedIndexChanged += new System.EventHandler(this.ddPlotCellSelection_SelectedIndexChanged);
            // 
            // lPlotCells
            // 
            this.lPlotCells.AutoSize = true;
            this.lPlotCells.Location = new System.Drawing.Point(450, 32);
            this.lPlotCells.Name = "lPlotCells";
            this.lPlotCells.Size = new System.Drawing.Size(32, 15);
            this.lPlotCells.TabIndex = 45;
            this.lPlotCells.Text = "Cells";
            // 
            // ePlotCellSelection
            // 
            this.ePlotCellSelection.Location = new System.Drawing.Point(634, 29);
            this.ePlotCellSelection.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ePlotCellSelection.Name = "ePlotCellSelection";
            this.ePlotCellSelection.Size = new System.Drawing.Size(47, 23);
            this.ePlotCellSelection.TabIndex = 44;
            this.ePlotCellSelection.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ddPlotSomiteSelection
            // 
            this.ddPlotSomiteSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotSomiteSelection.FormattingEnabled = true;
            this.ddPlotSomiteSelection.Location = new System.Drawing.Point(505, 2);
            this.ddPlotSomiteSelection.Name = "ddPlotSomiteSelection";
            this.ddPlotSomiteSelection.Size = new System.Drawing.Size(123, 23);
            this.ddPlotSomiteSelection.TabIndex = 37;
            this.ddPlotSomiteSelection.SelectedIndexChanged += new System.EventHandler(this.ddPlotSomiteSelection_SelectedIndexChanged);
            // 
            // lPlotSomites
            // 
            this.lPlotSomites.AutoSize = true;
            this.lPlotSomites.Location = new System.Drawing.Point(450, 6);
            this.lPlotSomites.Name = "lPlotSomites";
            this.lPlotSomites.Size = new System.Drawing.Size(49, 15);
            this.lPlotSomites.TabIndex = 42;
            this.lPlotSomites.Text = "Somites";
            // 
            // ePlotEnd
            // 
            this.ePlotEnd.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ePlotEnd.Location = new System.Drawing.Point(74, 30);
            this.ePlotEnd.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.ePlotEnd.Name = "ePlotEnd";
            this.ePlotEnd.Size = new System.Drawing.Size(100, 23);
            this.ePlotEnd.TabIndex = 33;
            this.ePlotEnd.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.ePlotEnd, "(in milliseconds)");
            this.ePlotEnd.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // ePlotStart
            // 
            this.ePlotStart.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ePlotStart.Location = new System.Drawing.Point(74, 3);
            this.ePlotStart.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.ePlotStart.Name = "ePlotStart";
            this.ePlotStart.Size = new System.Drawing.Size(100, 23);
            this.ePlotStart.TabIndex = 31;
            this.ePlotStart.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.ePlotStart, "(in milliseconds)");
            // 
            // lGroupingWindows
            // 
            this.lGroupingWindows.AutoSize = true;
            this.lGroupingWindows.Location = new System.Drawing.Point(217, 32);
            this.lGroupingWindows.Name = "lGroupingWindows";
            this.lGroupingWindows.Size = new System.Drawing.Size(57, 15);
            this.lGroupingWindows.TabIndex = 34;
            this.lGroupingWindows.Text = "Grouping";
            // 
            // ddGroupingWindows
            // 
            this.ddGroupingWindows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddGroupingWindows.FormattingEnabled = true;
            this.ddGroupingWindows.Items.AddRange(new object[] {
            "Full",
            "Individual Cell",
            "Cells in a Pool",
            "Individual Pool",
            "Pools on Opposite Sides"});
            this.ddGroupingWindows.Location = new System.Drawing.Point(280, 28);
            this.ddGroupingWindows.Name = "ddGroupingWindows";
            this.ddGroupingWindows.Size = new System.Drawing.Size(160, 23);
            this.ddGroupingWindows.TabIndex = 35;
            this.ddGroupingWindows.SelectedIndexChanged += new System.EventHandler(this.ddGroupingWindows_SelectedIndexChanged);
            // 
            // ePlotSomiteSelection
            // 
            this.ePlotSomiteSelection.Location = new System.Drawing.Point(634, 3);
            this.ePlotSomiteSelection.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ePlotSomiteSelection.Name = "ePlotSomiteSelection";
            this.ePlotSomiteSelection.Size = new System.Drawing.Size(47, 23);
            this.ePlotSomiteSelection.TabIndex = 39;
            this.ePlotSomiteSelection.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lPlotPool
            // 
            this.lPlotPool.AutoSize = true;
            this.lPlotPool.Location = new System.Drawing.Point(217, 59);
            this.lPlotPool.Name = "lPlotPool";
            this.lPlotPool.Size = new System.Drawing.Size(56, 15);
            this.lPlotPool.TabIndex = 26;
            this.lPlotPool.Text = "Cell/Pool";
            // 
            // ddCellsPools
            // 
            this.ddCellsPools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCellsPools.FormattingEnabled = true;
            this.ddCellsPools.Location = new System.Drawing.Point(280, 54);
            this.ddCellsPools.Name = "ddCellsPools";
            this.ddCellsPools.Size = new System.Drawing.Size(160, 23);
            this.ddCellsPools.TabIndex = 36;
            this.ddCellsPools.SelectedIndexChanged += new System.EventHandler(this.ddCellsPools_SelectedIndexChanged);
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "(ms)";
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(180, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "(ms)";
            // 
            // lPlotPlot
            // 
            this.lPlotPlot.AutoSize = true;
            this.lPlotPlot.Location = new System.Drawing.Point(217, 6);
            this.lPlotPlot.Name = "lPlotPlot";
            this.lPlotPlot.Size = new System.Drawing.Size(28, 15);
            this.lPlotPlot.TabIndex = 19;
            this.lPlotPlot.Text = "Plot";
            // 
            // btnPlotWindows
            // 
            this.btnPlotWindows.Enabled = false;
            this.btnPlotWindows.Location = new System.Drawing.Point(505, 53);
            this.btnPlotWindows.Name = "btnPlotWindows";
            this.btnPlotWindows.Size = new System.Drawing.Size(75, 23);
            this.btnPlotWindows.TabIndex = 50;
            this.btnPlotWindows.Text = "Plot";
            this.btnPlotWindows.UseVisualStyleBackColor = true;
            this.btnPlotWindows.Click += new System.EventHandler(this.btnPlotWindows_Click);
            // 
            // ddPlot
            // 
            this.ddPlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlot.FormattingEnabled = true;
            this.ddPlot.Location = new System.Drawing.Point(280, 2);
            this.ddPlot.Name = "ddPlot";
            this.ddPlot.Size = new System.Drawing.Size(160, 23);
            this.ddPlot.TabIndex = 34;
            this.ddPlot.SelectedIndexChanged += new System.EventHandler(this.ddPlot_SelectedIndexChanged);
            // 
            // tab2DModel
            // 
            this.tab2DModel.Controls.Add(this.webView2DModel);
            this.tab2DModel.Controls.Add(this.p2DModel);
            this.tab2DModel.Location = new System.Drawing.Point(4, 24);
            this.tab2DModel.Name = "tab2DModel";
            this.tab2DModel.Size = new System.Drawing.Size(786, 511);
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
            this.webView2DModel.Size = new System.Drawing.Size(786, 481);
            this.webView2DModel.TabIndex = 1;
            this.webView2DModel.ZoomFactor = 1D;
            this.webView2DModel.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            // 
            // p2DModel
            // 
            this.p2DModel.BackColor = System.Drawing.SystemColors.Control;
            this.p2DModel.Controls.Add(this.linkSaveHTML2D);
            this.p2DModel.Controls.Add(this.btnGenerate2DModel);
            this.p2DModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.p2DModel.Location = new System.Drawing.Point(0, 0);
            this.p2DModel.Name = "p2DModel";
            this.p2DModel.Size = new System.Drawing.Size(786, 30);
            this.p2DModel.TabIndex = 2;
            // 
            // linkSaveHTML2D
            // 
            this.linkSaveHTML2D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveHTML2D.AutoSize = true;
            this.linkSaveHTML2D.Location = new System.Drawing.Point(712, 7);
            this.linkSaveHTML2D.Name = "linkSaveHTML2D";
            this.linkSaveHTML2D.Size = new System.Drawing.Size(66, 15);
            this.linkSaveHTML2D.TabIndex = 23;
            this.linkSaveHTML2D.TabStop = true;
            this.linkSaveHTML2D.Text = "Save HTML";
            this.linkSaveHTML2D.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveHTML2D_LinkClicked);
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
            this.tab3DModel.Size = new System.Drawing.Size(786, 511);
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
            this.webView3DModel.Size = new System.Drawing.Size(780, 475);
            this.webView3DModel.TabIndex = 0;
            this.webView3DModel.ZoomFactor = 1D;
            this.webView3DModel.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            // 
            // p3DModel
            // 
            this.p3DModel.BackColor = System.Drawing.SystemColors.Control;
            this.p3DModel.Controls.Add(this.linkSaveHTML3D);
            this.p3DModel.Controls.Add(this.dd3DModelType);
            this.p3DModel.Controls.Add(this.btnGenerate3DModel);
            this.p3DModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.p3DModel.Location = new System.Drawing.Point(3, 3);
            this.p3DModel.Name = "p3DModel";
            this.p3DModel.Size = new System.Drawing.Size(780, 30);
            this.p3DModel.TabIndex = 3;
            // 
            // linkSaveHTML3D
            // 
            this.linkSaveHTML3D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveHTML3D.AutoSize = true;
            this.linkSaveHTML3D.Location = new System.Drawing.Point(712, 7);
            this.linkSaveHTML3D.Name = "linkSaveHTML3D";
            this.linkSaveHTML3D.Size = new System.Drawing.Size(66, 15);
            this.linkSaveHTML3D.TabIndex = 27;
            this.linkSaveHTML3D.TabStop = true;
            this.linkSaveHTML3D.Text = "Save HTML";
            this.linkSaveHTML3D.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveHTML3D_LinkClicked);
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
            this.dd3DModelType.Location = new System.Drawing.Point(5, 3);
            this.dd3DModelType.Name = "dd3DModelType";
            this.dd3DModelType.Size = new System.Drawing.Size(121, 23);
            this.dd3DModelType.TabIndex = 25;
            this.dd3DModelType.SelectedIndexChanged += new System.EventHandler(this.dd3DModelType_SelectedIndexChanged);
            // 
            // btnGenerate3DModel
            // 
            this.btnGenerate3DModel.AutoSize = true;
            this.btnGenerate3DModel.Location = new System.Drawing.Point(132, 2);
            this.btnGenerate3DModel.Name = "btnGenerate3DModel";
            this.btnGenerate3DModel.Size = new System.Drawing.Size(118, 25);
            this.btnGenerate3DModel.TabIndex = 26;
            this.btnGenerate3DModel.Text = "Generate 3D Model";
            this.btnGenerate3DModel.UseVisualStyleBackColor = true;
            this.btnGenerate3DModel.Click += new System.EventHandler(this.btnGenerate3DModel_Click);
            // 
            // tabAnimation
            // 
            this.tabAnimation.Controls.Add(this.webViewAnimation);
            this.tabAnimation.Controls.Add(this.pAnimation);
            this.tabAnimation.Location = new System.Drawing.Point(4, 24);
            this.tabAnimation.Name = "tabAnimation";
            this.tabAnimation.Size = new System.Drawing.Size(786, 511);
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
            this.webViewAnimation.Size = new System.Drawing.Size(786, 444);
            this.webViewAnimation.TabIndex = 2;
            this.webViewAnimation.ZoomFactor = 1D;
            this.webViewAnimation.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            // 
            // pAnimation
            // 
            this.pAnimation.BackColor = System.Drawing.SystemColors.Control;
            this.pAnimation.Controls.Add(this.linkSaveAnimationCSV);
            this.pAnimation.Controls.Add(this.lAnimationTime);
            this.pAnimation.Controls.Add(this.eAnimationEnd);
            this.pAnimation.Controls.Add(this.eAnimationStart);
            this.pAnimation.Controls.Add(this.label12);
            this.pAnimation.Controls.Add(this.linkSaveAnimationHTML);
            this.pAnimation.Controls.Add(this.btnAnimate);
            this.pAnimation.Controls.Add(this.lAnimationStart);
            this.pAnimation.Controls.Add(this.lms5);
            this.pAnimation.Controls.Add(this.lAnimationEnd);
            this.pAnimation.Controls.Add(this.lms6);
            this.pAnimation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pAnimation.Location = new System.Drawing.Point(0, 0);
            this.pAnimation.Name = "pAnimation";
            this.pAnimation.Size = new System.Drawing.Size(786, 67);
            this.pAnimation.TabIndex = 5;
            // 
            // linkSaveAnimationCSV
            // 
            this.linkSaveAnimationCSV.Enabled = false;
            this.linkSaveAnimationCSV.Location = new System.Drawing.Point(475, 8);
            this.linkSaveAnimationCSV.Name = "linkSaveAnimationCSV";
            this.linkSaveAnimationCSV.Size = new System.Drawing.Size(124, 15);
            this.linkSaveAnimationCSV.TabIndex = 34;
            this.linkSaveAnimationCSV.TabStop = true;
            this.linkSaveAnimationCSV.Text = "Save Animation CSV";
            this.linkSaveAnimationCSV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveAnimationCSV_LinkClicked);
            // 
            // lAnimationTime
            // 
            this.lAnimationTime.Location = new System.Drawing.Point(264, 27);
            this.lAnimationTime.Name = "lAnimationTime";
            this.lAnimationTime.Size = new System.Drawing.Size(294, 19);
            this.lAnimationTime.TabIndex = 33;
            // 
            // eAnimationEnd
            // 
            this.eAnimationEnd.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.eAnimationEnd.Location = new System.Drawing.Point(108, 34);
            this.eAnimationEnd.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.eAnimationEnd.Name = "eAnimationEnd";
            this.eAnimationEnd.Size = new System.Drawing.Size(100, 23);
            this.eAnimationEnd.TabIndex = 27;
            this.eAnimationEnd.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.eAnimationEnd, "(in milliseconds)");
            this.eAnimationEnd.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // eAnimationStart
            // 
            this.eAnimationStart.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.eAnimationStart.Location = new System.Drawing.Point(108, 6);
            this.eAnimationStart.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.eAnimationStart.Name = "eAnimationStart";
            this.eAnimationStart.Size = new System.Drawing.Size(100, 23);
            this.eAnimationStart.TabIndex = 25;
            this.eAnimationStart.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.eAnimationStart, "(in milliseconds)");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(264, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(294, 15);
            this.label12.TabIndex = 32;
            this.label12.Text = "Generated for proof of concept - requires optimization";
            // 
            // linkSaveAnimationHTML
            // 
            this.linkSaveAnimationHTML.Enabled = false;
            this.linkSaveAnimationHTML.Location = new System.Drawing.Point(345, 8);
            this.linkSaveAnimationHTML.Name = "linkSaveAnimationHTML";
            this.linkSaveAnimationHTML.Size = new System.Drawing.Size(138, 15);
            this.linkSaveAnimationHTML.TabIndex = 31;
            this.linkSaveAnimationHTML.TabStop = true;
            this.linkSaveAnimationHTML.Text = "Save Animation HTML";
            this.linkSaveAnimationHTML.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveAnimationHTML_LinkClicked);
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
            // lms6
            // 
            this.lms6.AutoSize = true;
            this.lms6.Location = new System.Drawing.Point(214, 37);
            this.lms6.Name = "lms6";
            this.lms6.Size = new System.Drawing.Size(31, 15);
            this.lms6.TabIndex = 16;
            this.lms6.Text = "(ms)";
            // 
            // pParamBottom
            // 
            this.pParamBottom.Controls.Add(this.cbMultiple);
            this.pParamBottom.Controls.Add(this.eRunNumber);
            this.pParamBottom.Controls.Add(this.eSkip);
            this.pParamBottom.Controls.Add(this.lSkip);
            this.pParamBottom.Controls.Add(this.edt);
            this.pParamBottom.Controls.Add(this.eTimeEnd);
            this.pParamBottom.Controls.Add(this.ldt);
            this.pParamBottom.Controls.Add(this.lRunTime);
            this.pParamBottom.Controls.Add(this.lStimulus);
            this.pParamBottom.Controls.Add(this.ddStimulusMode);
            this.pParamBottom.Controls.Add(this.linkSaveRun);
            this.pParamBottom.Controls.Add(this.progressBarRun);
            this.pParamBottom.Controls.Add(this.btnRun);
            this.pParamBottom.Controls.Add(this.lTimeEnd);
            this.pParamBottom.Controls.Add(this.lRunParameters);
            this.pParamBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.pParamBottom.Location = new System.Drawing.Point(0, 0);
            this.pParamBottom.Name = "pParamBottom";
            this.pParamBottom.Size = new System.Drawing.Size(794, 133);
            this.pParamBottom.TabIndex = 1;
            // 
            // cbMultiple
            // 
            this.cbMultiple.AutoSize = true;
            this.cbMultiple.Location = new System.Drawing.Point(169, 58);
            this.cbMultiple.Name = "cbMultiple";
            this.cbMultiple.Size = new System.Drawing.Size(70, 19);
            this.cbMultiple.TabIndex = 37;
            this.cbMultiple.Text = "Multiple";
            this.toolTip.SetToolTip(this.cbMultiple, "Runs the model multiple times saving the model details and episodes.");
            this.cbMultiple.UseVisualStyleBackColor = true;
            this.cbMultiple.CheckedChanged += new System.EventHandler(this.cbMultiple_CheckedChanged);
            // 
            // eRunNumber
            // 
            this.eRunNumber.Location = new System.Drawing.Point(240, 56);
            this.eRunNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eRunNumber.Name = "eRunNumber";
            this.eRunNumber.Size = new System.Drawing.Size(63, 23);
            this.eRunNumber.TabIndex = 36;
            this.toolTip.SetToolTip(this.eRunNumber, "Warning: depending on the complexity of the model, multiple runs can take a long " +
        "time.");
            this.eRunNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eRunNumber.Visible = false;
            // 
            // eSkip
            // 
            this.eSkip.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eSkip.Location = new System.Drawing.Point(74, 56);
            this.eSkip.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.eSkip.Name = "eSkip";
            this.eSkip.Size = new System.Drawing.Size(76, 23);
            this.eSkip.TabIndex = 34;
            this.toolTip.SetToolTip(this.eSkip, "(in milliseconds)");
            // 
            // lSkip
            // 
            this.lSkip.AutoSize = true;
            this.lSkip.Location = new System.Drawing.Point(16, 60);
            this.lSkip.Name = "lSkip";
            this.lSkip.Size = new System.Drawing.Size(29, 15);
            this.lSkip.TabIndex = 6;
            this.lSkip.Text = "Skip";
            // 
            // edt
            // 
            this.edt.DecimalPlaces = 2;
            this.edt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edt.Location = new System.Drawing.Point(74, 81);
            this.edt.Name = "edt";
            this.edt.Size = new System.Drawing.Size(76, 23);
            this.edt.TabIndex = 35;
            this.toolTip.SetToolTip(this.edt, "(in milliseconds)");
            this.edt.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // eTimeEnd
            // 
            this.eTimeEnd.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.eTimeEnd.Location = new System.Drawing.Point(74, 30);
            this.eTimeEnd.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.eTimeEnd.Name = "eTimeEnd";
            this.eTimeEnd.Size = new System.Drawing.Size(76, 23);
            this.eTimeEnd.TabIndex = 33;
            this.eTimeEnd.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.eTimeEnd, "(in milliseconds)");
            this.eTimeEnd.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.eTimeEnd.ValueChanged += new System.EventHandler(this.eTimeEnd_ValueChanged);
            this.eTimeEnd.Enter += new System.EventHandler(this.eTimeEnd_Enter);
            // 
            // ldt
            // 
            this.ldt.AutoSize = true;
            this.ldt.Location = new System.Drawing.Point(16, 84);
            this.ldt.Name = "ldt";
            this.ldt.Size = new System.Drawing.Size(19, 15);
            this.ldt.TabIndex = 32;
            this.ldt.Text = "Δt";
            // 
            // lRunTime
            // 
            this.lRunTime.Location = new System.Drawing.Point(310, 29);
            this.lRunTime.Name = "lRunTime";
            this.lRunTime.Size = new System.Drawing.Size(177, 76);
            this.lRunTime.TabIndex = 31;
            // 
            // lStimulus
            // 
            this.lStimulus.AutoSize = true;
            this.lStimulus.Location = new System.Drawing.Point(167, 30);
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
            this.ddStimulusMode.Location = new System.Drawing.Point(227, 29);
            this.ddStimulusMode.Name = "ddStimulusMode";
            this.ddStimulusMode.Size = new System.Drawing.Size(76, 23);
            this.ddStimulusMode.TabIndex = 30;
            // 
            // linkSaveRun
            // 
            this.linkSaveRun.AutoSize = true;
            this.linkSaveRun.Location = new System.Drawing.Point(249, 85);
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
            this.progressBarRun.Location = new System.Drawing.Point(0, 110);
            this.progressBarRun.Name = "progressBarRun";
            this.progressBarRun.Size = new System.Drawing.Size(794, 23);
            this.progressBarRun.TabIndex = 26;
            this.progressBarRun.Visible = false;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(167, 81);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 9;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lTimeEnd
            // 
            this.lTimeEnd.AutoSize = true;
            this.lTimeEnd.Location = new System.Drawing.Point(16, 35);
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
            this.lRunParameters.Size = new System.Drawing.Size(153, 35);
            this.lRunParameters.TabIndex = 1;
            this.lRunParameters.Text = "Simulation Parameters";
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
            // saveFileImage
            // 
            this.saveFileImage.Filter = "Image files(*.png)|*.png";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 711);
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
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.pBodyDiagrams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRostroCaudal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrossSection)).EndInit();
            this.grSpinalCord.ResumeLayout(false);
            this.grSpinalCord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eNumSomites)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalBodyPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalRostraoCaudal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalMedialLateral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSpinalDorsalVentral)).EndInit();
            this.grBody.ResumeLayout(false);
            this.grBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eBodyMedialLateral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eBodyDorsalVentral)).EndInit();
            this.tabCellPools.ResumeLayout(false);
            this.splitCellPoolsAndConnections.Panel1.ResumeLayout(false);
            this.splitCellPoolsAndConnections.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCellPoolsAndConnections)).EndInit();
            this.splitCellPoolsAndConnections.ResumeLayout(false);
            this.tabStimuli.ResumeLayout(false);
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            this.tabOutputs.ResumeLayout(false);
            this.tabPlot.ResumeLayout(false);
            this.tabPlotSub.ResumeLayout(false);
            this.tPlotWindows.ResumeLayout(false);
            this.splitPlotWindows.Panel1.ResumeLayout(false);
            this.splitPlotWindows.Panel1.PerformLayout();
            this.splitPlotWindows.Panel2.ResumeLayout(false);
            this.splitPlotWindows.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPlotWindows)).EndInit();
            this.splitPlotWindows.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).EndInit();
            this.tPlotHTML.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewPlot)).EndInit();
            this.pPlot.ResumeLayout(false);
            this.pPlot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotCellSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotSomiteSelection)).EndInit();
            this.tab2DModel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView2DModel)).EndInit();
            this.p2DModel.ResumeLayout(false);
            this.p2DModel.PerformLayout();
            this.tab3DModel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView3DModel)).EndInit();
            this.p3DModel.ResumeLayout(false);
            this.p3DModel.PerformLayout();
            this.tabAnimation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewAnimation)).EndInit();
            this.pAnimation.ResumeLayout(false);
            this.pAnimation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationStart)).EndInit();
            this.pParamBottom.ResumeLayout(false);
            this.pParamBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eRunNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eTimeEnd)).EndInit();
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
        private Label lTimeEnd;
        private Button btnPlotHTML;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView3DModel;
        private TabControl tabOutputs;
        private TabPage tab3DModel;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewPlot;
        private ProgressBar progressBarRun;
        private System.Windows.Forms.Timer timerRun;
        private LinkLabel linkSaveHTMLPlots;
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
        private Panel pAnimation;
        private Label lAnimationStart;
        private Label lms5;
        private Label lAnimationEnd;
        private Label lms6;
        private TabControl tabParams;
        private LinkLabel linkSaveParam;
        private Panel pMain;
        private LinkLabel linkLoadParam;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private LinkLabel linkSaveRun;
        private SaveFileDialog saveFileCSV;
        private ToolTip toolTip;
        private TabPage tabPlot;
        private SplitContainer splitPlotWindows;
        private Panel pPlot;
        private Label lPlotPool;
        private ComboBox ddCellsPools;
        private Label lPlotStart;
        private Label label7;
        private Label lPlotEnd;
        private Label label9;
        private Label lPlotPlot;
        private Button btnPlotWindows;
        private ComboBox ddPlot;
        private NumericUpDown ePlotSomiteSelection;
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
        private SplitContainer splitCellPoolsAndConnections;
        private ListBoxControl listCellPool;
        private ListBoxControl listConnections;
        private ListBoxControl listStimuli;
        private Panel pBodyDiagrams;
        private LinkLabel linkSaveAnimationHTML;
        private Label lRunTime;
        private Label label12;
        private NumericUpDown edt;
        private NumericUpDown eTimeEnd;
        private Label ldt;
        private NumericUpDown eSkip;
        private Label lSkip;
        private NumericUpDown eNumSomites;
        private Label lNoOfSomites;
        private PictureBox pictureBoxLeft;
        private PictureBox pictureBoxRight;
        private Label lGroupingWindows;
        private ComboBox ddGroupingWindows;
        private NumericUpDown ePlotEnd;
        private NumericUpDown ePlotStart;
        private NumericUpDown eAnimationStart;
        private NumericUpDown eAnimationEnd;
        private LinkLabel linkBrowseToOutputFolder;
        private Label lAnimationTime;
        private LinkLabel linkSaveAnimationCSV;
        private ComboBox ddPlotCellSelection;
        private Label lPlotCells;
        private NumericUpDown ePlotCellSelection;
        private ComboBox ddPlotSomiteSelection;
        private Label lPlotSomites;
        private TabControl tabPlotSub;
        private TabPage tPlotWindows;
        private TabPage tPlotHTML;
        private LinkLabel linkBrowseToTempFolder;
        private LinkLabel linkSaveHTML2D;
        private LinkLabel linkSaveHTML3D;
        private LinkLabel linkSavePlots;
        private SaveFileDialog saveFileImage;
        private NumericUpDown eRunNumber;
        private CheckBox cbMultiple;
    }
}