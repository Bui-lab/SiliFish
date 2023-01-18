namespace SiliFish.UI.Controls
{
    partial class FitnessFunctionControl
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
            this.lFitnessFunction = new System.Windows.Forms.Label();
            this.ddFitnessFunction = new System.Windows.Forms.ComboBox();
            this.pFitnessFunction = new System.Windows.Forms.Panel();
            this.eFitnessWeight = new System.Windows.Forms.TextBox();
            this.lFitnessWeight = new System.Windows.Forms.Label();
            this.pMinMax = new System.Windows.Forms.Panel();
            this.lSeperator = new System.Windows.Forms.Label();
            this.eMaxValue = new System.Windows.Forms.TextBox();
            this.eMinValue = new System.Windows.Forms.TextBox();
            this.lMinMaxValue = new System.Windows.Forms.Label();
            this.pCurrentRequired = new System.Windows.Forms.Panel();
            this.eCurrentApplied = new System.Windows.Forms.TextBox();
            this.lCurrentApplied = new System.Windows.Forms.Label();
            this.lCurrentMode = new System.Windows.Forms.Label();
            this.ddCurrentSelection = new System.Windows.Forms.ComboBox();
            this.ddFiringOption = new System.Windows.Forms.ComboBox();
            this.lFiringOption = new System.Windows.Forms.Label();
            this.linkRemove = new System.Windows.Forms.LinkLabel();
            this.pFiringOption = new System.Windows.Forms.Panel();
            this.pFitnessFunction.SuspendLayout();
            this.pMinMax.SuspendLayout();
            this.pCurrentRequired.SuspendLayout();
            this.pFiringOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // lFitnessFunction
            // 
            this.lFitnessFunction.AutoSize = true;
            this.lFitnessFunction.Location = new System.Drawing.Point(3, 13);
            this.lFitnessFunction.Name = "lFitnessFunction";
            this.lFitnessFunction.Size = new System.Drawing.Size(93, 15);
            this.lFitnessFunction.TabIndex = 3;
            this.lFitnessFunction.Tag = "0";
            this.lFitnessFunction.Text = "Fitness Function";
            this.lFitnessFunction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ddFitnessFunction
            // 
            this.ddFitnessFunction.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddFitnessFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddFitnessFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddFitnessFunction.FormattingEnabled = true;
            this.ddFitnessFunction.Location = new System.Drawing.Point(102, 9);
            this.ddFitnessFunction.Name = "ddFitnessFunction";
            this.ddFitnessFunction.Size = new System.Drawing.Size(130, 23);
            this.ddFitnessFunction.TabIndex = 2;
            this.ddFitnessFunction.Tag = "0";
            this.ddFitnessFunction.SelectedIndexChanged += new System.EventHandler(this.ddFitnessFunction_SelectedIndexChanged);
            // 
            // pFitnessFunction
            // 
            this.pFitnessFunction.Controls.Add(this.eFitnessWeight);
            this.pFitnessFunction.Controls.Add(this.lFitnessWeight);
            this.pFitnessFunction.Controls.Add(this.lFitnessFunction);
            this.pFitnessFunction.Controls.Add(this.ddFitnessFunction);
            this.pFitnessFunction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFitnessFunction.Location = new System.Drawing.Point(0, 0);
            this.pFitnessFunction.Name = "pFitnessFunction";
            this.pFitnessFunction.Size = new System.Drawing.Size(245, 64);
            this.pFitnessFunction.TabIndex = 4;
            // 
            // eFitnessWeight
            // 
            this.eFitnessWeight.Location = new System.Drawing.Point(102, 35);
            this.eFitnessWeight.Name = "eFitnessWeight";
            this.eFitnessWeight.Size = new System.Drawing.Size(56, 23);
            this.eFitnessWeight.TabIndex = 5;
            this.eFitnessWeight.Text = "1";
            // 
            // lFitnessWeight
            // 
            this.lFitnessWeight.AutoSize = true;
            this.lFitnessWeight.Location = new System.Drawing.Point(3, 38);
            this.lFitnessWeight.Name = "lFitnessWeight";
            this.lFitnessWeight.Size = new System.Drawing.Size(45, 15);
            this.lFitnessWeight.TabIndex = 4;
            this.lFitnessWeight.Tag = "0";
            this.lFitnessWeight.Text = "Weight";
            this.lFitnessWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pMinMax
            // 
            this.pMinMax.Controls.Add(this.lSeperator);
            this.pMinMax.Controls.Add(this.eMaxValue);
            this.pMinMax.Controls.Add(this.eMinValue);
            this.pMinMax.Controls.Add(this.lMinMaxValue);
            this.pMinMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMinMax.Location = new System.Drawing.Point(0, 158);
            this.pMinMax.MinimumSize = new System.Drawing.Size(240, 32);
            this.pMinMax.Name = "pMinMax";
            this.pMinMax.Size = new System.Drawing.Size(245, 32);
            this.pMinMax.TabIndex = 5;
            // 
            // lSeperator
            // 
            this.lSeperator.AutoSize = true;
            this.lSeperator.Location = new System.Drawing.Point(159, 10);
            this.lSeperator.Name = "lSeperator";
            this.lSeperator.Size = new System.Drawing.Size(12, 15);
            this.lSeperator.TabIndex = 3;
            this.lSeperator.Text = "-";
            // 
            // eMaxValue
            // 
            this.eMaxValue.Location = new System.Drawing.Point(176, 4);
            this.eMaxValue.Name = "eMaxValue";
            this.eMaxValue.Size = new System.Drawing.Size(56, 23);
            this.eMaxValue.TabIndex = 2;
            // 
            // eMinValue
            // 
            this.eMinValue.Location = new System.Drawing.Point(102, 4);
            this.eMinValue.Name = "eMinValue";
            this.eMinValue.Size = new System.Drawing.Size(56, 23);
            this.eMinValue.TabIndex = 1;
            // 
            // lMinMaxValue
            // 
            this.lMinMaxValue.AutoSize = true;
            this.lMinMaxValue.Location = new System.Drawing.Point(3, 10);
            this.lMinMaxValue.Name = "lMinMaxValue";
            this.lMinMaxValue.Size = new System.Drawing.Size(56, 15);
            this.lMinMaxValue.TabIndex = 0;
            this.lMinMaxValue.Text = "Min-Max";
            // 
            // pCurrentRequired
            // 
            this.pCurrentRequired.Controls.Add(this.eCurrentApplied);
            this.pCurrentRequired.Controls.Add(this.lCurrentApplied);
            this.pCurrentRequired.Controls.Add(this.lCurrentMode);
            this.pCurrentRequired.Controls.Add(this.ddCurrentSelection);
            this.pCurrentRequired.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCurrentRequired.Location = new System.Drawing.Point(0, 64);
            this.pCurrentRequired.Name = "pCurrentRequired";
            this.pCurrentRequired.Size = new System.Drawing.Size(245, 59);
            this.pCurrentRequired.TabIndex = 6;
            // 
            // eCurrentApplied
            // 
            this.eCurrentApplied.Location = new System.Drawing.Point(102, 31);
            this.eCurrentApplied.Name = "eCurrentApplied";
            this.eCurrentApplied.Size = new System.Drawing.Size(56, 23);
            this.eCurrentApplied.TabIndex = 7;
            // 
            // lCurrentApplied
            // 
            this.lCurrentApplied.AutoSize = true;
            this.lCurrentApplied.Location = new System.Drawing.Point(3, 35);
            this.lCurrentApplied.Name = "lCurrentApplied";
            this.lCurrentApplied.Size = new System.Drawing.Size(91, 15);
            this.lCurrentApplied.TabIndex = 6;
            this.lCurrentApplied.Tag = "0";
            this.lCurrentApplied.Text = "Current Applied";
            this.lCurrentApplied.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lCurrentMode
            // 
            this.lCurrentMode.AutoSize = true;
            this.lCurrentMode.Location = new System.Drawing.Point(3, 7);
            this.lCurrentMode.Name = "lCurrentMode";
            this.lCurrentMode.Size = new System.Drawing.Size(81, 15);
            this.lCurrentMode.TabIndex = 5;
            this.lCurrentMode.Tag = "0";
            this.lCurrentMode.Text = "Current Mode";
            this.lCurrentMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ddCurrentSelection
            // 
            this.ddCurrentSelection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddCurrentSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCurrentSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddCurrentSelection.FormattingEnabled = true;
            this.ddCurrentSelection.Items.AddRange(new object[] {
            "x Rheobase",
            "Fixed Current"});
            this.ddCurrentSelection.Location = new System.Drawing.Point(102, 4);
            this.ddCurrentSelection.Name = "ddCurrentSelection";
            this.ddCurrentSelection.Size = new System.Drawing.Size(130, 23);
            this.ddCurrentSelection.TabIndex = 4;
            this.ddCurrentSelection.Tag = "0";
            // 
            // ddFiringOption
            // 
            this.ddFiringOption.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddFiringOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddFiringOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddFiringOption.FormattingEnabled = true;
            this.ddFiringOption.Location = new System.Drawing.Point(102, 6);
            this.ddFiringOption.Name = "ddFiringOption";
            this.ddFiringOption.Size = new System.Drawing.Size(130, 23);
            this.ddFiringOption.TabIndex = 9;
            this.ddFiringOption.Tag = "0";
            // 
            // lFiringOption
            // 
            this.lFiringOption.AutoSize = true;
            this.lFiringOption.Location = new System.Drawing.Point(3, 13);
            this.lFiringOption.Name = "lFiringOption";
            this.lFiringOption.Size = new System.Drawing.Size(77, 15);
            this.lFiringOption.TabIndex = 8;
            this.lFiringOption.Tag = "0";
            this.lFiringOption.Text = "Firing Option";
            this.lFiringOption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkRemove
            // 
            this.linkRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkRemove.AutoSize = true;
            this.linkRemove.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkRemove.Location = new System.Drawing.Point(189, 197);
            this.linkRemove.Name = "linkRemove";
            this.linkRemove.Size = new System.Drawing.Size(50, 15);
            this.linkRemove.TabIndex = 7;
            this.linkRemove.TabStop = true;
            this.linkRemove.Text = "Remove";
            this.linkRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRemove_LinkClicked);
            // 
            // pFiringOption
            // 
            this.pFiringOption.Controls.Add(this.ddFiringOption);
            this.pFiringOption.Controls.Add(this.lFiringOption);
            this.pFiringOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFiringOption.Location = new System.Drawing.Point(0, 123);
            this.pFiringOption.Name = "pFiringOption";
            this.pFiringOption.Size = new System.Drawing.Size(245, 35);
            this.pFiringOption.TabIndex = 8;
            // 
            // FitnessFunctionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pMinMax);
            this.Controls.Add(this.pFiringOption);
            this.Controls.Add(this.linkRemove);
            this.Controls.Add(this.pCurrentRequired);
            this.Controls.Add(this.pFitnessFunction);
            this.Name = "FitnessFunctionControl";
            this.Size = new System.Drawing.Size(245, 222);
            this.pFitnessFunction.ResumeLayout(false);
            this.pFitnessFunction.PerformLayout();
            this.pMinMax.ResumeLayout(false);
            this.pMinMax.PerformLayout();
            this.pCurrentRequired.ResumeLayout(false);
            this.pCurrentRequired.PerformLayout();
            this.pFiringOption.ResumeLayout(false);
            this.pFiringOption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lFitnessFunction;
        private ComboBox ddFitnessFunction;
        private Panel pFitnessFunction;
        private Panel pMinMax;
        private Label lSeperator;
        private TextBox eMaxValue;
        private TextBox eMinValue;
        private Label lMinMaxValue;
        private Panel pCurrentRequired;
        private TextBox eCurrentApplied;
        private Label lCurrentApplied;
        private Label lCurrentMode;
        private ComboBox ddCurrentSelection;
        private ComboBox ddFiringOption;
        private Label lFiringOption;
        private TextBox eFitnessWeight;
        private Label lFitnessWeight;
        private LinkLabel linkRemove;
        private Panel pFiringOption;
    }
}
