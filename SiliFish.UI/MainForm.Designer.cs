﻿using SiliFish.UI.Controls;

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
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnCellularDynamics = new System.Windows.Forms.Button();
            this.linkBrowseToTempFolder = new System.Windows.Forms.LinkLabel();
            this.linkOpenOutputFolder = new System.Windows.Forms.LinkLabel();
            this.linkLoadModel = new System.Windows.Forms.LinkLabel();
            this.linkClearModel = new System.Windows.Forms.LinkLabel();
            this.linkSaveModel = new System.Windows.Forms.LinkLabel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.modelControl = new SiliFish.UI.Controls.ModelControl();
            this.pGenerateModel = new System.Windows.Forms.Panel();
            this.btnGenerateModel = new System.Windows.Forms.Button();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.pModelControlTop = new System.Windows.Forms.Panel();
            this.pSimulation = new System.Windows.Forms.Panel();
            this.ldtEuler = new System.Windows.Forms.Label();
            this.edtEuler = new System.Windows.Forms.NumericUpDown();
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
            this.modelOutputControl = new SiliFish.UI.Controls.ModelOutputControl();
            this.timerRun = new System.Windows.Forms.Timer(this.components);
            this.saveFileHTML = new System.Windows.Forms.SaveFileDialog();
            this.saveFileJson = new System.Windows.Forms.SaveFileDialog();
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            this.saveFileCSV = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileText = new System.Windows.Forms.SaveFileDialog();
            this.saveFileImage = new System.Windows.Forms.SaveFileDialog();
            this.pDistinguisher = new System.Windows.Forms.Panel();
            this.pDistinguisherTop = new System.Windows.Forms.Panel();
            this.pDistinguisherRight = new System.Windows.Forms.Panel();
            this.pDistinguisherBottom = new System.Windows.Forms.Panel();
            this.pTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pGenerateModel.SuspendLayout();
            this.pModelControlTop.SuspendLayout();
            this.pSimulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtEuler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eTimeEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pTop.Controls.Add(this.btnSettings);
            this.pTop.Controls.Add(this.btnAbout);
            this.pTop.Controls.Add(this.btnCellularDynamics);
            this.pTop.Controls.Add(this.linkBrowseToTempFolder);
            this.pTop.Controls.Add(this.linkOpenOutputFolder);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(4, 4);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1340, 40);
            this.pTop.TabIndex = 3;
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.AutoSize = true;
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(1200, 6);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(88, 27);
            this.btnSettings.TabIndex = 25;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.AutoSize = true;
            this.btnAbout.BackColor = System.Drawing.Color.Transparent;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.Location = new System.Drawing.Point(1294, 5);
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
            this.linkBrowseToTempFolder.Location = new System.Drawing.Point(1081, 13);
            this.linkBrowseToTempFolder.Name = "linkBrowseToTempFolder";
            this.linkBrowseToTempFolder.Size = new System.Drawing.Size(104, 15);
            this.linkBrowseToTempFolder.TabIndex = 7;
            this.linkBrowseToTempFolder.TabStop = true;
            this.linkBrowseToTempFolder.Text = "Open Temp Folder";
            this.toolTip.SetToolTip(this.linkBrowseToTempFolder, "The contents saved to the temp folder will be cleared on exit.");
            this.linkBrowseToTempFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOpenTempFolder_LinkClicked);
            // 
            // linkOpenOutputFolder
            // 
            this.linkOpenOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkOpenOutputFolder.AutoSize = true;
            this.linkOpenOutputFolder.ForeColor = System.Drawing.Color.White;
            this.linkOpenOutputFolder.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkOpenOutputFolder.Location = new System.Drawing.Point(962, 13);
            this.linkOpenOutputFolder.Name = "linkOpenOutputFolder";
            this.linkOpenOutputFolder.Size = new System.Drawing.Size(113, 15);
            this.linkOpenOutputFolder.TabIndex = 6;
            this.linkOpenOutputFolder.TabStop = true;
            this.linkOpenOutputFolder.Text = "Open Output Folder";
            this.toolTip.SetToolTip(this.linkOpenOutputFolder, "If there is a problem in creating html files, they will be saved in this folder.\r" +
        "\n");
            this.linkOpenOutputFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOpenOutputFolder_LinkClicked);
            // 
            // linkLoadModel
            // 
            this.linkLoadModel.AutoSize = true;
            this.linkLoadModel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLoadModel.Location = new System.Drawing.Point(11, 11);
            this.linkLoadModel.Name = "linkLoadModel";
            this.linkLoadModel.Size = new System.Drawing.Size(70, 15);
            this.linkLoadModel.TabIndex = 4;
            this.linkLoadModel.TabStop = true;
            this.linkLoadModel.Text = "Load Model";
            this.linkLoadModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLoadModel_LinkClicked);
            // 
            // linkClearModel
            // 
            this.linkClearModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkClearModel.AutoSize = true;
            this.linkClearModel.ForeColor = System.Drawing.Color.White;
            this.linkClearModel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkClearModel.Location = new System.Drawing.Point(459, 11);
            this.linkClearModel.Name = "linkClearModel";
            this.linkClearModel.Size = new System.Drawing.Size(71, 15);
            this.linkClearModel.TabIndex = 5;
            this.linkClearModel.TabStop = true;
            this.linkClearModel.Text = "Clear Model";
            this.linkClearModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClearModel_LinkClicked);
            // 
            // linkSaveModel
            // 
            this.linkSaveModel.AutoSize = true;
            this.linkSaveModel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveModel.Location = new System.Drawing.Point(87, 11);
            this.linkSaveModel.Name = "linkSaveModel";
            this.linkSaveModel.Size = new System.Drawing.Size(68, 15);
            this.linkSaveModel.TabIndex = 3;
            this.linkSaveModel.TabStop = true;
            this.linkSaveModel.Text = "Save Model";
            this.linkSaveModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveModel_LinkClicked);
            // 
            // splitMain
            // 
            this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(4, 44);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.modelControl);
            this.splitMain.Panel1.Controls.Add(this.pGenerateModel);
            this.splitMain.Panel1.Controls.Add(this.pModelControlTop);
            this.splitMain.Panel1.Controls.Add(this.pSimulation);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.modelOutputControl);
            this.splitMain.Size = new System.Drawing.Size(1340, 663);
            this.splitMain.SplitterDistance = 545;
            this.splitMain.TabIndex = 5;
            // 
            // modelControl
            // 
            this.modelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelControl.Location = new System.Drawing.Point(0, 40);
            this.modelControl.ModelUpdated = true;
            this.modelControl.Name = "modelControl";
            this.modelControl.Size = new System.Drawing.Size(543, 405);
            this.modelControl.TabIndex = 2;
            this.modelControl.ModelChanged += new System.EventHandler(this.modelControl_ModelChanged);
            this.modelControl.PlotRequested += new System.EventHandler(this.modelControl_PlotRequested);
            // 
            // pGenerateModel
            // 
            this.pGenerateModel.Controls.Add(this.btnGenerateModel);
            this.pGenerateModel.Controls.Add(this.linkLabel4);
            this.pGenerateModel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pGenerateModel.Location = new System.Drawing.Point(0, 445);
            this.pGenerateModel.Name = "pGenerateModel";
            this.pGenerateModel.Size = new System.Drawing.Size(543, 51);
            this.pGenerateModel.TabIndex = 4;
            // 
            // btnGenerateModel
            // 
            this.btnGenerateModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnGenerateModel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnGenerateModel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenerateModel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGenerateModel.ForeColor = System.Drawing.Color.White;
            this.btnGenerateModel.Location = new System.Drawing.Point(5, 5);
            this.btnGenerateModel.Name = "btnGenerateModel";
            this.btnGenerateModel.Size = new System.Drawing.Size(263, 39);
            this.btnGenerateModel.TabIndex = 10;
            this.btnGenerateModel.Text = "Generate Model from Template";
            this.btnGenerateModel.UseVisualStyleBackColor = false;
            this.btnGenerateModel.Click += new System.EventHandler(this.btnGenerateModel_Click);
            // 
            // linkLabel4
            // 
            this.linkLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.ForeColor = System.Drawing.Color.White;
            this.linkLabel4.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLabel4.Location = new System.Drawing.Point(807, 11);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(71, 15);
            this.linkLabel4.TabIndex = 5;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Clear Model";
            // 
            // pModelControlTop
            // 
            this.pModelControlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pModelControlTop.Controls.Add(this.linkLoadModel);
            this.pModelControlTop.Controls.Add(this.linkSaveModel);
            this.pModelControlTop.Controls.Add(this.linkClearModel);
            this.pModelControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pModelControlTop.Location = new System.Drawing.Point(0, 0);
            this.pModelControlTop.Name = "pModelControlTop";
            this.pModelControlTop.Size = new System.Drawing.Size(543, 40);
            this.pModelControlTop.TabIndex = 3;
            // 
            // pSimulation
            // 
            this.pSimulation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pSimulation.Controls.Add(this.ldtEuler);
            this.pSimulation.Controls.Add(this.edtEuler);
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
            this.pSimulation.Location = new System.Drawing.Point(0, 496);
            this.pSimulation.Name = "pSimulation";
            this.pSimulation.Size = new System.Drawing.Size(543, 165);
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
            this.lRunTime.Location = new System.Drawing.Point(153, 36);
            this.lRunTime.Name = "lRunTime";
            this.lRunTime.Size = new System.Drawing.Size(368, 66);
            this.lRunTime.TabIndex = 31;
            // 
            // linkExportOutput
            // 
            this.linkExportOutput.AutoSize = true;
            this.linkExportOutput.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkExportOutput.Location = new System.Drawing.Point(222, 114);
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
            this.progressBarRun.Size = new System.Drawing.Size(543, 23);
            this.progressBarRun.TabIndex = 26;
            this.progressBarRun.Visible = false;
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnRun.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(153, 110);
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
            // modelOutputControl
            // 
            this.modelOutputControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelOutputControl.Location = new System.Drawing.Point(0, 0);
            this.modelOutputControl.Name = "modelOutputControl";
            this.modelOutputControl.Size = new System.Drawing.Size(789, 661);
            this.modelOutputControl.TabIndex = 0;
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
            // pDistinguisher
            // 
            this.pDistinguisher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pDistinguisher.Dock = System.Windows.Forms.DockStyle.Left;
            this.pDistinguisher.Location = new System.Drawing.Point(0, 0);
            this.pDistinguisher.Name = "pDistinguisher";
            this.pDistinguisher.Size = new System.Drawing.Size(4, 711);
            this.pDistinguisher.TabIndex = 6;
            // 
            // pDistinguisherTop
            // 
            this.pDistinguisherTop.BackColor = System.Drawing.Color.Blue;
            this.pDistinguisherTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pDistinguisherTop.Location = new System.Drawing.Point(4, 0);
            this.pDistinguisherTop.Name = "pDistinguisherTop";
            this.pDistinguisherTop.Size = new System.Drawing.Size(1344, 4);
            this.pDistinguisherTop.TabIndex = 7;
            // 
            // pDistinguisherRight
            // 
            this.pDistinguisherRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.pDistinguisherRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pDistinguisherRight.Location = new System.Drawing.Point(1344, 4);
            this.pDistinguisherRight.Name = "pDistinguisherRight";
            this.pDistinguisherRight.Size = new System.Drawing.Size(4, 707);
            this.pDistinguisherRight.TabIndex = 8;
            // 
            // pDistinguisherBottom
            // 
            this.pDistinguisherBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.pDistinguisherBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pDistinguisherBottom.Location = new System.Drawing.Point(4, 707);
            this.pDistinguisherBottom.Name = "pDistinguisherBottom";
            this.pDistinguisherBottom.Size = new System.Drawing.Size(1340, 4);
            this.pDistinguisherBottom.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 711);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pDistinguisherBottom);
            this.Controls.Add(this.pDistinguisherRight);
            this.Controls.Add(this.pDistinguisherTop);
            this.Controls.Add(this.pDistinguisher);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SiliFish";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pGenerateModel.ResumeLayout(false);
            this.pGenerateModel.PerformLayout();
            this.pModelControlTop.ResumeLayout(false);
            this.pModelControlTop.PerformLayout();
            this.pSimulation.ResumeLayout(false);
            this.pSimulation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtEuler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eTimeEnd)).EndInit();
            this.ResumeLayout(false);

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
    }
}