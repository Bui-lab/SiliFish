namespace SiliFish.UI.Controls
{
    partial class StimulusTemplateControl
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
            this.timeLineControl = new SiliFish.UI.Controls.TimeLineControl();
            this.ddTargetPool = new System.Windows.Forms.ComboBox();
            this.lTargetPool = new System.Windows.Forms.Label();
            this.lSagittalPosition = new System.Windows.Forms.Label();
            this.ddSagittalPosition = new System.Windows.Forms.ComboBox();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.lTargetCells = new System.Windows.Forms.Label();
            this.lTargetSomites = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stimControl = new SiliFish.UI.Controls.StimulusSettingsControl();
            this.lAll = new System.Windows.Forms.Label();
            this.cbAllSomites = new System.Windows.Forms.CheckBox();
            this.cbAllCells = new System.Windows.Forms.CheckBox();
            this.lRange = new System.Windows.Forms.Label();
            this.eTargetSomites = new System.Windows.Forms.TextBox();
            this.eTargetCells = new System.Windows.Forms.TextBox();
            this.pTargetPool = new System.Windows.Forms.Panel();
            this.pActive = new System.Windows.Forms.Panel();
            this.pTargetPool.SuspendLayout();
            this.pActive.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeLineControl
            // 
            this.timeLineControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.timeLineControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLineControl.Location = new System.Drawing.Point(0, 296);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(243, 150);
            this.timeLineControl.TabIndex = 15;
            // 
            // ddTargetPool
            // 
            this.ddTargetPool.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddTargetPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetPool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddTargetPool.FormattingEnabled = true;
            this.ddTargetPool.Location = new System.Drawing.Point(102, 8);
            this.ddTargetPool.Name = "ddTargetPool";
            this.ddTargetPool.Size = new System.Drawing.Size(132, 23);
            this.ddTargetPool.TabIndex = 7;
            // 
            // lTargetPool
            // 
            this.lTargetPool.AutoSize = true;
            this.lTargetPool.Location = new System.Drawing.Point(3, 11);
            this.lTargetPool.Name = "lTargetPool";
            this.lTargetPool.Size = new System.Drawing.Size(66, 15);
            this.lTargetPool.TabIndex = 6;
            this.lTargetPool.Text = "Target Pool";
            // 
            // lSagittalPosition
            // 
            this.lSagittalPosition.AutoSize = true;
            this.lSagittalPosition.Location = new System.Drawing.Point(3, 107);
            this.lSagittalPosition.Name = "lSagittalPosition";
            this.lSagittalPosition.Size = new System.Drawing.Size(92, 15);
            this.lSagittalPosition.TabIndex = 12;
            this.lSagittalPosition.Text = "Sagittal Position";
            // 
            // ddSagittalPosition
            // 
            this.ddSagittalPosition.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddSagittalPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSagittalPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddSagittalPosition.FormattingEnabled = true;
            this.ddSagittalPosition.Location = new System.Drawing.Point(102, 104);
            this.ddSagittalPosition.Name = "ddSagittalPosition";
            this.ddSagittalPosition.Size = new System.Drawing.Size(132, 23);
            this.ddSagittalPosition.TabIndex = 13;
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(13, 6);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(59, 19);
            this.cbActive.TabIndex = 14;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // lTargetCells
            // 
            this.lTargetCells.AutoSize = true;
            this.lTargetCells.Location = new System.Drawing.Point(3, 81);
            this.lTargetCells.Name = "lTargetCells";
            this.lTargetCells.Size = new System.Drawing.Size(75, 15);
            this.lTargetCells.TabIndex = 10;
            this.lTargetCells.Text = "Target Cell(s)";
            // 
            // lTargetSomites
            // 
            this.lTargetSomites.AutoSize = true;
            this.lTargetSomites.Location = new System.Drawing.Point(3, 55);
            this.lTargetSomites.Name = "lTargetSomites";
            this.lTargetSomites.Size = new System.Drawing.Size(92, 15);
            this.lTargetSomites.TabIndex = 8;
            this.lTargetSomites.Text = "Target Somite(s)";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // stimControl
            // 
            this.stimControl.BackColor = System.Drawing.Color.White;
            this.stimControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.stimControl.Location = new System.Drawing.Point(0, 0);
            this.stimControl.Name = "stimControl";
            this.stimControl.Size = new System.Drawing.Size(243, 125);
            this.stimControl.TabIndex = 16;
            // 
            // lAll
            // 
            this.lAll.AutoSize = true;
            this.lAll.Location = new System.Drawing.Point(98, 36);
            this.lAll.Name = "lAll";
            this.lAll.Size = new System.Drawing.Size(21, 15);
            this.lAll.TabIndex = 17;
            this.lAll.Text = "All";
            // 
            // cbAllSomites
            // 
            this.cbAllSomites.AutoSize = true;
            this.cbAllSomites.Location = new System.Drawing.Point(102, 55);
            this.cbAllSomites.Name = "cbAllSomites";
            this.cbAllSomites.Size = new System.Drawing.Size(15, 14);
            this.cbAllSomites.TabIndex = 18;
            this.cbAllSomites.UseVisualStyleBackColor = true;
            this.cbAllSomites.CheckedChanged += new System.EventHandler(this.cbAllSomites_CheckedChanged);
            // 
            // cbAllCells
            // 
            this.cbAllCells.AutoSize = true;
            this.cbAllCells.Location = new System.Drawing.Point(102, 81);
            this.cbAllCells.Name = "cbAllCells";
            this.cbAllCells.Size = new System.Drawing.Size(15, 14);
            this.cbAllCells.TabIndex = 19;
            this.cbAllCells.UseVisualStyleBackColor = true;
            this.cbAllCells.CheckedChanged += new System.EventHandler(this.cbAllCells_CheckedChanged);
            // 
            // lRange
            // 
            this.lRange.AutoSize = true;
            this.lRange.Location = new System.Drawing.Point(123, 36);
            this.lRange.Name = "lRange";
            this.lRange.Size = new System.Drawing.Size(40, 15);
            this.lRange.TabIndex = 20;
            this.lRange.Text = "Range";
            // 
            // eTargetSomites
            // 
            this.eTargetSomites.Location = new System.Drawing.Point(123, 52);
            this.eTargetSomites.Name = "eTargetSomites";
            this.eTargetSomites.ReadOnly = true;
            this.eTargetSomites.Size = new System.Drawing.Size(109, 23);
            this.eTargetSomites.TabIndex = 34;
            this.eTargetSomites.Text = "example: 1, 3-5";
            // 
            // eTargetCells
            // 
            this.eTargetCells.Location = new System.Drawing.Point(123, 77);
            this.eTargetCells.Name = "eTargetCells";
            this.eTargetCells.ReadOnly = true;
            this.eTargetCells.Size = new System.Drawing.Size(109, 23);
            this.eTargetCells.TabIndex = 35;
            this.eTargetCells.Text = "example: 1, 3-5";
            // 
            // pTargetPool
            // 
            this.pTargetPool.Controls.Add(this.lTargetPool);
            this.pTargetPool.Controls.Add(this.ddTargetPool);
            this.pTargetPool.Controls.Add(this.eTargetCells);
            this.pTargetPool.Controls.Add(this.ddSagittalPosition);
            this.pTargetPool.Controls.Add(this.eTargetSomites);
            this.pTargetPool.Controls.Add(this.lSagittalPosition);
            this.pTargetPool.Controls.Add(this.lRange);
            this.pTargetPool.Controls.Add(this.lTargetCells);
            this.pTargetPool.Controls.Add(this.cbAllCells);
            this.pTargetPool.Controls.Add(this.lTargetSomites);
            this.pTargetPool.Controls.Add(this.cbAllSomites);
            this.pTargetPool.Controls.Add(this.lAll);
            this.pTargetPool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTargetPool.Location = new System.Drawing.Point(0, 125);
            this.pTargetPool.Name = "pTargetPool";
            this.pTargetPool.Size = new System.Drawing.Size(243, 139);
            this.pTargetPool.TabIndex = 36;
            // 
            // pActive
            // 
            this.pActive.Controls.Add(this.cbActive);
            this.pActive.Dock = System.Windows.Forms.DockStyle.Top;
            this.pActive.Location = new System.Drawing.Point(0, 264);
            this.pActive.Name = "pActive";
            this.pActive.Size = new System.Drawing.Size(243, 32);
            this.pActive.TabIndex = 37;
            // 
            // StimulusTemplateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.timeLineControl);
            this.Controls.Add(this.pActive);
            this.Controls.Add(this.pTargetPool);
            this.Controls.Add(this.stimControl);
            this.Name = "StimulusTemplateControl";
            this.Size = new System.Drawing.Size(243, 446);
            this.pTargetPool.ResumeLayout(false);
            this.pTargetPool.PerformLayout();
            this.pActive.ResumeLayout(false);
            this.pActive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TimeLineControl timeLineControl;
        private ComboBox ddTargetPool;
        private Label lTargetPool;
        private Label lSagittalPosition;
        private ComboBox ddSagittalPosition;
        private CheckBox cbActive;
        private Label lTargetCells;
        private Label lTargetSomites;
        private ToolTip toolTip;
        private ContextMenuStrip contextMenuStrip1;
        private StimulusSettingsControl stimControl;
        private Label lAll;
        private CheckBox cbAllSomites;
        private CheckBox cbAllCells;
        private Label lRange;
        private TextBox eTargetSomites;
        private TextBox eTargetCells;
        private Panel pTargetPool;
        private Panel pActive;
    }
}
