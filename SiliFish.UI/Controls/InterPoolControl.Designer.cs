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
            this.lMaxReach = new System.Windows.Forms.Label();
            this.numMedialReach = new System.Windows.Forms.NumericUpDown();
            this.numLateralReach = new System.Windows.Forms.NumericUpDown();
            this.lMedialReach = new System.Windows.Forms.Label();
            this.lLateralReach = new System.Windows.Forms.Label();
            this.numVentralReach = new System.Windows.Forms.NumericUpDown();
            this.numDorsalReach = new System.Windows.Forms.NumericUpDown();
            this.lVentralReach = new System.Windows.Forms.Label();
            this.lDorsalReach = new System.Windows.Forms.Label();
            this.lMaxIncoming = new System.Windows.Forms.Label();
            this.numMaxIncoming = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numMaxOutgoing = new System.Windows.Forms.NumericUpDown();
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
            this.eDescription = new System.Windows.Forms.RichTextBox();
            this.grReach = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numConductance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEReversal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAscReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDescReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMedialReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLateralReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVentralReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDorsalReach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxIncoming)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxOutgoing)).BeginInit();
            this.gSynapse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVthreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).BeginInit();
            this.grTimeline.SuspendLayout();
            this.grReach.SuspendLayout();
            this.SuspendLayout();
            // 
            // lSourcePool
            // 
            this.lSourcePool.AutoSize = true;
            this.lSourcePool.Location = new System.Drawing.Point(9, 14);
            this.lSourcePool.Name = "lSourcePool";
            this.lSourcePool.Size = new System.Drawing.Size(70, 15);
            this.lSourcePool.TabIndex = 4;
            this.lSourcePool.Text = "Source Pool";
            // 
            // lTargetPool
            // 
            this.lTargetPool.AutoSize = true;
            this.lTargetPool.Location = new System.Drawing.Point(9, 40);
            this.lTargetPool.Name = "lTargetPool";
            this.lTargetPool.Size = new System.Drawing.Size(66, 15);
            this.lTargetPool.TabIndex = 6;
            this.lTargetPool.Text = "Target Pool";
            // 
            // lAxonReachMode
            // 
            this.lAxonReachMode.AutoSize = true;
            this.lAxonReachMode.Location = new System.Drawing.Point(9, 66);
            this.lAxonReachMode.Name = "lAxonReachMode";
            this.lAxonReachMode.Size = new System.Drawing.Size(70, 15);
            this.lAxonReachMode.TabIndex = 8;
            this.lAxonReachMode.Text = "Axon Reach";
            // 
            // lConnectionType
            // 
            this.lConnectionType.AutoSize = true;
            this.lConnectionType.Location = new System.Drawing.Point(9, 92);
            this.lConnectionType.Name = "lConnectionType";
            this.lConnectionType.Size = new System.Drawing.Size(96, 15);
            this.lConnectionType.TabIndex = 10;
            this.lConnectionType.Text = "Connection Type";
            // 
            // lMinReach
            // 
            this.lMinReach.AutoSize = true;
            this.lMinReach.Location = new System.Drawing.Point(6, 51);
            this.lMinReach.Name = "lMinReach";
            this.lMinReach.Size = new System.Drawing.Size(28, 15);
            this.lMinReach.TabIndex = 2;
            this.lMinReach.Text = "Min";
            this.toolTip1.SetToolTip(this.lMinReach, "The distance in 3-D, calculated according to the selected \'distance mode\'");
            // 
            // lAscReach
            // 
            this.lAscReach.AutoSize = true;
            this.lAscReach.Location = new System.Drawing.Point(6, 77);
            this.lAscReach.Name = "lAscReach";
            this.lAscReach.Size = new System.Drawing.Size(29, 15);
            this.lAscReach.TabIndex = 6;
            this.lAscReach.Text = "Asc.";
            this.toolTip1.SetToolTip(this.lAscReach, "If body is composed of somites, this number represents the number of somites it c" +
        "an pass through. Otherwise, it represents the distance in the x-axis.");
            // 
            // lDescReach
            // 
            this.lDescReach.AutoSize = true;
            this.lDescReach.Location = new System.Drawing.Point(125, 77);
            this.lDescReach.Name = "lDescReach";
            this.lDescReach.Size = new System.Drawing.Size(35, 15);
            this.lDescReach.TabIndex = 8;
            this.lDescReach.Text = "Desc.";
            this.toolTip1.SetToolTip(this.lDescReach, "If body is composed of somites, this number represents the number of somites it c" +
        "an pass through. Otherwise, it represents the distance in the x-axis.");
            // 
            // lWeight
            // 
            this.lWeight.AutoSize = true;
            this.lWeight.Location = new System.Drawing.Point(9, 196);
            this.lWeight.Name = "lWeight";
            this.lWeight.Size = new System.Drawing.Size(45, 15);
            this.lWeight.TabIndex = 18;
            this.lWeight.Text = "Weight";
            this.toolTip1.SetToolTip(this.lWeight, "Maximum conductance");
            // 
            // lFixedDuration
            // 
            this.lFixedDuration.AutoSize = true;
            this.lFixedDuration.Location = new System.Drawing.Point(9, 224);
            this.lFixedDuration.Name = "lFixedDuration";
            this.lFixedDuration.Size = new System.Drawing.Size(60, 15);
            this.lFixedDuration.TabIndex = 20;
            this.lFixedDuration.Text = "Fixed Dur.";
            this.toolTip1.SetToolTip(this.lFixedDuration, "(in ms) - if entered, distance/speed will not be used to calculate duration.");
            // 
            // lDelay
            // 
            this.lDelay.AutoSize = true;
            this.lDelay.Location = new System.Drawing.Point(9, 250);
            this.lDelay.Name = "lDelay";
            this.lDelay.Size = new System.Drawing.Size(36, 15);
            this.lDelay.TabIndex = 22;
            this.lDelay.Text = "Delay";
            this.toolTip1.SetToolTip(this.lDelay, "in ms");
            // 
            // ddSourcePool
            // 
            this.ddSourcePool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSourcePool.FormattingEnabled = true;
            this.ddSourcePool.Location = new System.Drawing.Point(105, 11);
            this.ddSourcePool.Name = "ddSourcePool";
            this.ddSourcePool.Size = new System.Drawing.Size(128, 23);
            this.ddSourcePool.TabIndex = 5;
            this.ddSourcePool.SelectedIndexChanged += new System.EventHandler(this.ddSourcePool_SelectedIndexChanged);
            // 
            // ddTargetPool
            // 
            this.ddTargetPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetPool.FormattingEnabled = true;
            this.ddTargetPool.Location = new System.Drawing.Point(105, 37);
            this.ddTargetPool.Name = "ddTargetPool";
            this.ddTargetPool.Size = new System.Drawing.Size(128, 23);
            this.ddTargetPool.TabIndex = 7;
            this.ddTargetPool.SelectedIndexChanged += new System.EventHandler(this.ddTargetPool_SelectedIndexChanged);
            // 
            // ddAxonReachMode
            // 
            this.ddAxonReachMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddAxonReachMode.FormattingEnabled = true;
            this.ddAxonReachMode.Location = new System.Drawing.Point(105, 63);
            this.ddAxonReachMode.Name = "ddAxonReachMode";
            this.ddAxonReachMode.Size = new System.Drawing.Size(128, 23);
            this.ddAxonReachMode.TabIndex = 9;
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
            this.numConductance.Location = new System.Drawing.Point(105, 193);
            this.numConductance.Name = "numConductance";
            this.numConductance.Size = new System.Drawing.Size(66, 23);
            this.numConductance.TabIndex = 19;
            this.toolTip1.SetToolTip(this.numConductance, "Maximum conductance");
            // 
            // numDelay
            // 
            this.numDelay.DecimalPlaces = 2;
            this.numDelay.Location = new System.Drawing.Point(105, 245);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(66, 23);
            this.numDelay.TabIndex = 23;
            this.toolTip1.SetToolTip(this.numDelay, "in ms");
            // 
            // eFixedDuration
            // 
            this.eFixedDuration.Location = new System.Drawing.Point(105, 219);
            this.eFixedDuration.Name = "eFixedDuration";
            this.eFixedDuration.Size = new System.Drawing.Size(66, 23);
            this.eFixedDuration.TabIndex = 21;
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
            this.lTauR.TabIndex = 2;
            this.lTauR.Text = "τr";
            this.toolTip1.SetToolTip(this.lTauR, "Rise time constant");
            // 
            // lEReversal
            // 
            this.lEReversal.AutoSize = true;
            this.lEReversal.Location = new System.Drawing.Point(15, 107);
            this.lEReversal.Name = "lEReversal";
            this.lEReversal.Size = new System.Drawing.Size(32, 15);
            this.lEReversal.TabIndex = 6;
            this.lEReversal.Text = "E rev";
            this.toolTip1.SetToolTip(this.lEReversal, "Reversal potential");
            // 
            // numTauD
            // 
            this.numTauD.DecimalPlaces = 2;
            this.numTauD.Location = new System.Drawing.Point(102, 18);
            this.numTauD.Name = "numTauD";
            this.numTauD.Size = new System.Drawing.Size(120, 23);
            this.numTauD.TabIndex = 1;
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
            this.numTauR.TabIndex = 3;
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
            this.numEReversal.TabIndex = 7;
            this.toolTip1.SetToolTip(this.numEReversal, "Reversal potential");
            // 
            // numAscReach
            // 
            this.numAscReach.DecimalPlaces = 2;
            this.numAscReach.Location = new System.Drawing.Point(50, 71);
            this.numAscReach.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numAscReach.Name = "numAscReach";
            this.numAscReach.Size = new System.Drawing.Size(66, 23);
            this.numAscReach.TabIndex = 7;
            this.toolTip1.SetToolTip(this.numAscReach, "If body is composed of somites, this number represents the number of somites it c" +
        "an pass through. Otherwise, it represents the distance in the x-axis.");
            // 
            // numDescReach
            // 
            this.numDescReach.DecimalPlaces = 2;
            this.numDescReach.Location = new System.Drawing.Point(173, 71);
            this.numDescReach.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numDescReach.Name = "numDescReach";
            this.numDescReach.Size = new System.Drawing.Size(66, 23);
            this.numDescReach.TabIndex = 9;
            this.toolTip1.SetToolTip(this.numDescReach, "If body is composed of somites, this number represents the number of somites it c" +
        "an pass through. Otherwise, it represents the distance in the x-axis.");
            // 
            // numMaxReach
            // 
            this.numMaxReach.DecimalPlaces = 2;
            this.numMaxReach.Location = new System.Drawing.Point(173, 45);
            this.numMaxReach.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxReach.Name = "numMaxReach";
            this.numMaxReach.Size = new System.Drawing.Size(66, 23);
            this.numMaxReach.TabIndex = 5;
            this.toolTip1.SetToolTip(this.numMaxReach, "The distance in 3-D, calculated according to the selected \'distance mode\'");
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
            this.cbWithinSomite.Location = new System.Drawing.Point(6, 151);
            this.cbWithinSomite.Name = "cbWithinSomite";
            this.cbWithinSomite.Size = new System.Drawing.Size(133, 19);
            this.cbWithinSomite.TabIndex = 18;
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
            this.cbOtherSomite.Location = new System.Drawing.Point(6, 172);
            this.cbOtherSomite.Name = "cbOtherSomite";
            this.cbOtherSomite.Size = new System.Drawing.Size(127, 19);
            this.cbOtherSomite.TabIndex = 19;
            this.cbOtherSomite.Text = "To Different Somite";
            this.toolTip1.SetToolTip(this.cbOtherSomite, "valid if number of somites is defined for the model");
            this.cbOtherSomite.UseVisualStyleBackColor = true;
            this.cbOtherSomite.CheckedChanged += new System.EventHandler(this.cbOtherSomite_CheckedChanged);
            // 
            // numMinReach
            // 
            this.numMinReach.DecimalPlaces = 2;
            this.numMinReach.Location = new System.Drawing.Point(50, 45);
            this.numMinReach.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMinReach.Name = "numMinReach";
            this.numMinReach.Size = new System.Drawing.Size(66, 23);
            this.numMinReach.TabIndex = 3;
            this.toolTip1.SetToolTip(this.numMinReach, "The distance in 3-D, calculated according to the selected \'distance mode\'");
            // 
            // lMaxReach
            // 
            this.lMaxReach.AutoSize = true;
            this.lMaxReach.Location = new System.Drawing.Point(125, 51);
            this.lMaxReach.Name = "lMaxReach";
            this.lMaxReach.Size = new System.Drawing.Size(30, 15);
            this.lMaxReach.TabIndex = 4;
            this.lMaxReach.Text = "Max";
            this.toolTip1.SetToolTip(this.lMaxReach, "The distance in 3-D, calculated according to the selected \'distance mode\'");
            // 
            // numMedialReach
            // 
            this.numMedialReach.DecimalPlaces = 2;
            this.numMedialReach.Location = new System.Drawing.Point(173, 97);
            this.numMedialReach.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMedialReach.Name = "numMedialReach";
            this.numMedialReach.Size = new System.Drawing.Size(66, 23);
            this.numMedialReach.TabIndex = 13;
            this.toolTip1.SetToolTip(this.numMedialReach, "The medial max reach in the y-axis.");
            // 
            // numLateralReach
            // 
            this.numLateralReach.DecimalPlaces = 2;
            this.numLateralReach.Location = new System.Drawing.Point(50, 97);
            this.numLateralReach.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLateralReach.Name = "numLateralReach";
            this.numLateralReach.Size = new System.Drawing.Size(66, 23);
            this.numLateralReach.TabIndex = 11;
            this.toolTip1.SetToolTip(this.numLateralReach, "The lateral max reach in the y-axis.");
            // 
            // lMedialReach
            // 
            this.lMedialReach.AutoSize = true;
            this.lMedialReach.Location = new System.Drawing.Point(125, 103);
            this.lMedialReach.Name = "lMedialReach";
            this.lMedialReach.Size = new System.Drawing.Size(43, 15);
            this.lMedialReach.TabIndex = 12;
            this.lMedialReach.Text = "Medial";
            this.toolTip1.SetToolTip(this.lMedialReach, "The medial max reach in the y-axis.");
            // 
            // lLateralReach
            // 
            this.lLateralReach.AutoSize = true;
            this.lLateralReach.Location = new System.Drawing.Point(6, 103);
            this.lLateralReach.Name = "lLateralReach";
            this.lLateralReach.Size = new System.Drawing.Size(42, 15);
            this.lLateralReach.TabIndex = 10;
            this.lLateralReach.Text = "Lateral";
            this.toolTip1.SetToolTip(this.lLateralReach, "The lateral max reach in the y-axis.");
            // 
            // numVentralReach
            // 
            this.numVentralReach.DecimalPlaces = 2;
            this.numVentralReach.Location = new System.Drawing.Point(173, 123);
            this.numVentralReach.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numVentralReach.Name = "numVentralReach";
            this.numVentralReach.Size = new System.Drawing.Size(66, 23);
            this.numVentralReach.TabIndex = 17;
            this.toolTip1.SetToolTip(this.numVentralReach, "The ventral max reach in the z-axis.");
            // 
            // numDorsalReach
            // 
            this.numDorsalReach.DecimalPlaces = 2;
            this.numDorsalReach.Location = new System.Drawing.Point(50, 123);
            this.numDorsalReach.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numDorsalReach.Name = "numDorsalReach";
            this.numDorsalReach.Size = new System.Drawing.Size(66, 23);
            this.numDorsalReach.TabIndex = 15;
            this.toolTip1.SetToolTip(this.numDorsalReach, "The dorsal max reach in the z-axis.");
            this.numDorsalReach.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // lVentralReach
            // 
            this.lVentralReach.AutoSize = true;
            this.lVentralReach.Location = new System.Drawing.Point(125, 129);
            this.lVentralReach.Name = "lVentralReach";
            this.lVentralReach.Size = new System.Drawing.Size(43, 15);
            this.lVentralReach.TabIndex = 16;
            this.lVentralReach.Text = "Ventral";
            this.toolTip1.SetToolTip(this.lVentralReach, "The ventral max reach in the z-axis.");
            // 
            // lDorsalReach
            // 
            this.lDorsalReach.AutoSize = true;
            this.lDorsalReach.Location = new System.Drawing.Point(6, 129);
            this.lDorsalReach.Name = "lDorsalReach";
            this.lDorsalReach.Size = new System.Drawing.Size(40, 15);
            this.lDorsalReach.TabIndex = 14;
            this.lDorsalReach.Text = "Dorsal";
            this.toolTip1.SetToolTip(this.lDorsalReach, "The dorsal max reach in the z-axis.");
            // 
            // lMaxIncoming
            // 
            this.lMaxIncoming.AutoSize = true;
            this.lMaxIncoming.Location = new System.Drawing.Point(9, 118);
            this.lMaxIncoming.Name = "lMaxIncoming";
            this.lMaxIncoming.Size = new System.Drawing.Size(84, 15);
            this.lMaxIncoming.TabIndex = 12;
            this.lMaxIncoming.Text = "Max Incoming";
            this.toolTip1.SetToolTip(this.lMaxIncoming, "The number of maximum connections the target cell can have from the same source p" +
        "ool.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMaxIncoming
            // 
            this.numMaxIncoming.Location = new System.Drawing.Point(105, 115);
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
            this.label1.Location = new System.Drawing.Point(9, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Max Outgoing";
            this.toolTip1.SetToolTip(this.label1, "The number of maximum connections the source cell can have to the same target poo" +
        "l.\r\nEnter 0 if there is no such limit.\r\n");
            // 
            // numMaxOutgoing
            // 
            this.numMaxOutgoing.Location = new System.Drawing.Point(105, 141);
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
            this.gSynapse.Location = new System.Drawing.Point(5, 405);
            this.gSynapse.Name = "gSynapse";
            this.gSynapse.Size = new System.Drawing.Size(239, 145);
            this.gSynapse.TabIndex = 2;
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
            this.numVthreshold.TabIndex = 5;
            // 
            // lVThreshold
            // 
            this.lVThreshold.AutoSize = true;
            this.lVThreshold.Location = new System.Drawing.Point(15, 78);
            this.lVThreshold.Name = "lVThreshold";
            this.lVThreshold.Size = new System.Drawing.Size(69, 15);
            this.lVThreshold.TabIndex = 4;
            this.lVThreshold.Text = "Threshold V";
            // 
            // timeLineControl
            // 
            this.timeLineControl.AutoScroll = true;
            this.timeLineControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLineControl.Location = new System.Drawing.Point(3, 19);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(228, 157);
            this.timeLineControl.TabIndex = 0;
            // 
            // cbActive
            // 
            this.cbActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(105, 372);
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
            this.numProbability.Location = new System.Drawing.Point(105, 167);
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
            this.lProbability.Location = new System.Drawing.Point(9, 170);
            this.lProbability.Name = "lProbability";
            this.lProbability.Size = new System.Drawing.Size(64, 15);
            this.lProbability.TabIndex = 16;
            this.lProbability.Text = "Probability";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(9, 276);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(39, 15);
            this.lName.TabIndex = 24;
            this.lName.Text = "Name";
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(9, 302);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(67, 15);
            this.lDescription.TabIndex = 26;
            this.lDescription.Text = "Description";
            // 
            // eName
            // 
            this.eName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eName.Location = new System.Drawing.Point(105, 271);
            this.eName.Name = "eName";
            this.eName.Size = new System.Drawing.Size(379, 23);
            this.eName.TabIndex = 25;
            this.eName.Leave += new System.EventHandler(this.eName_Leave);
            // 
            // grTimeline
            // 
            this.grTimeline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grTimeline.Controls.Add(this.timeLineControl);
            this.grTimeline.Location = new System.Drawing.Point(250, 371);
            this.grTimeline.Name = "grTimeline";
            this.grTimeline.Size = new System.Drawing.Size(234, 179);
            this.grTimeline.TabIndex = 3;
            this.grTimeline.TabStop = false;
            this.grTimeline.Text = "Timeline";
            // 
            // ddDistanceMode
            // 
            this.ddDistanceMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.eDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eDescription.Location = new System.Drawing.Point(105, 297);
            this.eDescription.Name = "eDescription";
            this.eDescription.Size = new System.Drawing.Size(379, 71);
            this.eDescription.TabIndex = 27;
            this.eDescription.Text = "";
            // 
            // grReach
            // 
            this.grReach.Controls.Add(this.ddDistanceMode);
            this.grReach.Controls.Add(this.numVentralReach);
            this.grReach.Controls.Add(this.cbOtherSomite);
            this.grReach.Controls.Add(this.lDistanceMode);
            this.grReach.Controls.Add(this.cbWithinSomite);
            this.grReach.Controls.Add(this.numDorsalReach);
            this.grReach.Controls.Add(this.numMinReach);
            this.grReach.Controls.Add(this.lVentralReach);
            this.grReach.Controls.Add(this.lMinReach);
            this.grReach.Controls.Add(this.lDorsalReach);
            this.grReach.Controls.Add(this.lAscReach);
            this.grReach.Controls.Add(this.numMedialReach);
            this.grReach.Controls.Add(this.lDescReach);
            this.grReach.Controls.Add(this.numLateralReach);
            this.grReach.Controls.Add(this.numAscReach);
            this.grReach.Controls.Add(this.lMedialReach);
            this.grReach.Controls.Add(this.numDescReach);
            this.grReach.Controls.Add(this.lLateralReach);
            this.grReach.Controls.Add(this.lMaxReach);
            this.grReach.Controls.Add(this.numMaxReach);
            this.grReach.Location = new System.Drawing.Point(241, 21);
            this.grReach.Name = "grReach";
            this.grReach.Size = new System.Drawing.Size(243, 195);
            this.grReach.TabIndex = 1;
            this.grReach.TabStop = false;
            this.grReach.Text = "Reach";
            // 
            // InterPoolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numMaxOutgoing);
            this.Controls.Add(this.lMaxIncoming);
            this.Controls.Add(this.numMaxIncoming);
            this.Controls.Add(this.grReach);
            this.Controls.Add(this.eDescription);
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
            this.Controls.Add(this.ddConnectionType);
            this.Controls.Add(this.ddAxonReachMode);
            this.Controls.Add(this.ddTargetPool);
            this.Controls.Add(this.ddSourcePool);
            this.Controls.Add(this.lDelay);
            this.Controls.Add(this.lFixedDuration);
            this.Controls.Add(this.lWeight);
            this.Controls.Add(this.lConnectionType);
            this.Controls.Add(this.lAxonReachMode);
            this.Controls.Add(this.lTargetPool);
            this.Controls.Add(this.lSourcePool);
            this.Name = "InterPoolControl";
            this.Size = new System.Drawing.Size(493, 559);
            ((System.ComponentModel.ISupportInitialize)(this.numConductance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEReversal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAscReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDescReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMedialReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLateralReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVentralReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDorsalReach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxIncoming)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxOutgoing)).EndInit();
            this.gSynapse.ResumeLayout(false);
            this.gSynapse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVthreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).EndInit();
            this.grTimeline.ResumeLayout(false);
            this.grReach.ResumeLayout(false);
            this.grReach.PerformLayout();
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
        private NumericUpDown numMedialReach;
        private NumericUpDown numLateralReach;
        private Label lMedialReach;
        private Label lLateralReach;
        private NumericUpDown numVentralReach;
        private NumericUpDown numDorsalReach;
        private Label lVentralReach;
        private Label lDorsalReach;
        private GroupBox grReach;
        private Label lMaxIncoming;
        private NumericUpDown numMaxIncoming;
        private Label label1;
        private NumericUpDown numMaxOutgoing;
    }
}
