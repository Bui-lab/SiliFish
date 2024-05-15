﻿namespace SiliFish.UI.Controls
{
    partial class ModelOutputControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelOutputControl));
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            tabOutputs = new TabControl();
            t2DRender = new TabPage();
            gr2DCellPoolLegend = new GroupBox();
            p2DRenderOptions = new Panel();
            cb2DOffline = new CheckBox();
            cb2DShowUnselectedNodes = new CheckBox();
            cb2DHideNonspiking = new CheckBox();
            cb2DLegend = new CheckBox();
            l2DLinkSize = new Label();
            cb2DChemJunc = new CheckBox();
            ud2DLinkSize = new General.UpDownControl();
            cb2DGapJunc = new CheckBox();
            l2DNodeSize = new Label();
            ud2DNodeSize = new General.UpDownControl();
            gr2DLegend = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            p2DRender = new Panel();
            cb2DRenderShowOptions = new CheckBox();
            pLine2D = new Panel();
            linkSaveHTML2D = new LinkLabel();
            btn2DRender = new Button();
            webView2DRender = new Microsoft.Web.WebView2.WinForms.WebView2();
            t3DRender = new TabPage();
            p3DRenderOptions = new Panel();
            cb3DOffline = new CheckBox();
            l3DNodeSize = new Label();
            cb3DShowUnselectedNodes = new CheckBox();
            ud3DNodeSize = new General.UpDownControl();
            cb3DGapJunc = new CheckBox();
            cb3DLegend = new CheckBox();
            cb3DChemJunc = new CheckBox();
            e3DSomiteRange = new TextBox();
            dd3DViewpoint = new ComboBox();
            cb3DAllSomites = new CheckBox();
            btnZoomIn = new Button();
            btnZoomOut = new Button();
            gr3DLegend = new GroupBox();
            legendUnselected = new Label();
            legendOutgoing = new Label();
            legendGap = new Label();
            legendIncoming = new Label();
            p3DRender = new Panel();
            cb3DRenderShowOptions = new CheckBox();
            pLine3D = new Panel();
            linkSaveHTML3D = new LinkLabel();
            btn3DRender = new Button();
            webView3DRender = new Microsoft.Web.WebView2.WinForms.WebView2();
            tPlot = new TabPage();
            tabPlotSub = new TabControl();
            tPlotHTML = new TabPage();
            webViewPlot = new Microsoft.Web.WebView2.WinForms.WebView2();
            tPlotWindows = new TabPage();
            pPictureBox = new Panel();
            pictureBox = new PictureBox();
            cmPlotImageSave = new ContextMenuStrip(components);
            cmiPlotImageSave = new ToolStripMenuItem();
            pPlot = new Panel();
            timeRangePlot = new General.StartEndTimeControl();
            btnPlotHTML = new Button();
            cmPlot = new ContextMenuStrip(components);
            cmiNonInteractivePlot = new ToolStripMenuItem();
            ddPlot = new ComboBox();
            listPlotHistory = new HistoryListControl();
            cellSelectionPlot = new Display.CellSelectionControl();
            cbPlotHistory = new CheckBox();
            linkExportPlotData = new LinkLabel();
            pLinePlots = new Panel();
            linkSaveHTMLPlots = new LinkLabel();
            lPlotPlot = new Label();
            tStats = new TabPage();
            tabStats = new TabControl();
            tSpikes = new TabPage();
            dgSpikeStats = new DataGridView();
            cmGrid = new ContextMenuStrip(components);
            cmGridExport = new ToolStripMenuItem();
            cmGridCopyAll = new ToolStripMenuItem();
            tRCTrains = new TabPage();
            splitRCTrains = new SplitContainer();
            dgRCTrains = new DataGridView();
            colRCTrainNumber = new DataGridViewTextBoxColumn();
            colRCTrainCellGroup = new DataGridViewTextBoxColumn();
            colRCTrainSomite = new DataGridViewTextBoxColumn();
            colRCTrainStart = new DataGridViewTextBoxColumn();
            colRCTrainEnd = new DataGridViewTextBoxColumn();
            colRCTrainMidPoint = new DataGridViewTextBoxColumn();
            colRCTrainCenter = new DataGridViewTextBoxColumn();
            colRCTrainStartDelay = new DataGridViewTextBoxColumn();
            colRCTrainMidPointDelay = new DataGridViewTextBoxColumn();
            colRCTrainCenterDelay = new DataGridViewTextBoxColumn();
            webViewRCTrains = new Microsoft.Web.WebView2.WinForms.WebView2();
            tSpikeSummary = new TabPage();
            splitContainer1 = new SplitContainer();
            dgSpikeSummary = new DataGridView();
            colSumPeriod = new DataGridViewTextBoxColumn();
            colSumCellPool = new DataGridViewTextBoxColumn();
            colSumSpikeCount = new DataGridViewTextBoxColumn();
            dgSpikeSummaryDetails = new DataGridView();
            colSumDetailsPeriod = new DataGridViewTextBoxColumn();
            colSumDetailsCellID = new DataGridViewTextBoxColumn();
            colSumDetailsSagittal = new DataGridViewTextBoxColumn();
            colSumDetailsCellPool = new DataGridViewTextBoxColumn();
            colSumDetailsSomite = new DataGridViewTextBoxColumn();
            colSumDetailsCellSeq = new DataGridViewTextBoxColumn();
            colSumDetailsSpikeCount = new DataGridViewTextBoxColumn();
            tEpisodes = new TabPage();
            dgEpisodes = new DataGridView();
            panel1 = new Panel();
            timeRangeStat = new General.StartEndTimeControl();
            cbStatsAppend = new CheckBox();
            btnListStats = new Button();
            cellSelectionStats = new Display.CellSelectionControl();
            panel3 = new Panel();
            tAnimation = new TabPage();
            webViewAnimation = new Microsoft.Web.WebView2.WinForms.WebView2();
            pAnimation = new Panel();
            timeRangeAnimation = new General.StartEndTimeControl();
            pLineAnimation = new Panel();
            eAnimationdt = new NumericUpDown();
            lAnimationdt = new Label();
            linkSaveAnimationCSV = new LinkLabel();
            lAnimationTime = new Label();
            linkSaveAnimationHTML = new LinkLabel();
            btnAnimate = new Button();
            cmAnimation = new ContextMenuStrip(components);
            cmiAnimationClearCache = new ToolStripMenuItem();
            saveFileHTML = new SaveFileDialog();
            saveFileJson = new SaveFileDialog();
            openFileJson = new OpenFileDialog();
            toolTip = new ToolTip(components);
            saveFileText = new SaveFileDialog();
            saveFileCSV = new SaveFileDialog();
            saveFileImage = new SaveFileDialog();
            tabOutputs.SuspendLayout();
            t2DRender.SuspendLayout();
            p2DRenderOptions.SuspendLayout();
            gr2DLegend.SuspendLayout();
            p2DRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2DRender).BeginInit();
            t3DRender.SuspendLayout();
            p3DRenderOptions.SuspendLayout();
            gr3DLegend.SuspendLayout();
            p3DRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView3DRender).BeginInit();
            tPlot.SuspendLayout();
            tabPlotSub.SuspendLayout();
            tPlotHTML.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewPlot).BeginInit();
            tPlotWindows.SuspendLayout();
            pPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            cmPlotImageSave.SuspendLayout();
            pPlot.SuspendLayout();
            cmPlot.SuspendLayout();
            tStats.SuspendLayout();
            tabStats.SuspendLayout();
            tSpikes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgSpikeStats).BeginInit();
            cmGrid.SuspendLayout();
            tRCTrains.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitRCTrains).BeginInit();
            splitRCTrains.Panel1.SuspendLayout();
            splitRCTrains.Panel2.SuspendLayout();
            splitRCTrains.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgRCTrains).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewRCTrains).BeginInit();
            tSpikeSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgSpikeSummary).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgSpikeSummaryDetails).BeginInit();
            tEpisodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgEpisodes).BeginInit();
            panel1.SuspendLayout();
            tAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewAnimation).BeginInit();
            pAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)eAnimationdt).BeginInit();
            cmAnimation.SuspendLayout();
            SuspendLayout();
            // 
            // tabOutputs
            // 
            tabOutputs.Controls.Add(t2DRender);
            tabOutputs.Controls.Add(t3DRender);
            tabOutputs.Controls.Add(tPlot);
            tabOutputs.Controls.Add(tStats);
            tabOutputs.Controls.Add(tAnimation);
            tabOutputs.Dock = DockStyle.Fill;
            tabOutputs.Location = new Point(0, 0);
            tabOutputs.Name = "tabOutputs";
            tabOutputs.SelectedIndex = 0;
            tabOutputs.Size = new Size(702, 795);
            tabOutputs.TabIndex = 2;
            tabOutputs.SelectedIndexChanged += tabOutputs_SelectedIndexChanged;
            // 
            // t2DRender
            // 
            t2DRender.Controls.Add(gr2DCellPoolLegend);
            t2DRender.Controls.Add(p2DRenderOptions);
            t2DRender.Controls.Add(gr2DLegend);
            t2DRender.Controls.Add(p2DRender);
            t2DRender.Controls.Add(webView2DRender);
            t2DRender.Location = new Point(4, 24);
            t2DRender.Name = "t2DRender";
            t2DRender.Size = new Size(694, 767);
            t2DRender.TabIndex = 2;
            t2DRender.Text = "2D Rendering";
            t2DRender.UseVisualStyleBackColor = true;
            // 
            // gr2DCellPoolLegend
            // 
            gr2DCellPoolLegend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gr2DCellPoolLegend.AutoSize = true;
            gr2DCellPoolLegend.Location = new Point(554, 144);
            gr2DCellPoolLegend.Name = "gr2DCellPoolLegend";
            gr2DCellPoolLegend.Size = new Size(122, 94);
            gr2DCellPoolLegend.TabIndex = 55;
            gr2DCellPoolLegend.TabStop = false;
            gr2DCellPoolLegend.Text = "Cell Pool Legend";
            // 
            // p2DRenderOptions
            // 
            p2DRenderOptions.BackColor = Color.FromArgb(236, 239, 241);
            p2DRenderOptions.Controls.Add(cb2DOffline);
            p2DRenderOptions.Controls.Add(cb2DShowUnselectedNodes);
            p2DRenderOptions.Controls.Add(cb2DHideNonspiking);
            p2DRenderOptions.Controls.Add(cb2DLegend);
            p2DRenderOptions.Controls.Add(l2DLinkSize);
            p2DRenderOptions.Controls.Add(cb2DChemJunc);
            p2DRenderOptions.Controls.Add(ud2DLinkSize);
            p2DRenderOptions.Controls.Add(cb2DGapJunc);
            p2DRenderOptions.Controls.Add(l2DNodeSize);
            p2DRenderOptions.Controls.Add(ud2DNodeSize);
            p2DRenderOptions.Dock = DockStyle.Top;
            p2DRenderOptions.Location = new Point(0, 40);
            p2DRenderOptions.Name = "p2DRenderOptions";
            p2DRenderOptions.Size = new Size(694, 85);
            p2DRenderOptions.TabIndex = 55;
            p2DRenderOptions.Visible = false;
            // 
            // cb2DOffline
            // 
            cb2DOffline.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cb2DOffline.AutoSize = true;
            cb2DOffline.Location = new Point(625, 30);
            cb2DOffline.Name = "cb2DOffline";
            cb2DOffline.Size = new Size(62, 19);
            cb2DOffline.TabIndex = 75;
            cb2DOffline.Text = "Offline";
            cb2DOffline.UseVisualStyleBackColor = true;
            // 
            // cb2DShowUnselectedNodes
            // 
            cb2DShowUnselectedNodes.Checked = true;
            cb2DShowUnselectedNodes.CheckState = CheckState.Checked;
            cb2DShowUnselectedNodes.Location = new Point(145, 58);
            cb2DShowUnselectedNodes.Name = "cb2DShowUnselectedNodes";
            cb2DShowUnselectedNodes.Size = new Size(180, 20);
            cb2DShowUnselectedNodes.TabIndex = 74;
            cb2DShowUnselectedNodes.Text = "Show Unselected Nodes";
            cb2DShowUnselectedNodes.UseVisualStyleBackColor = true;
            cb2DShowUnselectedNodes.CheckedChanged += cb2DShowUnselectedNodes_CheckedChanged;
            // 
            // cb2DHideNonspiking
            // 
            cb2DHideNonspiking.AutoSize = true;
            cb2DHideNonspiking.Location = new Point(8, 6);
            cb2DHideNonspiking.Name = "cb2DHideNonspiking";
            cb2DHideNonspiking.Size = new Size(120, 19);
            cb2DHideNonspiking.TabIndex = 73;
            cb2DHideNonspiking.Text = "Hide Non-spiking";
            cb2DHideNonspiking.UseVisualStyleBackColor = true;
            cb2DHideNonspiking.CheckedChanged += cb2DHideNonspiking_CheckedChanged;
            // 
            // cb2DLegend
            // 
            cb2DLegend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cb2DLegend.AutoSize = true;
            cb2DLegend.Checked = true;
            cb2DLegend.CheckState = CheckState.Checked;
            cb2DLegend.Location = new Point(625, 8);
            cb2DLegend.Name = "cb2DLegend";
            cb2DLegend.Size = new Size(65, 19);
            cb2DLegend.TabIndex = 60;
            cb2DLegend.Text = "Legend";
            cb2DLegend.UseVisualStyleBackColor = true;
            cb2DLegend.CheckedChanged += cb2DLegend_CheckedChanged;
            // 
            // l2DLinkSize
            // 
            l2DLinkSize.AutoSize = true;
            l2DLinkSize.Location = new Point(249, 32);
            l2DLinkSize.Name = "l2DLinkSize";
            l2DLinkSize.Size = new Size(64, 15);
            l2DLinkSize.TabIndex = 71;
            l2DLinkSize.Text = "Link Width";
            l2DLinkSize.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cb2DChemJunc
            // 
            cb2DChemJunc.Checked = true;
            cb2DChemJunc.CheckState = CheckState.Checked;
            cb2DChemJunc.Location = new Point(145, 6);
            cb2DChemJunc.Name = "cb2DChemJunc";
            cb2DChemJunc.Size = new Size(85, 20);
            cb2DChemJunc.TabIndex = 58;
            cb2DChemJunc.Text = "Chem Junc";
            cb2DChemJunc.UseVisualStyleBackColor = true;
            cb2DChemJunc.CheckedChanged += cb2DChemJunc_CheckedChanged;
            // 
            // ud2DLinkSize
            // 
            ud2DLinkSize.Items.Add(" ");
            ud2DLinkSize.Items.Add("  ");
            ud2DLinkSize.Items.Add("   ");
            ud2DLinkSize.Items.Add("    ");
            ud2DLinkSize.Location = new Point(314, 30);
            ud2DLinkSize.Name = "ud2DLinkSize";
            ud2DLinkSize.Size = new Size(19, 23);
            ud2DLinkSize.TabIndex = 70;
            ud2DLinkSize.Wrap = true;
            ud2DLinkSize.SelectedItemChanged += ud2DLinkSize_SelectedItemChanged;
            // 
            // cb2DGapJunc
            // 
            cb2DGapJunc.Checked = true;
            cb2DGapJunc.CheckState = CheckState.Checked;
            cb2DGapJunc.Location = new Point(145, 32);
            cb2DGapJunc.Name = "cb2DGapJunc";
            cb2DGapJunc.Size = new Size(80, 20);
            cb2DGapJunc.TabIndex = 59;
            cb2DGapJunc.Text = "Gap Junc";
            cb2DGapJunc.UseVisualStyleBackColor = true;
            cb2DGapJunc.CheckedChanged += cb2DGapJunc_CheckedChanged;
            // 
            // l2DNodeSize
            // 
            l2DNodeSize.AutoSize = true;
            l2DNodeSize.Location = new Point(249, 6);
            l2DNodeSize.Name = "l2DNodeSize";
            l2DNodeSize.Size = new Size(59, 15);
            l2DNodeSize.TabIndex = 69;
            l2DNodeSize.Text = "Node Size";
            l2DNodeSize.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ud2DNodeSize
            // 
            ud2DNodeSize.Items.Add(" ");
            ud2DNodeSize.Items.Add("  ");
            ud2DNodeSize.Items.Add("   ");
            ud2DNodeSize.Items.Add("    ");
            ud2DNodeSize.Location = new Point(314, 4);
            ud2DNodeSize.Name = "ud2DNodeSize";
            ud2DNodeSize.Size = new Size(19, 23);
            ud2DNodeSize.TabIndex = 68;
            ud2DNodeSize.Wrap = true;
            ud2DNodeSize.SelectedItemChanged += ud2DNodeSize_SelectedItemChanged;
            // 
            // gr2DLegend
            // 
            gr2DLegend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            gr2DLegend.Controls.Add(label1);
            gr2DLegend.Controls.Add(label2);
            gr2DLegend.Controls.Add(label3);
            gr2DLegend.Controls.Add(label4);
            gr2DLegend.Location = new Point(554, 656);
            gr2DLegend.Name = "gr2DLegend";
            gr2DLegend.Size = new Size(122, 94);
            gr2DLegend.TabIndex = 54;
            gr2DLegend.TabStop = false;
            gr2DLegend.Text = "Projection Legend";
            // 
            // label1
            // 
            label1.BackColor = Color.Gray;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(6, 17);
            label1.Name = "label1";
            label1.Size = new Size(110, 18);
            label1.TabIndex = 49;
            label1.Text = "Cholinergic";
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(0, 204, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(6, 71);
            label2.Name = "label2";
            label2.Size = new Size(110, 18);
            label2.TabIndex = 52;
            label2.Text = "Excitatory Projections";
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(51, 51, 51);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(6, 35);
            label3.Name = "label3";
            label3.Size = new Size(110, 18);
            label3.TabIndex = 50;
            label3.Text = "Gap junc";
            // 
            // label4
            // 
            label4.BackColor = Color.FromArgb(204, 0, 0);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(6, 53);
            label4.Name = "label4";
            label4.Size = new Size(110, 18);
            label4.TabIndex = 51;
            label4.Text = "Inhibitory";
            // 
            // p2DRender
            // 
            p2DRender.BackColor = Color.FromArgb(236, 239, 241);
            p2DRender.Controls.Add(cb2DRenderShowOptions);
            p2DRender.Controls.Add(pLine2D);
            p2DRender.Controls.Add(linkSaveHTML2D);
            p2DRender.Controls.Add(btn2DRender);
            p2DRender.Dock = DockStyle.Top;
            p2DRender.Location = new Point(0, 0);
            p2DRender.Name = "p2DRender";
            p2DRender.Size = new Size(694, 40);
            p2DRender.TabIndex = 2;
            // 
            // cb2DRenderShowOptions
            // 
            cb2DRenderShowOptions.AutoSize = true;
            cb2DRenderShowOptions.Location = new Point(145, 12);
            cb2DRenderShowOptions.Name = "cb2DRenderShowOptions";
            cb2DRenderShowOptions.Size = new Size(100, 19);
            cb2DRenderShowOptions.TabIndex = 25;
            cb2DRenderShowOptions.Text = "Show Options";
            cb2DRenderShowOptions.UseVisualStyleBackColor = true;
            cb2DRenderShowOptions.CheckedChanged += cb2DRenderShowOptions_CheckedChanged;
            // 
            // pLine2D
            // 
            pLine2D.BackColor = Color.LightGray;
            pLine2D.Dock = DockStyle.Bottom;
            pLine2D.Location = new Point(0, 39);
            pLine2D.Name = "pLine2D";
            pLine2D.Size = new Size(694, 1);
            pLine2D.TabIndex = 24;
            // 
            // linkSaveHTML2D
            // 
            linkSaveHTML2D.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkSaveHTML2D.AutoSize = true;
            linkSaveHTML2D.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveHTML2D.Location = new Point(621, 13);
            linkSaveHTML2D.Name = "linkSaveHTML2D";
            linkSaveHTML2D.Size = new Size(69, 15);
            linkSaveHTML2D.TabIndex = 23;
            linkSaveHTML2D.TabStop = true;
            linkSaveHTML2D.Text = "Save HTML ";
            linkSaveHTML2D.TextAlign = ContentAlignment.MiddleRight;
            linkSaveHTML2D.LinkClicked += linkSaveHTML2D_LinkClicked;
            // 
            // btn2DRender
            // 
            btn2DRender.BackColor = Color.FromArgb(96, 125, 139);
            btn2DRender.FlatAppearance.BorderColor = Color.LightGray;
            btn2DRender.FlatStyle = FlatStyle.Flat;
            btn2DRender.ForeColor = Color.White;
            btn2DRender.Location = new Point(8, 8);
            btn2DRender.Name = "btn2DRender";
            btn2DRender.Size = new Size(120, 24);
            btn2DRender.TabIndex = 22;
            btn2DRender.Text = "Render";
            btn2DRender.UseVisualStyleBackColor = false;
            btn2DRender.Click += btn2DRender_Click;
            // 
            // webView2DRender
            // 
            webView2DRender.AllowExternalDrop = true;
            webView2DRender.BackColor = Color.White;
            webView2DRender.CreationProperties = null;
            webView2DRender.DefaultBackgroundColor = Color.White;
            webView2DRender.Dock = DockStyle.Fill;
            webView2DRender.Location = new Point(0, 0);
            webView2DRender.Name = "webView2DRender";
            webView2DRender.Padding = new Padding(10);
            webView2DRender.Size = new Size(694, 767);
            webView2DRender.TabIndex = 1;
            webView2DRender.ZoomFactor = 1D;
            webView2DRender.CoreWebView2InitializationCompleted += webView_CoreWebView2InitializationCompleted;
            // 
            // t3DRender
            // 
            t3DRender.Controls.Add(p3DRenderOptions);
            t3DRender.Controls.Add(gr3DLegend);
            t3DRender.Controls.Add(p3DRender);
            t3DRender.Controls.Add(webView3DRender);
            t3DRender.Location = new Point(4, 24);
            t3DRender.Name = "t3DRender";
            t3DRender.Size = new Size(694, 767);
            t3DRender.TabIndex = 0;
            t3DRender.Text = "3D Rendering";
            t3DRender.UseVisualStyleBackColor = true;
            // 
            // p3DRenderOptions
            // 
            p3DRenderOptions.BackColor = Color.FromArgb(236, 239, 241);
            p3DRenderOptions.Controls.Add(cb3DOffline);
            p3DRenderOptions.Controls.Add(l3DNodeSize);
            p3DRenderOptions.Controls.Add(cb3DShowUnselectedNodes);
            p3DRenderOptions.Controls.Add(ud3DNodeSize);
            p3DRenderOptions.Controls.Add(cb3DGapJunc);
            p3DRenderOptions.Controls.Add(cb3DLegend);
            p3DRenderOptions.Controls.Add(cb3DChemJunc);
            p3DRenderOptions.Controls.Add(e3DSomiteRange);
            p3DRenderOptions.Controls.Add(dd3DViewpoint);
            p3DRenderOptions.Controls.Add(cb3DAllSomites);
            p3DRenderOptions.Controls.Add(btnZoomIn);
            p3DRenderOptions.Controls.Add(btnZoomOut);
            p3DRenderOptions.Dock = DockStyle.Top;
            p3DRenderOptions.Location = new Point(0, 40);
            p3DRenderOptions.Name = "p3DRenderOptions";
            p3DRenderOptions.Size = new Size(694, 85);
            p3DRenderOptions.TabIndex = 58;
            p3DRenderOptions.Visible = false;
            // 
            // cb3DOffline
            // 
            cb3DOffline.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cb3DOffline.AutoSize = true;
            cb3DOffline.Location = new Point(625, 29);
            cb3DOffline.Name = "cb3DOffline";
            cb3DOffline.Size = new Size(62, 19);
            cb3DOffline.TabIndex = 76;
            cb3DOffline.Text = "Offline";
            cb3DOffline.UseVisualStyleBackColor = true;
            // 
            // l3DNodeSize
            // 
            l3DNodeSize.AutoSize = true;
            l3DNodeSize.Location = new Point(249, 6);
            l3DNodeSize.Name = "l3DNodeSize";
            l3DNodeSize.Size = new Size(59, 15);
            l3DNodeSize.TabIndex = 67;
            l3DNodeSize.Text = "Node Size";
            // 
            // cb3DShowUnselectedNodes
            // 
            cb3DShowUnselectedNodes.Checked = true;
            cb3DShowUnselectedNodes.CheckState = CheckState.Checked;
            cb3DShowUnselectedNodes.Location = new Point(145, 58);
            cb3DShowUnselectedNodes.Name = "cb3DShowUnselectedNodes";
            cb3DShowUnselectedNodes.Size = new Size(180, 20);
            cb3DShowUnselectedNodes.TabIndex = 58;
            cb3DShowUnselectedNodes.Text = "Show Unselected Nodes";
            cb3DShowUnselectedNodes.UseVisualStyleBackColor = true;
            cb3DShowUnselectedNodes.CheckedChanged += cb3DShowUnselectedNodes_CheckedChanged;
            // 
            // ud3DNodeSize
            // 
            ud3DNodeSize.Items.Add(" ");
            ud3DNodeSize.Items.Add("  ");
            ud3DNodeSize.Items.Add("   ");
            ud3DNodeSize.Items.Add("    ");
            ud3DNodeSize.Location = new Point(314, 4);
            ud3DNodeSize.Name = "ud3DNodeSize";
            ud3DNodeSize.Size = new Size(19, 23);
            ud3DNodeSize.TabIndex = 66;
            ud3DNodeSize.Wrap = true;
            ud3DNodeSize.SelectedItemChanged += ud3DNodeSize_SelectedItemChanged;
            // 
            // cb3DGapJunc
            // 
            cb3DGapJunc.Checked = true;
            cb3DGapJunc.CheckState = CheckState.Checked;
            cb3DGapJunc.Location = new Point(145, 32);
            cb3DGapJunc.Name = "cb3DGapJunc";
            cb3DGapJunc.Size = new Size(80, 20);
            cb3DGapJunc.TabIndex = 39;
            cb3DGapJunc.Text = "Gap Junc";
            cb3DGapJunc.UseVisualStyleBackColor = true;
            cb3DGapJunc.CheckedChanged += cb3DGapJunc_CheckedChanged;
            // 
            // cb3DLegend
            // 
            cb3DLegend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cb3DLegend.AutoSize = true;
            cb3DLegend.Checked = true;
            cb3DLegend.CheckState = CheckState.Checked;
            cb3DLegend.Location = new Point(625, 8);
            cb3DLegend.Name = "cb3DLegend";
            cb3DLegend.Size = new Size(65, 19);
            cb3DLegend.TabIndex = 63;
            cb3DLegend.Text = "Legend";
            cb3DLegend.UseVisualStyleBackColor = true;
            cb3DLegend.CheckedChanged += cb3DLegend_CheckedChanged;
            // 
            // cb3DChemJunc
            // 
            cb3DChemJunc.Checked = true;
            cb3DChemJunc.CheckState = CheckState.Checked;
            cb3DChemJunc.Location = new Point(145, 6);
            cb3DChemJunc.Name = "cb3DChemJunc";
            cb3DChemJunc.Size = new Size(85, 20);
            cb3DChemJunc.TabIndex = 38;
            cb3DChemJunc.Text = "Chem Junc";
            cb3DChemJunc.UseVisualStyleBackColor = true;
            cb3DChemJunc.CheckedChanged += cb3DChemJunc_CheckedChanged;
            // 
            // e3DSomiteRange
            // 
            e3DSomiteRange.Location = new Point(8, 29);
            e3DSomiteRange.Name = "e3DSomiteRange";
            e3DSomiteRange.Size = new Size(89, 23);
            e3DSomiteRange.TabIndex = 36;
            e3DSomiteRange.Text = "Ex: 1, 3-5";
            e3DSomiteRange.Visible = false;
            e3DSomiteRange.Enter += e3DSomiteRange_Enter;
            e3DSomiteRange.Leave += e3DSomiteRange_Leave;
            // 
            // dd3DViewpoint
            // 
            dd3DViewpoint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dd3DViewpoint.DropDownStyle = ComboBoxStyle.DropDownList;
            dd3DViewpoint.FormattingEnabled = true;
            dd3DViewpoint.Items.AddRange(new object[] { "Free view", "Dorsal view", "Ventral view", "Rostral view", "Caudal view", "Lateral view (left)", "Lateral view (right)" });
            dd3DViewpoint.Location = new Point(446, 51);
            dd3DViewpoint.Name = "dd3DViewpoint";
            dd3DViewpoint.Size = new Size(175, 23);
            dd3DViewpoint.TabIndex = 54;
            dd3DViewpoint.SelectedIndexChanged += dd3DViewpoint_SelectedIndexChanged;
            // 
            // cb3DAllSomites
            // 
            cb3DAllSomites.Checked = true;
            cb3DAllSomites.CheckState = CheckState.Checked;
            cb3DAllSomites.Location = new Point(8, 6);
            cb3DAllSomites.Name = "cb3DAllSomites";
            cb3DAllSomites.Size = new Size(85, 20);
            cb3DAllSomites.TabIndex = 35;
            cb3DAllSomites.Text = "All Somites";
            cb3DAllSomites.UseVisualStyleBackColor = true;
            cb3DAllSomites.CheckedChanged += cb3DAllSomites_CheckedChanged;
            // 
            // btnZoomIn
            // 
            btnZoomIn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnZoomIn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnZoomIn.Image = (Image)resources.GetObject("btnZoomIn.Image");
            btnZoomIn.ImageAlign = ContentAlignment.TopLeft;
            btnZoomIn.Location = new Point(658, 46);
            btnZoomIn.Name = "btnZoomIn";
            btnZoomIn.Size = new Size(32, 32);
            btnZoomIn.TabIndex = 48;
            btnZoomIn.UseVisualStyleBackColor = true;
            btnZoomIn.Click += btnZoomIn_Click;
            // 
            // btnZoomOut
            // 
            btnZoomOut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnZoomOut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnZoomOut.Image = (Image)resources.GetObject("btnZoomOut.Image");
            btnZoomOut.ImageAlign = ContentAlignment.TopLeft;
            btnZoomOut.Location = new Point(624, 46);
            btnZoomOut.Name = "btnZoomOut";
            btnZoomOut.Size = new Size(32, 32);
            btnZoomOut.TabIndex = 47;
            btnZoomOut.UseVisualStyleBackColor = true;
            btnZoomOut.Click += btnZoomOut_Click;
            // 
            // gr3DLegend
            // 
            gr3DLegend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            gr3DLegend.Controls.Add(legendUnselected);
            gr3DLegend.Controls.Add(legendOutgoing);
            gr3DLegend.Controls.Add(legendGap);
            gr3DLegend.Controls.Add(legendIncoming);
            gr3DLegend.Location = new Point(554, 656);
            gr3DLegend.Name = "gr3DLegend";
            gr3DLegend.Size = new Size(122, 94);
            gr3DLegend.TabIndex = 53;
            gr3DLegend.TabStop = false;
            gr3DLegend.Text = "Projection Legend";
            // 
            // legendUnselected
            // 
            legendUnselected.BackColor = Color.Gray;
            legendUnselected.ForeColor = SystemColors.ControlLightLight;
            legendUnselected.Location = new Point(6, 17);
            legendUnselected.Name = "legendUnselected";
            legendUnselected.Size = new Size(110, 18);
            legendUnselected.TabIndex = 49;
            legendUnselected.Text = "Unselected";
            // 
            // legendOutgoing
            // 
            legendOutgoing.BackColor = Color.FromArgb(0, 204, 0);
            legendOutgoing.ForeColor = SystemColors.ControlLightLight;
            legendOutgoing.Location = new Point(6, 71);
            legendOutgoing.Name = "legendOutgoing";
            legendOutgoing.Size = new Size(110, 18);
            legendOutgoing.TabIndex = 52;
            legendOutgoing.Text = "Outgoing";
            // 
            // legendGap
            // 
            legendGap.BackColor = Color.FromArgb(51, 51, 51);
            legendGap.ForeColor = SystemColors.ControlLightLight;
            legendGap.Location = new Point(6, 35);
            legendGap.Name = "legendGap";
            legendGap.Size = new Size(110, 18);
            legendGap.TabIndex = 50;
            legendGap.Text = "Gap junc";
            // 
            // legendIncoming
            // 
            legendIncoming.BackColor = Color.FromArgb(204, 0, 0);
            legendIncoming.ForeColor = SystemColors.ControlLightLight;
            legendIncoming.Location = new Point(6, 53);
            legendIncoming.Name = "legendIncoming";
            legendIncoming.Size = new Size(110, 18);
            legendIncoming.TabIndex = 51;
            legendIncoming.Text = "Incoming";
            // 
            // p3DRender
            // 
            p3DRender.BackColor = Color.FromArgb(236, 239, 241);
            p3DRender.Controls.Add(cb3DRenderShowOptions);
            p3DRender.Controls.Add(pLine3D);
            p3DRender.Controls.Add(linkSaveHTML3D);
            p3DRender.Controls.Add(btn3DRender);
            p3DRender.Dock = DockStyle.Top;
            p3DRender.Location = new Point(0, 0);
            p3DRender.Name = "p3DRender";
            p3DRender.Size = new Size(694, 40);
            p3DRender.TabIndex = 3;
            // 
            // cb3DRenderShowOptions
            // 
            cb3DRenderShowOptions.AutoSize = true;
            cb3DRenderShowOptions.Location = new Point(145, 12);
            cb3DRenderShowOptions.Name = "cb3DRenderShowOptions";
            cb3DRenderShowOptions.Size = new Size(100, 19);
            cb3DRenderShowOptions.TabIndex = 59;
            cb3DRenderShowOptions.Text = "Show Options";
            cb3DRenderShowOptions.UseVisualStyleBackColor = true;
            cb3DRenderShowOptions.CheckedChanged += cb3DRenderShowOptions_CheckedChanged;
            // 
            // pLine3D
            // 
            pLine3D.BackColor = Color.LightGray;
            pLine3D.Dock = DockStyle.Bottom;
            pLine3D.Location = new Point(0, 39);
            pLine3D.Name = "pLine3D";
            pLine3D.Size = new Size(694, 1);
            pLine3D.TabIndex = 30;
            // 
            // linkSaveHTML3D
            // 
            linkSaveHTML3D.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkSaveHTML3D.AutoSize = true;
            linkSaveHTML3D.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveHTML3D.Location = new Point(621, 13);
            linkSaveHTML3D.Name = "linkSaveHTML3D";
            linkSaveHTML3D.Size = new Size(69, 15);
            linkSaveHTML3D.TabIndex = 27;
            linkSaveHTML3D.TabStop = true;
            linkSaveHTML3D.Text = "Save HTML ";
            linkSaveHTML3D.TextAlign = ContentAlignment.MiddleRight;
            linkSaveHTML3D.LinkClicked += linkSaveHTML3D_LinkClicked;
            // 
            // btn3DRender
            // 
            btn3DRender.BackColor = Color.FromArgb(96, 125, 139);
            btn3DRender.FlatAppearance.BorderColor = Color.LightGray;
            btn3DRender.FlatStyle = FlatStyle.Flat;
            btn3DRender.ForeColor = Color.White;
            btn3DRender.Location = new Point(8, 8);
            btn3DRender.Name = "btn3DRender";
            btn3DRender.Size = new Size(120, 24);
            btn3DRender.TabIndex = 26;
            btn3DRender.Text = "Render";
            btn3DRender.UseVisualStyleBackColor = false;
            btn3DRender.Click += btn3DRender_Click;
            // 
            // webView3DRender
            // 
            webView3DRender.AllowExternalDrop = true;
            webView3DRender.BackColor = Color.White;
            webView3DRender.CreationProperties = null;
            webView3DRender.DefaultBackgroundColor = Color.White;
            webView3DRender.Dock = DockStyle.Fill;
            webView3DRender.Location = new Point(0, 0);
            webView3DRender.Name = "webView3DRender";
            webView3DRender.Padding = new Padding(10);
            webView3DRender.Size = new Size(694, 767);
            webView3DRender.TabIndex = 0;
            webView3DRender.ZoomFactor = 1D;
            webView3DRender.CoreWebView2InitializationCompleted += webView_CoreWebView2InitializationCompleted;
            // 
            // tPlot
            // 
            tPlot.Controls.Add(tabPlotSub);
            tPlot.Controls.Add(pPlot);
            tPlot.Location = new Point(4, 24);
            tPlot.Name = "tPlot";
            tPlot.Padding = new Padding(3);
            tPlot.Size = new Size(694, 767);
            tPlot.TabIndex = 4;
            tPlot.Text = "Plots";
            tPlot.UseVisualStyleBackColor = true;
            // 
            // tabPlotSub
            // 
            tabPlotSub.Controls.Add(tPlotHTML);
            tabPlotSub.Controls.Add(tPlotWindows);
            tabPlotSub.Dock = DockStyle.Fill;
            tabPlotSub.Location = new Point(3, 143);
            tabPlotSub.Name = "tabPlotSub";
            tabPlotSub.SelectedIndex = 0;
            tabPlotSub.Size = new Size(688, 621);
            tabPlotSub.TabIndex = 6;
            // 
            // tPlotHTML
            // 
            tPlotHTML.Controls.Add(webViewPlot);
            tPlotHTML.Location = new Point(4, 24);
            tPlotHTML.Name = "tPlotHTML";
            tPlotHTML.Padding = new Padding(3);
            tPlotHTML.Size = new Size(680, 593);
            tPlotHTML.TabIndex = 1;
            tPlotHTML.Text = "HTML Plots";
            tPlotHTML.UseVisualStyleBackColor = true;
            // 
            // webViewPlot
            // 
            webViewPlot.AllowExternalDrop = true;
            webViewPlot.BackColor = Color.White;
            webViewPlot.CreationProperties = null;
            webViewPlot.DefaultBackgroundColor = Color.White;
            webViewPlot.Dock = DockStyle.Fill;
            webViewPlot.Location = new Point(3, 3);
            webViewPlot.Name = "webViewPlot";
            webViewPlot.Size = new Size(674, 587);
            webViewPlot.TabIndex = 1;
            webViewPlot.ZoomFactor = 1D;
            webViewPlot.CoreWebView2InitializationCompleted += webView_CoreWebView2InitializationCompleted;
            // 
            // tPlotWindows
            // 
            tPlotWindows.AutoScroll = true;
            tPlotWindows.Controls.Add(pPictureBox);
            tPlotWindows.Location = new Point(4, 24);
            tPlotWindows.Name = "tPlotWindows";
            tPlotWindows.Padding = new Padding(3);
            tPlotWindows.Size = new Size(680, 593);
            tPlotWindows.TabIndex = 0;
            tPlotWindows.Text = "Image Plots";
            // 
            // pPictureBox
            // 
            pPictureBox.AutoScroll = true;
            pPictureBox.AutoSize = true;
            pPictureBox.Controls.Add(pictureBox);
            pPictureBox.Dock = DockStyle.Fill;
            pPictureBox.Location = new Point(3, 3);
            pPictureBox.Name = "pPictureBox";
            pPictureBox.Size = new Size(674, 587);
            pPictureBox.TabIndex = 1;
            // 
            // pictureBox
            // 
            pictureBox.ContextMenuStrip = cmPlotImageSave;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(104, 45);
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // cmPlotImageSave
            // 
            cmPlotImageSave.Items.AddRange(new ToolStripItem[] { cmiPlotImageSave });
            cmPlotImageSave.Name = "cmImageSave";
            cmPlotImageSave.Size = new Size(135, 26);
            // 
            // cmiPlotImageSave
            // 
            cmiPlotImageSave.Name = "cmiPlotImageSave";
            cmiPlotImageSave.Size = new Size(134, 22);
            cmiPlotImageSave.Text = "Save Image";
            cmiPlotImageSave.Click += cmiPlotImageSave_Click;
            // 
            // pPlot
            // 
            pPlot.BackColor = Color.FromArgb(236, 239, 241);
            pPlot.Controls.Add(timeRangePlot);
            pPlot.Controls.Add(btnPlotHTML);
            pPlot.Controls.Add(ddPlot);
            pPlot.Controls.Add(listPlotHistory);
            pPlot.Controls.Add(cellSelectionPlot);
            pPlot.Controls.Add(cbPlotHistory);
            pPlot.Controls.Add(linkExportPlotData);
            pPlot.Controls.Add(pLinePlots);
            pPlot.Controls.Add(linkSaveHTMLPlots);
            pPlot.Controls.Add(lPlotPlot);
            pPlot.Dock = DockStyle.Top;
            pPlot.Location = new Point(3, 3);
            pPlot.Name = "pPlot";
            pPlot.Size = new Size(688, 140);
            pPlot.TabIndex = 5;
            // 
            // timeRangePlot
            // 
            timeRangePlot.EndTime = 1000;
            timeRangePlot.Location = new Point(4, 3);
            timeRangePlot.Name = "timeRangePlot";
            timeRangePlot.Size = new Size(207, 56);
            timeRangePlot.StartTime = 0;
            timeRangePlot.TabIndex = 67;
            // 
            // btnPlotHTML
            // 
            btnPlotHTML.BackColor = Color.FromArgb(96, 125, 139);
            btnPlotHTML.ContextMenuStrip = cmPlot;
            btnPlotHTML.Enabled = false;
            btnPlotHTML.FlatAppearance.BorderColor = Color.LightGray;
            btnPlotHTML.FlatStyle = FlatStyle.Flat;
            btnPlotHTML.ForeColor = Color.White;
            btnPlotHTML.Location = new Point(446, 5);
            btnPlotHTML.Name = "btnPlotHTML";
            btnPlotHTML.Size = new Size(75, 24);
            btnPlotHTML.TabIndex = 52;
            btnPlotHTML.Text = "Plot";
            btnPlotHTML.UseVisualStyleBackColor = false;
            btnPlotHTML.Click += btnPlotHTML_Click;
            // 
            // cmPlot
            // 
            cmPlot.Items.AddRange(new ToolStripItem[] { cmiNonInteractivePlot });
            cmPlot.Name = "cmPlot";
            cmPlot.Size = new Size(182, 26);
            // 
            // cmiNonInteractivePlot
            // 
            cmiNonInteractivePlot.Name = "cmiNonInteractivePlot";
            cmiNonInteractivePlot.Size = new Size(181, 22);
            cmiNonInteractivePlot.Text = "Non-interactive plot";
            cmiNonInteractivePlot.Click += cmiNonInteractivePlot_Click;
            // 
            // ddPlot
            // 
            ddPlot.BackColor = Color.White;
            ddPlot.DropDownStyle = ComboBoxStyle.DropDownList;
            ddPlot.FlatStyle = FlatStyle.Flat;
            ddPlot.FormattingEnabled = true;
            ddPlot.Location = new Point(278, 6);
            ddPlot.Name = "ddPlot";
            ddPlot.Size = new Size(165, 23);
            ddPlot.TabIndex = 34;
            ddPlot.SelectedIndexChanged += ddPlot_SelectedIndexChanged;
            // 
            // listPlotHistory
            // 
            listPlotHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listPlotHistory.FirstItem = "(Plot History - Double click to select.)";
            listPlotHistory.Location = new Point(539, 28);
            listPlotHistory.Name = "listPlotHistory";
            listPlotHistory.SelectedIndex = -1;
            listPlotHistory.Size = new Size(146, 108);
            listPlotHistory.TabIndex = 64;
            listPlotHistory.ItemSelect += listPlotHistory_ItemSelect;
            listPlotHistory.ItemEdit += listPlotHistory_ItemEdit;
            listPlotHistory.ItemsExport += listPlotHistory_ItemsExport;
            listPlotHistory.ItemsImport += listPlotHistory_ItemsImport;
            // 
            // cellSelectionPlot
            // 
            cellSelectionPlot.CombineOptionsVisible = false;
            cellSelectionPlot.Location = new Point(212, 25);
            cellSelectionPlot.Name = "cellSelectionPlot";
            cellSelectionPlot.SelectedUnits = null;
            cellSelectionPlot.Size = new Size(327, 111);
            cellSelectionPlot.TabIndex = 66;
            // 
            // cbPlotHistory
            // 
            cbPlotHistory.AutoSize = true;
            cbPlotHistory.Checked = true;
            cbPlotHistory.CheckState = CheckState.Checked;
            cbPlotHistory.Location = new Point(644, 8);
            cbPlotHistory.Name = "cbPlotHistory";
            cbPlotHistory.Size = new Size(93, 19);
            cbPlotHistory.TabIndex = 65;
            cbPlotHistory.Text = "Keep History";
            cbPlotHistory.UseVisualStyleBackColor = true;
            // 
            // linkExportPlotData
            // 
            linkExportPlotData.AutoSize = true;
            linkExportPlotData.Enabled = false;
            linkExportPlotData.LinkColor = Color.FromArgb(64, 64, 64);
            linkExportPlotData.Location = new Point(584, 10);
            linkExportPlotData.Name = "linkExportPlotData";
            linkExportPlotData.Size = new Size(44, 15);
            linkExportPlotData.TabIndex = 59;
            linkExportPlotData.TabStop = true;
            linkExportPlotData.Text = "Export ";
            toolTip.SetToolTip(linkExportPlotData, "Export plot data as a csv file");
            linkExportPlotData.LinkClicked += linkExportPlotData_LinkClicked;
            // 
            // pLinePlots
            // 
            pLinePlots.BackColor = Color.LightGray;
            pLinePlots.Dock = DockStyle.Bottom;
            pLinePlots.Location = new Point(0, 139);
            pLinePlots.Name = "pLinePlots";
            pLinePlots.Size = new Size(688, 1);
            pLinePlots.TabIndex = 58;
            // 
            // linkSaveHTMLPlots
            // 
            linkSaveHTMLPlots.AutoSize = true;
            linkSaveHTMLPlots.Enabled = false;
            linkSaveHTMLPlots.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveHTMLPlots.Location = new Point(539, 10);
            linkSaveHTMLPlots.Name = "linkSaveHTMLPlots";
            linkSaveHTMLPlots.Size = new Size(34, 15);
            linkSaveHTMLPlots.TabIndex = 53;
            linkSaveHTMLPlots.TabStop = true;
            linkSaveHTMLPlots.Text = "Save ";
            toolTip.SetToolTip(linkSaveHTMLPlots, "Save plot as an HTML file");
            linkSaveHTMLPlots.LinkClicked += linkSaveHTMLPlots_LinkClicked;
            // 
            // lPlotPlot
            // 
            lPlotPlot.AutoSize = true;
            lPlotPlot.Location = new Point(217, 10);
            lPlotPlot.Name = "lPlotPlot";
            lPlotPlot.Size = new Size(28, 15);
            lPlotPlot.TabIndex = 19;
            lPlotPlot.Text = "Plot";
            lPlotPlot.DoubleClick += lPlotPlot_DoubleClick;
            // 
            // tStats
            // 
            tStats.Controls.Add(tabStats);
            tStats.Controls.Add(panel1);
            tStats.Location = new Point(4, 24);
            tStats.Name = "tStats";
            tStats.Size = new Size(694, 767);
            tStats.TabIndex = 8;
            tStats.Text = "Stats";
            tStats.UseVisualStyleBackColor = true;
            // 
            // tabStats
            // 
            tabStats.Controls.Add(tSpikes);
            tabStats.Controls.Add(tRCTrains);
            tabStats.Controls.Add(tSpikeSummary);
            tabStats.Controls.Add(tEpisodes);
            tabStats.Dock = DockStyle.Fill;
            tabStats.Location = new Point(0, 120);
            tabStats.Name = "tabStats";
            tabStats.SelectedIndex = 0;
            tabStats.Size = new Size(694, 647);
            tabStats.TabIndex = 8;
            tabStats.SelectedIndexChanged += tabStats_SelectedIndexChanged;
            // 
            // tSpikes
            // 
            tSpikes.Controls.Add(dgSpikeStats);
            tSpikes.Location = new Point(4, 24);
            tSpikes.Name = "tSpikes";
            tSpikes.Padding = new Padding(3);
            tSpikes.Size = new Size(686, 619);
            tSpikes.TabIndex = 0;
            tSpikes.Text = "Spikes";
            tSpikes.UseVisualStyleBackColor = true;
            // 
            // dgSpikeStats
            // 
            dgSpikeStats.AllowUserToAddRows = false;
            dgSpikeStats.AllowUserToDeleteRows = false;
            dgSpikeStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSpikeStats.ContextMenuStrip = cmGrid;
            dgSpikeStats.Dock = DockStyle.Fill;
            dgSpikeStats.Location = new Point(3, 3);
            dgSpikeStats.Name = "dgSpikeStats";
            dgSpikeStats.ReadOnly = true;
            dgSpikeStats.Size = new Size(680, 613);
            dgSpikeStats.TabIndex = 7;
            // 
            // cmGrid
            // 
            cmGrid.Items.AddRange(new ToolStripItem[] { cmGridExport, cmGridCopyAll });
            cmGrid.Name = "cmGrid";
            cmGrid.Size = new Size(147, 48);
            // 
            // cmGridExport
            // 
            cmGridExport.Name = "cmGridExport";
            cmGridExport.Size = new Size(146, 22);
            cmGridExport.Text = "Export to CSV";
            cmGridExport.Click += cmGridExport_Click;
            // 
            // cmGridCopyAll
            // 
            cmGridCopyAll.Name = "cmGridCopyAll";
            cmGridCopyAll.Size = new Size(146, 22);
            cmGridCopyAll.Text = "Copy All";
            cmGridCopyAll.Click += cmGridCopyAll_Click;
            // 
            // tRCTrains
            // 
            tRCTrains.Controls.Add(splitRCTrains);
            tRCTrains.Location = new Point(4, 24);
            tRCTrains.Name = "tRCTrains";
            tRCTrains.Padding = new Padding(3);
            tRCTrains.Size = new Size(686, 619);
            tRCTrains.TabIndex = 1;
            tRCTrains.Text = "Rostro-Caudal Burst Trains";
            tRCTrains.UseVisualStyleBackColor = true;
            // 
            // splitRCTrains
            // 
            splitRCTrains.Dock = DockStyle.Fill;
            splitRCTrains.Location = new Point(3, 3);
            splitRCTrains.Name = "splitRCTrains";
            splitRCTrains.Orientation = Orientation.Horizontal;
            // 
            // splitRCTrains.Panel1
            // 
            splitRCTrains.Panel1.Controls.Add(dgRCTrains);
            // 
            // splitRCTrains.Panel2
            // 
            splitRCTrains.Panel2.Controls.Add(webViewRCTrains);
            splitRCTrains.Size = new Size(680, 613);
            splitRCTrains.SplitterDistance = 306;
            splitRCTrains.TabIndex = 9;
            // 
            // dgRCTrains
            // 
            dgRCTrains.AllowUserToAddRows = false;
            dgRCTrains.AllowUserToDeleteRows = false;
            dgRCTrains.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgRCTrains.Columns.AddRange(new DataGridViewColumn[] { colRCTrainNumber, colRCTrainCellGroup, colRCTrainSomite, colRCTrainStart, colRCTrainEnd, colRCTrainMidPoint, colRCTrainCenter, colRCTrainStartDelay, colRCTrainMidPointDelay, colRCTrainCenterDelay });
            dgRCTrains.ContextMenuStrip = cmGrid;
            dgRCTrains.Dock = DockStyle.Fill;
            dgRCTrains.Location = new Point(0, 0);
            dgRCTrains.Name = "dgRCTrains";
            dgRCTrains.ReadOnly = true;
            dgRCTrains.Size = new Size(680, 306);
            dgRCTrains.TabIndex = 8;
            dgRCTrains.ColumnHeaderMouseDoubleClick += dgRCTrains_ColumnHeaderMouseDoubleClick;
            // 
            // colRCTrainNumber
            // 
            colRCTrainNumber.HeaderText = "Train #";
            colRCTrainNumber.Name = "colRCTrainNumber";
            colRCTrainNumber.ReadOnly = true;
            colRCTrainNumber.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainCellGroup
            // 
            colRCTrainCellGroup.HeaderText = "Cell Group";
            colRCTrainCellGroup.Name = "colRCTrainCellGroup";
            colRCTrainCellGroup.ReadOnly = true;
            colRCTrainCellGroup.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainSomite
            // 
            colRCTrainSomite.HeaderText = "Somite";
            colRCTrainSomite.Name = "colRCTrainSomite";
            colRCTrainSomite.ReadOnly = true;
            colRCTrainSomite.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainStart
            // 
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            colRCTrainStart.DefaultCellStyle = dataGridViewCellStyle8;
            colRCTrainStart.HeaderText = "Start Time";
            colRCTrainStart.Name = "colRCTrainStart";
            colRCTrainStart.ReadOnly = true;
            colRCTrainStart.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainEnd
            // 
            dataGridViewCellStyle9.Format = "N2";
            colRCTrainEnd.DefaultCellStyle = dataGridViewCellStyle9;
            colRCTrainEnd.HeaderText = "End Time";
            colRCTrainEnd.Name = "colRCTrainEnd";
            colRCTrainEnd.ReadOnly = true;
            colRCTrainEnd.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainMidPoint
            // 
            dataGridViewCellStyle10.Format = "N2";
            colRCTrainMidPoint.DefaultCellStyle = dataGridViewCellStyle10;
            colRCTrainMidPoint.HeaderText = "Mid Point";
            colRCTrainMidPoint.Name = "colRCTrainMidPoint";
            colRCTrainMidPoint.ReadOnly = true;
            colRCTrainMidPoint.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainCenter
            // 
            dataGridViewCellStyle11.Format = "N2";
            colRCTrainCenter.DefaultCellStyle = dataGridViewCellStyle11;
            colRCTrainCenter.HeaderText = "Center";
            colRCTrainCenter.Name = "colRCTrainCenter";
            colRCTrainCenter.ReadOnly = true;
            colRCTrainCenter.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainStartDelay
            // 
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            colRCTrainStartDelay.DefaultCellStyle = dataGridViewCellStyle12;
            colRCTrainStartDelay.HeaderText = "RC Delay [Start]";
            colRCTrainStartDelay.Name = "colRCTrainStartDelay";
            colRCTrainStartDelay.ReadOnly = true;
            colRCTrainStartDelay.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainMidPointDelay
            // 
            dataGridViewCellStyle13.Format = "N2";
            colRCTrainMidPointDelay.DefaultCellStyle = dataGridViewCellStyle13;
            colRCTrainMidPointDelay.HeaderText = "RC Delay [Mid Point]";
            colRCTrainMidPointDelay.Name = "colRCTrainMidPointDelay";
            colRCTrainMidPointDelay.ReadOnly = true;
            colRCTrainMidPointDelay.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainCenterDelay
            // 
            dataGridViewCellStyle14.Format = "N2";
            colRCTrainCenterDelay.DefaultCellStyle = dataGridViewCellStyle14;
            colRCTrainCenterDelay.HeaderText = "RC Delay [Center]";
            colRCTrainCenterDelay.Name = "colRCTrainCenterDelay";
            colRCTrainCenterDelay.ReadOnly = true;
            colRCTrainCenterDelay.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // webViewRCTrains
            // 
            webViewRCTrains.AllowExternalDrop = true;
            webViewRCTrains.BackColor = Color.White;
            webViewRCTrains.CreationProperties = null;
            webViewRCTrains.DefaultBackgroundColor = Color.White;
            webViewRCTrains.Dock = DockStyle.Fill;
            webViewRCTrains.Location = new Point(0, 0);
            webViewRCTrains.Name = "webViewRCTrains";
            webViewRCTrains.Size = new Size(680, 303);
            webViewRCTrains.TabIndex = 2;
            webViewRCTrains.ZoomFactor = 1D;
            webViewRCTrains.CoreWebView2InitializationCompleted += webView_CoreWebView2InitializationCompleted;
            // 
            // tSpikeSummary
            // 
            tSpikeSummary.Controls.Add(splitContainer1);
            tSpikeSummary.Location = new Point(4, 24);
            tSpikeSummary.Name = "tSpikeSummary";
            tSpikeSummary.Padding = new Padding(3);
            tSpikeSummary.Size = new Size(686, 619);
            tSpikeSummary.TabIndex = 2;
            tSpikeSummary.Text = "Spike Summary";
            tSpikeSummary.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgSpikeSummary);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgSpikeSummaryDetails);
            splitContainer1.Size = new Size(680, 613);
            splitContainer1.SplitterDistance = 244;
            splitContainer1.TabIndex = 9;
            // 
            // dgSpikeSummary
            // 
            dgSpikeSummary.AllowUserToAddRows = false;
            dgSpikeSummary.AllowUserToDeleteRows = false;
            dgSpikeSummary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSpikeSummary.Columns.AddRange(new DataGridViewColumn[] { colSumPeriod, colSumCellPool, colSumSpikeCount });
            dgSpikeSummary.ContextMenuStrip = cmGrid;
            dgSpikeSummary.Dock = DockStyle.Fill;
            dgSpikeSummary.Location = new Point(0, 0);
            dgSpikeSummary.Name = "dgSpikeSummary";
            dgSpikeSummary.ReadOnly = true;
            dgSpikeSummary.Size = new Size(680, 244);
            dgSpikeSummary.TabIndex = 9;
            // 
            // colSumPeriod
            // 
            colSumPeriod.HeaderText = "Period";
            colSumPeriod.Name = "colSumPeriod";
            colSumPeriod.ReadOnly = true;
            // 
            // colSumCellPool
            // 
            colSumCellPool.HeaderText = "Cell Pool";
            colSumCellPool.Name = "colSumCellPool";
            colSumCellPool.ReadOnly = true;
            // 
            // colSumSpikeCount
            // 
            colSumSpikeCount.HeaderText = "Spike Count";
            colSumSpikeCount.Name = "colSumSpikeCount";
            colSumSpikeCount.ReadOnly = true;
            // 
            // dgSpikeSummaryDetails
            // 
            dgSpikeSummaryDetails.AllowUserToAddRows = false;
            dgSpikeSummaryDetails.AllowUserToDeleteRows = false;
            dgSpikeSummaryDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSpikeSummaryDetails.Columns.AddRange(new DataGridViewColumn[] { colSumDetailsPeriod, colSumDetailsCellID, colSumDetailsSagittal, colSumDetailsCellPool, colSumDetailsSomite, colSumDetailsCellSeq, colSumDetailsSpikeCount });
            dgSpikeSummaryDetails.ContextMenuStrip = cmGrid;
            dgSpikeSummaryDetails.Dock = DockStyle.Fill;
            dgSpikeSummaryDetails.Location = new Point(0, 0);
            dgSpikeSummaryDetails.Name = "dgSpikeSummaryDetails";
            dgSpikeSummaryDetails.ReadOnly = true;
            dgSpikeSummaryDetails.Size = new Size(680, 365);
            dgSpikeSummaryDetails.TabIndex = 8;
            // 
            // colSumDetailsPeriod
            // 
            colSumDetailsPeriod.HeaderText = "Period";
            colSumDetailsPeriod.Name = "colSumDetailsPeriod";
            colSumDetailsPeriod.ReadOnly = true;
            // 
            // colSumDetailsCellID
            // 
            colSumDetailsCellID.HeaderText = "Cell ID";
            colSumDetailsCellID.Name = "colSumDetailsCellID";
            colSumDetailsCellID.ReadOnly = true;
            // 
            // colSumDetailsSagittal
            // 
            colSumDetailsSagittal.HeaderText = "Sagittal";
            colSumDetailsSagittal.Name = "colSumDetailsSagittal";
            colSumDetailsSagittal.ReadOnly = true;
            colSumDetailsSagittal.Width = 75;
            // 
            // colSumDetailsCellPool
            // 
            colSumDetailsCellPool.HeaderText = "Cell Pool";
            colSumDetailsCellPool.Name = "colSumDetailsCellPool";
            colSumDetailsCellPool.ReadOnly = true;
            // 
            // colSumDetailsSomite
            // 
            colSumDetailsSomite.HeaderText = "Somite";
            colSumDetailsSomite.Name = "colSumDetailsSomite";
            colSumDetailsSomite.ReadOnly = true;
            colSumDetailsSomite.Width = 75;
            // 
            // colSumDetailsCellSeq
            // 
            colSumDetailsCellSeq.HeaderText = "Cell Seq";
            colSumDetailsCellSeq.Name = "colSumDetailsCellSeq";
            colSumDetailsCellSeq.ReadOnly = true;
            colSumDetailsCellSeq.Width = 75;
            // 
            // colSumDetailsSpikeCount
            // 
            colSumDetailsSpikeCount.HeaderText = "Spike Count";
            colSumDetailsSpikeCount.Name = "colSumDetailsSpikeCount";
            colSumDetailsSpikeCount.ReadOnly = true;
            // 
            // tEpisodes
            // 
            tEpisodes.Controls.Add(dgEpisodes);
            tEpisodes.Location = new Point(4, 24);
            tEpisodes.Name = "tEpisodes";
            tEpisodes.Padding = new Padding(3);
            tEpisodes.Size = new Size(686, 619);
            tEpisodes.TabIndex = 3;
            tEpisodes.Text = "Episodes";
            tEpisodes.UseVisualStyleBackColor = true;
            // 
            // dgEpisodes
            // 
            dgEpisodes.AllowUserToAddRows = false;
            dgEpisodes.AllowUserToDeleteRows = false;
            dgEpisodes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgEpisodes.ContextMenuStrip = cmGrid;
            dgEpisodes.Dock = DockStyle.Fill;
            dgEpisodes.Location = new Point(3, 3);
            dgEpisodes.Name = "dgEpisodes";
            dgEpisodes.ReadOnly = true;
            dgEpisodes.Size = new Size(680, 613);
            dgEpisodes.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(236, 239, 241);
            panel1.Controls.Add(timeRangeStat);
            panel1.Controls.Add(cbStatsAppend);
            panel1.Controls.Add(btnListStats);
            panel1.Controls.Add(cellSelectionStats);
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(694, 120);
            panel1.TabIndex = 6;
            // 
            // timeRangeStat
            // 
            timeRangeStat.EndTime = 1000;
            timeRangeStat.Location = new Point(3, 4);
            timeRangeStat.Name = "timeRangeStat";
            timeRangeStat.Size = new Size(207, 56);
            timeRangeStat.StartTime = 0;
            timeRangeStat.TabIndex = 73;
            // 
            // cbStatsAppend
            // 
            cbStatsAppend.AutoSize = true;
            cbStatsAppend.Location = new Point(445, 38);
            cbStatsAppend.Name = "cbStatsAppend";
            cbStatsAppend.Size = new Size(68, 19);
            cbStatsAppend.TabIndex = 72;
            cbStatsAppend.Text = "Append";
            cbStatsAppend.UseVisualStyleBackColor = true;
            // 
            // btnListStats
            // 
            btnListStats.AutoSize = true;
            btnListStats.BackColor = Color.FromArgb(96, 125, 139);
            btnListStats.Enabled = false;
            btnListStats.FlatAppearance.BorderColor = Color.LightGray;
            btnListStats.FlatStyle = FlatStyle.Flat;
            btnListStats.ForeColor = Color.White;
            btnListStats.Location = new Point(445, 11);
            btnListStats.Name = "btnListStats";
            btnListStats.Size = new Size(100, 27);
            btnListStats.TabIndex = 61;
            btnListStats.Text = "List Stats";
            btnListStats.UseVisualStyleBackColor = false;
            btnListStats.Click += btnListStats_Click;
            // 
            // cellSelectionStats
            // 
            cellSelectionStats.CombineOptionsVisible = false;
            cellSelectionStats.Location = new Point(217, 4);
            cellSelectionStats.Name = "cellSelectionStats";
            cellSelectionStats.SelectedUnits = null;
            cellSelectionStats.Size = new Size(232, 113);
            cellSelectionStats.TabIndex = 60;
            // 
            // panel3
            // 
            panel3.BackColor = Color.LightGray;
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 119);
            panel3.Name = "panel3";
            panel3.Size = new Size(694, 1);
            panel3.TabIndex = 58;
            // 
            // tAnimation
            // 
            tAnimation.Controls.Add(webViewAnimation);
            tAnimation.Controls.Add(pAnimation);
            tAnimation.Location = new Point(4, 24);
            tAnimation.Name = "tAnimation";
            tAnimation.Size = new Size(694, 767);
            tAnimation.TabIndex = 3;
            tAnimation.Text = "Animation";
            tAnimation.UseVisualStyleBackColor = true;
            // 
            // webViewAnimation
            // 
            webViewAnimation.AllowExternalDrop = true;
            webViewAnimation.BackColor = Color.White;
            webViewAnimation.CreationProperties = null;
            webViewAnimation.DefaultBackgroundColor = Color.White;
            webViewAnimation.Dock = DockStyle.Fill;
            webViewAnimation.Location = new Point(0, 64);
            webViewAnimation.Name = "webViewAnimation";
            webViewAnimation.Size = new Size(694, 703);
            webViewAnimation.TabIndex = 2;
            webViewAnimation.ZoomFactor = 1D;
            webViewAnimation.CoreWebView2InitializationCompleted += webView_CoreWebView2InitializationCompleted;
            // 
            // pAnimation
            // 
            pAnimation.BackColor = Color.FromArgb(236, 239, 241);
            pAnimation.Controls.Add(timeRangeAnimation);
            pAnimation.Controls.Add(pLineAnimation);
            pAnimation.Controls.Add(eAnimationdt);
            pAnimation.Controls.Add(lAnimationdt);
            pAnimation.Controls.Add(linkSaveAnimationCSV);
            pAnimation.Controls.Add(lAnimationTime);
            pAnimation.Controls.Add(linkSaveAnimationHTML);
            pAnimation.Controls.Add(btnAnimate);
            pAnimation.Dock = DockStyle.Top;
            pAnimation.Location = new Point(0, 0);
            pAnimation.Name = "pAnimation";
            pAnimation.Size = new Size(694, 64);
            pAnimation.TabIndex = 5;
            // 
            // timeRangeAnimation
            // 
            timeRangeAnimation.EndTime = 1000;
            timeRangeAnimation.Location = new Point(3, 4);
            timeRangeAnimation.Name = "timeRangeAnimation";
            timeRangeAnimation.Size = new Size(207, 56);
            timeRangeAnimation.StartTime = 0;
            timeRangeAnimation.TabIndex = 68;
            // 
            // pLineAnimation
            // 
            pLineAnimation.BackColor = Color.LightGray;
            pLineAnimation.Dock = DockStyle.Bottom;
            pLineAnimation.Location = new Point(0, 63);
            pLineAnimation.Name = "pLineAnimation";
            pLineAnimation.Size = new Size(694, 1);
            pLineAnimation.TabIndex = 38;
            // 
            // eAnimationdt
            // 
            eAnimationdt.DecimalPlaces = 2;
            eAnimationdt.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            eAnimationdt.Location = new Point(286, 6);
            eAnimationdt.Name = "eAnimationdt";
            eAnimationdt.Size = new Size(53, 23);
            eAnimationdt.TabIndex = 37;
            eAnimationdt.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lAnimationdt
            // 
            lAnimationdt.AutoSize = true;
            lAnimationdt.Location = new Point(261, 9);
            lAnimationdt.Name = "lAnimationdt";
            lAnimationdt.Size = new Size(19, 15);
            lAnimationdt.TabIndex = 36;
            lAnimationdt.Text = "Δt";
            // 
            // linkSaveAnimationCSV
            // 
            linkSaveAnimationCSV.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkSaveAnimationCSV.AutoSize = true;
            linkSaveAnimationCSV.Enabled = false;
            linkSaveAnimationCSV.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveAnimationCSV.Location = new Point(633, 37);
            linkSaveAnimationCSV.Name = "linkSaveAnimationCSV";
            linkSaveAnimationCSV.Size = new Size(58, 15);
            linkSaveAnimationCSV.TabIndex = 34;
            linkSaveAnimationCSV.TabStop = true;
            linkSaveAnimationCSV.Text = "Save CSV ";
            linkSaveAnimationCSV.TextAlign = ContentAlignment.MiddleRight;
            linkSaveAnimationCSV.LinkClicked += linkSaveAnimationCSV_LinkClicked;
            // 
            // lAnimationTime
            // 
            lAnimationTime.Location = new Point(345, 6);
            lAnimationTime.Name = "lAnimationTime";
            lAnimationTime.Size = new Size(271, 54);
            lAnimationTime.TabIndex = 33;
            lAnimationTime.Text = "Last animation:";
            // 
            // linkSaveAnimationHTML
            // 
            linkSaveAnimationHTML.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkSaveAnimationHTML.AutoSize = true;
            linkSaveAnimationHTML.Enabled = false;
            linkSaveAnimationHTML.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveAnimationHTML.Location = new Point(622, 14);
            linkSaveAnimationHTML.Name = "linkSaveAnimationHTML";
            linkSaveAnimationHTML.Size = new Size(69, 15);
            linkSaveAnimationHTML.TabIndex = 31;
            linkSaveAnimationHTML.TabStop = true;
            linkSaveAnimationHTML.Text = "Save HTML ";
            linkSaveAnimationHTML.TextAlign = ContentAlignment.MiddleRight;
            linkSaveAnimationHTML.LinkClicked += linkSaveAnimationHTML_LinkClicked;
            // 
            // btnAnimate
            // 
            btnAnimate.BackColor = Color.FromArgb(96, 125, 139);
            btnAnimate.ContextMenuStrip = cmAnimation;
            btnAnimate.Enabled = false;
            btnAnimate.FlatAppearance.BorderColor = Color.LightGray;
            btnAnimate.FlatStyle = FlatStyle.Flat;
            btnAnimate.ForeColor = Color.White;
            btnAnimate.Location = new Point(264, 32);
            btnAnimate.Name = "btnAnimate";
            btnAnimate.Size = new Size(75, 24);
            btnAnimate.TabIndex = 29;
            btnAnimate.Text = "Animate";
            btnAnimate.UseVisualStyleBackColor = false;
            btnAnimate.Click += btnAnimate_Click;
            // 
            // cmAnimation
            // 
            cmAnimation.Items.AddRange(new ToolStripItem[] { cmiAnimationClearCache });
            cmAnimation.Name = "cmWebView";
            cmAnimation.Size = new Size(138, 26);
            // 
            // cmiAnimationClearCache
            // 
            cmiAnimationClearCache.Name = "cmiAnimationClearCache";
            cmiAnimationClearCache.Size = new Size(137, 22);
            cmiAnimationClearCache.Text = "Clear Cache";
            cmiAnimationClearCache.Click += cmiAnimationClearCache_Click;
            // 
            // saveFileHTML
            // 
            saveFileHTML.Filter = "HTML files(*.html)|*.html";
            // 
            // saveFileJson
            // 
            saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // openFileJson
            // 
            openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // saveFileText
            // 
            saveFileText.Filter = "Text files(*.txt)|*.txt";
            // 
            // saveFileCSV
            // 
            saveFileCSV.Filter = "CSV files(*.csv)|*.csv";
            // 
            // saveFileImage
            // 
            saveFileImage.Filter = "Image files(*.png)|*.png";
            // 
            // ModelOutputControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabOutputs);
            Name = "ModelOutputControl";
            Size = new Size(702, 795);
            Load += ModelOutputControl_Load;
            tabOutputs.ResumeLayout(false);
            t2DRender.ResumeLayout(false);
            t2DRender.PerformLayout();
            p2DRenderOptions.ResumeLayout(false);
            p2DRenderOptions.PerformLayout();
            gr2DLegend.ResumeLayout(false);
            p2DRender.ResumeLayout(false);
            p2DRender.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView2DRender).EndInit();
            t3DRender.ResumeLayout(false);
            p3DRenderOptions.ResumeLayout(false);
            p3DRenderOptions.PerformLayout();
            gr3DLegend.ResumeLayout(false);
            p3DRender.ResumeLayout(false);
            p3DRender.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView3DRender).EndInit();
            tPlot.ResumeLayout(false);
            tabPlotSub.ResumeLayout(false);
            tPlotHTML.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webViewPlot).EndInit();
            tPlotWindows.ResumeLayout(false);
            tPlotWindows.PerformLayout();
            pPictureBox.ResumeLayout(false);
            pPictureBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            cmPlotImageSave.ResumeLayout(false);
            pPlot.ResumeLayout(false);
            pPlot.PerformLayout();
            cmPlot.ResumeLayout(false);
            tStats.ResumeLayout(false);
            tabStats.ResumeLayout(false);
            tSpikes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgSpikeStats).EndInit();
            cmGrid.ResumeLayout(false);
            tRCTrains.ResumeLayout(false);
            splitRCTrains.Panel1.ResumeLayout(false);
            splitRCTrains.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitRCTrains).EndInit();
            splitRCTrains.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgRCTrains).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewRCTrains).EndInit();
            tSpikeSummary.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgSpikeSummary).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgSpikeSummaryDetails).EndInit();
            tEpisodes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgEpisodes).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tAnimation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webViewAnimation).EndInit();
            pAnimation.ResumeLayout(false);
            pAnimation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)eAnimationdt).EndInit();
            cmAnimation.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabOutputs;
        private TabPage tPlot;
        private TabControl tabPlotSub;
        private TabPage tPlotWindows;
        private TabPage tPlotHTML;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewPlot;
        private Panel pPlot;
        private LinkLabel linkExportPlotData;
        private Panel pLinePlots;
        private Button btnPlotHTML;
        private LinkLabel linkSaveHTMLPlots;
        private Label lPlotPlot;
        private ComboBox ddPlot;
        private TabPage t2DRender;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2DRender;
        private Panel p2DRender;
        private Panel pLine2D;
        private LinkLabel linkSaveHTML2D;
        private Button btn2DRender;
        private TabPage t3DRender;
        private ComboBox dd3DViewpoint;
        private Button btnZoomIn;
        private Button btnZoomOut;
        private GroupBox gr3DLegend;
        private Label legendUnselected;
        private Label legendOutgoing;
        private Label legendGap;
        private Label legendIncoming;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView3DRender;
        private Panel p3DRender;
        private CheckBox cb3DGapJunc;
        private CheckBox cb3DChemJunc;
        private TextBox e3DSomiteRange;
        private CheckBox cb3DAllSomites;
        private Panel pLine3D;
        private LinkLabel linkSaveHTML3D;
        private Button btn3DRender;
        private TabPage tAnimation;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewAnimation;
        private Panel pAnimation;
        private Panel pLineAnimation;
        private NumericUpDown eAnimationdt;
        private Label lAnimationdt;
        private LinkLabel linkSaveAnimationCSV;
        private Label lAnimationTime;
        private LinkLabel linkSaveAnimationHTML;
        private Button btnAnimate;
        private SaveFileDialog saveFileHTML;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private ToolTip toolTip;
        private SaveFileDialog saveFileText;
        private SaveFileDialog saveFileCSV;
        private SaveFileDialog saveFileImage;
        private CheckBox cb2DLegend;
        private CheckBox cb2DGapJunc;
        private CheckBox cb2DChemJunc;
        private GroupBox gr2DLegend;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ContextMenuStrip cmPlot;
        private ToolStripMenuItem cmiNonInteractivePlot;
        private ContextMenuStrip cmPlotImageSave;
        private ToolStripMenuItem cmiPlotImageSave;
        private ContextMenuStrip cmAnimation;
        private ToolStripMenuItem cmiAnimationClearCache;
        private CheckBox cb3DShowUnselectedNodes;
        private Panel p3DRenderOptions;
        private CheckBox cb3DLegend;
        private Label l3DNodeSize;
        private General.UpDownControl ud3DNodeSize;
        private HistoryListControl listPlotHistory;
        private CheckBox cbPlotHistory;
        private Label l2DNodeSize;
        private General.UpDownControl ud2DNodeSize;
        private Label l2DLinkSize;
        private General.UpDownControl ud2DLinkSize;
        private TabPage tStats;
        private DataGridView dgSpikeStats;
        private Panel panel1;
        private Panel panel3;
        private Display.CellSelectionControl cellSelectionPlot;
        private Display.CellSelectionControl cellSelectionStats;
        private Button btnListStats;

        public ModelOutputControl(Button btnListStats)
        {
            this.btnListStats = btnListStats;
        }

        private TabControl tabStats;
        private TabPage tSpikes;
        internal TabPage tRCTrains;
        internal DataGridView dgRCTrains;
        private SplitContainer splitRCTrains;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewRCTrains;
        private DataGridViewTextBoxColumn colRCTrainNumber;
        private DataGridViewTextBoxColumn colRCTrainCellGroup;
        private DataGridViewTextBoxColumn colRCTrainSomite;
        private DataGridViewTextBoxColumn colRCTrainStart;
        private DataGridViewTextBoxColumn colRCTrainEnd;
        private DataGridViewTextBoxColumn colRCTrainMidPoint;
        private DataGridViewTextBoxColumn colRCTrainCenter;
        private DataGridViewTextBoxColumn colRCTrainStartDelay;
        private DataGridViewTextBoxColumn colRCTrainMidPointDelay;
        private DataGridViewTextBoxColumn colRCTrainCenterDelay;
        private CheckBox cb2DHideNonspiking;
        private CheckBox cb2DShowUnselectedNodes;
        private Panel p2DRenderOptions;
        private CheckBox cb2DRenderShowOptions;
        private CheckBox cb3DRenderShowOptions;
        private PictureBox pictureBox;
        private Panel pPictureBox;
        private CheckBox cb2DOffline;
        private CheckBox cb3DOffline;
        private TabPage tSpikeSummary;
        private DataGridView dgSpikeSummaryDetails;
        private SplitContainer splitContainer1;
        private DataGridView dgSpikeSummary;
        private DataGridViewTextBoxColumn colSumPeriod;
        private DataGridViewTextBoxColumn colSumCellPool;
        private DataGridViewTextBoxColumn colSumSpikeCount;
        private DataGridViewTextBoxColumn colSumDetailsPeriod;
        private DataGridViewTextBoxColumn colSumDetailsCellID;
        private DataGridViewTextBoxColumn colSumDetailsSagittal;
        private DataGridViewTextBoxColumn colSumDetailsCellPool;
        private DataGridViewTextBoxColumn colSumDetailsSomite;
        private DataGridViewTextBoxColumn colSumDetailsCellSeq;
        private DataGridViewTextBoxColumn colSumDetailsSpikeCount;
        private CheckBox cbStatsAppend;
        private ContextMenuStrip cmGrid;
        private ToolStripMenuItem cmGridExport;
        private ToolStripMenuItem cmGridCopyAll;
        private TabPage tEpisodes;
        private DataGridView dgEpisodes;
        private General.StartEndTimeControl timeRangePlot;
        private General.StartEndTimeControl timeRangeStat;
        private General.StartEndTimeControl timeRangeAnimation;
        private GroupBox gr2DCellPoolLegend;
    }
}