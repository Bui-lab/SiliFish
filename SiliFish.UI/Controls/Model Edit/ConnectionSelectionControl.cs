using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiliFish.UI.Controls.Model_Edit
{
    public partial class ConnectionSelectionControl : UserControl
    {
        public bool GapJunctions { get => cbGapJunctions.Checked; set => cbGapJunctions.Checked = value; }
        public bool CheminalIncoming { get => cbChemIn.Checked; set => cbChemIn.Checked = value; }
        public bool ChemicalOutgoing { get => cbChemOut.Checked; set => cbChemOut.Checked = value; }

        public string Label { get => lLabel.Text; set => lLabel.Text = value; }
        public ConnectionSelectionControl()
        {
            InitializeComponent();
        }
    }
}
