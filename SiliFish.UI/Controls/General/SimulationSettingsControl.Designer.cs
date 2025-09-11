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
            edt = new NumericUpDown();
            eTimeEnd = new NumericUpDown();
            ldt = new Label();
            lTimeEnd = new Label();
            ((System.ComponentModel.ISupportInitialize)edt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eTimeEnd).BeginInit();
            SuspendLayout();
            // 
            // edt
            // 
            edt.DecimalPlaces = 2;
            edt.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            edt.Location = new Point(62, 29);
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
            ldt.Location = new Point(1, 32);
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
            lTimeEnd.Size = new Size(57, 15);
            lTimeEnd.TabIndex = 36;
            lTimeEnd.Text = "Time End";
            // 
            // SimulationSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(edt);
            Controls.Add(eTimeEnd);
            Controls.Add(ldt);
            Controls.Add(lTimeEnd);
            Name = "SimulationSettingsControl";
            Size = new Size(143, 60);
            ((System.ComponentModel.ISupportInitialize)edt).EndInit();
            ((System.ComponentModel.ISupportInitialize)eTimeEnd).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private NumericUpDown edt;
        private NumericUpDown eTimeEnd;
        private Label ldt;
        private Label lTimeEnd;
    }
}
