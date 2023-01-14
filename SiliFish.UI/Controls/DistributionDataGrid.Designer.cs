namespace SiliFish.UI.Controls
{
    partial class DistributionDataGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgDistribution = new System.Windows.Forms.DataGridView();
            this.colField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUniqueValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistribution = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistEditLink = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgDistribution)).BeginInit();
            this.SuspendLayout();
            // 
            // dgDistribution
            // 
            this.dgDistribution.AllowUserToAddRows = false;
            this.dgDistribution.AllowUserToDeleteRows = false;
            this.dgDistribution.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgDistribution.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDistribution.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colField,
            this.colUniqueValue,
            this.colRange,
            this.colDistribution,
            this.colDistDetails,
            this.colDistEditLink});
            this.dgDistribution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDistribution.Location = new System.Drawing.Point(0, 0);
            this.dgDistribution.Name = "dgDistribution";
            this.dgDistribution.RowHeadersVisible = false;
            this.dgDistribution.RowTemplate.Height = 25;
            this.dgDistribution.Size = new System.Drawing.Size(100, 100);
            this.dgDistribution.TabIndex = 10;
            this.dgDistribution.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDynamics_CellClick);
            this.dgDistribution.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDynamics_CellContentClick);
            this.dgDistribution.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDynamics_CellEndEdit);
            // 
            // colField
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.colField.DefaultCellStyle = dataGridViewCellStyle1;
            this.colField.HeaderText = "Field";
            this.colField.Name = "colField";
            this.colField.ReadOnly = true;
            this.colField.Width = 57;
            // 
            // colUniqueValue
            // 
            this.colUniqueValue.HeaderText = "Unique Value";
            this.colUniqueValue.Name = "colUniqueValue";
            this.colUniqueValue.Width = 101;
            // 
            // colRange
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colRange.DefaultCellStyle = dataGridViewCellStyle2;
            this.colRange.HeaderText = "Range";
            this.colRange.Name = "colRange";
            this.colRange.ReadOnly = true;
            this.colRange.Width = 65;
            // 
            // colDistribution
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colDistribution.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDistribution.HeaderText = "Distribution";
            this.colDistribution.Name = "colDistribution";
            this.colDistribution.ReadOnly = true;
            this.colDistribution.Width = 94;
            // 
            // colDistDetails
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colDistDetails.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDistDetails.HeaderText = "Details";
            this.colDistDetails.Name = "colDistDetails";
            this.colDistDetails.ReadOnly = true;
            this.colDistDetails.Width = 67;
            // 
            // colDistEditLink
            // 
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            this.colDistEditLink.DefaultCellStyle = dataGridViewCellStyle5;
            this.colDistEditLink.HeaderText = "";
            this.colDistEditLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.colDistEditLink.Name = "colDistEditLink";
            this.colDistEditLink.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDistEditLink.Text = "Edit";
            this.colDistEditLink.ToolTipText = "Edit distribution";
            this.colDistEditLink.UseColumnTextForLinkValue = true;
            this.colDistEditLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.colDistEditLink.Width = 5;
            // 
            // DistributionDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.dgDistribution);
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "DistributionDataGrid";
            this.Size = new System.Drawing.Size(100, 100);
            ((System.ComponentModel.ISupportInitialize)(this.dgDistribution)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgDistribution;
        private DataGridViewTextBoxColumn colField;
        private DataGridViewTextBoxColumn colUniqueValue;
        private DataGridViewTextBoxColumn colRange;
        private DataGridViewTextBoxColumn colDistribution;
        private DataGridViewTextBoxColumn colDistDetails;
        private DataGridViewLinkColumn colDistEditLink;
    }
}
