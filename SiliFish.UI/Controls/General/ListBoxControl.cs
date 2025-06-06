﻿using SiliFish.Extensions;
using SiliFish.ModelUnits;
using SiliFish.UI.EventArguments;
using System.ComponentModel;

namespace SiliFish.UI.Controls
{

    public partial class ListBoxControl : UserControl
    {
        private List<object> HiddenItems = [];
        private bool IsInactiveHidden = false;

        public event EventHandler ItemAdd;
        public event EventHandler ItemDelete;
        public event EventHandler ItemCopy;
        public event EventHandler ItemView;
        public event EventHandler ItemToggleActive;
        public event EventHandler ItemSelect;
        public event EventHandler ItemPlot;
        public event EventHandler ItemHighlight;
        public event EventHandler ItemsExport;
        public event EventHandler ItemsImport;
        public event EventHandler ItemsHideShowInactive;
        public event EventHandler ListBoxActivated;

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
            set
            {
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    if (listBox.Items[i] == value ||
                        listBox.Items[i].ToString() == value.ToString())
                    {
                        listBox.SelectedIndices.Clear();
                        //listBox.Focus();
                        listBox.SelectedIndex = i;
                        listBox.TopIndex = i;
                        return;
                    }
                }
            }
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
            listBox.Sorted = true;
        }

        private void SetActive(object item, int index, bool active, bool invoke)
        {
            item.SetPropertyValue("Active", active);
            listBox.Items[index] = item;//to refresh text
            if (invoke)
                ItemToggleActive?.Invoke(item, new EventArgs());
        }
        private void miActivate_Click(object sender, EventArgs e)
        {
            bool invoke = listBox.SelectedItems.Count == 1;
            foreach (int ind in listBox.SelectedIndices.Cast<int>().ToList())
            {
                SetActive(listBox.Items[ind], ind, true, invoke);
            }
            if (!invoke)
                ItemToggleActive?.Invoke(null, new EventArgs());
        }

        private void miDeactivate_Click(object sender, EventArgs e)
        {
            bool invoke = listBox.SelectedItems.Count == 1;
            foreach (int ind in listBox.SelectedIndices.Cast<int>().ToList())
            {
                SetActive(listBox.Items[ind], ind, false, invoke);
            }
            if (!invoke)
                ItemToggleActive?.Invoke(null, new EventArgs());
        }
        private void miActivateAll_Click(object sender, EventArgs e)
        {
            foreach (int index in Enumerable.Range(0, listBox.Items.Count))
                SetActive(listBox.Items[index], index, active: true, invoke: false);
            ItemToggleActive?.Invoke(null, new EventArgs());
        }

        private void miDeactivateAll_Click(object sender, EventArgs e)
        {
            foreach (int index in Enumerable.Range(0, listBox.Items.Count))
                SetActive(listBox.Items[index], index, active: false, invoke: false);
            ItemToggleActive?.Invoke(null, new EventArgs());
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.Capture)//Click on empty space causes this function to be called twice. Skip the first one called from listBox_MouseClick
                return;
            (string tt, bool _) = listBox.SelectedItem?.GetPropertyValue("Tooltip", listBox.SelectedItem?.ToString()) ?? ("", false);
            toolTip.SetToolTip(listBox, tt);
            ItemSelect?.Invoke(this, new EventArgs());
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

        private void HideInactive()
        {
            IsInactiveHidden = true;
            for (int ind = listBox.Items.Count - 1; ind >= 0; ind--)
            {
                object obj = listBox.Items[ind];
                var (active, exists) = obj.GetPropertyValue("Active", true);
                if (exists && !active)
                {
                    listBox.Items.RemoveAt(ind);
                    HiddenItems.Add(obj);
                }
            }
            ItemsHideShowInactive?.Invoke(this, new GenericArgs() { ValueBoolean = false });
        }
        private void miHideInactive_Click(object sender, EventArgs e)
        {
            HideInactive();
        }

        private void ShowInactive()
        {
            IsInactiveHidden = false;
            foreach (object obj in HiddenItems)
                listBox.Items.Add(obj);
            HiddenItems.Clear();
            ItemsHideShowInactive?.Invoke(this, new GenericArgs() { ValueBoolean = true });
        }

        private void miShowAll_Click(object sender, EventArgs e)
        {
            ShowInactive();
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
            try
            {
                List<ModelUnitBase> list = listBox.SelectedItems.Cast<ModelUnitBase>().ToList();
                ItemPlot?.Invoke(list, new EventArgs());
            }
            catch { }
        }
        private void miHighlight_Click(object sender, EventArgs e)
        {
            SelectedUnitArgs args = new() { enforce = true };
            List<ModelUnitBase> list = listBox.SelectedItems.Cast<ModelUnitBase>().ToList();
            args.unitsSelected.AddRange(list);
            ItemHighlight?.Invoke(listBox.SelectedItems, args);
        }
        private void contextMenuListBox_Opening(object sender, CancelEventArgs e)
        {
            ListBoxActivated?.Invoke(this, EventArgs.Empty);
            miActivate.Visible = miDeactivate.Visible = false;
            if (listBox.Items.Count == 0)
            {
                miHideInactive.Visible = false;
                miShowAll.Visible = HiddenItems.Count != 0;
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
            miPlot.Visible = ItemPlot != null;
            miHighlight.Visible = ItemHighlight != null;
            sepPlot.Visible = miPlot.Visible || miHighlight.Visible;
            sepShow.Visible = miShowAll.Visible || miHideInactive.Visible;
        }
        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && miEditItem.Enabled)
                EditItem();
            else if (e.KeyCode == Keys.Insert && miAddItem.Enabled)
                AddItem();
            else if (e.KeyCode == Keys.Delete && miDeleteItem.Enabled)
                DeleteItem();
            else if (e.KeyValue == 'A' || e.KeyValue == 'a')
            {
                if (e.Modifiers == Keys.Control)
                    foreach (var i in Enumerable.Range(0, listBox.Items.Count))
                    {
                        listBox.SetSelected(i, true);
                    }
            }
        }
        #endregion

        public List<object> GetItems(bool includeHidden)
        {
            if (!includeHidden || HiddenItems.Count == 0)
                return listBox.Items.Cast<object>().ToList();
            List<object> fullList = [.. listBox.Items.Cast<object>().ToList(), .. HiddenItems];
            return fullList;
        }


        public ListBoxControl()
        {
            InitializeComponent();
        }
        public void ClearItems()
        {
            listBox.Items.Clear();
            HiddenItems.Clear();
        }
        public void LoadItems(List<object> items)
        {
            ClearItems();
            AppendItems(items);
        }
        public void AppendItems(List<object> items)
        {
            foreach (object item in items)
                AppendItem(item);
            if (IsInactiveHidden)
                HideInactive();
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
        public void SelectItem(object obj)
        {
            SelectedItem = obj;
        }

        public void SelectItems(List<string> items)
        {
            int? topIndex = null;
            listBox.SelectedIndices.Clear();
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                if (items.Contains(listBox.Items[i].ToString()) ||
                    (listBox.Items[i] is ModelUnitBase ub && items.Contains(ub.ID)))
                {
                    listBox.SetSelected(i, true);
                    topIndex ??= i;
                }
            }
            listBox.TopIndex = topIndex ?? 0;
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
                sepCustom.Visible = true;
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
            {
                SelectedUnitArgs args = new() { enforce = false };
                List<ModelUnitBase> list = listBox.SelectedItems.Cast<ModelUnitBase>().ToList();
                args.unitsSelected.AddRange(list);
                ItemHighlight?.Invoke(listBox.SelectedItems, args);

            }
        }

        private void listBox_Leave(object sender, EventArgs e)
        {
            toolTip.RemoveAll();
        }

        private void listBox_Enter(object sender, EventArgs e)
        {
            ListBoxActivated?.Invoke(this, EventArgs.Empty);
        }
    }
}