using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SiliFish.UI.Controls
{
    public partial class AttachmentListControl : UserControl
    {
        private static string AttachmentDefaultFolder;
        public AttachmentListControl()
        {
            InitializeComponent();
        }

        private void cmAttachments_Opening(object sender, CancelEventArgs e)
        {
            cmiViewFile.Enabled = cmiRemoveAttachment.Enabled = listAttachments.SelectedItems.Count > 0;
        }
        private void cmiViewFile_Click(object sender, EventArgs e)
        {
            string filename = listAttachments.SelectedItem.ToString();
            if (!File.Exists(filename))
            {
                MessageBox.Show($"Path or file {filename} does not exist.", "Error");
                return;
            }
            FileUtil.ShowFile(filename);
        }

        private void cmiAddAttachment_Click(object sender, EventArgs e)
        {
            dlgOpenFile.InitialDirectory = AttachmentDefaultFolder;
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                listAttachments.Items.Add(dlgOpenFile.FileName);
                AttachmentDefaultFolder = Path.GetDirectoryName(dlgOpenFile.FileName);
            }
        }

        private void cmiRemoveAttachment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to remove the selected attachment?", "SiliFish", MessageBoxButtons.OKCancel) == DialogResult.OK)
                listAttachments.Items.RemoveAt(listAttachments.SelectedIndex);
        }

        public List<string> GetAttachments()
        {
            List<string> list=[];
            foreach (var att in listAttachments.Items)
            {
                list.Add(att.ToString());
            }
            return list;
        }

        public void SetAttachments(List<string> attachments)
        {
            listAttachments.Items.Clear();
            listAttachments.Items.AddRange(attachments?.ToArray());
        }
    }
}
