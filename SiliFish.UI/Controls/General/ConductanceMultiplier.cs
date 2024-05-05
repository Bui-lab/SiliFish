namespace SiliFish.UI.Controls.General
{
    public partial class ConductanceMultiplier : UserControl
    {
        private static double lastGapMult = 1;
        private static double lastChemMult = 1;
        public double GapMultiplier => (double)numGapMult.Value;
        public double ChemMultiplier => (double)numChemMult.Value;
        public ConductanceMultiplier()
        {
            InitializeComponent();
            numGapMult.Value = (decimal)lastGapMult;
            numChemMult.Value = (decimal)lastChemMult;
        }
        public ConductanceMultiplier(bool gap)
        {
            InitializeComponent();
            numGapMult.Value = (decimal)lastGapMult;
            numChemMult.Value = (decimal)lastChemMult;
            pGap.Visible = gap;
            pChem.Visible = !gap;
        }

        private void numGapMult_ValueChanged(object sender, EventArgs e)
        {
            lastGapMult = (double)numGapMult.Value;
        }

        private void numChemMult_ValueChanged(object sender, EventArgs e)
        {
            lastChemMult = (double)numChemMult.Value;
        }
    }
}
