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
            this.label14 = new System.Windows.Forms.Label();
            this.ddStimulusMode = new System.Windows.Forms.ComboBox();
            this.eValue2 = new System.Windows.Forms.TextBox();
            this.eValue1 = new System.Windows.Forms.TextBox();
            this.lValue1 = new System.Windows.Forms.Label();
            this.lValue2 = new System.Windows.Forms.Label();
            this.timeLineControl = new SiliFish.UI.Controls.TimeLineControl();
            this.ddTargetPool = new System.Windows.Forms.ComboBox();
            this.lTargetPool = new System.Windows.Forms.Label();
            this.lSagittalPosition = new System.Windows.Forms.Label();
            this.ddSagittalPosition = new System.Windows.Forms.ComboBox();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 15);
            this.label14.TabIndex = 31;
            this.label14.Text = "Stimulus";
            // 
            // ddStimulusMode
            // 
            this.ddStimulusMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddStimulusMode.FormattingEnabled = true;
            this.ddStimulusMode.Items.AddRange(new object[] {
            "Step",
            "Gaussian",
            "Ramp"});
            this.ddStimulusMode.Location = new System.Drawing.Point(101, 12);
            this.ddStimulusMode.Name = "ddStimulusMode";
            this.ddStimulusMode.Size = new System.Drawing.Size(121, 23);
            this.ddStimulusMode.TabIndex = 32;
            this.ddStimulusMode.SelectedIndexChanged += new System.EventHandler(this.ddStimulusMode_SelectedIndexChanged);
            // 
            // eValue2
            // 
            this.eValue2.Location = new System.Drawing.Point(101, 64);
            this.eValue2.Name = "eValue2";
            this.eValue2.Size = new System.Drawing.Size(52, 23);
            this.eValue2.TabIndex = 16;
            this.eValue2.TextChanged += new System.EventHandler(this.eValue2_TextChanged);
            // 
            // eValue1
            // 
            this.eValue1.Location = new System.Drawing.Point(101, 38);
            this.eValue1.Name = "eValue1";
            this.eValue1.Size = new System.Drawing.Size(52, 23);
            this.eValue1.TabIndex = 15;
            this.eValue1.TextChanged += new System.EventHandler(this.eValue1_TextChanged);
            // 
            // lValue1
            // 
            this.lValue1.AutoSize = true;
            this.lValue1.Location = new System.Drawing.Point(3, 41);
            this.lValue1.Name = "lValue1";
            this.lValue1.Size = new System.Drawing.Size(37, 15);
            this.lValue1.TabIndex = 13;
            this.lValue1.Text = "Mean";
            // 
            // lValue2
            // 
            this.lValue2.AutoSize = true;
            this.lValue2.Location = new System.Drawing.Point(3, 67);
            this.lValue2.Name = "lValue2";
            this.lValue2.Size = new System.Drawing.Size(47, 15);
            this.lValue2.TabIndex = 14;
            this.lValue2.Text = "Std Dev";
            // 
            // timeLineControl
            // 
            this.timeLineControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.timeLineControl.Location = new System.Drawing.Point(0, 170);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(236, 172);
            this.timeLineControl.TabIndex = 38;
            // 
            // ddTargetPool
            // 
            this.ddTargetPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetPool.FormattingEnabled = true;
            this.ddTargetPool.Location = new System.Drawing.Point(101, 90);
            this.ddTargetPool.Name = "ddTargetPool";
            this.ddTargetPool.Size = new System.Drawing.Size(121, 23);
            this.ddTargetPool.TabIndex = 40;
            this.ddTargetPool.SelectedIndexChanged += new System.EventHandler(this.ddTargetPool_SelectedIndexChanged);
            // 
            // lTargetPool
            // 
            this.lTargetPool.AutoSize = true;
            this.lTargetPool.Location = new System.Drawing.Point(3, 93);
            this.lTargetPool.Name = "lTargetPool";
            this.lTargetPool.Size = new System.Drawing.Size(66, 15);
            this.lTargetPool.TabIndex = 39;
            this.lTargetPool.Text = "Target Pool";
            // 
            // lSagittalPosition
            // 
            this.lSagittalPosition.AutoSize = true;
            this.lSagittalPosition.Location = new System.Drawing.Point(3, 119);
            this.lSagittalPosition.Name = "lSagittalPosition";
            this.lSagittalPosition.Size = new System.Drawing.Size(92, 15);
            this.lSagittalPosition.TabIndex = 41;
            this.lSagittalPosition.Text = "Sagittal Position";
            // 
            // ddSagittalPosition
            // 
            this.ddSagittalPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSagittalPosition.FormattingEnabled = true;
            this.ddSagittalPosition.Items.AddRange(new object[] {
            "Left/Right",
            "Left",
            "Right"});
            this.ddSagittalPosition.Location = new System.Drawing.Point(101, 116);
            this.ddSagittalPosition.Name = "ddSagittalPosition";
            this.ddSagittalPosition.Size = new System.Drawing.Size(121, 23);
            this.ddSagittalPosition.TabIndex = 42;
            this.ddSagittalPosition.SelectedIndexChanged += new System.EventHandler(this.ddSagittalPosition_SelectedIndexChanged);
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(163, 145);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(59, 19);
            this.cbActive.TabIndex = 43;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // StimulusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.eValue2);
            this.Controls.Add(this.lSagittalPosition);
            this.Controls.Add(this.eValue1);
            this.Controls.Add(this.lValue1);
            this.Controls.Add(this.ddSagittalPosition);
            this.Controls.Add(this.lValue2);
            this.Controls.Add(this.ddTargetPool);
            this.Controls.Add(this.lTargetPool);
            this.Controls.Add(this.timeLineControl);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.ddStimulusMode);
            this.Name = "StimulusControl";
            this.Size = new System.Drawing.Size(236, 342);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label14;
        private ComboBox ddStimulusMode;
        private TextBox eValue2;
        private TextBox eValue1;
        private Label lValue1;
        private Label lValue2;
        private TimeLineControl timeLineControl;
        private ComboBox ddTargetPool;
        private Label lTargetPool;
        private Label lSagittalPosition;
        private ComboBox ddSagittalPosition;
        private CheckBox cbActive;
    }
}
