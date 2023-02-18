﻿namespace SiliFish.UI.Controls
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
            this.miEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreateCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.sepActive = new System.Windows.Forms.ToolStripSeparator();
            this.miActivate = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeactivate = new System.Windows.Forms.ToolStripMenuItem();
            this.miActivateAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeactivateAll = new System.Windows.Forms.ToolStripMenuItem();
            this.sepShow = new System.Windows.Forms.ToolStripSeparator();
            this.miHideInactive = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.sepSort = new System.Windows.Forms.ToolStripSeparator();
            this.miSortAlphabetically = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.sepPlot = new System.Windows.Forms.ToolStripSeparator();
            this.miPlot = new System.Windows.Forms.ToolStripMenuItem();
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
            this.listBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseClick);
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            this.listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
            // 
            // contextMenuListBox
            // 
            this.contextMenuListBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditItem,
            this.miAddItem,
            this.miDeleteItem,
            this.miCreateCopy,
            this.sepActive,
            this.miActivate,
            this.miDeactivate,
            this.miActivateAll,
            this.miDeactivateAll,
            this.sepShow,
            this.miHideInactive,
            this.miShowAll,
            this.sepSort,
            this.miSortAlphabetically,
            this.sepPlot,
            this.miPlot});
            this.contextMenuListBox.Name = "contextMenuListBox";
            this.contextMenuListBox.Size = new System.Drawing.Size(181, 314);
            this.contextMenuListBox.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuListBox_Opening);
            // 
            // miEditItem
            // 
            this.miEditItem.Name = "miEditItem";
            this.miEditItem.Size = new System.Drawing.Size(180, 22);
            this.miEditItem.Text = "Edit Item";
            this.miEditItem.Click += new System.EventHandler(this.listBox_DoubleClick);
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
            // sepActive
            // 
            this.sepActive.Name = "sepActive";
            this.sepActive.Size = new System.Drawing.Size(177, 6);
            // 
            // miActivate
            // 
            this.miActivate.Name = "miActivate";
            this.miActivate.Size = new System.Drawing.Size(180, 22);
            this.miActivate.Text = "Activate";
            this.miActivate.Click += new System.EventHandler(this.miActivate_Click);
            // 
            // miDeactivate
            // 
            this.miDeactivate.Name = "miDeactivate";
            this.miDeactivate.Size = new System.Drawing.Size(180, 22);
            this.miDeactivate.Text = "Deactivate";
            this.miDeactivate.Click += new System.EventHandler(this.miDeactivate_Click);
            // 
            // miActivateAll
            // 
            this.miActivateAll.Name = "miActivateAll";
            this.miActivateAll.Size = new System.Drawing.Size(180, 22);
            this.miActivateAll.Text = "Activate All";
            this.miActivateAll.Click += new System.EventHandler(this.miActivateAll_Click);
            // 
            // miDeactivateAll
            // 
            this.miDeactivateAll.Name = "miDeactivateAll";
            this.miDeactivateAll.Size = new System.Drawing.Size(180, 22);
            this.miDeactivateAll.Text = "Deactivate All";
            this.miDeactivateAll.Click += new System.EventHandler(this.miDeactivateAll_Click);
            // 
            // sepShow
            // 
            this.sepShow.Name = "sepShow";
            this.sepShow.Size = new System.Drawing.Size(177, 6);
            // 
            // miHideInactive
            // 
            this.miHideInactive.Name = "miHideInactive";
            this.miHideInactive.Size = new System.Drawing.Size(180, 22);
            this.miHideInactive.Text = "Hide Inactive";
            this.miHideInactive.Click += new System.EventHandler(this.miHideInactive_Click);
            // 
            // miShowAll
            // 
            this.miShowAll.Name = "miShowAll";
            this.miShowAll.Size = new System.Drawing.Size(180, 22);
            this.miShowAll.Text = "Show All";
            this.miShowAll.Click += new System.EventHandler(this.miShowAll_Click);
            // 
            // sepSort
            // 
            this.sepSort.Name = "sepSort";
            this.sepSort.Size = new System.Drawing.Size(177, 6);
            // 
            // miSortAlphabetically
            // 
            this.miSortAlphabetically.Name = "miSortAlphabetically";
            this.miSortAlphabetically.Size = new System.Drawing.Size(180, 22);
            this.miSortAlphabetically.Text = "Sort Alphabetically";
            this.miSortAlphabetically.Click += new System.EventHandler(this.miSortAlphabetically_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 1000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // sepPlot
            // 
            this.sepPlot.Name = "sepPlot";
            this.sepPlot.Size = new System.Drawing.Size(177, 6);
            // 
            // miPlot
            // 
            this.miPlot.Name = "miPlot";
            this.miPlot.Size = new System.Drawing.Size(180, 22);
            this.miPlot.Text = "Plot";
            this.miPlot.Click += new System.EventHandler(this.miPlot_Click);
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
    }
}
