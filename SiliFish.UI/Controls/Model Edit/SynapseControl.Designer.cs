namespace SiliFish.UI.Controls
{
    partial class SynapseControl
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
            this.numEReversal = new System.Windows.Forms.NumericUpDown();
            this.numVthreshold = new System.Windows.Forms.NumericUpDown();
            this.numTauR = new System.Windows.Forms.NumericUpDown();
            this.numTauD = new System.Windows.Forms.NumericUpDown();
            this.lEReversal = new System.Windows.Forms.Label();
            this.lVThreshold = new System.Windows.Forms.Label();
            this.lTauR = new System.Windows.Forms.Label();
            this.lTauD = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numEReversal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVthreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauD)).BeginInit();
            this.SuspendLayout();
            // 
            // numEReversal
            // 
            this.numEReversal.DecimalPlaces = 3;
            this.numEReversal.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEReversal.Location = new System.Drawing.Point(75, 95);
            this.numEReversal.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numEReversal.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numEReversal.Name = "numEReversal";
            this.numEReversal.Size = new System.Drawing.Size(67, 23);
            this.numEReversal.TabIndex = 15;
            // 
            // numVthreshold
            // 
            this.numVthreshold.DecimalPlaces = 3;
            this.numVthreshold.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVthreshold.Location = new System.Drawing.Point(75, 66);
            this.numVthreshold.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numVthreshold.Name = "numVthreshold";
            this.numVthreshold.Size = new System.Drawing.Size(67, 23);
            this.numVthreshold.TabIndex = 13;
            // 
            // numTauR
            // 
            this.numTauR.DecimalPlaces = 3;
            this.numTauR.Location = new System.Drawing.Point(75, 37);
            this.numTauR.Name = "numTauR";
            this.numTauR.Size = new System.Drawing.Size(67, 23);
            this.numTauR.TabIndex = 11;
            // 
            // numTauD
            // 
            this.numTauD.DecimalPlaces = 3;
            this.numTauD.Location = new System.Drawing.Point(75, 8);
            this.numTauD.Name = "numTauD";
            this.numTauD.Size = new System.Drawing.Size(67, 23);
            this.numTauD.TabIndex = 9;
            this.numTauD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lEReversal
            // 
            this.lEReversal.AutoSize = true;
            this.lEReversal.Location = new System.Drawing.Point(3, 97);
            this.lEReversal.Name = "lEReversal";
            this.lEReversal.Size = new System.Drawing.Size(32, 15);
            this.lEReversal.TabIndex = 14;
            this.lEReversal.Text = "E rev";
            // 
            // lVThreshold
            // 
            this.lVThreshold.AutoSize = true;
            this.lVThreshold.Location = new System.Drawing.Point(3, 68);
            this.lVThreshold.Name = "lVThreshold";
            this.lVThreshold.Size = new System.Drawing.Size(69, 15);
            this.lVThreshold.TabIndex = 12;
            this.lVThreshold.Text = "Threshold V";
            // 
            // lTauR
            // 
            this.lTauR.AutoSize = true;
            this.lTauR.Location = new System.Drawing.Point(3, 39);
            this.lTauR.Name = "lTauR";
            this.lTauR.Size = new System.Drawing.Size(17, 15);
            this.lTauR.TabIndex = 10;
            this.lTauR.Text = "τr";
            // 
            // lTauD
            // 
            this.lTauD.AutoSize = true;
            this.lTauD.Location = new System.Drawing.Point(3, 10);
            this.lTauD.Name = "lTauD";
            this.lTauD.Size = new System.Drawing.Size(20, 15);
            this.lTauD.TabIndex = 8;
            this.lTauD.Text = "τd";
            // 
            // SynapseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.numEReversal);
            this.Controls.Add(this.numVthreshold);
            this.Controls.Add(this.numTauR);
            this.Controls.Add(this.numTauD);
            this.Controls.Add(this.lEReversal);
            this.Controls.Add(this.lVThreshold);
            this.Controls.Add(this.lTauR);
            this.Controls.Add(this.lTauD);
            this.Name = "SynapseControl";
            this.Size = new System.Drawing.Size(152, 127);
            ((System.ComponentModel.ISupportInitialize)(this.numEReversal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVthreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTauD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown numEReversal;
        private NumericUpDown numVthreshold;
        private NumericUpDown numTauR;
        private NumericUpDown numTauD;
        private Label lEReversal;
        private Label lVThreshold;
        private Label lTauR;
        private Label lTauD;
    }
}
