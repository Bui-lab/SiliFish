using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Drawing;
using System.Collections.Generic;

namespace SiliFish.ModelUnits.Junction
{
    public class ChemicalSynapse: JunctionBase
    {
        private int duration; //The number of time units (dt) it will take for the current to travel from one neuron to the other
                              //calculated at InitForSimulation, and used only during simulation

        private double ISynA = 0; //the momentary current value
        private double ISynB = 0; //the momentary current value
        private double ISyn { get { return ISynA - ISynB; } }

        public TwoExp_syn Core { get; set; }

        [JsonIgnore]
        public SynapseParameters SynapseParameters
        {
            get
            {
                if (Core == null) return null;
                return new SynapseParameters()
                {
                    E_rev = Core.ERev,
                    TauD = Core.TauD,
                    TauR = Core.TauD,
                    VTh = Core.Vth
                };
            }
        }
        public Neuron PreNeuron;
        public Cell PostCell; //can be a neuron or a muscle cell

        public double[] InputCurrent; //Current vector 
        [JsonIgnore]
        public override string ID { get { return $"Syn: {PreNeuron.ID} -> {PostCell.ID}; Conductance: {Weight:0.#####}"; } }
        internal bool IsActive(int timepoint)
        {
            double t_ms = PreNeuron.Model.RunParam.GetTimeOfIndex(timepoint);
            return TimeLine_ms?.IsActive(t_ms) ?? true;
        }

        public ChemicalSynapse()
        { }
        public ChemicalSynapse(Neuron preN, Cell postN, SynapseParameters param, double conductance, DistanceMode distmode)
        {
            Core = new TwoExp_syn(param, conductance, preN.Model.RunParam.DeltaT, preN.Model.RunParam.DeltaTEuler);
            PreNeuron = preN;
            PostCell = postN;
            Target = postN.ID;
            DistanceMode = distmode;
            Weight = conductance;
        }

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            Core.CheckValues(ref errors);
            return errors.Count == 0;
        }
        public void LinkObjects(RunningModel model)
        {
            CellPool cp = model.CellPools.Where(cp => cp.Cells.Exists(c => c.ID == Target)).FirstOrDefault();
            PostCell = cp.GetCell(Target);
            if (PostCell is Neuron n)
                n.Synapses.Add(this);
            else if (PostCell is MuscleCell m)
                m.EndPlates.Add(this);
            Core.DeltaTEuler = model.RunParam.DeltaTEuler;
            Core.DeltaT = model.RunParam.DeltaT;
        }
        public override string ToString()
        {
            string activeStatus = Active && TimeLine_ms.IsBlank() ? "" : Active ? " (timeline)" : " (inactive)";
            return $"{ID}{activeStatus}";
        }
        public void InitForSimulation(int nmax)
        {
            InputCurrent = new double[nmax];
            InputCurrent[0] = 0;
            if (FixedDuration_ms != null)
                duration = (int)(FixedDuration_ms / PreNeuron.Model.RunParam.DeltaT);
            else
            {
                double distance = Util.Distance(PreNeuron.Coordinate, PostCell.Coordinate, DistanceMode);
                int delay = (int)(Delay_ms / PreNeuron.Model.RunParam.DeltaT);
                if (PreNeuron.ConductionVelocity < GlobalSettings.Epsilon)
                    duration = int.MaxValue;
                else
                {
                    duration = Math.Max((int)(distance / (PreNeuron.ConductionVelocity * PreNeuron.Model.RunParam.DeltaT)), 1);
                    duration += delay;
                }
            }
        }

        public void SetFixedDuration(double dur)
        {
            FixedDuration_ms = dur;
        }
        public void SetDelay(double delay)
        {
            Delay_ms = delay;
        }
        public void SetTimeLine(TimeLine span)
        {
            TimeLine_ms = span;
        }
        public void NextStep(int tIndex)
        {
            int tt = duration;
            double vPre = tt <= tIndex ? PreNeuron.V[tIndex - tt] : PreNeuron.RestingMembranePotential;
            double vPost = tIndex > 0 ? PostCell.V[tIndex - 1] : 0;
            (ISynA, ISynB) = Core.GetNextVal(vPre, vPost, ISynA, ISynB);
        }
        public double GetSynapticCurrent(int tIndex)
        {
            double current = IsActive(tIndex) ? ISyn : 0;
            InputCurrent[tIndex] = current;
            return current;
        }
    }

}
