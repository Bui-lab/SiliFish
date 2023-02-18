namespace SiliFish.UI.Controls
{
    partial class ModelControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelControl));
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            this.saveFileJson = new System.Windows.Forms.SaveFileDialog();
            this.tJson = new System.Windows.Forms.TabPage();
            this.eModelJSON = new System.Windows.Forms.RichTextBox();
            this.pModelJSONTop = new System.Windows.Forms.Panel();
            this.linkOpenJsonViewer = new System.Windows.Forms.LinkLabel();
            this.pLineModelJSON = new System.Windows.Forms.Panel();
            this.linkSaveModelJSON = new System.Windows.Forms.LinkLabel();
            this.btnLoadModelJSON = new System.Windows.Forms.Button();
            this.btnDisplayModelJSON = new System.Windows.Forms.Button();
            this.tSettings = new System.Windows.Forms.TabPage();
            this.propSettings = new System.Windows.Forms.PropertyGrid();
            this.pSettingsTop = new System.Windows.Forms.Panel();
            this.ddDefaultMuscleCellCore = new System.Windows.Forms.ComboBox();
            this.lDefaultMuscleCore = new System.Windows.Forms.Label();
            this.ddDefaultNeuronCore = new System.Windows.Forms.ComboBox();
            this.lDefaultNeuronCore = new System.Windows.Forms.Label();
            this.tKinematics = new System.Windows.Forms.TabPage();
            this.propKinematics = new System.Windows.Forms.PropertyGrid();
            this.pKinematicsDescription = new System.Windows.Forms.Panel();
            this.eKinematicsDescription = new System.Windows.Forms.TextBox();
            this.pKinemSep = new System.Windows.Forms.Panel();
            this.tArchitecture = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitCellPools = new System.Windows.Forms.SplitContainer();
            this.listCellPools = new SiliFish.UI.Controls.ListBoxControl();
            this.listCells = new SiliFish.UI.Controls.ListBoxControl();
            this.pCellsTop = new System.Windows.Forms.Panel();
            this.pCellsSep = new System.Windows.Forms.Panel();
            this.lCellsTitle = new System.Windows.Forms.Label();
            this.pCellPoolsTop = new System.Windows.Forms.Panel();
            this.pCellPoolsSep = new System.Windows.Forms.Panel();
            this.lCellPoolsTitle = new System.Windows.Forms.Label();
            this.splitConnectionsStimuli = new System.Windows.Forms.SplitContainer();
            this.listConnections = new SiliFish.UI.Controls.ListBoxControl();
            this.pConnectionsTop = new System.Windows.Forms.Panel();
            this.pConnectionsSep = new System.Windows.Forms.Panel();
            this.lConnectionsTitle = new System.Windows.Forms.Label();
            this.listStimuli = new SiliFish.UI.Controls.ListBoxControl();
            this.pStimuliTop = new System.Windows.Forms.Panel();
            this.pStimuliSep = new System.Windows.Forms.Panel();
            this.lStimuliTitle = new System.Windows.Forms.Label();
            this.tGeneral = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propModelDimensions = new System.Windows.Forms.PropertyGrid();
            this.pDrawing = new System.Windows.Forms.Panel();
            this.pbDrawing = new System.Windows.Forms.PictureBox();
            this.eModelDescription = new System.Windows.Forms.TextBox();
            this.eModelName = new System.Windows.Forms.TextBox();
            this.lDescription = new System.Windows.Forms.Label();
            this.lModelName = new System.Windows.Forms.Label();
            this.tabModel = new System.Windows.Forms.TabControl();
            this.tJson.SuspendLayout();
            this.pModelJSONTop.SuspendLayout();
            this.tSettings.SuspendLayout();
            this.pSettingsTop.SuspendLayout();
            this.tKinematics.SuspendLayout();
            this.pKinematicsDescription.SuspendLayout();
            this.tArchitecture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCellPools)).BeginInit();
            this.splitCellPools.Panel1.SuspendLayout();
            this.splitCellPools.Panel2.SuspendLayout();
            this.splitCellPools.SuspendLayout();
            this.pCellsTop.SuspendLayout();
            this.pCellPoolsTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitConnectionsStimuli)).BeginInit();
            this.splitConnectionsStimuli.Panel1.SuspendLayout();
            this.splitConnectionsStimuli.Panel2.SuspendLayout();
            this.splitConnectionsStimuli.SuspendLayout();
            this.pConnectionsTop.SuspendLayout();
            this.pStimuliTop.SuspendLayout();
            this.tGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pDrawing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawing)).BeginInit();
            this.tabModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileJson
            // 
            this.openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // saveFileJson
            // 
            this.saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // tJson
            // 
            this.tJson.Controls.Add(this.eModelJSON);
            this.tJson.Controls.Add(this.pModelJSONTop);
            this.tJson.Location = new System.Drawing.Point(4, 24);
            this.tJson.Name = "tJson";
            this.tJson.Size = new System.Drawing.Size(682, 555);
            this.tJson.TabIndex = 7;
            this.tJson.Text = "JSON File";
            this.tJson.UseVisualStyleBackColor = true;
            // 
            // eModelJSON
            // 
            this.eModelJSON.AcceptsTab = true;
            this.eModelJSON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eModelJSON.Location = new System.Drawing.Point(0, 40);
            this.eModelJSON.Name = "eModelJSON";
            this.eModelJSON.Size = new System.Drawing.Size(682, 515);
            this.eModelJSON.TabIndex = 5;
            this.eModelJSON.Text = "";
            this.eModelJSON.TextChanged += new System.EventHandler(this.eModelJSON_TextChanged);
            // 
            // pModelJSONTop
            // 
            this.pModelJSONTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pModelJSONTop.Controls.Add(this.linkOpenJsonViewer);
            this.pModelJSONTop.Controls.Add(this.pLineModelJSON);
            this.pModelJSONTop.Controls.Add(this.linkSaveModelJSON);
            this.pModelJSONTop.Controls.Add(this.btnLoadModelJSON);
            this.pModelJSONTop.Controls.Add(this.btnDisplayModelJSON);
            this.pModelJSONTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pModelJSONTop.Location = new System.Drawing.Point(0, 0);
            this.pModelJSONTop.Name = "pModelJSONTop";
            this.pModelJSONTop.Size = new System.Drawing.Size(682, 40);
            this.pModelJSONTop.TabIndex = 4;
            // 
            // linkOpenJsonViewer
            // 
            this.linkOpenJsonViewer.AutoSize = true;
            this.linkOpenJsonViewer.Location = new System.Drawing.Point(179, 13);
            this.linkOpenJsonViewer.Name = "linkOpenJsonViewer";
            this.linkOpenJsonViewer.Size = new System.Drawing.Size(118, 15);
            this.linkOpenJsonViewer.TabIndex = 26;
            this.linkOpenJsonViewer.TabStop = true;
            this.linkOpenJsonViewer.Text = "Open in JSON Viewer";
            this.linkOpenJsonViewer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOpenJsonViewer_LinkClicked);
            // 
            // pLineModelJSON
            // 
            this.pLineModelJSON.BackColor = System.Drawing.Color.LightGray;
            this.pLineModelJSON.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineModelJSON.Location = new System.Drawing.Point(0, 39);
            this.pLineModelJSON.Name = "pLineModelJSON";
            this.pLineModelJSON.Size = new System.Drawing.Size(682, 1);
            this.pLineModelJSON.TabIndex = 25;
            // 
            // linkSaveModelJSON
            // 
            this.linkSaveModelJSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSaveModelJSON.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveModelJSON.Location = new System.Drawing.Point(1188, 10);
            this.linkSaveModelJSON.Name = "linkSaveModelJSON";
            this.linkSaveModelJSON.Size = new System.Drawing.Size(68, 16);
            this.linkSaveModelJSON.TabIndex = 5;
            this.linkSaveModelJSON.TabStop = true;
            this.linkSaveModelJSON.Text = "Save Model";
            this.linkSaveModelJSON.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // tSettings
            // 
            this.tSettings.Controls.Add(this.propSettings);
            this.tSettings.Controls.Add(this.pSettingsTop);
            this.tSettings.Location = new System.Drawing.Point(4, 24);
            this.tSettings.Name = "tSettings";
            this.tSettings.Size = new System.Drawing.Size(682, 555);
            this.tSettings.TabIndex = 5;
            this.tSettings.Text = "Advanced Settings";
            this.tSettings.UseVisualStyleBackColor = true;
            this.tSettings.Visible = false;
            // 
            // propSettings
            // 
            this.propSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propSettings.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propSettings.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(212)))));
            this.propSettings.Location = new System.Drawing.Point(0, 70);
            this.propSettings.Name = "propSettings";
            this.propSettings.Size = new System.Drawing.Size(682, 485);
            this.propSettings.TabIndex = 0;
            // 
            // pSettingsTop
            // 
            this.pSettingsTop.Controls.Add(this.ddDefaultMuscleCellCore);
            this.pSettingsTop.Controls.Add(this.lDefaultMuscleCore);
            this.pSettingsTop.Controls.Add(this.ddDefaultNeuronCore);
            this.pSettingsTop.Controls.Add(this.lDefaultNeuronCore);
            this.pSettingsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSettingsTop.Location = new System.Drawing.Point(0, 0);
            this.pSettingsTop.Name = "pSettingsTop";
            this.pSettingsTop.Size = new System.Drawing.Size(682, 70);
            this.pSettingsTop.TabIndex = 1;
            // 
            // ddDefaultMuscleCellCore
            // 
            this.ddDefaultMuscleCellCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddDefaultMuscleCellCore.FormattingEnabled = true;
            this.ddDefaultMuscleCellCore.Location = new System.Drawing.Point(153, 32);
            this.ddDefaultMuscleCellCore.Name = "ddDefaultMuscleCellCore";
            this.ddDefaultMuscleCellCore.Size = new System.Drawing.Size(187, 23);
            this.ddDefaultMuscleCellCore.TabIndex = 3;
            this.ddDefaultMuscleCellCore.SelectedIndexChanged += new System.EventHandler(this.ddDefaultMuscleCore_SelectedIndexChanged);
            // 
            // lDefaultMuscleCore
            // 
            this.lDefaultMuscleCore.AutoSize = true;
            this.lDefaultMuscleCore.Location = new System.Drawing.Point(8, 35);
            this.lDefaultMuscleCore.Name = "lDefaultMuscleCore";
            this.lDefaultMuscleCore.Size = new System.Drawing.Size(137, 15);
            this.lDefaultMuscleCore.TabIndex = 2;
            this.lDefaultMuscleCore.Text = "Default Muscle Cell Core";
            // 
            // ddDefaultNeuronCore
            // 
            this.ddDefaultNeuronCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddDefaultNeuronCore.FormattingEnabled = true;
            this.ddDefaultNeuronCore.Location = new System.Drawing.Point(153, 5);
            this.ddDefaultNeuronCore.Name = "ddDefaultNeuronCore";
            this.ddDefaultNeuronCore.Size = new System.Drawing.Size(187, 23);
            this.ddDefaultNeuronCore.TabIndex = 1;
            this.ddDefaultNeuronCore.SelectedIndexChanged += new System.EventHandler(this.ddDefaultNeuronCore_SelectedIndexChanged);
            // 
            // lDefaultNeuronCore
            // 
            this.lDefaultNeuronCore.AutoSize = true;
            this.lDefaultNeuronCore.Location = new System.Drawing.Point(8, 8);
            this.lDefaultNeuronCore.Name = "lDefaultNeuronCore";
            this.lDefaultNeuronCore.Size = new System.Drawing.Size(116, 15);
            this.lDefaultNeuronCore.TabIndex = 0;
            this.lDefaultNeuronCore.Text = "Default Neuron Core";
            // 
            // tKinematics
            // 
            this.tKinematics.Controls.Add(this.propKinematics);
            this.tKinematics.Controls.Add(this.pKinematicsDescription);
            this.tKinematics.Location = new System.Drawing.Point(4, 24);
            this.tKinematics.Name = "tKinematics";
            this.tKinematics.Size = new System.Drawing.Size(682, 555);
            this.tKinematics.TabIndex = 6;
            this.tKinematics.Text = "Kinematics";
            this.tKinematics.UseVisualStyleBackColor = true;
            this.tKinematics.Visible = false;
            // 
            // propKinematics
            // 
            this.propKinematics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propKinematics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propKinematics.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propKinematics.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(212)))));
            this.propKinematics.Location = new System.Drawing.Point(0, 0);
            this.propKinematics.Name = "propKinematics";
            this.propKinematics.Size = new System.Drawing.Size(682, 515);
            this.propKinematics.TabIndex = 1;
            // 
            // pKinematicsDescription
            // 
            this.pKinematicsDescription.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pKinematicsDescription.Controls.Add(this.eKinematicsDescription);
            this.pKinematicsDescription.Controls.Add(this.pKinemSep);
            this.pKinematicsDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pKinematicsDescription.Location = new System.Drawing.Point(0, 515);
            this.pKinematicsDescription.Name = "pKinematicsDescription";
            this.pKinematicsDescription.Size = new System.Drawing.Size(682, 40);
            this.pKinematicsDescription.TabIndex = 7;
            // 
            // eKinematicsDescription
            // 
            this.eKinematicsDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.eKinematicsDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.eKinematicsDescription.Location = new System.Drawing.Point(0, 0);
            this.eKinematicsDescription.Multiline = true;
            this.eKinematicsDescription.Name = "eKinematicsDescription";
            this.eKinematicsDescription.ReadOnly = true;
            this.eKinematicsDescription.Size = new System.Drawing.Size(682, 39);
            this.eKinematicsDescription.TabIndex = 26;
            this.eKinematicsDescription.Text = "Kinematics parameters are used in generating the animations and motoneuron based " +
    "statistics.\r\nThey do not effect the how the model behaves and can be modified af" +
    "ter the simulation.";
            // 
            // pKinemSep
            // 
            this.pKinemSep.BackColor = System.Drawing.Color.LightGray;
            this.pKinemSep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pKinemSep.Location = new System.Drawing.Point(0, 39);
            this.pKinemSep.Name = "pKinemSep";
            this.pKinemSep.Size = new System.Drawing.Size(682, 1);
            this.pKinemSep.TabIndex = 25;
            // 
            // tArchitecture
            // 
            this.tArchitecture.BackColor = System.Drawing.Color.White;
            this.tArchitecture.Controls.Add(this.splitContainer2);
            this.tArchitecture.Location = new System.Drawing.Point(4, 24);
            this.tArchitecture.Name = "tArchitecture";
            this.tArchitecture.Size = new System.Drawing.Size(682, 555);
            this.tArchitecture.TabIndex = 1;
            this.tArchitecture.Text = "Architecture";
            this.tArchitecture.UseVisualStyleBackColor = true;
            this.tArchitecture.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitCellPools);
            this.splitContainer2.Panel1.Controls.Add(this.pCellPoolsTop);
            this.splitContainer2.Panel1MinSize = 200;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitConnectionsStimuli);
            this.splitContainer2.Panel2MinSize = 200;
            this.splitContainer2.Size = new System.Drawing.Size(682, 555);
            this.splitContainer2.SplitterDistance = 316;
            this.splitContainer2.TabIndex = 5;
            // 
            // splitCellPools
            // 
            this.splitCellPools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCellPools.Location = new System.Drawing.Point(0, 32);
            this.splitCellPools.Name = "splitCellPools";
            this.splitCellPools.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCellPools.Panel1
            // 
            this.splitCellPools.Panel1.Controls.Add(this.listCellPools);
            // 
            // splitCellPools.Panel2
            // 
            this.splitCellPools.Panel2.Controls.Add(this.listCells);
            this.splitCellPools.Panel2.Controls.Add(this.pCellsTop);
            this.splitCellPools.Size = new System.Drawing.Size(314, 521);
            this.splitCellPools.SplitterDistance = 258;
            this.splitCellPools.TabIndex = 8;
            // 
            // listCellPools
            // 
            this.listCellPools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCellPools.Location = new System.Drawing.Point(0, 0);
            this.listCellPools.Name = "listCellPools";
            this.listCellPools.Padding = new System.Windows.Forms.Padding(2);
            this.listCellPools.SelectedIndex = -1;
            this.listCellPools.SelectedItem = null;
            this.listCellPools.Size = new System.Drawing.Size(314, 258);
            this.listCellPools.TabIndex = 7;
            this.listCellPools.ItemAdd += new System.EventHandler(this.listCellPools_ItemAdd);
            this.listCellPools.ItemDelete += new System.EventHandler(this.listCellPools_ItemDelete);
            this.listCellPools.ItemCopy += new System.EventHandler(this.listCellPools_ItemCopy);
            this.listCellPools.ItemView += new System.EventHandler(this.listCellPools_ItemView);
            this.listCellPools.ItemToggleActive += new System.EventHandler(this.listCellPools_ItemToggleActive);
            this.listCellPools.ItemsSort += new System.EventHandler(this.listCellPools_ItemsSort);
            this.listCellPools.ItemSelect += new System.EventHandler(this.listCellPools_ItemSelect);
            this.listCellPools.ItemPlot += new System.EventHandler(this.listCellPools_ItemPlot);
            // 
            // listCells
            // 
            this.listCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCells.Location = new System.Drawing.Point(0, 32);
            this.listCells.Name = "listCells";
            this.listCells.Padding = new System.Windows.Forms.Padding(2);
            this.listCells.SelectedIndex = -1;
            this.listCells.SelectedItem = null;
            this.listCells.Size = new System.Drawing.Size(314, 227);
            this.listCells.TabIndex = 0;
            this.listCells.ItemAdd += new System.EventHandler(this.listCells_ItemAdd);
            this.listCells.ItemDelete += new System.EventHandler(this.listCells_ItemDelete);
            this.listCells.ItemCopy += new System.EventHandler(this.listCells_ItemCopy);
            this.listCells.ItemView += new System.EventHandler(this.listCells_ItemView);
            this.listCells.ItemToggleActive += new System.EventHandler(this.listCells_ItemToggleActive);
            this.listCells.ItemsSort += new System.EventHandler(this.listCells_ItemsSort);
            this.listCells.ItemSelect += new System.EventHandler(this.listCells_ItemSelect);
            this.listCells.ItemPlot += new System.EventHandler(this.listCells_ItemPlot);
            // 
            // pCellsTop
            // 
            this.pCellsTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.pCellsTop.Controls.Add(this.pCellsSep);
            this.pCellsTop.Controls.Add(this.lCellsTitle);
            this.pCellsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCellsTop.Location = new System.Drawing.Point(0, 0);
            this.pCellsTop.Name = "pCellsTop";
            this.pCellsTop.Size = new System.Drawing.Size(314, 32);
            this.pCellsTop.TabIndex = 7;
            // 
            // pCellsSep
            // 
            this.pCellsSep.BackColor = System.Drawing.Color.LightGray;
            this.pCellsSep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pCellsSep.Location = new System.Drawing.Point(0, 31);
            this.pCellsSep.Name = "pCellsSep";
            this.pCellsSep.Size = new System.Drawing.Size(314, 1);
            this.pCellsSep.TabIndex = 25;
            // 
            // lCellsTitle
            // 
            this.lCellsTitle.AutoSize = true;
            this.lCellsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lCellsTitle.Location = new System.Drawing.Point(8, 6);
            this.lCellsTitle.Name = "lCellsTitle";
            this.lCellsTitle.Size = new System.Drawing.Size(32, 15);
            this.lCellsTitle.TabIndex = 0;
            this.lCellsTitle.Text = "Cells";
            // 
            // pCellPoolsTop
            // 
            this.pCellPoolsTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.pCellPoolsTop.Controls.Add(this.pCellPoolsSep);
            this.pCellPoolsTop.Controls.Add(this.lCellPoolsTitle);
            this.pCellPoolsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCellPoolsTop.Location = new System.Drawing.Point(0, 0);
            this.pCellPoolsTop.Name = "pCellPoolsTop";
            this.pCellPoolsTop.Size = new System.Drawing.Size(314, 32);
            this.pCellPoolsTop.TabIndex = 6;
            // 
            // pCellPoolsSep
            // 
            this.pCellPoolsSep.BackColor = System.Drawing.Color.LightGray;
            this.pCellPoolsSep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pCellPoolsSep.Location = new System.Drawing.Point(0, 31);
            this.pCellPoolsSep.Name = "pCellPoolsSep";
            this.pCellPoolsSep.Size = new System.Drawing.Size(314, 1);
            this.pCellPoolsSep.TabIndex = 25;
            // 
            // lCellPoolsTitle
            // 
            this.lCellPoolsTitle.AutoSize = true;
            this.lCellPoolsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lCellPoolsTitle.Location = new System.Drawing.Point(8, 6);
            this.lCellPoolsTitle.Name = "lCellPoolsTitle";
            this.lCellPoolsTitle.Size = new System.Drawing.Size(59, 15);
            this.lCellPoolsTitle.TabIndex = 0;
            this.lCellPoolsTitle.Text = "Cell Pools";
            // 
            // splitConnectionsStimuli
            // 
            this.splitConnectionsStimuli.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConnectionsStimuli.Location = new System.Drawing.Point(0, 0);
            this.splitConnectionsStimuli.Name = "splitConnectionsStimuli";
            this.splitConnectionsStimuli.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConnectionsStimuli.Panel1
            // 
            this.splitConnectionsStimuli.Panel1.Controls.Add(this.listConnections);
            this.splitConnectionsStimuli.Panel1.Controls.Add(this.pConnectionsTop);
            // 
            // splitConnectionsStimuli.Panel2
            // 
            this.splitConnectionsStimuli.Panel2.Controls.Add(this.listStimuli);
            this.splitConnectionsStimuli.Panel2.Controls.Add(this.pStimuliTop);
            this.splitConnectionsStimuli.Size = new System.Drawing.Size(360, 553);
            this.splitConnectionsStimuli.SplitterDistance = 276;
            this.splitConnectionsStimuli.TabIndex = 9;
            // 
            // listConnections
            // 
            this.listConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listConnections.Location = new System.Drawing.Point(0, 26);
            this.listConnections.Name = "listConnections";
            this.listConnections.Padding = new System.Windows.Forms.Padding(2);
            this.listConnections.SelectedIndex = -1;
            this.listConnections.SelectedItem = null;
            this.listConnections.Size = new System.Drawing.Size(360, 250);
            this.listConnections.TabIndex = 8;
            this.listConnections.ItemAdd += new System.EventHandler(this.listConnections_ItemAdd);
            this.listConnections.ItemDelete += new System.EventHandler(this.listConnections_ItemDelete);
            this.listConnections.ItemCopy += new System.EventHandler(this.listConnections_ItemCopy);
            this.listConnections.ItemView += new System.EventHandler(this.listConnections_ItemView);
            this.listConnections.ItemToggleActive += new System.EventHandler(this.listConnections_ItemToggleActive);
            this.listConnections.ItemsSort += new System.EventHandler(this.listConnections_SortItems);
            this.listConnections.ItemSelect += new System.EventHandler(this.listConnections_ItemSelect);
            this.listConnections.ItemPlot += new System.EventHandler(this.listConnections_ItemPlot);
            // 
            // pConnectionsTop
            // 
            this.pConnectionsTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.pConnectionsTop.Controls.Add(this.pConnectionsSep);
            this.pConnectionsTop.Controls.Add(this.lConnectionsTitle);
            this.pConnectionsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pConnectionsTop.Location = new System.Drawing.Point(0, 0);
            this.pConnectionsTop.Name = "pConnectionsTop";
            this.pConnectionsTop.Size = new System.Drawing.Size(360, 26);
            this.pConnectionsTop.TabIndex = 7;
            // 
            // pConnectionsSep
            // 
            this.pConnectionsSep.BackColor = System.Drawing.Color.LightGray;
            this.pConnectionsSep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pConnectionsSep.Location = new System.Drawing.Point(0, 25);
            this.pConnectionsSep.Name = "pConnectionsSep";
            this.pConnectionsSep.Size = new System.Drawing.Size(360, 1);
            this.pConnectionsSep.TabIndex = 25;
            // 
            // lConnectionsTitle
            // 
            this.lConnectionsTitle.AutoSize = true;
            this.lConnectionsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lConnectionsTitle.Location = new System.Drawing.Point(8, 6);
            this.lConnectionsTitle.Name = "lConnectionsTitle";
            this.lConnectionsTitle.Size = new System.Drawing.Size(75, 15);
            this.lConnectionsTitle.TabIndex = 0;
            this.lConnectionsTitle.Text = "Connections";
            // 
            // listStimuli
            // 
            this.listStimuli.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listStimuli.Location = new System.Drawing.Point(0, 26);
            this.listStimuli.Name = "listStimuli";
            this.listStimuli.Padding = new System.Windows.Forms.Padding(2);
            this.listStimuli.SelectedIndex = -1;
            this.listStimuli.SelectedItem = null;
            this.listStimuli.Size = new System.Drawing.Size(360, 247);
            this.listStimuli.TabIndex = 0;
            this.listStimuli.ItemAdd += new System.EventHandler(this.listStimuli_ItemAdd);
            this.listStimuli.ItemDelete += new System.EventHandler(this.listStimuli_ItemDelete);
            this.listStimuli.ItemCopy += new System.EventHandler(this.listStimuli_ItemCopy);
            this.listStimuli.ItemView += new System.EventHandler(this.listStimuli_ItemView);
            this.listStimuli.ItemToggleActive += new System.EventHandler(this.listStimuli_ItemToggleActive);
            this.listStimuli.ItemsSort += new System.EventHandler(this.listStimuli_ItemsSort);
            this.listStimuli.ItemSelect += new System.EventHandler(this.listStimuli_ItemSelect);
            this.listStimuli.ItemPlot += new System.EventHandler(this.listStimuli_ItemPlot);
            // 
            // pStimuliTop
            // 
            this.pStimuliTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.pStimuliTop.Controls.Add(this.pStimuliSep);
            this.pStimuliTop.Controls.Add(this.lStimuliTitle);
            this.pStimuliTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pStimuliTop.Location = new System.Drawing.Point(0, 0);
            this.pStimuliTop.Name = "pStimuliTop";
            this.pStimuliTop.Size = new System.Drawing.Size(360, 26);
            this.pStimuliTop.TabIndex = 8;
            // 
            // pStimuliSep
            // 
            this.pStimuliSep.BackColor = System.Drawing.Color.LightGray;
            this.pStimuliSep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pStimuliSep.Location = new System.Drawing.Point(0, 25);
            this.pStimuliSep.Name = "pStimuliSep";
            this.pStimuliSep.Size = new System.Drawing.Size(360, 1);
            this.pStimuliSep.TabIndex = 25;
            // 
            // lStimuliTitle
            // 
            this.lStimuliTitle.AutoSize = true;
            this.lStimuliTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lStimuliTitle.Location = new System.Drawing.Point(8, 6);
            this.lStimuliTitle.Name = "lStimuliTitle";
            this.lStimuliTitle.Size = new System.Drawing.Size(46, 15);
            this.lStimuliTitle.TabIndex = 0;
            this.lStimuliTitle.Text = "Stimuli";
            // 
            // tGeneral
            // 
            this.tGeneral.BackColor = System.Drawing.Color.White;
            this.tGeneral.Controls.Add(this.splitContainer1);
            this.tGeneral.Controls.Add(this.eModelDescription);
            this.tGeneral.Controls.Add(this.eModelName);
            this.tGeneral.Controls.Add(this.lDescription);
            this.tGeneral.Controls.Add(this.lModelName);
            this.tGeneral.Location = new System.Drawing.Point(4, 24);
            this.tGeneral.Name = "tGeneral";
            this.tGeneral.Size = new System.Drawing.Size(682, 555);
            this.tGeneral.TabIndex = 4;
            this.tGeneral.Text = "General";
            this.tGeneral.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 77);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propModelDimensions);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pDrawing);
            this.splitContainer1.Size = new System.Drawing.Size(682, 478);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 61;
            // 
            // propModelDimensions
            // 
            this.propModelDimensions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propModelDimensions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propModelDimensions.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propModelDimensions.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(212)))));
            this.propModelDimensions.Location = new System.Drawing.Point(0, 0);
            this.propModelDimensions.Name = "propModelDimensions";
            this.propModelDimensions.Size = new System.Drawing.Size(225, 476);
            this.propModelDimensions.TabIndex = 60;
            // 
            // pDrawing
            // 
            this.pDrawing.AutoScroll = true;
            this.pDrawing.Controls.Add(this.pbDrawing);
            this.pDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDrawing.Location = new System.Drawing.Point(0, 0);
            this.pDrawing.Name = "pDrawing";
            this.pDrawing.Size = new System.Drawing.Size(449, 476);
            this.pDrawing.TabIndex = 15;
            // 
            // pbDrawing
            // 
            this.pbDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDrawing.Image = ((System.Drawing.Image)(resources.GetObject("pbDrawing.Image")));
            this.pbDrawing.Location = new System.Drawing.Point(0, 0);
            this.pbDrawing.Name = "pbDrawing";
            this.pbDrawing.Size = new System.Drawing.Size(449, 476);
            this.pbDrawing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDrawing.TabIndex = 12;
            this.pbDrawing.TabStop = false;
            // 
            // eModelDescription
            // 
            this.eModelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eModelDescription.Location = new System.Drawing.Point(106, 30);
            this.eModelDescription.Multiline = true;
            this.eModelDescription.Name = "eModelDescription";
            this.eModelDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eModelDescription.Size = new System.Drawing.Size(568, 41);
            this.eModelDescription.TabIndex = 7;
            // 
            // eModelName
            // 
            this.eModelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eModelName.Location = new System.Drawing.Point(106, 3);
            this.eModelName.Name = "eModelName";
            this.eModelName.Size = new System.Drawing.Size(568, 23);
            this.eModelName.TabIndex = 6;
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(8, 35);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(67, 15);
            this.lDescription.TabIndex = 1;
            this.lDescription.Text = "Description";
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
            // tabModel
            // 
            this.tabModel.Controls.Add(this.tGeneral);
            this.tabModel.Controls.Add(this.tArchitecture);
            this.tabModel.Controls.Add(this.tKinematics);
            this.tabModel.Controls.Add(this.tSettings);
            this.tabModel.Controls.Add(this.tJson);
            this.tabModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabModel.Location = new System.Drawing.Point(0, 0);
            this.tabModel.Name = "tabModel";
            this.tabModel.SelectedIndex = 0;
            this.tabModel.Size = new System.Drawing.Size(690, 583);
            this.tabModel.TabIndex = 2;
            this.tabModel.Tag = "";
            // 
            // ModelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabModel);
            this.Name = "ModelControl";
            this.Size = new System.Drawing.Size(690, 583);
            this.tJson.ResumeLayout(false);
            this.pModelJSONTop.ResumeLayout(false);
            this.pModelJSONTop.PerformLayout();
            this.tSettings.ResumeLayout(false);
            this.pSettingsTop.ResumeLayout(false);
            this.pSettingsTop.PerformLayout();
            this.tKinematics.ResumeLayout(false);
            this.pKinematicsDescription.ResumeLayout(false);
            this.pKinematicsDescription.PerformLayout();
            this.tArchitecture.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitCellPools.Panel1.ResumeLayout(false);
            this.splitCellPools.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCellPools)).EndInit();
            this.splitCellPools.ResumeLayout(false);
            this.pCellsTop.ResumeLayout(false);
            this.pCellsTop.PerformLayout();
            this.pCellPoolsTop.ResumeLayout(false);
            this.pCellPoolsTop.PerformLayout();
            this.splitConnectionsStimuli.Panel1.ResumeLayout(false);
            this.splitConnectionsStimuli.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConnectionsStimuli)).EndInit();
            this.splitConnectionsStimuli.ResumeLayout(false);
            this.pConnectionsTop.ResumeLayout(false);
            this.pConnectionsTop.PerformLayout();
            this.pStimuliTop.ResumeLayout(false);
            this.pStimuliTop.PerformLayout();
            this.tGeneral.ResumeLayout(false);
            this.tGeneral.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pDrawing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawing)).EndInit();
            this.tabModel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private OpenFileDialog openFileJson;
        private SaveFileDialog saveFileJson;
        private TabPage tJson;
        private RichTextBox eModelJSON;
        private Panel pModelJSONTop;
        private Panel pLineModelJSON;
        private LinkLabel linkSaveModelJSON;
        private Button btnLoadModelJSON;
        private Button btnDisplayModelJSON;
        private TabPage tSettings;
        private PropertyGrid propSettings;
        private Panel pSettingsTop;
        private ComboBox ddDefaultMuscleCellCore;
        private Label lDefaultMuscleCore;
        private ComboBox ddDefaultNeuronCore;
        private Label lDefaultNeuronCore;
        private TabPage tKinematics;
        private PropertyGrid propKinematics;
        private Panel pKinematicsDescription;
        private TextBox eKinematicsDescription;
        private Panel pKinemSep;
        private TabPage tArchitecture;
        private SplitContainer splitContainer2;
        private SplitContainer splitCellPools;
        private ListBoxControl listCellPools;
        private ListBoxControl listCells;
        private Panel pCellsTop;
        private Panel pCellsSep;
        private Label lCellsTitle;
        private Panel pCellPoolsTop;
        private Panel pCellPoolsSep;
        private Label lCellPoolsTitle;
        private SplitContainer splitConnectionsStimuli;
        private ListBoxControl listConnections;
        private Panel pConnectionsTop;
        private Panel pConnectionsSep;
        private Label lConnectionsTitle;
        private ListBoxControl listStimuli;
        private Panel pStimuliTop;
        private Panel pStimuliSep;
        private Label lStimuliTitle;
        private TabPage tGeneral;
        private SplitContainer splitContainer1;
        private PropertyGrid propModelDimensions;
        private Panel pDrawing;
        private PictureBox pbDrawing;
        private TextBox eModelDescription;
        private TextBox eModelName;
        private Label lDescription;
        private Label lModelName;
        private TabControl tabModel;
        private LinkLabel linkOpenJsonViewer;
    }
}
