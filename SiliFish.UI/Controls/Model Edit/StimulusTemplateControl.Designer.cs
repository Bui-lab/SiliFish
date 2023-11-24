namespace SiliFish.UI.Controls
{
    partial class StimulusTemplateControl
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
            timeLineControl = new TimeLineControl();
            ddTargetPool = new ComboBox();
            lTargetPool = new Label();
            lSagittalPosition = new Label();
            ddSagittalPosition = new ComboBox();
            cbActive = new CheckBox();
            lTargetCells = new Label();
            lTargetSomites = new Label();
            toolTip = new ToolTip(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            stimControl = new StimulusSettingsControl();
            lAll = new Label();
            cbAllSomites = new CheckBox();
            cbAllCells = new CheckBox();
            lRange = new Label();
            eTargetSomites = new TextBox();
            eTargetCells = new TextBox();
            pTargetPool = new Panel();
            lms2 = new Label();
            eDelaySagittal = new TextBox();
            lDelaySagittal = new Label();
            lms = new Label();
            eDelayPerSomite = new TextBox();
            lDelayPerSomite = new Label();
            pActive = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel1 = new Panel();
            eName = new TextBox();
            lName = new Label();
            pTargetPool.SuspendLayout();
            pActive.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // timeLineControl
            // 
            timeLineControl.BackColor = Color.WhiteSmoke;
            timeLineControl.Dock = DockStyle.Fill;
            timeLineControl.Location = new Point(3, 378);
            timeLineControl.Name = "timeLineControl";
            timeLineControl.Size = new Size(249, 146);
            timeLineControl.TabIndex = 15;
            // 
            // ddTargetPool
            // 
            ddTargetPool.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddTargetPool.BackColor = Color.WhiteSmoke;
            ddTargetPool.DropDownStyle = ComboBoxStyle.DropDownList;
            ddTargetPool.FlatStyle = FlatStyle.Flat;
            ddTargetPool.FormattingEnabled = true;
            ddTargetPool.Location = new Point(102, 8);
            ddTargetPool.Name = "ddTargetPool";
            ddTargetPool.Size = new Size(144, 23);
            ddTargetPool.TabIndex = 7;
            ddTargetPool.SelectedIndexChanged += ddTargetPool_SelectedIndexChanged;
            // 
            // lTargetPool
            // 
            lTargetPool.AutoSize = true;
            lTargetPool.Location = new Point(3, 11);
            lTargetPool.Name = "lTargetPool";
            lTargetPool.Size = new Size(66, 15);
            lTargetPool.TabIndex = 6;
            lTargetPool.Text = "Target Pool";
            // 
            // lSagittalPosition
            // 
            lSagittalPosition.AutoSize = true;
            lSagittalPosition.Location = new Point(3, 107);
            lSagittalPosition.Name = "lSagittalPosition";
            lSagittalPosition.Size = new Size(92, 15);
            lSagittalPosition.TabIndex = 12;
            lSagittalPosition.Text = "Sagittal Position";
            // 
            // ddSagittalPosition
            // 
            ddSagittalPosition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddSagittalPosition.BackColor = Color.WhiteSmoke;
            ddSagittalPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSagittalPosition.FlatStyle = FlatStyle.Flat;
            ddSagittalPosition.FormattingEnabled = true;
            ddSagittalPosition.Location = new Point(102, 104);
            ddSagittalPosition.Name = "ddSagittalPosition";
            ddSagittalPosition.Size = new Size(144, 23);
            ddSagittalPosition.TabIndex = 13;
            // 
            // cbActive
            // 
            cbActive.AutoSize = true;
            cbActive.Checked = true;
            cbActive.CheckState = CheckState.Checked;
            cbActive.Location = new Point(13, 6);
            cbActive.Name = "cbActive";
            cbActive.Size = new Size(59, 19);
            cbActive.TabIndex = 14;
            cbActive.Text = "Active";
            cbActive.UseVisualStyleBackColor = true;
            // 
            // lTargetCells
            // 
            lTargetCells.AutoSize = true;
            lTargetCells.Location = new Point(3, 81);
            lTargetCells.Name = "lTargetCells";
            lTargetCells.Size = new Size(75, 15);
            lTargetCells.TabIndex = 10;
            lTargetCells.Text = "Target Cell(s)";
            // 
            // lTargetSomites
            // 
            lTargetSomites.AutoSize = true;
            lTargetSomites.Location = new Point(3, 55);
            lTargetSomites.Name = "lTargetSomites";
            lTargetSomites.Size = new Size(92, 15);
            lTargetSomites.TabIndex = 8;
            lTargetSomites.Text = "Target Somite(s)";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // stimControl
            // 
            stimControl.AutoSize = true;
            stimControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            stimControl.BackColor = Color.FromArgb(249, 249, 249);
            stimControl.Location = new Point(3, 40);
            stimControl.MinimumSize = new Size(200, 100);
            stimControl.Name = "stimControl";
            stimControl.Size = new Size(217, 100);
            stimControl.TabIndex = 16;
            // 
            // lAll
            // 
            lAll.AutoSize = true;
            lAll.Location = new Point(98, 36);
            lAll.Name = "lAll";
            lAll.Size = new Size(21, 15);
            lAll.TabIndex = 17;
            lAll.Text = "All";
            // 
            // cbAllSomites
            // 
            cbAllSomites.AutoSize = true;
            cbAllSomites.Location = new Point(102, 55);
            cbAllSomites.Name = "cbAllSomites";
            cbAllSomites.Size = new Size(15, 14);
            cbAllSomites.TabIndex = 18;
            cbAllSomites.UseVisualStyleBackColor = true;
            cbAllSomites.CheckedChanged += cbAllSomites_CheckedChanged;
            // 
            // cbAllCells
            // 
            cbAllCells.AutoSize = true;
            cbAllCells.Location = new Point(102, 81);
            cbAllCells.Name = "cbAllCells";
            cbAllCells.Size = new Size(15, 14);
            cbAllCells.TabIndex = 19;
            cbAllCells.UseVisualStyleBackColor = true;
            cbAllCells.CheckedChanged += cbAllCells_CheckedChanged;
            // 
            // lRange
            // 
            lRange.AutoSize = true;
            lRange.Location = new Point(123, 36);
            lRange.Name = "lRange";
            lRange.Size = new Size(40, 15);
            lRange.TabIndex = 20;
            lRange.Text = "Range";
            // 
            // eTargetSomites
            // 
            eTargetSomites.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eTargetSomites.Location = new Point(123, 52);
            eTargetSomites.Name = "eTargetSomites";
            eTargetSomites.ReadOnly = true;
            eTargetSomites.Size = new Size(121, 23);
            eTargetSomites.TabIndex = 34;
            eTargetSomites.Text = "example: 1, 3-5";
            // 
            // eTargetCells
            // 
            eTargetCells.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eTargetCells.Location = new Point(123, 77);
            eTargetCells.Name = "eTargetCells";
            eTargetCells.ReadOnly = true;
            eTargetCells.Size = new Size(121, 23);
            eTargetCells.TabIndex = 35;
            eTargetCells.Text = "example: 1, 3-5";
            // 
            // pTargetPool
            // 
            pTargetPool.Controls.Add(lms2);
            pTargetPool.Controls.Add(eDelaySagittal);
            pTargetPool.Controls.Add(lDelaySagittal);
            pTargetPool.Controls.Add(lms);
            pTargetPool.Controls.Add(eDelayPerSomite);
            pTargetPool.Controls.Add(lDelayPerSomite);
            pTargetPool.Controls.Add(lTargetPool);
            pTargetPool.Controls.Add(ddTargetPool);
            pTargetPool.Controls.Add(eTargetCells);
            pTargetPool.Controls.Add(ddSagittalPosition);
            pTargetPool.Controls.Add(eTargetSomites);
            pTargetPool.Controls.Add(lSagittalPosition);
            pTargetPool.Controls.Add(lRange);
            pTargetPool.Controls.Add(lTargetCells);
            pTargetPool.Controls.Add(cbAllCells);
            pTargetPool.Controls.Add(lTargetSomites);
            pTargetPool.Controls.Add(cbAllSomites);
            pTargetPool.Controls.Add(lAll);
            pTargetPool.Location = new Point(3, 146);
            pTargetPool.Name = "pTargetPool";
            pTargetPool.Size = new Size(249, 188);
            pTargetPool.TabIndex = 36;
            // 
            // lms2
            // 
            lms2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lms2.AutoSize = true;
            lms2.Location = new Point(215, 163);
            lms2.Name = "lms2";
            lms2.Size = new Size(31, 15);
            lms2.TabIndex = 41;
            lms2.Text = "(ms)";
            // 
            // eDelaySagittal
            // 
            eDelaySagittal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eDelaySagittal.Location = new Point(102, 159);
            eDelaySagittal.Name = "eDelaySagittal";
            eDelaySagittal.Size = new Size(107, 23);
            eDelaySagittal.TabIndex = 40;
            // 
            // lDelaySagittal
            // 
            lDelaySagittal.AutoSize = true;
            lDelaySagittal.Location = new Point(3, 163);
            lDelaySagittal.Name = "lDelaySagittal";
            lDelaySagittal.Size = new Size(78, 15);
            lDelaySagittal.TabIndex = 39;
            lDelaySagittal.Text = "Sagittal Delay";
            // 
            // lms
            // 
            lms.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lms.AutoSize = true;
            lms.Location = new Point(215, 137);
            lms.Name = "lms";
            lms.Size = new Size(31, 15);
            lms.TabIndex = 38;
            lms.Text = "(ms)";
            // 
            // eDelayPerSomite
            // 
            eDelayPerSomite.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eDelayPerSomite.Location = new Point(102, 133);
            eDelayPerSomite.Name = "eDelayPerSomite";
            eDelayPerSomite.Size = new Size(107, 23);
            eDelayPerSomite.TabIndex = 37;
            // 
            // lDelayPerSomite
            // 
            lDelayPerSomite.AutoSize = true;
            lDelayPerSomite.Location = new Point(3, 137);
            lDelayPerSomite.Name = "lDelayPerSomite";
            lDelayPerSomite.Size = new Size(78, 15);
            lDelayPerSomite.TabIndex = 36;
            lDelayPerSomite.Text = "Delay/Somite";
            // 
            // pActive
            // 
            pActive.Controls.Add(cbActive);
            pActive.Location = new Point(3, 340);
            pActive.Name = "pActive";
            pActive.Size = new Size(246, 32);
            pActive.TabIndex = 37;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(panel1);
            flowLayoutPanel1.Controls.Add(stimControl);
            flowLayoutPanel1.Controls.Add(pTargetPool);
            flowLayoutPanel1.Controls.Add(pActive);
            flowLayoutPanel1.Controls.Add(timeLineControl);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(255, 527);
            flowLayoutPanel1.TabIndex = 38;
            flowLayoutPanel1.WrapContents = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(eName);
            panel1.Controls.Add(lName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(249, 31);
            panel1.TabIndex = 38;
            // 
            // eName
            // 
            eName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eName.Location = new Point(102, 4);
            eName.Name = "eName";
            eName.Size = new Size(142, 23);
            eName.TabIndex = 27;
            eName.Leave += eName_Leave;
            // 
            // lName
            // 
            lName.AutoSize = true;
            lName.Location = new Point(3, 7);
            lName.Name = "lName";
            lName.Size = new Size(39, 15);
            lName.TabIndex = 26;
            lName.Text = "Name";
            // 
            // StimulusTemplateControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 249, 249);
            Controls.Add(flowLayoutPanel1);
            Name = "StimulusTemplateControl";
            Size = new Size(255, 446);
            pTargetPool.ResumeLayout(false);
            pTargetPool.PerformLayout();
            pActive.ResumeLayout(false);
            pActive.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TimeLineControl timeLineControl;
        private ComboBox ddTargetPool;
        private Label lTargetPool;
        private Label lSagittalPosition;
        private ComboBox ddSagittalPosition;
        private CheckBox cbActive;
        private Label lTargetCells;
        private Label lTargetSomites;
        private ToolTip toolTip;
        private ContextMenuStrip contextMenuStrip1;
        private StimulusSettingsControl stimControl;
        private Label lAll;
        private CheckBox cbAllSomites;
        private CheckBox cbAllCells;
        private Label lRange;
        private TextBox eTargetSomites;
        private TextBox eTargetCells;
        private Panel pTargetPool;
        private Panel pActive;
        private FlowLayoutPanel flowLayoutPanel1;
        private TextBox eDelayPerSomite;
        private Label lDelayPerSomite;
        private Label lms;
        private Label lms2;
        private TextBox eDelaySagittal;
        private Label lDelaySagittal;
        private Panel panel1;
        private TextBox eName;
        private Label lName;
    }
}
