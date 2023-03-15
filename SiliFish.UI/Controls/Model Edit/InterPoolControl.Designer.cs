namespace SiliFish.UI.Controls
{
    partial class InterPoolControl
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
            lSourcePool = new Label();
            lTargetPool = new Label();
            lAxonReachMode = new Label();
            lConnectionType = new Label();
            lMinReach = new Label();
            lWeight = new Label();
            lFixedDuration = new Label();
            lDelay = new Label();
            ddSourcePool = new ComboBox();
            ddTargetPool = new ComboBox();
            ddAxonReachMode = new ComboBox();
            ddConnectionType = new ComboBox();
            toolTip1 = new ToolTip(components);
            numConductance = new NumericUpDown();
            numDelay = new NumericUpDown();
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
            gSynapse = new GroupBox();
            synapseControl = new SynapseControl();
            cbActive = new CheckBox();
            lName = new Label();
            lDescription = new Label();
            eName = new TextBox();
            ddDistanceMode = new ComboBox();
            lDistanceMode = new Label();
            eDescription = new RichTextBox();
            grReach = new GroupBox();
            cbDescending = new CheckBox();
            cbAscending = new CheckBox();
            lUoD2 = new Label();
            lUoD1 = new Label();
            splitContainerMain = new SplitContainer();
            tabInterPool = new TabControl();
            tDynamics = new TabPage();
            tTimeline = new TabPage();
            timeLineControl = new TimeLineControl();
            tAttachments = new TabPage();
            attachmentList = new AttachmentListControl();
            ((System.ComponentModel.ISupportInitialize)numConductance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxIncoming).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxOutgoing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinAscReach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxAscReach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxDescReach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinDescReach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numProbability).BeginInit();
            gSynapse.SuspendLayout();
            grReach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            tabInterPool.SuspendLayout();
            tDynamics.SuspendLayout();
            tTimeline.SuspendLayout();
            tAttachments.SuspendLayout();
            SuspendLayout();
            // 
            // lSourcePool
            // 
            lSourcePool.AutoSize = true;
            lSourcePool.Location = new Point(13, 9);
            lSourcePool.Name = "lSourcePool";
            lSourcePool.Size = new Size(70, 15);
            lSourcePool.TabIndex = 4;
            lSourcePool.Text = "Source Pool";
            // 
            // lTargetPool
            // 
            lTargetPool.AutoSize = true;
            lTargetPool.Location = new Point(13, 35);
            lTargetPool.Name = "lTargetPool";
            lTargetPool.Size = new Size(66, 15);
            lTargetPool.TabIndex = 6;
            lTargetPool.Text = "Target Pool";
            // 
            // lAxonReachMode
            // 
            lAxonReachMode.AutoSize = true;
            lAxonReachMode.Location = new Point(13, 61);
            lAxonReachMode.Name = "lAxonReachMode";
            lAxonReachMode.Size = new Size(70, 15);
            lAxonReachMode.TabIndex = 8;
            lAxonReachMode.Text = "Axon Reach";
            // 
            // lConnectionType
            // 
            lConnectionType.AutoSize = true;
            lConnectionType.Location = new Point(13, 87);
            lConnectionType.Name = "lConnectionType";
            lConnectionType.Size = new Size(96, 15);
            lConnectionType.TabIndex = 10;
            lConnectionType.Text = "Connection Type";
            // 
            // lMinReach
            // 
            lMinReach.AutoSize = true;
            lMinReach.Location = new Point(100, 45);
            lMinReach.Name = "lMinReach";
            lMinReach.Size = new Size(28, 15);
            lMinReach.TabIndex = 2;
            lMinReach.Text = "Min";
            toolTip1.SetToolTip(lMinReach, "The distance in 3-D, calculated according to the selected 'distance mode'");
            // 
            // lWeight
            // 
            lWeight.AutoSize = true;
            lWeight.Location = new Point(13, 191);
            lWeight.Name = "lWeight";
            lWeight.Size = new Size(45, 15);
            lWeight.TabIndex = 18;
            lWeight.Text = "Weight";
            toolTip1.SetToolTip(lWeight, "Maximum conductance");
            // 
            // lFixedDuration
            // 
            lFixedDuration.AutoSize = true;
            lFixedDuration.Location = new Point(13, 219);
            lFixedDuration.Name = "lFixedDuration";
            lFixedDuration.Size = new Size(60, 15);
            lFixedDuration.TabIndex = 20;
            lFixedDuration.Text = "Fixed Dur.";
            toolTip1.SetToolTip(lFixedDuration, "(in ms) - if entered, distance/speed will not be used to calculate duration.");
            // 
            // lDelay
            // 
            lDelay.AutoSize = true;
            lDelay.Location = new Point(13, 245);
            lDelay.Name = "lDelay";
            lDelay.Size = new Size(36, 15);
            lDelay.TabIndex = 22;
            lDelay.Text = "Delay";
            toolTip1.SetToolTip(lDelay, "in ms");
            // 
            // ddSourcePool
            // 
            ddSourcePool.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddSourcePool.BackColor = Color.WhiteSmoke;
            ddSourcePool.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSourcePool.FlatStyle = FlatStyle.Flat;
            ddSourcePool.FormattingEnabled = true;
            ddSourcePool.Location = new Point(109, 6);
            ddSourcePool.Name = "ddSourcePool";
            ddSourcePool.Size = new Size(106, 23);
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
            ddTargetPool.Location = new Point(109, 32);
            ddTargetPool.Name = "ddTargetPool";
            ddTargetPool.Size = new Size(106, 23);
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
            ddAxonReachMode.Location = new Point(109, 58);
            ddAxonReachMode.Name = "ddAxonReachMode";
            ddAxonReachMode.Size = new Size(106, 23);
            ddAxonReachMode.TabIndex = 9;
            ddAxonReachMode.SelectedIndexChanged += ddAxonReachMode_SelectedIndexChanged;
            // 
            // ddConnectionType
            // 
            ddConnectionType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddConnectionType.BackColor = Color.WhiteSmoke;
            ddConnectionType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddConnectionType.FlatStyle = FlatStyle.Flat;
            ddConnectionType.FormattingEnabled = true;
            ddConnectionType.Items.AddRange(new object[] { "Synapse", "Gap", "NMJ" });
            ddConnectionType.Location = new Point(109, 84);
            ddConnectionType.Name = "ddConnectionType";
            ddConnectionType.Size = new Size(106, 23);
            ddConnectionType.TabIndex = 11;
            ddConnectionType.SelectedIndexChanged += ddConnectionType_SelectedIndexChanged;
            // 
            // numConductance
            // 
            numConductance.DecimalPlaces = 6;
            numConductance.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numConductance.Location = new Point(109, 188);
            numConductance.Name = "numConductance";
            numConductance.Size = new Size(66, 23);
            numConductance.TabIndex = 19;
            toolTip1.SetToolTip(numConductance, "Maximum conductance");
            // 
            // numDelay
            // 
            numDelay.DecimalPlaces = 2;
            numDelay.Location = new Point(109, 240);
            numDelay.Name = "numDelay";
            numDelay.Size = new Size(66, 23);
            numDelay.TabIndex = 23;
            toolTip1.SetToolTip(numDelay, "in ms");
            // 
            // eFixedDuration
            // 
            eFixedDuration.Location = new Point(109, 214);
            eFixedDuration.Name = "eFixedDuration";
            eFixedDuration.Size = new Size(66, 23);
            eFixedDuration.TabIndex = 21;
            toolTip1.SetToolTip(eFixedDuration, "(in ms) - if entered, distance/speed will not be used to calculate duration.\r\n");
            // 
            // lMaxReach
            // 
            lMaxReach.AutoSize = true;
            lMaxReach.Location = new Point(155, 42);
            lMaxReach.Name = "lMaxReach";
            lMaxReach.Size = new Size(30, 15);
            lMaxReach.TabIndex = 4;
            lMaxReach.Text = "Max";
            toolTip1.SetToolTip(lMaxReach, "The distance in 3-D, calculated according to the selected 'distance mode'");
            // 
            // lMaxIncoming
            // 
            lMaxIncoming.AutoSize = true;
            lMaxIncoming.Location = new Point(13, 113);
            lMaxIncoming.Name = "lMaxIncoming";
            lMaxIncoming.Size = new Size(84, 15);
            lMaxIncoming.TabIndex = 12;
            lMaxIncoming.Text = "Max Incoming";
            toolTip1.SetToolTip(lMaxIncoming, "The number of maximum connections the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMaxIncoming
            // 
            numMaxIncoming.Location = new Point(109, 110);
            numMaxIncoming.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxIncoming.Name = "numMaxIncoming";
            numMaxIncoming.Size = new Size(66, 23);
            numMaxIncoming.TabIndex = 13;
            toolTip1.SetToolTip(numMaxIncoming, "The number of maximum connections the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // lMaxOutgoing
            // 
            lMaxOutgoing.AutoSize = true;
            lMaxOutgoing.Location = new Point(13, 139);
            lMaxOutgoing.Name = "lMaxOutgoing";
            lMaxOutgoing.Size = new Size(84, 15);
            lMaxOutgoing.TabIndex = 14;
            lMaxOutgoing.Text = "Max Outgoing";
            toolTip1.SetToolTip(lMaxOutgoing, "The number of maximum connections the source cell can have to the same target pool.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMaxOutgoing
            // 
            numMaxOutgoing.Location = new Point(109, 136);
            numMaxOutgoing.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxOutgoing.Name = "numMaxOutgoing";
            numMaxOutgoing.Size = new Size(66, 23);
            numMaxOutgoing.TabIndex = 15;
            toolTip1.SetToolTip(numMaxOutgoing, "The number of maximum connections the source cell can have to the same target pool.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMinAscReach
            // 
            numMinAscReach.Location = new Point(100, 62);
            numMinAscReach.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMinAscReach.Name = "numMinAscReach";
            numMinAscReach.Size = new Size(50, 23);
            numMinAscReach.TabIndex = 14;
            toolTip1.SetToolTip(numMinAscReach, "The number of maximum connections the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // numMaxAscReach
            // 
            numMaxAscReach.Location = new Point(155, 62);
            numMaxAscReach.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxAscReach.Name = "numMaxAscReach";
            numMaxAscReach.Size = new Size(50, 23);
            numMaxAscReach.TabIndex = 15;
            toolTip1.SetToolTip(numMaxAscReach, "The number of maximum connections the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // numMaxDescReach
            // 
            numMaxDescReach.Location = new Point(155, 89);
            numMaxDescReach.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxDescReach.Name = "numMaxDescReach";
            numMaxDescReach.Size = new Size(50, 23);
            numMaxDescReach.TabIndex = 17;
            toolTip1.SetToolTip(numMaxDescReach, "The number of maximum connections the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // numMinDescReach
            // 
            numMinDescReach.Location = new Point(100, 89);
            numMinDescReach.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMinDescReach.Name = "numMinDescReach";
            numMinDescReach.Size = new Size(50, 23);
            numMinDescReach.TabIndex = 16;
            toolTip1.SetToolTip(numMinDescReach, "The number of maximum connections the target cell can have from the same source pool.\r\nEnter 0 if there is no such limit.");
            // 
            // numProbability
            // 
            numProbability.DecimalPlaces = 2;
            numProbability.Location = new Point(109, 162);
            numProbability.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numProbability.Name = "numProbability";
            numProbability.Size = new Size(66, 23);
            numProbability.TabIndex = 17;
            toolTip1.SetToolTip(numProbability, "The probability of whether there will be a connection between the source and target cell.");
            numProbability.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lProbability
            // 
            lProbability.AutoSize = true;
            lProbability.Location = new Point(13, 165);
            lProbability.Name = "lProbability";
            lProbability.Size = new Size(64, 15);
            lProbability.TabIndex = 16;
            lProbability.Text = "Probability";
            toolTip1.SetToolTip(lProbability, "The probability of whether there will be a connection between the source and target cell.");
            // 
            // gSynapse
            // 
            gSynapse.Controls.Add(synapseControl);
            gSynapse.Location = new Point(10, 128);
            gSynapse.Name = "gSynapse";
            gSynapse.Size = new Size(239, 157);
            gSynapse.TabIndex = 2;
            gSynapse.TabStop = false;
            gSynapse.Text = "Synapse Parameters";
            // 
            // synapseControl
            // 
            synapseControl.BackColor = Color.White;
            synapseControl.EReversal = 0D;
            synapseControl.Location = new Point(8, 22);
            synapseControl.Name = "synapseControl";
            synapseControl.Size = new Size(152, 127);
            synapseControl.TabIndex = 0;
            synapseControl.VThreshold = 0D;
            // 
            // cbActive
            // 
            cbActive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbActive.AutoSize = true;
            cbActive.Checked = true;
            cbActive.CheckState = CheckState.Checked;
            cbActive.Location = new Point(156, 296);
            cbActive.Name = "cbActive";
            cbActive.Size = new Size(59, 19);
            cbActive.TabIndex = 0;
            cbActive.Text = "Active";
            cbActive.UseVisualStyleBackColor = true;
            // 
            // lName
            // 
            lName.AutoSize = true;
            lName.Location = new Point(13, 271);
            lName.Name = "lName";
            lName.Size = new Size(39, 15);
            lName.TabIndex = 24;
            lName.Text = "Name";
            // 
            // lDescription
            // 
            lDescription.AutoSize = true;
            lDescription.Location = new Point(16, 300);
            lDescription.Name = "lDescription";
            lDescription.Size = new Size(67, 15);
            lDescription.TabIndex = 26;
            lDescription.Text = "Description";
            // 
            // eName
            // 
            eName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eName.Location = new Point(109, 268);
            eName.Name = "eName";
            eName.Size = new Size(106, 23);
            eName.TabIndex = 25;
            eName.Leave += eName_Leave;
            // 
            // ddDistanceMode
            // 
            ddDistanceMode.BackColor = Color.WhiteSmoke;
            ddDistanceMode.DropDownStyle = ComboBoxStyle.DropDownList;
            ddDistanceMode.FlatStyle = FlatStyle.Flat;
            ddDistanceMode.FormattingEnabled = true;
            ddDistanceMode.Location = new Point(100, 16);
            ddDistanceMode.Name = "ddDistanceMode";
            ddDistanceMode.Size = new Size(157, 23);
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
            eDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            eDescription.Location = new Point(13, 321);
            eDescription.Name = "eDescription";
            eDescription.Size = new Size(202, 234);
            eDescription.TabIndex = 27;
            eDescription.Text = "";
            // 
            // grReach
            // 
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
            grReach.Location = new Point(10, 3);
            grReach.Name = "grReach";
            grReach.Size = new Size(263, 124);
            grReach.TabIndex = 1;
            grReach.TabStop = false;
            grReach.Text = "Reach";
            // 
            // cbDescending
            // 
            cbDescending.AutoSize = true;
            cbDescending.Checked = true;
            cbDescending.CheckState = CheckState.Checked;
            cbDescending.Location = new Point(8, 89);
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
            cbAscending.Location = new Point(8, 66);
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
            lUoD2.Location = new Point(211, 91);
            lUoD2.Name = "lUoD2";
            lUoD2.Size = new Size(48, 15);
            lUoD2.TabIndex = 24;
            lUoD2.Text = "somites";
            // 
            // lUoD1
            // 
            lUoD1.AutoSize = true;
            lUoD1.Location = new Point(211, 67);
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
            splitContainerMain.Panel1.Controls.Add(lSourcePool);
            splitContainerMain.Panel1.Controls.Add(lMaxOutgoing);
            splitContainerMain.Panel1.Controls.Add(lTargetPool);
            splitContainerMain.Panel1.Controls.Add(numMaxOutgoing);
            splitContainerMain.Panel1.Controls.Add(lAxonReachMode);
            splitContainerMain.Panel1.Controls.Add(lMaxIncoming);
            splitContainerMain.Panel1.Controls.Add(lConnectionType);
            splitContainerMain.Panel1.Controls.Add(numMaxIncoming);
            splitContainerMain.Panel1.Controls.Add(lWeight);
            splitContainerMain.Panel1.Controls.Add(eDescription);
            splitContainerMain.Panel1.Controls.Add(lFixedDuration);
            splitContainerMain.Panel1.Controls.Add(eName);
            splitContainerMain.Panel1.Controls.Add(lDelay);
            splitContainerMain.Panel1.Controls.Add(lDescription);
            splitContainerMain.Panel1.Controls.Add(ddSourcePool);
            splitContainerMain.Panel1.Controls.Add(lName);
            splitContainerMain.Panel1.Controls.Add(ddTargetPool);
            splitContainerMain.Panel1.Controls.Add(lProbability);
            splitContainerMain.Panel1.Controls.Add(ddAxonReachMode);
            splitContainerMain.Panel1.Controls.Add(numProbability);
            splitContainerMain.Panel1.Controls.Add(ddConnectionType);
            splitContainerMain.Panel1.Controls.Add(cbActive);
            splitContainerMain.Panel1.Controls.Add(numConductance);
            splitContainerMain.Panel1.Controls.Add(eFixedDuration);
            splitContainerMain.Panel1.Controls.Add(numDelay);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(tabInterPool);
            splitContainerMain.Size = new Size(510, 559);
            splitContainerMain.SplitterDistance = 218;
            splitContainerMain.TabIndex = 28;
            // 
            // tabInterPool
            // 
            tabInterPool.Controls.Add(tDynamics);
            tabInterPool.Controls.Add(tTimeline);
            tabInterPool.Controls.Add(tAttachments);
            tabInterPool.Dock = DockStyle.Fill;
            tabInterPool.Location = new Point(0, 0);
            tabInterPool.Name = "tabInterPool";
            tabInterPool.SelectedIndex = 0;
            tabInterPool.Size = new Size(288, 559);
            tabInterPool.TabIndex = 0;
            // 
            // tDynamics
            // 
            tDynamics.Controls.Add(grReach);
            tDynamics.Controls.Add(gSynapse);
            tDynamics.Location = new Point(4, 24);
            tDynamics.Name = "tDynamics";
            tDynamics.Padding = new Padding(3);
            tDynamics.Size = new Size(280, 531);
            tDynamics.TabIndex = 0;
            tDynamics.Text = "Dynamics";
            tDynamics.UseVisualStyleBackColor = true;
            // 
            // tTimeline
            // 
            tTimeline.Controls.Add(timeLineControl);
            tTimeline.Location = new Point(4, 24);
            tTimeline.Name = "tTimeline";
            tTimeline.Padding = new Padding(3);
            tTimeline.Size = new Size(545, 531);
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
            timeLineControl.Size = new Size(539, 525);
            timeLineControl.TabIndex = 24;
            // 
            // tAttachments
            // 
            tAttachments.Controls.Add(attachmentList);
            tAttachments.Location = new Point(4, 24);
            tAttachments.Name = "tAttachments";
            tAttachments.Size = new Size(545, 531);
            tAttachments.TabIndex = 2;
            tAttachments.Text = "Attachments";
            tAttachments.UseVisualStyleBackColor = true;
            // 
            // attachmentList
            // 
            attachmentList.Dock = DockStyle.Fill;
            attachmentList.Location = new Point(0, 0);
            attachmentList.Name = "attachmentList";
            attachmentList.Size = new Size(545, 531);
            attachmentList.TabIndex = 0;
            // 
            // InterPoolControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            Controls.Add(splitContainerMain);
            Name = "InterPoolControl";
            Size = new Size(510, 559);
            ((System.ComponentModel.ISupportInitialize)numConductance).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxIncoming).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxOutgoing).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinAscReach).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxAscReach).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxDescReach).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinDescReach).EndInit();
            ((System.ComponentModel.ISupportInitialize)numProbability).EndInit();
            gSynapse.ResumeLayout(false);
            grReach.ResumeLayout(false);
            grReach.PerformLayout();
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel1.PerformLayout();
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            tabInterPool.ResumeLayout(false);
            tDynamics.ResumeLayout(false);
            tTimeline.ResumeLayout(false);
            tAttachments.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lSourcePool;
        private Label lTargetPool;
        private Label lAxonReachMode;
        private Label lConnectionType;
        private Label lMinReach;
        private Label lWeight;
        private Label lFixedDuration;
        private Label lDelay;
        private ComboBox ddSourcePool;
        private ComboBox ddTargetPool;
        private ComboBox ddAxonReachMode;
        private ComboBox ddConnectionType;
        private ToolTip toolTip1;
        private NumericUpDown numConductance;
        private NumericUpDown numDelay;
        private TextBox eFixedDuration;
        private GroupBox gSynapse;
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
        private TabPage tAttachments;
        private TimeLineControl timeLineControl;
        private SynapseControl synapseControl;
        private AttachmentListControl attachmentList;
    }
}
