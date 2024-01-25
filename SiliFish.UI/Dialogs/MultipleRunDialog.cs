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
    public partial class MultipleRunDialog : Form
    {
        public int NumOfSimulations { get; private set; }
        public MultipleRunDialog(bool runTimeStats)
        {
            InitializeComponent();
            if (runTimeStats) 
                lMessage.Text = "The simulations will be run with the same settings sequentially multiple times and time related information will be listed. \r\n" +
                    "Only the last simulation results will be available.";
            else 
                lMessage.Text = "The simulations will be run with the same settings in parallel (hardware permitting) " +
                    "and full information will be saved to the output folder or database.";
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
