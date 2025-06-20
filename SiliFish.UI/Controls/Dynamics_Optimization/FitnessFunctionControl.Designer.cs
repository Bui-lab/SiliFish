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
            components = new System.ComponentModel.Container();
            lFitnessFunction = new Label();
            ddFitnessFunction = new ComboBox();
            pFitnessFunction = new Panel();
            cbFitnessPartial = new CheckBox();
            eFitnessWeight = new TextBox();
            lFitnessWeight = new Label();
            pMinMax = new Panel();
            lSeperator = new Label();
            eMaxValue = new TextBox();
            eMinValue = new TextBox();
            lMinMaxValue = new Label();
            pCurrentRequired = new Panel();
            eCurrentApplied = new TextBox();
            lCurrentApplied = new Label();
            lCurrentMode = new Label();
            ddCurrentSelection = new ComboBox();
            ddFiringOption = new ComboBox();
            lFiringOption = new Label();
            linkRemove = new LinkLabel();
            pFiringOption = new Panel();
            toolTipFitness = new ToolTip(components);
            pFitnessFunction.SuspendLayout();
            pMinMax.SuspendLayout();
            pCurrentRequired.SuspendLayout();
            pFiringOption.SuspendLayout();
            SuspendLayout();
            // 
            // lFitnessFunction
            // 
            lFitnessFunction.AutoSize = true;
            lFitnessFunction.Location = new Point(3, 13);
            lFitnessFunction.Name = "lFitnessFunction";
            lFitnessFunction.Size = new Size(93, 15);
            lFitnessFunction.TabIndex = 3;
            lFitnessFunction.Tag = "0";
            lFitnessFunction.Text = "Fitness Function";
            lFitnessFunction.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ddFitnessFunction
            // 
            ddFitnessFunction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddFitnessFunction.BackColor = Color.WhiteSmoke;
            ddFitnessFunction.DropDownStyle = ComboBoxStyle.DropDownList;
            ddFitnessFunction.FlatStyle = FlatStyle.Flat;
            ddFitnessFunction.FormattingEnabled = true;
            ddFitnessFunction.Location = new Point(102, 9);
            ddFitnessFunction.Name = "ddFitnessFunction";
            ddFitnessFunction.Size = new Size(130, 23);
            ddFitnessFunction.TabIndex = 2;
            ddFitnessFunction.Tag = "0";
            ddFitnessFunction.SelectedIndexChanged += ddFitnessFunction_SelectedIndexChanged;
            // 
            // pFitnessFunction
            // 
            pFitnessFunction.Controls.Add(cbFitnessPartial);
            pFitnessFunction.Controls.Add(eFitnessWeight);
            pFitnessFunction.Controls.Add(lFitnessWeight);
            pFitnessFunction.Controls.Add(lFitnessFunction);
            pFitnessFunction.Controls.Add(ddFitnessFunction);
            pFitnessFunction.Dock = DockStyle.Top;
            pFitnessFunction.Location = new Point(0, 0);
            pFitnessFunction.Name = "pFitnessFunction";
            pFitnessFunction.Size = new Size(238, 64);
            pFitnessFunction.TabIndex = 4;
            // 
            // cbFitnessPartial
            // 
            cbFitnessPartial.AutoSize = true;
            cbFitnessPartial.Location = new Point(164, 38);
            cbFitnessPartial.Name = "cbFitnessPartial";
            cbFitnessPartial.Size = new Size(59, 19);
            cbFitnessPartial.TabIndex = 6;
            cbFitnessPartial.Text = "Partial";
            toolTipFitness.SetToolTip(cbFitnessPartial, "Whether partial weight is possible based on the distance to the target value.");
            cbFitnessPartial.UseVisualStyleBackColor = true;
            // 
            // eFitnessWeight
            // 
            eFitnessWeight.Location = new Point(102, 35);
            eFitnessWeight.Name = "eFitnessWeight";
            eFitnessWeight.Size = new Size(56, 23);
            eFitnessWeight.TabIndex = 5;
            eFitnessWeight.Text = "1";
            // 
            // lFitnessWeight
            // 
            lFitnessWeight.AutoSize = true;
            lFitnessWeight.Location = new Point(3, 38);
            lFitnessWeight.Name = "lFitnessWeight";
            lFitnessWeight.Size = new Size(45, 15);
            lFitnessWeight.TabIndex = 4;
            lFitnessWeight.Tag = "0";
            lFitnessWeight.Text = "Weight";
            lFitnessWeight.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pMinMax
            // 
            pMinMax.Controls.Add(lSeperator);
            pMinMax.Controls.Add(eMaxValue);
            pMinMax.Controls.Add(eMinValue);
            pMinMax.Controls.Add(lMinMaxValue);
            pMinMax.Dock = DockStyle.Top;
            pMinMax.Location = new Point(0, 158);
            pMinMax.MinimumSize = new Size(240, 32);
            pMinMax.Name = "pMinMax";
            pMinMax.Size = new Size(240, 32);
            pMinMax.TabIndex = 5;
            // 
            // lSeperator
            // 
            lSeperator.AutoSize = true;
            lSeperator.Location = new Point(159, 10);
            lSeperator.Name = "lSeperator";
            lSeperator.Size = new Size(12, 15);
            lSeperator.TabIndex = 3;
            lSeperator.Text = "-";
            // 
            // eMaxValue
            // 
            eMaxValue.Location = new Point(176, 4);
            eMaxValue.Name = "eMaxValue";
            eMaxValue.Size = new Size(56, 23);
            eMaxValue.TabIndex = 2;
            // 
            // eMinValue
            // 
            eMinValue.Location = new Point(102, 4);
            eMinValue.Name = "eMinValue";
            eMinValue.Size = new Size(56, 23);
            eMinValue.TabIndex = 1;
            // 
            // lMinMaxValue
            // 
            lMinMaxValue.AutoSize = true;
            lMinMaxValue.Location = new Point(3, 10);
            lMinMaxValue.Name = "lMinMaxValue";
            lMinMaxValue.Size = new Size(55, 15);
            lMinMaxValue.TabIndex = 0;
            lMinMaxValue.Text = "Min-Max";
            // 
            // pCurrentRequired
            // 
            pCurrentRequired.Controls.Add(eCurrentApplied);
            pCurrentRequired.Controls.Add(lCurrentApplied);
            pCurrentRequired.Controls.Add(lCurrentMode);
            pCurrentRequired.Controls.Add(ddCurrentSelection);
            pCurrentRequired.Dock = DockStyle.Top;
            pCurrentRequired.Location = new Point(0, 64);
            pCurrentRequired.Name = "pCurrentRequired";
            pCurrentRequired.Size = new Size(238, 59);
            pCurrentRequired.TabIndex = 6;
            // 
            // eCurrentApplied
            // 
            eCurrentApplied.Location = new Point(102, 31);
            eCurrentApplied.Name = "eCurrentApplied";
            eCurrentApplied.Size = new Size(56, 23);
            eCurrentApplied.TabIndex = 7;
            // 
            // lCurrentApplied
            // 
            lCurrentApplied.AutoSize = true;
            lCurrentApplied.Location = new Point(3, 35);
            lCurrentApplied.Name = "lCurrentApplied";
            lCurrentApplied.Size = new Size(91, 15);
            lCurrentApplied.TabIndex = 6;
            lCurrentApplied.Tag = "0";
            lCurrentApplied.Text = "Current Applied";
            lCurrentApplied.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lCurrentMode
            // 
            lCurrentMode.AutoSize = true;
            lCurrentMode.Location = new Point(3, 7);
            lCurrentMode.Name = "lCurrentMode";
            lCurrentMode.Size = new Size(81, 15);
            lCurrentMode.TabIndex = 5;
            lCurrentMode.Tag = "0";
            lCurrentMode.Text = "Current Mode";
            lCurrentMode.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ddCurrentSelection
            // 
            ddCurrentSelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCurrentSelection.BackColor = Color.WhiteSmoke;
            ddCurrentSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCurrentSelection.FlatStyle = FlatStyle.Flat;
            ddCurrentSelection.FormattingEnabled = true;
            ddCurrentSelection.Items.AddRange(new object[] { "x Rheobase", "Fixed Current" });
            ddCurrentSelection.Location = new Point(102, 4);
            ddCurrentSelection.Name = "ddCurrentSelection";
            ddCurrentSelection.Size = new Size(130, 23);
            ddCurrentSelection.TabIndex = 4;
            ddCurrentSelection.Tag = "0";
            // 
            // ddFiringOption
            // 
            ddFiringOption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddFiringOption.BackColor = Color.WhiteSmoke;
            ddFiringOption.DropDownStyle = ComboBoxStyle.DropDownList;
            ddFiringOption.FlatStyle = FlatStyle.Flat;
            ddFiringOption.FormattingEnabled = true;
            ddFiringOption.Location = new Point(102, 6);
            ddFiringOption.Name = "ddFiringOption";
            ddFiringOption.Size = new Size(130, 23);
            ddFiringOption.TabIndex = 9;
            ddFiringOption.Tag = "0";
            // 
            // lFiringOption
            // 
            lFiringOption.AutoSize = true;
            lFiringOption.Location = new Point(3, 13);
            lFiringOption.Name = "lFiringOption";
            lFiringOption.Size = new Size(77, 15);
            lFiringOption.TabIndex = 8;
            lFiringOption.Tag = "0";
            lFiringOption.Text = "Firing Option";
            lFiringOption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linkRemove
            // 
            linkRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            linkRemove.AutoSize = true;
            linkRemove.LinkColor = Color.FromArgb(64, 64, 64);
            linkRemove.Location = new Point(3, 196);
            linkRemove.Name = "linkRemove";
            linkRemove.Size = new Size(50, 15);
            linkRemove.TabIndex = 7;
            linkRemove.TabStop = true;
            linkRemove.Text = "Remove";
            linkRemove.LinkClicked += linkRemove_LinkClicked;
            // 
            // pFiringOption
            // 
            pFiringOption.Controls.Add(ddFiringOption);
            pFiringOption.Controls.Add(lFiringOption);
            pFiringOption.Dock = DockStyle.Top;
            pFiringOption.Location = new Point(0, 123);
            pFiringOption.Name = "pFiringOption";
            pFiringOption.Size = new Size(238, 35);
            pFiringOption.TabIndex = 8;
            // 
            // FitnessFunctionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pMinMax);
            Controls.Add(pFiringOption);
            Controls.Add(pCurrentRequired);
            Controls.Add(pFitnessFunction);
            Controls.Add(linkRemove);
            MinimumSize = new Size(238, 121);
            Name = "FitnessFunctionControl";
            Size = new Size(238, 220);
            pFitnessFunction.ResumeLayout(false);
            pFitnessFunction.PerformLayout();
            pMinMax.ResumeLayout(false);
            pMinMax.PerformLayout();
            pCurrentRequired.ResumeLayout(false);
            pCurrentRequired.PerformLayout();
            pFiringOption.ResumeLayout(false);
            pFiringOption.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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
        private CheckBox cbFitnessPartial;
        private ToolTip toolTipFitness;
    }
}
