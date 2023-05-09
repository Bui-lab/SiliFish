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
    public partial class UpDownControl : DomainUpDown
    {
        private bool upClick;
        private bool downClick;

        public bool UpClick
        {
            get
            {
                bool b = upClick;
                upClick = false;
                return b;
            }
        }

        public bool DownClick
        {
            get
            {
                bool b = downClick;
                downClick = false;
                return b;
            }
        }
        public UpDownControl()
        {
            InitializeComponent();
        }

        public override void UpButton()
        {
            base.UpButton();
            upClick = true;
            downClick = false;
        }

        public override void DownButton()
        {
            base.DownButton();
            downClick = true;
            upClick = false;
        }
    }
}
