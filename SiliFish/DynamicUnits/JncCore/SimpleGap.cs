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
        private double ISynA = 0; //the momentary current value
        private double ISynB = 0; //the momentary current value
        [JsonIgnore, Browsable(false)]
        public override double ISyn { get { return ISynA - ISynB; } }
        public override void ZeroISyn()
        {
            ISynA = ISynB = 0;
        }

        [JsonIgnore, Browsable(false)]
        public override string Identifier => $"Conductance: {Conductance:0.####}";


        public SimpleGap()
        { }

        public SimpleGap(Dictionary<string, double> paramExternal, double rundt, double eulerdt)
            :base(rundt, eulerdt)
        {
            SetParameters(paramExternal);
        }

        public SimpleGap(SimpleGap copyFrom)
            :base(copyFrom)
        {
        }
        public override void InitForSimulation()
        {
            ISynA = ISynB = 0;
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
            double VoltageDiff = 0;
            return Conductance * VoltageDiff; 
        }
    }

}
