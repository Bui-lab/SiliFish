﻿namespace SiliFish.UI.Controls
{
    partial class InterPoolTemplateControl
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lSourcePool = new Label();
            lTargetPool = new Label();
            lAxonReachMode = new Label();
            lJunctionType = new Label();
            lMinReach = new Label();
            lFixedDuration = new Label();
            lSynDelay = new Label();
            ddSourcePool = new ComboBox();
            ddTargetPool = new ComboBox();
            ddAxonReachMode = new ComboBox();
            ddJunctionType = new ComboBox();
            toolTip1 = new ToolTip(components);
            eFixedDuration = new TextBox();
            lMaxReach = new Label();
            lMaxIncoming = new Label();
            numMaxIncoming = new NumericUpDown();
            lMaxOutgoing = new Label();
            numMaxOutgoing = new NumericUpDown();
            numMinAscReach = new NumericUpDown();
            numMaxAscReach = new NumericUpDown();
            numMaxDescReach = new NumericUpDown();
            numMinDescReach = new NumericUpDown();
            numProbability = new NumericUpDown();
            lProbability = new Label();
            eSynDelay = new TextBox();
            cbActive = new CheckBox();
            lName = new Label();
            lDescription = new Label();
            eName = new TextBox();
            ddDistanceMode = new ComboBox();
            lDistanceMode = new Label();
            eDescription = new RichTextBox();
            grReach = new GroupBox();
            ddReachMode = new ComboBox();
            lReachMode = new Label();
            cbDescending = new CheckBox();
            cbAscending = new CheckBox();
            lUoD2 = new Label();
            lUoD1 = new Label();
            splitContainerMain = new SplitContainer();
            ddCoreType = new ComboBox();
            lCoreType = new Label();
            tabInterPool = new TabControl();
            tDynamics = new TabPage();
            dgDynamics = new DistributionDataGrid();
            tConductanceMatrix = new TabPage();
            dgConductanceMatrix = new DataGridView();
            panel2 = new Panel();
            linkSaveMatrix = new LinkLabel();
            linkLoadMatrix = new LinkLabel();
            panel1 = new Panel();
            lCondMatrixWarning = new Label();
            tTimeline = new TabPage();
            timeLineControl = new TimeLineControl();
            saveFileCSV = new SaveFileDialog();
            openFileCSV = new OpenFileDialog();
            linkClearMatrix = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)numMaxIncoming).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxOutgoing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinAscReach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxAscReach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxDescReach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinDescReach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numProbability).BeginInit();
            grReach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            tabInterPool.SuspendLayout();
            tDynamics.SuspendLayout();
            tConductanceMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgConductanceMatrix).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            tTimeline.SuspendLayout();
            SuspendLayout();
            // 
            // lSourcePool
            // 
            lSourcePool.AutoSize = true;
            lSourcePool.Location = new Point(3, 9);
            lSourcePool.Name = "lSourcePool";
            lSourcePool.Size = new Size(70, 15);
            lSourcePool.TabIndex = 4;
            lSourcePool.Text = "Source Pool";
            // 
            // lTargetPool
            // 
            lTargetPool.AutoSize = true;
            lTargetPool.Location = new Point(3, 35);
            lTargetPool.Name = "lTargetPool";
            lTargetPool.Size = new Size(67, 15);
            lTargetPool.TabIndex = 6;
            lTargetPool.Text = "Target Pool";
            // 
            // lAxonReachMode
            // 
            lAxonReachMode.AutoSize = true;
            lAxonReachMode.Location = new Point(3, 61);
            lAxonReachMode.Name = "lAxonReachMode";
            lAxonReachMode.Size = new Size(69, 15);
            lAxonReachMode.TabIndex = 8;
            lAxonReachMode.Text = "Axon Reach";
            // 
            // lJunctionType
            // 
            lJunctionType.AutoSize = true;
            lJunctionType.Location = new Point(3, 87);
            lJunctionType.Name = "lJunctionType";
            lJunctionType.Size = new Size(80, 15);
            lJunctionType.TabIndex = 10;
            lJunctionType.Text = "Junction Type";
            // 
            // lMinReach
            // 
            lMinReach.AutoSize = true;
            lMinReach.Location = new Point(100, 78);
            lMinReach.Name = "lMinReach";
            lMinReach.Size = new Size(28, 15);
            lMinReach.TabIndex = 2;
            lMinReach.Text = "Min";
            toolTip1.SetToolTip(lMinReach, "The distance in 3-D, calculated according to the selected 'distance mode'");
            // 
            // lFixedDuration
            // 
            lFixedDuration.AutoSize = true;
            lFixedDuration.Location = new Point(3, 217);
            lFixedDuration.Name = "lFixedDuration";
            lFixedDuration.Size = new Size(59, 15);
            lFixedDuration.TabIndex = 20;
            lFixedDuration.Text = "Fixed Dur.";
            toolTip1.SetToolTip(lFixedDuration, "If entered, distance/speed will not be used to calculate duration.");
            // 
            // lSynDelay
            // 
            lSynDelay.AutoSize = true;
            lSynDelay.Location = new Point(3, 243);
            lSynDelay.Name = "lSynDelay";
            lSynDelay.Size = new Size(61, 15);
            lSynDelay.TabIndex = 22;
            lSynDelay.Text = "Syn. Delay";
            toolTip1.SetToolTip(lSynDelay, "If entered, will replace to the model's generic synaptic delay.");
            // 
            // ddSourcePool
            // 
            ddSourcePool.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddSourcePool.BackColor = Color.WhiteSmoke;
            ddSourcePool.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSourcePool.FlatStyle = FlatStyle.Flat;
            ddSourcePool.FormattingEnabled = true;
            ddSourcePool.Location = new Point(97, 6);
            ddSourcePool.Name = "ddSourcePool";
            ddSourcePool.Size = new Size(145, 23);
            ddSourcePool.TabIndex = 5;
            ddSourcePool.SelectedIndexChanged += ddSourcePool_SelectedIndexChanged;
            // 
            // ddTargetPool
            // 
            ddTargetPool.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddTargetPool.BackColor = Color.WhiteSmoke;
            ddTargetPool.DropDownStyle = ComboBoxStyle.DropDownList;
            ddTargetPool.FlatStyle = FlatStyle.Flat;
            ddTargetPool.FormattingEnabled = true;
            ddTargetPool.Location = new Point(97, 32);
            ddTargetPool.Name = "ddTargetPool";
            ddTargetPool.Size = new Size(145, 23);
            ddTargetPool.TabIndex = 7;
            ddTargetPool.SelectedIndexChanged += ddTargetPool_SelectedIndexChanged;
            // 
            // ddAxonReachMode
            // 
            ddAxonReachMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddAxonReachMode.BackColor = Color.WhiteSmoke;
            ddAxonReachMode.DropDownStyle = ComboBoxStyle.DropDownList;
            ddAxonReachMode.FlatStyle = FlatStyle.Flat;
            ddAxonReachMode.FormattingEnabled = true;
            ddAxonReachMode.Location = new Point(97, 58);
            ddAxonReachMode.Name = "ddAxonReachMode";
            ddAxonReachMode.Size = new Size(145, 23);
            ddAxonReachMode.TabIndex = 9;
            ddAxonReachMode.SelectedIndexChanged += ddAxonReachMode_SelectedIndexChanged;
            // 
            // ddJunctionType
            // 
            ddJunctionType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddJunctionType.BackColor = Color.WhiteSmoke;
            ddJunctionType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddJunctionType.FlatStyle = FlatStyle.Flat;
            ddJunctionType.FormattingEnabled = true;
            ddJunctionType.Items.AddRange(new object[] { "Synapse", "Gap", "NMJ" });
            ddJunctionType.Location = new Point(97, 84);
            ddJunctionType.Name = "ddJunctionType";
            ddJunctionType.Size = new Size(145, 23);
            ddJunctionType.TabIndex = 11;
            ddJunctionType.SelectedIndexChanged += ddJunctionType_SelectedIndexChanged;
            // 
            // eFixedDuration
            // 
            eFixedDuration.Location = new Point(97, 214);
            eFixedDuration.Name = "eFixedDuration";
            eFixedDuration.Size = new Size(77, 23);
            eFixedDuration.TabIndex = 21;
            toolTip1.SetToolTip(eFixedDuration, "If entered, distance/speed will not be used to calculate duration.\r\n");
            // 
            // lMaxReach
            // 
            lMaxReach.AutoSize = true;
            lMaxReach.Location = new Point(155, 78);
            lMaxReach.Name = "lMaxReach";
            lMaxReach.Size = new Size(29, 15);
            lMaxReach.TabIndex = 4;
            lMaxReach.Text = "Max";
            toolTip1.SetToolTip(lMaxReach, "The distance in 3-D, calculated according to the selected 'distance mode'");
            // 
            // lMaxIncoming
            // 
            lMaxIncoming.AutoSize = true;
            lMaxIncoming.Location = new Point(3, 139);
            lMaxIncoming.Name = "lMaxIncoming";
            lMaxIncoming.Size = new Size(83, 15);
            lMaxIncoming.TabIndex = 12;
            lMaxIncoming.Text = "Max Incoming";
            toolTip1.SetToolTip(lMaxIncoming, "The number of maximum junctions/synapses the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMaxIncoming
            // 
            numMaxIncoming.Location = new Point(97, 136);
            numMaxIncoming.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxIncoming.Name = "numMaxIncoming";
            numMaxIncoming.Size = new Size(77, 23);
            numMaxIncoming.TabIndex = 13;
            toolTip1.SetToolTip(numMaxIncoming, "The number of maximum junctions/synapses the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // lMaxOutgoing
            // 
            lMaxOutgoing.AutoSize = true;
            lMaxOutgoing.Location = new Point(3, 165);
            lMaxOutgoing.Name = "lMaxOutgoing";
            lMaxOutgoing.Size = new Size(83, 15);
            lMaxOutgoing.TabIndex = 14;
            lMaxOutgoing.Text = "Max Outgoing";
            toolTip1.SetToolTip(lMaxOutgoing, "The number of maximum junctions/synapses the source cell can have to the same target pool.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMaxOutgoing
            // 
            numMaxOutgoing.Location = new Point(97, 162);
            numMaxOutgoing.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxOutgoing.Name = "numMaxOutgoing";
            numMaxOutgoing.Size = new Size(77, 23);
            numMaxOutgoing.TabIndex = 15;
            toolTip1.SetToolTip(numMaxOutgoing, "The number of maximum junctions/synapses the source cell can have to the same target pool.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMinAscReach
            // 
            numMinAscReach.Location = new Point(100, 95);
            numMinAscReach.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMinAscReach.Name = "numMinAscReach";
            numMinAscReach.Size = new Size(50, 23);
            numMinAscReach.TabIndex = 14;
            toolTip1.SetToolTip(numMinAscReach, "The number of maximum junctions/synapses the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // numMaxAscReach
            // 
            numMaxAscReach.Location = new Point(155, 95);
            numMaxAscReach.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxAscReach.Name = "numMaxAscReach";
            numMaxAscReach.Size = new Size(50, 23);
            numMaxAscReach.TabIndex = 15;
            toolTip1.SetToolTip(numMaxAscReach, "The number of maximum junctions/synapses the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // numMaxDescReach
            // 
            numMaxDescReach.Location = new Point(155, 122);
            numMaxDescReach.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxDescReach.Name = "numMaxDescReach";
            numMaxDescReach.Size = new Size(50, 23);
            numMaxDescReach.TabIndex = 17;
            toolTip1.SetToolTip(numMaxDescReach, "The number of maximum junctions/synapses the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // numMinDescReach
            // 
            numMinDescReach.Location = new Point(100, 122);
            numMinDescReach.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMinDescReach.Name = "numMinDescReach";
            numMinDescReach.Size = new Size(50, 23);
            numMinDescReach.TabIndex = 16;
            toolTip1.SetToolTip(numMinDescReach, "The number of maximum junctions/synapses the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // numProbability
            // 
            numProbability.DecimalPlaces = 3;
            numProbability.Location = new Point(97, 188);
            numProbability.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numProbability.Name = "numProbability";
            numProbability.Size = new Size(77, 23);
            numProbability.TabIndex = 17;
            toolTip1.SetToolTip(numProbability, "The probability of whether there will be a junction/synapse between the source and target cell.");
            numProbability.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lProbability
            // 
            lProbability.AutoSize = true;
            lProbability.Location = new Point(3, 191);
            lProbability.Name = "lProbability";
            lProbability.Size = new Size(64, 15);
            lProbability.TabIndex = 16;
            lProbability.Text = "Probability";
            toolTip1.SetToolTip(lProbability, "The probability of whether there will be a junction/synapse between the source and target cell.");
            // 
            // eSynDelay
            // 
            eSynDelay.Location = new Point(97, 240);
            eSynDelay.Name = "eSynDelay";
            eSynDelay.Size = new Size(77, 23);
            eSynDelay.TabIndex = 22;
            toolTip1.SetToolTip(eSynDelay, "If entered, distance/speed will not be used to calculate duration.\r\n");
            // 
            // cbActive
            // 
            cbActive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbActive.AutoSize = true;
            cbActive.Checked = true;
            cbActive.CheckState = CheckState.Checked;
            cbActive.Location = new Point(184, 294);
            cbActive.Name = "cbActive";
            cbActive.Size = new Size(59, 19);
            cbActive.TabIndex = 0;
            cbActive.Text = "Active";
            cbActive.UseVisualStyleBackColor = true;
            // 
            // lName
            // 
            lName.AutoSize = true;
            lName.Location = new Point(3, 269);
            lName.Name = "lName";
            lName.Size = new Size(39, 15);
            lName.TabIndex = 24;
            lName.Text = "Name";
            // 
            // lDescription
            // 
            lDescription.AutoSize = true;
            lDescription.Location = new Point(3, 295);
            lDescription.Name = "lDescription";
            lDescription.Size = new Size(67, 15);
            lDescription.TabIndex = 26;
            lDescription.Text = "Description";
            // 
            // eName
            // 
            eName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eName.Location = new Point(97, 266);
            eName.Name = "eName";
            eName.Size = new Size(145, 23);
            eName.TabIndex = 25;
            eName.Leave += eName_Leave;
            // 
            // ddDistanceMode
            // 
            ddDistanceMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddDistanceMode.BackColor = Color.WhiteSmoke;
            ddDistanceMode.DropDownStyle = ComboBoxStyle.DropDownList;
            ddDistanceMode.FlatStyle = FlatStyle.Flat;
            ddDistanceMode.FormattingEnabled = true;
            ddDistanceMode.Location = new Point(100, 18);
            ddDistanceMode.Name = "ddDistanceMode";
            ddDistanceMode.Size = new Size(140, 23);
            ddDistanceMode.TabIndex = 1;
            // 
            // lDistanceMode
            // 
            lDistanceMode.AutoSize = true;
            lDistanceMode.Location = new Point(4, 22);
            lDistanceMode.Name = "lDistanceMode";
            lDistanceMode.Size = new Size(86, 15);
            lDistanceMode.TabIndex = 0;
            lDistanceMode.Text = "Distance Mode";
            // 
            // eDescription
            // 
            eDescription.AcceptsTab = true;
            eDescription.Dock = DockStyle.Bottom;
            eDescription.Location = new Point(0, 319);
            eDescription.Name = "eDescription";
            eDescription.Size = new Size(246, 240);
            eDescription.TabIndex = 27;
            eDescription.Text = "";
            // 
            // grReach
            // 
            grReach.Controls.Add(ddReachMode);
            grReach.Controls.Add(lReachMode);
            grReach.Controls.Add(cbDescending);
            grReach.Controls.Add(cbAscending);
            grReach.Controls.Add(lUoD2);
            grReach.Controls.Add(lUoD1);
            grReach.Controls.Add(numMaxDescReach);
            grReach.Controls.Add(numMinDescReach);
            grReach.Controls.Add(numMaxAscReach);
            grReach.Controls.Add(numMinAscReach);
            grReach.Controls.Add(ddDistanceMode);
            grReach.Controls.Add(lDistanceMode);
            grReach.Controls.Add(lMinReach);
            grReach.Controls.Add(lMaxReach);
            grReach.Dock = DockStyle.Top;
            grReach.Location = new Point(3, 3);
            grReach.Name = "grReach";
            grReach.Size = new Size(246, 153);
            grReach.TabIndex = 1;
            grReach.TabStop = false;
            grReach.Text = "Reach";
            // 
            // ddReachMode
            // 
            ddReachMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddReachMode.BackColor = Color.WhiteSmoke;
            ddReachMode.DropDownStyle = ComboBoxStyle.DropDownList;
            ddReachMode.FlatStyle = FlatStyle.Flat;
            ddReachMode.FormattingEnabled = true;
            ddReachMode.Items.AddRange(new object[] { "# of somites", "unit of length" });
            ddReachMode.Location = new Point(100, 47);
            ddReachMode.Name = "ddReachMode";
            ddReachMode.Size = new Size(140, 23);
            ddReachMode.TabIndex = 28;
            ddReachMode.SelectedIndexChanged += ddReachMode_SelectedIndexChanged;
            // 
            // lReachMode
            // 
            lReachMode.AutoSize = true;
            lReachMode.Location = new Point(4, 51);
            lReachMode.Name = "lReachMode";
            lReachMode.Size = new Size(73, 15);
            lReachMode.TabIndex = 27;
            lReachMode.Text = "Reach Mode";
            // 
            // cbDescending
            // 
            cbDescending.AutoSize = true;
            cbDescending.Checked = true;
            cbDescending.CheckState = CheckState.Checked;
            cbDescending.Location = new Point(8, 122);
            cbDescending.Name = "cbDescending";
            cbDescending.Size = new Size(88, 19);
            cbDescending.TabIndex = 26;
            cbDescending.Text = "Descending";
            cbDescending.UseVisualStyleBackColor = true;
            cbDescending.CheckedChanged += cbDescending_CheckedChanged;
            // 
            // cbAscending
            // 
            cbAscending.AutoSize = true;
            cbAscending.Checked = true;
            cbAscending.CheckState = CheckState.Checked;
            cbAscending.Location = new Point(8, 99);
            cbAscending.Name = "cbAscending";
            cbAscending.Size = new Size(82, 19);
            cbAscending.TabIndex = 25;
            cbAscending.Text = "Ascending";
            cbAscending.UseVisualStyleBackColor = true;
            cbAscending.CheckedChanged += cbAscending_CheckedChanged;
            // 
            // lUoD2
            // 
            lUoD2.AutoSize = true;
            lUoD2.Location = new Point(211, 124);
            lUoD2.Name = "lUoD2";
            lUoD2.Size = new Size(48, 15);
            lUoD2.TabIndex = 24;
            lUoD2.Text = "somites";
            // 
            // lUoD1
            // 
            lUoD1.AutoSize = true;
            lUoD1.Location = new Point(211, 100);
            lUoD1.Name = "lUoD1";
            lUoD1.Size = new Size(48, 15);
            lUoD1.TabIndex = 23;
            lUoD1.Text = "somites";
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(0, 0);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(ddCoreType);
            splitContainerMain.Panel1.Controls.Add(eSynDelay);
            splitContainerMain.Panel1.Controls.Add(lCoreType);
            splitContainerMain.Panel1.Controls.Add(lSourcePool);
            splitContainerMain.Panel1.Controls.Add(lMaxOutgoing);
            splitContainerMain.Panel1.Controls.Add(lTargetPool);
            splitContainerMain.Panel1.Controls.Add(numMaxOutgoing);
            splitContainerMain.Panel1.Controls.Add(lAxonReachMode);
            splitContainerMain.Panel1.Controls.Add(lMaxIncoming);
            splitContainerMain.Panel1.Controls.Add(lJunctionType);
            splitContainerMain.Panel1.Controls.Add(numMaxIncoming);
            splitContainerMain.Panel1.Controls.Add(eDescription);
            splitContainerMain.Panel1.Controls.Add(lFixedDuration);
            splitContainerMain.Panel1.Controls.Add(eName);
            splitContainerMain.Panel1.Controls.Add(lSynDelay);
            splitContainerMain.Panel1.Controls.Add(lDescription);
            splitContainerMain.Panel1.Controls.Add(ddSourcePool);
            splitContainerMain.Panel1.Controls.Add(lName);
            splitContainerMain.Panel1.Controls.Add(ddTargetPool);
            splitContainerMain.Panel1.Controls.Add(lProbability);
            splitContainerMain.Panel1.Controls.Add(ddAxonReachMode);
            splitContainerMain.Panel1.Controls.Add(numProbability);
            splitContainerMain.Panel1.Controls.Add(ddJunctionType);
            splitContainerMain.Panel1.Controls.Add(cbActive);
            splitContainerMain.Panel1.Controls.Add(eFixedDuration);
            splitContainerMain.Panel1.SizeChanged += splitContainerMain_Panel1_SizeChanged;
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(tabInterPool);
            splitContainerMain.Size = new Size(510, 559);
            splitContainerMain.SplitterDistance = 246;
            splitContainerMain.TabIndex = 28;
            splitContainerMain.SplitterMoved += splitContainerMain_SplitterMoved;
            // 
            // ddCoreType
            // 
            ddCoreType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCoreType.BackColor = Color.WhiteSmoke;
            ddCoreType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCoreType.FlatStyle = FlatStyle.Flat;
            ddCoreType.FormattingEnabled = true;
            ddCoreType.Location = new Point(97, 110);
            ddCoreType.Name = "ddCoreType";
            ddCoreType.Size = new Size(146, 23);
            ddCoreType.TabIndex = 12;
            ddCoreType.SelectedIndexChanged += ddCoreType_SelectedIndexChanged;
            // 
            // lCoreType
            // 
            lCoreType.AutoSize = true;
            lCoreType.Location = new Point(3, 113);
            lCoreType.Name = "lCoreType";
            lCoreType.Size = new Size(60, 15);
            lCoreType.TabIndex = 6;
            lCoreType.Text = "Core Type";
            // 
            // tabInterPool
            // 
            tabInterPool.Controls.Add(tDynamics);
            tabInterPool.Controls.Add(tConductanceMatrix);
            tabInterPool.Controls.Add(tTimeline);
            tabInterPool.Dock = DockStyle.Fill;
            tabInterPool.Location = new Point(0, 0);
            tabInterPool.Name = "tabInterPool";
            tabInterPool.SelectedIndex = 0;
            tabInterPool.Size = new Size(260, 559);
            tabInterPool.TabIndex = 0;
            // 
            // tDynamics
            // 
            tDynamics.Controls.Add(dgDynamics);
            tDynamics.Controls.Add(grReach);
            tDynamics.Location = new Point(4, 24);
            tDynamics.Name = "tDynamics";
            tDynamics.Padding = new Padding(3);
            tDynamics.Size = new Size(252, 531);
            tDynamics.TabIndex = 0;
            tDynamics.Text = "Dynamics";
            tDynamics.UseVisualStyleBackColor = true;
            // 
            // dgDynamics
            // 
            dgDynamics.AutoSize = true;
            dgDynamics.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dgDynamics.BackColor = Color.WhiteSmoke;
            dgDynamics.Dock = DockStyle.Fill;
            dgDynamics.Location = new Point(3, 156);
            dgDynamics.MinimumSize = new Size(100, 100);
            dgDynamics.Name = "dgDynamics";
            dgDynamics.Size = new Size(246, 372);
            dgDynamics.TabIndex = 4;
            dgDynamics.Leave += dgDynamics_Leave;
            // 
            // tConductanceMatrix
            // 
            tConductanceMatrix.Controls.Add(dgConductanceMatrix);
            tConductanceMatrix.Controls.Add(panel2);
            tConductanceMatrix.Controls.Add(panel1);
            tConductanceMatrix.Location = new Point(4, 24);
            tConductanceMatrix.Name = "tConductanceMatrix";
            tConductanceMatrix.Padding = new Padding(3);
            tConductanceMatrix.Size = new Size(252, 531);
            tConductanceMatrix.TabIndex = 2;
            tConductanceMatrix.Text = "Conductance Matrix";
            tConductanceMatrix.UseVisualStyleBackColor = true;
            // 
            // dgConductanceMatrix
            // 
            dgConductanceMatrix.AllowUserToAddRows = false;
            dgConductanceMatrix.AllowUserToDeleteRows = false;
            dgConductanceMatrix.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "N4";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgConductanceMatrix.DefaultCellStyle = dataGridViewCellStyle2;
            dgConductanceMatrix.Dock = DockStyle.Fill;
            dgConductanceMatrix.Location = new Point(3, 41);
            dgConductanceMatrix.Name = "dgConductanceMatrix";
            dgConductanceMatrix.RowHeadersWidth = 100;
            dgConductanceMatrix.Size = new Size(246, 452);
            dgConductanceMatrix.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(linkClearMatrix);
            panel2.Controls.Add(linkSaveMatrix);
            panel2.Controls.Add(linkLoadMatrix);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(3, 493);
            panel2.Name = "panel2";
            panel2.Size = new Size(246, 35);
            panel2.TabIndex = 2;
            // 
            // linkSaveMatrix
            // 
            linkSaveMatrix.AutoSize = true;
            linkSaveMatrix.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveMatrix.Location = new Point(80, 10);
            linkSaveMatrix.Name = "linkSaveMatrix";
            linkSaveMatrix.Size = new Size(67, 15);
            linkSaveMatrix.TabIndex = 11;
            linkSaveMatrix.TabStop = true;
            linkSaveMatrix.Text = "Save Matrix";
            linkSaveMatrix.LinkClicked += linkSaveMatrix_LinkClicked;
            // 
            // linkLoadMatrix
            // 
            linkLoadMatrix.AutoSize = true;
            linkLoadMatrix.LinkColor = Color.FromArgb(64, 64, 64);
            linkLoadMatrix.Location = new Point(4, 10);
            linkLoadMatrix.Name = "linkLoadMatrix";
            linkLoadMatrix.Size = new Size(69, 15);
            linkLoadMatrix.TabIndex = 10;
            linkLoadMatrix.TabStop = true;
            linkLoadMatrix.Text = "Load Matrix";
            linkLoadMatrix.LinkClicked += linkLoadMatrix_LinkClicked;
            // 
            // panel1
            // 
            panel1.Controls.Add(lCondMatrixWarning);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(246, 38);
            panel1.TabIndex = 1;
            // 
            // lCondMatrixWarning
            // 
            lCondMatrixWarning.Dock = DockStyle.Fill;
            lCondMatrixWarning.Location = new Point(0, 0);
            lCondMatrixWarning.Name = "lCondMatrixWarning";
            lCondMatrixWarning.Size = new Size(246, 38);
            lCondMatrixWarning.TabIndex = 0;
            lCondMatrixWarning.Text = "Values entered in this matrix will override the conductance values in the dynamics tab.";
            // 
            // tTimeline
            // 
            tTimeline.Controls.Add(timeLineControl);
            tTimeline.Location = new Point(4, 24);
            tTimeline.Name = "tTimeline";
            tTimeline.Padding = new Padding(3);
            tTimeline.Size = new Size(252, 531);
            tTimeline.TabIndex = 1;
            tTimeline.Text = "Timeline";
            tTimeline.UseVisualStyleBackColor = true;
            // 
            // timeLineControl
            // 
            timeLineControl.AutoScroll = true;
            timeLineControl.BackColor = Color.FromArgb(236, 239, 241);
            timeLineControl.Dock = DockStyle.Fill;
            timeLineControl.Location = new Point(3, 3);
            timeLineControl.Name = "timeLineControl";
            timeLineControl.Size = new Size(246, 525);
            timeLineControl.TabIndex = 24;
            // 
            // saveFileCSV
            // 
            saveFileCSV.Filter = "CSV files(*.csv)|*.csv";
            // 
            // openFileCSV
            // 
            openFileCSV.Filter = "CSV files(*.csv)|*.csv";
            // 
            // linkClearMatrix
            // 
            linkClearMatrix.AutoSize = true;
            linkClearMatrix.LinkColor = Color.FromArgb(64, 64, 64);
            linkClearMatrix.Location = new Point(153, 10);
            linkClearMatrix.Name = "linkClearMatrix";
            linkClearMatrix.Size = new Size(70, 15);
            linkClearMatrix.TabIndex = 12;
            linkClearMatrix.TabStop = true;
            linkClearMatrix.Text = "Clear Matrix";
            linkClearMatrix.LinkClicked += linkClearMatrix_LinkClicked;
            // 
            // InterPoolTemplateControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            Controls.Add(splitContainerMain);
            Name = "InterPoolTemplateControl";
            Size = new Size(510, 559);
            ((System.ComponentModel.ISupportInitialize)numMaxIncoming).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxOutgoing).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinAscReach).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxAscReach).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxDescReach).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinDescReach).EndInit();
            ((System.ComponentModel.ISupportInitialize)numProbability).EndInit();
            grReach.ResumeLayout(false);
            grReach.PerformLayout();
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel1.PerformLayout();
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            tabInterPool.ResumeLayout(false);
            tDynamics.ResumeLayout(false);
            tDynamics.PerformLayout();
            tConductanceMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgConductanceMatrix).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            tTimeline.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lSourcePool;
        private Label lTargetPool;
        private Label lAxonReachMode;
        private Label lJunctionType;
        private Label lMinReach;
        private Label lFixedDuration;
        private Label lSynDelay;
        private ComboBox ddSourcePool;
        private ComboBox ddTargetPool;
        private ComboBox ddAxonReachMode;
        private ComboBox ddJunctionType;
        private ToolTip toolTip1;
        private TextBox eFixedDuration;
        private CheckBox cbActive;
        private NumericUpDown numProbability;
        private Label lProbability;
        private Label lName;
        private Label lDescription;
        private TextBox eName;
        private ComboBox ddDistanceMode;
        private Label lDistanceMode;
        private Label lMaxReach;
        private RichTextBox eDescription;
        private GroupBox grReach;
        private Label lMaxIncoming;
        private NumericUpDown numMaxIncoming;
        private Label lMaxOutgoing;
        private NumericUpDown numMaxOutgoing;
        private Label lUoD2;
        private Label lUoD1;
        private NumericUpDown numMaxDescReach;
        private NumericUpDown numMinDescReach;
        private NumericUpDown numMaxAscReach;
        private NumericUpDown numMinAscReach;
        private CheckBox cbDescending;
        private CheckBox cbAscending;
        private SplitContainer splitContainerMain;
        private TabControl tabInterPool;
        private TabPage tDynamics;
        private TabPage tTimeline;
        private TimeLineControl timeLineControl;
        private TextBox eSynDelay;
        private ComboBox ddCoreType;
        private Label lCoreType;
        private DistributionDataGrid dgDynamics;
        private ComboBox ddReachMode;
        private Label lReachMode;
        private TabPage tConductanceMatrix;
        private DataGridView dgConductanceMatrix;
        private Panel panel1;
        private Label lCondMatrixWarning;
        private Panel panel2;
        private LinkLabel linkSaveMatrix;
        private LinkLabel linkLoadMatrix;
        private SaveFileDialog saveFileCSV;
        private OpenFileDialog openFileCSV;
        private LinkLabel linkClearMatrix;
    }
}
