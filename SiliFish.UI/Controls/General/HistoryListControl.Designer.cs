namespace SiliFish.UI.Controls
{
    partial class HistoryListControl
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
            listBox = new ListBox();
            contextMenuListBox = new ContextMenuStrip(components);
            miDeleteItem = new ToolStripMenuItem();
            miClearAll = new ToolStripMenuItem();
            miExport = new ToolStripMenuItem();
            miImport = new ToolStripMenuItem();
            toolTip = new ToolTip(components);
            contextMenuListBox.SuspendLayout();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.ContextMenuStrip = contextMenuListBox;
            listBox.Dock = DockStyle.Fill;
            listBox.FormattingEnabled = true;
            listBox.IntegralHeight = false;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(0, 0);
            listBox.Name = "listBox";
            listBox.Size = new Size(213, 262);
            listBox.TabIndex = 0;
            listBox.MouseClick += listBox_MouseClick;
            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            listBox.DoubleClick += listBox_DoubleClick;
            listBox.KeyDown += listBox_KeyDown;
            // 
            // contextMenuListBox
            // 
            contextMenuListBox.Items.AddRange(new ToolStripItem[] { miDeleteItem, miClearAll, miExport, miImport });
            contextMenuListBox.Name = "contextMenuListBox";
            contextMenuListBox.Size = new Size(135, 92);
            contextMenuListBox.Opening += contextMenuListBox_Opening;
            // 
            // miDeleteItem
            // 
            miDeleteItem.Name = "miDeleteItem";
            miDeleteItem.Size = new Size(134, 22);
            miDeleteItem.Text = "Delete Item";
            miDeleteItem.Click += miDeleteItem_Click;
            // 
            // miClearAll
            // 
            miClearAll.Name = "miClearAll";
            miClearAll.Size = new Size(134, 22);
            miClearAll.Text = "Clear All";
            miClearAll.Click += miClearAll_Click;
            // 
            // miExport
            // 
            miExport.Name = "miExport";
            miExport.Size = new Size(134, 22);
            miExport.Text = "Export";
            miExport.Click += miExport_Click;
            // 
            // miImport
            // 
            miImport.Name = "miImport";
            miImport.Size = new Size(134, 22);
            miImport.Text = "Import";
            miImport.Click += miImport_Click;
            // 
            // HistoryListControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(listBox);
            Name = "HistoryListControl";
            Size = new Size(213, 262);
            contextMenuListBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox;
        private ContextMenuStrip contextMenuListBox;
        private ToolStripMenuItem miDeleteItem;
        private ToolTip toolTip;
        private ToolStripMenuItem miClearAll;
        private ToolStripMenuItem miExport;
        private ToolStripMenuItem miImport;
    }
}
