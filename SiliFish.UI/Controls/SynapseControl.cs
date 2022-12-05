using SiliFish.ModelUnits;

namespace SiliFish.UI.Controls
{
    public partial class SynapseControl : UserControl
    {
        public double EReversal
        {
            get { return (double)numEReversal.Value; }
            set { numEReversal.Value = (decimal)value; }
        }
        public double VThreshold
        {
            get { return (double)numVthreshold.Value; }
            set { numVthreshold.Value = (decimal)value; }
        }
        public SynapseControl()
        {
            InitializeComponent();
        }

        public void SetSynapseParameters(SynapseParameters synparam)
        {
            if (synparam == null) return;
            numEReversal.Value = (decimal)synparam.E_rev;
            numTauD.Value = (decimal)synparam.TauD;
            numTauR.Value = (decimal)synparam.TauR;
            numVthreshold.Value = (decimal)synparam.VTh;
        }

        public SynapseParameters GetSynapseParameters()
        {
            return new SynapseParameters
            {
                TauD = (double)numTauD.Value,
                TauR = (double)numTauR.Value,
                VTh = (double)numVthreshold.Value,
                E_rev = (double)numEReversal.Value,
            };
        }

    }
}
