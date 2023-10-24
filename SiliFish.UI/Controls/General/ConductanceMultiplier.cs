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
    public partial class ConductanceMultiplier : UserControl
    {
        public double GapMultiplier => (double)numGapMult.Value;
        public double ChemMultiplier => (double)numChemMult.Value;
        public ConductanceMultiplier()
        {
            InitializeComponent();
        }
        public ConductanceMultiplier(bool gap)
        {
            InitializeComponent();
            lGapJuncMultiplier.Visible = numGapMult.Visible = gap;
            lChemJncMultiplier.Visible = numChemMult.Visible = !gap;
        }
    }
}
