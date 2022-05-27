using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiliFish.UI
{
    public partial class ControlContainer : Form
    {
        public ControlContainer()
        {
            InitializeComponent();
        }

        public void AddControl(Control ctrl)
        {
            pMain.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
        }
    }
}
