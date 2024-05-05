using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiliFish.UI.Controls.General
{
    public partial class StartEndTimeControl : UserControl
    {
        public int EndTime { get => (int)eEndTime.Value; set => eEndTime.Value = value; } 
        public int StartTime { get => (int)eStartTime.Value;  set => eStartTime.Value = value; }

        public StartEndTimeControl()
        {
            InitializeComponent();
        }

    }
}
