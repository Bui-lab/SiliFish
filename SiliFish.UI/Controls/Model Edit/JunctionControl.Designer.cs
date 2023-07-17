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
            components = new System.ComponentModel.Container();
            lSourcePool = new Label();
            lTargetPool = new Label();
            lConnectionType = new Label();
            lWeight = new Label();
            lFixedDuration = new Label();
            lDelay = new Label();
            ddSourcePool = new ComboBox();
            ddConnectionType = new ComboBox();
            toolTip1 = new ToolTip(components);
            numConductance = new NumericUpDown();
            numDelay = new NumericUpDown();
            eFixedDuration = new TextBox();
            cbActive = new CheckBox();
            timeLineControl = new TimeLineControl();
            ddDistanceMode = new ComboBox();
            lDistanceMode = new Label();
            lSourceCell = new Label();
            ddSourceCell = new ComboBox();
            lTargetCell = new Label();
            ddTargetCell = new ComboBox();
            ddTargetPool = new ComboBox();
            gSynapse = new GroupBox();
            synapseControl = new SynapseControl();
            ((System.ComponentModel.ISupportInitialize)numConductance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDelay).BeginInit();
            gSynapse.SuspendLayout();
            SuspendLayout();
            // 
            // lSourcePool
            // 
            lSourcePool.AutoSize = true;
            lSourcePool.Location = new Point(12, 12);
            lSourcePool.Name = "lSourcePool";
            lSourcePool.Size = new Size(70, 15);
            lSourcePool.TabIndex = 4;
            lSourcePool.Text = "Source Pool";
            // 
            // lTargetPool
            // 
            lTargetPool.AutoSize = true;
            lTargetPool.Location = new Point(12, 64);
            lTargetPool.Name = "lTargetPool";
            lTargetPool.Size = new Size(66, 15);
            lTargetPool.TabIndex = 6;
            lTargetPool.Text = "Target Pool";
            // 
            // lConnectionType
            // 
            lConnectionType.AutoSize = true;
            lConnectionType.Location = new Point(12, 116);
            lConnectionType.Name = "lConnectionType";
            lConnectionType.Size = new Size(96, 15);
            lConnectionType.TabIndex = 10;
            lConnectionType.Text = "Connection Type";
            // 
            // lWeight
            // 
            lWeight.AutoSize = true;
            lWeight.Location = new Point(12, 168);
            lWeight.Name = "lWeight";
            lWeight.Size = new Size(45, 15);
            lWeight.TabIndex = 18;
            lWeight.Text = "Weight";
            toolTip1.SetToolTip(lWeight, "Maximum conductance");
            // 
            // lFixedDuration
            // 
            lFixedDuration.AutoSize = true;
            lFixedDuration.Location = new Point(12, 194);
            lFixedDuration.Name = "lFixedDuration";
            lFixedDuration.Size = new Size(87, 15);
            lFixedDuration.TabIndex = 20;
            lFixedDuration.Text = "Fixed Dur. (ms)";
            toolTip1.SetToolTip(lFixedDuration, "If entered, distance/speed and delay values will not be used to calculate duration.");
            // 
            // lDelay
            // 
            lDelay.AutoSize = true;
            lDelay.Location = new Point(12, 220);
            lDelay.Name = "lDelay";
            lDelay.Size = new Size(63, 15);
            lDelay.TabIndex = 22;
            lDelay.Text = "Delay (ms)";
            toolTip1.SetToolTip(lDelay, "The time that will be added to the duratino calculated by the distance and conduction velocity.");
            // 
            // ddSourcePool
            // 
            ddSourcePool.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddSourcePool.BackColor = Color.WhiteSmoke;
            ddSourcePool.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSourcePool.FlatStyle = FlatStyle.Flat;
            ddSourcePool.FormattingEnabled = true;
            ddSourcePool.Location = new Point(120, 10);
            ddSourcePool.Name = "ddSourcePool";
            ddSourcePool.Size = new Size(129, 23);
            ddSourcePool.TabIndex = 5;
            ddSourcePool.SelectedIndexChanged += ddSourcePool_SelectedIndexChanged;
            // 
            // ddConnectionType
            // 
            ddConnectionType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddConnectionType.BackColor = Color.WhiteSmoke;
            ddConnectionType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddConnectionType.FlatStyle = FlatStyle.Flat;
            ddConnectionType.FormattingEnabled = true;
            ddConnectionType.Items.AddRange(new object[] { "Synapse", "Gap", "NMJ" });
            ddConnectionType.Location = new Point(120, 114);
            ddConnectionType.Name = "ddConnectionType";
            ddConnectionType.Size = new Size(129, 23);
            ddConnectionType.TabIndex = 11;
            ddConnectionType.SelectedIndexChanged += ddConnectionType_SelectedIndexChanged;
            // 
            // numConductance
            // 
            numConductance.DecimalPlaces = 6;
            numConductance.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numConductance.Location = new Point(120, 166);
            numConductance.Name = "numConductance";
            numConductance.Size = new Size(66, 23);
            numConductance.TabIndex = 19;
            toolTip1.SetToolTip(numConductance, "Maximum conductance");
            // 
            // numDelay
            // 
            numDelay.DecimalPlaces = 2;
            numDelay.Location = new Point(120, 218);
            numDelay.Name = "numDelay";
            numDelay.Size = new Size(66, 23);
            numDelay.TabIndex = 23;
            toolTip1.SetToolTip(numDelay, "The time that will be added to the duratino calculated by the distance and conduction velocity.");
            // 
            // eFixedDuration
            // 
            eFixedDuration.Location = new Point(120, 192);
            eFixedDuration.Name = "eFixedDuration";
            eFixedDuration.Size = new Size(66, 23);
            eFixedDuration.TabIndex = 21;
            toolTip1.SetToolTip(eFixedDuration, "If entered, distance/speed and delay values will not be used to calculate duration.\r\n");
            // 
            // cbActive
            // 
            cbActive.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbActive.AutoSize = true;
            cbActive.CheckAlign = ContentAlignment.MiddleRight;
            cbActive.Checked = true;
            cbActive.CheckState = CheckState.Checked;
            cbActive.Location = new Point(12, 246);
            cbActive.Name = "cbActive";
            cbActive.Size = new Size(59, 19);
            cbActive.TabIndex = 0;
            cbActive.Text = "Active";
            cbActive.UseVisualStyleBackColor = true;
            // 
            // timeLineControl
            // 
            timeLineControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            timeLineControl.Location = new Point(3, 437);
            timeLineControl.Name = "timeLineControl";
            timeLineControl.Size = new Size(245, 125);
            timeLineControl.TabIndex = 0;
            // 
            // ddDistanceMode
            // 
            ddDistanceMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddDistanceMode.BackColor = Color.WhiteSmoke;
            ddDistanceMode.DropDownStyle = ComboBoxStyle.DropDownList;
            ddDistanceMode.FlatStyle = FlatStyle.Flat;
            ddDistanceMode.FormattingEnabled = true;
            ddDistanceMode.Location = new Point(120, 140);
            ddDistanceMode.Name = "ddDistanceMode";
            ddDistanceMode.Size = new Size(129, 23);
            ddDistanceMode.TabIndex = 29;
            // 
            // lDistanceMode
            // 
            lDistanceMode.AutoSize = true;
            lDistanceMode.Location = new Point(12, 142);
            lDistanceMode.Name = "lDistanceMode";
            lDistanceMode.Size = new Size(86, 15);
            lDistanceMode.TabIndex = 28;
            lDistanceMode.Text = "Distance Mode";
            // 
            // lSourceCell
            // 
            lSourceCell.AutoSize = true;
            lSourceCell.Location = new Point(12, 38);
            lSourceCell.Name = "lSourceCell";
            lSourceCell.Size = new Size(66, 15);
            lSourceCell.TabIndex = 30;
            lSourceCell.Text = "Source Cell";
            // 
            // ddSourceCell
            // 
            ddSourceCell.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddSourceCell.BackColor = Color.WhiteSmoke;
            ddSourceCell.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSourceCell.FlatStyle = FlatStyle.Flat;
            ddSourceCell.FormattingEnabled = true;
            ddSourceCell.Location = new Point(120, 36);
            ddSourceCell.Name = "ddSourceCell";
            ddSourceCell.Size = new Size(129, 23);
            ddSourceCell.TabIndex = 31;
            ddSourceCell.SelectedIndexChanged += ddSourceCell_SelectedIndexChanged;
            // 
            // lTargetCell
            // 
            lTargetCell.AutoSize = true;
            lTargetCell.Location = new Point(12, 90);
            lTargetCell.Name = "lTargetCell";
            lTargetCell.Size = new Size(62, 15);
            lTargetCell.TabIndex = 32;
            lTargetCell.Text = "Target Cell";
            // 
            // ddTargetCell
            // 
            ddTargetCell.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddTargetCell.BackColor = Color.WhiteSmoke;
            ddTargetCell.DropDownStyle = ComboBoxStyle.DropDownList;
            ddTargetCell.FlatStyle = FlatStyle.Flat;
            ddTargetCell.FormattingEnabled = true;
            ddTargetCell.Location = new Point(120, 88);
            ddTargetCell.Name = "ddTargetCell";
            ddTargetCell.Size = new Size(129, 23);
            ddTargetCell.TabIndex = 33;
            ddTargetCell.SelectedIndexChanged += ddTargetCell_SelectedIndexChanged;
            // 
            // ddTargetPool
            // 
            ddTargetPool.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddTargetPool.BackColor = Color.WhiteSmoke;
            ddTargetPool.DropDownStyle = ComboBoxStyle.DropDownList;
            ddTargetPool.FlatStyle = FlatStyle.Flat;
            ddTargetPool.FormattingEnabled = true;
            ddTargetPool.Location = new Point(120, 62);
            ddTargetPool.Name = "ddTargetPool";
            ddTargetPool.Size = new Size(129, 23);
            ddTargetPool.TabIndex = 7;
            ddTargetPool.SelectedIndexChanged += ddTargetPool_SelectedIndexChanged;
            // 
            // gSynapse
            // 
            gSynapse.Controls.Add(synapseControl);
            gSynapse.Location = new Point(9, 274);
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
            synapseControl.Location = new Point(18, 22);
            synapseControl.Name = "synapseControl";
            synapseControl.Size = new Size(152, 127);
            synapseControl.TabIndex = 0;
            synapseControl.VThreshold = 0D;
            // 
            // JunctionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            Controls.Add(timeLineControl);
            Controls.Add(gSynapse);
            Controls.Add(lSourcePool);
            Controls.Add(lTargetCell);
            Controls.Add(numDelay);
            Controls.Add(ddTargetCell);
            Controls.Add(eFixedDuration);
            Controls.Add(lSourceCell);
            Controls.Add(numConductance);
            Controls.Add(ddSourceCell);
            Controls.Add(cbActive);
            Controls.Add(ddDistanceMode);
            Controls.Add(ddConnectionType);
            Controls.Add(lDistanceMode);
            Controls.Add(ddTargetPool);
            Controls.Add(ddSourcePool);
            Controls.Add(lTargetPool);
            Controls.Add(lDelay);
            Controls.Add(lConnectionType);
            Controls.Add(lFixedDuration);
            Controls.Add(lWeight);
            Name = "JunctionControl";
            Size = new Size(254, 562);
            SizeChanged += JunctionControl_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)numConductance).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDelay).EndInit();
            gSynapse.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
