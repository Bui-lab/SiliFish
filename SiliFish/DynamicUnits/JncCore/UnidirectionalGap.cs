using SiliFish.Definitions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

//Modified from the code written by Yann Roussel and Tuan Bui

namespace SiliFish.DynamicUnits.JncCore
{
    public class UnidirectionalGap : ElecSynapseCore
    {
        private double IGap1to2 = 0; //the momentary current value

        [JsonIgnore, Browsable(false)]
        public override double ISyn { get { return IGap1to2; } }
        [JsonIgnore, Browsable(false)]
        public override double ISynBackward { get { return 0; } }

        public override void ZeroISyn()
        {
            IGap1to2 = 0;
        }

        [JsonIgnore, Browsable(false)]
        public override string Identifier => $"Conductance: {Conductance:0.####}";


        public UnidirectionalGap()
        {
            bidirectional = false;
        }

        public UnidirectionalGap(Dictionary<string, double> paramExternal)
            : base()
        {
            SetParameters(paramExternal);
            bidirectional = false;
        }

        public UnidirectionalGap(UnidirectionalGap copyFrom)
            : base(copyFrom)
        {
            bidirectional = false;
        }
        public override void InitForSimulation(double deltaT, ref int uniqueID)
        {
            base.InitForSimulation(deltaT, ref uniqueID);
            IGap1to2 = 0;
        }
        public override bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            errors ??= [];
            warnings ??= [];
            int preCount = errors.Count + warnings.Count;
            base.CheckValues(ref errors, ref warnings);
            return errors.Count + warnings.Count == preCount;
        }

        //if any other type of gap junction is implemented, this function may need to be modified
        public override double GetNextVal(double VoltageDiffFrom1To2, double _)
        {
            IGap1to2 = Conductance * VoltageDiffFrom1To2;
            return ISyn;
        }
    }

}
