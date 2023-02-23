namespace SiliFish.UI.Controls
{
    partial class StimulusSettingsControl
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
            this.eValue2 = new System.Windows.Forms.TextBox();
            this.eValue1 = new System.Windows.Forms.TextBox();
            this.lValue1 = new System.Windows.Forms.Label();
            this.lValue2 = new System.Windows.Forms.Label();
            this.lStimulus = new System.Windows.Forms.Label();
            this.ddStimulusMode = new System.Windows.Forms.ComboBox();
            this.eFrequency = new System.Windows.Forms.TextBox();
            this.lFrequency = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // eValue2
            // 
            this.eValue2.Location = new System.Drawing.Point(100, 62);
            this.eValue2.Name = "eValue2";
            this.eValue2.Size = new System.Drawing.Size(52, 23);
            this.eValue2.TabIndex = 11;
            this.eValue2.TextChanged += new System.EventHandler(this.eValue2_TextChanged);
            // 
            // eValue1
            // 
            this.eValue1.Location = new System.Drawing.Point(100, 36);
            this.eValue1.Name = "eValue1";
            this.eValue1.Size = new System.Drawing.Size(52, 23);
            this.eValue1.TabIndex = 9;
            this.eValue1.TextChanged += new System.EventHandler(this.eValue1_TextChanged);
            // 
            // lValue1
            // 
            this.lValue1.AutoSize = true;
            this.lValue1.Location = new System.Drawing.Point(3, 40);
            this.lValue1.Name = "lValue1";
            this.lValue1.Size = new System.Drawing.Size(37, 15);
            this.lValue1.TabIndex = 8;
            this.lValue1.Text = "Mean";
            // 
            // lValue2
            // 
            this.lValue2.AutoSize = true;
            this.lValue2.Location = new System.Drawing.Point(3, 66);
            this.lValue2.Name = "lValue2";
            this.lValue2.Size = new System.Drawing.Size(47, 15);
            this.lValue2.TabIndex = 10;
            this.lValue2.Text = "Std Dev";
            // 
            // lStimulus
            // 
            this.lStimulus.AutoSize = true;
            this.lStimulus.Location = new System.Drawing.Point(3, 13);
            this.lStimulus.Name = "lStimulus";
            this.lStimulus.Size = new System.Drawing.Size(87, 15);
            this.lStimulus.TabIndex = 6;
            this.lStimulus.Text = "Stimulus Mode";
            // 
            // ddStimulusMode
            // 
            this.ddStimulusMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddStimulusMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddStimulusMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddStimulusMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddStimulusMode.FormattingEnabled = true;
            this.ddStimulusMode.Location = new System.Drawing.Point(100, 10);
            this.ddStimulusMode.Name = "ddStimulusMode";
            this.ddStimulusMode.Size = new System.Drawing.Size(95, 23);
            this.ddStimulusMode.TabIndex = 7;
            this.ddStimulusMode.SelectedIndexChanged += new System.EventHandler(this.ddStimulusMode_SelectedIndexChanged);
            // 
            // eFrequency
            // 
            this.eFrequency.Location = new System.Drawing.Point(100, 89);
            this.eFrequency.Name = "eFrequency";
            this.eFrequency.Size = new System.Drawing.Size(52, 23);
            this.eFrequency.TabIndex = 13;
            this.eFrequency.TextChanged += new System.EventHandler(this.eFrequency_TextChanged);
            // 
            // lFrequency
            // 
            this.lFrequency.AutoSize = true;
            this.lFrequency.Location = new System.Drawing.Point(3, 93);
            this.lFrequency.Name = "lFrequency";
            this.lFrequency.Size = new System.Drawing.Size(62, 15);
            this.lFrequency.TabIndex = 12;
            this.lFrequency.Text = "Frequency";
            // 
            // StimulusSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.eFrequency);
            this.Controls.Add(this.lFrequency);
            this.Controls.Add(this.eValue2);
            this.Controls.Add(this.eValue1);
            this.Controls.Add(this.lValue1);
            this.Controls.Add(this.lValue2);
            this.Controls.Add(this.lStimulus);
            this.Controls.Add(this.ddStimulusMode);
            this.Name = "StimulusSettingsControl";
            this.Size = new System.Drawing.Size(203, 122);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox eValue2;
        private TextBox eValue1;
        private Label lValue1;
        private Label lValue2;
        private Label lStimulus;
        private ComboBox ddStimulusMode;
        private TextBox eFrequency;
        private Label lFrequency;
    }
}
