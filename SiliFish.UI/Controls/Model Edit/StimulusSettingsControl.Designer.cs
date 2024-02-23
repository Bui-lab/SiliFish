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
            eValue2 = new TextBox();
            eValue1 = new TextBox();
            lValue1 = new Label();
            lValue2 = new Label();
            lStimulus = new Label();
            ddStimulusMode = new ComboBox();
            eFrequency = new TextBox();
            lFrequency = new Label();
            pLayout = new TableLayoutPanel();
            lHz = new Label();
            pLayout.SuspendLayout();
            SuspendLayout();
            // 
            // eValue2
            // 
            eValue2.Location = new Point(101, 49);
            eValue2.Margin = new Padding(1);
            eValue2.Name = "eValue2";
            eValue2.Size = new Size(44, 23);
            eValue2.TabIndex = 11;
            eValue2.TextChanged += eValue2_TextChanged;
            // 
            // eValue1
            // 
            eValue1.Location = new Point(101, 25);
            eValue1.Margin = new Padding(1);
            eValue1.Name = "eValue1";
            eValue1.Size = new Size(44, 23);
            eValue1.TabIndex = 9;
            eValue1.TextChanged += eValue1_TextChanged;
            // 
            // lValue1
            // 
            lValue1.AutoSize = true;
            lValue1.Location = new Point(3, 29);
            lValue1.Margin = new Padding(3, 5, 3, 0);
            lValue1.Name = "lValue1";
            lValue1.Size = new Size(37, 15);
            lValue1.TabIndex = 8;
            lValue1.Text = "Mean";
            // 
            // lValue2
            // 
            lValue2.AutoSize = true;
            lValue2.Location = new Point(3, 53);
            lValue2.Margin = new Padding(3, 5, 3, 0);
            lValue2.Name = "lValue2";
            lValue2.Size = new Size(47, 15);
            lValue2.TabIndex = 10;
            lValue2.Text = "Std Dev";
            // 
            // lStimulus
            // 
            lStimulus.AutoSize = true;
            lStimulus.Location = new Point(3, 5);
            lStimulus.Margin = new Padding(3, 5, 3, 0);
            lStimulus.Name = "lStimulus";
            lStimulus.Size = new Size(87, 15);
            lStimulus.TabIndex = 6;
            lStimulus.Text = "Stimulus Mode";
            // 
            // ddStimulusMode
            // 
            ddStimulusMode.BackColor = Color.WhiteSmoke;
            pLayout.SetColumnSpan(ddStimulusMode, 2);
            ddStimulusMode.DropDownStyle = ComboBoxStyle.DropDownList;
            ddStimulusMode.FlatStyle = FlatStyle.Flat;
            ddStimulusMode.FormattingEnabled = true;
            ddStimulusMode.Location = new Point(101, 1);
            ddStimulusMode.Margin = new Padding(1);
            ddStimulusMode.Name = "ddStimulusMode";
            ddStimulusMode.Size = new Size(109, 23);
            ddStimulusMode.TabIndex = 7;
            ddStimulusMode.SelectedIndexChanged += ddStimulusMode_SelectedIndexChanged;
            // 
            // eFrequency
            // 
            eFrequency.Location = new Point(101, 73);
            eFrequency.Margin = new Padding(1);
            eFrequency.Name = "eFrequency";
            eFrequency.Size = new Size(44, 23);
            eFrequency.TabIndex = 13;
            eFrequency.TextChanged += eFrequency_TextChanged;
            // 
            // lFrequency
            // 
            lFrequency.AutoSize = true;
            lFrequency.Location = new Point(3, 77);
            lFrequency.Margin = new Padding(3, 5, 3, 0);
            lFrequency.Name = "lFrequency";
            lFrequency.Size = new Size(62, 15);
            lFrequency.TabIndex = 12;
            lFrequency.Text = "Frequency";
            // 
            // pLayout
            // 
            pLayout.AutoSize = true;
            pLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pLayout.ColumnCount = 3;
            pLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            pLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            pLayout.ColumnStyles.Add(new ColumnStyle());
            pLayout.Controls.Add(ddStimulusMode, 1, 0);
            pLayout.Controls.Add(lFrequency, 0, 3);
            pLayout.Controls.Add(lValue1, 0, 1);
            pLayout.Controls.Add(lValue2, 0, 2);
            pLayout.Controls.Add(lStimulus, 0, 0);
            pLayout.Controls.Add(eValue1, 1, 1);
            pLayout.Controls.Add(eValue2, 1, 2);
            pLayout.Controls.Add(eFrequency, 1, 3);
            pLayout.Controls.Add(lHz, 2, 3);
            pLayout.Location = new Point(3, 3);
            pLayout.Name = "pLayout";
            pLayout.RowCount = 5;
            pLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            pLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            pLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            pLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            pLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            pLayout.Size = new Size(211, 98);
            pLayout.TabIndex = 0;
            // 
            // lHz
            // 
            lHz.AutoSize = true;
            lHz.Location = new Point(153, 77);
            lHz.Margin = new Padding(3, 5, 3, 0);
            lHz.Name = "lHz";
            lHz.Size = new Size(29, 15);
            lHz.TabIndex = 14;
            lHz.Text = "(Hz)";
            // 
            // StimulusSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(249, 249, 249);
            Controls.Add(pLayout);
            MinimumSize = new Size(200, 100);
            Name = "StimulusSettingsControl";
            Size = new Size(217, 104);
            pLayout.ResumeLayout(false);
            pLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private TableLayoutPanel pLayout;
        private Label lHz;
    }
}
