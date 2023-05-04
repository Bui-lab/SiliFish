namespace SiliFish.UI.Controls
{
    partial class StimulusControl
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
            toolTip1 = new ToolTip(components);
            cbActive = new CheckBox();
            timeLineControl = new TimeLineControl();
            lTargetCell = new Label();
            stimControl = new StimulusSettingsControl();
            eTargetCell = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel2 = new Panel();
            panel1 = new Panel();
            ddTargetCell = new ComboBox();
            flowLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cbActive
            // 
            cbActive.AutoSize = true;
            cbActive.CheckAlign = ContentAlignment.MiddleRight;
            cbActive.Checked = true;
            cbActive.CheckState = CheckState.Checked;
            cbActive.Location = new Point(7, 29);
            cbActive.Name = "cbActive";
            cbActive.Size = new Size(59, 19);
            cbActive.TabIndex = 0;
            cbActive.Text = "Active";
            cbActive.UseVisualStyleBackColor = true;
            // 
            // timeLineControl
            // 
            timeLineControl.Dock = DockStyle.Fill;
            timeLineControl.Location = new Point(0, 57);
            timeLineControl.Name = "timeLineControl";
            timeLineControl.Size = new Size(314, 231);
            timeLineControl.TabIndex = 0;
            // 
            // lTargetCell
            // 
            lTargetCell.AutoSize = true;
            lTargetCell.Location = new Point(7, 6);
            lTargetCell.Name = "lTargetCell";
            lTargetCell.Size = new Size(62, 15);
            lTargetCell.TabIndex = 32;
            lTargetCell.Text = "Target Cell";
            // 
            // stimControl
            // 
            stimControl.AutoSize = true;
            stimControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            stimControl.BackColor = Color.FromArgb(249, 249, 249);
            stimControl.Dock = DockStyle.Fill;
            stimControl.Location = new Point(3, 3);
            stimControl.Margin = new Padding(3, 3, 3, 0);
            stimControl.MinimumSize = new Size(200, 100);
            stimControl.Name = "stimControl";
            stimControl.Size = new Size(314, 100);
            stimControl.TabIndex = 34;
            // 
            // eTargetCell
            // 
            eTargetCell.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eTargetCell.Location = new Point(105, 3);
            eTargetCell.Name = "eTargetCell";
            eTargetCell.ReadOnly = true;
            eTargetCell.Size = new Size(206, 23);
            eTargetCell.TabIndex = 36;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.BackColor = Color.FromArgb(249, 249, 249);
            flowLayoutPanel1.Controls.Add(stimControl);
            flowLayoutPanel1.Controls.Add(panel2);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(319, 394);
            flowLayoutPanel1.TabIndex = 37;
            flowLayoutPanel1.WrapContents = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(timeLineControl);
            panel2.Controls.Add(panel1);
            panel2.Location = new Point(3, 106);
            panel2.Name = "panel2";
            panel2.Size = new Size(314, 288);
            panel2.TabIndex = 38;
            // 
            // panel1
            // 
            panel1.Controls.Add(ddTargetCell);
            panel1.Controls.Add(eTargetCell);
            panel1.Controls.Add(cbActive);
            panel1.Controls.Add(lTargetCell);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(314, 57);
            panel1.TabIndex = 35;
            // 
            // ddTargetCell
            // 
            ddTargetCell.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddTargetCell.BackColor = Color.WhiteSmoke;
            ddTargetCell.DropDownStyle = ComboBoxStyle.DropDownList;
            ddTargetCell.FlatStyle = FlatStyle.Flat;
            ddTargetCell.FormattingEnabled = true;
            ddTargetCell.Location = new Point(105, 3);
            ddTargetCell.Name = "ddTargetCell";
            ddTargetCell.Size = new Size(206, 23);
            ddTargetCell.TabIndex = 37;
            ddTargetCell.Visible = false;
            ddTargetCell.SelectedIndexChanged += ddTargetCell_SelectedIndexChanged;
            // 
            // StimulusControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            Controls.Add(flowLayoutPanel1);
            Name = "StimulusControl";
            Size = new Size(322, 397);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip1;
        private CheckBox cbActive;
        private TimeLineControl timeLineControl;
        private Label lTargetCell;
        private StimulusSettingsControl stimControl;
        private TextBox eTargetCell;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel1;
        private Panel panel2;
        private ComboBox ddTargetCell;
    }
}
