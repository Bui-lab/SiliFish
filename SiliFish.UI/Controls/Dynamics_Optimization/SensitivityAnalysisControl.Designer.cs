namespace SiliFish.UI.Controls
{
    partial class SensitivityAnalysisControl
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
            this.lNumOfValues = new System.Windows.Forms.Label();
            this.eNumOfValues = new System.Windows.Forms.TextBox();
            this.cbLogScale = new System.Windows.Forms.CheckBox();
            this.eMaxMultiplier = new System.Windows.Forms.TextBox();
            this.lDash = new System.Windows.Forms.Label();
            this.eMinMultiplier = new System.Windows.Forms.TextBox();
            this.lMultiplierRange = new System.Windows.Forms.Label();
            this.ddParameter = new System.Windows.Forms.ComboBox();
            this.lParameter = new System.Windows.Forms.Label();
            this.btnRunAnalysis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lNumOfValues
            // 
            this.lNumOfValues.AutoSize = true;
            this.lNumOfValues.Location = new System.Drawing.Point(3, 63);
            this.lNumOfValues.Name = "lNumOfValues";
            this.lNumOfValues.Size = new System.Drawing.Size(64, 15);
            this.lNumOfValues.TabIndex = 40;
            this.lNumOfValues.Text = "# of Values";
            // 
            // eNumOfValues
            // 
            this.eNumOfValues.Location = new System.Drawing.Point(97, 60);
            this.eNumOfValues.Name = "eNumOfValues";
            this.eNumOfValues.Size = new System.Drawing.Size(41, 23);
            this.eNumOfValues.TabIndex = 39;
            this.eNumOfValues.Text = "10";
            // 
            // cbLogScale
            // 
            this.cbLogScale.AutoSize = true;
            this.cbLogScale.Location = new System.Drawing.Point(4, 89);
            this.cbLogScale.Name = "cbLogScale";
            this.cbLogScale.Size = new System.Drawing.Size(76, 19);
            this.cbLogScale.TabIndex = 38;
            this.cbLogScale.Text = "Log Scale";
            this.cbLogScale.UseVisualStyleBackColor = true;
            // 
            // eMaxMultiplier
            // 
            this.eMaxMultiplier.Location = new System.Drawing.Point(149, 35);
            this.eMaxMultiplier.Name = "eMaxMultiplier";
            this.eMaxMultiplier.Size = new System.Drawing.Size(41, 23);
            this.eMaxMultiplier.TabIndex = 36;
            this.eMaxMultiplier.Text = "10";
            // 
            // lDash
            // 
            this.lDash.AutoSize = true;
            this.lDash.Location = new System.Drawing.Point(138, 38);
            this.lDash.Name = "lDash";
            this.lDash.Size = new System.Drawing.Size(12, 15);
            this.lDash.TabIndex = 37;
            this.lDash.Text = "-";
            // 
            // eMinMultiplier
            // 
            this.eMinMultiplier.Location = new System.Drawing.Point(97, 35);
            this.eMinMultiplier.Name = "eMinMultiplier";
            this.eMinMultiplier.Size = new System.Drawing.Size(41, 23);
            this.eMinMultiplier.TabIndex = 35;
            this.eMinMultiplier.Text = "0.1";
            // 
            // lMultiplierRange
            // 
            this.lMultiplierRange.AutoSize = true;
            this.lMultiplierRange.Location = new System.Drawing.Point(3, 38);
            this.lMultiplierRange.Name = "lMultiplierRange";
            this.lMultiplierRange.Size = new System.Drawing.Size(94, 15);
            this.lMultiplierRange.TabIndex = 34;
            this.lMultiplierRange.Text = "Multiplier Range";
            // 
            // ddParameter
            // 
            this.ddParameter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddParameter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddParameter.FormattingEnabled = true;
            this.ddParameter.Location = new System.Drawing.Point(70, 6);
            this.ddParameter.Name = "ddParameter";
            this.ddParameter.Size = new System.Drawing.Size(121, 23);
            this.ddParameter.TabIndex = 33;
            // 
            // lParameter
            // 
            this.lParameter.AutoSize = true;
            this.lParameter.Location = new System.Drawing.Point(3, 12);
            this.lParameter.Name = "lParameter";
            this.lParameter.Size = new System.Drawing.Size(61, 15);
            this.lParameter.TabIndex = 32;
            this.lParameter.Text = "Parameter";
            // 
            // btnRunAnalysis
            // 
            this.btnRunAnalysis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnRunAnalysis.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnRunAnalysis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunAnalysis.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRunAnalysis.ForeColor = System.Drawing.Color.White;
            this.btnRunAnalysis.Location = new System.Drawing.Point(128, 89);
            this.btnRunAnalysis.Name = "btnRunAnalysis";
            this.btnRunAnalysis.Size = new System.Drawing.Size(63, 23);
            this.btnRunAnalysis.TabIndex = 31;
            this.btnRunAnalysis.Text = "Run";
            this.btnRunAnalysis.UseVisualStyleBackColor = false;
            this.btnRunAnalysis.Click += new System.EventHandler(this.btnRunAnalysis_Click);
            // 
            // SensitivityAnalysisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lNumOfValues);
            this.Controls.Add(this.eNumOfValues);
            this.Controls.Add(this.cbLogScale);
            this.Controls.Add(this.eMaxMultiplier);
            this.Controls.Add(this.lDash);
            this.Controls.Add(this.eMinMultiplier);
            this.Controls.Add(this.lMultiplierRange);
            this.Controls.Add(this.ddParameter);
            this.Controls.Add(this.lParameter);
            this.Controls.Add(this.btnRunAnalysis);
            this.Name = "SensitivityAnalysisControl";
            this.Size = new System.Drawing.Size(202, 118);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lNumOfValues;
        private TextBox eNumOfValues;
        private CheckBox cbLogScale;
        private TextBox eMaxMultiplier;
        private Label lDash;
        private TextBox eMinMultiplier;
        private Label lMultiplierRange;
        private ComboBox ddParameter;
        private Label lParameter;
        private Button btnRunAnalysis;
    }
}
