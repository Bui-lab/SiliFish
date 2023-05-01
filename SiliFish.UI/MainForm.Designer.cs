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
            btnSettings = new Button();
            btnAbout = new Button();
            btnCellularDynamics = new Button();
            linkBrowseToTempFolder = new LinkLabel();
            linkOpenOutputFolder = new LinkLabel();
            linkLoadModel = new LinkLabel();
            linkClearModel = new LinkLabel();
            linkSaveModel = new LinkLabel();
            splitMain = new SplitContainer();
            modelControl = new ModelControl();
            pGenerateModel = new Panel();
            btnGenerateModel = new Button();
            linkLabel4 = new LinkLabel();
            pModelControlTop = new Panel();
            linkNewModel = new LinkLabel();
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
            pTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            pGenerateModel.SuspendLayout();
            pModelControlTop.SuspendLayout();
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
            pTop.Controls.Add(btnSettings);
            pTop.Controls.Add(btnAbout);
            pTop.Controls.Add(btnCellularDynamics);
            pTop.Controls.Add(linkBrowseToTempFolder);
            pTop.Controls.Add(linkOpenOutputFolder);
            pTop.Dock = DockStyle.Top;
            pTop.Location = new Point(4, 4);
            pTop.Name = "pTop";
            pTop.Size = new Size(1340, 40);
            pTop.TabIndex = 3;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSettings.AutoSize = true;
            btnSettings.BackColor = Color.FromArgb(96, 125, 139);
            btnSettings.FlatAppearance.BorderColor = Color.LightGray;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.ForeColor = Color.White;
            btnSettings.Location = new Point(1200, 6);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(88, 27);
            btnSettings.TabIndex = 25;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnAbout
            // 
            btnAbout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAbout.AutoSize = true;
            btnAbout.BackColor = Color.Transparent;
            btnAbout.Image = (Image)resources.GetObject("btnAbout.Image");
            btnAbout.Location = new Point(1294, 5);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(33, 30);
            btnAbout.TabIndex = 24;
            toolTip.SetToolTip(btnAbout, "About");
            btnAbout.UseVisualStyleBackColor = false;
            btnAbout.Click += btnAbout_Click;
            // 
            // btnCellularDynamics
            // 
            btnCellularDynamics.AutoSize = true;
            btnCellularDynamics.BackColor = Color.FromArgb(96, 125, 139);
            btnCellularDynamics.FlatAppearance.BorderColor = Color.LightGray;
            btnCellularDynamics.FlatStyle = FlatStyle.Flat;
            btnCellularDynamics.ForeColor = Color.White;
            btnCellularDynamics.Location = new Point(4, 6);
            btnCellularDynamics.Name = "btnCellularDynamics";
            btnCellularDynamics.Size = new Size(120, 27);
            btnCellularDynamics.TabIndex = 23;
            btnCellularDynamics.Text = "Cellular Dynamics";
            btnCellularDynamics.UseVisualStyleBackColor = false;
            btnCellularDynamics.Click += btnCellularDynamics_Click;
            // 
            // linkBrowseToTempFolder
            // 
            linkBrowseToTempFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkBrowseToTempFolder.AutoSize = true;
            linkBrowseToTempFolder.ForeColor = Color.White;
            linkBrowseToTempFolder.LinkColor = Color.FromArgb(64, 64, 64);
            linkBrowseToTempFolder.Location = new Point(1081, 13);
            linkBrowseToTempFolder.Name = "linkBrowseToTempFolder";
            linkBrowseToTempFolder.Size = new Size(104, 15);
            linkBrowseToTempFolder.TabIndex = 7;
            linkBrowseToTempFolder.TabStop = true;
            linkBrowseToTempFolder.Text = "Open Temp Folder";
            toolTip.SetToolTip(linkBrowseToTempFolder, "The contents saved to the temp folder will be cleared on exit.");
            linkBrowseToTempFolder.LinkClicked += linkOpenTempFolder_LinkClicked;
            // 
            // linkOpenOutputFolder
            // 
            linkOpenOutputFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkOpenOutputFolder.AutoSize = true;
            linkOpenOutputFolder.ForeColor = Color.White;
            linkOpenOutputFolder.LinkColor = Color.FromArgb(64, 64, 64);
            linkOpenOutputFolder.Location = new Point(962, 13);
            linkOpenOutputFolder.Name = "linkOpenOutputFolder";
            linkOpenOutputFolder.Size = new Size(113, 15);
            linkOpenOutputFolder.TabIndex = 6;
            linkOpenOutputFolder.TabStop = true;
            linkOpenOutputFolder.Text = "Open Output Folder";
            toolTip.SetToolTip(linkOpenOutputFolder, "If there is a problem in creating html files, they will be saved in this folder.\r\n");
            linkOpenOutputFolder.LinkClicked += linkOpenOutputFolder_LinkClicked;
            // 
            // linkLoadModel
            // 
            linkLoadModel.AutoSize = true;
            linkLoadModel.LinkColor = Color.FromArgb(64, 64, 64);
            linkLoadModel.Location = new Point(11, 11);
            linkLoadModel.Name = "linkLoadModel";
            linkLoadModel.Size = new Size(70, 15);
            linkLoadModel.TabIndex = 4;
            linkLoadModel.TabStop = true;
            linkLoadModel.Text = "Load Model";
            linkLoadModel.LinkClicked += linkLoadModel_LinkClicked;
            // 
            // linkClearModel
            // 
            linkClearModel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkClearModel.AutoSize = true;
            linkClearModel.ForeColor = Color.White;
            linkClearModel.LinkColor = Color.FromArgb(64, 64, 64);
            linkClearModel.Location = new Point(459, 11);
            linkClearModel.Name = "linkClearModel";
            linkClearModel.Size = new Size(71, 15);
            linkClearModel.TabIndex = 5;
            linkClearModel.TabStop = true;
            linkClearModel.Text = "Clear Model";
            linkClearModel.LinkClicked += linkClearModel_LinkClicked;
            // 
            // linkSaveModel
            // 
            linkSaveModel.AutoSize = true;
            linkSaveModel.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveModel.Location = new Point(87, 11);
            linkSaveModel.Name = "linkSaveModel";
            linkSaveModel.Size = new Size(68, 15);
            linkSaveModel.TabIndex = 3;
            linkSaveModel.TabStop = true;
            linkSaveModel.Text = "Save Model";
            linkSaveModel.LinkClicked += linkSaveModel_LinkClicked;
            // 
            // splitMain
            // 
            splitMain.BackColor = Color.Gray;
            splitMain.BorderStyle = BorderStyle.FixedSingle;
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(4, 44);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.BackColor = Color.White;
            splitMain.Panel1.Controls.Add(modelControl);
            splitMain.Panel1.Controls.Add(pGenerateModel);
            splitMain.Panel1.Controls.Add(pModelControlTop);
            splitMain.Panel1.Controls.Add(pSimulation);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.BackColor = Color.White;
            splitMain.Panel2.Controls.Add(modelOutputControl);
            splitMain.Size = new Size(1340, 663);
            splitMain.SplitterDistance = 545;
            splitMain.SplitterWidth = 2;
            splitMain.TabIndex = 5;
            // 
            // modelControl
            // 
            modelControl.Dock = DockStyle.Fill;
            modelControl.Location = new Point(0, 40);
            modelControl.ModelUpdated = true;
            modelControl.Name = "modelControl";
            modelControl.Size = new Size(543, 405);
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
            pGenerateModel.Location = new Point(0, 445);
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
            // pModelControlTop
            // 
            pModelControlTop.BackColor = Color.FromArgb(236, 239, 241);
            pModelControlTop.Controls.Add(linkNewModel);
            pModelControlTop.Controls.Add(linkLoadModel);
            pModelControlTop.Controls.Add(linkSaveModel);
            pModelControlTop.Controls.Add(linkClearModel);
            pModelControlTop.Dock = DockStyle.Top;
            pModelControlTop.Location = new Point(0, 0);
            pModelControlTop.Name = "pModelControlTop";
            pModelControlTop.Size = new Size(543, 40);
            pModelControlTop.TabIndex = 3;
            // 
            // linkNewModel
            // 
            linkNewModel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkNewModel.AutoSize = true;
            linkNewModel.ForeColor = Color.White;
            linkNewModel.LinkColor = Color.FromArgb(64, 64, 64);
            linkNewModel.Location = new Point(382, 11);
            linkNewModel.Name = "linkNewModel";
            linkNewModel.Size = new Size(68, 15);
            linkNewModel.TabIndex = 6;
            linkNewModel.TabStop = true;
            linkNewModel.Text = "New Model";
            linkNewModel.LinkClicked += linkNewModel_LinkClicked;
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
            pSimulation.Location = new Point(0, 496);
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
            modelOutputControl.Size = new Size(791, 661);
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
            Name = "MainForm";
            Text = "SiliFish";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            pTop.ResumeLayout(false);
            pTop.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            pGenerateModel.ResumeLayout(false);
            pGenerateModel.PerformLayout();
            pModelControlTop.ResumeLayout(false);
            pModelControlTop.PerformLayout();
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
        private LinkLabel linkOpenOutputFolder;
        private LinkLabel linkBrowseToTempFolder;
        private SaveFileDialog saveFileImage;
        private Button btnCellularDynamics;
        private Label ldtEuler;
        private NumericUpDown edtEuler;
        private Button btnAbout;
        private LinkLabel linkLoadModel;
        private LinkLabel linkSaveModel;
        private LinkLabel linkClearModel;
        private ModelOutputControl modelOutputControl;
        private ModelControl modelControl;
        private Panel pModelControlTop;
        private Panel pGenerateModel;
        private LinkLabel linkLabel4;
        private Button btnGenerateModel;
        private Panel pDistinguisher;
        private Panel pDistinguisherTop;
        private Panel pDistinguisherRight;
        private Panel pDistinguisherBottom;
        private Button btnSettings;
        private LinkLabel linkNewModel;
    }
}