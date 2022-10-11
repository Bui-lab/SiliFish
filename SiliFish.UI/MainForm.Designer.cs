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
            this.pTop = new System.Windows.Forms.Panel();
            this.btnCellularDynamics = new System.Windows.Forms.Button();
            this.ddModelSelection = new System.Windows.Forms.ComboBox();
            this.lModelSelection = new System.Windows.Forms.Label();
            this.linkBrowseToTempFolder = new System.Windows.Forms.LinkLabel();
            this.linkOpenOutputFolder = new System.Windows.Forms.LinkLabel();
            this.linkClearModel = new System.Windows.Forms.LinkLabel();
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
            this.pCellPoolTop = new System.Windows.Forms.Panel();
            this.pLineCellPools = new System.Windows.Forms.Panel();
            this.lCellPools = new System.Windows.Forms.Label();
            this.listConnections = new SiliFish.UI.Controls.ListBoxControl();
            this.pConnectionTop = new System.Windows.Forms.Panel();
            this.pLineConnections = new System.Windows.Forms.Panel();
            this.lConnections = new System.Windows.Forms.Label();
            this.tabStimuli = new System.Windows.Forms.TabPage();
            this.listStimuli = new SiliFish.UI.Controls.ListBoxControl();
            this.pParamBottom = new System.Windows.Forms.Panel();
            this.lRunCount = new System.Windows.Forms.Label();
            this.eRunNumber = new System.Windows.Forms.NumericUpDown();
            this.eSkip = new System.Windows.Forms.NumericUpDown();
            this.lSkip = new System.Windows.Forms.Label();
            this.edt = new System.Windows.Forms.NumericUpDown();
            this.eTimeEnd = new System.Windows.Forms.NumericUpDown();
            this.ldt = new System.Windows.Forms.Label();
            this.lRunTime = new System.Windows.Forms.Label();
            this.lStimulus = new System.Windows.Forms.Label();
            this.ddStimulusMode = new System.Windows.Forms.ComboBox();
            this.linkExportOutput = new System.Windows.Forms.LinkLabel();
            this.progressBarRun = new System.Windows.Forms.ProgressBar();
            this.btnRun = new System.Windows.Forms.Button();
            this.lTimeEnd = new System.Windows.Forms.Label();
            this.lRunParameters = new System.Windows.Forms.Label();
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
            this.pLinePlots = new System.Windows.Forms.Panel();
            this.ePlotHeight = new System.Windows.Forms.NumericUpDown();
            this.ePlotWidth = new System.Windows.Forms.NumericUpDown();
            this.lPlotWidth = new System.Windows.Forms.Label();
            this.lpx1 = new System.Windows.Forms.Label();
            this.lPlotHeight = new System.Windows.Forms.Label();
            this.lpx2 = new System.Windows.Forms.Label();
            this.lNumberOfPlots = new System.Windows.Forms.Label();
            this.btnPlotWindows = new System.Windows.Forms.Button();
            this.ddPlotSagittal = new System.Windows.Forms.ComboBox();
            this.linkSavePlots = new System.Windows.Forms.LinkLabel();
            this.btnPlotHTML = new System.Windows.Forms.Button();
            this.ddPlotCellSelection = new System.Windows.Forms.ComboBox();
            this.lPlotCells = new System.Windows.Forms.Label();
            this.ePlotCellSelection = new System.Windows.Forms.NumericUpDown();
            this.ddPlotSomiteSelection = new System.Windows.Forms.ComboBox();
            this.lPlotSomites = new System.Windows.Forms.Label();
            this.ePlotEnd = new System.Windows.Forms.NumericUpDown();
            this.ePlotStart = new System.Windows.Forms.NumericUpDown();
            this.lSagittal = new System.Windows.Forms.Label();
            this.ePlotSomiteSelection = new System.Windows.Forms.NumericUpDown();
            this.lPlotPool = new System.Windows.Forms.Label();
            this.ddPlotPools = new System.Windows.Forms.ComboBox();
            this.lPlotStart = new System.Windows.Forms.Label();
            this.lms1 = new System.Windows.Forms.Label();
            this.lPlotEnd = new System.Windows.Forms.Label();
            this.lms2 = new System.Windows.Forms.Label();
            this.lPlotPlot = new System.Windows.Forms.Label();
            this.ddPlot = new System.Windows.Forms.ComboBox();
            this.tab2DModel = new System.Windows.Forms.TabPage();
            this.webView2DModel = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.p2DModel = new System.Windows.Forms.Panel();
            this.pLine2D = new System.Windows.Forms.Panel();
            this.linkSaveHTML2D = new System.Windows.Forms.LinkLabel();
            this.btnGenerate2DModel = new System.Windows.Forms.Button();
            this.tab3DModel = new System.Windows.Forms.TabPage();
            this.webView3DModel = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.p3DModel = new System.Windows.Forms.Panel();
            this.pLine3D = new System.Windows.Forms.Panel();
            this.ddSomites = new System.Windows.Forms.ComboBox();
            this.lSomites = new System.Windows.Forms.Label();
            this.linkSaveHTML3D = new System.Windows.Forms.LinkLabel();
            this.dd3DModelType = new System.Windows.Forms.ComboBox();
            this.btnGenerate3DModel = new System.Windows.Forms.Button();
            this.tabMNKinematics = new System.Windows.Forms.TabPage();
            this.splitKinematics = new System.Windows.Forms.SplitContainer();
            this.webViewSummaryV = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pLineMNKinematicsRight = new System.Windows.Forms.Panel();
            this.eEpisodesRight = new System.Windows.Forms.RichTextBox();
            this.eEpisodesLeft = new System.Windows.Forms.RichTextBox();
            this.pLineMNKinematicsLeft = new System.Windows.Forms.Panel();
            this.pMNKinematicsTop = new System.Windows.Forms.Panel();
            this.pLineMNKinematics = new System.Windows.Forms.Panel();
            this.ePlotKinematicsHeight = new System.Windows.Forms.NumericUpDown();
            this.ePlotKinematicsWidth = new System.Windows.Forms.NumericUpDown();
            this.lPlotKinematicsWidth = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lPlotKinematicsHeight = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.eKinematicsSomite = new System.Windows.Forms.NumericUpDown();
            this.btnGenerateEpisodes = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.eKinematicsEpisodeBreak = new System.Windows.Forms.NumericUpDown();
            this.lBurstBreak = new System.Windows.Forms.Label();
            this.eKinematicsBurstBreak = new System.Windows.Forms.NumericUpDown();
            this.lKinematicsTimes = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabAnimation = new System.Windows.Forms.TabPage();
            this.webViewAnimation = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pAnimation = new System.Windows.Forms.Panel();
            this.pLineAnimation = new System.Windows.Forms.Panel();
            this.eAnimationdt = new System.Windows.Forms.NumericUpDown();
            this.lAnimationdt = new System.Windows.Forms.Label();
            this.linkSaveAnimationCSV = new System.Windows.Forms.LinkLabel();
            this.lAnimationTime = new System.Windows.Forms.Label();
            this.eAnimationEnd = new System.Windows.Forms.NumericUpDown();
            this.eAnimationStart = new System.Windows.Forms.NumericUpDown();
            this.linkSaveAnimationHTML = new System.Windows.Forms.LinkLabel();
            this.btnAnimate = new System.Windows.Forms.Button();
            this.lAnimationStart = new System.Windows.Forms.Label();
            this.lms3 = new System.Windows.Forms.Label();
            this.lAnimationEnd = new System.Windows.Forms.Label();
            this.lms4 = new System.Windows.Forms.Label();
            this.tabTemplateJSON = new System.Windows.Forms.TabPage();
            this.eTemplateJSON = new System.Windows.Forms.RichTextBox();
            this.pTemplateJSONTop = new System.Windows.Forms.Panel();
            this.pLineTemplateJSON = new System.Windows.Forms.Panel();
            this.linkSaveTemplateJSON = new System.Windows.Forms.LinkLabel();
            this.btnLoadTemplateJSON = new System.Windows.Forms.Button();
            this.btnDisplayTemplateJSON = new System.Windows.Forms.Button();
            this.tabModelJSON = new System.Windows.Forms.TabPage();
            this.eModelJSON = new System.Windows.Forms.RichTextBox();
            this.pModelJSONTop = new System.Windows.Forms.Panel();
            this.pLineModelJSON = new System.Windows.Forms.Panel();
            this.linkSaveModelJSON = new System.Windows.Forms.LinkLabel();
            this.btnLoadModelJSON = new System.Windows.Forms.Button();
            this.btnDisplayModelJSON = new System.Windows.Forms.Button();
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
            this.pCellPoolTop.SuspendLayout();
            this.pConnectionTop.SuspendLayout();
            this.tabStimuli.SuspendLayout();
            this.pParamBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eRunNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eTimeEnd)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.ePlotHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotWidth)).BeginInit();
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
            this.tabMNKinematics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitKinematics)).BeginInit();
            this.splitKinematics.Panel1.SuspendLayout();
            this.splitKinematics.Panel2.SuspendLayout();
            this.splitKinematics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewSummaryV)).BeginInit();
            this.pMNKinematicsTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotKinematicsHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotKinematicsWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eKinematicsSomite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eKinematicsEpisodeBreak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eKinematicsBurstBreak)).BeginInit();
            this.tabAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewAnimation)).BeginInit();
            this.pAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationdt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationStart)).BeginInit();
            this.tabTemplateJSON.SuspendLayout();
            this.pTemplateJSONTop.SuspendLayout();
            this.tabModelJSON.SuspendLayout();
            this.pModelJSONTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pTop.Controls.Add(this.btnCellularDynamics);
            this.pTop.Controls.Add(this.ddModelSelection);
            this.pTop.Controls.Add(this.lModelSelection);
            this.pTop.Controls.Add(this.linkBrowseToTempFolder);
            this.pTop.Controls.Add(this.linkOpenOutputFolder);
            this.pTop.Controls.Add(this.linkClearModel);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1348, 40);
            this.pTop.TabIndex = 3;
            this.pTop.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pTop_MouseDoubleClick);
            // 
            // btnCellularDynamics
            // 
            this.btnCellularDynamics.AutoSize = true;
            this.btnCellularDynamics.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCellularDynamics.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnCellularDynamics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCellularDynamics.Location = new System.Drawing.Point(428, 8);
            this.btnCellularDynamics.Name = "btnCellularDynamics";
            this.btnCellularDynamics.Size = new System.Drawing.Size(120, 27);
            this.btnCellularDynamics.TabIndex = 23;
            this.btnCellularDynamics.Text = "Cellular Dynamics";
            this.btnCellularDynamics.UseVisualStyleBackColor = false;
            this.btnCellularDynamics.Click += new System.EventHandler(this.btnCellularDynamics_Click);
            // 
            // ddModelSelection
            // 
            this.ddModelSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddModelSelection.FormattingEnabled = true;
            this.ddModelSelection.Items.AddRange(new object[] {
            "User defined",
            "Single coil",
            "Double coil",
            "Beat and glide"});
            this.ddModelSelection.Location = new System.Drawing.Point(107, 11);
            this.ddModelSelection.Name = "ddModelSelection";
            this.ddModelSelection.Size = new System.Drawing.Size(121, 23);
            this.ddModelSelection.TabIndex = 10;
            this.ddModelSelection.SelectedIndexChanged += new System.EventHandler(this.ddModelSelection_SelectedIndexChanged);
            // 
            // lModelSelection
            // 
            this.lModelSelection.AutoSize = true;
            this.lModelSelection.Location = new System.Drawing.Point(9, 14);
            this.lModelSelection.Name = "lModelSelection";
            this.lModelSelection.Size = new System.Drawing.Size(92, 15);
            this.lModelSelection.TabIndex = 9;
            this.lModelSelection.Text = "Model Selection";
            // 
            // linkBrowseToTempFolder
            // 
            this.linkBrowseToTempFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkBrowseToTempFolder.AutoSize = true;
            this.linkBrowseToTempFolder.ForeColor = System.Drawing.Color.White;
            this.linkBrowseToTempFolder.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkBrowseToTempFolder.Location = new System.Drawing.Point(1211, 14);
            this.linkBrowseToTempFolder.Name = "linkBrowseToTempFolder";
            this.linkBrowseToTempFolder.Size = new System.Drawing.Size(104, 15);
            this.linkBrowseToTempFolder.TabIndex = 7;
            this.linkBrowseToTempFolder.TabStop = true;
            this.linkBrowseToTempFolder.Text = "Open Temp Folder";
            this.toolTip.SetToolTip(this.linkBrowseToTempFolder, "The contents of the temp folder is cleared at exit.");
            this.linkBrowseToTempFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOpenTempFolder_LinkClicked);
            // 
            // linkOpenOutputFolder
            // 
            this.linkOpenOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkOpenOutputFolder.AutoSize = true;
            this.linkOpenOutputFolder.ForeColor = System.Drawing.Color.White;
            this.linkOpenOutputFolder.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkOpenOutputFolder.Location = new System.Drawing.Point(1092, 14);
            this.linkOpenOutputFolder.Name = "linkOpenOutputFolder";
            this.linkOpenOutputFolder.Size = new System.Drawing.Size(113, 15);
            this.linkOpenOutputFolder.TabIndex = 6;
            this.linkOpenOutputFolder.TabStop = true;
            this.linkOpenOutputFolder.Text = "Open Output Folder";
            this.toolTip.SetToolTip(this.linkOpenOutputFolder, "If there is a problem in creating html files, they will be saved in this folder.\r" +
        "\n");
            this.linkOpenOutputFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOpenOutputFolder_LinkClicked);
            // 
            // linkClearModel
            // 
            this.linkClearModel.AutoSize = true;
            this.linkClearModel.ForeColor = System.Drawing.Color.White;
            this.linkClearModel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkClearModel.Location = new System.Drawing.Point(249, 14);
            this.linkClearModel.Name = "linkClearModel";
            this.linkClearModel.Size = new System.Drawing.Size(71, 15);
            this.linkClearModel.TabIndex = 5;
            this.linkClearModel.TabStop = true;
            this.linkClearModel.Text = "Clear Model";
            this.linkClearModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClearModel_LinkClicked);
            // 
            // linkSaveHTMLPlots
            // 
            this.linkSaveHTMLPlots.AutoSize = true;
            this.linkSaveHTMLPlots.Enabled = false;
            this.linkSaveHTMLPlots.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveHTMLPlots.Location = new System.Drawing.Point(527, 114);
            this.linkSaveHTMLPlots.Name = "linkSaveHTMLPlots";
            this.linkSaveHTMLPlots.Size = new System.Drawing.Size(66, 15);
            this.linkSaveHTMLPlots.TabIndex = 53;
            this.linkSaveHTMLPlots.TabStop = true;
            this.linkSaveHTMLPlots.Text = "Save HTML";
            this.linkSaveHTMLPlots.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveHTMLPlots_LinkClicked);
            // 
            // splitMain
            // 
            this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 40);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.tabParams);
            this.splitMain.Panel1.Controls.Add(this.pParamBottom);
            this.splitMain.Panel1.Controls.Add(this.pMain);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.tabOutputs);
            this.splitMain.Size = new System.Drawing.Size(1348, 671);
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
            this.tabParams.Size = new System.Drawing.Size(548, 484);
            this.tabParams.TabIndex = 2;
            this.tabParams.Tag = "";
            // 
            // tabGeneral
            // 
            this.tabGeneral.BackColor = System.Drawing.Color.White;
            this.tabGeneral.Controls.Add(this.pBodyDiagrams);
            this.tabGeneral.Controls.Add(this.grSpinalCord);
            this.tabGeneral.Controls.Add(this.grBody);
            this.tabGeneral.Controls.Add(this.eModelDescription);
            this.tabGeneral.Controls.Add(this.eModelName);
            this.tabGeneral.Controls.Add(this.lModelDescription);
            this.tabGeneral.Controls.Add(this.lModelName);
            this.tabGeneral.Location = new System.Drawing.Point(4, 24);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(540, 456);
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
            this.pBodyDiagrams.Size = new System.Drawing.Size(348, 383);
            this.pBodyDiagrams.TabIndex = 15;
            this.pBodyDiagrams.SizeChanged += new System.EventHandler(this.pBodyDiagrams_SizeChanged);
            // 
            // picRostroCaudal
            // 
            this.picRostroCaudal.Image = ((System.Drawing.Image)(resources.GetObject("picRostroCaudal.Image")));
            this.picRostroCaudal.Location = new System.Drawing.Point(3, 311);
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
            this.picCrossSection.Size = new System.Drawing.Size(329, 311);
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
            this.eSpinalBodyPosition.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
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
            this.eSpinalRostraoCaudal.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
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
            this.eSpinalMedialLateral.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
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
            this.eSpinalDorsalVentral.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
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
            this.eBodyMedialLateral.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
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
            this.eBodyDorsalVentral.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
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
            this.eModelDescription.Size = new System.Drawing.Size(400, 41);
            this.eModelDescription.TabIndex = 7;
            // 
            // eModelName
            // 
            this.eModelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eModelName.Location = new System.Drawing.Point(131, 3);
            this.eModelName.Name = "eModelName";
            this.eModelName.Size = new System.Drawing.Size(400, 23);
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
            this.tabCellPools.BackColor = System.Drawing.Color.White;
            this.tabCellPools.Controls.Add(this.splitCellPoolsAndConnections);
            this.tabCellPools.Location = new System.Drawing.Point(4, 24);
            this.tabCellPools.Name = "tabCellPools";
            this.tabCellPools.Size = new System.Drawing.Size(540, 456);
            this.tabCellPools.TabIndex = 1;
            this.tabCellPools.Text = "Cell Pools & Connections";
            this.tabCellPools.UseVisualStyleBackColor = true;
            // 
            // splitCellPoolsAndConnections
            // 
            this.splitCellPoolsAndConnections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitCellPoolsAndConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCellPoolsAndConnections.Location = new System.Drawing.Point(0, 0);
            this.splitCellPoolsAndConnections.Name = "splitCellPoolsAndConnections";
            // 
            // splitCellPoolsAndConnections.Panel1
            // 
            this.splitCellPoolsAndConnections.Panel1.Controls.Add(this.listCellPool);
            this.splitCellPoolsAndConnections.Panel1.Controls.Add(this.pCellPoolTop);
            this.splitCellPoolsAndConnections.Panel1MinSize = 200;
            // 
            // splitCellPoolsAndConnections.Panel2
            // 
            this.splitCellPoolsAndConnections.Panel2.Controls.Add(this.listConnections);
            this.splitCellPoolsAndConnections.Panel2.Controls.Add(this.pConnectionTop);
            this.splitCellPoolsAndConnections.Panel2MinSize = 200;
            this.splitCellPoolsAndConnections.Size = new System.Drawing.Size(540, 456);
            this.splitCellPoolsAndConnections.SplitterDistance = 251;
            this.splitCellPoolsAndConnections.TabIndex = 5;
            // 
            // listCellPool
            // 
            this.listCellPool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCellPool.Location = new System.Drawing.Point(0, 32);
            this.listCellPool.Name = "listCellPool";
            this.listCellPool.SelectedIndex = -1;
            this.listCellPool.SelectedItem = null;
            this.listCellPool.Size = new System.Drawing.Size(249, 422);
            this.listCellPool.TabIndex = 5;
            this.listCellPool.AddItem += new System.EventHandler(this.listCellPool_AddItem);
            this.listCellPool.DeleteItem += new System.EventHandler(this.listCellPool_DeleteItem);
            this.listCellPool.CopyItem += new System.EventHandler(this.listCellPool_CopyItem);
            this.listCellPool.ViewItem += new System.EventHandler(this.listCellPool_ViewItem);
            this.listCellPool.ActivateItem += new System.EventHandler(this.listCellPool_ActivateItem);
            this.listCellPool.SortItems += new System.EventHandler(this.listCellPool_SortItems);
            // 
            // pCellPoolTop
            // 
            this.pCellPoolTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pCellPoolTop.Controls.Add(this.pLineCellPools);
            this.pCellPoolTop.Controls.Add(this.lCellPools);
            this.pCellPoolTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCellPoolTop.Location = new System.Drawing.Point(0, 0);
            this.pCellPoolTop.Name = "pCellPoolTop";
            this.pCellPoolTop.Size = new System.Drawing.Size(249, 32);
            this.pCellPoolTop.TabIndex = 6;
            // 
            // pLineCellPools
            // 
            this.pLineCellPools.BackColor = System.Drawing.Color.LightGray;
            this.pLineCellPools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineCellPools.Location = new System.Drawing.Point(0, 31);
            this.pLineCellPools.Name = "pLineCellPools";
            this.pLineCellPools.Size = new System.Drawing.Size(249, 1);
            this.pLineCellPools.TabIndex = 25;
            // 
            // lCellPools
            // 
            this.lCellPools.AutoSize = true;
            this.lCellPools.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lCellPools.Location = new System.Drawing.Point(8, 6);
            this.lCellPools.Name = "lCellPools";
            this.lCellPools.Size = new System.Drawing.Size(59, 15);
            this.lCellPools.TabIndex = 0;
            this.lCellPools.Text = "Cell Pools";
            // 
            // listConnections
            // 
            this.listConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listConnections.Location = new System.Drawing.Point(0, 32);
            this.listConnections.Name = "listConnections";
            this.listConnections.SelectedIndex = -1;
            this.listConnections.SelectedItem = null;
            this.listConnections.Size = new System.Drawing.Size(283, 422);
            this.listConnections.TabIndex = 6;
            this.listConnections.AddItem += new System.EventHandler(this.listConnections_AddItem);
            this.listConnections.DeleteItem += new System.EventHandler(this.listConnections_DeleteItem);
            this.listConnections.CopyItem += new System.EventHandler(this.listConnections_CopyItem);
            this.listConnections.ViewItem += new System.EventHandler(this.listConnections_ViewItem);
            this.listConnections.ActivateItem += new System.EventHandler(this.listConnections_ActivateItem);
            this.listConnections.SortItems += new System.EventHandler(this.listConnections_SortItems);
            // 
            // pConnectionTop
            // 
            this.pConnectionTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pConnectionTop.Controls.Add(this.pLineConnections);
            this.pConnectionTop.Controls.Add(this.lConnections);
            this.pConnectionTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pConnectionTop.Location = new System.Drawing.Point(0, 0);
            this.pConnectionTop.Name = "pConnectionTop";
            this.pConnectionTop.Size = new System.Drawing.Size(283, 32);
            this.pConnectionTop.TabIndex = 7;
            // 
            // pLineConnections
            // 
            this.pLineConnections.BackColor = System.Drawing.Color.LightGray;
            this.pLineConnections.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineConnections.Location = new System.Drawing.Point(0, 31);
            this.pLineConnections.Name = "pLineConnections";
            this.pLineConnections.Size = new System.Drawing.Size(283, 1);
            this.pLineConnections.TabIndex = 25;
            // 
            // lConnections
            // 
            this.lConnections.AutoSize = true;
            this.lConnections.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lConnections.Location = new System.Drawing.Point(8, 6);
            this.lConnections.Name = "lConnections";
            this.lConnections.Size = new System.Drawing.Size(75, 15);
            this.lConnections.TabIndex = 0;
            this.lConnections.Text = "Connections";
            // 
            // tabStimuli
            // 
            this.tabStimuli.Controls.Add(this.listStimuli);
            this.tabStimuli.Location = new System.Drawing.Point(4, 24);
            this.tabStimuli.Name = "tabStimuli";
            this.tabStimuli.Size = new System.Drawing.Size(540, 456);
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
            this.listStimuli.Size = new System.Drawing.Size(540, 456);
            this.listStimuli.TabIndex = 6;
            this.listStimuli.AddItem += new System.EventHandler(this.listStimuli_AddItem);
            this.listStimuli.DeleteItem += new System.EventHandler(this.listStimuli_DeleteItem);
            this.listStimuli.CopyItem += new System.EventHandler(this.listStimuli_CopyItem);
            this.listStimuli.ViewItem += new System.EventHandler(this.listStimuli_ViewItem);
            this.listStimuli.ActivateItem += new System.EventHandler(this.listStimuli_ActivateItem);
            // 
            // pParamBottom
            // 
            this.pParamBottom.Controls.Add(this.lRunCount);
            this.pParamBottom.Controls.Add(this.eRunNumber);
            this.pParamBottom.Controls.Add(this.eSkip);
            this.pParamBottom.Controls.Add(this.lSkip);
            this.pParamBottom.Controls.Add(this.edt);
            this.pParamBottom.Controls.Add(this.eTimeEnd);
            this.pParamBottom.Controls.Add(this.ldt);
            this.pParamBottom.Controls.Add(this.lRunTime);
            this.pParamBottom.Controls.Add(this.lStimulus);
            this.pParamBottom.Controls.Add(this.ddStimulusMode);
            this.pParamBottom.Controls.Add(this.linkExportOutput);
            this.pParamBottom.Controls.Add(this.progressBarRun);
            this.pParamBottom.Controls.Add(this.btnRun);
            this.pParamBottom.Controls.Add(this.lTimeEnd);
            this.pParamBottom.Controls.Add(this.lRunParameters);
            this.pParamBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pParamBottom.Location = new System.Drawing.Point(0, 518);
            this.pParamBottom.Name = "pParamBottom";
            this.pParamBottom.Size = new System.Drawing.Size(548, 151);
            this.pParamBottom.TabIndex = 1;
            // 
            // lRunCount
            // 
            this.lRunCount.AutoSize = true;
            this.lRunCount.Location = new System.Drawing.Point(164, 45);
            this.lRunCount.Name = "lRunCount";
            this.lRunCount.Size = new System.Drawing.Size(64, 15);
            this.lRunCount.TabIndex = 37;
            this.lRunCount.Text = "Run Count";
            this.toolTip.SetToolTip(this.lRunCount, "If run count is greater than 1, the model details and episodes of each run are ex" +
        "ported automatically.");
            // 
            // eRunNumber
            // 
            this.eRunNumber.Location = new System.Drawing.Point(234, 42);
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
            // 
            // eSkip
            // 
            this.eSkip.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eSkip.Location = new System.Drawing.Point(73, 68);
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
            this.lSkip.Location = new System.Drawing.Point(15, 68);
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
            this.edt.Location = new System.Drawing.Point(73, 94);
            this.edt.Name = "edt";
            this.edt.Size = new System.Drawing.Size(76, 23);
            this.edt.TabIndex = 35;
            this.toolTip.SetToolTip(this.edt, "(in milliseconds)");
            this.edt.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edt.ValueChanged += new System.EventHandler(this.edt_ValueChanged);
            // 
            // eTimeEnd
            // 
            this.eTimeEnd.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.eTimeEnd.Location = new System.Drawing.Point(73, 42);
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
            this.ldt.Location = new System.Drawing.Point(16, 94);
            this.ldt.Name = "ldt";
            this.ldt.Size = new System.Drawing.Size(19, 15);
            this.ldt.TabIndex = 32;
            this.ldt.Text = "Δt";
            // 
            // lRunTime
            // 
            this.lRunTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lRunTime.Location = new System.Drawing.Point(309, 42);
            this.lRunTime.Name = "lRunTime";
            this.lRunTime.Size = new System.Drawing.Size(224, 80);
            this.lRunTime.TabIndex = 31;
            // 
            // lStimulus
            // 
            this.lStimulus.AutoSize = true;
            this.lStimulus.Location = new System.Drawing.Point(168, 18);
            this.lStimulus.Name = "lStimulus";
            this.lStimulus.Size = new System.Drawing.Size(53, 15);
            this.lStimulus.TabIndex = 29;
            this.lStimulus.Text = "Stimulus";
            // 
            // ddStimulusMode
            // 
            this.ddStimulusMode.BackColor = System.Drawing.Color.White;
            this.ddStimulusMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddStimulusMode.FormattingEnabled = true;
            this.ddStimulusMode.Items.AddRange(new object[] {
            "Step",
            "Gaussian",
            "Ramp"});
            this.ddStimulusMode.Location = new System.Drawing.Point(234, 15);
            this.ddStimulusMode.Name = "ddStimulusMode";
            this.ddStimulusMode.Size = new System.Drawing.Size(63, 23);
            this.ddStimulusMode.TabIndex = 30;
            // 
            // linkExportOutput
            // 
            this.linkExportOutput.AutoSize = true;
            this.linkExportOutput.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkExportOutput.Location = new System.Drawing.Point(220, 96);
            this.linkExportOutput.Name = "linkExportOutput";
            this.linkExportOutput.Size = new System.Drawing.Size(82, 15);
            this.linkExportOutput.TabIndex = 27;
            this.linkExportOutput.TabStop = true;
            this.linkExportOutput.Text = "Export Output";
            this.linkExportOutput.Visible = false;
            this.linkExportOutput.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveRun_LinkClicked);
            // 
            // progressBarRun
            // 
            this.progressBarRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarRun.Location = new System.Drawing.Point(0, 128);
            this.progressBarRun.Name = "progressBarRun";
            this.progressBarRun.Size = new System.Drawing.Size(548, 23);
            this.progressBarRun.TabIndex = 26;
            this.progressBarRun.Visible = false;
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRun.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Location = new System.Drawing.Point(234, 68);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(63, 23);
            this.btnRun.TabIndex = 9;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lTimeEnd
            // 
            this.lTimeEnd.AutoSize = true;
            this.lTimeEnd.Location = new System.Drawing.Point(15, 45);
            this.lTimeEnd.Name = "lTimeEnd";
            this.lTimeEnd.Size = new System.Drawing.Size(56, 15);
            this.lTimeEnd.TabIndex = 3;
            this.lTimeEnd.Text = "Time End";
            // 
            // lRunParameters
            // 
            this.lRunParameters.AutoSize = true;
            this.lRunParameters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lRunParameters.Location = new System.Drawing.Point(16, 15);
            this.lRunParameters.Name = "lRunParameters";
            this.lRunParameters.Size = new System.Drawing.Size(133, 15);
            this.lRunParameters.TabIndex = 1;
            this.lRunParameters.Text = "Simulation Parameters";
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.linkLoadParam);
            this.pMain.Controls.Add(this.lParameters);
            this.pMain.Controls.Add(this.linkSaveParam);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(548, 34);
            this.pMain.TabIndex = 4;
            // 
            // linkLoadParam
            // 
            this.linkLoadParam.AutoSize = true;
            this.linkLoadParam.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
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
            this.linkSaveParam.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
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
            this.tabOutputs.Controls.Add(this.tabMNKinematics);
            this.tabOutputs.Controls.Add(this.tabAnimation);
            this.tabOutputs.Controls.Add(this.tabTemplateJSON);
            this.tabOutputs.Controls.Add(this.tabModelJSON);
            this.tabOutputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOutputs.Location = new System.Drawing.Point(0, 0);
            this.tabOutputs.Name = "tabOutputs";
            this.tabOutputs.SelectedIndex = 0;
            this.tabOutputs.Size = new System.Drawing.Size(792, 669);
            this.tabOutputs.TabIndex = 1;
            // 
            // tabPlot
            // 
            this.tabPlot.Controls.Add(this.tabPlotSub);
            this.tabPlot.Controls.Add(this.pPlot);
            this.tabPlot.Location = new System.Drawing.Point(4, 24);
            this.tabPlot.Name = "tabPlot";
            this.tabPlot.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlot.Size = new System.Drawing.Size(784, 641);
            this.tabPlot.TabIndex = 4;
            this.tabPlot.Text = "Plots";
            this.tabPlot.UseVisualStyleBackColor = true;
            // 
            // tabPlotSub
            // 
            this.tabPlotSub.Controls.Add(this.tPlotWindows);
            this.tabPlotSub.Controls.Add(this.tPlotHTML);
            this.tabPlotSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPlotSub.Location = new System.Drawing.Point(3, 143);
            this.tabPlotSub.Name = "tabPlotSub";
            this.tabPlotSub.SelectedIndex = 0;
            this.tabPlotSub.Size = new System.Drawing.Size(778, 495);
            this.tabPlotSub.TabIndex = 6;
            // 
            // tPlotWindows
            // 
            this.tPlotWindows.Controls.Add(this.splitPlotWindows);
            this.tPlotWindows.Location = new System.Drawing.Point(4, 24);
            this.tPlotWindows.Name = "tPlotWindows";
            this.tPlotWindows.Padding = new System.Windows.Forms.Padding(3);
            this.tPlotWindows.Size = new System.Drawing.Size(770, 467);
            this.tPlotWindows.TabIndex = 0;
            this.tPlotWindows.Text = "Image Plots";
            // 
            // splitPlotWindows
            // 
            this.splitPlotWindows.BackColor = System.Drawing.Color.White;
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
            this.splitPlotWindows.Size = new System.Drawing.Size(764, 461);
            this.splitPlotWindows.SplitterDistance = 355;
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
            this.tPlotHTML.Size = new System.Drawing.Size(770, 467);
            this.tPlotHTML.TabIndex = 1;
            this.tPlotHTML.Text = "HTML Plots";
            this.tPlotHTML.UseVisualStyleBackColor = true;
            // 
            // webViewPlot
            // 
            this.webViewPlot.AllowExternalDrop = true;
            this.webViewPlot.BackColor = System.Drawing.Color.White;
            this.webViewPlot.CreationProperties = null;
            this.webViewPlot.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewPlot.Location = new System.Drawing.Point(3, 3);
            this.webViewPlot.Name = "webViewPlot";
            this.webViewPlot.Size = new System.Drawing.Size(764, 461);
            this.webViewPlot.TabIndex = 1;
            this.webViewPlot.ZoomFactor = 1D;
            this.webViewPlot.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            // 
            // pPlot
            // 
            this.pPlot.BackColor = System.Drawing.Color.White;
            this.pPlot.Controls.Add(this.pLinePlots);
            this.pPlot.Controls.Add(this.ePlotHeight);
            this.pPlot.Controls.Add(this.ePlotWidth);
            this.pPlot.Controls.Add(this.lPlotWidth);
            this.pPlot.Controls.Add(this.lpx1);
            this.pPlot.Controls.Add(this.lPlotHeight);
            this.pPlot.Controls.Add(this.lpx2);
            this.pPlot.Controls.Add(this.lNumberOfPlots);
            this.pPlot.Controls.Add(this.btnPlotWindows);
            this.pPlot.Controls.Add(this.ddPlotSagittal);
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
            this.pPlot.Controls.Add(this.lSagittal);
            this.pPlot.Controls.Add(this.ePlotSomiteSelection);
            this.pPlot.Controls.Add(this.lPlotPool);
            this.pPlot.Controls.Add(this.ddPlotPools);
            this.pPlot.Controls.Add(this.lPlotStart);
            this.pPlot.Controls.Add(this.lms1);
            this.pPlot.Controls.Add(this.lPlotEnd);
            this.pPlot.Controls.Add(this.lms2);
            this.pPlot.Controls.Add(this.lPlotPlot);
            this.pPlot.Controls.Add(this.ddPlot);
            this.pPlot.Dock = System.Windows.Forms.DockStyle.Top;
            this.pPlot.Location = new System.Drawing.Point(3, 3);
            this.pPlot.Name = "pPlot";
            this.pPlot.Size = new System.Drawing.Size(778, 140);
            this.pPlot.TabIndex = 5;
            // 
            // pLinePlots
            // 
            this.pLinePlots.BackColor = System.Drawing.Color.LightGray;
            this.pLinePlots.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLinePlots.Location = new System.Drawing.Point(0, 139);
            this.pLinePlots.Name = "pLinePlots";
            this.pLinePlots.Size = new System.Drawing.Size(778, 1);
            this.pLinePlots.TabIndex = 58;
            // 
            // ePlotHeight
            // 
            this.ePlotHeight.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ePlotHeight.Location = new System.Drawing.Point(494, 28);
            this.ePlotHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.ePlotHeight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ePlotHeight.Name = "ePlotHeight";
            this.ePlotHeight.Size = new System.Drawing.Size(48, 23);
            this.ePlotHeight.TabIndex = 46;
            this.ePlotHeight.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.ePlotHeight, "for each plot");
            this.ePlotHeight.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // ePlotWidth
            // 
            this.ePlotWidth.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ePlotWidth.Location = new System.Drawing.Point(494, 3);
            this.ePlotWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.ePlotWidth.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.ePlotWidth.Name = "ePlotWidth";
            this.ePlotWidth.Size = new System.Drawing.Size(48, 23);
            this.ePlotWidth.TabIndex = 45;
            this.ePlotWidth.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.ePlotWidth, "for each plot - 15% of it will be used for legend");
            this.ePlotWidth.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // lPlotWidth
            // 
            this.lPlotWidth.AutoSize = true;
            this.lPlotWidth.Location = new System.Drawing.Point(449, 8);
            this.lPlotWidth.Name = "lPlotWidth";
            this.lPlotWidth.Size = new System.Drawing.Size(39, 15);
            this.lPlotWidth.TabIndex = 54;
            this.lPlotWidth.Text = "Width";
            this.toolTip.SetToolTip(this.lPlotWidth, "for each plot - 15% of it will be used for legend");
            // 
            // lpx1
            // 
            this.lpx1.AutoSize = true;
            this.lpx1.Location = new System.Drawing.Point(548, 5);
            this.lpx1.Name = "lpx1";
            this.lpx1.Size = new System.Drawing.Size(28, 15);
            this.lpx1.TabIndex = 55;
            this.lpx1.Text = "(px)";
            // 
            // lPlotHeight
            // 
            this.lPlotHeight.AutoSize = true;
            this.lPlotHeight.Location = new System.Drawing.Point(449, 31);
            this.lPlotHeight.Name = "lPlotHeight";
            this.lPlotHeight.Size = new System.Drawing.Size(43, 15);
            this.lPlotHeight.TabIndex = 56;
            this.lPlotHeight.Text = "Height";
            this.toolTip.SetToolTip(this.lPlotHeight, "for each plot");
            // 
            // lpx2
            // 
            this.lpx2.AutoSize = true;
            this.lpx2.Location = new System.Drawing.Point(548, 31);
            this.lpx2.Name = "lpx2";
            this.lpx2.Size = new System.Drawing.Size(28, 15);
            this.lpx2.TabIndex = 57;
            this.lpx2.Text = "(px)";
            // 
            // lNumberOfPlots
            // 
            this.lNumberOfPlots.AutoSize = true;
            this.lNumberOfPlots.Location = new System.Drawing.Point(449, 62);
            this.lNumberOfPlots.Name = "lNumberOfPlots";
            this.lNumberOfPlots.Size = new System.Drawing.Size(63, 15);
            this.lNumberOfPlots.TabIndex = 53;
            this.lNumberOfPlots.Tag = "# of plots: ";
            this.lNumberOfPlots.Text = "# of plots: ";
            this.lNumberOfPlots.Visible = false;
            // 
            // btnPlotWindows
            // 
            this.btnPlotWindows.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPlotWindows.Enabled = false;
            this.btnPlotWindows.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnPlotWindows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlotWindows.Location = new System.Drawing.Point(449, 83);
            this.btnPlotWindows.Name = "btnPlotWindows";
            this.btnPlotWindows.Size = new System.Drawing.Size(75, 23);
            this.btnPlotWindows.TabIndex = 50;
            this.btnPlotWindows.Text = "Plot";
            this.btnPlotWindows.UseVisualStyleBackColor = false;
            this.btnPlotWindows.Click += new System.EventHandler(this.btnPlotWindows_Click);
            // 
            // ddPlotSagittal
            // 
            this.ddPlotSagittal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotSagittal.FormattingEnabled = true;
            this.ddPlotSagittal.Location = new System.Drawing.Point(280, 32);
            this.ddPlotSagittal.Name = "ddPlotSagittal";
            this.ddPlotSagittal.Size = new System.Drawing.Size(160, 23);
            this.ddPlotSagittal.TabIndex = 35;
            this.ddPlotSagittal.SelectedIndexChanged += new System.EventHandler(this.ddPlotSagittal_SelectedIndexChanged);
            // 
            // linkSavePlots
            // 
            this.linkSavePlots.AutoSize = true;
            this.linkSavePlots.Enabled = false;
            this.linkSavePlots.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSavePlots.Location = new System.Drawing.Point(527, 88);
            this.linkSavePlots.Name = "linkSavePlots";
            this.linkSavePlots.Size = new System.Drawing.Size(96, 15);
            this.linkSavePlots.TabIndex = 51;
            this.linkSavePlots.TabStop = true;
            this.linkSavePlots.Text = "Save Plot Images";
            this.linkSavePlots.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSavePlots_LinkClicked);
            // 
            // btnPlotHTML
            // 
            this.btnPlotHTML.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPlotHTML.Enabled = false;
            this.btnPlotHTML.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnPlotHTML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlotHTML.ForeColor = System.Drawing.Color.Black;
            this.btnPlotHTML.Location = new System.Drawing.Point(449, 110);
            this.btnPlotHTML.Name = "btnPlotHTML";
            this.btnPlotHTML.Size = new System.Drawing.Size(75, 23);
            this.btnPlotHTML.TabIndex = 52;
            this.btnPlotHTML.Text = "Plot HTML";
            this.toolTip.SetToolTip(this.btnPlotHTML, "Creates interactive plots - Needs Optimization");
            this.btnPlotHTML.UseVisualStyleBackColor = false;
            this.btnPlotHTML.Click += new System.EventHandler(this.btnPlotHTML_Click);
            // 
            // ddPlotCellSelection
            // 
            this.ddPlotCellSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotCellSelection.FormattingEnabled = true;
            this.ddPlotCellSelection.Location = new System.Drawing.Point(280, 110);
            this.ddPlotCellSelection.Name = "ddPlotCellSelection";
            this.ddPlotCellSelection.Size = new System.Drawing.Size(113, 23);
            this.ddPlotCellSelection.TabIndex = 41;
            this.ddPlotCellSelection.SelectedIndexChanged += new System.EventHandler(this.ddPlotCellSelection_SelectedIndexChanged);
            // 
            // lPlotCells
            // 
            this.lPlotCells.AutoSize = true;
            this.lPlotCells.Location = new System.Drawing.Point(217, 112);
            this.lPlotCells.Name = "lPlotCells";
            this.lPlotCells.Size = new System.Drawing.Size(32, 15);
            this.lPlotCells.TabIndex = 45;
            this.lPlotCells.Text = "Cells";
            // 
            // ePlotCellSelection
            // 
            this.ePlotCellSelection.Location = new System.Drawing.Point(397, 110);
            this.ePlotCellSelection.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ePlotCellSelection.Name = "ePlotCellSelection";
            this.ePlotCellSelection.Size = new System.Drawing.Size(43, 23);
            this.ePlotCellSelection.TabIndex = 44;
            this.ePlotCellSelection.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ePlotCellSelection.ValueChanged += new System.EventHandler(this.ePlotCellSelection_ValueChanged);
            // 
            // ddPlotSomiteSelection
            // 
            this.ddPlotSomiteSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotSomiteSelection.FormattingEnabled = true;
            this.ddPlotSomiteSelection.Location = new System.Drawing.Point(280, 84);
            this.ddPlotSomiteSelection.Name = "ddPlotSomiteSelection";
            this.ddPlotSomiteSelection.Size = new System.Drawing.Size(113, 23);
            this.ddPlotSomiteSelection.TabIndex = 37;
            this.ddPlotSomiteSelection.SelectedIndexChanged += new System.EventHandler(this.ddPlotSomiteSelection_SelectedIndexChanged);
            // 
            // lPlotSomites
            // 
            this.lPlotSomites.AutoSize = true;
            this.lPlotSomites.Location = new System.Drawing.Point(217, 86);
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
            this.ePlotEnd.Location = new System.Drawing.Point(74, 32);
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
            this.ePlotStart.Location = new System.Drawing.Point(74, 6);
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
            // lSagittal
            // 
            this.lSagittal.AutoSize = true;
            this.lSagittal.Location = new System.Drawing.Point(217, 34);
            this.lSagittal.Name = "lSagittal";
            this.lSagittal.Size = new System.Drawing.Size(46, 15);
            this.lSagittal.TabIndex = 34;
            this.lSagittal.Text = "Sagittal";
            // 
            // ePlotSomiteSelection
            // 
            this.ePlotSomiteSelection.Location = new System.Drawing.Point(397, 84);
            this.ePlotSomiteSelection.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ePlotSomiteSelection.Name = "ePlotSomiteSelection";
            this.ePlotSomiteSelection.Size = new System.Drawing.Size(43, 23);
            this.ePlotSomiteSelection.TabIndex = 39;
            this.ePlotSomiteSelection.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ePlotSomiteSelection.ValueChanged += new System.EventHandler(this.ePlotSomiteSelection_ValueChanged);
            // 
            // lPlotPool
            // 
            this.lPlotPool.AutoSize = true;
            this.lPlotPool.Location = new System.Drawing.Point(217, 60);
            this.lPlotPool.Name = "lPlotPool";
            this.lPlotPool.Size = new System.Drawing.Size(31, 15);
            this.lPlotPool.TabIndex = 26;
            this.lPlotPool.Text = "Pool";
            // 
            // ddPlotPools
            // 
            this.ddPlotPools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotPools.FormattingEnabled = true;
            this.ddPlotPools.Location = new System.Drawing.Point(280, 58);
            this.ddPlotPools.Name = "ddPlotPools";
            this.ddPlotPools.Size = new System.Drawing.Size(160, 23);
            this.ddPlotPools.TabIndex = 36;
            this.ddPlotPools.SelectedIndexChanged += new System.EventHandler(this.ddPlotPools_SelectedIndexChanged);
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
            // lms1
            // 
            this.lms1.AutoSize = true;
            this.lms1.Location = new System.Drawing.Point(180, 8);
            this.lms1.Name = "lms1";
            this.lms1.Size = new System.Drawing.Size(31, 15);
            this.lms1.TabIndex = 13;
            this.lms1.Text = "(ms)";
            // 
            // lPlotEnd
            // 
            this.lPlotEnd.AutoSize = true;
            this.lPlotEnd.Location = new System.Drawing.Point(12, 34);
            this.lPlotEnd.Name = "lPlotEnd";
            this.lPlotEnd.Size = new System.Drawing.Size(51, 15);
            this.lPlotEnd.TabIndex = 14;
            this.lPlotEnd.Text = "Plot End";
            // 
            // lms2
            // 
            this.lms2.AutoSize = true;
            this.lms2.Location = new System.Drawing.Point(180, 37);
            this.lms2.Name = "lms2";
            this.lms2.Size = new System.Drawing.Size(31, 15);
            this.lms2.TabIndex = 16;
            this.lms2.Text = "(ms)";
            // 
            // lPlotPlot
            // 
            this.lPlotPlot.AutoSize = true;
            this.lPlotPlot.Location = new System.Drawing.Point(217, 8);
            this.lPlotPlot.Name = "lPlotPlot";
            this.lPlotPlot.Size = new System.Drawing.Size(28, 15);
            this.lPlotPlot.TabIndex = 19;
            this.lPlotPlot.Text = "Plot";
            // 
            // ddPlot
            // 
            this.ddPlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlot.FormattingEnabled = true;
            this.ddPlot.Location = new System.Drawing.Point(280, 6);
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
            this.tab2DModel.Size = new System.Drawing.Size(784, 641);
            this.tab2DModel.TabIndex = 2;
            this.tab2DModel.Text = "2D Model";
            this.tab2DModel.UseVisualStyleBackColor = true;
            // 
            // webView2DModel
            // 
            this.webView2DModel.AllowExternalDrop = true;
            this.webView2DModel.BackColor = System.Drawing.Color.White;
            this.webView2DModel.CreationProperties = null;
            this.webView2DModel.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2DModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView2DModel.Location = new System.Drawing.Point(0, 40);
            this.webView2DModel.Name = "webView2DModel";
            this.webView2DModel.Padding = new System.Windows.Forms.Padding(10);
            this.webView2DModel.Size = new System.Drawing.Size(784, 601);
            this.webView2DModel.TabIndex = 1;
            this.webView2DModel.ZoomFactor = 1D;
            this.webView2DModel.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            // 
            // p2DModel
            // 
            this.p2DModel.BackColor = System.Drawing.Color.White;
            this.p2DModel.Controls.Add(this.pLine2D);
            this.p2DModel.Controls.Add(this.linkSaveHTML2D);
            this.p2DModel.Controls.Add(this.btnGenerate2DModel);
            this.p2DModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.p2DModel.Location = new System.Drawing.Point(0, 0);
            this.p2DModel.Name = "p2DModel";
            this.p2DModel.Size = new System.Drawing.Size(784, 40);
            this.p2DModel.TabIndex = 2;
            // 
            // pLine2D
            // 
            this.pLine2D.BackColor = System.Drawing.Color.LightGray;
            this.pLine2D.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLine2D.Location = new System.Drawing.Point(0, 39);
            this.pLine2D.Name = "pLine2D";
            this.pLine2D.Size = new System.Drawing.Size(784, 1);
            this.pLine2D.TabIndex = 24;
            // 
            // linkSaveHTML2D
            // 
            this.linkSaveHTML2D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveHTML2D.AutoSize = true;
            this.linkSaveHTML2D.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveHTML2D.Location = new System.Drawing.Point(711, 13);
            this.linkSaveHTML2D.Name = "linkSaveHTML2D";
            this.linkSaveHTML2D.Size = new System.Drawing.Size(66, 15);
            this.linkSaveHTML2D.TabIndex = 23;
            this.linkSaveHTML2D.TabStop = true;
            this.linkSaveHTML2D.Text = "Save HTML";
            this.linkSaveHTML2D.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveHTML2D_LinkClicked);
            // 
            // btnGenerate2DModel
            // 
            this.btnGenerate2DModel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGenerate2DModel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnGenerate2DModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate2DModel.Location = new System.Drawing.Point(6, 9);
            this.btnGenerate2DModel.Name = "btnGenerate2DModel";
            this.btnGenerate2DModel.Size = new System.Drawing.Size(120, 23);
            this.btnGenerate2DModel.TabIndex = 22;
            this.btnGenerate2DModel.Text = "Generate 2D Model";
            this.btnGenerate2DModel.UseVisualStyleBackColor = false;
            this.btnGenerate2DModel.Click += new System.EventHandler(this.btnGenerate2DModel_Click);
            // 
            // tab3DModel
            // 
            this.tab3DModel.Controls.Add(this.webView3DModel);
            this.tab3DModel.Controls.Add(this.p3DModel);
            this.tab3DModel.Location = new System.Drawing.Point(4, 24);
            this.tab3DModel.Name = "tab3DModel";
            this.tab3DModel.Size = new System.Drawing.Size(784, 641);
            this.tab3DModel.TabIndex = 0;
            this.tab3DModel.Text = "3D Model";
            this.tab3DModel.UseVisualStyleBackColor = true;
            // 
            // webView3DModel
            // 
            this.webView3DModel.AllowExternalDrop = true;
            this.webView3DModel.BackColor = System.Drawing.Color.White;
            this.webView3DModel.CreationProperties = null;
            this.webView3DModel.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView3DModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView3DModel.Location = new System.Drawing.Point(0, 40);
            this.webView3DModel.Name = "webView3DModel";
            this.webView3DModel.Padding = new System.Windows.Forms.Padding(10);
            this.webView3DModel.Size = new System.Drawing.Size(784, 601);
            this.webView3DModel.TabIndex = 0;
            this.webView3DModel.ZoomFactor = 1D;
            this.webView3DModel.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            // 
            // p3DModel
            // 
            this.p3DModel.Controls.Add(this.pLine3D);
            this.p3DModel.Controls.Add(this.ddSomites);
            this.p3DModel.Controls.Add(this.lSomites);
            this.p3DModel.Controls.Add(this.linkSaveHTML3D);
            this.p3DModel.Controls.Add(this.dd3DModelType);
            this.p3DModel.Controls.Add(this.btnGenerate3DModel);
            this.p3DModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.p3DModel.Location = new System.Drawing.Point(0, 0);
            this.p3DModel.Name = "p3DModel";
            this.p3DModel.Size = new System.Drawing.Size(784, 40);
            this.p3DModel.TabIndex = 3;
            // 
            // pLine3D
            // 
            this.pLine3D.BackColor = System.Drawing.Color.LightGray;
            this.pLine3D.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLine3D.Location = new System.Drawing.Point(0, 39);
            this.pLine3D.Name = "pLine3D";
            this.pLine3D.Size = new System.Drawing.Size(784, 1);
            this.pLine3D.TabIndex = 30;
            // 
            // ddSomites
            // 
            this.ddSomites.BackColor = System.Drawing.Color.White;
            this.ddSomites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSomites.FormattingEnabled = true;
            this.ddSomites.Items.AddRange(new object[] {
            "All Somites",
            "Custom range"});
            this.ddSomites.Location = new System.Drawing.Point(200, 9);
            this.ddSomites.Name = "ddSomites";
            this.ddSomites.Size = new System.Drawing.Size(121, 23);
            this.ddSomites.TabIndex = 29;
            this.toolTip.SetToolTip(this.ddSomites, "Enter a number or a range (eg. \"1\" or \"0-5\")");
            this.ddSomites.SelectedIndexChanged += new System.EventHandler(this.ddSomites_SelectedIndexChanged);
            // 
            // lSomites
            // 
            this.lSomites.AutoSize = true;
            this.lSomites.Location = new System.Drawing.Point(145, 13);
            this.lSomites.Name = "lSomites";
            this.lSomites.Size = new System.Drawing.Size(49, 15);
            this.lSomites.TabIndex = 28;
            this.lSomites.Text = "Somites";
            // 
            // linkSaveHTML3D
            // 
            this.linkSaveHTML3D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveHTML3D.AutoSize = true;
            this.linkSaveHTML3D.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveHTML3D.Location = new System.Drawing.Point(711, 13);
            this.linkSaveHTML3D.Name = "linkSaveHTML3D";
            this.linkSaveHTML3D.Size = new System.Drawing.Size(66, 15);
            this.linkSaveHTML3D.TabIndex = 27;
            this.linkSaveHTML3D.TabStop = true;
            this.linkSaveHTML3D.Text = "Save HTML";
            this.linkSaveHTML3D.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveHTML3D_LinkClicked);
            // 
            // dd3DModelType
            // 
            this.dd3DModelType.BackColor = System.Drawing.Color.White;
            this.dd3DModelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dd3DModelType.FormattingEnabled = true;
            this.dd3DModelType.Items.AddRange(new object[] {
            "Gap Jnc",
            "Chem Jnc",
            "Gap+Chem",
            "Gap/Chem"});
            this.dd3DModelType.Location = new System.Drawing.Point(6, 9);
            this.dd3DModelType.Name = "dd3DModelType";
            this.dd3DModelType.Size = new System.Drawing.Size(121, 23);
            this.dd3DModelType.TabIndex = 25;
            // 
            // btnGenerate3DModel
            // 
            this.btnGenerate3DModel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGenerate3DModel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnGenerate3DModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate3DModel.Location = new System.Drawing.Point(327, 9);
            this.btnGenerate3DModel.Name = "btnGenerate3DModel";
            this.btnGenerate3DModel.Size = new System.Drawing.Size(120, 23);
            this.btnGenerate3DModel.TabIndex = 26;
            this.btnGenerate3DModel.Text = "Generate 3D Model";
            this.btnGenerate3DModel.UseVisualStyleBackColor = false;
            this.btnGenerate3DModel.Click += new System.EventHandler(this.btnGenerate3DModel_Click);
            // 
            // tabMNKinematics
            // 
            this.tabMNKinematics.Controls.Add(this.splitKinematics);
            this.tabMNKinematics.Controls.Add(this.pMNKinematicsTop);
            this.tabMNKinematics.Location = new System.Drawing.Point(4, 24);
            this.tabMNKinematics.Name = "tabMNKinematics";
            this.tabMNKinematics.Size = new System.Drawing.Size(784, 641);
            this.tabMNKinematics.TabIndex = 7;
            this.tabMNKinematics.Text = "MN Based Kinematics";
            this.tabMNKinematics.UseVisualStyleBackColor = true;
            // 
            // splitKinematics
            // 
            this.splitKinematics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitKinematics.Location = new System.Drawing.Point(0, 90);
            this.splitKinematics.Name = "splitKinematics";
            // 
            // splitKinematics.Panel1
            // 
            this.splitKinematics.Panel1.Controls.Add(this.webViewSummaryV);
            this.splitKinematics.Panel1.Controls.Add(this.pLineMNKinematicsRight);
            // 
            // splitKinematics.Panel2
            // 
            this.splitKinematics.Panel2.Controls.Add(this.eEpisodesRight);
            this.splitKinematics.Panel2.Controls.Add(this.eEpisodesLeft);
            this.splitKinematics.Panel2.Controls.Add(this.pLineMNKinematicsLeft);
            this.splitKinematics.Panel2.SizeChanged += new System.EventHandler(this.splitContainer1_Panel2_SizeChanged);
            this.splitKinematics.Size = new System.Drawing.Size(784, 551);
            this.splitKinematics.SplitterDistance = 533;
            this.splitKinematics.TabIndex = 8;
            // 
            // webViewSummaryV
            // 
            this.webViewSummaryV.AllowExternalDrop = true;
            this.webViewSummaryV.BackColor = System.Drawing.Color.White;
            this.webViewSummaryV.CreationProperties = null;
            this.webViewSummaryV.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewSummaryV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewSummaryV.Location = new System.Drawing.Point(0, 0);
            this.webViewSummaryV.Name = "webViewSummaryV";
            this.webViewSummaryV.Size = new System.Drawing.Size(532, 551);
            this.webViewSummaryV.TabIndex = 6;
            this.webViewSummaryV.ZoomFactor = 1D;
            this.webViewSummaryV.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            // 
            // pLineMNKinematicsRight
            // 
            this.pLineMNKinematicsRight.BackColor = System.Drawing.Color.LightGray;
            this.pLineMNKinematicsRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pLineMNKinematicsRight.Location = new System.Drawing.Point(532, 0);
            this.pLineMNKinematicsRight.Name = "pLineMNKinematicsRight";
            this.pLineMNKinematicsRight.Size = new System.Drawing.Size(1, 551);
            this.pLineMNKinematicsRight.TabIndex = 7;
            // 
            // eEpisodesRight
            // 
            this.eEpisodesRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eEpisodesRight.Location = new System.Drawing.Point(115, 0);
            this.eEpisodesRight.Name = "eEpisodesRight";
            this.eEpisodesRight.Size = new System.Drawing.Size(132, 551);
            this.eEpisodesRight.TabIndex = 1;
            this.eEpisodesRight.Text = "";
            // 
            // eEpisodesLeft
            // 
            this.eEpisodesLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.eEpisodesLeft.Location = new System.Drawing.Point(1, 0);
            this.eEpisodesLeft.Name = "eEpisodesLeft";
            this.eEpisodesLeft.Size = new System.Drawing.Size(114, 551);
            this.eEpisodesLeft.TabIndex = 0;
            this.eEpisodesLeft.Text = "";
            // 
            // pLineMNKinematicsLeft
            // 
            this.pLineMNKinematicsLeft.BackColor = System.Drawing.Color.LightGray;
            this.pLineMNKinematicsLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pLineMNKinematicsLeft.Location = new System.Drawing.Point(0, 0);
            this.pLineMNKinematicsLeft.Name = "pLineMNKinematicsLeft";
            this.pLineMNKinematicsLeft.Size = new System.Drawing.Size(1, 551);
            this.pLineMNKinematicsLeft.TabIndex = 2;
            // 
            // pMNKinematicsTop
            // 
            this.pMNKinematicsTop.BackColor = System.Drawing.Color.White;
            this.pMNKinematicsTop.Controls.Add(this.pLineMNKinematics);
            this.pMNKinematicsTop.Controls.Add(this.ePlotKinematicsHeight);
            this.pMNKinematicsTop.Controls.Add(this.ePlotKinematicsWidth);
            this.pMNKinematicsTop.Controls.Add(this.lPlotKinematicsWidth);
            this.pMNKinematicsTop.Controls.Add(this.label3);
            this.pMNKinematicsTop.Controls.Add(this.lPlotKinematicsHeight);
            this.pMNKinematicsTop.Controls.Add(this.label8);
            this.pMNKinematicsTop.Controls.Add(this.label7);
            this.pMNKinematicsTop.Controls.Add(this.eKinematicsSomite);
            this.pMNKinematicsTop.Controls.Add(this.btnGenerateEpisodes);
            this.pMNKinematicsTop.Controls.Add(this.label2);
            this.pMNKinematicsTop.Controls.Add(this.eKinematicsEpisodeBreak);
            this.pMNKinematicsTop.Controls.Add(this.lBurstBreak);
            this.pMNKinematicsTop.Controls.Add(this.eKinematicsBurstBreak);
            this.pMNKinematicsTop.Controls.Add(this.lKinematicsTimes);
            this.pMNKinematicsTop.Controls.Add(this.label4);
            this.pMNKinematicsTop.Controls.Add(this.label6);
            this.pMNKinematicsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMNKinematicsTop.Location = new System.Drawing.Point(0, 0);
            this.pMNKinematicsTop.Name = "pMNKinematicsTop";
            this.pMNKinematicsTop.Size = new System.Drawing.Size(784, 90);
            this.pMNKinematicsTop.TabIndex = 7;
            // 
            // pLineMNKinematics
            // 
            this.pLineMNKinematics.BackColor = System.Drawing.Color.LightGray;
            this.pLineMNKinematics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineMNKinematics.Location = new System.Drawing.Point(0, 89);
            this.pLineMNKinematics.Name = "pLineMNKinematics";
            this.pLineMNKinematics.Size = new System.Drawing.Size(784, 1);
            this.pLineMNKinematics.TabIndex = 64;
            // 
            // ePlotKinematicsHeight
            // 
            this.ePlotKinematicsHeight.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ePlotKinematicsHeight.Location = new System.Drawing.Point(244, 33);
            this.ePlotKinematicsHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.ePlotKinematicsHeight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ePlotKinematicsHeight.Name = "ePlotKinematicsHeight";
            this.ePlotKinematicsHeight.Size = new System.Drawing.Size(48, 23);
            this.ePlotKinematicsHeight.TabIndex = 59;
            this.ePlotKinematicsHeight.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.ePlotKinematicsHeight, "for each plot");
            this.ePlotKinematicsHeight.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // ePlotKinematicsWidth
            // 
            this.ePlotKinematicsWidth.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ePlotKinematicsWidth.Location = new System.Drawing.Point(244, 6);
            this.ePlotKinematicsWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.ePlotKinematicsWidth.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.ePlotKinematicsWidth.Name = "ePlotKinematicsWidth";
            this.ePlotKinematicsWidth.Size = new System.Drawing.Size(48, 23);
            this.ePlotKinematicsWidth.TabIndex = 58;
            this.ePlotKinematicsWidth.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.ePlotKinematicsWidth, "for each plot - 15% of it will be used for legend");
            this.ePlotKinematicsWidth.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // lPlotKinematicsWidth
            // 
            this.lPlotKinematicsWidth.AutoSize = true;
            this.lPlotKinematicsWidth.Location = new System.Drawing.Point(199, 9);
            this.lPlotKinematicsWidth.Name = "lPlotKinematicsWidth";
            this.lPlotKinematicsWidth.Size = new System.Drawing.Size(39, 15);
            this.lPlotKinematicsWidth.TabIndex = 60;
            this.lPlotKinematicsWidth.Text = "Width";
            this.toolTip.SetToolTip(this.lPlotKinematicsWidth, "for each plot - 15% of it will be used for legend");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 15);
            this.label3.TabIndex = 61;
            this.label3.Text = "(px)";
            // 
            // lPlotKinematicsHeight
            // 
            this.lPlotKinematicsHeight.AutoSize = true;
            this.lPlotKinematicsHeight.Location = new System.Drawing.Point(199, 36);
            this.lPlotKinematicsHeight.Name = "lPlotKinematicsHeight";
            this.lPlotKinematicsHeight.Size = new System.Drawing.Size(43, 15);
            this.lPlotKinematicsHeight.TabIndex = 62;
            this.lPlotKinematicsHeight.Text = "Height";
            this.toolTip.SetToolTip(this.lPlotKinematicsHeight, "for each plot");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(298, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 15);
            this.label8.TabIndex = 63;
            this.label8.Text = "(px)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 15);
            this.label7.TabIndex = 43;
            this.label7.Text = "# of Somites";
            // 
            // eKinematicsSomite
            // 
            this.eKinematicsSomite.Location = new System.Drawing.Point(94, 60);
            this.eKinematicsSomite.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eKinematicsSomite.Name = "eKinematicsSomite";
            this.eKinematicsSomite.Size = new System.Drawing.Size(53, 23);
            this.eKinematicsSomite.TabIndex = 42;
            this.eKinematicsSomite.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnGenerateEpisodes
            // 
            this.btnGenerateEpisodes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGenerateEpisodes.Enabled = false;
            this.btnGenerateEpisodes.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnGenerateEpisodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateEpisodes.Location = new System.Drawing.Point(199, 60);
            this.btnGenerateEpisodes.Name = "btnGenerateEpisodes";
            this.btnGenerateEpisodes.Size = new System.Drawing.Size(115, 23);
            this.btnGenerateEpisodes.TabIndex = 29;
            this.btnGenerateEpisodes.Text = "Episode from MNs";
            this.btnGenerateEpisodes.UseVisualStyleBackColor = false;
            this.btnGenerateEpisodes.Click += new System.EventHandler(this.btnGenerateEpisodes_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 41;
            this.label2.Text = "Episode Break";
            // 
            // eKinematicsEpisodeBreak
            // 
            this.eKinematicsEpisodeBreak.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eKinematicsEpisodeBreak.Location = new System.Drawing.Point(94, 33);
            this.eKinematicsEpisodeBreak.Name = "eKinematicsEpisodeBreak";
            this.eKinematicsEpisodeBreak.Size = new System.Drawing.Size(53, 23);
            this.eKinematicsEpisodeBreak.TabIndex = 40;
            this.toolTip.SetToolTip(this.eKinematicsEpisodeBreak, "(in milliseconds)");
            this.eKinematicsEpisodeBreak.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lBurstBreak
            // 
            this.lBurstBreak.AutoSize = true;
            this.lBurstBreak.Location = new System.Drawing.Point(6, 9);
            this.lBurstBreak.Name = "lBurstBreak";
            this.lBurstBreak.Size = new System.Drawing.Size(66, 15);
            this.lBurstBreak.TabIndex = 39;
            this.lBurstBreak.Text = "Burst Break";
            // 
            // eKinematicsBurstBreak
            // 
            this.eKinematicsBurstBreak.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.eKinematicsBurstBreak.Location = new System.Drawing.Point(94, 6);
            this.eKinematicsBurstBreak.Name = "eKinematicsBurstBreak";
            this.eKinematicsBurstBreak.Size = new System.Drawing.Size(53, 23);
            this.eKinematicsBurstBreak.TabIndex = 38;
            this.toolTip.SetToolTip(this.eKinematicsBurstBreak, "(in milliseconds)");
            this.eKinematicsBurstBreak.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lKinematicsTimes
            // 
            this.lKinematicsTimes.Location = new System.Drawing.Point(330, 63);
            this.lKinematicsTimes.Name = "lKinematicsTimes";
            this.lKinematicsTimes.Size = new System.Drawing.Size(294, 19);
            this.lKinematicsTimes.TabIndex = 33;
            this.lKinematicsTimes.Text = "Last kinematics:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "(ms)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "(ms)";
            // 
            // tabAnimation
            // 
            this.tabAnimation.Controls.Add(this.webViewAnimation);
            this.tabAnimation.Controls.Add(this.pAnimation);
            this.tabAnimation.Location = new System.Drawing.Point(4, 24);
            this.tabAnimation.Name = "tabAnimation";
            this.tabAnimation.Size = new System.Drawing.Size(784, 641);
            this.tabAnimation.TabIndex = 3;
            this.tabAnimation.Text = "Animation";
            this.tabAnimation.UseVisualStyleBackColor = true;
            // 
            // webViewAnimation
            // 
            this.webViewAnimation.AllowExternalDrop = true;
            this.webViewAnimation.BackColor = System.Drawing.Color.White;
            this.webViewAnimation.CreationProperties = null;
            this.webViewAnimation.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewAnimation.Location = new System.Drawing.Point(0, 65);
            this.webViewAnimation.Name = "webViewAnimation";
            this.webViewAnimation.Size = new System.Drawing.Size(784, 576);
            this.webViewAnimation.TabIndex = 2;
            this.webViewAnimation.ZoomFactor = 1D;
            this.webViewAnimation.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            // 
            // pAnimation
            // 
            this.pAnimation.BackColor = System.Drawing.Color.White;
            this.pAnimation.Controls.Add(this.pLineAnimation);
            this.pAnimation.Controls.Add(this.eAnimationdt);
            this.pAnimation.Controls.Add(this.lAnimationdt);
            this.pAnimation.Controls.Add(this.linkSaveAnimationCSV);
            this.pAnimation.Controls.Add(this.lAnimationTime);
            this.pAnimation.Controls.Add(this.eAnimationEnd);
            this.pAnimation.Controls.Add(this.eAnimationStart);
            this.pAnimation.Controls.Add(this.linkSaveAnimationHTML);
            this.pAnimation.Controls.Add(this.btnAnimate);
            this.pAnimation.Controls.Add(this.lAnimationStart);
            this.pAnimation.Controls.Add(this.lms3);
            this.pAnimation.Controls.Add(this.lAnimationEnd);
            this.pAnimation.Controls.Add(this.lms4);
            this.pAnimation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pAnimation.Location = new System.Drawing.Point(0, 0);
            this.pAnimation.Name = "pAnimation";
            this.pAnimation.Size = new System.Drawing.Size(784, 65);
            this.pAnimation.TabIndex = 5;
            // 
            // pLineAnimation
            // 
            this.pLineAnimation.BackColor = System.Drawing.Color.LightGray;
            this.pLineAnimation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineAnimation.Location = new System.Drawing.Point(0, 64);
            this.pLineAnimation.Name = "pLineAnimation";
            this.pLineAnimation.Size = new System.Drawing.Size(784, 1);
            this.pLineAnimation.TabIndex = 38;
            // 
            // eAnimationdt
            // 
            this.eAnimationdt.DecimalPlaces = 2;
            this.eAnimationdt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.eAnimationdt.Location = new System.Drawing.Point(286, 6);
            this.eAnimationdt.Name = "eAnimationdt";
            this.eAnimationdt.Size = new System.Drawing.Size(53, 23);
            this.eAnimationdt.TabIndex = 37;
            this.toolTip.SetToolTip(this.eAnimationdt, "(in milliseconds)");
            this.eAnimationdt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lAnimationdt
            // 
            this.lAnimationdt.AutoSize = true;
            this.lAnimationdt.Location = new System.Drawing.Point(261, 9);
            this.lAnimationdt.Name = "lAnimationdt";
            this.lAnimationdt.Size = new System.Drawing.Size(19, 15);
            this.lAnimationdt.TabIndex = 36;
            this.lAnimationdt.Text = "Δt";
            // 
            // linkSaveAnimationCSV
            // 
            this.linkSaveAnimationCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveAnimationCSV.Enabled = false;
            this.linkSaveAnimationCSV.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveAnimationCSV.Location = new System.Drawing.Point(655, 41);
            this.linkSaveAnimationCSV.Name = "linkSaveAnimationCSV";
            this.linkSaveAnimationCSV.Size = new System.Drawing.Size(124, 15);
            this.linkSaveAnimationCSV.TabIndex = 34;
            this.linkSaveAnimationCSV.TabStop = true;
            this.linkSaveAnimationCSV.Text = "Save Animation CSV";
            this.linkSaveAnimationCSV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveAnimationCSV_LinkClicked);
            // 
            // lAnimationTime
            // 
            this.lAnimationTime.Location = new System.Drawing.Point(345, 6);
            this.lAnimationTime.Name = "lAnimationTime";
            this.lAnimationTime.Size = new System.Drawing.Size(290, 54);
            this.lAnimationTime.TabIndex = 33;
            this.lAnimationTime.Text = "Last animation:";
            // 
            // eAnimationEnd
            // 
            this.eAnimationEnd.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.eAnimationEnd.Location = new System.Drawing.Point(104, 34);
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
            this.eAnimationStart.Location = new System.Drawing.Point(104, 6);
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
            // linkSaveAnimationHTML
            // 
            this.linkSaveAnimationHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveAnimationHTML.Enabled = false;
            this.linkSaveAnimationHTML.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveAnimationHTML.Location = new System.Drawing.Point(641, 13);
            this.linkSaveAnimationHTML.Name = "linkSaveAnimationHTML";
            this.linkSaveAnimationHTML.Size = new System.Drawing.Size(138, 15);
            this.linkSaveAnimationHTML.TabIndex = 31;
            this.linkSaveAnimationHTML.TabStop = true;
            this.linkSaveAnimationHTML.Text = "Save Animation HTML";
            this.linkSaveAnimationHTML.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveAnimationHTML_LinkClicked);
            // 
            // btnAnimate
            // 
            this.btnAnimate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAnimate.Enabled = false;
            this.btnAnimate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnAnimate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimate.Location = new System.Drawing.Point(264, 34);
            this.btnAnimate.Name = "btnAnimate";
            this.btnAnimate.Size = new System.Drawing.Size(75, 23);
            this.btnAnimate.TabIndex = 29;
            this.btnAnimate.Text = "Animate";
            this.btnAnimate.UseVisualStyleBackColor = false;
            this.btnAnimate.Click += new System.EventHandler(this.btnAnimate_Click);
            // 
            // lAnimationStart
            // 
            this.lAnimationStart.AutoSize = true;
            this.lAnimationStart.Location = new System.Drawing.Point(6, 9);
            this.lAnimationStart.Name = "lAnimationStart";
            this.lAnimationStart.Size = new System.Drawing.Size(90, 15);
            this.lAnimationStart.TabIndex = 11;
            this.lAnimationStart.Text = "Animation Start";
            // 
            // lms3
            // 
            this.lms3.AutoSize = true;
            this.lms3.Location = new System.Drawing.Point(210, 9);
            this.lms3.Name = "lms3";
            this.lms3.Size = new System.Drawing.Size(31, 15);
            this.lms3.TabIndex = 13;
            this.lms3.Text = "(ms)";
            // 
            // lAnimationEnd
            // 
            this.lAnimationEnd.AutoSize = true;
            this.lAnimationEnd.Location = new System.Drawing.Point(6, 37);
            this.lAnimationEnd.Name = "lAnimationEnd";
            this.lAnimationEnd.Size = new System.Drawing.Size(86, 15);
            this.lAnimationEnd.TabIndex = 14;
            this.lAnimationEnd.Text = "Animation End";
            // 
            // lms4
            // 
            this.lms4.AutoSize = true;
            this.lms4.Location = new System.Drawing.Point(210, 37);
            this.lms4.Name = "lms4";
            this.lms4.Size = new System.Drawing.Size(31, 15);
            this.lms4.TabIndex = 16;
            this.lms4.Text = "(ms)";
            // 
            // tabTemplateJSON
            // 
            this.tabTemplateJSON.Controls.Add(this.eTemplateJSON);
            this.tabTemplateJSON.Controls.Add(this.pTemplateJSONTop);
            this.tabTemplateJSON.Location = new System.Drawing.Point(4, 24);
            this.tabTemplateJSON.Name = "tabTemplateJSON";
            this.tabTemplateJSON.Size = new System.Drawing.Size(784, 641);
            this.tabTemplateJSON.TabIndex = 5;
            this.tabTemplateJSON.Text = "Template JSON";
            this.tabTemplateJSON.UseVisualStyleBackColor = true;
            // 
            // eTemplateJSON
            // 
            this.eTemplateJSON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eTemplateJSON.Location = new System.Drawing.Point(0, 40);
            this.eTemplateJSON.Name = "eTemplateJSON";
            this.eTemplateJSON.Size = new System.Drawing.Size(784, 601);
            this.eTemplateJSON.TabIndex = 2;
            this.eTemplateJSON.Text = "";
            this.eTemplateJSON.TextChanged += new System.EventHandler(this.eTemplateJSON_TextChanged);
            // 
            // pTemplateJSONTop
            // 
            this.pTemplateJSONTop.BackColor = System.Drawing.Color.White;
            this.pTemplateJSONTop.Controls.Add(this.pLineTemplateJSON);
            this.pTemplateJSONTop.Controls.Add(this.linkSaveTemplateJSON);
            this.pTemplateJSONTop.Controls.Add(this.btnLoadTemplateJSON);
            this.pTemplateJSONTop.Controls.Add(this.btnDisplayTemplateJSON);
            this.pTemplateJSONTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTemplateJSONTop.Location = new System.Drawing.Point(0, 0);
            this.pTemplateJSONTop.Name = "pTemplateJSONTop";
            this.pTemplateJSONTop.Size = new System.Drawing.Size(784, 40);
            this.pTemplateJSONTop.TabIndex = 1;
            // 
            // pLineTemplateJSON
            // 
            this.pLineTemplateJSON.BackColor = System.Drawing.Color.LightGray;
            this.pLineTemplateJSON.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineTemplateJSON.Location = new System.Drawing.Point(0, 39);
            this.pLineTemplateJSON.Name = "pLineTemplateJSON";
            this.pLineTemplateJSON.Size = new System.Drawing.Size(784, 1);
            this.pLineTemplateJSON.TabIndex = 25;
            // 
            // linkSaveTemplateJSON
            // 
            this.linkSaveTemplateJSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveTemplateJSON.AutoSize = true;
            this.linkSaveTemplateJSON.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveTemplateJSON.Location = new System.Drawing.Point(695, 13);
            this.linkSaveTemplateJSON.Name = "linkSaveTemplateJSON";
            this.linkSaveTemplateJSON.Size = new System.Drawing.Size(82, 15);
            this.linkSaveTemplateJSON.TabIndex = 4;
            this.linkSaveTemplateJSON.TabStop = true;
            this.linkSaveTemplateJSON.Text = "Save Template";
            this.linkSaveTemplateJSON.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveTemplateJSON_LinkClicked);
            // 
            // btnLoadTemplateJSON
            // 
            this.btnLoadTemplateJSON.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLoadTemplateJSON.Enabled = false;
            this.btnLoadTemplateJSON.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnLoadTemplateJSON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadTemplateJSON.Location = new System.Drawing.Point(87, 9);
            this.btnLoadTemplateJSON.Name = "btnLoadTemplateJSON";
            this.btnLoadTemplateJSON.Size = new System.Drawing.Size(75, 23);
            this.btnLoadTemplateJSON.TabIndex = 1;
            this.btnLoadTemplateJSON.Text = "Load";
            this.btnLoadTemplateJSON.UseVisualStyleBackColor = false;
            this.btnLoadTemplateJSON.Click += new System.EventHandler(this.btnLoadTemplateJSON_Click);
            // 
            // btnDisplayTemplateJSON
            // 
            this.btnDisplayTemplateJSON.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDisplayTemplateJSON.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDisplayTemplateJSON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisplayTemplateJSON.Location = new System.Drawing.Point(6, 9);
            this.btnDisplayTemplateJSON.Name = "btnDisplayTemplateJSON";
            this.btnDisplayTemplateJSON.Size = new System.Drawing.Size(75, 23);
            this.btnDisplayTemplateJSON.TabIndex = 0;
            this.btnDisplayTemplateJSON.Text = "Display";
            this.btnDisplayTemplateJSON.UseVisualStyleBackColor = false;
            this.btnDisplayTemplateJSON.Click += new System.EventHandler(this.btnDisplayTemplateJSON_Click);
            // 
            // tabModelJSON
            // 
            this.tabModelJSON.Controls.Add(this.eModelJSON);
            this.tabModelJSON.Controls.Add(this.pModelJSONTop);
            this.tabModelJSON.Location = new System.Drawing.Point(4, 24);
            this.tabModelJSON.Name = "tabModelJSON";
            this.tabModelJSON.Size = new System.Drawing.Size(784, 641);
            this.tabModelJSON.TabIndex = 6;
            this.tabModelJSON.Text = "Model JSON";
            this.tabModelJSON.UseVisualStyleBackColor = true;
            // 
            // eModelJSON
            // 
            this.eModelJSON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eModelJSON.Location = new System.Drawing.Point(0, 40);
            this.eModelJSON.Name = "eModelJSON";
            this.eModelJSON.Size = new System.Drawing.Size(784, 601);
            this.eModelJSON.TabIndex = 4;
            this.eModelJSON.Text = "";
            this.eModelJSON.TextChanged += new System.EventHandler(this.eModelJSON_TextChanged);
            // 
            // pModelJSONTop
            // 
            this.pModelJSONTop.BackColor = System.Drawing.Color.White;
            this.pModelJSONTop.Controls.Add(this.pLineModelJSON);
            this.pModelJSONTop.Controls.Add(this.linkSaveModelJSON);
            this.pModelJSONTop.Controls.Add(this.btnLoadModelJSON);
            this.pModelJSONTop.Controls.Add(this.btnDisplayModelJSON);
            this.pModelJSONTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pModelJSONTop.Location = new System.Drawing.Point(0, 0);
            this.pModelJSONTop.Name = "pModelJSONTop";
            this.pModelJSONTop.Size = new System.Drawing.Size(784, 40);
            this.pModelJSONTop.TabIndex = 3;
            // 
            // pLineModelJSON
            // 
            this.pLineModelJSON.BackColor = System.Drawing.Color.LightGray;
            this.pLineModelJSON.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineModelJSON.Location = new System.Drawing.Point(0, 39);
            this.pLineModelJSON.Name = "pLineModelJSON";
            this.pLineModelJSON.Size = new System.Drawing.Size(784, 1);
            this.pLineModelJSON.TabIndex = 25;
            // 
            // linkSaveModelJSON
            // 
            this.linkSaveModelJSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveModelJSON.AutoSize = true;
            this.linkSaveModelJSON.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveModelJSON.Location = new System.Drawing.Point(706, 13);
            this.linkSaveModelJSON.Name = "linkSaveModelJSON";
            this.linkSaveModelJSON.Size = new System.Drawing.Size(68, 15);
            this.linkSaveModelJSON.TabIndex = 5;
            this.linkSaveModelJSON.TabStop = true;
            this.linkSaveModelJSON.Text = "Save Model";
            this.linkSaveModelJSON.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveModelJSON_LinkClicked);
            // 
            // btnLoadModelJSON
            // 
            this.btnLoadModelJSON.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLoadModelJSON.Enabled = false;
            this.btnLoadModelJSON.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnLoadModelJSON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadModelJSON.Location = new System.Drawing.Point(87, 9);
            this.btnLoadModelJSON.Name = "btnLoadModelJSON";
            this.btnLoadModelJSON.Size = new System.Drawing.Size(75, 23);
            this.btnLoadModelJSON.TabIndex = 1;
            this.btnLoadModelJSON.Text = "Load";
            this.btnLoadModelJSON.UseVisualStyleBackColor = false;
            this.btnLoadModelJSON.Click += new System.EventHandler(this.btnLoadModelJSON_Click);
            // 
            // btnDisplayModelJSON
            // 
            this.btnDisplayModelJSON.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDisplayModelJSON.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDisplayModelJSON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisplayModelJSON.Location = new System.Drawing.Point(6, 9);
            this.btnDisplayModelJSON.Name = "btnDisplayModelJSON";
            this.btnDisplayModelJSON.Size = new System.Drawing.Size(75, 23);
            this.btnDisplayModelJSON.TabIndex = 0;
            this.btnDisplayModelJSON.Text = "Display";
            this.btnDisplayModelJSON.UseVisualStyleBackColor = false;
            this.btnDisplayModelJSON.Click += new System.EventHandler(this.btnDisplayModelJSON_Click);
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
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 711);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            this.pCellPoolTop.ResumeLayout(false);
            this.pCellPoolTop.PerformLayout();
            this.pConnectionTop.ResumeLayout(false);
            this.pConnectionTop.PerformLayout();
            this.tabStimuli.ResumeLayout(false);
            this.pParamBottom.ResumeLayout(false);
            this.pParamBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eRunNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eTimeEnd)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.ePlotHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotWidth)).EndInit();
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
            this.tabMNKinematics.ResumeLayout(false);
            this.splitKinematics.Panel1.ResumeLayout(false);
            this.splitKinematics.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitKinematics)).EndInit();
            this.splitKinematics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewSummaryV)).EndInit();
            this.pMNKinematicsTop.ResumeLayout(false);
            this.pMNKinematicsTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotKinematicsHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotKinematicsWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eKinematicsSomite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eKinematicsEpisodeBreak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eKinematicsBurstBreak)).EndInit();
            this.tabAnimation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewAnimation)).EndInit();
            this.pAnimation.ResumeLayout(false);
            this.pAnimation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationdt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationStart)).EndInit();
            this.tabTemplateJSON.ResumeLayout(false);
            this.pTemplateJSONTop.ResumeLayout(false);
            this.pTemplateJSONTop.PerformLayout();
            this.tabModelJSON.ResumeLayout(false);
            this.pModelJSONTop.ResumeLayout(false);
            this.pModelJSONTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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
        private Label lms3;
        private Label lAnimationEnd;
        private Label lms4;
        private TabControl tabParams;
        private LinkLabel linkSaveParam;
        private Panel pMain;
        private LinkLabel linkLoadParam;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private LinkLabel linkExportOutput;
        private SaveFileDialog saveFileCSV;
        private ToolTip toolTip;
        private TabPage tabPlot;
        private SplitContainer splitPlotWindows;
        private Panel pPlot;
        private Label lPlotPool;
        private ComboBox ddPlotPools;
        private Label lPlotStart;
        private Label lms1;
        private Label lPlotEnd;
        private Label lms2;
        private Label lPlotPlot;
        private Button btnPlotWindows;
        private ComboBox ddPlot;
        private NumericUpDown ePlotSomiteSelection;
        private Label lStimulus;
        private ComboBox ddStimulusMode;
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
        private NumericUpDown edt;
        private NumericUpDown eTimeEnd;
        private Label ldt;
        private NumericUpDown eSkip;
        private Label lSkip;
        private NumericUpDown eNumSomites;
        private Label lNoOfSomites;
        private PictureBox pictureBoxLeft;
        private PictureBox pictureBoxRight;
        private Label lSagittal;
        private NumericUpDown ePlotEnd;
        private NumericUpDown ePlotStart;
        private NumericUpDown eAnimationStart;
        private NumericUpDown eAnimationEnd;
        private LinkLabel linkOpenOutputFolder;
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
        private ComboBox ddPlotSagittal;
        private Label lNumberOfPlots;
        private TabPage tabTemplateJSON;
        private Panel pTemplateJSONTop;
        private Button btnDisplayTemplateJSON;
        private Button btnLoadTemplateJSON;
        private TabPage tabModelJSON;
        private Panel pModelJSONTop;
        private Button btnLoadModelJSON;
        private Button btnDisplayModelJSON;
        private RichTextBox eTemplateJSON;
        private RichTextBox eModelJSON;
        private NumericUpDown ePlotHeight;
        private NumericUpDown ePlotWidth;
        private Label lPlotWidth;
        private Label lpx1;
        private Label lPlotHeight;
        private Label lpx2;
        private NumericUpDown eAnimationdt;
        private Label lAnimationdt;
        private LinkLabel linkSaveTemplateJSON;
        private LinkLabel linkSaveModelJSON;
        private TabPage tabMNKinematics;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewSummaryV;
        private Panel pMNKinematicsTop;
        private Label label7;
        private NumericUpDown eKinematicsSomite;
        private Label label2;
        private NumericUpDown eKinematicsEpisodeBreak;
        private Label lBurstBreak;
        private NumericUpDown eKinematicsBurstBreak;
        private Label lKinematicsTimes;
        private Button btnGenerateEpisodes;
        private Label label4;
        private Label label6;
        private SplitContainer splitKinematics;
        private RichTextBox eEpisodesLeft;
        private NumericUpDown ePlotKinematicsHeight;
        private NumericUpDown ePlotKinematicsWidth;
        private Label lPlotKinematicsWidth;
        private Label label3;
        private Label lPlotKinematicsHeight;
        private Label label8;
        private RichTextBox eEpisodesRight;
        private Panel pCellPoolTop;
        private Label lCellPools;
        private Panel pConnectionTop;
        private Label lConnections;
        private ComboBox ddSomites;
        private Label lSomites;
        private Label lRunCount;
        private Panel pLine2D;
        private Panel pLine3D;
        private Panel pLineMNKinematicsRight;
        private Panel pLineMNKinematicsLeft;
        private Panel pLinePlots;
        private Panel pLineMNKinematics;
        private Panel pLineAnimation;
        private Panel pLineTemplateJSON;
        private Panel pLineModelJSON;
        private Button btnCellularDynamics;
        private ComboBox ddModelSelection;
        private Label lModelSelection;
        private Panel pLineCellPools;
        private Panel pLineConnections;
    }
}