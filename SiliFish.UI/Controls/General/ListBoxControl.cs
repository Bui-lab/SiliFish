using SiliFish.Extensions;
using SiliFish.ModelUnits;
using SiliFish.UI.EventArguments;
using System;
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
        public event EventHandler ItemHighlight;
        public event EventHandler ItemsExport;
        public event EventHandler ItemsImport;

        public int SelectedIndex
        {
            get { return listBox.SelectedIndex; }
            set { listBox.SelectedIndex = value; }
        }
        public List<int> SelectedIndices
        {
            get { return listBox.SelectedIndices.Cast<int>().ToList(); }
        }
        public object SelectedItem
        {
            get { return listBox.SelectedItem; }
            set { listBox.SelectedItem = value; }
        }

        public List<object> SelectedItems
        {
            get { return listBox.SelectedItems?.Cast<object>().ToList(); }
        }

        public SelectionMode SelectionMode
        {
            get => listBox.SelectionMode;
            set { listBox.SelectionMode = value; }
        }

        public bool HighlightSelected => miHighlightSelected.CheckState == CheckState.Checked;


        public void EnableImportExport()
        {
            miExport.Enabled = miImport.Enabled = true;
        }

        public void EnableHightlight()
        {
            miHighlight.Visible = miHighlightSelected.Visible = true;
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
            ItemDelete?.Invoke(this, new EventArgs());//listBox is sent rather than the selected item, as the list box needs to be redrawn
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
            foreach (int ind in listBox.SelectedIndices.Cast<int>().ToList())
            {
                SetActive(listBox.Items[ind], ind, true);
            }
        }

        private void miDeactivate_Click(object sender, EventArgs e)
        {
            foreach (int ind in listBox.SelectedIndices.Cast<int>().ToList())
            {
                SetActive(listBox.Items[ind], ind, false);
            }
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
        private void miHighlight_Click(object sender, EventArgs e)
        {
            ItemHighlight?.Invoke(listBox.SelectedItem, new SelectedUnitArgs()
            { unitSelected = listBox.SelectedItem as ModelUnitBase, enforce = true });
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
                if (exists)
                {
                    bool activeExists = false;
                    bool inactiveExists = false;

                    foreach (var item in listBox.SelectedItems)
                    {
                        (bool active, bool _) = item.GetPropertyValue("Active", true);
                        if (active)
                            activeExists = true;
                        else
                            inactiveExists = true;
                        if (activeExists && inactiveExists)
                            break;
                    }
                    miActivate.Visible = inactiveExists;
                    miDeactivate.Visible = activeExists;
                }
            }
            miPlot.Visible = listBox.SelectedItems.Count == 1 && ItemPlot != null;
            miHighlight.Visible = ItemHighlight != null;
            sepPlot.Visible = miPlot.Visible || miHighlight.Visible;
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
                mi = new ToolStripMenuItem(label);
                contextMenuListBox.Items.Add(mi);
                mi.Click += miCustomSort_Click;
                mi.Tag = func;
            }
        }

        public void RemoveContextMenu(string label)
        {
            ToolStripItem mi = null;
            foreach (var item in contextMenuListBox.Items)
            {
                if (item.ToString() == label)
                {
                    mi = item as ToolStripItem;
                    break;
                }
            }
            if (mi != null)
            {
                mi.Visible = false;
                mi.Enabled = false;
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

        private void miHighlightSelected_Click(object sender, EventArgs e)
        {
            miHighlightSelected.CheckState = miHighlightSelected.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
            if (miHighlightSelected.CheckState == CheckState.Checked)
                ItemHighlight?.Invoke(listBox.SelectedItem,
                    new SelectedUnitArgs()
                    { unitSelected = listBox.SelectedItem as ModelUnitBase, enforce = false });
        }

        private void listBox_Leave(object sender, EventArgs e)
        {
            toolTip.RemoveAll();
        }
    }
}