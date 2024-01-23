using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiliFish.UI.Dialogs
{
    public partial class RunTimeStatsDialog : Form
    {
        public int NumOfSimulations { get; private set; }
        public RunTimeStatsDialog()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            NumOfSimulations = (int)eNumber.Value;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NumOfSimulations = 0;
            Close();
        }
    }
}
