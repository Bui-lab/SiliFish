using SiliFish.Extensions;
using System.ComponentModel;

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

        private event EventHandler activateItem;
        public event EventHandler ActivateItem { add => activateItem += value; remove => activateItem -= value; }

        private event EventHandler sortItems;
        public event EventHandler SortItems { add => sortItems += value; remove => sortItems -= value; }

        private event EventHandler selectItem;
        public event EventHandler SelectItem { add => selectItem += value; remove => selectItem -= value; }

        private Dictionary<int, object> HiddenItems = new Dictionary<int, object>();
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

        public void AddContextMenu(string label, EventHandler func)
        {
            ToolStripItem mi = contextMenuListBox.Items[label];
            if (mi != null)
            {
                mi.Visible = true;
                mi.Enabled = true;
            }
            else
            {
                mi = contextMenuListBox.Items.Add(label);
                mi.Click += miCustomSort_Click;
                mi.Tag = func;
            }
        }
        public void ChangeContextMenuAccess(string label, bool visible, bool enabled = true)
        {
            ToolStripItem mi = contextMenuListBox.Items[label];
            if (mi != null)
            {
                mi.Visible = visible;
                mi.Enabled = enabled;
            }
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

        private void SetActive(object item, int index, bool active)
        {
            item.SetPropertyValue("Active", active);
            listBox.Items[index] = item;//to refresh text
            activateItem?.Invoke(item, new EventArgs());
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
            (string tt, bool _) = listBox.SelectedItem?.GetPropertyValue("Tooltip", listBox.SelectedItem?.ToString()) ?? ("", false);
            toolTip.SetToolTip(listBox, tt);
            selectItem?.Invoke(listBox.SelectedItem, new EventArgs());
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
                viewItem?.Invoke(listBox.SelectedItem, null);
        }

        private void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = listBox.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
            {
                listBox.SelectedItem = null;
            }
        }
        private void contextMenuListBox_Opening(object sender, CancelEventArgs e)
        {
            miSortAlphabetically.Visible = sortItems != null;

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


    }
}
