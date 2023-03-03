using SiliFish.Extensions;
using System.ComponentModel;
using Windows.Devices.SmartCards;

namespace SiliFish.UI.Controls
{

    public partial class ListBoxControl : UserControl
    {
        private Dictionary<int, object> HiddenItems = new();
        
        public event EventHandler ItemAdd;
        public event EventHandler ItemDelete;
        public event EventHandler ItemCopy;
        public event EventHandler ItemView;
        public event EventHandler ItemToggleActive;
        public event EventHandler ItemsSort;
        public event EventHandler ItemSelect;
        public event EventHandler ItemPlot;
        public event EventHandler ItemsExport;
        public event EventHandler ItemsImport;

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

        public void EnableImportExport()
        {
            miExport.Enabled = miImport.Enabled = true;
        }

        #region Private Functions
        private void EditItem()
        {
            if (listBox.SelectedItem != null)
                ItemView?.Invoke(listBox.SelectedItem, null);
        }

        private void AddItem()
        {
            ItemAdd?.Invoke(this, new EventArgs());
        }
        private void DeleteItem()
        {
            ItemDelete?.Invoke(listBox.SelectedItem, new EventArgs());
        }

        private void miCustomSort_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripItem).Tag is EventHandler eh)
                eh.Invoke(this, e);
        }

        private void miAddItem_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void miDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void miCreateCopy_Click(object sender, EventArgs e)
        {
            ItemCopy?.Invoke(listBox.SelectedItem, new EventArgs());
        }

        private void miSortAlphabetically_Click(object sender, EventArgs e)
        {
            ItemsSort?.Invoke(this, new EventArgs());
        }

        private void SetActive(object item, int index, bool active)
        {
            item.SetPropertyValue("Active", active);
            listBox.Items[index] = item;//to refresh text
            ItemToggleActive?.Invoke(item, new EventArgs());
        }
        private void miActivate_Click(object sender, EventArgs e)
        {
            SetActive(listBox.SelectedItem, listBox.SelectedIndex, true);
        }

        private void miDeactivate_Click(object sender, EventArgs e)
        {
            SetActive(listBox.SelectedItem, listBox.SelectedIndex, false);
        }
        private void miActivateAll_Click(object sender, EventArgs e)
        {
            foreach (int index in Enumerable.Range(0, listBox.Items.Count))
                SetActive(listBox.Items[index], index, true);
        }

        private void miDeactivateAll_Click(object sender, EventArgs e)
        {
            foreach (int index in Enumerable.Range(0, listBox.Items.Count))
                SetActive(listBox.Items[index], index, false);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.Capture)//Click on empty space causes this function to be called twice. Skip the first one called from listBox_MouseClick
                return;
            (string tt, bool _) = listBox.SelectedItem?.GetPropertyValue("Tooltip", listBox.SelectedItem?.ToString()) ?? ("", false);
            toolTip.SetToolTip(listBox, tt);
            ItemSelect?.Invoke(listBox.SelectedItem, new EventArgs());
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
        }

        private void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = listBox.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
            {
                listBox.SelectedItem = null;
            }
        }

        private void miHideInactive_Click(object sender, EventArgs e)
        {
            for (int ind = listBox.Items.Count - 1; ind >= 0; ind--)
            {
                object obj = listBox.Items[ind];
                var (active, exists) = obj.GetPropertyValue("Active", true);
                if (exists && !active)
                {
                    listBox.Items.RemoveAt(ind);
                    HiddenItems.Add(ind, obj);
                }
            }
        }

        private void miShowAll_Click(object sender, EventArgs e)
        {
            foreach (int ind in HiddenItems.Keys.OrderBy(k => k))
                listBox.Items.Insert(ind, HiddenItems[ind]);
            HiddenItems.Clear();
        }
        private void miExport_Click(object sender, EventArgs e)
        {
            ItemsExport?.Invoke(this, EventArgs.Empty);
        }

        private void miImport_Click(object sender, EventArgs e)
        {
            ItemsImport?.Invoke(this, EventArgs.Empty);
        }
        private void miPlot_Click(object sender, EventArgs e)
        {
            ItemPlot?.Invoke(listBox.SelectedItem, new EventArgs());
        }
        private void contextMenuListBox_Opening(object sender, CancelEventArgs e)
        {
            miSortAlphabetically.Visible = ItemsSort != null;

            miActivate.Visible = miDeactivate.Visible = false;
            if (listBox.Items.Count == 0)
            {
                miHideInactive.Visible = false;
                miShowAll.Visible = HiddenItems.Any();
            }
            else
            {
                var (_, exists) = listBox.Items[0].GetPropertyValue("Active", true);
                miShowAll.Visible = miHideInactive.Visible = exists;
                if (listBox.SelectedItem != null)
                {
                    (var active, exists) = listBox.SelectedItem.GetPropertyValue("Active", true);
                    if (exists)
                    {
                        miActivate.Visible = !active;
                        miDeactivate.Visible = active;
                    }
                }
            }
        }
        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && miEditItem.Enabled)
                EditItem();
            else if (e.KeyCode == Keys.Insert && miAddItem.Enabled)
                AddItem();
            else if (e.KeyCode == Keys.Delete && miDeleteItem.Enabled)
                DeleteItem();
        }
        #endregion

        public List<object> GetItems(bool includeHidden)
        {
            if (!includeHidden || !HiddenItems.Any())
                return listBox.Items.Cast<object>().ToList();
            List<object> fullList = new();
            fullList.AddRange(listBox.Items.Cast<object>().ToList());
            foreach (int index in HiddenItems.Keys.OrderBy(k => k))
            {
                fullList.Insert(index, HiddenItems[index]);
            }
            return fullList;
        }

        public void ClearItems()
        {
            listBox.Items.Clear();
            HiddenItems.Clear();
        }

        public void AppendItem(object obj)
        {
            listBox.Items.Add(obj);
        }
        public void InsertItem(int ind, object obj)
        {
            listBox.Items.Insert(ind, obj);
        }

        public void RemoveItemAt(int ind)
        {
            listBox.Items.RemoveAt(ind);
        }

        public void RefreshItem(int ind, object obj)
        {
            listBox.Items.RemoveAt(ind);
            listBox.Items.Insert(ind, obj);
            listBox.SelectedIndex = ind;
        }

        public ListBoxControl()
        {
            InitializeComponent();
        }

        public void AddSortContextMenu(string label, EventHandler func)
        {
            ToolStripItem mi = contextMenuListBox.Items[label];
            if (mi != null)
            {
                mi.Visible = true;
                mi.Enabled = true;
            }
            else
            {
                int ind = sepSort.MergeIndex;
                mi = new ToolStripMenuItem(label);
                contextMenuListBox.Items.Insert(ind + 1, mi);
                mi.Click += miCustomSort_Click;
                mi.Tag = func;
            }
        }
        public void SetEnabledContextMenu(string label, bool enabled)
        {
            ToolStripItem mi = contextMenuListBox.Items[label];
            if (mi != null)
            {
                mi.Enabled = enabled;
            }
        }


    }
}