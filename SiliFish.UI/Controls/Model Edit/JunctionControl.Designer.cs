namespace SiliFish.UI.Controls
{
    partial class JunctionControl
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
            this.lConnectionType = new System.Windows.Forms.Label();
            this.lWeight = new System.Windows.Forms.Label();
            this.lFixedDuration = new System.Windows.Forms.Label();
            this.lDelay = new System.Windows.Forms.Label();
            this.ddSourcePool = new System.Windows.Forms.ComboBox();
            this.ddConnectionType = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numConductance = new System.Windows.Forms.NumericUpDown();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.eFixedDuration = new System.Windows.Forms.TextBox();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.timeLineControl = new SiliFish.UI.Controls.TimeLineControl();
            this.ddDistanceMode = new System.Windows.Forms.ComboBox();
            this.lDistanceMode = new System.Windows.Forms.Label();
            this.lSourceCell = new System.Windows.Forms.Label();
            this.ddSourceCell = new System.Windows.Forms.ComboBox();
            this.lTargetCell = new System.Windows.Forms.Label();
            this.ddTargetCell = new System.Windows.Forms.ComboBox();
            this.ddTargetPool = new System.Windows.Forms.ComboBox();
            this.gSynapse = new System.Windows.Forms.GroupBox();
            this.synapseControl = new SiliFish.UI.Controls.SynapseControl();
            ((System.ComponentModel.ISupportInitialize)(this.numConductance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.gSynapse.SuspendLayout();
            this.SuspendLayout();
            // 
            // lSourcePool
            // 
            this.lSourcePool.AutoSize = true;
            this.lSourcePool.Location = new System.Drawing.Point(12, 12);
            this.lSourcePool.Name = "lSourcePool";
            this.lSourcePool.Size = new System.Drawing.Size(70, 15);
            this.lSourcePool.TabIndex = 4;
            this.lSourcePool.Text = "Source Pool";
            // 
            // lTargetPool
            // 
            this.lTargetPool.AutoSize = true;
            this.lTargetPool.Location = new System.Drawing.Point(12, 64);
            this.lTargetPool.Name = "lTargetPool";
            this.lTargetPool.Size = new System.Drawing.Size(66, 15);
            this.lTargetPool.TabIndex = 6;
            this.lTargetPool.Text = "Target Pool";
            // 
            // lConnectionType
            // 
            this.lConnectionType.AutoSize = true;
            this.lConnectionType.Location = new System.Drawing.Point(12, 116);
            this.lConnectionType.Name = "lConnectionType";
            this.lConnectionType.Size = new System.Drawing.Size(96, 15);
            this.lConnectionType.TabIndex = 10;
            this.lConnectionType.Text = "Connection Type";
            // 
            // lWeight
            // 
            this.lWeight.AutoSize = true;
            this.lWeight.Location = new System.Drawing.Point(12, 168);
            this.lWeight.Name = "lWeight";
            this.lWeight.Size = new System.Drawing.Size(45, 15);
            this.lWeight.TabIndex = 18;
            this.lWeight.Text = "Weight";
            this.toolTip1.SetToolTip(this.lWeight, "Maximum conductance");
            // 
            // lFixedDuration
            // 
            this.lFixedDuration.AutoSize = true;
            this.lFixedDuration.Location = new System.Drawing.Point(12, 194);
            this.lFixedDuration.Name = "lFixedDuration";
            this.lFixedDuration.Size = new System.Drawing.Size(87, 15);
            this.lFixedDuration.TabIndex = 20;
            this.lFixedDuration.Text = "Fixed Dur. (ms)";
            this.toolTip1.SetToolTip(this.lFixedDuration, "If entered, distance/speed and delay values will not be used to calculate duratio" +
        "n.");
            // 
            // lDelay
            // 
            this.lDelay.AutoSize = true;
            this.lDelay.Location = new System.Drawing.Point(12, 220);
            this.lDelay.Name = "lDelay";
            this.lDelay.Size = new System.Drawing.Size(63, 15);
            this.lDelay.TabIndex = 22;
            this.lDelay.Text = "Delay (ms)";
            this.toolTip1.SetToolTip(this.lDelay, "The time that will be added to the duratino calculated by the distance and conduc" +
        "tion velocity.");
            // 
            // ddSourcePool
            // 
            this.ddSourcePool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddSourcePool.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddSourcePool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSourcePool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddSourcePool.FormattingEnabled = true;
            this.ddSourcePool.Location = new System.Drawing.Point(120, 10);
            this.ddSourcePool.Name = "ddSourcePool";
            this.ddSourcePool.Size = new System.Drawing.Size(129, 23);
            this.ddSourcePool.TabIndex = 5;
            this.ddSourcePool.SelectedIndexChanged += new System.EventHandler(this.ddSourcePool_SelectedIndexChanged);
            // 
            // ddConnectionType
            // 
            this.ddConnectionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddConnectionType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddConnectionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddConnectionType.FormattingEnabled = true;
            this.ddConnectionType.Items.AddRange(new object[] {
            "Synapse",
            "Gap",
            "NMJ"});
            this.ddConnectionType.Location = new System.Drawing.Point(120, 114);
            this.ddConnectionType.Name = "ddConnectionType";
            this.ddConnectionType.Size = new System.Drawing.Size(129, 23);
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
            this.numConductance.Location = new System.Drawing.Point(120, 166);
            this.numConductance.Name = "numConductance";
            this.numConductance.Size = new System.Drawing.Size(66, 23);
            this.numConductance.TabIndex = 19;
            this.toolTip1.SetToolTip(this.numConductance, "Maximum conductance");
            // 
            // numDelay
            // 
            this.numDelay.DecimalPlaces = 2;
            this.numDelay.Location = new System.Drawing.Point(120, 218);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(66, 23);
            this.numDelay.TabIndex = 23;
            this.toolTip1.SetToolTip(this.numDelay, "The time that will be added to the duratino calculated by the distance and conduc" +
        "tion velocity.");
            // 
            // eFixedDuration
            // 
            this.eFixedDuration.Location = new System.Drawing.Point(120, 192);
            this.eFixedDuration.Name = "eFixedDuration";
            this.eFixedDuration.Size = new System.Drawing.Size(66, 23);
            this.eFixedDuration.TabIndex = 21;
            this.toolTip1.SetToolTip(this.eFixedDuration, "If entered, distance/speed and delay values will not be used to calculate duratio" +
        "n.\r\n");
            // 
            // cbActive
            // 
            this.cbActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbActive.AutoSize = true;
            this.cbActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(12, 246);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(59, 19);
            this.cbActive.TabIndex = 0;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // timeLineControl
            // 
            this.timeLineControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLineControl.Location = new System.Drawing.Point(3, 437);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(245, 125);
            this.timeLineControl.TabIndex = 0;
            // 
            // ddDistanceMode
            // 
            this.ddDistanceMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddDistanceMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddDistanceMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddDistanceMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddDistanceMode.FormattingEnabled = true;
            this.ddDistanceMode.Location = new System.Drawing.Point(120, 140);
            this.ddDistanceMode.Name = "ddDistanceMode";
            this.ddDistanceMode.Size = new System.Drawing.Size(129, 23);
            this.ddDistanceMode.TabIndex = 29;
            // 
            // lDistanceMode
            // 
            this.lDistanceMode.AutoSize = true;
            this.lDistanceMode.Location = new System.Drawing.Point(12, 142);
            this.lDistanceMode.Name = "lDistanceMode";
            this.lDistanceMode.Size = new System.Drawing.Size(86, 15);
            this.lDistanceMode.TabIndex = 28;
            this.lDistanceMode.Text = "Distance Mode";
            // 
            // lSourceCell
            // 
            this.lSourceCell.AutoSize = true;
            this.lSourceCell.Location = new System.Drawing.Point(12, 38);
            this.lSourceCell.Name = "lSourceCell";
            this.lSourceCell.Size = new System.Drawing.Size(66, 15);
            this.lSourceCell.TabIndex = 30;
            this.lSourceCell.Text = "Source Cell";
            // 
            // ddSourceCell
            // 
            this.ddSourceCell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddSourceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddSourceCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSourceCell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddSourceCell.FormattingEnabled = true;
            this.ddSourceCell.Location = new System.Drawing.Point(120, 36);
            this.ddSourceCell.Name = "ddSourceCell";
            this.ddSourceCell.Size = new System.Drawing.Size(129, 23);
            this.ddSourceCell.TabIndex = 31;
            this.ddSourceCell.SelectedIndexChanged += new System.EventHandler(this.ddSourceCell_SelectedIndexChanged);
            // 
            // lTargetCell
            // 
            this.lTargetCell.AutoSize = true;
            this.lTargetCell.Location = new System.Drawing.Point(12, 90);
            this.lTargetCell.Name = "lTargetCell";
            this.lTargetCell.Size = new System.Drawing.Size(62, 15);
            this.lTargetCell.TabIndex = 32;
            this.lTargetCell.Text = "Target Cell";
            // 
            // ddTargetCell
            // 
            this.ddTargetCell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddTargetCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddTargetCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetCell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddTargetCell.FormattingEnabled = true;
            this.ddTargetCell.Location = new System.Drawing.Point(120, 88);
            this.ddTargetCell.Name = "ddTargetCell";
            this.ddTargetCell.Size = new System.Drawing.Size(129, 23);
            this.ddTargetCell.TabIndex = 33;
            this.ddTargetCell.SelectedIndexChanged += new System.EventHandler(this.ddTargetCell_SelectedIndexChanged);
            // 
            // ddTargetPool
            // 
            this.ddTargetPool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddTargetPool.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddTargetPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetPool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddTargetPool.FormattingEnabled = true;
            this.ddTargetPool.Location = new System.Drawing.Point(120, 62);
            this.ddTargetPool.Name = "ddTargetPool";
            this.ddTargetPool.Size = new System.Drawing.Size(129, 23);
            this.ddTargetPool.TabIndex = 7;
            this.ddTargetPool.SelectedIndexChanged += new System.EventHandler(this.ddTargetPool_SelectedIndexChanged);
            // 
            // gSynapse
            // 
            this.gSynapse.Controls.Add(this.synapseControl);
            this.gSynapse.Location = new System.Drawing.Point(9, 274);
            this.gSynapse.Name = "gSynapse";
            this.gSynapse.Size = new System.Drawing.Size(239, 157);
            this.gSynapse.TabIndex = 2;
            this.gSynapse.TabStop = false;
            this.gSynapse.Text = "Synapse Parameters";
            // 
            // synapseControl
            // 
            this.synapseControl.BackColor = System.Drawing.Color.White;
            this.synapseControl.EReversal = 0D;
            this.synapseControl.Location = new System.Drawing.Point(18, 22);
            this.synapseControl.Name = "synapseControl";
            this.synapseControl.Size = new System.Drawing.Size(152, 127);
            this.synapseControl.TabIndex = 0;
            this.synapseControl.VThreshold = 0D;
            // 
            // JunctionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.timeLineControl);
            this.Controls.Add(this.gSynapse);
            this.Controls.Add(this.lSourcePool);
            this.Controls.Add(this.lTargetCell);
            this.Controls.Add(this.numDelay);
            this.Controls.Add(this.ddTargetCell);
            this.Controls.Add(this.eFixedDuration);
            this.Controls.Add(this.lSourceCell);
            this.Controls.Add(this.numConductance);
            this.Controls.Add(this.ddSourceCell);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.ddDistanceMode);
            this.Controls.Add(this.ddConnectionType);
            this.Controls.Add(this.lDistanceMode);
            this.Controls.Add(this.ddTargetPool);
            this.Controls.Add(this.ddSourcePool);
            this.Controls.Add(this.lTargetPool);
            this.Controls.Add(this.lDelay);
            this.Controls.Add(this.lConnectionType);
            this.Controls.Add(this.lFixedDuration);
            this.Controls.Add(this.lWeight);
            this.Name = "JunctionControl";
            this.Size = new System.Drawing.Size(254, 562);
            ((System.ComponentModel.ISupportInitialize)(this.numConductance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.gSynapse.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lSourcePool;
        private Label lTargetPool;
        private Label lConnectionType;
        private Label lWeight;
        private Label lFixedDuration;
        private Label lDelay;
        private ComboBox ddSourcePool;
        private ComboBox ddConnectionType;
        private ToolTip toolTip1;
        private NumericUpDown numConductance;
        private NumericUpDown numDelay;
        private TextBox eFixedDuration;
        private CheckBox cbActive;
        private TimeLineControl timeLineControl;
        private Label lSourceCell;
        private ComboBox ddSourceCell;
        private ComboBox ddDistanceMode;
        private Label lDistanceMode;
        private Label lTargetCell;
        private ComboBox ddTargetCell;
        private ComboBox ddTargetPool;
        private GroupBox gSynapse;
        private SynapseControl synapseControl;
    }
}
