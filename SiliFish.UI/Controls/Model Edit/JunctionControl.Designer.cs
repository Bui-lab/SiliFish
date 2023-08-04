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
            lFixedDuration = new Label();
            lDelay = new Label();
            ddSourcePool = new ComboBox();
            ddConnectionType = new ComboBox();
            toolTip1 = new ToolTip(components);
            eFixedDuration = new TextBox();
            eSynDelay = new TextBox();
            cbActive = new CheckBox();
            timeLineControl = new TimeLineControl();
            ddDistanceMode = new ComboBox();
            lDistanceMode = new Label();
            lSourceCell = new Label();
            ddSourceCell = new ComboBox();
            lTargetCell = new Label();
            ddTargetCell = new ComboBox();
            ddTargetPool = new ComboBox();
            ddCoreType = new ComboBox();
            lCoreType = new Label();
            propCore = new PropertyGrid();
            tabJunction = new TabControl();
            tDynamics = new TabPage();
            tTimeLine = new TabPage();
            tabJunction.SuspendLayout();
            tDynamics.SuspendLayout();
            tTimeLine.SuspendLayout();
            SuspendLayout();
            // 
            // lSourcePool
            // 
            lSourcePool.AutoSize = true;
            lSourcePool.Location = new Point(9, 12);
            lSourcePool.Name = "lSourcePool";
            lSourcePool.Size = new Size(70, 15);
            lSourcePool.TabIndex = 4;
            lSourcePool.Text = "Source Pool";
            // 
            // lTargetPool
            // 
            lTargetPool.AutoSize = true;
            lTargetPool.Location = new Point(9, 62);
            lTargetPool.Name = "lTargetPool";
            lTargetPool.Size = new Size(66, 15);
            lTargetPool.TabIndex = 6;
            lTargetPool.Text = "Target Pool";
            // 
            // lConnectionType
            // 
            lConnectionType.AutoSize = true;
            lConnectionType.Location = new Point(9, 112);
            lConnectionType.Name = "lConnectionType";
            lConnectionType.Size = new Size(96, 15);
            lConnectionType.TabIndex = 10;
            lConnectionType.Text = "Connection Type";
            // 
            // lFixedDuration
            // 
            lFixedDuration.AutoSize = true;
            lFixedDuration.Location = new Point(9, 187);
            lFixedDuration.Name = "lFixedDuration";
            lFixedDuration.Size = new Size(87, 15);
            lFixedDuration.TabIndex = 20;
            lFixedDuration.Text = "Fixed Dur. (ms)";
            toolTip1.SetToolTip(lFixedDuration, "If entered, distance/speed and delay values will not be used to calculate duration.");
            // 
            // lDelay
            // 
            lDelay.AutoSize = true;
            lDelay.Location = new Point(9, 212);
            lDelay.Name = "lDelay";
            lDelay.Size = new Size(63, 15);
            lDelay.TabIndex = 22;
            lDelay.Text = "Delay (ms)";
            toolTip1.SetToolTip(lDelay, "If entered, will replace model's generic synaptic delay (for chemical synapses).");
            // 
            // ddSourcePool
            // 
            ddSourcePool.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddSourcePool.BackColor = Color.WhiteSmoke;
            ddSourcePool.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSourcePool.FlatStyle = FlatStyle.Flat;
            ddSourcePool.FormattingEnabled = true;
            ddSourcePool.Location = new Point(110, 10);
            ddSourcePool.Name = "ddSourcePool";
            ddSourcePool.Size = new Size(252, 23);
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
            ddConnectionType.Location = new Point(110, 110);
            ddConnectionType.Name = "ddConnectionType";
            ddConnectionType.Size = new Size(251, 23);
            ddConnectionType.TabIndex = 11;
            ddConnectionType.SelectedIndexChanged += ddConnectionType_SelectedIndexChanged;
            // 
            // eFixedDuration
            // 
            eFixedDuration.Location = new Point(110, 185);
            eFixedDuration.Name = "eFixedDuration";
            eFixedDuration.Size = new Size(76, 23);
            eFixedDuration.TabIndex = 21;
            toolTip1.SetToolTip(eFixedDuration, "If entered, distance/speed and delay values will not be used to calculate duration.\r\n");
            // 
            // eSynDelay
            // 
            eSynDelay.Location = new Point(110, 210);
            eSynDelay.Name = "eSynDelay";
            eSynDelay.Size = new Size(76, 23);
            eSynDelay.TabIndex = 22;
            toolTip1.SetToolTip(eSynDelay, "If entered, will replace model's generic synaptic delay (for chemical synapses).");
            // 
            // cbActive
            // 
            cbActive.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbActive.AutoSize = true;
            cbActive.CheckAlign = ContentAlignment.MiddleRight;
            cbActive.Checked = true;
            cbActive.CheckState = CheckState.Checked;
            cbActive.Location = new Point(9, 237);
            cbActive.Name = "cbActive";
            cbActive.Size = new Size(59, 19);
            cbActive.TabIndex = 0;
            cbActive.Text = "Active";
            cbActive.UseVisualStyleBackColor = true;
            // 
            // timeLineControl
            // 
            timeLineControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            timeLineControl.Location = new Point(6, 6);
            timeLineControl.Name = "timeLineControl";
            timeLineControl.Size = new Size(445, 489);
            timeLineControl.TabIndex = 0;
            // 
            // ddDistanceMode
            // 
            ddDistanceMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddDistanceMode.BackColor = Color.WhiteSmoke;
            ddDistanceMode.DropDownStyle = ComboBoxStyle.DropDownList;
            ddDistanceMode.FlatStyle = FlatStyle.Flat;
            ddDistanceMode.FormattingEnabled = true;
            ddDistanceMode.Location = new Point(110, 160);
            ddDistanceMode.Name = "ddDistanceMode";
            ddDistanceMode.Size = new Size(251, 23);
            ddDistanceMode.TabIndex = 29;
            // 
            // lDistanceMode
            // 
            lDistanceMode.AutoSize = true;
            lDistanceMode.Location = new Point(9, 162);
            lDistanceMode.Name = "lDistanceMode";
            lDistanceMode.Size = new Size(86, 15);
            lDistanceMode.TabIndex = 28;
            lDistanceMode.Text = "Distance Mode";
            // 
            // lSourceCell
            // 
            lSourceCell.AutoSize = true;
            lSourceCell.Location = new Point(9, 37);
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
            ddSourceCell.Location = new Point(110, 35);
            ddSourceCell.Name = "ddSourceCell";
            ddSourceCell.Size = new Size(251, 23);
            ddSourceCell.TabIndex = 31;
            ddSourceCell.SelectedIndexChanged += ddSourceCell_SelectedIndexChanged;
            // 
            // lTargetCell
            // 
            lTargetCell.AutoSize = true;
            lTargetCell.Location = new Point(9, 87);
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
            ddTargetCell.Location = new Point(110, 85);
            ddTargetCell.Name = "ddTargetCell";
            ddTargetCell.Size = new Size(251, 23);
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
            ddTargetPool.Location = new Point(110, 60);
            ddTargetPool.Name = "ddTargetPool";
            ddTargetPool.Size = new Size(251, 23);
            ddTargetPool.TabIndex = 7;
            ddTargetPool.SelectedIndexChanged += ddTargetPool_SelectedIndexChanged;
            // 
            // ddCoreType
            // 
            ddCoreType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCoreType.BackColor = Color.WhiteSmoke;
            ddCoreType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCoreType.FlatStyle = FlatStyle.Flat;
            ddCoreType.FormattingEnabled = true;
            ddCoreType.Location = new Point(110, 135);
            ddCoreType.Name = "ddCoreType";
            ddCoreType.Size = new Size(251, 23);
            ddCoreType.TabIndex = 35;
            // 
            // lCoreType
            // 
            lCoreType.AutoSize = true;
            lCoreType.Location = new Point(9, 137);
            lCoreType.Name = "lCoreType";
            lCoreType.Size = new Size(59, 15);
            lCoreType.TabIndex = 34;
            lCoreType.Text = "Core Type";
            // 
            // propCore
            // 
            propCore.Dock = DockStyle.Fill;
            propCore.Location = new Point(3, 3);
            propCore.Name = "propCore";
            propCore.Size = new Size(353, 362);
            propCore.TabIndex = 36;
            // 
            // tabJunction
            // 
            tabJunction.Controls.Add(tDynamics);
            tabJunction.Controls.Add(tTimeLine);
            tabJunction.Dock = DockStyle.Bottom;
            tabJunction.Location = new Point(0, 262);
            tabJunction.Name = "tabJunction";
            tabJunction.SelectedIndex = 0;
            tabJunction.Size = new Size(367, 396);
            tabJunction.TabIndex = 37;
            // 
            // tDynamics
            // 
            tDynamics.Controls.Add(propCore);
            tDynamics.Location = new Point(4, 24);
            tDynamics.Name = "tDynamics";
            tDynamics.Padding = new Padding(3);
            tDynamics.Size = new Size(359, 368);
            tDynamics.TabIndex = 1;
            tDynamics.Text = "Dynamics";
            tDynamics.UseVisualStyleBackColor = true;
            // 
            // tTimeLine
            // 
            tTimeLine.Controls.Add(timeLineControl);
            tTimeLine.Location = new Point(4, 24);
            tTimeLine.Name = "tTimeLine";
            tTimeLine.Padding = new Padding(3);
            tTimeLine.Size = new Size(359, 368);
            tTimeLine.TabIndex = 0;
            tTimeLine.Text = "TimeLine";
            tTimeLine.UseVisualStyleBackColor = true;
            // 
            // JunctionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            Controls.Add(tabJunction);
            Controls.Add(ddCoreType);
            Controls.Add(lCoreType);
            Controls.Add(eSynDelay);
            Controls.Add(lSourcePool);
            Controls.Add(lTargetCell);
            Controls.Add(ddTargetCell);
            Controls.Add(eFixedDuration);
            Controls.Add(lSourceCell);
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
            Name = "JunctionControl";
            Size = new Size(367, 658);
            SizeChanged += JunctionControl_SizeChanged;
            tabJunction.ResumeLayout(false);
            tDynamics.ResumeLayout(false);
            tTimeLine.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lSourcePool;
        private Label lTargetPool;
        private Label lConnectionType;
        private Label lFixedDuration;
        private Label lDelay;
        private ComboBox ddSourcePool;
        private ComboBox ddConnectionType;
        private ToolTip toolTip1;
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
        private TextBox eSynDelay;
        private ComboBox ddCoreType;
        private Label lCoreType;
        private PropertyGrid propCore;
        private TabControl tabJunction;
        private TabPage tTimeLine;
        private TabPage tDynamics;
    }
}
