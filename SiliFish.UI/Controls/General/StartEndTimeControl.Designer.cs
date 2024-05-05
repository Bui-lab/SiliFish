namespace SiliFish.UI.Controls.General
{
    partial class StartEndTimeControl
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
            eEndTime = new NumericUpDown();
            eStartTime = new NumericUpDown();
            lStartTime = new Label();
            lms1 = new Label();
            lEndTime = new Label();
            lms2 = new Label();
            ((System.ComponentModel.ISupportInitialize)eEndTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eStartTime).BeginInit();
            SuspendLayout();
            // 
            // eEndTime
            // 
            eEndTime.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            eEndTime.Location = new Point(67, 29);
            eEndTime.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            eEndTime.Name = "eEndTime";
            eEndTime.Size = new Size(100, 23);
            eEndTime.TabIndex = 39;
            eEndTime.ThousandsSeparator = true;
            eEndTime.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // eStartTime
            // 
            eStartTime.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            eStartTime.Location = new Point(67, 3);
            eStartTime.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            eStartTime.Name = "eStartTime";
            eStartTime.Size = new Size(100, 23);
            eStartTime.TabIndex = 38;
            eStartTime.ThousandsSeparator = true;
            // 
            // lStartTime
            // 
            lStartTime.AutoSize = true;
            lStartTime.Location = new Point(6, 7);
            lStartTime.Name = "lStartTime";
            lStartTime.Size = new Size(60, 15);
            lStartTime.TabIndex = 34;
            lStartTime.Text = "Start Time";
            // 
            // lms1
            // 
            lms1.AutoSize = true;
            lms1.Location = new Point(174, 7);
            lms1.Name = "lms1";
            lms1.Size = new Size(31, 15);
            lms1.TabIndex = 35;
            lms1.Text = "(ms)";
            // 
            // lEndTime
            // 
            lEndTime.AutoSize = true;
            lEndTime.Location = new Point(6, 31);
            lEndTime.Name = "lEndTime";
            lEndTime.Size = new Size(56, 15);
            lEndTime.TabIndex = 36;
            lEndTime.Text = "End Time";
            // 
            // lms2
            // 
            lms2.AutoSize = true;
            lms2.Location = new Point(174, 34);
            lms2.Name = "lms2";
            lms2.Size = new Size(31, 15);
            lms2.TabIndex = 37;
            lms2.Text = "(ms)";
            // 
            // StartEndTimeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(eEndTime);
            Controls.Add(eStartTime);
            Controls.Add(lStartTime);
            Controls.Add(lms1);
            Controls.Add(lEndTime);
            Controls.Add(lms2);
            Name = "StartEndTimeControl";
            Size = new Size(222, 56);
            ((System.ComponentModel.ISupportInitialize)eEndTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)eStartTime).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown eEndTime;
        private NumericUpDown eStartTime;
        private Label lStartTime;
        private Label lms1;
        private Label lEndTime;
        private Label lms2;
    }
}
