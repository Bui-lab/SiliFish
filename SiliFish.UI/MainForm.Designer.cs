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
            miFileSep1 = new ToolStripSeparator();
            miFileExport = new ToolStripMenuItem();
            miFileImport = new ToolStripMenuItem();
            miFileSep2 = new ToolStripSeparator();
            miFileNewModel = new ToolStripMenuItem();
            miFileClearModel = new ToolStripMenuItem();
            mTools = new ToolStripMenuItem();
            miToolsCompareModel = new ToolStripMenuItem();
            miToolsSep1 = new ToolStripSeparator();
            miToolsCellularDynamics = new ToolStripMenuItem();
            miToolsSep2 = new ToolStripSeparator();
            miToolsSettings = new ToolStripMenuItem();
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
            ldtEuler = new Label();
            edtEuler = new NumericUpDown();
            eSkip = new NumericUpDown();
            lSkip = new Label();
            edt = new NumericUpDown();
            eTimeEnd = new NumericUpDown();
            ldt = new Label();
            lRunTime = new Label();
            linkExportOutput = new LinkLabel();
            progressBarRun = new ProgressBar();
            btnRun = new Button();
            lTimeEnd = new Label();
            lRunParameters = new Label();
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
            ((System.ComponentModel.ISupportInitialize)edtEuler).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eSkip).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eTimeEnd).BeginInit();
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
            mFile.DropDownItems.AddRange(new ToolStripItem[] { miFileLoad, miFileSave, miFileSep1, miFileExport, miFileImport, miFileSep2, miFileNewModel, miFileClearModel });
            mFile.Name = "mFile";
            mFile.Size = new Size(37, 19);
            mFile.Text = "File";
            // 
            // miFileLoad
            // 
            miFileLoad.Name = "miFileLoad";
            miFileLoad.Size = new Size(169, 22);
            miFileLoad.Text = "Load";
            miFileLoad.Click += miFileLoad_Click;
            // 
            // miFileSave
            // 
            miFileSave.Name = "miFileSave";
            miFileSave.ShortcutKeys = Keys.Control | Keys.S;
            miFileSave.Size = new Size(169, 22);
            miFileSave.Text = "Save";
            miFileSave.Click += miFileSave_Click;
            // 
            // miFileSep1
            // 
            miFileSep1.Name = "miFileSep1";
            miFileSep1.Size = new Size(166, 6);
            // 
            // miFileExport
            // 
            miFileExport.Name = "miFileExport";
            miFileExport.Size = new Size(169, 22);
            miFileExport.Text = "Export to Excel";
            miFileExport.Click += miFileExport_Click;
            // 
            // miFileImport
            // 
            miFileImport.Name = "miFileImport";
            miFileImport.Size = new Size(169, 22);
            miFileImport.Text = "Import from Excel";
            miFileImport.Click += miFileImport_Click;
            // 
            // miFileSep2
            // 
            miFileSep2.Name = "miFileSep2";
            miFileSep2.Size = new Size(166, 6);
            // 
            // miFileNewModel
            // 
            miFileNewModel.Name = "miFileNewModel";
            miFileNewModel.Size = new Size(169, 22);
            miFileNewModel.Text = "New Model";
            miFileNewModel.Click += miFileNewModel_Click;
            // 
            // miFileClearModel
            // 
            miFileClearModel.Name = "miFileClearModel";
            miFileClearModel.Size = new Size(169, 22);
            miFileClearModel.Text = "Clear Model";
            miFileClearModel.Click += miFileClearModel_Click;
            // 
            // mTools
            // 
            mTools.DropDownItems.AddRange(new ToolStripItem[] { miToolsCompareModel, miToolsSep1, miToolsCellularDynamics, miToolsSep2, miToolsSettings });
            mTools.Name = "mTools";
            mTools.Size = new Size(46, 19);
            mTools.Text = "Tools";
            // 
            // miToolsCompareModel
            // 
            miToolsCompareModel.Name = "miToolsCompareModel";
            miToolsCompareModel.Size = new Size(175, 22);
            miToolsCompareModel.Text = "Compare To Model";
            miToolsCompareModel.Click += miToolsCompareModel_Click;
            // 
            // miToolsSep1
            // 
            miToolsSep1.Name = "miToolsSep1";
            miToolsSep1.Size = new Size(172, 6);
            // 
            // miToolsCellularDynamics
            // 
            miToolsCellularDynamics.Name = "miToolsCellularDynamics";
            miToolsCellularDynamics.Size = new Size(175, 22);
            miToolsCellularDynamics.Text = "Cellular Dynamics";
            miToolsCellularDynamics.Click += miToolsCellularDynamics_Click;
            // 
            // miToolsSep2
            // 
            miToolsSep2.Name = "miToolsSep2";
            miToolsSep2.Size = new Size(172, 6);
            // 
            // miToolsSettings
            // 
            miToolsSettings.Name = "miToolsSettings";
            miToolsSettings.Size = new Size(175, 22);
            miToolsSettings.Text = "Settings";
            miToolsSettings.Click += miToolsSettings_Click;
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
            btnGenerateModel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
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
            pSimulation.Controls.Add(ldtEuler);
            pSimulation.Controls.Add(edtEuler);
            pSimulation.Controls.Add(eSkip);
            pSimulation.Controls.Add(lSkip);
            pSimulation.Controls.Add(edt);
            pSimulation.Controls.Add(eTimeEnd);
            pSimulation.Controls.Add(ldt);
            pSimulation.Controls.Add(lRunTime);
            pSimulation.Controls.Add(linkExportOutput);
            pSimulation.Controls.Add(progressBarRun);
            pSimulation.Controls.Add(btnRun);
            pSimulation.Controls.Add(lTimeEnd);
            pSimulation.Controls.Add(lRunParameters);
            pSimulation.Dock = DockStyle.Bottom;
            pSimulation.Location = new Point(0, 504);
            pSimulation.Name = "pSimulation";
            pSimulation.Size = new Size(543, 165);
            pSimulation.TabIndex = 1;
            // 
            // ldtEuler
            // 
            ldtEuler.AutoSize = true;
            ldtEuler.Location = new Point(10, 113);
            ldtEuler.Name = "ldtEuler";
            ldtEuler.Size = new Size(48, 15);
            ldtEuler.TabIndex = 39;
            ldtEuler.Text = "Δt Euler";
            // 
            // edtEuler
            // 
            edtEuler.DecimalPlaces = 3;
            edtEuler.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            edtEuler.Location = new Point(71, 110);
            edtEuler.Name = "edtEuler";
            edtEuler.Size = new Size(76, 23);
            edtEuler.TabIndex = 38;
            toolTip.SetToolTip(edtEuler, "(in milliseconds)");
            edtEuler.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // eSkip
            // 
            eSkip.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            eSkip.Location = new Point(71, 58);
            eSkip.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            eSkip.Name = "eSkip";
            eSkip.Size = new Size(76, 23);
            eSkip.TabIndex = 34;
            toolTip.SetToolTip(eSkip, "(in milliseconds)");
            // 
            // lSkip
            // 
            lSkip.AutoSize = true;
            lSkip.Location = new Point(11, 61);
            lSkip.Name = "lSkip";
            lSkip.Size = new Size(29, 15);
            lSkip.TabIndex = 6;
            lSkip.Text = "Skip";
            // 
            // edt
            // 
            edt.DecimalPlaces = 2;
            edt.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            edt.Location = new Point(71, 84);
            edt.Name = "edt";
            edt.Size = new Size(76, 23);
            edt.TabIndex = 35;
            toolTip.SetToolTip(edt, "(in milliseconds)");
            edt.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // eTimeEnd
            // 
            eTimeEnd.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            eTimeEnd.Location = new Point(71, 32);
            eTimeEnd.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            eTimeEnd.Name = "eTimeEnd";
            eTimeEnd.Size = new Size(76, 23);
            eTimeEnd.TabIndex = 33;
            eTimeEnd.ThousandsSeparator = true;
            toolTip.SetToolTip(eTimeEnd, "(in milliseconds)");
            eTimeEnd.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // ldt
            // 
            ldt.AutoSize = true;
            ldt.Location = new Point(10, 87);
            ldt.Name = "ldt";
            ldt.Size = new Size(60, 15);
            ldt.TabIndex = 32;
            ldt.Text = "Δt Output";
            // 
            // lRunTime
            // 
            lRunTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lRunTime.Location = new Point(153, 36);
            lRunTime.Name = "lRunTime";
            lRunTime.Size = new Size(368, 66);
            lRunTime.TabIndex = 31;
            // 
            // linkExportOutput
            // 
            linkExportOutput.AutoSize = true;
            linkExportOutput.LinkColor = Color.FromArgb(64, 64, 64);
            linkExportOutput.Location = new Point(222, 114);
            linkExportOutput.Name = "linkExportOutput";
            linkExportOutput.Size = new Size(82, 15);
            linkExportOutput.TabIndex = 27;
            linkExportOutput.TabStop = true;
            linkExportOutput.Text = "Export Output";
            toolTip.SetToolTip(linkExportOutput, "Exports membrane potential values of all cells, and currents passing through every junction throughout the simulation to multiple CSV files.");
            linkExportOutput.Visible = false;
            linkExportOutput.LinkClicked += linkExportOutput_LinkClicked;
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
            // btnRun
            // 
            btnRun.BackColor = Color.FromArgb(96, 125, 139);
            btnRun.FlatAppearance.BorderColor = Color.LightGray;
            btnRun.FlatStyle = FlatStyle.Flat;
            btnRun.ForeColor = Color.White;
            btnRun.Location = new Point(153, 110);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(63, 23);
            btnRun.TabIndex = 9;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = false;
            btnRun.Click += btnRun_Click;
            // 
            // lTimeEnd
            // 
            lTimeEnd.AutoSize = true;
            lTimeEnd.Location = new Point(10, 35);
            lTimeEnd.Name = "lTimeEnd";
            lTimeEnd.Size = new Size(56, 15);
            lTimeEnd.TabIndex = 3;
            lTimeEnd.Text = "Time End";
            // 
            // lRunParameters
            // 
            lRunParameters.AutoSize = true;
            lRunParameters.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lRunParameters.Location = new Point(8, 8);
            lRunParameters.Name = "lRunParameters";
            lRunParameters.Size = new Size(133, 15);
            lRunParameters.TabIndex = 1;
            lRunParameters.Text = "Simulation Parameters";
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
            ((System.ComponentModel.ISupportInitialize)edtEuler).EndInit();
            ((System.ComponentModel.ISupportInitialize)eSkip).EndInit();
            ((System.ComponentModel.ISupportInitialize)edt).EndInit();
            ((System.ComponentModel.ISupportInitialize)eTimeEnd).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel pTop;
        private SplitContainer splitMain;
        private Panel pSimulation;
        private Label lRunParameters;
        private Button btnRun;
        private Label lTimeEnd;
        private ProgressBar progressBarRun;
        private System.Windows.Forms.Timer timerRun;
        private SaveFileDialog saveFileHTML;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private LinkLabel linkExportOutput;
        private SaveFileDialog saveFileCSV;
        private ToolTip toolTip;
        private SaveFileDialog saveFileText;
        private Label lRunTime;
        private NumericUpDown edt;
        private NumericUpDown eTimeEnd;
        private Label ldt;
        private NumericUpDown eSkip;
        private Label lSkip;
        private SaveFileDialog saveFileImage;
        private Button btnCellularDynamics;
        private Label ldtEuler;
        private NumericUpDown edtEuler;
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
    }
}