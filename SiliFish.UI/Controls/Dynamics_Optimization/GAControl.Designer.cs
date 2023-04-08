namespace SiliFish.UI.Controls
{
    partial class GAControl
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
            splitGA = new SplitContainer();
            splitGAParams = new SplitContainer();
            dgMinMaxValues = new DataGridView();
            colParameter = new DataGridViewTextBoxColumn();
            colMinValue = new DataGridViewTextBoxColumn();
            colMaxValue = new DataGridViewTextBoxColumn();
            pMinMax = new Panel();
            linkSuggestMinMax = new LinkLabel();
            dgFitnessFunctions = new DataGridView();
            colFFMode = new DataGridViewTextBoxColumn();
            colFFWeight = new DataGridViewTextBoxColumn();
            colFFFunction = new DataGridViewTextBoxColumn();
            colFFEdit = new DataGridViewLinkColumn();
            colFFDelete = new DataGridViewLinkColumn();
            pFitnessFunctions = new Panel();
            linkAddFitnessFunction = new LinkLabel();
            btnCalculateFitness = new Button();
            pLineFitnessFunctions = new Panel();
            lFitnessFunctions = new Label();
            grTermination = new GroupBox();
            cbTargetFitness = new CheckBox();
            cbCustomTermination = new CheckBox();
            eTargetFitness = new TextBox();
            eMaxGeneration = new TextBox();
            cbMaxGeneration = new CheckBox();
            ddGATermination = new ComboBox();
            lGATerminationParameter = new Label();
            eTerminationParameter = new TextBox();
            lOptimizationOutput = new Label();
            btnOptimize = new Button();
            linkLoadGAParams = new LinkLabel();
            linkSaveGAParams = new LinkLabel();
            LGAReinsertion = new Label();
            ddGAReinsertion = new ComboBox();
            lGASelection = new Label();
            ddGACrossOver = new ComboBox();
            eMaxChromosome = new TextBox();
            lGAMutation = new Label();
            ddGAMutation = new ComboBox();
            lGAMinMaxChromosome = new Label();
            ddGASelection = new ComboBox();
            eMinChromosome = new TextBox();
            lGACrossOver = new Label();
            lMinMaxSeperator = new Label();
            timerOptimization = new System.Windows.Forms.Timer(components);
            openFileJson = new OpenFileDialog();
            saveFileJson = new SaveFileDialog();
            toolTip = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)splitGA).BeginInit();
            splitGA.Panel1.SuspendLayout();
            splitGA.Panel2.SuspendLayout();
            splitGA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitGAParams).BeginInit();
            splitGAParams.Panel1.SuspendLayout();
            splitGAParams.Panel2.SuspendLayout();
            splitGAParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgMinMaxValues).BeginInit();
            pMinMax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgFitnessFunctions).BeginInit();
            pFitnessFunctions.SuspendLayout();
            grTermination.SuspendLayout();
            SuspendLayout();
            // 
            // splitGA
            // 
            splitGA.BorderStyle = BorderStyle.FixedSingle;
            splitGA.Dock = DockStyle.Fill;
            splitGA.Location = new Point(0, 0);
            splitGA.Name = "splitGA";
            // 
            // splitGA.Panel1
            // 
            splitGA.Panel1.Controls.Add(splitGAParams);
            // 
            // splitGA.Panel2
            // 
            splitGA.Panel2.BackColor = Color.White;
            splitGA.Panel2.Controls.Add(grTermination);
            splitGA.Panel2.Controls.Add(lOptimizationOutput);
            splitGA.Panel2.Controls.Add(btnOptimize);
            splitGA.Panel2.Controls.Add(linkLoadGAParams);
            splitGA.Panel2.Controls.Add(linkSaveGAParams);
            splitGA.Panel2.Controls.Add(LGAReinsertion);
            splitGA.Panel2.Controls.Add(ddGAReinsertion);
            splitGA.Panel2.Controls.Add(lGASelection);
            splitGA.Panel2.Controls.Add(ddGACrossOver);
            splitGA.Panel2.Controls.Add(eMaxChromosome);
            splitGA.Panel2.Controls.Add(lGAMutation);
            splitGA.Panel2.Controls.Add(ddGAMutation);
            splitGA.Panel2.Controls.Add(lGAMinMaxChromosome);
            splitGA.Panel2.Controls.Add(ddGASelection);
            splitGA.Panel2.Controls.Add(eMinChromosome);
            splitGA.Panel2.Controls.Add(lGACrossOver);
            splitGA.Panel2.Controls.Add(lMinMaxSeperator);
            splitGA.Size = new Size(1003, 476);
            splitGA.SplitterDistance = 697;
            splitGA.TabIndex = 39;
            // 
            // splitGAParams
            // 
            splitGAParams.BorderStyle = BorderStyle.FixedSingle;
            splitGAParams.Dock = DockStyle.Fill;
            splitGAParams.Location = new Point(0, 0);
            splitGAParams.Name = "splitGAParams";
            // 
            // splitGAParams.Panel1
            // 
            splitGAParams.Panel1.Controls.Add(dgMinMaxValues);
            splitGAParams.Panel1.Controls.Add(pMinMax);
            // 
            // splitGAParams.Panel2
            // 
            splitGAParams.Panel2.Controls.Add(dgFitnessFunctions);
            splitGAParams.Panel2.Controls.Add(pFitnessFunctions);
            splitGAParams.Size = new Size(697, 476);
            splitGAParams.SplitterDistance = 214;
            splitGAParams.TabIndex = 29;
            // 
            // dgMinMaxValues
            // 
            dgMinMaxValues.AllowUserToAddRows = false;
            dgMinMaxValues.AllowUserToDeleteRows = false;
            dgMinMaxValues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgMinMaxValues.Columns.AddRange(new DataGridViewColumn[] { colParameter, colMinValue, colMaxValue });
            dgMinMaxValues.Dock = DockStyle.Fill;
            dgMinMaxValues.Location = new Point(0, 29);
            dgMinMaxValues.Name = "dgMinMaxValues";
            dgMinMaxValues.RowHeadersVisible = false;
            dgMinMaxValues.RowTemplate.Height = 25;
            dgMinMaxValues.Size = new Size(212, 445);
            dgMinMaxValues.TabIndex = 27;
            // 
            // colParameter
            // 
            colParameter.Frozen = true;
            colParameter.HeaderText = "Parameter";
            colParameter.MinimumWidth = 100;
            colParameter.Name = "colParameter";
            colParameter.ReadOnly = true;
            colParameter.Width = 200;
            // 
            // colMinValue
            // 
            colMinValue.HeaderText = "Min Value";
            colMinValue.Name = "colMinValue";
            // 
            // colMaxValue
            // 
            colMaxValue.HeaderText = "Max Value";
            colMaxValue.Name = "colMaxValue";
            // 
            // pMinMax
            // 
            pMinMax.BackColor = Color.FromArgb(236, 239, 241);
            pMinMax.Controls.Add(linkSuggestMinMax);
            pMinMax.Dock = DockStyle.Top;
            pMinMax.Location = new Point(0, 0);
            pMinMax.Name = "pMinMax";
            pMinMax.Size = new Size(212, 29);
            pMinMax.TabIndex = 28;
            // 
            // linkSuggestMinMax
            // 
            linkSuggestMinMax.AutoSize = true;
            linkSuggestMinMax.LinkColor = Color.FromArgb(64, 64, 64);
            linkSuggestMinMax.Location = new Point(6, 9);
            linkSuggestMinMax.Name = "linkSuggestMinMax";
            linkSuggestMinMax.Size = new Size(137, 15);
            linkSuggestMinMax.TabIndex = 0;
            linkSuggestMinMax.TabStop = true;
            linkSuggestMinMax.Text = "Suggest Min/Max Values";
            linkSuggestMinMax.LinkClicked += linkSuggestMinMax_LinkClicked;
            // 
            // dgFitnessFunctions
            // 
            dgFitnessFunctions.AllowUserToAddRows = false;
            dgFitnessFunctions.AllowUserToDeleteRows = false;
            dgFitnessFunctions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgFitnessFunctions.Columns.AddRange(new DataGridViewColumn[] { colFFMode, colFFWeight, colFFFunction, colFFEdit, colFFDelete });
            dgFitnessFunctions.Dock = DockStyle.Fill;
            dgFitnessFunctions.Location = new Point(0, 29);
            dgFitnessFunctions.Name = "dgFitnessFunctions";
            dgFitnessFunctions.RowHeadersVisible = false;
            dgFitnessFunctions.RowTemplate.Height = 25;
            dgFitnessFunctions.Size = new Size(477, 445);
            dgFitnessFunctions.TabIndex = 30;
            dgFitnessFunctions.CellContentClick += dgFitnessFunctions_CellContentClick;
            dgFitnessFunctions.CellDoubleClick += dgFitnessFunctions_CellDoubleClick;
            dgFitnessFunctions.CellEndEdit += dgFitnessFunctions_CellEndEdit;
            // 
            // colFFMode
            // 
            colFFMode.HeaderText = "Mode";
            colFFMode.Name = "colFFMode";
            colFFMode.ReadOnly = true;
            // 
            // colFFWeight
            // 
            colFFWeight.HeaderText = "Weight";
            colFFWeight.Name = "colFFWeight";
            // 
            // colFFFunction
            // 
            colFFFunction.HeaderText = "Function";
            colFFFunction.Name = "colFFFunction";
            colFFFunction.ReadOnly = true;
            // 
            // colFFEdit
            // 
            colFFEdit.HeaderText = "";
            colFFEdit.MinimumWidth = 50;
            colFFEdit.Name = "colFFEdit";
            colFFEdit.Text = "Edit";
            colFFEdit.UseColumnTextForLinkValue = true;
            // 
            // colFFDelete
            // 
            colFFDelete.HeaderText = "";
            colFFDelete.MinimumWidth = 50;
            colFFDelete.Name = "colFFDelete";
            colFFDelete.Text = "Delete";
            colFFDelete.UseColumnTextForLinkValue = true;
            // 
            // pFitnessFunctions
            // 
            pFitnessFunctions.BackColor = Color.FromArgb(236, 239, 241);
            pFitnessFunctions.Controls.Add(linkAddFitnessFunction);
            pFitnessFunctions.Controls.Add(btnCalculateFitness);
            pFitnessFunctions.Controls.Add(pLineFitnessFunctions);
            pFitnessFunctions.Controls.Add(lFitnessFunctions);
            pFitnessFunctions.Dock = DockStyle.Top;
            pFitnessFunctions.Location = new Point(0, 0);
            pFitnessFunctions.Name = "pFitnessFunctions";
            pFitnessFunctions.Size = new Size(477, 29);
            pFitnessFunctions.TabIndex = 29;
            // 
            // linkAddFitnessFunction
            // 
            linkAddFitnessFunction.AutoSize = true;
            linkAddFitnessFunction.LinkColor = Color.FromArgb(64, 64, 64);
            linkAddFitnessFunction.Location = new Point(129, 6);
            linkAddFitnessFunction.Name = "linkAddFitnessFunction";
            linkAddFitnessFunction.Size = new Size(118, 15);
            linkAddFitnessFunction.TabIndex = 1;
            linkAddFitnessFunction.TabStop = true;
            linkAddFitnessFunction.Text = "Add Fitness Function";
            linkAddFitnessFunction.LinkClicked += linkAddFitnessFunction_LinkClicked;
            // 
            // btnCalculateFitness
            // 
            btnCalculateFitness.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalculateFitness.BackColor = Color.FromArgb(96, 125, 139);
            btnCalculateFitness.FlatAppearance.BorderColor = Color.LightGray;
            btnCalculateFitness.FlatStyle = FlatStyle.Flat;
            btnCalculateFitness.ForeColor = Color.White;
            btnCalculateFitness.Location = new Point(351, 3);
            btnCalculateFitness.Name = "btnCalculateFitness";
            btnCalculateFitness.Size = new Size(123, 23);
            btnCalculateFitness.TabIndex = 46;
            btnCalculateFitness.Text = "Calculate Fitness";
            btnCalculateFitness.UseVisualStyleBackColor = false;
            btnCalculateFitness.Click += btnCalculateFitness_Click;
            // 
            // pLineFitnessFunctions
            // 
            pLineFitnessFunctions.BackColor = Color.LightGray;
            pLineFitnessFunctions.Dock = DockStyle.Bottom;
            pLineFitnessFunctions.Location = new Point(0, 28);
            pLineFitnessFunctions.Name = "pLineFitnessFunctions";
            pLineFitnessFunctions.Size = new Size(477, 1);
            pLineFitnessFunctions.TabIndex = 3;
            // 
            // lFitnessFunctions
            // 
            lFitnessFunctions.AutoSize = true;
            lFitnessFunctions.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lFitnessFunctions.Location = new Point(13, 6);
            lFitnessFunctions.Name = "lFitnessFunctions";
            lFitnessFunctions.Size = new Size(101, 15);
            lFitnessFunctions.TabIndex = 2;
            lFitnessFunctions.Text = "Fitness Functions";
            // 
            // grTermination
            // 
            grTermination.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grTermination.Controls.Add(cbTargetFitness);
            grTermination.Controls.Add(cbCustomTermination);
            grTermination.Controls.Add(eTargetFitness);
            grTermination.Controls.Add(eMaxGeneration);
            grTermination.Controls.Add(cbMaxGeneration);
            grTermination.Controls.Add(ddGATermination);
            grTermination.Controls.Add(lGATerminationParameter);
            grTermination.Controls.Add(eTerminationParameter);
            grTermination.Location = new Point(6, 128);
            grTermination.Name = "grTermination";
            grTermination.Size = new Size(279, 121);
            grTermination.TabIndex = 51;
            grTermination.TabStop = false;
            grTermination.Text = "Termination";
            // 
            // cbTargetFitness
            // 
            cbTargetFitness.AutoSize = true;
            cbTargetFitness.Location = new Point(6, 16);
            cbTargetFitness.Name = "cbTargetFitness";
            cbTargetFitness.Size = new Size(97, 19);
            cbTargetFitness.TabIndex = 46;
            cbTargetFitness.Text = "Target Fitness";
            cbTargetFitness.UseVisualStyleBackColor = true;
            cbTargetFitness.CheckedChanged += cbTargetFitness_CheckedChanged;
            // 
            // cbCustomTermination
            // 
            cbCustomTermination.AutoSize = true;
            cbCustomTermination.Location = new Point(6, 66);
            cbCustomTermination.Name = "cbCustomTermination";
            cbCustomTermination.Size = new Size(68, 19);
            cbCustomTermination.TabIndex = 50;
            cbCustomTermination.Text = "Custom";
            cbCustomTermination.UseVisualStyleBackColor = true;
            cbCustomTermination.CheckedChanged += cbCustomTermination_CheckedChanged;
            // 
            // eTargetFitness
            // 
            eTargetFitness.Location = new Point(113, 13);
            eTargetFitness.Name = "eTargetFitness";
            eTargetFitness.Size = new Size(35, 23);
            eTargetFitness.TabIndex = 47;
            eTargetFitness.Text = "1";
            eTargetFitness.Visible = false;
            // 
            // eMaxGeneration
            // 
            eMaxGeneration.Location = new Point(113, 38);
            eMaxGeneration.Name = "eMaxGeneration";
            eMaxGeneration.Size = new Size(35, 23);
            eMaxGeneration.TabIndex = 49;
            eMaxGeneration.Text = "50";
            eMaxGeneration.Visible = false;
            // 
            // cbMaxGeneration
            // 
            cbMaxGeneration.AutoSize = true;
            cbMaxGeneration.Location = new Point(6, 41);
            cbMaxGeneration.Name = "cbMaxGeneration";
            cbMaxGeneration.Size = new Size(110, 19);
            cbMaxGeneration.TabIndex = 48;
            cbMaxGeneration.Text = "Max Generation";
            cbMaxGeneration.UseVisualStyleBackColor = true;
            cbMaxGeneration.CheckedChanged += cbMaxGeneration_CheckedChanged;
            // 
            // ddGATermination
            // 
            ddGATermination.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddGATermination.BackColor = Color.WhiteSmoke;
            ddGATermination.DropDownStyle = ComboBoxStyle.DropDownList;
            ddGATermination.FlatStyle = FlatStyle.Flat;
            ddGATermination.FormattingEnabled = true;
            ddGATermination.Location = new Point(75, 64);
            ddGATermination.Name = "ddGATermination";
            ddGATermination.Size = new Size(198, 23);
            ddGATermination.TabIndex = 33;
            ddGATermination.Visible = false;
            ddGATermination.SelectedIndexChanged += ddGATermination_SelectedIndexChanged;
            // 
            // lGATerminationParameter
            // 
            lGATerminationParameter.AutoSize = true;
            lGATerminationParameter.Location = new Point(6, 93);
            lGATerminationParameter.Name = "lGATerminationParameter";
            lGATerminationParameter.Size = new Size(76, 15);
            lGATerminationParameter.TabIndex = 40;
            lGATerminationParameter.Text = "Term. Param.";
            lGATerminationParameter.Visible = false;
            // 
            // eTerminationParameter
            // 
            eTerminationParameter.Location = new Point(113, 90);
            eTerminationParameter.Name = "eTerminationParameter";
            eTerminationParameter.Size = new Size(35, 23);
            eTerminationParameter.TabIndex = 41;
            eTerminationParameter.Text = "50";
            eTerminationParameter.Visible = false;
            // 
            // lOptimizationOutput
            // 
            lOptimizationOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lOptimizationOutput.Location = new Point(6, 316);
            lOptimizationOutput.Name = "lOptimizationOutput";
            lOptimizationOutput.Size = new Size(270, 148);
            lOptimizationOutput.TabIndex = 45;
            lOptimizationOutput.Text = "Latest fitness:";
            // 
            // btnOptimize
            // 
            btnOptimize.BackColor = Color.FromArgb(96, 125, 139);
            btnOptimize.FlatAppearance.BorderColor = Color.LightGray;
            btnOptimize.FlatStyle = FlatStyle.Flat;
            btnOptimize.ForeColor = Color.White;
            btnOptimize.Location = new Point(6, 290);
            btnOptimize.Name = "btnOptimize";
            btnOptimize.Size = new Size(67, 23);
            btnOptimize.TabIndex = 44;
            btnOptimize.Text = "Optimize";
            btnOptimize.UseVisualStyleBackColor = false;
            btnOptimize.Click += btnOptimize_Click;
            // 
            // linkLoadGAParams
            // 
            linkLoadGAParams.AutoSize = true;
            linkLoadGAParams.LinkColor = Color.FromArgb(64, 64, 64);
            linkLoadGAParams.Location = new Point(84, 294);
            linkLoadGAParams.Name = "linkLoadGAParams";
            linkLoadGAParams.Size = new Size(94, 15);
            linkLoadGAParams.TabIndex = 42;
            linkLoadGAParams.TabStop = true;
            linkLoadGAParams.Text = "Load GA Params";
            linkLoadGAParams.LinkClicked += linkLoadGAParams_LinkClicked;
            // 
            // linkSaveGAParams
            // 
            linkSaveGAParams.AutoSize = true;
            linkSaveGAParams.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveGAParams.Location = new Point(184, 294);
            linkSaveGAParams.Name = "linkSaveGAParams";
            linkSaveGAParams.Size = new Size(92, 15);
            linkSaveGAParams.TabIndex = 43;
            linkSaveGAParams.TabStop = true;
            linkSaveGAParams.Text = "Save GA Params";
            linkSaveGAParams.LinkClicked += linkSaveGAParams_LinkClicked;
            // 
            // LGAReinsertion
            // 
            LGAReinsertion.AutoSize = true;
            LGAReinsertion.Location = new Point(6, 102);
            LGAReinsertion.Name = "LGAReinsertion";
            LGAReinsertion.Size = new Size(66, 15);
            LGAReinsertion.TabIndex = 38;
            LGAReinsertion.Text = "Reinsertion";
            // 
            // ddGAReinsertion
            // 
            ddGAReinsertion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddGAReinsertion.BackColor = Color.WhiteSmoke;
            ddGAReinsertion.DropDownStyle = ComboBoxStyle.DropDownList;
            ddGAReinsertion.FlatStyle = FlatStyle.Flat;
            ddGAReinsertion.FormattingEnabled = true;
            ddGAReinsertion.Location = new Point(79, 99);
            ddGAReinsertion.Name = "ddGAReinsertion";
            ddGAReinsertion.Size = new Size(197, 23);
            ddGAReinsertion.TabIndex = 39;
            // 
            // lGASelection
            // 
            lGASelection.AutoSize = true;
            lGASelection.Location = new Point(6, 15);
            lGASelection.Name = "lGASelection";
            lGASelection.Size = new Size(55, 15);
            lGASelection.TabIndex = 26;
            lGASelection.Text = "Selection";
            // 
            // ddGACrossOver
            // 
            ddGACrossOver.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddGACrossOver.BackColor = Color.WhiteSmoke;
            ddGACrossOver.DropDownStyle = ComboBoxStyle.DropDownList;
            ddGACrossOver.FlatStyle = FlatStyle.Flat;
            ddGACrossOver.FormattingEnabled = true;
            ddGACrossOver.Location = new Point(79, 41);
            ddGACrossOver.Name = "ddGACrossOver";
            ddGACrossOver.Size = new Size(197, 23);
            ddGACrossOver.TabIndex = 31;
            // 
            // eMaxChromosome
            // 
            eMaxChromosome.Location = new Point(205, 261);
            eMaxChromosome.Name = "eMaxChromosome";
            eMaxChromosome.Size = new Size(35, 23);
            eMaxChromosome.TabIndex = 36;
            eMaxChromosome.Text = "100";
            // 
            // lGAMutation
            // 
            lGAMutation.AutoSize = true;
            lGAMutation.Location = new Point(6, 73);
            lGAMutation.Name = "lGAMutation";
            lGAMutation.Size = new Size(56, 15);
            lGAMutation.TabIndex = 28;
            lGAMutation.Text = "Mutation";
            // 
            // ddGAMutation
            // 
            ddGAMutation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddGAMutation.BackColor = Color.WhiteSmoke;
            ddGAMutation.DropDownStyle = ComboBoxStyle.DropDownList;
            ddGAMutation.FlatStyle = FlatStyle.Flat;
            ddGAMutation.FormattingEnabled = true;
            ddGAMutation.Location = new Point(79, 70);
            ddGAMutation.Name = "ddGAMutation";
            ddGAMutation.Size = new Size(197, 23);
            ddGAMutation.TabIndex = 32;
            // 
            // lGAMinMaxChromosome
            // 
            lGAMinMaxChromosome.AutoSize = true;
            lGAMinMaxChromosome.Location = new Point(6, 264);
            lGAMinMaxChromosome.Name = "lGAMinMaxChromosome";
            lGAMinMaxChromosome.Size = new Size(142, 15);
            lGAMinMaxChromosome.TabIndex = 34;
            lGAMinMaxChromosome.Text = "Min/Max Chromosome #";
            // 
            // ddGASelection
            // 
            ddGASelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddGASelection.BackColor = Color.WhiteSmoke;
            ddGASelection.DropDownStyle = ComboBoxStyle.DropDownList;
            ddGASelection.FlatStyle = FlatStyle.Flat;
            ddGASelection.FormattingEnabled = true;
            ddGASelection.Location = new Point(79, 12);
            ddGASelection.Name = "ddGASelection";
            ddGASelection.Size = new Size(197, 23);
            ddGASelection.TabIndex = 30;
            // 
            // eMinChromosome
            // 
            eMinChromosome.Location = new Point(151, 261);
            eMinChromosome.Name = "eMinChromosome";
            eMinChromosome.Size = new Size(35, 23);
            eMinChromosome.TabIndex = 35;
            eMinChromosome.Text = "50";
            // 
            // lGACrossOver
            // 
            lGACrossOver.AutoSize = true;
            lGACrossOver.Location = new Point(6, 44);
            lGACrossOver.Name = "lGACrossOver";
            lGACrossOver.Size = new Size(61, 15);
            lGACrossOver.TabIndex = 27;
            lGACrossOver.Text = "CrossOver";
            // 
            // lMinMaxSeperator
            // 
            lMinMaxSeperator.AutoSize = true;
            lMinMaxSeperator.Location = new Point(192, 267);
            lMinMaxSeperator.Name = "lMinMaxSeperator";
            lMinMaxSeperator.Size = new Size(12, 15);
            lMinMaxSeperator.TabIndex = 37;
            lMinMaxSeperator.Text = "/";
            // 
            // timerOptimization
            // 
            timerOptimization.Tick += timerOptimization_Tick;
            // 
            // openFileJson
            // 
            openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // saveFileJson
            // 
            saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // GAControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitGA);
            Name = "GAControl";
            Size = new Size(1003, 476);
            splitGA.Panel1.ResumeLayout(false);
            splitGA.Panel2.ResumeLayout(false);
            splitGA.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitGA).EndInit();
            splitGA.ResumeLayout(false);
            splitGAParams.Panel1.ResumeLayout(false);
            splitGAParams.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitGAParams).EndInit();
            splitGAParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgMinMaxValues).EndInit();
            pMinMax.ResumeLayout(false);
            pMinMax.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgFitnessFunctions).EndInit();
            pFitnessFunctions.ResumeLayout(false);
            pFitnessFunctions.PerformLayout();
            grTermination.ResumeLayout(false);
            grTermination.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitGA;
        private SplitContainer splitGAParams;
        private DataGridView dgMinMaxValues;
        private DataGridViewTextBoxColumn colParameter;
        private DataGridViewTextBoxColumn colMinValue;
        private DataGridViewTextBoxColumn colMaxValue;
        private Panel pMinMax;
        private LinkLabel linkSuggestMinMax;
        private LinkLabel linkAddFitnessFunction;
        private Panel pFitnessFunctions;
        private Panel pLineFitnessFunctions;
        private Label lFitnessFunctions;
        private TextBox eTerminationParameter;
        private Label lGATerminationParameter;
        private Label LGAReinsertion;
        private ComboBox ddGAReinsertion;
        private Label lGASelection;
        private ComboBox ddGACrossOver;
        private TextBox eMaxChromosome;
        private Label lGAMutation;
        private ComboBox ddGAMutation;
        private Label lGAMinMaxChromosome;
        private ComboBox ddGATermination;
        private ComboBox ddGASelection;
        private TextBox eMinChromosome;
        private Label lGACrossOver;
        private Label lMinMaxSeperator;
        private System.Windows.Forms.Timer timerOptimization;
        private LinkLabel linkLoadGAParams;
        private LinkLabel linkSaveGAParams;
        private Button btnOptimize;
        private OpenFileDialog openFileJson;
        private SaveFileDialog saveFileJson;
        private Label lOptimizationOutput;
        private Button btnCalculateFitness;
        private GroupBox grTermination;
        private CheckBox cbTargetFitness;
        private CheckBox cbCustomTermination;
        private TextBox eTargetFitness;
        private TextBox eMaxGeneration;
        private CheckBox cbMaxGeneration;
        private ToolTip toolTip;
        private DataGridView dgFitnessFunctions;
        private DataGridViewTextBoxColumn colFFMode;
        private DataGridViewTextBoxColumn colFFWeight;
        private DataGridViewTextBoxColumn colFFFunction;
        private DataGridViewLinkColumn colFFEdit;
        private DataGridViewLinkColumn colFFDelete;
    }
}
