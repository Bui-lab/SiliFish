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
            this.pTargetRheobase = new System.Windows.Forms.Panel();
            this.lSeperator = new System.Windows.Forms.Label();
            this.eMaxRheobase = new System.Windows.Forms.TextBox();
            this.eMinRheobase = new System.Windows.Forms.TextBox();
            this.lMinMaxRheobase = new System.Windows.Forms.Label();
            this.pFiringRelated = new System.Windows.Forms.Panel();
            this.ddFiringValues = new System.Windows.Forms.ComboBox();
            this.lFiringValues = new System.Windows.Forms.Label();
            this.eCurrentApplied = new System.Windows.Forms.TextBox();
            this.lCurrentApplied = new System.Windows.Forms.Label();
            this.lCurrentMode = new System.Windows.Forms.Label();
            this.ddCurrentSelection = new System.Windows.Forms.ComboBox();
            this.linkRemove = new System.Windows.Forms.LinkLabel();
            this.pFitnessFunction.SuspendLayout();
            this.pTargetRheobase.SuspendLayout();
            this.pFiringRelated.SuspendLayout();
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
            // pTargetRheobase
            // 
            this.pTargetRheobase.Controls.Add(this.lSeperator);
            this.pTargetRheobase.Controls.Add(this.eMaxRheobase);
            this.pTargetRheobase.Controls.Add(this.eMinRheobase);
            this.pTargetRheobase.Controls.Add(this.lMinMaxRheobase);
            this.pTargetRheobase.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTargetRheobase.Location = new System.Drawing.Point(0, 64);
            this.pTargetRheobase.MinimumSize = new System.Drawing.Size(240, 32);
            this.pTargetRheobase.Name = "pTargetRheobase";
            this.pTargetRheobase.Size = new System.Drawing.Size(245, 32);
            this.pTargetRheobase.TabIndex = 5;
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
            // eMaxRheobase
            // 
            this.eMaxRheobase.Location = new System.Drawing.Point(176, 4);
            this.eMaxRheobase.Name = "eMaxRheobase";
            this.eMaxRheobase.Size = new System.Drawing.Size(56, 23);
            this.eMaxRheobase.TabIndex = 2;
            // 
            // eMinRheobase
            // 
            this.eMinRheobase.Location = new System.Drawing.Point(102, 4);
            this.eMinRheobase.Name = "eMinRheobase";
            this.eMinRheobase.Size = new System.Drawing.Size(56, 23);
            this.eMinRheobase.TabIndex = 1;
            // 
            // lMinMaxRheobase
            // 
            this.lMinMaxRheobase.AutoSize = true;
            this.lMinMaxRheobase.Location = new System.Drawing.Point(3, 10);
            this.lMinMaxRheobase.Name = "lMinMaxRheobase";
            this.lMinMaxRheobase.Size = new System.Drawing.Size(56, 15);
            this.lMinMaxRheobase.TabIndex = 0;
            this.lMinMaxRheobase.Text = "Min-Max";
            // 
            // pFiringRelated
            // 
            this.pFiringRelated.Controls.Add(this.ddFiringValues);
            this.pFiringRelated.Controls.Add(this.lFiringValues);
            this.pFiringRelated.Controls.Add(this.eCurrentApplied);
            this.pFiringRelated.Controls.Add(this.lCurrentApplied);
            this.pFiringRelated.Controls.Add(this.lCurrentMode);
            this.pFiringRelated.Controls.Add(this.ddCurrentSelection);
            this.pFiringRelated.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFiringRelated.Location = new System.Drawing.Point(0, 96);
            this.pFiringRelated.MinimumSize = new System.Drawing.Size(240, 92);
            this.pFiringRelated.Name = "pFiringRelated";
            this.pFiringRelated.Size = new System.Drawing.Size(245, 92);
            this.pFiringRelated.TabIndex = 6;
            // 
            // ddFiringValues
            // 
            this.ddFiringValues.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddFiringValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddFiringValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddFiringValues.FormattingEnabled = true;
            this.ddFiringValues.Items.AddRange(new object[] {
            "x Rheobase",
            "Fixed Current"});
            this.ddFiringValues.Location = new System.Drawing.Point(102, 62);
            this.ddFiringValues.Name = "ddFiringValues";
            this.ddFiringValues.Size = new System.Drawing.Size(130, 23);
            this.ddFiringValues.TabIndex = 9;
            this.ddFiringValues.Tag = "0";
            // 
            // lFiringValues
            // 
            this.lFiringValues.AutoSize = true;
            this.lFiringValues.Location = new System.Drawing.Point(5, 69);
            this.lFiringValues.Name = "lFiringValues";
            this.lFiringValues.Size = new System.Drawing.Size(60, 15);
            this.lFiringValues.TabIndex = 8;
            this.lFiringValues.Tag = "0";
            this.lFiringValues.Text = "Firing ____";
            this.lFiringValues.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eCurrentApplied
            // 
            this.eCurrentApplied.Location = new System.Drawing.Point(102, 33);
            this.eCurrentApplied.Name = "eCurrentApplied";
            this.eCurrentApplied.Size = new System.Drawing.Size(56, 23);
            this.eCurrentApplied.TabIndex = 7;
            // 
            // lCurrentApplied
            // 
            this.lCurrentApplied.AutoSize = true;
            this.lCurrentApplied.Location = new System.Drawing.Point(5, 37);
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
            // linkRemove
            // 
            this.linkRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkRemove.AutoSize = true;
            this.linkRemove.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkRemove.Location = new System.Drawing.Point(189, 198);
            this.linkRemove.Name = "linkRemove";
            this.linkRemove.Size = new System.Drawing.Size(50, 15);
            this.linkRemove.TabIndex = 7;
            this.linkRemove.TabStop = true;
            this.linkRemove.Text = "Remove";
            this.linkRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRemove_LinkClicked);
            // 
            // FitnessFunctionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.linkRemove);
            this.Controls.Add(this.pFiringRelated);
            this.Controls.Add(this.pTargetRheobase);
            this.Controls.Add(this.pFitnessFunction);
            this.Name = "FitnessFunctionControl";
            this.Size = new System.Drawing.Size(245, 223);
            this.pFitnessFunction.ResumeLayout(false);
            this.pFitnessFunction.PerformLayout();
            this.pTargetRheobase.ResumeLayout(false);
            this.pTargetRheobase.PerformLayout();
            this.pFiringRelated.ResumeLayout(false);
            this.pFiringRelated.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lFitnessFunction;
        private ComboBox ddFitnessFunction;
        private Panel pFitnessFunction;
        private Panel pTargetRheobase;
        private Label lSeperator;
        private TextBox eMaxRheobase;
        private TextBox eMinRheobase;
        private Label lMinMaxRheobase;
        private Panel pFiringRelated;
        private TextBox eCurrentApplied;
        private Label lCurrentApplied;
        private Label lCurrentMode;
        private ComboBox ddCurrentSelection;
        private ComboBox ddFiringValues;
        private Label lFiringValues;
        private TextBox eFitnessWeight;
        private Label lFitnessWeight;
        private LinkLabel linkRemove;
    }
}
