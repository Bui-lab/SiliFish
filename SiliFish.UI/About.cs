using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SiliFish.UI
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        public void SetTimer(int ms)
        {
            timerAbout.Interval = ms;
            timerAbout.Enabled = true;
            timerAbout.Tick += TimerAbout_Tick;
        }

        private void TimerAbout_Tick(object sender, EventArgs e)
        {
            timerAbout.Tick-=TimerAbout_Tick;
            timerAbout.Enabled = false;
            this.Close();
        }
    }
}
