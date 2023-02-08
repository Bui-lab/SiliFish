namespace Controls
{
    partial class GlobalSettingsControl
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
            this.panel8 = new System.Windows.Forms.Panel();
            this.eTemporaryFolder = new System.Windows.Forms.TextBox();
            this.eOutputFolder = new System.Windows.Forms.TextBox();
            this.lTempFolder = new System.Windows.Forms.Label();
            this.lOutputFolder = new System.Windows.Forms.Label();
            this.propSettings = new System.Windows.Forms.PropertyGrid();
            this.browseFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.eTemporaryFolder);
            this.panel8.Controls.Add(this.eOutputFolder);
            this.panel8.Controls.Add(this.lTempFolder);
            this.panel8.Controls.Add(this.lOutputFolder);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(716, 71);
            this.panel8.TabIndex = 2;
            // 
            // eTemporaryFolder
            // 
            this.eTemporaryFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eTemporaryFolder.Location = new System.Drawing.Point(113, 33);
            this.eTemporaryFolder.Name = "eTemporaryFolder";
            this.eTemporaryFolder.ReadOnly = true;
            this.eTemporaryFolder.Size = new System.Drawing.Size(590, 23);
            this.eTemporaryFolder.TabIndex = 7;
            this.eTemporaryFolder.Click += new System.EventHandler(this.eTemporaryFolder_Click);
            // 
            // eOutputFolder
            // 
            this.eOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eOutputFolder.Location = new System.Drawing.Point(113, 7);
            this.eOutputFolder.Name = "eOutputFolder";
            this.eOutputFolder.ReadOnly = true;
            this.eOutputFolder.Size = new System.Drawing.Size(590, 23);
            this.eOutputFolder.TabIndex = 6;
            this.eOutputFolder.Click += new System.EventHandler(this.eOutputFolder_Click);
            // 
            // lTempFolder
            // 
            this.lTempFolder.AutoSize = true;
            this.lTempFolder.Location = new System.Drawing.Point(6, 37);
            this.lTempFolder.Name = "lTempFolder";
            this.lTempFolder.Size = new System.Drawing.Size(99, 15);
            this.lTempFolder.TabIndex = 5;
            this.lTempFolder.Text = "Temporary Folder";
            // 
            // lOutputFolder
            // 
            this.lOutputFolder.AutoSize = true;
            this.lOutputFolder.Location = new System.Drawing.Point(6, 10);
            this.lOutputFolder.Name = "lOutputFolder";
            this.lOutputFolder.Size = new System.Drawing.Size(81, 15);
            this.lOutputFolder.TabIndex = 4;
            this.lOutputFolder.Text = "Output Folder";
            // 
            // propSettings
            // 
            this.propSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propSettings.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.propSettings.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(212)))));
            this.propSettings.Location = new System.Drawing.Point(0, 71);
            this.propSettings.Name = "propSettings";
            this.propSettings.Size = new System.Drawing.Size(716, 311);
            this.propSettings.TabIndex = 3;
            // 
            // GlobalSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propSettings);
            this.Controls.Add(this.panel8);
            this.Name = "GlobalSettingsControl";
            this.Size = new System.Drawing.Size(716, 382);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel8;
        private TextBox eTemporaryFolder;
        private TextBox eOutputFolder;
        private Label lTempFolder;
        private Label lOutputFolder;
        private PropertyGrid propSettings;
        private FolderBrowserDialog browseFolder;
    }
}
