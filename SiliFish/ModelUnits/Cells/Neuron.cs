using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
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
        public override IEnumerable<JunctionBase> Projections
        {
            get
            {
                IEnumerable<JunctionBase> gapJunctions = base.Projections;
                return Terminals.Union(Synapses).Union(gapJunctions);
            }
        }
        protected override string Discriminator => "neuron";

        public Neuron()
        {
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalSynapse>();
            Terminals = new List<ChemicalSynapse>();
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
            Core = CellCore.CreateCore(coreType, cellParams, model.RunParam.DeltaT);
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalSynapse>();
            Terminals = new List<ChemicalSynapse>();
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
        public override List<string> DiffersFrom(ModelUnitBase other)
        {
            if (other is not Neuron oc)
                return new() { $"Incompatible classes: {ID}({GetType()}) versus {other.ID}({other.GetType()})" };

            List<string> differences = base.DiffersFrom(other) ?? new();
            List<string> diffs = ListDiffersFrom(Terminals.Select(c => c as ModelUnitBase).ToList(), 
                oc.Terminals.Select(c => c as ModelUnitBase).ToList());
            if (diffs != null)
                differences.AddRange(diffs);
            diffs = ListDiffersFrom(Synapses.Select(c => c as ModelUnitBase).ToList(),
                oc.Synapses.Select(c => c as ModelUnitBase).ToList());
            if (diffs != null)
                differences.AddRange(diffs);

            if (differences.Any())
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
        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            int errorCount = errors.Count;
            Core.CheckValues(ref errors);
            foreach (ChemicalSynapse syn in Terminals)
                syn.CheckValues(ref errors);
            if (errors.Count > errorCount)
                errors.Insert(errorCount, $"{ID}:");
            return errors.Count == 0;
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
            Terminals = Terminals.OrderBy(jnc => jnc.PreNeuron.ID).ToList();
        }

        public override void SortJunctionsByTarget()
        {
            base.SortJunctionsByTarget();
            Terminals = Terminals.OrderBy(jnc => jnc.PostCell.ID).ToList();
        }
        #endregion

        #region Connection functions
        public ChemicalSynapse CreateChemicalSynapse(Cell postCell, string coreType, Dictionary<string, double> param, DistanceMode distanceMode)
        {
            ChemicalSynapse jnc = new(this, postCell, coreType, param, distanceMode);
            if (jnc == null) return null;
            Terminals.Add(jnc);
            postCell.AddChemicalSynapse(jnc);
            return jnc;
        }

        public override (double, double) GetConnectionRange()
        {
            (double minWeight1, double maxWeight1) = base.GetConnectionRange();
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
                while (Synapses.Any())
                {
                    ChemicalSynapse jnc = Synapses.First();
                    jnc.PreNeuron.RemoveJunction(jnc);
                    Synapses.Remove(jnc);
                }
            }
            if (chemout)
            {
                while (Terminals.Any())
                {
                    ChemicalSynapse jnc = Terminals.First();
                    jnc.PostCell.RemoveJunction(jnc);
                    Terminals.Remove(jnc);
                }
            }

        }

        public override bool HasConnections(bool gap, bool chemin, bool chemout)
        {
            return gap && GapJunctions.Any() ||
                chemin && Synapses.Any() ||
                chemout && Terminals.Any();
        }

        #endregion

        #region Runtime
        public override void InitForSimulation(RunParam runParam)
        {
            base.InitForSimulation(runParam);
            foreach (ChemicalSynapse jnc in this.Synapses)
                jnc.InitForSimulation(runParam.iMax, runParam.TrackJunctionCurrent, runParam.DeltaT);

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
                        if (jnc.IsActive(timeIndex))
                            IGap += jnc.Cell1 == this ? -1 * jnc.Core.ISyn : jnc.Core.ISyn;
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
            return Synapses != null && Synapses.Any() ? Synapses.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
        }
        public override double MaxSynInCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return Synapses != null && Synapses.Any() ? Synapses.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
        }

        public override double MinSynOutCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return Terminals != null && Terminals.Any() ? Terminals.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
        }
        public override double MaxSynOutCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return Terminals != null && Terminals.Any() ? Terminals.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
        }
        public override bool IsSpiking(int iStart = 0, int iEnd = -1)
        {
            return V.HasSpike(Core.Vthreshold, iStart, iEnd);
        }
        public override List<int> GetSpikeIndices(int iStart = 0, int iEnd = -1, int buffer = 0)
        {
            return V.GetSpikeIndices(Core.Vmax, iStart, iEnd, buffer);
        }
        public override (Dictionary<string, Color>, Dictionary<string, List<double>>) GetIncomingSynapticCurrents()
        {
            Dictionary<string, Color> colors = new();
            Dictionary<string, List<double>> AffarentCurrents = new();
            foreach (ChemicalSynapse jnc in Synapses)
            {
                colors.TryAdd(jnc.PreNeuron.ID, jnc.PreNeuron.CellPool.Color);
                AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
            }
            return (colors, AffarentCurrents);
        }
        public override (Dictionary<string, Color>, Dictionary<string, List<double>>) GetOutgoingSynapticCurrents()
        {
            Dictionary<string, Color> colors = new();
            Dictionary<string, List<double>> EfferentCurrents = new();
            foreach (ChemicalSynapse jnc in Terminals)
            {
                colors.TryAdd(jnc.PostCell.ID, jnc.PostCell.CellPool.Color);
                EfferentCurrents.AddObject(jnc.PostCell.ID, jnc.InputCurrent.ToList());
            }
            return (colors, EfferentCurrents);
        }
        #endregion
    }
}