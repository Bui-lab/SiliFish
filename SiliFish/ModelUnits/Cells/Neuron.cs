﻿using SiliFish.Database;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    public class Neuron : Cell
    {
        //Writing only Terminal to JSON file is enough and prevents duplication
        [JsonPropertyOrder(2)]
        public List<ChemicalSynapse> Terminals { get; set; } //keeps the list of all synapses the current cells extends to
        [JsonIgnore]
        public List<ChemicalSynapse> Synapses { get; set; } //keeps the list of all synapses targeting the current cell

        [JsonIgnore]
        public override double RestingMembranePotential { get { return (Core?.Vr) ?? 0; } }

        [JsonIgnore]
        public override IEnumerable<JunctionBase> Junctions
        {
            get
            {
                IEnumerable<JunctionBase> gapJunctions = base.Junctions;
                return Terminals.Union(Synapses).Union(gapJunctions);
            }
        }
        protected override string Discriminator => "neuron";

        public Neuron()
        {
            GapJunctions = [];
            Synapses = [];
            Terminals = [];
        }
        /// <summary>
        /// Used as a template
        /// </summary>
        public Neuron(RunningModel model, string coreType, string group, int somite, int seq,
            Dictionary<string, double> cellParams, double cv, double ascAxon, double descAxon,
            TimeLine timeline = null)
        {
            Model = model;
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            Core = CellCore.CreateCore(coreType, cellParams, model.SimulationSettings.SimulationDeltaT);
            GapJunctions = [];
            Synapses = [];
            Terminals = [];
            TimeLine_ms = timeline;
            ConductionVelocity = cv;
            AscendingAxonLength = ascAxon;
            DescendingAxonLength = descAxon;
        }
        public Neuron(RunningModel model, CellPoolTemplate cellTemp, int somite, int seq, Dictionary<string, double> cellParams, double cv, double ascAxon, double descAxon)
            : this(model, cellTemp.CoreType, cellTemp.CellGroup, somite, seq, cellParams, cv, ascAxon, descAxon, cellTemp.TimeLine_ms)
        {
        }

        public Neuron(RunningModel model, string coreType, string group)
            : this(model, coreType, group, -1, -1, null, 0, 0, 0)
        {
        }

        public override ModelUnitBase CreateCopy()
        {
            Neuron neuron = new()
            {
                CellPool = CellPool,
                CellGroup = CellGroup,
                Core = CellCore.CreateCore(this.Core),
                Coordinate = Coordinate,
                Model = Model,
                ConductionVelocity = ConductionVelocity,
                Somite = Somite,
                Sequence = CellPool.GetMaxCellSequence(Somite) + 1,
                PositionLeftRight = PositionLeftRight,
                TimeLine_ms = new(TimeLine_ms)
            };
            return neuron;
        }
        public override List<Difference> DiffersFrom(ModelUnitBase other)
        {
            if (other is not Neuron oc)
                return [new Difference("Incompatible classes", $"{ID}({GetType()}", $"{other.ID}{other.GetType()}")];

            List<Difference> differences = base.DiffersFrom(other) ?? [];
            List<Difference> diffs = ListDiffersFrom(Terminals.Select(c => c as ModelUnitBase).ToList(),
                oc.Terminals.Select(c => c as ModelUnitBase).ToList());
            if (diffs != null)
                differences.AddRange(diffs);
            diffs = ListDiffersFrom(Synapses.Select(c => c as ModelUnitBase).ToList(),
                oc.Synapses.Select(c => c as ModelUnitBase).ToList());
            if (diffs != null)
                differences.AddRange(diffs);

            if (differences.Count != 0)
                return differences;
            return null;
        }

        public override void LinkObjects(RunningModel model, CellPool pool)
        {
            base.LinkObjects(model, pool);
            foreach (ChemicalSynapse jnc in Terminals.Where(t => t.PreNeuron == null))
            {
                jnc.PreNeuron = this;
                jnc.LinkObjects(model);
            }
        }
        public override bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            int preErrorCount = errors.Count;
            int preWarningCount = warnings.Count;
            base.CheckValues(ref errors, ref warnings);
            Core.CheckValues(ref errors, ref warnings);
            foreach (ChemicalSynapse syn in Terminals)
            {
                if (!syn.CheckValues(ref errors, ref warnings))
                {
                    if (errors.Count > preErrorCount)
                        errors.Insert(preErrorCount, $"{syn.ID}:");
                    if (warnings.Count > preWarningCount)
                        warnings.Insert(preWarningCount, $"{syn.ID}:");
                }
            }
            return errors.Count == preErrorCount && warnings.Count == preWarningCount;
        }
        #region Sort functions
        public override void SortJunctions()
        {
            base.SortJunctions();
            Terminals.Sort();
        }
        public override void SortJunctionsBySource()
        {
            base.SortJunctionsBySource();
            Terminals = [.. Terminals.OrderBy(jnc => jnc.PreNeuron.ID)];
        }

        public override void SortJunctionsByTarget()
        {
            base.SortJunctionsByTarget();
            Terminals = [.. Terminals.OrderBy(jnc => jnc.PostCell.ID)];
        }
        #endregion

        #region Junction functions
        public override int TimeDistance()
        {
            return Math.Max(Terminals?.Select(junc => junc.iDuration).DefaultIfEmpty().Max() ?? 0, base.TimeDistance());
        }

        public ChemicalSynapse CreateChemicalSynapse(Cell postCell, string coreType, Dictionary<string, double> param, DistanceMode distanceMode)
        {
            ChemicalSynapse jnc = new(this, postCell, coreType, param, distanceMode);
            if (jnc == null) return null;
            Terminals.Add(jnc);
            postCell.AddChemicalSynapse(jnc);
            return jnc;
        }

        public override (double, double) GetJunctionRange()
        {
            (double minWeight1, double maxWeight1) = base.GetJunctionRange();
            double maxWeight2 = Synapses.Select(j => j.Core.Conductance).DefaultIfEmpty(0).Max();
            double minWeight2 = Synapses.Where(j => j.Core.Conductance > 0).Select(j => j.Core.Conductance).DefaultIfEmpty(999).Min();
            double maxWeight = Math.Max(maxWeight1, maxWeight2);
            double minWeight = Math.Min(minWeight1, minWeight2);
            return (minWeight, maxWeight);
        }
        public override void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            Synapses.Add(jnc);
        }

        internal override bool RemoveJunction(JunctionBase junction)
        {
            if (base.RemoveJunction(junction))
                return true;
            if (junction is ChemicalSynapse syn)
            {
                if (Synapses.Remove(syn))
                    return true;
                if (Terminals.Remove(syn))
                    return true;
            }
            return false;
        }

        public override void DeleteJunctions(bool gap = true, bool chemin = true, bool chemout = true)
        {
            base.DeleteJunctions(gap, chemin, chemout);
            if (chemin)
            {
                while (Synapses.Count != 0)
                {
                    ChemicalSynapse jnc = Synapses.First();
                    jnc.PreNeuron.RemoveJunction(jnc);
                    Synapses.Remove(jnc);
                }
            }
            if (chemout)
            {
                while (Terminals.Count != 0)
                {
                    ChemicalSynapse jnc = Terminals.First();
                    jnc.PostCell.RemoveJunction(jnc);
                    Terminals.Remove(jnc);
                }
            }

        }

        public override bool HasJunctions(bool gap, bool chemin, bool chemout)
        {
            return gap && GapJunctions.Count != 0 ||
                chemin && Synapses.Count != 0 ||
                chemout && Terminals.Count != 0;
        }

        #endregion

        #region Runtime

        public override void MemoryAllocation(RunParam runParam, SimulationDBLink dBLink)
        {
            base.MemoryAllocation(runParam, dBLink);
            foreach (ChemicalSynapse jnc in Synapses)
                jnc.MemoryAllocation(runParam, dBLink);
        }
        public override double MemoryFlush()
        {
            double cleared = base.MemoryFlush();
            foreach (ChemicalSynapse jnc in Synapses)
                cleared += jnc.MemoryFlush();
            return cleared;
        }
        public override void InitForSimulation(RunParam runParam, ref int uniqueID)
        {
            base.InitForSimulation(runParam, ref uniqueID);
            foreach (ChemicalSynapse jnc in Synapses)
                jnc.InitForSimulation(runParam, ref uniqueID);
        }

        public override void FinalizeSimulation(RunParam runParam, SimulationDBLink dbLink)
        {
            base.FinalizeSimulation(runParam, dbLink);
            foreach (ChemicalSynapse jnc in Synapses)
                jnc.FinalizeSimulation(runParam, dbLink);
        }
        public override void CalculateCellularOutputs(int t)
        {
            try
            {
                base.CalculateCellularOutputs(t);
                foreach (ChemicalSynapse syn in Terminals.Where(t => t.Active))
                {
                    if (syn.IsActive(t))
                        syn.NextStep(t);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public override void CalculateMembranePotential(int timeIndex)
        {
            try
            {
                double ISyn = 0, IGap = 0, stim = 0;
                double minV = 0;
                if (IsActive(timeIndex))
                {
                    foreach (ChemicalSynapse syn in Synapses.Where(syn => syn.Active))
                    {
                        if (syn.IsActive(timeIndex))
                        {
                            ISyn += syn.Core.ISyn;
                            ChemSynapseCore cs = syn.Core as ChemSynapseCore;
                            if (cs.ERev < minV)
                                minV = cs.ERev;
                        }
                    }
                    foreach (GapJunction jnc in GapJunctions.Where(jnc => jnc.Active))
                    {
                        //if this is the source of a unidirectinoal gap jnc, skip
                        if (!(jnc.Core as ElecSynapseCore).bidirectional && jnc.Cell1 == this)
                            continue;
                        if (jnc.IsActive(timeIndex))
                            IGap += jnc.Cell1 == this ? (jnc.Core as ElecSynapseCore).ISynBackward : (jnc.Core as ElecSynapseCore).ISynForward;
                    }
                    stim = GetStimulus(timeIndex);
                }
                if (minV == 0) minV = GlobalSettings.BiologicalMinPotential;
                NextStep(timeIndex, stim + ISyn + IGap, minV, GlobalSettings.BiologicalMaxPotential);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        #endregion

        #region Simulation Stats

        public override double MinSynInCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return Synapses != null && Synapses.Count != 0 ? Synapses.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
        }
        public override double MaxSynInCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return Synapses != null && Synapses.Count != 0 ? Synapses.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
        }

        public override double MinSynOutCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return Terminals != null && Terminals.Count != 0 ? Terminals.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
        }
        public override double MaxSynOutCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return Terminals != null && Terminals.Count != 0 ? Terminals.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
        }

        public override (Dictionary<string, Color>, Dictionary<string, List<double>>) GetIncomingSynapticCurrents()
        {
            Dictionary<string, Color> colors = [];
            Dictionary<string, List<double>> AffarentCurrents = [];
            foreach (ChemicalSynapse jnc in Synapses)
            {
                colors.TryAdd(jnc.PreNeuron.ID, jnc.PreNeuron.CellPool.Color);
                AffarentCurrents.AddObject(jnc.PreNeuron.ID, [.. jnc.InputCurrent]);
            }
            return (colors, AffarentCurrents);
        }
        public override (Dictionary<string, Color>, Dictionary<string, List<double>>) GetOutgoingSynapticCurrents()
        {
            Dictionary<string, Color> colors = [];
            Dictionary<string, List<double>> EfferentCurrents = [];
            foreach (ChemicalSynapse jnc in Terminals)
            {
                colors.TryAdd(jnc.PostCell.ID, jnc.PostCell.CellPool.Color);
                EfferentCurrents.AddObject(jnc.PostCell.ID, [.. jnc.InputCurrent]);
            }
            return (colors, EfferentCurrents);
        }
        #endregion
    }
}