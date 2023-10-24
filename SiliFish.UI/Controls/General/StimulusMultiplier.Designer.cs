namespace SiliFish.UI.Controls.General
{
    partial class StimulusMultiplier
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
            lStimulus = new Label();
            numStimulus = new NumericUpDown();
            rbValue = new RadioButton();
            rbMultiplier = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)numStimulus).BeginInit();
            SuspendLayout();
            // 
            // lStimulus
            // 
            lStimulus.AutoSize = true;
            lStimulus.Location = new Point(4, 15);
            lStimulus.Name = "lStimulus";
            lStimulus.Size = new Size(53, 15);
            lStimulus.TabIndex = 4;
            lStimulus.Text = "Stimulus";
            // 
            // numStimulus
            // 
            numStimulus.DecimalPlaces = 2;
            numStimulus.Location = new Point(63, 13);
            numStimulus.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numStimulus.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numStimulus.Name = "numStimulus";
            numStimulus.Size = new Size(75, 23);
            numStimulus.TabIndex = 6;
            numStimulus.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numStimulus.ValueChanged += numStimulus_ValueChanged;
            // 
            // rbValue
            // 
            rbValue.AutoSize = true;
            rbValue.Location = new Point(4, 42);
            rbValue.Name = "rbValue";
            rbValue.Size = new Size(53, 19);
            rbValue.TabIndex = 7;
            rbValue.Text = "Value";
            rbValue.UseVisualStyleBackColor = true;
            rbValue.CheckedChanged += rbValue_CheckedChanged;
            // 
            // rbMultiplier
            // 
            rbMultiplier.AutoSize = true;
            rbMultiplier.Checked = true;
            rbMultiplier.Location = new Point(63, 42);
            rbMultiplier.Name = "rbMultiplier";
            rbMultiplier.Size = new Size(76, 19);
            rbMultiplier.TabIndex = 8;
            rbMultiplier.TabStop = true;
            rbMultiplier.Text = "Multiplier";
            rbMultiplier.UseVisualStyleBackColor = true;
            // 
            // StimulusMultiplier
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rbMultiplier);
            Controls.Add(rbValue);
            Controls.Add(numStimulus);
            Controls.Add(lStimulus);
            Name = "StimulusMultiplier";
            Size = new Size(205, 72);
            ((System.ComponentModel.ISupportInitialize)numStimulus).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lStimulus;
        private NumericUpDown numStimulus;
        private RadioButton rbValue;
        private RadioButton rbMultiplier;
    }
}
