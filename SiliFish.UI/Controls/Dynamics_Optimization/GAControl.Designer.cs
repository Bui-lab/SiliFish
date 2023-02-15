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
            this.components = new System.ComponentModel.Container();
            this.splitGA = new System.Windows.Forms.SplitContainer();
            this.splitGAParams = new System.Windows.Forms.SplitContainer();
            this.dgMinMaxValues = new System.Windows.Forms.DataGridView();
            this.colParameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pMinMax = new System.Windows.Forms.Panel();
            this.linkSuggestMinMax = new System.Windows.Forms.LinkLabel();
            this.dgFitnessFunctions = new System.Windows.Forms.DataGridView();
            this.linkAddFitnessFunction = new System.Windows.Forms.LinkLabel();
            this.pFitnessFunctions = new System.Windows.Forms.Panel();
            this.btnCalculateFitness = new System.Windows.Forms.Button();
            this.pLineFitnessFunctions = new System.Windows.Forms.Panel();
            this.lFitnessFunctions = new System.Windows.Forms.Label();
            this.grTermination = new System.Windows.Forms.GroupBox();
            this.cbTargetFitness = new System.Windows.Forms.CheckBox();
            this.cbCustomTermination = new System.Windows.Forms.CheckBox();
            this.eTargetFitness = new System.Windows.Forms.TextBox();
            this.eMaxGeneration = new System.Windows.Forms.TextBox();
            this.cbMaxGeneration = new System.Windows.Forms.CheckBox();
            this.ddGATermination = new System.Windows.Forms.ComboBox();
            this.lGATerminationParameter = new System.Windows.Forms.Label();
            this.eTerminationParameter = new System.Windows.Forms.TextBox();
            this.lOptimizationOutput = new System.Windows.Forms.Label();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.linkLoadGAParams = new System.Windows.Forms.LinkLabel();
            this.linkSaveGAParams = new System.Windows.Forms.LinkLabel();
            this.LGAReinsertion = new System.Windows.Forms.Label();
            this.ddGAReinsertion = new System.Windows.Forms.ComboBox();
            this.lGASelection = new System.Windows.Forms.Label();
            this.ddGACrossOver = new System.Windows.Forms.ComboBox();
            this.eMaxChromosome = new System.Windows.Forms.TextBox();
            this.lGAMutation = new System.Windows.Forms.Label();
            this.ddGAMutation = new System.Windows.Forms.ComboBox();
            this.lGAMinMaxChromosome = new System.Windows.Forms.Label();
            this.ddGASelection = new System.Windows.Forms.ComboBox();
            this.eMinChromosome = new System.Windows.Forms.TextBox();
            this.lGACrossOver = new System.Windows.Forms.Label();
            this.lMinMaxSeperator = new System.Windows.Forms.Label();
            this.timerOptimization = new System.Windows.Forms.Timer(this.components);
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            this.saveFileJson = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.colFFMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFFWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFFFunction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFFEdit = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colFFDelete = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitGA)).BeginInit();
            this.splitGA.Panel1.SuspendLayout();
            this.splitGA.Panel2.SuspendLayout();
            this.splitGA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGAParams)).BeginInit();
            this.splitGAParams.Panel1.SuspendLayout();
            this.splitGAParams.Panel2.SuspendLayout();
            this.splitGAParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMinMaxValues)).BeginInit();
            this.pMinMax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFitnessFunctions)).BeginInit();
            this.pFitnessFunctions.SuspendLayout();
            this.grTermination.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitGA
            // 
            this.splitGA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitGA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGA.Location = new System.Drawing.Point(0, 0);
            this.splitGA.Name = "splitGA";
            // 
            // splitGA.Panel1
            // 
            this.splitGA.Panel1.Controls.Add(this.splitGAParams);
            // 
            // splitGA.Panel2
            // 
            this.splitGA.Panel2.BackColor = System.Drawing.Color.White;
            this.splitGA.Panel2.Controls.Add(this.grTermination);
            this.splitGA.Panel2.Controls.Add(this.lOptimizationOutput);
            this.splitGA.Panel2.Controls.Add(this.btnOptimize);
            this.splitGA.Panel2.Controls.Add(this.linkLoadGAParams);
            this.splitGA.Panel2.Controls.Add(this.linkSaveGAParams);
            this.splitGA.Panel2.Controls.Add(this.LGAReinsertion);
            this.splitGA.Panel2.Controls.Add(this.ddGAReinsertion);
            this.splitGA.Panel2.Controls.Add(this.lGASelection);
            this.splitGA.Panel2.Controls.Add(this.ddGACrossOver);
            this.splitGA.Panel2.Controls.Add(this.eMaxChromosome);
            this.splitGA.Panel2.Controls.Add(this.lGAMutation);
            this.splitGA.Panel2.Controls.Add(this.ddGAMutation);
            this.splitGA.Panel2.Controls.Add(this.lGAMinMaxChromosome);
            this.splitGA.Panel2.Controls.Add(this.ddGASelection);
            this.splitGA.Panel2.Controls.Add(this.eMinChromosome);
            this.splitGA.Panel2.Controls.Add(this.lGACrossOver);
            this.splitGA.Panel2.Controls.Add(this.lMinMaxSeperator);
            this.splitGA.Size = new System.Drawing.Size(1003, 476);
            this.splitGA.SplitterDistance = 697;
            this.splitGA.TabIndex = 39;
            // 
            // splitGAParams
            // 
            this.splitGAParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitGAParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGAParams.Location = new System.Drawing.Point(0, 0);
            this.splitGAParams.Name = "splitGAParams";
            // 
            // splitGAParams.Panel1
            // 
            this.splitGAParams.Panel1.Controls.Add(this.dgMinMaxValues);
            this.splitGAParams.Panel1.Controls.Add(this.pMinMax);
            // 
            // splitGAParams.Panel2
            // 
            this.splitGAParams.Panel2.Controls.Add(this.dgFitnessFunctions);
            this.splitGAParams.Panel2.Controls.Add(this.pFitnessFunctions);
            this.splitGAParams.Size = new System.Drawing.Size(697, 476);
            this.splitGAParams.SplitterDistance = 214;
            this.splitGAParams.TabIndex = 29;
            // 
            // dgMinMaxValues
            // 
            this.dgMinMaxValues.AllowUserToAddRows = false;
            this.dgMinMaxValues.AllowUserToDeleteRows = false;
            this.dgMinMaxValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMinMaxValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParameter,
            this.colMinValue,
            this.colMaxValue});
            this.dgMinMaxValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMinMaxValues.Location = new System.Drawing.Point(0, 29);
            this.dgMinMaxValues.Name = "dgMinMaxValues";
            this.dgMinMaxValues.RowHeadersVisible = false;
            this.dgMinMaxValues.RowTemplate.Height = 25;
            this.dgMinMaxValues.Size = new System.Drawing.Size(212, 445);
            this.dgMinMaxValues.TabIndex = 27;
            // 
            // colParameter
            // 
            this.colParameter.Frozen = true;
            this.colParameter.HeaderText = "Parameter";
            this.colParameter.MinimumWidth = 100;
            this.colParameter.Name = "colParameter";
            this.colParameter.ReadOnly = true;
            this.colParameter.Width = 200;
            // 
            // colMinValue
            // 
            this.colMinValue.HeaderText = "Min Value";
            this.colMinValue.Name = "colMinValue";
            // 
            // colMaxValue
            // 
            this.colMaxValue.HeaderText = "Max Value";
            this.colMaxValue.Name = "colMaxValue";
            // 
            // pMinMax
            // 
            this.pMinMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pMinMax.Controls.Add(this.linkSuggestMinMax);
            this.pMinMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMinMax.Location = new System.Drawing.Point(0, 0);
            this.pMinMax.Name = "pMinMax";
            this.pMinMax.Size = new System.Drawing.Size(212, 29);
            this.pMinMax.TabIndex = 28;
            // 
            // linkSuggestMinMax
            // 
            this.linkSuggestMinMax.AutoSize = true;
            this.linkSuggestMinMax.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSuggestMinMax.Location = new System.Drawing.Point(6, 9);
            this.linkSuggestMinMax.Name = "linkSuggestMinMax";
            this.linkSuggestMinMax.Size = new System.Drawing.Size(137, 15);
            this.linkSuggestMinMax.TabIndex = 0;
            this.linkSuggestMinMax.TabStop = true;
            this.linkSuggestMinMax.Text = "Suggest Min/Max Values";
            this.linkSuggestMinMax.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSuggestMinMax_LinkClicked);
            // 
            // dgFitnessFunctions
            // 
            this.dgFitnessFunctions.AllowUserToAddRows = false;
            this.dgFitnessFunctions.AllowUserToDeleteRows = false;
            this.dgFitnessFunctions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFitnessFunctions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFFMode,
            this.colFFWeight,
            this.colFFFunction,
            this.colFFEdit,
            this.colFFDelete});
            this.dgFitnessFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFitnessFunctions.Location = new System.Drawing.Point(0, 29);
            this.dgFitnessFunctions.Name = "dgFitnessFunctions";
            this.dgFitnessFunctions.RowHeadersVisible = false;
            this.dgFitnessFunctions.RowTemplate.Height = 25;
            this.dgFitnessFunctions.Size = new System.Drawing.Size(477, 445);
            this.dgFitnessFunctions.TabIndex = 30;
            this.dgFitnessFunctions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFitnessFunctions_CellContentClick);
            this.dgFitnessFunctions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFitnessFunctions_CellDoubleClick);
            // 
            // linkAddFitnessFunction
            // 
            this.linkAddFitnessFunction.AutoSize = true;
            this.linkAddFitnessFunction.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkAddFitnessFunction.Location = new System.Drawing.Point(129, 6);
            this.linkAddFitnessFunction.Name = "linkAddFitnessFunction";
            this.linkAddFitnessFunction.Size = new System.Drawing.Size(118, 15);
            this.linkAddFitnessFunction.TabIndex = 1;
            this.linkAddFitnessFunction.TabStop = true;
            this.linkAddFitnessFunction.Text = "Add Fitness Function";
            this.linkAddFitnessFunction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAddFitnessFunction_LinkClicked);
            // 
            // pFitnessFunctions
            // 
            this.pFitnessFunctions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pFitnessFunctions.Controls.Add(this.linkAddFitnessFunction);
            this.pFitnessFunctions.Controls.Add(this.btnCalculateFitness);
            this.pFitnessFunctions.Controls.Add(this.pLineFitnessFunctions);
            this.pFitnessFunctions.Controls.Add(this.lFitnessFunctions);
            this.pFitnessFunctions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFitnessFunctions.Location = new System.Drawing.Point(0, 0);
            this.pFitnessFunctions.Name = "pFitnessFunctions";
            this.pFitnessFunctions.Size = new System.Drawing.Size(477, 29);
            this.pFitnessFunctions.TabIndex = 29;
            // 
            // btnCalculateFitness
            // 
            this.btnCalculateFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculateFitness.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnCalculateFitness.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnCalculateFitness.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculateFitness.ForeColor = System.Drawing.Color.White;
            this.btnCalculateFitness.Location = new System.Drawing.Point(351, 3);
            this.btnCalculateFitness.Name = "btnCalculateFitness";
            this.btnCalculateFitness.Size = new System.Drawing.Size(123, 23);
            this.btnCalculateFitness.TabIndex = 46;
            this.btnCalculateFitness.Text = "Calculate Fitness";
            this.btnCalculateFitness.UseVisualStyleBackColor = false;
            this.btnCalculateFitness.Click += new System.EventHandler(this.btnCalculateFitness_Click);
            // 
            // pLineFitnessFunctions
            // 
            this.pLineFitnessFunctions.BackColor = System.Drawing.Color.LightGray;
            this.pLineFitnessFunctions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineFitnessFunctions.Location = new System.Drawing.Point(0, 28);
            this.pLineFitnessFunctions.Name = "pLineFitnessFunctions";
            this.pLineFitnessFunctions.Size = new System.Drawing.Size(477, 1);
            this.pLineFitnessFunctions.TabIndex = 3;
            // 
            // lFitnessFunctions
            // 
            this.lFitnessFunctions.AutoSize = true;
            this.lFitnessFunctions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lFitnessFunctions.Location = new System.Drawing.Point(13, 6);
            this.lFitnessFunctions.Name = "lFitnessFunctions";
            this.lFitnessFunctions.Size = new System.Drawing.Size(101, 15);
            this.lFitnessFunctions.TabIndex = 2;
            this.lFitnessFunctions.Text = "Fitness Functions";
            // 
            // grTermination
            // 
            this.grTermination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grTermination.Controls.Add(this.cbTargetFitness);
            this.grTermination.Controls.Add(this.cbCustomTermination);
            this.grTermination.Controls.Add(this.eTargetFitness);
            this.grTermination.Controls.Add(this.eMaxGeneration);
            this.grTermination.Controls.Add(this.cbMaxGeneration);
            this.grTermination.Controls.Add(this.ddGATermination);
            this.grTermination.Controls.Add(this.lGATerminationParameter);
            this.grTermination.Controls.Add(this.eTerminationParameter);
            this.grTermination.Location = new System.Drawing.Point(6, 128);
            this.grTermination.Name = "grTermination";
            this.grTermination.Size = new System.Drawing.Size(279, 121);
            this.grTermination.TabIndex = 51;
            this.grTermination.TabStop = false;
            this.grTermination.Text = "Termination";
            // 
            // cbTargetFitness
            // 
            this.cbTargetFitness.AutoSize = true;
            this.cbTargetFitness.Location = new System.Drawing.Point(6, 16);
            this.cbTargetFitness.Name = "cbTargetFitness";
            this.cbTargetFitness.Size = new System.Drawing.Size(97, 19);
            this.cbTargetFitness.TabIndex = 46;
            this.cbTargetFitness.Text = "Target Fitness";
            this.cbTargetFitness.UseVisualStyleBackColor = true;
            this.cbTargetFitness.CheckedChanged += new System.EventHandler(this.cbTargetFitness_CheckedChanged);
            // 
            // cbCustomTermination
            // 
            this.cbCustomTermination.AutoSize = true;
            this.cbCustomTermination.Location = new System.Drawing.Point(6, 66);
            this.cbCustomTermination.Name = "cbCustomTermination";
            this.cbCustomTermination.Size = new System.Drawing.Size(68, 19);
            this.cbCustomTermination.TabIndex = 50;
            this.cbCustomTermination.Text = "Custom";
            this.cbCustomTermination.UseVisualStyleBackColor = true;
            this.cbCustomTermination.CheckedChanged += new System.EventHandler(this.cbCustomTermination_CheckedChanged);
            // 
            // eTargetFitness
            // 
            this.eTargetFitness.Location = new System.Drawing.Point(113, 13);
            this.eTargetFitness.Name = "eTargetFitness";
            this.eTargetFitness.Size = new System.Drawing.Size(35, 23);
            this.eTargetFitness.TabIndex = 47;
            this.eTargetFitness.Text = "1";
            this.eTargetFitness.Visible = false;
            // 
            // eMaxGeneration
            // 
            this.eMaxGeneration.Location = new System.Drawing.Point(113, 38);
            this.eMaxGeneration.Name = "eMaxGeneration";
            this.eMaxGeneration.Size = new System.Drawing.Size(35, 23);
            this.eMaxGeneration.TabIndex = 49;
            this.eMaxGeneration.Text = "50";
            this.eMaxGeneration.Visible = false;
            // 
            // cbMaxGeneration
            // 
            this.cbMaxGeneration.AutoSize = true;
            this.cbMaxGeneration.Location = new System.Drawing.Point(6, 41);
            this.cbMaxGeneration.Name = "cbMaxGeneration";
            this.cbMaxGeneration.Size = new System.Drawing.Size(110, 19);
            this.cbMaxGeneration.TabIndex = 48;
            this.cbMaxGeneration.Text = "Max Generation";
            this.cbMaxGeneration.UseVisualStyleBackColor = true;
            this.cbMaxGeneration.CheckedChanged += new System.EventHandler(this.cbMaxGeneration_CheckedChanged);
            // 
            // ddGATermination
            // 
            this.ddGATermination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddGATermination.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddGATermination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddGATermination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddGATermination.FormattingEnabled = true;
            this.ddGATermination.Location = new System.Drawing.Point(75, 64);
            this.ddGATermination.Name = "ddGATermination";
            this.ddGATermination.Size = new System.Drawing.Size(198, 23);
            this.ddGATermination.TabIndex = 33;
            this.ddGATermination.Visible = false;
            this.ddGATermination.SelectedIndexChanged += new System.EventHandler(this.ddGATermination_SelectedIndexChanged);
            // 
            // lGATerminationParameter
            // 
            this.lGATerminationParameter.AutoSize = true;
            this.lGATerminationParameter.Location = new System.Drawing.Point(6, 93);
            this.lGATerminationParameter.Name = "lGATerminationParameter";
            this.lGATerminationParameter.Size = new System.Drawing.Size(76, 15);
            this.lGATerminationParameter.TabIndex = 40;
            this.lGATerminationParameter.Text = "Term. Param.";
            this.lGATerminationParameter.Visible = false;
            // 
            // eTerminationParameter
            // 
            this.eTerminationParameter.Location = new System.Drawing.Point(113, 90);
            this.eTerminationParameter.Name = "eTerminationParameter";
            this.eTerminationParameter.Size = new System.Drawing.Size(35, 23);
            this.eTerminationParameter.TabIndex = 41;
            this.eTerminationParameter.Text = "50";
            this.eTerminationParameter.Visible = false;
            // 
            // lOptimizationOutput
            // 
            this.lOptimizationOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lOptimizationOutput.Location = new System.Drawing.Point(6, 316);
            this.lOptimizationOutput.Name = "lOptimizationOutput";
            this.lOptimizationOutput.Size = new System.Drawing.Size(270, 148);
            this.lOptimizationOutput.TabIndex = 45;
            this.lOptimizationOutput.Text = "Latest fitness:";
            // 
            // btnOptimize
            // 
            this.btnOptimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnOptimize.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnOptimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptimize.ForeColor = System.Drawing.Color.White;
            this.btnOptimize.Location = new System.Drawing.Point(6, 290);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(67, 23);
            this.btnOptimize.TabIndex = 44;
            this.btnOptimize.Text = "Optimize";
            this.btnOptimize.UseVisualStyleBackColor = false;
            this.btnOptimize.Click += new System.EventHandler(this.btnOptimize_Click);
            // 
            // linkLoadGAParams
            // 
            this.linkLoadGAParams.AutoSize = true;
            this.linkLoadGAParams.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLoadGAParams.Location = new System.Drawing.Point(84, 294);
            this.linkLoadGAParams.Name = "linkLoadGAParams";
            this.linkLoadGAParams.Size = new System.Drawing.Size(94, 15);
            this.linkLoadGAParams.TabIndex = 42;
            this.linkLoadGAParams.TabStop = true;
            this.linkLoadGAParams.Text = "Load GA Params";
            this.linkLoadGAParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLoadGAParams_LinkClicked);
            // 
            // linkSaveGAParams
            // 
            this.linkSaveGAParams.AutoSize = true;
            this.linkSaveGAParams.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveGAParams.Location = new System.Drawing.Point(184, 294);
            this.linkSaveGAParams.Name = "linkSaveGAParams";
            this.linkSaveGAParams.Size = new System.Drawing.Size(92, 15);
            this.linkSaveGAParams.TabIndex = 43;
            this.linkSaveGAParams.TabStop = true;
            this.linkSaveGAParams.Text = "Save GA Params";
            this.linkSaveGAParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveGAParams_LinkClicked);
            // 
            // LGAReinsertion
            // 
            this.LGAReinsertion.AutoSize = true;
            this.LGAReinsertion.Location = new System.Drawing.Point(6, 102);
            this.LGAReinsertion.Name = "LGAReinsertion";
            this.LGAReinsertion.Size = new System.Drawing.Size(66, 15);
            this.LGAReinsertion.TabIndex = 38;
            this.LGAReinsertion.Text = "Reinsertion";
            // 
            // ddGAReinsertion
            // 
            this.ddGAReinsertion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddGAReinsertion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddGAReinsertion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddGAReinsertion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddGAReinsertion.FormattingEnabled = true;
            this.ddGAReinsertion.Location = new System.Drawing.Point(79, 99);
            this.ddGAReinsertion.Name = "ddGAReinsertion";
            this.ddGAReinsertion.Size = new System.Drawing.Size(197, 23);
            this.ddGAReinsertion.TabIndex = 39;
            // 
            // lGASelection
            // 
            this.lGASelection.AutoSize = true;
            this.lGASelection.Location = new System.Drawing.Point(6, 15);
            this.lGASelection.Name = "lGASelection";
            this.lGASelection.Size = new System.Drawing.Size(55, 15);
            this.lGASelection.TabIndex = 26;
            this.lGASelection.Text = "Selection";
            // 
            // ddGACrossOver
            // 
            this.ddGACrossOver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddGACrossOver.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddGACrossOver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddGACrossOver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddGACrossOver.FormattingEnabled = true;
            this.ddGACrossOver.Location = new System.Drawing.Point(79, 41);
            this.ddGACrossOver.Name = "ddGACrossOver";
            this.ddGACrossOver.Size = new System.Drawing.Size(197, 23);
            this.ddGACrossOver.TabIndex = 31;
            // 
            // eMaxChromosome
            // 
            this.eMaxChromosome.Location = new System.Drawing.Point(205, 261);
            this.eMaxChromosome.Name = "eMaxChromosome";
            this.eMaxChromosome.Size = new System.Drawing.Size(35, 23);
            this.eMaxChromosome.TabIndex = 36;
            this.eMaxChromosome.Text = "100";
            // 
            // lGAMutation
            // 
            this.lGAMutation.AutoSize = true;
            this.lGAMutation.Location = new System.Drawing.Point(6, 73);
            this.lGAMutation.Name = "lGAMutation";
            this.lGAMutation.Size = new System.Drawing.Size(56, 15);
            this.lGAMutation.TabIndex = 28;
            this.lGAMutation.Text = "Mutation";
            // 
            // ddGAMutation
            // 
            this.ddGAMutation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddGAMutation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddGAMutation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddGAMutation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddGAMutation.FormattingEnabled = true;
            this.ddGAMutation.Location = new System.Drawing.Point(79, 70);
            this.ddGAMutation.Name = "ddGAMutation";
            this.ddGAMutation.Size = new System.Drawing.Size(197, 23);
            this.ddGAMutation.TabIndex = 32;
            // 
            // lGAMinMaxChromosome
            // 
            this.lGAMinMaxChromosome.AutoSize = true;
            this.lGAMinMaxChromosome.Location = new System.Drawing.Point(6, 264);
            this.lGAMinMaxChromosome.Name = "lGAMinMaxChromosome";
            this.lGAMinMaxChromosome.Size = new System.Drawing.Size(142, 15);
            this.lGAMinMaxChromosome.TabIndex = 34;
            this.lGAMinMaxChromosome.Text = "Min/Max Chromosome #";
            // 
            // ddGASelection
            // 
            this.ddGASelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddGASelection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddGASelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddGASelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddGASelection.FormattingEnabled = true;
            this.ddGASelection.Location = new System.Drawing.Point(79, 12);
            this.ddGASelection.Name = "ddGASelection";
            this.ddGASelection.Size = new System.Drawing.Size(197, 23);
            this.ddGASelection.TabIndex = 30;
            // 
            // eMinChromosome
            // 
            this.eMinChromosome.Location = new System.Drawing.Point(151, 261);
            this.eMinChromosome.Name = "eMinChromosome";
            this.eMinChromosome.Size = new System.Drawing.Size(35, 23);
            this.eMinChromosome.TabIndex = 35;
            this.eMinChromosome.Text = "50";
            // 
            // lGACrossOver
            // 
            this.lGACrossOver.AutoSize = true;
            this.lGACrossOver.Location = new System.Drawing.Point(6, 44);
            this.lGACrossOver.Name = "lGACrossOver";
            this.lGACrossOver.Size = new System.Drawing.Size(61, 15);
            this.lGACrossOver.TabIndex = 27;
            this.lGACrossOver.Text = "CrossOver";
            // 
            // lMinMaxSeperator
            // 
            this.lMinMaxSeperator.AutoSize = true;
            this.lMinMaxSeperator.Location = new System.Drawing.Point(192, 267);
            this.lMinMaxSeperator.Name = "lMinMaxSeperator";
            this.lMinMaxSeperator.Size = new System.Drawing.Size(12, 15);
            this.lMinMaxSeperator.TabIndex = 37;
            this.lMinMaxSeperator.Text = "/";
            // 
            // timerOptimization
            // 
            this.timerOptimization.Tick += new System.EventHandler(this.timerOptimization_Tick);
            // 
            // openFileJson
            // 
            this.openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // saveFileJson
            // 
            this.saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // colFFMode
            // 
            this.colFFMode.HeaderText = "Mode";
            this.colFFMode.Name = "colFFMode";
            this.colFFMode.ReadOnly = true;
            // 
            // colFFWeight
            // 
            this.colFFWeight.HeaderText = "Weight";
            this.colFFWeight.Name = "colFFWeight";
            // 
            // colFFFunction
            // 
            this.colFFFunction.HeaderText = "Function";
            this.colFFFunction.Name = "colFFFunction";
            this.colFFFunction.ReadOnly = true;
            // 
            // colFFEdit
            // 
            this.colFFEdit.HeaderText = "";
            this.colFFEdit.MinimumWidth = 50;
            this.colFFEdit.Name = "colFFEdit";
            this.colFFEdit.Text = "Edit";
            this.colFFEdit.UseColumnTextForLinkValue = true;
            // 
            // colFFDelete
            // 
            this.colFFDelete.HeaderText = "";
            this.colFFDelete.MinimumWidth = 50;
            this.colFFDelete.Name = "colFFDelete";
            this.colFFDelete.Text = "Delete";
            this.colFFDelete.UseColumnTextForLinkValue = true;
            // 
            // GAControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitGA);
            this.Name = "GAControl";
            this.Size = new System.Drawing.Size(1003, 476);
            this.splitGA.Panel1.ResumeLayout(false);
            this.splitGA.Panel2.ResumeLayout(false);
            this.splitGA.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGA)).EndInit();
            this.splitGA.ResumeLayout(false);
            this.splitGAParams.Panel1.ResumeLayout(false);
            this.splitGAParams.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGAParams)).EndInit();
            this.splitGAParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMinMaxValues)).EndInit();
            this.pMinMax.ResumeLayout(false);
            this.pMinMax.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFitnessFunctions)).EndInit();
            this.pFitnessFunctions.ResumeLayout(false);
            this.pFitnessFunctions.PerformLayout();
            this.grTermination.ResumeLayout(false);
            this.grTermination.PerformLayout();
            this.ResumeLayout(false);

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
