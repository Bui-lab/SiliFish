namespace SiliFish.UI
{
    partial class About
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.linkGitHub = new System.Windows.Forms.LinkLabel();
            this.lLocation = new System.Windows.Forms.Label();
            this.lCoder = new System.Windows.Forms.Label();
            this.lVersionWindows = new System.Windows.Forms.Label();
            this.lPTMA = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.timerAbout = new System.Windows.Forms.Timer(this.components);
            this.lLogo = new System.Windows.Forms.Label();
            this.lVersionEngine = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // linkGitHub
            // 
            this.linkGitHub.AutoSize = true;
            this.linkGitHub.Location = new System.Drawing.Point(14, 381);
            this.linkGitHub.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkGitHub.Name = "linkGitHub";
            this.linkGitHub.Size = new System.Drawing.Size(193, 15);
            this.linkGitHub.TabIndex = 13;
            this.linkGitHub.TabStop = true;
            this.linkGitHub.Text = "https://github.com/Bui-lab/SiliFish";
            // 
            // lLocation
            // 
            this.lLocation.AutoSize = true;
            this.lLocation.Location = new System.Drawing.Point(14, 356);
            this.lLocation.Name = "lLocation";
            this.lLocation.Size = new System.Drawing.Size(138, 15);
            this.lLocation.TabIndex = 11;
            this.lLocation.Text = "University of Ottawa, ON";
            // 
            // lCoder
            // 
            this.lCoder.AutoSize = true;
            this.lCoder.Location = new System.Drawing.Point(14, 337);
            this.lCoder.Name = "lCoder";
            this.lCoder.Size = new System.Drawing.Size(131, 15);
            this.lCoder.TabIndex = 9;
            this.lCoder.Text = "Emine Topcu @ Bui Lab";
            // 
            // lVersionWindows
            // 
            this.lVersionWindows.AutoSize = true;
            this.lVersionWindows.Location = new System.Drawing.Point(14, 289);
            this.lVersionWindows.Name = "lVersionWindows";
            this.lVersionWindows.Size = new System.Drawing.Size(72, 15);
            this.lVersionWindows.TabIndex = 8;
            this.lVersionWindows.Text = "Version 0.1.1";
            // 
            // lPTMA
            // 
            this.lPTMA.AutoSize = true;
            this.lPTMA.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lPTMA.Location = new System.Drawing.Point(12, 254);
            this.lPTMA.Name = "lPTMA";
            this.lPTMA.Size = new System.Drawing.Size(70, 25);
            this.lPTMA.TabIndex = 7;
            this.lPTMA.Text = "SiliFish";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(-1, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(328, 251);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 14;
            this.pbLogo.TabStop = false;
            // 
            // lLogo
            // 
            this.lLogo.AutoSize = true;
            this.lLogo.Location = new System.Drawing.Point(174, 234);
            this.lLogo.Name = "lLogo";
            this.lLogo.Size = new System.Drawing.Size(150, 15);
            this.lLogo.TabIndex = 15;
            this.lLogo.Text = "Logo design by Sibel Aydin";
            // 
            // lVersionEngine
            // 
            this.lVersionEngine.AutoSize = true;
            this.lVersionEngine.Location = new System.Drawing.Point(14, 308);
            this.lVersionEngine.Name = "lVersionEngine";
            this.lVersionEngine.Size = new System.Drawing.Size(72, 15);
            this.lVersionEngine.TabIndex = 17;
            this.lVersionEngine.Text = "Version 0.1.1";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 409);
            this.Controls.Add(this.lVersionEngine);
            this.Controls.Add(this.lLogo);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.linkGitHub);
            this.Controls.Add(this.lLocation);
            this.Controls.Add(this.lCoder);
            this.Controls.Add(this.lVersionWindows);
            this.Controls.Add(this.lPTMA);
            this.MaximizeBox = false;
            this.Name = "About";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkLabel linkGitHub;
        private Label lLocation;
        private Label lCoder;
        private Label lVersionWindows;
        private Label lPTMA;
        private PictureBox pbLogo;
        private System.Windows.Forms.Timer timerAbout;
        private Label lLogo;
        private Label lVersionEngine;
    }
}