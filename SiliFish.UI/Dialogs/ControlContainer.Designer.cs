namespace SiliFish.UI
{
    partial class ControlContainer
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
            pBottom = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            pMain = new Panel();
            pBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pBottom
            // 
            pBottom.Controls.Add(btnCancel);
            pBottom.Controls.Add(btnSave);
            pBottom.Dock = DockStyle.Bottom;
            pBottom.Location = new Point(0, 242);
            pBottom.Name = "pBottom";
            pBottom.Size = new Size(318, 37);
            pBottom.TabIndex = 23;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(93, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.DialogResult = DialogResult.OK;
            btnSave.Location = new Point(12, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // pMain
            // 
            pMain.AutoSize = true;
            pMain.Dock = DockStyle.Fill;
            pMain.Location = new Point(0, 0);
            pMain.Name = "pMain";
            pMain.Size = new Size(318, 242);
            pMain.TabIndex = 24;
            // 
            // ControlContainer
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = btnCancel;
            ClientSize = new Size(318, 279);
            Controls.Add(pMain);
            Controls.Add(pBottom);
            Name = "ControlContainer";
            Text = "Form1";
            Load += ControlContainer_Load;
            pBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pBottom;
        private Button btnCancel;
        private Button btnSave;
        private Panel pMain;
    }
}