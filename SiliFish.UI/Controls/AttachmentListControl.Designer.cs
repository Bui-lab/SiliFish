namespace SiliFish.UI.Controls
{
    partial class AttachmentListControl
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
            this.listAttachments = new System.Windows.Forms.ListBox();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.cmAttachments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiViewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiAddAttachment = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiRemoveAttachment = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAttachments.SuspendLayout();
            this.SuspendLayout();
            // 
            // listAttachments
            // 
            this.listAttachments.ContextMenuStrip = this.cmAttachments;
            this.listAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listAttachments.FormattingEnabled = true;
            this.listAttachments.HorizontalScrollbar = true;
            this.listAttachments.ItemHeight = 15;
            this.listAttachments.Location = new System.Drawing.Point(0, 0);
            this.listAttachments.Name = "listAttachments";
            this.listAttachments.Size = new System.Drawing.Size(472, 505);
            this.listAttachments.TabIndex = 32;
            // 
            // cmAttachments
            // 
            this.cmAttachments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiViewFile,
            this.cmiAddAttachment,
            this.cmiRemoveAttachment});
            this.cmAttachments.Name = "cmAttachments";
            this.cmAttachments.Size = new System.Drawing.Size(184, 70);
            this.cmAttachments.Opening += new System.ComponentModel.CancelEventHandler(this.cmAttachments_Opening);
            // 
            // cmiViewFile
            // 
            this.cmiViewFile.Name = "cmiViewFile";
            this.cmiViewFile.Size = new System.Drawing.Size(183, 22);
            this.cmiViewFile.Text = "View File";
            this.cmiViewFile.Click += new System.EventHandler(this.cmiViewFile_Click);
            // 
            // cmiAddAttachment
            // 
            this.cmiAddAttachment.Name = "cmiAddAttachment";
            this.cmiAddAttachment.Size = new System.Drawing.Size(183, 22);
            this.cmiAddAttachment.Text = "Add Attachment";
            this.cmiAddAttachment.Click += new System.EventHandler(this.cmiAddAttachment_Click);
            // 
            // cmiRemoveAttachment
            // 
            this.cmiRemoveAttachment.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.cmiRemoveAttachment.Name = "cmiRemoveAttachment";
            this.cmiRemoveAttachment.Size = new System.Drawing.Size(183, 22);
            this.cmiRemoveAttachment.Text = "Remove Attachment";
            this.cmiRemoveAttachment.Click += new System.EventHandler(this.cmiRemoveAttachment_Click);
            // 
            // AttachmentListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listAttachments);
            this.Name = "AttachmentListControl";
            this.Size = new System.Drawing.Size(472, 505);
            this.cmAttachments.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listAttachments;
        private OpenFileDialog dlgOpenFile;
        private ContextMenuStrip cmAttachments;
        private ToolStripMenuItem cmiViewFile;
        private ToolStripMenuItem cmiAddAttachment;
        private ToolStripMenuItem cmiRemoveAttachment;
    }
}
