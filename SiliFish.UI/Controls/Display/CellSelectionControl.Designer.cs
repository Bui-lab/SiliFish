namespace SiliFish.UI.Controls.Display
{
    partial class CellSelectionControl
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
            lSagittal = new Label();
            ddPools = new ComboBox();
            lPool = new Label();
            eSomiteSelection = new NumericUpDown();
            lSomites = new Label();
            ddSomiteSelection = new ComboBox();
            eCellSelection = new NumericUpDown();
            lCells = new Label();
            ddCellSelection = new ComboBox();
            ddSagittal = new ComboBox();
            cbCombinePools = new CheckBox();
            cbCombineCells = new CheckBox();
            cbCombineSomites = new CheckBox();
            pCombine = new Panel();
            pMain = new Panel();
            ((System.ComponentModel.ISupportInitialize)eSomiteSelection).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eCellSelection).BeginInit();
            pCombine.SuspendLayout();
            pMain.SuspendLayout();
            SuspendLayout();
            // 
            // lSagittal
            // 
            lSagittal.AutoSize = true;
            lSagittal.Location = new Point(3, 9);
            lSagittal.Name = "lSagittal";
            lSagittal.Size = new Size(46, 15);
            lSagittal.TabIndex = 34;
            lSagittal.Text = "Sagittal";
            // 
            // ddPools
            // 
            ddPools.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddPools.BackColor = Color.White;
            ddPools.DropDownStyle = ComboBoxStyle.DropDownList;
            ddPools.FlatStyle = FlatStyle.Flat;
            ddPools.FormattingEnabled = true;
            ddPools.Location = new Point(66, 33);
            ddPools.Name = "ddPools";
            ddPools.Size = new Size(165, 23);
            ddPools.TabIndex = 36;
            ddPools.SelectedIndexChanged += ddPools_SelectedIndexChanged;
            // 
            // lPool
            // 
            lPool.AutoSize = true;
            lPool.Location = new Point(3, 35);
            lPool.Name = "lPool";
            lPool.Size = new Size(31, 15);
            lPool.TabIndex = 26;
            lPool.Text = "Pool";
            // 
            // eSomiteSelection
            // 
            eSomiteSelection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            eSomiteSelection.Location = new Point(184, 59);
            eSomiteSelection.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            eSomiteSelection.Name = "eSomiteSelection";
            eSomiteSelection.Size = new Size(47, 23);
            eSomiteSelection.TabIndex = 39;
            eSomiteSelection.Value = new decimal(new int[] { 1, 0, 0, 0 });
            eSomiteSelection.ValueChanged += eSomiteSelection_ValueChanged;
            eSomiteSelection.EnabledChanged += eSomiteSelection_EnabledChanged;
            // 
            // lSomites
            // 
            lSomites.AutoSize = true;
            lSomites.Location = new Point(3, 61);
            lSomites.Name = "lSomites";
            lSomites.Size = new Size(49, 15);
            lSomites.TabIndex = 42;
            lSomites.Text = "Somites";
            // 
            // ddSomiteSelection
            // 
            ddSomiteSelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddSomiteSelection.BackColor = Color.White;
            ddSomiteSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSomiteSelection.FlatStyle = FlatStyle.Flat;
            ddSomiteSelection.FormattingEnabled = true;
            ddSomiteSelection.Location = new Point(66, 59);
            ddSomiteSelection.Name = "ddSomiteSelection";
            ddSomiteSelection.Size = new Size(116, 23);
            ddSomiteSelection.TabIndex = 37;
            ddSomiteSelection.SelectedIndexChanged += ddSomiteSelection_SelectedIndexChanged;
            // 
            // eCellSelection
            // 
            eCellSelection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            eCellSelection.Location = new Point(184, 85);
            eCellSelection.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            eCellSelection.Name = "eCellSelection";
            eCellSelection.Size = new Size(47, 23);
            eCellSelection.TabIndex = 44;
            eCellSelection.Value = new decimal(new int[] { 1, 0, 0, 0 });
            eCellSelection.ValueChanged += eCellSelection_ValueChanged;
            // 
            // lCells
            // 
            lCells.AutoSize = true;
            lCells.Location = new Point(3, 87);
            lCells.Name = "lCells";
            lCells.Size = new Size(32, 15);
            lCells.TabIndex = 45;
            lCells.Text = "Cells";
            // 
            // ddCellSelection
            // 
            ddCellSelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCellSelection.BackColor = Color.White;
            ddCellSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCellSelection.FlatStyle = FlatStyle.Flat;
            ddCellSelection.FormattingEnabled = true;
            ddCellSelection.Location = new Point(66, 85);
            ddCellSelection.Name = "ddCellSelection";
            ddCellSelection.Size = new Size(116, 23);
            ddCellSelection.TabIndex = 41;
            ddCellSelection.SelectedIndexChanged += ddCellSelection_SelectedIndexChanged;
            // 
            // ddSagittal
            // 
            ddSagittal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddSagittal.BackColor = Color.White;
            ddSagittal.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSagittal.FlatStyle = FlatStyle.Flat;
            ddSagittal.FormattingEnabled = true;
            ddSagittal.Location = new Point(66, 7);
            ddSagittal.Name = "ddSagittal";
            ddSagittal.Size = new Size(165, 23);
            ddSagittal.TabIndex = 35;
            ddSagittal.SelectedIndexChanged += ddSagittal_SelectedIndexChanged;
            // 
            // cbCombinePools
            // 
            cbCombinePools.AutoSize = true;
            cbCombinePools.Location = new Point(4, 37);
            cbCombinePools.Name = "cbCombinePools";
            cbCombinePools.Size = new Size(75, 19);
            cbCombinePools.TabIndex = 66;
            cbCombinePools.Text = "Combine";
            cbCombinePools.UseVisualStyleBackColor = true;
            cbCombinePools.CheckedChanged += cbCombinePools_CheckedChanged;
            // 
            // cbCombineCells
            // 
            cbCombineCells.AutoSize = true;
            cbCombineCells.Checked = true;
            cbCombineCells.CheckState = CheckState.Checked;
            cbCombineCells.Location = new Point(4, 87);
            cbCombineCells.Name = "cbCombineCells";
            cbCombineCells.Size = new Size(75, 19);
            cbCombineCells.TabIndex = 65;
            cbCombineCells.Text = "Combine";
            cbCombineCells.UseVisualStyleBackColor = true;
            cbCombineCells.CheckedChanged += cbCombineCells_CheckedChanged;
            // 
            // cbCombineSomites
            // 
            cbCombineSomites.AutoSize = true;
            cbCombineSomites.Checked = true;
            cbCombineSomites.CheckState = CheckState.Checked;
            cbCombineSomites.Location = new Point(4, 62);
            cbCombineSomites.Name = "cbCombineSomites";
            cbCombineSomites.Size = new Size(75, 19);
            cbCombineSomites.TabIndex = 64;
            cbCombineSomites.Text = "Combine";
            cbCombineSomites.UseVisualStyleBackColor = true;
            cbCombineSomites.CheckedChanged += cbCombineSomites_CheckedChanged;
            // 
            // pCombine
            // 
            pCombine.Controls.Add(cbCombinePools);
            pCombine.Controls.Add(cbCombineSomites);
            pCombine.Controls.Add(cbCombineCells);
            pCombine.Dock = DockStyle.Right;
            pCombine.Location = new Point(240, 0);
            pCombine.Name = "pCombine";
            pCombine.Size = new Size(86, 116);
            pCombine.TabIndex = 67;
            // 
            // pMain
            // 
            pMain.Controls.Add(lSagittal);
            pMain.Controls.Add(eCellSelection);
            pMain.Controls.Add(ddSomiteSelection);
            pMain.Controls.Add(ddPools);
            pMain.Controls.Add(lCells);
            pMain.Controls.Add(lPool);
            pMain.Controls.Add(lSomites);
            pMain.Controls.Add(ddSagittal);
            pMain.Controls.Add(ddCellSelection);
            pMain.Controls.Add(eSomiteSelection);
            pMain.Dock = DockStyle.Fill;
            pMain.Location = new Point(0, 0);
            pMain.Name = "pMain";
            pMain.Size = new Size(240, 116);
            pMain.TabIndex = 68;
            pMain.EnabledChanged += pMain_EnabledChanged;
            // 
            // CellSelectionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pMain);
            Controls.Add(pCombine);
            Name = "CellSelectionControl";
            Size = new Size(326, 116);
            EnabledChanged += CellSelectionControl_EnabledChanged;
            ((System.ComponentModel.ISupportInitialize)eSomiteSelection).EndInit();
            ((System.ComponentModel.ISupportInitialize)eCellSelection).EndInit();
            pCombine.ResumeLayout(false);
            pCombine.PerformLayout();
            pMain.ResumeLayout(false);
            pMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lSagittal;
        private ComboBox ddPools;
        private Label lPool;
        private NumericUpDown eSomiteSelection;
        private Label lSomites;
        private ComboBox ddSomiteSelection;
        private NumericUpDown eCellSelection;
        private Label lCells;
        private ComboBox ddCellSelection;
        private ComboBox ddSagittal;
        private CheckBox cbCombinePools;
        private CheckBox cbCombineCells;
        private CheckBox cbCombineSomites;
        private Panel pCombine;
        private Panel pMain;
    }
}
