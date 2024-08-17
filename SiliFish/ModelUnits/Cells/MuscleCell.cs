using SiliFish.Database;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    public class MuscleCell : Cell
    {
        private double[] tension = null;

        [JsonIgnore]
        public double R
        {
            get
            {
                if (Core is Leaky_Integrator clr)
                    return clr.R;
                if (Core is LeakyIntegrateAndFire clrf)
                    return clrf.R;
                return 0;
            }
        }        
        
        [JsonIgnore]
        public double[] RelativeTension { get { return (Core as ContractibleCellCore).CalculateRelativeTension(V); } }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0305:Simplify collection initialization",
            Justification = "ToArray exists to be ready for future DBMemory feature")]
        public double[] Tension 
        { 
            get 
            {
                tension ??= (Core as ContractibleCellCore).CalculateTension(V.ToArray());
                return tension;
            } 
        }
        public override double RestingMembranePotential { get { return (Core?.Vr) ?? 0; } }

        [JsonIgnore]
        public List<ChemicalSynapse> EndPlates { get; set; } //keeps the list of all synapses targeting the current cell

        [JsonIgnore]
        public override IEnumerable<JunctionBase> Junctions
        {
            get
            {
                IEnumerable<JunctionBase> gapJunctions = base.Junctions;
                return EndPlates.Union(gapJunctions);
            }
        }
        protected override string Discriminator => "musclecell";

        public MuscleCell()
        {
            GapJunctions = [];
            EndPlates = [];
        }
        /// <summary>
        /// Used as a template
        /// </summary>
        public MuscleCell(RunningModel model, string coreType, string group, int somite, int seq, Dictionary<string, double> cellParams, double cv,
            double ascAxon, double descAxon, TimeLine timeline = null)
        {
            Model = model;
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            Core = CellCore.CreateCore(coreType, cellParams, model.Settings.SimulationDeltaT);
            EndPlates = [];
            GapJunctions = [];
            ConductionVelocity = cv;
            AscendingAxonLength = ascAxon;
            DescendingAxonLength = descAxon;
            TimeLine_ms = timeline;
        }
        public MuscleCell(RunningModel model, CellPoolTemplate cellTemp, int somite, int seq, Dictionary<string, double> cellParams, double cv, double ascAxon, double descAxon)
            : this(model, cellTemp.CoreType, cellTemp.CellGroup, somite, seq, cellParams, cv, ascAxon, descAxon, cellTemp.TimeLine_ms)
        {
        }
        public MuscleCell(RunningModel model, string coreType, string group)
            : this(model, coreType, group, -1, -1, null, 0, 0, 0)
        {
        }
        public override ModelUnitBase CreateCopy()
        {
            MuscleCell muscleCell = new()
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
            return muscleCell;
        }

        public override List<Difference> DiffersFrom(ModelUnitBase other)
        {
            if (other is not MuscleCell oc)
                return [new Difference("Incompatible classes", $"{ID}({GetType()}", $"{other.ID}{other.GetType()}")];
            
            List<Difference> differences = base.DiffersFrom(other) ?? [];
            List<Difference> diffs = ListDiffersFrom(EndPlates.Select(c => c as ModelUnitBase).ToList(),
                oc.EndPlates.Select(c => c as ModelUnitBase).ToList());
            if (diffs != null)
                differences.AddRange(diffs);

            if (differences.Count != 0)
                return differences;
            return null;
        }

        #region Junction functions
        public override int TimeDistance()
        {
            return Math.Max(EndPlates?.Select(junc => junc.iDuration).DefaultIfEmpty().Max() ?? 0, base.TimeDistance());
        }
        public override void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            EndPlates.Add(jnc);
        }

        public override (double, double) GetJunctionRange()
        {
            (double minWeight1, double maxWeight1) = base.GetJunctionRange();
            double maxWeight2 = EndPlates.Select(j => j.Core.Conductance).DefaultIfEmpty(0).Max();
            double minWeight2 = EndPlates.Where(j => j.Core.Conductance > 0).Select(j => j.Core.Conductance).DefaultIfEmpty(999).Min();

            double maxWeight = Math.Max(maxWeight1, maxWeight2);
            double minWeight = Math.Min(minWeight1, minWeight2);
            return (minWeight, maxWeight);
        }

        internal override bool RemoveJunction(JunctionBase junction)
        {
            if (base.RemoveJunction(junction))
                return true;
            if (junction is ChemicalSynapse syn)
            {
                if (EndPlates.Remove(syn))
                    return true;
            }
            return false;
        }

        public override void DeleteJunctions(bool gap = true, bool chemin = true, bool chemout = true)
        {
            base.DeleteJunctions(gap, chemin, chemout);
            if (!chemin) return;
            while (EndPlates.Count != 0)
            {
                ChemicalSynapse jnc = EndPlates.First();
                jnc.PreNeuron.Terminals.Remove(jnc);
                EndPlates.Remove(jnc);
            }
        }
        public override bool HasJunctions(bool gap, bool chemin, bool chemout)
        {
            return gap && GapJunctions.Count != 0 ||
                chemin && EndPlates.Count != 0;
        }

        #endregion

        #region Runtime

        public override void MemoryAllocation(RunParam runParam, SimulationDBLink dBLink)
        {
            base.MemoryAllocation(runParam, dBLink);
            foreach (ChemicalSynapse jnc in EndPlates)
                jnc.MemoryAllocation(runParam, dBLink);
        }
        public override double MemoryFlush()
        {
            double cleared = base.MemoryFlush();
            foreach (ChemicalSynapse jnc in EndPlates)
                cleared += jnc.MemoryFlush();
            return cleared;
        }
        public override void InitForSimulation(RunParam runParam, ref int uniqueID)
        {
            base.InitForSimulation(runParam, ref uniqueID);
            tension = null;
            foreach (ChemicalSynapse jnc in EndPlates)
                jnc.InitForSimulation(runParam, ref uniqueID);
        }
        public override void FinalizeSimulation(RunParam runParam, SimulationDBLink dbLink)
        {
            base.FinalizeSimulation(runParam, dbLink);
            foreach (ChemicalSynapse jnc in EndPlates)
                jnc.FinalizeSimulation(runParam, dbLink);
        }
        public override void CalculateMembranePotential(int timeIndex)
        {
            double ISyn = 0, IGap = 0, stim = 0;
            double minV = 0;
            if (IsActive(timeIndex))
            {
                foreach (ChemicalSynapse syn in EndPlates)
                {
                    if (syn.IsActive(timeIndex))
                    {
                        ISyn += syn.Core.ISyn;
                        ChemSynapseCore cs = syn.Core as ChemSynapseCore;
                        if (cs.ERev < minV)
                            minV = cs.ERev;
                    }
                    ISyn += syn.Core.ISyn;
                }
                foreach (GapJunction jnc in GapJunctions)
                {
                    if (jnc.IsActive(timeIndex))
                        IGap += jnc.Cell1 == this ? jnc.Core.ISyn : -1 * jnc.Core.ISyn;
                }
                stim = GetStimulus(timeIndex);
            }
            if (minV == 0) minV = GlobalSettings.BiologicalMinPotential;
            NextStep(timeIndex, stim + ISyn + IGap, minV, GlobalSettings.BiologicalMaxPotential);
        }
        #endregion

        #region Simulation Stats

        public override double MinSynInCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return EndPlates?.Count > 0 ? EndPlates.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
        }
        public override double MaxSynInCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return EndPlates?.Count > 0 ? EndPlates.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
        }

        public override double MinSynOutCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return 0;
        }
        public override double MaxSynOutCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return 0;
        }


        public override (Dictionary<string, Color>, Dictionary<string, List<double>>) GetIncomingSynapticCurrents()
        {
            Dictionary<string, Color> colors = [];
            Dictionary<string, List<double>> AffarentCurrents = [];
            foreach (ChemicalSynapse jnc in EndPlates)
            {
                colors.TryAdd(jnc.PreNeuron.ID, jnc.PostCell.CellPool.Color);
                AffarentCurrents.AddObject(jnc.PreNeuron.ID, [.. jnc.InputCurrent]);
            }
            return (colors, AffarentCurrents);
        }

        public override (Dictionary<string, Color>, Dictionary<string, List<double>>) GetOutgoingSynapticCurrents()
        {
            return (null, null);
        }
        #endregion


    }

}
