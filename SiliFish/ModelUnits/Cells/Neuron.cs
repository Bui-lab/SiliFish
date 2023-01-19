using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
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
        public Neuron()
        {
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalSynapse>();
            Terminals = new List<ChemicalSynapse>();
        }
        /// <summary>
        /// Used as a template
        /// </summary>
        public Neuron(string coreType, string group, int somite, int seq, Dictionary<string, double> cellParams, double cv, TimeLine timeline = null)
        {
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            Core = CellCoreUnit.CreateCore(coreType, cellParams);
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalSynapse>();
            Terminals = new List<ChemicalSynapse>();
            TimeLine_ms = timeline;
            ConductionVelocity = cv;
        }
        public Neuron(CellPoolTemplate cellTemp, int somite, int seq, Dictionary<string, double> cellParams, double cv)
            : this(cellTemp.CoreType, cellTemp.CellGroup, somite, seq, cellParams, cv, cellTemp.TimeLine_ms)
        {
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
        public ChemicalSynapse CreateChemicalSynapse(Cell postCell, SynapseParameters param, double conductance, DistanceMode distanceMode)
        {
            ChemicalSynapse jnc = new(this, postCell, param, conductance, distanceMode);
            if (jnc == null) return null;
            Terminals.Add(jnc);
            postCell.AddChemicalSynapse(jnc);
            return jnc;
        }

        public override (double, double) GetConnectionRange()
        {
            (double minWeight1, double maxWeight1) = base.GetConnectionRange();
            double maxWeight2 = Synapses.Select(j => j.Conductance).DefaultIfEmpty(0).Max();
            double minWeight2 = Synapses.Where(j => j.Conductance > 0).Select(j => j.Conductance).DefaultIfEmpty(999).Min();
            double maxWeight = Math.Max(maxWeight1, maxWeight2);
            double minWeight = Math.Min(minWeight1, minWeight2);
            return (minWeight, maxWeight);
        }
        public override void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            Synapses.Add(jnc);
        }
        #endregion

        #region Runtime
        public override void InitDataVectors(int nmax)
        {
            base.InitDataVectors(nmax);
            V[0] = Core.Vr;
            foreach (ChemicalSynapse jnc in this.Synapses)
                jnc.InitDataVectors(nmax);

        }

        public void NextStep(int t, double stim)
        {
            bool spike = false;
            V[t] = Core.GetNextVal(stim, ref spike);
        }

        public override void CalculateCellularOutputs(int t)
        {
            try
            {
                base.CalculateCellularOutputs(t);
                foreach (ChemicalSynapse syn in Terminals)
                {
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
                if (IsAlive(timeIndex))
                {
                    foreach (ChemicalSynapse syn in Synapses)
                    {
                        ISyn += syn.GetSynapticCurrent(timeIndex);
                    }
                    foreach (GapJunction jnc in GapJunctions)
                    {
                        IGap += jnc.GetGapCurrent(this, timeIndex);
                    }
                    stim = GetStimulus(timeIndex, RunningModel.rand);
                }

                NextStep(timeIndex, stim + ISyn + IGap);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        #endregion

        #region Simulation Stats
        public override double MinCurrentValue(int iStart = 0, int iEnd = -1)
        {
            double cur1 = base.MinCurrentValue(iStart, iEnd);
            double cur2 = Synapses != null && Synapses.Any() ? Synapses.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
            double cur3 = Terminals != null && Terminals.Any() ? Terminals.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
            return Math.Min(Math.Min(cur1, cur2), cur3);
        }
        public override double MaxCurrentValue(int iStart = 0, int iEnd = -1)
        {
            double cur1 = base.MaxCurrentValue(iStart, iEnd);
            double cur2 = Synapses != null && Synapses.Any() ? Synapses.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
            double cur3 = Terminals != null && Terminals.Any() ? Terminals.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
            return Math.Max(Math.Max(cur1, cur2), cur3);
        }

        public override List<int> GetSpikeIndices(int iStart = 0, int iEnd = -1)
        {
            return V.GetPeakIndices(Core.Vmax, iStart, iEnd);
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