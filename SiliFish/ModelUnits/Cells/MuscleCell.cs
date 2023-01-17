using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
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
        [JsonIgnore]
        public double R { get { return (Core as Leaky_Integrator).R; } set { } }
        [JsonIgnore]
        public double[] RelativeTension { get { return (Core as Leaky_Integrator).CalculateRelativeTension(V); } }
        [JsonIgnore]
        public double[] Tension { get { return (Core as Leaky_Integrator).CalculateTension(V); } }
        public override double RestingMembranePotential { get { return (Core?.Vr) ?? 0; } }

        [JsonIgnore]
        public List<ChemicalSynapse> EndPlates { get; set; } //keeps the list of all synapses targeting the current cell

        public MuscleCell()
        {
            GapJunctions = new();
            EndPlates = new();
        }
        /// <summary>
        /// Used as a template
        /// </summary>
        public MuscleCell(string coreType, string group, int somite, int seq, Dictionary<string, double> cellParams, TimeLine timeline = null)
        {
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            Core = CellCoreUnit.CreateCore(coreType, cellParams);
            EndPlates = new List<ChemicalSynapse>();
            GapJunctions = new List<GapJunction>();
            TimeLine_ms = timeline;
        }
        public MuscleCell(CellPoolTemplate cellTemp, int somite, int seq, Dictionary<string, double> cellParams)
            : this(cellTemp.CoreType, cellTemp.CellGroup, somite, seq, cellParams, cellTemp.TimeLine_ms)
        {
        }

        /// <summary>
        /// Constructor called from predefined models
        /// </summary>
        public MuscleCell(string group, int seq, double R, double C, double Vr, double sigma_dyn, Coordinate coor, TimeLine timeline = null)
        {
            CellGroup = group;
            Sequence = seq;
            PositionLeftRight = coor.Y < 0 ? SagittalPlane.Left : SagittalPlane.Right;
            if (sigma_dyn > 0)
            {
                R *= RunningModel.rand.Gauss(1, sigma_dyn);
                C *= RunningModel.rand.Gauss(1, sigma_dyn);
            }
            Core = new Leaky_Integrator(R, C, Vr);
            EndPlates = new List<ChemicalSynapse>();
            GapJunctions = new List<GapJunction>();
            coordinate = coor;
            TimeLine_ms = timeline;
        }

        public override double MinCurrentValue(int iStart = 0, int iEnd = -1)
        {
            double cur1 = base.MinCurrentValue(iStart, iEnd);
            double cur2 = EndPlates?.Count > 0 ? EndPlates.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
            return Math.Min(cur1, cur2);
        }
        public override double MaxCurrentValue(int iStart = 0, int iEnd = -1)
        {
            double cur1 = base.MaxCurrentValue(iStart, iEnd);
            double cur2 = EndPlates?.Count > 0 ? EndPlates.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
            return Math.Max(cur1, cur2);
        }

        public override List<int> GetSpikeIndices(int iStart = 0, int iEnd = -1)
        {
            return V.GetPeakIndices(Core.Vr * 1.1, iStart, iEnd);//URGENT this is a random multiplier 
        }

        public override void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            EndPlates.Add(jnc);
        }

        public override (double, double) GetConnectionRange()
        {
            (double minWeight1, double maxWeight1) = base.GetConnectionRange();
            double maxWeight2 = EndPlates.Select(j => j.Conductance).DefaultIfEmpty(0).Max();
            double minWeight2 = EndPlates.Where(j => j.Conductance > 0).Select(j => j.Conductance).DefaultIfEmpty(999).Min();

            double maxWeight = Math.Max(maxWeight1, maxWeight2);
            double minWeight = Math.Min(minWeight1, minWeight2);
            return (minWeight, maxWeight);
        }

        public override void InitDataVectors(int nmax)
        {
            base.InitDataVectors(nmax);
            V[0] = Core.Vr;
            foreach (ChemicalSynapse jnc in this.EndPlates)
                jnc.InitDataVectors(nmax);
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
                stim = GetStimulus(timeIndex, RunningModel.rand);
            }
            NextStep(timeIndex, stim + ISyn + IGap);
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
    }

}
