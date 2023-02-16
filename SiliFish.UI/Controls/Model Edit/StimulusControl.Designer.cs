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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.timeLineControl = new SiliFish.UI.Controls.TimeLineControl();
            this.lTargetCell = new System.Windows.Forms.Label();
            this.stimControl = new SiliFish.UI.Controls.StimulusSettingsControl();
            this.eTargetCell = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(3, 116);
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
            this.timeLineControl.Location = new System.Drawing.Point(3, 141);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(259, 212);
            this.timeLineControl.TabIndex = 0;
            // 
            // lTargetCell
            // 
            this.lTargetCell.AutoSize = true;
            this.lTargetCell.Location = new System.Drawing.Point(3, 93);
            this.lTargetCell.Name = "lTargetCell";
            this.lTargetCell.Size = new System.Drawing.Size(62, 15);
            this.lTargetCell.TabIndex = 32;
            this.lTargetCell.Text = "Target Cell";
            // 
            // stimControl
            // 
            this.stimControl.BackColor = System.Drawing.Color.White;
            this.stimControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.stimControl.Location = new System.Drawing.Point(0, 0);
            this.stimControl.Name = "stimControl";
            this.stimControl.Size = new System.Drawing.Size(265, 90);
            this.stimControl.TabIndex = 34;
            // 
            // eTargetCell
            // 
            this.eTargetCell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eTargetCell.Location = new System.Drawing.Point(101, 90);
            this.eTargetCell.Name = "eTargetCell";
            this.eTargetCell.ReadOnly = true;
            this.eTargetCell.Size = new System.Drawing.Size(161, 23);
            this.eTargetCell.TabIndex = 36;
            // 
            // StimulusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.eTargetCell);
            this.Controls.Add(this.stimControl);
            this.Controls.Add(this.timeLineControl);
            this.Controls.Add(this.lTargetCell);
            this.Controls.Add(this.cbActive);
            this.Name = "StimulusControl";
            this.Size = new System.Drawing.Size(265, 357);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ToolTip toolTip1;
        private CheckBox cbActive;
        private TimeLineControl timeLineControl;
        private Label lTargetCell;
        private StimulusSettingsControl stimControl;
        private TextBox eTargetCell;
    }
}
