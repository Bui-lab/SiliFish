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
using SiliFish.Services;
using System.Xml.Linq;
using OfficeOpenXml.ExternalReferences;

namespace SiliFish.ModelUnits.Junction
{
    public class ChemicalSynapse : JunctionBase
    {
        private int duration; //The number of time units (dt) it will take for the current to travel from one neuron to the other
                              //calculated at InitForSimulation, and used only during simulation

        private double ISynA = 0; //the momentary current value
        private double ISynB = 0; //the momentary current value
        [JsonIgnore]
        public double ISyn { get { return ISynA - ISynB; } }

        public TwoExp_syn Core { get; set; }

        protected override int nMax => PreNeuron.V.Length;

        [JsonIgnore]
        public SynapseParameters SynapseParameters
        {
            get
            {
                if (Core == null) return null;
                return new SynapseParameters()
                {
                    Erev = Core.ERev,
                    TauD = Core.TauD,
                    TauR = Core.TauR,
                    Vth = Core.Vth
                };
            }
            set
            {
                if (Core == null) return;
                Core.ERev = value.Erev;
                Core.TauD = value.TauD;
                Core.TauR = value.TauR;
                Core.Vth = value.Vth;
            }
        }
        public Neuron PreNeuron;
        public Cell PostCell; //can be a neuron or a muscle cell

        [JsonIgnore]
        public override string ID { get { return $"{PreNeuron.ID} → {PostCell.ID}; Conductance: {Weight:0.#####}"; } }

        public override List<string> ExportValues()
        {
            return ListBuilder.Build<string>(
            ConnectionType.Synapse, PreNeuron.ID, PostCell.ID,
                DistanceMode,
                SynapseParameters.ExportValues(),
                Weight, FixedDuration_ms, Delay_ms,
                Active,
                TimeLine_ms?.ExportValues());
        }

        public override void ImportValues(List<string> values)
        {
            try
            {
                int iter = 1;//junction type is already read before junction creation
                if (values.Count < ColumnNames.Count - TimeLine.ColumnNames.Count) return;
                Source = values[iter++].Trim();
                Target = values[iter++].Trim();

                DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), values[iter++]);
                Core = new TwoExp_syn();
                SynapseParameters synpar = new();
                synpar.ImportValues(values.Skip(iter).Take(SynapseParameters.ColumnNames.Count).ToList());
                SynapseParameters = synpar;
                iter += SynapseParameters.ColumnNames.Count;
                Weight = double.Parse(values[iter++]);
                Core.Conductance = Weight;
                if (double.TryParse(values[iter++], out double d))
                    FixedDuration_ms = d;
                Delay_ms = double.Parse(values[iter++]);
                Active = bool.Parse(values[iter++]);
                if (iter < values.Count)
                    TimeLine_ms.ImportValues(new[] { values[iter++] }.ToList());
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public ChemicalSynapse()
        { }
        public ChemicalSynapse(Neuron preN, Cell postN, SynapseParameters param, double conductance, DistanceMode distmode)
        {
            Core = new TwoExp_syn(param, conductance, preN.Model.RunParam.DeltaT, preN.Model.RunParam.DeltaTEuler);
            PreNeuron = preN;
            PostCell = postN;
            Source = preN.ID;
            Target = postN.ID;
            DistanceMode = distmode;
            Weight = conductance;
        }


        public ChemicalSynapse(ChemicalSynapse syn): base(syn)
        {
            Core = new TwoExp_syn(syn.Core as TwoExp_syn);
            PreNeuron = syn.PreNeuron;
            PostCell = syn.PostCell;
        }

        internal bool IsActive(int timepoint)
        {
            double t_ms = PreNeuron.Model.RunParam.GetTimeOfIndex(timepoint);
            return TimeLine_ms?.IsActive(t_ms) ?? true;
        }


        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            Core.CheckValues(ref errors);
            return errors.Count == 0;
        }
        public override void LinkObjects()
        {
            PreNeuron.Terminals.Add(this);
            if (PostCell is MuscleCell mc)
                mc.EndPlates.Add(this);
            else if (PostCell is Neuron n)
                n.Synapses.Add(this);
        }
        public override void UnlinkObjects()
        {
            PreNeuron.Terminals.Remove(this);
            if (PostCell is MuscleCell mc)
                mc.EndPlates.Remove(this);
            else if (PostCell is Neuron n)
                n.Synapses.Remove(this);
        }

        public override void LinkObjects(RunningModel model)
        {
            PreNeuron ??= model.GetCell(Source) as Neuron;
            Source ??= PreNeuron.ID;
            if (!PreNeuron.Terminals.Contains(this))
                PreNeuron.Terminals.Add(this);
            
            PostCell = model.GetCell(Target);
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
        public override void InitForSimulation(int nmax, bool trackCurrent)
        {
            base.InitForSimulation(nmax, trackCurrent);
            Core.Conductance = Weight;
            
            ISynA = ISynB = 0;
            
            if (FixedDuration_ms != null)
                duration = (int)(FixedDuration_ms / PreNeuron.Model.RunParam.DeltaT);
            else
            {
                double distance = Util.Distance(PreNeuron.Coordinate, PostCell.Coordinate, DistanceMode);
                int delay = (int)(Delay_ms / PreNeuron.Model.RunParam.DeltaT);
                duration = Math.Max((int)(distance / (PreNeuron.ConductionVelocity * PreNeuron.Model.RunParam.DeltaT)), 1);
                duration += delay;
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
        public override void NextStep(int tIndex)
        {
            if (!IsActive(tIndex))
            {
                ISynA = ISynB = 0;
                if (inputCurrent != null)
                    inputCurrent[tIndex] = 0;
                return;
            }
            int tt = duration;
            double vPreSynapse = tt <= tIndex ? PreNeuron.V[tIndex - tt] : PreNeuron.RestingMembranePotential;
            double vPost = tIndex > 0 ? PostCell.V[tIndex - 1] : PostCell.RestingMembranePotential;
            (ISynA, ISynB) = Core.GetNextVal(vPreSynapse, vPost, ISynA, ISynB);
            if (inputCurrent != null)
                inputCurrent[tIndex] = ISyn;
        }

    }

}
