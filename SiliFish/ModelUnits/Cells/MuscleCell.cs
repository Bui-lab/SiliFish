using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    public class MuscleCell : Cell
    {
        //TODO muscle cell assumed to be LeakyIntegrator
        private double[] tension = null;

        [JsonIgnore]
        public double R { get { return (Core as Leaky_Integrator).R; } set { } }
        [JsonIgnore]
        public double[] RelativeTension { get { return (Core as Leaky_Integrator).CalculateRelativeTension(V); } }
        [JsonIgnore]
        public double[] Tension 
        { 
            get 
            { 
                tension ??= (Core as Leaky_Integrator).CalculateTension(V);
                return tension;
            } 
        }
        public override double RestingMembranePotential { get { return (Core?.Vr) ?? 0; } }

        [JsonIgnore]
        public List<ChemicalSynapse> EndPlates { get; set; } //keeps the list of all synapses targeting the current cell

        [JsonIgnore]
        public override IEnumerable<JunctionBase> Projections
        {
            get
            {
                IEnumerable<JunctionBase> gapJunctions = base.Projections;
                return EndPlates.Union(gapJunctions);
            }
        }
        protected override string Discriminator => "musclecell";

        public MuscleCell()
        {
            GapJunctions = new();
            EndPlates = new();
        }
        /// <summary>
        /// Used as a template
        /// </summary>
        public MuscleCell(RunningModel model, string coreType, string group, int somite, int seq, Dictionary<string, double> cellParams, double cv, TimeLine timeline = null)
        {
            Model = model;
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            Core = CellCoreUnit.CreateCore(coreType, cellParams, model.RunParam.DeltaT, model.RunParam.DeltaTEuler);
            EndPlates = new List<ChemicalSynapse>();
            GapJunctions = new List<GapJunction>();
            ConductionVelocity = cv;
            TimeLine_ms = timeline;
        }
        public MuscleCell(RunningModel model, CellPoolTemplate cellTemp, int somite, int seq, Dictionary<string, double> cellParams, double cv)
            : this(model, cellTemp.CoreType, cellTemp.CellGroup, somite, seq, cellParams, cv, cellTemp.TimeLine_ms)
        {
        }
        public override ModelUnitBase CreateCopy()
        {
            MuscleCell muscleCell = new()
            {
                CellPool = CellPool,
                CellGroup = CellGroup,
                Core = CellCoreUnit.CreateCore(this.Core),
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

        #region Connection functions
        public override void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            EndPlates.Add(jnc);
        }

        public override (double, double) GetConnectionRange()
        {
            (double minWeight1, double maxWeight1) = base.GetConnectionRange();
            double maxWeight2 = EndPlates.Select(j => j.Weight).DefaultIfEmpty(0).Max();
            double minWeight2 = EndPlates.Where(j => j.Weight > 0).Select(j => j.Weight).DefaultIfEmpty(999).Min();

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
            while (EndPlates.Any())
            {
                ChemicalSynapse jnc = EndPlates.First();
                jnc.PreNeuron.Terminals.Remove(jnc);
                EndPlates.Remove(jnc);
            }
        }
        public override bool HasConnections(bool gap, bool chemin, bool chemout)
        {
            return gap && GapJunctions.Any() ||
                chemin && EndPlates.Any();
        }

        #endregion

        #region Runtime
        public override void InitForSimulation(RunParam runParam)
        {
            base.InitForSimulation(runParam);
            tension = null;
            V = Enumerable.Repeat(Core.Vr, runParam.iMax).ToArray();
            foreach (ChemicalSynapse jnc in this.EndPlates)
                jnc.InitForSimulation(runParam.iMax);
        }
        public void NextStep(int t, double stim)
        {
            bool spike = false;
            V[t] = Core.GetNextVal(stim, ref spike);
        }
        public override void CalculateMembranePotential(int timeIndex)
        {
            double ISyn = 0, IGap = 0, stim = 0;
            if (IsAlive(timeIndex))
            {
                foreach (ChemicalSynapse syn in EndPlates)
                {
                    ISyn += syn.GetSynapticCurrent(timeIndex);
                }
                foreach (GapJunction jnc in GapJunctions)
                {
                    IGap += jnc.GetGapCurrent(this, timeIndex);
                }
                stim = GetStimulus(timeIndex);
            }
            NextStep(timeIndex, stim + ISyn + IGap);
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

        public override bool IsSpiking(int iStart = 0, int iEnd = -1)
        {
            return V.HasSpike(Core.Vthreshold, iStart, iEnd);
        }
        public override List<int> GetSpikeIndices(int iStart = 0, int iEnd = -1)
        {
            return V.GetSpikeIndices(Core.Vthreshold * 1.1, iStart, iEnd);//URGENT this is a random multiplier 
        }
        public override (Dictionary<string, Color>, Dictionary<string, List<double>>) GetIncomingSynapticCurrents()
        {
            Dictionary<string, Color> colors = new();
            Dictionary<string, List<double>> AffarentCurrents = new();
            foreach (ChemicalSynapse jnc in EndPlates)
            {
                colors.TryAdd(jnc.PreNeuron.ID, jnc.PostCell.CellPool.Color);
                AffarentCurrents.AddObject(jnc.PreNeuron.ID, jnc.InputCurrent.ToList());
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
