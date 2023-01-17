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
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnCellularDynamics = new System.Windows.Forms.Button();
            this.linkBrowseToTempFolder = new System.Windows.Forms.LinkLabel();
            this.linkOpenOutputFolder = new System.Windows.Forms.LinkLabel();
            this.linkSaveHTMLPlots = new System.Windows.Forms.LinkLabel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tabModel = new System.Windows.Forms.TabControl();
            this.tTemplate = new System.Windows.Forms.TabPage();
            this.mcTemplate = new SiliFish.UI.Controls.ModelControl();
            this.tModel = new System.Windows.Forms.TabPage();
            this.mcRunningModel = new SiliFish.UI.Controls.ModelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkGenerateRunningModel = new System.Windows.Forms.LinkLabel();
            this.linkLoadModel = new System.Windows.Forms.LinkLabel();
            this.lTitle = new System.Windows.Forms.Label();
            this.linkSaveModel = new System.Windows.Forms.LinkLabel();
            this.linkClearModel = new System.Windows.Forms.LinkLabel();
            this.pSimulation = new System.Windows.Forms.Panel();
            this.ldtEuler = new System.Windows.Forms.Label();
            this.edtEuler = new System.Windows.Forms.NumericUpDown();
            this.lRunCount = new System.Windows.Forms.Label();
            this.eRunNumber = new System.Windows.Forms.NumericUpDown();
            this.eSkip = new System.Windows.Forms.NumericUpDown();
            this.lSkip = new System.Windows.Forms.Label();
            this.edt = new System.Windows.Forms.NumericUpDown();
            this.eTimeEnd = new System.Windows.Forms.NumericUpDown();
            this.ldt = new System.Windows.Forms.Label();
            this.lRunTime = new System.Windows.Forms.Label();
            this.linkExportOutput = new System.Windows.Forms.LinkLabel();
            this.progressBarRun = new System.Windows.Forms.ProgressBar();
            this.btnRun = new System.Windows.Forms.Button();
            this.lTimeEnd = new System.Windows.Forms.Label();
            this.lRunParameters = new System.Windows.Forms.Label();
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
            this.linkExportPlotData = new System.Windows.Forms.LinkLabel();
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
            this.p3DViewing = new System.Windows.Forms.Panel();
            this.dd3DViewpoint = new System.Windows.Forms.ComboBox();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.grLegend = new System.Windows.Forms.GroupBox();
            this.legendUnselected = new System.Windows.Forms.Label();
            this.legendOutgoing = new System.Windows.Forms.Label();
            this.legendGap = new System.Windows.Forms.Label();
            this.legendIncoming = new System.Windows.Forms.Label();
            this.webView3DModel = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.p3DModel = new System.Windows.Forms.Panel();
            this.cb3DLegend = new System.Windows.Forms.CheckBox();
            this.cb3DGapJunc = new System.Windows.Forms.CheckBox();
            this.cb3DChemJunc = new System.Windows.Forms.CheckBox();
            this.e3DSomiteRange = new System.Windows.Forms.TextBox();
            this.cb3DAllSomites = new System.Windows.Forms.CheckBox();
            this.pLine3D = new System.Windows.Forms.Panel();
            this.linkSaveHTML3D = new System.Windows.Forms.LinkLabel();
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
            this.lpx3 = new System.Windows.Forms.Label();
            this.lPlotKinematicsHeight = new System.Windows.Forms.Label();
            this.lpx4 = new System.Windows.Forms.Label();
            this.lNumOfSomitesMNDynamics = new System.Windows.Forms.Label();
            this.eKinematicsSomite = new System.Windows.Forms.NumericUpDown();
            this.btnGenerateEpisodes = new System.Windows.Forms.Button();
            this.lKinematicsTimes = new System.Windows.Forms.Label();
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
            this.tabModel.SuspendLayout();
            this.tTemplate.SuspendLayout();
            this.tModel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pSimulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtEuler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eRunNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eTimeEnd)).BeginInit();
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
            this.p3DViewing.SuspendLayout();
            this.grLegend.SuspendLayout();
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
            this.pTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pTop.Controls.Add(this.btnAbout);
            this.pTop.Controls.Add(this.btnCellularDynamics);
            this.pTop.Controls.Add(this.linkBrowseToTempFolder);
            this.pTop.Controls.Add(this.linkOpenOutputFolder);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1348, 40);
            this.pTop.TabIndex = 3;
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.AutoSize = true;
            this.btnAbout.BackColor = System.Drawing.Color.Transparent;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.Location = new System.Drawing.Point(1302, 5);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(33, 30);
            this.btnAbout.TabIndex = 24;
            this.toolTip.SetToolTip(this.btnAbout, "About");
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnCellularDynamics
            // 
            this.btnCellularDynamics.AutoSize = true;
            this.btnCellularDynamics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnCellularDynamics.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnCellularDynamics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCellularDynamics.ForeColor = System.Drawing.Color.White;
            this.btnCellularDynamics.Location = new System.Drawing.Point(4, 6);
            this.btnCellularDynamics.Name = "btnCellularDynamics";
            this.btnCellularDynamics.Size = new System.Drawing.Size(120, 27);
            this.btnCellularDynamics.TabIndex = 23;
            this.btnCellularDynamics.Text = "Cellular Dynamics";
            this.btnCellularDynamics.UseVisualStyleBackColor = false;
            this.btnCellularDynamics.Click += new System.EventHandler(this.btnCellularDynamics_Click);
            // 
            // linkBrowseToTempFolder
            // 
            this.linkBrowseToTempFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkBrowseToTempFolder.AutoSize = true;
            this.linkBrowseToTempFolder.ForeColor = System.Drawing.Color.White;
            this.linkBrowseToTempFolder.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkBrowseToTempFolder.Location = new System.Drawing.Point(1173, 14);
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
            this.linkOpenOutputFolder.Location = new System.Drawing.Point(1054, 14);
            this.linkOpenOutputFolder.Name = "linkOpenOutputFolder";
            this.linkOpenOutputFolder.Size = new System.Drawing.Size(113, 15);
            this.linkOpenOutputFolder.TabIndex = 6;
            this.linkOpenOutputFolder.TabStop = true;
            this.linkOpenOutputFolder.Text = "Open Output Folder";
            this.toolTip.SetToolTip(this.linkOpenOutputFolder, "If there is a problem in creating html files, they will be saved in this folder.\r" +
        "\n");
            this.linkOpenOutputFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOpenOutputFolder_LinkClicked);
            // 
            // linkSaveHTMLPlots
            // 
            this.linkSaveHTMLPlots.Enabled = false;
            this.linkSaveHTMLPlots.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveHTMLPlots.Location = new System.Drawing.Point(524, 62);
            this.linkSaveHTMLPlots.Name = "linkSaveHTMLPlots";
            this.linkSaveHTMLPlots.Size = new System.Drawing.Size(103, 16);
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
            this.splitMain.Panel1.Controls.Add(this.tabModel);
            this.splitMain.Panel1.Controls.Add(this.panel1);
            this.splitMain.Panel1.Controls.Add(this.pSimulation);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.tabOutputs);
            this.splitMain.Size = new System.Drawing.Size(1348, 671);
            this.splitMain.SplitterDistance = 550;
            this.splitMain.TabIndex = 5;
            // 
            // tabModel
            // 
            this.tabModel.Controls.Add(this.tTemplate);
            this.tabModel.Controls.Add(this.tModel);
            this.tabModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabModel.Location = new System.Drawing.Point(0, 64);
            this.tabModel.Name = "tabModel";
            this.tabModel.SelectedIndex = 0;
            this.tabModel.Size = new System.Drawing.Size(548, 440);
            this.tabModel.TabIndex = 6;
            // 
            // tTemplate
            // 
            this.tTemplate.Controls.Add(this.mcTemplate);
            this.tTemplate.Location = new System.Drawing.Point(4, 24);
            this.tTemplate.Name = "tTemplate";
            this.tTemplate.Padding = new System.Windows.Forms.Padding(3);
            this.tTemplate.Size = new System.Drawing.Size(540, 412);
            this.tTemplate.TabIndex = 0;
            this.tTemplate.Text = "Template";
            this.tTemplate.UseVisualStyleBackColor = true;
            // 
            // mcTemplate
            // 
            this.mcTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcTemplate.Location = new System.Drawing.Point(3, 3);
            this.mcTemplate.ModelUpdated = false;
            this.mcTemplate.Name = "mcTemplate";
            this.mcTemplate.Size = new System.Drawing.Size(534, 406);
            this.mcTemplate.TabIndex = 2;
            this.mcTemplate.ModelChanged += new System.EventHandler(this.mcTemplate_ModelChanged);
            // 
            // tModel
            // 
            this.tModel.Controls.Add(this.mcRunningModel);
            this.tModel.Location = new System.Drawing.Point(4, 24);
            this.tModel.Name = "tModel";
            this.tModel.Padding = new System.Windows.Forms.Padding(3);
            this.tModel.Size = new System.Drawing.Size(540, 412);
            this.tModel.TabIndex = 1;
            this.tModel.Text = "Model";
            this.tModel.UseVisualStyleBackColor = true;
            // 
            // mcRunningModel
            // 
            this.mcRunningModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcRunningModel.Location = new System.Drawing.Point(3, 3);
            this.mcRunningModel.ModelUpdated = false;
            this.mcRunningModel.Name = "mcRunningModel";
            this.mcRunningModel.Size = new System.Drawing.Size(534, 406);
            this.mcRunningModel.TabIndex = 3;
            this.mcRunningModel.ModelChanged += new System.EventHandler(this.mcRunningModel_ModelChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.panel1.Controls.Add(this.linkGenerateRunningModel);
            this.panel1.Controls.Add(this.linkLoadModel);
            this.panel1.Controls.Add(this.lTitle);
            this.panel1.Controls.Add(this.linkSaveModel);
            this.panel1.Controls.Add(this.linkClearModel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 64);
            this.panel1.TabIndex = 5;
            // 
            // linkGenerateRunningModel
            // 
            this.linkGenerateRunningModel.AutoSize = true;
            this.linkGenerateRunningModel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkGenerateRunningModel.Location = new System.Drawing.Point(12, 35);
            this.linkGenerateRunningModel.Name = "linkGenerateRunningModel";
            this.linkGenerateRunningModel.Size = new System.Drawing.Size(171, 15);
            this.linkGenerateRunningModel.TabIndex = 6;
            this.linkGenerateRunningModel.TabStop = true;
            this.linkGenerateRunningModel.Text = "Generate Model from Template";
            this.linkGenerateRunningModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGenerateRunningModel_LinkClicked);
            // 
            // linkLoadModel
            // 
            this.linkLoadModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLoadModel.AutoSize = true;
            this.linkLoadModel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLoadModel.Location = new System.Drawing.Point(401, 12);
            this.linkLoadModel.Name = "linkLoadModel";
            this.linkLoadModel.Size = new System.Drawing.Size(70, 15);
            this.linkLoadModel.TabIndex = 4;
            this.linkLoadModel.TabStop = true;
            this.linkLoadModel.Text = "Load Model";
            this.linkLoadModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLoadModel_LinkClicked);
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lTitle.Location = new System.Drawing.Point(12, 12);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(109, 15);
            this.lTitle.TabIndex = 0;
            this.lTitle.Text = "Model Parameters";
            // 
            // linkSaveModel
            // 
            this.linkSaveModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveModel.AutoSize = true;
            this.linkSaveModel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveModel.Location = new System.Drawing.Point(477, 12);
            this.linkSaveModel.Name = "linkSaveModel";
            this.linkSaveModel.Size = new System.Drawing.Size(68, 15);
            this.linkSaveModel.TabIndex = 3;
            this.linkSaveModel.TabStop = true;
            this.linkSaveModel.Text = "Save Model";
            this.linkSaveModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveModel_LinkClicked);
            // 
            // linkClearModel
            // 
            this.linkClearModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkClearModel.AutoSize = true;
            this.linkClearModel.ForeColor = System.Drawing.Color.White;
            this.linkClearModel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkClearModel.Location = new System.Drawing.Point(474, 35);
            this.linkClearModel.Name = "linkClearModel";
            this.linkClearModel.Size = new System.Drawing.Size(71, 15);
            this.linkClearModel.TabIndex = 5;
            this.linkClearModel.TabStop = true;
            this.linkClearModel.Text = "Clear Model";
            this.linkClearModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClearModel_LinkClicked);
            // 
            // pSimulation
            // 
            this.pSimulation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pSimulation.Controls.Add(this.ldtEuler);
            this.pSimulation.Controls.Add(this.edtEuler);
            this.pSimulation.Controls.Add(this.lRunCount);
            this.pSimulation.Controls.Add(this.eRunNumber);
            this.pSimulation.Controls.Add(this.eSkip);
            this.pSimulation.Controls.Add(this.lSkip);
            this.pSimulation.Controls.Add(this.edt);
            this.pSimulation.Controls.Add(this.eTimeEnd);
            this.pSimulation.Controls.Add(this.ldt);
            this.pSimulation.Controls.Add(this.lRunTime);
            this.pSimulation.Controls.Add(this.linkExportOutput);
            this.pSimulation.Controls.Add(this.progressBarRun);
            this.pSimulation.Controls.Add(this.btnRun);
            this.pSimulation.Controls.Add(this.lTimeEnd);
            this.pSimulation.Controls.Add(this.lRunParameters);
            this.pSimulation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pSimulation.Location = new System.Drawing.Point(0, 504);
            this.pSimulation.Name = "pSimulation";
            this.pSimulation.Size = new System.Drawing.Size(548, 165);
            this.pSimulation.TabIndex = 1;
            // 
            // ldtEuler
            // 
            this.ldtEuler.AutoSize = true;
            this.ldtEuler.Location = new System.Drawing.Point(10, 113);
            this.ldtEuler.Name = "ldtEuler";
            this.ldtEuler.Size = new System.Drawing.Size(48, 15);
            this.ldtEuler.TabIndex = 39;
            this.ldtEuler.Text = "Δt Euler";
            // 
            // edtEuler
            // 
            this.edtEuler.DecimalPlaces = 3;
            this.edtEuler.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edtEuler.Location = new System.Drawing.Point(71, 110);
            this.edtEuler.Name = "edtEuler";
            this.edtEuler.Size = new System.Drawing.Size(76, 23);
            this.edtEuler.TabIndex = 38;
            this.toolTip.SetToolTip(this.edtEuler, "(in milliseconds)");
            this.edtEuler.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // lRunCount
            // 
            this.lRunCount.AutoSize = true;
            this.lRunCount.Location = new System.Drawing.Point(156, 35);
            this.lRunCount.Name = "lRunCount";
            this.lRunCount.Size = new System.Drawing.Size(64, 15);
            this.lRunCount.TabIndex = 37;
            this.lRunCount.Text = "Run Count";
            this.toolTip.SetToolTip(this.lRunCount, "If run count is greater than 1, the model details and episodes of each run are ex" +
        "ported automatically.");
            // 
            // eRunNumber
            // 
            this.eRunNumber.Location = new System.Drawing.Point(226, 32);
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
            this.eSkip.Location = new System.Drawing.Point(71, 58);
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
            this.lSkip.Location = new System.Drawing.Point(11, 61);
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
            131072});
            this.edt.Location = new System.Drawing.Point(71, 84);
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
            this.eTimeEnd.Location = new System.Drawing.Point(71, 32);
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
            this.ldt.Location = new System.Drawing.Point(10, 87);
            this.ldt.Name = "ldt";
            this.ldt.Size = new System.Drawing.Size(60, 15);
            this.ldt.TabIndex = 32;
            this.ldt.Text = "Δt Output";
            // 
            // lRunTime
            // 
            this.lRunTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lRunTime.Location = new System.Drawing.Point(302, 36);
            this.lRunTime.Name = "lRunTime";
            this.lRunTime.Size = new System.Drawing.Size(224, 80);
            this.lRunTime.TabIndex = 31;
            // 
            // linkExportOutput
            // 
            this.linkExportOutput.AutoSize = true;
            this.linkExportOutput.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkExportOutput.Location = new System.Drawing.Point(212, 86);
            this.linkExportOutput.Name = "linkExportOutput";
            this.linkExportOutput.Size = new System.Drawing.Size(82, 15);
            this.linkExportOutput.TabIndex = 27;
            this.linkExportOutput.TabStop = true;
            this.linkExportOutput.Text = "Export Output";
            this.linkExportOutput.Visible = false;
            this.linkExportOutput.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkExportOutput_LinkClicked);
            // 
            // progressBarRun
            // 
            this.progressBarRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarRun.Location = new System.Drawing.Point(0, 142);
            this.progressBarRun.Name = "progressBarRun";
            this.progressBarRun.Size = new System.Drawing.Size(548, 23);
            this.progressBarRun.TabIndex = 26;
            this.progressBarRun.Visible = false;
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnRun.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(226, 58);
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
            this.lTimeEnd.Location = new System.Drawing.Point(10, 35);
            this.lTimeEnd.Name = "lTimeEnd";
            this.lTimeEnd.Size = new System.Drawing.Size(56, 15);
            this.lTimeEnd.TabIndex = 3;
            this.lTimeEnd.Text = "Time End";
            // 
            // lRunParameters
            // 
            this.lRunParameters.AutoSize = true;
            this.lRunParameters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lRunParameters.Location = new System.Drawing.Point(8, 8);
            this.lRunParameters.Name = "lRunParameters";
            this.lRunParameters.Size = new System.Drawing.Size(133, 15);
            this.lRunParameters.TabIndex = 1;
            this.lRunParameters.Text = "Simulation Parameters";
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
            this.pPlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pPlot.Controls.Add(this.linkExportPlotData);
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
            // linkExportPlotData
            // 
            this.linkExportPlotData.Enabled = false;
            this.linkExportPlotData.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkExportPlotData.Location = new System.Drawing.Point(524, 88);
            this.linkExportPlotData.Name = "linkExportPlotData";
            this.linkExportPlotData.Size = new System.Drawing.Size(103, 16);
            this.linkExportPlotData.TabIndex = 59;
            this.linkExportPlotData.TabStop = true;
            this.linkExportPlotData.Text = "Export Plot Data";
            this.toolTip.SetToolTip(this.linkExportPlotData, "Every plot data will be exported as a separate CSV file.");
            this.linkExportPlotData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkExportPlotData_LinkClicked);
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
            this.ePlotHeight.Location = new System.Drawing.Point(73, 110);
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
            this.ePlotWidth.Location = new System.Drawing.Point(73, 84);
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
            this.lPlotWidth.Location = new System.Drawing.Point(12, 86);
            this.lPlotWidth.Name = "lPlotWidth";
            this.lPlotWidth.Size = new System.Drawing.Size(39, 15);
            this.lPlotWidth.TabIndex = 54;
            this.lPlotWidth.Text = "Width";
            this.toolTip.SetToolTip(this.lPlotWidth, "for each plot - 15% of it will be used for legend");
            // 
            // lpx1
            // 
            this.lpx1.AutoSize = true;
            this.lpx1.Location = new System.Drawing.Point(127, 89);
            this.lpx1.Name = "lpx1";
            this.lpx1.Size = new System.Drawing.Size(28, 15);
            this.lpx1.TabIndex = 55;
            this.lpx1.Text = "(px)";
            // 
            // lPlotHeight
            // 
            this.lPlotHeight.AutoSize = true;
            this.lPlotHeight.Location = new System.Drawing.Point(12, 112);
            this.lPlotHeight.Name = "lPlotHeight";
            this.lPlotHeight.Size = new System.Drawing.Size(43, 15);
            this.lPlotHeight.TabIndex = 56;
            this.lPlotHeight.Text = "Height";
            this.toolTip.SetToolTip(this.lPlotHeight, "for each plot");
            // 
            // lpx2
            // 
            this.lpx2.AutoSize = true;
            this.lpx2.Location = new System.Drawing.Point(127, 115);
            this.lpx2.Name = "lpx2";
            this.lpx2.Size = new System.Drawing.Size(28, 15);
            this.lpx2.TabIndex = 57;
            this.lpx2.Text = "(px)";
            // 
            // lNumberOfPlots
            // 
            this.lNumberOfPlots.AutoSize = true;
            this.lNumberOfPlots.Location = new System.Drawing.Point(446, 8);
            this.lNumberOfPlots.Name = "lNumberOfPlots";
            this.lNumberOfPlots.Size = new System.Drawing.Size(63, 15);
            this.lNumberOfPlots.TabIndex = 53;
            this.lNumberOfPlots.Tag = "# of plots: ";
            this.lNumberOfPlots.Text = "# of plots: ";
            this.lNumberOfPlots.Visible = false;
            // 
            // btnPlotWindows
            // 
            this.btnPlotWindows.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnPlotWindows.Enabled = false;
            this.btnPlotWindows.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnPlotWindows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlotWindows.ForeColor = System.Drawing.Color.White;
            this.btnPlotWindows.Location = new System.Drawing.Point(446, 31);
            this.btnPlotWindows.Name = "btnPlotWindows";
            this.btnPlotWindows.Size = new System.Drawing.Size(75, 24);
            this.btnPlotWindows.TabIndex = 50;
            this.btnPlotWindows.Text = "Plot";
            this.toolTip.SetToolTip(this.btnPlotWindows, "Windows based faster plots - non user-interactive");
            this.btnPlotWindows.UseVisualStyleBackColor = false;
            this.btnPlotWindows.Click += new System.EventHandler(this.btnPlotWindows_Click);
            // 
            // ddPlotSagittal
            // 
            this.ddPlotSagittal.BackColor = System.Drawing.Color.White;
            this.ddPlotSagittal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotSagittal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddPlotSagittal.FormattingEnabled = true;
            this.ddPlotSagittal.Location = new System.Drawing.Point(280, 32);
            this.ddPlotSagittal.Name = "ddPlotSagittal";
            this.ddPlotSagittal.Size = new System.Drawing.Size(160, 23);
            this.ddPlotSagittal.TabIndex = 35;
            this.ddPlotSagittal.SelectedIndexChanged += new System.EventHandler(this.ddPlotSagittal_SelectedIndexChanged);
            // 
            // linkSavePlots
            // 
            this.linkSavePlots.Enabled = false;
            this.linkSavePlots.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSavePlots.Location = new System.Drawing.Point(524, 36);
            this.linkSavePlots.Name = "linkSavePlots";
            this.linkSavePlots.Size = new System.Drawing.Size(103, 16);
            this.linkSavePlots.TabIndex = 51;
            this.linkSavePlots.TabStop = true;
            this.linkSavePlots.Text = "Save Plot Images";
            this.linkSavePlots.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSavePlots_LinkClicked);
            // 
            // btnPlotHTML
            // 
            this.btnPlotHTML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnPlotHTML.Enabled = false;
            this.btnPlotHTML.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnPlotHTML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlotHTML.ForeColor = System.Drawing.Color.White;
            this.btnPlotHTML.Location = new System.Drawing.Point(446, 58);
            this.btnPlotHTML.Name = "btnPlotHTML";
            this.btnPlotHTML.Size = new System.Drawing.Size(75, 24);
            this.btnPlotHTML.TabIndex = 52;
            this.btnPlotHTML.Text = "Plot HTML";
            this.toolTip.SetToolTip(this.btnPlotHTML, "Creates interactive plots");
            this.btnPlotHTML.UseVisualStyleBackColor = false;
            this.btnPlotHTML.Click += new System.EventHandler(this.btnPlotHTML_Click);
            // 
            // ddPlotCellSelection
            // 
            this.ddPlotCellSelection.BackColor = System.Drawing.Color.White;
            this.ddPlotCellSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotCellSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.ddPlotSomiteSelection.BackColor = System.Drawing.Color.White;
            this.ddPlotSomiteSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotSomiteSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.ePlotEnd.Location = new System.Drawing.Point(73, 32);
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
            this.ePlotStart.Location = new System.Drawing.Point(73, 6);
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
            this.ddPlotPools.BackColor = System.Drawing.Color.White;
            this.ddPlotPools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlotPools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.ddPlot.BackColor = System.Drawing.Color.White;
            this.ddPlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.p2DModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
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
            this.linkSaveHTML2D.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveHTML2D.Location = new System.Drawing.Point(711, 10);
            this.linkSaveHTML2D.Name = "linkSaveHTML2D";
            this.linkSaveHTML2D.Size = new System.Drawing.Size(66, 16);
            this.linkSaveHTML2D.TabIndex = 23;
            this.linkSaveHTML2D.TabStop = true;
            this.linkSaveHTML2D.Text = "Save HTML";
            this.linkSaveHTML2D.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkSaveHTML2D.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveHTML2D_LinkClicked);
            // 
            // btnGenerate2DModel
            // 
            this.btnGenerate2DModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnGenerate2DModel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnGenerate2DModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate2DModel.ForeColor = System.Drawing.Color.White;
            this.btnGenerate2DModel.Location = new System.Drawing.Point(8, 8);
            this.btnGenerate2DModel.Name = "btnGenerate2DModel";
            this.btnGenerate2DModel.Size = new System.Drawing.Size(120, 24);
            this.btnGenerate2DModel.TabIndex = 22;
            this.btnGenerate2DModel.Text = "Generate 2D Model";
            this.btnGenerate2DModel.UseVisualStyleBackColor = false;
            this.btnGenerate2DModel.Click += new System.EventHandler(this.btnGenerate2DModel_Click);
            // 
            // tab3DModel
            // 
            this.tab3DModel.Controls.Add(this.p3DViewing);
            this.tab3DModel.Controls.Add(this.grLegend);
            this.tab3DModel.Controls.Add(this.webView3DModel);
            this.tab3DModel.Controls.Add(this.p3DModel);
            this.tab3DModel.Location = new System.Drawing.Point(4, 24);
            this.tab3DModel.Name = "tab3DModel";
            this.tab3DModel.Size = new System.Drawing.Size(784, 641);
            this.tab3DModel.TabIndex = 0;
            this.tab3DModel.Text = "3D Model";
            this.tab3DModel.UseVisualStyleBackColor = true;
            // 
            // p3DViewing
            // 
            this.p3DViewing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p3DViewing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p3DViewing.Controls.Add(this.dd3DViewpoint);
            this.p3DViewing.Controls.Add(this.btnZoomIn);
            this.p3DViewing.Controls.Add(this.btnZoomOut);
            this.p3DViewing.Location = new System.Drawing.Point(525, 42);
            this.p3DViewing.Name = "p3DViewing";
            this.p3DViewing.Size = new System.Drawing.Size(256, 39);
            this.p3DViewing.TabIndex = 58;
            // 
            // dd3DViewpoint
            // 
            this.dd3DViewpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dd3DViewpoint.FormattingEnabled = true;
            this.dd3DViewpoint.Items.AddRange(new object[] {
            "Free view",
            "Dorsal view",
            "Ventral view",
            "Rostral view",
            "Caudal view",
            "Lateral view (left)",
            "Lateral view (right)"});
            this.dd3DViewpoint.Location = new System.Drawing.Point(3, 7);
            this.dd3DViewpoint.Name = "dd3DViewpoint";
            this.dd3DViewpoint.Size = new System.Drawing.Size(180, 23);
            this.dd3DViewpoint.TabIndex = 54;
            this.dd3DViewpoint.SelectedIndexChanged += new System.EventHandler(this.dd3DViewpoint_SelectedIndexChanged);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.Image")));
            this.btnZoomIn.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnZoomIn.Location = new System.Drawing.Point(220, 3);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(32, 32);
            this.btnZoomIn.TabIndex = 48;
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
            this.btnZoomOut.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnZoomOut.Location = new System.Drawing.Point(186, 3);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(32, 32);
            this.btnZoomOut.TabIndex = 47;
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // grLegend
            // 
            this.grLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grLegend.Controls.Add(this.legendUnselected);
            this.grLegend.Controls.Add(this.legendOutgoing);
            this.grLegend.Controls.Add(this.legendGap);
            this.grLegend.Controls.Add(this.legendIncoming);
            this.grLegend.Location = new System.Drawing.Point(648, 542);
            this.grLegend.Name = "grLegend";
            this.grLegend.Size = new System.Drawing.Size(130, 94);
            this.grLegend.TabIndex = 53;
            this.grLegend.TabStop = false;
            this.grLegend.Text = "Projection Legend";
            // 
            // legendUnselected
            // 
            this.legendUnselected.BackColor = System.Drawing.Color.Gray;
            this.legendUnselected.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.legendUnselected.Location = new System.Drawing.Point(6, 17);
            this.legendUnselected.Name = "legendUnselected";
            this.legendUnselected.Size = new System.Drawing.Size(120, 18);
            this.legendUnselected.TabIndex = 49;
            this.legendUnselected.Text = "Unselected";
            // 
            // legendOutgoing
            // 
            this.legendOutgoing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.legendOutgoing.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.legendOutgoing.Location = new System.Drawing.Point(6, 71);
            this.legendOutgoing.Name = "legendOutgoing";
            this.legendOutgoing.Size = new System.Drawing.Size(120, 18);
            this.legendOutgoing.TabIndex = 52;
            this.legendOutgoing.Text = "Outgoing Projections";
            // 
            // legendGap
            // 
            this.legendGap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.legendGap.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.legendGap.Location = new System.Drawing.Point(6, 35);
            this.legendGap.Name = "legendGap";
            this.legendGap.Size = new System.Drawing.Size(120, 18);
            this.legendGap.TabIndex = 50;
            this.legendGap.Text = "Gap junc";
            // 
            // legendIncoming
            // 
            this.legendIncoming.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.legendIncoming.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.legendIncoming.Location = new System.Drawing.Point(6, 53);
            this.legendIncoming.Name = "legendIncoming";
            this.legendIncoming.Size = new System.Drawing.Size(120, 18);
            this.legendIncoming.TabIndex = 51;
            this.legendIncoming.Text = "Incoming Projections";
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
            this.p3DModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.p3DModel.Controls.Add(this.cb3DLegend);
            this.p3DModel.Controls.Add(this.cb3DGapJunc);
            this.p3DModel.Controls.Add(this.cb3DChemJunc);
            this.p3DModel.Controls.Add(this.e3DSomiteRange);
            this.p3DModel.Controls.Add(this.cb3DAllSomites);
            this.p3DModel.Controls.Add(this.pLine3D);
            this.p3DModel.Controls.Add(this.linkSaveHTML3D);
            this.p3DModel.Controls.Add(this.btnGenerate3DModel);
            this.p3DModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.p3DModel.Location = new System.Drawing.Point(0, 0);
            this.p3DModel.Name = "p3DModel";
            this.p3DModel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.p3DModel.Size = new System.Drawing.Size(784, 40);
            this.p3DModel.TabIndex = 3;
            // 
            // cb3DLegend
            // 
            this.cb3DLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb3DLegend.AutoSize = true;
            this.cb3DLegend.Checked = true;
            this.cb3DLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb3DLegend.Location = new System.Drawing.Point(642, 10);
            this.cb3DLegend.Name = "cb3DLegend";
            this.cb3DLegend.Size = new System.Drawing.Size(65, 19);
            this.cb3DLegend.TabIndex = 57;
            this.cb3DLegend.Text = "Legend";
            this.cb3DLegend.UseVisualStyleBackColor = true;
            this.cb3DLegend.CheckedChanged += new System.EventHandler(this.cb3DLegend_CheckedChanged);
            // 
            // cb3DGapJunc
            // 
            this.cb3DGapJunc.Checked = true;
            this.cb3DGapJunc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb3DGapJunc.Location = new System.Drawing.Point(239, 10);
            this.cb3DGapJunc.Name = "cb3DGapJunc";
            this.cb3DGapJunc.Size = new System.Drawing.Size(80, 20);
            this.cb3DGapJunc.TabIndex = 39;
            this.cb3DGapJunc.Text = "Gap Junc";
            this.cb3DGapJunc.UseVisualStyleBackColor = true;
            this.cb3DGapJunc.CheckedChanged += new System.EventHandler(this.cb3DGapJunc_CheckedChanged);
            // 
            // cb3DChemJunc
            // 
            this.cb3DChemJunc.Checked = true;
            this.cb3DChemJunc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb3DChemJunc.Location = new System.Drawing.Point(148, 10);
            this.cb3DChemJunc.Name = "cb3DChemJunc";
            this.cb3DChemJunc.Size = new System.Drawing.Size(85, 20);
            this.cb3DChemJunc.TabIndex = 38;
            this.cb3DChemJunc.Text = "Chem Junc";
            this.cb3DChemJunc.UseVisualStyleBackColor = true;
            this.cb3DChemJunc.CheckedChanged += new System.EventHandler(this.cb3DChemJunc_CheckedChanged);
            // 
            // e3DSomiteRange
            // 
            this.e3DSomiteRange.Location = new System.Drawing.Point(410, 7);
            this.e3DSomiteRange.Name = "e3DSomiteRange";
            this.e3DSomiteRange.Size = new System.Drawing.Size(89, 23);
            this.e3DSomiteRange.TabIndex = 36;
            this.e3DSomiteRange.Text = "Ex: 1, 3-5";
            this.toolTip.SetToolTip(this.e3DSomiteRange, "Enter a range, or list of somites. Ex: 1,3,5-7");
            this.e3DSomiteRange.Visible = false;
            this.e3DSomiteRange.Enter += new System.EventHandler(this.e3DSomiteRange_Enter);
            this.e3DSomiteRange.Leave += new System.EventHandler(this.e3DSomiteRange_Leave);
            // 
            // cb3DAllSomites
            // 
            this.cb3DAllSomites.Checked = true;
            this.cb3DAllSomites.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb3DAllSomites.Location = new System.Drawing.Point(319, 10);
            this.cb3DAllSomites.Name = "cb3DAllSomites";
            this.cb3DAllSomites.Size = new System.Drawing.Size(85, 20);
            this.cb3DAllSomites.TabIndex = 35;
            this.cb3DAllSomites.Text = "All Somites";
            this.cb3DAllSomites.UseVisualStyleBackColor = true;
            this.cb3DAllSomites.CheckedChanged += new System.EventHandler(this.cb3DAllSomites_CheckedChanged);
            // 
            // pLine3D
            // 
            this.pLine3D.BackColor = System.Drawing.Color.LightGray;
            this.pLine3D.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLine3D.Location = new System.Drawing.Point(0, 39);
            this.pLine3D.Name = "pLine3D";
            this.pLine3D.Size = new System.Drawing.Size(779, 1);
            this.pLine3D.TabIndex = 30;
            // 
            // linkSaveHTML3D
            // 
            this.linkSaveHTML3D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveHTML3D.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveHTML3D.Location = new System.Drawing.Point(711, 10);
            this.linkSaveHTML3D.Name = "linkSaveHTML3D";
            this.linkSaveHTML3D.Size = new System.Drawing.Size(66, 16);
            this.linkSaveHTML3D.TabIndex = 27;
            this.linkSaveHTML3D.TabStop = true;
            this.linkSaveHTML3D.Text = "Save HTML";
            this.linkSaveHTML3D.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkSaveHTML3D.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveHTML3D_LinkClicked);
            // 
            // btnGenerate3DModel
            // 
            this.btnGenerate3DModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnGenerate3DModel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnGenerate3DModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate3DModel.ForeColor = System.Drawing.Color.White;
            this.btnGenerate3DModel.Location = new System.Drawing.Point(8, 8);
            this.btnGenerate3DModel.Name = "btnGenerate3DModel";
            this.btnGenerate3DModel.Size = new System.Drawing.Size(120, 24);
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
            this.splitKinematics.Location = new System.Drawing.Point(0, 64);
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
            this.splitKinematics.Panel2.SizeChanged += new System.EventHandler(this.splitKinematics_Panel2_SizeChanged);
            this.splitKinematics.Size = new System.Drawing.Size(784, 577);
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
            this.webViewSummaryV.Size = new System.Drawing.Size(532, 577);
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
            this.pLineMNKinematicsRight.Size = new System.Drawing.Size(1, 577);
            this.pLineMNKinematicsRight.TabIndex = 7;
            // 
            // eEpisodesRight
            // 
            this.eEpisodesRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eEpisodesRight.Location = new System.Drawing.Point(115, 0);
            this.eEpisodesRight.Name = "eEpisodesRight";
            this.eEpisodesRight.Size = new System.Drawing.Size(132, 577);
            this.eEpisodesRight.TabIndex = 1;
            this.eEpisodesRight.Text = "";
            // 
            // eEpisodesLeft
            // 
            this.eEpisodesLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.eEpisodesLeft.Location = new System.Drawing.Point(1, 0);
            this.eEpisodesLeft.Name = "eEpisodesLeft";
            this.eEpisodesLeft.Size = new System.Drawing.Size(114, 577);
            this.eEpisodesLeft.TabIndex = 0;
            this.eEpisodesLeft.Text = "";
            // 
            // pLineMNKinematicsLeft
            // 
            this.pLineMNKinematicsLeft.BackColor = System.Drawing.Color.LightGray;
            this.pLineMNKinematicsLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pLineMNKinematicsLeft.Location = new System.Drawing.Point(0, 0);
            this.pLineMNKinematicsLeft.Name = "pLineMNKinematicsLeft";
            this.pLineMNKinematicsLeft.Size = new System.Drawing.Size(1, 577);
            this.pLineMNKinematicsLeft.TabIndex = 2;
            // 
            // pMNKinematicsTop
            // 
            this.pMNKinematicsTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pMNKinematicsTop.Controls.Add(this.pLineMNKinematics);
            this.pMNKinematicsTop.Controls.Add(this.ePlotKinematicsHeight);
            this.pMNKinematicsTop.Controls.Add(this.ePlotKinematicsWidth);
            this.pMNKinematicsTop.Controls.Add(this.lPlotKinematicsWidth);
            this.pMNKinematicsTop.Controls.Add(this.lpx3);
            this.pMNKinematicsTop.Controls.Add(this.lPlotKinematicsHeight);
            this.pMNKinematicsTop.Controls.Add(this.lpx4);
            this.pMNKinematicsTop.Controls.Add(this.lNumOfSomitesMNDynamics);
            this.pMNKinematicsTop.Controls.Add(this.eKinematicsSomite);
            this.pMNKinematicsTop.Controls.Add(this.btnGenerateEpisodes);
            this.pMNKinematicsTop.Controls.Add(this.lKinematicsTimes);
            this.pMNKinematicsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMNKinematicsTop.Location = new System.Drawing.Point(0, 0);
            this.pMNKinematicsTop.Name = "pMNKinematicsTop";
            this.pMNKinematicsTop.Size = new System.Drawing.Size(784, 64);
            this.pMNKinematicsTop.TabIndex = 7;
            // 
            // pLineMNKinematics
            // 
            this.pLineMNKinematics.BackColor = System.Drawing.Color.LightGray;
            this.pLineMNKinematics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineMNKinematics.Location = new System.Drawing.Point(0, 63);
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
            this.ePlotKinematicsHeight.Location = new System.Drawing.Point(208, 30);
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
            this.ePlotKinematicsWidth.Location = new System.Drawing.Point(208, 3);
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
            this.lPlotKinematicsWidth.Location = new System.Drawing.Point(163, 6);
            this.lPlotKinematicsWidth.Name = "lPlotKinematicsWidth";
            this.lPlotKinematicsWidth.Size = new System.Drawing.Size(39, 15);
            this.lPlotKinematicsWidth.TabIndex = 60;
            this.lPlotKinematicsWidth.Text = "Width";
            this.toolTip.SetToolTip(this.lPlotKinematicsWidth, "for each plot - 15% of it will be used for legend");
            // 
            // lpx3
            // 
            this.lpx3.AutoSize = true;
            this.lpx3.Location = new System.Drawing.Point(262, 6);
            this.lpx3.Name = "lpx3";
            this.lpx3.Size = new System.Drawing.Size(28, 15);
            this.lpx3.TabIndex = 61;
            this.lpx3.Text = "(px)";
            // 
            // lPlotKinematicsHeight
            // 
            this.lPlotKinematicsHeight.AutoSize = true;
            this.lPlotKinematicsHeight.Location = new System.Drawing.Point(163, 33);
            this.lPlotKinematicsHeight.Name = "lPlotKinematicsHeight";
            this.lPlotKinematicsHeight.Size = new System.Drawing.Size(43, 15);
            this.lPlotKinematicsHeight.TabIndex = 62;
            this.lPlotKinematicsHeight.Text = "Height";
            this.toolTip.SetToolTip(this.lPlotKinematicsHeight, "for each plot");
            // 
            // lpx4
            // 
            this.lpx4.AutoSize = true;
            this.lpx4.Location = new System.Drawing.Point(262, 33);
            this.lpx4.Name = "lpx4";
            this.lpx4.Size = new System.Drawing.Size(28, 15);
            this.lpx4.TabIndex = 63;
            this.lpx4.Text = "(px)";
            // 
            // lNumOfSomitesMNDynamics
            // 
            this.lNumOfSomitesMNDynamics.AutoSize = true;
            this.lNumOfSomitesMNDynamics.Location = new System.Drawing.Point(3, 8);
            this.lNumOfSomitesMNDynamics.Name = "lNumOfSomitesMNDynamics";
            this.lNumOfSomitesMNDynamics.Size = new System.Drawing.Size(73, 15);
            this.lNumOfSomitesMNDynamics.TabIndex = 43;
            this.lNumOfSomitesMNDynamics.Text = "# of Somites";
            // 
            // eKinematicsSomite
            // 
            this.eKinematicsSomite.Location = new System.Drawing.Point(82, 3);
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
            this.btnGenerateEpisodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnGenerateEpisodes.Enabled = false;
            this.btnGenerateEpisodes.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnGenerateEpisodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateEpisodes.ForeColor = System.Drawing.Color.White;
            this.btnGenerateEpisodes.Location = new System.Drawing.Point(8, 32);
            this.btnGenerateEpisodes.Name = "btnGenerateEpisodes";
            this.btnGenerateEpisodes.Size = new System.Drawing.Size(140, 24);
            this.btnGenerateEpisodes.TabIndex = 29;
            this.btnGenerateEpisodes.Text = "Calculate Episodes";
            this.toolTip.SetToolTip(this.btnGenerateEpisodes, "Calculate episodes using left/right motoneuron membrane potential changes");
            this.btnGenerateEpisodes.UseVisualStyleBackColor = false;
            this.btnGenerateEpisodes.Click += new System.EventHandler(this.btnGenerateEpisodes_Click);
            // 
            // lKinematicsTimes
            // 
            this.lKinematicsTimes.Location = new System.Drawing.Point(315, 5);
            this.lKinematicsTimes.Name = "lKinematicsTimes";
            this.lKinematicsTimes.Size = new System.Drawing.Size(294, 19);
            this.lKinematicsTimes.TabIndex = 33;
            this.lKinematicsTimes.Text = "Last kinematics:";
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
            this.webViewAnimation.Location = new System.Drawing.Point(0, 64);
            this.webViewAnimation.Name = "webViewAnimation";
            this.webViewAnimation.Size = new System.Drawing.Size(784, 577);
            this.webViewAnimation.TabIndex = 2;
            this.webViewAnimation.ZoomFactor = 1D;
            this.webViewAnimation.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            // 
            // pAnimation
            // 
            this.pAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
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
            this.pAnimation.Size = new System.Drawing.Size(784, 64);
            this.pAnimation.TabIndex = 5;
            // 
            // pLineAnimation
            // 
            this.pLineAnimation.BackColor = System.Drawing.Color.LightGray;
            this.pLineAnimation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineAnimation.Location = new System.Drawing.Point(0, 63);
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
            this.linkSaveAnimationCSV.Location = new System.Drawing.Point(653, 36);
            this.linkSaveAnimationCSV.Name = "linkSaveAnimationCSV";
            this.linkSaveAnimationCSV.Size = new System.Drawing.Size(124, 16);
            this.linkSaveAnimationCSV.TabIndex = 34;
            this.linkSaveAnimationCSV.TabStop = true;
            this.linkSaveAnimationCSV.Text = "Save Animation CSV";
            this.linkSaveAnimationCSV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.linkSaveAnimationHTML.Location = new System.Drawing.Point(639, 10);
            this.linkSaveAnimationHTML.Name = "linkSaveAnimationHTML";
            this.linkSaveAnimationHTML.Size = new System.Drawing.Size(138, 16);
            this.linkSaveAnimationHTML.TabIndex = 31;
            this.linkSaveAnimationHTML.TabStop = true;
            this.linkSaveAnimationHTML.Text = "Save Animation HTML";
            this.linkSaveAnimationHTML.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkSaveAnimationHTML.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveAnimationHTML_LinkClicked);
            // 
            // btnAnimate
            // 
            this.btnAnimate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnAnimate.Enabled = false;
            this.btnAnimate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnAnimate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimate.ForeColor = System.Drawing.Color.White;
            this.btnAnimate.Location = new System.Drawing.Point(264, 32);
            this.btnAnimate.Name = "btnAnimate";
            this.btnAnimate.Size = new System.Drawing.Size(75, 24);
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
            this.eTemplateJSON.AcceptsTab = true;
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
            this.pTemplateJSONTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
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
            this.linkSaveTemplateJSON.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveTemplateJSON.Location = new System.Drawing.Point(695, 10);
            this.linkSaveTemplateJSON.Name = "linkSaveTemplateJSON";
            this.linkSaveTemplateJSON.Size = new System.Drawing.Size(82, 16);
            this.linkSaveTemplateJSON.TabIndex = 4;
            this.linkSaveTemplateJSON.TabStop = true;
            this.linkSaveTemplateJSON.Text = "Save Template";
            this.linkSaveTemplateJSON.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkSaveTemplateJSON.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveTemplateJSON_LinkClicked);
            // 
            // btnLoadTemplateJSON
            // 
            this.btnLoadTemplateJSON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnLoadTemplateJSON.Enabled = false;
            this.btnLoadTemplateJSON.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnLoadTemplateJSON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadTemplateJSON.ForeColor = System.Drawing.Color.White;
            this.btnLoadTemplateJSON.Location = new System.Drawing.Point(88, 8);
            this.btnLoadTemplateJSON.Name = "btnLoadTemplateJSON";
            this.btnLoadTemplateJSON.Size = new System.Drawing.Size(75, 24);
            this.btnLoadTemplateJSON.TabIndex = 1;
            this.btnLoadTemplateJSON.Text = "Load";
            this.btnLoadTemplateJSON.UseVisualStyleBackColor = false;
            this.btnLoadTemplateJSON.Click += new System.EventHandler(this.btnLoadTemplateJSON_Click);
            // 
            // btnDisplayTemplateJSON
            // 
            this.btnDisplayTemplateJSON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnDisplayTemplateJSON.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDisplayTemplateJSON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisplayTemplateJSON.ForeColor = System.Drawing.Color.White;
            this.btnDisplayTemplateJSON.Location = new System.Drawing.Point(8, 8);
            this.btnDisplayTemplateJSON.Name = "btnDisplayTemplateJSON";
            this.btnDisplayTemplateJSON.Size = new System.Drawing.Size(75, 24);
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
            this.eModelJSON.AcceptsTab = true;
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
            this.pModelJSONTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
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
            this.linkSaveModelJSON.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveModelJSON.Location = new System.Drawing.Point(706, 10);
            this.linkSaveModelJSON.Name = "linkSaveModelJSON";
            this.linkSaveModelJSON.Size = new System.Drawing.Size(68, 16);
            this.linkSaveModelJSON.TabIndex = 5;
            this.linkSaveModelJSON.TabStop = true;
            this.linkSaveModelJSON.Text = "Save Model";
            this.linkSaveModelJSON.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkSaveModelJSON.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveModelJSON_LinkClicked);
            // 
            // btnLoadModelJSON
            // 
            this.btnLoadModelJSON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnLoadModelJSON.Enabled = false;
            this.btnLoadModelJSON.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnLoadModelJSON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadModelJSON.ForeColor = System.Drawing.Color.White;
            this.btnLoadModelJSON.Location = new System.Drawing.Point(88, 8);
            this.btnLoadModelJSON.Name = "btnLoadModelJSON";
            this.btnLoadModelJSON.Size = new System.Drawing.Size(75, 24);
            this.btnLoadModelJSON.TabIndex = 1;
            this.btnLoadModelJSON.Text = "Load";
            this.btnLoadModelJSON.UseVisualStyleBackColor = false;
            this.btnLoadModelJSON.Click += new System.EventHandler(this.btnLoadModelJSON_Click);
            // 
            // btnDisplayModelJSON
            // 
            this.btnDisplayModelJSON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnDisplayModelJSON.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDisplayModelJSON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisplayModelJSON.ForeColor = System.Drawing.Color.White;
            this.btnDisplayModelJSON.Location = new System.Drawing.Point(8, 8);
            this.btnDisplayModelJSON.Name = "btnDisplayModelJSON";
            this.btnDisplayModelJSON.Size = new System.Drawing.Size(75, 24);
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
            this.tabModel.ResumeLayout(false);
            this.tTemplate.ResumeLayout(false);
            this.tModel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pSimulation.ResumeLayout(false);
            this.pSimulation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtEuler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eRunNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eTimeEnd)).EndInit();
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
            this.tab3DModel.ResumeLayout(false);
            this.p3DViewing.ResumeLayout(false);
            this.grLegend.ResumeLayout(false);
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
            this.tabAnimation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewAnimation)).EndInit();
            this.pAnimation.ResumeLayout(false);
            this.pAnimation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationdt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAnimationStart)).EndInit();
            this.tabTemplateJSON.ResumeLayout(false);
            this.pTemplateJSONTop.ResumeLayout(false);
            this.tabModelJSON.ResumeLayout(false);
            this.pModelJSONTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel pTop;
        private SplitContainer splitMain;
        private Panel pSimulation;
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
        private SaveFileDialog saveFileText;
        private LinkLabel linkSaveAnimationHTML;
        private Label lRunTime;
        private NumericUpDown edt;
        private NumericUpDown eTimeEnd;
        private Label ldt;
        private NumericUpDown eSkip;
        private Label lSkip;
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
        private Label lNumOfSomitesMNDynamics;
        private NumericUpDown eKinematicsSomite;
        private Label lKinematicsTimes;
        private Button btnGenerateEpisodes;
        private SplitContainer splitKinematics;
        private RichTextBox eEpisodesLeft;
        private NumericUpDown ePlotKinematicsHeight;
        private NumericUpDown ePlotKinematicsWidth;
        private Label lPlotKinematicsWidth;
        private Label lpx3;
        private Label lPlotKinematicsHeight;
        private Label lpx4;
        private RichTextBox eEpisodesRight;
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
        private Label ldtEuler;
        private NumericUpDown edtEuler;
        private TextBox e3DSomiteRange;
        private CheckBox cb3DAllSomites;
        private LinkLabel linkExportPlotData;
        private CheckBox cb3DGapJunc;
        private CheckBox cb3DChemJunc;
        private Button btnZoomIn;
        private Button btnZoomOut;
        private Button btnAbout;
        private Label legendUnselected;
        private Label legendGap;
        private Label legendIncoming;
        private Label legendOutgoing;
        private GroupBox grLegend;
        private ComboBox dd3DViewpoint;
        private Panel p3DViewing;
        private CheckBox cb3DLegend;
        private Controls.ModelControl mcTemplate;
        private Panel panel1;
        private LinkLabel linkLoadModel;
        private Label lTitle;
        private LinkLabel linkSaveModel;
        private LinkLabel linkClearModel;
        private TabControl tabModel;
        private TabPage tTemplate;
        private TabPage tModel;
        private LinkLabel linkGenerateRunningModel;
        private ModelControl mcRunningModel;
    }
}