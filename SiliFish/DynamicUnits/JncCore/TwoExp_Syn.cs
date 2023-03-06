using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using System.Collections.Generic;

namespace SiliFish.DynamicUnits
{
    public class TwoExp_syn
    {
        public double DeltaT, DeltaTEuler;

        public double TauR{ get; set; }
        public double TauD{ get; set; }
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
            TauR = param.TauR;
            ERev = param.E_rev;
            Conductance = conductance; //unitary conductance
        }

        public TwoExp_syn(TwoExp_syn copyFrom)
        {
            DeltaT = copyFrom.DeltaT;
            DeltaTEuler = copyFrom.DeltaTEuler;
            //Set synapse constants.
            TauD = copyFrom.TauD;
            TauR = copyFrom.TauR;
            ERev = copyFrom.ERev;
            Conductance = copyFrom.Conductance; //unitary conductance
        }

        public bool CheckValues(ref List<string> errors)
        {
            errors ??= new();
            if (TauD < GlobalSettings.Epsilon || TauR < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Tau has 0 value.");
            if (Conductance < GlobalSettings.Epsilon)
                errors.Add($"Chemical synapse: Conductance has 0 value.");
            return errors.Count == 0;
        }
        public (double, double) GetNextVal(bool spike, double vPost, double IsynA, double IsynB)
        {
            double IsynANew = IsynA, IsynBNew = IsynB;
            double dtTracker = 0;
            while (dtTracker < DeltaT)
            {
                dtTracker += DeltaTEuler;
                if (spike)//pre-synaptic neuron spikes
                {
                    // mEPSC
                    IsynA += (ERev - vPost) * Conductance;
                    IsynB += (ERev - vPost) * Conductance;
                    double dIsynA = -1 / TauD * IsynA;
                    double dIsynB = -1 / TauR * IsynB;
                    IsynANew = IsynA + DeltaTEuler * dIsynA;
                    IsynBNew = IsynB + DeltaTEuler * dIsynB;
                    break;
                }
                else
                {
                    // no synaptic event
                    double dIsynA = -1 / TauD * IsynA;
                    double dIsynB = -1 / TauR * IsynB;
                    IsynANew = IsynA + DeltaTEuler * dIsynA;
                    IsynBNew = IsynB + DeltaTEuler * dIsynB;
                }
            }

            return (IsynANew, IsynBNew);
        }
    }

}
