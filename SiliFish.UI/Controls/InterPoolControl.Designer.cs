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
            this.components = new System.ComponentModel.Container();
            this.lSourcePool = new System.Windows.Forms.Label();
            this.lTargetPool = new System.Windows.Forms.Label();
            this.lAxonReachMode = new System.Windows.Forms.Label();
            this.lConnectionType = new System.Windows.Forms.Label();
            this.lMinReach = new System.Windows.Forms.Label();
            this.lWeight = new System.Windows.Forms.Label();
            this.lFixedDuration = new System.Windows.Forms.Label();
            this.lDelay = new System.Windows.Forms.Label();
            this.ddSourcePool = new System.Windows.Forms.ComboBox();
            this.ddTargetPool = new System.Windows.Forms.ComboBox();
            this.ddAxonReachMode = new System.Windows.Forms.ComboBox();
            this.ddConnectionType = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numConductance = new System.Windows.Forms.NumericUpDown();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.eFixedDuration = new System.Windows.Forms.TextBox();
            this.lMaxReach = new System.Windows.Forms.Label();
            this.lMaxIncoming = new System.Windows.Forms.Label();
            this.numMaxIncoming = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numMaxOutgoing = new System.Windows.Forms.NumericUpDown();
            this.numMinAscReach = new System.Windows.Forms.NumericUpDown();
            this.numMaxAscReach = new System.Windows.Forms.NumericUpDown();
            this.numMaxDescReach = new System.Windows.Forms.NumericUpDown();
            this.numMinDescReach = new System.Windows.Forms.NumericUpDown();
            this.gSynapse = new System.Windows.Forms.GroupBox();
            this.synapseControl = new SiliFish.UI.Controls.SynapseControl();
            this.timeLineControl = new SiliFish.UI.Controls.TimeLineControl();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.numProbability = new System.Windows.Forms.NumericUpDown();
            this.lProbability = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.lDescription = new System.Windows.Forms.Label();
            this.eName = new System.Windows.Forms.TextBox();
            this.ddDistanceMode = new System.Windows.Forms.ComboBox();
            this.lDistanceMode = new System.Windows.Forms.Label();
            this.eDescription = new System.Windows.Forms.RichTextBox();
            this.grReach = new System.Windows.Forms.GroupBox();
            this.cbDescending = new System.Windows.Forms.CheckBox();
            this.cbAscending = new System.Windows.Forms.CheckBox();
            this.lUoD2 = new System.Windows.Forms.Label();
            this.lUoD1 = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tabInterPool = new System.Windows.Forms.TabControl();
            this.tDynamics = new System.Windows.Forms.TabPage();
            this.tTimeline = new System.Windows.Forms.TabPage();
            this.tAttachments = new System.Windows.Forms.TabPage();
            this.attachmentList = new SiliFish.UI.Controls.AttachmentListControl();
            ((System.ComponentModel.ISupportInitialize)(this.numConductance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxIncoming)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxOutgoing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAscReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAscReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDescReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinDescReach)).BeginInit();
            this.gSynapse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).BeginInit();
            this.grReach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabInterPool.SuspendLayout();
            this.tDynamics.SuspendLayout();
            this.tTimeline.SuspendLayout();
            this.tAttachments.SuspendLayout();
            this.SuspendLayout();
            // 
            // lSourcePool
            // 
            this.lSourcePool.AutoSize = true;
            this.lSourcePool.Location = new System.Drawing.Point(13, 9);
            this.lSourcePool.Name = "lSourcePool";
            this.lSourcePool.Size = new System.Drawing.Size(70, 15);
            this.lSourcePool.TabIndex = 4;
            this.lSourcePool.Text = "Source Pool";
            // 
            // lTargetPool
            // 
            this.lTargetPool.AutoSize = true;
            this.lTargetPool.Location = new System.Drawing.Point(13, 35);
            this.lTargetPool.Name = "lTargetPool";
            this.lTargetPool.Size = new System.Drawing.Size(66, 15);
            this.lTargetPool.TabIndex = 6;
            this.lTargetPool.Text = "Target Pool";
            // 
            // lAxonReachMode
            // 
            this.lAxonReachMode.AutoSize = true;
            this.lAxonReachMode.Location = new System.Drawing.Point(13, 61);
            this.lAxonReachMode.Name = "lAxonReachMode";
            this.lAxonReachMode.Size = new System.Drawing.Size(70, 15);
            this.lAxonReachMode.TabIndex = 8;
            this.lAxonReachMode.Text = "Axon Reach";
            // 
            // lConnectionType
            // 
            this.lConnectionType.AutoSize = true;
            this.lConnectionType.Location = new System.Drawing.Point(13, 87);
            this.lConnectionType.Name = "lConnectionType";
            this.lConnectionType.Size = new System.Drawing.Size(96, 15);
            this.lConnectionType.TabIndex = 10;
            this.lConnectionType.Text = "Connection Type";
            // 
            // lMinReach
            // 
            this.lMinReach.AutoSize = true;
            this.lMinReach.Location = new System.Drawing.Point(92, 47);
            this.lMinReach.Name = "lMinReach";
            this.lMinReach.Size = new System.Drawing.Size(28, 15);
            this.lMinReach.TabIndex = 2;
            this.lMinReach.Text = "Min";
            this.toolTip1.SetToolTip(this.lMinReach, "The distance in 3-D, calculated according to the selected \'distance mode\'");
            // 
            // lWeight
            // 
            this.lWeight.AutoSize = true;
            this.lWeight.Location = new System.Drawing.Point(13, 191);
            this.lWeight.Name = "lWeight";
            this.lWeight.Size = new System.Drawing.Size(45, 15);
            this.lWeight.TabIndex = 18;
            this.lWeight.Text = "Weight";
            this.toolTip1.SetToolTip(this.lWeight, "Maximum conductance");
            // 
            // lFixedDuration
            // 
            this.lFixedDuration.AutoSize = true;
            this.lFixedDuration.Location = new System.Drawing.Point(13, 219);
            this.lFixedDuration.Name = "lFixedDuration";
            this.lFixedDuration.Size = new System.Drawing.Size(60, 15);
            this.lFixedDuration.TabIndex = 20;
            this.lFixedDuration.Text = "Fixed Dur.";
            this.toolTip1.SetToolTip(this.lFixedDuration, "(in ms) - if entered, distance/speed will not be used to calculate duration.");
            // 
            // lDelay
            // 
            this.lDelay.AutoSize = true;
            this.lDelay.Location = new System.Drawing.Point(13, 245);
            this.lDelay.Name = "lDelay";
            this.lDelay.Size = new System.Drawing.Size(36, 15);
            this.lDelay.TabIndex = 22;
            this.lDelay.Text = "Delay";
            this.toolTip1.SetToolTip(this.lDelay, "in ms");
            // 
            // ddSourcePool
            // 
            this.ddSourcePool.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddSourcePool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSourcePool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddSourcePool.FormattingEnabled = true;
            this.ddSourcePool.Location = new System.Drawing.Point(109, 6);
            this.ddSourcePool.Name = "ddSourcePool";
            this.ddSourcePool.Size = new System.Drawing.Size(128, 23);
            this.ddSourcePool.TabIndex = 5;
            this.ddSourcePool.SelectedIndexChanged += new System.EventHandler(this.ddSourcePool_SelectedIndexChanged);
            // 
            // ddTargetPool
            // 
            this.ddTargetPool.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddTargetPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetPool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddTargetPool.FormattingEnabled = true;
            this.ddTargetPool.Location = new System.Drawing.Point(109, 32);
            this.ddTargetPool.Name = "ddTargetPool";
            this.ddTargetPool.Size = new System.Drawing.Size(128, 23);
            this.ddTargetPool.TabIndex = 7;
            this.ddTargetPool.SelectedIndexChanged += new System.EventHandler(this.ddTargetPool_SelectedIndexChanged);
            // 
            // ddAxonReachMode
            // 
            this.ddAxonReachMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddAxonReachMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddAxonReachMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddAxonReachMode.FormattingEnabled = true;
            this.ddAxonReachMode.Location = new System.Drawing.Point(109, 58);
            this.ddAxonReachMode.Name = "ddAxonReachMode";
            this.ddAxonReachMode.Size = new System.Drawing.Size(128, 23);
            this.ddAxonReachMode.TabIndex = 9;
            this.ddAxonReachMode.SelectedIndexChanged += new System.EventHandler(this.ddAxonReachMode_SelectedIndexChanged);
            // 
            // ddConnectionType
            // 
            this.ddConnectionType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddConnectionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddConnectionType.FormattingEnabled = true;
            this.ddConnectionType.Items.AddRange(new object[] {
            "Synapse",
            "Gap",
            "NMJ"});
            this.ddConnectionType.Location = new System.Drawing.Point(109, 84);
            this.ddConnectionType.Name = "ddConnectionType";
            this.ddConnectionType.Size = new System.Drawing.Size(128, 23);
            this.ddConnectionType.TabIndex = 11;
            this.ddConnectionType.SelectedIndexChanged += new System.EventHandler(this.ddConnectionType_SelectedIndexChanged);
            // 
            // numConductance
            // 
            this.numConductance.DecimalPlaces = 6;
            this.numConductance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numConductance.Location = new System.Drawing.Point(109, 188);
            this.numConductance.Name = "numConductance";
            this.numConductance.Size = new System.Drawing.Size(66, 23);
            this.numConductance.TabIndex = 19;
            this.toolTip1.SetToolTip(this.numConductance, "Maximum conductance");
            // 
            // numDelay
            // 
            this.numDelay.DecimalPlaces = 2;
            this.numDelay.Location = new System.Drawing.Point(109, 240);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(66, 23);
            this.numDelay.TabIndex = 23;
            this.toolTip1.SetToolTip(this.numDelay, "in ms");
            // 
            // eFixedDuration
            // 
            this.eFixedDuration.Location = new System.Drawing.Point(109, 214);
            this.eFixedDuration.Name = "eFixedDuration";
            this.eFixedDuration.Size = new System.Drawing.Size(66, 23);
            this.eFixedDuration.TabIndex = 21;
            this.toolTip1.SetToolTip(this.eFixedDuration, "(in ms) - if entered, distance/speed will not be used to calculate duration.\r\n");
            // 
            // lMaxReach
            // 
            this.lMaxReach.AutoSize = true;
            this.lMaxReach.Location = new System.Drawing.Point(140, 47);
            this.lMaxReach.Name = "lMaxReach";
            this.lMaxReach.Size = new System.Drawing.Size(30, 15);
            this.lMaxReach.TabIndex = 4;
            this.lMaxReach.Text = "Max";
            this.toolTip1.SetToolTip(this.lMaxReach, "The distance in 3-D, calculated according to the selected \'distance mode\'");
            // 
            // lMaxIncoming
            // 
            this.lMaxIncoming.AutoSize = true;
            this.lMaxIncoming.Location = new System.Drawing.Point(13, 113);
            this.lMaxIncoming.Name = "lMaxIncoming";
            this.lMaxIncoming.Size = new System.Drawing.Size(84, 15);
            this.lMaxIncoming.TabIndex = 12;
            this.lMaxIncoming.Text = "Max Incoming";
            this.toolTip1.SetToolTip(this.lMaxIncoming, "The number of maximum connections the target cell can have from the same source p" +
        "ool.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMaxIncoming
            // 
            this.numMaxIncoming.Location = new System.Drawing.Point(109, 110);
            this.numMaxIncoming.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxIncoming.Name = "numMaxIncoming";
            this.numMaxIncoming.Size = new System.Drawing.Size(66, 23);
            this.numMaxIncoming.TabIndex = 13;
            this.toolTip1.SetToolTip(this.numMaxIncoming, "The number of maximum connections the target cell can have from the same source p" +
        "ool.\r\nEnter 0 if there is no such limit.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Max Outgoing";
            this.toolTip1.SetToolTip(this.label1, "The number of maximum connections the source cell can have to the same target poo" +
        "l.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMaxOutgoing
            // 
            this.numMaxOutgoing.Location = new System.Drawing.Point(109, 136);
            this.numMaxOutgoing.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxOutgoing.Name = "numMaxOutgoing";
            this.numMaxOutgoing.Size = new System.Drawing.Size(66, 23);
            this.numMaxOutgoing.TabIndex = 15;
            this.toolTip1.SetToolTip(this.numMaxOutgoing, "The number of maximum connections the source cell can have to the same target poo" +
        "l.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMinAscReach
            // 
            this.numMinAscReach.Location = new System.Drawing.Point(92, 62);
            this.numMinAscReach.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMinAscReach.Name = "numMinAscReach";
            this.numMinAscReach.Size = new System.Drawing.Size(46, 23);
            this.numMinAscReach.TabIndex = 14;
            this.toolTip1.SetToolTip(this.numMinAscReach, "The number of maximum connections the target cell can have from the same source p" +
        "ool.\r\nEnter 0 if there is no such limit.");
            // 
            // numMaxAscReach
            // 
            this.numMaxAscReach.Location = new System.Drawing.Point(144, 62);
            this.numMaxAscReach.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxAscReach.Name = "numMaxAscReach";
            this.numMaxAscReach.Size = new System.Drawing.Size(46, 23);
            this.numMaxAscReach.TabIndex = 15;
            this.toolTip1.SetToolTip(this.numMaxAscReach, "The number of maximum connections the target cell can have from the same source p" +
        "ool.\r\nEnter 0 if there is no such limit.");
            // 
            // numMaxDescReach
            // 
            this.numMaxDescReach.Location = new System.Drawing.Point(144, 89);
            this.numMaxDescReach.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxDescReach.Name = "numMaxDescReach";
            this.numMaxDescReach.Size = new System.Drawing.Size(46, 23);
            this.numMaxDescReach.TabIndex = 17;
            this.toolTip1.SetToolTip(this.numMaxDescReach, "The number of maximum connections the target cell can have from the same source p" +
        "ool.\r\nEnter 0 if there is no such limit.");
            // 
            // numMinDescReach
            // 
            this.numMinDescReach.Location = new System.Drawing.Point(92, 89);
            this.numMinDescReach.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMinDescReach.Name = "numMinDescReach";
            this.numMinDescReach.Size = new System.Drawing.Size(46, 23);
            this.numMinDescReach.TabIndex = 16;
            this.toolTip1.SetToolTip(this.numMinDescReach, "The number of maximum connections the target cell can have from the same source p" +
        "ool.\r\nEnter 0 if there is no such limit.");
            // 
            // gSynapse
            // 
            this.gSynapse.Controls.Add(this.synapseControl);
            this.gSynapse.Location = new System.Drawing.Point(10, 143);
            this.gSynapse.Name = "gSynapse";
            this.gSynapse.Size = new System.Drawing.Size(239, 148);
            this.gSynapse.TabIndex = 2;
            this.gSynapse.TabStop = false;
            this.gSynapse.Text = "Synapse Parameters";
            // 
            // synapseControl
            // 
            this.synapseControl.EReversal = 0D;
            this.synapseControl.Location = new System.Drawing.Point(14, 21);
            this.synapseControl.Name = "synapseControl";
            this.synapseControl.Size = new System.Drawing.Size(152, 127);
            this.synapseControl.TabIndex = 0;
            this.synapseControl.VThreshold = 0D;
            // 
            // timeLineControl
            // 
            this.timeLineControl.AutoScroll = true;
            this.timeLineControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLineControl.Location = new System.Drawing.Point(3, 3);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(539, 525);
            this.timeLineControl.TabIndex = 0;
            // 
            // cbActive
            // 
            this.cbActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(205, 296);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(59, 19);
            this.cbActive.TabIndex = 0;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            this.cbActive.CheckedChanged += new System.EventHandler(this.cbActive_CheckedChanged);
            // 
            // numProbability
            // 
            this.numProbability.DecimalPlaces = 2;
            this.numProbability.Location = new System.Drawing.Point(109, 162);
            this.numProbability.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numProbability.Name = "numProbability";
            this.numProbability.Size = new System.Drawing.Size(66, 23);
            this.numProbability.TabIndex = 17;
            this.numProbability.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lProbability
            // 
            this.lProbability.AutoSize = true;
            this.lProbability.Location = new System.Drawing.Point(13, 165);
            this.lProbability.Name = "lProbability";
            this.lProbability.Size = new System.Drawing.Size(64, 15);
            this.lProbability.TabIndex = 16;
            this.lProbability.Text = "Probability";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(13, 271);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(39, 15);
            this.lName.TabIndex = 24;
            this.lName.Text = "Name";
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(16, 300);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(67, 15);
            this.lDescription.TabIndex = 26;
            this.lDescription.Text = "Description";
            // 
            // eName
            // 
            this.eName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eName.Location = new System.Drawing.Point(109, 268);
            this.eName.Name = "eName";
            this.eName.Size = new System.Drawing.Size(166, 23);
            this.eName.TabIndex = 25;
            this.eName.Leave += new System.EventHandler(this.eName_Leave);
            // 
            // ddDistanceMode
            // 
            this.ddDistanceMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddDistanceMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddDistanceMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddDistanceMode.FormattingEnabled = true;
            this.ddDistanceMode.Location = new System.Drawing.Point(100, 16);
            this.ddDistanceMode.Name = "ddDistanceMode";
            this.ddDistanceMode.Size = new System.Drawing.Size(137, 23);
            this.ddDistanceMode.TabIndex = 1;
            // 
            // lDistanceMode
            // 
            this.lDistanceMode.AutoSize = true;
            this.lDistanceMode.Location = new System.Drawing.Point(4, 22);
            this.lDistanceMode.Name = "lDistanceMode";
            this.lDistanceMode.Size = new System.Drawing.Size(86, 15);
            this.lDistanceMode.TabIndex = 0;
            this.lDistanceMode.Text = "Distance Mode";
            // 
            // eDescription
            // 
            this.eDescription.AcceptsTab = true;
            this.eDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eDescription.Location = new System.Drawing.Point(13, 321);
            this.eDescription.Name = "eDescription";
            this.eDescription.Size = new System.Drawing.Size(262, 234);
            this.eDescription.TabIndex = 27;
            this.eDescription.Text = "";
            // 
            // grReach
            // 
            this.grReach.Controls.Add(this.cbDescending);
            this.grReach.Controls.Add(this.cbAscending);
            this.grReach.Controls.Add(this.lUoD2);
            this.grReach.Controls.Add(this.lUoD1);
            this.grReach.Controls.Add(this.numMaxDescReach);
            this.grReach.Controls.Add(this.numMinDescReach);
            this.grReach.Controls.Add(this.numMaxAscReach);
            this.grReach.Controls.Add(this.numMinAscReach);
            this.grReach.Controls.Add(this.ddDistanceMode);
            this.grReach.Controls.Add(this.lDistanceMode);
            this.grReach.Controls.Add(this.lMinReach);
            this.grReach.Controls.Add(this.lMaxReach);
            this.grReach.Location = new System.Drawing.Point(6, 6);
            this.grReach.Name = "grReach";
            this.grReach.Size = new System.Drawing.Size(243, 124);
            this.grReach.TabIndex = 1;
            this.grReach.TabStop = false;
            this.grReach.Text = "Reach";
            // 
            // cbDescending
            // 
            this.cbDescending.AutoSize = true;
            this.cbDescending.Checked = true;
            this.cbDescending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDescending.Location = new System.Drawing.Point(2, 91);
            this.cbDescending.Name = "cbDescending";
            this.cbDescending.Size = new System.Drawing.Size(88, 19);
            this.cbDescending.TabIndex = 26;
            this.cbDescending.Text = "Descending";
            this.cbDescending.UseVisualStyleBackColor = true;
            this.cbDescending.CheckedChanged += new System.EventHandler(this.cbDescending_CheckedChanged);
            // 
            // cbAscending
            // 
            this.cbAscending.AutoSize = true;
            this.cbAscending.Checked = true;
            this.cbAscending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAscending.Location = new System.Drawing.Point(2, 66);
            this.cbAscending.Name = "cbAscending";
            this.cbAscending.Size = new System.Drawing.Size(82, 19);
            this.cbAscending.TabIndex = 25;
            this.cbAscending.Text = "Ascending";
            this.cbAscending.UseVisualStyleBackColor = true;
            this.cbAscending.CheckedChanged += new System.EventHandler(this.cbAscending_CheckedChanged);
            // 
            // lUoD2
            // 
            this.lUoD2.AutoSize = true;
            this.lUoD2.Location = new System.Drawing.Point(192, 91);
            this.lUoD2.Name = "lUoD2";
            this.lUoD2.Size = new System.Drawing.Size(48, 15);
            this.lUoD2.TabIndex = 24;
            this.lUoD2.Text = "somites";
            // 
            // lUoD1
            // 
            this.lUoD1.AutoSize = true;
            this.lUoD1.Location = new System.Drawing.Point(192, 67);
            this.lUoD1.Name = "lUoD1";
            this.lUoD1.Size = new System.Drawing.Size(48, 15);
            this.lUoD1.TabIndex = 23;
            this.lUoD1.Text = "somites";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.lSourcePool);
            this.splitContainerMain.Panel1.Controls.Add(this.label1);
            this.splitContainerMain.Panel1.Controls.Add(this.lTargetPool);
            this.splitContainerMain.Panel1.Controls.Add(this.numMaxOutgoing);
            this.splitContainerMain.Panel1.Controls.Add(this.lAxonReachMode);
            this.splitContainerMain.Panel1.Controls.Add(this.lMaxIncoming);
            this.splitContainerMain.Panel1.Controls.Add(this.lConnectionType);
            this.splitContainerMain.Panel1.Controls.Add(this.numMaxIncoming);
            this.splitContainerMain.Panel1.Controls.Add(this.lWeight);
            this.splitContainerMain.Panel1.Controls.Add(this.eDescription);
            this.splitContainerMain.Panel1.Controls.Add(this.lFixedDuration);
            this.splitContainerMain.Panel1.Controls.Add(this.eName);
            this.splitContainerMain.Panel1.Controls.Add(this.lDelay);
            this.splitContainerMain.Panel1.Controls.Add(this.lDescription);
            this.splitContainerMain.Panel1.Controls.Add(this.ddSourcePool);
            this.splitContainerMain.Panel1.Controls.Add(this.lName);
            this.splitContainerMain.Panel1.Controls.Add(this.ddTargetPool);
            this.splitContainerMain.Panel1.Controls.Add(this.lProbability);
            this.splitContainerMain.Panel1.Controls.Add(this.ddAxonReachMode);
            this.splitContainerMain.Panel1.Controls.Add(this.numProbability);
            this.splitContainerMain.Panel1.Controls.Add(this.ddConnectionType);
            this.splitContainerMain.Panel1.Controls.Add(this.cbActive);
            this.splitContainerMain.Panel1.Controls.Add(this.numConductance);
            this.splitContainerMain.Panel1.Controls.Add(this.eFixedDuration);
            this.splitContainerMain.Panel1.Controls.Add(this.numDelay);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabInterPool);
            this.splitContainerMain.Size = new System.Drawing.Size(835, 559);
            this.splitContainerMain.SplitterDistance = 278;
            this.splitContainerMain.TabIndex = 28;
            // 
            // tabInterPool
            // 
            this.tabInterPool.Controls.Add(this.tDynamics);
            this.tabInterPool.Controls.Add(this.tTimeline);
            this.tabInterPool.Controls.Add(this.tAttachments);
            this.tabInterPool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInterPool.Location = new System.Drawing.Point(0, 0);
            this.tabInterPool.Name = "tabInterPool";
            this.tabInterPool.SelectedIndex = 0;
            this.tabInterPool.Size = new System.Drawing.Size(553, 559);
            this.tabInterPool.TabIndex = 0;
            // 
            // tDynamics
            // 
            this.tDynamics.Controls.Add(this.grReach);
            this.tDynamics.Controls.Add(this.gSynapse);
            this.tDynamics.Location = new System.Drawing.Point(4, 24);
            this.tDynamics.Name = "tDynamics";
            this.tDynamics.Padding = new System.Windows.Forms.Padding(3);
            this.tDynamics.Size = new System.Drawing.Size(545, 531);
            this.tDynamics.TabIndex = 0;
            this.tDynamics.Text = "Dynamics";
            this.tDynamics.UseVisualStyleBackColor = true;
            // 
            // tTimeline
            // 
            this.tTimeline.Controls.Add(this.timeLineControl);
            this.tTimeline.Location = new System.Drawing.Point(4, 24);
            this.tTimeline.Name = "tTimeline";
            this.tTimeline.Padding = new System.Windows.Forms.Padding(3);
            this.tTimeline.Size = new System.Drawing.Size(545, 531);
            this.tTimeline.TabIndex = 1;
            this.tTimeline.Text = "Timeline";
            this.tTimeline.UseVisualStyleBackColor = true;
            // 
            // tAttachments
            // 
            this.tAttachments.Controls.Add(this.attachmentList);
            this.tAttachments.Location = new System.Drawing.Point(4, 24);
            this.tAttachments.Name = "tAttachments";
            this.tAttachments.Size = new System.Drawing.Size(545, 531);
            this.tAttachments.TabIndex = 2;
            this.tAttachments.Text = "Attachments";
            this.tAttachments.UseVisualStyleBackColor = true;
            // 
            // attachmentList
            // 
            this.attachmentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attachmentList.Location = new System.Drawing.Point(0, 0);
            this.attachmentList.Name = "attachmentList";
            this.attachmentList.Size = new System.Drawing.Size(545, 531);
            this.attachmentList.TabIndex = 0;
            // 
            // InterPoolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "InterPoolControl";
            this.Size = new System.Drawing.Size(835, 559);
            ((System.ComponentModel.ISupportInitialize)(this.numConductance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxIncoming)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxOutgoing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAscReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAscReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDescReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinDescReach)).EndInit();
            this.gSynapse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).EndInit();
            this.grReach.ResumeLayout(false);
            this.grReach.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabInterPool.ResumeLayout(false);
            this.tDynamics.ResumeLayout(false);
            this.tTimeline.ResumeLayout(false);
            this.tAttachments.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private TimeLineControl timeLineControl;
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
        private Label label1;
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
        private AttachmentListControl attachmentList;
        private SynapseControl synapseControl;
    }
}
