using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiliFish.UI.Controls
{
    public partial class ListBoxControl : UserControl
    {
        private event EventHandler addItem;
        public event EventHandler AddItem { add => addItem += value; remove => addItem -= value; }

        private event EventHandler deleteItem;
        public event EventHandler DeleteItem { add => deleteItem += value; remove => deleteItem -= value; }

        private event EventHandler copyItem;
        public event EventHandler CopyItem { add => copyItem += value; remove => copyItem -= value; }

        private event EventHandler viewItem;
        public event EventHandler ViewItem { add => viewItem += value; remove => viewItem -= value; }

        private event EventHandler sortItems;
        public event EventHandler SortItems { add => sortItems += value; remove => sortItems -= value; }

        public ListBox.ObjectCollection Items { get { return listBox.Items;  }}
        public int SelectedIndex
        {
            get { return listBox.SelectedIndex; }
            set { listBox.SelectedIndex = value; }
        }
        public object SelectedItem
        {
            get { return listBox.SelectedItem; }
            set { listBox.SelectedItem = value; }
        }
        public ListBoxControl()
        {
            InitializeComponent();
        }

        public void AddSortMenu(string sortBy, EventHandler sortFunc)
        {
            ToolStripItem mi = contextMenuListBox.Items.Add("Sort by " + sortBy);
            mi.Click += miCustomSort_Click;
            mi.Tag = sortFunc;
        }

        private void miCustomSort_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripItem).Tag is EventHandler eh)
                eh.Invoke(this, e);
        }

        private void miAddItem_Click(object sender, EventArgs e)
        {
            addItem?.Invoke(this, new EventArgs());
        }

        private void miDeleteItem_Click(object sender, EventArgs e)
        {
            deleteItem?.Invoke(listBox.SelectedItem, new EventArgs());
        }

        private void miCreateCopy_Click(object sender, EventArgs e)
        {
            copyItem?.Invoke(listBox.SelectedItem, new EventArgs());
        }

        private void miSortAlphabetically_Click(object sender, EventArgs e)
        {
            sortItems?.Invoke(this, new EventArgs());
        }

        private void miActivate_Click(object sender, EventArgs e)
        {
            listBox.SelectedItem.SetPropertyValue("Active", true);
            listBox.Items[listBox.SelectedIndex] = listBox.Items[listBox.SelectedIndex];//to refresh text
        }

        private void miDeactivate_Click(object sender, EventArgs e)
        {
            listBox.SelectedItem.SetPropertyValue("Active", false);
            listBox.Items[listBox.SelectedIndex] = listBox.Items[listBox.SelectedIndex];//to refresh text
        }
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fnc = listBox.SelectedItem?.GetType().GetMethod("GetTooltip");
            string tt = fnc?.Invoke(listBox.SelectedItem, null).ToString() ?? listBox.SelectedItem?.ToString();
            toolTip.SetToolTip(listBox, tt);
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            if (listBox.SelectedItem!=null)
                viewItem?.Invoke(listBox.SelectedItem, null);
        }

        private void contextMenuListBox_Opening(object sender, CancelEventArgs e)
        {
            miSortAlphabetically.Visible = sortItems != null;

            miActivate.Visible = miDeactivate.Visible = false;
            if (listBox.SelectedItem != null)
            {
                var (active, exists) = listBox.SelectedItem.GetPropertyValue("Active", true);
                if (exists)
                {
                    miActivate.Visible = !active;
                    miDeactivate.Visible = active;
                }
            }
            

        }


    }
}
