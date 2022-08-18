﻿namespace SiliFish.UI.Controls
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
            this.lAscReach = new System.Windows.Forms.Label();
            this.lDescReach = new System.Windows.Forms.Label();
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
            this.lTauD = new System.Windows.Forms.Label();
            this.lTauR = new System.Windows.Forms.Label();
            this.lEReversal = new System.Windows.Forms.Label();
            this.numTauD = new System.Windows.Forms.NumericUpDown();
            this.numTauR = new System.Windows.Forms.NumericUpDown();
            this.numEReversal = new System.Windows.Forms.NumericUpDown();
            this.numAscReach = new System.Windows.Forms.NumericUpDown();
            this.numDescReach = new System.Windows.Forms.NumericUpDown();
            this.numMaxReach = new System.Windows.Forms.NumericUpDown();
            this.cbWithinSomite = new System.Windows.Forms.CheckBox();
            this.cbOtherSomite = new System.Windows.Forms.CheckBox();
            this.numMinReach = new System.Windows.Forms.NumericUpDown();
            this.gSynapse = new System.Windows.Forms.GroupBox();
            this.numVthreshold = new System.Windows.Forms.NumericUpDown();
            this.lVThreshold = new System.Windows.Forms.Label();
            this.timeLineControl = new SiliFish.UI.Controls.TimeLineControl();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.numProbability = new System.Windows.Forms.NumericUpDown();
            this.lProbability = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.lDescription = new System.Windows.Forms.Label();
            this.eName = new System.Windows.Forms.TextBox();
            this.grTimeline = new System.Windows.Forms.GroupBox();
            this.ddDistanceMode = new System.Windows.Forms.ComboBox();
            this.lDistanceMode = new System.Windows.Forms.Label();
            this.lMaxReach = new System.Windows.Forms.Label();
            this.eDescription = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numConductance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEReversal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAscReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDescReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinReach)).BeginInit();
            this.gSynapse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVthreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).BeginInit();
            this.grTimeline.SuspendLayout();
            this.SuspendLayout();
            // 
            // lSourcePool
            // 
            this.lSourcePool.AutoSize = true;
            this.lSourcePool.Location = new System.Drawing.Point(9, 17);
            this.lSourcePool.Name = "lSourcePool";
            this.lSourcePool.Size = new System.Drawing.Size(70, 15);
            this.lSourcePool.TabIndex = 0;
            this.lSourcePool.Text = "Source Pool";
            // 
            // lTargetPool
            // 
            this.lTargetPool.AutoSize = true;
            this.lTargetPool.Location = new System.Drawing.Point(9, 43);
            this.lTargetPool.Name = "lTargetPool";
            this.lTargetPool.Size = new System.Drawing.Size(66, 15);
            this.lTargetPool.TabIndex = 2;
            this.lTargetPool.Text = "Target Pool";
            // 
            // lAxonReachMode
            // 
            this.lAxonReachMode.AutoSize = true;
            this.lAxonReachMode.Location = new System.Drawing.Point(9, 69);
            this.lAxonReachMode.Name = "lAxonReachMode";
            this.lAxonReachMode.Size = new System.Drawing.Size(70, 15);
            this.lAxonReachMode.TabIndex = 4;
            this.lAxonReachMode.Text = "Axon Reach";
            // 
            // lConnectionType
            // 
            this.lConnectionType.AutoSize = true;
            this.lConnectionType.Location = new System.Drawing.Point(9, 95);
            this.lConnectionType.Name = "lConnectionType";
            this.lConnectionType.Size = new System.Drawing.Size(96, 15);
            this.lConnectionType.TabIndex = 6;
            this.lConnectionType.Text = "Connection Type";
            // 
            // lMinReach
            // 
            this.lMinReach.AutoSize = true;
            this.lMinReach.Location = new System.Drawing.Point(253, 65);
            this.lMinReach.Name = "lMinReach";
            this.lMinReach.Size = new System.Drawing.Size(63, 15);
            this.lMinReach.TabIndex = 17;
            this.lMinReach.Text = "Min Reach";
            // 
            // lAscReach
            // 
            this.lAscReach.AutoSize = true;
            this.lAscReach.Location = new System.Drawing.Point(253, 119);
            this.lAscReach.Name = "lAscReach";
            this.lAscReach.Size = new System.Drawing.Size(64, 15);
            this.lAscReach.TabIndex = 21;
            this.lAscReach.Text = "Asc. Reach";
            // 
            // lDescReach
            // 
            this.lDescReach.AutoSize = true;
            this.lDescReach.Location = new System.Drawing.Point(253, 146);
            this.lDescReach.Name = "lDescReach";
            this.lDescReach.Size = new System.Drawing.Size(70, 15);
            this.lDescReach.TabIndex = 23;
            this.lDescReach.Text = "Desc. Reach";
            // 
            // lWeight
            // 
            this.lWeight.AutoSize = true;
            this.lWeight.Location = new System.Drawing.Point(9, 173);
            this.lWeight.Name = "lWeight";
            this.lWeight.Size = new System.Drawing.Size(45, 15);
            this.lWeight.TabIndex = 27;
            this.lWeight.Text = "Weight";
            this.toolTip1.SetToolTip(this.lWeight, "Maximum conductance");
            // 
            // lFixedDuration
            // 
            this.lFixedDuration.AutoSize = true;
            this.lFixedDuration.Location = new System.Drawing.Point(252, 171);
            this.lFixedDuration.Name = "lFixedDuration";
            this.lFixedDuration.Size = new System.Drawing.Size(60, 15);
            this.lFixedDuration.TabIndex = 29;
            this.lFixedDuration.Text = "Fixed Dur.";
            this.toolTip1.SetToolTip(this.lFixedDuration, "(in ms) - if entered, distance/speed will not be used to calculate duration.");
            // 
            // lDelay
            // 
            this.lDelay.AutoSize = true;
            this.lDelay.Location = new System.Drawing.Point(252, 198);
            this.lDelay.Name = "lDelay";
            this.lDelay.Size = new System.Drawing.Size(36, 15);
            this.lDelay.TabIndex = 31;
            this.lDelay.Text = "Delay";
            this.toolTip1.SetToolTip(this.lDelay, "in ms");
            // 
            // ddSourcePool
            // 
            this.ddSourcePool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSourcePool.FormattingEnabled = true;
            this.ddSourcePool.Location = new System.Drawing.Point(105, 11);
            this.ddSourcePool.Name = "ddSourcePool";
            this.ddSourcePool.Size = new System.Drawing.Size(121, 23);
            this.ddSourcePool.TabIndex = 1;
            this.ddSourcePool.SelectedIndexChanged += new System.EventHandler(this.ddSourcePool_SelectedIndexChanged);
            // 
            // ddTargetPool
            // 
            this.ddTargetPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetPool.FormattingEnabled = true;
            this.ddTargetPool.Location = new System.Drawing.Point(105, 37);
            this.ddTargetPool.Name = "ddTargetPool";
            this.ddTargetPool.Size = new System.Drawing.Size(121, 23);
            this.ddTargetPool.TabIndex = 3;
            this.ddTargetPool.SelectedIndexChanged += new System.EventHandler(this.ddTargetPool_SelectedIndexChanged);
            // 
            // ddAxonReachMode
            // 
            this.ddAxonReachMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddAxonReachMode.FormattingEnabled = true;
            this.ddAxonReachMode.Location = new System.Drawing.Point(105, 63);
            this.ddAxonReachMode.Name = "ddAxonReachMode";
            this.ddAxonReachMode.Size = new System.Drawing.Size(121, 23);
            this.ddAxonReachMode.TabIndex = 5;
            this.ddAxonReachMode.SelectedIndexChanged += new System.EventHandler(this.ddAxonReachMode_SelectedIndexChanged);
            // 
            // ddConnectionType
            // 
            this.ddConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddConnectionType.FormattingEnabled = true;
            this.ddConnectionType.Items.AddRange(new object[] {
            "Synapse",
            "Gap",
            "NMJ"});
            this.ddConnectionType.Location = new System.Drawing.Point(105, 89);
            this.ddConnectionType.Name = "ddConnectionType";
            this.ddConnectionType.Size = new System.Drawing.Size(121, 23);
            this.ddConnectionType.TabIndex = 7;
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
            this.numConductance.Location = new System.Drawing.Point(105, 169);
            this.numConductance.Name = "numConductance";
            this.numConductance.Size = new System.Drawing.Size(122, 23);
            this.numConductance.TabIndex = 28;
            this.toolTip1.SetToolTip(this.numConductance, "Maximum conductance");
            // 
            // numDelay
            // 
            this.numDelay.DecimalPlaces = 2;
            this.numDelay.Location = new System.Drawing.Point(328, 195);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(83, 23);
            this.numDelay.TabIndex = 32;
            this.toolTip1.SetToolTip(this.numDelay, "in ms");
            // 
            // eFixedDuration
            // 
            this.eFixedDuration.Location = new System.Drawing.Point(328, 168);
            this.eFixedDuration.Name = "eFixedDuration";
            this.eFixedDuration.Size = new System.Drawing.Size(83, 23);
            this.eFixedDuration.TabIndex = 30;
            this.toolTip1.SetToolTip(this.eFixedDuration, "(in ms) - if entered, distance/speed will not be used to calculate duration.\r\n");
            // 
            // lTauD
            // 
            this.lTauD.AutoSize = true;
            this.lTauD.Location = new System.Drawing.Point(15, 20);
            this.lTauD.Name = "lTauD";
            this.lTauD.Size = new System.Drawing.Size(20, 15);
            this.lTauD.TabIndex = 0;
            this.lTauD.Text = "τd";
            this.toolTip1.SetToolTip(this.lTauD, "Decay time constant");
            // 
            // lTauR
            // 
            this.lTauR.AutoSize = true;
            this.lTauR.Location = new System.Drawing.Point(15, 49);
            this.lTauR.Name = "lTauR";
            this.lTauR.Size = new System.Drawing.Size(17, 15);
            this.lTauR.TabIndex = 1;
            this.lTauR.Text = "τr";
            this.toolTip1.SetToolTip(this.lTauR, "Rise time constant");
            // 
            // lEReversal
            // 
            this.lEReversal.AutoSize = true;
            this.lEReversal.Location = new System.Drawing.Point(15, 107);
            this.lEReversal.Name = "lEReversal";
            this.lEReversal.Size = new System.Drawing.Size(32, 15);
            this.lEReversal.TabIndex = 3;
            this.lEReversal.Text = "E rev";
            this.toolTip1.SetToolTip(this.lEReversal, "Reversal potential");
            // 
            // numTauD
            // 
            this.numTauD.DecimalPlaces = 2;
            this.numTauD.Location = new System.Drawing.Point(102, 18);
            this.numTauD.Name = "numTauD";
            this.numTauD.Size = new System.Drawing.Size(120, 23);
            this.numTauD.TabIndex = 0;
            this.toolTip1.SetToolTip(this.numTauD, "Decay time constant");
            this.numTauD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numTauR
            // 
            this.numTauR.DecimalPlaces = 2;
            this.numTauR.Location = new System.Drawing.Point(102, 47);
            this.numTauR.Name = "numTauR";
            this.numTauR.Size = new System.Drawing.Size(120, 23);
            this.numTauR.TabIndex = 1;
            this.toolTip1.SetToolTip(this.numTauR, "Rise time constant");
            // 
            // numEReversal
            // 
            this.numEReversal.DecimalPlaces = 2;
            this.numEReversal.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEReversal.Location = new System.Drawing.Point(102, 105);
            this.numEReversal.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numEReversal.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numEReversal.Name = "numEReversal";
            this.numEReversal.Size = new System.Drawing.Size(120, 23);
            this.numEReversal.TabIndex = 3;
            this.toolTip1.SetToolTip(this.numEReversal, "Reversal potential");
            // 
            // numAscReach
            // 
            this.numAscReach.DecimalPlaces = 2;
            this.numAscReach.Location = new System.Drawing.Point(329, 116);
            this.numAscReach.Name = "numAscReach";
            this.numAscReach.Size = new System.Drawing.Size(82, 23);
            this.numAscReach.TabIndex = 22;
            this.toolTip1.SetToolTip(this.numAscReach, "If body is composed of somites, this number represents the number of somites it c" +
        "an pass through. Otherwise, it represents the distance in the x-axis.");
            // 
            // numDescReach
            // 
            this.numDescReach.DecimalPlaces = 2;
            this.numDescReach.Location = new System.Drawing.Point(329, 143);
            this.numDescReach.Name = "numDescReach";
            this.numDescReach.Size = new System.Drawing.Size(82, 23);
            this.numDescReach.TabIndex = 24;
            this.toolTip1.SetToolTip(this.numDescReach, "If body is composed of somites, this number represents the number of somites it c" +
        "an pass through. Otherwise, it represents the distance in the x-axis.");
            // 
            // numMaxReach
            // 
            this.numMaxReach.DecimalPlaces = 2;
            this.numMaxReach.Location = new System.Drawing.Point(329, 89);
            this.numMaxReach.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMaxReach.Name = "numMaxReach";
            this.numMaxReach.Size = new System.Drawing.Size(82, 23);
            this.numMaxReach.TabIndex = 20;
            this.toolTip1.SetToolTip(this.numMaxReach, "the distance in 3-D, calculated according to the distance mode");
            this.numMaxReach.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // cbWithinSomite
            // 
            this.cbWithinSomite.AutoSize = true;
            this.cbWithinSomite.Checked = true;
            this.cbWithinSomite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWithinSomite.Location = new System.Drawing.Point(253, 12);
            this.cbWithinSomite.Name = "cbWithinSomite";
            this.cbWithinSomite.Size = new System.Drawing.Size(133, 19);
            this.cbWithinSomite.TabIndex = 15;
            this.cbWithinSomite.Text = "Within Same Somite";
            this.toolTip1.SetToolTip(this.cbWithinSomite, "valid if number of somites is defined for the model");
            this.cbWithinSomite.UseVisualStyleBackColor = true;
            this.cbWithinSomite.CheckedChanged += new System.EventHandler(this.cbWithinSomite_CheckedChanged);
            // 
            // cbOtherSomite
            // 
            this.cbOtherSomite.AutoSize = true;
            this.cbOtherSomite.Checked = true;
            this.cbOtherSomite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOtherSomite.Location = new System.Drawing.Point(253, 38);
            this.cbOtherSomite.Name = "cbOtherSomite";
            this.cbOtherSomite.Size = new System.Drawing.Size(127, 19);
            this.cbOtherSomite.TabIndex = 16;
            this.cbOtherSomite.Text = "To Different Somite";
            this.toolTip1.SetToolTip(this.cbOtherSomite, "valid if number of somites is defined for the model");
            this.cbOtherSomite.UseVisualStyleBackColor = true;
            this.cbOtherSomite.CheckedChanged += new System.EventHandler(this.cbOtherSomite_CheckedChanged);
            // 
            // numMinReach
            // 
            this.numMinReach.DecimalPlaces = 2;
            this.numMinReach.Location = new System.Drawing.Point(329, 62);
            this.numMinReach.Name = "numMinReach";
            this.numMinReach.Size = new System.Drawing.Size(82, 23);
            this.numMinReach.TabIndex = 18;
            // 
            // gSynapse
            // 
            this.gSynapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gSynapse.Controls.Add(this.numEReversal);
            this.gSynapse.Controls.Add(this.numVthreshold);
            this.gSynapse.Controls.Add(this.numTauR);
            this.gSynapse.Controls.Add(this.numTauD);
            this.gSynapse.Controls.Add(this.lEReversal);
            this.gSynapse.Controls.Add(this.lVThreshold);
            this.gSynapse.Controls.Add(this.lTauR);
            this.gSynapse.Controls.Add(this.lTauD);
            this.gSynapse.Location = new System.Drawing.Point(5, 306);
            this.gSynapse.Name = "gSynapse";
            this.gSynapse.Size = new System.Drawing.Size(239, 145);
            this.gSynapse.TabIndex = 33;
            this.gSynapse.TabStop = false;
            this.gSynapse.Text = "Synapse Parameters";
            // 
            // numVthreshold
            // 
            this.numVthreshold.DecimalPlaces = 2;
            this.numVthreshold.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVthreshold.Location = new System.Drawing.Point(102, 76);
            this.numVthreshold.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numVthreshold.Name = "numVthreshold";
            this.numVthreshold.Size = new System.Drawing.Size(120, 23);
            this.numVthreshold.TabIndex = 2;
            // 
            // lVThreshold
            // 
            this.lVThreshold.AutoSize = true;
            this.lVThreshold.Location = new System.Drawing.Point(15, 78);
            this.lVThreshold.Name = "lVThreshold";
            this.lVThreshold.Size = new System.Drawing.Size(69, 15);
            this.lVThreshold.TabIndex = 2;
            this.lVThreshold.Text = "Threshold V";
            // 
            // timeLineControl
            // 
            this.timeLineControl.AutoScroll = true;
            this.timeLineControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLineControl.Location = new System.Drawing.Point(3, 19);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(152, 157);
            this.timeLineControl.TabIndex = 0;
            // 
            // cbActive
            // 
            this.cbActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(167, 275);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(59, 19);
            this.cbActive.TabIndex = 14;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            this.cbActive.CheckedChanged += new System.EventHandler(this.cbActive_CheckedChanged);
            // 
            // numProbability
            // 
            this.numProbability.DecimalPlaces = 2;
            this.numProbability.Location = new System.Drawing.Point(105, 142);
            this.numProbability.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numProbability.Name = "numProbability";
            this.numProbability.Size = new System.Drawing.Size(121, 23);
            this.numProbability.TabIndex = 26;
            this.numProbability.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lProbability
            // 
            this.lProbability.AutoSize = true;
            this.lProbability.Location = new System.Drawing.Point(9, 147);
            this.lProbability.Name = "lProbability";
            this.lProbability.Size = new System.Drawing.Size(64, 15);
            this.lProbability.TabIndex = 25;
            this.lProbability.Text = "Probability";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(9, 199);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(39, 15);
            this.lName.TabIndex = 10;
            this.lName.Text = "Name";
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(9, 225);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(67, 15);
            this.lDescription.TabIndex = 12;
            this.lDescription.Text = "Description";
            // 
            // eName
            // 
            this.eName.Location = new System.Drawing.Point(105, 195);
            this.eName.Name = "eName";
            this.eName.Size = new System.Drawing.Size(121, 23);
            this.eName.TabIndex = 11;
            this.eName.Leave += new System.EventHandler(this.eName_Leave);
            // 
            // grTimeline
            // 
            this.grTimeline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grTimeline.Controls.Add(this.timeLineControl);
            this.grTimeline.Location = new System.Drawing.Point(250, 275);
            this.grTimeline.Name = "grTimeline";
            this.grTimeline.Size = new System.Drawing.Size(158, 179);
            this.grTimeline.TabIndex = 34;
            this.grTimeline.TabStop = false;
            this.grTimeline.Text = "Timeline";
            // 
            // ddDistanceMode
            // 
            this.ddDistanceMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddDistanceMode.FormattingEnabled = true;
            this.ddDistanceMode.Location = new System.Drawing.Point(105, 115);
            this.ddDistanceMode.Name = "ddDistanceMode";
            this.ddDistanceMode.Size = new System.Drawing.Size(121, 23);
            this.ddDistanceMode.TabIndex = 9;
            // 
            // lDistanceMode
            // 
            this.lDistanceMode.AutoSize = true;
            this.lDistanceMode.Location = new System.Drawing.Point(9, 121);
            this.lDistanceMode.Name = "lDistanceMode";
            this.lDistanceMode.Size = new System.Drawing.Size(86, 15);
            this.lDistanceMode.TabIndex = 8;
            this.lDistanceMode.Text = "Distance Mode";
            // 
            // lMaxReach
            // 
            this.lMaxReach.AutoSize = true;
            this.lMaxReach.Location = new System.Drawing.Point(253, 92);
            this.lMaxReach.Name = "lMaxReach";
            this.lMaxReach.Size = new System.Drawing.Size(65, 15);
            this.lMaxReach.TabIndex = 19;
            this.lMaxReach.Text = "Max Reach";
            // 
            // eDescription
            // 
            this.eDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eDescription.Location = new System.Drawing.Point(105, 222);
            this.eDescription.Name = "eDescription";
            this.eDescription.Size = new System.Drawing.Size(306, 47);
            this.eDescription.TabIndex = 13;
            this.eDescription.Text = "";
            // 
            // InterPoolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.eDescription);
            this.Controls.Add(this.cbOtherSomite);
            this.Controls.Add(this.cbWithinSomite);
            this.Controls.Add(this.numMaxReach);
            this.Controls.Add(this.lMaxReach);
            this.Controls.Add(this.lDistanceMode);
            this.Controls.Add(this.ddDistanceMode);
            this.Controls.Add(this.grTimeline);
            this.Controls.Add(this.eName);
            this.Controls.Add(this.lDescription);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.lProbability);
            this.Controls.Add(this.numProbability);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.gSynapse);
            this.Controls.Add(this.eFixedDuration);
            this.Controls.Add(this.numDelay);
            this.Controls.Add(this.numConductance);
            this.Controls.Add(this.numDescReach);
            this.Controls.Add(this.numAscReach);
            this.Controls.Add(this.numMinReach);
            this.Controls.Add(this.ddConnectionType);
            this.Controls.Add(this.ddAxonReachMode);
            this.Controls.Add(this.ddTargetPool);
            this.Controls.Add(this.ddSourcePool);
            this.Controls.Add(this.lDelay);
            this.Controls.Add(this.lFixedDuration);
            this.Controls.Add(this.lWeight);
            this.Controls.Add(this.lDescReach);
            this.Controls.Add(this.lAscReach);
            this.Controls.Add(this.lMinReach);
            this.Controls.Add(this.lConnectionType);
            this.Controls.Add(this.lAxonReachMode);
            this.Controls.Add(this.lTargetPool);
            this.Controls.Add(this.lSourcePool);
            this.Name = "InterPoolControl";
            this.Size = new System.Drawing.Size(423, 468);
            ((System.ComponentModel.ISupportInitialize)(this.numConductance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEReversal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAscReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDescReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinReach)).EndInit();
            this.gSynapse.ResumeLayout(false);
            this.gSynapse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVthreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).EndInit();
            this.grTimeline.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lSourcePool;
        private Label lTargetPool;
        private Label lAxonReachMode;
        private Label lConnectionType;
        private Label lMinReach;
        private Label lAscReach;
        private Label lDescReach;
        private Label lWeight;
        private Label lFixedDuration;
        private Label lDelay;
        private ComboBox ddSourcePool;
        private ComboBox ddTargetPool;
        private ComboBox ddAxonReachMode;
        private ComboBox ddConnectionType;
        private ToolTip toolTip1;
        private NumericUpDown numMinReach;
        private NumericUpDown numAscReach;
        private NumericUpDown numDescReach;
        private NumericUpDown numConductance;
        private NumericUpDown numDelay;
        private TextBox eFixedDuration;
        private GroupBox gSynapse;
        private Label lEReversal;
        private Label lVThreshold;
        private Label lTauR;
        private Label lTauD;
        private NumericUpDown numEReversal;
        private NumericUpDown numVthreshold;
        private NumericUpDown numTauR;
        private NumericUpDown numTauD;
        private TimeLineControl timeLineControl;
        private CheckBox cbActive;
        private NumericUpDown numProbability;
        private Label lProbability;
        private Label lName;
        private Label lDescription;
        private TextBox eName;
        private GroupBox grTimeline;
        private ComboBox ddDistanceMode;
        private Label lDistanceMode;
        private NumericUpDown numMaxReach;
        private Label lMaxReach;
        private CheckBox cbWithinSomite;
        private CheckBox cbOtherSomite;
        private RichTextBox eDescription;
    }
}
