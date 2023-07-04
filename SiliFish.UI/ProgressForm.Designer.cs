namespace SiliFish.UI
{
    partial class ProgressForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            progressBar = new ProgressBar();
            linkStopRun = new LinkLabel();
            lProgress = new Label();
            lProgressValue = new Label();
            lProgressDetails = new Label();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Location = new Point(0, 124);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(348, 23);
            progressBar.TabIndex = 0;
            // 
            // linkStopRun
            // 
            linkStopRun.AutoSize = true;
            linkStopRun.Location = new Point(281, 19);
            linkStopRun.Name = "linkStopRun";
            linkStopRun.Size = new Size(55, 15);
            linkStopRun.TabIndex = 1;
            linkStopRun.TabStop = true;
            linkStopRun.Text = "Stop Run";
            linkStopRun.LinkClicked += linkStopRun_LinkClicked;
            // 
            // lProgress
            // 
            lProgress.AutoSize = true;
            lProgress.Location = new Point(12, 19);
            lProgress.Name = "lProgress";
            lProgress.Size = new Size(55, 15);
            lProgress.TabIndex = 2;
            lProgress.Text = "Progress:";
            // 
            // lProgressValue
            // 
            lProgressValue.AutoSize = true;
            lProgressValue.Location = new Point(73, 19);
            lProgressValue.Name = "lProgressValue";
            lProgressValue.Size = new Size(23, 15);
            lProgressValue.TabIndex = 3;
            lProgressValue.Text = "0%";
            // 
            // lProgressDetails
            // 
            lProgressDetails.AutoSize = true;
            lProgressDetails.Location = new Point(12, 47);
            lProgressDetails.Name = "lProgressDetails";
            lProgressDetails.Size = new Size(19, 15);
            lProgressDetails.TabIndex = 4;
            lProgressDetails.Text = "....";
            // 
            // ProgressForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(348, 147);
            ControlBox = false;
            Controls.Add(lProgressDetails);
            Controls.Add(lProgressValue);
            Controls.Add(lProgress);
            Controls.Add(linkStopRun);
            Controls.Add(progressBar);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgressForm";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "ProgressForm";
            Shown += ProgressForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar;
        private LinkLabel linkStopRun;
        private Label lProgress;
        private Label lProgressValue;
        private Label lProgressDetails;
    }
}