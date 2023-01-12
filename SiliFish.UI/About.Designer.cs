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
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.timerAbout = new System.Windows.Forms.Timer(this.components);
            this.lLogo = new System.Windows.Forms.Label();
            this.lVersionEngine = new System.Windows.Forms.Label();
            this.pbName = new System.Windows.Forms.PictureBox();
            this.tabAbout = new System.Windows.Forms.TabControl();
            this.tGeneral = new System.Windows.Forms.TabPage();
            this.tCredits = new System.Windows.Forms.TabPage();
            this.eCredits = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbName)).BeginInit();
            this.tabAbout.SuspendLayout();
            this.tGeneral.SuspendLayout();
            this.tCredits.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkGitHub
            // 
            this.linkGitHub.AutoSize = true;
            this.linkGitHub.Location = new System.Drawing.Point(6, 145);
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
            this.lLocation.Location = new System.Drawing.Point(6, 120);
            this.lLocation.Name = "lLocation";
            this.lLocation.Size = new System.Drawing.Size(138, 15);
            this.lLocation.TabIndex = 11;
            this.lLocation.Text = "University of Ottawa, ON";
            // 
            // lCoder
            // 
            this.lCoder.AutoSize = true;
            this.lCoder.Location = new System.Drawing.Point(6, 101);
            this.lCoder.Name = "lCoder";
            this.lCoder.Size = new System.Drawing.Size(131, 15);
            this.lCoder.TabIndex = 9;
            this.lCoder.Text = "Emine Topcu @ Bui Lab";
            // 
            // lVersionWindows
            // 
            this.lVersionWindows.AutoSize = true;
            this.lVersionWindows.Location = new System.Drawing.Point(6, 53);
            this.lVersionWindows.Name = "lVersionWindows";
            this.lVersionWindows.Size = new System.Drawing.Size(72, 15);
            this.lVersionWindows.TabIndex = 8;
            this.lVersionWindows.Text = "Version 0.1.1";
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
            this.lVersionEngine.Location = new System.Drawing.Point(6, 72);
            this.lVersionEngine.Name = "lVersionEngine";
            this.lVersionEngine.Size = new System.Drawing.Size(72, 15);
            this.lVersionEngine.TabIndex = 17;
            this.lVersionEngine.Text = "Version 0.1.1";
            // 
            // pbName
            // 
            this.pbName.Image = ((System.Drawing.Image)(resources.GetObject("pbName.Image")));
            this.pbName.Location = new System.Drawing.Point(6, 6);
            this.pbName.Name = "pbName";
            this.pbName.Size = new System.Drawing.Size(147, 44);
            this.pbName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbName.TabIndex = 18;
            this.pbName.TabStop = false;
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.tGeneral);
            this.tabAbout.Controls.Add(this.tCredits);
            this.tabAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabAbout.Location = new System.Drawing.Point(0, 252);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.SelectedIndex = 0;
            this.tabAbout.Size = new System.Drawing.Size(326, 197);
            this.tabAbout.TabIndex = 19;
            // 
            // tGeneral
            // 
            this.tGeneral.Controls.Add(this.pbName);
            this.tGeneral.Controls.Add(this.lVersionWindows);
            this.tGeneral.Controls.Add(this.lVersionEngine);
            this.tGeneral.Controls.Add(this.lCoder);
            this.tGeneral.Controls.Add(this.lLocation);
            this.tGeneral.Controls.Add(this.linkGitHub);
            this.tGeneral.Location = new System.Drawing.Point(4, 24);
            this.tGeneral.Name = "tGeneral";
            this.tGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tGeneral.Size = new System.Drawing.Size(318, 169);
            this.tGeneral.TabIndex = 0;
            this.tGeneral.Text = "General";
            this.tGeneral.UseVisualStyleBackColor = true;
            // 
            // tCredits
            // 
            this.tCredits.Controls.Add(this.eCredits);
            this.tCredits.Location = new System.Drawing.Point(4, 24);
            this.tCredits.Name = "tCredits";
            this.tCredits.Padding = new System.Windows.Forms.Padding(3);
            this.tCredits.Size = new System.Drawing.Size(318, 169);
            this.tCredits.TabIndex = 1;
            this.tCredits.Text = "Credits";
            this.tCredits.UseVisualStyleBackColor = true;
            // 
            // eCredits
            // 
            this.eCredits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eCredits.Location = new System.Drawing.Point(3, 3);
            this.eCredits.Name = "eCredits";
            this.eCredits.ReadOnly = true;
            this.eCredits.Size = new System.Drawing.Size(312, 163);
            this.eCredits.TabIndex = 0;
            this.eCredits.Text = "";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 449);
            this.Controls.Add(this.tabAbout);
            this.Controls.Add(this.lLogo);
            this.Controls.Add(this.pbLogo);
            this.MaximizeBox = false;
            this.Name = "About";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbName)).EndInit();
            this.tabAbout.ResumeLayout(false);
            this.tGeneral.ResumeLayout(false);
            this.tGeneral.PerformLayout();
            this.tCredits.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkLabel linkGitHub;
        private Label lLocation;
        private Label lCoder;
        private Label lVersionWindows;
        private PictureBox pbLogo;
        private System.Windows.Forms.Timer timerAbout;
        private Label lLogo;
        private Label lVersionEngine;
        private PictureBox pbName;
        private TabControl tabAbout;
        private TabPage tGeneral;
        private TabPage tCredits;
        private RichTextBox eCredits;
    }
}