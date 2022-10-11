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
            this.tabOptimization = new System.Windows.Forms.TabControl();
            this.tGAParameters = new System.Windows.Forms.TabPage();
            this.splitGAParams = new System.Windows.Forms.SplitContainer();
            this.dgMinMaxValues = new System.Windows.Forms.DataGridView();
            this.colParameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pMinMax = new System.Windows.Forms.Panel();
            this.linkSuggestMinMax = new System.Windows.Forms.LinkLabel();
            this.pfGAParams = new System.Windows.Forms.FlowLayoutPanel();
            this.linkAddFitnessFunction = new System.Windows.Forms.LinkLabel();
            this.pFitnessFunctions = new System.Windows.Forms.Panel();
            this.pLineFitnessFunctions = new System.Windows.Forms.Panel();
            this.lFitnessFunctions = new System.Windows.Forms.Label();
            this.tGAOutput = new System.Windows.Forms.TabPage();
            this.eOptimizationOutput = new System.Windows.Forms.RichTextBox();
            this.eTerminationParameter = new System.Windows.Forms.TextBox();
            this.lGATerminationParameter = new System.Windows.Forms.Label();
            this.LGAReinsertion = new System.Windows.Forms.Label();
            this.ddGAReinsertion = new System.Windows.Forms.ComboBox();
            this.lGASelection = new System.Windows.Forms.Label();
            this.ddGACrossOver = new System.Windows.Forms.ComboBox();
            this.eMaxChromosome = new System.Windows.Forms.TextBox();
            this.lGAMutation = new System.Windows.Forms.Label();
            this.ddGAMutation = new System.Windows.Forms.ComboBox();
            this.lGAMinMaxChromosome = new System.Windows.Forms.Label();
            this.ddGATermination = new System.Windows.Forms.ComboBox();
            this.ddGASelection = new System.Windows.Forms.ComboBox();
            this.linkOptimize = new System.Windows.Forms.LinkLabel();
            this.eMinChromosome = new System.Windows.Forms.TextBox();
            this.lGACrossOver = new System.Windows.Forms.Label();
            this.lMinMaxSeperator = new System.Windows.Forms.Label();
            this.lGATermination = new System.Windows.Forms.Label();
            this.timerOptimization = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitGA)).BeginInit();
            this.splitGA.Panel1.SuspendLayout();
            this.splitGA.Panel2.SuspendLayout();
            this.splitGA.SuspendLayout();
            this.tabOptimization.SuspendLayout();
            this.tGAParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGAParams)).BeginInit();
            this.splitGAParams.Panel1.SuspendLayout();
            this.splitGAParams.Panel2.SuspendLayout();
            this.splitGAParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMinMaxValues)).BeginInit();
            this.pMinMax.SuspendLayout();
            this.pfGAParams.SuspendLayout();
            this.pFitnessFunctions.SuspendLayout();
            this.tGAOutput.SuspendLayout();
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
            this.splitGA.Panel1.Controls.Add(this.tabOptimization);
            // 
            // splitGA.Panel2
            // 
            this.splitGA.Panel2.Controls.Add(this.eTerminationParameter);
            this.splitGA.Panel2.Controls.Add(this.lGATerminationParameter);
            this.splitGA.Panel2.Controls.Add(this.LGAReinsertion);
            this.splitGA.Panel2.Controls.Add(this.ddGAReinsertion);
            this.splitGA.Panel2.Controls.Add(this.lGASelection);
            this.splitGA.Panel2.Controls.Add(this.ddGACrossOver);
            this.splitGA.Panel2.Controls.Add(this.eMaxChromosome);
            this.splitGA.Panel2.Controls.Add(this.lGAMutation);
            this.splitGA.Panel2.Controls.Add(this.ddGAMutation);
            this.splitGA.Panel2.Controls.Add(this.lGAMinMaxChromosome);
            this.splitGA.Panel2.Controls.Add(this.ddGATermination);
            this.splitGA.Panel2.Controls.Add(this.ddGASelection);
            this.splitGA.Panel2.Controls.Add(this.linkOptimize);
            this.splitGA.Panel2.Controls.Add(this.eMinChromosome);
            this.splitGA.Panel2.Controls.Add(this.lGACrossOver);
            this.splitGA.Panel2.Controls.Add(this.lMinMaxSeperator);
            this.splitGA.Panel2.Controls.Add(this.lGATermination);
            this.splitGA.Size = new System.Drawing.Size(1003, 476);
            this.splitGA.SplitterDistance = 697;
            this.splitGA.TabIndex = 39;
            // 
            // tabOptimization
            // 
            this.tabOptimization.Controls.Add(this.tGAParameters);
            this.tabOptimization.Controls.Add(this.tGAOutput);
            this.tabOptimization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOptimization.Location = new System.Drawing.Point(0, 0);
            this.tabOptimization.Name = "tabOptimization";
            this.tabOptimization.SelectedIndex = 0;
            this.tabOptimization.Size = new System.Drawing.Size(695, 474);
            this.tabOptimization.TabIndex = 30;
            // 
            // tGAParameters
            // 
            this.tGAParameters.Controls.Add(this.splitGAParams);
            this.tGAParameters.Location = new System.Drawing.Point(4, 24);
            this.tGAParameters.Name = "tGAParameters";
            this.tGAParameters.Padding = new System.Windows.Forms.Padding(3);
            this.tGAParameters.Size = new System.Drawing.Size(687, 446);
            this.tGAParameters.TabIndex = 0;
            this.tGAParameters.Text = "Optimization Parameters";
            this.tGAParameters.UseVisualStyleBackColor = true;
            // 
            // splitGAParams
            // 
            this.splitGAParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitGAParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGAParams.Location = new System.Drawing.Point(3, 3);
            this.splitGAParams.Name = "splitGAParams";
            // 
            // splitGAParams.Panel1
            // 
            this.splitGAParams.Panel1.Controls.Add(this.dgMinMaxValues);
            this.splitGAParams.Panel1.Controls.Add(this.pMinMax);
            // 
            // splitGAParams.Panel2
            // 
            this.splitGAParams.Panel2.Controls.Add(this.pfGAParams);
            this.splitGAParams.Panel2.Controls.Add(this.pFitnessFunctions);
            this.splitGAParams.Size = new System.Drawing.Size(681, 440);
            this.splitGAParams.SplitterDistance = 225;
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
            this.dgMinMaxValues.Size = new System.Drawing.Size(223, 409);
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
            this.pMinMax.Controls.Add(this.linkSuggestMinMax);
            this.pMinMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMinMax.Location = new System.Drawing.Point(0, 0);
            this.pMinMax.Name = "pMinMax";
            this.pMinMax.Size = new System.Drawing.Size(223, 29);
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
            // pfGAParams
            // 
            this.pfGAParams.AutoScroll = true;
            this.pfGAParams.Controls.Add(this.linkAddFitnessFunction);
            this.pfGAParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfGAParams.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pfGAParams.Location = new System.Drawing.Point(0, 29);
            this.pfGAParams.Name = "pfGAParams";
            this.pfGAParams.Size = new System.Drawing.Size(450, 409);
            this.pfGAParams.TabIndex = 0;
            // 
            // linkAddFitnessFunction
            // 
            this.linkAddFitnessFunction.AutoSize = true;
            this.linkAddFitnessFunction.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkAddFitnessFunction.Location = new System.Drawing.Point(3, 0);
            this.linkAddFitnessFunction.Name = "linkAddFitnessFunction";
            this.linkAddFitnessFunction.Padding = new System.Windows.Forms.Padding(10);
            this.linkAddFitnessFunction.Size = new System.Drawing.Size(138, 35);
            this.linkAddFitnessFunction.TabIndex = 1;
            this.linkAddFitnessFunction.TabStop = true;
            this.linkAddFitnessFunction.Text = "Add Fitness Function";
            this.linkAddFitnessFunction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAddFitnessFunction_LinkClicked);
            // 
            // pFitnessFunctions
            // 
            this.pFitnessFunctions.Controls.Add(this.pLineFitnessFunctions);
            this.pFitnessFunctions.Controls.Add(this.lFitnessFunctions);
            this.pFitnessFunctions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFitnessFunctions.Location = new System.Drawing.Point(0, 0);
            this.pFitnessFunctions.Name = "pFitnessFunctions";
            this.pFitnessFunctions.Size = new System.Drawing.Size(450, 29);
            this.pFitnessFunctions.TabIndex = 29;
            // 
            // pLineFitnessFunctions
            // 
            this.pLineFitnessFunctions.BackColor = System.Drawing.Color.LightGray;
            this.pLineFitnessFunctions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pLineFitnessFunctions.Location = new System.Drawing.Point(0, 28);
            this.pLineFitnessFunctions.Name = "pLineFitnessFunctions";
            this.pLineFitnessFunctions.Size = new System.Drawing.Size(450, 1);
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
            // tGAOutput
            // 
            this.tGAOutput.Controls.Add(this.eOptimizationOutput);
            this.tGAOutput.Location = new System.Drawing.Point(4, 24);
            this.tGAOutput.Name = "tGAOutput";
            this.tGAOutput.Size = new System.Drawing.Size(687, 446);
            this.tGAOutput.TabIndex = 2;
            this.tGAOutput.Text = "Output";
            this.tGAOutput.UseVisualStyleBackColor = true;
            // 
            // eOptimizationOutput
            // 
            this.eOptimizationOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eOptimizationOutput.Location = new System.Drawing.Point(0, 0);
            this.eOptimizationOutput.Name = "eOptimizationOutput";
            this.eOptimizationOutput.Size = new System.Drawing.Size(687, 446);
            this.eOptimizationOutput.TabIndex = 25;
            this.eOptimizationOutput.Text = "";
            // 
            // eTerminationParameter
            // 
            this.eTerminationParameter.Location = new System.Drawing.Point(143, 157);
            this.eTerminationParameter.Name = "eTerminationParameter";
            this.eTerminationParameter.Size = new System.Drawing.Size(35, 23);
            this.eTerminationParameter.TabIndex = 41;
            this.eTerminationParameter.Text = "50";
            // 
            // lGATerminationParameter
            // 
            this.lGATerminationParameter.AutoSize = true;
            this.lGATerminationParameter.Location = new System.Drawing.Point(3, 162);
            this.lGATerminationParameter.Name = "lGATerminationParameter";
            this.lGATerminationParameter.Size = new System.Drawing.Size(127, 15);
            this.lGATerminationParameter.TabIndex = 40;
            this.lGATerminationParameter.Text = "Termination Parameter";
            // 
            // LGAReinsertion
            // 
            this.LGAReinsertion.AutoSize = true;
            this.LGAReinsertion.Location = new System.Drawing.Point(3, 102);
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
            this.ddGAReinsertion.Size = new System.Drawing.Size(206, 23);
            this.ddGAReinsertion.TabIndex = 39;
            // 
            // lGASelection
            // 
            this.lGASelection.AutoSize = true;
            this.lGASelection.Location = new System.Drawing.Point(3, 15);
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
            this.ddGACrossOver.Size = new System.Drawing.Size(206, 23);
            this.ddGACrossOver.TabIndex = 31;
            // 
            // eMaxChromosome
            // 
            this.eMaxChromosome.Location = new System.Drawing.Point(197, 190);
            this.eMaxChromosome.Name = "eMaxChromosome";
            this.eMaxChromosome.Size = new System.Drawing.Size(35, 23);
            this.eMaxChromosome.TabIndex = 36;
            this.eMaxChromosome.Text = "100";
            // 
            // lGAMutation
            // 
            this.lGAMutation.AutoSize = true;
            this.lGAMutation.Location = new System.Drawing.Point(3, 73);
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
            this.ddGAMutation.Size = new System.Drawing.Size(206, 23);
            this.ddGAMutation.TabIndex = 32;
            // 
            // lGAMinMaxChromosome
            // 
            this.lGAMinMaxChromosome.AutoSize = true;
            this.lGAMinMaxChromosome.Location = new System.Drawing.Point(0, 195);
            this.lGAMinMaxChromosome.Name = "lGAMinMaxChromosome";
            this.lGAMinMaxChromosome.Size = new System.Drawing.Size(142, 15);
            this.lGAMinMaxChromosome.TabIndex = 34;
            this.lGAMinMaxChromosome.Text = "Min/Max Chromosome #";
            // 
            // ddGATermination
            // 
            this.ddGATermination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddGATermination.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddGATermination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddGATermination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddGATermination.FormattingEnabled = true;
            this.ddGATermination.Location = new System.Drawing.Point(79, 128);
            this.ddGATermination.Name = "ddGATermination";
            this.ddGATermination.Size = new System.Drawing.Size(206, 23);
            this.ddGATermination.TabIndex = 33;
            this.ddGATermination.SelectedIndexChanged += new System.EventHandler(this.ddGATermination_SelectedIndexChanged);
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
            this.ddGASelection.Size = new System.Drawing.Size(206, 23);
            this.ddGASelection.TabIndex = 30;
            // 
            // linkOptimize
            // 
            this.linkOptimize.AutoSize = true;
            this.linkOptimize.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkOptimize.Location = new System.Drawing.Point(6, 223);
            this.linkOptimize.Name = "linkOptimize";
            this.linkOptimize.Size = new System.Drawing.Size(55, 15);
            this.linkOptimize.TabIndex = 0;
            this.linkOptimize.TabStop = true;
            this.linkOptimize.Text = "Optimize";
            this.linkOptimize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOptimize_LinkClicked);
            // 
            // eMinChromosome
            // 
            this.eMinChromosome.Location = new System.Drawing.Point(143, 190);
            this.eMinChromosome.Name = "eMinChromosome";
            this.eMinChromosome.Size = new System.Drawing.Size(35, 23);
            this.eMinChromosome.TabIndex = 35;
            this.eMinChromosome.Text = "50";
            // 
            // lGACrossOver
            // 
            this.lGACrossOver.AutoSize = true;
            this.lGACrossOver.Location = new System.Drawing.Point(3, 44);
            this.lGACrossOver.Name = "lGACrossOver";
            this.lGACrossOver.Size = new System.Drawing.Size(61, 15);
            this.lGACrossOver.TabIndex = 27;
            this.lGACrossOver.Text = "CrossOver";
            // 
            // lMinMaxSeperator
            // 
            this.lMinMaxSeperator.AutoSize = true;
            this.lMinMaxSeperator.Location = new System.Drawing.Point(184, 196);
            this.lMinMaxSeperator.Name = "lMinMaxSeperator";
            this.lMinMaxSeperator.Size = new System.Drawing.Size(12, 15);
            this.lMinMaxSeperator.TabIndex = 37;
            this.lMinMaxSeperator.Text = "/";
            // 
            // lGATermination
            // 
            this.lGATermination.AutoSize = true;
            this.lGATermination.Location = new System.Drawing.Point(3, 131);
            this.lGATermination.Name = "lGATermination";
            this.lGATermination.Size = new System.Drawing.Size(70, 15);
            this.lGATermination.TabIndex = 29;
            this.lGATermination.Text = "Termination";
            // 
            // timerOptimization
            // 
            this.timerOptimization.Tick += new System.EventHandler(this.timerOptimization_Tick);
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
            this.tabOptimization.ResumeLayout(false);
            this.tGAParameters.ResumeLayout(false);
            this.splitGAParams.Panel1.ResumeLayout(false);
            this.splitGAParams.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGAParams)).EndInit();
            this.splitGAParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMinMaxValues)).EndInit();
            this.pMinMax.ResumeLayout(false);
            this.pMinMax.PerformLayout();
            this.pfGAParams.ResumeLayout(false);
            this.pfGAParams.PerformLayout();
            this.pFitnessFunctions.ResumeLayout(false);
            this.pFitnessFunctions.PerformLayout();
            this.tGAOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitGA;
        private TabControl tabOptimization;
        private TabPage tGAParameters;
        private SplitContainer splitGAParams;
        private DataGridView dgMinMaxValues;
        private DataGridViewTextBoxColumn colParameter;
        private DataGridViewTextBoxColumn colMinValue;
        private DataGridViewTextBoxColumn colMaxValue;
        private Panel pMinMax;
        private LinkLabel linkSuggestMinMax;
        private FlowLayoutPanel pfGAParams;
        private LinkLabel linkAddFitnessFunction;
        private Panel pFitnessFunctions;
        private Panel pLineFitnessFunctions;
        private Label lFitnessFunctions;
        private TabPage tGAOutput;
        private RichTextBox eOptimizationOutput;
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
        private LinkLabel linkOptimize;
        private TextBox eMinChromosome;
        private Label lGACrossOver;
        private Label lMinMaxSeperator;
        private Label lGATermination;
        private System.Windows.Forms.Timer timerOptimization;
    }
}
