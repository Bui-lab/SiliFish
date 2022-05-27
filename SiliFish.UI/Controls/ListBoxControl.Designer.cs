namespace SiliFish.UI.Controls
{
    partial class ListBoxControl
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
            this.listBox = new System.Windows.Forms.ListBox();
            this.contextMenuListBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreateCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miSortAlphabetically = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuListBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.ContextMenuStrip = this.contextMenuListBox;
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(0, 0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(213, 262);
            this.listBox.TabIndex = 0;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // contextMenuListBox
            // 
            this.contextMenuListBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddItem,
            this.miDeleteItem,
            this.miCreateCopy,
            this.miSortAlphabetically});
            this.contextMenuListBox.Name = "contextMenuListBox";
            this.contextMenuListBox.Size = new System.Drawing.Size(181, 114);
            this.contextMenuListBox.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuListBox_Opening);
            // 
            // miAddItem
            // 
            this.miAddItem.Name = "miAddItem";
            this.miAddItem.Size = new System.Drawing.Size(180, 22);
            this.miAddItem.Text = "Add Item";
            this.miAddItem.Click += new System.EventHandler(this.miAddItem_Click);
            // 
            // miDeleteItem
            // 
            this.miDeleteItem.Name = "miDeleteItem";
            this.miDeleteItem.Size = new System.Drawing.Size(180, 22);
            this.miDeleteItem.Text = "Delete Item";
            this.miDeleteItem.Click += new System.EventHandler(this.miDeleteItem_Click);
            // 
            // miCreateCopy
            // 
            this.miCreateCopy.Name = "miCreateCopy";
            this.miCreateCopy.Size = new System.Drawing.Size(180, 22);
            this.miCreateCopy.Text = "Create a Copy";
            this.miCreateCopy.Click += new System.EventHandler(this.miCreateCopy_Click);
            // 
            // miSortAlphabetically
            // 
            this.miSortAlphabetically.Name = "miSortAlphabetically";
            this.miSortAlphabetically.Size = new System.Drawing.Size(180, 22);
            this.miSortAlphabetically.Text = "Sort Alphabetically";
            this.miSortAlphabetically.Click += new System.EventHandler(this.miSortAlphabetically_Click);
            // 
            // ListBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBox);
            this.Name = "ListBoxControl";
            this.Size = new System.Drawing.Size(213, 262);
            this.contextMenuListBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listBox;
        private ContextMenuStrip contextMenuListBox;
        private ToolStripMenuItem miAddItem;
        private ToolStripMenuItem miDeleteItem;
        private ToolStripMenuItem miCreateCopy;
        private ToolTip toolTip;
        private ToolStripMenuItem miSortAlphabetically;
    }
}
