namespace SiliFish.UI.Controls
{
    partial class TimeLineControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgTimeLine = new System.Windows.Forms.DataGridView();
            this.cmTimeLine = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiSort = new System.Windows.Forms.ToolStripMenuItem();
            this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepeat = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colRest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepetition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeLine)).BeginInit();
            this.cmTimeLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgTimeLine
            // 
            this.dgTimeLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTimeLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStartTime,
            this.colEndTime,
            this.colRepeat,
            this.colRest,
            this.colRepetition});
            this.dgTimeLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTimeLine.Location = new System.Drawing.Point(0, 0);
            this.dgTimeLine.Name = "dgTimeLine";
            this.dgTimeLine.RowHeadersVisible = false;
            this.dgTimeLine.RowTemplate.Height = 25;
            this.dgTimeLine.Size = new System.Drawing.Size(467, 150);
            this.dgTimeLine.TabIndex = 0;
            this.dgTimeLine.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTimeLine_CellValueChanged);
            // 
            // cmTimeLine
            // 
            this.cmTimeLine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiClearAll,
            this.cmiSort});
            this.cmTimeLine.Name = "cmTimeLine";
            this.cmTimeLine.Size = new System.Drawing.Size(119, 48);
            this.cmTimeLine.Opening += new System.ComponentModel.CancelEventHandler(this.cmTimeLine_Opening);
            // 
            // cmiClearAll
            // 
            this.cmiClearAll.Name = "cmiClearAll";
            this.cmiClearAll.Size = new System.Drawing.Size(118, 22);
            this.cmiClearAll.Text = "Clear All";
            this.cmiClearAll.Click += new System.EventHandler(this.cmiClearAll_Click);
            // 
            // cmiSort
            // 
            this.cmiSort.Name = "cmiSort";
            this.cmiSort.Size = new System.Drawing.Size(118, 22);
            this.cmiSort.Text = "Sort";
            this.cmiSort.Click += new System.EventHandler(this.cmiSort_Click);
            // 
            // colStartTime
            // 
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.colStartTime.DefaultCellStyle = dataGridViewCellStyle1;
            this.colStartTime.HeaderText = "Start (ms)";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.Width = 85;
            // 
            // colEndTime
            // 
            dataGridViewCellStyle2.Format = "N0";
            this.colEndTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.colEndTime.HeaderText = "End (ms)";
            this.colEndTime.Name = "colEndTime";
            this.colEndTime.Width = 80;
            // 
            // colRepeat
            // 
            this.colRepeat.HeaderText = "Repeat";
            this.colRepeat.Name = "colRepeat";
            this.colRepeat.Visible = false;
            // 
            // colRest
            // 
            dataGridViewCellStyle3.Format = "N0";
            this.colRest.DefaultCellStyle = dataGridViewCellStyle3;
            this.colRest.HeaderText = "Rest (ms)";
            this.colRest.Name = "colRest";
            this.colRest.Visible = false;
            // 
            // colRepetition
            // 
            dataGridViewCellStyle4.Format = "N0";
            this.colRepetition.DefaultCellStyle = dataGridViewCellStyle4;
            this.colRepetition.HeaderText = "Repetition";
            this.colRepetition.Name = "colRepetition";
            this.colRepetition.Visible = false;
            // 
            // TimeLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgTimeLine);
            this.Name = "TimeLineControl";
            this.Size = new System.Drawing.Size(467, 150);
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeLine)).EndInit();
            this.cmTimeLine.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgTimeLine;
        private ContextMenuStrip cmTimeLine;
        private ToolStripMenuItem cmiClearAll;
        private ToolStripMenuItem cmiSort;
        private DataGridViewTextBoxColumn colStartTime;
        private DataGridViewTextBoxColumn colEndTime;
        private DataGridViewCheckBoxColumn colRepeat;
        private DataGridViewTextBoxColumn colRest;
        private DataGridViewTextBoxColumn colRepetition;
    }
}
