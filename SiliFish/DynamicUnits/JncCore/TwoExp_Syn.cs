using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;

namespace SiliFish.DynamicUnits
{
    public class TwoExp_syn
    {
        public double DeltaT, DeltaTEuler;

        public double taur{ get; set; }
        public double TauD{ get; set; }
        public double Vth{ get; set; }
        public double ERev { get; set; }
        
        public double Conductance { get; set; }

        public TwoExp_syn()
        { }
        public TwoExp_syn(SynapseParameters param, double conductance, double rundt, double eulerdt)
        {
            DeltaT = rundt;
            DeltaTEuler = eulerdt;
            //Set synapse constants.
            TauD = param.TauD;
            taur = param.TauR;
            Vth = param.VTh;
            ERev = param.E_rev;
            Conductance = conductance; //unitary conductance
        }

        public (double, double) GetNextVal(double v1, double v2, double IsynA, double IsynB)
        {
            double IsynANew = IsynA, IsynBNew = IsynB;
            double dtTracker = 0;
            while (dtTracker < DeltaT)
            {
                dtTracker += DeltaTEuler;
                if (v1 > Vth)//pre-synaptic neuron spikes
                {
                    // mEPSC
                    IsynA += (ERev - v2) * Conductance;
                    IsynB += (ERev - v2) * Conductance;
                    double dIsynA = -1 / TauD * IsynA;
                    double dIsynB = -1 / taur * IsynB;
                    IsynANew = IsynA + DeltaTEuler * dIsynA;
                    IsynBNew = IsynB + DeltaTEuler * dIsynB;
                    break;
                }
                else
                {
                    // no synaptic event
                    double dIsynA = -1 / TauD * IsynA;
                    double dIsynB = -1 / taur * IsynB;
                    IsynANew = IsynA + DeltaTEuler * dIsynA;
                    IsynBNew = IsynB + DeltaTEuler * dIsynB;
                }
            }

            return (IsynANew, IsynBNew);
        }
    }

}
