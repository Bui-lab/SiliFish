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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgTimeLine = new System.Windows.Forms.DataGridView();
            this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeLine)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTimeLine
            // 
            this.dgTimeLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTimeLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStartTime,
            this.colEndTime});
            this.dgTimeLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTimeLine.Location = new System.Drawing.Point(0, 0);
            this.dgTimeLine.Name = "dgTimeLine";
            this.dgTimeLine.RowHeadersVisible = false;
            this.dgTimeLine.RowTemplate.Height = 25;
            this.dgTimeLine.Size = new System.Drawing.Size(150, 150);
            this.dgTimeLine.TabIndex = 0;
            this.dgTimeLine.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTimeLine_CellValueChanged);
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
            // TimeLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgTimeLine);
            this.Name = "TimeLineControl";
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgTimeLine;
        private DataGridViewTextBoxColumn colStartTime;
        private DataGridViewTextBoxColumn colEndTime;
    }
}
