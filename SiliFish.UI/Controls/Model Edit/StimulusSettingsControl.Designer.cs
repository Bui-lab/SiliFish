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
            this.pLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lHz = new System.Windows.Forms.Label();
            this.pLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // eValue2
            // 
            this.eValue2.Location = new System.Drawing.Point(101, 49);
            this.eValue2.Margin = new System.Windows.Forms.Padding(1);
            this.eValue2.Name = "eValue2";
            this.eValue2.Size = new System.Drawing.Size(44, 23);
            this.eValue2.TabIndex = 11;
            this.eValue2.TextChanged += new System.EventHandler(this.eValue2_TextChanged);
            // 
            // eValue1
            // 
            this.eValue1.Location = new System.Drawing.Point(101, 25);
            this.eValue1.Margin = new System.Windows.Forms.Padding(1);
            this.eValue1.Name = "eValue1";
            this.eValue1.Size = new System.Drawing.Size(44, 23);
            this.eValue1.TabIndex = 9;
            this.eValue1.TextChanged += new System.EventHandler(this.eValue1_TextChanged);
            // 
            // lValue1
            // 
            this.lValue1.AutoSize = true;
            this.lValue1.Location = new System.Drawing.Point(3, 29);
            this.lValue1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lValue1.Name = "lValue1";
            this.lValue1.Size = new System.Drawing.Size(37, 15);
            this.lValue1.TabIndex = 8;
            this.lValue1.Text = "Mean";
            // 
            // lValue2
            // 
            this.lValue2.AutoSize = true;
            this.lValue2.Location = new System.Drawing.Point(3, 53);
            this.lValue2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lValue2.Name = "lValue2";
            this.lValue2.Size = new System.Drawing.Size(47, 15);
            this.lValue2.TabIndex = 10;
            this.lValue2.Text = "Std Dev";
            // 
            // lStimulus
            // 
            this.lStimulus.AutoSize = true;
            this.lStimulus.Location = new System.Drawing.Point(3, 5);
            this.lStimulus.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lStimulus.Name = "lStimulus";
            this.lStimulus.Size = new System.Drawing.Size(87, 15);
            this.lStimulus.TabIndex = 6;
            this.lStimulus.Text = "Stimulus Mode";
            // 
            // ddStimulusMode
            // 
            this.ddStimulusMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pLayout.SetColumnSpan(this.ddStimulusMode, 2);
            this.ddStimulusMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddStimulusMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddStimulusMode.FormattingEnabled = true;
            this.ddStimulusMode.Location = new System.Drawing.Point(101, 1);
            this.ddStimulusMode.Margin = new System.Windows.Forms.Padding(1);
            this.ddStimulusMode.Name = "ddStimulusMode";
            this.ddStimulusMode.Size = new System.Drawing.Size(109, 23);
            this.ddStimulusMode.TabIndex = 7;
            this.ddStimulusMode.SelectedIndexChanged += new System.EventHandler(this.ddStimulusMode_SelectedIndexChanged);
            // 
            // eFrequency
            // 
            this.eFrequency.Location = new System.Drawing.Point(101, 73);
            this.eFrequency.Margin = new System.Windows.Forms.Padding(1);
            this.eFrequency.Name = "eFrequency";
            this.eFrequency.Size = new System.Drawing.Size(44, 23);
            this.eFrequency.TabIndex = 13;
            this.eFrequency.TextChanged += new System.EventHandler(this.eFrequency_TextChanged);
            // 
            // lFrequency
            // 
            this.lFrequency.AutoSize = true;
            this.lFrequency.Location = new System.Drawing.Point(3, 77);
            this.lFrequency.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lFrequency.Name = "lFrequency";
            this.lFrequency.Size = new System.Drawing.Size(62, 15);
            this.lFrequency.TabIndex = 12;
            this.lFrequency.Text = "Frequency";
            // 
            // pLayout
            // 
            this.pLayout.AutoSize = true;
            this.pLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pLayout.ColumnCount = 3;
            this.pLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.pLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pLayout.Controls.Add(this.ddStimulusMode, 1, 0);
            this.pLayout.Controls.Add(this.lFrequency, 0, 3);
            this.pLayout.Controls.Add(this.lValue1, 0, 1);
            this.pLayout.Controls.Add(this.lValue2, 0, 2);
            this.pLayout.Controls.Add(this.lStimulus, 0, 0);
            this.pLayout.Controls.Add(this.eValue1, 1, 1);
            this.pLayout.Controls.Add(this.eValue2, 1, 2);
            this.pLayout.Controls.Add(this.eFrequency, 1, 3);
            this.pLayout.Controls.Add(this.lHz, 2, 3);
            this.pLayout.Location = new System.Drawing.Point(3, 3);
            this.pLayout.Name = "pLayout";
            this.pLayout.RowCount = 5;
            this.pLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.pLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.pLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.pLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.pLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.pLayout.Size = new System.Drawing.Size(211, 98);
            this.pLayout.TabIndex = 0;
            // 
            // lHz
            // 
            this.lHz.AutoSize = true;
            this.lHz.Location = new System.Drawing.Point(153, 77);
            this.lHz.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lHz.Name = "lHz";
            this.lHz.Size = new System.Drawing.Size(29, 15);
            this.lHz.TabIndex = 14;
            this.lHz.Text = "(Hz)";
            // 
            // StimulusSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.pLayout);
            this.MinimumSize = new System.Drawing.Size(200, 100);
            this.Name = "StimulusSettingsControl";
            this.Size = new System.Drawing.Size(217, 104);
            this.pLayout.ResumeLayout(false);
            this.pLayout.PerformLayout();
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
        private TableLayoutPanel pLayout;
        private Label lHz;
    }
}
