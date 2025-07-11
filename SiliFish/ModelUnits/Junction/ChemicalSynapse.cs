﻿using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Junction
{
    public class ChemicalSynapse : JunctionBase
    {
        public Neuron PreNeuron;
        public Cell PostCell; //can be a neuron or a muscle cell
        private int duration; //The number of time units (dt) it will take for the current to travel from one neuron to the other
                              //calculated at InitForSimulation, and used only during simulation
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0305:Simplify collection initialization",
            Justification = "V.ToArray() exists to be ready for future DBMemory feature")]
        protected override int nMax => PreNeuron?.V.ToArray().Length ?? 0;
        protected override double dt => PreNeuron?.Model.DeltaT ?? 0.1;
        public override int iDuration => duration;

        public override double Duration_ms
        {
            get
            {
                if (FixedDuration_ms != null)
                    return (double)FixedDuration_ms;
                else
                {
                    double distance = Util.Distance(PreNeuron.Coordinate, PostCell.Coordinate, DistanceMode);
                    return (distance / PreNeuron.ConductionVelocity) + (Delay_ms ?? PreNeuron.Model.Settings.synaptic_delay);
                }
            }
        }

        public override string SourcePool
        {
            get => PreNeuron.CellPool.ID;
            set { }
        }
        public override string TargetPool
        {
            get => PostCell.CellPool.ID;
            set { }
        }
        [JsonIgnore]
        public override string ID { get { return $"{PreNeuron.ID} → {PostCell.ID}"; } }

        public override List<string> ExportValues()
        {
            return ListBuilder.Build<string>(ID,
            PreNeuron.ID, PostCell.ID,
            JunctionType.Synapse, Core.SynapseType,
            csvExportCoreValues,
                DistanceMode,
                FixedDuration_ms, Delay_ms, Duration_ms,
                Active,
                TimeLine_ms?.ExportValues());
        }

        public override void ImportValues(List<string> values)
        {
            try
            {
                int iter = 1;//skip the first id column
                if (values.Count < ColumnNames.Count - TimeLine.ColumnNames.Count) return;
                Source = values[iter++].Trim();
                Target = values[iter++].Trim();
                iter++; //junction type is already read before junction creation
                string coreType = values[iter++].Trim();
                Dictionary<string, double> parameters = [];
                for (int i = 1; i <= JunctionCore.CoreParamMaxCount; i++)
                {
                    if (iter > values.Count - 2) break;
                    string paramkey = values[iter++].Trim();
                    if (double.TryParse(values[iter++].Trim(), out double paramvalue) && !string.IsNullOrEmpty(paramkey))
                    {
                        parameters.Add(paramkey, paramvalue);
                    }
                }
                Core = ChemSynapseCore.CreateCore(coreType, parameters);

                DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), values[iter++]);

                if (double.TryParse(values[iter++], out double d))
                    FixedDuration_ms = d;
                if (double.TryParse(values[iter++], out double dd))
                    Delay_ms = dd;
                iter++;//Duration_ms is readonly
                Active = bool.Parse(values[iter++]);
                if (iter < values.Count)
                    TimeLine_ms.ImportValues([values[iter++]]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public ChemicalSynapse()
        { }
        public ChemicalSynapse(Neuron preN, Cell postN, string coreType, Dictionary<string, double> synParams, DistanceMode distmode)
        {
            Core = ChemSynapseCore.CreateCore(coreType, synParams);

            PreNeuron = preN;
            PostCell = postN;
            Source = preN.ID;
            Target = postN.ID;
            DistanceMode = distmode;
        }


        public ChemicalSynapse(ChemicalSynapse syn) : base(syn)
        {
            Core = ChemSynapseCore.CreateCore(syn.Core as ChemSynapseCore);
            Core = new SimpleSyn(syn.Core as SimpleSyn);
            PreNeuron = syn.PreNeuron;
            PostCell = syn.PostCell;
        }


        public override bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            errors ??= [];
            warnings ??= [];
            int preCount = errors.Count + warnings.Count;
            base.CheckValues(ref errors, ref warnings);
            Core.CheckValues(ref errors, ref warnings);
            return errors.Count + warnings.Count == preCount;
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
        }
        public override string ToString()
        {
            string ntmode = PreNeuron.CellPool.CellOutputMode == CellOutputMode.Cholinergic ? "C" :
                PreNeuron.CellPool.CellOutputMode == CellOutputMode.Excitatory ? "+" :
                PreNeuron.CellPool.CellOutputMode == CellOutputMode.Inhibitory ? "-" :
                PreNeuron.CellPool.CellOutputMode == CellOutputMode.Modulatory ? "M" :
                "?";
            string activeStatus = Active && TimeLine_ms.IsBlank() ? "" : Active ? " (timeline)" : " (inactive)";
            return $"({ntmode}) {ID}{activeStatus}";
        }
        public override void InitForSimulation(RunParam runParam, ref int uniqueID)
        {
            base.InitForSimulation(runParam, ref uniqueID);
            RunningModel model = PreNeuron.Model;
            Core.InitForSimulation(dt, ref uniqueID);

            if (FixedDuration_ms != null)
                duration = (int)(FixedDuration_ms / dt);
            else
            {
                double distance = Util.Distance(PreNeuron.Coordinate, PostCell.Coordinate, DistanceMode);
                duration = Math.Max((int)Math.Round(distance / (PreNeuron.ConductionVelocity * dt)), 1);
            }
            int delay = (int)((Delay_ms ?? model.Settings.synaptic_delay) / dt);
            duration += delay;
        }

        public override (int source, int target) GetCellIndices()
        {
            int source = PreNeuron.CellPool.BodyLocation == BodyLocation.SupraSpinal ? PreNeuron.Sequence : PreNeuron.Somite;
            int target = PostCell.CellPool.BodyLocation == BodyLocation.SupraSpinal ? PostCell.Sequence : PostCell.Somite;
            return (source, target);
        }
        public override void NextStep(int tIndex)
        {
            if (!IsActive(tIndex))
            {
                Core.ZeroISyn();
                if (inputCurrent != null)
                    inputCurrent[tIndex] = 0;
                return;
            }
            int tt = duration;
            double vPreSynapse = tt <= tIndex ? PreNeuron.V[tIndex - tt] : PreNeuron.RestingMembranePotential;
            double vPost = tIndex > 0 ? PostCell.V[tIndex - 1] : PostCell.RestingMembranePotential;
            bool excitatory = PreNeuron.CellPool.NTMode == NeuronClass.Glutamatergic || PreNeuron.CellPool.NTMode == NeuronClass.Cholinergic;
            double ISyn;
            DynamicsParam dynamics = PreNeuron.Model.DynamicsParam;
            List<double> spikeArrivalTimes = PreNeuron.SpikeTrain.Select(t => (t + tt) * Core.DeltaT).ToList();
            ISyn = (Core as ChemSynapseCore).GetNextVal(vPreSynapse, vPost, spikeArrivalTimes, tIndex * Core.DeltaT, dynamics, excitatory);
            if (inputCurrent != null)
                inputCurrent[tIndex] = ISyn;
        }

        public override bool IsActive(int timepoint)
        {
            return PreNeuron.IsActive(timepoint) && base.IsActive(timepoint);
        }

    }

}
