namespace SiliFish.UI.Controls.General
{
    partial class SimulationSettingsControl
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
            eSkip = new NumericUpDown();
            lSkip = new Label();
            edt = new NumericUpDown();
            eTimeEnd = new NumericUpDown();
            ldt = new Label();
            lTimeEnd = new Label();
            ((System.ComponentModel.ISupportInitialize)eSkip).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eTimeEnd).BeginInit();
            SuspendLayout();
            // 
            // eSkip
            // 
            eSkip.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            eSkip.Location = new Point(62, 29);
            eSkip.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            eSkip.Name = "eSkip";
            eSkip.Size = new Size(76, 23);
            eSkip.TabIndex = 40;
            // 
            // lSkip
            // 
            lSkip.AutoSize = true;
            lSkip.Location = new Point(1, 32);
            lSkip.Name = "lSkip";
            lSkip.Size = new Size(29, 15);
            lSkip.TabIndex = 37;
            lSkip.Text = "Skip";
            // 
            // edt
            // 
            edt.DecimalPlaces = 2;
            edt.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            edt.Location = new Point(62, 55);
            edt.Name = "edt";
            edt.Size = new Size(76, 23);
            edt.TabIndex = 41;
            edt.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // eTimeEnd
            // 
            eTimeEnd.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            eTimeEnd.Location = new Point(62, 3);
            eTimeEnd.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            eTimeEnd.Name = "eTimeEnd";
            eTimeEnd.Size = new Size(76, 23);
            eTimeEnd.TabIndex = 39;
            eTimeEnd.ThousandsSeparator = true;
            eTimeEnd.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // ldt
            // 
            ldt.AutoSize = true;
            ldt.Location = new Point(1, 58);
            ldt.Name = "ldt";
            ldt.Size = new Size(19, 15);
            ldt.TabIndex = 38;
            ldt.Text = "Δt";
            // 
            // lTimeEnd
            // 
            lTimeEnd.AutoSize = true;
            lTimeEnd.Location = new Point(1, 6);
            lTimeEnd.Name = "lTimeEnd";
            lTimeEnd.Size = new Size(56, 15);
            lTimeEnd.TabIndex = 36;
            lTimeEnd.Text = "Time End";
            // 
            // StimulationSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(eSkip);
            Controls.Add(lSkip);
            Controls.Add(edt);
            Controls.Add(eTimeEnd);
            Controls.Add(ldt);
            Controls.Add(lTimeEnd);
            Name = "StimulationSettingsControl";
            Size = new Size(143, 84);
            ((System.ComponentModel.ISupportInitialize)eSkip).EndInit();
            ((System.ComponentModel.ISupportInitialize)edt).EndInit();
            ((System.ComponentModel.ISupportInitialize)eTimeEnd).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown eSkip;
        private Label lSkip;
        private NumericUpDown edt;
        private NumericUpDown eTimeEnd;
        private Label ldt;
        private Label lTimeEnd;
    }
}
