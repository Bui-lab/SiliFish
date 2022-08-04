using SiliFish.ModelUnits;

namespace SiliFish.DynamicUnits
{
    public class TwoExp_syn
    {
        readonly double taur;
        readonly double taud;
        readonly double vth;
        readonly double E_rev;
        public readonly double Conductance;
        public TwoExp_syn(SynapseParameters param, double conductance)
        {
            //Set synapse constants.
            taud = param.TauD;
            taur = param.TauR;
            vth = param.VTh;
            E_rev = param.E_rev;
            Conductance = conductance; //unitary conductance
        }

        public (double, double) GetNextVal(double v1, double v2, double IsynA, double IsynB)
        {
            double IsynANew, IsynBNew;
            if (v1 > vth)//pre-synaptic neuron spikes
            {
                // mEPSC
                IsynA += (E_rev - v2) * Conductance;
                IsynB += (E_rev - v2) * Conductance;
                double dIsynA = -1 / taud * IsynA;
                double dIsynB = -1 / taur * IsynB;
                IsynANew = IsynA + RunParam.static_dt * dIsynA;
                IsynBNew = IsynB + RunParam.static_dt * dIsynB;
            }
            else
            {
                // no synaptic event
                double dIsynA = -1 / taud * IsynA;
                double dIsynB = -1 / taur * IsynB;
                IsynANew = IsynA + RunParam.static_dt * dIsynA;
                IsynBNew = IsynB + RunParam.static_dt * dIsynB;
            }

            return (IsynANew, IsynBNew);
        }
    }

}
