using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Junction;

namespace SiliFish.UI.Controls
{
    public partial class SynapseControl : UserControl
    {
        public double EReversal
        {
            get { return (double)numEReversal.Value; }
            set { numEReversal.Value = (decimal)value; }
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
        }

        public SynapseParameters GetSynapseParameters()
        {
            return new SynapseParameters
            {
                TauD = (double)numTauD.Value,
                TauR = (double)numTauR.Value,
                E_rev = (double)numEReversal.Value,
            };
        }

        internal List<string> CheckValues()
        {
            List<string> errors = new();
            if ((double)numTauD.Value < GlobalSettings.Epsilon)
                errors.Add("Decay tau is 0.");
            if ((double)numTauR.Value < GlobalSettings.Epsilon)
                errors.Add("Rise tau is 0.");
            return errors;
        }
    }
}
