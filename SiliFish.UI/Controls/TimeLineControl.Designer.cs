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
            this.pTimelineTop = new System.Windows.Forms.Panel();
            this.linkClearTimeline = new System.Windows.Forms.LinkLabel();
            this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCyclic = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeLine)).BeginInit();
            this.cmTimeLine.SuspendLayout();
            this.pTimelineTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgTimeLine
            // 
            this.dgTimeLine.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgTimeLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTimeLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStartTime,
            this.colEndTime,
            this.colCyclic,
            this.colActive,
            this.colRest});
            this.dgTimeLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTimeLine.Location = new System.Drawing.Point(0, 30);
            this.dgTimeLine.Name = "dgTimeLine";
            this.dgTimeLine.RowHeadersVisible = false;
            this.dgTimeLine.RowTemplate.Height = 25;
            this.dgTimeLine.Size = new System.Drawing.Size(467, 120);
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
            // pTimelineTop
            // 
            this.pTimelineTop.Controls.Add(this.linkClearTimeline);
            this.pTimelineTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTimelineTop.Location = new System.Drawing.Point(0, 0);
            this.pTimelineTop.Name = "pTimelineTop";
            this.pTimelineTop.Size = new System.Drawing.Size(467, 30);
            this.pTimelineTop.TabIndex = 1;
            // 
            // linkClearTimeline
            // 
            this.linkClearTimeline.AutoSize = true;
            this.linkClearTimeline.Location = new System.Drawing.Point(6, 9);
            this.linkClearTimeline.Name = "linkClearTimeline";
            this.linkClearTimeline.Size = new System.Drawing.Size(82, 15);
            this.linkClearTimeline.TabIndex = 0;
            this.linkClearTimeline.TabStop = true;
            this.linkClearTimeline.Text = "Clear Timeline";
            this.linkClearTimeline.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClearTimeline_LinkClicked);
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
            // colCyclic
            // 
            this.colCyclic.HeaderText = "Cyclic";
            this.colCyclic.Name = "colCyclic";
            this.colCyclic.Visible = false;
            this.colCyclic.Width = 44;
            // 
            // colActive
            // 
            dataGridViewCellStyle3.Format = "N0";
            this.colActive.DefaultCellStyle = dataGridViewCellStyle3;
            this.colActive.HeaderText = "Active (ms)";
            this.colActive.Name = "colActive";
            this.colActive.ReadOnly = true;
            this.colActive.Visible = false;
            // 
            // colRest
            // 
            dataGridViewCellStyle4.Format = "N0";
            this.colRest.DefaultCellStyle = dataGridViewCellStyle4;
            this.colRest.HeaderText = "Rest (ms)";
            this.colRest.Name = "colRest";
            this.colRest.ReadOnly = true;
            this.colRest.Visible = false;
            // 
            // TimeLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgTimeLine);
            this.Controls.Add(this.pTimelineTop);
            this.Name = "TimeLineControl";
            this.Size = new System.Drawing.Size(467, 150);
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeLine)).EndInit();
            this.cmTimeLine.ResumeLayout(false);
            this.pTimelineTop.ResumeLayout(false);
            this.pTimelineTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgTimeLine;
        private ContextMenuStrip cmTimeLine;
        private ToolStripMenuItem cmiClearAll;
        private ToolStripMenuItem cmiSort;
        private Panel pTimelineTop;
        private LinkLabel linkClearTimeline;
        private DataGridViewTextBoxColumn colStartTime;
        private DataGridViewTextBoxColumn colEndTime;
        private DataGridViewCheckBoxColumn colCyclic;
        private DataGridViewTextBoxColumn colActive;
        private DataGridViewTextBoxColumn colRest;
    }
}
