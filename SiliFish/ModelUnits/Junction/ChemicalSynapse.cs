﻿using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Junction
{
    public class ChemicalSynapse: JunctionBase
    {
        private string target;//used to temporarily hold the target cell's id while reading the JSON files
        public string Target { get => PostCell.ID; set => target = value; }
        public TwoExp_syn Core { get; set; }
        public Neuron PreNeuron;
        public Cell PostCell; //can be a neuron or a muscle cell
        public int Duration { get; set; } //The number of time units (dt) it will take for the current to travel from presynaptic to postsynaptic neuron
        public int Delay { get; set; } = 0; //Extra number of time units (dt) to add to Duration
        [JsonIgnore]
        public double ISynA = 0; //the momentary current value
        [JsonIgnore]
        public double ISynB = 0; //the momentary current value
        [JsonIgnore]
        public double ISyn { get { return ISynA - ISynB; } }
        public double[] InputCurrent; //Current vector 
        [JsonIgnore]
        public override string ID { get { return $"Syn: {PreNeuron.ID} -> {PostCell.ID}; Conductance: {Conductance:0.#####}"; } }
        [JsonIgnore]
        public double Conductance { get { return Core.Conductance; } }
        internal bool IsActive(int timepoint)
        {
            double t_ms = RunParam.GetTimeOfIndex(timepoint);
            return TimeLine_ms?.IsActive(t_ms) ?? true;
        }

        public ChemicalSynapse()
        { }
        public ChemicalSynapse(Neuron preN, Cell postN, SynapseParameters param, double conductance, DistanceMode distmode)
        {
            Core = new TwoExp_syn(param, conductance);
            PreNeuron = preN;
            PostCell = postN;
            double distance = Util.Distance(PreNeuron.coordinate, PostCell.coordinate, distmode);
            Duration = Math.Max((int)(distance / (preN.ConductionVelocity * RunParam.static_dt)), 1);
        }

        public void LinkObjects(RunningModel model)
        {
            CellPool cp = model.CellPools.Where(cp => cp.Cells.Exists(c => c.ID == target)).FirstOrDefault();
            PostCell = cp.GetCell(target);
            if (PostCell is Neuron n)
                n.Synapses.Add(this);
            else if (PostCell is MuscleCell m)
                m.EndPlates.Add(this);
        }
        public override string ToString()
        {
            string activeStatus = Active && TimeLine_ms.IsBlank() ? "" : Active ? " (timeline)" : " (inactive)";
            return $"{ID}{activeStatus}";
        }
        public void InitDataVectors(int nmax)
        {
            InputCurrent = new double[nmax];
            InputCurrent[0] = 0;
        }

        public void SetFixedDuration(double dur)
        {
            Duration = (int)(dur / RunParam.static_dt);
        }
        public void SetDelay(double delay)
        {
            Delay = (int)(delay / RunParam.static_dt);
        }
        public void SetTimeLine(TimeLine span)
        {
            TimeLine_ms = span;
        }
        public void NextStep(int tIndex)
        {
            int tt = Duration + Delay;
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