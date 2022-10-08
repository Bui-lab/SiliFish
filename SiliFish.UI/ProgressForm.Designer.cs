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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.linkStopRun = new System.Windows.Forms.LinkLabel();
            this.lProgress = new System.Windows.Forms.Label();
            this.lProgressValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 48);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(348, 23);
            this.progressBar.TabIndex = 0;
            // 
            // linkStopRun
            // 
            this.linkStopRun.AutoSize = true;
            this.linkStopRun.Location = new System.Drawing.Point(281, 19);
            this.linkStopRun.Name = "linkStopRun";
            this.linkStopRun.Size = new System.Drawing.Size(55, 15);
            this.linkStopRun.TabIndex = 1;
            this.linkStopRun.TabStop = true;
            this.linkStopRun.Text = "Stop Run";
            this.linkStopRun.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkStopRun_LinkClicked);
            // 
            // lProgress
            // 
            this.lProgress.AutoSize = true;
            this.lProgress.Location = new System.Drawing.Point(12, 19);
            this.lProgress.Name = "lProgress";
            this.lProgress.Size = new System.Drawing.Size(55, 15);
            this.lProgress.TabIndex = 2;
            this.lProgress.Text = "Progress:";
            // 
            // lProgressValue
            // 
            this.lProgressValue.AutoSize = true;
            this.lProgressValue.Location = new System.Drawing.Point(73, 19);
            this.lProgressValue.Name = "lProgressValue";
            this.lProgressValue.Size = new System.Drawing.Size(23, 15);
            this.lProgressValue.TabIndex = 3;
            this.lProgressValue.Text = "0%";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 71);
            this.ControlBox = false;
            this.Controls.Add(this.lProgressValue);
            this.Controls.Add(this.lProgress);
            this.Controls.Add(this.linkStopRun);
            this.Controls.Add(this.progressBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ProgressForm";
            this.Shown += new System.EventHandler(this.ProgressForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar progressBar;
        private LinkLabel linkStopRun;
        private Label lProgress;
        private Label lProgressValue;
    }
}