namespace SiliFish.UI.Controls.General
{
    partial class ConductanceMultiplier
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
            pGap = new Panel();
            pChem = new Panel();
            ((System.ComponentModel.ISupportInitialize)numGapMult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numChemMult).BeginInit();
            pGap.SuspendLayout();
            pChem.SuspendLayout();
            SuspendLayout();
            // 
            // lChemJncMultiplier
            // 
            lChemJncMultiplier.Location = new Point(3, 5);
            lChemJncMultiplier.Margin = new Padding(3, 5, 3, 0);
            lChemJncMultiplier.Name = "lChemJncMultiplier";
            lChemJncMultiplier.Size = new Size(100, 15);
            lChemJncMultiplier.TabIndex = 5;
            lChemJncMultiplier.Text = "Chem Junc. Mult.";
            // 
            // lGapJuncMultiplier
            // 
            lGapJuncMultiplier.Location = new Point(3, 5);
            lGapJuncMultiplier.Margin = new Padding(3, 5, 3, 0);
            lGapJuncMultiplier.Name = "lGapJuncMultiplier";
            lGapJuncMultiplier.Size = new Size(100, 15);
            lGapJuncMultiplier.TabIndex = 4;
            lGapJuncMultiplier.Text = "Gap Junc. Mult.";
            // 
            // numGapMult
            // 
            numGapMult.DecimalPlaces = 5;
            numGapMult.Location = new Point(109, 3);
            numGapMult.Name = "numGapMult";
            numGapMult.Size = new Size(75, 23);
            numGapMult.TabIndex = 6;
            numGapMult.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numGapMult.ValueChanged += numGapMult_ValueChanged;
            // 
            // numChemMult
            // 
            numChemMult.DecimalPlaces = 5;
            numChemMult.Location = new Point(109, 5);
            numChemMult.Name = "numChemMult";
            numChemMult.Size = new Size(75, 23);
            numChemMult.TabIndex = 7;
            numChemMult.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numChemMult.ValueChanged += numChemMult_ValueChanged;
            // 
            // pGap
            // 
            pGap.Controls.Add(numGapMult);
            pGap.Controls.Add(lGapJuncMultiplier);
            pGap.Dock = DockStyle.Top;
            pGap.Location = new Point(0, 0);
            pGap.Name = "pGap";
            pGap.Size = new Size(198, 32);
            pGap.TabIndex = 9;
            // 
            // pChem
            // 
            pChem.Controls.Add(numChemMult);
            pChem.Controls.Add(lChemJncMultiplier);
            pChem.Dock = DockStyle.Top;
            pChem.Location = new Point(0, 32);
            pChem.Name = "pChem";
            pChem.Size = new Size(198, 33);
            pChem.TabIndex = 10;
            // 
            // ConductanceMultiplier
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pChem);
            Controls.Add(pGap);
            Name = "ConductanceMultiplier";
            Size = new Size(198, 66);
            ((System.ComponentModel.ISupportInitialize)numGapMult).EndInit();
            ((System.ComponentModel.ISupportInitialize)numChemMult).EndInit();
            pGap.ResumeLayout(false);
            pChem.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lChemJncMultiplier;
        private Label lGapJuncMultiplier;
        private NumericUpDown numGapMult;
        private NumericUpDown numChemMult;
        private Panel pGap;
        private Panel pChem;
    }
}
