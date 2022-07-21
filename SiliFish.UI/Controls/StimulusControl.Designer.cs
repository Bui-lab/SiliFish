namespace SiliFish.UI.Controls
{
    partial class StimulusControl
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
            this.lStimulus = new System.Windows.Forms.Label();
            this.ddStimulusMode = new System.Windows.Forms.ComboBox();
            this.eValue2 = new System.Windows.Forms.TextBox();
            this.eValue1 = new System.Windows.Forms.TextBox();
            this.lValue1 = new System.Windows.Forms.Label();
            this.lValue2 = new System.Windows.Forms.Label();
            this.timeLineControl = new SiliFish.UI.Controls.TimeLineControl();
            this.ddTargetPool = new System.Windows.Forms.ComboBox();
            this.lTargetPool = new System.Windows.Forms.Label();
            this.lSagittalPosition = new System.Windows.Forms.Label();
            this.ddSagittalPosition = new System.Windows.Forms.ComboBox();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.lTargetCell = new System.Windows.Forms.Label();
            this.ddTargetCells = new System.Windows.Forms.ComboBox();
            this.ddTargetSomites = new System.Windows.Forms.ComboBox();
            this.lTargetSomites = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lStimulus
            // 
            this.lStimulus.AutoSize = true;
            this.lStimulus.Location = new System.Drawing.Point(3, 15);
            this.lStimulus.Name = "lStimulus";
            this.lStimulus.Size = new System.Drawing.Size(53, 15);
            this.lStimulus.TabIndex = 0;
            this.lStimulus.Text = "Stimulus";
            // 
            // ddStimulusMode
            // 
            this.ddStimulusMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddStimulusMode.FormattingEnabled = true;
            this.ddStimulusMode.Items.AddRange(new object[] {
            "Step",
            "Gaussian",
            "Ramp"});
            this.ddStimulusMode.Location = new System.Drawing.Point(101, 12);
            this.ddStimulusMode.Name = "ddStimulusMode";
            this.ddStimulusMode.Size = new System.Drawing.Size(121, 23);
            this.ddStimulusMode.TabIndex = 1;
            this.ddStimulusMode.SelectedIndexChanged += new System.EventHandler(this.ddStimulusMode_SelectedIndexChanged);
            // 
            // eValue2
            // 
            this.eValue2.Location = new System.Drawing.Point(101, 64);
            this.eValue2.Name = "eValue2";
            this.eValue2.Size = new System.Drawing.Size(52, 23);
            this.eValue2.TabIndex = 5;
            this.eValue2.TextChanged += new System.EventHandler(this.eValue2_TextChanged);
            // 
            // eValue1
            // 
            this.eValue1.Location = new System.Drawing.Point(101, 38);
            this.eValue1.Name = "eValue1";
            this.eValue1.Size = new System.Drawing.Size(52, 23);
            this.eValue1.TabIndex = 3;
            this.eValue1.TextChanged += new System.EventHandler(this.eValue1_TextChanged);
            // 
            // lValue1
            // 
            this.lValue1.AutoSize = true;
            this.lValue1.Location = new System.Drawing.Point(3, 41);
            this.lValue1.Name = "lValue1";
            this.lValue1.Size = new System.Drawing.Size(37, 15);
            this.lValue1.TabIndex = 2;
            this.lValue1.Text = "Mean";
            // 
            // lValue2
            // 
            this.lValue2.AutoSize = true;
            this.lValue2.Location = new System.Drawing.Point(3, 67);
            this.lValue2.Name = "lValue2";
            this.lValue2.Size = new System.Drawing.Size(47, 15);
            this.lValue2.TabIndex = 4;
            this.lValue2.Text = "Std Dev";
            // 
            // timeLineControl
            // 
            this.timeLineControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.timeLineControl.Location = new System.Drawing.Point(0, 225);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(236, 117);
            this.timeLineControl.TabIndex = 15;
            // 
            // ddTargetPool
            // 
            this.ddTargetPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetPool.FormattingEnabled = true;
            this.ddTargetPool.Location = new System.Drawing.Point(101, 90);
            this.ddTargetPool.Name = "ddTargetPool";
            this.ddTargetPool.Size = new System.Drawing.Size(121, 23);
            this.ddTargetPool.TabIndex = 7;
            this.ddTargetPool.SelectedIndexChanged += new System.EventHandler(this.ddTargetPool_SelectedIndexChanged);
            // 
            // lTargetPool
            // 
            this.lTargetPool.AutoSize = true;
            this.lTargetPool.Location = new System.Drawing.Point(3, 93);
            this.lTargetPool.Name = "lTargetPool";
            this.lTargetPool.Size = new System.Drawing.Size(66, 15);
            this.lTargetPool.TabIndex = 6;
            this.lTargetPool.Text = "Target Pool";
            // 
            // lSagittalPosition
            // 
            this.lSagittalPosition.AutoSize = true;
            this.lSagittalPosition.Location = new System.Drawing.Point(3, 171);
            this.lSagittalPosition.Name = "lSagittalPosition";
            this.lSagittalPosition.Size = new System.Drawing.Size(92, 15);
            this.lSagittalPosition.TabIndex = 12;
            this.lSagittalPosition.Text = "Sagittal Position";
            // 
            // ddSagittalPosition
            // 
            this.ddSagittalPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSagittalPosition.FormattingEnabled = true;
            this.ddSagittalPosition.Items.AddRange(new object[] {
            "Left/Right",
            "Left",
            "Right"});
            this.ddSagittalPosition.Location = new System.Drawing.Point(101, 168);
            this.ddSagittalPosition.Name = "ddSagittalPosition";
            this.ddSagittalPosition.Size = new System.Drawing.Size(121, 23);
            this.ddSagittalPosition.TabIndex = 13;
            this.ddSagittalPosition.SelectedIndexChanged += new System.EventHandler(this.ddSagittalPosition_SelectedIndexChanged);
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(163, 200);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(59, 19);
            this.cbActive.TabIndex = 14;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // lTargetCell
            // 
            this.lTargetCell.AutoSize = true;
            this.lTargetCell.Location = new System.Drawing.Point(3, 145);
            this.lTargetCell.Name = "lTargetCell";
            this.lTargetCell.Size = new System.Drawing.Size(75, 15);
            this.lTargetCell.TabIndex = 10;
            this.lTargetCell.Text = "Target Cell(s)";
            // 
            // ddTargetCells
            // 
            this.ddTargetCells.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetCells.FormattingEnabled = true;
            this.ddTargetCells.Items.AddRange(new object[] {
            "All Cells",
            "Enter cell number",
            "or range (0-5)"});
            this.ddTargetCells.Location = new System.Drawing.Point(101, 142);
            this.ddTargetCells.Name = "ddTargetCells";
            this.ddTargetCells.Size = new System.Drawing.Size(121, 23);
            this.ddTargetCells.TabIndex = 11;
            this.ddTargetCells.SelectedIndexChanged += new System.EventHandler(this.ddTargetCell_SelectedIndexChanged);
            // 
            // ddTargetSomites
            // 
            this.ddTargetSomites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetSomites.FormattingEnabled = true;
            this.ddTargetSomites.Items.AddRange(new object[] {
            "All Somites",
            "Enter somite number",
            "or range (0-5)"});
            this.ddTargetSomites.Location = new System.Drawing.Point(101, 116);
            this.ddTargetSomites.Name = "ddTargetSomites";
            this.ddTargetSomites.Size = new System.Drawing.Size(121, 23);
            this.ddTargetSomites.TabIndex = 9;
            this.ddTargetSomites.SelectedIndexChanged += new System.EventHandler(this.ddTargetSomites_SelectedIndexChanged);
            // 
            // lTargetSomites
            // 
            this.lTargetSomites.AutoSize = true;
            this.lTargetSomites.Location = new System.Drawing.Point(3, 119);
            this.lTargetSomites.Name = "lTargetSomites";
            this.lTargetSomites.Size = new System.Drawing.Size(92, 15);
            this.lTargetSomites.TabIndex = 8;
            this.lTargetSomites.Text = "Target Somite(s)";
            // 
            // StimulusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ddTargetSomites);
            this.Controls.Add(this.lTargetSomites);
            this.Controls.Add(this.ddTargetCells);
            this.Controls.Add(this.lTargetCell);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.eValue2);
            this.Controls.Add(this.lSagittalPosition);
            this.Controls.Add(this.eValue1);
            this.Controls.Add(this.lValue1);
            this.Controls.Add(this.ddSagittalPosition);
            this.Controls.Add(this.lValue2);
            this.Controls.Add(this.ddTargetPool);
            this.Controls.Add(this.lTargetPool);
            this.Controls.Add(this.timeLineControl);
            this.Controls.Add(this.lStimulus);
            this.Controls.Add(this.ddStimulusMode);
            this.Name = "StimulusControl";
            this.Size = new System.Drawing.Size(236, 342);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lStimulus;
        private ComboBox ddStimulusMode;
        private TextBox eValue2;
        private TextBox eValue1;
        private Label lValue1;
        private Label lValue2;
        private TimeLineControl timeLineControl;
        private ComboBox ddTargetPool;
        private Label lTargetPool;
        private Label lSagittalPosition;
        private ComboBox ddSagittalPosition;
        private CheckBox cbActive;
        private Label lTargetCell;
        private ComboBox ddTargetCells;
        private ComboBox ddTargetSomites;
        private Label lTargetSomites;
        private ToolTip toolTip;
    }
}
