using SiliFish.Extensions;
using SiliFish.ModelUnits;
using SiliFish.UI.EventArguments;
using System;
using System.ComponentModel;
using Windows.Devices.SmartCards;

namespace SiliFish.UI.Controls
{
    public partial class HistoryListControl : UserControl
    {
        private string firstItem = "(Double click to select.)";
        public string FirstItem
        {
            get => firstItem;
            set
            {
                listBox.Items.Remove(firstItem);
                listBox.Items.Add(value);
                firstItem = value;
            }
        }
        public event EventHandler ItemSelect;
        public event EventHandler ItemsExport;
        public event EventHandler ItemsImport;
        public int SelectedIndex
        {
            get { return listBox.SelectedIndex; }
            set { listBox.SelectedIndex = value; }
        }

        #region Private Functions
        private void DeleteItem()
        {
            if (listBox.SelectedIndex <= 0) return;
            listBox.Items.RemoveAt(listBox.SelectedIndex);
        }
        private void SelectItem()
        {
            if (listBox.SelectedIndex <= 0) return;
            ItemSelect?.Invoke(listBox.SelectedItem, EventArgs.Empty);
        }
        private void miDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.Capture)//Click on empty space causes this function to be called twice. Skip the first one called from listBox_MouseClick
                return;
            (string tt, bool _) = listBox.SelectedItem?.GetPropertyValue("Tooltip", listBox.SelectedItem?.ToString()) ?? ("", false);
            toolTip.SetToolTip(listBox, tt);
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            SelectItem();
        }

        private void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = listBox.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
            {
                listBox.SelectedItem = null;
            }
        }

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SelectItem();
            else if (e.KeyCode == Keys.Delete && miDeleteItem.Enabled)
                DeleteItem();
        }
        #endregion



        public HistoryListControl()
        {
            InitializeComponent();

        }
        public void ClearItems()
        {
            listBox.Items.Clear();
            listBox.Items.Add(FirstItem);
        }

        public void AppendItem<T>(T obj)
        {
            foreach (var item in listBox.Items)
                if (item.ToString() == obj.ToString()) return;
            listBox.Items.Add(obj);
        }

        public void SetItems<T>(List<T> list)
        {
            ClearItems();
            listBox.Items.AddRange(list.Cast<object>().ToArray());
        }
        public List<T> GetItems<T>()
        {
            return listBox.Items.OfType<T>().ToList();
        }
        private void miClearAll_Click(object sender, EventArgs e)
        {
            ClearItems();
        }

        private void contextMenuListBox_Opening(object sender, CancelEventArgs e)
        {
            miExport.Visible = ItemsExport != null;
            miImport.Visible = ItemsImport != null;
            miExport.Enabled = listBox.Items.Count > 1;
        }

        private void miExport_Click(object sender, EventArgs e)
        {
            ItemsExport?.Invoke(this, EventArgs.Empty);
        }

        private void miImport_Click(object sender, EventArgs e)
        {
            ItemsImport?.Invoke(this, EventArgs.Empty);
        }
    }
}