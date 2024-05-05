namespace SiliFish.UI.Controls.Model_Edit
{
    public partial class ConnectionSelectionControl : UserControl
    {
        private bool chemInOutExists = false;
       public bool GapJunctions { get => cbGapJunctions.Checked; set => cbGapJunctions.Checked = value; }
        public bool CheminalIncoming 
        { 
            get => ChemInOutExists && cbChemIn.Checked ||
                !ChemInOutExists && cbChem.Checked;
            set
            {
                if (ChemInOutExists) cbChemIn.Checked = value;
                else cbChem.Checked = value;
            }
        }
        public bool ChemicalOutgoing        
        {
            get => ChemInOutExists && cbChemOut.Checked ||
                !ChemInOutExists && cbChem.Checked;
            set
            {
                if (ChemInOutExists) cbChemOut.Checked = value;
                else cbChem.Checked = value;
            }
        }

        public bool ChemInOutExists
        {
            get => chemInOutExists;
            set
            {
                chemInOutExists = value;
                cbChem.Visible = !value;
                cbChemIn.Visible = cbChemOut.Visible = value;
            }
        }

        public string Label { get => lLabel.Text; set => lLabel.Text = value; }
        public ConnectionSelectionControl()
        {
            InitializeComponent();
        }
    }
}
