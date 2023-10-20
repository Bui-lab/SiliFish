namespace SiliFish.UI.Controls.General
{
    partial class ProjectionMultiplier
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
            lChemJncMultiplier = new Label();
            lGapJuncMultiplier = new Label();
            numGapMult = new NumericUpDown();
            numChemMult = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numGapMult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numChemMult).BeginInit();
            SuspendLayout();
            // 
            // lChemJncMultiplier
            // 
            lChemJncMultiplier.AutoSize = true;
            lChemJncMultiplier.Location = new Point(3, 42);
            lChemJncMultiplier.Name = "lChemJncMultiplier";
            lChemJncMultiplier.Size = new Size(100, 15);
            lChemJncMultiplier.TabIndex = 5;
            lChemJncMultiplier.Text = "Chem Junc. Mult.";
            // 
            // lGapJuncMultiplier
            // 
            lGapJuncMultiplier.AutoSize = true;
            lGapJuncMultiplier.Location = new Point(4, 15);
            lGapJuncMultiplier.Name = "lGapJuncMultiplier";
            lGapJuncMultiplier.Size = new Size(89, 15);
            lGapJuncMultiplier.TabIndex = 4;
            lGapJuncMultiplier.Text = "Gap Junc. Mult.";
            // 
            // numGapMult
            // 
            numGapMult.DecimalPlaces = 2;
            numGapMult.Location = new Point(115, 11);
            numGapMult.Name = "numGapMult";
            numGapMult.Size = new Size(75, 23);
            numGapMult.TabIndex = 6;
            numGapMult.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numChemMult
            // 
            numChemMult.DecimalPlaces = 2;
            numChemMult.Location = new Point(115, 37);
            numChemMult.Name = "numChemMult";
            numChemMult.Size = new Size(75, 23);
            numChemMult.TabIndex = 7;
            numChemMult.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ProjectionMultiplier
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(numChemMult);
            Controls.Add(numGapMult);
            Controls.Add(lChemJncMultiplier);
            Controls.Add(lGapJuncMultiplier);
            Name = "ProjectionMultiplier";
            Size = new Size(205, 72);
            ((System.ComponentModel.ISupportInitialize)numGapMult).EndInit();
            ((System.ComponentModel.ISupportInitialize)numChemMult).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lChemJncMultiplier;
        private Label lGapJuncMultiplier;
        private NumericUpDown numGapMult;
        private NumericUpDown numChemMult;
    }
}
