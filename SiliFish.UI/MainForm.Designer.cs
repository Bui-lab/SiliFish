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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pTop = new Panel();
            btnAbout = new Button();
            menuStripMain = new MenuStrip();
            mFile = new ToolStripMenuItem();
            miFileLoad = new ToolStripMenuItem();
            miFileSave = new ToolStripMenuItem();
            miFileSaveSimulationResults = new ToolStripMenuItem();
            miFileBatchSave = new ToolStripMenuItem();
            miFileSep1 = new ToolStripSeparator();
            miFileExport = new ToolStripMenuItem();
            miFileImport = new ToolStripMenuItem();
            miFileSep2 = new ToolStripSeparator();
            miFileNewModel = new ToolStripMenuItem();
            miFileClearModel = new ToolStripMenuItem();
            miFileSep3 = new ToolStripSeparator();
            miFileExit = new ToolStripMenuItem();
            mTools = new ToolStripMenuItem();
            miToolsCompareModel = new ToolStripMenuItem();
            miToolsSep1 = new ToolStripSeparator();
            miToolsCellularDynamics = new ToolStripMenuItem();
            miToolsSep2 = new ToolStripSeparator();
            miToolsGenerateStatsData = new ToolStripMenuItem();
            miToolsStatsSpikeStats = new ToolStripMenuItem();
            miToolsStatsSpikeCounts = new ToolStripMenuItem();
            miToolsStatsSpikes = new ToolStripMenuItem();
            miToolsStatsEpisodes = new ToolStripMenuItem();
            miToolsStatsMembranePotentials = new ToolStripMenuItem();
            miToolsStatsCurrents = new ToolStripMenuItem();
            miToolsSep3 = new ToolStripSeparator();
            miToolsStatsFull = new ToolStripMenuItem();
            miToolsMultipleRun = new ToolStripMenuItem();
            miToolsRunTimeStats = new ToolStripMenuItem();
            miToolsSepStats = new ToolStripSeparator();
            miToolsSettings = new ToolStripMenuItem();
            miToolsMemoryFlush = new ToolStripMenuItem();
            mView = new ToolStripMenuItem();
            miViewOutputFolder = new ToolStripMenuItem();
            miViewTempFolder = new ToolStripMenuItem();
            miViewAbout = new ToolStripMenuItem();
            splitMain = new SplitContainer();
            modelControl = new ModelControl();
            pGenerateModel = new Panel();
            btnGenerateModel = new Button();
            linkLabel4 = new LinkLabel();
            pSimulation = new Panel();
            btnResume = new Button();
            btnStart = new Button();
            btnStop = new Button();
            btnPause = new Button();
            simulationSettings = new SiliFish.UI.Controls.General.SimulationSettingsControl();
            lProgress = new Label();
            lRunTime = new Label();
            progressBarRun = new ProgressBar();
            lSimulationSettings = new Label();
            modelOutputControl = new ModelOutputControl();
            timerRun = new System.Windows.Forms.Timer(components);
            saveFileHTML = new SaveFileDialog();
            saveFileJson = new SaveFileDialog();
            openFileJson = new OpenFileDialog();
            saveFileCSV = new SaveFileDialog();
            toolTip = new ToolTip(components);
            saveFileText = new SaveFileDialog();
            saveFileImage = new SaveFileDialog();
            pDistinguisher = new Panel();
            pDistinguisherTop = new Panel();
            pDistinguisherRight = new Panel();
            pDistinguisherBottom = new Panel();
            saveFileExcel = new SaveFileDialog();
            openFileExcel = new OpenFileDialog();
            pTop.SuspendLayout();
            menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            pGenerateModel.SuspendLayout();
            pSimulation.SuspendLayout();
            SuspendLayout();
            // 
            // pTop
            // 
            pTop.BorderStyle = BorderStyle.FixedSingle;
            pTop.Controls.Add(btnAbout);
            pTop.Controls.Add(menuStripMain);
            pTop.Dock = DockStyle.Top;
            pTop.Location = new Point(4, 4);
            pTop.Name = "pTop";
            pTop.Size = new Size(1340, 32);
            pTop.TabIndex = 3;
            // 
            // btnAbout
            // 
            btnAbout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAbout.AutoSize = true;
            btnAbout.BackColor = Color.Transparent;
            btnAbout.Image = (Image)resources.GetObject("btnAbout.Image");
            btnAbout.Location = new Point(1303, 0);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(33, 30);
            btnAbout.TabIndex = 24;
            toolTip.SetToolTip(btnAbout, "About");
            btnAbout.UseVisualStyleBackColor = false;
            btnAbout.Click += btnAbout_Click;
            // 
            // menuStripMain
            // 
            menuStripMain.BackColor = Color.FromArgb(207, 216, 220);
            menuStripMain.Items.AddRange(new ToolStripItem[] { mFile, mTools, mView });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new Padding(6, 6, 0, 6);
            menuStripMain.Size = new Size(1338, 31);
            menuStripMain.TabIndex = 28;
            menuStripMain.Text = "menuStrip1";
            // 
            // mFile
            // 
            mFile.DropDownItems.AddRange(new ToolStripItem[] { miFileLoad, miFileSave, miFileSaveSimulationResults, miFileBatchSave, miFileSep1, miFileExport, miFileImport, miFileSep2, miFileNewModel, miFileClearModel, miFileSep3, miFileExit });
            mFile.Name = "mFile";
            mFile.Size = new Size(37, 19);
            mFile.Text = "File";
            // 
            // miFileLoad
            // 
            miFileLoad.Name = "miFileLoad";
            miFileLoad.Size = new Size(270, 22);
            miFileLoad.Text = "&Load";
            miFileLoad.Click += miFileLoad_Click;
            // 
            // miFileSave
            // 
            miFileSave.Name = "miFileSave";
            miFileSave.ShortcutKeys = Keys.Control | Keys.S;
            miFileSave.Size = new Size(270, 22);
            miFileSave.Text = "&Save";
            miFileSave.Click += miFileSave_Click;
            // 
            // miFileSaveSimulationResults
            // 
            miFileSaveSimulationResults.Name = "miFileSaveSimulationResults";
            miFileSaveSimulationResults.Size = new Size(270, 22);
            miFileSaveSimulationResults.Text = "Save Simulation &Results (to database)";
            miFileSaveSimulationResults.Click += miFileSaveSimulationResults_Click;
            // 
            // miFileBatchSave
            // 
            miFileBatchSave.Name = "miFileBatchSave";
            miFileBatchSave.Size = new Size(270, 22);
            miFileBatchSave.Text = "Batch Save";
            miFileBatchSave.Click += miFileBatchSave_Click;
            // 
            // miFileSep1
            // 
            miFileSep1.Name = "miFileSep1";
            miFileSep1.Size = new Size(267, 6);
            // 
            // miFileExport
            // 
            miFileExport.Name = "miFileExport";
            miFileExport.Size = new Size(270, 22);
            miFileExport.Text = "&Export to Excel";
            miFileExport.Click += miFileExport_Click;
            // 
            // miFileImport
            // 
            miFileImport.Name = "miFileImport";
            miFileImport.Size = new Size(270, 22);
            miFileImport.Text = "&Import from Excel";
            miFileImport.Click += miFileImport_Click;
            // 
            // miFileSep2
            // 
            miFileSep2.Name = "miFileSep2";
            miFileSep2.Size = new Size(267, 6);
            // 
            // miFileNewModel
            // 
            miFileNewModel.Name = "miFileNewModel";
            miFileNewModel.Size = new Size(270, 22);
            miFileNewModel.Text = "&New Model";
            miFileNewModel.Click += miFileNewModel_Click;
            // 
            // miFileClearModel
            // 
            miFileClearModel.Name = "miFileClearModel";
            miFileClearModel.Size = new Size(270, 22);
            miFileClearModel.Text = "&Clear Model";
            miFileClearModel.Click += miFileClearModel_Click;
            // 
            // miFileSep3
            // 
            miFileSep3.Name = "miFileSep3";
            miFileSep3.Size = new Size(267, 6);
            // 
            // miFileExit
            // 
            miFileExit.Name = "miFileExit";
            miFileExit.Size = new Size(270, 22);
            miFileExit.Text = "E&xit";
            miFileExit.Click += miFileExit_Click;
            // 
            // mTools
            // 
            mTools.DropDownItems.AddRange(new ToolStripItem[] { miToolsCompareModel, miToolsSep1, miToolsCellularDynamics, miToolsSep2, miToolsGenerateStatsData, miToolsMultipleRun, miToolsRunTimeStats, miToolsSepStats, miToolsSettings, miToolsMemoryFlush });
            mTools.Name = "mTools";
            mTools.Size = new Size(47, 19);
            mTools.Text = "Tools";
            // 
            // miToolsCompareModel
            // 
            miToolsCompareModel.Name = "miToolsCompareModel";
            miToolsCompareModel.Size = new Size(207, 22);
            miToolsCompareModel.Text = "Compare To Model";
            miToolsCompareModel.Click += miToolsCompareModel_Click;
            // 
            // miToolsSep1
            // 
            miToolsSep1.Name = "miToolsSep1";
            miToolsSep1.Size = new Size(204, 6);
            // 
            // miToolsCellularDynamics
            // 
            miToolsCellularDynamics.Name = "miToolsCellularDynamics";
            miToolsCellularDynamics.Size = new Size(207, 22);
            miToolsCellularDynamics.Text = "Cellular Dynamics";
            miToolsCellularDynamics.Click += miToolsCellularDynamics_Click;
            // 
            // miToolsSep2
            // 
            miToolsSep2.Name = "miToolsSep2";
            miToolsSep2.Size = new Size(204, 6);
            // 
            // miToolsGenerateStatsData
            // 
            miToolsGenerateStatsData.DropDownItems.AddRange(new ToolStripItem[] { miToolsStatsSpikeStats, miToolsStatsSpikeCounts, miToolsStatsSpikes, miToolsStatsEpisodes, miToolsStatsMembranePotentials, miToolsStatsCurrents, miToolsSep3, miToolsStatsFull });
            miToolsGenerateStatsData.Name = "miToolsGenerateStatsData";
            miToolsGenerateStatsData.Size = new Size(207, 22);
            miToolsGenerateStatsData.Text = "Generate Stats Data";
            // 
            // miToolsStatsSpikeStats
            // 
            miToolsStatsSpikeStats.Name = "miToolsStatsSpikeStats";
            miToolsStatsSpikeStats.Size = new Size(188, 22);
            miToolsStatsSpikeStats.Text = "Spike Frequency Stats";
            miToolsStatsSpikeStats.Click += miToolsStatsSpikeStats_Click;
            // 
            // miToolsStatsSpikeCounts
            // 
            miToolsStatsSpikeCounts.Name = "miToolsStatsSpikeCounts";
            miToolsStatsSpikeCounts.Size = new Size(188, 22);
            miToolsStatsSpikeCounts.Text = "Spike Counts";
            miToolsStatsSpikeCounts.Click += miToolsStatsSpikeCounts_Click;
            // 
            // miToolsStatsSpikes
            // 
            miToolsStatsSpikes.Name = "miToolsStatsSpikes";
            miToolsStatsSpikes.Size = new Size(188, 22);
            miToolsStatsSpikes.Text = "Spikes";
            miToolsStatsSpikes.Click += miToolsStatsSpikes_Click;
            // 
            // miToolsStatsEpisodes
            // 
            miToolsStatsEpisodes.Name = "miToolsStatsEpisodes";
            miToolsStatsEpisodes.Size = new Size(188, 22);
            miToolsStatsEpisodes.Text = "Episodes";
            miToolsStatsEpisodes.Click += miToolsStatsEpisodes_Click;
            // 
            // miToolsStatsMembranePotentials
            // 
            miToolsStatsMembranePotentials.Name = "miToolsStatsMembranePotentials";
            miToolsStatsMembranePotentials.Size = new Size(188, 22);
            miToolsStatsMembranePotentials.Text = "Membrane Potentials";
            miToolsStatsMembranePotentials.Click += miToolsStatsMembranePotentials_Click;
            // 
            // miToolsStatsCurrents
            // 
            miToolsStatsCurrents.Name = "miToolsStatsCurrents";
            miToolsStatsCurrents.Size = new Size(188, 22);
            miToolsStatsCurrents.Text = "Currents";
            miToolsStatsCurrents.Click += miToolsStatsCurrents_Click;
            // 
            // miToolsSep3
            // 
            miToolsSep3.Name = "miToolsSep3";
            miToolsSep3.Size = new Size(185, 6);
            // 
            // miToolsStatsFull
            // 
            miToolsStatsFull.Name = "miToolsStatsFull";
            miToolsStatsFull.Size = new Size(188, 22);
            miToolsStatsFull.Text = "Full Stats";
            miToolsStatsFull.Click += miToolsStatsFull_Click;
            // 
            // miToolsMultipleRun
            // 
            miToolsMultipleRun.Name = "miToolsMultipleRun";
            miToolsMultipleRun.Size = new Size(207, 22);
            miToolsMultipleRun.Text = "Run Multiple Simulations";
            miToolsMultipleRun.Click += miToolsMultipleRun_Click;
            // 
            // miToolsRunTimeStats
            // 
            miToolsRunTimeStats.Name = "miToolsRunTimeStats";
            miToolsRunTimeStats.Size = new Size(207, 22);
            miToolsRunTimeStats.Text = "Run Time Statistics";
            miToolsRunTimeStats.Click += miToolsRunTimeStats_Click;
            // 
            // miToolsSepStats
            // 
            miToolsSepStats.Name = "miToolsSepStats";
            miToolsSepStats.Size = new Size(204, 6);
            // 
            // miToolsSettings
            // 
            miToolsSettings.Name = "miToolsSettings";
            miToolsSettings.Size = new Size(207, 22);
            miToolsSettings.Text = "Settings";
            miToolsSettings.Click += miToolsSettings_Click;
            // 
            // miToolsMemoryFlush
            // 
            miToolsMemoryFlush.Name = "miToolsMemoryFlush";
            miToolsMemoryFlush.Size = new Size(207, 22);
            miToolsMemoryFlush.Text = "Memory Flush";
            miToolsMemoryFlush.Click += miToolsMemoryFlush_Click;
            // 
            // mView
            // 
            mView.DropDownItems.AddRange(new ToolStripItem[] { miViewOutputFolder, miViewTempFolder, miViewAbout });
            mView.Name = "mView";
            mView.Size = new Size(44, 19);
            mView.Text = "View";
            // 
            // miViewOutputFolder
            // 
            miViewOutputFolder.Name = "miViewOutputFolder";
            miViewOutputFolder.Size = new Size(148, 22);
            miViewOutputFolder.Text = "Output Folder";
            miViewOutputFolder.Click += miViewOutputFolder_Click;
            // 
            // miViewTempFolder
            // 
            miViewTempFolder.Name = "miViewTempFolder";
            miViewTempFolder.Size = new Size(148, 22);
            miViewTempFolder.Text = "Temp Folder";
            miViewTempFolder.Click += miViewTempFolder_Click;
            // 
            // miViewAbout
            // 
            miViewAbout.Name = "miViewAbout";
            miViewAbout.Size = new Size(148, 22);
            miViewAbout.Text = "About";
            miViewAbout.Click += miViewAbout_Click;
            // 
            // splitMain
            // 
            splitMain.BackColor = Color.Gray;
            splitMain.BorderStyle = BorderStyle.FixedSingle;
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(4, 36);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.BackColor = Color.White;
            splitMain.Panel1.Controls.Add(modelControl);
            splitMain.Panel1.Controls.Add(pGenerateModel);
            splitMain.Panel1.Controls.Add(pSimulation);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.BackColor = Color.White;
            splitMain.Panel2.Controls.Add(modelOutputControl);
            splitMain.Size = new Size(1340, 671);
            splitMain.SplitterDistance = 545;
            splitMain.SplitterWidth = 2;
            splitMain.TabIndex = 5;
            // 
            // modelControl
            // 
            modelControl.Dock = DockStyle.Fill;
            modelControl.Location = new Point(0, 0);
            modelControl.ModelUpdated = true;
            modelControl.Name = "modelControl";
            modelControl.Size = new Size(543, 453);
            modelControl.TabIndex = 2;
            modelControl.ModelChanged += modelControl_ModelChanged;
            modelControl.PlotRequested += modelControl_PlotRequested;
            modelControl.HighlightRequested += modelControl_HighlightRequested;
            // 
            // pGenerateModel
            // 
            pGenerateModel.Controls.Add(btnGenerateModel);
            pGenerateModel.Controls.Add(linkLabel4);
            pGenerateModel.Dock = DockStyle.Bottom;
            pGenerateModel.Location = new Point(0, 453);
            pGenerateModel.Name = "pGenerateModel";
            pGenerateModel.Size = new Size(543, 51);
            pGenerateModel.TabIndex = 4;
            // 
            // btnGenerateModel
            // 
            btnGenerateModel.BackColor = Color.FromArgb(96, 125, 139);
            btnGenerateModel.FlatAppearance.BorderColor = Color.LightGray;
            btnGenerateModel.FlatStyle = FlatStyle.Popup;
            btnGenerateModel.Font = new Font("Segoe UI", 12F);
            btnGenerateModel.ForeColor = Color.White;
            btnGenerateModel.Location = new Point(5, 5);
            btnGenerateModel.Name = "btnGenerateModel";
            btnGenerateModel.Size = new Size(263, 39);
            btnGenerateModel.TabIndex = 10;
            btnGenerateModel.Text = "Generate Model from Template";
            btnGenerateModel.UseVisualStyleBackColor = false;
            btnGenerateModel.Click += btnGenerateModel_Click;
            // 
            // linkLabel4
            // 
            linkLabel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkLabel4.AutoSize = true;
            linkLabel4.ForeColor = Color.White;
            linkLabel4.LinkColor = Color.FromArgb(64, 64, 64);
            linkLabel4.Location = new Point(807, 11);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(71, 15);
            linkLabel4.TabIndex = 5;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Clear Model";
            // 
            // pSimulation
            // 
            pSimulation.BackColor = Color.FromArgb(236, 239, 241);
            pSimulation.Controls.Add(btnResume);
            pSimulation.Controls.Add(btnStart);
            pSimulation.Controls.Add(btnStop);
            pSimulation.Controls.Add(btnPause);
            pSimulation.Controls.Add(simulationSettings);
            pSimulation.Controls.Add(lProgress);
            pSimulation.Controls.Add(lRunTime);
            pSimulation.Controls.Add(progressBarRun);
            pSimulation.Controls.Add(lSimulationSettings);
            pSimulation.Dock = DockStyle.Bottom;
            pSimulation.Location = new Point(0, 504);
            pSimulation.Name = "pSimulation";
            pSimulation.Size = new Size(543, 165);
            pSimulation.TabIndex = 1;
            // 
            // btnResume
            // 
            btnResume.BackColor = Color.FromArgb(96, 125, 139);
            btnResume.Font = new Font("Segoe UI", 14F);
            btnResume.ForeColor = Color.White;
            btnResume.Location = new Point(8, 104);
            btnResume.Name = "btnResume";
            btnResume.Size = new Size(60, 33);
            btnResume.TabIndex = 41;
            btnResume.Text = "⏸▶";
            toolTip.SetToolTip(btnResume, "Resume Simulation");
            btnResume.UseVisualStyleBackColor = false;
            btnResume.Visible = false;
            btnResume.Click += btnResume_Click;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.FromArgb(96, 125, 139);
            btnStart.Font = new Font("Segoe UI", 14F);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(8, 104);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(60, 33);
            btnStart.TabIndex = 40;
            btnStart.Text = "▶";
            toolTip.SetToolTip(btnStart, "Run Simulation");
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.FromArgb(96, 125, 139);
            btnStop.Font = new Font("Segoe UI", 14F);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(65, 104);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(44, 33);
            btnStop.TabIndex = 39;
            btnStop.Text = "⏹";
            toolTip.SetToolTip(btnStop, "Stop Simulation");
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Visible = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnPause
            // 
            btnPause.BackColor = Color.FromArgb(96, 125, 139);
            btnPause.Font = new Font("Segoe UI", 14F);
            btnPause.ForeColor = Color.White;
            btnPause.Location = new Point(8, 104);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(60, 33);
            btnPause.TabIndex = 38;
            btnPause.Text = "⏸";
            toolTip.SetToolTip(btnPause, "Pause Simulation");
            btnPause.UseVisualStyleBackColor = false;
            btnPause.Visible = false;
            btnPause.Click += btnPause_Click;
            // 
            // simulationSettings
            // 
            simulationSettings.Location = new Point(8, 26);
            simulationSettings.Name = "simulationSettings";
            simulationSettings.Size = new Size(143, 84);
            simulationSettings.TabIndex = 37;
            // 
            // lProgress
            // 
            lProgress.AutoSize = true;
            lProgress.Location = new Point(115, 113);
            lProgress.Name = "lProgress";
            lProgress.Size = new Size(55, 15);
            lProgress.TabIndex = 36;
            lProgress.Text = "Progress:";
            lProgress.Visible = false;
            // 
            // lRunTime
            // 
            lRunTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lRunTime.Font = new Font("Segoe UI", 8F);
            lRunTime.Location = new Point(153, 8);
            lRunTime.Name = "lRunTime";
            lRunTime.Size = new Size(387, 94);
            lRunTime.TabIndex = 31;
            // 
            // progressBarRun
            // 
            progressBarRun.Dock = DockStyle.Bottom;
            progressBarRun.Location = new Point(0, 142);
            progressBarRun.Name = "progressBarRun";
            progressBarRun.Size = new Size(543, 23);
            progressBarRun.TabIndex = 26;
            progressBarRun.Visible = false;
            // 
            // lSimulationSettings
            // 
            lSimulationSettings.AutoSize = true;
            lSimulationSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lSimulationSettings.Location = new Point(8, 8);
            lSimulationSettings.Name = "lSimulationSettings";
            lSimulationSettings.Size = new Size(115, 15);
            lSimulationSettings.TabIndex = 1;
            lSimulationSettings.Text = "Simulation Settings";
            // 
            // modelOutputControl
            // 
            modelOutputControl.Dock = DockStyle.Fill;
            modelOutputControl.Location = new Point(0, 0);
            modelOutputControl.Name = "modelOutputControl";
            modelOutputControl.Size = new Size(791, 669);
            modelOutputControl.TabIndex = 0;
            // 
            // timerRun
            // 
            timerRun.Interval = 1000;
            timerRun.Tick += timerRun_Tick;
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
            // saveFileCSV
            // 
            saveFileCSV.Filter = "CSV files(*.csv)|*.csv";
            // 
            // saveFileText
            // 
            saveFileText.Filter = "Text files(*.txt)|*.txt";
            // 
            // saveFileImage
            // 
            saveFileImage.Filter = "Image files(*.png)|*.png";
            // 
            // pDistinguisher
            // 
            pDistinguisher.BackColor = Color.FromArgb(192, 192, 255);
            pDistinguisher.Dock = DockStyle.Left;
            pDistinguisher.Location = new Point(0, 0);
            pDistinguisher.Name = "pDistinguisher";
            pDistinguisher.Size = new Size(4, 711);
            pDistinguisher.TabIndex = 6;
            // 
            // pDistinguisherTop
            // 
            pDistinguisherTop.BackColor = Color.Blue;
            pDistinguisherTop.Dock = DockStyle.Top;
            pDistinguisherTop.Location = new Point(4, 0);
            pDistinguisherTop.Name = "pDistinguisherTop";
            pDistinguisherTop.Size = new Size(1344, 4);
            pDistinguisherTop.TabIndex = 7;
            // 
            // pDistinguisherRight
            // 
            pDistinguisherRight.BackColor = Color.FromArgb(76, 175, 80);
            pDistinguisherRight.Dock = DockStyle.Right;
            pDistinguisherRight.Location = new Point(1344, 4);
            pDistinguisherRight.Name = "pDistinguisherRight";
            pDistinguisherRight.Size = new Size(4, 707);
            pDistinguisherRight.TabIndex = 8;
            // 
            // pDistinguisherBottom
            // 
            pDistinguisherBottom.BackColor = Color.FromArgb(76, 175, 80);
            pDistinguisherBottom.Dock = DockStyle.Bottom;
            pDistinguisherBottom.Location = new Point(4, 707);
            pDistinguisherBottom.Name = "pDistinguisherBottom";
            pDistinguisherBottom.Size = new Size(1340, 4);
            pDistinguisherBottom.TabIndex = 9;
            // 
            // saveFileExcel
            // 
            saveFileExcel.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            // 
            // openFileExcel
            // 
            openFileExcel.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1348, 711);
            Controls.Add(splitMain);
            Controls.Add(pTop);
            Controls.Add(pDistinguisherBottom);
            Controls.Add(pDistinguisherRight);
            Controls.Add(pDistinguisherTop);
            Controls.Add(pDistinguisher);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMain;
            Name = "MainForm";
            Text = "SiliFish";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            pTop.ResumeLayout(false);
            pTop.PerformLayout();
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            pGenerateModel.ResumeLayout(false);
            pGenerateModel.PerformLayout();
            pSimulation.ResumeLayout(false);
            pSimulation.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pTop;
        private SplitContainer splitMain;
        private Panel pSimulation;
        private Label lSimulationSettings;
        private ProgressBar progressBarRun;
        private System.Windows.Forms.Timer timerRun;
        private SaveFileDialog saveFileHTML;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private SaveFileDialog saveFileCSV;
        private ToolTip toolTip;
        private SaveFileDialog saveFileText;
        private Label lRunTime;
        private SaveFileDialog saveFileImage;
        private Button btnAbout;
        private ModelOutputControl modelOutputControl;
        private ModelControl modelControl;
        private Panel pGenerateModel;
        private LinkLabel linkLabel4;
        private Button btnGenerateModel;
        private Panel pDistinguisher;
        private Panel pDistinguisherTop;
        private Panel pDistinguisherRight;
        private Panel pDistinguisherBottom;
        private SaveFileDialog saveFileExcel;
        private OpenFileDialog openFileExcel;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem mFile;
        private ToolStripMenuItem miFileLoad;
        private ToolStripMenuItem miFileSave;
        private ToolStripMenuItem miFileNewModel;
        private ToolStripMenuItem miFileClearModel;
        private ToolStripMenuItem mTools;
        private ToolStripMenuItem miToolsCompareModel;
        private ToolStripSeparator miFileSep1;
        private ToolStripMenuItem miFileExport;
        private ToolStripMenuItem miFileImport;
        private ToolStripSeparator miToolsSep1;
        private ToolStripMenuItem miToolsCellularDynamics;
        private ToolStripSeparator miFileSep2;
        private ToolStripMenuItem mView;
        private ToolStripMenuItem miViewOutputFolder;
        private ToolStripMenuItem miViewTempFolder;
        private ToolStripMenuItem miViewAbout;
        private ToolStripMenuItem miToolsSettings;
        private ToolStripSeparator miToolsSep2;
        private ToolStripMenuItem miToolsGenerateStatsData;
        private ToolStripMenuItem miToolsRunTimeStats;
        private ToolStripSeparator miToolsSepStats;
        private ToolStripMenuItem miToolsStatsSpikeStats;
        private ToolStripMenuItem miToolsStatsSpikes;
        private ToolStripSeparator miToolsSep3;
        private ToolStripMenuItem miToolsStatsFull;
        private ToolStripMenuItem miToolsMultipleRun;
        private ToolStripMenuItem miFileSaveSimulationResults;
        private Label lProgress;
        private Controls.General.SimulationSettingsControl simulationSettings;
        private ToolStripMenuItem miToolsStatsSpikeCounts;
        private ToolStripMenuItem miToolsStatsEpisodes;
        private ToolStripMenuItem miToolsMemoryFlush;
        private ToolStripSeparator miFileSep3;
        private ToolStripMenuItem miFileExit;
        private ToolStripMenuItem miToolsStatsMembranePotentials;
        private ToolStripMenuItem miToolsStatsCurrents;
        private Button btnStart;
        private Button btnStop;
        private Button btnPause;
        private Button btnResume;
        private ToolStripMenuItem miFileBatchSave;
    }
}