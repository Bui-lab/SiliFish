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
            this.dgDynamics = new System.Windows.Forms.DataGridView();
            this.colField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDist = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgDynamics)).BeginInit();
            this.SuspendLayout();
            // 
            // dgDynamics
            // 
            this.dgDynamics.AllowUserToAddRows = false;
            this.dgDynamics.AllowUserToDeleteRows = false;
            this.dgDynamics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDynamics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colField,
            this.colValue,
            this.colDist});
            this.dgDynamics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDynamics.Location = new System.Drawing.Point(0, 0);
            this.dgDynamics.Name = "dgDynamics";
            this.dgDynamics.RowHeadersVisible = false;
            this.dgDynamics.RowTemplate.Height = 25;
            this.dgDynamics.Size = new System.Drawing.Size(100, 100);
            this.dgDynamics.TabIndex = 10;
            this.dgDynamics.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDynamics_CellContentClick);
            // 
            // colField
            // 
            this.colField.HeaderText = "Field";
            this.colField.Name = "colField";
            this.colField.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            // 
            // colDist
            // 
            this.colDist.HeaderText = "Distr.";
            this.colDist.Name = "colDist";
            this.colDist.Width = 40;
            // 
            // DistributionDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.dgDynamics);
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "DistributionDataGrid";
            this.Size = new System.Drawing.Size(100, 100);
            ((System.ComponentModel.ISupportInitialize)(this.dgDynamics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgDynamics;
        private DataGridViewTextBoxColumn colField;
        private DataGridViewTextBoxColumn colValue;
        private DataGridViewButtonColumn colDist;
    }
}
