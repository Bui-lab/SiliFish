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
            components = new System.ComponentModel.Container();
            listBox = new ListBox();
            contextMenuListBox = new ContextMenuStrip(components);
            miEditItem = new ToolStripMenuItem();
            miAddItem = new ToolStripMenuItem();
            miDeleteItem = new ToolStripMenuItem();
            miCreateCopy = new ToolStripMenuItem();
            sepActive = new ToolStripSeparator();
            miActivate = new ToolStripMenuItem();
            miDeactivate = new ToolStripMenuItem();
            miActivateAll = new ToolStripMenuItem();
            miDeactivateAll = new ToolStripMenuItem();
            sepShow = new ToolStripSeparator();
            miHideInactive = new ToolStripMenuItem();
            miShowAll = new ToolStripMenuItem();
            sepSort = new ToolStripSeparator();
            miSortAlphabetically = new ToolStripMenuItem();
            splitExport = new ToolStripSeparator();
            miExport = new ToolStripMenuItem();
            miImport = new ToolStripMenuItem();
            sepPlot = new ToolStripSeparator();
            miPlot = new ToolStripMenuItem();
            miHighlight = new ToolStripMenuItem();
            miHighlightSelected = new ToolStripMenuItem();
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
            listBox.Enter += listBox_Enter;
            listBox.KeyDown += listBox_KeyDown;
            listBox.Leave += listBox_Leave;
            // 
            // contextMenuListBox
            // 
            contextMenuListBox.Items.AddRange(new ToolStripItem[] { miEditItem, miAddItem, miDeleteItem, miCreateCopy, sepActive, miActivate, miDeactivate, miActivateAll, miDeactivateAll, sepShow, miHideInactive, miShowAll, sepSort, miSortAlphabetically, splitExport, miExport, miImport, sepPlot, miPlot, miHighlight, miHighlightSelected });
            contextMenuListBox.Name = "contextMenuListBox";
            contextMenuListBox.Size = new Size(174, 386);
            contextMenuListBox.Opening += contextMenuListBox_Opening;
            // 
            // miEditItem
            // 
            miEditItem.Name = "miEditItem";
            miEditItem.Size = new Size(173, 22);
            miEditItem.Text = "Edit Item";
            miEditItem.Click += listBox_DoubleClick;
            // 
            // miAddItem
            // 
            miAddItem.Name = "miAddItem";
            miAddItem.Size = new Size(173, 22);
            miAddItem.Text = "Add Item";
            miAddItem.Click += miAddItem_Click;
            // 
            // miDeleteItem
            // 
            miDeleteItem.Name = "miDeleteItem";
            miDeleteItem.Size = new Size(173, 22);
            miDeleteItem.Text = "Delete Item";
            miDeleteItem.Click += miDeleteItem_Click;
            // 
            // miCreateCopy
            // 
            miCreateCopy.Name = "miCreateCopy";
            miCreateCopy.Size = new Size(173, 22);
            miCreateCopy.Text = "Create a Copy";
            miCreateCopy.Click += miCreateCopy_Click;
            // 
            // sepActive
            // 
            sepActive.Name = "sepActive";
            sepActive.Size = new Size(170, 6);
            // 
            // miActivate
            // 
            miActivate.Name = "miActivate";
            miActivate.Size = new Size(173, 22);
            miActivate.Text = "Activate";
            miActivate.Click += miActivate_Click;
            // 
            // miDeactivate
            // 
            miDeactivate.Name = "miDeactivate";
            miDeactivate.Size = new Size(173, 22);
            miDeactivate.Text = "Deactivate";
            miDeactivate.Click += miDeactivate_Click;
            // 
            // miActivateAll
            // 
            miActivateAll.Name = "miActivateAll";
            miActivateAll.Size = new Size(173, 22);
            miActivateAll.Text = "Activate All";
            miActivateAll.Click += miActivateAll_Click;
            // 
            // miDeactivateAll
            // 
            miDeactivateAll.Name = "miDeactivateAll";
            miDeactivateAll.Size = new Size(173, 22);
            miDeactivateAll.Text = "Deactivate All";
            miDeactivateAll.Click += miDeactivateAll_Click;
            // 
            // sepShow
            // 
            sepShow.Name = "sepShow";
            sepShow.Size = new Size(170, 6);
            // 
            // miHideInactive
            // 
            miHideInactive.Name = "miHideInactive";
            miHideInactive.Size = new Size(173, 22);
            miHideInactive.Text = "Hide Inactive";
            miHideInactive.Click += miHideInactive_Click;
            // 
            // miShowAll
            // 
            miShowAll.Name = "miShowAll";
            miShowAll.Size = new Size(173, 22);
            miShowAll.Text = "Show All";
            miShowAll.Click += miShowAll_Click;
            // 
            // sepSort
            // 
            sepSort.Name = "sepSort";
            sepSort.Size = new Size(170, 6);
            // 
            // miSortAlphabetically
            // 
            miSortAlphabetically.Name = "miSortAlphabetically";
            miSortAlphabetically.Size = new Size(173, 22);
            miSortAlphabetically.Text = "Sort Alphabetically";
            miSortAlphabetically.Click += miSortAlphabetically_Click;
            // 
            // splitExport
            // 
            splitExport.Name = "splitExport";
            splitExport.Size = new Size(170, 6);
            // 
            // miExport
            // 
            miExport.Enabled = false;
            miExport.Name = "miExport";
            miExport.Size = new Size(173, 22);
            miExport.Text = "Export";
            miExport.Click += miExport_Click;
            // 
            // miImport
            // 
            miImport.Enabled = false;
            miImport.Name = "miImport";
            miImport.Size = new Size(173, 22);
            miImport.Text = "Import";
            miImport.Click += miImport_Click;
            // 
            // sepPlot
            // 
            sepPlot.Name = "sepPlot";
            sepPlot.Size = new Size(170, 6);
            // 
            // miPlot
            // 
            miPlot.Name = "miPlot";
            miPlot.Size = new Size(173, 22);
            miPlot.Text = "Plot";
            miPlot.Click += miPlot_Click;
            // 
            // miHighlight
            // 
            miHighlight.Name = "miHighlight";
            miHighlight.Size = new Size(173, 22);
            miHighlight.Text = "Highlight";
            miHighlight.Visible = false;
            miHighlight.Click += miHighlight_Click;
            // 
            // miHighlightSelected
            // 
            miHighlightSelected.Name = "miHighlightSelected";
            miHighlightSelected.Size = new Size(173, 22);
            miHighlightSelected.Text = "Highlight Selected";
            miHighlightSelected.Visible = false;
            miHighlightSelected.Click += miHighlightSelected_Click;
            // 
            // ListBoxControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(listBox);
            Name = "ListBoxControl";
            Size = new Size(213, 262);
            contextMenuListBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox;
        private ContextMenuStrip contextMenuListBox;
        private ToolStripMenuItem miAddItem;
        private ToolStripMenuItem miDeleteItem;
        private ToolStripMenuItem miCreateCopy;
        private ToolTip toolTip;
        private ToolStripMenuItem miSortAlphabetically;
        private ToolStripMenuItem miActivate;
        private ToolStripMenuItem miDeactivate;
        private ToolStripSeparator sepSort;
        private ToolStripSeparator sepActive;
        private ToolStripMenuItem miHideInactive;
        private ToolStripMenuItem miShowAll;
        private ToolStripMenuItem miActivateAll;
        private ToolStripMenuItem miDeactivateAll;
        private ToolStripSeparator sepShow;
        private ToolStripMenuItem miEditItem;
        private ToolStripSeparator sepPlot;
        private ToolStripMenuItem miPlot;
        private ToolStripMenuItem miExport;
        private ToolStripMenuItem miImport;
        private ToolStripSeparator splitExport;
        private ToolStripMenuItem miHighlight;
        private ToolStripMenuItem miHighlightSelected;
    }
}
