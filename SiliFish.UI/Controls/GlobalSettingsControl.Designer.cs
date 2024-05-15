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
            panel8 = new Panel();
            eTemporaryFolder = new TextBox();
            eOutputFolder = new TextBox();
            lTempFolder = new Label();
            lOutputFolder = new Label();
            propSettings = new PropertyGrid();
            browseFolder = new FolderBrowserDialog();
            eDatabaseFile = new TextBox();
            lDatabaseFile = new Label();
            browseFile = new OpenFileDialog();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // panel8
            // 
            panel8.Controls.Add(eDatabaseFile);
            panel8.Controls.Add(lDatabaseFile);
            panel8.Controls.Add(eTemporaryFolder);
            panel8.Controls.Add(eOutputFolder);
            panel8.Controls.Add(lTempFolder);
            panel8.Controls.Add(lOutputFolder);
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(0, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(716, 99);
            panel8.TabIndex = 2;
            // 
            // eTemporaryFolder
            // 
            eTemporaryFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eTemporaryFolder.Location = new Point(113, 33);
            eTemporaryFolder.Name = "eTemporaryFolder";
            eTemporaryFolder.ReadOnly = true;
            eTemporaryFolder.Size = new Size(590, 23);
            eTemporaryFolder.TabIndex = 7;
            eTemporaryFolder.Click += eTemporaryFolder_Click;
            // 
            // eOutputFolder
            // 
            eOutputFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eOutputFolder.Location = new Point(113, 7);
            eOutputFolder.Name = "eOutputFolder";
            eOutputFolder.ReadOnly = true;
            eOutputFolder.Size = new Size(590, 23);
            eOutputFolder.TabIndex = 6;
            eOutputFolder.Click += eOutputFolder_Click;
            // 
            // lTempFolder
            // 
            lTempFolder.AutoSize = true;
            lTempFolder.Location = new Point(6, 37);
            lTempFolder.Name = "lTempFolder";
            lTempFolder.Size = new Size(99, 15);
            lTempFolder.TabIndex = 5;
            lTempFolder.Text = "Temporary Folder";
            // 
            // lOutputFolder
            // 
            lOutputFolder.AutoSize = true;
            lOutputFolder.Location = new Point(6, 10);
            lOutputFolder.Name = "lOutputFolder";
            lOutputFolder.Size = new Size(81, 15);
            lOutputFolder.TabIndex = 4;
            lOutputFolder.Text = "Output Folder";
            // 
            // propSettings
            // 
            propSettings.BackColor = Color.FromArgb(207, 216, 220);
            propSettings.Dock = DockStyle.Fill;
            propSettings.HelpBackColor = Color.FromArgb(207, 216, 220);
            propSettings.HelpBorderColor = Color.FromArgb(0, 184, 212);
            propSettings.Location = new Point(0, 99);
            propSettings.Name = "propSettings";
            propSettings.Size = new Size(716, 283);
            propSettings.TabIndex = 3;
            // 
            // eDatabaseFile
            // 
            eDatabaseFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eDatabaseFile.Location = new Point(113, 62);
            eDatabaseFile.Name = "eDatabaseFile";
            eDatabaseFile.ReadOnly = true;
            eDatabaseFile.Size = new Size(590, 23);
            eDatabaseFile.TabIndex = 9;
            eDatabaseFile.Click += eDatabaseFile_Click;
            // 
            // lDatabaseFile
            // 
            lDatabaseFile.AutoSize = true;
            lDatabaseFile.Location = new Point(6, 66);
            lDatabaseFile.Name = "lDatabaseFile";
            lDatabaseFile.Size = new Size(76, 15);
            lDatabaseFile.TabIndex = 8;
            lDatabaseFile.Text = "Database File";
            // 
            // browseFile
            // 
            browseFile.FileName = "SiliFish.sqlite";
            browseFile.Filter = "Sqlite files|*.sqlite";
            // 
            // GlobalSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(propSettings);
            Controls.Add(panel8);
            Name = "GlobalSettingsControl";
            Size = new Size(716, 382);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel8;
        private TextBox eTemporaryFolder;
        private TextBox eOutputFolder;
        private Label lTempFolder;
        private Label lOutputFolder;
        private PropertyGrid propSettings;
        private FolderBrowserDialog browseFolder;
        private TextBox eDatabaseFile;
        private Label lDatabaseFile;
        private OpenFileDialog browseFile;
    }
}
