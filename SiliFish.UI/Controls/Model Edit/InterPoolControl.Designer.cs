namespace SiliFish.UI.Controls
{
    partial class InterPoolControl
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lSourcePool = new Label();
            lTargetPool = new Label();
            toolTip1 = new ToolTip(components);
            eCoreType = new TextBox();
            eTargetPool = new TextBox();
            eSourcePool = new TextBox();
            lCoreType = new Label();
            tabInterPool = new TabControl();
            tJunctionMatrix = new TabPage();
            dgConductanceMatrix = new DataGridView();
            pTop = new Panel();
            tabInterPool.SuspendLayout();
            tJunctionMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgConductanceMatrix).BeginInit();
            pTop.SuspendLayout();
            SuspendLayout();
            // 
            // lSourcePool
            // 
            lSourcePool.AutoSize = true;
            lSourcePool.Location = new Point(15, 11);
            lSourcePool.Name = "lSourcePool";
            lSourcePool.Size = new Size(70, 15);
            lSourcePool.TabIndex = 4;
            lSourcePool.Text = "Source Pool";
            // 
            // lTargetPool
            // 
            lTargetPool.AutoSize = true;
            lTargetPool.Location = new Point(15, 38);
            lTargetPool.Name = "lTargetPool";
            lTargetPool.Size = new Size(66, 15);
            lTargetPool.TabIndex = 6;
            lTargetPool.Text = "Target Pool";
            // 
            // eCoreType
            // 
            eCoreType.Location = new Point(91, 63);
            eCoreType.Name = "eCoreType";
            eCoreType.ReadOnly = true;
            eCoreType.Size = new Size(245, 23);
            eCoreType.TabIndex = 16;
            // 
            // eTargetPool
            // 
            eTargetPool.Location = new Point(91, 34);
            eTargetPool.Name = "eTargetPool";
            eTargetPool.ReadOnly = true;
            eTargetPool.Size = new Size(245, 23);
            eTargetPool.TabIndex = 14;
            // 
            // eSourcePool
            // 
            eSourcePool.Location = new Point(91, 5);
            eSourcePool.Name = "eSourcePool";
            eSourcePool.ReadOnly = true;
            eSourcePool.Size = new Size(245, 23);
            eSourcePool.TabIndex = 13;
            // 
            // lCoreType
            // 
            lCoreType.AutoSize = true;
            lCoreType.Location = new Point(15, 65);
            lCoreType.Name = "lCoreType";
            lCoreType.Size = new Size(59, 15);
            lCoreType.TabIndex = 6;
            lCoreType.Text = "Core Type";
            // 
            // tabInterPool
            // 
            tabInterPool.Controls.Add(tJunctionMatrix);
            tabInterPool.Dock = DockStyle.Fill;
            tabInterPool.Location = new Point(0, 97);
            tabInterPool.Name = "tabInterPool";
            tabInterPool.SelectedIndex = 0;
            tabInterPool.Size = new Size(510, 462);
            tabInterPool.TabIndex = 0;
            // 
            // tJunctionMatrix
            // 
            tJunctionMatrix.Controls.Add(dgConductanceMatrix);
            tJunctionMatrix.Location = new Point(4, 24);
            tJunctionMatrix.Name = "tJunctionMatrix";
            tJunctionMatrix.Padding = new Padding(3);
            tJunctionMatrix.Size = new Size(502, 434);
            tJunctionMatrix.TabIndex = 2;
            tJunctionMatrix.Text = "Junction Matrix";
            tJunctionMatrix.UseVisualStyleBackColor = true;
            // 
            // dgConductanceMatrix
            // 
            dgConductanceMatrix.AllowUserToAddRows = false;
            dgConductanceMatrix.AllowUserToDeleteRows = false;
            dgConductanceMatrix.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "N4";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgConductanceMatrix.DefaultCellStyle = dataGridViewCellStyle2;
            dgConductanceMatrix.Dock = DockStyle.Fill;
            dgConductanceMatrix.Location = new Point(3, 3);
            dgConductanceMatrix.Name = "dgConductanceMatrix";
            dgConductanceMatrix.ReadOnly = true;
            dgConductanceMatrix.RowHeadersWidth = 100;
            dgConductanceMatrix.Size = new Size(496, 428);
            dgConductanceMatrix.TabIndex = 0;
            // 
            // pTop
            // 
            pTop.Controls.Add(eCoreType);
            pTop.Controls.Add(lSourcePool);
            pTop.Controls.Add(eTargetPool);
            pTop.Controls.Add(lTargetPool);
            pTop.Controls.Add(eSourcePool);
            pTop.Controls.Add(lCoreType);
            pTop.Dock = DockStyle.Top;
            pTop.Location = new Point(0, 0);
            pTop.Name = "pTop";
            pTop.Size = new Size(510, 97);
            pTop.TabIndex = 29;
            // 
            // InterPoolControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            Controls.Add(tabInterPool);
            Controls.Add(pTop);
            Name = "InterPoolControl";
            Size = new Size(510, 559);
            tabInterPool.ResumeLayout(false);
            tJunctionMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgConductanceMatrix).EndInit();
            pTop.ResumeLayout(false);
            pTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lSourcePool;
        private Label lTargetPool;
        private ToolTip toolTip1;
        private TabControl tabInterPool;
        private Label lCoreType;
        private TabPage tJunctionMatrix;
        private DataGridView dgConductanceMatrix;
        private TextBox eTargetPool;
        private TextBox eSourcePool;
        private TextBox eCoreType;
        private Panel pTop;
    }
}
