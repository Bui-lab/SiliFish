using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text.Json.Serialization;

namespace SiliFish.DynamicUnits
{
    public class SimpleGap: ElecSynapseCore
    {
        private double IGap1to2 = 0; //the momentary current value
        private double IGap2to1 = 0; //the momentary current value
        [JsonIgnore, Browsable(false)]
        public override double ISyn { get { return IGap1to2 - IGap2to1; } }
        public override void ZeroISyn()
        {
            IGap1to2 = IGap2to1 = 0;
        }

        [JsonIgnore, Browsable(false)]
        public override string Identifier => $"Conductance: {Conductance:0.####}";


        public SimpleGap()
        { }

        public SimpleGap(Dictionary<string, double> paramExternal)
            :base()
        {
            SetParameters(paramExternal);
        }

        public SimpleGap(SimpleGap copyFrom)
            :base(copyFrom)
        {
        }
        public override void InitForSimulation(double deltaT, double deltaTEuler)
        {
            base.InitForSimulation(deltaT, deltaTEuler);
            IGap1to2 = IGap2to1 = 0;
        }

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            errors ??= new();
            return errors.Count == 0;
        }

        //if any other type of gap junction is implemented, this function may need to be modified
        public override double GetNextVal(double VoltageDiffFrom1To2, double VoltageDiffFrom2To1)
        {
            IGap1to2 = Conductance * VoltageDiffFrom1To2;
            IGap2to1 = Conductance * VoltageDiffFrom2To1;
            return ISyn; 
        }
    }

}
