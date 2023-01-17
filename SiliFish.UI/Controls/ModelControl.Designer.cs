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
            this.propModelDimensions = new System.Windows.Forms.PropertyGrid();
            this.pbDrawing = new System.Windows.Forms.PictureBox();
            this.pDrawing = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.eModelDescription = new System.Windows.Forms.TextBox();
            this.eModelName = new System.Windows.Forms.TextBox();
            this.lDescription = new System.Windows.Forms.Label();
            this.lModelName = new System.Windows.Forms.Label();
            this.tGeneral = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lCellPoolsTitle = new System.Windows.Forms.Label();
            this.pCellsTop = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lConnectionsTitle = new System.Windows.Forms.Label();
            this.pConnectionsTop = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitCellPools = new System.Windows.Forms.SplitContainer();
            this.listCellPools = new SiliFish.UI.Controls.ListBoxControl();
            this.listCells = new SiliFish.UI.Controls.ListBoxControl();
            this.listConnections = new SiliFish.UI.Controls.ListBoxControl();
            this.tCellPoolsJuncs = new System.Windows.Forms.TabPage();
            this.tStimuli = new System.Windows.Forms.TabPage();
            this.listStimuli = new SiliFish.UI.Controls.ListBoxControl();
            this.propKinematics = new System.Windows.Forms.PropertyGrid();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tKinematics = new System.Windows.Forms.TabPage();
            this.propSettings = new System.Windows.Forms.PropertyGrid();
            this.eTemporaryFolder = new System.Windows.Forms.TextBox();
            this.eOutputFolder = new System.Windows.Forms.TextBox();
            this.lTempFolder = new System.Windows.Forms.Label();
            this.lOutputFolder = new System.Windows.Forms.Label();
            this.ddDefaultMuscleCellCore = new System.Windows.Forms.ComboBox();
            this.lDefaultMuscleCore = new System.Windows.Forms.Label();
            this.ddDefaultNeuronCore = new System.Windows.Forms.ComboBox();
            this.lDefaultNeuronCore = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tSettings = new System.Windows.Forms.TabPage();
            this.tabModel = new System.Windows.Forms.TabControl();
            this.browseFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            this.saveFileJson = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawing)).BeginInit();
            this.pDrawing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tGeneral.SuspendLayout();
            this.pCellsTop.SuspendLayout();
            this.pConnectionsTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCellPools)).BeginInit();
            this.splitCellPools.Panel1.SuspendLayout();
            this.splitCellPools.Panel2.SuspendLayout();
            this.splitCellPools.SuspendLayout();
            this.tCellPoolsJuncs.SuspendLayout();
            this.tStimuli.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tKinematics.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tSettings.SuspendLayout();
            this.tabModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // propModelDimensions
            // 
            this.propModelDimensions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propModelDimensions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propModelDimensions.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.propModelDimensions.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.propModelDimensions.Location = new System.Drawing.Point(0, 0);
            this.propModelDimensions.Name = "propModelDimensions";
            this.propModelDimensions.Size = new System.Drawing.Size(225, 476);
            this.propModelDimensions.TabIndex = 60;
            // 
            // pbDrawing
            // 
            this.pbDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDrawing.Location = new System.Drawing.Point(0, 0);
            this.pbDrawing.Name = "pbDrawing";
            this.pbDrawing.Size = new System.Drawing.Size(449, 476);
            this.pbDrawing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDrawing.TabIndex = 12;
            this.pbDrawing.TabStop = false;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(315, 1);
            this.panel2.TabIndex = 25;
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
            // pCellsTop
            // 
            this.pCellsTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.pCellsTop.Controls.Add(this.panel2);
            this.pCellsTop.Controls.Add(this.lCellPoolsTitle);
            this.pCellsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCellsTop.Location = new System.Drawing.Point(0, 0);
            this.pCellsTop.Name = "pCellsTop";
            this.pCellsTop.Size = new System.Drawing.Size(315, 32);
            this.pCellsTop.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGray;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 31);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(359, 1);
            this.panel4.TabIndex = 25;
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
            // pConnectionsTop
            // 
            this.pConnectionsTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.pConnectionsTop.Controls.Add(this.panel4);
            this.pConnectionsTop.Controls.Add(this.lConnectionsTitle);
            this.pConnectionsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pConnectionsTop.Location = new System.Drawing.Point(0, 0);
            this.pConnectionsTop.Name = "pConnectionsTop";
            this.pConnectionsTop.Size = new System.Drawing.Size(359, 32);
            this.pConnectionsTop.TabIndex = 7;
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
            this.splitContainer2.Panel1.Controls.Add(this.pCellsTop);
            this.splitContainer2.Panel1MinSize = 200;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listConnections);
            this.splitContainer2.Panel2.Controls.Add(this.pConnectionsTop);
            this.splitContainer2.Panel2MinSize = 200;
            this.splitContainer2.Size = new System.Drawing.Size(682, 555);
            this.splitContainer2.SplitterDistance = 317;
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
            this.splitCellPools.Size = new System.Drawing.Size(315, 521);
            this.splitCellPools.SplitterDistance = 259;
            this.splitCellPools.TabIndex = 8;
            // 
            // listCellPools
            // 
            this.listCellPools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCellPools.Location = new System.Drawing.Point(0, 0);
            this.listCellPools.Name = "listCellPools";
            this.listCellPools.SelectedIndex = -1;
            this.listCellPools.SelectedItem = null;
            this.listCellPools.Size = new System.Drawing.Size(315, 259);
            this.listCellPools.TabIndex = 7;
            this.listCellPools.AddItem += new System.EventHandler(this.listCellPool_AddItem);
            this.listCellPools.DeleteItem += new System.EventHandler(this.listCellPool_DeleteItem);
            this.listCellPools.CopyItem += new System.EventHandler(this.listCellPool_CopyItem);
            this.listCellPools.ViewItem += new System.EventHandler(this.listCellPool_ViewItem);
            this.listCellPools.ActivateItem += new System.EventHandler(this.listCellPool_ActivateItem);
            this.listCellPools.SortItems += new System.EventHandler(this.listCellPool_SortItems);
            this.listCellPools.SelectItem += new System.EventHandler(this.listCellPools_SelectItem);
            // 
            // listCells
            // 
            this.listCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCells.Location = new System.Drawing.Point(0, 0);
            this.listCells.Name = "listCells";
            this.listCells.SelectedIndex = -1;
            this.listCells.SelectedItem = null;
            this.listCells.Size = new System.Drawing.Size(315, 258);
            this.listCells.TabIndex = 0;
            // 
            // listConnections
            // 
            this.listConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listConnections.Location = new System.Drawing.Point(0, 32);
            this.listConnections.Name = "listConnections";
            this.listConnections.SelectedIndex = -1;
            this.listConnections.SelectedItem = null;
            this.listConnections.Size = new System.Drawing.Size(359, 521);
            this.listConnections.TabIndex = 8;
            this.listConnections.AddItem += new System.EventHandler(this.listConnections_AddItem);
            this.listConnections.DeleteItem += new System.EventHandler(this.listConnections_DeleteItem);
            this.listConnections.CopyItem += new System.EventHandler(this.listConnections_CopyItem);
            this.listConnections.ViewItem += new System.EventHandler(this.listConnections_ViewItem);
            this.listConnections.ActivateItem += new System.EventHandler(this.listConnections_ActivateItem);
            this.listConnections.SortItems += new System.EventHandler(this.listConnections_SortItems);
            // 
            // tCellPoolsJuncs
            // 
            this.tCellPoolsJuncs.BackColor = System.Drawing.Color.White;
            this.tCellPoolsJuncs.Controls.Add(this.splitContainer2);
            this.tCellPoolsJuncs.Location = new System.Drawing.Point(4, 24);
            this.tCellPoolsJuncs.Name = "tCellPoolsJuncs";
            this.tCellPoolsJuncs.Size = new System.Drawing.Size(682, 555);
            this.tCellPoolsJuncs.TabIndex = 1;
            this.tCellPoolsJuncs.Text = "Cell Pools & Connections";
            this.tCellPoolsJuncs.UseVisualStyleBackColor = true;
            this.tCellPoolsJuncs.Visible = false;
            // 
            // tStimuli
            // 
            this.tStimuli.Controls.Add(this.listStimuli);
            this.tStimuli.Location = new System.Drawing.Point(4, 24);
            this.tStimuli.Name = "tStimuli";
            this.tStimuli.Size = new System.Drawing.Size(682, 555);
            this.tStimuli.TabIndex = 3;
            this.tStimuli.Text = "Stimuli";
            this.tStimuli.UseVisualStyleBackColor = true;
            this.tStimuli.Visible = false;
            // 
            // listStimuli
            // 
            this.listStimuli.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listStimuli.Location = new System.Drawing.Point(0, 0);
            this.listStimuli.Name = "listStimuli";
            this.listStimuli.SelectedIndex = -1;
            this.listStimuli.SelectedItem = null;
            this.listStimuli.Size = new System.Drawing.Size(682, 555);
            this.listStimuli.TabIndex = 0;
            this.listStimuli.AddItem += new System.EventHandler(this.listStimuli_AddItem);
            this.listStimuli.DeleteItem += new System.EventHandler(this.listStimuli_DeleteItem);
            this.listStimuli.CopyItem += new System.EventHandler(this.listStimuli_CopyItem);
            this.listStimuli.ViewItem += new System.EventHandler(this.listStimuli_ViewItem);
            this.listStimuli.ActivateItem += new System.EventHandler(this.listStimuli_ActivateItem);
            // 
            // propKinematics
            // 
            this.propKinematics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propKinematics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propKinematics.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.propKinematics.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.propKinematics.Location = new System.Drawing.Point(0, 0);
            this.propKinematics.Name = "propKinematics";
            this.propKinematics.Size = new System.Drawing.Size(682, 515);
            this.propKinematics.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(682, 39);
            this.textBox3.TabIndex = 26;
            this.textBox3.Text = "Kinematics parameters are used in generating the animations and motoneuron based " +
    "statistics.\r\nThey do not effect the how the model behaves and can be modified af" +
    "ter the simulation.";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightGray;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 39);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(682, 1);
            this.panel6.TabIndex = 25;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel7.Controls.Add(this.textBox3);
            this.panel7.Controls.Add(this.panel6);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 515);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(682, 40);
            this.panel7.TabIndex = 7;
            // 
            // tKinematics
            // 
            this.tKinematics.Controls.Add(this.propKinematics);
            this.tKinematics.Controls.Add(this.panel7);
            this.tKinematics.Location = new System.Drawing.Point(4, 24);
            this.tKinematics.Name = "tKinematics";
            this.tKinematics.Size = new System.Drawing.Size(682, 555);
            this.tKinematics.TabIndex = 6;
            this.tKinematics.Text = "Kinematics";
            this.tKinematics.UseVisualStyleBackColor = true;
            this.tKinematics.Visible = false;
            // 
            // propSettings
            // 
            this.propSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propSettings.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.propSettings.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.propSettings.Location = new System.Drawing.Point(0, 112);
            this.propSettings.Name = "propSettings";
            this.propSettings.Size = new System.Drawing.Size(682, 443);
            this.propSettings.TabIndex = 0;
            // 
            // eTemporaryFolder
            // 
            this.eTemporaryFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eTemporaryFolder.Location = new System.Drawing.Point(153, 84);
            this.eTemporaryFolder.Name = "eTemporaryFolder";
            this.eTemporaryFolder.ReadOnly = true;
            this.eTemporaryFolder.Size = new System.Drawing.Size(520, 23);
            this.eTemporaryFolder.TabIndex = 7;
            this.eTemporaryFolder.Click += new System.EventHandler(this.eTemporaryFolder_Click);
            // 
            // eOutputFolder
            // 
            this.eOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eOutputFolder.Location = new System.Drawing.Point(153, 58);
            this.eOutputFolder.Name = "eOutputFolder";
            this.eOutputFolder.ReadOnly = true;
            this.eOutputFolder.Size = new System.Drawing.Size(520, 23);
            this.eOutputFolder.TabIndex = 6;
            this.eOutputFolder.Click += new System.EventHandler(this.eOutputFolder_Click);
            // 
            // lTempFolder
            // 
            this.lTempFolder.AutoSize = true;
            this.lTempFolder.Location = new System.Drawing.Point(8, 88);
            this.lTempFolder.Name = "lTempFolder";
            this.lTempFolder.Size = new System.Drawing.Size(99, 15);
            this.lTempFolder.TabIndex = 5;
            this.lTempFolder.Text = "Temporary Folder";
            // 
            // lOutputFolder
            // 
            this.lOutputFolder.AutoSize = true;
            this.lOutputFolder.Location = new System.Drawing.Point(8, 61);
            this.lOutputFolder.Name = "lOutputFolder";
            this.lOutputFolder.Size = new System.Drawing.Size(81, 15);
            this.lOutputFolder.TabIndex = 4;
            this.lOutputFolder.Text = "Output Folder";
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
            // panel8
            // 
            this.panel8.Controls.Add(this.eTemporaryFolder);
            this.panel8.Controls.Add(this.eOutputFolder);
            this.panel8.Controls.Add(this.lTempFolder);
            this.panel8.Controls.Add(this.lOutputFolder);
            this.panel8.Controls.Add(this.ddDefaultMuscleCellCore);
            this.panel8.Controls.Add(this.lDefaultMuscleCore);
            this.panel8.Controls.Add(this.ddDefaultNeuronCore);
            this.panel8.Controls.Add(this.lDefaultNeuronCore);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(682, 112);
            this.panel8.TabIndex = 1;
            // 
            // tSettings
            // 
            this.tSettings.Controls.Add(this.propSettings);
            this.tSettings.Controls.Add(this.panel8);
            this.tSettings.Location = new System.Drawing.Point(4, 24);
            this.tSettings.Name = "tSettings";
            this.tSettings.Size = new System.Drawing.Size(682, 555);
            this.tSettings.TabIndex = 5;
            this.tSettings.Text = "Advanced Settings";
            this.tSettings.UseVisualStyleBackColor = true;
            this.tSettings.Visible = false;
            // 
            // tabModel
            // 
            this.tabModel.Controls.Add(this.tGeneral);
            this.tabModel.Controls.Add(this.tCellPoolsJuncs);
            this.tabModel.Controls.Add(this.tStimuli);
            this.tabModel.Controls.Add(this.tKinematics);
            this.tabModel.Controls.Add(this.tSettings);
            this.tabModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabModel.Location = new System.Drawing.Point(0, 0);
            this.tabModel.Name = "tabModel";
            this.tabModel.SelectedIndex = 0;
            this.tabModel.Size = new System.Drawing.Size(690, 583);
            this.tabModel.TabIndex = 2;
            this.tabModel.Tag = "";
            // 
            // openFileJson
            // 
            this.openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // saveFileJson
            // 
            this.saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // ModelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabModel);
            this.Name = "ModelControl";
            this.Size = new System.Drawing.Size(690, 583);
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawing)).EndInit();
            this.pDrawing.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tGeneral.ResumeLayout(false);
            this.tGeneral.PerformLayout();
            this.pCellsTop.ResumeLayout(false);
            this.pCellsTop.PerformLayout();
            this.pConnectionsTop.ResumeLayout(false);
            this.pConnectionsTop.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitCellPools.Panel1.ResumeLayout(false);
            this.splitCellPools.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCellPools)).EndInit();
            this.splitCellPools.ResumeLayout(false);
            this.tCellPoolsJuncs.ResumeLayout(false);
            this.tStimuli.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tKinematics.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tSettings.ResumeLayout(false);
            this.tabModel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private PropertyGrid propModelDimensions;
        private PictureBox pbDrawing;
        private Panel pDrawing;
        private SplitContainer splitContainer1;
        private TextBox eModelDescription;
        private TextBox eModelName;
        private Label lDescription;
        private Label lModelName;
        private TabPage tGeneral;
        private Panel panel2;
        private Label lCellPoolsTitle;
        private Panel pCellsTop;
        private Panel panel4;
        private Label lConnectionsTitle;
        private Panel pConnectionsTop;
        private SplitContainer splitContainer2;
        private TabPage tCellPoolsJuncs;
        private TabPage tStimuli;
        private PropertyGrid propKinematics;
        private TextBox textBox3;
        private Panel panel6;
        private Panel panel7;
        private TabPage tKinematics;
        private PropertyGrid propSettings;
        private TextBox eTemporaryFolder;
        private TextBox eOutputFolder;
        private Label lTempFolder;
        private Label lOutputFolder;
        private ComboBox ddDefaultMuscleCellCore;
        private Label lDefaultMuscleCore;
        private ComboBox ddDefaultNeuronCore;
        private Label lDefaultNeuronCore;
        private Panel panel8;
        private TabPage tSettings; 
        private TabControl tabModel;
        private FolderBrowserDialog browseFolder;
        private OpenFileDialog openFileJson;
        private SaveFileDialog saveFileJson;
        private SplitContainer splitCellPools;
        private ListBoxControl listCellPools;
        private ListBoxControl listCells;
        private ListBoxControl listConnections;
        private ListBoxControl listStimuli;
    }
}
