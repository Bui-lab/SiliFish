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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            linkGitHub = new LinkLabel();
            lLocation = new Label();
            lCoder = new Label();
            lVersionWindows = new Label();
            pbLogo = new PictureBox();
            timerAbout = new System.Windows.Forms.Timer(components);
            lLogo = new Label();
            lVersionEngine = new Label();
            pbName = new PictureBox();
            tabAbout = new TabControl();
            tGeneral = new TabPage();
            tCredits = new TabPage();
            eCredits = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbName).BeginInit();
            tabAbout.SuspendLayout();
            tGeneral.SuspendLayout();
            tCredits.SuspendLayout();
            SuspendLayout();
            // 
            // linkGitHub
            // 
            linkGitHub.AutoSize = true;
            linkGitHub.Location = new Point(6, 145);
            linkGitHub.Margin = new Padding(2, 0, 2, 0);
            linkGitHub.Name = "linkGitHub";
            linkGitHub.Size = new Size(193, 15);
            linkGitHub.TabIndex = 13;
            linkGitHub.TabStop = true;
            linkGitHub.Text = "https://github.com/Bui-lab/SiliFish";
            // 
            // lLocation
            // 
            lLocation.AutoSize = true;
            lLocation.Location = new Point(6, 120);
            lLocation.Name = "lLocation";
            lLocation.Size = new Size(138, 15);
            lLocation.TabIndex = 11;
            lLocation.Text = "University of Ottawa, ON";
            // 
            // lCoder
            // 
            lCoder.AutoSize = true;
            lCoder.Location = new Point(6, 101);
            lCoder.Name = "lCoder";
            lCoder.Size = new Size(131, 15);
            lCoder.TabIndex = 9;
            lCoder.Text = "Emine Topcu @ Bui Lab";
            // 
            // lVersionWindows
            // 
            lVersionWindows.AutoSize = true;
            lVersionWindows.Location = new Point(6, 53);
            lVersionWindows.Name = "lVersionWindows";
            lVersionWindows.Size = new Size(72, 15);
            lVersionWindows.TabIndex = 8;
            lVersionWindows.Text = "Version 0.1.1";
            // 
            // pbLogo
            // 
            pbLogo.Image = (Image)resources.GetObject("pbLogo.Image");
            pbLogo.Location = new Point(-1, 0);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(328, 251);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 14;
            pbLogo.TabStop = false;
            // 
            // lLogo
            // 
            lLogo.AutoSize = true;
            lLogo.Location = new Point(174, 234);
            lLogo.Name = "lLogo";
            lLogo.Size = new Size(150, 15);
            lLogo.TabIndex = 15;
            lLogo.Text = "Logo design by Sibel Aydin";
            // 
            // lVersionEngine
            // 
            lVersionEngine.AutoSize = true;
            lVersionEngine.Location = new Point(6, 72);
            lVersionEngine.Name = "lVersionEngine";
            lVersionEngine.Size = new Size(72, 15);
            lVersionEngine.TabIndex = 17;
            lVersionEngine.Text = "Version 0.1.1";
            // 
            // pbName
            // 
            pbName.Image = (Image)resources.GetObject("pbName.Image");
            pbName.Location = new Point(6, 6);
            pbName.Name = "pbName";
            pbName.Size = new Size(147, 44);
            pbName.SizeMode = PictureBoxSizeMode.AutoSize;
            pbName.TabIndex = 18;
            pbName.TabStop = false;
            // 
            // tabAbout
            // 
            tabAbout.Controls.Add(tGeneral);
            tabAbout.Controls.Add(tCredits);
            tabAbout.Dock = DockStyle.Bottom;
            tabAbout.Location = new Point(0, 252);
            tabAbout.Name = "tabAbout";
            tabAbout.SelectedIndex = 0;
            tabAbout.Size = new Size(326, 197);
            tabAbout.TabIndex = 19;
            // 
            // tGeneral
            // 
            tGeneral.Controls.Add(pbName);
            tGeneral.Controls.Add(lVersionWindows);
            tGeneral.Controls.Add(lVersionEngine);
            tGeneral.Controls.Add(lCoder);
            tGeneral.Controls.Add(lLocation);
            tGeneral.Controls.Add(linkGitHub);
            tGeneral.Location = new Point(4, 24);
            tGeneral.Name = "tGeneral";
            tGeneral.Padding = new Padding(3);
            tGeneral.Size = new Size(318, 169);
            tGeneral.TabIndex = 0;
            tGeneral.Text = "General";
            tGeneral.UseVisualStyleBackColor = true;
            // 
            // tCredits
            // 
            tCredits.Controls.Add(eCredits);
            tCredits.Location = new Point(4, 24);
            tCredits.Name = "tCredits";
            tCredits.Padding = new Padding(3);
            tCredits.Size = new Size(318, 169);
            tCredits.TabIndex = 1;
            tCredits.Text = "Credits";
            tCredits.UseVisualStyleBackColor = true;
            // 
            // eCredits
            // 
            eCredits.Dock = DockStyle.Fill;
            eCredits.Location = new Point(3, 3);
            eCredits.Name = "eCredits";
            eCredits.ReadOnly = true;
            eCredits.Size = new Size(312, 163);
            eCredits.TabIndex = 0;
            eCredits.Text = "";
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(326, 449);
            Controls.Add(tabAbout);
            Controls.Add(lLogo);
            Controls.Add(pbLogo);
            MaximizeBox = false;
            Name = "About";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "About";
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbName).EndInit();
            tabAbout.ResumeLayout(false);
            tGeneral.ResumeLayout(false);
            tGeneral.PerformLayout();
            tCredits.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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