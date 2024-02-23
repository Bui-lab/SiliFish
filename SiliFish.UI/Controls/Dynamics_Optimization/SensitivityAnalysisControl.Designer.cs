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
            lNumOfValues = new Label();
            eNumOfValues = new TextBox();
            cbLogScale = new CheckBox();
            eMaxMultiplier = new TextBox();
            lDash = new Label();
            eMinMultiplier = new TextBox();
            lMultiplierRange = new Label();
            ddParameter = new ComboBox();
            lParameter = new Label();
            btnRunAnalysis = new Button();
            pParameters = new Panel();
            pSingleParam = new Panel();
            eParameter = new TextBox();
            lSingleParameter = new Label();
            pRange = new Panel();
            pParameters.SuspendLayout();
            pSingleParam.SuspendLayout();
            pRange.SuspendLayout();
            SuspendLayout();
            // 
            // lNumOfValues
            // 
            lNumOfValues.AutoSize = true;
            lNumOfValues.Location = new Point(4, 35);
            lNumOfValues.Name = "lNumOfValues";
            lNumOfValues.Size = new Size(64, 15);
            lNumOfValues.TabIndex = 40;
            lNumOfValues.Text = "# of Values";
            // 
            // eNumOfValues
            // 
            eNumOfValues.Location = new Point(97, 31);
            eNumOfValues.Name = "eNumOfValues";
            eNumOfValues.Size = new Size(41, 23);
            eNumOfValues.TabIndex = 39;
            eNumOfValues.Text = "10";
            // 
            // cbLogScale
            // 
            cbLogScale.AutoSize = true;
            cbLogScale.Location = new Point(4, 61);
            cbLogScale.Name = "cbLogScale";
            cbLogScale.Size = new Size(76, 19);
            cbLogScale.TabIndex = 38;
            cbLogScale.Text = "Log Scale";
            cbLogScale.UseVisualStyleBackColor = true;
            // 
            // eMaxMultiplier
            // 
            eMaxMultiplier.Location = new Point(149, 6);
            eMaxMultiplier.Name = "eMaxMultiplier";
            eMaxMultiplier.Size = new Size(41, 23);
            eMaxMultiplier.TabIndex = 36;
            eMaxMultiplier.Text = "10";
            // 
            // lDash
            // 
            lDash.AutoSize = true;
            lDash.Location = new Point(138, 9);
            lDash.Name = "lDash";
            lDash.Size = new Size(12, 15);
            lDash.TabIndex = 37;
            lDash.Text = "-";
            // 
            // eMinMultiplier
            // 
            eMinMultiplier.Location = new Point(97, 6);
            eMinMultiplier.Name = "eMinMultiplier";
            eMinMultiplier.Size = new Size(41, 23);
            eMinMultiplier.TabIndex = 35;
            eMinMultiplier.Text = "0.1";
            // 
            // lMultiplierRange
            // 
            lMultiplierRange.AutoSize = true;
            lMultiplierRange.Location = new Point(4, 10);
            lMultiplierRange.Name = "lMultiplierRange";
            lMultiplierRange.Size = new Size(94, 15);
            lMultiplierRange.TabIndex = 34;
            lMultiplierRange.Text = "Multiplier Range";
            // 
            // ddParameter
            // 
            ddParameter.BackColor = Color.WhiteSmoke;
            ddParameter.DropDownStyle = ComboBoxStyle.DropDownList;
            ddParameter.FlatStyle = FlatStyle.Flat;
            ddParameter.FormattingEnabled = true;
            ddParameter.Location = new Point(78, 3);
            ddParameter.Name = "ddParameter";
            ddParameter.Size = new Size(121, 23);
            ddParameter.TabIndex = 33;
            // 
            // lParameter
            // 
            lParameter.AutoSize = true;
            lParameter.Location = new Point(4, 7);
            lParameter.Name = "lParameter";
            lParameter.Size = new Size(61, 15);
            lParameter.TabIndex = 32;
            lParameter.Text = "Parameter";
            // 
            // btnRunAnalysis
            // 
            btnRunAnalysis.BackColor = Color.FromArgb(96, 125, 139);
            btnRunAnalysis.FlatAppearance.BorderColor = Color.LightGray;
            btnRunAnalysis.FlatStyle = FlatStyle.Flat;
            btnRunAnalysis.Font = new Font("Segoe UI", 8F);
            btnRunAnalysis.ForeColor = Color.White;
            btnRunAnalysis.Location = new Point(128, 60);
            btnRunAnalysis.Name = "btnRunAnalysis";
            btnRunAnalysis.Size = new Size(63, 23);
            btnRunAnalysis.TabIndex = 31;
            btnRunAnalysis.Text = "Run";
            btnRunAnalysis.UseVisualStyleBackColor = false;
            btnRunAnalysis.Click += btnRunAnalysis_Click;
            // 
            // pParameters
            // 
            pParameters.Controls.Add(lParameter);
            pParameters.Controls.Add(ddParameter);
            pParameters.Dock = DockStyle.Top;
            pParameters.Location = new Point(0, 0);
            pParameters.Name = "pParameters";
            pParameters.Size = new Size(202, 30);
            pParameters.TabIndex = 41;
            // 
            // pSingleParam
            // 
            pSingleParam.Controls.Add(eParameter);
            pSingleParam.Controls.Add(lSingleParameter);
            pSingleParam.Dock = DockStyle.Top;
            pSingleParam.Location = new Point(0, 30);
            pSingleParam.Name = "pSingleParam";
            pSingleParam.Size = new Size(202, 30);
            pSingleParam.TabIndex = 42;
            // 
            // eParameter
            // 
            eParameter.Location = new Point(97, 4);
            eParameter.Name = "eParameter";
            eParameter.Size = new Size(93, 23);
            eParameter.TabIndex = 36;
            eParameter.Text = "0.1";
            // 
            // lSingleParameter
            // 
            lSingleParameter.AutoSize = true;
            lSingleParameter.Location = new Point(4, 7);
            lSingleParameter.Name = "lSingleParameter";
            lSingleParameter.Size = new Size(61, 15);
            lSingleParameter.TabIndex = 32;
            lSingleParameter.Text = "Parameter";
            // 
            // pRange
            // 
            pRange.Controls.Add(eMinMultiplier);
            pRange.Controls.Add(lMultiplierRange);
            pRange.Controls.Add(btnRunAnalysis);
            pRange.Controls.Add(lNumOfValues);
            pRange.Controls.Add(lDash);
            pRange.Controls.Add(eNumOfValues);
            pRange.Controls.Add(eMaxMultiplier);
            pRange.Controls.Add(cbLogScale);
            pRange.Dock = DockStyle.Top;
            pRange.Location = new Point(0, 60);
            pRange.Name = "pRange";
            pRange.Size = new Size(202, 88);
            pRange.TabIndex = 43;
            // 
            // SensitivityAnalysisControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            Controls.Add(pRange);
            Controls.Add(pSingleParam);
            Controls.Add(pParameters);
            Name = "SensitivityAnalysisControl";
            Size = new Size(202, 149);
            pParameters.ResumeLayout(false);
            pParameters.PerformLayout();
            pSingleParam.ResumeLayout(false);
            pSingleParam.PerformLayout();
            pRange.ResumeLayout(false);
            pRange.PerformLayout();
            ResumeLayout(false);
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
        private Panel pParameters;
        private Panel pSingleParam;
        private TextBox eParameter;
        private Label lSingleParameter;
        private Panel pRange;
    }
}
