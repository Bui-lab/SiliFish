﻿namespace SiliFish.UI.Controls
{
    partial class AppliedStimulusControl
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
            this.lTargetCell = new System.Windows.Forms.Label();
            this.lTargetSomites = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stimControl = new SiliFish.UI.Controls.StimulusControl();
            this.lAll = new System.Windows.Forms.Label();
            this.cbAllSomites = new System.Windows.Forms.CheckBox();
            this.cbAllCells = new System.Windows.Forms.CheckBox();
            this.lRange = new System.Windows.Forms.Label();
            this.eTargetSomites = new System.Windows.Forms.TextBox();
            this.eTargetCells = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // timeLineControl
            // 
            this.timeLineControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.timeLineControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.timeLineControl.Location = new System.Drawing.Point(0, 270);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(236, 117);
            this.timeLineControl.TabIndex = 15;
            // 
            // ddTargetPool
            // 
            this.ddTargetPool.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddTargetPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTargetPool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddTargetPool.FormattingEnabled = true;
            this.ddTargetPool.Location = new System.Drawing.Point(101, 91);
            this.ddTargetPool.Name = "ddTargetPool";
            this.ddTargetPool.Size = new System.Drawing.Size(121, 23);
            this.ddTargetPool.TabIndex = 7;
            this.ddTargetPool.SelectedIndexChanged += new System.EventHandler(this.ddTargetPool_SelectedIndexChanged);
            // 
            // lTargetPool
            // 
            this.lTargetPool.AutoSize = true;
            this.lTargetPool.Location = new System.Drawing.Point(3, 94);
            this.lTargetPool.Name = "lTargetPool";
            this.lTargetPool.Size = new System.Drawing.Size(66, 15);
            this.lTargetPool.TabIndex = 6;
            this.lTargetPool.Text = "Target Pool";
            // 
            // lSagittalPosition
            // 
            this.lSagittalPosition.AutoSize = true;
            this.lSagittalPosition.Location = new System.Drawing.Point(3, 203);
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
            this.ddSagittalPosition.Items.AddRange(new object[] {
            "Left/Right",
            "Left",
            "Right"});
            this.ddSagittalPosition.Location = new System.Drawing.Point(101, 200);
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
            this.cbActive.Location = new System.Drawing.Point(163, 231);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(59, 19);
            this.cbActive.TabIndex = 14;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // lTargetCell
            // 
            this.lTargetCell.AutoSize = true;
            this.lTargetCell.Location = new System.Drawing.Point(3, 177);
            this.lTargetCell.Name = "lTargetCell";
            this.lTargetCell.Size = new System.Drawing.Size(75, 15);
            this.lTargetCell.TabIndex = 10;
            this.lTargetCell.Text = "Target Cell(s)";
            // 
            // lTargetSomites
            // 
            this.lTargetSomites.AutoSize = true;
            this.lTargetSomites.Location = new System.Drawing.Point(3, 151);
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
            this.stimControl.Size = new System.Drawing.Size(236, 90);
            this.stimControl.TabIndex = 16;
            // 
            // lAll
            // 
            this.lAll.AutoSize = true;
            this.lAll.Location = new System.Drawing.Point(98, 125);
            this.lAll.Name = "lAll";
            this.lAll.Size = new System.Drawing.Size(21, 15);
            this.lAll.TabIndex = 17;
            this.lAll.Text = "All";
            // 
            // cbAllSomites
            // 
            this.cbAllSomites.AutoSize = true;
            this.cbAllSomites.Location = new System.Drawing.Point(101, 152);
            this.cbAllSomites.Name = "cbAllSomites";
            this.cbAllSomites.Size = new System.Drawing.Size(15, 14);
            this.cbAllSomites.TabIndex = 18;
            this.cbAllSomites.UseVisualStyleBackColor = true;
            this.cbAllSomites.CheckedChanged += new System.EventHandler(this.cbAllSomites_CheckedChanged);
            // 
            // cbAllCells
            // 
            this.cbAllCells.AutoSize = true;
            this.cbAllCells.Location = new System.Drawing.Point(101, 177);
            this.cbAllCells.Name = "cbAllCells";
            this.cbAllCells.Size = new System.Drawing.Size(15, 14);
            this.cbAllCells.TabIndex = 19;
            this.cbAllCells.UseVisualStyleBackColor = true;
            this.cbAllCells.CheckedChanged += new System.EventHandler(this.cbAllCells_CheckedChanged);
            // 
            // lRange
            // 
            this.lRange.AutoSize = true;
            this.lRange.Location = new System.Drawing.Point(145, 125);
            this.lRange.Name = "lRange";
            this.lRange.Size = new System.Drawing.Size(40, 15);
            this.lRange.TabIndex = 20;
            this.lRange.Text = "Range";
            // 
            // eTargetSomites
            // 
            this.eTargetSomites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eTargetSomites.Location = new System.Drawing.Point(122, 148);
            this.eTargetSomites.Name = "eTargetSomites";
            this.eTargetSomites.ReadOnly = true;
            this.eTargetSomites.Size = new System.Drawing.Size(100, 23);
            this.eTargetSomites.TabIndex = 34;
            this.eTargetSomites.Text = "example: 1-5";
            // 
            // eTargetCells
            // 
            this.eTargetCells.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eTargetCells.Location = new System.Drawing.Point(122, 173);
            this.eTargetCells.Name = "eTargetCells";
            this.eTargetCells.ReadOnly = true;
            this.eTargetCells.Size = new System.Drawing.Size(100, 23);
            this.eTargetCells.TabIndex = 35;
            this.eTargetCells.Text = "example: 1-5";
            // 
            // AppliedStimulusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.eTargetCells);
            this.Controls.Add(this.eTargetSomites);
            this.Controls.Add(this.lRange);
            this.Controls.Add(this.cbAllCells);
            this.Controls.Add(this.cbAllSomites);
            this.Controls.Add(this.lAll);
            this.Controls.Add(this.stimControl);
            this.Controls.Add(this.lTargetSomites);
            this.Controls.Add(this.lTargetCell);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.lSagittalPosition);
            this.Controls.Add(this.ddSagittalPosition);
            this.Controls.Add(this.ddTargetPool);
            this.Controls.Add(this.lTargetPool);
            this.Controls.Add(this.timeLineControl);
            this.Name = "AppliedStimulusControl";
            this.Size = new System.Drawing.Size(236, 387);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TimeLineControl timeLineControl;
        private ComboBox ddTargetPool;
        private Label lTargetPool;
        private Label lSagittalPosition;
        private ComboBox ddSagittalPosition;
        private CheckBox cbActive;
        private Label lTargetCell;
        private Label lTargetSomites;
        private ToolTip toolTip;
        private ContextMenuStrip contextMenuStrip1;
        private StimulusControl stimControl;
        private Label lAll;
        private CheckBox cbAllSomites;
        private CheckBox cbAllCells;
        private Label lRange;
        private TextBox eTargetSomites;
        private TextBox eTargetCells;
    }
}
